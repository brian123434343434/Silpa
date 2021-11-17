using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class EstadoCorreo
    {
        private EstadoCorreoDALC objEstadoCorreoDALC;
        public EstadoCorreo()
        { 
            objEstadoCorreoDALC = new EstadoCorreoDALC();
        }

        #region "ESTADO_CORREO"

        public DataTable Listar_Estado_Correo(string strDescripcion)
        {
            return objEstadoCorreoDALC.Listar_Estado_Correo(strDescripcion);
        }

        public void Insertar_Estado_Correo(string strNombreEstado)
        {
            objEstadoCorreoDALC.Insertar_Estado_Correo(strNombreEstado);
        }

        public void Actualizar_Estado_Correo(int iID, string strNombreEstado)
        {
            objEstadoCorreoDALC.Actualizar_Estado_Correo(iID, strNombreEstado);
        }

        public void Eliminar_Estado_Correo(int iID)
        {
            objEstadoCorreoDALC.Eliminar_Estado_Correo(iID);
        }

        #endregion
    }


}
