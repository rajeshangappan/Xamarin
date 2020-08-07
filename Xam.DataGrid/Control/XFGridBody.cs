using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    internal class XFGridBody : FlexLayout
    {
        private readonly XFDataGridControl _parent;
        private ListView _listView;
        
        internal XFGridBody(XFDataGridControl control)
        {
            _parent = control;
            Direction = FlexDirection.Column;
            BackgroundColor = Color.Gray;
        }
        internal double BorderWidth
        {
            get
            {
                return _parent.GridBorderWidth;
            }
        }

        internal double RowHeight
        {
            get
            {
                return _parent.GridRowHeight;
            }
        }

        internal IList GridItemSource
        {
            get
            {
                return _parent.ItemsSource;
            }
        }

        internal void RefreshBody()
        {
            this.Children.Clear();
            var template = CreateTemplate();
            _listView = new ListView { ItemsSource = GridItemSource, ItemTemplate = template };//, Margin = new Thickness(0, 20, 0, 0) };
            this.Children.Add(_listView);
        }

        internal void RefreshOnSort()
        {
            _listView.ItemsSource = null;
            _listView.ItemsSource = _parent.ItemsSource;           
        }

        public DataTemplate CreateTemplate()
        {
            var listTemplate = new DataTemplate(() =>
            {
                var grid = GetRowGrid();
                var obj = GridItemSource[0];
                grid.BackgroundColor = _parent.GridBorderColor;
                var column = 0;
                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                    {
                        var propLabel = new Label();
                        propLabel.SetBinding(Label.TextProperty, prop.Name);
                        propLabel.BackgroundColor = _parent.GridBackgroundColor;
                        grid.Children.Add(propLabel, column, 0);
                        var tapGestureRecognizer = new TapGestureRecognizer
                        {
                            NumberOfTapsRequired = 2,
                            CommandParameter = prop.Name
                        };
                        tapGestureRecognizer.Tapped -= TapGestureRecognizer_Tapped;
                        tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                        propLabel.GestureRecognizers.Add(tapGestureRecognizer);
                        column++;
                    }
                }
                BoxView bv = new BoxView { HeightRequest = 5, BackgroundColor = _parent.GridBorderColor };
                grid.Children.Add(bv, 0, 1);
                Grid.SetColumnSpan(bv, column);
                return new ViewCell { View = grid };
            });
            return listTemplate;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var bc = (sender as View).BindingContext;
            XFItemClickEventArgs args = new XFItemClickEventArgs(bc);
            _parent.OnEditEvent(sender, args);
        }

        private Grid GetRowGrid()
        {
            var grid = new Grid { RowSpacing = BorderWidth, ColumnSpacing = BorderWidth };
            grid.ColumnDefinitions = new ColumnDefinitionCollection();
            var count = XFGridHelper.GetPropCount(GridItemSource);
            for (int i = 0; i < count; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }
            grid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = RowHeight },
                new RowDefinition{Height = 5}
            };
            return grid;
        }
    }
}
