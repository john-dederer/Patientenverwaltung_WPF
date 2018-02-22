﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    public class Patient : Datamodel, INotifyPropertyChanged
    {
        private long patientId = 0;
        private long insuranceId = 0;
        private string firstname = "";
        private string secondname = "";
        private string street = "";
        private int streetnumber = 0;
        private int postalcode = 0;
        private string city = "";
        private DateTime birthday = DateTime.Now;
        private int phonenumber = 0;

        public long PatientId { get { return patientId; } set { patientId = value; OnPropertyChanged("PatientId"); } }
        public long InsuranceId { get { return insuranceId; } set { insuranceId = value; OnPropertyChanged("InsuranceId"); } }
        public string Firstname { get { return firstname; } set { firstname = value; OnPropertyChanged("Firstname"); } }
        public string Secondname { get { return secondname; } set { secondname = value; OnPropertyChanged("Secondname"); } }
        public string Street { get { return street; } set { street = value; OnPropertyChanged("Street"); } }
        public int Streetnumber { get { return streetnumber; } set { streetnumber = value; OnPropertyChanged("Streetnumber"); } }
        public int Postalcode { get { return postalcode; } set { postalcode = value; OnPropertyChanged("Postalcode"); } }
        public string City { get { return city; } set { city = value; OnPropertyChanged("City"); } }   
        public DateTime Birthday { get { return birthday; } set { birthday = value; OnPropertyChanged("Birthday"); } }
        public int Phonenumber { get { return phonenumber; } set { phonenumber = value; OnPropertyChanged("Phonenumber"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
