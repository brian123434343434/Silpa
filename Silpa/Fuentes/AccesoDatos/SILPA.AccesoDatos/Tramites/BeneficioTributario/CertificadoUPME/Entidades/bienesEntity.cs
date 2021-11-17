using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades
{
    public class bienesEntity
    {
        public int certificadoID { get; set; }
        public string elemento { get; set; }
        public string subpartida_arancelaria { get; set; }
        public string cantidad { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string fabricante { get; set; }
        public string proveedor { get; set; }
        public string funcion { get; set; }
        public decimal valor_total { get; set; }
        public decimal iva { get; set; }
        public override string ToString()
        {
            return string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\"", this.certificadoID, this.elemento, this.subpartida_arancelaria, this.cantidad, this.marca, this.modelo, this.fabricante, this.proveedor, this.funcion, this.valor_total, this.iva);
        }
    }
}
