using System;
using System.CodeDom;
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
            try
            {
                if (!loadFromFile) return;

                var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText($@"{Logfile.AppFolder}\Settings.json"));

                if (settings == null) throw new Exception("Settings Datei wurde noch nicht richtig initialisiert");
                Savetype = settings.Savetype;
                Savelocation = settings.Savelocation;
            }
            catch (Exception e)
            {
                Logfile.WriteToLog(e.Message).Wait();
            }            
        }

        public bool SettingsJSONExist()
        {
            try
            {
                return File.Exists($@"{Logfile.AppFolder}\Settings.json");
            }
            catch (Exception e)
            {
                Logfile.WriteToLog(e.Message).Wait();
                return false;
            }           
        }

        public void CreateSettingsJSON()
        {
            if (SettingsJSONExist()) return;

            try
            {
                using (File.CreateText($@"{Logfile.AppFolder}\Settings.json")) { }
            }
            catch (Exception e)
            {
                Logfile.WriteToLog(e.Message).Wait();
            }            
        }

        public void UpdateJSON()
        {            
            try
            {
                var json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText($@"{Logfile.AppFolder}\Settings.json", json);
            }
            catch (Exception e)
            {
                Logfile.WriteToLog(e.Message).Wait();
            }
        }
    }

    public enum Savetype
    {
        JSON,
        SQL,
        XML
    }
}
