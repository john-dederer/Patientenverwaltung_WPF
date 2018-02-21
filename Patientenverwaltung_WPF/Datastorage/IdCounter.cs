using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    public class IdCounter
    {
        public long UserId;
        public long PatientId;
        public long TreatmentId;
        public long HealthinsuranceId;
        public long InsuranceId;

        public long GetId(string id)
        {
            UpdateId();
            id.ToLower();

            long toReturn = 0;

            switch(id)
            {
                case "user":
                    toReturn = UserId++; break;
                case "patient":
                    toReturn = PatientId++; break;
                case "treatment":
                    toReturn = TreatmentId++; break;
                case "healthinsurance":
                    toReturn = HealthinsuranceId++; break;
                case "insurance":
                    toReturn = InsuranceId++; break;

            }

            UpdateJSON();
            return toReturn;
        }

        private void UpdateJSON()
        {
            try
            {
                var json = JsonConvert.SerializeObject(this, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}IdCounter.json", json);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                UpdateJSON();
            }
        }

        private void UpdateId()
        {
            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}IdCounter.json"))
            {
                using (var writer =
                    File.CreateText($@"{CurrentContext.GetSettings().Savelocation}IdCounter.json"))
                {
                    
                }

                    SetAllZero();
                return;
            }

            var counter = JsonConvert.DeserializeObject<IdCounter>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}IdCounter.json"));

            if (counter == null)
            {
                SetAllZero();
                return;
            }

            SetValues(counter);
        }

        private void SetValues(IdCounter counter)
        {
            UserId = counter.UserId;
            PatientId = counter.PatientId;
            TreatmentId = counter.TreatmentId;
            HealthinsuranceId = counter.HealthinsuranceId;
            InsuranceId = counter.InsuranceId;
        }

        private void SetAllZero()
        {
            // Set Counter to 0
            UserId = 0;
            PatientId = 0;
            TreatmentId = 0;
            HealthinsuranceId = 0;
            InsuranceId = 0;
        }
    }
}
