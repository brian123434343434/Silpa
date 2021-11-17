using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class RuhTipoFalta
    {
        private Ruh_Tipo_FaltaDALC obj_ruh_tipo_falta_dalc;

        public RuhTipoFalta()
        {
            obj_ruh_tipo_falta_dalc = new Ruh_Tipo_FaltaDALC();
        }

        #region RUH_TIPO_FALTA
        public DataTable Listar_RUH_Tipo_Falta(string strDescripcion)
        {
            return obj_ruh_tipo_falta_dalc.Listar_Ruh_Tipo_Falta(strDescripcion);
        }

        public void Insertar_RUH_Tipo_Falta(string strDescripcion, byte byEstado)
        {
            obj_ruh_tipo_falta_dalc.Insertar_Ruh_Tipo_Falta(strDescripcion, byEstado);
        }

        public void Actualizar_RUH_Tipo_Falta(int intId, string strDescripcion, byte byEstado)
        {
            obj_ruh_tipo_falta_dalc.Actualizar_Ruh_Tipo_Falta(intId, strDescripcion, byEstado);
        }

        public void Eliminar_RUH_Tipo_Falta(int intId)
        {
            obj_ruh_tipo_falta_dalc.Eliminar_Ruh_Tipo_Falta(intId);
        }

        #endregion
    }
}
