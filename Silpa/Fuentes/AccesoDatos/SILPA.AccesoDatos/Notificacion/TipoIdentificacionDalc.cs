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
using SILPA.AccesoDatos.Notificacion;




namespace SILPA.AccesoDatos.Generico
{
    public class TipoIdentificacionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoIdentificacionDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoIdentificacionDalc(ref TipoIdentificacionEntity objTipoId)
        {
            objConfiguracion = new Configuracion();
            this.ObtenerTipoIdentificacion(ref objTipoId);
        }


        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de los tipos de identificación
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipos de identificación a cargar, en la propiedad ID del objetoIdentity</param>
        

        public List<TipoIdentificacionEntity> ListarTiposIdentificacion()
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            TipoIdentificacionEntity tipo;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_TIPO_ID");
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<TipoIdentificacionEntity> lista;

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<TipoIdentificacionEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        tipo = new TipoIdentificacionEntity();

                        tipo.Id = Convert.ToInt32(dt["ID_TIPO_IDENTIFICACION"]);
                        tipo.Sigla = dt["NTI_CODIGO_TIPO_ID"].ToString();
                        tipo.Nombre = dt["NTI_NOMBRE_TIPO_ID"].ToString();

                        lista.Add(tipo);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public TipoIdentificacionEntity ListarTipoIdentificacion(object[] parametros)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            TipoIdentificacionEntity tipo;
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_TIPO_ID", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        tipo = new TipoIdentificacionEntity();

                        tipo.Id = Convert.ToInt32(dt["ID_TIPO_IDENTIFICACION"]);
                        tipo.Sigla = dt["NTI_CODIGO_TIPO_ID"].ToString();
                        tipo.Nombre = dt["NTI_NOMBRE_TIPO_ID"].ToString();

                        return tipo;
                    }
                    return null;
                }
                return null;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de los tipos de identificación
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipos de identificación a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerTipoIdentificacion(ref TipoIdentificacionEntity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id, objIdentity.Sigla };
                DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_TIPO_ID", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_TIPO_IDENTIFICACION"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["NTI_NOMBRE_TIPO_ID"]);
                objIdentity.Sigla = Convert.ToString(dsResultado.Tables[0].Rows[0]["NTI_CODIGO_TIPO_ID"]);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al obtener los Tipo de Identificación.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Consulta un tipo de identificación proporicionando el código
        /// </summary>
        /// <param name="codigo">código de gel-xml de la identificación</param>
        /// <returns>objeto de identificación</returns>
        public TipoIdentificacionEntity ObtenerTipoIdentificacionPorCodigo(string codigo)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
	            object[] parametros = new object[] { null, codigo };
	            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_TIPO_ID", parametros);
	            DataSet dsResultado = db.ExecuteDataSet(cmd);
	            TipoIdentificacionEntity objIdentity = new TipoIdentificacionEntity();
	            objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_TIPO_IDENTIFICACION"]);
	            objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["NTI_NOMBRE_TIPO_ID"]);
	            objIdentity.Sigla = Convert.ToString(dsResultado.Tables[0].Rows[0]["NTI_CODIGO_TIPO_ID"]);
	            return objIdentity;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al consultar Tipo de Identificación.";
                throw new Exception(strException, ex);
            }
        }

        ///// <summary>
        ///// Lista todos los tipo de identificación en la BD o uno en particular.
        ///// </summary>
        ///// <param name="objIdentity.Id" >Con este valor se lista los tipos de identificación del proceso con un identificador determinado, si es null no existen restricciones</param>
        ///// <returns>DataSet con los registros y las siguientes columnas: TES_ID_ESTADO_PROCESO, TES_ESTADO_PROCESO, TES_DESCRIPCION, TES_ACTIVO</returns>
        //public DataSet ListarTipoIdentificacion(TipoIdentificacionIdentity objIdentity)
        //{
        //    try
        //    {
        //        SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
        //        object[] parametros = new object[] { objIdentity.Id };
        //        DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_IDENTIFICACION", parametros);
        //        DataSet dsResultado = db.ExecuteDataSet(cmd);
        //        return (dsResultado);
        //    }
        //    catch (SqlException sqle)
        //    {
        //        throw new Exception(sqle.Message);
        //    }
        //}

        /// <summary>
        /// Lista todos los tipo de identificación en la BD o uno en particular.
        /// </summary>
        /// <param name="intId" >Con este valor se lista los tipos de estado del proceso con un identificador determinado, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: TID_ID, TID_NOMBRE, TID_ACTIVO, TID_SIGLA</returns>
        public DataSet ListarTipoIdentificacionPorID(Nullable<int> intId)
        {
           
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_IDENTIFICACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                db = null;
                return (dsResultado);


        }

        /// <summary>
        /// Lista todos los tipo de identificación en la BD con el tipo de persona corespondiente a cada documento.
        /// </summary>
        /// <param name="intId" >Con este valor se lista los tipos de estado del proceso con un identificador determinado, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: TID_ID, TID_NOMBRE, TID_ACTIVO, TID_SIGLA</returns>
        public DataSet ListarTipoIdentificacionXTipoPersona()
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_IDENTIFICACION_TPER");
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            db = null;
            return (dsResultado);


        }
    }
}
