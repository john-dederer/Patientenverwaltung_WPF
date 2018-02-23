using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace Patientenverwaltung_WPF
{
    public class Settings
    {
        public Savetype Savetype { get; set; }
        public string Savelocation { get; set; }

        public void SetSettings(Settings settings)
        {
            if (settings == null) return;
            Savetype = settings.Savetype;
            Savelocation = settings.Savelocation;
        }

        public void SetSettings(bool loadFromFile)
        {
            if (!loadFromFile) return;

            var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Settings.json"));

            if (settings == null) throw new Exception("Settings Datei wurde noch nicht richtig initialisiert");
            Savetype = settings.Savetype;
            Savelocation = settings.Savelocation;
        }

        public bool SettingsJSONExist()
        {
            return File.Exists($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Settings.json");
        }

        public void CreateSettingsJSON()
        {
            if (SettingsJSONExist()) return;

            File.CreateText($@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Settings.json");
        }

        public void UpdateJSON()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText($@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Settings.json", json);
        }
    }

    public enum Savetype
    {
        JSON,
        SQL,
        XML
    }
}
