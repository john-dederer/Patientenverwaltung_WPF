using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    public static class CurrentContext
    {
        private static Settings Settings;
        private static User User;

        public static ref Settings GetSettings()
        {
            if (Settings == null) Settings = new Settings();
            return ref Settings;
        }

        public static ref User GetUser()
        {
            if (User == null) User = new User();
            return ref User;
        }
    }
}
