using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace SILPA.AccesoDatos.Salvoconducto
{
    public class AsignarNumeracionSalvoconductoDalc
    {

        private Configuracion objConfiguracion;

        public AsignarNumeracionSalvoconductoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        #region Obtengo la paranetrizacion 
        public DataSet ListarParametrizacionSalvoconducto(string campos, string tabla, string condicion, string orden)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_VALORES_TABLA");
                db.AddInParameter(cmd, "@TXT_CAMPOS", DbType.String, campos);
                db.AddInParameter(cmd, "@TXT_TABLA", DbType.String, tabla);
                db.AddInParameter(cmd, "@TXT_WHERE", DbType.String, condicion);
                db.AddInParameter(cmd, "@TXT_ORDER_BY", DbType.String, orden);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion

        #region Validar la numeracion 
        public DataSet ValidarNumeracionSalvoconducto(int id_serie,int RangoDesde, int RangoHasta)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_VALIDA_SERIE_SALVOCONDUCTO");
                db.AddInParameter(cmd, "@P_ID_SERIE", DbType.Int32, id_serie);
                db.AddInParameter(cmd, "@P_SERIE_DESDE", DbType.Int32, RangoDesde);
                db.AddInParameter(cmd, "@P_SERIE_HASTA", DbType.Int32, RangoHasta);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region Grabo la serie
        public bool GrabarSerieDalc(int ID_AUT_AMBIENTAL, int SERIE_DESDE, int SERIE_HASTA, int CNT_SERIES_ALERTA, string NOMBRE_ARCHIVO_CREACION_SERIE, string RUTA_ARCHIVO_CREACION_SERIE, string CODIGO_USUARIO)
        {
            bool respuesta = true;

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTA_SERIES_SALVOCONDUCTO");
                db.AddInParameter(cmd, "@p_ID_AUT_AMBIENTAL", DbType.Int32, ID_AUT_AMBIENTAL);
                db.AddInParameter(cmd, "@p_SERIE_DESDE", DbType.Int32, SERIE_DESDE);
                db.AddInParameter(cmd, "@p_SERIE_HASTA", DbType.Int32, SERIE_HASTA);
                db.AddInParameter(cmd, "@p_PJE_SERIES_ALERTA", DbType.Int32, CNT_SERIES_ALERTA);
                db.AddInParameter(cmd, "@p_NOMBRE_ARCHIVO_CREACION_SERIE", DbType.String, NOMBRE_ARCHIVO_CREACION_SERIE);
                db.AddInParameter(cmd, "@p_RUTA_ARCHIVO_CREACION_SERIE", DbType.String, RUTA_ARCHIVO_CREACION_SERIE);
                db.AddInParameter(cmd, "@p_COD_USUARIO_CREACION", DbType.String, CODIGO_USUARIO);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                respuesta = false;
                throw new Exception(ex.Message);

            }
            return respuesta;
        }
        #endregion

        #region Bloqueo la Serie
        public bool BloquearSerieDalc(int ID_SERIE, int ESTADO_SERIE_ID, string NOMBRE_ARCHIVO_BLOQUEO_SERIE, string RUTA_ARCHIVO_BLOQUEO_SERIE, string MOTIVO_BLOQUEO)
        {
            bool respuesta = true;
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_BLOQUEA_SERIES_SALVOCONDUCTO");
                db.AddInParameter(cmd, "@p_ID_SERIE", DbType.Int32, ID_SERIE);
                db.AddInParameter(cmd, "@p_ESTADO_SERIE_ID", DbType.Int32, ESTADO_SERIE_ID);
                db.AddInParameter(cmd, "@p_NOMBRE_ARCHIVO_BLOQUEO_SERIE", DbType.String, NOMBRE_ARCHIVO_BLOQUEO_SERIE);
                db.AddInParameter(cmd, "@p_RUTA_ARCHIVO_BLOQUEO_SERIE", DbType.String, RUTA_ARCHIVO_BLOQUEO_SERIE);
                db.AddInParameter(cmd, "@p_MOTIVO_BLOQUEO", DbType.String, MOTIVO_BLOQUEO);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            return respuesta;
        }
        #endregion

        #region Edito la Serie
        public bool EditarSerieDalc(int ID_SERIE, int SERIE_DESDE, int SERIE_HASTA, int CNT_SERIES_ALERTA, string NOMBRE_ARCHIVO_CREACION_SERIE, string RUTA_ARCHIVO_CREACION_SERIE, string CODIGO_USUARIO)
        {
            bool respuesta = true;
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_EDITA_SERIES_SALVOCONDUCTO");
                db.AddInParameter(cmd, "@p_ID_SERIE", DbType.Int32, ID_SERIE);
                db.AddInParameter(cmd, "@p_SERIE_DESDE", DbType.Int32, SERIE_DESDE);
                db.AddInParameter(cmd, "@p_SERIE_HASTA", DbType.Int32, SERIE_HASTA);
                db.AddInParameter(cmd, "@p_PJE_SERIES_ALERTA", DbType.Int32, CNT_SERIES_ALERTA);
                db.AddInParameter(cmd, "@p_NOMBRE_ARCHIVO_CREACION_SERIE", DbType.String, NOMBRE_ARCHIVO_CREACION_SERIE);
                db.AddInParameter(cmd, "@p_RUTA_ARCHIVO_CREACION_SERIE", DbType.String, RUTA_ARCHIVO_CREACION_SERIE);
                db.AddInParameter(cmd, "@p_COD_USUARIO_CREACION", DbType.String, CODIGO_USUARIO);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return respuesta;
        }
        #endregion

        #region Consulto la serie para editar y bloquear
        public DataSet AsignarNumeroSalvoconductoDalc(int ID_AUT_AMBIENTAL, int? SERIE_DESDE, int? SERIE_HASTA, DateTime? FECHA_INGRESO_INI, DateTime? FECHA_INGRESO_FIN, int ESTADO_SERIE_ID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_SERIES_SALVOCONDUCTO");
                db.AddInParameter(cmd, "@p_ID_AUT_AMBIENTAL", DbType.Int32, ID_AUT_AMBIENTAL);
                db.AddInParameter(cmd, "@p_SERIE_DESDE", DbType.Int32, SERIE_DESDE);
                db.AddInParameter(cmd, "@p_SERIE_HASTA", DbType.Int32, SERIE_HASTA);
                db.AddInParameter(cmd, "@p_FECHA_INGRESO_INI", DbType.DateTime, FECHA_INGRESO_INI);
                db.AddInParameter(cmd, "@p_FECHA_INGRESO_FIN", DbType.DateTime, FECHA_INGRESO_FIN);
                db.AddInParameter(cmd, "@p_ESTADO_SERIE_ID", DbType.Int32, ESTADO_SERIE_ID);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Consulto la serie para editar y bloquear
        public DataSet ConsultarDatosSerieDalc(int ID_AUT_AMBIENTAL, int? SERIE_DESDE, int? SERIE_HASTA, DateTime? FECHA_INGRESO_INI, DateTime? FECHA_INGRESO_FIN, int ESTADO_SERIE_ID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_SERIES_SALVOCONDUCTO");
                db.AddInParameter(cmd, "@p_ID_AUT_AMBIENTAL", DbType.Int32, ID_AUT_AMBIENTAL);
                db.AddInParameter(cmd, "@p_SERIE_DESDE", DbType.Int32, SERIE_DESDE);
                db.AddInParameter(cmd, "@p_SERIE_HASTA", DbType.Int32, SERIE_HASTA);
                db.AddInParameter(cmd, "@p_FECHA_INGRESO_INI", DbType.DateTime, FECHA_INGRESO_INI);
                db.AddInParameter(cmd, "@p_FECHA_INGRESO_FIN", DbType.DateTime, FECHA_INGRESO_FIN);
                db.AddInParameter(cmd, "@p_ESTADO_SERIE_ID", DbType.Int32, ESTADO_SERIE_ID);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

    }
}
