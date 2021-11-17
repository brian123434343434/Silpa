using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class RuiaTiempoSancion
    {
        private RuiaTiempoSancionDALC objTiempoSancionDALC;

        public RuiaTiempoSancion()
        {
            objTiempoSancionDALC = new RuiaTiempoSancionDALC();
        }

        #region RUH_TIPO_SANCION
        public DataTable ListarTiempoSancion(int intId, string strNombre)
        {
            return objTiempoSancionDALC.ListarTiempoSancion(intId, strNombre);
        }

        public void InsertarTiempoSancion(int intDias, bool bEstado, string strNombre)
        {
            objTiempoSancionDALC.InsertarTiempoSancion(intDias, bEstado, strNombre);
        }

        public void ActualizarTiempoSancion(int intId, int intDias, bool bEstado, string strNombre)
        {
            objTiempoSancionDALC.ActualizarTiempoSancion(intId, intDias, bEstado, strNombre);
        }

        public void EliminarTiempoSancion(int intId)
        {
            objTiempoSancionDALC.EliminarTiempoSancion(intId);
        }

        #endregion
    }
}
