using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    public abstract class Connector
    {
        /// <summary>
        /// Tries to create the dataModel in storage
        /// </summary>
        /// <param name="datamodel"></param>
        /// <returns>If creation failed or not</returns>
        public abstract bool Create(Datamodel datamodel);

        /// <summary>
        /// Tries to create the user in storage
        /// </summary>
        /// <param name="datamodel"></param>
        /// <returns>If creation failed or not</returns>
        public abstract bool Create(User user);

        /// <summary>
        /// Check if give datamodel is in store
        /// </summary>
        /// <param name="datamodel"></param>
        /// <returns></returns>
        public abstract bool Select(Datamodel datamodel);

        /// <summary>
        /// If more than one dataset is found for given dataModel then the ref is null, otherwise fill the ref dataModel
        /// </summary>
        /// <param name="datamodelOut"></param>
        /// <returns>If given dataModel was found in storage</returns>
        public abstract bool Select(Datamodel datamodelIn, out Datamodel datamodelOut);

        /// <summary>
        /// Tries to update a given dataModel in storage
        /// </summary>
        /// <param name="datamodel"></param>
        /// <returns>True if update worked</returns>
        public abstract bool Update(Datamodel datamodel);

        /// <summary>
        /// Tries to delete the given dataModel from storage
        /// </summary>
        /// <param name="datamodel"></param>
        /// <returns>True if deletion was successfull</returns>
        public abstract bool Delete(Datamodel datamodel);

        /// <summary>
        /// Select returns a user instance
        /// </summary>
        /// <param name="user"></param>
        /// <param name="returned"></param>
        /// <returns></returns>
        internal abstract bool Select(User user, out User returned);
        internal abstract List<Healthinsurance> GetHealthinsuranceList();
        internal abstract List<Treatment> GetTreatmentList();

        /// <summary>
        /// Select returns a patient instance
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="returned"></param>
        /// <returns></returns>
        internal abstract bool Select(Patient patient, out Patient returned);

        /// <summary>
        /// Select returns a patient instance
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="returned"></param>
        /// <returns></returns>
        internal abstract bool Select(Treatment treatment, out Treatment returned);

        internal abstract List<Patient> GetPatientList();
    }

    public class Connector_JSON : Connector
    {
        private const string UserPath = "User.json";
        private const string PatientPath = "Patient.json";
        private const string TreatmentPath = "Treatment.json";
        private const string HealthinsurancePath = "Healthinsurance.json";

        public Connector_JSON()
        {
            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"))
            {
                using (StreamWriter writer = File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"))
                {
                    
                }
            }

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}"))
            {
                using (StreamWriter writer = File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}"))
                {
                    
                }
            }

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"))
            {
                using (StreamWriter writer =
                    File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"))
                {
                    
                }
            }

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}"))
            {
                using (StreamWriter writer =
                    File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}"))
                {
                    
                }
            }
        }

        public override bool Create(Datamodel datamodel)
        {
            if (Select(datamodel)) return false;

            if (datamodel.GetType() == typeof(Patient))
            {
                var list = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}"));
                if (list == null) list = new List<Patient>();

                var patient = datamodel as Patient;
                // Set id
                patient.PatientId = CurrentContext.GetIdCounter().GetId("patient");

                // Set log data
                patient.SetLogData();

                // Add to list
                list.Add(patient);

                var json = JsonConvert.SerializeObject(list, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}", json);

                return true;
            }
            else if (datamodel.GetType() == typeof(Healthinsurance))
            {
                var list = JsonConvert.DeserializeObject<List<Healthinsurance>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}"));
                if (list == null) list = new List<Healthinsurance>();

                var healthinsurance = datamodel as Healthinsurance;
                // Set id
                healthinsurance.HealthinsuranceId = CurrentContext.GetIdCounter().GetId("healthinsurance");

                // Set log data
                healthinsurance.SetLogData();

                // Add to list
                list.Add(healthinsurance);

                var json = JsonConvert.SerializeObject(list, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}", json);

                return true;
            }
            else if (datamodel.GetType() == typeof(Treatment))
            {
                var list = JsonConvert.DeserializeObject<List<Treatment>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"));
                if (list == null) list = new List<Treatment>();

                var treatment = datamodel as Treatment;
                // Set id
                treatment.TreatmentId = CurrentContext.GetIdCounter().GetId("treatment");

                // Set log data
                treatment.SetLogData();

                // Add to list
                list.Add(treatment);

                var json = JsonConvert.SerializeObject(list, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}", json);

                return true;
            }
            else if (datamodel.GetType() == typeof(User))
            {
                var list = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"));
                if (list == null) list = new List<User>();

                var user = datamodel as User;
                // Set id
                user.UserId = CurrentContext.GetIdCounter().GetId("user");

                // Set log data
                user.SetLogData();

                // Add to list
                list.Add(user);

                var json = JsonConvert.SerializeObject(list, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}", json);

                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Create(User user)
        {
            // safety measurement: check if  user exists
            if (Select(user, out User temp)) return false;

            var list = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"));
            if (list == null) list = new List<User>();

            // Set id
            user.UserId = CurrentContext.GetIdCounter().GetId("user");

            // Set log data
            user.SetLogData();

            // Add to list
            list.Add(user);

            var json = JsonConvert.SerializeObject(list, Formatting.Indented);

            File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}", json);

            return true;
        }

        public override bool Delete(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Select(Datamodel datamodelIn, out Datamodel datamodelOut)
        {
            throw new NotImplementedException();
        }

        public string GetDatamodelPath(Datamodel datamodel)
        {
            if (datamodel.GetType() == typeof(Patient))
            {
                return PatientPath;
            }
            else if (datamodel.GetType() == typeof(Healthinsurance))
            {
                return HealthinsurancePath;
            }
            else if (datamodel.GetType() == typeof(Treatment))
            {
                return TreatmentPath;
            }
            else if (datamodel.GetType() == typeof(User))
            {
                return UserPath;
            }
            else
            {
                return string.Empty;
            }
        }

        public bool CheckList(Datamodel datamodel)
        {
            if (datamodel.GetType() == typeof(Patient))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}"));

                if (deserializedList == null) return false;

                foreach (var modelInLIst in deserializedList)
                {
                    if (modelInLIst.GetHashCode() == datamodel.GetHashCode())
                    {
                        return true;
                    }
                }

                return false;
            }
            else if (datamodel.GetType() == typeof(Healthinsurance))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<Healthinsurance>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}"));

                if (deserializedList == null) return false;

                foreach (var modelInLIst in deserializedList)
                {
                    if (modelInLIst.GetHashCode() == datamodel.GetHashCode())
                    {
                        return true;
                    }
                }

                return false;
            }
            else if (datamodel.GetType() == typeof(Treatment))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<Treatment>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"));

                if (deserializedList == null) return false;

                foreach (var modelInLIst in deserializedList)
                {
                    if (modelInLIst.GetHashCode() == datamodel.GetHashCode())
                    {
                        return true;
                    }
                }

                return false;
            }
            else if (datamodel.GetType() == typeof(User))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"));

                if (deserializedList == null) return false;

                foreach (var modelInLIst in deserializedList)
                {
                    if (modelInLIst.GetHashCode() == datamodel.GetHashCode())
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public override bool Select(Datamodel datamodel)
        {
            var path = GetDatamodelPath(datamodel);
            var model = datamodel.GetType().Name;

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{path}"))
            {
                using (StreamWriter writer = File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{path}"))
                {

                }

                return false;
            }

            return CheckList(datamodel);
        }

        public override bool Update(Datamodel datamodel)
        {                        
            var path = GetDatamodelPath(datamodel);

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{path}"))
            {
                using (StreamWriter writer = File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{path}"))
                {

                }

                return false;
            }

            return UpdateList(datamodel);
        }

        private bool UpdateList(Datamodel datamodel)
        {
            if (datamodel.GetType() == typeof(Patient))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}"));

                if (deserializedList == null) return false;

                var patient = datamodel as Patient;

                var index = deserializedList.FindIndex(x => x.PatientId == patient.PatientId);

                if (index == -1) return false;

                deserializedList[index] = patient;
                deserializedList[index].SetLogData();               

                var json = JsonConvert.SerializeObject(deserializedList, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}", json);

                return true;
            }
            else if (datamodel.GetType() == typeof(Healthinsurance))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<Healthinsurance>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}"));

                if (deserializedList == null) return false;

                var healthinsurance = datamodel as Healthinsurance;

                var index = deserializedList.FindIndex(x => x.HealthinsuranceId == healthinsurance.HealthinsuranceId);

                if (index == -1) return false;

                deserializedList[index] = healthinsurance;
                deserializedList[index].SetLogData();

                var json = JsonConvert.SerializeObject(deserializedList, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}", json);

                return true;
            }
            else if (datamodel.GetType() == typeof(Treatment))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<Treatment>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"));

                if (deserializedList == null) return false;

                var treatment = datamodel as Treatment;

                var index = deserializedList.FindIndex(x => x.TreatmentId == treatment.TreatmentId);

                if (index == -1) return false;

                deserializedList[index] = treatment;
                deserializedList[index].SetLogData();

                var json = JsonConvert.SerializeObject(deserializedList, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}", json);

                return true;
            }
            else if (datamodel.GetType() == typeof(User))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"));

                if (deserializedList == null) return false;

                var user = datamodel as User;

                var index = deserializedList.FindIndex(x => x.Username == user.Username);

                if (index == -1) return false;

                deserializedList[index].Passwordhash = user.Passwordhash;
                deserializedList[index].SetLogData();

                var json = JsonConvert.SerializeObject(deserializedList, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}", json);

                return true;
            }
            else
            {
                return false;
            }
        }

        internal override List<Patient> GetPatientList()
        {
            var deserialzedList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}"));

            if (deserialzedList == null) deserialzedList = new List<Patient>();

            return deserialzedList;
        }

        internal override bool Select(User user, out User returned)
        {
            returned = null;

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"))
            {
                using (StreamWriter writer = File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"))
                {
                    
                }

                return false;
            }

            // Deserialize JSON
            var deserializedList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"));

            if (deserializedList == null) return false;

            foreach (var userInList in deserializedList)
            {
                if (userInList.Username == user.Username)
                {
                    returned = userInList;
                    return true;
                }
            }

            return false;
        }

        internal override bool Select(Patient patient, out Patient returned)
        {
            returned = null;

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"))
            {
                using (StreamWriter writer = File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"))
                {

                }

                return false;
            }

            // Deserialize JSON
            var deserializedList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}"));

            if (deserializedList == null) return false;

            foreach (var patientInList in deserializedList)
            {
                if (patientInList.GetHashCode() != patient.GetHashCode()) continue;
                returned = patient;
                return true;
            }

            return false;
        }

        internal override bool Select(Treatment treatment, out Treatment returned)
        {
            returned = null;

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"))
            {
                using (StreamWriter writer = File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"))
                {

                }

                return false;
            }

            // Deserialize JSON
            var deserializedList = JsonConvert.DeserializeObject<List<Treatment>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"));

            if (deserializedList == null) return false;

            foreach (var treatmentInList in deserializedList)
            {
                if (treatmentInList.GetHashCode() != treatment.GetHashCode()) continue;
                returned = treatment;
                return true;
            }

            return false;
        }

        internal override List<Healthinsurance> GetHealthinsuranceList()
        {
            var deserialzedList = JsonConvert.DeserializeObject<List<Healthinsurance>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}"));

            if (deserialzedList == null) deserialzedList = new List<Healthinsurance>();

            return deserialzedList;
        }

        internal override List<Treatment> GetTreatmentList()
        {
            var deserialzedList = JsonConvert.DeserializeObject<List<Treatment>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"));

            if (deserialzedList == null) deserialzedList = new List<Treatment>();

            return deserialzedList;
        }
    }

    public class Connector_SQL : Connector
    {
        public override bool Create(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Create(User user)
        {
            throw new NotImplementedException();
        }

        public override bool Select(Datamodel datamodelIn, out Datamodel datamodelOut)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        internal override bool Select(User user, out User returned)
        {
            throw new NotImplementedException();
        }

        internal override bool Select(Patient patient, out Patient returned)
        {
            throw new NotImplementedException();
        }

        internal override List<Patient> GetPatientList()
        {
            throw new NotImplementedException();
        }

        internal override bool Select(Treatment treatment, out Treatment returned)
        {
            throw new NotImplementedException();
        }

        public override bool Select(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        internal override List<Healthinsurance> GetHealthinsuranceList()
        {
            throw new NotImplementedException();
        }

        internal override List<Treatment> GetTreatmentList()
        {
            throw new NotImplementedException();
        }
    }

    public class Connector_XML : Connector
    {
        public override bool Create(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Create(User user)
        {
            throw new NotImplementedException();
        }

        public override bool Select(Datamodel datamodelIn, out Datamodel datamodelOut)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        internal override bool Select(User user, out User returned)
        {
            throw new NotImplementedException();
        }

        internal override bool Select(Patient patient, out Patient returned)
        {
            throw new NotImplementedException();
        }

        internal override List<Patient> GetPatientList()
        {
            throw new NotImplementedException();
        }

        internal override bool Select(Treatment treatment, out Treatment returned)
        {
            throw new NotImplementedException();
        }

        public override bool Select(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        internal override List<Healthinsurance> GetHealthinsuranceList()
        {
            throw new NotImplementedException();
        }

        internal override List<Treatment> GetTreatmentList()
        {
            throw new NotImplementedException();
        }
    }
}
