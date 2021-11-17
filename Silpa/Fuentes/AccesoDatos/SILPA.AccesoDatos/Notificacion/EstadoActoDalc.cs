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
    public class EstadoActoDalc
    {
        private Configuracion objConfiguracion;

        #region Creadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public EstadoActoDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Obtiene el listado de estados existentes
            /// </summary>
            /// <returns>List con la información de los estados</returns>
            public List<EstadoActoEntity> ObtenerListaEstados()
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objEstados = null;
                List<EstadoActoEntity> objEstadosActo = null;
                EstadoActoEntity objEstadoActo = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_LISTA_ESTADO_ACTO");

                    //Crear registro
                    objEstados = objDataBase.ExecuteDataSet(objCommand);

                    if (objEstados != null && objEstados.Tables.Count > 0 &&  objEstados.Tables[0].Rows.Count > 0)
                    {
                        //Crear listado
                        objEstadosActo = new List<EstadoActoEntity>();
                        
                        //Ciclo que carga los flujos encontrados
                        foreach (DataRow objEstado in objEstados.Tables[0].Rows)
                        {
                            //Estado del flujo
                            objEstadoActo = new EstadoActoEntity
                            {
                                EstadoID = Convert.ToInt32(objEstado["ID_ESTADO_ACTO"]),
                                Estado = objEstado["DESCRIPCION"].ToString(),
                                PermiteVisualizarInformacion = Convert.ToBoolean(objEstado["PERMITIR_VISUALIZAR_INFORMACION_ACTO"]),
                                Activo = Convert.ToBoolean(objEstado["ACTIVO"]),
                            };

                            //Cargar a listado
                            objEstadosActo.Add(objEstadoActo);
                        }
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoActoDalc :: ObtenerListaEstados -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoActoDalc :: ObtenerListaEstados -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objEstadosActo;
            }

        #endregion

    }
}
