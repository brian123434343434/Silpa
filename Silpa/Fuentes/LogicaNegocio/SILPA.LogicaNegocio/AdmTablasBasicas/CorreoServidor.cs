using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
   public class CorreoServidor
    {
        private CorreoServidorDALC objCorreoServiodorDALC;
        public CorreoServidor()
        {
            objCorreoServiodorDALC = new CorreoServidorDALC();
        }

        #region "CORREO_SERVIDOR"

        public DataTable Listar_Correo_Servidor(string strDescripcion)
        {
            return objCorreoServiodorDALC.Listar_Servidor_Correo(strDescripcion);
        }

        public void Insertar_Correo_Servidor(string strNombreServidor, string strHost, string strUsuario, string strContrasena, string strSeparador, int Puerto)
        {
            objCorreoServiodorDALC.Insertar_Servidor_Correo(strNombreServidor, strHost, strUsuario, strContrasena, strSeparador, Puerto);
        }

        public void Actualizar_Correo_Servidor(int iID, string strNombreServidor, string strHost, string strUsuario, string strContrasena, string strSeparador, int Puerto)
        {
            objCorreoServiodorDALC.Actualizar_Servidor_Correo(iID, strNombreServidor, strHost, strUsuario, strContrasena, strSeparador, Puerto);
        }

        public void Eliminar_Correo_Servidor(int iID)
        {
            objCorreoServiodorDALC.Eliminar_Servidor_Correo(iID);
        }
       
        #endregion

    }
}
