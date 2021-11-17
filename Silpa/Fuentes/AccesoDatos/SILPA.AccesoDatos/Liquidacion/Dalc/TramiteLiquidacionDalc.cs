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
    public class TramiteLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public TramiteLiquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los tramites pertenecientes a una solicitud
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud</param>
            /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
            /// <returns>List con la información de los tramites</returns>
            public List<TramiteLiquidacionEntity> ConsultarTramites(int p_intTipoSolicitudID, int p_intSolicitudID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<TramiteLiquidacionEntity> objLstTramites = null;
                TramiteLiquidacionEntity objTramite = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_TRAMITES");
                    objDataBase.AddInParameter(objCommand, "@P_AUTOLIQTIPOSOLICITUD_ID", DbType.Int32, p_intTipoSolicitudID);
                    objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSOLICITUD_ID", DbType.Int32, p_intSolicitudID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstTramites = new List<TramiteLiquidacionEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objInformacionSolicitud in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objTramite = new TramiteLiquidacionEntity
                            {
                                ClaseSolicitud = new ClaseSolicitudLiquidacionEntity
                                {
                                    TipoSolicitud = new TipoSolicitudLiquidacionEntity
                                    {
                                        TipoSolicitudID = Convert.ToInt32(objInformacionSolicitud["AUTOLIQTIPOSOLICITUD_ID"]),
                                        TipoSolicitud = objInformacionSolicitud["AUTOLIQTIPOSOLICITUD"].ToString()
                                    },
                                    ClaseSolicitudID = Convert.ToInt32(objInformacionSolicitud["AUTOLIQSOLICITUD_ID"]),
                                    ClaseSolicitud = objInformacionSolicitud["AUTOLIQSOLICITUD"].ToString()
                                },
                                TramiteID = Convert.ToInt32(objInformacionSolicitud["AUTOLIQTRAMITE_ID"]),
                                Tramite = objInformacionSolicitud["AUTOLIQTRAMITE"].ToString()
                            };

                            //Adiciona al listado
                            objLstTramites.Add(objTramite);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "TramiteLiquidacionDalc :: ConsultarTramitesSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "TramiteLiquidacionDalc :: ConsultarTramitesSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstTramites;
            }

        #endregion

    }
}
