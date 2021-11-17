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
    public class CargueEnvioNotificacionDalc
    {
        private Configuracion objConfiguracion;


        #region Creadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public CargueEnvioNotificacionDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Retornar el listado de cargues realizados en el rango de fechas especificado
            /// </summary>
            /// <param name="p_objFechaDesde">DateTime con la fecha desde la cual se debe buscar</param>
            /// <param name="p_objFechaHasta">DateTime con la fecha hasta la cual se debe buscar</param>
            /// <param name="p_intUsuarioID">int con el identificador del usuario que se encuentra realizando la consulta</param>
            /// <returns>Listado con la informacion del cargue realizado</returns>
            public List<CargueEnvioNotificacionEntity> ConsultarCarguesEnvios(DateTime p_objFechaDesde, DateTime p_objFechaHasta, int p_intUsuarioID)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objCargues = null;
                List<CargueEnvioNotificacionEntity> objLstCargues = null;
                CargueEnvioNotificacionEntity objCargue = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_CARGUE_ENVIOS");
                    objDataBase.AddInParameter(objCommand, "@P_FECHA_INICIO", DbType.DateTime, p_objFechaDesde);
                    objDataBase.AddInParameter(objCommand, "@P_FECHA_FIN", DbType.DateTime, p_objFechaHasta);
                    objDataBase.AddInParameter(objCommand, "@P_ID_APPLICATION_USER", DbType.Int32, p_intUsuarioID);

                    //Crear registro
                    objCargues = objDataBase.ExecuteDataSet(objCommand);

                    if (objCargues != null && objCargues.Tables.Count > 0 && objCargues.Tables[0].Rows.Count > 0)
                    {
                        objLstCargues = new List<CargueEnvioNotificacionEntity>();

                        foreach (DataRow objInformacionCargue in objCargues.Tables[0].Rows)
                        {
                            //Estado del flujo
                            objCargue = new CargueEnvioNotificacionEntity
                            {
                                CargueID = Convert.ToInt32(objInformacionCargue["ID_CARGUE_ENVIOS"]),
                                AutoridadID = Convert.ToInt32(objInformacionCargue["AUT_ID"]),
                                Autoridad = objInformacionCargue["AUTORIDAD"].ToString().Trim(),
                                UsuarioId = Convert.ToInt32(objInformacionCargue["APPLICATION_USER_ID_CARGUE"]),
                                NombreUsuario = objInformacionCargue["NOMBRE_USUARIO"].ToString().Trim(),
                                FechaCargue = Convert.ToDateTime(objInformacionCargue["FECHA_CARGUE"]),
                                Descripcion = objInformacionCargue["DESCRIPCION"].ToString().Trim(),
                                RegistrosCargados = Convert.ToInt32(objInformacionCargue["REGISTROS_CARGADOS"]),
                                RegistrosRelacionados = Convert.ToInt32(objInformacionCargue["REGISTROS_RELACIONADOS"])
                            };

                            //Adicionar al listado
                            objLstCargues.Add(objCargue);
                        }
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "CargueEnvioNotificacionDalc :: ConsultarCarguesEnvios -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "CargueEnvioNotificacionDalc :: ConsultarCarguesEnvios -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objLstCargues;
            }


            

        #endregion

    }
}
