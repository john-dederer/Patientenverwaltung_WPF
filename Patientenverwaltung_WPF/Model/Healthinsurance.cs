using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Patientenverwaltung_WPF
{
    public class Healthinsurance : Datamodel, INotifyPropertyChanged
    {
        private long healthinsuranceId = 0;
        private string name = string.Empty;
        private string street = string.Empty;
        private int streetnumber = 0;
        private int postalcode = 0;
        private string city = string.Empty;
        private HealthinsuranceState state = HealthinsuranceState.ByLaw;

        public long HealthinsuranceId { get { return healthinsuranceId; } set { healthinsuranceId = value; OnPropertyChanged(nameof(HealthinsuranceId)); } }
        public string Name { get { return name; } set { if(value != string.Empty) name = value; OnPropertyChanged(nameof(name)); } }
        public string Street { get { return street; } set { street = value; OnPropertyChanged(nameof(street)); } }
        public int Streetnumber { get { return streetnumber; } set { streetnumber = value; OnPropertyChanged(nameof(streetnumber)); } }
        public int Postalcode { get { return postalcode; } set { postalcode = value; OnPropertyChanged(nameof(postalcode)); } }
        public string City { get { return city; } set { city = value; OnPropertyChanged(nameof(city)); } }
        public HealthinsuranceState State { get { return state; } set { state = value; OnPropertyChanged(nameof(state)); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override int GetHashCode()
        {
            var hash = 0;
            hash += name.Trim().Length;
            hash += street.Trim().Length;
            hash += streetnumber * 1234;
            hash += postalcode * 231;
            hash += city.Trim().Length;
            hash += state.GetHashCode();

            return hash;
        }
    }

    public enum HealthinsuranceState
    {
        Private,
        ByLaw
    }
}
