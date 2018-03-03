using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF.Pages
{
    /// <summary>
    /// Interaktionslogik für LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();

            // Initialize settings
            InitializeSettings();

            // Set DataContext
            DataContext = UserViewModel.SharedViewModel();
            btnLogin.DataContext = UserViewModel.SharedViewModel();

            Loaded += LoginPage_Loaded;
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {           
            UserViewModel.Errors = 0;
        }

        private void InitializeSettings()
        {
            if (!CurrentContext.GetSettings().SettingsJSONExist())
            {
                // Create Settings.json file at exe file location
                CurrentContext.GetSettings().CreateSettingsJSON();

                // Load Initial Settings page
                MainWindow.UpdatePage(Constants.InitialSettingsPageUri);
            }
            else
            {
                // Read settings from file
                CurrentContext.GetSettings().SetSettings(true);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxUsername.Text == string.Empty)
            {
                lblInfo.Content = "Benutzername fehlt";
                return;
            }
            else if (passwordBox.SecurePassword.Length == 0)
            {
                lblInfo.Content = "Passwort fehlt";
                return;
            }

            // Check if username exist
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(CurrentContext.GetUser(), out User returned))
            {
                if (returned == null) return;

                // check hashes
                if (PasswordStorage.VerifyPassword(passwordBox.Password, returned.Passwordhash))
                {
                    // Login verified
                    lblInfo.Content = "Login erfolgreich";
                    //MainWindow.UpdatePage("HomescreenPage.xaml");

                    HomeWindow homeWindow = new HomeWindow();
                    homeWindow.Show();

                    MainWindow.Destroy();
                }
                else
                {
                    // Show login credentials do differ
                    lblInfo.Content = "Zugangsdaten stimmen nicht überein";
                }
            }
            else
            {
                // User does not exist yet
                MainWindow.UpdatePage(Constants.AskToCreateAccountPageUri);
            }
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UpdatePage(Constants.CreateAccountPageUri);
        }

        private void btnPasswordForgotten_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UpdatePage(Constants.ResetPasswordPageUri);
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) UserViewModel.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) UserViewModel.Errors -= 1;
        }
    }
}
