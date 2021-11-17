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
    public class MedioTransporteLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public MedioTransporteLiquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los medios de transporte
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los medios de transportes activos o inactivos. Opcional </param>
            /// <returns>List con la información de los medios de transportes</returns>
            public List<MedioTransporteLiquidacionEntity> ConsultarMediosTransporte(bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<MedioTransporteLiquidacionEntity> objLstMediosTransporte = null;
                MedioTransporteLiquidacionEntity objMedioTransporte = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_MEDIO_TRANSPORTE");
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstMediosTransporte = new List<MedioTransporteLiquidacionEntity>();

                        //Ciclo que carga los medio de transportes
                        foreach (DataRow objTipo in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objMedioTransporte = new MedioTransporteLiquidacionEntity
                            {
                                MedioTransporteID = Convert.ToInt32(objTipo["AUTOLIQMedioTransporte_ID"]),
                                MedioTransporte = objTipo["DESCRIPCION"].ToString(),
                                Activo = Convert.ToBoolean(objTipo["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstMediosTransporte.Add(objMedioTransporte);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "MedioTransporteLiquidacionDalc :: ConsultarMediosTransporte -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "MedioTransporteLiquidacionDalc :: ConsultarMediosTransporte -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstMediosTransporte;
            }

        #endregion

    }
}
