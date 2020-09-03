using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="XFPageIndex" />.
    /// </summary>
    internal class XFPageIndex : Label
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="XFPageIndex"/> class.
        /// </summary>
        internal XFPageIndex()
        {
            HeightRequest = 30;
            WidthRequest = 30;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment = TextAlignment.Center;
        }

        #endregion
    }
}
