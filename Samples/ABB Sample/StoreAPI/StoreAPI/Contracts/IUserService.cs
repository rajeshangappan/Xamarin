using StoreAPI.Models;

namespace StoreAPI.Contracts
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IUserService" />.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// The GetUser.
        /// </summary>
        /// <param name="username">The username<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="User"/>.</returns>
        User GetUser(string username, string password);
    }

    #endregion
}
