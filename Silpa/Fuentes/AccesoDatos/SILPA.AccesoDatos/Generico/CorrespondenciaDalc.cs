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
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{

    /// <summary>
    /// Clase que obtiene los datos de correspondencia generados para cada autoridad ambiental
    /// </summary>
    public class CorrespondenciaDalc
    {

        private Configuracion objConfiguracion;
        public CorrespondenciaDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Obtiene los datos de la entidad DocumentoIdentity (Documento relacionado con un Acto)
        /// mediante el número de acto administrativo.
        /// y el identificador de la Autoridad Ambiental.
        /// </summary>
        /// <param name="objIdentity">DataSet: con el conjunto de datos</param>
        public DataSet ListarEstadosCorrespondencia(string estado)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { estado };

                DbCommand cmd = db.GetStoredProcCommand("LISTAR_ESTADOS_CORRESPONDENCIA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return dsResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método que lista los movimientos en radicación....
        /// </summary>
        /// <param name="strAutoridadAmbiental"> código de la autoridad ambiental</param>
        /// <param name="strNumeroRadicado">numero de radicado</param>
        /// <param name="intIdRemitente">identificador del usuario</param>
        /// <param name="dteFechaDesde">Fecha inicial de consulta</param>
        /// <param name="dteFechaHasta">Fecha final de consulta</param>
        /// <param name="intEstadoId">Identificador del estado de la radicación </param>
        /// <param name="strAsunto"></param>
        /// <returns></returns>
        public DataSet ListarMovimientoRadicacion
            (
                int strAutoridadAmbiental, string strNumeroRadicado, string strNumeroSilpa,
                int intIdRemitente, string dteFechaDesde,
                string dteFechaHasta, int intEstadoId, string strAsunto
            )
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                {
                    strAutoridadAmbiental, strNumeroRadicado, strNumeroSilpa, 
                    intIdRemitente, dteFechaDesde,dteFechaHasta, intEstadoId, strAsunto 
                };

                DbCommand cmd = db.GetStoredProcCommand("COR_LISTAR_MOVIMIENTO_RADICACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return dsResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Copnsulta los movimientos por identificador de radicaco en base de datos
        /// </summary>
        /// <param name="int64IDRadicacion">identificador del radicado</param>
        /// <returns>DataSet con el conjunto de resultados</returns>
        public DataSet ConsultarMovimientoSilpaxId(Int64 int64IDRadicacion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { int64IDRadicacion };

                DbCommand cmd = db.GetStoredProcCommand("COR_CONSULTAR_MOVIMIENTO_SILPA_X_ID", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return dsResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public Movimiento consultarMovimientoxNUR(string strRadicado)
        public Movimiento ConsultarMovimientoPorRadicado(string strRadicado)
        {
            //this.oConn.crearComando("xs_sila_con_documento_nur", CommandType.StoredProcedure);
            //this.oConn.agregarParametro("@STR_NUR", ParameterDirection.Input, DbType.String, str_nur);
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { strRadicado };

                DbCommand cmd = db.GetStoredProcCommand("COR_CONSULTAR_MOVIMIENTO_SILPA_X_RADICADO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                Movimiento result = new Movimiento();

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Método para obtener el listado de correspondencia adjunto por un expediente
        /// SOFT - NETCO - HAVA
        /// 11 -FEB -10
        /// </summary>
        /// <param name="intIdExpediente">identificador del expediente en SilaMc</param>
        /// <param name="intAutId">Identificador de la autoridad ambiental en SilaMc</param>
        /// <returns>bool: true/ false</returns>
        public DataSet CorresPondenciaPorExpediente(Int32 intIdExpediente, Int32 intAutId)
        {
            try
            {

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intIdExpediente, intAutId };

                DbCommand cmd = db.GetStoredProcCommand("COR_OBTENER_CORRESPONDENCIA_RADICACION", parametros);

                DataSet ds = db.ExecuteDataSet(cmd);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return null;
            }
        }

    }
}