using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    class Treatment : Datamodel
    {
        public long TreatmentId { get; set; }
        public long PatientId { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Other { get; set; }
    }
}
