﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Patientenverwaltung_WPF;
using Patientenverwaltung_WPF.Notification;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    ///     Interaktionslogik für HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        // Choosing HI for patient
        public bool ChoosingHealthinsurance;

        private ICollectionView viewFilter;
        private ICollectionView _viewFilterTreatment;

        public HomeWindow()
        {
            InitializeComponent();

            // Patient Properties
            //Patient = new CurrentPatient();
            //Patients = CurrentContext.GetPatientListOc();
            //Patient.Patient = CurrentContext.GetPatient();
            //Treatment = new CurrentTreatment();
            //Treatments = CurrentContext.GetTreatmentListOc();
            //
            //// Healthinsurance Properties
            //Healthinsurance = new CurrentHealthinsurance();
            //Healthinsurances = CurrentContext.GetHealthinsuranceOc();
            //Healthinsurance.Healthinsurance = CurrentContext.GetHealthinsurance();

            // Test
            //Healthinsurance = HealthinsuranceViewModel.SharedViewModel();

            //DataContext = this;
            //
            //// First UIState is Patient
            //UiState = UIState.Patient;
            //viewFilter = CollectionViewSource.GetDefaultView(Patients);
            //_viewFilterTreatment = CollectionViewSource.GetDefaultView(Treatments);

            UiHelper.SwitchUiToPatient();
        }

        /*
        // Properties for Patient UI
        public ObservableCollection<Patient> Patients { get; set; }

        public CurrentPatient Patient { get; set; }
        public CurrentTreatment Treatment { get; set; }
        public ObservableCollection<Treatment> Treatments { get; set; }

        // Properties for Healthinsurance UI
        public CurrentHealthinsurance Healthinsurance { get; set; }

        public ObservableCollection<Healthinsurance> Healthinsurances { get; set; }
        public HealthinsuranceViewModel HealthinsuranceTest { get; set; }


        // Properties for UIState
        public UIState UiState { get; set; }

        // Selection Indicators
        public Border SelectionIndicatorPatient { get; set; }

        public Border SelectionIndicatorHI { get; set; }
        public bool HIAdded { get; private set; }
        public bool PatientAdded { get; private set; }

        private void AddPatient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                CreatePatientMask.Visibility = Visibility.Visible;
                //TreatmentList.Visibility = Visibility.Hidden;
                PatientViewModel.SharedViewModel().NewPatient = new Patient();
                Patient.Patient = new Patient();

                //btnAddPatient.Visibility = Visibility.Visible;
                //btnUpdatePatient.Visibility = Visibility.Hidden;
                //btnAddTreatmentForPatient.Visibility = Visibility.Hidden;
                //btnChooseHI.Visibility = Visibility.Hidden;
                //SearchFieldTreatment.Visibility = Visibility.Hidden;
                //btnDeletePatient.Visibility = Visibility.Hidden;
                //
                //txtBoxFirstname.Focus();
            }
        }

        private void AddHealthinsurance_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                CreateHealthinsuranceMask.Visibility = Visibility.Visible;
                Healthinsurance.Healthinsurance = new Healthinsurance();
                //btnAddHI.Visibility = Visibility.Visible;
                //btnUpdateHI.Visibility = Visibility.Hidden;
                //btnDeleteHI.Visibility = Visibility.Hidden;

                //if (ChoosingHealthinsurance) btnHIChosen.Visibility = Visibility.Visible;

                //HIName.Focus();
            }
        }

        /// <summary>
        ///     Selecting item from patient list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var test = ((Grid) sender).Tag as Patient;

            // Selected Item as current Patient Context
            Patient.Patient = test;

            // Selection indicator
            var grid = (Grid) sender;
            var selIndicator = (Border) grid.FindName("selIndicator");

            // Remove old selection indicator
            if (SelectionIndicatorPatient != null) SelectionIndicatorPatient.Visibility = Visibility.Hidden;

            SelectionIndicatorPatient = selIndicator;
            selIndicator.Visibility = Visibility.Visible;


            //TreatmentList.Visibility = Visibility.Visible;
            CreatePatientMask.Visibility = Visibility.Visible;
            //btnAddPatient.Visibility = Visibility.Hidden;
            //btnAddTreatmentForPatient.Visibility = Visibility.Visible;
            //btnChooseHI.Visibility = Visibility.Visible;
            //btnUpdatePatient.Visibility = Visibility.Visible;
            //SearchFieldTreatment.Visibility = Visibility.Visible;
            //btnDeletePatient.Visibility = Visibility.Visible;

            // Show only treatments for my patient
            _viewFilterTreatment = CollectionViewSource.GetDefaultView(Treatments);
            _viewFilterTreatment.Filter = delegate(object item)
            {
                if (item == null) return false;
                return ((Treatment) item).PatientId.Equals(Patient.Patient.PatientId);
            };

            _viewFilterTreatment.Refresh();
        }

        private void SelectHealthinsuranceFromList(object sender, RoutedEventArgs e)
        {
            var healthinsurance = ((Grid) sender).Tag as Healthinsurance;

            // Selection indicator
            var grid = (Grid) sender;
            var selIndicator = (Border) grid.FindName("selIndicator");

            // Remove old selection indicator
            if (SelectionIndicatorHI != null) SelectionIndicatorHI.Visibility = Visibility.Hidden;

            SelectionIndicatorHI = selIndicator;
            selIndicator.Visibility = Visibility.Visible;

            Healthinsurance.Healthinsurance = healthinsurance;
            CreateHealthinsuranceMask.Visibility = Visibility.Visible;
            //btnAddHI.Visibility = Visibility.Hidden;
            //btnUpdateHI.Visibility = Visibility.Visible;
            //btnDeleteHI.Visibility = Visibility.Visible;

            //if (ChoosingHealthinsurance) btnHIChosen.Visibility = Visibility.Visible;
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            // First we have to check if username already exists
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(Patient.Patient))
            {
                // Show patient already exists
                var infoMessageWindow =
                    new InfoMessageWindow(
                        $@"Patient {Patient.Patient.Firstname} {Patient.Patient.Secondname} exisitert bereits");
                infoMessageWindow.ShowDialog();

                //btnAddPatient.Visibility = Visibility.Hidden;
                //btnChooseHI.Visibility = Visibility.Visible;
                //btnUpdatePatient.Visibility = Visibility.Visible;
                //btnAddTreatmentForPatient.Visibility = Visibility.Visible;
                //btnDeletePatient.Visibility = Visibility.Visible;
            }
            else
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(Patient.Patient))
                {
                    // Successfully created                    
                    Patients.Add(Patient.Patient);
                    //btnAddPatient.Visibility = Visibility.Hidden;
                    //btnChooseHI.Visibility = Visibility.Visible;
                    //btnUpdatePatient.Visibility = Visibility.Visible;
                    //btnAddTreatmentForPatient.Visibility = Visibility.Visible;
                    //btnDeletePatient.Visibility = Visibility.Visible;

                    PatientAdded = true;

                    var infoMessageWindow = new InfoMessageWindow(
                        $@"Patient {Patient.Patient.Firstname} {Patient.Patient.Secondname} erfolgreich angelegt");
                    infoMessageWindow.ShowDialog();
                }
                else
                {
                    var infoMessageWindow = new InfoMessageWindow(
                        $@"Patient {Patient.Patient.Firstname} {
                                Patient.Patient.Secondname
                            } konnte nicht angelegt werden");
                    infoMessageWindow.ShowDialog();
                }
            }
        }

        private void AddTreatment(object sender, RoutedEventArgs e)
        {
            var window = new AddTreatment(Patient.Patient.PatientId);
            var dialogResult = window.ShowDialog();

            if (dialogResult == true)
            {
                Treatment.Treatment = window.Treatment;
                Treatments.Add(Treatment.Treatment);

                _viewFilterTreatment.Refresh();
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
                var infomsg =
                    new InfoMessageWindow(
                        $@"Krankenversicherung {Healthinsurance.Healthinsurance.Name} für Patient {
                                Patient.Patient.Firstname
                            } {Patient.Patient.Secondname} ausgewählt");
                infomsg.ShowDialog();
            }
            else
            {
                var infomsg =
                    new InfoMessageWindow(
                        $@"Krankenversicherung {Healthinsurance.Healthinsurance.Name} konnte für Patient {
                                Patient.Patient.Firstname
                            } {Patient.Patient.Secondname} nicht ausgewählt werden");
                infomsg.ShowDialog();
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

                    //btnAddHI.Visibility = Visibility.Hidden;
                    //btnDeleteHI.Visibility = Visibility.Visible;
                    //btnUpdateHI.Visibility = Visibility.Visible;

                    HIAdded = true;

                    var infoMessageWindow = new InfoMessageWindow("Krankenversicherung erfolgreich angelgt");
                    infoMessageWindow.ShowDialog();
                }
                else
                {
                    // healthinsurance already exists
                    var infoMessageWindow = new InfoMessageWindow("Krankenversicherung konnte nicht angelegt werden");
                    infoMessageWindow.ShowDialog();
                }
            }
            else
            {
                var infoMessageWindow = new InfoMessageWindow("Krankenversicherung exisitert bereits");
                infoMessageWindow.ShowDialog();
            }
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Update(Patient.Patient))
            {
                // Successfully updated
                var infoMessage =
                    new InfoMessageWindow(
                        $@"Patient {Patient.Patient.Firstname} {Patient.Patient.Secondname} erfolgreich geändert.");
                infoMessage.ShowDialog();
            }
            else
            {
                var infoMessage =
                    new InfoMessageWindow(
                        $@"Patient {Patient.Patient.Firstname} {
                                Patient.Patient.Secondname
                            } konnte nicht geändert werden.");
                infoMessage.ShowDialog();
            }
        }

        private void UpdateHealthinsurance(object sender, RoutedEventArgs e)
        {
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Update(Healthinsurance.Healthinsurance))
            {
                // Successfully updated
                var infoMessage =
                    new InfoMessageWindow(
                        $@"Krankenversicherung {Healthinsurance.Healthinsurance.Name} erfolgreich geändert.");
                infoMessage.ShowDialog();
            }
            else
            {
                var infoMessage =
                    new InfoMessageWindow(
                        $@"Krankenversicherung {Healthinsurance.Healthinsurance.Name} konnte nicht geändert werden.");
                infoMessage.ShowDialog();
            }
        }

        private void ChangeUIToSettings(object sender, RoutedEventArgs e)
        {
            if (UiState.Equals(UIState.Patient))
            {
                // Disable Patient UI elements
                MakePatientUIVisible(false);
            }
            else if (UiState.Equals(UIState.Healthinsurance))
            {
                // Do nothing
                MakeHealthinsuranceUIVisible(false);
            }
            else if (UiState.Equals(UIState.Settings))
            {
                // Do nothing
            }

            MakeSettingsUIVisible(true);

            //UIState = UIState.Settings;

            // Show Settings page
            var window = new SettingsWindow();

            if (window.ShowDialog() == true)
            {
                if (UiState == UIState.Patient) ChangeUIToPatients(null, null);
                else if (UiState == UIState.Healthinsurance) ChangeUIToHealthinsurance(null, null);

                var infomsg = new InfoMessageWindow("Einstellungen erfolgreich geändert");
                infomsg.ShowDialog();
            }
        }

        private void ChangeUIToHealthinsurance(object sender, RoutedEventArgs e)
        {
            if (UiState.Equals(UIState.Patient))
            {
                // Disable Patient UI elements
                MakePatientUIVisible(false);
            }
            else if (UiState.Equals(UIState.Healthinsurance))
            {
                // Do nothing
            }
            else if (UiState.Equals(UIState.Settings))
            {
                // Disable Settings UI elements
                MakeSettingsUIVisible(false);
            }

            MakeHealthinsuranceUIVisible(true);

            //Reload List
            //Healthinsurances = CurrentContext.GetHealthinsuranceOC();
            //viewFilter = CollectionViewSource.GetDefaultView(Healthinsurances);

            UiState = UIState.Healthinsurance;
        }

        private void ChangeUIToPatients(object sender, RoutedEventArgs e)
        {
            if (UiState.Equals(UIState.Patient))
            {
                // Do nothing                
            }
            else if (UiState.Equals(UIState.Healthinsurance))
            {
                // Disable Healthinsurance UI elements
                MakeHealthinsuranceUIVisible(false);
            }
            else if (UiState.Equals(UIState.Settings))
            {
                // Disable Settings UI elements
                MakeSettingsUIVisible(false);
            }

            MakePatientUIVisible(true);

            //Reload list
            //Patients = CurrentContext.GetPatientListOC();
            //viewFilter = CollectionViewSource.GetDefaultView(Patients);

            UiState = UIState.Patient;
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

            // Remove selection indicator
            if (SelectionIndicatorHI != null) SelectionIndicatorHI.Visibility = Visibility.Hidden;
        }

        private void MakePatientUIVisible(bool visible)
        {
            var visibility = visible ? Visibility.Visible : Visibility.Hidden;

            CreatePatientMask.Visibility = Visibility.Hidden;
            //TreatmentList.Visibility = Visibility.Hidden;
            Patientlist.Visibility = visibility;
            AddPatientCtrl.Visibility = visibility;
            //SearchFieldTreatment.Visibility = Visibility.Hidden;

            // Remove selection indicator
            if (SelectionIndicatorPatient != null) SelectionIndicatorPatient.Visibility = Visibility.Hidden;
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
                viewFilter.Filter = delegate (object item)
                {
                    return ((Patient)item).Firstname.ToLower().Contains(txtSearchField.Text.ToLower());
                };
            }
            else if (UIState == UIState.Healthinsurance)
            {
                viewFilter = CollectionViewSource.GetDefaultView(Healthinsurances);
                viewFilter.Filter = delegate (object item)
                {
                    return ((Healthinsurance)item).Name.ToLower().Contains(txtSearchField.Text.ToLower());
                };
            }


            viewFilter.Refresh();
            
        }

        private void TreatmentItem_FocusLost(object sender, RoutedEventArgs e)
        {
            var grid = (Grid) sender;
            var btn = (Button) grid.FindName("btnUpdateTreatment");
            btn.Visibility = Visibility.Collapsed;

            var btnDelete = (Button) grid.FindName("btnDeleteTreatment");
            btnDelete.Visibility = Visibility.Collapsed;

            var txtDesc = (TextBox) grid.FindName("txtDescription");
            txtDesc.IsReadOnly = true;
            txtDesc.MaxLines = 3;

            var txtOth = (TextBox) grid.FindName("txtOther");
            txtOth.IsReadOnly = true;
            txtOth.MaxLines = 3;
        }

        private void TreatmentItem_Selected(object sender, RoutedEventArgs e)
        {
            var treatment = ((Grid) sender).Tag as Treatment;
            Treatment.Treatment = treatment;

            var grid = (Grid) sender;
            var btn = (Button) grid.FindName("btnUpdateTreatment");
            btn.Visibility = Visibility.Visible;

            var btnDelete = (Button) grid.FindName("btnDeleteTreatment");
            btnDelete.Visibility = Visibility.Visible;

            var txtDesc = (TextBox) grid.FindName("txtDescription");
            txtDesc.IsReadOnly = false;
            txtDesc.MaxLines = 10;

            var txtOth = (TextBox) grid.FindName("txtOther");
            txtOth.IsReadOnly = false;
            txtOth.MaxLines = 10;
        }

        private void UpdateTreatment(object sender, RoutedEventArgs e)
        {
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Update(Treatment.Treatment))
            {
                // Show treatment updated successfully
                var infoMessage =
                    new InfoMessageWindow(
                        $@"Behandlung {Treatment.Treatment.TreatmentId} für {Patient.Patient.Firstname} {
                                Patient.Patient.Secondname
                            } erfolgreich geändert.");
                infoMessage.ShowDialog();
            }
            else
            {
                // show Update failed
                var infoMessage =
                    new InfoMessageWindow(
                        $@"Behandlung {Treatment.Treatment.TreatmentId} für {Patient.Patient.Firstname} {
                                Patient.Patient.Secondname
                            } konnte nicht geändert werden.");
                infoMessage.ShowDialog();
            }
        }


        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation
            var boxWindow =
                new MessageBoxWindow(
                    $@"Patient {Patient.Patient.Firstname} {Patient.Patient.Secondname} wirklich löschen ?");

            if (boxWindow.ShowDialog() == true)
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Delete(Patient.Patient))
                {
                    var index = Patients.IndexOf(Patient.Patient);
                    if (index == -1) return;

                    Patients.RemoveAt(index);

                    var infoMsg = new InfoMessageWindow(
                        $@"Patient {Patient.Patient.Firstname} {Patient.Patient.Secondname} erfolgreich gelöscht");
                    infoMsg.ShowDialog();

                    Patient.Patient = new Patient();
                    CreatePatientMask.Visibility = Visibility.Hidden;
                    //TreatmentList.Visibility = Visibility.Hidden;
                    //SearchFieldTreatment.Visibility = Visibility.Hidden;
                    _viewFilterTreatment = null;
                }
                else
                {
                    var infoMsg = new InfoMessageWindow(
                        $@"Patient {Patient.Patient.Firstname} {
                                Patient.Patient.Secondname
                            } konnte nicht gelöscht werden");
                    infoMsg.ShowDialog();
                }
        }

        private void DeleteHealthinsurance(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation
            var boxWindow =
                new MessageBoxWindow($@"Krankenversicherung {Healthinsurance.Healthinsurance.Name} wirklich löschen ?");

            if (boxWindow.ShowDialog() == true)
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Delete(Healthinsurance.Healthinsurance))
                {
                    var index = Healthinsurances.IndexOf(Healthinsurance.Healthinsurance);
                    if (index == -1) return;

                    Healthinsurances.RemoveAt(index);

                    var infoMsg =
                        new InfoMessageWindow(
                            $@"Krankenversicherung {Healthinsurance.Healthinsurance.Name} erfolgreich gelöscht");
                    infoMsg.ShowDialog();

                    Healthinsurance.Healthinsurance = new Healthinsurance();
                    CreateHealthinsuranceMask.Visibility = Visibility.Hidden;
                }
                else
                {
                    var infoMsg = new InfoMessageWindow(
                        $@"Krankenversicherung {Healthinsurance.Healthinsurance.Name} konnte nicht gelöscht werden");
                    infoMsg.ShowDialog();
                }
        }

        private void DeleteTreatment(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation
            var boxWindow =
                new MessageBoxWindow(
                    $@"Behandlung {Treatment.Treatment.TreatmentId} vom {
                            Treatment.Treatment.Date.ToShortDateString()
                        } wirklich löschen ?");

            if (boxWindow.ShowDialog() == true)
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Delete(Treatment.Treatment))
                {
                    var index = Treatments.IndexOf(Treatment.Treatment);
                    if (index == -1) return;

                    Treatments.RemoveAt(index);

                    var infoMsg = new InfoMessageWindow(
                        $@"Behandlung {Treatment.Treatment.TreatmentId} vom {
                                Treatment.Treatment.Date.ToShortDateString()
                            } erfolgreich gelöscht");
                    infoMsg.ShowDialog();

                    Treatment.Treatment = new Treatment();
                }
                else
                {
                    var infoMsg = new InfoMessageWindow(
                        $@"Behandlung {Treatment.Treatment.TreatmentId} vom {
                                Treatment.Treatment.Date.ToShortDateString()
                            } konnte nicht gelöscht werden");
                    infoMsg.ShowDialog();
                }
            }
        }

        private void HIGridItem_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            var selInd = (Border) grid.FindName("selIndicator");

            // Set last added Items selection indicator
            SelectionIndicatorHI = selInd;

            if (HIAdded) SelectionIndicatorHI.Visibility = Visibility.Visible;

            // Reset AddedBoolean
            HIAdded = false;
        }

        private void PatientGridItem_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            var selInd = (Border) grid.FindName("selIndicator");

            // Set last added Items selection indicator
            SelectionIndicatorPatient = selInd;

            if (PatientAdded) SelectionIndicatorPatient.Visibility = Visibility.Visible;

            // Reset AddedBoolean
            PatientAdded = false;
        }

        private void OpenHITest(object sender, RoutedEventArgs e)
        {
            var test = new HealthinsuranceTestWindow();
            test.ShowDialog();
        }
    }
}

/// <summary>
///     Helperclass to achieve a reload of a list
/// </summary>
public class CurrentPatient : INotifyPropertyChanged
{
    private Patient patient = new Patient();

    public Patient Patient
    {
        get => patient;
        set
        {
            patient = value;
            OnPropertyChanged("Patient");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

/// <summary>
///     Helperclass to achieve a realod of a list
/// </summary>
public class CurrentHealthinsurance : INotifyPropertyChanged
{
    private Healthinsurance healthinsurance = new Healthinsurance();

    public Healthinsurance Healthinsurance
    {
        get => healthinsurance;
        set
        {
            healthinsurance = value;
            OnPropertyChanged("Healthinsurance");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

/// <summary>
///     Helperclass to achieve a realod of a list
/// </summary>
public class CurrentTreatment : INotifyPropertyChanged
{
    private Treatment treatment = new Treatment();

    public Treatment Treatment
    {
        get => treatment;
        set
        {
            treatment = value;
            OnPropertyChanged("Treatment");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
*/
       
    }
}