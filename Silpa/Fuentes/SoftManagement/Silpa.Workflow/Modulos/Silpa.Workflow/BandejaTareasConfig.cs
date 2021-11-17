using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Silpa.Workflow
{
    public class BandejaTareasConfig
    {
        public const string EntryDataType = "EntryDataType";
        public const string EntryData = "EntryData";
        public const string IDEntryData = "IDEntryData";
        public const string ID = "IDEntryData";


        public static string ConexionSilpa
        {
            get
            {
                return "SILPAConnectionString";
            }
        }
        
    }
}
