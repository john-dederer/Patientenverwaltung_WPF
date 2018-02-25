using System.Windows.Controls;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für CreatePatient.xaml
    /// </summary>
    public partial class CreatePatient
    {
        public CreatePatient()
        {
            InitializeComponent();

            DataContext = PatientViewModel.SharedViewModel();
        }
    }
}