using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class TipoContactoCampoIdentity
    {
        public int TipoContactoId { get; set; }
        public int CampoId { get; set; }
        public string NombreCampo { get; set; }
        public string TipoCampo { get; set; }
        public string TipoControl { get; set; }
        public string EtiquetaCampo { get; set; }
        public string Titulo { get; set; }
        public bool EsObligatorio { get; set; }
        public bool Activo { get; set; }
    }
}
