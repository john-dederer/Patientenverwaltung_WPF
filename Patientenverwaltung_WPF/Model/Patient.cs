using System;
using System.ComponentModel;

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

        public long InsuranceId
        {
            get { return GetValue(() => InsuranceId); }
            set { SetValue(() => InsuranceId, value); }
        }

        public string Firstname
        {
            get { return GetValue(() => Firstname); }
            set { SetValue(() => Firstname, value); }
        }

        public string Secondname
        {
            get { return GetValue(() => Secondname); }
            set { SetValue(() => Secondname, value); }
        }

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

        public DateTime Birthday
        {
            get { return GetValue(() => Birthday); }
            set { SetValue(() => Birthday, value); }
        }

        public int Phonenumber
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
            hash += Streetnumber * 1500;
            hash += Postalcode * 17 + 99;
            if (City != null) hash += City.Trim().Length;
            //hash += birthday.GetHashCode();

            return hash;
        }
    }
}