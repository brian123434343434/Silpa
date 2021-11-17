using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Documento
{
    public class MisTramitesDocumentos
    {
        public MisTramitesDocumentos()
        {
           
        }
        // Identificador del objeto
        public Int32? IDMisTramitesDocumentos { get; set; }
        // Identificador del objeto
        public String Ruta { get; set; }
        // Identificador del objeto
        public String NumeroSilpa { get; set; }
        // Identificador del objeto
        public String Autoridad_Ambiental { get; set; }
        // Identificador del objeto
        public String Acto_Administrativo { get; set; }
        // Identificador del objeto
        public String NombreDocumento { get; set; }
    }
}
