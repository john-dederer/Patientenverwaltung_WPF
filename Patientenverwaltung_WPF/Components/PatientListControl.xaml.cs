using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für PatientListControl.xaml
    /// </summary>
    public partial class PatientListControl : UserControl
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public PatientListControl()
        {
            InitializeComponent();

            Patients = CurrentContext.GetPatientListViewModel();

            
            DataContext = this;

            
        }
    }
}
