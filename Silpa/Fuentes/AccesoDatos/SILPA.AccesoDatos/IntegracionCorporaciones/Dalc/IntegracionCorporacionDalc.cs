using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using SoftManagement.Log;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.IntegracionCorporaciones.Entidades;

namespace SILPA.AccesoDatos.IntegracionCorporaciones.Dalc
{
    public class IntegracionCorporacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public IntegracionCorporacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar las clases de solicitudes pertenecientes a un tipo de liquidación
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud</param>
            /// <returns>List con la información de las clases solicitudes</returns>
            public List<IntegracionCorporacionEntity> ObtenerListaAutoridadesIntegradas()
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<IntegracionCorporacionEntity> objLstAutoridades = null;
                IntegracionCorporacionEntity objAutoridad = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("GEN_LISTA_INTEGRACION_AUTORIDAD");

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstAutoridades = new List<IntegracionCorporacionEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objIntegracion in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objAutoridad = new IntegracionCorporacionEntity
                            {
                                IntegracionCorporacionID = Convert.ToInt32(objIntegracion["INTEGACION_AUTORIDAD_ID"]),
                                AutoridadID = Convert.ToInt32(objIntegracion["AUT_ID"]),
                                Autoridad = objIntegracion["AUTORIDAD"].ToString()
                            };

                            //Adiciona al listado
                            objLstAutoridades.Add(objAutoridad);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacionDalc :: ObtenerListaAutoridadesIntegradas -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacionDalc :: ObtenerListaAutoridadesIntegradas -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstAutoridades;
            }


            /// <summary>
            /// Obtener la informacion de integracion de una autoridad
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad</param>
            /// <returns>IntegracionCorporacionEntity con la iformacion de integracion</returns>
            public IntegracionCorporacionEntity ObtenerAutoridadIntegrada(int p_intAutoridadID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                IntegracionCorporacionEntity objAutoridad = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("GEN_CONSULTAR_INTEGRACION_AUTORIDAD");
                    objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear objeto y cargar datos
                        objAutoridad = new IntegracionCorporacionEntity
                        {
                            IntegracionCorporacionID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["INTEGACION_AUTORIDAD_ID"]),
                            AutoridadID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["AUT_ID"]),
                            Autoridad = objInformacion.Tables[0].Rows[0]["AUTORIDAD"].ToString(),
                            Credenciales = new CredencialesEntity { Usuario = objInformacion.Tables[0].Rows[0]["USUARIO_ACCESO"].ToString(), Clave = objInformacion.Tables[0].Rows[0]["CLAVE_ACCESO"].ToString() },
                            ServicioAutorizacion = (objInformacion.Tables[0].Rows[0]["SERVICIO_AUTENTICACION"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["SERVICIO_AUTENTICACION"].ToString() : ""),
                            ServicioverificacionToken = (objInformacion.Tables[0].Rows[0]["SERVICIO_VERIFICACION_TOKEN"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["SERVICIO_VERIFICACION_TOKEN"].ToString() : ""),
                            ServicioMenu = (objInformacion.Tables[0].Rows[0]["SERVICIO_MENU"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["SERVICIO_MENU"].ToString() : ""),
                            ServicioCrearSesion = (objInformacion.Tables[0].Rows[0]["SERVICIO_CREAR_SESION"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["SERVICIO_CREAR_SESION"].ToString() : ""),
                            ServicioCerrarSesion = (objInformacion.Tables[0].Rows[0]["SERVICIO_CERRAR_SESION"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["SERVICIO_CERRAR_SESION"].ToString() : ""),
                            Activo = Convert.ToBoolean(objInformacion.Tables[0].Rows[0]["ACTIVO"]),
                            FechaCreacion = Convert.ToDateTime(objInformacion.Tables[0].Rows[0]["FECHA_CREACION"]),
                            FechaUltimaModificacion = Convert.ToDateTime(objInformacion.Tables[0].Rows[0]["FECHA_ULTIMA_MODIFICACION"]),
                        };
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacionDalc :: ObtenerAutoridadIntegrada -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacionDalc :: ObtenerAutoridadIntegrada -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objAutoridad;
            }

        #endregion
    }
}
