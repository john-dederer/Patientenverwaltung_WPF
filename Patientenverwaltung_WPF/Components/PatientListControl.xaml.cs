using System;
using System.Collections.ObjectModel;
using System.Windows;
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

        /// <summary>
        /// When list item is selected, make the selection indicator visible
        /// </summary>
        /// <param name="sender">ListItem</param>
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

        /// <summary>
        /// Helper method to return the selection indicator from the selected list item
        /// </summary>
        /// <param name="sender">ListItem</param>
        /// <returns></returns>
        internal static Border GetSelectionIndicator(object sender)
        {
            var grid = (Grid)((PatientListItemControl)sender).FindName("GridItem");
            return (Border)grid.FindName("SelectionIndicator");
        }

        /// <summary>
        /// Is triggered when the UI changes to an other view
        /// </summary>
        /// <param name="sender">ListItem</param>
        private void ListDisappeared(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var border = GetSelectionIndicator(sender);
            border.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ItemListChangedSize(object sender, SizeChangedEventArgs e)
        {
            // Check if we have to set the selection indicator
            if (!string.IsNullOrEmpty(PatientViewModel.SharedViewModel().NewPatient.Firstname))
            {
                // Find patient in list which was just added
                foreach (var curItem in ItemsControl.Items)
                {
                    var patientModel = curItem as Patient;

                    if (patientModel.GetHashCode() != PatientViewModel.SharedViewModel().NewPatient.GetHashCode()) continue;

                    var container = ItemsControl.ItemContainerGenerator.ContainerFromItem(curItem) as FrameworkElement;
                    var patientListItem = ItemsControl.ItemTemplate.FindName("PatientItemCtrl", container) as PatientListItemControl;
                    var selInd = GetSelectionIndicator(patientListItem);
                    selInd.Visibility = System.Windows.Visibility.Visible;

                    // Remove selction indicator from last selected item
                    if (_lastItemSelected != null) _lastItemSelected.Visibility = System.Windows.Visibility.Hidden;

                    // Set the last selection indicator to newly added item
                    _lastItemSelected = selInd;
                }
            }
        }
    }
}