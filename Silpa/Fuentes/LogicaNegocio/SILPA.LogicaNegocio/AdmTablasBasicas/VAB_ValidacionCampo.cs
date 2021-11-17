using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class VAB_ValidacionCampo
    {
        private VAB_ValidacionCampoDALC objValidacionCampoDALC;
        public VAB_ValidacionCampo()
        {
            objValidacionCampoDALC = new VAB_ValidacionCampoDALC();
        }

        #region "VALIDACIOM_CAMPO"

        public DataTable Listar_ValidacionCampo(string strDescripcion)
        {
            return objValidacionCampoDALC.Listar_ValidacionCampo(strDescripcion);
        }

        public void Insertar_ValidacionCampo(string strIdCampo, int iIdValidacion, string strActivo)
        {
            objValidacionCampoDALC.Insertar_ValidacionCampo(strIdCampo, iIdValidacion, strActivo);
        }

        public void Actualizar_ValidacionCampo(int iID, string strIdCampo, int iIdValidacion, string strActivo)
        {
            objValidacionCampoDALC.Actualizar_ValidacionCampo(iID, strIdCampo, iIdValidacion, strActivo);
        }

        public void Eliminar_ValidacionCampo(int iID)
        {
            objValidacionCampoDALC.Eliminar_ValidacionCampo(iID);
        }

        public DataTable Cargar_Combo_Campo()
        {
            return objValidacionCampoDALC.Cargar_Combo_Campos();
        }

        public DataTable Cargar_Combo_Validaciones()
        {
            return objValidacionCampoDALC.Cargar_Combo_Validaciones();
        }

        #endregion

    }
}
