using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF.Notification
{
    public static class UiHelper
    {
        public static UIState UiState { get; set; }

        public static void SwitchUiToHealthinsurance()
        {
            if (UiState == UIState.Patient)
            {
                // Hide Create mask, treatment list, patient ist, and add control
                PatientViewModel.SharedViewModel().ShowCreateMaskUi = false;
                TreatmentViewModel.SharedViewModel().ShowTreatmentListUi = false;
                PatientViewModel.SharedViewModel().ShowPatientListUi = false;
                PatientViewModel.SharedViewModel().ShowAddPatientUi = false;
                TreatmentViewModel.SharedViewModel().ShowSearchTreatmentUi = false;
            }
            else if (UiState == UIState.Healthinsurance)
            {
                // Do nothing
            }
            else if (UiState == UIState.Settings)
            {
                // Nothing to do since this is opened in a new window
            }

            // Wenn ein HI schon ausgewählt wurde, diese in liste als selektiert anzeigen und in maske auswählen
            if (PatientViewModel.SharedViewModel().NewPatient.HealthinsuranceId >= 0)
            {
                var index = HealthinsuranceViewModel.SharedViewModel().Healthinsurances.ToList().FindIndex(x =>
                    x.HealthinsuranceId == PatientViewModel.SharedViewModel().NewPatient.HealthinsuranceId);

                if (index >= 0)
                {
                    var hi = HealthinsuranceViewModel.SharedViewModel().Healthinsurances.ElementAt(index);

                    HealthinsuranceViewModel.SharedViewModel().Healthinsurances.Remove(hi);
                    HealthinsuranceViewModel.SharedViewModel().Healthinsurances.Insert(index, hi);


                    HealthinsuranceViewModel.SharedViewModel().ShowCreateHiMask = true;
                    HealthinsuranceViewModel.SharedViewModel().NewHealthinsurance = HealthinsuranceViewModel.SharedViewModel().Healthinsurances.ElementAt(index);
                }                
            }
            
            HealthinsuranceViewModel.SharedViewModel().ShowAddHiUi = true;
            HealthinsuranceViewModel.SharedViewModel().ShowHiListUi = true;

            UiState = UIState.Healthinsurance;
        }

        public static void SwitchUiToPatient()
        {
            if (UiState == UIState.Patient)
            {
                // Do nothing
            }
            else if (UiState == UIState.Healthinsurance)
            {
                // Hide create mask, add control and healthinsurance list
                HealthinsuranceViewModel.SharedViewModel().ShowCreateHiMask = false;
                HealthinsuranceViewModel.SharedViewModel().ShowAddHiUi = false;
                HealthinsuranceViewModel.SharedViewModel().ShowHiListUi = false;

                HealthinsuranceViewModel.SharedViewModel().ChoosingHiForPatient = false;
            }
            else if (UiState == UIState.Settings)
            {
                // Nothing to do since this is opened in a new window
            }

            PatientViewModel.SharedViewModel().ShowAddPatientUi = true;
            PatientViewModel.SharedViewModel().ShowPatientListUi = true;

            UiState = UIState.Patient;
        }

        public static void SwitchUiToSettings()
        {
            SettingsWindow window = new SettingsWindow();

            if (window.ShowDialog() == true)
            { 
                var infomsg = new InfoMessageWindow("Einstellungen erfolgreich geändert");
                infomsg.ShowDialog();
            }
        }
    }
}
