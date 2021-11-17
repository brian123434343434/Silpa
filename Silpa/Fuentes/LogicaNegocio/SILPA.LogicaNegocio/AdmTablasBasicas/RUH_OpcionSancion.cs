using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class RUH_OpcionSancion
    {
        private RUH_OpcionSancionDALC objOpcionSancionDALC;
        public RUH_OpcionSancion()
        {
            objOpcionSancionDALC = new RUH_OpcionSancionDALC();
        }

        #region "OPCION_SANCION"

        public DataTable Listar_OpcionSancion(string strDescripcion)
        {
            return objOpcionSancionDALC.Listar_Opcion_Sancion(strDescripcion);
        }

        public void Insertar_OpcionSancion(string strNombre, bool bActivo, int iDias)
        {
            objOpcionSancionDALC.Insertar_Opcion_Sancion(strNombre, bActivo, iDias);
        }

        public void Actualizar_OpcionSancion(int iID, string strNombre, bool bActivo, byte byDias)
        {
            objOpcionSancionDALC.Actualizar_Opcion_Sancion(iID, strNombre, bActivo, byDias);
        }

        public void Eliminar_OpcionSancion(int iID)
        {
            objOpcionSancionDALC.Eliminar_Opcion_Sancion(iID);
        }

        #endregion
    }
}
