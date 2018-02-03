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

        internal static ref ObservableCollection<Patient> GetPatientListViewModel()
        {
            PatientListViewModel = new ObservableCollection<Patient>(GetPatientList());

            return ref PatientListViewModel;
        }

        internal static ref List<Patient> GetPatientList()
        {
            if (PatientList == null) PatientList = new List<Patient>();

            // GetData from storage
            PatientList = Factory.Get(CurrentContext.GetSettings().Savetype).GetList();
            return ref PatientList;
        }

        public static ref IdCounter GetIdCounter()
        {
            if (IdCounter == null) IdCounter = new IdCounter();
            return ref IdCounter;
        }
    }
}
