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
    public class EstadoInicialActoParametroDalc
    {
        private Configuracion objConfiguracion;

        #region CReadoras
        
            /// <summary>
            /// Creadora
            /// </summary>
            public EstadoInicialActoParametroDalc()
            {
                this.objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Publicos


            /// <summary>
            /// Obtiene los estados iniciales de un acto administrativo para una entidad especifica
            /// </summary>
            /// <param name="p_intAutoridadAmbiental">int con el identificador de la autoridad ambiental</param>
            /// <returns>EstadoInicialActoParametroEntity con la información de estados iniciales</returns>
            public EstadoInicialActoParametroEntity ObtenerEstadoInicialEntidad(int p_intAutoridadAmbiental)
            {
                DbCommand objCommand = null;
                SqlDatabase objDataBase = null;
                DataSet objEstados = null;
                EstadoInicialActoParametroEntity objEstadosIniciales = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTA_ESTADO_INICIAL_ACTO_PARAMETRO_AUTORIDAD");
                    objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadAmbiental);

                    //Crear registro
                    objEstados = objDataBase.ExecuteDataSet(objCommand);

                    if (objEstados != null && objEstados.Tables.Count > 0 &&  objEstados.Tables[0].Rows.Count > 0)
                    {
                        //Estado del flujo
                        objEstadosIniciales = new EstadoInicialActoParametroEntity
                        {
                            AutoridadAmbientalID = p_intAutoridadAmbiental,
                            EstadoInicialActoID = Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_ESTADO_INICIAL_ACTO"]),
                            EstadoInicialPersonaID = Convert.ToInt32(objEstados.Tables[0].Rows[0]["ID_ESTADO_INICIAL_PERSONA_NOTIFICAR"])
                        };
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoInicialActoParametroDalc :: ObtenerEstadoInicialEntidad -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "EstadoInicialActoParametroDalc :: ObtenerEstadoInicialEntidad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

                return objEstadosIniciales;
            }

        #endregion

    }
}
