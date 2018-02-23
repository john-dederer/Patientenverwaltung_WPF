using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using Patientenverwaltung_WPF;
using Patientenverwaltung_WPF.Converter;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        ICollectionView viewFilter = null;

        // Properties for Patient UI
        public ObservableCollection<Patient> Patients { get; set; }
        public CurrentPatient Patient { get; set; }
        public ObservableCollection<Treatment> Treatments { get; set; }

        // Properties for Healthinsurance UI
        public CurrentHealthinsurance Healthinsurance { get; set; }
        public ObservableCollection<Healthinsurance> Healthinsurances { get; set; }


        // Properties for UIState
        public UIState UIState { get; set; }

        // Choosing HI for patient
        public bool ChoosingHealthinsurance = false;

        public HomeWindow()
        {
            InitializeComponent();

            // Patient Properties
            Patient = new CurrentPatient();            
            Patients = CurrentContext.GetPatientListOC();
            Patient.Patient = CurrentContext.GetPatient();
            Treatments = new ObservableCollection<Treatment>();

            // Healthinsurance Properties
            Healthinsurance = new CurrentHealthinsurance();
            Healthinsurances = CurrentContext.GetHealthinsuranceOC();
            Healthinsurance.Healthinsurance = CurrentContext.GetHealthinsurance();

            DataContext = this;

            // First UIState is Patient
            UIState = UIState.Patient;
            viewFilter = CollectionViewSource.GetDefaultView(Patients);
        }

        private void AddPatient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                CreatePatientMask.Visibility = Visibility.Visible;
                TreatmentList.Visibility = Visibility.Hidden;
                Patient.Patient = new Patientenverwaltung_WPF.Patient();

                btnAddPatient.Visibility = Visibility.Visible;
                btnAddTreatmentForPatient.Visibility = Visibility.Hidden;
                btnChooseHI.Visibility = Visibility.Hidden;

                txtBoxFirstname.Focus();
            }
        }

        private void AddHealthinsurance_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                CreateHealthinsuranceMask.Visibility = Visibility.Visible;
                Healthinsurance.Healthinsurance = new Patientenverwaltung_WPF.Healthinsurance();
                btnAddHI.Visibility = Visibility.Visible;

                if (ChoosingHealthinsurance) btnHIChosen.Visibility = Visibility.Visible;

                HIName.Focus();                
            }
        }

        /// <summary>
        /// Selecting item from patient list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Patient test = ((Grid)sender).Tag as Patient;

            // Selected Item as current Patient Context
            Patient.Patient = test;

            TreatmentList.Visibility = Visibility.Visible;
            CreatePatientMask.Visibility = Visibility.Visible;
            btnAddPatient.Visibility = Visibility.Hidden;
            btnAddTreatmentForPatient.Visibility = Visibility.Visible;
            btnChooseHI.Visibility = Visibility.Visible;
        }

        private void SelectHealthinsuranceFromList(object sender, RoutedEventArgs e)
        {
            Healthinsurance healthinsurance = ((Grid)sender).Tag as Healthinsurance;

            Healthinsurance.Healthinsurance = healthinsurance;
            CreateHealthinsuranceMask.Visibility = Visibility.Visible;
            btnAddHI.Visibility = Visibility.Hidden;

            if (ChoosingHealthinsurance) btnHIChosen.Visibility = Visibility.Visible;
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            // First we have to check if username already exists
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(Patient.Patient, out Patient returned))
            {
                if (returned == null) return;

                if (Patient.Patient.GetHashCode() == returned.GetHashCode())
                {
                    // Show patient already exists

                }
            }
            else
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(Patient.Patient))
                {
                    // Successfully created                    
                    Patients.Add(Patient.Patient);
                    btnChooseHI.Visibility = Visibility.Visible;

                }
                else
                {
                    // Username already exists
                }
            }
        }

        private void AddTreatment(object sender, RoutedEventArgs e)
        {
            AddTreatment window = new AddTreatment(Patient.Patient.PatientId);
            if (window.ShowDialog() == true)
            {
                Treatments.Add(window.Treatment);

                DataGridTreatment.Items.Refresh();
            }
        }

        private void ChooseHealthinsurance(object sender, RoutedEventArgs e)
        {
            ChangeUIToHealthinsurance(null, null);
            ChoosingHealthinsurance = true;
            
        }

        private void Healthinsurance_Chosen(object sender, RoutedEventArgs e)
        {
            // Get chosen HI and update patient
            Patient.Patient.HealthinsuranceId = Healthinsurance.Healthinsurance.HealthinsuranceId;

            if (Factory.Get(CurrentContext.GetSettings().Savetype).Update(Patient.Patient))
            {
                // HI added to patient
            }
            else
            {

            }


            ChangeUIToPatients(null, null);
            ChoosingHealthinsurance = false;
        }

        private void AddHealthinsurance(object sender, RoutedEventArgs e)
        {
            // First we have to check if healthinsurance already             
            if (!Factory.Get(CurrentContext.GetSettings().Savetype).Select(Healthinsurance.Healthinsurance))
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(Healthinsurance.Healthinsurance))
                {
                    // Successfully created                    
                    Healthinsurances.Add(Healthinsurance.Healthinsurance);

                    btnAddHI.Visibility = Visibility.Hidden;
                }
                else
                {
                    // healthinsurance already exists
                }
            }            
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Update(Patient.Patient))
            {
                // Successfully updated
            }
            else
            {

            }
        }

        private void UpdateHealthinsurance(object sender, RoutedEventArgs e)
        {
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Update(Healthinsurance.Healthinsurance))
            {
                // Successfully updated
            }
            else
            {

            }
        }

        private void ChangeUIToSettings(object sender, RoutedEventArgs e)
        {
            if (UIState.Equals(UIState.Patient))
            {
                // Disable Patient UI elements
                MakePatientUIVisible(false);
            }
            else if (UIState.Equals(UIState.Healthinsurance))
            {
                // Do nothing
                MakeHealthinsuranceUIVisible(false);

            }
            else if (UIState.Equals(UIState.Settings))
            {
                // Do nothing
            }

            MakeSettingsUIVisible(true);

            //UIState = UIState.Settings;

            // Show Settings page
            SettingsWindow window = new SettingsWindow();

            if (window.ShowDialog() == true)
            {
                if (UIState == UIState.Patient) ChangeUIToPatients(null, null);
                else if (UIState == UIState.Healthinsurance) ChangeUIToHealthinsurance(null, null);
            }
        }

        private void ChangeUIToHealthinsurance(object sender, RoutedEventArgs e)
        {
            if (UIState.Equals(UIState.Patient))
            {
                // Disable Patient UI elements
                MakePatientUIVisible(false);                
            }
            else if (UIState.Equals(UIState.Healthinsurance))
            {
                // Do nothing
            }
            else if (UIState.Equals(UIState.Settings))
            {
                // Disable Settings UI elements
                MakeSettingsUIVisible(false);
            }

            MakeHealthinsuranceUIVisible(true);

            //Reload List
            //Healthinsurances = CurrentContext.GetHealthinsuranceOC();
            //viewFilter = CollectionViewSource.GetDefaultView(Healthinsurances);

            UIState = UIState.Healthinsurance;
        }

        private void ChangeUIToPatients(object sender, RoutedEventArgs e)
        {
            if (UIState.Equals(UIState.Patient))
            {
                // Do nothing                
            }
            else if (UIState.Equals(UIState.Healthinsurance))
            {
                // Disable Healthinsurance UI elements
                MakeHealthinsuranceUIVisible(false);
            }
            else if (UIState.Equals(UIState.Settings))
            {
                // Disable Settings UI elements
                MakeSettingsUIVisible(false);
            }

            MakePatientUIVisible(true);

            //Reload list
            //Patients = CurrentContext.GetPatientListOC();
            //viewFilter = CollectionViewSource.GetDefaultView(Patients);

            UIState = UIState.Patient;
        }

        private void MakeSettingsUIVisible(bool visible)
        {
            var visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        private void MakeHealthinsuranceUIVisible(bool visible)
        {
            var visibility = visible ? Visibility.Visible : Visibility.Hidden;

            AddHealthinsuranceCtrl.Visibility = visibility;
            CreateHealthinsuranceMask.Visibility = Visibility.Hidden;
            Healthinsurancelist.Visibility = visibility;            
        }

        private void MakePatientUIVisible(bool visible)
        {
            var visibility = visible ? Visibility.Visible : Visibility.Hidden;

            CreatePatientMask.Visibility = Visibility.Hidden;
            TreatmentList.Visibility = Visibility.Hidden;
            Patientlist.Visibility = visibility;
            AddPatientCtrl.Visibility = visibility;
        }

        private void searchField_Changed(object sender, TextChangedEventArgs e)
        {
            if (txtSearchField.Text == string.Empty)
            {
                viewFilter.Filter = null;
                viewFilter.Refresh();

                // Hide create mask
                if (UIState == UIState.Patient) CreatePatientMask.Visibility = Visibility.Hidden;
                else if (UIState == UIState.Healthinsurance) CreateHealthinsuranceMask.Visibility = Visibility.Hidden;

                return;
            }

            if (UIState == UIState.Patient)
            {
                viewFilter = CollectionViewSource.GetDefaultView(Patients);
                viewFilter.Filter = delegate (object item) {
                    return ((Patient)item).Firstname.ToLower().Contains(txtSearchField.Text.ToLower());
                };
            }
            else if (UIState == UIState.Healthinsurance)
            {
                viewFilter = CollectionViewSource.GetDefaultView(Healthinsurances);
                viewFilter.Filter = delegate (object item) {
                    return ((Healthinsurance)item).Name.ToLower().Contains(txtSearchField.Text.ToLower());
                };
            }


            viewFilter.Refresh();


        }
    }
}

/// <summary>
/// Helperclass to achieve a reload of a list
/// </summary>
public class CurrentPatient : INotifyPropertyChanged
{
    private Patient patient = new Patient();

    public Patient Patient { get { return patient; } set { patient = value; OnPropertyChanged("Patient"); } }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

/// <summary>
/// Helperclass to achieve a realod of a list
/// </summary>
public class CurrentHealthinsurance : INotifyPropertyChanged
{
    private Healthinsurance healthinsurance = new Healthinsurance();

    public Healthinsurance Healthinsurance { get { return healthinsurance; } set { healthinsurance = value; OnPropertyChanged("Healthinsurance"); } }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

/// <summary>
/// Defines the current UIState
/// </summary>
public enum UIState
{
    Patient,
    Healthinsurance,
    Settings
}