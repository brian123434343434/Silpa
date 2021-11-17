using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using System.Data;
using System.Data.SqlClient;


namespace SILPA.Servicios
{
    public class Persona
    {

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        private PersonaIdentity Identity;

        public PersonaIdentity PersonaIdentity 
        {
            get { return this.Identity; }
        }

        private PersonaDalc objPersonaDalc;

        public Persona()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Carga los datos de la clase persona
        /// </summary>
        public void CargarDatosPersona(string strIdUsuario)
        {
            this.objPersonaDalc = new PersonaDalc();
            this.Identity = objPersonaDalc.BuscarPersonaByUserId(strIdUsuario);
        }

        /// <summary>
        /// Devuelve un grupo de personas por autoridad ambiental
        /// </summary>
        public List<PersonaIdentity> CargarDatosPersonaByAutoridadAmbiental(int intAutoridadAmbiental,int personaID,int intEnProceso, string ruta)
        {
            this.objPersonaDalc = new PersonaDalc();
            return objPersonaDalc.ObtenerPersonasByAutoridadAmbiental(intAutoridadAmbiental, personaID, intEnProceso, ruta);
        }

        /// <summary>
        /// Devuelve un grupo de personas por autoridad ambiental con estado por aprobar y rechazado
        /// </summary>
        public List<PersonaIdentity> CargarDatosPersonaByAutoridadAmbientalAprobRec(int intAutoridadAmbiental, int personaID, string ruta)
        {
            this.objPersonaDalc = new PersonaDalc();
            return objPersonaDalc.ObtenerPersonasByAutoridadAmbientalAprobRec(intAutoridadAmbiental, personaID, ruta);
        }

        /// <summary>
        /// 01-jul-2010 - aegb
        /// Devuelve un grupo de personas por autoridad ambiental relacionadas con el numero vital
        /// </summary>
        public List<PersonaIdentity> CargarDatosPersonaByAutoridadAmbiental(int intAutoridadAmbiental, int personaID)
        {
            this.objPersonaDalc = new PersonaDalc();
            return objPersonaDalc.ObtenerPersonasByAutoridadAmbiental(intAutoridadAmbiental, personaID);
        }
        /// <summary>
        /// 04-oct-2010 - aegb
        /// Devuelve un grupo de numeros vital por autoridad ambiental relacionadas con el tramite
        /// </summary>
        public DataSet CargarNumeroVitalAutoridad(int tramiteID, int autoridadID)
        {
            this.objPersonaDalc = new PersonaDalc();
            return objPersonaDalc.ConsultarNumeroVitalAutoridad(tramiteID, autoridadID);
        }

        /// <summary>
        /// 04-oct-2010 - aegb
        /// Devuelve un grupo de numeros vital por autoridad ambiental relacionadas con el tramite
        /// </summary>
        public DataSet CargarNumeroVitalAutoridad(int tramiteID, int autoridadID, long personaID)
        {
            this.objPersonaDalc = new PersonaDalc();
            return objPersonaDalc.ConsultarNumeroVitalAutoridad(tramiteID, autoridadID, personaID);
        }

        /// <summary>
        /// Obtiene el correo electronico de una persona
        /// </summary>
        /// <param name="personaID">int con el identificador de la persona</param>
        /// <returns>string con el correo electronico de la persona</returns>
        public string ObtenerCorreoPersona(int personaID)
        {
            this.objPersonaDalc = new PersonaDalc();
            return objPersonaDalc.ObtenerCorreoPersona(personaID);
        }


        /// <summary>
        /// Obtener listado de usuarios activos en VITAL
        /// </summary>
        /// <returns>string con la informacion de los usuarios</returns>
        public string ObtenerListaUsuariosActivos()
        {
            XmlSerializador objXmlSerializador;
            List<PersonaIdentity> objLstPersonas;
            DataTable objDtPersonas;
            string strPersonas = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objDtPersonas = objPersona.ConsultarPersonasActivas();

            //verifcar si se obtuvo listado de personas
            if (objDtPersonas != null && objDtPersonas.Rows.Count > 0)
            {
                //Crear el listado de personas
                objLstPersonas = new List<AccesoDatos.Generico.PersonaIdentity>();

                //Ciclo que carga los datos de las personas
                foreach (DataRow objPersonaLista in objDtPersonas.Rows)
                {
                    objLstPersonas.Add(new PersonaIdentity
                                            {
                                                IdApplicationUser = Convert.ToInt64(objPersonaLista["PER_ID"]),
                                                PrimerNombre = (objPersonaLista["PER_PRIMER_NOMBRE"] != System.DBNull.Value ? objPersonaLista["PER_PRIMER_NOMBRE"].ToString() : ""),
                                                SegundoNombre = (objPersonaLista["PER_SEGUNDO_NOMBRE"] != System.DBNull.Value ? objPersonaLista["PER_SEGUNDO_NOMBRE"].ToString() : ""),
                                                PrimerApellido = (objPersonaLista["PER_PRIMER_APELLIDO"] != System.DBNull.Value ? objPersonaLista["PER_PRIMER_APELLIDO"].ToString() : ""),
                                                SegundoApellido = (objPersonaLista["PER_SEGUNDO_APELLIDO"] != System.DBNull.Value ? objPersonaLista["PER_SEGUNDO_APELLIDO"].ToString() : ""),
                                                RazonSocial = (objPersonaLista["PER_RAZON_SOCIAL"] != System.DBNull.Value ? objPersonaLista["PER_RAZON_SOCIAL"].ToString() : ""),
                                            });
                }

                //Serializar el resultado
                objXmlSerializador = new XmlSerializador();
                strPersonas = objXmlSerializador.serializar(objLstPersonas);
            }

            return strPersonas;

        }


        /// <summary>
        /// Obtenerel listado de personas activas que pertenecen a un role especifico
        /// </summary>
        /// <param name="p_intRoleID">int con el identifiacdor del role</param>
        /// <param name="p_blnInformacionDetallada">bool que indica si se extrae la informacion detallada</param>
        /// <returns>string con la informacion de las personas</returns>
        public string ObtenerPersonasPorRoles(int p_intRoleID, bool p_blnInformacionDetallada)
        {
            XmlSerializador objXmlSerializador;
            List<PersonaIdentity> objLstPersonas;
            string strPersonas = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objLstPersonas = objPersona.ObtenerPersonasPorRoles(p_intRoleID, p_blnInformacionDetallada);

            //verifcar si se obtuvo listado de personas
            if (objLstPersonas != null && objLstPersonas.Count > 0)
            {
                //Serializar el resultado
                objXmlSerializador = new XmlSerializador();
                strPersonas = objXmlSerializador.serializar(objLstPersonas);
            }

            return strPersonas;

        }


        /// <summary>
        /// Obtiene la información de una persona dado el tipo y número de identificación
        /// </summary>
        /// <param name="p_intTipoIdentificacion">int con tipo de identificación de la persona</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación de la persona</param>
        /// <returns>string que contiene la informacion de la persona en XML</returns>
        public string ObtenerInformacionPersonaIdentificacion(int p_intTipoIdentificacion, string p_strNumeroIdentificacion)
        {
            string strPersona = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objPersona.ObtenerInformacionPersonaIdentificacion(p_intTipoIdentificacion, p_strNumeroIdentificacion);

            //Verificar si se obtuvo persona
            if (objPersona.Identity != null && objPersona.Identity.PersonaId > 0)
            {
                //Obtener el lugar de expedicion del documento de identificacion
                if (!string.IsNullOrEmpty(objPersona.Identity.LugarExpediciónDocumento) && objPersona.Identity.LugarExpediciónDocumento != "-1")
                    objPersona.Identity.LugarExpediciónDocumento = Municipio.obtenerNomDepMunByMunId(int.Parse(objPersona.Identity.LugarExpediciónDocumento));
                else
                    objPersona.Identity.LugarExpediciónDocumento = "";

                strPersona = objPersona.Identity.GetXml();
            }
            else
            {
                strPersona = "<Error><Codigo>1</Codigo><Mensaje>Persona especificada no existe</Mensaje></Error>";
            }

            return strPersona;
        }



        /// <summary>
        /// Obtiene la informacion de una persona dado el identificador de gattaca
        /// </summary>
        /// <param name="p_lngIdApplicationUser">long con el identificador</param>
        /// <param name="p_blnDetalleInformacion">bool que indica si se obtiene el detalle de la informacion</param>
        /// <returns>string que contiene la informacion de la persona</returns>
        public string ObtenerInformacionPersonaByAppId(long p_lngIdApplicationUser, bool p_blnDetalleInformacion)
        {
            string strPersona = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objPersona.ObternerPersonaByUserIdApp(p_lngIdApplicationUser.ToString(), p_blnDetalleInformacion);

            //Verificar si se obtuvo persona
            if (objPersona.Identity != null)
            {
                //Obtener el lugar de expedicion del documento de identificacion
                if (!string.IsNullOrEmpty(objPersona.Identity.LugarExpediciónDocumento) && objPersona.Identity.LugarExpediciónDocumento != "-1")
                    objPersona.Identity.LugarExpediciónDocumento = Municipio.obtenerNomDepMunByMunId(int.Parse(objPersona.Identity.LugarExpediciónDocumento));
                else
                    objPersona.Identity.LugarExpediciónDocumento = "";

                strPersona = objPersona.Identity.GetXml();
            }
            else
            {
                strPersona = "ERROR: Persona especificada no existe";
            }

            return strPersona;
        }

        /// <summary>
        /// Obtiene el listado de personas asociadas a un usuario
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_intTipoRelacion">int con el tipo de relacion. Opcional sino se requiere envia -1</param>
        /// <returns>string con el listado de personas relacionadas</returns>
        public string ObtenerInformacionPersonasRelacionadas(long p_lngPersonaID, int p_intTipoRelacion)
        {
            XmlSerializador objXmlSerializador;
            List<PersonaIdentity> objLstPersonas;
            string strPersonas = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objLstPersonas = objPersona.ListaPersonasAsociadasSolicitanteApoderado(p_intTipoRelacion, p_lngPersonaID); 

            //verifcar si se obtuvo listado de personas
            if (objLstPersonas != null && objLstPersonas.Count > 0)
            {   
                //Serializar el resultado
                objXmlSerializador = new XmlSerializador();
                strPersonas = objXmlSerializador.serializar(objLstPersonas);
            }

            return strPersonas;

        }


        /// <summary>
        /// Obtiene el listado de representantes asociados a un usuario
        /// </summary>
        /// <param name="p_lngPersonaID">int con el identificador de la persona</param>
        /// <param name="p_blnInformacionDetallada">bool que indica si se extrae la informacion detallada</param>
        /// <returns>string con el listado de representantes</returns>
        public string ObtenerPersonasRepresentanUsuario(long p_lngPersonaID, bool p_blnInformacionDetallada)
        {
            XmlSerializador objXmlSerializador;
            List<PersonaIdentity> objLstPersonas;
            string strPersonas = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objLstPersonas = objPersona.ListaPersonasRepresentanUsuario(p_lngPersonaID, p_blnInformacionDetallada);

            //verifcar si se obtuvo listado de personas
            if (objLstPersonas != null && objLstPersonas.Count > 0)
            {
                //Serializar el resultado
                objXmlSerializador = new XmlSerializador();
                strPersonas = objXmlSerializador.serializar(objLstPersonas);
            }

            return strPersonas;

        }

        /// <summary>
        /// Obtiene el listado de personas asociadas a un usuario
        /// </summary>
        /// <param name="p_lngIdApplicationUser">long con el identificador de la persona en VITAL</param>
        /// <param name="p_intTipoRelacion">int con el tipo de relacion. Opcional sino se requiere envia -1</param>
        /// <returns>string con el listado de personas relacionadas</returns>
        public string ObtenerInformacionPersonasRelacionadasByAppId(long p_lngIdApplicationUser, int p_intTipoRelacion)
        {
            string strPersonas = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objPersona.ObternerPersonaByUserIdApp(p_lngIdApplicationUser.ToString());

            //Obtener las personas
            if (objPersona != null && objPersona.Identity != null && objPersona.Identity.PersonaId > 0)
                strPersonas = this.ObtenerInformacionPersonasRelacionadas(Convert.ToInt64(objPersona.Identity.PersonaId), p_intTipoRelacion);

            return strPersonas;
        }


        /// <summary>
        /// Verifica si el usuario configuro la segunda clave
        /// </summary>
        /// <param name="p_lngIdApplicationUser">int con el identificador de la persona en VITAL</param>
        /// <returns>bool con true en caso de haberla configurado, false en caso contrario</returns>
        public bool TieneSegundaClave(long p_lngIdApplicationUser)
        {
            bool blnTieneSegundaClave;

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            blnTieneSegundaClave = objPersona.TieneSegundaClave(Convert.ToInt32(p_lngIdApplicationUser));

            return blnTieneSegundaClave;
        }


        /// <summary>
        /// Valida la segunda clave
        /// </summary>
        /// <param name="p_lngIdApplicationUser">int con el identificador de la persona en VITAL</param>
        /// <param name="p_strClave">string con la clave de la persona</param>
        /// <returns>bool con true en caso de que la clave sea valida, false en caso contrario</returns>
        public bool SegundaClaveValida(long p_lngIdApplicationUser, string p_strClave)
        {
            bool blnClaveValida;

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            blnClaveValida = objPersona.ValidarSegundaClave(Convert.ToInt32(p_lngIdApplicationUser), EnDecript.Encriptar(p_strClave));

            return blnClaveValida;
        }


        /// <summary>
        /// Verifica si el usuario configuro la firma
        /// </summary>
        /// <param name="p_lngIdApplicationUser">int con el identificador de la persona en VITAL</param>
        /// <returns>bool con true en caso de haberla configurado</returns>
        public bool TieneFirma(long p_lngIdApplicationUser)
        {
            bool blnTieneFirma;

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            blnTieneFirma = objPersona.TieneFirma(Convert.ToInt32(p_lngIdApplicationUser));

            return blnTieneFirma;
        }


        /// <summary>
        /// Obtiene la firma de un usuario
        /// </summary>
        /// <param name="p_lngIdApplicationUser">personaVITALID</param>
        /// <returns>string con la informacion de la firma</returns>
        public string ObtenerFirma(long p_lngIdApplicationUser)
        {
            XmlSerializador objXmlSerializador;
            PersonaFirmaIdentity objFirma;
            string strDatosFirma = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objFirma = objPersona.ConsultarFirma(Convert.ToInt32(p_lngIdApplicationUser));

            //Obtener las personas
            if (objFirma != null)
            {
                objFirma.ImagenBase64 = Convert.ToBase64String(objFirma.Imagen, 0, objFirma.Imagen.Length, Base64FormattingOptions.None);
                objFirma.Imagen = null;

                //Serializar el resultado
                objXmlSerializador = new XmlSerializador();
                strDatosFirma = objXmlSerializador.serializar(objFirma);
            }

            return strDatosFirma;
        }


        /// <summary>
        /// Obtiene el logo para el usuario indicado
        /// </summary>
        /// <param name="p_lngIdApplicationUser">personaVITALID</param>
        /// <returns>string con la informacion del logo</returns>
        public string ObtenerLogo(long p_lngIdApplicationUser)
        {
            XmlSerializador objXmlSerializador;
            PersonaLogoIdentity objLogo;
            string strDatosLogo = "";

            //Obtener la informacion de la persona
            SILPA.LogicaNegocio.Generico.Persona objPersona = new LogicaNegocio.Generico.Persona();
            objLogo = objPersona.ConsultarLogo(Convert.ToInt32(p_lngIdApplicationUser));

            //Obtener las personas
            if (objLogo != null)
            {
                objLogo.LogoBase64 = Convert.ToBase64String(objLogo.Logo, 0, objLogo.Logo.Length, Base64FormattingOptions.None);
                objLogo.Logo = null;

                //Serializar el resultado
                objXmlSerializador = new XmlSerializador();
                strDatosLogo = objXmlSerializador.serializar(objLogo);
            }

            return strDatosLogo;
        }

    }
}
