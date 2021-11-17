using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Expedientes
{
    public class ExpedienteAutExtDaLC
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;


        public ExpedienteAutExtDaLC()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Lista las publicaciones existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="IdAA">Identificador de la Autoridad Ambiental</param>
        /// <param name="IdentificacionSol">Identificador del Identificacion SOlictante Expediente</param>
            public List<ExpedientesExternosEntity> ListarExpedientesAutExternas(Int32 IdAA, string IdentificacionSol)
        {
            try
            {
                List<ExpedientesExternosEntity> ListaRespuesta = new List<ExpedientesExternosEntity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IdAA, IdentificacionSol };
                DbCommand cmd = db.GetStoredProcCommand("AAE_LISTA_DET_EXPEDIENTES_AUTORIDADES_EXTERNAS", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
               // return (dsResultado);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsResultado.Tables[0].Rows)
                    {
                        ExpedientesExternosEntity _res = new ExpedientesExternosEntity();
                        _res.EXT_EXPEDIENTE_AA = r["DET_EXT_EXPEDIENTE_AA"].ToString();
                        _res.ACTIVO = Convert.ToBoolean(r["DET_ACTIVO"]);
                        _res.EXP_IDENTIFICACION_SOLICITANTE_AA = r["DET_EXP_IDENTIFICACION_SOL"].ToString();
                        _res.EXP_NOMBRE_SOLICITANTE_AA = r["DET_EXP_NOMBRE_SOL"].ToString();
                        _res.EXP_NOMBREE_AA = r["EXT_NOMBRE_AUT_EXT"].ToString();
                        _res.EXP_NUMERO_SILPA = r["NUMERO_SILPA"].ToString();
                        _res.Tipo_Tramite_ID = r["TRAMITE_ID"].ToString();
                        ListaRespuesta.Add(_res);
                    }
                }

                return ListaRespuesta;

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


       
            /// <summary>
            /// Inserta una autoridad Externa en la base de datos
            /// </summary>
            /// <param name="objIdentity">Identidad de la publicacion</param>
            public void InsertarExpedientesAutExternas(ref ExpedienteAutExtEntity objIdentity)
            {
                 SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.EXP_ID_AUT_EXT, 
                           objIdentity.EXT_NOMBRE_AUT_EXT, 
                           objIdentity.EXP_FECHA_REGISTRO,
                           objIdentity.EXT_ID
                        };

                DbCommand cmd = db.GetStoredProcCommand("AAE_ADICIONAR_EXPEDIENTES_AUTORIDADES_EXTERNAS", parametros);
                db.ExecuteNonQuery(cmd);
                string prueba = cmd.Parameters["@EXT_ID"].Value.ToString();
                objIdentity.EXT_ID = prueba;       
            }


            /// <summary>
            /// Inserta una autoridad Externa en la base de datos
            /// </summary>
            /// <param name="objIdentity">Identidad de la publicacion</param>
            public void InsertarExpedientesExternos(ExpedientesExternosEntity objIdentity)
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                if (objIdentity.Tipo_Tramite_ID == "" || objIdentity.Tipo_Tramite_ID == null)
                {
                    objIdentity.Tipo_Tramite_ID = "0";
                }

                object[] parametros = new object[]
                       {
                           objIdentity.EXP_ID, 
                           objIdentity.EXT_EXPEDIENTE_AA, 
                           objIdentity.EXP_IDENTIFICACION_SOLICITANTE_AA,
                           objIdentity.EXP_NOMBRE_SOLICITANTE_AA,
                           objIdentity.EXP_NUMERO_SILPA,
                           Convert.ToInt32(objIdentity.Tipo_Tramite_ID),
                           objIdentity.ACTIVO
                        };

                DbCommand cmd = db.GetStoredProcCommand("AAE_ADICIONAR_DET_EXPEDIENTES_AUTORIDADES_EXTERNAS", parametros);
                db.ExecuteNonQuery(cmd);
          }


            /// <summary>
            /// guarda la relación entre dos procesos padre hijo (los numero silpa)
            /// ejemplo el caso de información adicional encualquier momento
            /// </summary>
            /// <param name="padre">string: número silpa del trámite desde donde se origina el segundo proceso</param>
            /// <param name="hijo">string: número silpa del trámite hijo originado</param>
            public void RelacionarExpedienteSilpaPadreHijo(string codigo_Expediente, string padre, string hijo, int tipoTramite)
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { codigo_Expediente, padre, hijo, tipoTramite };
                    DbCommand cmd = db.GetStoredProcCommand("GEN_RELACIONAR_EXP_PADRE_HIJO", parametros);
                    int i = db.ExecuteNonQuery(cmd);
                }
                catch (SqlException sqle)
                {
                    SMLog.Escribir(Severidad.Critico, sqle.Message.ToString());
                }
            }


    }
}
