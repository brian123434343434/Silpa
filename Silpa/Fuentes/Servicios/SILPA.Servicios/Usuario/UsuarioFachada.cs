using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SILPA.LogicaNegocio.Generico;


namespace SILPA.Servicios.Usuario
{
   public class UsuarioFachada
    {      
        /// <summary>
        /// Metodo delegado para utilizar enviar peoceso de comunicacion EE con ThradPool
        /// </summary>
        /// <param name="xmlDatos">string que simboliza los datos completo a enviar a la entidad externa</param>
        public static void EnviarProceso(object xmlDatos)
        {
            string strXmlDatos = (string)xmlDatos;

            SILPA.LogicaNegocio.Usuario.Usuario _objUsuario = new SILPA.LogicaNegocio.Usuario.Usuario();
            _objUsuario.EnviarComunicacionAprobacionUsuario(strXmlDatos);
        }
        public static void ActivarUsuario(object xmlDatos)
        {
           string strXmlDatos = (string)xmlDatos;

           SILPA.LogicaNegocio.Usuario.Usuario _objUsuario = new SILPA.LogicaNegocio.Usuario.Usuario();
           _objUsuario.ActivarUsuario(strXmlDatos);
        }
    }
}
