using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Patientenverwaltung_WPF
{
    public class Patient : PropertyChangedNotification
    {
        public long PatientId
        {
            get { return GetValue(() => PatientId); }
            set { SetValue(() => PatientId, value); }            
        }

        public long HealthinsuranceId
        {
            get { return GetValue(() => HealthinsuranceId); }
            set { SetValue(() => HealthinsuranceId, value); }
        }

        [Required(ErrorMessage = "Versicherungsnummer bitte angeben")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Darf nur aus Nummern bestehen")]
        public long InsuranceId
        {
            get { return GetValue(() => InsuranceId); }
            set { SetValue(() => InsuranceId, value); }
        }

        [Required(ErrorMessage = "Vorname muss gefüllt sein")]        
        public string Firstname
        {
            get { return GetValue(() => Firstname); }
            set { SetValue(() => Firstname, value); }
        }

        [Required(ErrorMessage = "Nachname muss gefüllt sein")]
        public string Secondname
        {
            get { return GetValue(() => Secondname); }
            set { SetValue(() => Secondname, value); }
        }

        [Required(ErrorMessage = "Straße muss gefüllt sein")]
        public string Street
        {
            get { return GetValue(() => Street); }
            set { SetValue(() => Street, value); }
        }

        [Required(ErrorMessage = "Straßennummer muss gefüllt sein")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Darf nur aus Nummern bestehen")]
        public string Streetnumber
        {
            get { return GetValue(() => Streetnumber); }
            set { SetValue(() => Streetnumber, value); }
        }

        [Required(ErrorMessage = "Postleitzahl muss gefüllt sein")]
        [RegularExpression(@"^(?!00000|99999)(0[0-9]\d{3}|[0-9]\d{4})$", ErrorMessage = "Bitte Postleitzahl bestehend aus 5 Nummern eingeben")]
        public string Postalcode
        {
            get { return GetValue(() => Postalcode); }
            set { SetValue(() => Postalcode, value); }
        }

        [Required(ErrorMessage = "Stadt muss gefüllt sein")]
        public string City
        {
            get { return GetValue(() => City); }
            set { SetValue(() => City, value); }
        }

        [Required(ErrorMessage = "Geburtstag muss gefüllt sein")]
        public DateTime Birthday
        {
            get { return GetValue(() => Birthday); }
            set { SetValue(() => Birthday, value); }
        }

        [Required(ErrorMessage = "Bitte Geschlecht angeben")]
        public Gender Gender
        {
            get { return GetValue(() => Gender); }
            set { SetValue(() => Gender, value); }
        }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Darf nur aus Nummern bestehen")]
        public string Phonenumber
        {
            get { return GetValue(() => Phonenumber); }
            set { SetValue(() => Phonenumber, value); }
        }


        public override int GetHashCode()
        {
            var hash = 0;
            if (Firstname != null) hash += Firstname.Trim().Length;
            if (Secondname != null) hash += Secondname.Trim().Length;
            if (Street != null) hash += Street.Trim().Length;
            if (Streetnumber != null) hash += Streetnumber.Trim().Length;
            if (Postalcode != null) hash += Postalcode.Trim().Length;
            if (City != null) hash += City.Trim().Length;
            //hash += birthday.GetHashCode();

            return hash;
        }
    }
}