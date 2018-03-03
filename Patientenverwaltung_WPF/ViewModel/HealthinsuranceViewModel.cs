using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Patientenverwaltung_WPF.Notification;

namespace Patientenverwaltung_WPF.ViewModel
{
    public class HealthinsuranceViewModel : PropertyChangedNotification
    {
        private static HealthinsuranceViewModel _healthinsuranceViewModel;
        private static ICollectionView _collectionView = null;

        /// <summary>
        /// List of all healthinsurances
        /// </summary>
        public ObservableCollection<Healthinsurance> Healthinsurances
        {
            get { return GetValue(() => Healthinsurances); }
            set { SetValue(() => Healthinsurances, value); }
        }

        /// <summary>
        /// New healthinsurance to be added thorugh form
        /// </summary>
        public Healthinsurance NewHealthinsurance
        {
            get { return GetValue(() => NewHealthinsurance); }
            set { SetValue(() => NewHealthinsurance, value); }
        }

        /// <summary>
        /// Used to add the new healthinsurance to the healthinsurance Observable collection
        /// </summary>
        public RelayCommand CreateCommand { get; set; }

        /// <summary>
        /// Used to update the data to the datastorage
        /// </summary>
        public RelayCommand UpdateCommand { get; set; }

        /// <summary>
        /// Used to choose the healthinsurance for a patient
        /// </summary>
        public RelayCommand ChooseCommand { get; set; }

        /// <summary>
        /// Used to delete a healthinsurance from the datastorage and the observable collection
        /// </summary>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Counts the errors in the form
        /// </summary>
        public static int Errors { get; set; }

        /// <summary>
        /// Notifies the Ui to show elements
        /// </summary>
        public bool ShowCreateHiMask
        {
            get { return GetValue(() => ShowCreateHiMask); }
            set { SetValue(() => ShowCreateHiMask, value); }
        }

        /// <summary>
        /// Notifies the ui to show the add control
        /// </summary>
        public bool ShowAddHiUi
        {
            get { return GetValue(() => ShowAddHiUi); }
            set { SetValue(() => ShowAddHiUi, value); }
        }

        /// <summary>
        /// Notifies the ui to show the list control
        /// </summary>
        public bool ShowHiListUi
        {
            get { return GetValue(() => ShowHiListUi); }
            set { SetValue(() => ShowHiListUi, value); }
        }

        /// <summary>
        /// For deciding when the choose button is enabled
        /// </summary>
        public bool ChoosingHiForPatient
        {
            get { return GetValue(() => ChoosingHiForPatient); }
            set { SetValue(() => ChoosingHiForPatient, value); }
        }

        /// <summary>
        /// Checks if HI is to be created
        /// </summary>
        public bool IsHIBeingCreated
        {
            get { return GetValue(() => IsHIBeingCreated); }
            set { SetValue(() => IsHIBeingCreated, value); }
        }

        /// <summary>
        /// Filtering predicate for healthinsurance list
        /// </summary>
        public string FilterPredicate { get; set; }


        /// <summary>
        /// Viewmodel used as the datacontext in the view
        /// </summary>
        /// <returns></returns>
        public static HealthinsuranceViewModel SharedViewModel()
        {
            return _healthinsuranceViewModel ?? (_healthinsuranceViewModel = new HealthinsuranceViewModel());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private HealthinsuranceViewModel()
        {
            // Load data from datastorage
            Healthinsurances = CurrentContext.GetHealthinsuranceOc();

            // Relay commands
            NewHealthinsurance = new Healthinsurance();
            CreateCommand = new RelayCommand(Create, CanCreate);
            UpdateCommand = new RelayCommand(Update, CanExecute);
            ChooseCommand = new RelayCommand(Choose, CanChoose);
            DeleteCommand = new RelayCommand(Delete, CanDelete);

            FilterPredicate = string.Empty;
            Errors = 0;
            ShowCreateHiMask = false;
            ShowAddHiUi = false;
            ShowHiListUi = false;
            ChoosingHiForPatient = false;
            IsHIBeingCreated = false;
        }

        /// <summary>
        /// Action for relay command.
        /// Adds healthinsurance to observable collection and datastorage.
        /// </summary>
        /// <param name="parameter"></param>
        public void Create(object parameter)
        {
            if (!Factory.Get(CurrentContext.GetSettings().Savetype).Create(NewHealthinsurance)) return;
            Healthinsurances.Add(NewHealthinsurance);

            IsHIBeingCreated = false;
        }

        /// <summary>
        /// Action for relay command.
        /// Clears the form.
        /// </summary>
        /// <param name="parameter"></param>
        public void Clear(object parameter)
        {
            NewHealthinsurance = new Healthinsurance();
        }

        /// <summary>
        /// Predicate for relay command.
        /// Checks if action can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanCreate(object parameter)
        {
            return Errors == 0 && !string.IsNullOrEmpty(NewHealthinsurance.Name) && IsHIBeingCreated;
        }

        /// <summary>
        /// Predicate for relay commands.
        /// Checks if no errors exist
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return Errors == 0 && !string.IsNullOrEmpty(NewHealthinsurance.Name) && !IsHIBeingCreated;
        }

        /// <summary>
        /// Predicate for relay commands.
        /// Checks if HI is not null
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanDelete(object parameter)
        {
            return !string.IsNullOrEmpty(NewHealthinsurance.Name) && !IsHIBeingCreated;
        }

        /// <summary>
        /// Action for relay command.
        /// Updates the healthinsurance in datastorage
        /// </summary>
        /// <param name="parameter"></param>
        public void Update(object parameter)
        {
            Factory.Get(CurrentContext.GetSettings().Savetype).Update(NewHealthinsurance);
        }

        /// <summary>
        /// Action for relay command.
        /// Sets the healthinsurance for the patient.
        /// </summary>
        /// <param name="parameter"></param>
        public void Choose(object parameter)
        {
            UiHelper.UiState = UIState.Healthinsurance;

            // Get chosen HI and update patient
            PatientViewModel.SharedViewModel().NewPatient.HealthinsuranceId = NewHealthinsurance.HealthinsuranceId;

            ChoosingHiForPatient = !Factory.Get(CurrentContext.GetSettings().Savetype)
                .Update(PatientViewModel.SharedViewModel().NewPatient);
        }

        /// <summary>
        /// Predicate for relay command.
        /// Checks if button is enabled
        /// </summary>
        public bool CanChoose(object parameter)
        {
            return ChoosingHiForPatient;
        }

        /// <summary>
        /// Action for relay command.
        /// Deletes the healthinsurance from datastorage.
        /// </summary>
        /// <param name="parameter"></param>
        public void Delete(object parameter)
        {
            if (!Factory.Get(CurrentContext.GetSettings().Savetype).Delete(NewHealthinsurance)) return;

            Healthinsurances.Remove(NewHealthinsurance);
            Clear(this);

            ShowCreateHiMask = false;
        }

        /// <summary>
        /// Filters the list , based on a predicate if needed
        /// </summary>
        public void FilterList()
        {
            if (!string.IsNullOrEmpty(FilterPredicate))
            {
                // Show healthinsurances filtered
                //_collectionView.Filter = null;

                _collectionView = CollectionViewSource.GetDefaultView(Healthinsurances);
                _collectionView.Filter = delegate (object item)
                {
                    if (item == null) return false;
                    var nameContains = ((Healthinsurance)item).Name.Contains(FilterPredicate);

                    return nameContains;
                };
            }
            else
            {
                _collectionView = CollectionViewSource.GetDefaultView(Healthinsurances);
                _collectionView.Filter = null;
            }



            _collectionView.Refresh();
        }
    }
}
