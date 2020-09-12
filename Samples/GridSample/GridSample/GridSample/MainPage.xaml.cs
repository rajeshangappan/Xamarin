using System;
using System.Collections;
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
        public string testing { get; set; } = "testing";
        public MainPage()
        {
            
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        internal void CreateGrid()
        {
            var model = new MainPageViewModel();
            XFDataGridControl control = new XFDataGridControl();
            control.ItemsSource = model.Itemsource;
            control.ColumnsSource = (List<XFGridColumn>)model.GridColumns;
            control.EnablePagination = true;
            control.ShowRecordPerPages = 12;
        }

        private void Grid_OnPullToRefresh(object sender, XFPullToRefreshEventArgs args)
        {
            IList tt = new emp[]
            {
                new emp {  name = "test",age=89, id = "test" + 89 },

            }.ToList();
            for (int k = 0; k < 3; k++)
            {
                tt.Add(new emp { name = "newitem" + k, age = 20 + k + 10, id = "upee" + k });
            }
            args.NewItems = tt;
        }

        private void XFDataGridControl_OnNeedDataSource(object sender, XFNeedDataSourceEventArgs args)
        {
            var src = new emp[]
           {
                new emp { name = "datasource", age = 38, id = "sam" + 1 }

           }.ToList();

            for (int i = 2; i < 12; i++)
            {
                src.Add(new emp { name = "datasource " + i, age = i + 20, id = "sam" + i });
            }
            args.ItemSource = src;
        }

        private void XFDataGridControl_OnItemSelect(object sender, XFItemClickEventArgs args)
        {

        }
    }

    public class emp
    {
        public string name { get; set; }
        public int age { get; set; }
        public string id { get; set; }
    }
}
