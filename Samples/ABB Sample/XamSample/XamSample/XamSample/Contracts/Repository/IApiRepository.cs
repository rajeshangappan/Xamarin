using System.Threading.Tasks;

namespace XamSample.Contracts
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IApiRepository" />.
    /// </summary>
    public interface IApiRepository
    {
        /// <summary>
        /// The GetAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        Task<T> GetAsync<T>(string uri);

        /// <summary>
        /// The PostAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="object"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        Task<T> PostAsync<T>(string uri, object data);

        /// <summary>
        /// The DeleteAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="object"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        Task<T> DeleteAsync<T>(string uri, object data);
    }

    #endregion
}
