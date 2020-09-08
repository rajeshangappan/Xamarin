using System;
using System.Collections;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="XFItemClickEventArgs" />.
    /// </summary>
    public class XFItemClickEventArgs
    {
        #region Public_Internal_Properties

        /// <summary>
        /// Gets or sets the Item.
        /// </summary>
        public Object Item { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="XFItemClickEventArgs"/> class.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        public XFItemClickEventArgs(object obj)
        {
            Item = obj;
        }

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="XFPullToRefreshEventArgs" />.
    /// </summary>
    public class XFPullToRefreshEventArgs
    {
        #region Public_Internal_Properties

        /// <summary>
        /// Gets or sets the NewItems.
        /// </summary>
        public IList NewItems { get; set; }

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="XFNeedDataSourceEventArgs" />.
    /// </summary>
    public class XFNeedDataSourceEventArgs
    {
        #region Public_Internal_Properties

        /// <summary>
        /// Gets or sets the ItemSource.
        /// </summary>
        public IList ItemSource { get; set; }

        /// <summary>
        /// Defines the CurrentPageIndex.
        /// </summary>
        public int CurrentPageIndex;

        #endregion
    }
}
