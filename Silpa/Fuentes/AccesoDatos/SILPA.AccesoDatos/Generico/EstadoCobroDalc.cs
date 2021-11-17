using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class EstadoCobroDalc
    {
        private Configuracion objConfiguracion;

        public EstadoCobroDalc()
        {
            objConfiguracion = new Configuracion();
        }    
  
        /// <summary>
        /// Lista el estado del cobro basado en un ID de cobro
        /// </summary>
        /// <param name="idCobro">ID del Cobro</param>
        /// <returns></returns>
        public void ConsultarEstadoCobro(ref EstadoCobroIdentity objIdentity)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.IdEstadoCobro, objIdentity.Nombre };

            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_ESTADO_COBRO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            objIdentity.IdEstadoCobro = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ECO_ID"]);
            objIdentity.Nombre = dsResultado.Tables[0].Rows[0]["ECO_NOMBRE"].ToString();
            objIdentity.Descripcion = dsResultado.Tables[0].Rows[0]["ECO_DESCRIPCION"].ToString();
            objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["ECO_ACTIVO"]);
        }

    }

}
