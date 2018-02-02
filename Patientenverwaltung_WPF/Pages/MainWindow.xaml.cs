using Patientenverwaltung_WPF.Pages;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Frame PageFrame;

        public MainWindow()
        {
            InitializeComponent();
            PageFrame = pageFrame;

            UpdatePage(Constants.LoginPageUri);
        }

        public static void UpdatePage(string pageUri)
        {
            if (PageFrame != null) PageFrame.Source = new Uri(pageUri, UriKind.RelativeOrAbsolute);
        }
    }

    
}
