using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    internal class XFPageIndex : Label
    {
        internal XFPageIndex()
        {
            HeightRequest = 30;
            WidthRequest = 30;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment = TextAlignment.Center;
        }
    }
}
