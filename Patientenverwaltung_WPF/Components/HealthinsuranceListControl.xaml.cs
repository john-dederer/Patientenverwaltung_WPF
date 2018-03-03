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
    /// Interaktionslogik für HealthinsuranceListControl.xaml
    /// </summary>
    public partial class HealthinsuranceListControl : UserControl
    {
        private Border _lastItemSelected;

        public HealthinsuranceListControl()
        {
            InitializeComponent();

            DataContext = HealthinsuranceViewModel.SharedViewModel();
        }

        private void HISelected(object sender, MouseButtonEventArgs e)
        {
            if (_lastItemSelected == null)
            {
                _lastItemSelected = GetSelectionIndicator(sender);
                _lastItemSelected.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                _lastItemSelected.Visibility = System.Windows.Visibility.Hidden;

                _lastItemSelected = GetSelectionIndicator(sender);
                _lastItemSelected.Visibility = System.Windows.Visibility.Visible;
            }
        }      

        internal static Border GetSelectionIndicator(object sender)
        {
            var grid = (Grid)((HealthinsuranceListItemControl)sender).FindName("GridItem");
            return (Border)grid.FindName("SelectionIndicator");
        }

    }
}
