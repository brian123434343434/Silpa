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
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Usuario
{
    public class EnlaceActivacionUsuarioDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public EnlaceActivacionUsuarioDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Consultar la información de un enlace
            /// </summary>
            /// <param name="p_strLlave">string con la llave</param>
            /// <returns>EnlaceActivacionUsuarioEntity con la información del enlace</returns>
            public EnlaceActivacionUsuarioEntity ConsultarEnlace(string p_strLlave)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objDatosEnlace = null;
                EnlaceActivacionUsuarioEntity objEnlace = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("GEN_CONSULTAR_ENLACE_ACTIVACION_USUARIO");
                    if (!string.IsNullOrEmpty(p_strLlave))
                        objDataBase.AddInParameter(objCommand, "@P_LLAVE", DbType.String, p_strLlave);

                    //Crear registro
                    objDatosEnlace = objDataBase.ExecuteDataSet(objCommand);

                    if (objDatosEnlace != null && objDatosEnlace.Tables.Count > 0 && objDatosEnlace.Tables[0].Rows.Count > 0)
                    {
                        
                        //Estado del flujo
                        objEnlace = new EnlaceActivacionUsuarioEntity
                        {
                            EnlaceID = Convert.ToInt64(objDatosEnlace.Tables[0].Rows[0]["ID_ENLACE_ACTIVACION_USUARIO"]),
                            PersonaID = (objDatosEnlace.Tables[0].Rows[0]["PER_ID"] != System.DBNull.Value ? Convert.ToInt32(objDatosEnlace.Tables[0].Rows[0]["PER_ID"]) : -1),
                            NumeroIdentificacion = (objDatosEnlace.Tables[0].Rows[0]["NUMERO_IDENTIFICACION"] != System.DBNull.Value ? objDatosEnlace.Tables[0].Rows[0]["NUMERO_IDENTIFICACION"].ToString() : ""),
                            Llave = objDatosEnlace.Tables[0].Rows[0]["LLAVE"].ToString(),
                            Activo = (objDatosEnlace.Tables[0].Rows[0]["ACTIVO"] != System.DBNull.Value ? Convert.ToBoolean(objDatosEnlace.Tables[0].Rows[0]["ACTIVO"]) : false),
                            FechaCreacion = Convert.ToDateTime(objDatosEnlace.Tables[0].Rows[0]["FECHA_CREACION"]),
                            FechaVigencia = (objDatosEnlace.Tables[0].Rows[0]["FECHA_VIGENCIA"] != System.DBNull.Value ? Convert.ToDateTime(objDatosEnlace.Tables[0].Rows[0]["FECHA_VIGENCIA"]) : default(DateTime)),
                            FechaUtilizacion = (objDatosEnlace.Tables[0].Rows[0]["FECHA_UTILIZACION_ENLACE"] != System.DBNull.Value ? Convert.ToDateTime(objDatosEnlace.Tables[0].Rows[0]["FECHA_UTILIZACION_ENLACE"]) : default(DateTime))
                        };  
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceActivacionUsuarioDalc :: ConsultarEnlace -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceActivacionUsuarioDalc :: ConsultarEnlace -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }

                return objEnlace;
            }


            /// <summary>
            /// Crear un nuevo enlace
            /// </summary>
            /// <param name="p_objEnlace">EnlaceActivacionUsuarioEntity con la informcaion del enlace</param>
            public void CrearEnlace(EnlaceActivacionUsuarioEntity p_objEnlace)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("GEN_INSERTAR_ENLACE_ACTIVACION_USUARIO");
                    objDataBase.AddInParameter(objCommand, "@P_PER_ID", DbType.Int64, p_objEnlace.PersonaID);
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_IDENTIFICACION", DbType.String, p_objEnlace.NumeroIdentificacion);
                    objDataBase.AddInParameter(objCommand, "@P_LLAVE", DbType.String, p_objEnlace.Llave);
                    objDataBase.AddInParameter(objCommand, "@P_FECHA_VIGENCIA", DbType.DateTime, p_objEnlace.FechaVigencia);
                    if (p_objEnlace.FechaUtilizacion != default(DateTime))
                        objDataBase.AddInParameter(objCommand, "@P_FECHA_UTILIZACION", DbType.DateTime, p_objEnlace.FechaUtilizacion);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceActivacionUsuarioDalc :: CrearEnlace -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceActivacionUsuarioDalc :: CrearEnlace -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }
            }


            /// <summary>
            /// Editar un enlace
            /// </summary>
            /// <param name="p_objEnlace">EnlaceActivacionUsuarioEntity con la informcaion del enlace</param>
            public void EditarEnlace(EnlaceActivacionUsuarioEntity p_objEnlace)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("GEN_ACTUALIZAR_ENLACE_ACTIVACION_USUARIO");
                    objDataBase.AddInParameter(objCommand, "@P_ID_ENLACE_ACTIVACION_USUARIO", DbType.Int64, p_objEnlace.EnlaceID);
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.String, p_objEnlace.Activo);
                    if (p_objEnlace.FechaUtilizacion != default(DateTime))
                        objDataBase.AddInParameter(objCommand, "@P_FECHA_UTILIZACION", DbType.DateTime, p_objEnlace.FechaUtilizacion);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceActivacionUsuarioDalc :: EditarEnlace -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EnlaceActivacionUsuarioDalc :: EditarEnlace -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
                finally
                {
                    if (objCommand != null)
                    {
                        objCommand.Dispose();
                        objCommand = null;
                    }
                    if (objDataBase != null)
                        objDataBase = null;
                }
            }

        #endregion

    }
}
