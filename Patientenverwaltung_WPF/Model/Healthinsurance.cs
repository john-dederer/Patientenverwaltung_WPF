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
        [UniqueHealthinsuranceName(ErrorMessage = "Name exisitert bereits")]
        [StringLength(50, ErrorMessage = "Nicht mehr als 50 Zeichen erlaubt. Mindestens 3", MinimumLength = 3)]
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value.Trim()); }
        }

//        [ExcludeChar("/")]
        [Required(ErrorMessage = "Straße muss angegeben werden")]
        public string Street
        {
            get { return GetValue(() => Street); }
            set { SetValue(() => Street, value.Trim()); }
        }

        [Required(ErrorMessage = "Straßennummer muss angegeben werden")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Darf nur aus Nummern bestehen")]
        public string Streetnumber
        {
            get { return GetValue(() => Streetnumber); }
            set { SetValue(() => Streetnumber, value); }
        }

        [Required(ErrorMessage = "Postleitzahl muss angegeben werden")]
        [RegularExpression(@"^(?!00000|99999)(0[0-9]\d{3}|[0-9]\d{4})$", ErrorMessage = "Bitte Postleitzahl bestehend aus 5 Nummern eingeben")]
        public string Postalcode
        {
            get { return GetValue(() => Postalcode); }
            set { SetValue(() => Postalcode, value); }
        }

        [Required(ErrorMessage = "Stadt muss angegeben werden")]
        public string City
        {
            get { return GetValue(() => City); }
            set { SetValue(() => City, value); }
        }

        [Required(ErrorMessage = "Status muss angegeben werden")]
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
            if (Streetnumber != null) hash += Streetnumber.Trim().Length;
            if (Postalcode != null) hash += Postalcode.Trim().Length;
            if (City != null) hash += City.Trim().Length;
            hash += State.GetHashCode();

            return hash;
        }
    }
}
