using System.Windows.Controls;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für CreateHealthinsurance.xaml
    /// </summary>
    public partial class CreateHealthinsurance
    {
        public CreateHealthinsurance()
        {
            InitializeComponent();

            DataContext = HealthinsuranceViewModel.SharedViewModel();

            Loaded += CreateHealthinsurance_Loaded;
        }

        private void CreateHealthinsurance_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var errors = Grid.GetValue(Validation.ErrorsProperty);
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)   HealthinsuranceViewModel.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) HealthinsuranceViewModel.Errors -= 1;
        }
    }
}