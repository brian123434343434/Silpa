using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data.SqlClient;
using SILPA.AccesoDatos.DAA;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Generico
{
   public class EstadoCobro
    {
        /// <summary>
        ///  Consulta la informacion de estado de cobro
        /// </summary>
        /// <param name="numReferencia">numero de referencia</param>
       public void ConsultarEstadoCobro(ref EstadoCobroIdentity objEstadoCobro)
        {
            EstadoCobroDalc obj = new EstadoCobroDalc();
            obj.ConsultarEstadoCobro(ref objEstadoCobro);
        }
    }
}
