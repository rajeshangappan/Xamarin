using StoreAPI.Models;
using System;
using System.Collections.Generic;

namespace StoreAPI.Contracts
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IProductService" />.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// The GetProduct.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Product"/>.</returns>
        Product GetProduct(Guid id);

        /// <summary>
        /// The GetProducts.
        /// </summary>
        /// <returns>The <see cref="List{Product}"/>.</returns>
        List<Product> GetProducts();

        /// <summary>
        /// The Updateproduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool Updateproduct(Product product);

        /// <summary>
        /// The AddProducts.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool AddProducts(Product product);

        /// <summary>
        /// The DeleteProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool DeleteProduct(Product product);

        /// <summary>
        /// The GetProductsByVersion.
        /// </summary>
        /// <param name="offlineVersion">The offlineVersion<see cref="long"/>.</param>
        /// <returns>The <see cref="List{Product}"/>.</returns>
        List<Product> GetProductsByVersion(long offlineVersion);
    }

    #endregion
}
