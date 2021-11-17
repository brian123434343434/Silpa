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
    public class EsfuerzoMuestreoDalc
    {
     #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public EsfuerzoMuestreoDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion

        #region MetodosPublicos
        public List<EsfuerzoMuestreoEntity> ListaEsfuerzoMuestreoXGrupoBiologicoXTecnicamuestreo(int p_intGrupoBiologicoID, int p_intTecnicaMuestreoID, int? p_intUnidadMuestreoPadreID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<EsfuerzoMuestreoEntity> objLstEsfuerzoMuestreo = null;
            EsfuerzoMuestreoEntity objEsfuerzoMuestreo = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_ESFUERZO_MUESTREO_X_GRUPO_BIOLOGICO_X_TECNICA_MUESTREO");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                objDataBase.AddInParameter(objCommand, "@P_TECNICA_MUESTREO_ID", DbType.Int32, p_intTecnicaMuestreoID);
                objDataBase.AddInParameter(objCommand, "@P_ESFUERZO_MUESTREO_PADRE_ID", DbType.Int32, p_intUnidadMuestreoPadreID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstEsfuerzoMuestreo = new List<EsfuerzoMuestreoEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objTecnicaMuestreRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objEsfuerzoMuestreo = new EsfuerzoMuestreoEntity
                        {
                            EsfuerzoMuestreoID = Convert.ToInt32(objTecnicaMuestreRow["ESFUERZO_MUESTREO_ID"]),
                            EsfuerzoMuestreo = (objTecnicaMuestreRow["TEXTO_ESFUERZO_MUESTREO"] != System.DBNull.Value ? objTecnicaMuestreRow["TEXTO_ESFUERZO_MUESTREO"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstEsfuerzoMuestreo.Add(objEsfuerzoMuestreo);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EsfuerzoMuestreoDalc :: ListaEsfuerzoMuestreoXGrupoBiologicoXTecnicamuestreo -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "EsfuerzoMuestreoDalc :: ListaEsfuerzoMuestreoXGrupoBiologicoXTecnicamuestreo -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstEsfuerzoMuestreo;
        }

        #endregion
    }
}
