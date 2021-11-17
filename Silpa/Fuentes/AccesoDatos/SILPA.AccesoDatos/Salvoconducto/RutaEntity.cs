using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    [Serializable]
    public class RutaEntity
    {
        public int RutaID { get; set; }
        public int DepartamentoID { get; set; }
        public string Departamento { get; set; }
        public int MunicipioID { get; set; }
        public string Municipio { get; set; }
        public string Corregimiento { get; set; }
        public string Vereda { get; set; }
        public string Barrio { get; set; }
        public int TipoRutaID { get; set; }
        public int Orden { get; set; }
        public string TipoRuta { get; set; }
        public int IdOrigenDestino { get; set; }
        public bool Estado { get; set; }
    }
}
