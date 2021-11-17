using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.AccesoDatos.Generico;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using SILPA.Servicios;
using SILPA.LogicaNegocio.Comunicacion;
using SILPA.LogicaNegocio.Recurso;
using SILPA.Servicios.Comunicacion;
using SILPA.Comun;
using System.Threading;
using SILPA.Servicios.Usuario;
using SoftManagement.Log;
using System.Configuration;
using SoftManagement.LogWS;
using SILPA.LogicaNegocio.CesionDeDerechos;
using System.Data;
using System.Data.SqlClient;
using SILPA.Servicios.Generico.Entidades;
using SILPA.Servicios.Generico.Enum;
using SILPA.Servicios.Generico;

/// <summary>
/// Descripción breve de WSPQ09
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSPQ09 : System.Web.Services.WebService
{

    public WSPQ09()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }


    #region Informacion Solicitudes

        [WebMethod(Description = "[Devueleve el archivo en forma binaria")]
        public Byte[] ObtenerArchivo(string archivo)
        {
            Int64 iIdPadre = 0;        
            String strNoVital = "";
            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerArchivo", "Archivo: " + archivo, "0",0);              
                SILPA.LogicaNegocio.Generico.RadicacionDocumento radDoc = new SILPA.LogicaNegocio.Generico.RadicacionDocumento();
                return radDoc.ObtenerArchivo(archivo);
        
            }
            catch (Exception ex)
            {
                //SMLog.Escribir(ex);
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerArchivo", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerArchivo", "Archivo: " + archivo,"", 0, iIdPadre);
            }
        }

        /// <summary>
        /// HAVA:
        /// </summary>
        /// <param name="personaID">long: identificador del solicitante</param>
        /// <param name="autoridadAmbientalID">int: identificador de la autoridad ambiental</param>
        /// <returns>STRING</returns>
        [WebMethod(Description = "[Devuelve listado de numero vital asociados al solicitante de cesion de derechos", MessageName = "")]
        public string ObtenerNumeroVitalCesionPersona(long personaID, int autoridadAmbientalID) 
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            String strNoVital = autoridadAmbientalID.ToString();
            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerNumeroVitalCesionPersona", "Id AA: " + autoridadAmbientalID + "Id Persona: " + personaID, "", 0);
            
                Cesion objCesion = new Cesion();
                return objCesion.ObtenerNumeroVitalSecionPersona(personaID, autoridadAmbientalID);
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerNumeroVitalCesionPersona", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerNumeroVitalCesionPersona", "Id AA: " + autoridadAmbientalID + "Id Persona: " + personaID, strNoVital, iAA, iIdPadre);
            }   
        }


        /// <summary>
        /// 04-oct-2010 - aegb
        /// </summary>
        /// <param name="personaID">long: identificador del solicitante</param>
        /// <param name="autoridadAmbientalID">int: identificador de la autoridad ambiental</param>
        /// <returns>DataSet</returns>
        [WebMethod(Description = "[Devuelve listado de numero vital asociados al tramite", MessageName = "")]
        public DataSet ObtenerNumeroVitalAutoridad(int tramiteID, int autoridadID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            String strNoVital = autoridadID.ToString();
            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerNumeroVitalAutoridad", "Id AA: " + autoridadID + "Id Tramite: " + tramiteID, "", 0);

                Persona objPersona = new Persona();

                return objPersona.CargarNumeroVitalAutoridad(tramiteID, autoridadID);
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerNumeroVitalAutoridad", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerNumeroVitalAutoridad", "Id AA: " + autoridadID + "Id Tramite: " + tramiteID, strNoVital, iAA, iIdPadre);
            }
        }

        /// <summary>
        /// 04-oct-2010 - aegb
        /// </summary>
        /// <param name="personaID">long: identificador del solicitante</param>
        /// <param name="autoridadAmbientalID">int: identificador de la autoridad ambiental</param>
        /// <returns>DataSet</returns>
        [WebMethod(Description = "[Devuelve listado de numero vital asociados al tramite y/o a la persona", MessageName = "")]
        public DataSet ObtenerNumeroVitalAutoridadTramite(int tramiteID, int autoridadID, long personaID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            String strNoVital = autoridadID.ToString();
            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerNumeroVitalAutoridad", "Id AA: " + autoridadID + "Id Tramite: " + tramiteID + "Id Persona: " + personaID, "", 0);

                Persona objPersona = new Persona();
                return objPersona.CargarNumeroVitalAutoridad(tramiteID, autoridadID, personaID);
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerNumeroVitalTramite", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerNumeroVitalTramite", "Id AA: " + autoridadID + "Id Tramite: " + tramiteID + "Id Persona: " + personaID, strNoVital, iAA, iIdPadre);
            }
        }

        /// <summary>
        /// Regresa los números Vital de Recurso de Reposición asociados a un Número Vital
        /// </summary>
        /// <param name="personaID">long: identificador del solicitante</param>
        /// <param name="autoridadAmbientalID">int: identificador de la autoridad ambiental</param>
        /// <returns>DataSet</returns>
        [WebMethod(Description = "Regresa los números Vital de Recurso de Reposición asociados a un Número Vital", MessageName = "")]
        public DataSet ObtenerNumeroVitalRecursoReposicion(string numeroVital, string expediente, int idAutoridad)
        {
            Int64 iIdPadre = 0;
            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerNumeroVitalRecursoReposicion", "numeroVital: " + numeroVital + " expediente: " + expediente + "  IDAA:  " + idAutoridad.ToString(), "", 0);

                RecursoReposicionServicios servicio = new RecursoReposicionServicios();
                return servicio.ObtenerNumeroVitalRecursoReposicion(numeroVital, expediente, idAutoridad);
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerNumeroVitalRecursoReposicion", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerNumeroVitalRecursoReposicion", "numeroVital: " + numeroVital, numeroVital, 0, iIdPadre);
            }
        }

    #endregion


    #region Informacion Usuarios

        /// <summary>
        /// Si es acto administrativo y requiere notificación se reenvia al servicio de emitirDocumento
        /// Si es un acto administrativo y nno requiere notificación se envía el correo al solicitante
        /// Si es un oficio se envía el documento al correo del solicitante
        /// </summary>
        /// <param name="documentoXML"></param>
        /// <returns></returns>
        [WebMethod(Description = "[Devuelve los datos de los solicitante por Autoridad Ambiental", MessageName = "CU-USU-01")]
        public string ObtenerDatosSolicitud(int autoridadAmbientalID, int personaID, int enProceso)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = autoridadAmbientalID;
            String strNoVital = "";
            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerDatosSolicitud", "ID_AA: " + autoridadAmbientalID + "Id Persona: " + personaID + "en proceso:" + enProceso, "", 0);
                Persona objServicioPersona = new Persona();
                List<PersonaIdentity> arrayPersonas = objServicioPersona.CargarDatosPersonaByAutoridadAmbiental(autoridadAmbientalID, personaID, enProceso, "");
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer serializador = new XmlSerializer(typeof(List<PersonaIdentity>));
                //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\ObjetosXml\\ObtenerDatosSolicitud.xml");
                //serializador.Serialize(file, arrayPersonas);
                serializador.Serialize(memoryStream, arrayPersonas);
                return System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                //SMLog.Escribir(ex);
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerDatosSolicitud", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerDatosSolicitud", "ID_AA: " + autoridadAmbientalID + "Id Persona: " + personaID + "en proceso:" + enProceso, strNoVital, iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Si es acto administrativo y requiere notificación se reenvia al servicio de emitirDocumento
        /// Si es un acto administrativo y nno requiere notificación se envía el correo al solicitante
        /// Si es un oficio se envía el documento al correo del solicitante
        /// </summary>
        /// <param name="documentoXML"></param>
        /// <returns></returns>
        [WebMethod(Description = "[Devuelve los datos de los solicitante por Autoridad Ambiental y pendientes por aprobar y rechazados", MessageName = "CU-USU-01_APRZ")]
        public string ObtenerDatosSolicitudAprobadaRechazada(int autoridadAmbientalID, int personaID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            String strNoVital = "";
            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerDatosSolicitudAprobadaRechazada", "ID_AA: " + autoridadAmbientalID + "Id Persona: " + personaID, "", 0);
                Persona objServicioPersona = new Persona();
                List<PersonaIdentity> arrayPersonas = objServicioPersona.CargarDatosPersonaByAutoridadAmbientalAprobRec(autoridadAmbientalID, personaID, ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC"]);
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer serializador = new XmlSerializer(typeof(List<PersonaIdentity>));
                //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\ObjetosXml\\ObtenerDatosSolicitudAprobadaRechazada.xml");
                //serializador.Serialize(file, arrayPersonas);
                serializador.Serialize(memoryStream, arrayPersonas);
                return System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                //SMLog.Escribir(ex);
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerDatosSolicitudAprobadaRechazada", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerDatosSolicitudAprobadaRechazada", "ID_AA: " + autoridadAmbientalID + "Id Persona: " + personaID, strNoVital, iAA, iIdPadre);
            }
        }

        /// <summary>
        /// Si es acto administrativo y requiere notificación se reenvia al servicio de emitirDocumento
        /// Si es un acto administrativo y nno requiere notificación se envía el correo al solicitante
        /// Si es un oficio se envía el documento al correo del solicitante
        /// </summary>
        /// <param name="documentoXML"></param>
        /// <returns></returns>
        [WebMethod(Description = "[Recibe la aprobación o rechazo de los solicitante por Autoridad Ambiental", MessageName = "CU-USU-02")]
        public bool RecibirDatosCredencial(string xmlObject)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            String strNoVital = "";
            try
            {
                UsuarioFachada.EnviarProceso(xmlObject);
                return true;
            }
            catch (Exception ex)
            {
                //SMLog.Escribir(ex);
                SMLogWS.EscribirExcepcion(this.ToString(), "RecibirDatosCredencial", ex.ToString(), iIdPadre);
                return false;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "RecibirDatosCredencial", xmlObject, strNoVital, iAA, iIdPadre);
            }
        }

        /// <summary>
        /// 01-jul-2010 - aegb
        /// Devuelve un grupo de personas por autoridad ambiental relacionadas con el numero vital
        /// </summary>
        /// <param name="documentoXML"></param>
        /// <returns></returns>
        [WebMethod(Description = "[Devuelve los datos de los solicitantes por Autoridad Ambiental relacionados con el numero vital", MessageName = "CU-USU-03")]
        public string ObtenerDatosPersona(int autoridadAmbientalID, int personaID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            String strNoVital = autoridadAmbientalID.ToString();
            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerDatosPersona", "Id AA: " + autoridadAmbientalID + "Id Persona: " + personaID, "", 0);
                Persona objServicioPersona = new Persona();
                List<PersonaIdentity> arrayPersonas = objServicioPersona.CargarDatosPersonaByAutoridadAmbiental(autoridadAmbientalID, personaID);
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer serializador = new XmlSerializer(typeof(List<PersonaIdentity>));
                //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\ObjetosXml\\ObtenerDatosPersona.xml");
                //serializador.Serialize(file, arrayPersonas);
                serializador.Serialize(memoryStream, arrayPersonas);

                return System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerDatosPersona", ex.ToString(), iIdPadre);
                //SMLog.Escribir(ex);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerDatosPersona", "Id AA: " + autoridadAmbientalID + "Id Persona: " + personaID, strNoVital, iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Obtiene elcorreo electronico registrado para una persona
        /// </summary>
        /// <param name="documentoXML"></param>
        /// <returns></returns>
        [WebMethod(Description = "Obtiene el correo electronico registrado para una persona")]
        public string ObtenerCorreoPersona(int personaID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strCorreo = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerCorreoPersona", "Id Persona: " + personaID, "", 0);
                Persona objServicioPersona = new Persona();
                strCorreo = objServicioPersona.ObtenerCorreoPersona(personaID);
                return strCorreo;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerDatosPersona", ex.ToString(), iIdPadre);
                //SMLog.Escribir(ex);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerCorreoPersona", strCorreo, "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Obtiene la informacion de una persona dado el tipo y número de identificación
        /// </summary>
        /// <param name="tipoIdentificacion">int con tipo de identificación de la persona</param>
        /// <param name="numeroIdentificacion">string con el número de identificación de la persona</param>
        /// <returns>string con la informacion de la persona en XML</returns>
        [WebMethod(Description = "Obtiene la informacion de una persona dado el tipo y número de identificación")]
        public string ObtenerInformacionPersonaIdentificacion(int tipoIdentificacion, string numeroIdentificacion)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strPersona = "";
            string strDatosEntrada = "";

            try
            {
                strDatosEntrada = "tipoIdentificacion: " + tipoIdentificacion + "\n " +
                                  "numeroIdentificacion: " + (numeroIdentificacion ?? "null") + "\n ";
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerInformacionPersonaIdentificacion", strDatosEntrada, "", 0);
                Persona objServicioPersona = new Persona();
                strPersona = objServicioPersona.ObtenerInformacionPersonaIdentificacion(tipoIdentificacion, numeroIdentificacion);
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerInformacionPersonaIdentificacion", ex.ToString(), iIdPadre);
                strPersona = "<Error><Codigo>0</Codigo><Mensaje>Se presento una falla durante el proceso de consulta</Mensaje></Error>";
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerInformacionPersonaIdentificacion", strPersona, "", iAA, iIdPadre);
            }

            return strPersona;
        }



        /// <summary>
        /// Obtiene la informacion de una persona dado el identifiacdor en VITAL
        /// </summary>
        /// <param name="personaVITALID">int con el identifiacdor de la persona en VITAL</param>
        /// <param name="detalleInformacion">bool que indica si se obtiene el detalle de los datos del usuario</param>
        /// <returns>string con la informacion de la persona</returns>
        [WebMethod(Description = "Obtiene la informacion de una persona dado el identifiacdor en VITAL")]
        public string ObtenerInformacionPersonaIDVITAL(long personaVITALID, bool detalleInformacion)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strPersona = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerInformacionPersonaIDVITAL", "Id Persona: " + personaVITALID + " -- detalleInformacion: " + detalleInformacion, "", 0);
                Persona objServicioPersona = new Persona();
                strPersona = objServicioPersona.ObtenerInformacionPersonaByAppId(personaVITALID, detalleInformacion);
                return strPersona;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerInformacionPersonaIDVITAL", ex.ToString(), iIdPadre);
                //SMLog.Escribir(ex);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerInformacionPersonaIDVITAL", strPersona, "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Obtener listado de usuarios activos en VITAL
        /// </summary>
        /// <returns>string con la informacion de los usuarios</returns>
        [WebMethod(Description = "Obtiene el listado de personas asociadas a un usuario")]
        public string ObtenerListaUsuariosActivos()
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strLstPersona = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerListaUsuariosActivos", "", "", 0);
                Persona objServicioPersona = new Persona();
                strLstPersona = objServicioPersona.ObtenerListaUsuariosActivos();
                return strLstPersona;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerListaUsuariosActivos", ex.ToString(), iIdPadre);
                //SMLog.Escribir(ex);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerListaUsuariosActivos", "strLstPersona: " + strLstPersona, "", iAA, iIdPadre);
            }
        }



        /// <summary>
        /// Obtener el listado de personas asociadas a un role
        /// </summary>
        /// <param name="roleID">int con el identifiacador del role</param>
        /// <param name="informacionDetallada">bool que indica si se extrae la informacion detallada</param>
        /// <returns>string con la informacion de los usuarios</returns>
        [WebMethod(Description = "Obtiene el listado de personas asociadas a un usuario")]
        public string ObtenerPersonasPorRole(int roleID, bool informacionDetallada)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strLstPersona = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerPersonasPorRole", "Id ROle: " + roleID + " -- informacionDetallada: " + informacionDetallada, "", 0);
                Persona objServicioPersona = new Persona();
                strLstPersona = objServicioPersona.ObtenerPersonasPorRoles(roleID, informacionDetallada);
                return strLstPersona;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerPersonasPorRole", ex.ToString(), iIdPadre);
                //SMLog.Escribir(ex);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerPersonasPorRole", "strLstPersona: " + strLstPersona, "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Obtiene el listado de personas asociadas a un usuario
        /// </summary>
        /// <param name="personaVITALID">int con el identificador de la persona</param>
        /// <param name="tipoRelacion">int con el tipo de relacion. Opcional sino se requiere envia -1</param>
        /// <returns>string con el listado de personas relacionadas</returns>
        [WebMethod(Description = "Obtiene el listado de personas asociadas a un usuario")]
        public string ObtenerPersonasAsociadasUsuario(long personaID, int tipoRelacion)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strLstPersona = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerPersonasAsociadasUsuario", "Id Persona: " + personaID + " -- Tipo de Relacion: " + tipoRelacion, "", 0);
                Persona objServicioPersona = new Persona();
                strLstPersona = objServicioPersona.ObtenerInformacionPersonasRelacionadas(personaID, tipoRelacion);
                return strLstPersona;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerPersonasAsociadasUsuario", ex.ToString(), iIdPadre);
                //SMLog.Escribir(ex);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerPersonasAsociadasUsuario", "Id Persona: " + personaID, "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Obtiene el listado de representantes asociados a un usuario
        /// </summary>
        /// <param name="personaVITALID">int con el identificador de la persona</param>
        /// <param name="informacionDetallada">bool con la informacion detallada del usuario</param>
        /// <returns>string con el listado de representantes</returns>
        [WebMethod(Description = "Obtiene el listado de representantes asociados a un usuario")]
        public string ObtenerPersonasRepresentanUsuario(long personaVITALID, bool informacionDetallada)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strLstPersona = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerPersonasRepresentanUsuario", "Id Persona: " + personaVITALID + " -- informacionDetallada: " + informacionDetallada, "", 0);
                Persona objServicioPersona = new Persona();
                strLstPersona = objServicioPersona.ObtenerPersonasRepresentanUsuario(personaVITALID, informacionDetallada);
                return strLstPersona;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerPersonasRepresentanUsuario", ex.ToString(), iIdPadre);
                //SMLog.Escribir(ex);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerPersonasRepresentanUsuario", "Personas: " + (strLstPersona ?? "null"), "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Obtiene el listado de personas asociadas a un usuario
        /// </summary>
        /// <param name="personaVITALID">int con el identificador de la persona en VITAL</param>
        /// <param name="tipoRelacion">int con el tipo de relacion. Opcional sino se requiere envia -1</param>
        /// <returns>string con el listado de personas relacionadas</returns>
        [WebMethod(Description = "Obtiene el listado de personas asociadas a un usuario dado su identificador en VITAL")]
        public string ObtenerPersonasAsociadasUsuarioIDVITAL(long personaVITALID, int tipoRelacion)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strLstPersona = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerPersonasAsociadasUsuarioIDVITAL", "Id Persona: " + personaVITALID + " -- Tipo de Relacion: " + tipoRelacion, "", 0);
                Persona objServicioPersona = new Persona();
                strLstPersona = objServicioPersona.ObtenerInformacionPersonasRelacionadasByAppId(personaVITALID, tipoRelacion);
                return strLstPersona;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerPersonasAsociadasUsuarioIDVITAL", ex.ToString(), iIdPadre);
                //SMLog.Escribir(ex);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerPersonasAsociadasUsuarioIDVITAL", strLstPersona, "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Verifica si el usuario configuro la segunda clave
        /// </summary>
        /// <param name="personaVITALID">int con el identificador de la persona en VITAL</param>
        /// <returns>bool con true en caso de haberla configurado</returns>
        [WebMethod(Description = "Verifica si el usuario configuro la segunda clave")]
        public bool TieneSegundaClave(long personaVITALID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            bool blnTieneClave = false;

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "TieneSegundaClave", "Id Persona: " + personaVITALID, "", 0);
                Persona objServicioPersona = new Persona();
                blnTieneClave = objServicioPersona.TieneSegundaClave(personaVITALID);
                return blnTieneClave;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "TieneSegundaClave", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "TieneSegundaClave", "blnTieneClave: " + blnTieneClave.ToString(), "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Valida la segunda clave
        /// </summary>
        /// <param name="personaVITALID">int con el identificador de la persona en VITAL</param>
        /// <param name="clavePersona">string con la clave de la persona</param>
        /// <returns>bool con true en caso de que la clave sea valida, false en caso contrario</returns>
        [WebMethod(Description = "Verifica si el usuario configuro la segunda clave")]
        public bool SegundaClaveValida(long personaVITALID, string clavePersona)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            bool blnClaveValida = false;

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "SegundaClaveValida", "Id Persona: " + personaVITALID, "", 0);
                Persona objServicioPersona = new Persona();
                blnClaveValida = objServicioPersona.SegundaClaveValida(personaVITALID, clavePersona);
                return blnClaveValida;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "SegundaClaveValida", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "SegundaClaveValida", "blnClaveValida: " + blnClaveValida.ToString(), "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Verifica si el usuario configuro la firma
        /// </summary>
        /// <param name="personaVITALID">int con el identificador de la persona en VITAL</param>
        /// <returns>bool con true en caso de haberla configurado</returns>
        [WebMethod(Description = "Verifica si el usuario configuro la firma")]
        public bool TieneFirma(long personaVITALID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            bool blnTieneFirma = false;

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "TieneFirma", "Id Persona: " + personaVITALID, "", 0);
                Persona objServicioPersona = new Persona();
                blnTieneFirma = objServicioPersona.TieneFirma(personaVITALID);
                return blnTieneFirma;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "TieneFirma", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "TieneFirma", "blnTieneFirma: " + blnTieneFirma.ToString(), "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Obtiene la firma de un usuario
        /// </summary>
        /// <param name="personaVITALID">personaVITALID</param>
        /// <returns>string con la informacion de la firma</returns>
        [WebMethod(Description = "Obtiene la firma de un usuario")]
        public string ObtenerFirma(long personaVITALID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strDatosFirma = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerFirma", "Id Persona: " + personaVITALID, "", 0);
                Persona objServicioPersona = new Persona();
                strDatosFirma = objServicioPersona.ObtenerFirma(personaVITALID);
                return strDatosFirma;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerFirma", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerFirma", "strDatosFirma: " + (strDatosFirma ?? "null"), "", iAA, iIdPadre);
            }
        }


        /// <summary>
        /// Obtiene el logo de un usuario
        /// </summary>
        /// <param name="personaVITALID">personaVITALID</param>
        /// <returns>string con la informacion del logo</returns>
        [WebMethod(Description = "Obtiene el logo de un usuario")]
        public string ObtenerLogo(long personaVITALID)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strDatosLogo = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ObtenerLogo", "Id Persona: " + personaVITALID, "", 0);
                Persona objServicioPersona = new Persona();
                strDatosLogo = objServicioPersona.ObtenerLogo(personaVITALID);
                return strDatosLogo;
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ObtenerLogo", ex.ToString(), iIdPadre);
                throw;
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ObtenerLogo", "strDatosLogo: " + (strDatosLogo ?? "null"), "", iAA, iIdPadre);
            }
        }



        /// <summary>
        /// Registra un nuevo usuario en el sistema VITAL
        /// </summary>
        /// <param name="informacionPersona">string que contiene el XML con la información de la persona</param>
        /// <returns>string con el XML que contiene el resultado del proceso y la información de la persona registrada</returns>
        [WebMethod(Description = "Registra un nuevo usuario en el sistema VITAL")]
        public string RegistrarPersona(string informacionPersona)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strRespuesta = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "RegistrarPersona", informacionPersona, "", 0);

                //Realizar el registro de la persona
                strRespuesta = PersonaRegistroFachada.GetInstance().RegistrarPersona(informacionPersona);
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "RegistrarPersona", ex.ToString(), iIdPadre);

                //Cargar mensaje de error
                PersonaRespuestaRegistroEntity objPersonaRespuestaRegistroEntity = new PersonaRespuestaRegistroEntity();                
                objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.ERROR).ToString();
                objPersonaRespuestaRegistroEntity.Mensaje = "Se presento un problema durante el registro de la persona";
                objPersonaRespuestaRegistroEntity.InformacionPersona = null;
                strRespuesta = objPersonaRespuestaRegistroEntity.GetXml();
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "RegistrarPersona", strRespuesta, "", iAA, iIdPadre);
            }

            return strRespuesta;
        }


        /// <summary>
        /// Actualiza la información de un usuario en el sistema VITAL
        /// </summary>
        /// <param name="informacionPersona">string que contiene el XML con la información de la persona</param>
        /// <returns>string con el XML que contiene el resultado del proceso y la información de la persona registrada</returns>
        [WebMethod(Description = "Actualiza la información de un usuario en el sistema VITAL")]
        public string ActualizarPersona(string informacionPersona)
        {
            Int64 iIdPadre = 0;
            Int32 iAA = 0;
            string strRespuesta = "";

            try
            {
                iIdPadre = SMLogWS.EscribirInicio(this.ToString(), "ActualizarPersona", informacionPersona, "", 0);

                //Realizar el registro de la persona
                strRespuesta = PersonaRegistroFachada.GetInstance().RegistrarPersona(informacionPersona);
            }
            catch (Exception ex)
            {
                SMLogWS.EscribirExcepcion(this.ToString(), "ActualizarPersona", ex.ToString(), iIdPadre);

                //Cargar mensaje de error
                PersonaRespuestaRegistroEntity objPersonaRespuestaRegistroEntity = new PersonaRespuestaRegistroEntity();
                objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.ERROR).ToString();
                objPersonaRespuestaRegistroEntity.Mensaje = "Se presento un problema durante la actualización de la persona";
                objPersonaRespuestaRegistroEntity.InformacionPersona = null;
                strRespuesta = objPersonaRespuestaRegistroEntity.GetXml();
            }
            finally
            {
                SMLogWS.EscribirFinalizar(this.ToString(), "ActualizarPersona", strRespuesta, "", iAA, iIdPadre);
            }

            return strRespuesta;
        }


    #endregion


    [WebMethod(Description = "Metodo de test del servicio")]
    public void Test()
    {
        return;
    }

}

