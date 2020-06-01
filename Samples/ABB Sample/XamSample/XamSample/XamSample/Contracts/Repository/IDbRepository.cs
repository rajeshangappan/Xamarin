using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace XamSample.Contracts
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IDbRepository{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IDbRepository<T> where T : class, new()
    {
        /// <summary>
        /// The Get.
        /// </summary>
        /// <returns>The <see cref="Task{List{T}}"/>.</returns>
        Task<List<T>> Get();

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        Task<T> Get(Guid id);

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        Task<int> Insert(T entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        Task<int> Delete(T entity);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        Task<int> Update(T entity);

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <returns>The <see cref="Task{List{T}}"/>.</returns>
        Task<List<T>> Get(Expression<Func<T, bool>> predicate);
    }

    #endregion
}
