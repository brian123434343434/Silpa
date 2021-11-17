using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class VAB_Validacion
    {
        private VAB_ValidacionDALC objValidacionDALC;
        public VAB_Validacion()
        {
            objValidacionDALC = new VAB_ValidacionDALC();
        }

        #region "VALIDACION"

        public DataTable Listar_Validacion(string strDescripcion)
        {
            return objValidacionDALC.Listar_Validacion(strDescripcion);
        }

        public void Insertar_Validacion(string strDescripcion, string strSentencia)
        {
            objValidacionDALC.Insertar_Validacion(strDescripcion, strSentencia);
        }

        public void Actualizar_Validacion(int iID, string strDescripcion, string strSentencia)
        {
            objValidacionDALC.Actualizar_Validacion(iID, strDescripcion, strSentencia);
        }

        public void Eliminar_Validacion(int iID)
        {
            objValidacionDALC.Eliminar_Validacion(iID);
        }

        #endregion

    }
}
