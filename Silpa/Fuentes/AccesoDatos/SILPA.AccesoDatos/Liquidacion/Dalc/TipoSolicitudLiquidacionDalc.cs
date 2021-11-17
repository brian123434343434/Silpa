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
    public class TipoSolicitudLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public TipoSolicitudLiquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los tipos de solicitud de una liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae las solicitudes activas o inactivas. Opcional </param>
            /// <returns>List con la información de los tipos de solicitud</returns>
            public List<TipoSolicitudLiquidacionEntity> ConsultarTiposSolicitud(bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<TipoSolicitudLiquidacionEntity> objLstTiposSolicitud = null;
                TipoSolicitudLiquidacionEntity objTipoSolicitud = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_TIPO_SOLICITUD");
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstTiposSolicitud = new List<TipoSolicitudLiquidacionEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objTipo in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objTipoSolicitud = new TipoSolicitudLiquidacionEntity
                            {
                                TipoSolicitudID = Convert.ToInt32(objTipo["AUTOLIQTIPOSOLICITUD_ID"]),
                                TipoSolicitud = objTipo["DESCRIPCION"].ToString(),
                                CasoProcesoID = Convert.ToInt32(objTipo["CASO_PROCESO_VITAL_ID"]),
                                FormularioID = Convert.ToInt32(objTipo["FORMULARIO_VITAL_ID"]),
                                Activo = Convert.ToBoolean(objTipo["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstTiposSolicitud.Add(objTipoSolicitud);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "TipoSolicitudLiquidacionDalc :: ConsultarTiposSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "TipoSolicitudLiquidacionDalc :: ConsultarTiposSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstTiposSolicitud;
            }


            /// <summary>
            /// Consultar la información de un tipo de solicitud especifico
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificador de tipo de solicitud</param>
            /// <returns>TipoSolicitudLiquidacionEntity con la información del tipo de solicitud indicado</returns>
            public TipoSolicitudLiquidacionEntity ConsultarTipoSolicitud(int p_intTipoSolicitudID)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                TipoSolicitudLiquidacionEntity objTipoSolicitud = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_CONSULTAR_TIPO_SOLICITUD");
                    objDataBase.AddInParameter(objCommand, "@P_AUTOLIQTIPOSOLICITUD_ID", DbType.Int32, p_intTipoSolicitudID);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear objeto y cargar datos
                        objTipoSolicitud = new TipoSolicitudLiquidacionEntity
                        {
                            TipoSolicitudID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["AUTOLIQTIPOSOLICITUD_ID"]),
                            TipoSolicitud = objInformacion.Tables[0].Rows[0]["DESCRIPCION"].ToString(),
                            CasoProcesoID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["CASO_PROCESO_VITAL_ID"]),
                            FormularioID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["FORMULARIO_VITAL_ID"]),
                            Activo = Convert.ToBoolean(objInformacion.Tables[0].Rows[0]["ACTIVO"])
                        };
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "TipoSolicitudLiquidacionDalc :: ConsultarTipoSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "TipoSolicitudLiquidacionDalc :: ConsultarTipoSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objTipoSolicitud;
            }

        #endregion

    }
}
