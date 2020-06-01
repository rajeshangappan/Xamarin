using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamSample.Contracts;
using XamSample.Implementations;
using XamSample.Models;
using XamSample.Models.DAO;

namespace XamSample.Services
{
    /// <summary>
    /// Defines the <see cref="ProductDBService" />.
    /// </summary>
    public class ProductDBService : IProductDBService
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _productItem.
        /// </summary>
        private readonly DbRepository<ProductDAO> _productItem;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDBService"/> class.
        /// </summary>
        /// <param name="database">The database<see cref="Database"/>.</param>
        public ProductDBService(Database database)
        {
            var tt = database.Connection.CreateTableAsync<ProductDAO>().GetAwaiter().GetResult();

            _productItem = new DbRepository<ProductDAO>(database.Connection);
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The AddProducts.
        /// </summary>
        /// <param name="product">The product<see cref="ProductDAO"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public async Task<int> AddProducts(ProductDAO product)
        {
            product.Id = Guid.NewGuid();
            return await _productItem.Insert(product);
        }

        /// <summary>
        /// The DeleteProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public async Task<int> DeleteProduct(Product product)
        {
            product.Version = 0;
            product.IsDeleted = true;
            product.UpdatedAt = DateTime.Now;
            return await _productItem.Update(Mapper.Map<ProductDAO>(product));
        }

        /// <summary>
        /// The GetOfflineProducts.
        /// </summary>
        /// <returns>The <see cref="Task{List{ProductDAO}}"/>.</returns>
        public async Task<List<ProductDAO>> GetOfflineProducts()
        {
            var result = await _productItem.Get(x => x.Version == 0);
            return result;
        }

        /// <summary>
        /// The GetProduct.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{Product}"/>.</returns>
        public async Task<Product> GetProduct(Guid id)
        {
            var result = await _productItem.Get(id);
            return Mapper.Map<Product>(result);
        }

        /// <summary>
        /// The GetProducts.
        /// </summary>
        /// <returns>The <see cref="Task{List{Product}}"/>.</returns>
        public async Task<List<Product>> GetProducts()
        {
            var result = await _productItem.Get();
            result = result.Where(x => x.IsDeleted == false).ToList();
            return Mapper.Map<List<Product>>(result);
        }

        /// <summary>
        /// The Updateproduct.
        /// </summary>
        /// <param name="product">The product<see cref="ProductDAO"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public async Task<int> Updateproduct(ProductDAO product)
        {
            product.Version = 0;
            product.UpdatedAt = DateTime.Now;
            var result = await _productItem.Update(product);
            return result;
        }

        #endregion
    }
}
