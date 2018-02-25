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
using Patientenverwaltung_WPF.Notification;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für SwitchToHiUi.xaml
    /// </summary>
    public partial class SwitchToHiUi : UserControl
    {
        public SwitchToHiUi()
        {
            InitializeComponent();
        }

        private void ChangeUIToHealthinsurance(object sender, RoutedEventArgs e)
        {            
            UiHelper.SwitchUiToHealthinsurance();
            UiHelper.UiState = UIState.Healthinsurance;
        }
    }
}
