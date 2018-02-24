using System.ComponentModel;

namespace Patientenverwaltung_WPF
{
    public class Healthinsurance : Datamodel, INotifyPropertyChanged
    {
        private string _city = string.Empty;
        private long _healthinsuranceId;
        private string _name = string.Empty;
        private int _postalcode;
        private HealthinsuranceState _state = HealthinsuranceState.ByLaw;
        private string _street = string.Empty;
        private int _streetnumber;

        public long HealthinsuranceId
        {
            get => _healthinsuranceId;
            set
            {
                _healthinsuranceId = value;
                OnPropertyChanged(nameof(HealthinsuranceId));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value != string.Empty) _name = value;
                OnPropertyChanged(nameof(_name));
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged(nameof(_street));
            }
        }

        public int Streetnumber
        {
            get => _streetnumber;
            set
            {
                _streetnumber = value;
                OnPropertyChanged(nameof(_streetnumber));
            }
        }

        public int Postalcode
        {
            get => _postalcode;
            set
            {
                _postalcode = value;
                OnPropertyChanged(nameof(_postalcode));
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(_city));
            }
        }

        public HealthinsuranceState State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(_state));
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
            hash += Name.Trim().Length;
            hash += Street.Trim().Length;
            hash += Streetnumber * 1234;
            hash += Postalcode * 231;
            hash += City.Trim().Length;
            hash += State.GetHashCode();

            return hash;
        }
    }

    public enum HealthinsuranceState
    {
        Private,
        ByLaw
    }
}