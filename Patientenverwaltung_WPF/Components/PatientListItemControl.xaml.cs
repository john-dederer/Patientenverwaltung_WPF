using System;
using System.Windows;
using System.Windows.Controls;

namespace Patientenverwaltung_WPF
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Interaktionslogik für PatientListItemControl.xaml
    /// </summary>
    public partial class PatientListItemControl
    {
        public PatientListItemControl()
        {
            InitializeComponent();           
        }

        /// <summary>
        /// Sets the selected item as current patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientItemSelected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PatientViewModel.SharedViewModel().NewPatient = DataContext as Patient;

            // Update the Ui
            PatientViewModel.SharedViewModel().ShowCreateMaskUi = true;
            PatientViewModel.SharedViewModel().IsPatientBeingCreated = false;

            // Show only treatments for patient
            TreatmentViewModel.SharedViewModel().FilterTreatments();
            TreatmentViewModel.SharedViewModel().ShowTreatmentListUi = true;
            TreatmentViewModel.SharedViewModel().ShowSearchTreatmentUi = true;



        }

        private void GridItem_OnLoaded(object sender, RoutedEventArgs e)
        {
            var patient = DataContext as Patient;

            var ageSpan = DateTime.Now.Subtract(patient.Birthday);
            var age = new DateTime(ageSpan.Ticks).Year - 1;

            if (age <= 3)
            {
                // baby
                BabyIcon.Visibility = Visibility.Visible;
            }
            else if (age <= 15)
            {
                // child
                ChildIcon.Visibility = Visibility.Visible;
            }
            else if (age <= 21)
            {
                // teenager
                TeenagerIcon.Visibility = Visibility.Visible;
            }
            else if (age <= 55)
            {
                // adult
                AdultIcon.Visibility = Visibility.Visible;
            }
            else
            {
                // elderly
                ElderlyIcon.Visibility = Visibility.Visible;
            }
        }
    }
}