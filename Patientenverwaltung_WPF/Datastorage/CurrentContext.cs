using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Patientenverwaltung_WPF
{
    public static class CurrentContext
    {
        private static Settings _settings;
        private static User _user;
        private static IdCounter _idCounter;
        private static ObservableCollection<Patient> _patientListViewModel;
        private static List<Patient> _patientList;
        private static Patient _patient;
        private static ObservableCollection<Treatment> _treatmentListViewModel;
        private static List<Treatment> _treatmentList;

        private static List<Healthinsurance> _healthinsuranceList;
        private static Healthinsurance _healthinsurance;
        private static ObservableCollection<Healthinsurance> _healthinsuranceViewModel;

        private static ObservableCollection<User> _userCollection;
        private static List<User> _userList;

        public static ref Settings GetSettings()
        {
            if (_settings == null) _settings = new Settings();
            return ref _settings;
        }

        public static ref User GetUser()
        {
            if (_user == null) _user = new User();
            return ref _user;
        }

        internal static ref ObservableCollection<Patient> GetPatientListOc()
        {
            _patientListViewModel = new ObservableCollection<Patient>(GetPatientList());

            return ref _patientListViewModel;
        }

        internal static ref List<Patient> GetPatientList()
        {
            if (_patientList == null) _patientList = new List<Patient>();

            // GetData from storage
            _patientList = Factory.Get(GetSettings().Savetype).GetPatientList();
            return ref _patientList;
        }

        public static ref IdCounter GetIdCounter()
        {
            if (_idCounter == null) _idCounter = new IdCounter();
            return ref _idCounter;
        }

        internal static ref ObservableCollection<Treatment> GetTreatmentListOc()
        {
            _treatmentListViewModel = new ObservableCollection<Treatment>(GetTreatmentList());

            return ref _treatmentListViewModel;
        }

        internal static ref List<Treatment> GetTreatmentList()
        {
            if (_treatmentList == null) _treatmentList = new List<Treatment>();

            // GetData from storage
            _treatmentList = Factory.Get(GetSettings().Savetype).GetTreatmentList();
            return ref _treatmentList;
        }

        public static ref Patient GetPatient()
        {
            if (_patient == null) _patient = new Patient();
            return ref _patient;
        }

        public static ref Healthinsurance GetHealthinsurance()
        {
            if (_healthinsurance == null) _healthinsurance = new Healthinsurance();
            return ref _healthinsurance;
        }

        internal static ref ObservableCollection<Healthinsurance> GetHealthinsuranceOc()
        {
            _healthinsuranceViewModel = new ObservableCollection<Healthinsurance>(GetHealthinsuranceList());

            return ref _healthinsuranceViewModel;
        }

        private static ref List<Healthinsurance> GetHealthinsuranceList()
        {
            if (_healthinsuranceList == null) _healthinsuranceList = new List<Healthinsurance>();

            // GetData from storage
            _healthinsuranceList = Factory.Get(GetSettings().Savetype).GetHealthinsuranceList();
            return ref _healthinsuranceList;
        }

        public static ref ObservableCollection<User> GetUserOc()
        {
            _userCollection = new ObservableCollection<User>(GetUserList());

            return ref _userCollection;
        }

        private static ref List<User> GetUserList()
        {
            if (_userList == null) _userList = new List<User>();

            // GetData from storage
            _userList = Factory.Get(GetSettings().Savetype).GetUserList();
            return ref _userList;
        }
    }
}