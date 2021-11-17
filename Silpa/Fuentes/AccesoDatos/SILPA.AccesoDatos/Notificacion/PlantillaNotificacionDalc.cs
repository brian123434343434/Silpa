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

namespace SILPA.AccesoDatos.Notificacion
{
    public class PlantillaNotificacionDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public PlantillaNotificacionDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Retorna el listado de Plantillas existentes para asociar a las plantillas
            /// </summary>
            /// <param name="p_strNombre">string con el nombre del Plantilla. Opcional</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad. Opcional</param>
            /// <param name="p_blnActivo">bool indica si se lista solo los Plantillas activos. Opcional</param>
            /// <returns>List con la información de los Plantillas</returns>
            public List<PlantillaNotificacionEntity> ListarPlantillas(string p_strNombre = "", int p_intAutoridadID = 0, bool? p_blnActivo = null)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objPlantillas = null;
                List<PlantillaNotificacionEntity> objLstPlantillas = null;
                PlantillaNotificacionEntity objPlantilla = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_PLANTILLA");

                    if (p_intAutoridadID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);
                    if (!string.IsNullOrEmpty(p_strNombre))
                        objDataBase.AddInParameter(objCommand, "@P_NOMBRE", DbType.String, p_strNombre);
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Crear registro
                    objPlantillas = objDataBase.ExecuteDataSet(objCommand);

                    if (objPlantillas != null && objPlantillas.Tables.Count > 0 &&  objPlantillas.Tables[0].Rows.Count > 0)
                    {
                        objLstPlantillas = new List<PlantillaNotificacionEntity>();

                        foreach (DataRow objPlantillaNotificacion in objPlantillas.Tables[0].Rows)
                        {
                            //Estado del flujo
                            objPlantilla = new PlantillaNotificacionEntity
                            {
                                PlantillaID = Convert.ToInt32(objPlantillaNotificacion["ID_PLANTILLA"]),
                                AutoridadID = (objPlantillaNotificacion["AUT_ID"] != System.DBNull.Value ? Convert.ToInt32(objPlantillaNotificacion["AUT_ID"]) : -1),
                                Autoridad = objPlantillaNotificacion["AUTORIDAD"].ToString(),
                                Nombre = objPlantillaNotificacion["NOMBRE"].ToString().Trim(),
                                Formato = new FormatoPlantillaNotificacionEntity{FormatoID = Convert.ToInt32(objPlantillaNotificacion["ID_FORMATO"]), Nombre = objPlantillaNotificacion["NOMBRE_FORMATO"].ToString().Trim()},
                                Encabezado = (objPlantillaNotificacion["ENCABEZADO"] != System.DBNull.Value ? objPlantillaNotificacion["ENCABEZADO"].ToString().Trim() : ""),
                                Cuerpo = (objPlantillaNotificacion["CUERPO"] != System.DBNull.Value ? objPlantillaNotificacion["CUERPO"].ToString().Trim() : ""),
                                PieFirma = (objPlantillaNotificacion["PIE_FIRMA"] != System.DBNull.Value ? objPlantillaNotificacion["PIE_FIRMA"].ToString().Trim() : ""),
                                Pie = (objPlantillaNotificacion["PIE"] != System.DBNull.Value ? objPlantillaNotificacion["PIE"].ToString().Trim() : ""),
                                Activo = Convert.ToBoolean(objPlantillaNotificacion["ACTIVO"])
                            };

                            //Adicionar al listado
                            objLstPlantillas.Add(objPlantilla);
                        }
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ListarPlantillas -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ListarPlantillas -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objLstPlantillas;
            }


            /// <summary>
            /// Obtener la información de una plantilla
            /// </summary>
            /// <param name="p_intPlantillaID">int con el identificador de la plantilla</param>
            /// <returns>PlantillaNotificacionEntity con la información de la plantilla</returns>
            public PlantillaNotificacionEntity ObtenerPlantilla(int p_intPlantillaID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objPlantillas = null;
                PlantillaNotificacionEntity objPlantilla = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_PLANTILLA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_PLANTILLA", DbType.Int32, p_intPlantillaID);

                    //Crear registro
                    objPlantillas = objDataBase.ExecuteDataSet(objCommand);

                    if (objPlantillas != null && objPlantillas.Tables.Count > 0 && objPlantillas.Tables[0].Rows.Count > 0)
                    {
                        //Estado del flujo
                        objPlantilla = new PlantillaNotificacionEntity
                        {
                            PlantillaID = Convert.ToInt32(objPlantillas.Tables[0].Rows[0]["ID_PLANTILLA"]),
                            AutoridadID = (objPlantillas.Tables[0].Rows[0]["AUT_ID"] != System.DBNull.Value ? Convert.ToInt32(objPlantillas.Tables[0].Rows[0]["AUT_ID"]) : -1),
                            Autoridad = objPlantillas.Tables[0].Rows[0]["AUTORIDAD"].ToString(),
                            Nombre = objPlantillas.Tables[0].Rows[0]["NOMBRE"].ToString().Trim(),
                            Formato = new FormatoPlantillaNotificacionEntity { FormatoID = Convert.ToInt32(objPlantillas.Tables[0].Rows[0]["ID_FORMATO"]), Nombre = objPlantillas.Tables[0].Rows[0]["NOMBRE_FORMATO"].ToString().Trim(), Formato = objPlantillas.Tables[0].Rows[0]["FORMATO"].ToString().Trim() },
                            Encabezado = (objPlantillas.Tables[0].Rows[0]["ENCABEZADO"] != System.DBNull.Value ? objPlantillas.Tables[0].Rows[0]["ENCABEZADO"].ToString().Trim() : ""),
                            Cuerpo = (objPlantillas.Tables[0].Rows[0]["CUERPO"] != System.DBNull.Value ? objPlantillas.Tables[0].Rows[0]["CUERPO"].ToString().Trim() : ""),
                            PieFirma = (objPlantillas.Tables[0].Rows[0]["PIE_FIRMA"] != System.DBNull.Value ? objPlantillas.Tables[0].Rows[0]["PIE_FIRMA"].ToString().Trim() : ""),
                            Pie = (objPlantillas.Tables[0].Rows[0]["PIE"] != System.DBNull.Value ? objPlantillas.Tables[0].Rows[0]["PIE"].ToString().Trim() : ""),
                            Activo = Convert.ToBoolean(objPlantillas.Tables[0].Rows[0]["ACTIVO"])
                        };
                     
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ObtenerPlantilla -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ObtenerPlantilla -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objPlantilla;
            }


            /// <summary>
            /// Crea un nueva plantilla
            /// </summary>
            /// <param name="p_objPlantilla">PlantillaNotificacionEntity con la información de la plantilla</param>
            public int  CrearPlantilla(PlantillaNotificacionEntity p_objPlantilla)
            {                
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                int intPlantillaID = 0;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_INSERT_PLANTILLA");
                    if (p_objPlantilla.AutoridadID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_objPlantilla.AutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE", DbType.String, p_objPlantilla.Nombre.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_ID_FORMATO", DbType.Int32, p_objPlantilla.Formato.FormatoID);
                    if (!string.IsNullOrEmpty(p_objPlantilla.Encabezado))
                        objDataBase.AddInParameter(objCommand, "@P_ENCABEZADO", DbType.String, p_objPlantilla.Encabezado.Trim());
                    if (!string.IsNullOrEmpty(p_objPlantilla.Cuerpo))
                        objDataBase.AddInParameter(objCommand, "@P_CUERPO", DbType.String, p_objPlantilla.Cuerpo.Trim());
                    if (!string.IsNullOrEmpty(p_objPlantilla.PieFirma))
                        objDataBase.AddInParameter(objCommand, "@P_PIE_FIRMA", DbType.String, p_objPlantilla.PieFirma.Trim());
                    if (!string.IsNullOrEmpty(p_objPlantilla.Pie))
                        objDataBase.AddInParameter(objCommand, "@P_PIE", DbType.String, p_objPlantilla.Pie.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_objPlantilla.Activo);

                    //Ejecuta sentencia
                    using (IDataReader reader = objDataBase.ExecuteReader(objCommand))
                    {
                        //Cargar id del certificado
                        if (reader.Read())
                        {
                            intPlantillaID = Convert.ToInt32(reader["ID_PLANTILLA"]);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: CrearPlantilla -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: CrearPlantilla -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return intPlantillaID;
            }


            /// <summary>
            /// Editar la información del Plantilla
            /// </summary>
            /// <param name="p_objPlantilla">PlantillaNotificacionEntity con la información del Plantilla</param>
            public void EditarPlantilla(PlantillaNotificacionEntity p_objPlantilla)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_UPDATE_PLANTILLA");
                    if(p_objPlantilla.AutoridadID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_objPlantilla.AutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_PLANTILLA", DbType.Int32, p_objPlantilla.PlantillaID);
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE", DbType.String, p_objPlantilla.Nombre.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_ID_FORMATO", DbType.Int32, p_objPlantilla.Formato.FormatoID);
                    if (!string.IsNullOrEmpty(p_objPlantilla.Encabezado))
                        objDataBase.AddInParameter(objCommand, "@P_ENCABEZADO", DbType.String, p_objPlantilla.Encabezado.Trim());
                    if (!string.IsNullOrEmpty(p_objPlantilla.Cuerpo))
                        objDataBase.AddInParameter(objCommand, "@P_CUERPO", DbType.String, p_objPlantilla.Cuerpo.Trim());
                    if (!string.IsNullOrEmpty(p_objPlantilla.PieFirma))
                        objDataBase.AddInParameter(objCommand, "@P_PIE_FIRMA", DbType.String, p_objPlantilla.PieFirma.Trim());
                    if (!string.IsNullOrEmpty(p_objPlantilla.Pie))
                        objDataBase.AddInParameter(objCommand, "@P_PIE", DbType.String, p_objPlantilla.Pie.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_objPlantilla.Activo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: EditarPlantilla -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: EditarPlantilla -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
            /// Obtener el listado de marcas configuradas
            /// </summary>
            /// <returns>List con la información de las marcas</returns>
            public List<MarcaPlantillaNotificacionEntity> ObtenerListadoMarcas()
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objMarcas = null;
                List<MarcaPlantillaNotificacionEntity> objLstMarcas = null;
                MarcaPlantillaNotificacionEntity objMarca = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Realizar la consulta
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_MARCA_PLANTILLA");
                    objMarcas = objDataBase.ExecuteDataSet(objCommand);

                    if (objMarcas != null && objMarcas.Tables.Count > 0 && objMarcas.Tables[0].Rows.Count > 0)
                    {
                        objLstMarcas = new List<MarcaPlantillaNotificacionEntity>();

                        foreach (DataRow objMarcaPlantilla in objMarcas.Tables[0].Rows)
                        {
                            //Estado del flujo
                            objMarca = new MarcaPlantillaNotificacionEntity
                            {
                                MarcaID = Convert.ToInt32(objMarcaPlantilla["ID_MARCA_PLANTILLA"]),
                                Marca = objMarcaPlantilla["MARCA"].ToString().Trim(),
                                Tabla = objMarcaPlantilla["TABLA"].ToString().Trim(),
                                Campo = objMarcaPlantilla["CAMPO"].ToString().Trim(),
                                Activo = Convert.ToBoolean(objMarcaPlantilla["ACTIVO"])
                            };

                            //Adicionar al listado
                            objLstMarcas.Add(objMarca);
                        }
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ObtenerListadoMarcas -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ObtenerListadoMarcas -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objLstMarcas;
            }

        #endregion

    }
}
