using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamSample.AppHelper;
using XamSample.Contracts;
using XamSample.Models;

namespace XamSample.Implementations
{
    /// <summary>
    /// Defines the <see cref="LoginService" />.
    /// </summary>
    public class LoginService : ILoginService
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _apiRepository.
        /// </summary>
        private readonly IApiRepository _apiRepository;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        /// <param name="apiRepository">The apiRepository<see cref="IApiRepository"/>.</param>
        public LoginService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The TempUserCheck.
        /// </summary>
        /// <param name="username">The username<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        private async Task<bool> TempUserCheck(string username, string password)
        {
            Dictionary<string, string> users = new Dictionary<string, string>
            {
                { "user1", "test" },
                { "admin1", "test" },
                { "admin2", "test" }
            };
            if (users.ContainsKey(username) && users[username] == password)
            {
                if (username.Contains("admin"))
                {
                    await Xamarin.Essentials.SecureStorage.SetAsync("role", "admin");
                }
                else
                {
                    await Xamarin.Essentials.SecureStorage.SetAsync("role", "user");
                }
                return true;
            }
            return false;
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The Login.
        /// </summary>
        /// <param name="username">The username<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> Login(string username, string password)
        {
            var URI = AppConstants.LogInUrl;

            if (!AppConstants.UseLocal)
            {
                try
                {
                    var result = await _apiRepository.PostAsync<LoginResponse>(URI, new User { Username = username, Password = password });

                    if (result != null)
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("username", username);
                        await Xamarin.Essentials.SecureStorage.SetAsync("password", password);
                        await Xamarin.Essentials.SecureStorage.SetAsync("role", result.User.Role);
                        Xamarin.Forms.Application.Current.Properties["token"] = result.Token;                        
                        return true;
                    }
                    else
                    {
                        return await TempUserCheck(username, password);
                        //check username and password for offline. If user available return true.
                    }
                }
                catch (Exception)
                {
                    // log exception
                    return await TempUserCheck(username, password);
                }
            }
            else
            {
                return await TempUserCheck(username, password);
            }
        }

        #endregion
    }
}
