using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    class Healthinsurance : Datamodel
    {
        public long HealthinsuranceId { get; set; }
        public long InsuranceId { get; set; }

        public string Name { get; set; }        
        public string Street { get; set; }
        public int Streetnumber { get; set; }
        public int Postalcode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
