using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamSample.AppHelper;
using XamSample.Contracts;
using XamSample.Models;
using XamSample.Models.DAO;

namespace XamSample.Implementations.Services.Data.Sync
{
    /// <summary>
    /// Defines the <see cref="BackgroundSync" />.
    /// </summary>
    public class BackgroundSync
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _apiRepository.
        /// </summary>
        private readonly IApiRepository _apiRepository;

        /// <summary>
        /// Defines the _productDBService.
        /// </summary>
        private readonly IProductDBService _productDBService;

        /// <summary>
        /// Defines the _productItem.
        /// </summary>
        private readonly DbRepository<ProductDAO> _productItem;

        /// <summary>
        /// Defines the _productService.
        /// </summary>
        private readonly IProductService _productService;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundSync"/> class.
        /// </summary>
        /// <param name="productService">The productService<see cref="IProductService"/>.</param>
        /// <param name="productDBService">The productDBService<see cref="IProductDBService"/>.</param>
        /// <param name="apiRepository">The apiRepository<see cref="IApiRepository"/>.</param>
        public BackgroundSync(IProductService productService, IProductDBService productDBService, IApiRepository apiRepository)
        {
            _productService = productService;
            _productDBService = productDBService;
            _apiRepository = apiRepository;
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The SyncDataToOnline.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task SyncDataToOnline()
        {
            var oldProducts = await _productDBService.GetOfflineProducts();

            try
            {
                var prod = Mapper.Map<List<Product>>(oldProducts);
                var URI = AppConstants.SyncProductUrl;

                var result = await _apiRepository.PostAsync<List<Product>>(URI, prod);

                await SyncToOffline(result);

            }
            catch (Exception ex)
            {
                // Handling exception
            }
        }

        /// <summary>
        /// The SyncToOffline.
        /// </summary>
        /// <param name="onlineProd">The onlineProd<see cref="List{Product}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task SyncToOffline(List<Product> onlineProd)
        {
            var offTables = Mapper.Map<List<ProductDAO>>(onlineProd);

            foreach (var data in offTables)
            {
                var offentity = await _productDBService.GetProduct(data.Id);
                if (offentity != null)
                {
                    await _productDBService.Updateproduct(data);
                }
                else
                {
                    await _productDBService.AddProducts(data);
                }
            }
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The SyncData.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task SyncData()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet && !AppConstants.UseLocal)
            {
                await SyncDataToOnline();
            }
        }

        #endregion
    }
}
