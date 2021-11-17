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
    public class EtapaProyectoContingenciasDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public EtapaProyectoContingenciasDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar el listado de etapas del proyecto que se encuentran activas
            /// </summary>
            /// <returns>List con la información de las etapas del proyecto que se encuentran activas</returns>
            public List<EtapaProyectoContingenciasEntity> ConsultarEtapasProyectoContingencias()
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<EtapaProyectoContingenciasEntity> objLstEtapas = null;
                EtapaProyectoContingenciasEntity objExpediente = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_LISTA_ETAPA_PROYECTO_CONTINGENCIA");

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstEtapas = new List<EtapaProyectoContingenciasEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objExpedienteInformacion in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objExpediente = new EtapaProyectoContingenciasEntity
                            {
                                EtapaProyectoID = Convert.ToInt32(objExpedienteInformacion["ENCETAPA_PROYECTO_CONTINGENCIA_ID"]),
                                EtapaProyecto = objExpedienteInformacion["ETAPA_PROYECTO"].ToString(),
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
                    SMLog.Escribir(Severidad.Critico, "EtapaProyectoContingenciasDalc :: ConsultarEtapasProyectoContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EtapaProyectoContingenciasDalc :: ConsultarEtapasProyectoContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstEtapas;
            }


            /// <summary>
            /// Consultar la información de una etapa del proyecto especificad
            /// </summary>
            /// <param name="p_intEtapaID">int con el identificador de la etapa</param>
            /// <returns>EtapaProyectoContingenciasEntity con la información de la etapa</returns>
            public EtapaProyectoContingenciasEntity ConsultarEtapaProyectoContingencias(int p_intEtapaID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                EtapaProyectoContingenciasEntity objExpediente = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_CONSULTAR_ETAPA_PROYECTO_CONTINGENCIA");
                    objDataBase.AddInParameter(objCommand, "@P_ENCETAPA_PROYECTO_CONTINGENCIA_ID", DbType.String, p_intEtapaID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {                        
                        //Crear objeto y cargar datos
                        objExpediente = new EtapaProyectoContingenciasEntity
                        {
                            EtapaProyectoID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["ENCETAPA_PROYECTO_CONTINGENCIA_ID"]),
                            EtapaProyecto = objInformacion.Tables[0].Rows[0]["ETAPA_PROYECTO"].ToString(),
                            Activo = Convert.ToBoolean(objInformacion.Tables[0].Rows[0]["ACTIVO"])
                        };
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EtapaProyectoContingenciasDalc :: ConsultarEtapaProyectoContingencias -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EtapaProyectoContingenciasDalc :: ConsultarEtapaProyectoContingencias -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objExpediente;
            }


        #endregion
    }
}
