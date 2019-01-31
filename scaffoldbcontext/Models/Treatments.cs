using System;
using System.Collections.Generic;

namespace scaffoldbcontext.Models
{
    public partial class Treatments
    {
        public long TreatmentId { get; set; }
        public long PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Other { get; set; }
        public string LastChange { get; set; }

        public virtual Patients Patient { get; set; }
    }
}
