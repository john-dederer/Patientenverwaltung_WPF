using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für PatientListControl.xaml
    /// </summary>
    public partial class PatientListControl
    {
        public PatientListControl()
        {
            InitializeComponent();        

            DataContext = PatientViewModel.SharedViewModel();
        }
    }
}