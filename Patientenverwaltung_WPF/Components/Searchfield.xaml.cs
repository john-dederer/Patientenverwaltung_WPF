using System.Windows.Controls;
using Patientenverwaltung_WPF.Notification;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für Searchfield.xaml
    /// </summary>
    public partial class Searchfield
    {
        public Searchfield()
        {
            InitializeComponent();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (UiHelper.UiState)
            {
                case UIState.Patient:
                    PatientViewModel.SharedViewModel().FilterPredicate = txtSearch.Text;
                    PatientViewModel.SharedViewModel().FilterList();
                    break;
                case UIState.Healthinsurance:
                    HealthinsuranceViewModel.SharedViewModel().FilterPredicate = txtSearch.Text;
                    HealthinsuranceViewModel.SharedViewModel().FilterList();
                    break;
            }
        }
    }
}