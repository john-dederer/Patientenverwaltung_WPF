using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Patientenverwaltung_WPF.ViewModel
{
    public class UserViewModel : PropertyChangedNotification
    {
        private static UserViewModel _userViewModel;

        public static UserViewModel SharedViewModel()
        {
            return _userViewModel ?? (_userViewModel = new UserViewModel());
        }

        /// <summary>
        /// List of users in memory
        /// </summary>
        public ObservableCollection<User> Users
        {
            get { return GetValue(() => Users); }
            set { SetValue(() => Users, value); }
        }

        /// <summary>
        /// New user to be added
        /// </summary>
        public User NewUser
        {
            get { return GetValue(() => NewUser); }
            set { SetValue(() => NewUser, value); }
        }

        /// <summary>
        /// Used to login the user
        /// </summary>
        public RelayCommand LoginCommand { get; set; }   
        
        /// <summary>
        /// Used to create a user
        /// </summary>
        public RelayCommand CreateCommand { get; set; }

        /// <summary>
        /// Counts the errors in the form
        /// </summary>
        public static int Errors { get; set; }

        public UserViewModel()
        {
            // Load data from datastorage
            Users = CurrentContext.GetUserOc();

            NewUser = new User();

            LoginCommand = new RelayCommand(LoginUser);
            CreateCommand = new RelayCommand(CreateUser, CanCreate);
        }

        private static bool CanCreate(object obj)
        {
            return Errors == 0;
        }

        private static void CreateUser(object parameter)
        {
            // parameter is PasswordBox
            var pwBox = parameter as PasswordBox;
            SharedViewModel().NewUser.Passwordhash = PasswordStorage.CreateHash(pwBox.Password);

            if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(SharedViewModel().NewUser))
            {
                var info = new InfoMessageWindow("Benutzer Erfolreich angelegt");
                info.ShowDialog();

                Errors = 0;
            }
            else
            {
                var info = new InfoMessageWindow("Benutzer konnte nicht angelegt werden");
                info.ShowDialog();

                
            }
        }

        private static void LoginUser(object obj)
        {
            // Check if username exist
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(SharedViewModel().NewUser, out User returned))
            {
                if (returned == null) return;

                // check hashes
                if (PasswordStorage.VerifyPassword(SharedViewModel().NewUser.Passwordhash, returned.Passwordhash))
                {                  
                    var homeWindow = new HomeWindow();
                    homeWindow.Show();

                    MainWindow.Destroy();

                    CurrentContext.GetUser() = returned;
                }
                else
                {
                    // Show login credentials do differ
                    var info = new InfoMessageWindow("Zugangsdaten stimmen nicht überein");
                    info.ShowDialog();
                }
            }
            else
            {
                // User does not exist yet
                MainWindow.UpdatePage(Constants.AskToCreateAccountPageUri);
            }
        }
    }
}
