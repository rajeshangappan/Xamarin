using AutoMapper;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamSample.AppHelper;
using XamSample.Automap;
using XamSample.Contracts;
using XamSample.Implementations;
using XamSample.Implementations.Services.Data.Sync;
using XamSample.Services;
using XamSample.ViewModel;
using XamSample.Views;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamSample
{
    /// <summary>
    /// Defines the <see cref="App" />.
    /// </summary>
    public partial class App : Application
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the syncInterval.
        /// </summary>
        private int syncInterval = 100;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
            RegisterTypes();
            var login = IocContainer.Resolve<LoginPageViewModel>();
            MainPage = new NavigationPage(new LoginPage { BindingContext = login });
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The AutoMapRegister.
        /// </summary>
        private void AutoMapRegister()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new ProfileMapper()));
        }

        /// <summary>
        /// The Connectivity_ConnectivityChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ConnectivityChangedEventArgs"/>.</param>
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                //backgroundService.SyncData();
            }
        }

        /// <summary>
        /// The RegisterTypes.
        /// </summary>
        private void RegisterTypes()
        {
            //IUnityContainer container = new UnityContainer();

            IocContainer.Register<IApiRepository, ApiRepository>();

            IocContainer.Register<IProductService, ProductService>();
            IocContainer.Register<ILoginService, LoginService>();
            IocContainer.RegisterInstance(Database.Instance);
            IocContainer.Register<HttpClientService>();

            IocContainer.RegisterSinglton<IProductDBService, ProductDBService>();

            IocContainer.Register<LoginPageViewModel>();
            IocContainer.Register<ProductViewModel>();
            IocContainer.Register<ProductDetailsPageViewModel>();
            IocContainer.Register<StoreMainPageViewModel>();
            IocContainer.Register<StaffPageViewModel>();

            IocContainer.Register<BackgroundSync>();

            AutoMapRegister();
        }

        #endregion

        /// <summary>
        /// The OnStart.
        /// </summary>
        protected override void OnStart()
        {
            var seconds = TimeSpan.FromSeconds(syncInterval);

            //Run the background tasks for sync
            Xamarin.Forms.Device.StartTimer(seconds, () =>
            {

                // Call SyncService
                var backgroundService = IocContainer.Resolve<BackgroundSync>();

                backgroundService.SyncData();

                // Returning true means you want to repeat this timer
                return true;
            });
        }

        /// <summary>
        /// The OnSleep.
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// The OnResume.
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}
