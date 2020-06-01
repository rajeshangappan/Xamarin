using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamSample.AppHelper;
using XamSample.Views;

namespace XamSample.ViewModel
{
    /// <summary>
    /// Defines the <see cref="StoreMainPageViewModel" />.
    /// </summary>
    public class StoreMainPageViewModel
    {
        #region PUBLIC_PPTY

        /// <summary>
        /// Gets or sets a value indicating whether IsAdmin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets the LogoutCommand.
        /// </summary>
        public ICommand LogoutCommand => new Command(OnLogout);

        /// <summary>
        /// Gets the ProductClickCommand.
        /// </summary>
        public ICommand ProductClickCommand => new Command(async () => await OnProductClicked());

        /// <summary>
        /// Gets the StaffClickCommand.
        /// </summary>
        public ICommand StaffClickCommand => new Command(async () => await OnStaffClicked());

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreMainPageViewModel"/> class.
        /// </summary>
        public StoreMainPageViewModel()
        {
            IsAdmin = SampleHelper.IsAdmin();
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The OnLogout.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnLogout(object obj)
        {
            //Logout API and pop this page
            Application.Current.Properties.Clear();
            Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// The OnProductClicked.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task OnProductClicked()
        {
            var vm = IocContainer.Resolve<ProductViewModel>();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductPage { BindingContext = vm });
        }

        /// <summary>
        /// The OnStaffClicked.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task OnStaffClicked()
        {
            var vm = IocContainer.Resolve<StaffPageViewModel>();
            await Application.Current.MainPage.Navigation.PushAsync(new StaffPage { BindingContext = vm });
        }

        #endregion
    }
}
