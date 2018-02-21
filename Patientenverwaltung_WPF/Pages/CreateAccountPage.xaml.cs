using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page
    {
        public CreateAccountPage()
        {
            InitializeComponent();

            DataContext = CurrentContext.GetUser();

        }

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            // Clear the page
            ClearPage();

            // Load login page
            MainWindow.UpdatePage(Constants.LoginPageUri);
        }

        private void ClearPage()
        {
            txtBoxTitle.Clear();
            txtBoxName.Clear();
            txtBoxUsername.Clear();
            passwordBox.Clear();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxTitle.Text == string.Empty || txtBoxName.Text == string.Empty || txtBoxUsername.Text == string.Empty || passwordBox.SecurePassword.Length == 0)
            {
                lblInfo.Content = "Bitte alle Felder ausfüllen";
                return;
            }
            else
            {
                lblInfo.Content = "";
            }

            // First we have to check if username already exists
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(CurrentContext.GetUser(), out User returned))
            {
                if (returned == null) return;

                if (CurrentContext.GetUser().Username == returned.Username)
                {
                    // Show username already exists
                    lblInfo.Content = $@"{txtBoxUsername.Text} existiert bereits";
                }
            }
            else
            {
                // To not breakt the MVVM pattern we hash the password asap
                CurrentContext.GetUser().Passwordhash = PasswordStorage.CreateHash(passwordBox.Password);

                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(CurrentContext.GetUser()))
                {
                    // Successfully created
                    lblInfo.Content = "Account erfolgreich angelegt";
                }
                else
                {
                    // Username already exists
                    lblInfo.Content = $@"{txtBoxUsername.Text} existiert bereits";
                }
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            btnCreateAccount.IsEnabled = passwordBox.SecurePassword.Length != 0;
        }

        private void txtBoxTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnCreateAccount.IsEnabled = !string.IsNullOrEmpty(txtBoxTitle.Text);
        }

        private void txtBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnCreateAccount.IsEnabled = !string.IsNullOrEmpty(txtBoxName.Text);
        }

        private void txtBoxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnCreateAccount.IsEnabled = !string.IsNullOrEmpty(txtBoxUsername.Text);
        }
    }
}
