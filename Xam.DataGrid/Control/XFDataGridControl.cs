using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    public delegate void OnItemClickEventHandler(object sender, XFItemClickEventArgs args);

    public class XFDataGridControl : Frame, INotifyPropertyChanged
    {
        private XFGridHeader _gridHeader;

        internal Grid _mParent;

        private XFGridBody _gridItemBody;

        public Color HeaderColor = Color.FromHex("e1e1e1");

        public Color GridBorderColor = Color.Gray;

        public Color GridBackgroundColor = Color.White;

        public double GridBorderWidth = 2;

        public event OnItemClickEventHandler OnItemEdit;

        internal XFGridHelper _gridHelper;

        public XFDataGridControl()
        {
            _mParent = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            _gridHeader = new XFGridHeader(this);
            _gridItemBody = new XFGridBody(this);
            _gridHelper = new XFGridHelper();
        }

        public void Refresh()
        {
            GridLayout();
            Content = _mParent;
        }

        internal void OnEditEvent(object sender, XFItemClickEventArgs args)
        {
            OnItemEdit?.Invoke(sender, args);
        }

        internal IList<object> _ItemsSource;

        public IList ItemsSource
        {
            get
            {
                return _ItemsSource as IList;
            }
            set
            {
                var listitem = XFGridHelper.ConvertToListOf<object>(value);
                _ItemsSource = listitem;
            }
        }
        public IList ColumnsSource { get; set; }
        public double HeaderHeight { get; private set; } = 50;
        public double GridRowHeight { get; set; } = 50;
        protected override void OnParentSet()
        {
            Refresh();
        }

        private void GridLayout()
        {
            Padding = 0;
            Margin = 0;
            _mParent.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition{Height= new GridLength(HeaderHeight + 5 , GridUnitType.Absolute) },
                new RowDefinition{Height= GridLength.Star }
            };
            _mParent.Children.Add(_gridHeader, 0, 0);
            _mParent.Children.Add(_gridItemBody, 0, 1);
            RefreshGrid();
        }

        public void RefreshGrid()
        {
            _gridHeader.RefreshHeader();
            _gridItemBody.RefreshBody();
        }

        internal void RefreshSorting(IList<object> source)
        {
            _ItemsSource = source;
            _gridItemBody.RefreshOnSort();
        }
    }
}








































