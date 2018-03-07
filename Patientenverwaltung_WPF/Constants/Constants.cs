using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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

    public static class Logfile
    {
        private static bool _initialized = false;
        public static string AppFolder;
        private static string _logFile;

        public static void Initialize()
        {
            if (_initialized) return;

            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AppFolder = Path.Combine(appdata, "Patientenverwaltung");

            Directory.CreateDirectory(AppFolder);

            _logFile = Path.Combine(AppFolder, "Logfile.log");

            _initialized = true;
        }

        private static async Task WriteAsync(string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (FileStream sourceStream = new FileStream(_logFile,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }

        public static Task WriteToLog(string logstring)
        {
            Initialize();
            var toLogFile = $@"{DateTime.Now:G} : {logstring}";
            return WriteAsync(toLogFile);
        }
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