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

        public static Connector Get(Savetype savetype)
        {
            Initialize();

            switch (savetype)
            {
                case Savetype.JSON:
                    return Connector_JSON;
                case Savetype.SQL:
                    return Connector_SQL;
                case Savetype.XML:
                    return Connector_XML;
                default:
                    throw new Exception($@"storagetype {savetype} ist nicht bekannt.");
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
