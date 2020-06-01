using StoreAPI.Contracts;
using StoreAPI.Models;
using System;
using System.Collections.Generic;

namespace StoreAPI.ServiceImpl
{
    /// <summary>
    /// Defines the <see cref="SyncServiceImpl" />.
    /// </summary>
    public class SyncServiceImpl : ISyncService
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _productService.
        /// </summary>
        private IProductService _productService;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncServiceImpl"/> class.
        /// </summary>
        /// <param name="productService">The productService<see cref="IProductService"/>.</param>
        public SyncServiceImpl(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The addProd.
        /// </summary>
        /// <param name="prod">The prod<see cref="Product"/>.</param>
        private void addProd(Product prod)
        {
            _productService.AddProducts(prod);
        }

        /// <summary>
        /// The deleteProd.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        private void deleteProd(Product product)
        {
            _productService.DeleteProduct(product);
        }

        /// <summary>
        /// The Refresh.
        /// </summary>
        /// <param name="offlineVersion">The offlineVersion<see cref="long"/>.</param>
        /// <returns>The <see cref="List{Product}"/>.</returns>
        private List<Product> Refresh(long offlineVersion)
        {
            return _productService.GetProductsByVersion(offlineVersion);
        }

        /// <summary>
        /// The updateProd.
        /// </summary>
        /// <param name="prod">The prod<see cref="Product"/>.</param>
        /// <param name="offProd">The offProd<see cref="Product"/>.</param>
        private void updateProd(Product prod, Product offProd)
        {
            if (offProd.IsDeleted)
            {
                deleteProd(offProd);
            }
            else
            {
                updateProductByTimeStamp(prod, offProd);
            }
        }

        /// <summary>
        /// The updateProductByTimeStamp.
        /// </summary>
        /// <param name="prod">The prod<see cref="Product"/>.</param>
        /// <param name="offProd">The offProd<see cref="Product"/>.</param>
        private void updateProductByTimeStamp(Product prod, Product offProd)
        {
            if (offProd.UpdatedAt > prod.UpdatedAt)
            {
                prod.Cost = offProd.Cost;
                prod.MaxLimit = offProd.MaxLimit;
                prod.AvailableQty = offProd.AvailableQty;
                _productService.Updateproduct(prod);
            }
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The SyncData.
        /// </summary>
        /// <param name="products">The products<see cref="List{Product}"/>.</param>
        /// <param name="offlineversion">The offlineversion<see cref="long"/>.</param>
        /// <returns>The <see cref="List{Product}"/>.</returns>
        public List<Product> SyncData(List<Product> products, long offlineversion)
        {
            try
            {
                foreach (var product in products)
                {
                    var prod = _productService.GetProduct(product.Id);
                    if (prod != null)
                    {
                        updateProd(prod, product);
                    }
                    else
                    {
                        addProd(product);
                    }
                }
                return Refresh(offlineversion);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
