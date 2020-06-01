using Microsoft.AspNetCore.Mvc;
using StoreAPI.Contracts;
using StoreAPI.Models;
using System.Collections.Generic;

namespace StoreAPI.Controllers
{
    /// <summary>
    /// Defines the <see cref="SyncController" />.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _syncService.
        /// </summary>
        private ISyncService _syncService;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncController"/> class.
        /// </summary>
        /// <param name="syncService">The syncService<see cref="ISyncService"/>.</param>
        public SyncController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The SyncData.
        /// </summary>
        /// <param name="products">The products<see cref="List{Product}"/>.</param>
        /// <param name="offVersion">The offVersion<see cref="long"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost("syncdata")]
        public IActionResult SyncData(List<Product> products, long offVersion)
        {
            var result = _syncService.SyncData(products, offVersion);
            return new OkObjectResult(result);
        }

        #endregion
    }
}
