using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Tipo_Salvoconducto
    {
        private Tipo_SalvoconductoDALC objTipoSalvoconductoDALC;
        public Tipo_Salvoconducto()
        {
            objTipoSalvoconductoDALC = new Tipo_SalvoconductoDALC();
        }

        #region "TIPO_SALVOCONDUCTO"

        public DataTable Listar_Tipo_Salvoconducto(string strDescripcion)
        {
            return objTipoSalvoconductoDALC.Listar_Tipo_Salvoconducto(strDescripcion);
        }

        public void Insertar_Tipo_Salvoconducto(string strNombre, bool boActivo)
        {
            objTipoSalvoconductoDALC.Insertar_Tipo_Salvoconducto(strNombre, boActivo);
        }

        public void Actualizar_Tipo_Salvoconducto(int iID, string strNombre, bool boActivo)
        {
            objTipoSalvoconductoDALC.Actualizar_Tipo_Salvoconducto(iID, strNombre, boActivo);
        }

        public void Eliminar_Tipo_Salvoconducto(int iID)
        {
            objTipoSalvoconductoDALC.Eliminar_Tipo_Salvoconducto(iID);
        }

        #endregion

    }
}
