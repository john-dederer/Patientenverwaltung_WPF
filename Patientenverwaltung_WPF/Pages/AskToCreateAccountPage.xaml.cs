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

namespace Patientenverwaltung_WPF.Pages
{
    /// <summary>
    /// Interaktionslogik für AskToCreateAccountPage.xaml
    /// </summary>
    public partial class AskToCreateAccountPage : Page
    {
        public AskToCreateAccountPage()
        {
            InitializeComponent();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.UpdatePage(Constants.LoginPageUri);
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            //UserViewModel.SharedViewModel().NewUser = new User();
            UserViewModel.Errors = 0;
            MainWindow.UpdatePage(Constants.CreateAccountPageUri);
        }
    }
}
