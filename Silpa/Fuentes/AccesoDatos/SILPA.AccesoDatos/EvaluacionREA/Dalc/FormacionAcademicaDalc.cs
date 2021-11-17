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
    public class FormacionAcademicaDalc
    {
         #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion

        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public FormacionAcademicaDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region MetodosPublicos
        public List<FormacionAcademicaProfesionalEntity> ListaFormacionAcademicaXGrupoBiologico(int p_intGrupoBiologicoID)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;
            DataSet objInformacion = null;
            List<FormacionAcademicaProfesionalEntity> objLstFormacionAcademicaProfesional = null;
            FormacionAcademicaProfesionalEntity objFormacionAcademicaProfesional = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("REASP_LISTA_FORMACION_ACADEMICA_X_GRUPO_BIOLOGICO");
                objDataBase.AddInParameter(objCommand, "@P_GRUPO_BIOLOGICO_ID", DbType.Int32, p_intGrupoBiologicoID);
                //Consultar
                objInformacion = objDataBase.ExecuteDataSet(objCommand);
                if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                {
                    //Crear el listado
                    objLstFormacionAcademicaProfesional = new List<FormacionAcademicaProfesionalEntity>();

                    //Ciclo que carga los tipos de solicitud
                    foreach (DataRow objRow in objInformacion.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objFormacionAcademicaProfesional = new FormacionAcademicaProfesionalEntity
                        {
                            FormacionAcademicaProfesionalID = Convert.ToInt32(objRow["FORMACION_ACADEMICA_PROFESIONAL_ID"]),
                            FormacionAcademicaProfesional = (objRow["TEXTO_FORMACION_ACADEMICA_PROFESIONAL"] != System.DBNull.Value ? objRow["TEXTO_FORMACION_ACADEMICA_PROFESIONAL"].ToString() : ""),
                        };

                        //Adiciona al listado
                        objLstFormacionAcademicaProfesional.Add(objFormacionAcademicaProfesional);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "FormacionAcademicaDalc :: ListaFormacionAcademicaXGrupoBiologico -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "FormacionAcademicaDalc :: ListaFormacionAcademicaXGrupoBiologico -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstFormacionAcademicaProfesional;
        }
        

        #endregion
    }
}
