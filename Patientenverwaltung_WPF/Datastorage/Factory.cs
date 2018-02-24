using System;

namespace Patientenverwaltung_WPF
{
    internal static class Factory
    {
        private static ConnectorJson ConnectorJson { get; set; }
        private static ConnectorSql ConnectorSql { get; set; }
        private static ConnectorXml ConnectorXml { get; set; }

        public static Connector Get(Savetype savetype)
        {
            Initialize();

            switch (savetype)
            {
                case Savetype.JSON:
                    return ConnectorJson;
                case Savetype.SQL:
                    return ConnectorSql;
                case Savetype.XML:
                    return ConnectorXml;
                default:
                    throw new Exception($@"storagetype {savetype} ist nicht bekannt.");
            }
        }

        private static void Initialize()
        {
            if (ConnectorJson == null) ConnectorJson = new ConnectorJson();
            if (ConnectorSql == null) ConnectorSql = new ConnectorSql();
            if (ConnectorXml == null) ConnectorXml = new ConnectorXml();
        }
    }
}