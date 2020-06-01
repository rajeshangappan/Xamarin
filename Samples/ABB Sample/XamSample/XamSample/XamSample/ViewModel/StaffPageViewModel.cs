using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamSample.Models;

namespace XamSample.ViewModel
{
    /// <summary>
    /// Defines the <see cref="StaffPageViewModel" />.
    /// </summary>
    public class StaffPageViewModel : INotifyPropertyChanged
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _userList.
        /// </summary>
        private ObservableCollection<User> _userList;

        #endregion

        #region PUBLIC_PPTY

        /// <summary>
        /// Gets the ItemSelectedCommand.
        /// </summary>
        public ICommand ItemSelectedCommand => new Command(OnItemSelected);

        /// <summary>
        /// Gets the OnAppearingCommand.
        /// </summary>
        public ICommand OnAppearingCommand => new Command(async () => await OnAppearing());

        /// <summary>
        /// Gets or sets the SelectedUser.
        /// </summary>
        public User SelectedUser { get; set; }

        /// <summary>
        /// Gets or sets the UserList.
        /// </summary>
        public ObservableCollection<User> UserList
        {
            get => _userList;
            set
            {
                if (_userList != value)
                {
                    _userList = value;
                }

                OnPropertyChanged("UserList");
            }
        }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffPageViewModel"/> class.
        /// </summary>
        public StaffPageViewModel()
        {
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
        /// The OnAppearing.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task OnAppearing()
        {
            //Get User Detail from API
            UserList = new ObservableCollection<User>
            {
                new User{Username="user1", LoggedIn=true},
                new User{Username="user2", LoggedIn = false},
                new User{Username="user3", LoggedIn = false}
            };
        }

        /// <summary>
        /// The OnItemSelected.
        /// </summary>
        private void OnItemSelected()
        {
            Application.Current.MainPage.DisplayAlert(
                    "Selected User",
                    $"User Name:{SelectedUser.Username} \n LoggedIn:{SelectedUser.LoggedIn}",
                    "Ok");
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
