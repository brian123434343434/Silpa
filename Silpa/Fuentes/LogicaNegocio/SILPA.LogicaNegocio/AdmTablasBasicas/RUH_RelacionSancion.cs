using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class RUH_RelacionSancion
    {
        private RUH_RelacionSancionDALC objRelacionSancionDALC;
        public RUH_RelacionSancion()
        {
            objRelacionSancionDALC = new RUH_RelacionSancionDALC();
        }

        #region "RELACION_SANCION"

        public DataTable Listar_Relacion(string strDescripcion)
        {
            return objRelacionSancionDALC.Listar_Relacion_Sancion(strDescripcion);
        }


        public DataTable Cargar_TipoSancion()
        {
            return objRelacionSancionDALC.CargarCombo_TipoSancion();
        }


        public DataTable Cargar_Opcion()
        {
            return objRelacionSancionDALC.CargarCombo_Opcion();
        }

        public void Insertar_Relacion(int iIdTipoSancion, int iIdOpcion, string strSancion)
        {
            objRelacionSancionDALC.Insertar_Relacion_Sancion(iIdTipoSancion, iIdOpcion, strSancion);
        }

        public void Actualizar_Relacion(int iID, int iIdTipoSancion, int iIdOpcion, string strSancion)
        {
            objRelacionSancionDALC.Actualizar_Relacion_Sancion(iID, iIdTipoSancion, iIdOpcion, strSancion);
        }

        public void Eliminar_Relacion(int iID)
        {
            objRelacionSancionDALC.Eliminar_Relacion_Sancion(iID);
        }

        #endregion

    }
}
