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

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public CurrentPatient Patient { get; set; }
        public ObservableCollection<Treatment> Treatments { get; set; }


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

        public HomeWindow()
        {
            InitializeComponent();
            Patient = new CurrentPatient();

            Patients = CurrentContext.GetPatientListViewModel();
            Patient.Patient = CurrentContext.GetPatient();
            Treatments = new ObservableCollection<Treatment>();

            DataContext = this;

        }

        private void AddPatient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                CreatePatientMask.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // First we have to check if username already exists
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(CurrentContext.GetPatient(), out Patient returned))
            {
                if (returned == null) return;

                if (CurrentContext.GetPatient().GetHashCode() == returned.GetHashCode())
                {
                    // Show username already exists

                }
            }
            else
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(CurrentContext.GetPatient()))
                {
                    // Successfully created                    
                    Patients.Add(CurrentContext.GetPatient());


                }
                else
                {
                    // Username already exists
                }
            }
        }

        private void PatientListItemControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Patient test = ((Grid)sender).Tag as Patient;

            // Selected Item as current Patient Context
            CurrentContext.GetPatient() = test;
            Patient.Patient = test;

            DataContext = this;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // First we have to check if username already exists
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(CurrentContext.GetPatient(), out Patient returned))
            {
                if (returned == null) return;

                if (CurrentContext.GetPatient().GetHashCode() == returned.GetHashCode())
                {
                    // Show username already exists

                }
            }
            else
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(CurrentContext.GetPatient()))
                {
                    // Successfully created                    
                    Patients.Add(CurrentContext.GetPatient());


                }
                else
                {
                    // Username already exists
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // First we have to check if username already exists
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(CurrentContext.GetPatient(), out Patient returned))
            {
                if (returned == null) return;

                if (CurrentContext.GetPatient().GetHashCode() == returned.GetHashCode())
                {
                    // Show username already exists

                }
            }
            else
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(CurrentContext.GetPatient()))
                {
                    // Successfully created                    
                    Patients.Add(CurrentContext.GetPatient());


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
    }
}
