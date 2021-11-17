using SILPA.AccesoDatos.Comunicacion;
using SILPA.AccesoDatos.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Comunicacion
{
    public class ComunicacionReunion
    {
        public ComunicacionReunionType ComIdentity;
        public List<ComunicacionReunionType> LstComIdentity;
        /// <summary>
        /// Constructor
        /// </summary>
        public ComunicacionReunion()
        {
        }
        /// <summary>
        /// Envia el correo de comunicacion de la visita al(os) solicitante(s).
        /// </summary>
        /// <param name="strXmlDatos">string en formato XML para el envio de la comunicacion</param>
        /// <returns>El identity de comunicacion Reunion serializado</returns>
        public void EnviarComunicacionReunion(string strXmlDatos)
        {
            try
            {
	            strXmlDatos = strXmlDatos.Replace(((char)'\n'), ' ');
	            strXmlDatos = strXmlDatos.Replace(((char)'\r'), ' ');
	            ComIdentity = new ComunicacionReunionType();
	            ComIdentity = (ComunicacionReunionType)ComIdentity.Deserializar(strXmlDatos);
	            ICorreo.Correo.EnviarComunicacionReunion(ComIdentity);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al enviar el correo de comunicación de la visita a los solicitantes.";
                throw new Exception(strException, ex);
            }
        }
        public void EnviarComunicacionReunionInfoAdicional(object strXmlDatos)
        {
            LstComIdentity = new List<ComunicacionReunionType>();
            LstComIdentity = (List<ComunicacionReunionType>)strXmlDatos;
            foreach (ComunicacionReunionType objComunicacionReunionType in LstComIdentity)
            {
                ICorreo.Correo.EnviarComunicacionReunion(objComunicacionReunionType);
            }
        }
    }
}
