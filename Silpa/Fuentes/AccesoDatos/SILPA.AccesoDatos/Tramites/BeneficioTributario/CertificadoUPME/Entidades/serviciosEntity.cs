using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    public class serviciosEntity
    {
        public int certificadoID { get; set; }
        public string servicio { get; set; }
        public string proveedor { get; set; }
        public string alcance { get; set; }
        public decimal valor_total { get; set; }
        public decimal iva { get; set; }

        public override string ToString()
        {
            return string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"", this.certificadoID, this.servicio, this.proveedor, this.alcance, this.valor_total, this.iva);
        }
    }
}
