namespace Patientenverwaltung_WPF
{
    internal static class Constants
    {
        public const string LoginPageUri = "LoginPage.xaml";
        public const string CreateAccountPageUri = "CreateAccountPage.xaml";
        public const string InitialSettingsPageUri = "InitialSettingsPage.xaml";
        public const string MainWindowUri = "MainWindow.xaml";
        public const string AskToCreateAccountPageUri = "AskToCreateAccountPage.xaml";
        public const string ResetPasswordPageUri = "ResetPasswordPage.xaml";        
    }

    public enum HealthinsuranceState
    {
        Private,
        ByLaw
    }

    public enum Gender
    {
        Male,
        Female,
        Distinguished
    }

    /// <summary>
    ///     Defines the current UIState
    /// </summary>
    public enum UIState
    {
        Patient,
        Healthinsurance,
        Settings
    }
}