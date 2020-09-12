using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xam.DataGrid.Control;

namespace GridSample
{
    public class MainPageViewModel
    {
        public IList Itemsource { get; set; }

        public IList GridColumns { get; set; }

        public MainPageViewModel()
        {
            var src = new emp[]
           {
                new emp { name = "test", age = 38, id = "sam" + 1 }

           }.ToList();

            for (int i = 2; i < 100; i++)
            {
                src.Add(new emp { name = "test " + i, age = i + 20, id = "sam" + i });
            }
            Itemsource = src;

            GridColumns = new List<XFGridColumn>();
            GridColumns.Add(new XFGridColumn { DisplayName = "ID", PropertyName = "id" });
            GridColumns.Add(new XFGridColumn { DisplayName = "Name", PropertyName = "name" });
            GridColumns.Add(new XFGridColumn { DisplayName = "Age", PropertyName = "age" });
        }
    }
}
