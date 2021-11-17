using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Comunicacion;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;

namespace SILPA.LogicaNegocio.Comunicacion
{
    public class ComunicacionVisita
    {
        public ComunicacionVisitaType ComIdentity;
           
        /// <summary>
        /// Constructor
        /// </summary>
        public ComunicacionVisita()
        {
        }

        /// <summary>
        /// Envia el correo de comunicacion de la visita al(os) solicitante(s).
        /// </summary>
        /// <param name="strXmlDatos">string en formato XML para el envio de la comunicacion</param>
        /// <returns>El identity de comunicacion visita serializado</returns>
        public void EnviarComunicacionVisita(string strXmlDatos)
        {
            ComIdentity = new ComunicacionVisitaType();
            ComIdentity = (ComunicacionVisitaType)ComIdentity.Deserializar(strXmlDatos);
            
            //Se busca la persona
            PersonaDalc _objPersonaDalc = new PersonaDalc();
            //PersonaIdentity _objPersona = new PersonaIdentity();
            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();

            _listaPersona = _objPersonaDalc.BuscarPersonaNumeroVITAL(ComIdentity.numSilpa);
            //Se envia el correo por cada persona o solicitante
            foreach (PersonaIdentity _objPersona in _listaPersona)
                ICorreo.Correo.EnviarComunicacionVisita(ComIdentity, _objPersona);
                    
            //WSRespuesta result = new WSRespuesta(ComIdentity.numSilpa, ComIdentity.numExpediente, "001", "Comunicación de visita enviada exitosamente", true);
            //return result.GetXml();
        }     
    }
}

