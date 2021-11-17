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
    public class TipoDireccionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoDireccionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad del tipo de dirección
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipo de dirección a cargar, en la propiedad ID del objetoIdentity</param>
        public TipoDireccionDalc(ref TipoDireccionIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id  };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_DIRECCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_TIPO_DIRECCCION"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["NOMBRE_TIPO_DIRECCION"]);
                objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["TIPO_DIRECCION_ACTIVO"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Carga los valores para una identidad del tipo de dirección cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipo de dirección a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerTipoDireccion(ref TipoDireccionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_DIRECCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_TIPO_DIRECCCION"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["NOMBRE_TIPO_DIRECCION"]);
                objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["TIPO_DIRECCION_ACTIVO"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Lista los tipos de dirección en la BD.
        /// </summary>
        /// <param name="objIdentity.Id" >Valor del identificador del tipo de direccion por el cual se filtraran los tipo de direccion.
        /// si es null no existen restricciones.</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  ID_TIPO_DIRECCCION, NOMBRE_TIPO_DIRECCION, TIPO_DIRECCION_ACTIVO</returns>
        public DataSet ListarTipoDireccion(Nullable<int> intTipoProyecto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intTipoProyecto};
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_DIRECCION", parametros);
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
