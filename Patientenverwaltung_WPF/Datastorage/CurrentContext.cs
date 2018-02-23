using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    public static class CurrentContext
    {
        private static Settings Settings;
        private static User User;
        private static IdCounter IdCounter;
        private static ObservableCollection<Patient> PatientListViewModel;
        private static List<Patient> PatientList;
        private static Patient Patient;

        private static List<Healthinsurance> HealthinsuranceList;
        private static Healthinsurance Healthinsurance;
        private static ObservableCollection<Healthinsurance> HealthinsuranceViewModel;

        public static ref Settings GetSettings()
        {
            if (Settings == null) Settings = new Settings();
            return ref Settings;
        }

        public static ref User GetUser()
        {
            if (User == null) User = new User();
            return ref User;
        }

        internal static ref ObservableCollection<Patient> GetPatientListOC()
        {
            PatientListViewModel = new ObservableCollection<Patient>(GetPatientList());

            return ref PatientListViewModel;
        }

        internal static ref List<Patient> GetPatientList()
        {
            if (PatientList == null) PatientList = new List<Patient>();

            // GetData from storage
            PatientList = Factory.Get(GetSettings().Savetype).GetPatientList();
            return ref PatientList;
        }

        public static ref IdCounter GetIdCounter()
        {
            if (IdCounter == null) IdCounter = new IdCounter();
            return ref IdCounter;
        }

        public static ref Patient GetPatient()
        {
            if (Patient == null) Patient = new Patient();
            return ref Patient;
        }

        public static ref Healthinsurance GetHealthinsurance()
        {
            if (Healthinsurance == null) Healthinsurance = new Healthinsurance();
            return ref Healthinsurance;
        }

        internal static ref ObservableCollection<Healthinsurance> GetHealthinsuranceOC()
        {
            HealthinsuranceViewModel = new ObservableCollection<Healthinsurance>(GetHealthinsuranceList());

            return ref HealthinsuranceViewModel;
        }

        private static ref List<Healthinsurance> GetHealthinsuranceList()
        {
            if (HealthinsuranceList == null) HealthinsuranceList = new List<Healthinsurance>();

            // GetData from storage
            HealthinsuranceList = Factory.Get(GetSettings().Savetype).GetHealthinsuranceList();
            return ref HealthinsuranceList;
        }
    }
}
