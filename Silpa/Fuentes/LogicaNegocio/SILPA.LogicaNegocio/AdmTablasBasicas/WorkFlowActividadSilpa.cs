using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class WorkFlowActividadSilpa
    {
        private WorkFlowActividadSilpaDALC  objActividadSilpaDALC;
        public WorkFlowActividadSilpa()
        {
            objActividadSilpaDALC = new WorkFlowActividadSilpaDALC();
        }

        #region "ACTIVIDAD_SILPA"

        public DataTable Listar_Actividad_Silpa(string strDescripcion)
        {
            return objActividadSilpaDALC.Listar_Actividades_Silpa(strDescripcion);
        }

        public void Insertar_Actividad_Silpa(string strNombreActividad)
        {
            objActividadSilpaDALC.Insertar_Actividades_Silpa(strNombreActividad);
        }

        public void Actualizar_Actividad_Silpa(int iID, string strNombreActividad)
        {
            objActividadSilpaDALC.Actualizar_Actividades_Silpa(iID, strNombreActividad);
        }

        public void Eliminar_Actividad_Silpa(int iID)
        {
            objActividadSilpaDALC.Eliminar_Actividades_Silpa(iID);
        }

        #endregion
    }
}
