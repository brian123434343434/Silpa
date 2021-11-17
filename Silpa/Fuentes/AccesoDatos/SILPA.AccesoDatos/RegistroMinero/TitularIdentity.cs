using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.RegistroMinero
{
    [Serializable]
    public class TitularIdentity : EntidadSerializable
    {
        public int TitularID { get; set; }
        public int RegistroMineriaID { get; set; }
        public string NombreTitular { get; set; }
        public string Nroidentificacion { get; set; }
    }
}
