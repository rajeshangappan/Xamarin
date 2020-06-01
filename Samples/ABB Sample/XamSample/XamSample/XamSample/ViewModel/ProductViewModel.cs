using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamSample.AppHelper;
using XamSample.Contracts;
using XamSample.Models;
using XamSample.Views;

namespace XamSample.ViewModel
{
    /// <summary>
    /// Defines the <see cref="ProductViewModel" />.
    /// </summary>
    public class ProductViewModel : INotifyPropertyChanged
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _productList.
        /// </summary>
        private ObservableCollection<Product> _productList;

        /// <summary>
        /// Defines the _productService.
        /// </summary>
        private IProductService _productService;

        #endregion

        #region PUBLIC_PPTY

        /// <summary>
        /// Gets the AddProductCommand.
        /// </summary>
        public ICommand AddProductCommand => new Command(OnAddProduct);

        /// <summary>
        /// Gets or sets a value indicating whether IsAdmin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets the ItemSelectedCommand.
        /// </summary>
        public ICommand ItemSelectedCommand => new Command(OnItemSelected);

        /// <summary>
        /// Gets the OnAppearingCommand.
        /// </summary>
        public ICommand OnAppearingCommand => new Command(async () => await OnAppearing());

        /// <summary>
        /// Gets or sets the ProductList.
        /// </summary>
        public ObservableCollection<Product> ProductList
        {
            get => _productList;
            set
            {
                if (_productList != value)
                {
                    _productList = value;
                }

                OnPropertyChanged("ProductList");
            }
        }

        /// <summary>
        /// Gets or sets the SelectedProduct.
        /// </summary>
        public Product SelectedProduct { get; set; }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductViewModel"/> class.
        /// </summary>
        /// <param name="productService">The productService<see cref="IProductService"/>.</param>
        public ProductViewModel(IProductService productService)
        {
            _productService = productService;
            IsAdmin = SampleHelper.IsAdmin();
        }

        #endregion

        #region Events

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The NavigateToProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <param name="IsNewProduct">The IsNewProduct<see cref="bool"/>.</param>
        private void NavigateToProduct(Product product, bool IsNewProduct)
        {
            var vm = IocContainer.Resolve<ProductDetailsPageViewModel>();
            vm.Product = product;
            vm.IsAdmin = IsAdmin;
            vm.IsNewProduct = IsNewProduct;
            Application.Current.MainPage.Navigation.PushAsync(new ProductDetailsPage { BindingContext = vm });
        }

        /// <summary>
        /// The OnAddProduct.
        /// </summary>
        private void OnAddProduct()
        {
            NavigateToProduct(new Product(), true);
        }

        /// <summary>
        /// The OnAppearing.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task OnAppearing()
        {
            var products = await _productService.GetProducts();
            ProductList = new ObservableCollection<Product>(products);
        }

        /// <summary>
        /// The OnItemSelected.
        /// </summary>
        private void OnItemSelected()
        {
            NavigateToProduct(SelectedProduct, false);
        }

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/>.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
