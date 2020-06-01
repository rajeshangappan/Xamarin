using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XamSample.Contracts;

namespace XamSample.Implementations
{
    /// <summary>
    /// Defines the <see cref="DbRepository{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class DbRepository<T> : IDbRepository<T> where T : class, new()
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the db.
        /// </summary>
        private readonly SQLiteAsyncConnection db;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository{T}"/> class.
        /// </summary>
        /// <param name="db">The db<see cref="SQLiteAsyncConnection"/>.</param>
        public DbRepository(SQLiteAsyncConnection db)
        {
            this.db = db;
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public async Task<int> Delete(T entity)
        {
            return await db.DeleteAsync(entity);
        }

        /// <summary>
        /// The Get.
        /// </summary>
        /// <returns>The <see cref="Task{List{T}}"/>.</returns>
        public async Task<List<T>> Get()
        {
            try
            {
                return await db.Table<T>().ToListAsync();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <returns>The <see cref="Task{List{T}}"/>.</returns>
        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await db.FindAsync<List<T>>(predicate);
        }

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        public async Task<T> Get(Guid id)
        {
            return await db.FindAsync<T>(id);
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public async Task<int> Insert(T entity)
        {
            return await db.InsertAsync(entity);
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public async Task<int> Update(T entity)
        {
            try
            {
                return await db.UpdateAsync(entity);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion
    }
}
