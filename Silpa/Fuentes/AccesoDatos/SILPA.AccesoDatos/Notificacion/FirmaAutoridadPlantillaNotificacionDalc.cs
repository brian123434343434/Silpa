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
    public class FirmaAutoridadPlantillaNotificacionDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public FirmaAutoridadPlantillaNotificacionDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Retorna el listado de firmas de una plantilla
            /// </summary>
            /// <param name="p_intPlantillaID">int con el id de la plantilla</param>
            /// <returns>List con la informacion de las firmas</returns>
            public List<FirmaAutoridadNotificacionEntity> ListarFirmasPlantilla(int p_intPlantillaID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objFirmas = null;
                List<FirmaAutoridadNotificacionEntity> objLstFirmas = null;
                FirmaAutoridadNotificacionEntity objFirma = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_FIRMA_AUTORIDAD_PLANTILLA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_PLANTILLA", DbType.Int32, p_intPlantillaID);

                    //Crear registro
                    objFirmas = objDataBase.ExecuteDataSet(objCommand);

                    if (objFirmas != null && objFirmas.Tables.Count > 0 &&  objFirmas.Tables[0].Rows.Count > 0)
                    {
                        objLstFirmas = new List<FirmaAutoridadNotificacionEntity>();

                        foreach (DataRow objFirmaAutoridadNotificacion in objFirmas.Tables[0].Rows)
                        {
                            //Estado del flujo
                            objFirma = new FirmaAutoridadNotificacionEntity
                            {
                                FirmaAutoridadID = Convert.ToInt32(objFirmaAutoridadNotificacion["ID_FIRMA_AUTORIDAD"]),
                                AutoridadID = Convert.ToInt32(objFirmaAutoridadNotificacion["AUT_ID"]),
                                Autoridad = objFirmaAutoridadNotificacion["AUTORIDAD"].ToString().Trim(),
                                NombreAutorizado = objFirmaAutoridadNotificacion["NOMBRE_AUTORIZADO"].ToString().Trim(),
                                CargoAutorizado = objFirmaAutoridadNotificacion["CARGO_AUTORIZADO"].ToString().Trim(),
                                Firma = (byte[])objFirmaAutoridadNotificacion["FIRMA"],
                                LongitudFirma = Convert.ToInt32(objFirmaAutoridadNotificacion["LONGITUD_FIRMA"]),
                                TipoFirma = objFirmaAutoridadNotificacion["TIPO_FIRMA"].ToString().Trim(),
                                NombreFirma = objFirmaAutoridadNotificacion["NOMBRE_FIRMA"].ToString().Trim(),
                                EmailAutorizado = objFirmaAutoridadNotificacion["EMAIL"].ToString().Trim(),
                                Activo = Convert.ToBoolean(objFirmaAutoridadNotificacion["ACTIVO"])
                            };

                            //Adicionar al listado
                            objLstFirmas.Add(objFirma);
                        }
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadPlantillaNotificacionDalc :: ListarFirmasPlantilla -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadPlantillaNotificacionDalc :: ListarFirmasPlantilla -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objLstFirmas;
            }


            /// <summary>
            /// Crear una nueva firma a una plantilla
            /// </summary>
            /// <param name="p_intFirmaAutoridadID">int con el identificador de la firma</param>
            /// <param name="p_intPlantillaID">int con el identificador de la plantilla</param>
            public void CrearFirmaAutoridadPlantilla(int p_intFirmaAutoridadID, int p_intPlantillaID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_INSERT_FIRMA_AUTORIDAD_PLANTILLA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_FIRMA_AUTORIDAD", DbType.Int32, p_intFirmaAutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_PLANTILLA", DbType.Int32, p_intPlantillaID);                    

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadPlantillaNotificacionDalc :: CrearFirmaAutoridadPlantilla -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadPlantillaNotificacionDalc :: CrearFirmaAutoridadPlantilla -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
            /// Eliminar las firmas de una plantilla
            /// </summary>
            /// <param name="p_intPlantillaID">int con el identificador de la plantilla</param>
            public void EliminarFirmasPlantilla(int p_intPlantillaID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_DELETE_TODOS_FIRMA_AUTORIDAD_PLANTILLA");
                    objDataBase.AddInParameter(objCommand, "@P_ID_PLANTILLA", DbType.Int32, p_intPlantillaID);                    

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadPlantillaNotificacionDalc :: EliminarFirmasPlantilla -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadPlantillaNotificacionDalc :: EliminarFirmasPlantilla -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
