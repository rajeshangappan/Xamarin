using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamSample.AppHelper;
using XamSample.Contracts;
using XamSample.Models;
using XamSample.Models.DAO;

namespace XamSample.Services
{
    /// <summary>
    /// Defines the <see cref="ProductService" />.
    /// </summary>
    public class ProductService : IProductService
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _apiRepository.
        /// </summary>
        private readonly IApiRepository _apiRepository;

        /// <summary>
        /// Defines the _productDBService.
        /// </summary>
        private IProductDBService _productDBService;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="apiRepository">The apiRepository<see cref="IApiRepository"/>.</param>
        /// <param name="productDBService">The productDBService<see cref="IProductDBService"/>.</param>
        public ProductService(IApiRepository apiRepository, IProductDBService productDBService)
        {
            _apiRepository = apiRepository;
            _productDBService = productDBService;
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The AddProducts.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> AddProducts(Product product)
        {
            var URI = AppConstants.AddProductUrl;

            try
            {
                var current = Connectivity.NetworkAccess;
                Product result = null;
                if (current != NetworkAccess.Internet || AppConstants.UseLocal)
                {
                    var locresult = await _productDBService.AddProducts(Mapper.Map<ProductDAO>(product));
                    return locresult == 1;
                }
                else
                {
                    result = await _apiRepository.PostAsync<Product>(URI, product);
                }
                return !(result == null);
            }
            catch (Exception)
            {
                // log exception
            }

            return false;
        }

        /// <summary>
        /// The DeleteProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> DeleteProduct(Product product)
        {
            var URI = AppConstants.DeleteProductUrl;
            try
            {
                var current = Connectivity.NetworkAccess;
                Product result = null;
                if (current != NetworkAccess.Internet || AppConstants.UseLocal)
                {
                    var locresult = await _productDBService.DeleteProduct(product);
                    return locresult == 1;
                }
                else
                {
                    result = await _apiRepository.PostAsync<Product>(URI, product);
                }
                return !(result == null);
            }
            catch (Exception)
            {
                // log exception
            }
            return false;
        }

        /// <summary>
        /// The GetProduct.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{Product}"/>.</returns>
        public async Task<Product> GetProduct(Guid id)
        {
            var URI = AddParameterToURI(AppConstants.ProductUrl, "Id", id);
            try
            {
                var current = Connectivity.NetworkAccess;
                Product result = null;
                if (current != NetworkAccess.Internet || AppConstants.UseLocal)
                {
                    result = await _productDBService.GetProduct(id);
                }
                else
                {
                    result = await _apiRepository.GetAsync<Product>("URI");
                }
                return result;
            }
            catch (Exception)
            {
                // log exception
            }
            return null;
        }

        /// <summary>
        /// The GetProducts.
        /// </summary>
        /// <returns>The <see cref="Task{List{Product}}"/>.</returns>
        public async Task<List<Product>> GetProducts()
        {
            var URI = AppConstants.ProductsUrl;
            try
            {
                var current = Connectivity.NetworkAccess;
                List<Product> result = new List<Product>();
                if (current != NetworkAccess.Internet || AppConstants.UseLocal)
                {
                    result = await _productDBService.GetProducts();
                }
                else
                {
                    result = await _apiRepository.GetAsync<List<Product>>(URI);
                }
                return result;
            }
            catch (Exception)
            {
                // log exception                
            }
            return null;
        }

        /// <summary>
        /// The Updateproduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> Updateproduct(Product product)
        {
            var URI = AppConstants.UpdateProductUrl;
            try
            {
                var current = Connectivity.NetworkAccess;
                Product result = null;
                if (current != NetworkAccess.Internet || AppConstants.UseLocal)
                {
                    var locresult = await _productDBService.Updateproduct(Mapper.Map<ProductDAO>(product));
                    return locresult == 1;
                }
                else
                {
                    result = await _apiRepository.PostAsync<Product>(URI, product);
                }
                return !(result == null);
            }
            catch (Exception)
            {
                // log exception
            }
            return false;
        }

        #endregion

        /// <summary>
        /// The AddParameterToURI.
        /// </summary>
        /// <param name="tempuri">The tempuri<see cref="string"/>.</param>
        /// <param name="paramName">The paramName<see cref="string"/>.</param>
        /// <param name="paramValue">The paramValue<see cref="object"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        protected string AddParameterToURI(string tempuri, string paramName, object paramValue)
        {
            StringBuilder URI = new StringBuilder(tempuri);
            if (!string.IsNullOrEmpty(paramValue?.ToString()))
            {
                if (URI.ToString().Contains("?"))
                {
                    return URI.Append("&" + paramName + $"={paramValue}").ToString();
                }
                else
                {
                    return URI.Append("?" + paramName + $"={paramValue}").ToString();
                }
            }
            return URI.ToString();
        }
    }
}
