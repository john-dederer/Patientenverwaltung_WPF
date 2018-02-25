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

            HealthinsuranceViewModel.SharedViewModel().ShowAddHiUi = true;
            HealthinsuranceViewModel.SharedViewModel().ShowHiListUi = true;
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
            }
            else if (UiState == UIState.Settings)
            {
                // Nothing to do since this is opened in a new window
            }

            PatientViewModel.SharedViewModel().ShowAddPatientUi = true;
            PatientViewModel.SharedViewModel().ShowPatientListUi = true;
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
