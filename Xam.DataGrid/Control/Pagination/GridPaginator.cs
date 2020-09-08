using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="GridPaginator" />.
    /// </summary>
    internal class GridPaginator : Grid
    {
        #region Private_Properties

        /// <summary>
        /// Defines the _defaultPaginationLabel.
        /// </summary>
        private IList<XFPageIndex> _defaultPaginationLabel;

        /// <summary>
        /// Defines the _parent.
        /// </summary>
        private XFDataGridControl _parent;

        #endregion

        #region Public_Internal_Properties

        /// <summary>
        /// Gets the ShowRecordPerPages.
        /// </summary>
        internal int ShowRecordPerPages
        {
            get
            {
                return _parent == null ? 0 : _parent.ShowRecordPerPages;
            }
        }

        /// <summary>
        /// Gets the TotalPage.
        /// </summary>
        internal int TotalPage
        {
            get
            {
                return _parent == null ? 0 : (int)Math.Ceiling((float)_parent.RecordCount / _parent.ShowRecordPerPages); ;
            }
        }

        /// <summary>
        /// Gets or sets the SelectedIndex.
        /// </summary>
        internal int SelectedIndex { get; set; }

        /// <summary>
        /// Gets or sets the CurrentPagerIndex.
        /// </summary>
        public int CurrentPagerIndex { get; set; }

        /// <summary>
        /// Defines the PaginationBody.
        /// </summary>
        internal StackLayout PaginationBody;

        /// <summary>
        /// Defines the ShowingIndexs.
        /// </summary>
        internal IList<int> ShowingIndexs = new List<int>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GridPaginator"/> class.
        /// </summary>
        /// <param name="control">The control<see cref="XFDataGridControl"/>.</param>
        public GridPaginator(XFDataGridControl control)
        {
            _parent = control;
            PaginationBody = new StackLayout { Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.Center };
            PaginationBody.Margin = 10;
            InitializeIndex();
            this.Children.Add(PaginationBody);
            BackgroundColor = Color.FromHex("#D3D3D3");
        }

        #endregion

        #region Private_Methods

        /// <summary>
        /// The InitializeIndex.
        /// </summary>
        private void InitializeIndex()
        {
            _defaultPaginationLabel = new List<XFPageIndex>
            {
                CreateLabel("|<","|<"),
                CreateLabel("<","<"),
                CreateLabel(">", ">"),
                CreateLabel(">|",">|")
            };
        }

        /// <summary>
        /// The CreateLabel.
        /// </summary>
        /// <param name="text">The text<see cref="string"/>.</param>
        /// <param name="symbol">The symbol<see cref="string"/>.</param>
        /// <returns>The <see cref="XFPageIndex"/>.</returns>
        private XFPageIndex CreateLabel(string text, string symbol = "")
        {
            var index = new XFPageIndex();
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += TapGesture_Tapped;
            tapGesture.CommandParameter = symbol;
            index.GestureRecognizers.Add(tapGesture);
            index.Text = text;
            return index;
        }

        /// <summary>
        /// The TapGesture_Tapped.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void TapGesture_Tapped(object sender, EventArgs e)
        {
            var text = (sender as XFPageIndex).Text;
            if (string.IsNullOrEmpty(text))
                return;

            if (text == "|<")
            {
                MoveFirstPage();
            }
            else if (text == "<")
            {
                MovePreviousPage();
            }
            else if (text == ">|")
            {
                MoveLastPage();
            }
            else if (text == ">")
            {
                MoveNextPage();
            }
            else
            {
                SelectPage((sender as XFPageIndex).Text);
            }
        }

        /// <summary>
        /// The MoveNextPage.
        /// </summary>
        private void MoveNextPage()
        {
            var newindex = SelectedIndex + 1;
            if (ShowingIndexs.Contains(newindex))
            {
                LoadData(newindex);
            }
            else
            {
                CurrentPagerIndex += 1;
                LoadPager(newindex);
            }
        }

        /// <summary>
        /// The SelectPage.
        /// </summary>
        /// <param name="text">The text<see cref="string"/>.</param>
        private void SelectPage(string text)
        {
            if (int.TryParse(text, out int index) && ShowingIndexs.Contains(index))
            {
                SelectedIndex = index;
                LoadData(SelectedIndex);
            }
        }

        /// <summary>
        /// The MoveLastPage.
        /// </summary>
        private void MoveLastPage()
        {
            if (ShowingIndexs.Contains(TotalPage))
                LoadData(TotalPage);
            else
            {
                CurrentPagerIndex = (int)Math.Ceiling((float)TotalPage / 3);
                LoadPager(TotalPage);
            }
        }

        /// <summary>
        /// The MovePreviousPage.
        /// </summary>
        private void MovePreviousPage()
        {
            var newindex = SelectedIndex - 1;
            if (ShowingIndexs.Contains(newindex))
            {
                LoadData(newindex);
            }
            else
            {
                CurrentPagerIndex -= 1;
                LoadPager(newindex);
            }
        }

        /// <summary>
        /// The LoadPager.
        /// </summary>
        /// <param name="newIndex">The newIndex<see cref="int"/>.</param>
        private void LoadPager(int newIndex)
        {
            UpdateIndexes();
            SelectedIndex = newIndex;
            LoadData(SelectedIndex);
        }

        /// <summary>
        /// The UpdateIndexes.
        /// </summary>
        private void UpdateIndexes()
        {
            var startIndex = ((CurrentPagerIndex - 1) * 3) + 1;
            ShowingIndexs.Clear();
            for (int k = 0; k < 3; k++)
            {
                if (startIndex + k <= TotalPage)
                {
                    ShowingIndexs.Add(startIndex + k);
                    (PaginationBody.Children[k + 2] as XFPageIndex).IsVisible = true;
                    (PaginationBody.Children[k + 2] as XFPageIndex).Text = (startIndex + k).ToString();
                }
                else
                {
                    (PaginationBody.Children[k + 2] as XFPageIndex).IsVisible = false;
                }
            }
        }

        /// <summary>
        /// The GetPageSource.
        /// </summary>
        /// <returns>The <see cref="IList{object}"/>.</returns>
        private IList<object> GetPageSource()
        {
            if (_parent.EnableVirtualPagination)
            {
                int startindex = 0;
                int endindex = startindex + ShowRecordPerPages <= _parent.DataSource.Count ?
                                startindex + ShowRecordPerPages : _parent.DataSource.Count;
                return _parent.DataSource.GetRange(startindex, endindex - startindex);
            }
            else
            {
                int startindex = (SelectedIndex - 1) * ShowRecordPerPages;
                int endindex = startindex + ShowRecordPerPages <= _parent.DataSource.Count ?
                                startindex + ShowRecordPerPages : _parent.DataSource.Count;
                return _parent.DataSource.GetRange(startindex, endindex - startindex);
            }
        }

        /// <summary>
        /// The HighlightIndex.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        private void HighlightIndex(int index)
        {
            for (int i = 0; i < ShowingIndexs.Count; i++)
            {
                (PaginationBody.Children[i + 2] as XFPageIndex).BackgroundColor = Color.Default;
                if (ShowingIndexs[i] == index)
                    (PaginationBody.Children[i + 2] as XFPageIndex).BackgroundColor = Color.Yellow;
            }
        }

        /// <summary>
        /// The MoveFirstPage.
        /// </summary>
        private void MoveFirstPage()
        {
            if (ShowingIndexs.Contains(1))
                LoadData(1);
            else
            {
                CurrentPagerIndex = 1;
                LoadPager(1);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The LoadData.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        internal void LoadData(int index)
        {
            SelectedIndex = index;
            HighlightIndex(index);
            _parent.OnNeedDataSourceEvent();
            _parent._gridItemBody.LoadPagerSource(GetPageSource());
            UpdatePager();
        }

        /// <summary>
        /// The CreatePagination.
        /// </summary>
        internal void CreatePagination()
        {
            if (_parent.ItemsSource != null)
            {
                PaginationBody.Children.Add(_defaultPaginationLabel[0]);
                PaginationBody.Children.Add(_defaultPaginationLabel[1]);
                for (int i = 0; i < 3; i++)
                {
                    if (i < TotalPage)
                    {
                        PaginationBody.Children.Add(CreateLabel($"{i + 1}"));
                        ShowingIndexs.Add(i + 1);
                        SelectedIndex = 1;
                    }
                }
                PaginationBody.Children.Add(_defaultPaginationLabel[2]);
                PaginationBody.Children.Add(_defaultPaginationLabel[3]);
                CurrentPagerIndex = 1;
                LoadData(SelectedIndex);
            }
        }

        /// <summary>
        /// The UpdatePager.
        /// </summary>
        internal void UpdatePager()
        {
            if (SelectedIndex == 1)
            {
                _defaultPaginationLabel[0].IsEnabled = false;
                _defaultPaginationLabel[1].IsEnabled = false;
            }
            else
            {
                _defaultPaginationLabel[0].IsEnabled = true;
                _defaultPaginationLabel[1].IsEnabled = true;
            }
            if (SelectedIndex == TotalPage)
            {
                _defaultPaginationLabel[2].IsEnabled = false;
                _defaultPaginationLabel[3].IsEnabled = false;
            }
            else
            {
                _defaultPaginationLabel[2].IsEnabled = true;
                _defaultPaginationLabel[3].IsEnabled = true;
            }
        }

        #endregion
    }
}
