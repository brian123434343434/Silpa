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
    public class TipoFijacionPreservacionDalc
    {
          #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TipoFijacionPreservacionDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region MetodosPublicos
        public List<TipoFijacionPreservacionEntity> ListaTipoFijacionPreservacionXGrupoBiologicoXTipoSacrificio(int p_intGrupoBiologicoID, int p_intTipoSacrificioID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<TipoFijacionPreservacionEntity> objLstTipoFijacionPreservacion = null;
            TipoFijacionPreservacionEntity objTipoFijacionPreservacion = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_FIJACION_PRESER_X_GRUPO_BIOLOGICO_X_TIPO_SACRIFICIO");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                objDataBase.AddInParameter(objCommand, "@P_TIPO_SACRIFICIO_ID", DbType.Int32, p_intTipoSacrificioID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstTipoFijacionPreservacion = new List<TipoFijacionPreservacionEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objTipoFijacionPreservacion = new TipoFijacionPreservacionEntity
                        {
                            TipoFijacionPreservacionID = Convert.ToInt32(objRow["TIPO_FIJACION_PRESERVACION_ID"]),
                            Nomenclatura = (objRow["NOMENCLATURA"] != System.DBNull.Value ? objRow["NOMENCLATURA"].ToString() : ""),
                            TipoFijacionPreservacion = (objRow["TEXTO_FIJACION_PRESEVACION"] != System.DBNull.Value ? objRow["TEXTO_FIJACION_PRESEVACION"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstTipoFijacionPreservacion.Add(objTipoFijacionPreservacion);
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

            return objLstTipoFijacionPreservacion;
        }

        #endregion
    }
}
