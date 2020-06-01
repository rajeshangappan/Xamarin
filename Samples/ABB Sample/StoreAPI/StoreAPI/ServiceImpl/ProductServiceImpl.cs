using StoreAPI.AppDBContext;
using StoreAPI.Contracts;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreAPI.ServiceImpl
{
    /// <summary>
    /// Defines the <see cref="ProductServiceImpl" />.
    /// </summary>
    public class ProductServiceImpl : IProductService
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _storeDbContext.
        /// </summary>
        private readonly StoreDBContext _storeDbContext;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServiceImpl"/> class.
        /// </summary>
        /// <param name="storeDBContext">The storeDBContext<see cref="StoreDBContext"/>.</param>
        public ProductServiceImpl(StoreDBContext storeDBContext)
        {
            _storeDbContext = storeDBContext;
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The updateProdVersion.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        private void updateProdVersion(Product product)
        {
            product.Version = _storeDbContext.Products.Max(x => x.Version) + 1;
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The AddProducts.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AddProducts(Product product)
        {
            if (string.IsNullOrEmpty(product.Id.ToString()))
            {
                var IsExist = _storeDbContext.Products.FirstOrDefault(x => x.ProdName == product.ProdName);
                if (IsExist == null)
                {
                    product.UpdatedAt = DateTime.Now;
                    updateProdVersion(product);
                    _storeDbContext.Add(product);
                    _storeDbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// The DeleteProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool DeleteProduct(Product product)
        {
            product.IsDeleted = true;
            updateProdVersion(product);
            _storeDbContext.Products.Update(product);
            _storeDbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// The GetProduct.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Product"/>.</returns>
        public Product GetProduct(Guid id)
        {
            return _storeDbContext.Products.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// The GetProducts.
        /// </summary>
        /// <returns>The <see cref="List{Product}"/>.</returns>
        public List<Product> GetProducts()
        {
            return _storeDbContext.Products.ToList();
        }

        /// <summary>
        /// The GetProductsByVersion.
        /// </summary>
        /// <param name="offlineVersion">The offlineVersion<see cref="long"/>.</param>
        /// <returns>The <see cref="List{Product}"/>.</returns>
        public List<Product> GetProductsByVersion(long offlineVersion)
        {
            var products = _storeDbContext.Products.Where(x => x.Version > offlineVersion);
            return products.ToList();
        }

        /// <summary>
        /// The Updateproduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Updateproduct(Product product)
        {
            var prod = _storeDbContext.Products.FirstOrDefault(x => x.Id == product.Id);
            if (prod != null)
            {
                prod.Cost = product.Cost;
                prod.MaxLimit = product.MaxLimit;
                prod.AvailableQty = product.AvailableQty;
                updateProdVersion(product);
                _storeDbContext.Products.Update(prod);
                return true;
            }
            return false;
        }

        #endregion
    }
}
