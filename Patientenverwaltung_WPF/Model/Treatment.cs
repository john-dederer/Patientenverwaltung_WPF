using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Patientenverwaltung_WPF
{
    public class Treatment : Datamodel, INotifyPropertyChanged
    {
        private long treatmentId = 0;
        private long patientId = 0;

        private DateTime date = DateTime.Now;
        private string description = "";
        private string other = "";




        public long TreatmentId { get { return treatmentId; } set { treatmentId = value; OnPropertyChanged(nameof(TreatmentId)); } }
        public long PatientId { get { return patientId; } set { patientId = value; OnPropertyChanged(nameof(PatientId)); } }

        public DateTime Date { get { return date; } set { date = value; OnPropertyChanged(nameof(Date)); } }
        public string Description { get { return description; } set { description = value; OnPropertyChanged(nameof(Description)); } }
        public string Other { get { return other; } set { other = value; OnPropertyChanged(nameof(Other)); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
