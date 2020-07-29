using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.DataGrid.Control
{
    public class XFItemClickEventArgs
    {
        public XFItemClickEventArgs(object obj)
        {
            Item = obj;
        }

        public Object Item { get; set; }
    }
}
