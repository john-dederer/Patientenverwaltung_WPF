using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.RegularExpressions;

namespace Patientenverwaltung_WPF
{
    public class Healthinsurance : PropertyChangedNotification
    {
        //[UniqueHealthinsuranceId(ErrorMessage = "Krankenversicherung existiert bereits")]
        public long HealthinsuranceId
        {
           get { return GetValue(() => HealthinsuranceId); }
           set { SetValue(() => HealthinsuranceId, value); }
        }

        [Required]       
        [UniqueHealthinsuranceName]
        [StringLength(50, ErrorMessage = "Nicht mehr als 50 Zeichen erlaubt. Mindestens 3", MinimumLength = 3)]
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }
        }

//        [ExcludeChar("/")]
        public string Street
        {
            get { return GetValue(() => Street); }
            set { SetValue(() => Street, value); }
        }

        public int Streetnumber
        {
            get { return GetValue(() => Streetnumber); }
            set { SetValue(() => Streetnumber, value); }
        }

        public int Postalcode
        {
            get { return GetValue(() => Postalcode); }
            set { SetValue(() => Postalcode, value); }
        }

        public string City
        {
            get { return GetValue(() => City); }
            set { SetValue(() => City, value); }
        }

        public HealthinsuranceState State
        {
            get { return GetValue(() => State); }
            set { SetValue(() => State, value); }
        }

        public override int GetHashCode()
        {
            var hash = 0;
            if (Name != null) hash += Name.Trim().Length;
            if (Street != null) hash += Street.Trim().Length;
            hash += Streetnumber * 1234;
            hash += Postalcode * 231;
            if (City != null) hash += City.Trim().Length;
            hash += State.GetHashCode();

            return hash;
        }
    }
}
