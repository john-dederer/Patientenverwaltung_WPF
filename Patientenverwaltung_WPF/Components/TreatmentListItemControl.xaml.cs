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
    /// Interaktionslogik für TreatmentListItemControl.xaml
    /// </summary>
    public partial class TreatmentListItemControl : UserControl
    {
        public TreatmentListItemControl()
        {
            InitializeComponent();
        }

        private void TreatmentGotFocus(object sender, RoutedEventArgs e)
        {
            TreatmentViewModel.SharedViewModel().NewTreatment = DataContext as Treatment;

            btnUpdateTreatment.Visibility = Visibility.Visible;
            btnDeleteTreatment.Visibility = Visibility.Visible;

            txtDescription.IsReadOnly = false;
            txtOther.IsReadOnly = false;
        }

        private void UpdateTreatment(object sender, RoutedEventArgs e)
        {
            TreatmentViewModel.SharedViewModel().Update(this);

            TreatmentLostFocus(null, null);
        }

        private void DeleteTreatment(object sender, RoutedEventArgs e)
        {
            TreatmentViewModel.SharedViewModel().Delete(this);
            TreatmentLostFocus(null, null);
        }

        private void TreatmentLostFocus(object sender, RoutedEventArgs e)
        {
            btnUpdateTreatment.Visibility = Visibility.Collapsed;
            btnDeleteTreatment.Visibility = Visibility.Collapsed;

            txtDescription.IsReadOnly = true;
            txtOther.IsReadOnly = true;
        }
    }
}
