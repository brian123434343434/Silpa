using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class WorkFlowActividadRelacionada
    {
        private WorkFlowActividadRelacionadaDALC objActividadRelacionadaDALC;
        public WorkFlowActividadRelacionada()
        {
            objActividadRelacionadaDALC = new WorkFlowActividadRelacionadaDALC();
        }

        #region "ACTIVIDAD_RELACIONADA"

        public DataTable Listar_Actividad_Silpa()
        {
            return objActividadRelacionadaDALC.Listar_Actividades_Silpa();
        }

        public DataTable Listar_Actividad_Relacionada(Int32 iIdActividadSilpa)
        {
            return objActividadRelacionadaDALC.Listar_Actividades_Relacionadas(iIdActividadSilpa);
        }

        public DataTable Listar_Actividad_NoRelacionada(Int32 iIdActividadSilpa)
        {
            return objActividadRelacionadaDALC.Listar_Actividades_NoRelacionadas(iIdActividadSilpa);
        }

        public void Insertar_Actividad_Relacionada(int iActividadId, int iIdActividadSilpa)
        {
            objActividadRelacionadaDALC.Insertar_Actividades_Relacionadas(iActividadId, iIdActividadSilpa);
        }

        public void Eliminar_Actividad_Relacionada(int iActividadId, int iIdActividadSilpa)
        {
            objActividadRelacionadaDALC.Eliminar_Actividades_Relacionadas(iActividadId, iIdActividadSilpa);
        }

        #endregion
    }
}
