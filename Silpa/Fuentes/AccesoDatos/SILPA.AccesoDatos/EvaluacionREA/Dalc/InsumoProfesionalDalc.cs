using SILPA.AccesoDatos.EvaluacionREA.Entity;
using SILPA.Comun;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.EvaluacionREA.Dalc
{
    public class InsumoProfesionalDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public InsumoProfesionalDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region  Metodos Publicos
        public void InsertarInsumoProfesional(SqlCommand p_objCommand, InsumoProfesionalEntity p_objInsumoProfesionalEntity, int p_intSolicitudREAID)
        {
            try
            {
                p_objCommand.CommandText = "REASP_INSERTAR_INSUMO_PROFESIONAL";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_SOLICITUD_EVALUACION_REA_ID", SqlDbType.Int).Value = p_intSolicitudREAID;
                p_objCommand.Parameters.Add("@P_GRUPO_BIOLOGICO_ID", SqlDbType.Int).Value = p_objInsumoProfesionalEntity.objGrupoBiologico.GrupoBiologicoID;
                p_objCommand.Parameters.Add("@P_FORMACION_ACADEMICA_PROFESIONAL_ID", SqlDbType.Int).Value = p_objInsumoProfesionalEntity.objFormacionAcademicaProfesionalEntity.FormacionAcademicaProfesionalID;
                p_objCommand.Parameters.Add("@P_TIEMPO_EXPERIENCIA_ID", SqlDbType.Int).Value = p_objInsumoProfesionalEntity.objTiempoExperienciaEntity.TiempoExperienciaID;
                p_objCommand.Parameters.Add("@P_EXPERIENCIA_ESPECIFICA_ID", SqlDbType.Int).Value = p_objInsumoProfesionalEntity.objExperienciaEspecificaEntity.ExperienciaEspecificaID;
                //Ejecuta sentencia
                p_objCommand.ExecuteNonQuery();
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "InsumoProfesionalDalc :: InsertarInsumoProfesional -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "InsumoProfesionalDalc :: InsertarInsumoProfesional -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }
        #endregion
    }
}
