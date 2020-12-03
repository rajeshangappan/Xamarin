using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="XFGridHeader" />.
    /// </summary>
    internal class XFGridHeader : Grid
    {
        #region Private_Properties

        /// <summary>
        /// Defines the _parent.
        /// </summary>
        private XFDataGridControl _parent;

        /// <summary>
        /// Gets the ColumnSource.
        /// </summary>
        private List<XFGridColumn> ColumnSource
        {
            get
            {
                return _parent.ColumnsSource;
            }
        }

        /// <summary>
        /// Gets the DataSource.
        /// </summary>
        private IList<object> DataSource
        {
            get
            {
                return _parent.DataSource;
            }
        }

        /// <summary>
        /// Gets the BorderColor.
        /// </summary>
        private Color BorderColor
        {
            get
            {
                return _parent.GridBorderColor;
            }
        }

        /// <summary>
        /// Gets the HeaderColor.
        /// </summary>
        private Color HeaderColor
        {
            get
            {
                return _parent.HeaderColor;
            }
        }

        /// <summary>
        /// Gets the HeaderHeight.
        /// </summary>
        private double HeaderHeight
        {
            get
            {
                return _parent.HeaderHeight;
            }
        }

        /// <summary>
        /// Gets the HeaderHeight.
        /// </summary>
        private double HeaderSelectorHeight
        {
            get
            {
                return _parent.HeaderSelectorHeight;
            }
        }

        /// <summary>
        /// Gets the GridHelper.
        /// </summary>
        private XFGridHelper GridHelper
        {
            get
            {
                return _parent._gridHelper;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="XFGridHeader"/> class.
        /// </summary>
        /// <param name="xFDataGrid">The xFDataGrid<see cref="XFDataGridControl"/>.</param>
        public XFGridHeader(XFDataGridControl xFDataGrid)
        {
            _parent = xFDataGrid;
            RowSpacing = 0;
            ColumnSpacing = xFDataGrid.GridBorderWidth;
        }

        #endregion

        #region Private_Methods

        /// <summary>
        /// The CreateGridHeader.
        /// </summary>
        private void CreateGridHeader()
        {
            BackgroundColor = BorderColor;
            var count = ColumnSource != null ? ColumnSource.Count : XFGridHelper.GetPropCount(DataSource);
            this.ColumnDefinitions = new ColumnDefinitionCollection();
            for (int i = 0; i < count; i++)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }
            this.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = HeaderHeight },
                new RowDefinition{ Height = HeaderSelectorHeight }
            };
            if (ColumnSource == null || ColumnSource.Count == 0)
                CreateDeafultHeader();
            else
                CreateGridColumnHeader();
            BoxView v = new BoxView { BackgroundColor = _parent.GridBorderColor };
            this.Children.Add(v, 0, 1);
            SetColumnSpan(v, count);
        }

        /// <summary>
        /// The CreateGridColumnHeader.
        /// </summary>
        private void CreateGridColumnHeader()
        {
            for (int i = 0; i < ColumnSource.Count; i++)
            {
                this.Children.Add(GetHeaderLabel(ColumnSource[i]), i, 0);
            }
        }

        /// <summary>
        /// The CreateDeafultHeader.
        /// </summary>
        private void CreateDeafultHeader()
        {
            var column = 0;
            foreach (var prop in DataSource[0].GetType().GetProperties())
            {
                if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    var gridcolumn = new XFGridColumn
                    {
                        DisplayName = prop.Name,
                        PropertyName = prop.Name
                    };
                    this.Children.Add(GetHeaderLabel(gridcolumn), column, 0);
                    column++;
                }
            }
        }

        /// <summary>
        /// The GetHeaderLabel.
        /// </summary>
        /// <param name="gridcolumn">The gridcolumn<see cref="XFGridColumn"/>.</param>
        /// <returns>The <see cref="Label"/>.</returns>
        private Label GetHeaderLabel(XFGridColumn gridcolumn)
        {
            var propLabel = new HeaderLabel
            {
                BackgroundColor = HeaderColor,
                Text = gridcolumn.PropertyName,
                FontAttributes = FontAttributes.Bold,
                ColumnObj = gridcolumn
            };
            ApplyHeaderTextStyle(propLabel);
            var sortGesture = new TapGestureRecognizer
            {
                CommandParameter = gridcolumn
            };
            sortGesture.Tapped -= SortGesture_Tapped;
            sortGesture.Tapped += SortGesture_Tapped;
            propLabel.GestureRecognizers.Add(sortGesture);
            return propLabel;
        }

        /// <summary>
        /// The ApplyHeaderTextStyle.
        /// </summary>
        /// <param name="label">The label<see cref="HeaderLabel"/>.</param>
        private void ApplyHeaderTextStyle(HeaderLabel label)
        {
            if (_parent.GridHeaderStyle != null)
            {
                label.TextColor = _parent.GridHeaderStyle.TextColor;
                label.HorizontalTextAlignment = _parent.GridHeaderStyle.HorizontalTextAlignment;
                label.VerticalTextAlignment = _parent.GridHeaderStyle.VerticalTextAlignment;
                label.FontSize = _parent.GridHeaderStyle.FontSize;
                label.FontAttributes = _parent.GridHeaderStyle.FontAttributes;
                label.FontFamily = _parent.GridHeaderStyle.FontFamily;
            }
        }

        /// <summary>
        /// The SortGesture_Tapped.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void SortGesture_Tapped(object sender, EventArgs e)
        {
            _parent.OnNeedDataSourceEvent();
            if ((e as TappedEventArgs).Parameter is XFGridColumn gridCol)
            {
                var sorttype = (gridCol.ColumnSortType == SortType.None || gridCol.ColumnSortType == SortType.Descending) ? SortType.Ascending : SortType.Descending;
                gridCol.ColumnSortType = sorttype;
                var result = GridHelper.SortList(sorttype, gridCol.PropertyName, DataSource as List<object>);
                _parent.RefreshSorting(result);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The RefreshHeader.
        /// </summary>
        internal void RefreshHeader()
        {
            if (DataSource != null)
                CreateGridHeader();
        }

        #endregion
    }
}
