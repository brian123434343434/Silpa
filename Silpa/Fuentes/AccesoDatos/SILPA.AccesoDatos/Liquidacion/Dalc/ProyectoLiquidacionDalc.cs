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
    public class ProyectoLiquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ProyectoLiquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los proyectos asociados a un sector
            /// </summary>
            ///<param name="p_intSectorID">int con el identificador del sector</param>
            /// <param name="p_blnActivo">bool que indica si se extrae los proyectos activos o inactivos. Opcional </param>
            /// <returns>List con la información de los Proyectos</returns>
            public List<ProyectoLiquidacionEntity> ConsultarProyectosSector(int p_intSectorID,  bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<ProyectoLiquidacionEntity> objLstProyectos = null;
                ProyectoLiquidacionEntity objProyecto = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTA_SECTOR_PROYECTO");
                    objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSECTOR_ID", DbType.Int32, p_intSectorID);
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstProyectos = new List<ProyectoLiquidacionEntity>();

                        //Ciclo que carga los tipos de solicitud
                        foreach (DataRow objInfoProyecto in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objProyecto = new ProyectoLiquidacionEntity
                            {
                                ProyectoID = Convert.ToInt32(objInfoProyecto["AUTOLIQPROYECTO_ID"]),
                                Proyecto = objInfoProyecto["AUTOLIQPROYECTO"].ToString(),
                                Activo = Convert.ToBoolean(objInfoProyecto["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstProyectos.Add(objProyecto);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ProyectoLiquidacionDalc :: ConsultarProyectosSector -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ProyectoLiquidacionDalc :: ConsultarProyectosSector -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstProyectos;
            }

        #endregion

    }
}
