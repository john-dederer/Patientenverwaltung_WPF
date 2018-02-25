using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Patientenverwaltung_WPF
{
    public class TreatmentViewModel : PropertyChangedNotification
    {
        private static TreatmentViewModel _treatmentViewModel;

        private static ICollectionView _collectionView = null;

        /// <summary>
        /// List of all Treatments
        /// </summary>
        public ObservableCollection<Treatment> Treatments
        {
            get { return GetValue(() => Treatments); }
            set { SetValue(() => Treatments, value); }
        }

        /// <summary>
        /// New treatment to be added thorugh form
        /// </summary>
        public Treatment NewTreatment
        {
            get { return GetValue(() => NewTreatment); }
            set { SetValue(() => NewTreatment, value); }
        }

        /// <summary>
        /// Used to update the data to the datastorage
        /// </summary>
        public RelayCommand UpdateCommand { get; set; }

        /// <summary>
        /// Used to delete a treatment from the datastorage and the observable collection
        /// </summary>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Counts the errors in the form
        /// </summary>
        public static int Errors { get; set; }

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
        public bool ShowUpdateButton
        {
            get { return GetValue(() => ShowUpdateButton); }
            set { SetValue(() => ShowUpdateButton, value); }
        }

        /// <summary>
        /// Used to inform the Ui to show the form
        /// </summary>
        public bool ShowDeleteButton
        {
            get { return GetValue(() => ShowDeleteButton); }
            set { SetValue(() => ShowDeleteButton, value); }
        }

        /// <summary>
        /// Used to inform the UI to show a search field
        /// </summary>
        public bool ShowSearchTreatmentUi
        {
            get { return GetValue(() => ShowSearchTreatmentUi); }
            set { SetValue(() => ShowSearchTreatmentUi, value); }
        }

        /// <summary>
        /// Filter the current list based on this predicate
        /// </summary>
        public string FilterPredicate { get; set; }

        /// <summary>
        /// Viewmodel used as the datacontext in the view
        /// </summary>
        /// <returns></returns>
        public static TreatmentViewModel SharedViewModel()
        {
            return _treatmentViewModel ?? (_treatmentViewModel = new TreatmentViewModel());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private TreatmentViewModel()
        {
            // Load data from datastorage
            Treatments = CurrentContext.GetTreatmentListOc();

            // Relay commands
            NewTreatment = new Treatment();
            UpdateCommand = new RelayCommand(Update);
            DeleteCommand = new RelayCommand(Delete);

            // Init static variables
            Errors = 0;
            FilterPredicate = string.Empty;

            ShowTreatmentListUi = false;
            ShowDeleteButton = false;
            ShowUpdateButton = false;
            ShowSearchTreatmentUi = false;
        }

        /// <summary>
        /// Action for relay command.
        /// Adds patient to observable collection and datastorage.
        /// </summary>
        /// <param name="parameter"></param>
        public void Create(object parameter)
        {
            Treatments.Add(NewTreatment);
        }

        /// <summary>
        /// Action for relay command.
        /// Clears the form.
        /// </summary>
        /// <param name="parameter"></param>
        public void Clear(object parameter)
        {
            NewTreatment = new Treatment();
        }

        /// <summary>
        /// Predicate for relay command.
        /// Checks if action can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanCreate(object parameter)
        {
            var res = Treatments.ToList().Exists(x => x.GetHashCode().Equals(NewTreatment.GetHashCode()));
            return (Errors == 0) && (res);
        }

        /// <summary>
        /// Action for relay command.
        /// Updates the treatment in datastorage
        /// </summary>
        /// <param name="parameter"></param>
        public void Update(object parameter)
        {
            Factory.Get(CurrentContext.GetSettings().Savetype).Update(NewTreatment);
        }

        /// <summary>
        /// Action for relay command.
        /// Sets the treatment for the patient.
        /// </summary>
        /// <param name="parameter"></param>
        public void Choose(object parameter)
        {

        }

        /// <summary>
        /// Action for relay command.
        /// Deletes the treatment from datastorage.
        /// </summary>
        /// <param name="parameter"></param>
        public void Delete(object parameter)
        {
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Delete(NewTreatment))
            {
                Treatments.Remove(NewTreatment);

                Clear(this);
            }
        }

        public void FilterTreatments()
        {
            if (!string.IsNullOrEmpty(FilterPredicate))
            {
                // Show treatments filtered
                _collectionView.Filter = null;

                _collectionView = CollectionViewSource.GetDefaultView(Treatments);
                _collectionView.Filter = delegate (object item)
                {
                    if (item == null) return false;
                    var forPatient = ((Treatment)item).PatientId.Equals(PatientViewModel.SharedViewModel().NewPatient.PatientId);
                    var fromDate = ((Treatment)item).Date.ToLongDateString().Contains(FilterPredicate);

                    return (forPatient && fromDate);
                };
            }
            else
            {
                // Show only treatments for my patient
                _collectionView = CollectionViewSource.GetDefaultView(Treatments);
                _collectionView.Filter = delegate (object item)
                {
                    if (item == null) return false;
                    return ((Treatment)item).PatientId.Equals(PatientViewModel.SharedViewModel().NewPatient.PatientId);
                };
            }

            

            _collectionView.Refresh();
        }
    }
}
