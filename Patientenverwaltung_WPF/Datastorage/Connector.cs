﻿using Newtonsoft.Json;
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
                File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}");
            }

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}"))
            {
                File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{PatientPath}");
            }

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}"))
            {
                File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{TreatmentPath}");
            }

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}"))
            {
                File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{HealthinsurancePath}");
            }
        }

        public override bool Create(Datamodel datamodel)
        {
            throw new NotImplementedException();
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

        public override bool Update(Datamodel datamodel)
        {
            if (datamodel.GetType() == typeof(User))
            {
                var userToUpdate = (User)datamodel;

                var list = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"));

                foreach (var user in list)
                {
                    if (user.Username == userToUpdate.Username)
                    {
                        var index = list.IndexOf(user);

                        if (index != -1)
                        {
                            list[index].Passwordhash = userToUpdate.Passwordhash;
                            list[index].SetLogData();
                        }
                    }
                }

                var json = JsonConvert.SerializeObject(list, Formatting.Indented);

                File.WriteAllText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}", json);

                return true;
            }

            return false;
        }

        internal override bool Select(User user, out User returned)
        {
            returned = null;

            if (!File.Exists($@"{CurrentContext.GetSettings().Savelocation}{UserPath}"))
            {
                File.CreateText($@"{CurrentContext.GetSettings().Savelocation}{UserPath}");

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
    }
}
