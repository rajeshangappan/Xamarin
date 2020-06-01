using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamSample.Views
{
    /// <summary>
    /// Defines the <see cref="ProductPage" />.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPage"/> class.
        /// </summary>
        public ProductPage()
        {
            InitializeComponent();
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The ContentPage_Appearing.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
        }

        #endregion
    }
}
