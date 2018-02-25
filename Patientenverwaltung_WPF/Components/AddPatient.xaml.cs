using System.Windows.Controls;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für AddPatient.xaml
    /// </summary>
    public partial class AddPatient
    {
        public AddPatient()
        {
            InitializeComponent();

            DataContext = PatientViewModel.SharedViewModel();
        }

        private void ShowCreatePatientUi(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PatientViewModel.SharedViewModel().NewPatient = new Patient();
            PatientViewModel.SharedViewModel().ShowCreateMaskUi = true;

            TreatmentViewModel.SharedViewModel().FilterTreatments();
            TreatmentViewModel.SharedViewModel().ShowSearchTreatmentUi = false;
        }
    }
}