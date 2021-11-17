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
    public class TipoEstadoProcesoDalc
    {

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoEstadoProcesoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de los tipos de ubicación
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipos de ubicación a cargar, en la propiedad ID del objetoIdentity</param>
        public TipoEstadoProcesoDalc(ref TipoEstadoProcesoIdentity objIdentity)
        {
            try
            {
                objConfiguracion = new Configuracion();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_TIPO_ESTADO_PROCESO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TES_ID_ESTADO_PROCESO"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["TES_ESTADO_PROCESO"]);
                objIdentity.Descripcion = Convert.ToString(dsResultado.Tables[0].Rows[0]["TES_DESCRIPCION"]);
                objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["TES_ACTIVO"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Modifica en la tabla Solicitud
        /// </summary>
        /// <param name="objIdentity.Id">Objeto que contiene los valores de los campos que se van a modificiar</param>
        /// <param name="objIdentity.Nombre">Objeto que contiene los valores de los campos que se van a modificiar</param>
        /// <param name="objIdentity.Descripcion">Objeto que contiene los valores de los campos que se van a modificiar</param>
        /// <param name="objIdentity.Activo">Objeto que contiene los valores de los campos que se van a modificiar</param>
        public void ActualizarTipoEstadoProceso(ref TipoEstadoProcesoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());

                object[] parametros = new object[] 
                                      { 
                                          objIdentity.Id,
                                          objIdentity.Nombre,
                                          objIdentity.Descripcion,
                                          objIdentity.Activo
                                      };

                DbCommand cmd = db.GetStoredProcCommand("GEN_UPDATE_TIPO_ESTADO_PROCESO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Inserta un registro de la tabla Tipo_Estado_Proceso
        /// </summary>
        /// <param name="objIdentity.Nombre">Nombre del tipo estado proceso</param>
        /// <param name="objIdentity.Descripcion">Descripción del tipo estado proceso</param>
        public void InsertarTipoEstadoProceso(ref TipoEstadoProcesoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());

                object[] parametros = new object[] { objIdentity.Nombre, objIdentity.Descripcion };

                DbCommand cmd = db.GetStoredProcCommand("GEN_UPDATE_TIPO_ESTADO_PROCESO", parametros);
                db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un registro de la tabla Tipo_Estado_Proceso
        /// </summary>
        /// <param name="objIdentity.Id">Identificador del registro del tipo estado proceso a eliminar</param>
        public void EliminarTipoEstadoProceso(ref TipoEstadoProcesoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());

                object[] parametros = new object[] { objIdentity.Id };

                DbCommand cmd = db.GetStoredProcCommand("GEN_DELETE_TIPO_ESTADO_PROCESO", parametros);
                db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de los tipos de ubicación
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipos de ubicación a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerTipoEstadoProceso(ref TipoEstadoProcesoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_TIPO_ESTADO_PROCESO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TES_ID_ESTADO_PROCESO"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["TES_ESTADO_PROCESO"]);
                objIdentity.Descripcion = Convert.ToString(dsResultado.Tables[0].Rows[0]["TES_DESCRIPCION"]);
                objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["TES_ACTIVO"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista todos los tipo de ubicación en la BD o uno en particular.
        /// </summary>
        /// <param name="objIdentity.Id" >Con este valor se lista los tipos de estado del proceso con un identificador determinado, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: TES_ID_ESTADO_PROCESO, TES_ESTADO_PROCESO, TES_DESCRIPCION, TES_ACTIVO</returns>
        public DataSet ListarTipoEstadoProceso(TipoEstadoProcesoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_TIPO_ESTADO_PROCESO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista todos los tipo de ubicación en la BD o uno en particular.
        /// </summary>
        /// <param name="intId" >Con este valor se lista los tipos de estado del proceso con un identificador determinado, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: TES_ID_ESTADO_PROCESO, TES_ESTADO_PROCESO, TES_DESCRIPCION, TES_ACTIVO</returns>
        public DataSet ListarTipoEstadoProceso(Nullable<int> intId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_TIPO_ESTADO_PROCESO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }
    }
}
