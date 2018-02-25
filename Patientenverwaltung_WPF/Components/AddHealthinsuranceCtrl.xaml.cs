using System.Windows.Controls;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für AddHealthinsuranceCtrl.xaml
    /// </summary>
    public partial class AddHealthinsuranceCtrl
    {
        public AddHealthinsuranceCtrl()
        {
            InitializeComponent();

            DataContext = HealthinsuranceViewModel.SharedViewModel();
        }

        private void ShowHiUi(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HealthinsuranceViewModel.SharedViewModel().NewHealthinsurance = new Healthinsurance();
            HealthinsuranceViewModel.SharedViewModel().ShowCreateHiMask = true;
        }
    }
}