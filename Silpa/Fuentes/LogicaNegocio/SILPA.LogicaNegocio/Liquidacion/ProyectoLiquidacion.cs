using System.Collections.Generic;
using System;
using SILPA.Comun;
using SILPA.AccesoDatos.Liquidacion.Entity;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Liquidacion.Dalc;
using System.Data;

namespace SILPA.LogicaNegocio.Liquidacion
{
    public class ProyectoLiquidacion
    {                
        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public ProyectoLiquidacion(){}

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar los proyectos asociados a un sector
            /// </summary>
            ///<param name="p_intSectorID">int con el identificador del sector</param>
            /// <param name="p_blnActivo">bool que indica si se extrae los proyectos activos o inactivos. Opcional </param>
            /// <returns>List con la información de los Proyectos</returns>
            public List<ProyectoLiquidacionEntity> ConsultarProyectosSector(int p_intSectorID, bool? p_blnActivo = null)
            {
                ProyectoLiquidacionDalc objProyectoLiquidacionDalc = null;                
                
                //Cargar el listado de proyectos
                objProyectoLiquidacionDalc = new ProyectoLiquidacionDalc();
                return objProyectoLiquidacionDalc.ConsultarProyectosSector(p_intSectorID, p_blnActivo);
            }



            /// <summary>
            /// Consultar listado de actividades pertenecientes a un proyecto
            /// </summary>
            /// <returns>List con la información de las actividades</returns>
            public List<ActividadLiquidacionEntity> ConsultarActividadesProyecto(int p_intProyectoID)
            {
                Listas objListasDalc = null;
                DataSet objActividades = null;
                List<ActividadLiquidacionEntity> objLstActividades = null;

                //Cargar el listado de actividades
                objListasDalc = new Listas();
                objActividades = objListasDalc.ListarSectorTipoProyecto(null, p_intProyectoID);

                //Verificar si se obtuvieron datos
                if (objActividades != null && objActividades.Tables != null && objActividades.Tables[0].Rows.Count > 0)
                {
                    //Crear listado 
                    objLstActividades = new List<ActividadLiquidacionEntity>();

                    //Ciclo que carga los datos
                    foreach (DataRow objDatosProyecto in objActividades.Tables[0].Rows)
                    {
                        //No se debe incluir el proyecto en el listado de actividades
                        if (Convert.ToInt32(objDatosProyecto["SEC_ID"]) != p_intProyectoID && (objDatosProyecto["SEC_CORRESPONDE_MAVDT"] == System.DBNull.Value || !Convert.ToBoolean(objDatosProyecto["SEC_CORRESPONDE_MAVDT"])) )
                        {

                            objLstActividades.Add(new ActividadLiquidacionEntity
                            {
                                ActividadID = Convert.ToInt32(objDatosProyecto["SEC_ID"]),
                                Actividad = objDatosProyecto["SEC_NOMBRE"].ToString()
                            });
                        }
                    }
                }

                return objLstActividades;
            }

        #endregion

    }
}
