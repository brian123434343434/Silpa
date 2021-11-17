using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class SeguimientoRutaSalvocondcutoDalc
    {
        private Configuracion objConfiguracion;

        public SeguimientoRutaSalvocondcutoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        /// <summary>
        /// Crea solicitud de salvoconducto todas empiezan en estado Solicitud.
        /// </summary>
        /// <param name="SeguimientoRutaSalvocondcutoDalc"> Objeto SalvoconductoNewIdentity </param>
        /// <returns>Salvoconducto ID</returns>
        /// 

        //public int GrabarLogConsultaDalc(ref SeguimientoRutaSalvoconductoIdentity pSeguimientoRutaSalvoconductoIdentity)
        public int GrabarLogConsultaDalc(int SalvoconductoID, int DptoID, int MunID, string NombreRevisor, string IdentificacionRevisor, int IdAplicationUser)
        {
            try
            {
                int LogID = 0;
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTA_LOG_CONSULTA_SUNL");
                db.AddOutParameter(cmd, "P_LOG_ID", DbType.Int32, 10);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, SalvoconductoID);
                db.AddInParameter(cmd, "P_DEP_ID", DbType.Int32, DptoID);
                db.AddInParameter(cmd, "P_MUN_ID", DbType.Int32, MunID);
                db.AddInParameter(cmd, "P_NOMBRE_REVISOR", DbType.String, NombreRevisor);
                db.AddInParameter(cmd, "P_IDENTIFICACION_REVISOR", DbType.String, IdentificacionRevisor);
                db.AddInParameter(cmd, "P_ID_APPLICATION_USER", DbType.Int32, IdAplicationUser);
                db.ExecuteNonQuery(cmd);
                LogID = Convert.ToInt32(db.GetParameterValue(cmd, "@P_LOG_ID"));
                return LogID;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<SeguimientoRutaSalvoconductoIdentity> ValidarSalvoconductoDalc(string NUMERO, string CODIGO_SEGURIDAD, string DOCUMENTO_SOLICITANTE)
        {
            List<SeguimientoRutaSalvoconductoIdentity> LstValidacion = new List<SeguimientoRutaSalvoconductoIdentity>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());



            DbCommand cmd = db.GetStoredProcCommand("SP_VALIDAR_SALVOCONDUCTO");
            db.AddInParameter(cmd, "P_NUMERO", DbType.String, NUMERO);
            db.AddInParameter(cmd, "P_CODIGO_SEGURIDAD", DbType.String, CODIGO_SEGURIDAD);
            //JMARTTINEZ 13/11/2020
            if (!string.IsNullOrEmpty(DOCUMENTO_SOLICITANTE))
            {
                db.AddInParameter(cmd, "P_NUMERO_IDENTIFICACION", DbType.String, DOCUMENTO_SOLICITANTE);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {

                    {
                        SeguimientoRutaSalvoconductoIdentity vSeguimientoRutaSalvoconductoIdentity = new SeguimientoRutaSalvoconductoIdentity();
                        vSeguimientoRutaSalvoconductoIdentity.SalvoConductoID = Convert.ToInt32(reader["SALVOCONDUCTO_ID"]);
                        vSeguimientoRutaSalvoconductoIdentity.Estado_Descripcion = reader["ESTADO_DESCRIPCION"].ToString();
                        vSeguimientoRutaSalvoconductoIdentity.Tipo_Salvoconducto = reader["TIPO_SALVOCONDUCTO"].ToString();
                        if (reader["FECHA_INI_VIGENCIA"].ToString() != string.Empty)
                        {
                            vSeguimientoRutaSalvoconductoIdentity.Fecha_Ini_Vigencia = (reader["FECHA_INI_VIGENCIA"].ToString());
                        }

                        if (reader["FECHA_FIN_VIGENCIA"].ToString() != string.Empty)
                        {
                            vSeguimientoRutaSalvoconductoIdentity.Fecha_Fin_Vigencia = (reader["FECHA_FIN_VIGENCIA"].ToString());
                        }
                        vSeguimientoRutaSalvoconductoIdentity.Mensaje_Error = reader["MENSAJE_ERROR"].ToString();
                        vSeguimientoRutaSalvoconductoIdentity.Sn_Error = Convert.ToBoolean(reader["SN_ERROR"]);
                        if (reader["ESTADO_ID"].ToString() != string.Empty)
                            vSeguimientoRutaSalvoconductoIdentity.EstadoID = Convert.ToInt32(reader["ESTADO_ID"]);
                        LstValidacion.Add(vSeguimientoRutaSalvoconductoIdentity);
                    }
                }
                return LstValidacion;
            }
        }

        public DataSet ConsultaSalvoconductosEmitidosDalc(int AUT_ID, DateTime FEC_EXP_DESDE, DateTime FEC_EXP_HASTA, int TIPO_FILTRO_VIGENCIA, String NUMERO_SALVOCNDUCTO, int DPTO_ORIGEN_ID, int MUNICIPIO_ORIGEN_ID, int DPTO_DESTINO_ID, int MUNICIPIO_DESTINO_ID, int TITULAR_ID, int TIPO_SALVOCONDUCTO_ID, int ESTADO_ID, int CLASE_RECURSO_ID, bool USUARIO_CONSULTA, bool SN_RESOLUCION_438)
        {
            DataSet ds = new DataSet();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd;

            if (!USUARIO_CONSULTA)
            {
                if (SN_RESOLUCION_438)
                {
                    cmd = db.GetStoredProcCommand("SSP_REPORTE_SALVOCONDUCTOS_RES_438");
                }
                else
                {
                    cmd = db.GetStoredProcCommand("SSP_REPORTE_SALVOCONDUCTOS");
                }

            }
            else
            {
                if (SN_RESOLUCION_438)
                {
                    cmd = db.GetStoredProcCommand("SSP_REPORTE_SALVOCONDUCTOS_RES_438");
                    cmd.CommandTimeout = 0;
                }
                else
                {
                    cmd = db.GetStoredProcCommand("SSP_REPORTE_SALVOCONDUCTOS_TODOS");
                    cmd.CommandTimeout = 0;
                }
            }
            db.AddInParameter(cmd, "p_ID_AUT_AMBIENTAL", DbType.String, AUT_ID);
            db.AddInParameter(cmd, "p_FECHA_EXPEDICION_DESDE", DbType.DateTime, FEC_EXP_DESDE);
            db.AddInParameter(cmd, "p_FECHA_EXPEDICION_HASTA", DbType.DateTime, FEC_EXP_HASTA);
            db.AddInParameter(cmd, "P_TIPO_FILTRO_VIGENCIA", DbType.Int32, TIPO_FILTRO_VIGENCIA);
            db.AddInParameter(cmd, "p_NUMERO", DbType.String, NUMERO_SALVOCNDUCTO);
            db.AddInParameter(cmd, "p_DEPARTAMENTO_ORIGEN_ID", DbType.Int32, DPTO_ORIGEN_ID);
            db.AddInParameter(cmd, "p_MUNICIPIO_ORIGEN_ID", DbType.Int32, MUNICIPIO_ORIGEN_ID);
            db.AddInParameter(cmd, "p_DEPARTAMENTO_DESTINO_ID", DbType.Int32, DPTO_DESTINO_ID);
            db.AddInParameter(cmd, "p_MUNICIPIO_DESTINO_ID", DbType.Int32, MUNICIPIO_DESTINO_ID);
            db.AddInParameter(cmd, "p_SOLICITANTE_ID", DbType.Int32, TITULAR_ID);
            db.AddInParameter(cmd, "p_TIPO_SALVOCONDUCTO_ID", DbType.Int32, TIPO_SALVOCONDUCTO_ID);
            db.AddInParameter(cmd, "p_ESTADO_ID", DbType.Int32, ESTADO_ID);
            db.AddInParameter(cmd, "p_CLASE_RECURSO_ID", DbType.Int32, CLASE_RECURSO_ID);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }
        public DataTable ConsultaSalvoconductosEmitidosFullInfo(int AUT_ID, DateTime FEC_EXP_DESDE, DateTime FEC_EXP_HASTA, int TIPO_FILTRO_VIGENCIA, String NUMERO_SALVOCNDUCTO, int DPTO_ORIGEN_ID, int MUNICIPIO_ORIGEN_ID, int DPTO_DESTINO_ID, int MUNICIPIO_DESTINO_ID, int TITULAR_ID, int TIPO_SALVOCONDUCTO_ID, int ESTADO_ID, int CLASE_RECURSO_ID, bool USUARIO_CONSULTA, bool SN_RESOLUCION_438)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DataSet ds = new DataSet();
            DbCommand cmd;
            if (!USUARIO_CONSULTA)
            {
                if (SN_RESOLUCION_438)
                {
                    cmd = db.GetStoredProcCommand("SSP_REPORTE_SALVOCONDUCTOS_FULL_INFO_TODOS_RES_438");
                    cmd.CommandTimeout = 0;
                }
                else
                {
                    cmd = db.GetStoredProcCommand("SSP_REPORTE_SALVOCONDUCTOS");
                }
            }
            else
            {
                if (SN_RESOLUCION_438)
                {
                    cmd = db.GetStoredProcCommand("SSP_REPORTE_SALVOCONDUCTOS_FULL_INFO_TODOS_RES_438");
                    cmd.CommandTimeout = 0;
                }
                else
                {
                    cmd = db.GetStoredProcCommand("SSP_REPORTE_SALVOCONDUCTOS_FULL_INFO_TODOS");
                    cmd.CommandTimeout = 0;
                }
            }

            db.AddInParameter(cmd, "p_ID_AUT_AMBIENTAL", DbType.String, AUT_ID);
            db.AddInParameter(cmd, "p_FECHA_EXPEDICION_DESDE", DbType.DateTime, FEC_EXP_DESDE);
            db.AddInParameter(cmd, "p_FECHA_EXPEDICION_HASTA", DbType.DateTime, FEC_EXP_HASTA);
            db.AddInParameter(cmd, "P_TIPO_FILTRO_VIGENCIA", DbType.Int32, TIPO_FILTRO_VIGENCIA);
            db.AddInParameter(cmd, "p_NUMERO", DbType.String, NUMERO_SALVOCNDUCTO);
            db.AddInParameter(cmd, "p_DEPARTAMENTO_ORIGEN_ID", DbType.Int32, DPTO_ORIGEN_ID);
            db.AddInParameter(cmd, "p_MUNICIPIO_ORIGEN_ID", DbType.Int32, MUNICIPIO_ORIGEN_ID);
            db.AddInParameter(cmd, "p_DEPARTAMENTO_DESTINO_ID", DbType.Int32, DPTO_DESTINO_ID);
            db.AddInParameter(cmd, "p_MUNICIPIO_DESTINO_ID", DbType.Int32, MUNICIPIO_DESTINO_ID);
            db.AddInParameter(cmd, "p_SOLICITANTE_ID", DbType.Int32, TITULAR_ID);
            db.AddInParameter(cmd, "p_TIPO_SALVOCONDUCTO_ID", DbType.Int32, TIPO_SALVOCONDUCTO_ID);
            db.AddInParameter(cmd, "p_ESTADO_ID", DbType.Int32, ESTADO_ID);
            db.AddInParameter(cmd, "p_CLASE_RECURSO_ID", DbType.Int32, CLASE_RECURSO_ID);
            ds = db.ExecuteDataSet(cmd);
            return ds.Tables[0];
        }





    }
}
