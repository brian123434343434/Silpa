using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    [Serializable]
    public class bien : EntidadSerializable
    {
        public string id { get; set; }
        public string elemento { get; set; }
        public string subpartida_arancelaria { get; set; }
        public int cantidad { get; set; }
        public string unidad_medida { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string fabricante { get; set; }
        public string proveedor { get; set; }
        public string funcion { get; set; }
        public decimal valor_total { get; set; }
        public decimal iva { get; set; }

    }
}
