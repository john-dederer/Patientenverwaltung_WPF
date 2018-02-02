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

namespace Patientenverwaltung_WPF.Pages
{
    /// <summary>
    /// Interaktionslogik für ResetPasswordPage.xaml
    /// </summary>
    public partial class ResetPasswordPage : Page
    {
        public ResetPasswordPage()
        {
            InitializeComponent();

            DataContext = CurrentContext.GetUser();
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UpdatePage(Constants.LoginPageUri);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxUsername.Text == string.Empty)
            {
                lblInfo.Content = "Benutzername darf nicht leer sein";
                return;
            }
            else if (passwordBox.SecurePassword.Length == 0)
            {
                lblInfo.Content = "Passwort darf nicht leer sein";
                return;
            }

            // save the hash
            var hash = CurrentContext.GetUser().Passwordhash;

            // To not breakt the MVVM pattern we hash the password asap
            CurrentContext.GetUser().Passwordhash = PasswordStorage.CreateHash(passwordBox.Password);

            if (Factory.Get(CurrentContext.GetSettings().Savetype).Update(CurrentContext.GetUser()))
            {
                MainWindow.UpdatePage(Constants.LoginPageUri);
            }
            else
            {
                CurrentContext.GetUser().Passwordhash = hash;
                lblInfo.Content = "Es wurde nichts geändert";
            }
        }
    }
}
