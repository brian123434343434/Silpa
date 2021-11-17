using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Salvoconducto;
using SILPA.AccesoDatos.Aprovechamiento;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class TipoAprovechamiento
    {

        private TipoAprovechamientoDalc TipoAprovechamientoDalc;

        public TipoAprovechamiento()
        {
            TipoAprovechamientoDalc = new TipoAprovechamientoDalc();
        }

        public List<TipoAprovechamientoIdentity> ListaTipoDocumento()
        {
            return TipoAprovechamientoDalc.ListaTipoDocumento();
        }

    }
}
