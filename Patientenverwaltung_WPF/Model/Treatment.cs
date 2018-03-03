using System;
using System.ComponentModel;

namespace Patientenverwaltung_WPF
{
    public class Treatment : PropertyChangedNotification
    {
        public long TreatmentId
        {
            get { return GetValue(() => TreatmentId); }
            set { SetValue(() => TreatmentId, value); }
        }

        public long PatientId
        {
            get { return GetValue(() => PatientId); }
            set { SetValue(() => PatientId, value); }
        }

        public DateTime Date
        {
            get { return GetValue(() => Date); }
            set { SetValue(() => Date, value); }
        }

        public string Description
        {
            get { return GetValue(() => Description); }
            set { SetValue(() => Description, value); }
        }

        public string Other
        {
            get { return GetValue(() => Other); }
            set { SetValue(() => Other, value); }
        }

        public override int GetHashCode()
        {
            var hash = 0;
            hash += PatientId.GetHashCode();
            hash += Date.GetHashCode();
            if (Description != null) hash += Description.Trim().Length;
            if (Other != null) hash += Other.Trim().Length;

            return hash;
        }
    }
}