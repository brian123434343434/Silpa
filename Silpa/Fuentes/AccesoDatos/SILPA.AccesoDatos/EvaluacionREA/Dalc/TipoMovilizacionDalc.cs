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
    public class TipoMovilizacionDalc
    {
          #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TipoMovilizacionDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region MetodosPublicos
        public List<TipoMovilizacionEntity> ListaTipoMovilizacionXGrupoBiologicoXTipoSacrificioXFijaPreserv(int p_intGrupoBiologicoID, int p_intTipoSacrificioID, int p_intFijacionPreservID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<TipoMovilizacionEntity> objLstTipoMovilizacion = null;
            TipoMovilizacionEntity objTipoMovilizacion = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_MOVILIZACION_X_GRUPO_BIOLOGICO_X_TIPO_SACRIFICIO_X_FIJA_PRESERV");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                objDataBase.AddInParameter(objCommand, "@P_TIPO_SACRIFICIO_ID", DbType.Int32, p_intTipoSacrificioID);
                objDataBase.AddInParameter(objCommand, "@P_TIPO_FIJACION_PRESERVACION_ID", DbType.Int32, p_intFijacionPreservID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstTipoMovilizacion = new List<TipoMovilizacionEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objTipoMovilizacion = new TipoMovilizacionEntity
                        {
                            TipoMovilizacionID = Convert.ToInt32(objRow["TIPO_MOVILIZACION_ID"]),
                            Nomenclatura = (objRow["NOMENCLATURA"] != System.DBNull.Value ? objRow["NOMENCLATURA"].ToString() : ""),
                            TipoMovilizacion = (objRow["TEXTO_TIPO_MOVILIZACION"] != System.DBNull.Value ? objRow["TEXTO_TIPO_MOVILIZACION"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstTipoMovilizacion.Add(objTipoMovilizacion);
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

            return objLstTipoMovilizacion;
        }

        #endregion
    }
}
