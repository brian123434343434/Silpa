using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class PlantillaCorreo
    {
        private CorreoPlantillaDALC objCorreoPlantillaDALC;
        public PlantillaCorreo ()
        {
            objCorreoPlantillaDALC = new CorreoPlantillaDALC();
        }

        #region "ESTADO_CORREO"

        public DataTable Listar_Plantilla_Correo(string strDescripcion)
        {
            return objCorreoPlantillaDALC.Listar_Correo_Plantilla(strDescripcion);
        }

        public DataTable Cargar_Combo_Servidor()
        {
            return objCorreoPlantillaDALC.Carga_Combo_Servidores(); 
        }

        public void Insertar_Plantilla_Correo(string strDe, string strCC, string strPlantilla, string strAsunto, Int32 iIdCorreoServidor, Int32 iConfirmarEnvio)
        {
            objCorreoPlantillaDALC.Insertar_Correo_Plantilla(strDe, strCC, strPlantilla, strAsunto,iIdCorreoServidor, iConfirmarEnvio);
        }

        public void Actualizar_Plantilla_Correo(int iID, string strDe, string strCC, string strPlantilla, string strAsunto, Int32 iIdCorreoServidor, Int32 iConfirmarEnvio)
        {
            objCorreoPlantillaDALC.Actualizar_Correo_Plantilla(iID,strDe,strCC,strPlantilla,strAsunto,iIdCorreoServidor,iConfirmarEnvio);
        }

        public void Eliminar_Plantilla_Correo(int iID)
        {
            objCorreoPlantillaDALC.Eliminar_Correo_Plantilla(iID);
        }

        #endregion

    }
}
