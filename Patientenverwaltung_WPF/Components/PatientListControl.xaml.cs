using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für PatientListControl.xaml
    /// </summary>
    public partial class PatientListControl
    {
        private Border _lastItemSelected;
        public PatientListControl()
        {
            InitializeComponent();        

            DataContext = PatientViewModel.SharedViewModel();
        }

        private void PatientSelected(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
            var grid = (Grid)((PatientListItemControl)sender).FindName("GridItem");
            return (Border)grid.FindName("SelectionIndicator");
        }
    }
}