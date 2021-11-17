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
    public class SectorLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public SectorLiquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los sectores que se pueden asociar a una liquidación
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los sectores activas o inactivas. Opcional </param>
            /// <returns>List con la información de los sectores</returns>
            public List<SectorLiquidacionEntity> ConsultarSectores(bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<SectorLiquidacionEntity> objLstSectores = null;
                SectorLiquidacionEntity objSector = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_SECTOR");
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstSectores = new List<SectorLiquidacionEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objTipo in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objSector = new SectorLiquidacionEntity
                            {
                                SectorID = Convert.ToInt32(objTipo["AUTOLIQSECTOR_ID"]),
                                Sector = objTipo["DESCRIPCION"].ToString(),
                                Activo = Convert.ToBoolean(objTipo["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstSectores.Add(objSector);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SectorLiquidacionDalc :: ConsultarSectores -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SectorLiquidacionDalc :: ConsultarSectores -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstSectores;
            }

        #endregion

    }
}
