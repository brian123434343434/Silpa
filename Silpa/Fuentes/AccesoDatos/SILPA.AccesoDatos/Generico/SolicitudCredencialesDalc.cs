using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Generico
{
    public class SolicitudCredencialesDalc
    {
        private Configuracion objConfiguracion;
        public SolicitudCredencialesDalc() { objConfiguracion = new Configuracion(); }

        public SolicitudCredencialesDalc(ref SolicitudCredencialesEntity objEntity)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objEntity.PersonaID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SOLICITUD_CREDENCIAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            objEntity.PersonaID = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_PER_ID"]);
            objEntity.Fecha = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["SOL_FECHA"]);
            //23-jun-2010 - aegb
            objEntity.EnProceso = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SOL_EN_PROCESO"]);
        }

        /// <summary>
        /// Inserta un registro en la tabla Solicitud credencial
        /// </summary>
        public void InsertarSolicitudPersona(ref SolicitudCredencialesEntity objEntity)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { objEntity.SolicitudID,
                                                 objEntity.PersonaID,
                                                 objEntity.EnProceso,
                            
                };

            DbCommand cmd = db.GetStoredProcCommand("BAS_CREAR_SOLICITUD_CREDENCIAL", parametros);
            db.ExecuteDataSet(cmd);
            objEntity.SolicitudID = int.Parse(cmd.Parameters["@SOL_ID"].Value.ToString());

        }
    }
}
