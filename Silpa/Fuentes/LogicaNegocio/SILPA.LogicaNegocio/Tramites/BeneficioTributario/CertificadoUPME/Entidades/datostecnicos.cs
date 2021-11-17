using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class datostecnicos
    {
        public decimal energia_media { get; set; }
        public decimal vida_util { get; set; }
        public decimal rendimiento { get; set; }
    }
}
