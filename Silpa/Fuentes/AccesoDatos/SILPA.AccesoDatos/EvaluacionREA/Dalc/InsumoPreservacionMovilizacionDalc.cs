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
    public class InsumoPreservacionMovilizacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public InsumoPreservacionMovilizacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion
        #region  Metodos Publicos
        /// <summary>
        /// Guarda la informacion del insumo de la preservacion y movilizacion
        /// </summary>
        /// <param name="p_objCommand"></param>
        /// <param name="p_objInsumoPreservacionMovilizacionEntity"></param>
        /// <param name="p_intSolicitudREAID"></param>
        public void InsertarInsumoPreservacionMovilizacion(SqlCommand p_objCommand, InsumoPreservacionMovilizacionEntity p_objInsumoPreservacionMovilizacionEntity, int p_intSolicitudREAID)
        {
            try
            {
                p_objCommand.CommandText = "REASP_INSERTAR_INSUMO_PRESERVACION_MOVILIZACION";
                p_objCommand.Parameters.Clear();

                //Cargar parametros
                p_objCommand.Parameters.Add("@P_SOLICITUD_EVALUACION_REA_ID", SqlDbType.Int).Value = p_intSolicitudREAID;
                p_objCommand.Parameters.Add("@P_GRUPO_BIOLOGICO_ID", SqlDbType.Int).Value = p_objInsumoPreservacionMovilizacionEntity.objGrupoBiologico.GrupoBiologicoID;
                p_objCommand.Parameters.Add("@P_TIPO_SACRIFICIO_ID", SqlDbType.Int).Value = p_objInsumoPreservacionMovilizacionEntity.objTipoSacrificioEntity.TipoSacrificioID;
                p_objCommand.Parameters.Add("@P_TIPO_FIJACION_PRESERVACION_ID", SqlDbType.Int).Value = p_objInsumoPreservacionMovilizacionEntity.objTipoFijacionPreservacionEntity.TipoFijacionPreservacionID;
                p_objCommand.Parameters.Add("@P_TIPO_MOVILIZACION_ID", SqlDbType.Int).Value = p_objInsumoPreservacionMovilizacionEntity.objTipoMovilizacionEntity.TipoMovilizacionID;
                //Ejecuta sentencia
                p_objCommand.ExecuteNonQuery();
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "InsumoPreservacionMovilizacionDalc :: InsertarInsumoPreservacionMovilizacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "InsumoPreservacionMovilizacionDalc :: InsertarInsumoPreservacionMovilizacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }
        #endregion
    }
}
