using System.Threading.Tasks;

namespace XamSample.Contracts
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="ILoginService" />.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// The Login.
        /// </summary>
        /// <param name="username">The username<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> Login(string username, string password);
    }

    #endregion
}
