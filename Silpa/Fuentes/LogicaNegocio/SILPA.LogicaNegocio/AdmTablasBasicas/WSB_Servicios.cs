using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class WSB_Servicios
    {
        private WSB_ServiciosDALC objWSBServiciosDALC;
        public WSB_Servicios()
        {
            objWSBServiciosDALC = new WSB_ServiciosDALC();
        }

        #region "TIPO_DATO"

        public DataTable Listar_Servicios(string strDescripcion)
        {
            SMLog.Escribir(Severidad.Informativo, this.ToString() + ".Listar_Servicios.Inicio");
            return objWSBServiciosDALC.Listar_Servicios(strDescripcion);
            SMLog.Escribir(Severidad.Informativo, this.ToString() + ".Listar_Servicios.Finalizo");
        }

        public void Insertar_Servicios(string strNombre, string strURL, byte bActivo, int iPrioridad)
        {
            SMLog.Escribir(Severidad.Informativo, this.ToString() + ".Insertar_Servicios.Inicio");
            objWSBServiciosDALC.Insertar_Servicios(strNombre, strURL, bActivo, iPrioridad);
            SMLog.Escribir(Severidad.Informativo, this.ToString() + ".Insertar_Servicios.Finalizo");
        }

        public void Actualizar_Servicios(int iID, string strNombre, string strURL, byte bActivo, int iPrioridad)
        {
            SMLog.Escribir(Severidad.Informativo, this.ToString() + ".Actualizar_Servicios.Inicio");
            objWSBServiciosDALC.Actualizar_Servicios(iID, strNombre, strURL, bActivo, iPrioridad);
            SMLog.Escribir(Severidad.Informativo, this.ToString() + ".Actualizar_Servicios.Finalizo");
        }

        public void Eliminar_Servicios(int iID)
        {
            SMLog.Escribir(Severidad.Informativo, this.ToString() + ".Actualizar_Servicios.Inicio");
            objWSBServiciosDALC.Eliminar_Servicios(iID);
            SMLog.Escribir(Severidad.Informativo, this.ToString() + ".Actualizar_Servicios.Finalizo");
        }

        #endregion


    }
}
