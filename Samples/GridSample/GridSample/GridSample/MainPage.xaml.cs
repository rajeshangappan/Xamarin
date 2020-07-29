using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.DataGrid.Control;
using Xamarin.Forms;

namespace GridSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var source = new emp[]
            {
                new emp {  Name = "Smith",Age=89 },
                new emp {  Name = "John", Age=11 }
            }.ToList();

            for (int k = 0; k < 43; k++)
            {
                var test = new[]
            {
                new emp {  Name = "Smith",Age=20 + k + 10 },
                new emp {  Name = "John", Age=200 + k + 10 }
            }.ToList();
                source.AddRange(test.ToList());
            }



            var grid = new XFDataGridControl
            {
                ItemsSource = source,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            // grid.refres();
            grid.OnItemEdit += Grid_OnItemEdit;
            stack.Children.Add(grid);
        }

        private void Grid_OnItemEdit(object sender, XFItemClickEventArgs args)
        {
            var obj = args.Item as emp;
            obj.Name = "testing";
        }
    }

    public class emp
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
