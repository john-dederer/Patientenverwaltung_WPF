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
    /// Interaktionslogik für CreatePatient.xaml
    /// </summary>
    public partial class CreatePatient : UserControl
    {
        public CreatePatient()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // First we have to check if username already exists
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(CurrentContext.GetPatient(), out Patient returned))
            {
                if (returned == null) return;

                if (CurrentContext.GetPatient().GetHashCode() == returned.GetHashCode())
                {
                    // Show username already exists
                    
                }
            }
            else
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(CurrentContext.GetPatient()))
                {
                    // Successfully created
                }
                else
                {
                    // Username already exists
                }
            }
        }
    }
}
