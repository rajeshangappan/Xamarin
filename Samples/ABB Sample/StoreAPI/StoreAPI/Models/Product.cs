using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreAPI.Models
{
    /// <summary>
    /// Defines the <see cref="Product" />.
    /// </summary>
    [Table("Product")]
    public class Product
    {
        #region PUBLIC_PPTY

        /// <summary>
        /// Gets or sets the AvailableQty.
        /// </summary>
        public int AvailableQty { get; set; }

        /// <summary>
        /// Gets or sets the Cost.
        /// </summary>
        public Double Cost { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the MaxLimit.
        /// </summary>
        public int MaxLimit { get; set; }

        /// <summary>
        /// Gets or sets the ProdName.
        /// </summary>
        public string ProdName { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedAt.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        public Int64 Version { get; set; }

        #endregion
    }
}
