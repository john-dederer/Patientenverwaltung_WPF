using System;
using System.Collections.Generic;
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
        /// If more than one dataset is found for given dataModel then the ref is null, otherwise fill the ref dataModel
        /// </summary>
        /// <param name="datamodel"></param>
        /// <returns>If given dataModel was found in storage</returns>
        public abstract bool Select(ref Datamodel datamodel);

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
    }

    public class Connector_JSON : Connector
    {
        public override bool Create(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Select(ref Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }
    }

    public class Connector_SQL : Connector
    {
        public override bool Create(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Select(ref Datamodel datamodel)
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
    }

    public class Connector_XML : Connector
    {
        public override bool Create(Datamodel datamodel)
        {
            throw new NotImplementedException();
        }

        public override bool Select(ref Datamodel datamodel)
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
    }
}
