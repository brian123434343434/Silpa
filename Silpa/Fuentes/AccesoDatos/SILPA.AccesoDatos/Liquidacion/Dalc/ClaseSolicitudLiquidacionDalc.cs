using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using SoftManagement.Log;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.AccesoDatos.Liquidacion.Dalc
{
    public class ClaseSolicitudLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ClaseSolicitudLiquidacionDalc()
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
            public List<ClaseSolicitudLiquidacionEntity> ConsultarClaseSolicitudesTipoSolicitud(int p_intTipoSolicitudID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<ClaseSolicitudLiquidacionEntity> objLstSolicitud = null;
                ClaseSolicitudLiquidacionEntity objSolicitud = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_SOLICITUD");
                    objDataBase.AddInParameter(objCommand, "@P_AUTOLIQTIPOSOLICITUD_ID", DbType.Int32, p_intTipoSolicitudID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstSolicitud = new List<ClaseSolicitudLiquidacionEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objInformacionSolicitud in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objSolicitud = new ClaseSolicitudLiquidacionEntity
                            {
                                TipoSolicitud = new TipoSolicitudLiquidacionEntity
                                {
                                    TipoSolicitudID = Convert.ToInt32(objInformacionSolicitud["AUTOLIQTIPOSOLICITUD_ID"]),
                                    TipoSolicitud = objInformacionSolicitud["AUTOLIQTIPOSOLICITUD"].ToString()
                                },
                                ClaseSolicitudID = Convert.ToInt32(objInformacionSolicitud["AUTOLIQSOLICITUD_ID"]),
                                ClaseSolicitud = objInformacionSolicitud["AUTOLIQSOLICITUD"].ToString()
                            };

                            //Adiciona al listado
                            objLstSolicitud.Add(objSolicitud);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudLiquidacionDalc :: ConsultarClaseSolicitudesTipoSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SolicitudLiquidacionDalc :: ConsultarClaseSolicitudesTipoSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstSolicitud;
            }

        #endregion

    }
}
