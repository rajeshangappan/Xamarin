using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.DataGrid.Control
{
    public class XFGridColumn
    {
        public string DisplayName { get; set; }

        public string PropertyName { get; set; }

        public DataType PropertyType { get; set; }

        internal SortType ColumnSortType { get; set; }
    }

    public enum SortType
    {
        None,
        Ascending,
        Descending
    }

    public enum DataType
    {
        TextType,
        ImageURLType
    }
}
