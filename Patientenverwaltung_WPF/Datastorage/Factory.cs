using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    static class Factory
    {
        private static Connector_JSON Connector_JSON { get; set; }
        private static Connector_SQL Connector_SQL { get; set; }
        private static Connector_XML Connector_XML { get; set; }

        public static Connector Get(string storageType)
        {
            Initialize();

            switch (storageType)
            {
                case Constants.Storagetype_JSON:
                    return Connector_JSON;
                case Constants.Storagetype_SQL:
                    return Connector_SQL;
                case Constants.Storagetype_XML:
                    return Connector_XML;
                default:
                    throw new Exception($@"storagetype {storageType} ist nicht bekannt.");
            }
        }

        private static void Initialize()
        {
            if (Connector_JSON == null) Connector_JSON = new Connector_JSON();
            if (Connector_SQL  == null) Connector_SQL  = new Connector_SQL();
            if (Connector_XML  == null) Connector_XML  = new Connector_XML();
        }
    }
}
