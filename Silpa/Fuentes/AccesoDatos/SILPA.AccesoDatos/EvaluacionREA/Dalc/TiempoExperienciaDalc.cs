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
    public class TiempoExperienciaDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public TiempoExperienciaDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region MetodosPublicos
        public List<TiempoExperienciaEntity> ListaTiempoExperiencia()
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<TiempoExperienciaEntity> objLstTiempoExperiencia = null;
            TiempoExperienciaEntity objTiempoExperiencia = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_TIEMPO_EXPERIENCIA");
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstTiempoExperiencia = new List<TiempoExperienciaEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objTiempoExperiencia = new TiempoExperienciaEntity
                        {
                            TiempoExperienciaID = Convert.ToInt32(objRow["TIEMPO_EXPERIENCIA_ID"]),
                            TiempoExperiencia = (objRow["TEXTO_TIEMPO_EXPERIENCIA"] != System.DBNull.Value ? objRow["TEXTO_TIEMPO_EXPERIENCIA"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstTiempoExperiencia.Add(objTiempoExperiencia);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "TiempoExperienciaDalc :: ListaTiempoExperiencia -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "TiempoExperienciaDalc :: ListaTiempoExperiencia -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstTiempoExperiencia;
        }

        #endregion
    }
}
