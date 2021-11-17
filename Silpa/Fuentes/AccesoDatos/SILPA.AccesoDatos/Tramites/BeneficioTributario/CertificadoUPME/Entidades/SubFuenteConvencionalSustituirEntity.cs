using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    public class SubFuenteConvencionalSustituirEntity
    {
        public int subFuenteConvencionalSustituirID { get; set; }
        public int fuenteConvencionalSustiruirID { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
    }
}
