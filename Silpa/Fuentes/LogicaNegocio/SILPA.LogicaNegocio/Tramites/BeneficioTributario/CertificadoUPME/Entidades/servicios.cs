using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class servicios : EntidadSerializable
    {
        public string id { get; set; }
        public string proyecto { get; set; }
        public string servicio { get; set; }
        public string proveedor { get; set; }
        public string alcance { get; set; }
        public decimal valor_total { get; set; }
        public decimal iva { get; set; }
    }
}
