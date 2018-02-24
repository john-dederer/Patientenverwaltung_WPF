using System;
using System.ComponentModel;

namespace Patientenverwaltung_WPF
{
    public class Treatment : Datamodel, INotifyPropertyChanged
    {
        private DateTime _date = DateTime.Now;
        private string _description = "";
        private string _other = "";
        private long _patientId;
        private long _treatmentId;


        public long TreatmentId
        {
            get => _treatmentId;
            set
            {
                _treatmentId = value;
                OnPropertyChanged(nameof(TreatmentId));
            }
        }

        public long PatientId
        {
            get => _patientId;
            set
            {
                _patientId = value;
                OnPropertyChanged(nameof(PatientId));
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Other
        {
            get => _other;
            set
            {
                _other = value;
                OnPropertyChanged(nameof(Other));
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
            hash += PatientId.GetHashCode();
            hash += Date.GetHashCode();
            hash += Description.Trim().Length;
            hash += Other.Trim().Length;

            return hash;
        }
    }
}