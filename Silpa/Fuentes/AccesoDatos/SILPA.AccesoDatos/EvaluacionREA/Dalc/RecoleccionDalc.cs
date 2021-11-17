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
    public class RecoleccionDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public RecoleccionDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region MetodosPublicos
        public List<RecoleccionEntity> ListaRecoleccionXGrupoBiologicoXTecnicaMuestreo(int p_intGrupoBiologicoID, int p_intTecnicaMuestreoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<RecoleccionEntity> objLstRecoleccion = null;
            RecoleccionEntity objRecoleccion = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_RECOLECCION_X_GRUPO_BIOLOGICO_X_TECNICA_MUESTREO");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                objDataBase.AddInParameter(objCommand, "@P_TECNICA_MUESTREO_ID", DbType.Int32, p_intTecnicaMuestreoID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstRecoleccion = new List<RecoleccionEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objRecoleccion = new RecoleccionEntity
                        {
                            RecoleccionID = Convert.ToInt32(objRow["RECOLECCION_ID"]),
                            Recoleccion = (objRow["TEXTO_RECOLECCION"] != System.DBNull.Value ? objRow["TEXTO_RECOLECCION"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstRecoleccion.Add(objRecoleccion);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "UnidadMuestreoDalc :: ListaTecnicaMuestreoPorGrupoBiologico -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "UnidadMuestreoDalc :: ListaTecnicaMuestreoPorGrupoBiologico -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstRecoleccion;
        }

        #endregion
    }
}
