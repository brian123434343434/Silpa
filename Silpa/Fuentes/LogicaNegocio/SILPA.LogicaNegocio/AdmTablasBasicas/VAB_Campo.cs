using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;


namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class VAB_Campo
    {
        private VAB_CampoDALC objVAB_CampoDALC;
        public VAB_Campo()
        {
            objVAB_CampoDALC = new VAB_CampoDALC();
        }

        #region "ESTADO_CORREO"

        public DataTable Listar_Campos(string strDescripcion)
        {
            return objVAB_CampoDALC.Listar_Campo(strDescripcion);
        }

        public DataTable Cargar_Combo_TDatos()
        {
            return objVAB_CampoDALC.Carga_Combo_Tipo_Dato(); 
        }

        public void Insertar_Campos(string strID, string strDescripcion, Int32 iTipoDato)
        {
            objVAB_CampoDALC.Insertar_Campo(strID, strDescripcion, iTipoDato);
        }

        public void Actualizar_Campos(string strID, string strDescripcion, Int32 iTipoDato)
        {
            objVAB_CampoDALC.Actualizar_Campo(strID, strDescripcion, iTipoDato);
        }

        public void Eliminar_Campos(string strID)
        {
            objVAB_CampoDALC.Eliminar_Campo(strID);
        }

        #endregion

    }
}
