using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class VAB_TipoDato
    {
        private VAB_TipoDatoDALC  objTipoDatoDALC;
        public VAB_TipoDato()
        {
            objTipoDatoDALC = new VAB_TipoDatoDALC();
        }

        #region "TIPO_DATO"

        public DataTable Listar_Tipo_Dato(string strDescripcion)
        {
            return objTipoDatoDALC.Listar_Tipo_Datos(strDescripcion);
        }

        public void Insertar_Tipo_Dato(string strDescripcion, string strSeparador)
        {
            objTipoDatoDALC.Insertar_Tipo_Datos(strDescripcion, strSeparador);
        }

        public void Actualizar_Tipo_Dato(int iID, string strDescripcion, string strSeparador)
        {
            objTipoDatoDALC.Actualizar_Tipo_Datos(iID, strDescripcion, strSeparador);
        }

        public void Eliminar_Tipo_Dato(int iID)
        {
            objTipoDatoDALC.Eliminar_Tipo_Datos(iID);
        }

        #endregion
    }
}
