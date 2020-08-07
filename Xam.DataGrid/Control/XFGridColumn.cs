using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.DataGrid.Control
{
    public class XFGridColumn
    {
        internal string ColumnName { get; set; }

        internal SortType ColumnSortType { get; set; }
    }

    public enum SortType
    {
        None,
        Ascending,
        Descending
    }
}
