using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Encuestas.Contingencias.Entity;
using SoftManagement.Log;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Dalc
{
    public class NivelEmergenciaContingenciasDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public NivelEmergenciaContingenciasDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar el listado de niveles de emergencia que se encuentran activas
            /// </summary>
            /// <returns>List con la información de los niveles de emergencia</returns>
            public List<NivelEmergenciaContingenciasEntity> ConsultarNivelesEmergenciaContingencias()
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<NivelEmergenciaContingenciasEntity> objLstEtapas = null;
                NivelEmergenciaContingenciasEntity objExpediente = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_LISTA_NIVEL_EMERGENCIA_CONTINGENCIA");

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstEtapas = new List<NivelEmergenciaContingenciasEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objExpedienteInformacion in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objExpediente = new NivelEmergenciaContingenciasEntity
                            {
                                NivelEmergenciaID = Convert.ToInt32(objExpedienteInformacion["ENCNIVELEMERGENCIACONTINGENCIA_ID"]),
                                NivelEmergencia = objExpedienteInformacion["NIVEL_EMERGENCIA"].ToString(),
                                Activo = Convert.ToBoolean(objExpedienteInformacion["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstEtapas.Add(objExpediente);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NivelEmergenciaContingenciasDalc :: ConsultarNivelesEmergenciaContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NivelEmergenciaContingenciasDalc :: ConsultarNivelesEmergenciaContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstEtapas;
            }


            /// <summary>
            /// Consultar la información de un nivel de emergencia especificado
            /// </summary>
            /// <param name="p_intNivelEmergenciaID">int con el identificador del nivel de emergencia</param>
            /// <returns>NivelEmergenciaContingenciasEntity con la información del nivel de emergencia</returns>
            public NivelEmergenciaContingenciasEntity ConsultarNivelEmergenciaContingencias(int p_intNivelEmergenciaID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                NivelEmergenciaContingenciasEntity objExpediente = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_CONSULTAR_NIVEL_EMERGENCIA_CONTINGENCIA");
                    objDataBase.AddInParameter(objCommand, "@P_ENCNIVELEMERGENCIACONTINGENCIA_ID", DbType.String, p_intNivelEmergenciaID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {                        
                        //Crear objeto y cargar datos
                        objExpediente = new NivelEmergenciaContingenciasEntity
                        {
                            NivelEmergenciaID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["ENCNIVELEMERGENCIACONTINGENCIA_ID"]),
                            NivelEmergencia = objInformacion.Tables[0].Rows[0]["NIVEL_EMERGENCIA"].ToString(),
                            Activo = Convert.ToBoolean(objInformacion.Tables[0].Rows[0]["ACTIVO"])
                        };
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NivelEmergenciaContingenciasDalc :: ConsultarNivelEmergenciaContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NivelEmergenciaContingenciasDalc :: ConsultarNivelEmergenciaContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objExpediente;
            }


        #endregion
    }
}
