using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    class PatientListViewModel
    {
        public ObservableCollection<Patient> DataList { get; set; }
        public static PatientListViewModel Instance => new PatientListViewModel();

        public PatientListViewModel()
        {
            DataList = CurrentContext.GetPatientListOC();
        }

        public PatientListViewModel(List<Patient> list)
        {
            DataList = new ObservableCollection<Patient>(list);
        }
    }
}
