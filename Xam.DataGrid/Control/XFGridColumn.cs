namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="XFGridColumn" />.
    /// </summary>
    public class XFGridColumn
    {
        #region Public_Internal_Properties

        /// <summary>
        /// Gets or sets the DisplayName.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the PropertyName.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the PropertyType.
        /// </summary>
        public DataType PropertyType { get; set; }

        /// <summary>
        /// Gets or sets the ColumnSortType.
        /// </summary>
        internal SortType ColumnSortType { get; set; }

        #endregion
    }

    /// <summary>
    /// Defines the SortType.
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// Defines the None.
        /// </summary>
        None,
        /// <summary>
        /// Defines the Ascending.
        /// </summary>
        Ascending,
        /// <summary>
        /// Defines the Descending.
        /// </summary>
        Descending
    }

    /// <summary>
    /// Defines the DataType.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Defines the TextType.
        /// </summary>
        TextType,
        /// <summary>
        /// Defines the ImageURLType.
        /// </summary>
        ImageURLType
    }
}
