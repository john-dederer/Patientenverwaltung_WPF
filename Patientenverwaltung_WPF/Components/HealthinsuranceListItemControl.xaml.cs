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
    /// Interaktionslogik für HealthinsuranceListItemControl.xaml
    /// </summary>
    public partial class HealthinsuranceListItemControl : UserControl
    {
        public HealthinsuranceListItemControl()
        {
            InitializeComponent();           
        }

        /// <summary>
        /// Set selected item as current healthinsurance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HealthinsuranceViewModel.SharedViewModel().NewHealthinsurance = DataContext as Healthinsurance;           

            // Update the Ui
            HealthinsuranceViewModel.SharedViewModel().ShowCreateHiMask = true;
            HealthinsuranceViewModel.SharedViewModel().IsHIBeingCreated = false;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var healthinsurance = DataContext as Healthinsurance;
            if (healthinsurance.State == HealthinsuranceState.ByLaw) { HIByLawIcon.Visibility = Visibility.Visible; }
            else { HiPrivateIcon.Visibility = Visibility.Visible; }            
        }
    }
}
