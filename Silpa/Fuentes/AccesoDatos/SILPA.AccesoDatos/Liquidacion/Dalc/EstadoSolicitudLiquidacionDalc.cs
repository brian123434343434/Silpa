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
    public class EstadoSolicitudLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public EstadoSolicitudLiquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los estados que puede tomar una solicitud de liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los estados activos o inactivos. Opcional </param>
            /// <returns>List con la información de los estados</returns>
            public List<EstadoSolicitudLiquidacionEntity> ConsultarEstadosSolicitud(bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<EstadoSolicitudLiquidacionEntity> objLstEstados = null;
                EstadoSolicitudLiquidacionEntity objEstado = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_ESTADO_SOLICITUD");
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstEstados = new List<EstadoSolicitudLiquidacionEntity>();

                        //Ciclo que carga los oceanos
                        foreach (DataRow objTipo in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objEstado = new EstadoSolicitudLiquidacionEntity
                            {
                                EstadoSolicitudID = Convert.ToInt32(objTipo["AUTOLIQESTADOSOLICITUD_ID"]),
                                EstadoSolicitud = objTipo["DESCRIPCION"].ToString(),
                                Activo = Convert.ToBoolean(objTipo["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstEstados.Add(objEstado);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoSolicitudLiquidacionDalc :: ConsultarEstadosSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoSolicitudLiquidacionDalc :: ConsultarEstadosSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstEstados;
            }

        #endregion

    }
}
