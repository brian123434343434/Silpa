using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class VAB_ValidacionNegocio
    {
        private VAB_ValidacionNegocioDALC objValidacionNegocioDALC;
        public VAB_ValidacionNegocio()
        {
            objValidacionNegocioDALC = new VAB_ValidacionNegocioDALC();
        }

        #region "VALIDACION"

        public DataTable Listar_ValidacionNegocio(string strDescripcion)
        {
            return objValidacionNegocioDALC.Listar_ValidacionNegocio(strDescripcion);
        }

        public void Insertar_ValidacionNegocio(string strValidacion, string strProcedimiento, int iActivo)
        {
            objValidacionNegocioDALC.Insertar_ValidacionNegocio(strValidacion, strProcedimiento, iActivo);
        }

        public void Actualizar_ValidacionNegocio(int iID, string strValidacion, string strProcedimiento, int iActivo)
        {
            objValidacionNegocioDALC.Actualizar_ValidacionNegocio(iID, strValidacion, strProcedimiento, iActivo);
        }

        public void Eliminar_ValidacionNegocio(int iID)
        {
            objValidacionNegocioDALC.Eliminar_ValidacionNegocio(iID);
        }

        #endregion

    }
}
