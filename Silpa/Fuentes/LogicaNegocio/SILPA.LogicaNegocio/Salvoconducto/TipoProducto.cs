using SILPA.AccesoDatos.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class TipoProducto
    {
        private TipoProductoDalc vTipoProductoDalc;

        public TipoProducto()
        {
            vTipoProductoDalc = new TipoProductoDalc();
        }

        public List<TipoProductoIdentity> ListaTipoProducto(int claseProductoID, bool esSalvoconducto)
        {
            return vTipoProductoDalc.ListaTipoProducto(claseProductoID, esSalvoconducto);
        }
    }
}
