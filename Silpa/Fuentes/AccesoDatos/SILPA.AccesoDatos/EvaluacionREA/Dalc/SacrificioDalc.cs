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
    public class TipoSacrificioDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TipoSacrificioDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region MetodosPublicos
        public List<TipoSacrificioEntity> ListaTipoSacrificioXGrupoBiologico(int p_intGrupoBiologicoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<TipoSacrificioEntity> objLstTipoSacrificio = null;
            TipoSacrificioEntity objTipoSacrificio = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_TIPO_SACRIFICION_X_GRUPO_BIOLOGICO");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstTipoSacrificio = new List<TipoSacrificioEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objTipoSacrificio = new TipoSacrificioEntity
                        {
                            TipoSacrificioID = Convert.ToInt32(objRow["TIPO_SACRIFICIO_ID"]),
                            Nomenclatura = (objRow["NOMENCLATURA"] != System.DBNull.Value ? objRow["NOMENCLATURA"].ToString() : ""),
                            TipoSacrificio = (objRow["TEXTO_TIPO_SACRIFICIO"] != System.DBNull.Value ? objRow["TEXTO_TIPO_SACRIFICIO"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstTipoSacrificio.Add(objTipoSacrificio);
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

            return objLstTipoSacrificio;
        }

        #endregion
    }
}
