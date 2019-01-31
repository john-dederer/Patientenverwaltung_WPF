using System;
using System.Collections.Generic;

namespace scaffoldbcontext.Models
{
    public partial class Patients
    {
        public Patients()
        {
            Treatments = new HashSet<Treatments>();
        }

        public long PatientId { get; set; }
        public long HealthinsuranceId { get; set; }
        public long InsuranceId { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Street { get; set; }
        public string Streetnumber { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
        public string Phonenumber { get; set; }
        public string LastChange { get; set; }

        public virtual ICollection<Treatments> Treatments { get; set; }
    }
}
