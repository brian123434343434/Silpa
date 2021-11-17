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
    public class ExperienciaEspecificaDalc
    {
         #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public ExperienciaEspecificaDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region MetodosPublicos
        public List<ExperienciaEspecificaEntity> ListaExperienciaEspecificaXGrupoBiologicoXFormacionAcademica(int p_intGrupoBiologicoID, int p_intFormacionAcademicaID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<ExperienciaEspecificaEntity> objLstExperienciaEspecifica = null;
            ExperienciaEspecificaEntity objExperienciaEspecifica = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_ESPERIENCIA_ESPECIFICA_X_GRUPO_BIOLOGICO_X_FORMACION_ACADEMICA");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                objDataBase.AddInParameter(objCommand, "@P_FORMACION_ACADEMICA_PROFESIONAL_ID", DbType.Int32, p_intFormacionAcademicaID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstExperienciaEspecifica = new List<ExperienciaEspecificaEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objExperienciaEspecifica = new ExperienciaEspecificaEntity
                        {
                            ExperienciaEspecificaID = Convert.ToInt32(objRow["EXPERIENCIA_ESPECIFICA_ID"]),
                            ExperienciaEspecifica = (objRow["TEXTO_EXPERIENCIA_ESPECIFICA"] != System.DBNull.Value ? objRow["TEXTO_EXPERIENCIA_ESPECIFICA"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstExperienciaEspecifica.Add(objExperienciaEspecifica);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "ExperienciaEspecificaDalc :: ListaExperienciaEspecificaXGrupoBiologicoXFormacionAcademica -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "ExperienciaEspecificaDalc :: ListaExperienciaEspecificaXGrupoBiologicoXFormacionAcademica -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstExperienciaEspecifica;
        }

        #endregion


        
    }
}
