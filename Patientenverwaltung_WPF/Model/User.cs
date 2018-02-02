using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    public class User : Datamodel, IDataErrorInfo
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Passwordhash { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "UserId":
                        if (UserId < 0) return "UserId darf nicht kleiner 0 sein"; break;
                    case "Title":
                        if (string.IsNullOrEmpty(Title)) return "Feld darf nicht leer sein."; break;
                    case "Name":
                        if (string.IsNullOrEmpty(Title)) return "Feld darf nicht leer sein."; break;
                    case "Username":
                        if (string.IsNullOrEmpty(Title)) return "Feld darf nicht leer sein."; break;

                }

                return string.Empty;
            }
        }

        public string Error { get; }
    }
}
