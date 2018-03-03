using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Patientenverwaltung_WPF;
using Patientenverwaltung_WPF.Notification;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    ///     Interaktionslogik für HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();

            UiHelper.SwitchUiToPatient();
        }
    }
}