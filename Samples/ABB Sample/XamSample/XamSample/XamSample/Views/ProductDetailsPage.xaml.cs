using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamSample.Views
{
    /// <summary>
    /// Defines the <see cref="ProductDetailsPage" />.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsPage : ContentPage
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailsPage"/> class.
        /// </summary>
        public ProductDetailsPage()
        {
            InitializeComponent();
        }

        #endregion
    }
}
