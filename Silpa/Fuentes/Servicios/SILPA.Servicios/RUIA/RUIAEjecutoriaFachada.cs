using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.Generico;
using System.Threading;

namespace SILPA.Servicios.RUIA
{
  public class RUIAEjecutoriaFachada
    {      

        /// <summary>
        /// Metodo delegado para utilizar enviar proceso de  actualizar fecha ejecutoria RUIA
        /// </summary>
        /// <param name="xmlDatos">string que simboliza los datos</param>
        public static void ActualizarSancionEjecutoria(object xmlDatos)
        {
            string strXmlDatos = (string)xmlDatos;
            SILPA.LogicaNegocio.RUIA.Sancion _objSancion = new SILPA.LogicaNegocio.RUIA.Sancion();
            _objSancion.ActualizarSancionEjecutoria(strXmlDatos);
        }

        /// <summary>
        /// Metodo delegado para utilizar enviar proceso de  actualizar fecha cumplimiento RUIA
        /// </summary>
        /// <param name="xmlDatos">string que simboliza los datos</param>
        public static void ActualizarSancion(object xmlDatos)
        {
            string strXmlDatos = (string)xmlDatos;
            SILPA.LogicaNegocio.RUIA.Sancion _objSancion = new SILPA.LogicaNegocio.RUIA.Sancion();
            _objSancion.ActualizarSancion(strXmlDatos);
        }
    }
}
