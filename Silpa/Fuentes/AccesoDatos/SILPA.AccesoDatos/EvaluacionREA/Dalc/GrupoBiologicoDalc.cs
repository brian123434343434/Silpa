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
    public class GrupoBiologicoDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public GrupoBiologicoDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion

        #region MetodosPublicos
        public List<GrupoBiologicoEntity> ListaGruposBiologicos()
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<GrupoBiologicoEntity> objLstGrupoBiologico = null;
            GrupoBiologicoEntity objGrupoBiologico = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_GRUPO_BIOLOGICO");

                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstGrupoBiologico = new List<GrupoBiologicoEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objGrupoBiologicoRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objGrupoBiologico = new GrupoBiologicoEntity
                        {
                            GrupoBiologicoID = Convert.ToInt32(objGrupoBiologicoRow["GRUPO_BIOLOGICO_ID"]),
                            GrupoBiologico = (objGrupoBiologicoRow["TEXTO_GRUPO_BIOLOGICO"] != System.DBNull.Value ? objGrupoBiologicoRow["TEXTO_GRUPO_BIOLOGICO"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstGrupoBiologico.Add(objGrupoBiologico);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "GrupoBiologicoDalc :: ListaGruposBiologicos -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "GrupoBiologicoDalc :: ListaGruposBiologicos -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstGrupoBiologico;
        }

        #endregion
    }
}
