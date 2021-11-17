using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.RegistroMinero
{
    [Serializable]
    public class UbicacionIdentity : EntidadSerializable
    {
        public int DepartamentoID { get; set; }
        public string NombreDepartamento { get; set; }
        public int MunicipioID { get; set; }
        public string NombreMunicipio { get; set; }
    }
}
