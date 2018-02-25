using System.Windows.Controls;
using Patientenverwaltung_WPF.ViewModel;

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

            Loaded += CreatePatient_Loaded;
        }

        private void CreatePatient_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var errors = Grid.GetValue(Validation.ErrorsProperty);
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) PatientViewModel.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) PatientViewModel.Errors -= 1;
        }
    }
}