using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
  public class ActividadRadicable
    {
  private ActividadRadicableDALC objActividadRadicableDALC;

        public ActividadRadicable()
        {
            objActividadRadicableDALC = new ActividadRadicableDALC();
        }

        #region RUH_TIPO_SANCION
        public DataTable ListarActividadRadicable(string strNombre)
        {
            return objActividadRadicableDALC.ListarActividadRadicable(strNombre);
        }

      public void InsertarActividadRadicable(int intIdForma, bool bEstado, string strNombre, int intIdActividad)
        {
            objActividadRadicableDALC.InsertarActividadRadicable(intIdForma, bEstado, strNombre, intIdActividad);
        }

      public void ActualizarActividadRadicable(int intId, int intIdForma, bool bEstado, string strNombre, int intIdActividad)
        {
            objActividadRadicableDALC.ActualizarActividadRadicable(intId, intIdForma, bEstado, strNombre, intIdActividad);
        }

        public void EliminarActividadRadicable(int intId)
        {
            objActividadRadicableDALC.EliminarActividadRadicable(intId);
        }

        #endregion
    }
}

