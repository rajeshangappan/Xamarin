using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamSample.Views
{
    /// <summary>
    /// Defines the <see cref="LoginPage" />.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The Button_Clicked.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Button_Clicked(object sender, EventArgs e)
        {
        }

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
