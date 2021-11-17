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
    public class InsumoRecoleccionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public InsumoRecoleccionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Guardar la información del insumo de recoleccion
            /// </summary>
            /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
            /// <param name="p_objSolicitud">PreguntaSolicitudCambioMenorEntity con la información de la pregunta</param>
            /// <param name="p_intSolicitudREAID">Solicitud REA ID</param>
            public void InsertarInsumoRecoleccion(SqlCommand p_objCommand, InsumoRecoleccionEntity p_objInsumoRecoleccionEntity, int p_intSolicitudREAID)
            {
                int contadorCaracteristica = 1;
                int contadorEsfuerzoMuestreo = 1;

                try
                {
                    p_objCommand.CommandText = "REASP_INSERTAR_INSUMO_RECOLECCION";
                    p_objCommand.Parameters.Clear();

                    //Cargar parametros
                    p_objCommand.Parameters.Add("@P_SOLICITUD_EVALUACION_REA_ID", SqlDbType.Int).Value = p_intSolicitudREAID;
                    p_objCommand.Parameters.Add("@P_GRUPO_BIOLOGICO_ID", SqlDbType.Int).Value = p_objInsumoRecoleccionEntity.objGrupoBiologico.GrupoBiologicoID;
                    p_objCommand.Parameters.Add("@P_TECNICA_MUESTREO_ID", SqlDbType.Int).Value = p_objInsumoRecoleccionEntity.objTecnicaMuestreoEntity.TecnicaMuestreoID;
                    foreach (CaracteristicaEntity iCaracteristicaEntity in p_objInsumoRecoleccionEntity.ObjLstCaracteristicaEntity)
                    {
                        p_objCommand.Parameters.Add(string.Format("@P_CARACTERISTICA_{0}_ID", contadorCaracteristica), SqlDbType.Int).Value = iCaracteristicaEntity.CaracteristicaID;
                        contadorCaracteristica++;
                    }
                    p_objCommand.Parameters.Add("@P_UNIDAD_MUESTREO_ID", SqlDbType.Int).Value = p_objInsumoRecoleccionEntity.objUnidadMuestreoEntity.UnidadMuestreoID;
                    foreach (EsfuerzoMuestreoEntity iEsfuerzoMuestreoEntity in p_objInsumoRecoleccionEntity.objLstEsfuerzoMuestreoEntity)
                    {
                        p_objCommand.Parameters.Add(string.Format("@P_ESFUERZO_MUESTREO_{0}_ID", contadorEsfuerzoMuestreo), SqlDbType.Int).Value = iEsfuerzoMuestreoEntity.EsfuerzoMuestreoID;
                        contadorEsfuerzoMuestreo++;
                    }
                    p_objCommand.Parameters.Add("@P_RECOLECCION_ID", SqlDbType.Int).Value = p_objInsumoRecoleccionEntity.objRecoleccionEntity.RecoleccionID;
                    //Ejecuta sentencia
                    p_objCommand.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "InsumoRecoleccionDalc :: InsertarInsumoRecoleccion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "InsumoRecoleccionDalc :: InsertarInsumoRecoleccion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }

        #endregion
    }
}
