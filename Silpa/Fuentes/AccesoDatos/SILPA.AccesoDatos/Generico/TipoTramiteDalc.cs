using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Generico
{
    public class TipoTramiteDalc
    {

        /// <summary>
        /// Objeto de configuracion de la aplicaci�n
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuraci�n
        /// </summary>
        public TipoTramiteDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los tipos de de tramites en la BD.
        /// </summary>
        /// <param name="intTipoTramite" >Valor del identificador del tipo de tramite.
        /// si es null no existen restricciones.</param>
        /// <returns>DataSet con los registros y las siguientes columnas: ID_TRAMITE, NOMBRE_TRAMITE, ACTIVO_TRAMITE </returns>
        public DataSet ListarTipoTramite(Nullable<int> intTipoTramite, string nombreTramite)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intTipoTramite, nombreTramite, null};
                DbCommand cmd = db.GetStoredProcCommand("gen_lista_tipo_tramite", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public DataSet ListarTipoTramiteVisible()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());                
                DbCommand cmd = db.GetStoredProcCommand("gen_lista_tipo_tramite_visible");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public DataSet ListarTipoTramiteVisibleAutoridadAmbiental(int autoridadAmbId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = { autoridadAmbId };
                DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_TRAMITE_AUT_AMB", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataSet ListarUsuariosPorTramite(int tramiteId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = { tramiteId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTA_USUARIO_TRAMITE", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Obtiene el ID del tr�mite para el nombre entregado
        /// </summary>
        /// <param name="nombreTramite">Nombre del Tr�mite</param>
        /// <returns>ID del tipo de Tr�mite</returns>
        public int ObtenerIDTramiteXNombre(string nombreTramite)
        {
            
            DataSet ds = ListarTipoTramite(null, nombreTramite);
            if (ds.Tables[0].Rows.Count > 0)
                return Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
            else
                return -1;

        }

    }
}
