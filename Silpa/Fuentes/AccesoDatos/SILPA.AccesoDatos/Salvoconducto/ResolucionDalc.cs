using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class ResolucionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ResolucionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos
            
            /// <summary>
            /// Verificar si existe la resolución
            /// </summary>
            /// <param name="p_intAutoridadAmbientalID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_intSolicitanteID">int con el identificador del solicitante</param>
            /// <param name="p_strNumeroResolucion">string con el número de resolución</param>
            /// <param name="p_objFechaResolucion">DateTime con la fecha de resolución</param>
            /// <returns>bool indicando si la resolucion se encuentra registrada</returns>
            public bool ExisteResolucion(int p_intAutoridadAmbientalID, int p_intSolicitanteID, string p_strNumeroResolucion, DateTime p_objFechaResolcion)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                bool blnExiste = false;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("SSP_VALIDA_RESOLUCION_APROVECHAMIENTO");
                    objDataBase.AddInParameter(objCommand, "@P_AUTORIDAD_EMISORA_ID", DbType.Int32, p_intAutoridadAmbientalID);
                    objDataBase.AddInParameter(objCommand, "@P_SOL_ID", DbType.Int32, p_intSolicitanteID);
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO", DbType.String, p_strNumeroResolucion);
                    objDataBase.AddInParameter(objCommand, "@P_FECHA", DbType.DateTime, p_objFechaResolcion);
                    
                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        blnExiste = Convert.ToBoolean(objInformacion.Tables[0].Rows[0]["EXISTE"]);
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ResolucionDalc :: ExisteResolucion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ResolucionDalc :: ExisteResolucion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return blnExiste;
            }

        #endregion
    }
}
