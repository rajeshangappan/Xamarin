using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// The OnItemClickEventHandler.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="args">The args<see cref="XFItemClickEventArgs"/>.</param>
    public delegate void OnItemClickEventHandler(object sender, XFItemClickEventArgs args);

    /// <summary>
    /// The OnPullToRefreshEventHandler.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="args">The args<see cref="XFPullToRefreshEventArgs"/>.</param>
    public delegate void OnPullToRefreshEventHandler(object sender, XFPullToRefreshEventArgs args);

    /// <summary>
    /// The OnNeedDataEventHandler.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="args">The args<see cref="XFNeedDataSourceEventArgs"/>.</param>
    public delegate void OnNeedDataEventHandler(object sender, XFNeedDataSourceEventArgs args);

    /// <summary>
    /// Defines the <see cref="XFDataGridControl" />.
    /// </summary>
    public partial class XFDataGridControl : Frame, INotifyPropertyChanged
    {
        #region Public_Internal_Properties

        /// <summary>
        /// Gets the DataSource.
        /// </summary>
        internal List<object> DataSource { get; private set; }

        /// <summary>
        /// Gets the RecordCount.
        /// </summary>
        internal int RecordCount
        {
            get
            {
                if (EnableVirtualPagination)
                    return this.VirtualRecordCount;
                return DataSource == null ? 0 : DataSource.Count;
            }
        }

        /// <summary>
        /// Gets or sets the ColumnsSource.
        /// </summary>
        public List<XFGridColumn> ColumnsSource
        {
            get { return (List<XFGridColumn>)GetValue(ColumnsSourceProperty); }
            set { SetValue(ColumnsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether EnablePagination.
        /// </summary>
        public bool EnablePagination
        {
            get { return (bool)GetValue(EnablePaginationProperty); }
            set { SetValue(EnablePaginationProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether EnablePullToRefresh.
        /// </summary>
        public bool EnablePullToRefresh
        {
            get { return (bool)GetValue(EnablePullToRefreshProperty); }
            set { SetValue(EnablePullToRefreshProperty, value); }
        }

        /// <summary>
        /// Gets or sets the GridHeaderStyle.
        /// </summary>
        public GridStyle GridHeaderStyle
        {
            get { return (GridStyle)GetValue(GridHeaderStyleProperty); }
            set { SetValue(GridHeaderStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the GridItemStyle.
        /// </summary>
        public GridStyle GridItemStyle
        {
            get { return (GridStyle)GetValue(GridItemStyleProperty); }
            set { SetValue(GridItemStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the GridRowHeight.
        /// </summary>
        public double GridRowHeight
        {
            get { return (double)GetValue(GridRowHeightProperty); }
            set { SetValue(GridRowHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether EnableVirtualPagination.
        /// </summary>
        public bool EnableVirtualPagination
        {
            get { return (bool)GetValue(EnableVirtualPaginationProperty); }
            set { SetValue(GridRowHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the VirtualRecordCount.
        /// </summary>
        public int VirtualRecordCount
        {
            get { return (int)GetValue(VirtualRecordCountProperty); }
            set { SetValue(GridRowHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the HeaderHeight.
        /// </summary>
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ItemsSource.
        /// </summary>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the PaginatorHeight.
        /// </summary>
        public double PaginatorHeight
        {
            get { return (double)GetValue(PaginatorHeightProperty); }
            set { SetValue(PaginatorHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ShowRecordPerPages.
        /// </summary>
        public int ShowRecordPerPages
        {
            get { return (int)GetValue(ShowRecordPerPagesProperty); }
            set { SetValue(ShowRecordPerPagesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the GridBackgroundColor.
        /// </summary>
        public Color GridBackgroundColor
        {
            get { return (Color)GetValue(GridBackgroundColorProperty); }
            set { SetValue(GridBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the GridBorderColor.
        /// </summary>
        public Color GridBorderColor
        {
            get { return (Color)GetValue(GridBorderColorProperty); }
            set { SetValue(GridBorderColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the GridBorderWidth.
        /// </summary>
        public double GridBorderWidth
        {
            get { return (double)GetValue(GridBorderWidthProperty); }
            set { SetValue(GridBorderWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the GridBorderWidth.
        /// </summary>
        public double HeaderSelectorHeight
        {
            get { return (double)GetValue(HeaderSelectorHeightProperty); }
            set { SetValue(HeaderSelectorHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the HeaderColor.
        /// </summary>
        public Color HeaderColor
        {
            get { return (Color)GetValue(HeaderColorProperty); }
            set { SetValue(HeaderColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the HeaderColor.
        /// </summary>
        public Color PaginatorBackgroundColor
        {
            get { return (Color)GetValue(PaginatorBackgroundColorProperty); }
            set { SetValue(PaginatorBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Defines the IsLoaded.
        /// </summary>
        internal bool IsLoaded;

        /// <summary>
        /// Defines the EnableVirtualPaginationProperty.
        /// </summary>
        public static readonly BindableProperty EnableVirtualPaginationProperty = BindableProperty.Create (nameof(EnableVirtualPagination), typeof(bool), typeof(XFDataGridControl), false);

        /// <summary>
        /// Defines the VirtualRecordCountProperty.
        /// </summary>
        public static readonly BindableProperty VirtualRecordCountProperty = BindableProperty.Create (nameof(VirtualRecordCount), typeof(int), typeof(XFDataGridControl), 0);

        /// <summary>
        /// Defines the ColumnsSourceProperty.
        /// </summary>
        public static readonly BindableProperty ColumnsSourceProperty = BindableProperty.Create (nameof(ColumnsSource), typeof(List<XFGridColumn>), typeof(XFDataGridControl), null);

        /// <summary>
        /// Defines the EnablePaginationProperty.
        /// </summary>
        public static readonly BindableProperty EnablePaginationProperty = BindableProperty.Create (nameof(EnablePagination), typeof(bool), typeof(XFDataGridControl), false);

        /// <summary>
        /// Defines the EnablePullToRefreshProperty.
        /// </summary>
        public static readonly BindableProperty EnablePullToRefreshProperty = BindableProperty.Create (nameof(EnablePullToRefresh), typeof(bool), typeof(XFDataGridControl), false);

        /// <summary>
        /// Defines the GridHeaderStyleProperty.
        /// </summary>
        public static readonly BindableProperty GridHeaderStyleProperty = BindableProperty.Create (nameof(GridHeaderStyle), typeof(GridStyle), typeof(XFDataGridControl), null);

        /// <summary>
        /// Defines the GridItemStyleProperty.
        /// </summary>
        public static readonly BindableProperty GridItemStyleProperty = BindableProperty.Create (nameof(GridItemStyle), typeof(GridStyle), typeof(XFDataGridControl), null);

        /// <summary>
        /// Defines the GridRowHeightProperty.
        /// </summary>
        public static readonly BindableProperty GridRowHeightProperty = BindableProperty.Create (nameof(GridRowHeight), typeof(double), typeof(XFDataGridControl), 50.0);

        /// <summary>
        /// Defines the HeaderHeightProperty.
        /// </summary>
        public static readonly BindableProperty HeaderHeightProperty = BindableProperty.Create (nameof(HeaderHeight), typeof(double), typeof(XFDataGridControl), 50.0);

        /// <summary>
        /// Defines the PaginatorHeightProperty.
        /// </summary>
        public static readonly BindableProperty PaginatorHeightProperty = BindableProperty.Create (nameof(PaginatorHeight), typeof(double), typeof(XFDataGridControl), 50.0);

        /// <summary>
        /// Defines the ShowRecordPerPagesProperty.
        /// </summary>
        public static readonly BindableProperty ShowRecordPerPagesProperty = BindableProperty.Create (nameof(ShowRecordPerPages), typeof(int), typeof(XFDataGridControl), 10);

        /// <summary>
        /// Defines the _gridHeader.
        /// </summary>
        internal XFGridHeader _gridHeader;

        /// <summary>
        /// Defines the _gridHelper.
        /// </summary>
        internal XFGridHelper _gridHelper;

        /// <summary>
        /// Defines the _gridItemBody.
        /// </summary>
        internal XFGridBody _gridItemBody;

        /// <summary>
        /// Defines the _gridPaginator.
        /// </summary>
        internal GridPaginator _gridPaginator;

        /// <summary>
        /// Defines the _mParent.
        /// </summary>
        internal Grid _mParent;

        /// <summary>
        /// Defines the GridBackgroundColorProperty.
        /// </summary>
        public static readonly BindableProperty GridBackgroundColorProperty = BindableProperty.Create (nameof(GridBackgroundColor), typeof(Color), typeof(XFDataGridControl), Color.White);

        /// <summary>
        /// Defines the GridBorderColorProperty.
        /// </summary>
        public static readonly BindableProperty GridBorderColorProperty = BindableProperty.Create (nameof(GridBorderColor), typeof(Color), typeof(XFDataGridControl), Color.FromHex("#A9A9A9"));

        /// <summary>
        /// Defines the GridBorderWidthProperty.
        /// </summary>
        public static readonly BindableProperty GridBorderWidthProperty = BindableProperty.Create (nameof(GridBorderWidth), typeof(double), typeof(XFDataGridControl), 1.0);

        /// <summary>
        /// Defines the GridBorderWidthProperty.
        /// </summary>
        public static readonly BindableProperty HeaderSelectorHeightProperty = BindableProperty.Create (nameof(HeaderSelectorHeight), typeof(double), typeof(XFDataGridControl), 5.0);

        /// <summary>
        /// Defines the HeaderColorProperty.
        /// </summary>
        public static readonly BindableProperty HeaderColorProperty = BindableProperty.Create (nameof(HeaderColor), typeof(Color), typeof(XFDataGridControl), Color.FromHex("#D3D3D3"));

        /// <summary>
        /// Defines the HeaderColorProperty.
        /// </summary>
        public static readonly BindableProperty PaginatorBackgroundColorProperty = BindableProperty.Create (nameof(PaginatorBackgroundColor), typeof(Color), typeof(XFDataGridControl), Color.FromHex("#D3D3D3"));

        /// <summary>
        /// Defines the ItemsSourceProperty.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create (nameof(ItemsSource), typeof(IList), typeof(XFDataGridControl), null);

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="XFDataGridControl"/> class.
        /// </summary>
        public XFDataGridControl()
        {
            _mParent = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            _gridHeader = new XFGridHeader(this);
            _gridItemBody = new XFGridBody(this);
            _gridHelper = new XFGridHelper();
            this.PropertyChanged += XFDataGridControl_PropertyChanged;
        }

        #endregion

        #region Private_Methods

        /// <summary>
        /// The GridLayout.
        /// </summary>
        private void GridLayout()
        {
            Padding = 0;
            Margin = 0;
            _mParent.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition{Height= new GridLength(HeaderHeight + HeaderSelectorHeight , GridUnitType.Absolute) },
                new RowDefinition{Height= GridLength.Star }
            };

            _mParent.Children.Add(_gridHeader, 0, 0);
            _mParent.Children.Add(_gridItemBody, 0, 1);
            if (EnablePagination)
            {
                _gridPaginator = new GridPaginator(this);
                _mParent.RowDefinitions.Add(new RowDefinition { Height = new GridLength(PaginatorHeight + 5, GridUnitType.Absolute) });
                _mParent.Children.Add(_gridPaginator, 0, 2);
            }
            RefreshGrid();
        }

        /// <summary>
        /// The XFDataGridControl_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void XFDataGridControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemsSource))
            {
                DataSource = (sender as XFDataGridControl).ItemsSource.ToListObject<object>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The OnItemSelectEvent.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="args">The args<see cref="XFItemClickEventArgs"/>.</param>
        internal void OnItemSelectEvent(object sender, XFItemClickEventArgs args)
        {
            OnItemSelect?.Invoke(sender, args);
        }

        /// <summary>
        /// The OnPullToRefreshEvent.
        /// </summary>
        /// <param name="args">The args<see cref="XFPullToRefreshEventArgs"/>.</param>
        internal void OnPullToRefreshEvent(XFPullToRefreshEventArgs args)
        {
            OnPullToRefresh?.Invoke(this, args);
        }

        /// <summary>
        /// The OnPullToRefreshEvent.
        /// </summary>
        internal void OnNeedDataSourceEvent()
        {
            if (OnNeedDataSource != null && IsLoaded)
            {
                XFNeedDataSourceEventArgs args = new XFNeedDataSourceEventArgs();
                args.CurrentPageIndex = _gridPaginator.SelectedIndex;
                OnNeedDataSource.Invoke(this, args);
                ItemsSource = args.ItemSource;
            }
        }

        /// <summary>
        /// The RefreshGrid.
        /// </summary>
        internal void RefreshGrid()
        {
            _gridHeader.RefreshHeader();
            _gridItemBody.RefreshBody();
            if (EnablePagination)
            {
                _gridPaginator.CreatePagination();
            }
        }

        /// <summary>
        /// The RefreshSorting.
        /// </summary>
        /// <param name="source">The source<see cref="IList{object}"/>.</param>
        internal void RefreshSorting(IList<object> source)
        {
            ItemsSource = (IList)source;
            if (EnablePagination)
                _gridPaginator.LoadData(_gridPaginator.SelectedIndex);
            else
                _gridItemBody.RefreshOnSort();
        }

        /// <summary>
        /// The Refresh.
        /// </summary>
        public void Refresh()
        {
            GridLayout();
            Content = _mParent;
        }

        /// <summary>
        /// The OnMeasure.
        /// </summary>
        /// <param name="widthConstraint">The widthConstraint<see cref="double"/>.</param>
        /// <param name="heightConstraint">The heightConstraint<see cref="double"/>.</param>
        /// <returns>The <see cref="SizeRequest"/>.</returns>
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (!IsLoaded)
                Refresh();
            IsLoaded = true;
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        #endregion

        /// <summary>
        /// Defines the OnItemSelect.
        /// </summary>
        public event OnItemClickEventHandler OnItemSelect;

        /// <summary>
        /// Defines the OnPullToRefresh.
        /// </summary>
        public event OnPullToRefreshEventHandler OnPullToRefresh;

        /// <summary>
        /// Defines the OnNeedDataSource.
        /// </summary>
        public event OnNeedDataEventHandler OnNeedDataSource;
    }
}
