using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class UnidadMedidaIdentity
    {
        public int UnidadMedidaId { get; set; }
        public string UnidadMedidad { get; set; }
        public int TipoProductoID { get; set; }
        public double Factor { get; set; }
    }
}
