using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patientenverwaltung_WPF.Notification;
using Patientenverwaltung_WPF.ViewModel;
using System.Windows.Data;

namespace Patientenverwaltung_WPF
{
    public class PatientViewModel : PropertyChangedNotification
    {
        private static PatientViewModel _patientViewModel;
        private static ICollectionView  _collectionView = null;
        
        /// <summary>
        /// List of all Patients
        /// </summary>
        public ObservableCollection<Patient> Patients
        {
            get { return GetValue(() => Patients); }
            set { SetValue(() => Patients, value); }
        }

        /// <summary>
        /// New patient to be added thorugh form
        /// </summary>
        public Patient NewPatient
        {
            get { return GetValue(() => NewPatient); }
            set { SetValue(() => NewPatient, value); }
        }

        /// <summary>
        /// Used to add the new patient to the patient Observable collection
        /// </summary>
        public RelayCommand CreateCommand { get; set; }

        /// <summary>
        /// Used to update the data to the datastorage
        /// </summary>
        public RelayCommand UpdateCommand { get; set; }

        /// <summary>
        /// Used to create treatments for a patient
        /// </summary>
        public RelayCommand CreateTreatmentCommand { get; set; }

        /// <summary>
        /// Used to choose a healthinsurance for a patient
        /// </summary>
        public RelayCommand ChooseHealthinsuranceCommand { get; set; }

        /// <summary>
        /// Used to delete a patient from the datastorage and the observable collection
        /// </summary>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Counts the errors in the form
        /// </summary>
        public static int Errors { get; set; }

        /// <summary>
        /// Used to inform the Ui to show the form
        /// </summary>
        public bool ShowCreateMaskUi
        {
            get { return GetValue(() => ShowCreateMaskUi); }
            set { SetValue(() => ShowCreateMaskUi, value); }
        }

        /// <summary>
        /// Used to inform the Ui to show the form
        /// </summary>
        public bool ShowTreatmentListUi
        {
            get { return GetValue(() => ShowTreatmentListUi); }
            set { SetValue(() => ShowTreatmentListUi, value); }
        }

        /// <summary>
        /// Used to inform the Ui to show the form
        /// </summary>
        public bool ShowAddPatientUi
        {
            get { return GetValue(() => ShowAddPatientUi); }
            set { SetValue(() => ShowAddPatientUi, value); }
        }

        /// <summary>
        /// Used to inform the Ui to show the form
        /// </summary>
        public bool ShowPatientListUi
        {
            get { return GetValue(() => ShowPatientListUi); }
            set { SetValue(() => ShowPatientListUi, value); }
        }

        /// <summary>
        /// Indicating if currently a patient is being added
        /// </summary>
        public bool IsPatientBeingCreated
        {
            get { return GetValue(() => IsPatientBeingCreated); }
            set { SetValue(() => IsPatientBeingCreated, value); }
        }

        /// <summary>
        /// Indicating if currently a patient is being updated
        /// </summary>
        public bool IsPatientBeingUpdated { get; set; }

        /// <summary>
        /// Filter predicate for Listfiltering
        /// </summary>
        public string FilterPredicate { get; set; }        

        /// <summary>
        /// Viewmodel used as the datacontext in the view
        /// </summary>
        /// <returns></returns>
        public static PatientViewModel SharedViewModel()
        {
            return _patientViewModel ?? (_patientViewModel = new PatientViewModel());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private PatientViewModel()
        {
            // Load data from datastorage
            Patients = CurrentContext.GetPatientListOc();

            // Relay commands
            NewPatient = new Patient {Birthday = DateTime.Now};
            CreateCommand = new RelayCommand(Create, CanCreate);
            UpdateCommand = new RelayCommand(Update, CanDelete);
            CreateTreatmentCommand = new RelayCommand(CreateTreatment, CanDelete);
            ChooseHealthinsuranceCommand = new RelayCommand(Choose, CanDelete);
            DeleteCommand = new RelayCommand(Delete, CanDelete);

            // Init static variables
            //Errors = 0;
            FilterPredicate = string.Empty;
            ShowCreateMaskUi = false;
            ShowTreatmentListUi = false;
            ShowAddPatientUi = false;
            ShowPatientListUi = false;
            IsPatientBeingCreated = false;
            IsPatientBeingUpdated = false;
        }

        /// <summary>
        /// Action for relay command.
        /// Adds patient to observable collection and datastorage.
        /// </summary>
        /// <param name="parameter"></param>
        public void Create(object parameter)
        {
            if (!Factory.Get(CurrentContext.GetSettings().Savetype).Create(NewPatient)) return;
            Patients.Add(NewPatient);

            TreatmentViewModel.SharedViewModel().ShowSearchTreatmentUi = true;

            IsPatientBeingCreated = false;
        }

        /// <summary>
        /// Action for relay command
        /// Opend new window
        /// </summary>
        /// <param name="parameter"></param>
        public void CreateTreatment(object parameter)
        {
            var window = new AddTreatment(NewPatient.PatientId);
            var dialogResult = window.ShowDialog();

            if (dialogResult != true) return;
            TreatmentViewModel.SharedViewModel().NewTreatment = window.Treatment;
            TreatmentViewModel.SharedViewModel().Create(null);
            TreatmentViewModel.SharedViewModel().FilterTreatments();
        }

        /// <summary>
        /// Action for relay command.
        /// Clears the form.
        /// </summary>
        /// <param name="parameter"></param>
        public void Clear(object parameter)
        {
            NewPatient = new Patient {Birthday = DateTime.Now};
        }

        /// <summary>
        /// Predicate for relay command.
        /// Checks if action can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanCreate(object parameter)
        {
            return Errors == 0 && IsPatientBeingCreated;
        }

        /// <summary>
        /// Predicate for relay commands.
        /// Checks if patient is not null, no errors exist and patient was selected from list
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanDelete(object parameter)
        {
            return !string.IsNullOrEmpty(NewPatient.Firstname) && Errors == 0 && !IsPatientBeingCreated;
        }

        /// <summary>
        /// Action for relay command.
        /// Updates the patient in datastorage
        /// </summary>
        /// <param name="parameter"></param>
        public void Update(object parameter)
        {
            Factory.Get(CurrentContext.GetSettings().Savetype).Update(NewPatient);

            IsPatientBeingUpdated = true;

            var index = Patients.IndexOf(NewPatient);
            Patients.Remove(NewPatient);
            Patients.Insert(index, NewPatient);
        }

        /// <summary>
        /// Action for relay command.
        /// Sets the healthinsurance for the patient.
        /// </summary>
        /// <param name="parameter"></param>
        public void Choose(object parameter)
        {
            HealthinsuranceViewModel.SharedViewModel().ChoosingHiForPatient = true;
            UiHelper.SwitchUiToHealthinsurance();            
        }

        /// <summary>
        /// Action for relay command.
        /// Deletes the patient from datastorage.
        /// </summary>
        /// <param name="parameter"></param>
        public void Delete(object parameter)
        {
            if (!Factory.Get(CurrentContext.GetSettings().Savetype).Delete(NewPatient)) return;
            Patients.Remove(NewPatient);

            ShowCreateMaskUi = false;
            ShowTreatmentListUi = false;
            TreatmentViewModel.SharedViewModel().ShowSearchTreatmentUi = false;

            Clear(this);            
        }

        /// <summary>
        /// Filter list based on filter predicate
        /// </summary>
        public void FilterList()
        {
           if (!string.IsNullOrEmpty(FilterPredicate))
            {
                // Show patients filtered
                //_collectionView.Filter = null;

                _collectionView = CollectionViewSource.GetDefaultView(Patients);
                _collectionView.Filter = delegate (object item)
                {
                    if (item == null) return false;                    
                    var firstNameContains = ((Patient)item).Firstname.Contains(FilterPredicate);

                    return firstNameContains;
                };
            }
            else
          {                
                _collectionView = CollectionViewSource.GetDefaultView(Patients);
              _collectionView.Filter = null;

          }



            _collectionView.Refresh();
        }
    }
}
