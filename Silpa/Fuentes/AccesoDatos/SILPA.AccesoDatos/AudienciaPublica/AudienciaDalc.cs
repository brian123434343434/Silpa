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

namespace SILPA.AccesoDatos.AudienciaPublica
{
    public class AudienciaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public AudienciaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void InsertarDatosAudiencia(ref AudienciaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                {
                    objIdentity.IdAudiencia,
                    objIdentity.FechaExpEdicto, objIdentity.NumeroSilpa, objIdentity.NombreProyecto,
                    objIdentity.NumeroExpediente, objIdentity.IdAutoridad, objIdentity.EntidadesAud,
                    objIdentity.FechaAudiencia, objIdentity.LugarAudiencia, objIdentity.FechaReunionInformativa,
                    objIdentity.LugarReunionInformativa, objIdentity.LugarInscripcionPonentes, 
                    objIdentity.LugaresConsultaEstudios, objIdentity.Edicto, objIdentity.Convocatoria
                };

                DbCommand cmd = db.GetStoredProcCommand("AUD_CREAR_AUDIENCIA", parametros);
                db.ExecuteNonQuery(cmd);

                objIdentity.IdAudiencia = Int32.Parse(cmd.Parameters["@P_AUD_ID"].Value.ToString());
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Datos Audiencia.";
                throw new Exception(strException, ex);
            }
        }

        public DataSet ListarAudiencias(string _idAudiencia)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idAudiencia };
                DbCommand cmd = db.GetStoredProcCommand("AUD_LISTAR_AUDIENCIA", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos);
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        public DataSet ConsultarInscritosAudiencia(string _idAudiencia)
        {
            DataSet ds_datos = new DataSet();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idAudiencia };
                DbCommand cmd = db.GetStoredProcCommand("AUD_CONSULTAR_INSCRITOS_AUDIENCIA", parametros);
                
                ds_datos = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Inscritos Audiencia.";
                throw new Exception(strException, ex);
            }

            return ds_datos;
        }

        public bool AprobarInscritos(string _numeroInscripcion, bool _aprobado, string _motivo)
        {

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { _numeroInscripcion, _aprobado, _motivo };

                DbCommand cmd = db.GetStoredProcCommand("AUD_ACT_APROBAR_INSCRITOS_AUDIENCIA", parametros);

                db.ExecuteNonQuery(cmd);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ObtenerPathDocumentos(string numeroVitalAudiencia)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { numeroVitalAudiencia };

                DbCommand cmd = db.GetStoredProcCommand("AUD_CONSULTAR_RUTA_INSCRITO_AUDIENCIA", parametros);

                return db.ExecuteScalar(cmd).ToString();

                
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// Consultar el listado de audiencias
        /// </summary>
        /// <param name="idprocess">identificador del proceso</param>
        /// <param name="sol_id_aa">identificador de la aurtoridad ambiental</param>
        /// <param name="exp_cod">codigo del expediente</param>
        /// <returns></returns>
        public DataSet ConsultarSolicitanteAudienciaPublicaGenEdi(int idprocess, int sol_id_aa, string exp_cod)
        {
           
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { idprocess, sol_id_aa, exp_cod };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SOLICITANTE_AUD_GEN_EDIPTO", parametros);

                return db.ExecuteDataSet(cmd);
            }
            catch(Exception exc)
            {
                throw new Exception("Error consultando el listado de solicitantes", exc);
            }
        }


        /// <summary>
        /// Consultar informacion de solicitante de audiencia
        /// </summary>
        /// <param name="idprocess">identificador del proceso</param>
        /// <param name="sol_id_aa">identificador de la aurtoridad ambiental</param>
        /// <param name="mov_id">identficador del movimiento</param>
        /// <returns></returns>
        public DataSet ConsultarSolicitanteAudienciaPublica(Int32 idprocess, Int32 sol_id_aa, Int32 mov_id)
        {
            
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { idprocess, sol_id_aa, mov_id };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SOLICITANTE_AUDIENCIA", parametros);

                return db.ExecuteDataSet(cmd);
            }
            catch (Exception exc)
            {
                throw new Exception("Error consultando el listado de solicitantes", exc);
            }
        }
    }
}
