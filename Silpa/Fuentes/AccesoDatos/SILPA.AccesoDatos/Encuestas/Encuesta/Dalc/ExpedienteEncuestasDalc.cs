using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;
using SoftManagement.Log;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Dalc
{
    public class ExpedienteEncuestasDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ExpedienteEncuestasDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los expedientes asociados a un solicitante pertenecientes a una entidad especifica
            /// </summary>
            /// <param name="p_intSolicitanteID">int con el identificador del solicitante</param>
            /// <param name="p_intSectorID">int con el identificador del sector. Opcional</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental. Opcional</param>
            /// <returns>List con la información de los expedientes</returns>
            public List<ExpedienteEncuestasEntity> ConsultarExpedientesSolicitanteSectorAutoridad(int p_intSolicitanteID, int p_intSectorID = -1, int p_intAutoridadID = -1)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<ExpedienteEncuestasEntity> objLstExpedientees = null;
                ExpedienteEncuestasEntity objExpediente = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_LISTA_EXPEDIENTE_USUARIO_SECTOR_AUTORIDAD");
                    objDataBase.AddInParameter(objCommand, "@P_ID_APPLICATION_USER", DbType.Int32, p_intSolicitanteID);
                    if (p_intSectorID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_CMSECTOR_ID", DbType.Int32, p_intSectorID);
                    if (p_intAutoridadID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstExpedientees = new List<ExpedienteEncuestasEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objExpedienteInformacion in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objExpediente = new ExpedienteEncuestasEntity
                            {
                                SolicitanteID = Convert.ToInt32(objExpedienteInformacion["ID_APPLICATION_USER"]),
                                ExpedienteCodigo = objExpedienteInformacion["EXP_CODIGO"].ToString(),
                                ExpedienteNombre = (objExpedienteInformacion["EXP_NOMBRE"] != System.DBNull.Value ? objExpedienteInformacion["EXP_NOMBRE"].ToString() : ""),
                                AutoridadID = Convert.ToInt32(objExpedienteInformacion["AUT_ID"])
                            };

                            //Adiciona al listado
                            objLstExpedientees.Add(objExpediente);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ExpedienteEncuestasDalc :: ConsultarExpedientesSolicitanteSectorAutoridad -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ExpedienteEncuestasDalc :: ConsultarExpedientesSolicitanteSectorAutoridad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstExpedientees;
            }


            /// <summary>
            /// Consultar la información del expediente especificado perteneciente a una autoridad ambiental
            /// </summary>
            /// <param name="p_strCodigoExpediente">string con el codigo del expediente</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad</param>
            /// <returns>ExpedienteEncuestasEntity con la información del expediente</returns>
            public ExpedienteEncuestasEntity ConsultarExpedienteAutoridad(string p_strCodigoExpediente, int p_intAutoridadID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                ExpedienteEncuestasEntity objExpediente = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_CONSULTAR_EXPEDIENTE_AUTORIDAD");
                    objDataBase.AddInParameter(objCommand, "@P_EXP_CODIGO", DbType.String, p_strCodigoExpediente);
                    objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {                        
                        //Crear objeto y cargar datos
                        objExpediente = new ExpedienteEncuestasEntity
                        {
                            SolicitanteID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["ID_APPLICATION_USER"]),
                            ExpedienteCodigo = objInformacion.Tables[0].Rows[0]["EXP_CODIGO"].ToString(),
                            ExpedienteNombre = (objInformacion.Tables[0].Rows[0]["EXP_NOMBRE"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["EXP_NOMBRE"].ToString() : ""),
                            AutoridadID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["AUT_ID"])
                        };
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ExpedienteEncuestasDalc :: ConsultarExpedienteAutoridad -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ExpedienteEncuestasDalc :: ConsultarExpedienteAutoridad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objExpediente;
            }


        #endregion
    }
}
