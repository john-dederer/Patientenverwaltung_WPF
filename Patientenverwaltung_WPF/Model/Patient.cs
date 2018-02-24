using System;
using System.ComponentModel;

namespace Patientenverwaltung_WPF
{
    public class Patient : Datamodel, INotifyPropertyChanged
    {
        private DateTime _birthday = DateTime.Now;
        private string _city = "";
        private string _firstname = "";
        private long _healthinsuranceId;
        private long _insuranceId;
        private long _patientId;
        private int _phonenumber;
        private int _postalcode;
        private string _secondname = "";
        private string _street = "";
        private int _streetnumber;

        public long PatientId
        {
            get => _patientId;
            set
            {
                _patientId = value;
                OnPropertyChanged("PatientId");
            }
        }

        public long HealthinsuranceId
        {
            get => _healthinsuranceId;
            set
            {
                _healthinsuranceId = value;
                OnPropertyChanged("HealthinsuranceId");
            }
        }

        public long InsuranceId
        {
            get => _insuranceId;
            set
            {
                _insuranceId = value;
                OnPropertyChanged("InsuranceId");
            }
        }

        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        public string Secondname
        {
            get => _secondname;
            set
            {
                _secondname = value;
                OnPropertyChanged("Secondname");
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged("Street");
            }
        }

        public int Streetnumber
        {
            get => _streetnumber;
            set
            {
                _streetnumber = value;
                OnPropertyChanged("Streetnumber");
            }
        }

        public int Postalcode
        {
            get => _postalcode;
            set
            {
                _postalcode = value;
                OnPropertyChanged("Postalcode");
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public int Phonenumber
        {
            get => _phonenumber;
            set
            {
                _phonenumber = value;
                OnPropertyChanged("Phonenumber");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override int GetHashCode()
        {
            var hash = 0;
            hash += Firstname.Trim().Length;
            hash += Secondname.Trim().Length;
            hash += Street.Trim().Length;
            hash += Streetnumber * 1500;
            hash += Postalcode * 17 + 99;
            hash += City.Trim().Length;
            //hash += birthday.GetHashCode();

            return hash;
        }
    }
}