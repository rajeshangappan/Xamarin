using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    public class XFGridHeader : Grid
    {
        private XFDataGridControl _parent;

        private List<XFGridColumn> xFGridColumns;

        public XFGridHeader(XFDataGridControl xFDataGrid)
        {
            _parent = xFDataGrid;
            RowSpacing = 0;
            ColumnSpacing = xFDataGrid.GridBorderWidth;
            xFGridColumns = new List<XFGridColumn>();
        }

        private IList ColumnSource
        {
            get
            {
                return _parent.ColumnsSource;
            }
        }

        private IList ItemSource
        {
            get
            {
                return _parent.ItemsSource;
            }
        }

        private Color BorderColor
        {
            get
            {
                return _parent.GridBorderColor;
            }
        }

        private Color HeaderColor
        {
            get
            {
                return _parent.HeaderColor;
            }
        }

        private double HeaderHeight
        {
            get
            {
                return _parent.HeaderHeight;
            }
        }
        private XFGridHelper GridHelper
        {
            get
            {
                return _parent._gridHelper;
            }
        }
        internal void RefreshHeader()
        {
            CreateGridHeader();
        }

        private void CreateGridHeader()
        {
            this.BackgroundColor = BorderColor;
            var count = XFGridHelper.GetPropCount(ItemSource);
            this.ColumnDefinitions = new ColumnDefinitionCollection();
            for (int i = 0; i < count; i++)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }
            this.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = HeaderHeight },
                new RowDefinition{ Height = 5 }
            };
            if (ColumnSource == null)
                CreateDeafultHeader();
            BoxView v = new BoxView { BackgroundColor = BorderColor };
            this.Children.Add(v, 0, 1);
            SetColumnSpan(v, count);
        }

        private void CreateDeafultHeader()
        {
            var column = 0;
            foreach (var prop in ItemSource[0].GetType().GetProperties())
            {
                if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    var gridcolumn = new XFGridColumn();
                    gridcolumn.ColumnName = prop.Name;
                    var propLabel = new Label
                    {
                        BackgroundColor = HeaderColor,
                        Text = prop.Name,
                        FontAttributes = FontAttributes.Bold
                    };
                    var sortGesture = new TapGestureRecognizer
                    {
                        CommandParameter = gridcolumn
                    };
                    sortGesture.Tapped -= SortGesture_Tapped;
                    sortGesture.Tapped += SortGesture_Tapped;
                    propLabel.GestureRecognizers.Add(sortGesture);
                    this.Children.Add(propLabel, column, 0);
                    column++;

                }
            }
        }

        private void SortGesture_Tapped(object sender, EventArgs e)
        {
            var gridCol = (e as TappedEventArgs).Parameter as XFGridColumn;
            if (gridCol != null)
            {
                var sorttype = (gridCol.ColumnSortType == SortType.None || gridCol.ColumnSortType == SortType.Descending) ? SortType.Ascending : SortType.Descending;
                gridCol.ColumnSortType = sorttype;
                var result = GridHelper.Sort_List<object>(sorttype, gridCol.ColumnName, ItemSource as List<object>);
                _parent.RefreshSorting(result);
            }
        }
    }
}
