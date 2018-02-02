﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    static class Constants
    {
        public const string LoginPageUri = "LoginPage.xaml";
        public const string CreateAccountPageUri = "CreateAccountPage.xaml";
        public const string InitialSettingsPageUri = "InitialSettingsPage.xaml";

        public const string Storagetype_JSON = "JSON";
        public const string Storagetype_SQL = "SQL";
        public const string Storagetype_XML = "XML";

        private static Settings Settings;
        
        public static ref Settings GetSettings()
        {
            if (Settings == null) Settings = new Settings();
            return ref Settings;
        }
    }
}
