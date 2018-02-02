using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    class User : Datamodel
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Passwordhash { get; set; }
    }
}
