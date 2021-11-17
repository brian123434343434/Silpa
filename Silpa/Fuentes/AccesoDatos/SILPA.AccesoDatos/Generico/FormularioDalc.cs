using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace SILPA.AccesoDatos.Generico
{
    
    /// <summary>
    /// Obtiene los datos del formulario
    /// </summary>
    public class FormularioDalc
    {
        /// <summary>
        /// 
        /// </summary>
        public FormularioDalc() { this.objConfiguracion = new Configuracion(); }

        /// <summary>
        /// Objeto de datos de configuración de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Obtiene los datos del formulario consultado
        /// </summary>
        /// <returns>dataset con las siguientes columnas -> [ValorCampo - Valor]</returns>
        public DataSet ConsultarDatosFormulario(Int64 int64ProcessInstance)
        {
           // XmlDocument xmlDoc = new XmlDocument();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { int64ProcessInstance };
                DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_INFO_FORMULARIO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                //xmlDoc.LoadXml(dsResultado.GetXml());
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Consulta el listado de formularios asociados a un proceso y/o a una actividad o los
        /// formularios hijos de un formulario, Columna Retorno -> ["ID"]
        /// </summary>
        /// <param name="idProcessInstance">ID de la instancia de proceso - Valor es childForm si se usa para hijos de formulario</param>
        /// <param name="idActivityInstance">ID de la instancia de la actividad - Valor es ID del formulario padre si se trata de buscar hijos</param>
        /// <returns>Dataset con lista de formularios</returns>
        /// <remarks>Si se trata de buscar formularios hijos se debe poner la combinación de parametros "childForm", idFormularioPadre,
        /// es decir, para el primer parámetro no se puede colocar valor de instancia del proceso</remarks>
        /// <exception cref="exc_creacion_proceso">Este método retornará vacío si se usa en el momento de creación de un proceso de bpm</exception>
        public DataSet ConsultarListadoFormulario(string idProcessInstance, string idActivityInstance) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idProcessInstance, idActivityInstance };
            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_LISTADO_FORMULARIOS", parametros);
                
            try
            {

                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }


        /// <summary>
        /// Consulta el listado de formularios multiregistros asociados a una instancia de proceso
        /// </summary>
        /// <returns>DataSet : conjunto de resultados con los identificadores de las instancias de los
        /// formularios asociados. Con las siguientes columnas: -> [idFormInstance - TEXTO - FORMULARIO - DATO]
        /// </returns>
        
        public DataSet ConsultarListadoCamposMultiRegistros(Int64 int64ProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { int64ProcessInstance };
                DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_MULTI_REGISTRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ConsultarListadoCamposMultiRegistrosyPrincipal(Int64 int64ProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { int64ProcessInstance };
                DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_MULTI_REGISTRO_MAS_PADRE", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Consulta el listado de valores asociados a un formulario a partir de un identificador de instancia del formulario
        /// </summary>
        /// <returns>DataSet : conjunto de resultados con los valores ingresados en determinado formmulario
        /// Con las siguientes columnas: -> [TEXTO- VALOR]
        /// </returns>
        public DataSet ConsultarListadoCamposForm(Int64 intFormInstance)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intFormInstance };
            DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_INFO_FORMULARIO", parametros);
            try
            {
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }


        /// <summary>
        /// Método que entrega el listado de formularios del bpm
        /// </summary>
        /// <returns>Dataset  con el listado de los formularios del bpm</returns>
        public DataSet ListarFormulariosBPM(Int64 intProcessInstance) 
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intProcessInstance };
                DbCommand cmd = db.GetStoredProcCommand("BPM_LISTAR_FORMULARIOS", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Método que entrega el listado de formularios del bpm
        /// </summary>
        /// <returns>Dataset  con el listado de los formularios del bpm</returns>
        public DataSet ObtenerDatosFormulariosPorProceso(Int64 intProcessInstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intProcessInstance };
                DbCommand cmd = db.GetStoredProcCommand("OBTENER_DATOS_FORMULARIOS_POR_PROCESO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Método que entrega el listado de formularios del bpm
        /// </summary>
        /// <returns>Dataset  con el listado de los formularios del bpm</returns>
        public DataSet ObtenerDatosFormulariosProceso(Int64 intProcessInstance, string valorCampo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intProcessInstance, valorCampo };
                DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_DATOS_FORMULARIOS_PROCESO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// hava:04-oct-10
        /// Actualiza el identificador del participante en FormInstance
        /// </summary>
        /// <param name="IdProcessInstance">long: identificador del proceso</param>
        public void ActualizarParticipanteCesionDerechos(long IdProcessInstance) 
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IdProcessInstance};
                DbCommand cmd = db.GetStoredProcCommand("ACTUALIZAR_FORMINSTANCE_CESION_DERECHOS", parametros);
                int  i = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataSet ObtenerDatosCorreoRespuestaEE(string IDENTRYDATA, Int64 IDAPPUSER, string IDFORM)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { Convert.ToInt32(IDENTRYDATA), IDAPPUSER, Convert.ToInt32(IDFORM) };
                DbCommand cmd = db.GetStoredProcCommand("BPM_VALIDA_ENVIA_CORREO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
