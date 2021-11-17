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
    public class FlujoNotificacionElectronicaDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public FlujoNotificacionElectronicaDalc()
            {
                objConfiguracion = new Configuracion();
            }

        #endregion

        #region Metodos Publicos


            /// <summary>
            /// Consultar el listado de flujos existentes que cumplan con las condiciones de busqueda
            /// </summary>
            /// <param name="intIdFlujoNot">int con el id del flujo</param>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="strFlujoNotNombre">string con el nombre del flujo</param>
            /// <returns>DataTable con la información de los flujos existentes</returns>
            public DataTable ListarFLujosNotificacionElectronica(int? intIdFlujoNot, int? p_intAutoridadID, string strFlujoNotNombre)
            {                 
                 DbCommand objCommand = null;
                 SqlDatabase objDataBase = null;
                 DataSet objFlujos = null;
                 DataTable objTablaFlujos = null;

                 try
                 {
                     //Cargar conexion
                     objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                     //Cargar comando
                     objCommand = objDataBase.GetStoredProcCommand("BAS_LISTAR_FLUJO_NOTIFICACION_ELECTRONICA");
                     if (intIdFlujoNot != null && intIdFlujoNot.Value > 0)
                         objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, intIdFlujoNot.Value);
                     if (p_intAutoridadID != null && p_intAutoridadID.Value > 0)
                         objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID.Value);
                     if (!string.IsNullOrEmpty(strFlujoNotNombre))
                         objDataBase.AddInParameter(objCommand, "@P_FLUJO_NOT_ELEC_DESC", DbType.Int32, strFlujoNotNombre);

                     //Crear registro
                     objFlujos = objDataBase.ExecuteDataSet(objCommand);

                     //Cargar tabla
                     if(objFlujos != null)
                        objTablaFlujos = objFlujos.Tables[0];

                 }
                 catch (SqlException sqle)
                 {
                     //Escribir error
                     SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ListarFLujosNotificacionElectronica -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                     //Escalar error
                     throw sqle;
                 }
                 catch (Exception exc)
                 {
                     //Escribir error
                     SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ListarFLujosNotificacionElectronica -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                 return objTablaFlujos;
            }


            /// <summary>
            /// Consultar el listado de flujos existentes que se relacionan al usuario especificado
            /// </summary>
            /// <param name="p_lngUsuarioID">long con el identificador del usuario</param>
            public DataTable ConsultaFlujosNotificacionUsuario(long p_lngUsuarioID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objFlujos = null;
                DataTable objTablaFlujos = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("BAS_LISTAR_FLUJO_NOTIFICACION_ELECTRONICA_USUARIO");
                    objDataBase.AddInParameter(objCommand, "@P_ID_USUARIO", DbType.Int64, p_lngUsuarioID);

                    //Crear registro
                    objFlujos = objDataBase.ExecuteDataSet(objCommand);

                    //Cargar tabla
                    if (objFlujos != null)
                        objTablaFlujos = objFlujos.Tables[0];

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ConsultaFlujosNotificacionUsuario -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ConsultaFlujosNotificacionUsuario -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objTablaFlujos;
            }


            /// <summary>
            /// Crear un nuevo flujo
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad</param>
            /// <param name="p_strNombreFlujo">string con el nombre del flujo</param>
            /// <param name="p_blnActivo">bool indicando si el flujo se encuentra activo</param>
            public void CrearFlujoNotificacion(int p_intAutoridadID, string p_strNombreFlujo, bool p_blnActivo)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_INSERT_FLUJO_NOTIFICACION_ELECTRONICA");
                    if (p_intAutoridadID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.String, p_intAutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_FLUJO_NOT_ELEC_DESC", DbType.String, p_strNombreFlujo);
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FlujoNotificacionElectronicaDalc :: CrearFlujoNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FlujoNotificacionElectronicaDalc :: CrearFlujoNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
            /// Editar la información de un flujo
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad</param>
            /// <param name="p_intFlujoID">int con el identificador del flujo</param>
            /// <param name="p_strNombreFlujo">string con el nombre del flujo</param>
            /// <param name="p_blnActivo">bool indicando si el flujo se encuentra activo</param>
            public void EditarFlujoNotificacion(int p_intAutoridadID, int p_intFlujoID, string p_strNombreFlujo, bool p_blnActivo)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_UPDATE_FLUJO_NOTIFICACION_ELECTRONICA");
                    if (p_intAutoridadID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.String, p_intAutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_intFlujoID);
                    objDataBase.AddInParameter(objCommand, "@P_FLUJO_NOT_ELEC_DESC", DbType.String, p_strNombreFlujo);
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FlujoNotificacionElectronicaDalc :: EditarFlujoNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FlujoNotificacionElectronicaDalc :: EditarFlujoNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
            /// Obtener los datos del flujo que debe aplicarse de acuerdo a los parametros
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_blnEsnotificacion">bool que indica si es notificacion</param>
            /// <param name="p_blnEsComunicacion">bool que indica si es comunicación</param>
            /// <param name="p_blnEsCumplase">bool que indica si es cumplase</param>
            /// <param name="p_blnEsNotEdicto">bool que indica si es una notificacion por edicto</param>
            /// <param name="p_blnEsNotEstrado">bool que indica si es una notificacion por estrado</param>
            /// <param name="p_blnAplicaRecurso">bool que indica si aplica recurso</param>
            /// <returns>DataTable con el flujo que se debe aplicar</returns>
            public DataTable ConsultarFlujoPorParametros(int p_intAutoridadID, bool p_blnEsnotificacion, bool p_blnEsComunicacion, bool p_blnEsCumplase, bool p_blnEsNotEdicto, bool p_blnEsNotEstrado, bool p_blnAplicaRecurso, bool p_blnEsnotificacionElectronica)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataTable objInformacionFlujo = null;
                DataSet objDatosConsulta = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTA_FLUJO_POR_PARAMETRO");
                    if (p_intAutoridadID > 0)
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_ES_NOTIFICACION", DbType.Boolean, p_blnEsnotificacion);
                    objDataBase.AddInParameter(objCommand, "@P_ES_COMUNICACION", DbType.Boolean, p_blnEsComunicacion);
                    objDataBase.AddInParameter(objCommand, "@P_ES_CUMPLASE", DbType.Boolean, p_blnEsCumplase);
                    objDataBase.AddInParameter(objCommand, "@P_ES_EDICTO", DbType.Boolean, p_blnEsNotEdicto);
                    objDataBase.AddInParameter(objCommand, "@P_ES_ESTRADO", DbType.Boolean, p_blnEsNotEstrado);
                    objDataBase.AddInParameter(objCommand, "@P_APLICA_RECURSO", DbType.Boolean, p_blnAplicaRecurso);
                    objDataBase.AddInParameter(objCommand, "@P_NOTIFICACION_ELECTRONICA", DbType.Boolean, p_blnEsnotificacionElectronica);
                    objDatosConsulta = objDataBase.ExecuteDataSet(objCommand);

                    //Verificar si se obtuvo datos
                    if (objDatosConsulta != null && objDatosConsulta.Tables.Count > 0 && objDatosConsulta.Tables[0].Rows.Count > 0)
                    {
                        objInformacionFlujo = objDatosConsulta.Tables[0];
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FlujoNotificacionElectronicaDalc :: ConsultarFlujoPorParametros -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FlujoNotificacionElectronicaDalc :: ConsultarFlujoPorParametros -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objInformacionFlujo;
            }


            public List<FlujoNotificacionElectronicaEntity> ConsulaFlujosNotificacionElectronica(long lngActoID, int intTipoNotificacionID, int intAutoridadID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objDatosReporte = null;
                List<FlujoNotificacionElectronicaEntity> LstFlujos = new List<FlujoNotificacionElectronicaEntity>();

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_FLUJOS_NOTIFICACION_POR_TIPO_NOT_AA");
                    objDataBase.AddInParameter(objCommand, "@P_ACTO_ID", DbType.Int64, lngActoID);
                    objDataBase.AddInParameter(objCommand, "@P_TIPO_NOTIFICACION_ID", DbType.Int32, intTipoNotificacionID);
                    objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, intAutoridadID);
                    //Crear registro
                    //Realizar la consulta
                    objDatosReporte = objDataBase.ExecuteDataSet(objCommand);


                    foreach (DataRow item in objDatosReporte.Tables[0].Rows)
                    {
                        LstFlujos.Add(new FlujoNotificacionElectronicaEntity { FlujoNotificacionElectronicaID = Convert.ToInt32(item["ID_FLUJO_NOT_ELEC"]), AutoridadAmbientalID = ( item["AUT_ID"] != System.DBNull.Value ? Convert.ToInt32(item["AUT_ID"]) : -1), FlujoNotificacionElectronica = item["FLUJO_NOT_ELEC_DESC"].ToString(), EstadoInicialFlujoID = Convert.ToInt32(item["ID_ESTADO_FLUJO_NOT_ELEC_INICIAL"]), EstadoInicialFlujo = item["ESTADO"].ToString() });
                    }

                    return LstFlujos;
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FlujoNotificacionElectronicaDalc :: ConsulaFlujosNotificacionElectronica -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FlujoNotificacionElectronicaDalc :: ConsulaFlujosNotificacionElectronica -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
