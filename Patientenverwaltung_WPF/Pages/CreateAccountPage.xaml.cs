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
            // First we have to check if username already exists

            // To not breakt the MVVM pattern we hash the password asap
            CurrentContext.GetUser().Passwordhash = PasswordStorage.CreateHash(passwordBox.Password);
        }
    }
}
