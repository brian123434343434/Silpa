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
    public class RegionLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public RegionLiquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar las regiones en las cuales se puede llevar a cabo un proyecto asociado a una liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae las regiones activas o inactivas. Opcional </param>
            /// <returns>List con la información de las regiones</returns>
            public List<RegionLiquidacionEntity> ConsultarRegiones(bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<RegionLiquidacionEntity> objLstRegiones = null;
                RegionLiquidacionEntity objRegion = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_REGION");
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstRegiones = new List<RegionLiquidacionEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objTipo in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objRegion = new RegionLiquidacionEntity
                            {
                                RegionID = Convert.ToInt32(objTipo["AUTOLIQREGION_ID"]),
                                Region = objTipo["DESCRIPCION"].ToString(),
                                Activo = Convert.ToBoolean(objTipo["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstRegiones.Add(objRegion);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RegionLiquidacionDalc :: ConsultarRegiones -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RegionLiquidacionDalc :: ConsultarRegiones -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstRegiones;
            }

        #endregion

    }
}
