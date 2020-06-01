using StoreAPI.Models;
using System.Collections.Generic;

namespace StoreAPI.Contracts
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="ISyncService" />.
    /// </summary>
    public interface ISyncService
    {
        /// <summary>
        /// The SyncData.
        /// </summary>
        /// <param name="products">The products<see cref="List{Product}"/>.</param>
        /// <param name="offlineversion">The offlineversion<see cref="long"/>.</param>
        /// <returns>The <see cref="List{Product}"/>.</returns>
        List<Product> SyncData(List<Product> products, long offlineversion);
    }

    #endregion
}
