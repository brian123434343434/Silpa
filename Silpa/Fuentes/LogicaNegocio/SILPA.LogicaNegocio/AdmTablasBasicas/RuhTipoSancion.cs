using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class RuhTipoSancion
    {
        private Ruh_Tipo_SancionDALC objRuhTipoSancionDALC;

        public RuhTipoSancion()
        {
            objRuhTipoSancionDALC = new Ruh_Tipo_SancionDALC();
        }

        #region RUH_TIPO_SANCION
        public DataTable Listar_RUH_Tipo_Sancion(string strDescripcion)
        {
            return objRuhTipoSancionDALC.Listar_Ruh_Tipo_Sancion(strDescripcion);
        }

        public void Insertar_RUH_Tipo_Sancion(string strDescripcion, byte byEstado)
        {
            objRuhTipoSancionDALC.Insertar_Ruh_Tipo_Sancion(strDescripcion, byEstado);
        }

        public void Actualizar_RUH_Tipo_Falta(int intId, string strDescripcion, byte byEstado)
        {
            objRuhTipoSancionDALC.Actualizar_Ruh_Tipo_Sancion(intId, strDescripcion, byEstado);
        }

        public void Eliminar_RUH_Tipo_Falta(int intId)
        {
            objRuhTipoSancionDALC.Eliminar_Ruh_Tipo_Sancion(intId);
        }

        #endregion
    }
}
