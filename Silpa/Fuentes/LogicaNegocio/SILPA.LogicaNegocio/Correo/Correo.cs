using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Comunicacion;
using SILPA.AccesoDatos.Notificacion;
using SILPA.AccesoDatos.Salvoconducto;
using SILPA.AccesoDatos.Sancionatorio;
using SILPA.AccesoDatos.RUIA;
using SILPA.AccesoDatos.AudienciaPublica;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Usuario;
using SoftManagement.ServicioCorreoElectronico;
using SILPA.LogicaNegocio.Enumeracion;
using SoftManagement.Log;
using System.Configuration;
using SILPA.AccesoDatos.DAA;
using System.Linq;
using SILPA.LogicaNegocio.Contingencias;

namespace SILPA.LogicaNegocio.ICorreo
{
    public class Correo
    {

        /// <summary>
        /// Nombre de la Autoridad Ambiental que Envía el correo
        /// </summary>
        private static string _nombreAutoridadAmbiental;
        public static string NombreAutoridadAmbiental
        {
            get { return _nombreAutoridadAmbiental; }
            set { _nombreAutoridadAmbiental = value; }
        }

        /// <summary>
        /// Identificador de de la Autoridad Ambiental que Envía el correo
        /// </summary>
        private static int _idAutoridadAmbiental;
        public static int IdAutoridadAmbiental
        {
            get { return _idAutoridadAmbiental; }
            set
            {
                _idAutoridadAmbiental = value; 
                ObtenerNombreAutoridadAmbiental();
            }
        }


        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionVisitaFormato1(ComunicacionVisitaType comunicacion, PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Señor(a):</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>&nbsp;</p>");
            sb.Append("<p>Se ha programado la siguiente visita: ");
            sb.Append("<p>&nbsp;</p>");
            sb.Append("<table style=\"width: 500px;font:Tahoma\">");
            sb.Append("<tr>");
            sb.Append("	<td><strong>Número VITAL:</strong></td>");
            sb.Append("	<td>" + comunicacion.numSilpa + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Número Expediente:</strong></td>");
            sb.Append("<td>" + comunicacion.numExpediente + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Fecha Inicial:</strong></td>");
            sb.Append("<td>" + comunicacion.fechaInicial + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Fecha Final:</strong></td>");

            // hava:08-abr-10
            sb.Append("<td>" + comunicacion.fechaFinal + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Descripción:</strong></td>");
            sb.Append("<td>" + comunicacion.descripcionVisita + "</td>");
            sb.Append("</tr>");
            //para los responsables
            if (comunicacion.responsable != null)
            {
                foreach (ResponsableType responsable in comunicacion.responsable)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("<td>&nbsp;</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><strong>Nombre del responsable de la visita:</strong></td>");
                    sb.Append("<td>" + responsable.nombre + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><strong>Cédula del responsable de la visita:</strong></td>");
                    sb.Append("<td>" + responsable.cedula + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><strong>Cargo del responsable de la visita:</strong></td>");
                    sb.Append("<td>" + responsable.cargo + "</td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionCobroVencimientoFormato1(CobroIdentity cobro, PersonaIdentity persona, string nombreProyecto)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Señor(a):</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>El pago para la solicitud de trámite para el " + nombreProyecto + " se ha vencido en la fecha " + cobro.FechaVencimiento.ToShortDateString()
                     + " debido a que el sistema no registra su pago el proceso ha sido inactivado.");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionCobroVencimientoFormato1(CobroIdentity cobro, AutoridadAmbientalIdentity autoridad, List<PersonaIdentity> listaPersona, string nombreProyecto)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Autoridad Ambiental:</p>");
            sb.Append("<p>" + autoridad.Nombre + "</p>");
            sb.Append("<p>El pago para la solicitud de trámite para el " + nombreProyecto + " se ha vencido en la fecha " + cobro.FechaVencimiento.ToShortDateString()
                     + " debido a que el sistema no registra el pago el proceso ha sido inactivado.");
            sb.Append("<table style=\"width: 500px;font:Tahoma\">");
            foreach (PersonaIdentity persona in listaPersona)
            {
                sb.Append("<tr>");
                sb.Append("	<td><strong> " + persona.TipoPersona.NombreTipoPersona + ":</strong></td>");
                sb.Append("	<td>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");

            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionCobroRecordatorioFormato1(CobroIdentity cobro, PersonaIdentity persona, string nombreProyecto)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Señor(a):</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>El pago para su solicitud de trámite para el " + nombreProyecto + " vence en la fecha " + cobro.FechaVencimiento.ToShortDateString()
                      + " si el sistema no registra su pago antes de la fecha indicada el proceso será inactivado.");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string SolicitudRegistro1(PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Señor(a):</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>Su solicitud de credenciales está en proceso de aprobación.");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        public static void SolicitudRegistro(PersonaIdentity persona)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            //sb.Append("<head>");
            //sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            //sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            //sb.Append("<title>Oficio VITAL</title>");
            //sb.Append("</head>");
            //sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            //sb.Append("<p>Señor(a):</p>");
            //sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            //sb.Append("<p>Su solicitud de credenciales está en proceso de aprobación.");
            //sb.Append("</body>");
            //sb.Append("</html>");
            //return sb.ToString();

            // pendiente este cambio al componente de correo
            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("PERSONA", usuario.Trim());

            int plantillaID = (int)EnumPlantillaCorreo.SolicitudCredenciales;
            //correo.Adjuntos.Add("");           
            correo.Enviar(plantillaID);
        }



        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionSolFormato1(ComunicacionEEType comunicacion, PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Comunicación enviada por:</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>&nbsp;</p>");
            //sb.Append("<table style=\"width: 500px;font:Tahoma\">");
            //sb.Append("<tr>");
            //sb.Append("	<td><strong>Número VITAL:</strong></td>");
            //sb.Append("	<td>" + comunicacion.numSilpa + "</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><strong>Número Expediente:</strong></td>");
            //sb.Append("<td>" + comunicacion.numExpediente + "</td>");
            //sb.Append("</tr>");

            //sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionEEFormato1(ComunicacionEEType comunicacion, EntidadExternaType EET)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Comunicación enviada por:</p>");
            sb.Append("<p>" + EET.nombre + "</p>");
            sb.Append("<p>&nbsp;</p>");
            //sb.Append("<table style=\"width: 500px;font:Tahoma\">");
            //sb.Append("<tr>");
            //sb.Append("	<td><strong>Número VITAL:</strong></td>");
            //sb.Append("	<td>" + comunicacion.numSilpa + "</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><strong>Número Expediente:</strong></td>");
            //sb.Append("<td>" + comunicacion.numExpediente + "</td>");
            //sb.Append("</tr>");

            //sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string CorreoSalvoconductoFormato1(SalvoconductoIdentity comunicacion, PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Datos del Salvoconducto:</p>");
            sb.Append("<p>Tipo de Salvoconducto:</p>");
            sb.Append("<p>" + comunicacion.TipoSalvoconducto + "</p>");
            sb.Append("<p>Fecha de Radicación:</p>");
            sb.Append("<p>" + comunicacion.FechaDesde + "</p>");
            sb.Append("<p>Fecha de Vencimiento:</p>");
            sb.Append("<p>" + comunicacion.FechaHasta + "</p>");
            sb.Append("<p>Titular:</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>Clase de Recurso:</p>");
            sb.Append("<p>" + comunicacion.TipoRecursoFlora + "</p>");
            sb.Append("<p><b>Información de los Especímenes:<b></p>");
            foreach (EspecimenIdentity esp in comunicacion.especimen)
            {
                sb.Append("<p>" + esp.NombreCientifico + " " + esp.NombreComun + " " + esp.DescripcionEspecimen + " " + esp.IdentificacionEspecimen + " " + esp.CantidadEspecimen + " " + esp.UnidadMedida + " " + esp.DimensionesEspecimen + "</p>");
            }


            sb.Append("<p>&nbsp;</p>");
            //sb.Append("<table style=\"width: 500px;font:Tahoma\">");
            //sb.Append("<tr>");
            //sb.Append("	<td><strong>Número VITAL:</strong></td>");
            //sb.Append("	<td>" + comunicacion.numSilpa + "</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><strong>Número Expediente:</strong></td>");
            //sb.Append("<td>" + comunicacion.numExpediente + "</td>");
            //sb.Append("</tr>");

            //sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string CorreoRuiaFormato1(SancionType comunicacion, PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Datos del RUIA:</p>");
            sb.Append("<p>Tipo de Salvoconducto:</p>");
            sb.Append("<p>" + comunicacion.descripcionNorma + "</p>");
            sb.Append("<p>Fecha de Radicación:</p>");
            sb.Append("<p>" + comunicacion.fechaExpActo + "</p>");
            //sb.Append("<p>Fecha de Vencimiento:</p>");
            //sb.Append("<p>" + comunicacion.fechaHasta + "</p>");
            sb.Append("<p>Titular:</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");

            sb.Append("<p>&nbsp;</p>");
            //sb.Append("<table style=\"width: 500px;font:Tahoma\">");
            //sb.Append("<tr>");
            //sb.Append("	<td><strong>Número VITAL:</strong></td>");
            //sb.Append("	<td>" + comunicacion.numSilpa + "</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><strong>Número Expediente:</strong></td>");
            //sb.Append("<td>" + comunicacion.numExpediente + "</td>");
            //sb.Append("</tr>");

            //sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string CorreoSancionatorioFormato1(SancionatorioIdentity comunicacion, PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Respuesta a Queja o Denuncia:</p>");

            sb.Append("<p>&nbsp;</p>");
            sb.Append("<table style=\"width: 500px;font:Tahoma\">");
            sb.Append("<tr>");
            sb.Append("	<td><strong>Número VITAL:</strong></td>");
            sb.Append("	<td>" + comunicacion.NumeroSilpa + "</td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><strong>Número Expediente:</strong></td>");
            //sb.Append("<td>" + comunicacion.numExpediente + "</td>");
            //sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string CorreoAudienciaFormato1(AudienciaIdentity comunicacion, PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Audiencia Publica:</p>");

            sb.Append("<p>&nbsp;</p>");
            sb.Append("<table style=\"width: 500px;font:Tahoma\">");
            sb.Append("<tr>");
            sb.Append("	<td><strong>Nombre del Proyecto:</strong></td>");
            sb.Append("	<td>" + comunicacion.NombreProyecto + "</td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><strong>Número Expediente:</strong></td>");
            //sb.Append("<td>" + comunicacion.numExpediente + "</td>");
            //sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionAprobacionUsuario1(string motivo, PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Sr(a):</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>La solicitud realizada ha sido ");
            if (motivo != string.Empty)
            {
                sb.Append("<strong> Rechazada.</p>");
                sb.Append("<p>El motivo es " + motivo + ".</p>");
            }
            else
            {
                sb.Append("<strong> Aprobada.</p>");
                sb.Append("<p>Su usuario de acceso es " + persona.NumeroIdentificacion + " y su contraseña es " + persona.NumeroIdentificacion + "*</p>");
                sb.Append("<p>Tenga en cuenta que debe cambiarla para ingresar por primera vez al sistema.</p>");
            }
            sb.Append("</body>");
            sb.Append("</html>");

            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionAprobacionUsuario1(PersonaIdentity persona)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>La solicitud de credenciales del usuario:</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>con número de identificación " + persona.NumeroIdentificacion + " ha sido Aprobada y por lo tanto debe activarlo en el sistema.</p>");
            sb.Append("</body>");
            sb.Append("</html>");

            return sb.ToString();
        }

        /// <summary>
        /// Genera el cuerpo del mail que serà enviado una vez radicado
        /// </summary>
        /// <param name="Radicacion">Radicaciòn que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string RadicarFormato1(RadicacionDocumentoIdentity Radicacion, PersonaIdentity persona)
        {
            ///  se determina si es acto u oficio.
            ///  
            string tipoDocumento = string.Empty;
            string numeroSilpa = string.Empty;
            string NumeroExpediente = string.Empty;
            string Numero = string.Empty;
            string TipoDocumento = string.Empty;
            string Descripcion = string.Empty;
            DateTime Fecha = DateTime.Now;
            List<string> LstArchivosAdjuntos = new List<string>();

            if (Radicacion != null)
            {
                tipoDocumento = "Radicacion";
                numeroSilpa = Radicacion.NumeroSilpa;
                //NumeroExpediente = Radicacion.;
                Numero = Radicacion.NumeroRadicacionDocumento;
                Fecha = Radicacion.FechaRadicacion;
                //Descripcion = oficio.Descripcion;
                //LstArchivosAdjuntos = oficio.LstArchivosAdjuntos;

            }
            //else
            //{
            //    if (acto != null)
            //    {

            //        tipoDocumento = "Acto Vital";
            //        numeroSilpa = acto.NumeroSilpa;
            //        NumeroExpediente = acto.NumeroExpediente;
            //        Numero = acto.NumeroActo;
            //        Fecha = acto.FechaActo;
            //        Descripcion = acto.DescripcionActo;
            //        LstArchivosAdjuntos.Add(acto.UrlArchivosAdjuntos);
            //    }
            //}


            string listaArchivos = string.Empty;

            foreach (string str in LstArchivosAdjuntos)
            {
                listaArchivos = listaArchivos + " , " + str;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Señor(a):</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>Ciudad</p>");
            sb.Append("<p>&nbsp;</p>");
            sb.Append("<p>Su solicitud ha sido radicada, con la siguiente información: ");
            sb.Append("<table style=\"width: 400px;font:Tahoma\">");
            sb.Append("<tr>");
            sb.Append("	<td><strong>Número VITAL:</strong></td>");
            sb.Append("	<td>" + Radicacion.NumeroVITALCompleto + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Número Radicaciòn:</strong></td>");
            sb.Append("<td>" + Radicacion.NumeroRadicacionDocumento + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Fecha Radicaciòn :</strong></td>");
            sb.Append("<td>" + Radicacion.FechaRadicacion + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string CorreoFormato1(NotificacionEntity oficio, PersonaIdentity persona)
        {
            ///  se determina si es acto u oficio.
            ///  
            string tipoDocumento = string.Empty;
            string numeroSilpa = string.Empty;
            string NumeroExpediente = string.Empty;
            string Numero = string.Empty;
            string TipoDocumento = string.Empty;
            string Descripcion = string.Empty;
            DateTime Fecha = DateTime.Now;
            List<string> LstArchivosAdjuntos = new List<string>();

            if (oficio != null)
            {
                tipoDocumento = oficio.IdTipoActo.Nombre;
                numeroSilpa = oficio.NumeroSILPA;
                NumeroExpediente = oficio.ProcesoAdministracion;
                Numero = oficio.NumeroActoAdministrativo;
                Descripcion = oficio.ParteResolutiva;
                //LstArchivosAdjuntos = oficio.

            }

            string listaArchivos = string.Empty;

            //foreach (string str in LstArchivosAdjuntos)
            //{
            //    listaArchivos = listaArchivos + " , " + str;
            //}


            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>" + tipoDocumento + "</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Señor(a):</p>");
            sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>Ciudad</p>");
            sb.Append("<p>&nbsp;</p>");
            sb.Append("<p>Su solicitud ya fue procesada, a continuación se presenta la ");
            sb.Append("información del " + tipoDocumento + " de Respuesta de la Autoridad Ambiental</p>");
            sb.Append("<table style=\"width: 600px;font:Tahoma\">");
            sb.Append("<tr>");
            sb.Append("	<td><strong>Número VITAL:</strong></td>");
            sb.Append("	<td>" + numeroSilpa + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Número Expediente:</strong></td>");
            sb.Append("<td>" + NumeroExpediente + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Número de Documento:</strong></td>");
            sb.Append("<td>" + Numero + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Fecha de Documento:</strong></td>");
            sb.Append("<td>" + Fecha + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Tipo de Documento:</strong></td>");
            sb.Append("<td>" + tipoDocumento + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Descripción:</strong></td>");
            sb.Append("<td>" + Descripcion + "</td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><strong>Archivos Adjuntos:</strong></td>");
            //sb.Append("<td>" + listaArchivos + "</td>");
            //sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Método para enviar oficio al solicitante
        /// </summary>
        /// <param name="oficio">Oficio de Tipo NotificacionType</param>
        /// <param name="persona">Persona a quién se envia el oficio</param>
        public static void EnviarOficio(NotificacionEntity oficio, PersonaIdentity persona, byte[] documento, string nombreArchivo)
        {
            ///  se determina si es acto u oficio.
            ///  
            string tipoDocumento = string.Empty;
            string numeroSilpa = string.Empty;
            string NumeroExpediente = string.Empty;
            string Numero = string.Empty;
            string TipoDocumento = string.Empty;
            string Descripcion = string.Empty;
            DateTime Fecha = DateTime.Now;
            List<string> LstArchivosAdjuntos = new List<string>();

            if (oficio != null)
            {
                tipoDocumento = oficio.IdTipoActo.Nombre;
                numeroSilpa = oficio.NumeroSILPA;
                NumeroExpediente = oficio.ProcesoAdministracion;
                Numero = oficio.NumeroActoAdministrativo;
                Descripcion = oficio.ParteResolutiva;
            }

            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            bool flag = false;
            string _nombreArchivo = "";
            if (nombreArchivo == null)
            {
                _nombreArchivo = "";
                SMLog.Escribir(Severidad.Informativo, "EnvioOficio: nombreArchivo es null");
            }
            else
                _nombreArchivo = nombreArchivo;


            SMLog.Escribir(Severidad.Informativo, "EnvioOficio: nombreArchivo" + nombreArchivo + "*");

            if (!_nombreArchivo.Equals(""))
                flag = true;

            SMLog.Escribir(Severidad.Informativo, "EnvioOficio: flag" + flag.ToString());

            string archivo = ConfigurationManager.AppSettings["ArchivosTemporales"];
            if (!Directory.Exists(archivo)) Directory.CreateDirectory(archivo);

            if (flag == true)
            {
                archivo += "\\" + nombreArchivo;
                archivo = BuscarNombreNuevoArchivoFisico(archivo);

                if (documento != null)
                {
                    using (BinaryWriter binWriter = new BinaryWriter(File.Open(archivo, FileMode.Create)))
                    {
                        binWriter.Write(documento);
                    }
                }
            }

            //Obtiene el nombre de la AA mediante el número silpa
            ObtenerIdAAPorNumeroSilpa(oficio.NumeroSILPA);

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();

            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("TIPO_DOCUMENTO", tipoDocumento);
            correo.Tokens.Add("NUMERO_SILPA", numeroSilpa);
            correo.Tokens.Add("NUMERO_EXPEDIENTE", NumeroExpediente);
            correo.Tokens.Add("NUMERO_DOCUMENTO", Numero);
            correo.Tokens.Add("FECHA_DOCUMENTO", Fecha.ToShortDateString());
            correo.Tokens.Add("DESCRIPCION", Descripcion);
            correo.Tokens.Add("NOMBRE_ACTO", oficio.IdTipoActo.Nombre);
            correo.Tokens.Add("NUMERO_ACTO", oficio.NumeroActoAdministrativo);


            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", NombreAutoridadAmbiental);

            
            int plantillaID = (int)EnumPlantillaCorreo.EnviarOficio;
            if (flag == true)
                correo.Adjuntos.Add(archivo);
            correo.Enviar(plantillaID);

        }

        protected static string BuscarNombreNuevoArchivoFisico(string archivoFisico)
        {
            if (File.Exists(archivoFisico))
            {
                string ruta = Path.GetDirectoryName(archivoFisico);
                string nombreArchivo;
                string nombreArchivoCompleto = Path.GetFileName(archivoFisico);
                int posPunto = nombreArchivoCompleto.IndexOf(".");
                string extension;
                if (posPunto > 0)
                {
                    nombreArchivo = nombreArchivoCompleto.Substring(0, posPunto);
                    extension = nombreArchivoCompleto.Substring(posPunto, nombreArchivoCompleto.Length - posPunto);
                }
                else
                {
                    nombreArchivo = nombreArchivoCompleto;
                    extension = string.Empty;
                }

                Random aleatorio = new Random();
                int consecutivo = aleatorio.Next(99);
                string nuevoArchivoFisico = string.Format("{0}\\\\{1}_{2}{3}", ruta, nombreArchivo, consecutivo, extension);
                return BuscarNombreNuevoArchivoFisico(nuevoArchivoFisico);
            }
            else
            {
                return archivoFisico;
            }
        }


        /// <summary>
        /// Método para enviar poficio al solicitante
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        public static void EnviarComunicacionVisita(ComunicacionVisitaType comunicacion, PersonaIdentity persona)
        {
            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;

            // Se setea el nombre de la Autoridad Ambiental y su id
            ObtenerIdAAPorNumeroSilpa(comunicacion.numSilpa);

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("NUMERO_SILPA", comunicacion.numSilpa);
            correo.Tokens.Add("NUMERO_EXPEDIENTE", comunicacion.numExpediente);
            correo.Tokens.Add("FECHA_INICIAL", comunicacion.fechaInicial);
            correo.Tokens.Add("FECHA_FINAL", comunicacion.fechaFinal);
            correo.Tokens.Add("DESCRIPCION", comunicacion.descripcionVisita);

            StringBuilder responsables = new StringBuilder();
            foreach (ResponsableType responsable in comunicacion.responsable)
            {
                responsables.Append(Environment.NewLine);
                responsables.Append(Environment.NewLine + "Nombre: " + responsable.nombre);
                responsables.Append(Environment.NewLine + "Cédula: " + responsable.cedula);
                responsables.Append(Environment.NewLine + "Cargo: " + responsable.cargo);
            }

            correo.Tokens.Add("RESPONSABLES", responsables.ToString());


            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", NombreAutoridadAmbiental);

            int plantillaID = (int)EnumPlantillaCorreo.ComunicacionVisita;
            //correo.Adjuntos.Add("");           
            correo.Enviar(plantillaID);
        }
        

        public static void EnviarComunicacionReunion(ComunicacionReunionType comunicacion)
        {
            try
            {
	            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
	            correo.Para.Add(comunicacion.CorreoElectronico);
	            foreach (string CorreoResponsable in comunicacion.Responsables)
	            {
	                correo.Cc.Add(CorreoResponsable);
	            }
	            if(comunicacion.CC != string.Empty)
	                correo.Cc.Add(comunicacion.CC);
	            correo.Tokens.Add("SOLICITANTE", comunicacion.Usuario);
	            correo.Tokens.Add("FECHA_REUNION", comunicacion.FechaReunion.ToString());
	            correo.Tokens.Add("SALA_REUNION", comunicacion.Sala);
	            correo.Tokens.Add("NUR", comunicacion.NUR);
	            correo.Tokens.Add("NOMBRE_EXPEDIENTE", comunicacion.NombreExpediente);
	            correo.Tokens.Add("NUMERO_VITAL", comunicacion.NumeroVital);
	            correo.Tokens.Add("AUTORIDAD_AMB", comunicacion.AutoridadAmbiental);
	            correo.Tokens.Add("DIRECCION", comunicacion.Direccion);
	            correo.Tokens.Add("PIE_PAGINA", comunicacion.PieMensaje);
	            correo.Tokens.Add("FECHA_FINALIZA_ACTIVIDAD", comunicacion.FechaFinalizacionActividad.ToShortDateString());
	            correo.Tokens.Add("COD_EXPEDIENTE", comunicacion.CodExpediente);
	            correo.Tokens.Add("FECHA_RADICACION", comunicacion.FechaRadicacion.ToShortDateString());

	            int plantillaID = comunicacion.PlantillaID;
	            //(int)EnumPlantillaCorreo.ComunicacionReunion;
	            correo.Enviar(plantillaID);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al enviar el correo de comunicación de la visita a los solicitantes.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Metodo para enviar correo al 
        /// </summary>
        /// <param name="comunicacion"></param>
        /// <param name="persona"></param>
        public static void EnviarComunicacionSol(ComunicacionEEType comunicacion, PersonaIdentity persona)
        {
            //List<string> LstArchivosAdjuntos = new List<string>();
            List<string> LstArchivosAdjuntos = new List<string>();
           //List<Byte[]> LstArchivosAdjuntosBytes = new List<Byte[]>();

            foreach (documentoAdjuntoType doc in comunicacion.ListaDocumentoAdjuntoType.ListaDocumento)
            {
                LstArchivosAdjuntos.Add(doc.nombreArchivo);
                //LstArchivosAdjuntosBytes.Add(doc.bytes);
            }

            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            
            string entidadOrigen = string.Empty;

            /// se obtiene el nombre de la autoridad ambiental origen:
            AutoridadAmbientalIdentity aut = new AutoridadAmbientalIdentity();
            aut.IdAutoridad = comunicacion.Id_AA_Origen;
            entidadOrigen = aut.Nombre;

            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("NOMBRE_ENTIDAD", entidadOrigen);
            correo.Tokens.Add("NUMERO_SILPA", comunicacion.numSilpa);
            correo.Tokens.Add("NUM_EXPEDIENTE", comunicacion.numExpediente);
            correo.Tokens.Add("DESCRIPCION", comunicacion.Descripcion);

            //LstArchivosAdjuntos  = comunicacion.ListaDocumentoAdjuntoType.ListaDocumento
            int plantillaID = (int)EnumPlantillaCorreo.ComunicacionSolicitud;
            foreach (string str in LstArchivosAdjuntos)
            {
                correo.Adjuntos.Add(str);
            }
            correo.Enviar(plantillaID);
        }

        /// <summary>
        /// Metodo para enviar correo al 
        /// </summary>
        /// <param name="comunicacion"></param>
        /// <param name="persona"></param>
        public static void EnviarComunicacionEE(ComunicacionEEType comunicacion, EntidadExternaType EET, bool bRespuesta)
        {
            List<string> LstArchivosAdjuntos = new List<string>();
            List<Byte[]> LstArchivosAdjuntosBytes = new List<Byte[]>();

            foreach (documentoAdjuntoType doc in comunicacion.ListaDocumentoAdjuntoType.ListaDocumento)
            {
                LstArchivosAdjuntos.Add(doc.nombreArchivo);
                //LstArchivosAdjuntosBytes.Add(doc.bytes);
            }

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();

            string entidadOrigen = string.Empty;
            string entidadDestino = string.Empty;

            /// se obtiene el nombre de la autoridad ambiental origen:
            AutoridadAmbientalIdentity aut = new AutoridadAmbientalIdentity();
            aut.IdAutoridad = comunicacion.Id_AA_Origen;
            AutoridadAmbientalDalc autDalc = new AutoridadAmbientalDalc();

            // HAVA: 13 - dic - 2010
            // Consulta la AA sin filtro de Ventanilla Integrada.
            //autDalc.ObtenerAutoridadById(ref aut);
            autDalc.ObtenerAutoridadNoIntegradaById(ref aut);

            entidadOrigen = aut.Nombre;

            //aut.IdAutoridad = comunicacion.Id_AA_Destino;
            //autDalc.ObtenerAutoridadById(ref aut);
            //entidadDestino = aut.Nombre;

            correo.Para.Add(EET.correoElectronico);
            //correo.CC.Add();
            //correo.Tokens.Add("NOMBRE_ENTIDAD", EET.nombre);
            correo.Tokens.Add("NOMBRE_ENTIDAD", entidadOrigen);
            correo.Tokens.Add("NUM_EXPEDIENTE", comunicacion.numExpediente);
            correo.Tokens.Add("NUMERO_SILPA", comunicacion.numSilpa);
            correo.Tokens.Add("DESCRIPCION", comunicacion.Descripcion);

            int plantillaID = (int)EnumPlantillaCorreo.ComunicacionSolicitudE;
            //07-jul-2010 - aegb
            if (bRespuesta)
                plantillaID = (int)EnumPlantillaCorreo.RespuestaComunicacionSolicitudE;

            foreach (string str in LstArchivosAdjuntos)
            {
                correo.Adjuntos.Add(str);
            }
            correo.Enviar(plantillaID);
        }

        /// <summary>
        /// Método para enviar al solicitante
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        public static void EnviarRadicacion(RadicacionDocumentoIdentity Radicacion, PersonaIdentity persona)
        {
            if (persona == null)
            {
                return;
            }
            AutoridadAmbientalDalc objAutoridad = new AutoridadAmbientalDalc();
            DataSet _dsDatos_AA = objAutoridad.ListarAutoridadAmbiental(Radicacion.IdAA);
            String str_Autoridad_Ambiental = "";
            if (_dsDatos_AA.Tables[0].Rows.Count > 0)
            {
                str_Autoridad_Ambiental = _dsDatos_AA.Tables[0].Rows[0]["AUT_DESCRIPCION"].ToString();
            }
            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("NUMERO_VITAL_COMPLETO", Radicacion.NumeroVITALCompleto);
            correo.Tokens.Add("NUMERO_RADICACION", Radicacion.NumeroRadicacionDocumento);
            correo.Tokens.Add("FECHA_RADICACION", Radicacion.FechaRadicacion.ToShortDateString());
            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", str_Autoridad_Ambiental);
            
            int plantillaID = (int)EnumPlantillaCorreo.EnviarRadicacion;
            //correo.Adjuntos.Add("");          
            correo.Enviar(plantillaID);
        }
        
        /// <summary>
        /// Método para enviar al solicitante el correo de radicacion no habilitado
        /// el correo se habilita para envío cuando sea radicada la solicitud por parte de la AA
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        //public static void EnviarRadicacionInHabilitado(RadicacionDocumentoIdentity Radicacion, PersonaIdentity persona)
        //{
        //    string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
        //    ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
        //    correo.Para.Add(persona.CorreoElectronico);
        //    //correo.CC.Add();
        //    correo.Tokens.Add("USUARIO", usuario.Trim());
        //    correo.Tokens.Add("NUMERO_VITAL_COMPLETO", Radicacion.NumeroVITALCompleto);
        //    correo.Tokens.Add("NUMERO_RADICACION", Radicacion.NumeroRadicacionDocumento);
        //    correo.Tokens.Add("FECHA_RADICACION", Radicacion.FechaRadicacion.ToShortDateString());
        //    int plantillaID = (int)EnumPlantillaCorreo.EnviarRadicacion;
        //    //correo.Adjuntos.Add("");          
        //    correo.Enviar(plantillaID);
        //}

        /// <summary>
        /// Método para enviar oficio por correo electónico a la autoridad ambiental
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        /// <param name="objAA"></param>
        public static void EnviarOficio(NotificacionEntity oficio, PersonaIdentity persona, AutoridadAmbientalIdentity objAA)
        {
            ///  se determina si es acto u oficio.
            ///  
            string tipoDocumento = string.Empty;
            string numeroSilpa = string.Empty;
            string NumeroExpediente = string.Empty;
            string Numero = string.Empty;
            string TipoDocumento = string.Empty;
            string Descripcion = string.Empty;
            DateTime Fecha = DateTime.Now;
            List<string> LstArchivosAdjuntos = new List<string>();

            if (oficio != null)
            {
                tipoDocumento = oficio.IdTipoActo.Nombre;
                numeroSilpa = oficio.NumeroSILPA;
                NumeroExpediente = oficio.ProcesoAdministracion;
                Numero = oficio.NumeroActoAdministrativo;
                Descripcion = oficio.ParteResolutiva;
            }

            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(objAA.Email);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("TIPO_DOCUMENTO", tipoDocumento);
            correo.Tokens.Add("NUMERO_SILPA", numeroSilpa);
            correo.Tokens.Add("NUMERO_EXPEDIENTE", NumeroExpediente);
            correo.Tokens.Add("NUMERO_DOCUMENTO", Numero);
            correo.Tokens.Add("FECHA_DOCUMENTO", Fecha.ToShortDateString());
            correo.Tokens.Add("DESCRIPCION", Descripcion);
            correo.Tokens.Add("NUMERO_ACTO", oficio.NumeroActoAdministrativo);

            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", objAA.Nombre);

            int plantillaID = (int)EnumPlantillaCorreo.EnviarOficioAA;
            //correo.Adjuntos.Add("");          
            correo.Enviar(plantillaID);


            //Comun.Correo _objCorreo = new Comun.Correo(null, objAA.Email, "Oficio No:" + oficio.NumeroActoAdministrativo
            //      + " del Trámite No. " + oficio.NumeroSILPA + " con TDR para EIA", CorreoFormato(oficio, persona), "");
        }
        
        public static void EnviarAcuseEnvio(Comun.Correo correoenviado)
        {
            Configuracion _configuration = new Configuracion();
            Comun.Correo _correoControl = new SILPA.Comun.Correo(null, _configuration.CuentaControl,
                "Acuse de Envío a " + correoenviado.To + " de: " + correoenviado.Subject, "SE ENVIÓ EXITOSAMENTE EL CORREO AL SOLICITANTE CON LA SIGUIENTE INFORMACIÓN:"
                + correoenviado.Body, "");
            _correoControl.EnviarCorreoFormato();
        }
                
        public static void EnvioNoExitoso(Comun.Correo correoenviado, string error)
        {
            Configuracion _configuration = new Configuracion();
            Comun.Correo _correoControl = new SILPA.Comun.Correo(null, _configuration.CuentaControl,
                "Envío no Exitoso a " + correoenviado.To + " de: " + correoenviado.Subject, "OCURRIÓ EL SIGUIENTE ERROR AL ENVIAR LA INFORMACIÓN: " + error
                + correoenviado.Body, "");
            _correoControl.EnviarCorreoFormato();
        }

        /// <summary>
        /// Método para enviar oficio al solicitante de vencimiento
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        public static void EnviarComunicacionVencimiento(CobroIdentity cobro, PersonaIdentity persona, string nombreProyecto)
        {

            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("NOMBRE_PROYECTO", nombreProyecto);
            correo.Tokens.Add("FECHA", cobro.FechaVencimiento.ToShortDateString());
            correo.Tokens.Add("NUMERO_SILPA", cobro.NumSILPA);
            correo.Tokens.Add("NOMBRE_CONCEPTO", cobro.ConceptoCobro.Nombre);
            int plantillaID = (int)EnumPlantillaCorreo.ComunicacionCobroVencimiento;
            //correo.Adjuntos.Add("");          
            correo.Enviar(plantillaID);
        }

        /// <summary>
        /// Método para enviar oficio a la arutoridad ambiental de vencimiento
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        public static void EnviarComunicacionVencimiento(CobroIdentity cobro, AutoridadAmbientalIdentity autoridad, List<PersonaIdentity> listaPersona, string nombreProyecto)
        {
            //string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerNombre + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(autoridad.EmailCorrespondencia);
            //correo.CC.Add();
            correo.Tokens.Add("NOMBRE_AUTORIDAD", autoridad.Nombre);
            correo.Tokens.Add("NOMBRE_PROYECTO", nombreProyecto);
            correo.Tokens.Add("FECHA_VENCIMIENTO", cobro.FechaVencimiento.ToShortDateString());
            StringBuilder sb = new StringBuilder();
            foreach (PersonaIdentity persona in listaPersona)
            {
                sb.Append("<tr>");
                sb.Append("	<td><strong> " + persona.TipoPersona.NombreTipoPersona + ":</strong></td>");
                sb.Append("	<td>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</td>");
                sb.Append("</tr>");
            }
            correo.Tokens.Add("LISTA_PERSONAS", sb.ToString());
            correo.Tokens.Add("NUMERO_SILPA", cobro.NumSILPA);
            correo.Tokens.Add("NOMBRE_CONCEPTO", cobro.ConceptoCobro.Nombre);

            int plantillaID = (int)EnumPlantillaCorreo.ComunicacionCobroVencimientoAA;
            //correo.Adjuntos.Add("");          
            correo.Enviar(plantillaID);
        }

        /// <summary>
        /// Método para enviar oficio al solicitante de recordatorio
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        public static void EnviarComunicacionRecordatorio(CobroIdentity cobro, PersonaIdentity persona, string nombreProyecto)
        {
            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;

            /// setea el id y nombre de la autoridad ambiental
            ObtenerIdAAPorNumeroSilpa(cobro.NumSILPA);

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();

            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("NOMBRE_PROYECTO", nombreProyecto);
            correo.Tokens.Add("FECHA_VENCIMIENTO", cobro.FechaVencimiento.ToShortDateString());
            correo.Tokens.Add("NUMERO_SILPA", cobro.NumSILPA);
            correo.Tokens.Add("NOMBRE_CONCEPTO", cobro.ConceptoCobro.Nombre);

            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", NombreAutoridadAmbiental);

            int plantillaID = (int)EnumPlantillaCorreo.ComunicacionRecordatorio;
            //correo.Adjuntos.Add("");          
            correo.Enviar(plantillaID);
        }

        /// <summary>
        /// Metodo para enviar correo al 
        /// </summary>
        /// <param name="comunicacion"></param>
        /// <param name="persona"></param>
        public static void EnviarCorreoSalvoconducto(SalvoconductoIdentity comunicacion, PersonaIdentity persona)
        {
            List<string> LstArchivosAdjuntos = new List<string>();

            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;

            /// Setea el identificador y nombre de la autoridad ambiental.
            ObtenerIdAAPorNumeroSilpa(comunicacion.NumeroSilpa);

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("TIPO_SALVOCONDUCTO", comunicacion.TipoSalvoconducto);
            correo.Tokens.Add("FECHA_RADICACION", comunicacion.FechaDesde);
            correo.Tokens.Add("FECHA_VENCIMIENTO", comunicacion.FechaHasta);
            correo.Tokens.Add("TITULAR", usuario.Trim());
            correo.Tokens.Add("TIPO_RECURSO", comunicacion.TipoRecursoFlora);
            correo.Tokens.Add("NUMERO_SALVOCONDUCTO", comunicacion.NumeroSalvoconducto);


            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", NombreAutoridadAmbiental);


            StringBuilder sb = new StringBuilder();
            foreach (EspecimenIdentity esp in comunicacion.especimen)
            {
                sb.Append("<p>" + esp.NombreCientifico + " " + esp.NombreComun + " " + esp.DescripcionEspecimen + " " + esp.IdentificacionEspecimen + " " + esp.CantidadEspecimen + " " + esp.UnidadMedida + " " + esp.DimensionesEspecimen + "</p>");
            }
            correo.Tokens.Add("ESPECIMENES", sb.ToString());
            int plantillaID = (int)EnumPlantillaCorreo.Salvoconducto;

            foreach (string str in LstArchivosAdjuntos)
            {
                correo.Adjuntos.Add(str);
            }
            correo.Enviar(plantillaID);
        }

        /// <summary>
        /// Metodo para enviar correo al 
        /// </summary>
        /// <param name="comunicacion"></param>
        /// <param name="persona"></param>
        public static void EnviarCorreoRuia1(SancionType comunicacion, PersonaIdentity persona)
        {

            List<string> LstArchivosAdjuntos = new List<string>();

            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();


            IdAutoridadAmbiental = comunicacion.autoridadId;

            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("DESCRIPCION_NORMA", comunicacion.descripcionNorma);
            correo.Tokens.Add("FECHA_RADICACION", comunicacion.fechaExpActo);
            correo.Tokens.Add("TITULAR", usuario.Trim());
            correo.Tokens.Add("NUMERO_ACTO", comunicacion.numeroActo);

            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", NombreAutoridadAmbiental);

            int plantillaID = (int)EnumPlantillaCorreo.CorreoRuia;
            foreach (string str in LstArchivosAdjuntos)
            {
                correo.Adjuntos.Add(str);
            }
            correo.Enviar(plantillaID);
        }

        public static void EnviarCorreoSancionatorio(SancionatorioIdentity comunicacion, PersonaIdentity persona)
        {
            List<string> LstArchivosAdjuntos = new List<string>();

            ObtenerIdAAPorNumeroSilpa(comunicacion.NumeroSilpa);

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("NUMERO_SILPA", comunicacion.NumeroSilpa);
            correo.Tokens.Add("NUMERO_RADICACION", comunicacion.NumeroRadicacion);

            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", NombreAutoridadAmbiental);

            int plantillaID = (int)EnumPlantillaCorreo.CorreoSancionatorio;
            //correo.Adjuntos.Add("");          
            foreach (string str in LstArchivosAdjuntos)
            {
                correo.Adjuntos.Add(str);
            }

            correo.Enviar(plantillaID);

        }
     
        public static void EnviarCorreoAudiencia(AudienciaIdentity comunicacion, PersonaIdentity persona)
        {
            try
            {
	            List<string> LstArchivosAdjuntos = new List<string>();

	            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
	
	            ///Se setea el Id y el nombre de la autoridad Ambiental
	            IdAutoridadAmbiental = comunicacion.IdAutoridad;
	            
	            correo.Para.Add(persona.CorreoElectronico);
	            //correo.CC.Add();
	            correo.Tokens.Add("NOMBRE_PROYECTO", comunicacion.NombreProyecto);
	            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", NombreAutoridadAmbiental);
	
	            int plantillaID = (int)EnumPlantillaCorreo.CorreoAudiencia;
	            //correo.Adjuntos.Add("");      
	            foreach (string str in LstArchivosAdjuntos)
	            {
	                correo.Adjuntos.Add(str);
	
	            }
	            correo.Enviar(plantillaID);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Enviar Correo Audiencia.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Enviar correo electronico correspondencia AA asuntos de Audiencia Pública
        /// </summary>
        /// <param name="comunicacion"></param>
        /// <param name="persona"></param>
        public static void EnviarCorrespondenciaAutoridadAmbiental(CorreoAudienciaIdentity datos, string correoAutoridadAmbiental)
        {
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(correoAutoridadAmbiental);


            // Se obtiene el identificador de la Autoridad ambiental y su nombre
            ObtenerIdAAPorNumeroSilpa(datos.NumeroSilpaSolicitud);

            //correo.CC.Add();
            correo.Tokens.Add("NUMERO_SILPA_SOLICITUD", datos.NumeroSilpaSolicitud);
            correo.Tokens.Add("NOMBRE_ARCHIVOS", datos.nombreArchivos);
            
            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", _nombreAutoridadAmbiental);

            int plantillaID = (int)EnumPlantillaCorreo.CorrespondenciaAA;
            //correo.Adjuntos.Add("");          
            correo.Enviar(plantillaID);

            //Comun.Correo _objCorreo = new Comun.Correo(null, correoAutoridadAmbiental, "Inscripción para Audiencia Pública: " + datos.NumeroSilpaSolicitud,
            //                                           CorreoAudienciaFormatoAutoridadAmbiental(datos), datos.listaArchivos);

        }

        /// <summary>
        /// Enviar correo electronico al ciudadano Asunto Audiencia Pública
        /// </summary>
        /// <param name="comunicacion"></param>
        /// <param name="persona"></param>
        public static void EnviarCorrespondenciaCiudadano(CorreoAudienciaIdentity datos, PersonaIdentity persona)
        {

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();

            // Se obtiene el identificador de la Autoridad ambiental y su nombre
            ObtenerIdAAPorNumeroSilpa(datos.NumeroSilpaSolicitud);

            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("NUMERO_SILPA_SOLICITUD", datos.NumeroSilpaSolicitud);
            correo.Tokens.Add("NOMBRE_ARCHIVOS", datos.nombreArchivos);
            correo.Tokens.Add("NUMERO_SILPA_INSCRIPCION", datos.NumeroSilpaInscripcion);

            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", _nombreAutoridadAmbiental);

            int plantillaID = (int)EnumPlantillaCorreo.CorrespondenciaCiudadano;
            //correo.Adjuntos.Add("");          
            correo.Enviar(plantillaID);

            //Comun.Correo _objCorreo = new Comun.Correo(null, persona.CorreoElectronico, "Inscripción: " + datos.NumeroSilpaInscripcion +", para la audiencia: "+datos.NumeroSilpaSolicitud,
            //                                           CorreoAudienciaFormatoCiudadano(datos), "");

        }

        /// <summary>
        /// Método para enviar al solicitante la informaciond e aprobacion o rechazo de la solicitud de credencial
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        public static void EnviarCorreoAprobacionUsuario(string motivo, PersonaIdentity persona)
        {
            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(persona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("IDENTIFICACION", persona.NumeroIdentificacion);

            NombreAutoridadAmbiental = ObtenerNombreAutoridadPorPersona(persona.IdSolicitante);

            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", NombreAutoridadAmbiental);

            int plantillaID = (int)EnumPlantillaCorreo.AprobacionUsuario;
            if (motivo != string.Empty)
            {
                correo.Tokens.Add("MOTIVO", motivo.Trim());
                plantillaID = (int)EnumPlantillaCorreo.RechazoUsuario;
            }
            //correo.Adjuntos.Add("");           
            correo.Enviar(plantillaID);
        }

        /// <summary>
        /// Método para enviar al solicitante la informaciond e aprobacion o rechazo de la solicitud de credencial
        /// </summary>
        /// <param name="oficio"></param>
        /// <param name="persona"></param>
        public static void EnviarCorreoAprobacionUsuario(PersonaIdentity persona, string correoAdministrador)
        {
            string usuario = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(correoAdministrador);
            //correo.CC.Add();
            correo.Tokens.Add("USUARIO", usuario.Trim());
            correo.Tokens.Add("IDENTIFICACION", persona.NumeroIdentificacion);
            int plantillaID = (int)EnumPlantillaCorreo.AprobacionUsuarioAdmin;
            //correo.Adjuntos.Add("");           
            correo.Enviar(plantillaID);
        }


        /// <summary>
        /// Enviar correo para que el usuario confirme y se active la cuenta
        /// </summary>
        /// <param name="p_strCorreo">string con el usuario registrado por el usuario</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_strNombreUsuario">string con el nombre del usuario</param>
        /// <param name="p_strLinkAcceso">string con el link de acceso</param>
        /// <param name="p_strAutoridadAmbiental">string con el nombre de la autoridad ambiental</param>
        public static void EnviarCorreoConfirmacionCorreo(string p_strCorreo, string p_strNumeroIdentificacion, string p_strNombreUsuario, string p_strLinkAcceso, string p_strAutoridadAmbiental)
        {
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(p_strCorreo);
            correo.Tokens.Add("IDENTIFICACION", p_strNumeroIdentificacion);
            correo.Tokens.Add("USUARIO", p_strNombreUsuario);
            correo.Tokens.Add("LINK_ACCESO", p_strLinkAcceso);
            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", p_strAutoridadAmbiental);
            int plantillaID = (int)EnumPlantillaCorreo.RegistroUsuarioConfirmacionCorreo;
            correo.Enviar(plantillaID);
        }


        /// <summary>
        /// Formato de correo electronico correspondencia AA
        /// </summary>
        /// <param name="documentos"></param>
        /// <returns></returns>
        public static string CorreoAudienciaFormatoAutoridadAmbiental1(CorreoAudienciaIdentity datos)
        {
            DateTime Fecha = DateTime.Now;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Inscripción a Audiencia Pública</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<table style=\"width: 400px;font:Tahoma\">");
            sb.Append("<tr>");
            sb.Append("	<td><strong>Solicitud de audiencia Pública No.</strong></td>");
            sb.Append("	<td>" + datos.NumeroSilpaSolicitud + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Archivos Adjuntos:</strong></td>");
            sb.Append("<td>" + datos.nombreArchivos + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Formato de correo electronico correspondencia AA
        /// </summary>
        /// <param name="documentos"></param>
        /// <returns></returns>
        public static string CorreoAudienciaFormatoCiudadano1(CorreoAudienciaIdentity datos)
        {
            DateTime Fecha = DateTime.Now;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Inscripción a Audiencia Pública</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<table style=\"width: 400px;font:Tahoma\">");
            sb.Append("<tr>");
            sb.Append("	<td><strong>Solicitud de audiencia Pública No.</strong></td>");
            sb.Append("	<td>" + datos.NumeroSilpaSolicitud + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Archivos Adjuntos:</strong></td>");
            sb.Append("<td>" + datos.nombreArchivos + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Información:</strong></td>");
            sb.Append("<td>" + datos.nombreArchivos + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Metodo para enviar correo al 
        /// </summary>
        /// <param name="comunicacion"></param>
        /// <param name="persona"></param>
        public static void EnviarComunicacionEERespuesta(string numeroExpediente, string nombre, string correoElectronico, string fechaEnvio)
        {

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(correoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("NOMBRE_ENTIDAD", nombre);
            correo.Tokens.Add("FECHA_ENVIO", fechaEnvio);
            correo.Tokens.Add("NUMERO_EXPEDIENTE", numeroExpediente);
            int plantillaID = (int)EnumPlantillaCorreo.ComunicacionEE;
            //correo.Adjuntos.Add("");          
            correo.Enviar(plantillaID);

            //Comun.Correo _objCorreo = new Comun.Correo(null, correoElectronico, "Comunicación de Entidad Respuesta: " + numeroExpediente,
            //                                           ComunicacionEEFormatoRespuesta(numeroExpediente, nombre, fechaEnvio), "");
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo envíado
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static string ComunicacionEEFormatoRespuesta1(string numeroExpediente, string nombre, string fechaEnvio)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Oficio VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Entidad:</p>");
            sb.Append("<p>" + nombre + "</p>");
            sb.Append("<p>A la fecha actual no se ha obtenido respuesta del correo enviado en la fecha " + fechaEnvio
                + " solicitandole información adicional para el expediente número " + numeroExpediente + ".");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        /// <summary>
        /// Convierte el Oficio a formato HTML para obtener una presentación más formal del correo para el paswword
        /// </summary>
        /// <param name="oficio">Oficio que se va a ingresar en el Formato HTML</param>
        /// <param name="persona">Persona a la Cual va dirigido el Oficio</param>
        /// <returns>Cadena con el formato para ser envíado en el cuerpo del correo</returns>
        public static void EnviarMailReestableceClave(PersonaIdentity objPersona)
        {

            List<string> LstArchivosAdjuntos = new List<string>();

            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(objPersona.CorreoElectronico);
            //correo.CC.Add();
            correo.Tokens.Add("NUMERO_IDENTIFICACION", objPersona.NumeroIdentificacion);            
            correo.Tokens.Add("NOMBRE", objPersona.PrimerNombre);            
            correo.Tokens.Add("APELLIDO", objPersona.PrimerApellido);            

            int plantillaID = (int)EnumPlantillaCorreo.ReestablecerContraseña;
            //correo.Adjuntos.Add("");          
            foreach (string str in LstArchivosAdjuntos)
            {
                correo.Adjuntos.Add(str);
            }
            correo.Enviar(plantillaID);

            //StringBuilder sb = new StringBuilder();
            //Comun.Correo _objCorreo;
            //string estado = "";
            //sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            //sb.Append("<head>");
            //sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            //sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            //sb.Append("<title>Oficio VITAL</title>");
            //sb.Append("</head>");
            //sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            //sb.Append("<p>Cordial Saludo: Sr.</p>");
            //sb.Append("<p>" + objPersona.PrimerNombre + " " + objPersona.PrimerApellido + "</p>");
            //sb.Append("<p>Su contraseña ha sido reestablecida, su contraseña actual es: " + objPersona.Username + "*</p>");
            //sb.Append("</body>");
            //sb.Append("</html>");


            //_objCorreo = new Comun.Correo(null, objPersona.CorreoElectronico, ,
            //   sb.ToString(), "");

            //estado = _objCorreo.EnviarCorreoFormato();
            //si estado es false el correo no fue envíado
            //if (estado.Equals(string.Empty))
            //{
            //    //se envía acuse de envío a la cuenta de control
            //    EnviarAcuseEnvio(_objCorreo);
            //}
            //else
            //{
            //    //Se envía correo a la cuenta de control indicando que él correo no fue envíado
            //    EnvioNoExitoso(_objCorreo, estado);
            //}

        }
        
        /// <summary>
        /// hava:06-oct-10
        /// </summary>
        /// <param name="numeroViltal"></param>
        /// <param name="justificacion"></param>
        /// <param name="aprobado"></param>
        public void EnviarCorreoFinalizaAudiencia(string email, string numeroViltal, string justificacion, string estadoAprobado, string nombrePersona, string Autoridad_Ambiental) 
        {
            try
			{
	            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
	            correo.Para.Add(email);
	            //correo.CC.Add();
	            correo.Tokens.Add("USUARIO", nombrePersona);
	            correo.Tokens.Add("NUMERO_VITAL", numeroViltal);
	            correo.Tokens.Add("JUSTIFICACION", justificacion);
	            correo.Tokens.Add("ESTADO_APROBADO", estadoAprobado);
	            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", Autoridad_Ambiental);
	            int?  plantillaID = (int)EnumPlantillaCorreo.FinalizarAudiencia;
	            //correo.Adjuntos.Add("");          
	            if (plantillaID.HasValue) 
	            {
	                correo.Enviar((int)plantillaID);
	            }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Enviar Correo Finaliza Audiencia.";
                throw new Exception(strException, ex);
            }
        }
        
        internal static void EnviarRadicacionAA(RadicacionDocumentoIdentity _objRadDocIdentity, AutoridadAmbientalIdentity objAutoridadIdentity, Persona objPersona)
        {
            ///  se determina si es acto u oficio.
            ///  
            string tipoDocumento = string.Empty;
            string numeroSilpa = string.Empty;
            string NumeroExpediente = string.Empty;
            string TipoDocumento = string.Empty;
            string Descripcion = string.Empty;
            List<string> LstArchivosAdjuntos = new List<string>();

            if (_objRadDocIdentity != null)
            {
                tipoDocumento = "Radicación";
                numeroSilpa = _objRadDocIdentity.NumeroSilpa;

                if (objPersona.Identity.NumeroIdentificacion == string.Empty || objPersona.Identity.NumeroIdentificacion == null)
                {
                    objPersona.Identity.NumeroIdentificacion = _objRadDocIdentity.IdentificacionSolicitante;
                }
                //NumeroExpediente = Radicacion.;
                //Descripcion = oficio.Descripcion;
                //LstArchivosAdjuntos = oficio.LstArchivosAdjuntos;

            }
            //else
            //{
            //    if (acto != null)
            //    {

            //        tipoDocumento = "Acto Vital";
            //        numeroSilpa = acto.NumeroSilpa;
            //        NumeroExpediente = acto.NumeroExpediente;
            //        Numero = acto.NumeroActo;
            //        Fecha = acto.FechaActo;
            //        Descripcion = acto.DescripcionActo;
            //        LstArchivosAdjuntos.Add(acto.UrlArchivosAdjuntos);
            //    }
            //}
            Parametrizacion.Parametrizacion clsParametrizacion = new Parametrizacion.Parametrizacion();
            string strUrlProyectoVital = string.Format(clsParametrizacion.ObtenerValorParametroGeneral(74,"-1"),_objRadDocIdentity.NumeroVITALCompleto,_objRadDocIdentity.IdSolicitante);


            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(objAutoridadIdentity.CorreoCorrespondencia);
            //correo.CC.Add();
            correo.Tokens.Add("NOMBREAA", objAutoridadIdentity.Nombre);
            correo.Tokens.Add("NIT", objAutoridadIdentity.NIT);
            correo.Tokens.Add("DIRECCION", objAutoridadIdentity.Direccion);
            correo.Tokens.Add("NUMERO_VITAL", _objRadDocIdentity.NumeroVITALCompleto);
            correo.Tokens.Add("IDENTIFICACION_SOLICITANTE", objPersona.Identity.NumeroIdentificacion);
            correo.Tokens.Add("FECHA_SOLICITUD", _objRadDocIdentity.FechaSolicitud.ToShortDateString());
            correo.Tokens.Add("URL_SOLCITUD_VITAL", strUrlProyectoVital);

            correo.Tokens.Add("NOMBRE_SOLICITANTE", objPersona.Identity.PrimerNombre + " " + objPersona.Identity.SegundoNombre + " " + objPersona.Identity.PrimerApellido + " " + objPersona.Identity.SegundoApellido);

            // Obtener el tipo de solicitud
            string idProcessInstance = _objRadDocIdentity.NumeroSilpa;

            if (!String.IsNullOrEmpty(_objRadDocIdentity.NumeroVITALCompleto))
            {
                NumeroSilpaDalc processInstance = new NumeroSilpaDalc();
                idProcessInstance = processInstance.ProcessInstance(_objRadDocIdentity.NumeroVITALCompleto);
            }

            SILPA.AccesoDatos.ReporteTramite.ReporteTramiteDalc rep = new SILPA.AccesoDatos.ReporteTramite.ReporteTramiteDalc();

            int? intIdProcessInstance = null;
            if (!String.IsNullOrEmpty(idProcessInstance))
            {
                intIdProcessInstance = Convert.ToInt32(idProcessInstance);
            }

            DataSet datos=null;

            if (intIdProcessInstance != null)
            {
                datos = rep.ListarReporteTramite(null, null, null, null, intIdProcessInstance);
            }
            
            if (datos != null)
            {
                if (datos.Tables[0].Rows.Count > 0)
                {
                    correo.Tokens.Add("TIPO_SOLICITUD", datos.Tables[0].Rows[0]["NOMBRE_TIPO_TRAMITE"].ToString());
                }
                else 
                {
                    if (intIdProcessInstance != null) 
                    {
                        Proceso proceso = new Proceso();
                        string nombreTipoTramite = proceso.ObtenerTipoTramiteByProcessInstance(Convert.ToInt64(intIdProcessInstance));
                        correo.Tokens.Add("TIPO_SOLICITUD", nombreTipoTramite);
                    }
                }
            }
            else
            {
                if (intIdProcessInstance != null)
                {
                    Proceso proceso = new Proceso();
                    string nombreTipoTramite = proceso.ObtenerTipoTramiteByProcessInstance(Convert.ToInt64(intIdProcessInstance));
                    correo.Tokens.Add("TIPO_SOLICITUD", nombreTipoTramite);
                }
            }
            int plantillaID = (int)EnumPlantillaCorreo.RadicacionAA;


            string rutaFus = string.Empty;

            ///SMLog.Escribir(Severidad.Informativo, "Entra buscar fus" + _objRadDocIdentity.NumeroVITALCompleto.ToString());

            //if (!String.IsNullOrEmpty(_objRadDocIdentity.NumeroVITALCompleto)) 
            //{
            //    RadicacionDocumento raddoc = new RadicacionDocumento();
            //    rutaFus = raddoc.ObtenerRutaFus(_objRadDocIdentity.NumeroVITALCompleto);

            //}

            if (_objRadDocIdentity.LstNombreDocumentoAdjunto != null)
            {
                foreach (string str in _objRadDocIdentity.LstNombreDocumentoAdjunto)
                {
                    correo.Adjuntos.Add(str);
                }
            }

            //Logica para agregar los RTF al correo electrónico - MIRM
            //if (_objRadDocIdentity.NumeroVITALCompleto.Length != 0)
            if (!String.IsNullOrEmpty(_objRadDocIdentity.NumeroVITALCompleto))
            {

                if (string.IsNullOrEmpty(_objRadDocIdentity.UbicacionDocumento))
                {
                    SILPA.Comun.TraficoDocumento objTrafico = new SILPA.Comun.TraficoDocumento();
                    _objRadDocIdentity.UbicacionDocumento = objTrafico.CrearDirectorio(objTrafico.FileTraffic, _objRadDocIdentity.NumeroSilpa, _objRadDocIdentity.IdSolicitante);
                    RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
                    if (_objRadDocIdentity.Id == 0)                    
                        objRadicarDocDalc.ActualizarRadicacionRuta(_objRadDocIdentity.IdRadicacion, _objRadDocIdentity.UbicacionDocumento);                    
                    else
                        objRadicarDocDalc.ActualizarRadicacionRuta(_objRadDocIdentity.Id, _objRadDocIdentity.UbicacionDocumento);
                }

                SILPA.AccesoDatos.Generico.NumeroSilpaDalc numero = new NumeroSilpaDalc();
                string instanciaProceso = numero.NumeroInstancia(_objRadDocIdentity.NumeroVITALCompleto);
                string rutaRtf = _objRadDocIdentity.UbicacionDocumento + intIdProcessInstance.ToString() + ".rtf";
                correo.Adjuntos.Add(rutaRtf);
                string rutaPdf = _objRadDocIdentity.UbicacionDocumento + intIdProcessInstance.ToString() + ".pdf";
                correo.Adjuntos.Add(rutaPdf);
            }

            correo.Enviar(plantillaID);
            //Comun.Correo _objCorreo = new Comun.Correo(null, objAutoridadIdentity.CorreoCorrespondencia, "Pendiente Radicación de Solicitud No. :" + _objRadDocIdentity.NumeroVITALCompleto,
            //      RadicarAAFormato(_objRadDocIdentity, objAutoridadIdentity, objPersona), _objRadDocIdentity.LstNombreDocumentoAdjunto);

        }

        /// <summary>
        /// Crea un formato para Enviar correos de Radicaciones Pendientes a Autoridades Ambientales
        /// </summary>
        /// <param name="_objRadDocIdentity"></param>
        /// <param name="objAutoridadIdentity"></param>
        /// <returns></returns>
        private static string RadicarAAFormato1(RadicacionDocumentoIdentity _objRadDocIdentity, AutoridadAmbientalIdentity objAutoridadIdentity, Persona persona)
        {
            ///  se determina si es acto u oficio.
            ///  
            string tipoDocumento = string.Empty;
            string numeroSilpa = string.Empty;
            string NumeroExpediente = string.Empty;
            string TipoDocumento = string.Empty;
            string Descripcion = string.Empty;
            List<string> LstArchivosAdjuntos = new List<string>();

            if (_objRadDocIdentity != null)
            {
                tipoDocumento = "Radicación";
                numeroSilpa = _objRadDocIdentity.NumeroSilpa;

                //hava:30-abr-10
                if (persona.Identity.NumeroIdentificacion == string.Empty || persona.Identity.NumeroIdentificacion == null)
                {
                    persona.Identity.NumeroIdentificacion = _objRadDocIdentity.IdentificacionSolicitante;
                }
                //NumeroExpediente = Radicacion.;
                //Descripcion = oficio.Descripcion;
                //LstArchivosAdjuntos = oficio.LstArchivosAdjuntos;

            }
            //else
            //{
            //    if (acto != null)
            //    {

            //        tipoDocumento = "Acto Vital";
            //        numeroSilpa = acto.NumeroSilpa;
            //        NumeroExpediente = acto.NumeroExpediente;
            //        Numero = acto.NumeroActo;
            //        Fecha = acto.FechaActo;
            //        Descripcion = acto.DescripcionActo;
            //        LstArchivosAdjuntos.Add(acto.UrlArchivosAdjuntos);
            //    }
            //}


            string listaArchivos = string.Empty;

            foreach (string str in LstArchivosAdjuntos)
            {
                listaArchivos = listaArchivos + " , " + str;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta content=\"es-co\" http-equiv=\"Content-Language\" />");
            sb.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />");
            sb.Append("<title>Radicación Documento VITAL</title>");
            sb.Append("</head>");
            sb.Append("<body style=\"font-family: Tahoma; font-size: small\">");
            sb.Append("<p>Autoridad Ambiental:</p>");
            sb.Append("<p>" + objAutoridadIdentity.Nombre + "</p>");
            sb.Append("<p> NIT:" + objAutoridadIdentity.NIT + "</p>");
            //sb.Append("<p>" + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + "</p>");
            sb.Append("<p>" + objAutoridadIdentity.Direccion + "</p>");
            sb.Append("<p>&nbsp;</p>");
            sb.Append("<p>Se han adjuntado documentos a la siguiente solicitud, y están pendientes por radicar: ");
            sb.Append("<table style=\"width: 400px;font:Tahoma\">");
            sb.Append("<tr>");
            sb.Append("	<td><strong>Número VITAL:</strong></td>");
            sb.Append("	<td>" + _objRadDocIdentity.NumeroVITALCompleto + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Identificación del Solicitante:</strong></td>");
            sb.Append("<td>" + persona.Identity.NumeroIdentificacion + "</td>");
            //sb.Append("<td>" + _objRadDocIdentity.IdentificacionSolicitante + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><strong>Fecha Solicitud :</strong></td>");
            sb.Append("<td>" + _objRadDocIdentity.FechaSolicitud + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        public static void EnviarCorreoRstaEE(string str_CorreoElectronico, string str_NombreSolicitnate, string str_Mensaje, string str_NombreArchivo, string str_AA)
        {
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(str_CorreoElectronico);
            correo.Tokens.Add("NOMBRE_SOLICITANTE", str_NombreSolicitnate);
            correo.Tokens.Add("MENSAJE", str_Mensaje);
            correo.Tokens.Add("NOMBRE_AA", str_AA);
            int plantillaID = (int)EnumPlantillaCorreo.RespuestaEE;

            correo.Adjuntos.Add(str_NombreArchivo);
            correo.Enviar(plantillaID);
        }


        /// <summary>
        /// Envía correo electrónico al solicitante que se va a notificar
        /// </summary>
        /// <param name="str_CorreoElectronico"></param>
        /// <param name="str_NombreSolicitnate"></param>
        /// <param name="str_Mensaje"></param>
        /// <param name="str_NombreArchivo"></param>
        public void EnviarCorreoCambioEstadoNotificacionPersona(
            string str_CorreoElectronico, 
            string str_NombreSolicitante,
            string str_TipoIdentificacion,
            string str_Identificacion,
            string str_Mensaje, 
            string str_NombreArchivo, 
            string str_NumeroSilpa,
            string str_Expediente,
            string str_Funcionario,
            string str_Fecha_EnvioCorreo,
            string str_numeroVital,
            string str_nroactoadministrativo,
            string str_correoAutAmbiNotificacion
            ) 
        {
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();

            string nitAA= string.Empty;
            string telefonoAA = string.Empty;
            string nombreAA = string.Empty;

            this.ObtenerDatosAAPorNumeroSilpa(str_NumeroSilpa, ref nitAA, ref telefonoAA, ref nombreAA);

            correo.Para.Add(str_CorreoElectronico);
            correo.Tokens.Add("NUMERO_SILPA", str_NumeroSilpa);
            correo.Tokens.Add("USUARIO", str_NombreSolicitante);
            correo.Tokens.Add("TIPO_IDENTIFICACION", str_TipoIdentificacion);
            correo.Tokens.Add("NUMERO_IDENTIFICACION", str_Identificacion);
            correo.Tokens.Add("NUMERO_EXPEDIENTE", str_Expediente);
            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", nombreAA);
            correo.Tokens.Add("NIT_AA", nitAA);
            correo.Tokens.Add("TELEFONO_AA", telefonoAA);
            correo.Tokens.Add("MENSAJE", str_Mensaje);
            correo.Tokens.Add("FUNCIONARIO_NOTIFICADOR", str_Funcionario);
            correo.Tokens.Add("FECHA_ENVIO_CORREO", str_Fecha_EnvioCorreo);
            correo.Tokens.Add("NUM_ACTO_ADMINISTRATIVO", str_nroactoadministrativo);
            correo.Tokens.Add("NRO_VITAL",str_numeroVital);
            if(str_correoAutAmbiNotificacion != string.Empty)
                correo.Cc.Add(str_correoAutAmbiNotificacion);


            int plantillaID = (int)EnumPlantillaCorreo.CambioEstadoNotificacionPersona;
            correo.Adjuntos.Add(str_NombreArchivo);
            correo.Enviar(plantillaID);
        }


        /// <summary>
        /// Envira correo de notificación
        /// </summary>
        /// <param name="p_strCorreoElectronico">string con la dirección de correo eléctronico</param>
        /// <param name="p_strNombreSolicitante">string con el nombre del solicitante</param>
        /// <param name="p_strTipoIdentificacion">string con el tipo de identificación</param>
        /// <param name="p_strIdentificacion">string con el número de identificación</param>
        /// <param name="p_strMensaje">string con mensaje</param>
        /// <param name="p_lstArchivosAdjuntos">List con la ruta de los archivos adjuntar</param>
        /// <param name="p_strExpediente">string con el número de expediente</param>
        /// <param name="p_strFuncionario">string con el nombre del funcionario que realiza la notificación</param>
        /// <param name="p_strFecha_EnvioCorreo">string con la fecha del correo</param>
        /// <param name="p_strNumeroVital">string con el número VITAL</param>
        /// <param name="strNumeroActoadministrativo">string con el número del acto administrativo</param>
        /// <param name="p_strUrlDocumentos">string con el enlace de acceso a listado de documentos</param>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental que realiza el envío del correo</param>
        /// <param name="p_objTipoNotificacion">TipoNotificacion con el tipo de notificación realizada</param>
        public void EnviarCorreoNotificacionPersona(string p_strIdentificadorCorreo,
                                                    string p_strCorreoElectronico, string p_strNombreSolicitante, string p_strTipoIdentificacion, string p_strIdentificacion,
                                                    string p_strMensaje, List<string> p_lstArchivosAdjuntos, string p_strExpediente,
                                                    string p_strFecha_EnvioCorreo, string p_strNumeroVital, string strNumeroActoadministrativo, string p_strUrlDocumentos, int p_intPlantillaNotificacion)
        {
            ServicioCorreoElectronico objCorreo = null;
            string strNitAA = string.Empty;
            string strTelefonoAA = string.Empty;
            string strNombreAA = string.Empty;
            string strMensajeEnlace = "";

            try
            {
                //Cargar el mensaje de enlace
                if (p_lstArchivosAdjuntos != null && p_lstArchivosAdjuntos.Count > 0 && !string.IsNullOrEmpty(p_strUrlDocumentos))
                {
                    strMensajeEnlace = "<p>Para consultar los documentos asociados al acto administrativo haga clic sobre el siguiente enlace <a href='" + p_strUrlDocumentos + "'>" + p_strUrlDocumentos + "</a> o cópielo en el navegador.</p>";
                }

                //Cargar datos autoridad ambiental
                this.ObtenerDatosAAPorNumeroSilpa(p_strNumeroVital, ref strNitAA, ref strTelefonoAA, ref strNombreAA);

                //Se crea objeto envío de correo electronico
                objCorreo = new ServicioCorreoElectronico();

                //Cargar datos envío de correo
                objCorreo.Para.Add(p_strCorreoElectronico);
                objCorreo.Tokens.Add("IDENTIFICADOR_CORREO", p_strIdentificadorCorreo);
                objCorreo.Tokens.Add("USUARIO", p_strNombreSolicitante);
                objCorreo.Tokens.Add("TIPO_IDENTIFICACION", p_strTipoIdentificacion);
                objCorreo.Tokens.Add("NUMERO_IDENTIFICACION", p_strIdentificacion);
                objCorreo.Tokens.Add("NUMERO_EXPEDIENTE", p_strExpediente);
                objCorreo.Tokens.Add("AUTORIDAD_AMBIENTAL", strNombreAA);
                objCorreo.Tokens.Add("NIT_AA", strNitAA);
                objCorreo.Tokens.Add("TELEFONO_AA", strTelefonoAA);
                objCorreo.Tokens.Add("MENSAJE", p_strMensaje);
                objCorreo.Tokens.Add("MENSAJE_ENLACE", strMensajeEnlace);
                objCorreo.Tokens.Add("FUNCIONARIO_NOTIFICADOR", "VITAL");
                objCorreo.Tokens.Add("FECHA_ENVIO_CORREO", p_strFecha_EnvioCorreo);
                objCorreo.Tokens.Add("NUM_ACTO_ADMINISTRATIVO", strNumeroActoadministrativo);
                objCorreo.Tokens.Add("NRO_VITAL", p_strNumeroVital);

                //Cargar adjuntos
                if (string.IsNullOrEmpty(strMensajeEnlace) && p_lstArchivosAdjuntos != null && p_lstArchivosAdjuntos.Count > 0)
                {
                    foreach (string strAdjunto in p_lstArchivosAdjuntos)
                    {
                        objCorreo.Adjuntos.Add(strAdjunto);
                    }
                }

                //Cargar plantilla
                if (p_intPlantillaNotificacion > 0)
                {
                    //Enviar correo
                    objCorreo.Enviar(p_intPlantillaNotificacion);
                }
                else
                {
                    throw new Exception("No se envío correo ya que no se especifico plantilla.");
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoNotificacionPersona -> Error Inesperado: " + exc.Message);
            }
        }


        public void EnviarCorreoComunicadores(string[] strdestinaradios, string str_NumeroSilpa, string str_correo_notificacion,
            string strTipoActoAdministrativo, string strNroActo, string strFechaActo, string strCodigoExpediente, string strTipoTramite,
            string strSolicitante)
        {
             ServicioCorreoElectronico correo = new ServicioCorreoElectronico();

            string nitAA= string.Empty;
            string telefonoAA = string.Empty;
            string nombreAA = string.Empty;

            this.ObtenerDatosAAPorNumeroSilpa(str_NumeroSilpa, ref nitAA, ref telefonoAA, ref nombreAA);
            foreach (string strcomunicado in strdestinaradios)
            {
                correo.Para.Add(strcomunicado);
            }
            if (str_correo_notificacion != string.Empty)
            {
                correo.Para.Add(str_correo_notificacion);
            }
            correo.Tokens.Add("TIPO_ACTO_ADMINISTRATIVO", strTipoActoAdministrativo);
            correo.Tokens.Add("NRO_ACTO", strNroActo);
            correo.Tokens.Add("FECHA_ACTO", strFechaActo);
            correo.Tokens.Add("CODIGO_EXPEDIENTE", strCodigoExpediente);
            correo.Tokens.Add("TIPO_DE_TRAMITE", strTipoTramite);
            correo.Tokens.Add("SOLICITANTE", strSolicitante);
            correo.Tokens.Add("TELEFONO_AUTORIDAD", telefonoAA);
            correo.Tokens.Add("FECHA_ENVIO_CORREO", DateTime.Now.ToShortDateString());
            correo.Tokens.Add("NUMERO_SILPA", str_NumeroSilpa);
            int plantillaID = (int)EnumPlantillaCorreo.CorreoComunicador;
            correo.Enviar(plantillaID);
        }


        /// <summary>
        /// Envía correo informando Falla de comunicación co PDI
        /// </summary>
        /// <param name="correoAdminMaestro"></param>
        /// <param name="correoNotificador"></param>
        /// <param name="fecha">string: fecha de la falla</param>
        /// <param name="hora">string : hora de la falla</param>
        public void EnviarCorreoFallaComunicacionPDI(string correoAdminMaestro, string correoNotificador, string fecha, string hora)
        {
            try
            {
	            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
	
	            if (!String.IsNullOrEmpty(correoAdminMaestro)) 
	            {
	                correo.Para.Add(correoAdminMaestro);
	            }
	
	            if (!String.IsNullOrEmpty(correoNotificador))
	            {
	                correo.Para.Add(correoNotificador);
	            }
	
	            if (correo.Para.Count > 0) 
	            {
	                correo.Tokens.Add("FECHA_FALLA", fecha);
	                correo.Tokens.Add("HORA_FALLA", hora);
	
	                int plantillaID = (int)EnumPlantillaCorreo.FallaComunicacionPDI;
	                correo.Enviar(plantillaID);
	            }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al envíar correo informando falla de comunicación con PDI.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// Método que obtiene el nombre de la Autoridad Ambiental
        /// </summary>
        public static void ObtenerNombreAutoridadAmbiental()
        {
            /// se obtiene el nombre de la autoridad ambiental origen:
            AutoridadAmbientalIdentity aut = new AutoridadAmbientalIdentity();
            aut.IdAutoridad = IdAutoridadAmbiental;
            AutoridadAmbientalDalc autDalc = new AutoridadAmbientalDalc();
            //autDalc.ObtenerAutoridadById(ref aut);
            // HAVA: 13 - dic - 2010
            // Consulta la AA sin filtro de Ventanilla Integrada.
            autDalc.ObtenerAutoridadNoIntegradaById(ref aut);

            NombreAutoridadAmbiental = aut.Nombre;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroSilpaAudiencia"></param>
        public static void ObtenerIdAAPorNumeroSilpa(string numeroSilpa) 
        { 
            SolicitudDAAEIADalc daaDalc=  new SolicitudDAAEIADalc();
            IdAutoridadAmbiental = daaDalc.ObtenerIdAAporNumeroSilpa(numeroSilpa);
        }


        public void ObtenerDatosAAPorNumeroSilpa(string numeroSilpa, ref string nitAA, ref string telefonoAA, ref string nombreAA)
        {
            SolicitudDAAEIADalc daaDalc = new SolicitudDAAEIADalc();
            IdAutoridadAmbiental = daaDalc.ObtenerIdAAporNumeroSilpa(numeroSilpa);
            daaDalc.ObtenerDatosAAPorNumeroSilpa(numeroSilpa, ref nitAA, ref telefonoAA, ref nombreAA);
        }


        /// <summary>
        /// Obtiene el nombre de la autoridad ambiental asociada el solcitante al registrarse
        /// </summary>
        /// <param name="idPersona">long: identificador de la persoba en silamc</param>
        /// <returns>string: nombre de la AA</returns>
        public static string ObtenerNombreAutoridadPorPersona(long idPersona) 
        {
            PersonaDalc pDalc = new PersonaDalc();
            int idAA = 0;
            return pDalc.ObtenerAutoridadPorPersona(idPersona, out idAA);
        }

        public void EnviarCorreoRecursoInterpuestoPublico(
            string str_CorreoElectronico,
            string str_NombreSolicitante,
            string str_TipoIdentificacion,
            string str_Identificacion,
            string str_NombreArchivo,
            string str_NumeroSilpa,
            string str_Expediente
            )
        {
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();

            string nitAA = string.Empty;
            string telefonoAA = string.Empty;
            string nombreAA = string.Empty;

            this.ObtenerDatosAAPorNumeroSilpa(str_NumeroSilpa, ref nitAA, ref telefonoAA, ref nombreAA);

            correo.Para.Add(str_CorreoElectronico);
            correo.Tokens.Add("SOLICITANTE", str_NombreSolicitante);
            correo.Tokens.Add("TIPO_IDENTIFICACION", str_TipoIdentificacion);
            correo.Tokens.Add("NUMERO_IDENTIFICACION", str_Identificacion);
            correo.Tokens.Add("NUMERO_EXPEDIENTE", str_Expediente);
            correo.Tokens.Add("AUTORIDAD_AMBIENTAL", nombreAA);
            correo.Tokens.Add("NIT_AA", nitAA);
            correo.Tokens.Add("TELEFONO_AA", telefonoAA);

            int plantillaID = (int)EnumPlantillaCorreo.RecursoInterposicion;
            correo.Adjuntos.Add(str_NombreArchivo);
            correo.Enviar(plantillaID);
        }




        public void EnviarCorreoSolicitudNotElectronica(
              string str_NombreSolicitante,
              string str_Identificacion,
              string str_NumeroSilpa,
              string str_NombreAA,
              string str_CorreoElectronico,
              string Fecha
          )
        {
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();

       
            correo.Para.Add(str_CorreoElectronico);
            correo.Tokens.Add("NOMBRE_COMPLETO", str_NombreSolicitante);
            correo.Tokens.Add("NUMERO_IDENTIFICACION", str_Identificacion);
            correo.Tokens.Add("NOMBRE_AA", str_NombreAA);
            correo.Tokens.Add("NUMERO_SILPA", str_NumeroSilpa);
            correo.Tokens.Add("FECHA", Fecha);


            int plantillaID = (int)EnumPlantillaCorreo.SolNotificacionElectronica;
            correo.Enviar(plantillaID);
        }


        
            #region Contigencias
            /// <summary>
            /// Enviar correo electronico de alerta para contingencia
            /// </summary>
            /// <param name="p_strCorreoElectronico">string con las direcciones de correo electronico</param>
            /// <param name="p_strArchivoAnexo">string con el nombre del archivo anexo</param>
            /// <param name="p_strNivel">string con el nivel de urgencia</param>
            public void EnviarCorreoAlertaContingencia(string p_strCorreoElectronico,
                                                       string p_strNumeroVital,
                                                       string p_strArchivoAnexo,
                                                       string p_strNivel)
            {
                ServicioCorreoElectronico objCorreo = null;
                string[] strLstCorreos = null;

                try
                {
                    //Crear objeto envío de correo electronico
                    objCorreo = new ServicioCorreoElectronico();

                    //Cargar variables
                    strLstCorreos = p_strCorreoElectronico.Split(';');
                    foreach (string strCorreo in strLstCorreos)
                    {
                        if (!string.IsNullOrEmpty(strCorreo))
                            objCorreo.Para.Add(strCorreo.Trim());
                    }
                    objCorreo.Tokens.Add("NUM_VITAL", p_strNumeroVital);
                    objCorreo.Tokens.Add("NIVEL_URGENCIA", p_strNivel.Trim().ToUpper());

                    //Anexar Adjunto
                    objCorreo.Adjuntos.Add(p_strArchivoAnexo);

                    //Enviar el correo
                    objCorreo.Enviar((int)EnumPlantillaCorreo.AlertaContigencia);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoAlertaContingencia -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_strAutoridadAmbietal"></param>
            /// <param name="p_strNumeroVITAL"></param>
            /// <param name="p_strArchivoAnexo"></param>
            public void EnviarCorreoReporteContigenciasAutoridadAmbiental(string p_strAutoridadAmbietal, string p_strNumeroVITAL, string p_strArchivoAnexo, string p_strNivel)
            {
                ServicioCorreoElectronico objCorreo = null;
                string[] strLstCorreos = null;

                try
                {
                    string p_strCorreoElectronico = string.Empty;
                    // consultamos si existe la autoridad ambiental que se ercibe por parametro y traemos el correo asociado a la misma
                    Generico.Listas _listaAutoridades = new Generico.Listas();
                    var lstAutoridadesAmbientales = _listaAutoridades.ListarAutoridades(null).Tables[0].AsEnumerable();

                    var autoridadambiental = lstAutoridadesAmbientales.Where(x => x.Field<string>("AUT_DESCRIPCION").ToUpper().Trim() == p_strAutoridadAmbietal.ToUpper().Trim() || x.Field<Int32>("AUT_ID") == Convert.ToInt32(p_strAutoridadAmbietal)).FirstOrDefault();
                    if (autoridadambiental != null)
                    {
                        p_strCorreoElectronico = autoridadambiental["AUT_CORREO_NOTIFICACION_AA"].ToString();
                    }

                    //Crear objeto envío de correo electronico
                    objCorreo = new ServicioCorreoElectronico();

                    //Cargar variables
                    strLstCorreos = p_strCorreoElectronico.Split(';');
                    foreach (string strCorreo in strLstCorreos)
                    {
                        if (!string.IsNullOrEmpty(strCorreo))
                            objCorreo.Para.Add(strCorreo.Trim());
                    }
                    objCorreo.Tokens.Add("NUM_VITAL", p_strNumeroVITAL);
                    objCorreo.Tokens.Add("NIVEL_URGENCIA", p_strNivel.Trim().ToUpper());

                    //Anexar Adjunto
                    objCorreo.Adjuntos.Add(p_strArchivoAnexo);

                    //Enviar el correo
                    objCorreo.Enviar((int)EnumPlantillaCorreo.AlertaContigencia);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoReporteContigenciasAutoridadAmbiental -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_strMunicipio"></param>
            /// <param name="p_strNumeroVITAL"></param>
            /// <param name="p_strArchvioAnexo"></param>
            public void EnviarCorreoReporteContigenciasPorJurisdiccionAutoridadAmbiental(string p_strMunicipio, string p_strNumeroVITAL, string p_strArchvioAnexo, string p_strNivel)
            {
                ServicioCorreoElectronico objCorreo = null;
                string[] strLstCorreos = null;
                try
                {
                Generico.Listas lstMunicipio = new Generico.Listas();
                var municipio = lstMunicipio.ListaMunicipios(null, null, null).Tables[0].AsEnumerable().Where(x => x.Field<string>("MUN_NOMBRE") == p_strMunicipio || x.Field<int>("MUN_ID") == Convert.ToInt32(p_strMunicipio)).FirstOrDefault();
                if (municipio["MUN_ID"].ToString() != string.Empty)
                {
                    SILPA.LogicaNegocio.Generico.AutoridadAmbiental _autoridad = new SILPA.LogicaNegocio.Generico.AutoridadAmbiental();
                    var vrAutoridadAmbiental = _autoridad.ListarAAXJurisdiccion((Int32)municipio["MUN_ID"]).Tables[0].AsEnumerable().FirstOrDefault();
                    if (vrAutoridadAmbiental != null)
                    {
                        Generico.Listas _listaAutoridades = new Generico.Listas();
                        var objAutoridadAmbietal = _listaAutoridades.ListarAutoridades((Int32)vrAutoridadAmbiental["AUT_ID"]).Tables[0].AsEnumerable().FirstOrDefault();
                        if (objAutoridadAmbietal["AUT_ID"].ToString() != string.Empty)
                        {
                            string p_strCorreoElectronico = string.Empty;
                            p_strCorreoElectronico = objAutoridadAmbietal["AUT_CORREO_NOTIFICACION_AA"].ToString();

                            //Crear objeto envío de correo electronico
                            objCorreo = new ServicioCorreoElectronico();

                            //Cargar variables
                            strLstCorreos = p_strCorreoElectronico.Split(';');
                            foreach (string strCorreo in strLstCorreos)
                            {
                                if (!string.IsNullOrEmpty(strCorreo))
                                    objCorreo.Para.Add(strCorreo.Trim());
                            }
                            objCorreo.Tokens.Add("NUM_VITAL", p_strNumeroVITAL);
                            objCorreo.Tokens.Add("NIVEL_URGENCIA", p_strNivel.Trim().ToUpper());

                            //Anexar Adjunto
                            objCorreo.Adjuntos.Add(p_strArchvioAnexo);

                            //Enviar el correo
                            objCorreo.Enviar((int)EnumPlantillaCorreo.AlertaContigencia);
                        }
                    }
                }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoReporteContigenciasPorJurisdiccionAutoridadAmbiental -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
               
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_strAutoridadAmbietal"></param>
            /// <param name="p_strNumeroVITAL"></param>
            /// <param name="p_strArchvioAnexo"></param>
            public void EnviarCorreoReporteContigenciasDIMAR(string p_strAutoridadAmbietal, string p_strNumeroVITAL, string p_strArchvioAnexo, string p_strNivel)
            {
                ServicioCorreoElectronico objCorreo = null;
                string[] strLstCorreos = null;

                try
                {
                    string p_strCorreoElectronico = string.Empty;
                    string p_strCorreoCopiaOculta = string.Empty;
                    // consultamos si existe la autoridad ambiental que se ercibe por parametro y traemos el correo asociado a la misma
                    ParametroDalc clsParametros = new ParametroDalc();
                    ParametroEntity objParametroEntity = new ParametroEntity();
                    
                    objParametroEntity.NombreParametro = "CORREO_DIMAR";
                    objParametroEntity.IdParametro = -1;
                    clsParametros.obtenerParametros(ref objParametroEntity);

                    p_strCorreoElectronico = objParametroEntity.Parametro;

                    objParametroEntity.NombreParametro = "CORREO_CCO_DIMAR";
                    objParametroEntity.IdParametro = -1;
                    clsParametros.obtenerParametros(ref objParametroEntity);

                    p_strCorreoCopiaOculta = objParametroEntity.Parametro;

                    //Crear objeto envío de correo electronico
                    objCorreo = new ServicioCorreoElectronico();

                    //Cargar variables
                    strLstCorreos = p_strCorreoElectronico.Split(';');
                    
                    foreach (string strCorreo in strLstCorreos)
                    {
                        if (!string.IsNullOrEmpty(strCorreo))
                            objCorreo.Para.Add(strCorreo.Trim());
                    }

                    if (p_strCorreoCopiaOculta != string.Empty)
                    {
                        foreach (string strCorreo in p_strCorreoCopiaOculta.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(strCorreo))
                                objCorreo.Cco.Add(strCorreo.Trim());
                        }
                    }
                    objCorreo.Tokens.Add("NUM_VITAL", p_strNumeroVITAL);
                    objCorreo.Tokens.Add("NIVEL_URGENCIA", p_strNivel.Trim().ToUpper());

                    //Anexar Adjunto
                    objCorreo.Adjuntos.Add(p_strArchvioAnexo);

                    //Enviar el correo
                    objCorreo.Enviar((int)EnumPlantillaCorreo.AlertaContigencia);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoReporteContigenciasDIMAR -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }

            public void EnviarCorreoAlertaContingenciaPorNiveldeEmergencia(string p_municipio, string p_nivelEmergencia, string p_strNumeroVITAL, string p_strArchvioAnexo)
            {
                ServicioCorreoElectronico objCorreo = null;
                string[] strLstCorreos = null;

                try
                {
                    string p_strCorreoElectronico = string.Empty;
                    Contingencias.Contingencias clsContingencias = new Contingencias.Contingencias();

                    p_strCorreoElectronico = clsContingencias.obtenerDestinadariosNivelEmergencia(p_municipio,p_nivelEmergencia);

                    //Crear objeto envío de correo electronico
                    objCorreo = new ServicioCorreoElectronico();

                    //Cargar variables
                    strLstCorreos = p_strCorreoElectronico.Split(';');
                    
                    foreach (string strCorreo in strLstCorreos)
                    {
                        if (!string.IsNullOrEmpty(strCorreo))
                            objCorreo.Para.Add(strCorreo.Trim());
                    }

                    objCorreo.Tokens.Add("NUM_VITAL", p_strNumeroVITAL);
                    objCorreo.Tokens.Add("NIVEL_URGENCIA", p_nivelEmergencia.Trim().ToUpper());

                    //Anexar Adjunto
                    objCorreo.Adjuntos.Add(p_strArchvioAnexo);

                    //Enviar el correo
                    objCorreo.Enviar((int)EnumPlantillaCorreo.AlertaContigencia);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoAlertaContingenciaPorNiveldeEmergencia -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }
            #endregion
            #region Salvoconducto
            public void EnviarCorreoNuevaSolicitudAutoridadAmbiental(string p_strCorreoAutoridadAmbiental, string p_strCorreoSolicitante, ref SalvoconductoNewIdentity vSalvoconductoNewIdentity, string p_strAutNombre)
            {
                ServicioCorreoElectronico objCorreoAutoridad = null;
                ServicioCorreoElectronico objCorreoSolicante = null;

                try
                {
                    //Crear objeto envío de correo electronico autoridad ambiental
                    objCorreoAutoridad = new ServicioCorreoElectronico();
                    //Crear objeto envío de correo electronico autoridad solicitante
                    objCorreoSolicante = new ServicioCorreoElectronico();
                    //asignar valores al objeto correo para la autoridad ambiental
                    objCorreoAutoridad.Para.Add(p_strCorreoAutoridadAmbiental);
                    objCorreoAutoridad.Tokens.Add("SALVO_ID", vSalvoconductoNewIdentity.SalvoconductoID.ToString());
                    objCorreoAutoridad.Tokens.Add("FECHA", DateTime.Today.ToShortDateString());
                    //Enviar el correo
                    objCorreoAutoridad.Enviar((int)EnumPlantillaCorreo.NuevaSolicitudSalvoconductoAA);
                    //asignar valores al objeto correo para el solicitante
                    objCorreoSolicante.Para.Add(p_strCorreoSolicitante);
                    objCorreoSolicante.Tokens.Add("SALVO_ID", vSalvoconductoNewIdentity.SalvoconductoID.ToString());
                    objCorreoSolicante.Tokens.Add("FECHA", DateTime.Today.ToShortDateString());
                    //JMARTINEZ CASO EXCEL 26 ADICIONO LA AUTORIDAD AMBIENTAL DONDE HA SIDO DIRIGIDO EL SALVOCONDUCTO
                    objCorreoSolicante.Tokens.Add("AUT_NOMBRE", p_strAutNombre);
                //Enviar el correo
                objCorreoSolicante.Enviar((int)EnumPlantillaCorreo.NuevaSolicitudSalvoconductoSolicitante);
                    
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoNuevaSolicitudAutoridadAmbiental -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }


        public void EnviarCorreoVencimientoSerieSUNL(string p_strCorreoDestino, string token_aut_amb_nombre ,string token_usuario, string token_pje, string token_MailMads)
        {
            ServicioCorreoElectronico objCorreoSolicante = null;
            try
            {
                //Crear objeto envío de correo electronico autoridad solicitante
                objCorreoSolicante = new ServicioCorreoElectronico();
                //asignar valores al objeto correo para el solicitante
                objCorreoSolicante.Para.Add(p_strCorreoDestino);
                objCorreoSolicante.Tokens.Add("USUARIO", token_usuario);
                objCorreoSolicante.Tokens.Add("PJE", token_pje);
                objCorreoSolicante.Tokens.Add("REMITIR_EMAIL_MADS", token_MailMads);
                objCorreoSolicante.Tokens.Add("AUTORIDAD_AMBIENTAL", token_aut_amb_nombre);
                //Enviar el correo
                objCorreoSolicante.Enviar((int)EnumPlantillaCorreo.VencimientoSerieNumeracionSUNL);

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoEmisionSalvoconducto -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }
        }


            public void EnviarCorreoEmisionSalvoconducto(string p_strCorreoSolicitante, ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
            {
                ServicioCorreoElectronico objCorreoSolicante = null;
                try
                {
                    //Crear objeto envío de correo electronico autoridad solicitante
                    objCorreoSolicante = new ServicioCorreoElectronico();
                    //asignar valores al objeto correo para el solicitante
                    objCorreoSolicante.Para.Add(p_strCorreoSolicitante);
                    objCorreoSolicante.Tokens.Add("SALVO_ID", vSalvoconductoNewIdentity.SalvoconductoID.ToString());
                    //Enviar el correo
                    objCorreoSolicante.Enviar((int)EnumPlantillaCorreo.EmisionSalvoconducto);

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoEmisionSalvoconducto -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }
            public void EnviarCorreoRechazoSalvoconducto(string p_strCorreoSolicitante, ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
            {
                ServicioCorreoElectronico objCorreoSolicante = null;
                try
                {
                    //Crear objeto envío de correo electronico autoridad solicitante
                    objCorreoSolicante = new ServicioCorreoElectronico();
                    //asignar valores al objeto correo para el solicitante
                    objCorreoSolicante.Para.Add(p_strCorreoSolicitante);
                    objCorreoSolicante.Tokens.Add("NUM_SALVOCONDUCTO", vSalvoconductoNewIdentity.SalvoconductoID.ToString());
                    objCorreoSolicante.Tokens.Add("MOTIVO_BLOQUEO", vSalvoconductoNewIdentity.SalvoconductoID.ToString());
                    //Enviar el correo
                    objCorreoSolicante.Enviar((int)EnumPlantillaCorreo.RechazoSalvoconducto);

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoRechazoSalvoconducto -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }


            public void EnviarCorreBloqueoSalvoconducto(string p_strCorreoSolicitante, ref SalvoconductoNewIdentity vSalvoconductoNewIdentity)
            {
                ServicioCorreoElectronico objCorreoSolicante = null;
                try
                {
                    //Crear objeto envío de correo electronico autoridad solicitante
                    objCorreoSolicante = new ServicioCorreoElectronico();
                    //asignar valores al objeto correo para el solicitante
                    objCorreoSolicante.Para.Add(p_strCorreoSolicitante);
                    objCorreoSolicante.Tokens.Add("NUMERO", vSalvoconductoNewIdentity.Numero.ToString());
                    objCorreoSolicante.Tokens.Add("MOTIVO_BLOQUEO", vSalvoconductoNewIdentity.MotivoBloqueo.ToString());
                    //Enviar el correo
                    objCorreoSolicante.Enviar((int)EnumPlantillaCorreo.BloqueoSalvoconducto);

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoRechazoSalvoconducto -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }
            #endregion

        #region Autoliquidacion

        /// <summary>
        /// Enviar correo de registro de solicitud de autoliquidación
        /// </summary>
        /// <param name="p_strSolicitante">string con el nombre del solicitante</param>
        /// <param name="p_strCorreoElectronico">string con el correo electrónico</param>
        /// <param name="p_strClaseSolicitud">string con la clase de solicitud</param>
        /// <param name="p_strTipoSOlicitud">string con el tipo de solicitud</param>
        /// <param name="p_strAutoridadAmbiental">string con la autoridad ambiental</param>
        /// <param name="p_strFechaRadicacion">string con la fecha de radicación</param>
        /// <param name="p_strNumeroVital">string con el número VITAL</param>
        public void EnviarCorreoRegistroSolicitudAutoliquidacion(string p_strSolicitante,
                                                                  string p_strCorreoElectronico,
                                                                  string p_strClaseSolicitud,
                                                                  string p_strTipoSOlicitud,
                                                                  string p_strAutoridadAmbiental,
                                                                  string p_strFechaRadicacion,
                                                                  string p_strNumeroVital)
        {
            ServicioCorreoElectronico objCorreo = null;

            try
            {
                //Crear objeto envío de correo electronico
                objCorreo = new ServicioCorreoElectronico();

                //Cargar variables
                objCorreo.Para.Add(p_strCorreoElectronico);
                objCorreo.Tokens.Add("SOLICITANTE", p_strSolicitante.Trim().ToUpper());
                objCorreo.Tokens.Add("CLASE_SOLICITUD", p_strClaseSolicitud);
                objCorreo.Tokens.Add("TIPO_SOLICITUD", p_strTipoSOlicitud);
                objCorreo.Tokens.Add("AUTORIDAD_AMBIENTAL", p_strAutoridadAmbiental);
                objCorreo.Tokens.Add("FECHA_RADICACION", p_strFechaRadicacion);
                objCorreo.Tokens.Add("NUMERO_VITAL", p_strNumeroVital);

                //Enviar el correo
                objCorreo.Enviar((int)EnumPlantillaCorreo.RegistroAutoliquidacionVITAL);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoRegistroSolicitudAutoliquidacion -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }
        }


		#endregion


        #region Reasignacion

            /// <summary>
            /// Enviar correo informativo indicando que la autoridad ambiental realizó una solicitud
            /// </summary>
            /// <param name="p_strCorreoElectronico">string con el correo electronico de la autoridad ambiental</param>
            /// <param name="p_strAutoridadAmbiental">string con el nombre de la autoridad ambiental que recibe</param>
            /// <param name="p_strAutoridadAmbientalSolicitante">string con el nombre de la autoridad ambiental que solicita</param>
            /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
            public void EnviarCorreoRegistroSolicitudReasignacion(string p_strCorreoElectronico,
                                                                string p_strAutoridadAmbiental,
                                                                string p_strAutoridadAmbientalSolicitante,
                                                                string p_strNumeroVITAL)
            {
                ServicioCorreoElectronico objCorreo = null;

                try
                {
                    //Crear objeto envío de correo electronico
                    objCorreo = new ServicioCorreoElectronico();

                    //Cargar variables
                    objCorreo.Para.Add(p_strCorreoElectronico);
                    objCorreo.Tokens.Add("AUTORIDAD", p_strAutoridadAmbiental);
                    objCorreo.Tokens.Add("AUTORIDAD_SOLICITANTE", p_strAutoridadAmbientalSolicitante);
                    objCorreo.Tokens.Add("NUMERO_VITAL", p_strNumeroVITAL);

                    //Enviar el correo
                    objCorreo.Enviar((int)EnumPlantillaCorreo.ReasignacionRegistro);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoRegistroSolicitudReasignacion -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }


            /// <summary>
            /// Enviar correo informativo indicando el resultado de la solicitud de asignación
            /// </summary>
            /// <param name="p_strCorreoElectronico">string con el correo electronico de la autoridad ambiental solicitante</param>
            /// <param name="p_strAutoridadAmbiental">string con el nombre de la autoridad ambiental que aprobo o rechazo</param>
            /// <param name="p_strAutoridadAmbientalSolicitante">string con el nombre de la autoridad ambiental que solicita</param>
            /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
            /// <param name="p_strResultadoAsunto">string con el Resultado ha colocar en el asunto</param>
            /// <param name="p_strResultado">string con el Resultado ha colocar en el cuerpo</param>
            /// <param name="p_strResultadoDescripcion">string con el la descripción del resulta ha colocar en el cuerpo</param>
            public void EnviarCorreoResultadoSolicitudReasignacion(string p_strCorreoElectronico,
                                                                    string p_strAutoridadAmbiental,
                                                                    string p_strAutoridadAmbientalSolicitante,
                                                                    string p_strNumeroVITAL,
                                                                    string p_strResultadoAsunto,
                                                                    string p_strResultado,
                                                                    string p_strResultadoDescripcion
                                                                    )
            {
                ServicioCorreoElectronico objCorreo = null;

                try
                {
                    //Crear objeto envío de correo electronico
                    objCorreo = new ServicioCorreoElectronico();

                    //Cargar variables
                    objCorreo.Para.Add(p_strCorreoElectronico);
                    objCorreo.Tokens.Add("AUTORIDAD", p_strAutoridadAmbiental);
                    objCorreo.Tokens.Add("AUTORIDAD_SOLICITANTE", p_strAutoridadAmbientalSolicitante);
                    objCorreo.Tokens.Add("NUMERO_VITAL", p_strNumeroVITAL);
                    objCorreo.Tokens.Add("RESULTADO_ASUNTO", p_strResultadoAsunto);
                    objCorreo.Tokens.Add("RESULTADO", p_strResultado);
                    objCorreo.Tokens.Add("RESULTADO_DESCRIPCION", p_strResultadoDescripcion);

                    //Enviar el correo
                    objCorreo.Enviar((int)EnumPlantillaCorreo.ReasignacionRespuesta);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Correo :: EnviarCorreoResultadoSolicitudReasignacion -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
                }
            }


        #endregion
    }

    	
}