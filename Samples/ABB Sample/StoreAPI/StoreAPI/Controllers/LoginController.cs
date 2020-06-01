using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Contracts;
using StoreAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StoreAPI.Controllers
{
    /// <summary>
    /// Defines the <see cref="LoginController" />.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _config.
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Defines the _userService.
        /// </summary>
        private IUserService _userService;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="config">The config<see cref="IConfiguration"/>.</param>
        /// <param name="userService">The userService<see cref="IUserService"/>.</param>
        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The AuthenticateUser.
        /// </summary>
        /// <param name="login">The login<see cref="User"/>.</param>
        /// <returns>The <see cref="User"/>.</returns>
        private User AuthenticateUser(User login)
        {
            return _userService.GetUser(login.Username, login.Password);
        }

        /// <summary>
        /// The GenerateJSONWebToken.
        /// </summary>
        /// <param name="userInfo">The userInfo<see cref="User"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The Login.
        /// </summary>
        /// <param name="login">The login<see cref="User"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]User login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        #endregion
    }
}
