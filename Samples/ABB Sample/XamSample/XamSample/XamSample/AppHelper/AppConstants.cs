using System;
using System.IO;

namespace XamSample.AppHelper
{
    /// <summary>
    /// Defines the <see cref="AppConstants" />.
    /// </summary>
    public class AppConstants
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the AddProductUrl.
        /// </summary>
        public static readonly string AddProductUrl = "https://localhost:44385/api/Store/aproducts";

        /// <summary>
        /// Defines the DeleteProductUrl.
        /// </summary>
        public static readonly string DeleteProductUrl = "https://localhost:44385/api/Store/dproducts";

        /// <summary>
        /// Defines the LogInUrl.
        /// </summary>
        public static readonly string LogInUrl = "https://localhost:44385/api/Login";

        /// <summary>
        /// Defines the ProductsUrl.
        /// </summary>
        public static readonly string ProductsUrl = "https://localhost:44385/api/Store/products";

        /// <summary>
        /// Defines the ProductUrl.
        /// </summary>
        public static readonly string ProductUrl = "https://localhost:44385/api/Store/product";

        /// <summary>
        /// Defines the SyncProductUrl.
        /// </summary>
        public static readonly string SyncProductUrl = "https://localhost:44385/api/Sync/syncdata";

        /// <summary>
        /// Defines the UpdateProductUrl.
        /// </summary>
        public static readonly string UpdateProductUrl = "https://localhost:44385/api/Store/uproducts";

        /// <summary>
        /// Defines the UseLocal.
        /// </summary>
        public static bool UseLocal = true;

        #endregion

        #region PUBLIC_PPTY

        /// <summary>
        /// Gets the DatabasePath.
        /// </summary>
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, "StoreSQLitesample.db3");
            }
        }

        #endregion
    }
}
