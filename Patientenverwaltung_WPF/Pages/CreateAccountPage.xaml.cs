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
using Patientenverwaltung_WPF.ViewModel;

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

            DataContext = UserViewModel.SharedViewModel();
            btnCreateAccount.DataContext = UserViewModel.SharedViewModel();

            Loaded += CreateAccountPage_Loaded;
        }

        private void CreateAccountPage_Loaded(object sender, RoutedEventArgs e)
        {
            //UserViewModel.Errors = 0;
        }

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            // Clear the page
            //ClearPage();

            // Load login page
            MainWindow.UpdatePage(Constants.LoginPageUri);
        }

        private void ClearPage()
        {
            txtBoxTitle.Clear();
            txtBoxName.Clear();
            txtBoxUsername.Clear();
            passwordBox.Clear();
            plainPw.Clear();
        }
    
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) UserViewModel.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) UserViewModel.Errors -= 1;
        }
    }
}
