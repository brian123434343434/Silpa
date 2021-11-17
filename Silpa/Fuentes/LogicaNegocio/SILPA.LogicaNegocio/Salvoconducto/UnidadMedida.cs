using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class UnidadMedida
    {
        private UnidadMedidaDalc vUnidadMedidaDalc;

        public UnidadMedida()
        {
            vUnidadMedidaDalc = new UnidadMedidaDalc();
        }

        public List<UnidadMedidaIdentity> ListaUnidadMedidaTipoProducto(int tipoProductoID)
        {
            return vUnidadMedidaDalc.ListaUnidadMedidaTipoProducto(tipoProductoID);
        }
    }
}
