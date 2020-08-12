using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    internal class GridPaginator : Grid
    {
        internal int ShowRecordPerPages
        {
            get
            {
                return _parent == null ? 0 : _parent.ShowRecordPerPages;
            }
        }

        internal int TotalPage
        {
            get
            {
                return _parent == null ? 0 : (int)Math.Ceiling((float)_parent.RecordCount / _parent.ShowRecordPerPages); ;
            }
        }

        internal StackLayout PaginationBody;
        private IList<XFPageIndex> _defaultPaginationLabel;
        private XFDataGridControl _parent;

        IList<int> ShowingIndexs = new List<int>();

        internal int SelectedIndex { get; set; }

        public int CurrentPagerIndex { get; set; }

        public GridPaginator(XFDataGridControl control)
        {
            _parent = control;
            PaginationBody = new StackLayout { Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.Center };
            PaginationBody.Margin = 10;
            InitializeIndex();
            this.Children.Add(PaginationBody);
        }

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

        private void SelectPage(string text)
        {
            if (int.TryParse(text, out int index) && ShowingIndexs.Contains(index))
            {
                SelectedIndex = index;
                LoadData(SelectedIndex);
            }
        }

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

        private void LoadPager(int newIndex)
        {
            UpdateIndexes();
            SelectedIndex = newIndex;
            LoadData(SelectedIndex);
        }

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

        internal void LoadData(int index)
        {
            SelectedIndex = index;
            HighlightIndex(index);
            _parent._gridItemBody.LoadPagerSource(GetPageSource());
            UpdatePager();
        }

        private IList<Object> GetPageSource()
        {
            int startindex = (SelectedIndex - 1) * ShowRecordPerPages;
            int endindex = startindex + ShowRecordPerPages <= _parent.ItemsSource.Count ?
                                startindex + ShowRecordPerPages : _parent.ItemsSource.Count;
            return (_parent._ItemsSource as List<object>).GetRange(startindex, endindex - startindex);
        }

        private void HighlightIndex(int index)
        {
            for (int i = 0; i < ShowingIndexs.Count; i++)
            {
                (PaginationBody.Children[i + 2] as XFPageIndex).BackgroundColor = Color.Default;
                if (ShowingIndexs[i] == index)
                    (PaginationBody.Children[i + 2] as XFPageIndex).BackgroundColor = Color.Yellow;
            }
        }

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

        internal void CreatePagination()
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
    }
}
