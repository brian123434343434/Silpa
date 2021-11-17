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
    public class InsumoCoberturaDalc
    {
        #region  Objetos

        private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

        /// <summary>
        /// Creadora
        /// </summary>
        public InsumoCoberturaDalc()
        {
            //Creary cargar configuración
            this._objConfiguracion = new Configuracion();
        }

        #endregion
        #region  Metodos Publicos
        public void InsertarInsumoCobertura(SqlCommand p_objCommand, InsumoCoberturaEntity p_objInsumoCoberturaEntity, int p_intSolicitudREAID)
        {
            try
            {
                p_objCommand.CommandText = "REASP_INSERTAR_INSUMO_COBERTURA";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_SOLICITUD_EVALUACION_REA_ID", SqlDbType.Int).Value = p_intSolicitudREAID;
                p_objCommand.Parameters.Add("@P_DEPARTAMENTO_ID", SqlDbType.Int).Value = p_objInsumoCoberturaEntity.DepartamentoID;
                p_objCommand.Parameters.Add("@P_MUNICIPIO_ID", SqlDbType.Int).Value = p_objInsumoCoberturaEntity.MunicipioID;
                //Ejecuta sentencia
                p_objCommand.ExecuteNonQuery();
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "InsumoCoberturaDalc :: InsertarInsumoCobertura -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "InsumoCoberturaDalc :: InsertarInsumoCobertura -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }
        #endregion
    }
}
