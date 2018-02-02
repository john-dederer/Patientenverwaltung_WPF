using System;
using System.Collections.Generic;
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
            DataContext = CurrentContext.GetUser();
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
            // Check if username exist
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(CurrentContext.GetUser(), out User returned))
            {
                // check hashes
                if (PasswordStorage.VerifyPassword(passwordBox.Password, returned.Passwordhash))
                {
                    // Login verified
                }
                else
                {
                    // Show login credentials do differ
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

        }
    }
}
