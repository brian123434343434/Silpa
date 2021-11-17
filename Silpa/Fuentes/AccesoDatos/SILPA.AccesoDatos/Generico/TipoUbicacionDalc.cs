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
    public class TipoUbicacionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoUbicacionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de los tipos de ubicación
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipos de ubicación a cargar, en la propiedad ID del objetoIdentity</param>
        public TipoUbicacionDalc(ref TipoUbicacionIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["UBI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["UBI_NOMBRE"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de los tipos de ubicación
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipos de ubicación a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerTipoUbicacion(ref TipoUbicacionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["UBI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["UBI_NOMBRE"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista todos los tipo de ubicación en la BD o uno en particular.
        /// </summary>
        /// <param name="objIdentity.Id" >Con este valor se lista los tipos de ubicación con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  UBI_ID, UBI_NOMBRE</returns>
        public DataSet ListarTipoUbicacion(TipoUbicacionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("", parametros);
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
        /// <param name="intId" >Con este valor se lista los tipos de ubicación con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  UBI_ID, UBI_NOMBRE</returns>
        public DataSet ListarTipoUbicacion(Nullable<int> intId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_UBICACION", parametros);
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
