using System;
using System.IO;
using Newtonsoft.Json;

namespace Patientenverwaltung_WPF
{
    public class IdCounter
    {
        public long HealthinsuranceId;
        public long InsuranceId;
        public long PatientId;
        public long TreatmentId;
        public long UserId;

        public long GetId(string id)
        {
            UpdateId();
            id.ToLower();

            long toReturn = 0;

            switch (id)
            {
                case "user":
                    toReturn = UserId++;
                    break;
                case "patient":
                    toReturn = PatientId++;
                    break;
                case "treatment":
                    toReturn = TreatmentId++;
                    break;
                case "healthinsurance":
                    toReturn = HealthinsuranceId++;
                    break;
                case "insurance":
                    toReturn = InsuranceId++;
                    break;
            }

            UpdateJson();
            return toReturn;
        }

        private void UpdateJson()
        {
            try
            {
                var json = JsonConvert.SerializeObject(this, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}IdCounter.json", json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                UpdateJson();
            }
        }

        private void UpdateId()
        {
            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}IdCounter.json"))
            {
                using (File.CreateText($@"{CurrentContext.GetSettings().Savelocation}IdCounter.json"))
                {
                }

                InitCounter();
                return;
            }

            var counter =
                JsonConvert.DeserializeObject<IdCounter>(
                    File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}IdCounter.json"));

            if (counter == null)
            {
                InitCounter();
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

        private void InitCounter()
        {
            // Set Counter to 1
            UserId = 1;
            PatientId = 1;
            TreatmentId = 1;
            HealthinsuranceId = 1;
            InsuranceId = 1;
        }
    }
}