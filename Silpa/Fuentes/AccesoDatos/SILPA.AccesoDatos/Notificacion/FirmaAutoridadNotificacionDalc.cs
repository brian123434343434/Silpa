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
    public class FirmaAutoridadNotificacionDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public FirmaAutoridadNotificacionDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Retotna el listado de firmas que cumplan con las opciones de busqueda
            /// </summary>
            /// <param name="p_intAutoridadId">int con el id de la autoridad. Opcional</param>
            /// <param name="p_strNombreAutorizado">string con el nombre del autorizado.</param>
            /// <param name="p_blnActivo">bool que indica si se obtienen los activos o los inactivos. Opcional</param>
            /// <returns>List con la informacion de las firmas</returns>
            public List<FirmaAutoridadNotificacionEntity> ListarFirmas(int? p_intAutoridadId = null, string p_strNombreAutorizado = "", bool? p_blnActivo = null)
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
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_FIRMA_AUTORIDAD");
                    if (p_intAutoridadId != null && p_intAutoridadId.Value > 0)
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadId.Value);
                    if (!string.IsNullOrEmpty(p_strNombreAutorizado))
                        objDataBase.AddInParameter(objCommand, "@P_NOMBRE_AUTORIZADO", DbType.String, p_strNombreAutorizado);
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

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
                                SubdireccionAutorizado = (objFirmaAutoridadNotificacion["SUBDIRECCION"] != System.DBNull.Value ? objFirmaAutoridadNotificacion["SUBDIRECCION"].ToString().Trim() : ""),
                                GrupoAutorizado = ( objFirmaAutoridadNotificacion["GRUPO"] != System.DBNull.Value ? objFirmaAutoridadNotificacion["GRUPO"].ToString().Trim() : ""),
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
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadNotificacionDalc :: ListarFirmas -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadNotificacionDalc :: ListarFirmas -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
            /// Obtener la información de la firma
            /// </summary>
            /// <param name="p_intFirmaAutoridadID">int con el identificador de la firma</param>
            /// <returns>FirmaAutoridadNotificacionEntity con la informacion de la firma</returns>
            public FirmaAutoridadNotificacionEntity ObtenerFirma(int p_intFirmaAutoridadID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objFirmas = null;
                FirmaAutoridadNotificacionEntity objFirma = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_FIRMA_AUTORIDAD");
                    objDataBase.AddInParameter(objCommand, "@P_ID_FIRMA_AUTORIDAD", DbType.Int32, p_intFirmaAutoridadID);

                    //Crear registro
                    objFirmas = objDataBase.ExecuteDataSet(objCommand);

                    if (objFirmas != null && objFirmas.Tables.Count > 0 && objFirmas.Tables[0].Rows.Count > 0)
                    {
                        //Estado del flujo
                        objFirma = new FirmaAutoridadNotificacionEntity
                        {
                            FirmaAutoridadID = Convert.ToInt32(objFirmas.Tables[0].Rows[0]["ID_FIRMA_AUTORIDAD"]),
                            AutoridadID = Convert.ToInt32(objFirmas.Tables[0].Rows[0]["AUT_ID"]),
                            Autoridad = objFirmas.Tables[0].Rows[0]["AUTORIDAD"].ToString().Trim(),
                            TipoIdentificacionAutorizadoID = Convert.ToInt32(objFirmas.Tables[0].Rows[0]["ID_TIPO_IDENTIFICACION"]),
                            NumeroIdentificaionAutorizado = objFirmas.Tables[0].Rows[0]["NUMERO_IDENTIFICACION"].ToString().Trim(),
                            NombreAutorizado = objFirmas.Tables[0].Rows[0]["NOMBRE_AUTORIZADO"].ToString().Trim(),
                            CargoAutorizado = objFirmas.Tables[0].Rows[0]["CARGO_AUTORIZADO"].ToString().Trim(),
                            SubdireccionAutorizado = (objFirmas.Tables[0].Rows[0]["SUBDIRECCION"] != System.DBNull.Value ? objFirmas.Tables[0].Rows[0]["SUBDIRECCION"].ToString().Trim() : ""),
                            GrupoAutorizado = ( objFirmas.Tables[0].Rows[0]["GRUPO"] != System.DBNull.Value ? objFirmas.Tables[0].Rows[0]["GRUPO"].ToString().Trim() : ""),
                            Firma = (byte[])objFirmas.Tables[0].Rows[0]["FIRMA"],
                            LongitudFirma = Convert.ToInt32(objFirmas.Tables[0].Rows[0]["LONGITUD_FIRMA"]),
                            TipoFirma = objFirmas.Tables[0].Rows[0]["TIPO_FIRMA"].ToString().Trim(),
                            NombreFirma = objFirmas.Tables[0].Rows[0]["NOMBRE_FIRMA"].ToString().Trim(),
                            EmailAutorizado = objFirmas.Tables[0].Rows[0]["EMAIL"].ToString().Trim(),
                            Activo = Convert.ToBoolean(objFirmas.Tables[0].Rows[0]["ACTIVO"])
                        };
                     
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadNotificacionDalc :: ObtenerFirma -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadNotificacionDalc :: ObtenerFirma -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objFirma;
            }


            /// <summary>
            /// Crea un nueva firma
            /// </summary>
            /// <param name="p_objFirma">FirmaAutoridadNotificacionEntity con la información de la firma</param>
            public void CrearFirma(FirmaAutoridadNotificacionEntity p_objFirma)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_INSERT_FIRMA_AUTORIDAD");
                    objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_objFirma.AutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_TIPO_IDENTIFICACION", DbType.Int32, p_objFirma.TipoIdentificacionAutorizadoID);
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_IDENTIFICACION", DbType.String, p_objFirma.NumeroIdentificaionAutorizado.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE_AUTORIZADO", DbType.String, p_objFirma.NombreAutorizado.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_CARGO_AUTORIZADO", DbType.String, p_objFirma.CargoAutorizado.Trim());
                    if (!string.IsNullOrEmpty(p_objFirma.SubdireccionAutorizado))
                        objDataBase.AddInParameter(objCommand, "@P_SUBDIRECCION", DbType.String, p_objFirma.SubdireccionAutorizado.Trim());
                    if (!string.IsNullOrEmpty(p_objFirma.GrupoAutorizado))
                        objDataBase.AddInParameter(objCommand, "@P_GRUPO", DbType.String, p_objFirma.GrupoAutorizado.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_FIRMA", DbType.Binary, p_objFirma.Firma);
                    objDataBase.AddInParameter(objCommand, "@P_LONGITUD_FIRMA", DbType.Int32, p_objFirma.LongitudFirma);
                    objDataBase.AddInParameter(objCommand, "@P_TIPO_FIRMA", DbType.String, p_objFirma.TipoFirma.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE_FIRMA", DbType.String, p_objFirma.NombreFirma.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_EMAIL", DbType.String, p_objFirma.EmailAutorizado.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_objFirma.Activo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadNotificacionDalc :: CrearFirma -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadNotificacionDalc :: CrearFirma -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
            /// Editar la información de la firma
            /// </summary>
            /// <param name="p_objFirma">FirmaAutoridadNotificacionEntity con la información de la firma</param>
            public void EditarFirma(FirmaAutoridadNotificacionEntity p_objFirma)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_UPDATE_FIRMA_AUTORIDAD");
                    objDataBase.AddInParameter(objCommand, "@P_ID_FIRMA_AUTORIDAD", DbType.Int32, p_objFirma.FirmaAutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_objFirma.AutoridadID);
                    objDataBase.AddInParameter(objCommand, "@P_ID_TIPO_IDENTIFICACION", DbType.Int32, p_objFirma.TipoIdentificacionAutorizadoID);
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_IDENTIFICACION", DbType.String, p_objFirma.NumeroIdentificaionAutorizado.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_NOMBRE_AUTORIZADO", DbType.String, p_objFirma.NombreAutorizado.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_CARGO_AUTORIZADO", DbType.String, p_objFirma.CargoAutorizado.Trim());
                    if (!string.IsNullOrEmpty(p_objFirma.SubdireccionAutorizado))
                        objDataBase.AddInParameter(objCommand, "@P_SUBDIRECCION", DbType.String, p_objFirma.SubdireccionAutorizado.Trim());
                    if (!string.IsNullOrEmpty(p_objFirma.GrupoAutorizado))
                        objDataBase.AddInParameter(objCommand, "@P_GRUPO", DbType.String, p_objFirma.GrupoAutorizado.Trim());
                    if (p_objFirma.LongitudFirma > 0)
                    {
                        objDataBase.AddInParameter(objCommand, "@P_FIRMA", DbType.Binary, p_objFirma.Firma);
                        objDataBase.AddInParameter(objCommand, "@P_LONGITUD_FIRMA", DbType.Int32, p_objFirma.LongitudFirma);
                        objDataBase.AddInParameter(objCommand, "@P_TIPO_FIRMA", DbType.String, p_objFirma.TipoFirma.Trim());
                        objDataBase.AddInParameter(objCommand, "@P_NOMBRE_FIRMA", DbType.String, p_objFirma.NombreFirma.Trim());
                    }
                    objDataBase.AddInParameter(objCommand, "@P_EMAIL", DbType.String, p_objFirma.EmailAutorizado.Trim());
                    objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_objFirma.Activo);

                    //Crear registro
                    objDataBase.ExecuteNonQuery(objCommand);

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadNotificacionDalc :: EditarFirma -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "FirmaAutoridadNotificacionDalc :: EditarFirma -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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
