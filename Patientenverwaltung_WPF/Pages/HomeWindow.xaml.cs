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
        // Properties for Patient UI
        public ObservableCollection<Patient> Patients { get; set; }
        public CurrentPatient Patient { get; set; }
        public ObservableCollection<Treatment> Treatments { get; set; }

        // Properties for Healthinsurance UI
        public CurrentHealthinsurance Healthinsurance { get; set; }
        public ObservableCollection<Healthinsurance> Healthinsurances { get; set; }


        // Properties for UIState
        public UIState UIState { get; set; }

        public HomeWindow()
        {
            InitializeComponent();

            // Patient Properties
            Patient = new CurrentPatient();            
            Patients = CurrentContext.GetPatientListViewModel();
            Patient.Patient = CurrentContext.GetPatient();
            Treatments = new ObservableCollection<Treatment>();

            // Healthinsurance Properties
            Healthinsurance = new CurrentHealthinsurance();
            Healthinsurances = new ObservableCollection<Healthinsurance>();
            Healthinsurance.Healthinsurance = CurrentContext.GetHealthinsurance();

            DataContext = this;

            // First UIState is Patient
            UIState = UIState.Patient;
        }

        private void AddPatient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                CreatePatientMask.Visibility = Visibility.Visible;
                TreatmentList.Visibility = Visibility.Visible;
                Patient.Patient = new Patientenverwaltung_WPF.Patient();
            }
        }

        private void AddHealthinsurance_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                CreateHealthinsuranceMask.Visibility = Visibility.Visible;
                Healthinsurance.Healthinsurance = new Patientenverwaltung_WPF.Healthinsurance();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Patient test = ((Grid)sender).Tag as Patient;

            // Selected Item as current Patient Context
            Patient.Patient = test;        
        }

        private void SelectHealthinsuranceFromList(object sender, RoutedEventArgs e)
        {
            Healthinsurance healthinsurance = ((Grid)sender).Tag as Healthinsurance;

            Healthinsurance.Healthinsurance = healthinsurance;
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

        private void AddHealthinsurance(object sender, RoutedEventArgs e)
        {
            // First we have to check if healthinsurance already             
            if (!Factory.Get(CurrentContext.GetSettings().Savetype).Select(Healthinsurance.Healthinsurance))
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(Healthinsurance.Healthinsurance))
                {
                    // Successfully created                    
                    Healthinsurances.Add(Healthinsurance.Healthinsurance);
                }
                else
                {
                    // healthinsurance already exists
                }
            }            
        }

        private void ChangeUIToHealthinsurance(object sender, RoutedEventArgs e)
        {
            if (UIState.Equals(UIState.Patient))
            {
                // Disable Patient UI elements
                MakePatientUIVisible(false);
                MakeHealthinsuranceUIVisible(true);
            }
            else if (UIState.Equals(UIState.Healthinsurance))
            {
                // Do nothing
            }
            else if (UIState.Equals(UIState.Settings))
            {
                // Disable Settings UI elements
                MakeSettingsUIVisible(false);
                MakeHealthinsuranceUIVisible(true);
            }

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
                MakePatientUIVisible(true);
            }
            else if (UIState.Equals(UIState.Settings))
            {
                // Disable Settings UI elements
                MakeSettingsUIVisible(false);
                MakePatientUIVisible(true);
            }

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
            CreateHealthinsuranceMask.Visibility = visibility;
            Healthinsurancelist.Visibility = visibility;
        }

        private void MakePatientUIVisible(bool visible)
        {
            var visibility = visible ? Visibility.Visible : Visibility.Hidden;

            CreatePatientMask.Visibility = visibility;
            TreatmentList.Visibility = visibility;
            Patientlist.Visibility = visibility;
            SearchPatient.Visibility = visibility;
            AddPatientCtrl.Visibility = visibility;
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