using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    class PatientViewModel
    {
        public static Patient Patient { get; set; }
        public static PatientViewModel Instance => new PatientViewModel();

        public PatientViewModel()
        {
            Patient = CurrentContext.GetPatient();
        }
    }
}
