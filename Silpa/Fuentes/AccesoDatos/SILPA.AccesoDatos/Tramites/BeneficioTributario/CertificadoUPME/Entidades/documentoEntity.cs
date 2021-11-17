using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    public class documentoEntity
    {
        public int certificadoID { get; set; }
        public int documentoID { get; set; }
        public string nombre { get; set; }
        public string ruta { get; set; }
        public string rutaUPME { get; set; }
    }
}
