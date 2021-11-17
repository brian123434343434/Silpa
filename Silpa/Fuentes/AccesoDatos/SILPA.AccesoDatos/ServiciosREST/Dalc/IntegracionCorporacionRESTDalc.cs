using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using SoftManagement.Log;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.ServiciosREST.Entidades;

namespace SILPA.AccesoDatos.ServiciosREST.Dalc
{
    public class IntegracionCorporacionRESTDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public IntegracionCorporacionRESTDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos


            /// <summary>
            /// Obtener la informacion del servicio ID
            /// </summary>
            /// <param name="p_intServicioID">int con el identificador del servicio</param>
            /// <returns>IntegracionCorporacionEntity con la iformacion de integracion</returns>
            public IntegracionCorporacionRESTEntity ObtenerServicio(int p_intServicioID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                IntegracionCorporacionRESTEntity objServicio = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("GEN_CONSULTAR_INTEGRACION_SERVICIO_REST");
                    objDataBase.AddInParameter(objCommand, "@P_INTEGRACION_SERVICIOS_REST_ID", DbType.Int32, p_intServicioID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear objeto y cargar datos
                        objServicio = new IntegracionCorporacionRESTEntity
                        {
                            IntegracionCorporacionRESTID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["INTEGRACION_SERVICIOS_REST_ID"]),
                            AutoridadID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["AUT_ID"]),
                            Autoridad = objInformacion.Tables[0].Rows[0]["AUTORIDAD"].ToString(),
                            Credenciales = new CredencialesEntity { Usuario = objInformacion.Tables[0].Rows[0]["USUARIO_ACCESO"].ToString(), Clave = EnDecript.DesencriptarDesplazamiento(objInformacion.Tables[0].Rows[0]["CLAVE_ACCESO"].ToString()) },
                            URLServicioAutorizacion = (objInformacion.Tables[0].Rows[0]["URL_SERVICIO_AUTENTICACION"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["URL_SERVICIO_AUTENTICACION"].ToString() : ""),
                            URLServicioverificacionToken = (objInformacion.Tables[0].Rows[0]["URL_SERVICIO_VERIFICACION_TOKEN"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["URL_SERVICIO_VERIFICACION_TOKEN"].ToString() : ""),
                            URLServicio = (objInformacion.Tables[0].Rows[0]["URL_SERVICIO"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["URL_SERVICIO"].ToString() : ""),
                            Activo = Convert.ToBoolean(objInformacion.Tables[0].Rows[0]["ACTIVO"]),
                            FechaCreacion = Convert.ToDateTime(objInformacion.Tables[0].Rows[0]["FECHA_CREACION"]),
                            FechaUltimaModificacion = Convert.ToDateTime(objInformacion.Tables[0].Rows[0]["FECHA_ULTIMA_MODIFICACION"]),
                        };
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacionRESTDalc :: ObtenerServicio -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacionRESTDalc :: ObtenerServicio -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objServicio;
            }

        #endregion
    }
}
