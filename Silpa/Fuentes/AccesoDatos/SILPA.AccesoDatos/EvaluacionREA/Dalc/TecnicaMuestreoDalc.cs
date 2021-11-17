using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos.EvaluacionREA.Entity;
using SILPA.Comun;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Dalc
{
    public class TecnicaMuestreoDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TecnicaMuestreoDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region MetodosPublicos
        public List<TecnicaMuestreoEntity> ListaTecnicaMuestreoPorGrupoBiologico(int p_intGrupoBiologicoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<TecnicaMuestreoEntity> objLstTecnicaMuestreo = null;
            TecnicaMuestreoEntity objTecnicaMuestreo = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_TECNICA_MUESTREO_X_GRUPO_BIOLOGICO");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstTecnicaMuestreo = new List<TecnicaMuestreoEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objTecnicaMuestreRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objTecnicaMuestreo = new TecnicaMuestreoEntity
                        {
                            TecnicaMuestreoID = Convert.ToInt32(objTecnicaMuestreRow["TECNICA_MUESTREO_ID"]),
                            TecnicaMuestreo = (objTecnicaMuestreRow["TEXTO_TECNICA_MUESTREO"] != System.DBNull.Value ? objTecnicaMuestreRow["TEXTO_TECNICA_MUESTREO"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstTecnicaMuestreo.Add(objTecnicaMuestreo);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "TecnicaMuestreoDalc :: ListaTecnicaMuestreoPorGrupoBiologico -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "TecnicaMuestreoDalc :: ListaTecnicaMuestreoPorGrupoBiologico -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstTecnicaMuestreo;
        }

        #endregion
    }
}
