using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamSample.Views
{
    /// <summary>
    /// Defines the <see cref="StoreMainPage" />.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreMainPage : ContentPage
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreMainPage"/> class.
        /// </summary>
        public StoreMainPage()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// The OnBackButtonPressed.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
