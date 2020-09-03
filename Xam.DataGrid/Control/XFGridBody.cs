using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="XFGridBody" />.
    /// </summary>
    internal class XFGridBody : FlexLayout
    {
        #region Private_Properties

        /// <summary>
        /// Defines the _parent.
        /// </summary>
        private readonly XFDataGridControl _parent;

        /// <summary>
        /// Defines the _listView.
        /// </summary>
        private ListView _listView;

        #endregion

        #region Public_Internal_Properties

        /// <summary>
        /// Gets the BorderWidth.
        /// </summary>
        internal double BorderWidth
        {
            get
            {
                return _parent.GridBorderWidth;
            }
        }

        /// <summary>
        /// Gets a value indicating whether EnablePullToRefresh
        /// Gets or sets a value indicating whether EnablePullToRefresh..
        /// </summary>
        internal bool EnablePullToRefresh
        {
            get
            {
                return _parent.EnablePullToRefresh;
            }
        }

        /// <summary>
        /// Gets the RowHeight.
        /// </summary>
        internal double RowHeight
        {
            get
            {
                return _parent.GridRowHeight;
            }
        }

        /// <summary>
        /// Gets the GridItemSource.
        /// </summary>
        internal IList<object> GridItemSource
        {
            get
            {
                return _parent.DataSource;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="XFGridBody"/> class.
        /// </summary>
        /// <param name="control">The control<see cref="XFDataGridControl"/>.</param>
        internal XFGridBody(XFDataGridControl control)
        {
            _parent = control;
            Direction = FlexDirection.Column;
        }

        #endregion

        #region Private_Methods

        /// <summary>
        /// The RefreshData.
        /// </summary>
        private void RefreshData()
        {
            var refobj = new XFPullToRefreshEventArgs();
            _parent.OnPullToRefreshEvent(refobj);
            foreach (var item in refobj.NewItems)
                _parent.DataSource.Add(item);
            _listView.ItemsSource = null;
            _listView.ItemsSource = _parent.DataSource;
        }

        /// <summary>
        /// The TapGestureRecognizer_Tapped.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var bc = (sender as View).BindingContext;
            XFItemClickEventArgs args = new XFItemClickEventArgs(bc);
            _parent.OnItemSelectEvent(sender, args);
        }

        /// <summary>
        /// The GetRowGrid.
        /// </summary>
        /// <returns>The <see cref="Grid"/>.</returns>
        private Grid GetRowGrid()
        {
            var grid = new Grid { RowSpacing = BorderWidth, ColumnSpacing = BorderWidth };
            grid.ColumnDefinitions = new ColumnDefinitionCollection();
            var count = _parent.ColumnsSource != null ? _parent.ColumnsSource.Count : XFGridHelper.GetPropCount(GridItemSource);
            for (int i = 0; i < count; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }
            grid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = RowHeight },
                new RowDefinition{ Height = 5 }
            };
            return grid;
        }

        /// <summary>
        /// The ApplyItemTextStyle.
        /// </summary>
        /// <param name="label">The label<see cref="Label"/>.</param>
        private void ApplyItemTextStyle(Label label)
        {
            if (_parent.GridItemStyle != null)
            {
                label.TextColor = _parent.GridItemStyle.TextColor;
                label.HorizontalTextAlignment = _parent.GridItemStyle.HorizontalTextAlignment;
                label.VerticalTextAlignment = _parent.GridItemStyle.VerticalTextAlignment;
                label.FontSize = _parent.GridItemStyle.FontSize;
                label.FontAttributes = _parent.GridItemStyle.FontAttributes;
                label.FontFamily = _parent.GridItemStyle.FontFamily;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The RefreshBody.
        /// </summary>
        internal void RefreshBody()
        {
            this.Children.Clear();
            if (GridItemSource != null)
            {
                var template = CreateTemplate();
                _listView = new ListView { ItemsSource = GridItemSource, ItemTemplate = template };//, Margin
                this.Children.Add(_listView);
                PullToRefresh();
            }
        }

        /// <summary>
        /// The PullToRefresh.
        /// </summary>
        internal void PullToRefresh()
        {
            if (EnablePullToRefresh)
            {
                _listView.IsPullToRefreshEnabled = EnablePullToRefresh;
                _listView.RefreshCommand = new Command(() =>
                {
                    _listView.IsRefreshing = true;
                    RefreshData();
                    _listView.IsRefreshing = false;
                });
            }
        }

        /// <summary>
        /// The RefreshOnSort.
        /// </summary>
        internal void RefreshOnSort()
        {
            _listView.ItemsSource = null;
            _listView.ItemsSource = _parent.ItemsSource;
        }

        /// <summary>
        /// The LoadPagerSource.
        /// </summary>
        /// <param name="paginationSource">The paginationSource<see cref="IList{object}"/>.</param>
        internal void LoadPagerSource(IList<object> paginationSource)
        {
            _listView.ItemsSource = null;
            _listView.ItemsSource = paginationSource;
        }

        /// <summary>
        /// The CreateTemplate.
        /// </summary>
        /// <returns>The <see cref="DataTemplate"/>.</returns>
        public DataTemplate CreateTemplate()
        {
            var listTemplate = new DataTemplate(() =>
            {
                var grid = GetRowGrid();
                grid.BackgroundColor = _parent.GridBorderColor;
                var column = 0;
                foreach (var headercolumn in _parent._gridHeader.Children)
                {
                    if (!(headercolumn is HeaderLabel _headerLabel)) break;
                    var propLabel = new Label();
                    propLabel.SetBinding(Label.TextProperty, _headerLabel.ColumnObj.PropertyName);
                    propLabel.BackgroundColor = _parent.GridBackgroundColor;
                    ApplyItemTextStyle(propLabel);
                    grid.Children.Add(propLabel, column, 0);
                    var tapGestureRecognizer = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 2,
                        CommandParameter = _headerLabel.ColumnObj.PropertyName
                    };
                    tapGestureRecognizer.Tapped -= TapGestureRecognizer_Tapped;
                    tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                    propLabel.GestureRecognizers.Add(tapGestureRecognizer);
                    column++;
                }
                BoxView bv = new BoxView { HeightRequest = 5, BackgroundColor = _parent.GridBorderColor };
                grid.Children.Add(bv, 0, 1);
                Grid.SetColumnSpan(bv, column);
                return new ViewCell { View = grid };
            });
            return listTemplate;
        }

        #endregion
    }
}
