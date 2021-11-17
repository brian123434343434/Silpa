using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class ubicaciones
    {
        public string zona { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
    }
}
