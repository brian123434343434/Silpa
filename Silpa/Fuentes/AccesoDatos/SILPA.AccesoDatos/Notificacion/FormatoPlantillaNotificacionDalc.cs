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
    public class FormatoPlantillaNotificacionDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public FormatoPlantillaNotificacionDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Retorna el listado de formatos existentes para asociar a las plantillas
            /// </summary>
            /// <param name="p_strNombre">string con el nombre del formato. Opcional</param>
            /// <param name="p_blnActivo">bool indica si se lista solo los formatos activos. Opcional</param>
            /// <returns>List con la información de los formatos</returns>
            public List<FormatoPlantillaNotificacionEntity> ListarFormatos(string p_strNombre = "", bool? p_blnActivo = null)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objFormatos = null;
                List<FormatoPlantillaNotificacionEntity> objLstFormatos = null;
                FormatoPlantillaNotificacionEntity objFormato = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_FORMATO_PLANTILLA");
                    if (!string.IsNullOrEmpty(p_strNombre))
                        objDataBase.AddInParameter(objCommand, "@P_NOMBRE", DbType.String, p_strNombre);
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Crear registro
                    objFormatos = objDataBase.ExecuteDataSet(objCommand);

                    if (objFormatos != null && objFormatos.Tables.Count > 0 &&  objFormatos.Tables[0].Rows.Count > 0)
                    {
                        objLstFormatos = new List<FormatoPlantillaNotificacionEntity>();

                        foreach (DataRow objFormatoPlantilla in objFormatos.Tables[0].Rows)
                        {
                            //Estado del flujo
                            objFormato = new FormatoPlantillaNotificacionEntity
                            {
                                FormatoID = Convert.ToInt32(objFormatoPlantilla["ID_FORMATO"]),
                                Nombre = objFormatoPlantilla["NOMBRE"].ToString().Trim(),
                                Formato = objFormatoPlantilla["FORMATO"].ToString().Trim(),
                                Activo = Convert.ToBoolean(objFormatoPlantilla["ACTIVO"])
                            };

                            //Adicionar al listado
                            objLstFormatos.Add(objFormato);
                        }
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormatoPlantillaNotificacionDalc :: ListarFormatos -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormatoPlantillaNotificacionDalc :: ListarFormatos -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objLstFormatos;
            }


            /// <summary>
            /// Crea un nuevo formato
            /// </summary>
            /// <param name="p_objFormato">FormatoPlantillaNotificacionEntity con la información del formato</param>
            public void CrearFormato(FormatoPlantillaNotificacionEntity p_objFormato)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_INSERT_FORMATO_PLANTILLA");
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE", DbType.String, p_objFormato.Nombre.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_FORMATO", DbType.String, p_objFormato.Formato.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_objFormato.Activo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormatoPlantillaNotificacionDalc :: CrearFormato -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormatoPlantillaNotificacionDalc :: CrearFormato -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
            /// Editar la información del formato
            /// </summary>
            /// <param name="p_objFormato">FormatoPlantillaNotificacionEntity con la información del formato</param>
            public void EditarFormato(FormatoPlantillaNotificacionEntity p_objFormato)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_UPDATE_FORMATO_PLANTILLA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_FORMATO", DbType.Int32, p_objFormato.FormatoID);
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE", DbType.String, p_objFormato.Nombre.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_FORMATO", DbType.String, p_objFormato.Formato.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_objFormato.Activo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormatoPlantillaNotificacionDalc :: EditarFormato -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FormatoPlantillaNotificacionDalc :: EditarFormato -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
