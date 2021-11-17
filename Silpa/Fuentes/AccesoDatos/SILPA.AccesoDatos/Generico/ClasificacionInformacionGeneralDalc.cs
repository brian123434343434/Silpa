using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using SoftManagement.Log;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace SILPA.AccesoDatos.Generico
{
    public class ClasificacionInformacionGeneralDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ClasificacionInformacionGeneralDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar las clasificaciones de informacion especificadas
            /// </summary>
            /// <param name="p_strDescripcion">string con la descripcion</param>
            /// <param name="p_blnActivo">bool que indica si solo se trae los activos</param>
            /// <returns>List con la información de las clasificaciones</returns>
            public List<ClasificacionInformacionGeneralIdentity> ObtenerClasificaciones(string p_strDescripcion = "", bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<ClasificacionInformacionGeneralIdentity> objLstClases = null;
                ClasificacionInformacionGeneralIdentity objClase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("EIG_LISTA_CLASIFICACION_INFORMACION");
                    if (!string.IsNullOrEmpty(p_strDescripcion))
                        objDataBase.AddInParameter(objCommand, "@P_TAREA_ACTIVIDAD_AUTOMATICA_ID", DbType.String, p_strDescripcion);
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstClases = new List<ClasificacionInformacionGeneralIdentity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objDatos in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objClase = new ClasificacionInformacionGeneralIdentity
                            {
                                ClasificacionInformacionID = Convert.ToInt32(objDatos["CLASIFICACIONINFORMACION_ID"]),
                                Descripcion = (objDatos["DESCRIPCION"] != System.DBNull.Value ? objDatos["DESCRIPCION"].ToString() : ""),
                                Activo = (objDatos["ACTIVO"] != System.DBNull.Value ? Convert.ToBoolean(objDatos["ACTIVO"]) : false)
                            };

                            //Adiciona al listado
                            objLstClases.Add(objClase);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ClasificacionInformacionGeneralDalc :: ObtenerClasificaciones -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ClasificacionInformacionGeneralDalc :: ObtenerClasificaciones -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstClases;
            }


            /// <summary>
            /// Obtiene la informacion de la clasificiacion especificada
            /// </summary>
            /// <param name="p_intClasificacionID">int con la identificacion de la clasificacion</param>
            /// <returns>string con la informacion de la clasificacion</returns>
            public ClasificacionInformacionGeneralIdentity ObtenerClasificacionInformacionAdicional(int p_intClasificacionID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                ClasificacionInformacionGeneralIdentity objClase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("EIG_CONSULTA_CLASIFICACION_INFORMACION");
                    objDataBase.AddInParameter(objCommand, "@P_CLASIFICACIONINFORMACION_ID", DbType.Int32, p_intClasificacionID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear objeto y cargar datos
                        objClase = new ClasificacionInformacionGeneralIdentity
                        {
                            ClasificacionInformacionID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["CLASIFICACIONINFORMACION_ID"]),
                            Descripcion = (objInformacion.Tables[0].Rows[0]["DESCRIPCION"] != System.DBNull.Value ? objInformacion.Tables[0].Rows[0]["DESCRIPCION"].ToString() : ""),
                            Activo = (objInformacion.Tables[0].Rows[0]["ACTIVO"] != System.DBNull.Value ? Convert.ToBoolean(objInformacion.Tables[0].Rows[0]["ACTIVO"]) : false)
                        };
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ClasificacionInformacionGeneralDalc :: ObtenerClasificaciones -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ClasificacionInformacionGeneralDalc :: ObtenerClasificaciones -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objClase;
            }

        #endregion
    }
}
