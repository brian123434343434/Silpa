using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.Servicios.Comunicacion
{
    public class ComunicacionReunionFachada
    {
        public static void EnviarProceso(object xmlDatos)
        {
            try
            {
                string strXmlDatos = (string)xmlDatos;
                SILPA.LogicaNegocio.Comunicacion.ComunicacionReunion _objComunicacion = new SILPA.LogicaNegocio.Comunicacion.ComunicacionReunion();
                _objComunicacion.EnviarComunicacionReunion(strXmlDatos);
                _objComunicacion = null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Enviar Proceso de Comunicación Reunión.";
                throw new Exception(strException, ex);
            }
        }
        public static void EnviarProcesoInfoAdicional(object xmlDatos)
        {
            SILPA.LogicaNegocio.Comunicacion.ComunicacionReunion _objComunicacion = new SILPA.LogicaNegocio.Comunicacion.ComunicacionReunion();
            _objComunicacion.EnviarComunicacionReunionInfoAdicional(xmlDatos);
            _objComunicacion = null;
        }
    }
}
