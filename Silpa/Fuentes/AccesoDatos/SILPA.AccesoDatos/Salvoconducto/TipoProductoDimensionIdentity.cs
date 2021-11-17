using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class TipoProductoDimensionIdentity
    {
        public int TipoProductoID { get; set; }
        public decimal Alto { get; set; }
        public decimal Largo { get; set; }
        public decimal? Ancho { get; set; }
        public string DescripcionDimension { get { return Descripcion(); } }

        private string Descripcion()
        {
            return string.Format("{0}*{1}", Alto.ToString(), Largo.ToString()) + Ancho != null ? ("*" + Ancho.ToString()) : "";
        }
    }
}
