using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.AdmTablasBasicas
{
    public class TablasBasicasDalc
    {
        private Configuracion objConfiguracion;

        public TablasBasicasDalc()
        { 
            objConfiguracion = new Configuracion();
        }

        #region "BPM_PARAMETROS"
        
        /// <summary>
        /// Listar la informacion de los parametros
        /// </summary>
        /// <param name="strNombre">Nombre Parametro</param>
        /// <returns></returns>
        public DataTable Listar_Bpm_Parametros(string strNombre)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNombre };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_BPM_PARAMETROS", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Obtiene la infomacion de los tipos de parametro, es una funcion que retorna una tabla con la
        /// descripcion de la informacion.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable Listar_Tipos_Parametros()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetSqlStringCommand("SELECT TIPOID, DESCRIPCION FROM dbo.FUN_TIPO_BPM_PARAMETROS()");
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Insertar un nuevo parametro
        /// </summary>
        /// <param name="iTipo">Tipo Parametro</param>
        /// <param name="strNombre">Nombre Parametro</param>
        /// <param name="iCodigo">Valor del Parametro</param>
        public void Insertar_Bpm_Parametros(int iTipo, string strNombre, int iCodigo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iTipo, strNombre, iCodigo };
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_BPM_PARAMETROS", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Actualizar informacion de los parametros
        /// </summary>
        /// <param name="iID">Llave de los parametros</param>
        /// <param name="iTipo">Tipo Parametro</param>
        /// <param name="strNombre">Nombre Parametro</param>
        /// <param name="iCodigo">Valor del Parametro</param>
        public void Actualizar_Bpm_Parametros(int iID, int iTipo, string strNombre, int iCodigo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {iID, iTipo, strNombre, iCodigo };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_BPM_PARAMETROS", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Eliminar informacion de los parametros
        /// </summary>
        /// <param name="iID">Llave de los parametros</param>
        public void Eliminar_Bpm_Parametros(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_BPM_PARAMETROS", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }
            
        #endregion


        #region "DOC_TIPO_ADQUISICION"

        /// <summary>
        /// Listar la informacion de los tipos de adquisicion
        /// </summary>
        /// <param name="strDescripcion">Nombre o Descripcion</param>
        /// <returns></returns>
        public DataTable Listar_doc_tipo_adquisicion(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DOC_TIPO_ADQUISICION", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Registra la descripcion de los tipos de adquisicion
        /// </summary>
        /// <param name="strDescripcion"></param>
        public void Insertar_doc_tipo_adquisicion(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_DOC_TIPO_ADQUISICION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Actualizar los tipos de adquisicion
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        /// <param name="strDescripcion">Descripcion adquisicion</param>
        public void Actualizar_doc_tipo_adquisicion(int iID, string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID, strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_DOC_TIPO_ADQUISICION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// Eliminar la informacion de los tipos de adquisicion
        /// </summary>
        /// <param name="iID">Codigo de la adquisicion</param>
        public void Eliminar_doc_tipo_adquisicion(int iID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { iID };
                DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_DOC_TIPO_ADQUISICION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }
        #endregion

        #region "TABLAS BASICAS"
        /// <summary>
        /// Informacion de las tablas basicas parametrizadas en el sistema
        /// </summary>
        /// <param name="strDescripcion">Codigo de busqueda</param>
        /// <returns></returns>
        public DataTable Listar_Tablas_Basicas(string strDescripcion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strDescripcion };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TABLAS_BASICAS", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        /// <summary>
        /// 21-jun-2010 - aegb
        /// metodo que retorna la lista de la informacion de datos de homologacion de una tabla basica
        /// </summary>
        /// <param name="idTabla"></param>
        /// <returns></returns>
        public List<DatosHomologacionEntity> ObtenerDatosHomologacion(int idTabla)
        {
            object[] parametros = new object[] { idTabla };
            SqlDatabase db = new SqlDatabase(   objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_DATOS_HOMOLOGACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            DatosHomologacionEntity _objDatos;
            List<DatosHomologacionEntity> listaDatos = new List<DatosHomologacionEntity>();          
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        _objDatos = new DatosHomologacionEntity();
                        _objDatos.id = dt["ID"].ToString();
                        _objDatos.nombre = dt["NOMBRE"].ToString();
                        listaDatos.Add(_objDatos);                       
                    }
                }
                return listaDatos;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }


        public string ObtenerApplicationUserComplementoHomologacion(int idPersona)
        {
            object[] parametros = new object[] { idPersona };
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_APPLICATION_USER_HOMOLOGACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            var applicationUser = string.Empty;

            try
            {
                if (dsResultado.Tables[0] != null && dsResultado.Tables[0].Rows.Count > 0)
                {
                    applicationUser = dsResultado.Tables[0].Rows[0]["APPUSER"].ToString();
                }
                return applicationUser;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }


        public int ObtenerSiPersonaActivaHomologacion(int idPersona)
        {
            object[] parametros = new object[] { idPersona };
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_PERSONA_ACTIVA_HOMOLOGACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            var estadoPersona = 0;

            try
            {
                if (dsResultado.Tables[0] != null && dsResultado.Tables[0].Rows.Count > 0)
                {
                    estadoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
                }
                return estadoPersona;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }

        #endregion


    }
}
