using StoreAPI.AppDBContext;
using StoreAPI.Contracts;
using StoreAPI.Models;
using System.Linq;

namespace StoreAPI.ServiceImpl
{
    /// <summary>
    /// Defines the <see cref="UserServiceImpl" />.
    /// </summary>
    public class UserServiceImpl : IUserService
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _storeDbContext.
        /// </summary>
        private readonly StoreDBContext _storeDbContext;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="UserServiceImpl"/> class.
        /// </summary>
        /// <param name="storeDBContext">The storeDBContext<see cref="StoreDBContext"/>.</param>
        public UserServiceImpl(StoreDBContext storeDBContext)
        {
            _storeDbContext = storeDBContext;
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The GetUser.
        /// </summary>
        /// <param name="username">The username<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="User"/>.</returns>
        public User GetUser(string username, string password)
        {
            return _storeDbContext.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        #endregion
    }
}
