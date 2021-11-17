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
    public class CaracteriticaDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public CaracteriticaDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion

        #region MetodosPublicos
        public List<CaracteristicaEntity> ListaCaracteristicasXGrupoBiologicoXTecnicamuestreo(int p_intGrupoBiologicoID, int p_intTecnicaMuestreoID, int? p_intCaracteristicaPadreID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<CaracteristicaEntity> objLstCaracteristica = null;
            CaracteristicaEntity objCaracteristica = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_CARACTERISTICA_X_GRUPO_BIOLOGICO_X_TECNICA_MUESTREO");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                objDataBase.AddInParameter(objCommand, "@P_TECNICA_MUESTREO_ID", DbType.Int32, p_intTecnicaMuestreoID);
                objDataBase.AddInParameter(objCommand, "@P_CARACTERISTICA_PADRE_ID", DbType.Int32, p_intCaracteristicaPadreID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstCaracteristica = new List<CaracteristicaEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objTecnicaMuestreRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objCaracteristica = new CaracteristicaEntity
                        {
                            CaracteristicaID = Convert.ToInt32(objTecnicaMuestreRow["CARACTERISTICA_ID"]),
                            Caractaristica = (objTecnicaMuestreRow["TEXTO_CARACTERISTICA"] != System.DBNull.Value ? objTecnicaMuestreRow["TEXTO_CARACTERISTICA"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstCaracteristica.Add(objCaracteristica);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "CaracteriticaDalc :: ListaCaracteristicasXGrupoBiologicoXTecnicamuestreo -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "CaracteriticaDalc :: ListaCaracteristicasXGrupoBiologicoXTecnicamuestreo -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstCaracteristica;
        }

        #endregion
    }
}
