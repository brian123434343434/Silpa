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
    public class TipoCoordenadaUbicacionLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public TipoCoordenadaUbicacionLiquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los tipos de coordenada
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los tipos de coordenada activos o inactivos. Opcional </param>
            /// <returns>List con la información de los tipo de coordenada</returns>
            public List<TipoCoordenadaUbicacionLiquidacionEntity> ConsultarTiposCoordenada(bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<TipoCoordenadaUbicacionLiquidacionEntity> objLstTipoCoordenadas = null;
                TipoCoordenadaUbicacionLiquidacionEntity objTipoCoordenada = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_TIPO_COORDENADA");
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstTipoCoordenadas = new List<TipoCoordenadaUbicacionLiquidacionEntity>();

                        //Ciclo que carga los tipo de geometria
                        foreach (DataRow objTipo in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objTipoCoordenada = new TipoCoordenadaUbicacionLiquidacionEntity
                            {
                                TipoCoordenadaID = Convert.ToInt32(objTipo["AUTOLIQTIPOCOORDENADA_ID"]),
                                TipoCoordenada = objTipo["DESCRIPCION"].ToString(),
                                Activo = Convert.ToBoolean(objTipo["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstTipoCoordenadas.Add(objTipoCoordenada);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "TipoCoordenadaUbicacionLiquidacionDalc :: ConsultarTiposCoordenada -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "TipoCoordenadaUbicacionLiquidacionDalc :: ConsultarTiposCoordenada -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstTipoCoordenadas;
            }

        #endregion

    }
}
