using System.Windows.Controls;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für PatientListItemControl.xaml
    /// </summary>
    public partial class PatientListItemControl
    {
        public PatientListItemControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the selected item as current patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientItemSelected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PatientViewModel.SharedViewModel().NewPatient = DataContext as Patient;

            // Update the Ui
            PatientViewModel.SharedViewModel().ShowCreateMaskUi = true;

            // Show only treatments for patient
            TreatmentViewModel.SharedViewModel().FilterTreatments();
            TreatmentViewModel.SharedViewModel().ShowTreatmentListUi = true;
            TreatmentViewModel.SharedViewModel().ShowSearchTreatmentUi = true;
        }
    }
}