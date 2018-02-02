using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    class Patient : Datamodel
    {
        public long PatientId { get; set; }
        public long InsuranceId { get; set; }

        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Street { get; set; }
        public int Streetnumber { get; set; }
        public int Postalcode { get; set; }
        public string City { get; set; }
        public long InsuranceID { get; set; }
        

        public DateTime Birthday { get; set; }
        public int Phonenumber { get; set; }
    }
}
