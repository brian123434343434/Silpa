using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace SILPA.AccesoDatos.Generico
{
    /*
     * Clase útil para la selección del proceso 
     * desde el servicio proxy - bpmservices
     */
    public class ProcesoDalc
    {

        /// <summary>
        /// objeto de configuracion web.config
        /// </summary>
        private Configuracion _objConfiguracion;


        

        public ProcesoDalc()
        {
            _objConfiguracion = new Configuracion();
        }




        /// <summary>
        /// Obtiene el nombre del proceso a ejecutar
        /// </summary>
        /// <returns></returns>
        public string ObtenerProceso(Int64 int64ProcessInstance)
        {
            // XmlDocument xmlDoc = new XmlDocument();
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { int64ProcessInstance };
            DbCommand cmd = db.GetStoredProcCommand("WSB_OBTENER_CASO_PROCESO_POR_PROCESO_ID", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="int64ProcessInstance"></param>
        /// <returns></returns>
        public ProcesoIdentity ObtenerObjProceso(Int64 int64ProcessInstance)
        {
            // XmlDocument xmlDoc = new XmlDocument();
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { int64ProcessInstance };
            DbCommand cmd = db.GetStoredProcCommand("WSB_OBTENER_CASO_PROCESO_POR_PROCESO_ID", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            ProcesoIdentity proceso = new ProcesoIdentity();
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                 proceso.Clave=Convert.ToString(dsResultado.Tables[0].Rows[0]["PRO_CLAVE_PROCESO"]);
                 proceso.IdProcessCase = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["PRO_ID_CASO_PROCESO"]);
                 proceso.TipoEntidad = (dsResultado.Tables[0].Rows[0]["PRO_TIPO_ENTIDAD"] != DBNull.Value) ? Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PRO_TIPO_ENTIDAD"]) : true; 
                
            }
            return proceso;

        }

        /// <summary>
        /// A partir del processinstances determinarmos cual es el estado actual del proceso actual
        /// </summary>
        /// <returns>Estado del proceso</returns>
        public DataTable ObtenerEstadoProceso(Int64 int64ProcessInstance)
        {
            // XmlDocument xmlDoc = new XmlDocument();
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { int64ProcessInstance };
            DbCommand cmd = db.GetStoredProcCommand("GEN_CONSULTAR_ACTIVIDAD_PROINSTANCE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado.Tables[0];
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// Busca en la BD la condicion que finaliza la actividad actual siempre y cuando el formulario asociado sea el
        /// formulario con el nombre strParametroBpm
        /// </summary>
        /// <param name="intActivityInstance">Instancia de la actividad</param>
        /// <param name="strNombreParametroBpm">Nombre del Formulario BPM previamente registrado en la BD de silpa</param>
        /// <returns>Identificador de la condicion</returns>
        public DataSet CondicionActual(int intActivityInstance, string strNombreParametroBpm)
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intActivityInstance, strNombreParametroBpm };
            DbCommand cmd = db.GetStoredProcCommand("BPM_CONDICION_ACTUAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado;
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// Busca en la BD la condicion que finaliza la actividad actual siempre y cuando el formulario asociado a la 
        /// actividad siguiente sea el formulario con el nombre strParametroBpm
        /// </summary>
        /// <param name="intActivityInstance">Instancia de la actividad</param>
        /// <param name="strNombreParametroBpm">Nombre del Formulario BPM previamente registrado en la BD de silpa</param>
        /// <returns>DataSet con las siguietes columnas: [IDActivityInstance - IDCondition]</returns>
        public DataSet CondicionSiguiente(int intActivityInstance, string strNombreParametroBpm)
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intActivityInstance, strNombreParametroBpm };
            DbCommand cmd = db.GetStoredProcCommand("BPM_CONDICION_SIGUIENTE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                return dsResultado;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Busca en la BD el ultimo identificador del la instancia de la actividad asociado al identificador del process instance
        /// </summary>
        ///<param name="intIdProcessInstance">Identificador del proceso</param>
        /// <returns>El identificador de la ultima instancia de actividad asociado al identificador 
        /// de la instancia del proceso</returns>
        public int UltimoIdActivityInstance(int intIdProcessInstance)
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdProcessInstance };
            DbCommand cmd = db.GetStoredProcCommand("BPM_ULTIMO_ACTINSTANCE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsResultado.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// Obtiene el identificador de la actividad actual
        /// </summary>
        /// <param name="intIdProcessInstance">Identificador del Procesinstace </param>
        /// <returns></returns>
        public int ActividadActual(long intIdProcessInstance)
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdProcessInstance, 0 };
            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_ACTIVIDAD_ACTUAL", parametros);
            int i = db.ExecuteNonQuery(cmd);
            int result = (int)db.GetParameterValue(cmd, "IDActivity");
            return result;
        }

        /// <summary>
        /// hava:28-abr-10
        /// Obtiene la condición necesaria para avanzar el proceso 
        /// partiendo de una actividad inicial para llevalrlo a una activida destino
        /// </summary>
        /// <param name="intActividadActual">identidicador de la actividad inicial</param>
        /// <param name="intActividadFinal">identificador de la  actividad  final</param>
        /// <returns>identificador de la condición para la trancisión</returns>
        public int ObtenerCondicionTransicion(int intActividadActual, int intActividadFinal) 
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intActividadActual, intActividadFinal, 0 };
            DbCommand cmd = db.GetStoredProcCommand("BPM_CONDICION_POR_TRANCISION_DE_ACTIVIDADES", parametros);
            int i = db.ExecuteNonQuery(cmd);
            int result = (int)db.GetParameterValue(cmd, "IDCondition");
            return result;
        }


        /// <summary>
        /// hava:21-jun-10
        /// Método que permite obtener las condiciones por proceso para el formulario de pago electrónico
        /// Existen dos condicones posibles:  una para Botón pago y otra para botón impresión del recibo de pago
        /// </summary>
        public void ObtenerCondicionPagoElectronico(ref ProcesoIdentity identity) 
        { 
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { identity.IdProcessCase, string.Empty, string.Empty };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_CONDICION_PROCESO_PAGO", parametros);
            
            db.ExecuteNonQuery(cmd);

            identity.CondicionPago = (string)db.GetParameterValue(cmd, "CONDICION_PAGO");
            identity.CondicionImprimirReciboPago = (string)db.GetParameterValue(cmd, "CONDICION_IMPRIMIR");
        }
 
        /// <summary>
        /// Obtiene el tipo de tramite por processInstance
        /// </summary>
        /// <param name="IntProcessInstance"></param>
        /// <returns>string: Nombre del tipo de proceso</returns>
        public string ObtenerTipoTramiteByProcessInstance(Int64 IntProcessInstance)
        {
            SqlDatabase db = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { IntProcessInstance, String.Empty };
            DbCommand cmd = db.GetStoredProcCommand("GEN_OBTENER_TIPO_TRAMITE_POR_PROCESSINSTANCE", parametros);
	        db.ExecuteNonQuery(cmd);
            string result =  (string)db.GetParameterValue(cmd, "@NombreTipoTramite");
            return result;
        }

    }
}
