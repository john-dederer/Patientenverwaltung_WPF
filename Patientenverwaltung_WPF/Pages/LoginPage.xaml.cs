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
    /// Interaktionslogik für LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

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
