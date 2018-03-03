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

        /// <summary>
        /// When list item is selected, make the selection indicator visible
        /// </summary>
        /// <param name="sender">ListItem</param>      
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

        /// <summary>
        /// Helper method to return the selection indicator from the selected list item
        /// </summary>
        /// <param name="sender">ListItem</param>
        /// <returns></returns>
        internal static Border GetSelectionIndicator(object sender)
        {
            var grid = (Grid)((HealthinsuranceListItemControl)sender).FindName("GridItem");
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

        /// <summary>
        /// Triggered when listsize changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemListChangedSize(object sender, SizeChangedEventArgs e)
        {  
            // Check if we have to set the selection indicator
            if (!string.IsNullOrEmpty(HealthinsuranceViewModel.SharedViewModel().NewHealthinsurance.Name))
            {
                // Find HI in list which was just added
                foreach (var curItem in ItemsControl.Items)
                {
                    var hiModel = curItem as Healthinsurance;

                    if (hiModel.GetHashCode() != HealthinsuranceViewModel.SharedViewModel().NewHealthinsurance.GetHashCode()) continue;

                    var container = ItemsControl.ItemContainerGenerator.ContainerFromItem(curItem) as FrameworkElement;
                    var hiListItem = ItemsControl.ItemTemplate.FindName("HIItemControl", container) as HealthinsuranceListItemControl;
                    var selInd = GetSelectionIndicator(hiListItem);
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
