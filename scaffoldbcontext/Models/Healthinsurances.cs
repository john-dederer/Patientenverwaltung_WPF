using System;
using System.Collections.Generic;

namespace scaffoldbcontext.Models
{
    public partial class Healthinsurances
    {
        public long HealthinsuranceId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Streetnumber { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        public string LastChange { get; set; }
    }
}
