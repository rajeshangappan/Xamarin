using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace Xam.DataGrid.Control
{
    public delegate void OnItemClickEventHandler(object sender, XFItemClickEventArgs args);

    public class XFDataGridControl : Frame
    {
        private XFGridHeader _gridHeader;

        internal Grid _mParent;

        private FlexLayout _itemBody;

        public Color HeaderColor = Color.FromHex("e1e1e1");

        public Color GridBorderColor = Color.Gray;

        public Color GridBackgroundColor = Color.White;

        public double GridBorderWidth = 2;

        public event OnItemClickEventHandler OnItemEdit;

        internal XFGridHelper _gridHelper;

        public XFDataGridControl()
        {
            _mParent = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            _gridHeader = new XFGridHeader { RowSpacing = 0, ColumnSpacing = GridBorderWidth };
            _itemBody = new FlexLayout { Direction = FlexDirection.Column, BackgroundColor = Color.Gray };
            _gridHelper = new XFGridHelper();  
        }

        public void Refresh()
        {
            GridLayout();
            Content = _mParent;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var bc = (sender as View).BindingContext;
            XFItemClickEventArgs args = new XFItemClickEventArgs(bc);
            OnItemEdit?.Invoke(sender, args);
        }

        internal IList<object> _ItemsSource;

       // internal IList<object> GridItemSource { get; set; }

        public IList ItemsSource
        {
            get
            {
                return _ItemsSource as IList;
            }
            set
            {
                var input = value;
                var listitem = XFGridHelper.ConvertToListOf<object>(input);
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
            _mParent.Children.Add(_itemBody, 0, 1);
            RefreshGrid();
        }

        public void RefreshGrid()
        {
            CreateGridHeader();
            CreateDataGridRow();
        }

        private void CreateGridHeader()
        {
            _gridHeader.BackgroundColor = GridBorderColor;
            var count = GetPropCount();
            _gridHeader.ColumnDefinitions = new ColumnDefinitionCollection();
            for (int i = 0; i < count; i++)
            {
                _gridHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }
            _gridHeader.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = HeaderHeight },
                new RowDefinition{ Height = 5 }
            };
            if (ColumnsSource == null)
                CreateDeafultHeader();
            BoxView v = new BoxView { BackgroundColor = BorderColor };
            _gridHeader.Children.Add(v, 0, 1);
            Grid.SetColumnSpan(v, count);
        }

        private void CreateDataGridRow()
        {
            var template = CreateTemplate();
            var listview = new ListView { ItemsSource = ItemsSource, ItemTemplate = template };//, Margin = new Thickness(0, 20, 0, 0) };
            _itemBody.Children.Add(listview);
        }

        private void CreateDeafultHeader()
        {
            var column = 0;
            foreach (var prop in ItemsSource[0].GetType().GetProperties())
            {
                var propLabel = new Label();
                propLabel.BackgroundColor = HeaderColor;
                propLabel.Text = prop.Name;
                propLabel.FontAttributes = FontAttributes.Bold;
                var sortGesture = new TapGestureRecognizer();
                sortGesture.Tapped -= SortGesture_Tapped;
                sortGesture.Tapped += SortGesture_Tapped;
                propLabel.GestureRecognizers.Add(sortGesture);
                _gridHeader.Children.Add(propLabel, column, 0);
                column++;
            }
        }

        private void SortGesture_Tapped(object sender, EventArgs e)
        {
            var result = _gridHelper.Sort_List<object>("Asending", "Age", _ItemsSource as List<object>);            
        }

        public DataTemplate CreateTemplate()
        {
            var listTemplate = new DataTemplate(() =>
            {
                var grid = GetRowGrid();
                var obj = ItemsSource[0];
                grid.BackgroundColor = GridBorderColor;
                var column = 0;
                foreach (var prop in obj.GetType().GetProperties())
                {
                    if(prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                    {
                        var propLabel = new Label();
                        propLabel.SetBinding(Label.TextProperty, prop.Name);
                        propLabel.BackgroundColor = GridBackgroundColor;
                        grid.Children.Add(propLabel, column, 0);
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.NumberOfTapsRequired = 2;
                        tapGestureRecognizer.CommandParameter = prop.Name;
                        tapGestureRecognizer.Tapped -= TapGestureRecognizer_Tapped;
                        tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                        propLabel.GestureRecognizers.Add(tapGestureRecognizer);
                        column++;
                    }
                }
                BoxView bv = new BoxView { HeightRequest = 5, BackgroundColor = GridBorderColor };
                grid.Children.Add(bv, 0, 1);
                Grid.SetColumnSpan(bv, column);

                return new ViewCell { View = grid };
            });
            return listTemplate;
        }

        private Grid GetRowGrid()
        {
            var grid = new Grid { RowSpacing = GridBorderWidth, ColumnSpacing = GridBorderWidth };
            grid.ColumnDefinitions = new ColumnDefinitionCollection();
            var count = GetPropCount();
            for (int i = 0; i < count; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }
            grid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridRowHeight },
                new RowDefinition{Height = 5}
            };
            return grid;
        }

        private int GetPropCount()
        {
            var count = 0;
            foreach (var prop in ItemsSource[0].GetType().GetProperties())
            {
                if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    count++;
                }
            }
            return count;

        }
    }
}








































