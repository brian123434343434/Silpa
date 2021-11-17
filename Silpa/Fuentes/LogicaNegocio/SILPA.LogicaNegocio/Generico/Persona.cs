using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using System.Data;


namespace SILPA.LogicaNegocio.Generico
{
    [Serializable]
    public class Persona
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public Persona()
        {
            this._objConfiguracion = new Configuracion();
            this.Dalc = new PersonaDalc();
            this.Identity = new PersonaIdentity();
            this.DalcDir = new DireccionPersonaDalc();
            this.IdentityDir = new DireccionPersonaIdentity();
        }

        #region Atributos de la entidad
        private Configuracion _objConfiguracion;
        private PersonaDalc Dalc;
        public PersonaIdentity Identity;
        private DireccionPersonaDalc DalcDir;
        public DireccionPersonaIdentity IdentityDir;
        //public PersonaIndentity Identity;

        #endregion

        #region Métodos de la clase ..

        public DataSet PersonasAsociadasSolicitante(TipoPersona tipoPersona, int solicitanteID)
        {
            this.Identity.TipoPersona.CodigoTipoPersona = (int)tipoPersona;
            this.Identity.PersonaId = solicitanteID;
            return this.Dalc.ObtenerPersonasAsociadasSolicitante(ref this.Identity);
        }

        public DataSet PersonasAsociadasSolicitanteLeft(int tipoPersona, int solicitanteID)
        {
            this.Identity.TipoPersona.CodigoTipoPersona = tipoPersona;
            this.Identity.PersonaId = solicitanteID;
            return this.Dalc.ObtenerPersonasAsociadasSolicitanteLeft(ref this.Identity);
        }

        public DataSet PersonasFiltro(string nombre, string idNumero)
        {            
            return this.Dalc.ObtenerPersonasFiltro(idNumero,nombre);
        }

        /// <summary>
        /// Obtener el listado de personas asociadas
        /// </summary>
        /// <param name="tipoPersona">int con el tipo de persona</param>
        /// <param name="solicitanteID">long con el identifiacdor del solicitante</param>
        /// <returns>List con la informacion de las personas</returns>
        public DataSet PersonasAsociadasSolicitanteApoderado(int tipoPersona, long solicitanteID)
        {
            this.Identity.TipoPersona.CodigoTipoPersona = tipoPersona;
            this.Identity.PersonaId = solicitanteID;
            return this.Dalc.ObtenerPersonasAsociadasSolicitanteApoderado(this.Identity);
        }

        /// <summary>
        /// Obtenerel listado de personas activas que pertenecen a un role especifico
        /// </summary>
        /// <param name="p_intRoleID">int con el identifiacdor del role</param>
        /// <param name="p_blnInformacionDetallada">bool que indica si se extrae la informacion detallada</param>
        /// <returns>List con la informacion de las personas</returns>
        public List<PersonaIdentity> ObtenerPersonasPorRoles(int p_intRoleID, bool p_blnInformacionDetallada)
        {

            List<PersonaIdentity> objLstPersonas = this.Dalc.ObtenerPersonasPorRoles(p_intRoleID, p_blnInformacionDetallada);

            //Cargar las direcciones por cada una de las personas del listado
            if (objLstPersonas != null && objLstPersonas.Count > 0 && p_blnInformacionDetallada)
            {
                //Ciclo que carga la informacion de direcciones de la persona
                foreach (PersonaIdentity objPersona in objLstPersonas)
                {
                    //Cargar direccion persona
                    objPersona._direccionPersona.IdPersona = objPersona.PersonaId;
                    this.DalcDir.ObtenerDireccionPersona(ref objPersona._direccionPersona);

                    //Cargar direcciones relacionadas
                    objPersona.Direcciones = this.DalcDir.ObtenerDirecciones(objPersona.PersonaId);
                }
            }

            return objLstPersonas;
        }


        /// <summary>
        /// Obtener el listado de personas asociadas
        /// </summary>
        /// <param name="tipoPersona">int con el tipo de persona</param>
        /// <param name="solicitanteID">long con el identifiacdor del solicitante</param>
        /// <returns>List con la informacion de las personas</returns>
        public List<PersonaIdentity> ListaPersonasAsociadasSolicitanteApoderado(int tipoPersona, long solicitanteID)
        {
            this.Identity.TipoPersona.CodigoTipoPersona = tipoPersona;
            this.Identity.PersonaId = solicitanteID;
            List<PersonaIdentity> objLstPersonas = this.Dalc.ListaObtenerPersonasAsociadasSolicitanteApoderado(this.Identity);

            //Cargar las direcciones por cada una de las personas del listado
            if(objLstPersonas != null && objLstPersonas.Count > 0)
            {
                //Ciclo que carga la informacion de direcciones de la persona
                foreach(PersonaIdentity objPersona in objLstPersonas)
                {
                    //Cargar direccion persona
                    objPersona._direccionPersona.IdPersona = objPersona.PersonaId;
                    this.DalcDir.ObtenerDireccionPersona(ref objPersona._direccionPersona);

                    //Cargar direcciones relacionadas
                    objPersona.Direcciones = this.DalcDir.ObtenerDirecciones(objPersona.PersonaId);
                }
            }
              
            return objLstPersonas;
        }


        /// <summary>
        /// Obtiene el listado de representantes asociados a un usuario
        /// </summary>
        /// <param name="p_lngSolicitanteID">long con el identificador de la persona</param>
        /// <param name="p_blnInformacionCompleta">bool que indica si extrae la informacion detallada del usuario</param>
        /// <returns>string con el listado de representantes</returns>
        public List<PersonaIdentity> ListaPersonasRepresentanUsuario(long p_lngSolicitanteID, bool p_blnInformacionCompleta)
        {
            List<PersonaIdentity> objLstPersonas = this.Dalc.ListaPersonasRepresentanUsuario(p_lngSolicitanteID, p_blnInformacionCompleta);

            //Cargar las direcciones por cada una de las personas del listado
            if (objLstPersonas != null && objLstPersonas.Count > 0 && p_blnInformacionCompleta)
            {
                //Ciclo que carga la informacion de direcciones de la persona
                foreach (PersonaIdentity objPersona in objLstPersonas)
                {
                    //Cargar direccion persona
                    objPersona._direccionPersona.IdPersona = objPersona.PersonaId;
                    this.DalcDir.ObtenerDireccionPersona(ref objPersona._direccionPersona);

                    //Cargar direcciones relacionadas
                    objPersona.Direcciones = this.DalcDir.ObtenerDirecciones(objPersona.PersonaId);
                }
            }

            return objLstPersonas;
        }

        /// <summary>
        /// Método que obtiene los datos de la persona por autoridad ambiental
        /// </summary>
        /// <param name="intAutoridadAmbientalID"></param>
        /// <param name="personaID"></param>
        /// <param name="blnEnProceso"></param>
        public void PersonasByAutoridadAmbiental(int intAutoridadAmbientalID, int personaID, int blnEnProceso, string ruta)
        {
            this.Dalc.ObtenerPersonasByAutoridadAmbiental(intAutoridadAmbientalID, personaID, blnEnProceso, ruta);
        }

        public void PersonaByNumeroIdentificacion(string strNumeroIdentificacion)
        {
            this.Identity.NumeroIdentificacion = strNumeroIdentificacion;
            this.Dalc.ObtenerPersonaByNumeroIdentificacion(ref this.Identity);
        }

        public void PersonaPorNumeroIdentificacion(string strNumeroIdentificacion, int tipoDocumentoIdentificacion)
        {
            this.Identity.NumeroIdentificacion = strNumeroIdentificacion;
            this.Identity.TipoDocumentoIdentificacion.Id =  tipoDocumentoIdentificacion;
            this.Dalc.ObtenerPersonaPorNumeroIdentificacion(ref this.Identity);
        }


        public bool ConsultarPersonaPol(string user, string pass)
        { 
           PersonaDalc objPersona = new PersonaDalc();
           return objPersona.ConsultarPersonaPol(user,pass);
        }

        public bool ConsultarPersonaSun(string user)
        {
            PersonaDalc objPersona = new PersonaDalc();
            DataSet ds=objPersona.ConsultarPersonaSun(user);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool ConsultarPersonaAsociaApod(string user)
        { 
           PersonaDalc objPersona = new PersonaDalc();
           return objPersona.ConsultarPersonaAsociaApod(user);
        }

        public bool ConsultarPersonaAsociaRepre(string user)
        {
            PersonaDalc objPersona = new PersonaDalc();
            return objPersona.ConsultarPersonaAsociaRepre(user);
        }
        
        /// <summary>
        /// Método que obtiene los datos de la entidad 
        /// persona mediante el identificador en la base de datos
        /// </summary>
        public void PersonaById(int intIdPersona)
        {
            Identity = new PersonaIdentity();
            Identity.PersonaId = intIdPersona;
            this.Identity._direccionPersona.IdPersona = this.Identity.PersonaId;
            this.DalcDir.ObtenerDireccionPersona(ref this.Identity._direccionPersona);

            //_objPersonaDalc = new PersonaDalc();
        }


        /// <summary>
        /// Método que obtiene los datos de la entidad 
        /// persona mediante el identificador en la base de datos
        /// </summary>
        public void ObtenerInformacionPersonaIdentificacion(int p_intTipoIdentificacion, string p_strNumeroIdentificacion)
        {
            this.Identity.TipoDocumentoIdentificacion = new AccesoDatos.Notificacion.TipoIdentificacionEntity { Id = p_intTipoIdentificacion };
            this.Identity.NumeroIdentificacion = p_strNumeroIdentificacion;
            this.Identity.PersonaId = -1;
            this.Identity.TipoPersona.CodigoTipoPersona = -1;
            this.Dalc.ObtenerPersonaPorNumeroIdentificacion(ref this.Identity);
        }


        /// <summary>
        /// Método que obtiene los datos de la entidad 
        /// persona mediante el identificador en la base de datos
        /// </summary>
        public void ObternerPersonaByUserIdApp(string strIdPersona, bool p_blnDetalleInformacion = true)
        {
            this.Identity.IdApplicationUser = Convert.ToInt64(strIdPersona);
            this.Identity.PersonaId = -1;
            this.Identity.TipoPersona.CodigoTipoPersona = -1;
            this.Dalc.ObtenerPersona(ref this.Identity, p_blnDetalleInformacion);

            if (p_blnDetalleInformacion)
            {
                this.Identity._direccionPersona.IdPersona = this.Identity.PersonaId;
                this.DalcDir.ObtenerDireccionPersona(ref this.Identity._direccionPersona);
                this.Identity.Direcciones = this.DalcDir.ObtenerDirecciones(this.Identity.PersonaId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdPersona"></param>
        public void PersonaByUserId(string strIdPersona)
        {
            this.Identity.IdApplicationUser = Convert.ToInt64(strIdPersona);
            this.Dalc.ObtenerPersona(ref this.Identity);
            this.Identity._direccionPersona.IdPersona = this.Identity.PersonaId;
            this.DalcDir.ObtenerDireccionPersona(ref this.Identity._direccionPersona);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdPersona"></param>
        public void ObternerPersonaByNumeroSilpa(string strNumSilpa)
        {
            this.Dalc.ObternerPersonaByNumeroSilpa(strNumSilpa, ref this.Identity);
            List<DireccionPersonaIdentity> objListDir = this.DalcDir.ObtenerDirecciones(this.Identity.PersonaId);
            if (objListDir != null)
            {
                foreach (DireccionPersonaIdentity objDir in objListDir)
                {
                    if (objDir.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Domicilio)
                        this.Identity._direccionPersona = objDir;
                    else if (objDir.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Correspondencia)
                        this.IdentityDir = objDir;
                }
            }

        }

        public void ObtenerPersonaNotificacion(long idPersona)
        {
            this.Dalc.ObtenerPersonaNotificacion(idPersona, ref this.Identity);
            List<DireccionPersonaIdentity> objListDir = this.DalcDir.ObtenerDirecciones(this.Identity.PersonaId);
            foreach (DireccionPersonaIdentity objDir in objListDir)
            {
                if (objDir.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Domicilio)
                    this.Identity._direccionPersona = objDir;
                else if (objDir.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Correspondencia)
                    this.IdentityDir = objDir;
            }
        }

        /// <summary>
        /// Asigna los valores a una instacia del objeto persona la cual tenga asignado el processInstance
        /// </summary>
        /// <param name="strIdPersona">identificador de la instancia del processo</param>
        public void ObternerPersonaByProcessInstace(int intProcessInstace)
        {
            this.Dalc.ObternerPersonaByProcessInstace(intProcessInstace, ref this.Identity);
            List<DireccionPersonaIdentity> objListDir= this.DalcDir.ObtenerDirecciones(this.Identity.PersonaId);
            if (objListDir != null)
            {
                foreach (DireccionPersonaIdentity objDir in objListDir)
                {
                    if (objDir.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Domicilio)
                        this.Identity._direccionPersona = objDir;
                    else if (objDir.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Correspondencia)
                        this.IdentityDir = objDir;
                }
            }
            
        }

        /// <summary>
        /// Método que obtiene los datos de la entidad 
        /// persona mediante el identificador en la base de datos
        /// </summary>
        public void ObternerPersonaByIdSolicitante(string strIdPersona)
        {
            this.Identity.IdSolicitante = Convert.ToInt64(strIdPersona);
            this.Dalc.ObtenerPersona(ref this.Identity);
            this.Identity._direccionPersona.IdPersona = this.Identity.PersonaId;
            this.DalcDir.ObtenerDireccionPersona(ref this.Identity._direccionPersona);

        }

        /// <summary>
        /// Método que obtiene los datos de la entidad 
        /// persona mediante el identificador en la base de datos
        /// </summary>
        public void ObternerPersonaByUsername(string strUsername)
        {
            try
            {
	            this.Identity = this.Dalc.BuscarPersonaByUsername(strUsername);
	            this.Dalc.ObtenerPersona(ref this.Identity);
	            this.Identity._direccionPersona.IdPersona = this.Identity.PersonaId;
	            this.DalcDir.ObtenerDireccionPersona(ref this.Identity._direccionPersona);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al obtener los datos de la entidad persona.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Método que determina la existencia de una persona mediante el número de identificación
        /// </summary>
        /// <param name="strNumeroIdentificacion">string: número de identificación </param>
        /// <returns>True/false</returns>
        public int VerificarExistenciaByNumeroIdentificacion(string strNumeroIdentificacion) 
        {
            PersonaDalc dalc = new PersonaDalc();
            return dalc.ExistePersona(strNumeroIdentificacion);
        }

        /// <summary>
        /// crea un los registros necesario para que un usuario sea adicionado al sistema
        /// </summary>
        /// <param name="strCode">Cedula</param>
        /// <param name="strName">UserName BPM</param>
        /// <param name="strPrimerNombre">Primer Nombre</param>
        /// <param name="strSegundoNombre">Segundo Nombre</param>
        /// <param name="strPrimerApellido">Primer Apellido</param>
        /// <param name="strSegundoApellido">Segundo Apellido</param>
        /// <param name="strIdlocality">Debe ser 1 por defecto</param>
        /// <param name="strTipoPersona">Tipo Persona</param>
        /// <param name="strIdentification">Numero de documento de Identification</param>
        /// <param name="strEmail">eMail</param>
        /// <param name="intIdTipoId">Identificador de tipo de identificacion</param>
        /// <param name="strLugarExpDoc">Lugar de expediciòn del documento de identidad </param>
        /// <param name="intPais">Identificador del pais</param>
        /// <param name="strTelefono">Numero telefonico</param>
        /// <param name="strCelular">Numero del celular</param>
        /// <param name="strFax">Numero de fax</param>
        /// <param name="strRazonSocial">Razon social</param>
        /// <param name="intIdCodTpPersona">identificador del codigo del tipo de persona</param>
        /// <param name="strOtro">Otro</param>
        /// <param name="strTarjetaPro">Numero de la tarjeta profesional</param>
        /// <param name="int64IdSolicitante">identificador del solicitante asociado a esta persona</param>
        /// <param name="intAutoridadAmbiental">identificador de la autoridad ambiental</param>
        /// <param name="strPregunta">pregunta en caso de olvidar contraseña</param>
        /// <param name="strRespuesta">respuesta a pregunta en caso de olvidar contraseña</param>
        /// <param name="intExpiration">tiempo en dias de expiracion de contraseña</param>
        /// <param name="strEnabled">T o F</param>
        /// <param name="strActive">T o F</param>
        /// <param name="strImageuser">por defecto debe ser NoImage.gif</param>
        /// <param name="strChangepassword">T o F</param>
        /// <returns>Identificador el registro del usuario que se acaba de ingresar.</returns>
        public Int64 InsertarPersona(string strCode, string strName, string strPassword, string strPrimerNombre, string strSegundoNombre, string strPrimerApellido,
            string strSegundoApellido, int intIdlocality, string strTipoPersona, string strIdentification, string strEmail, int intIdTipoId,
            string strLugarExpDoc, int intPais, string strTelefono, string strCelular, string strFax, string strRazonSocial,
            int intIdCodTpPersona, string strTarjetaPro, Int64 int64IdSolicitante, int intAutoridadAmbiental,
            string strPregunta, string strRespuesta, int intExpiration, string strEnabled, string strActive, 
            string strImageuser, string strChangepassword,
            int intDepartamento, int intMunicipio, int intVereda, int intCorregimiento, int intIdCodTpSolicitante, bool autorizaCorreo) //jmartinez adiciono campo autoriza correo
        {

            //try
            //{
            this.Identity.PrimerNombre = strPrimerNombre;
            this.Identity.SegundoNombre = strSegundoNombre;
            this.Identity.PrimerApellido = strPrimerApellido;
            this.Identity.SegundoApellido = strSegundoApellido;
            this.Identity.NumeroIdentificacion = strIdentification;
            this.Identity.CorreoElectronico = strEmail;
            this.Identity.TipoDocumentoIdentificacion.Id = intIdTipoId;
            this.Identity.LugarExpediciónDocumento = strLugarExpDoc;
            this.Identity.Pais = intPais;
            this.Identity.Telefono = strTelefono;
            this.Identity.Celular = strCelular;
            this.Identity.Fax = strFax;
            this.Identity.IdApplicationUser = 0;
            this.Identity.RazonSocial = strRazonSocial;
            this.Identity.TipoPersona.CodigoTipoPersona = intIdCodTpPersona;
            //this.Identity.Otro = strOtro;
            this.Identity.TarjetaProfesional = strTarjetaPro;
            this.Identity.IdSolicitante = int64IdSolicitante;
            this.Identity.IdAutoridadAmbiental = intAutoridadAmbiental;
            this.Identity.Pregunta = strPregunta;
            this.Identity.Respuesta = strRespuesta;
            //this.Identity.Activo = Convert.ToBoolean(Convert.ToInt32(strActive.ToString()));
            this.Identity.Activo = Convert.ToBoolean(strActive.ToString());
            this.Identity.TipoSolicitante.CodigoTipoPersona = intIdCodTpSolicitante;

            //datos de la direccion
            this.IdentityDir.PaisId = this.Identity.Pais;
            this.IdentityDir.MunicipioId = intMunicipio;
            this.IdentityDir.VeredaId = intVereda;
            this.IdentityDir.CorregimientoId = intCorregimiento;

            //jmartinez Adiciono campo autoriza correo 
            this.Identity.AutorizaCorreo = autorizaCorreo;


            if (int64IdSolicitante != 0)
            {
                this.Dalc.InsertarPersona(ref this.Identity);
                this.IdentityDir.IdPersona = this.Identity.PersonaId;
                return this.Identity.PersonaId;
            }
            else
            {
                // JUNIO 25 - 2010 JMM
                string strNombre = "";
                string strApellidos = "";

                if (intIdCodTpPersona == (int)TipoPersona.Natural)
                {
                        strNombre = strPrimerNombre + " " + strSegundoNombre + " " + strPrimerApellido + " " + strSegundoApellido;
                        strApellidos = strPrimerApellido + " " + strSegundoApellido;

                        this.Identity.IdApplicationUser = this.Dalc.InsertarPersonaBPM(strCode, strNombre, strPassword, strPrimerNombre, strSegundoNombre, strApellidos,
                            intIdlocality, strTipoPersona, strIdentification, strEmail, intExpiration, strEnabled, strActive, strImageuser, strChangepassword);                
                }
                else if ((intIdCodTpPersona == (int)TipoPersona.JuridicaPublica) || (intIdCodTpPersona == (int)TipoPersona.JuridicaPrivada))  
                {
                        strNombre = strRazonSocial;
                        strApellidos = "";

                        this.Identity.IdApplicationUser = this.Dalc.InsertarPersonaBPM(strCode, strNombre, strPassword, strNombre, "", strApellidos,
                            intIdlocality, strTipoPersona, strIdentification, strEmail, intExpiration, strEnabled, strActive, strImageuser, strChangepassword);                
                }

                //string strNombre = strPrimerNombre + " " + strSegundoNombre + " " + strPrimerApellido + " " + strSegundoApellido; 
                //string strApellidos = strPrimerApellido + " " + strSegundoApellido;

                //this.Identity.IdApplicationUser = this.Dalc.InsertarPersonaBPM(strCode, strNombre, strPassword, strPrimerNombre, strSegundoNombre, strApellidos,
                //    intIdlocality, strTipoPersona, strIdentification, strEmail, intExpiration, strEnabled, strActive, strImageuser, strChangepassword); 
                ////this.Identity.IdApplicationUser = this.Dalc.InsertarPersonaBPM(strCode, strName, strPassword, strPrimerNombre, strSegundoApellido, strPrimerApellido,
                ////    intIdlocality, strTipoPersona, strIdentification, strEmail, intExpiration, strEnabled, strActive, strImageuser, strChangepassword);
                
                //JMM
                this.Dalc.InsertarPersona(ref this.Identity);
                this.IdentityDir.TipoDireccion=(int)SILPA.Comun.TipoDireccion.Domicilio;
                this.IdentityDir.IdPersona = this.Identity.PersonaId;
                this.DalcDir.InsertarDireccionPersona(ref this.IdentityDir);
                return this.Identity.PersonaId;
            }
        }
        
        /// <summary>
        /// crea un los registros necesario para que un usuario sea adicionado al sistema
        /// </summary>
        /// <param name="strCode">Cedula</param>
        /// <param name="strName">UserName BPM</param>
        /// <param name="strPrimerNombre">Primer Nombre</param>
        /// <param name="strSegundoNombre">Segundo Nombre</param>
        /// <param name="strPrimerApellido">Primer Apellido</param>
        /// <param name="strSegundoApellido">Segundo Apellido</param>
        /// <param name="strIdlocality">Debe ser 1 por defecto</param>
        /// <param name="strTipoPersona">Tipo Persona</param>
        /// <param name="strIdentification">Numero de documento de Identification</param>
        /// <param name="strEmail">eMail</param>
        /// <param name="intIdTipoId">Identificador de tipo de identificacion</param>
        /// <param name="strLugarExpDoc">Lugar de expediciòn del documento de identidad </param>
        /// <param name="intPais">Identificador del pais</param>
        /// <param name="strTelefono">Numero telefonico</param>
        /// <param name="strCelular">Numero del celular</param>
        /// <param name="strFax">Numero de fax</param>
        /// <param name="strRazonSocial">Razon social</param>
        /// <param name="intIdCodTpPersona">identificador del codigo del tipo de persona</param>
        /// <param name="strOtro">Otro</param>
        /// <param name="strTarjetaPro">Numero de la tarjeta profesional</param>
        /// <param name="int64IdSolicitante">identificador del solicitante asociado a esta persona</param>
        /// <param name="intAutoridadAmbiental">identificador de la autoridad ambiental</param>
        /// <param name="strPregunta">pregunta en caso de olvidar contraseña</param>
        /// <param name="strRespuesta">respuesta a pregunta en caso de olvidar contraseña</param>
        /// <param name="intExpiration">tiempo en dias de expiracion de contraseña</param>
        /// <param name="strEnabled">T o F</param>
        /// <param name="strActive">T o F</param>
        /// <param name="strImageuser">por defecto debe ser NoImage.gif</param>
        /// <param name="strChangepassword">T o F</param>
        /// <returns>Identificador el registro del usuario que se acaba de ingresar.</returns>
        public Int64 InsertarPersona(string strPrimerNombre, string strSegundoNombre, string strPrimerApellido,
            string strSegundoApellido, int intIdlocality, string strTipoPersona, string strIdentification, string strEmail, int intIdTipoId,
            string strLugarExpDoc, int intPais, string strTelefono, string strCelular, string strFax, string strRazonSocial,
            int intIdCodTpPersona, string strTarjetaPro, Int64 int64IdSolicitante, int intAutoridadAmbiental,
            string strPregunta, string strRespuesta,
            int intDepartamento, int intMunicipio, int intVereda, int intCorregimiento, int intIdApplicationUser, int intIdCodTpSolicitante)
        {

            this.Identity.PrimerNombre = strPrimerNombre;
            this.Identity.SegundoNombre = strSegundoNombre;
            this.Identity.PrimerApellido = strPrimerApellido;
            this.Identity.SegundoApellido = strSegundoApellido;
            this.Identity.NumeroIdentificacion = strIdentification;
            this.Identity.CorreoElectronico = strEmail;
            this.Identity.TipoDocumentoIdentificacion.Id = intIdTipoId;
            this.Identity.LugarExpediciónDocumento = strLugarExpDoc;
            this.Identity.Pais = intPais;
            this.Identity.Telefono = strTelefono;
            this.Identity.Celular = strCelular;
            this.Identity.Fax = strFax;
            this.Identity.IdApplicationUser = intIdApplicationUser;
            this.Identity.RazonSocial = strRazonSocial;
            this.Identity.TipoPersona.CodigoTipoPersona = intIdCodTpPersona;
            //this.Identity.Otro = strOtro;
            this.Identity.TarjetaProfesional = strTarjetaPro;
            this.Identity.IdSolicitante = int64IdSolicitante;
            this.Identity.IdAutoridadAmbiental = intAutoridadAmbiental;
            this.Identity.Pregunta = strPregunta;
            this.Identity.Respuesta = strRespuesta;

            //datos de la direccion
            //this.IdentityDir.PaisId = intPais;
            this.IdentityDir.MunicipioId = intMunicipio;
            this.IdentityDir.VeredaId = intVereda;
            this.IdentityDir.CorregimientoId = intCorregimiento;
            this.Identity.TipoSolicitante.CodigoTipoPersona = intIdCodTpSolicitante;

            //insertar
            this.Dalc.InsertarPersona(ref this.Identity);
            this.IdentityDir.IdPersona = this.Identity.PersonaId;
            if (IdentityDir.DireccionPersona != null)
            {
                this.DalcDir.InsertarDireccionPersona(ref this.IdentityDir);
            }
            return this.Identity.PersonaId;
        }

        public Int64 ActualizaApoderadoRepresentante(string strPrimerNombre, string strSegundoNombre, string strPrimerApellido, string strSegundoApellido, string strIdendificacion, 
            int iTID, string IdMunicipioExp, string strTelefono, string strCelular, string strFax,
            string strCorreo, int IdAppUser, int IdTipopersona, string strTarfetaProfesional, int IdSolicitante,
            int IdAutoridadAmbiental, bool bPerSila, int iTipoSolicitante, string strDireccion, int IdPais, 
            int IdMunicipio, int IdVereda, int IdCorregimiento, int iTipoDireccion, bool bEstado)
        {

            this.Identity.PrimerNombre = strPrimerNombre;
            this.Identity.SegundoNombre = strSegundoNombre;
            this.Identity.PrimerApellido = strPrimerApellido;
            this.Identity.SegundoApellido = strSegundoApellido;
            this.Identity.NumeroIdentificacion = strIdendificacion;
            this.Identity.TipoDocumentoIdentificacion.Id = iTID;
            this.Identity.LugarExpediciónDocumento = IdMunicipioExp;
            this.Identity.Telefono = strTelefono;
            this.Identity.Celular = strCelular;
            this.Identity.Fax = strFax;
            this.Identity.CorreoElectronico = strCorreo;
            this.Identity.IdApplicationUser = 0;
            this.Identity.TipoPersona.CodigoTipoPersona = IdTipopersona;
            this.Identity.TarjetaProfesional = strTarfetaProfesional;
            this.Identity.IdSolicitante = IdSolicitante;
            this.Identity.IdAutoridadAmbiental = IdAutoridadAmbiental;
            this.Identity.TipoSolicitante.CodigoTipoPersona = iTipoSolicitante;
            this.Identity.Pais = IdPais;
            this.Identity.Activo = bEstado;
                     
            //datos de la direccion

            this.IdentityDir.DireccionPersona = strDireccion;
            this.IdentityDir.PaisId = this.Identity.Pais;
            this.IdentityDir.MunicipioId = IdMunicipio;
            this.IdentityDir.VeredaId = IdVereda;
            this.IdentityDir.CorregimientoId = IdCorregimiento;
            this.IdentityDir.TipoDireccion = iTipoDireccion;

            this.Dalc.ActualizaApoderado(ref this.Identity, ref this.IdentityDir);
            return -1;
        }

        public void ActualizaApoderadoAcreditacion(Int64 IdSolicitante, string IdApoderado, Int64 tipIdAcreditacion)
        {
            this.DalcDir.ActualizarTipoDocumentoAcreditacion(IdSolicitante,IdApoderado, tipIdAcreditacion);
            
        }

        /// <summary>
        /// Hava: 26-May-10
        /// Registra la dirección de expedición del documento de identificación o de correspondencia
        /// </summary>
        /// <param name="idDeptoExpedicionDocumento">int: identificador del departamento donde fué expedido el documnento</param>
        /// <param name="idMunExpedicionDocumento">int: identificador del municipio donde fué expedido el documnento</param>
        public void InsertarDireccion(int idPais, int idDeptoExpedicionDocumento, 
                                      int idMunExpedicionDocumento, int idCorregimiento, 
                                      int idVereda, int idTipoDireccion, 
                                      string strDireccion ) 
        { 
            DireccionPersonaIdentity objIdentityCorrespondencia = new DireccionPersonaIdentity();
            objIdentityCorrespondencia.IdPersona = this.IdentityDir.IdPersona;
            objIdentityCorrespondencia.PaisId = idPais;
            objIdentityCorrespondencia.DepartamentoId = idDeptoExpedicionDocumento;
            objIdentityCorrespondencia.MunicipioId = idMunExpedicionDocumento;
            objIdentityCorrespondencia.VeredaId = idVereda;
            objIdentityCorrespondencia.CorregimientoId = idCorregimiento;
            objIdentityCorrespondencia.DireccionPersona = strDireccion;
            objIdentityCorrespondencia.TipoDireccion = idTipoDireccion;
            //insertar
            this.DalcDir.InsertarDireccionPersona(ref objIdentityCorrespondencia);
        }

        /// <summary>
        /// JMM Jul 8 2010
        /// Registra la dirección del apoderado
        /// </summary>
        /// <param name="idDeptoExpedicionDocumento">int: identificador del departamento donde fué expedido el documnento</param>
        /// <param name="idMunExpedicionDocumento">int: identificador del municipio donde fué expedido el documnento</param>
        public void InsertarDireccionApoderado(Int64 IdApoderado, int idPais, int idDeptoExpedicionDocumento,
                                      int idMunExpedicionDocumento, int idCorregimiento,
                                      int idVereda, int idTipoDireccion,
                                      string strDireccion)
        {
            DireccionPersonaIdentity objIdentityCorrespondencia = new DireccionPersonaIdentity();
            objIdentityCorrespondencia.IdPersona = IdApoderado;
            objIdentityCorrespondencia.PaisId = idPais;
            objIdentityCorrespondencia.DepartamentoId = idDeptoExpedicionDocumento;
            objIdentityCorrespondencia.MunicipioId = idMunExpedicionDocumento;
            objIdentityCorrespondencia.VeredaId = idVereda;
            objIdentityCorrespondencia.CorregimientoId = idCorregimiento;
            objIdentityCorrespondencia.DireccionPersona = strDireccion;
            objIdentityCorrespondencia.TipoDireccion = idTipoDireccion;
            //insertar
            this.DalcDir.InsertarDireccionPersona(ref objIdentityCorrespondencia);
        }
        public void InsertarTipoDocumentoAcreditacion(Int64 IdApoderado, Int64 tipIdAcreditacion)
        {
            this.DalcDir.InsertarTipoDocumentoAcreditacion(IdApoderado, tipIdAcreditacion);
        }

        /// <summary>
        /// crea un los registros necesario para que un usuario sea adicionado al sistema
        /// </summary>
        /// <param name="strCode">Cedula</param>
        /// <param name="strName">UserName BPM</param>
        /// <param name="strPrimerNombre">Primer Nombre</param>
        /// <param name="strSegundoNombre">Segundo Nombre</param>
        /// <param name="strPrimerApellido">Primer Apellido</param>
        /// <param name="strSegundoApellido">Segundo Apellido</param>
        /// <param name="strIdlocality">Debe ser 1 por defecto</param>
        /// <param name="strTipoPersona">Tipo Persona</param>
        /// <param name="strIdentification">Numero de documento de Identification</param>
        /// <param name="strEmail">eMail</param>
        /// <param name="intIdTipoId">Identificador de tipo de identificacion</param>
        /// <param name="strLugarExpDoc">Lugar de expediciòn del documento de identidad </param>
        /// <param name="intPais">Identificador del pais</param>
        /// <param name="strTelefono">Numero telefonico</param>
        /// <param name="strCelular">Numero del celular</param>
        /// <param name="strFax">Numero de fax</param>
        /// <param name="strRazonSocial">Razon social</param>
        /// <param name="intIdCodTpPersona">identificador del codigo del tipo de persona</param>
        /// <param name="strOtro">Otro</param>
        /// <param name="strTarjetaPro">Numero de la tarjeta profesional</param>
        /// <param name="int64IdSolicitante">identificador del solicitante asociado a esta persona</param>
        /// <param name="intAutoridadAmbiental">identificador de la autoridad ambiental</param>
        /// <param name="strPregunta">pregunta en caso de olvidar contraseña</param>
        /// <param name="strRespuesta">respuesta a pregunta en caso de olvidar contraseña</param>
        /// <param name="intExpiration">tiempo en dias de expiracion de contraseña</param>
        /// <param name="strEnabled">T o F</param>
        /// <param name="strActive">T o F</param>
        /// <param name="strImageuser">por defecto debe ser NoImage.gif</param>
        /// <param name="strChangepassword">T o F</param>
        /// <returns>Identificador el registro del usuario que se acaba de ingresar.</returns>
        public void ActualizarPersona(long perID, string strPrimerNombre, string strSegundoNombre, string strPrimerApellido,
             string strSegundoApellido, int intIdlocality, string strTipoPersona, string strIdentification, string strEmail, int intIdTipoId,
             string strLugarExpDoc, int intPais, string strTelefono, string strCelular, string strFax, string strRazonSocial,
             int intIdCodTpPersona, string strTarjetaPro, Int64 int64IdSolicitante, int intAutoridadAmbiental,
             string strPregunta, string strRespuesta,
             int intDepartamento, int intMunicipio, int intVereda, int intCorregimiento)
        {

            this.Identity.PrimerNombre = strPrimerNombre;
            this.Identity.SegundoNombre = strSegundoNombre;
            this.Identity.PrimerApellido = strPrimerApellido;
            this.Identity.SegundoApellido = strSegundoApellido;
            this.Identity.NumeroIdentificacion = strIdentification;
            this.Identity.CorreoElectronico = strEmail;
            this.Identity.TipoDocumentoIdentificacion.Id = intIdTipoId;
            this.Identity.LugarExpediciónDocumento = strLugarExpDoc;
            this.Identity.Pais = intPais;
            this.Identity.Telefono = strTelefono;
            this.Identity.Celular = strCelular;
            this.Identity.Fax = strFax;
            //this.Identity.IdApplicationUser = intIdApplicationUser;
            this.Identity.RazonSocial = strRazonSocial;
            this.Identity.TipoPersona.CodigoTipoPersona = intIdCodTpPersona;
            //this.Identity.Otro = strOtro;
            this.Identity.TarjetaProfesional = strTarjetaPro;
            this.Identity.IdSolicitante = int64IdSolicitante;
            this.Identity.IdAutoridadAmbiental = intAutoridadAmbiental;
            this.Identity.Pregunta = strPregunta;
            this.Identity.Respuesta = strRespuesta;

            //datos de la direccion
            //this.IdentityDir.PaisId = intPais;
            this.IdentityDir.MunicipioId = intMunicipio;
            this.IdentityDir.VeredaId = intVereda;
            this.IdentityDir.CorregimientoId = intCorregimiento;

            if (int64IdSolicitante != 0)
            {
                this.Dalc.ActualizarPersona(ref this.Identity);

            }
            else
            {
                //this.Identity.IdApplicationUser = this.Dalc.InsertarPersonaBPM(strCode, strName, strPassword, strPrimerNombre, strSegundoApellido, strPrimerApellido,
                //    intIdlocality, strTipoPersona, strIdentification, strEmail, intExpiration, strEnabled, strActive, strImageuser, strChangepassword);
                this.Dalc.ActualizarPersona(ref this.Identity);
                this.IdentityDir.IdPersona = this.Identity.PersonaId;
                this.DalcDir.ActualizarDireccionPersona(ref this.IdentityDir);

            }

        }

        /// <summary>
        /// JMM - 09/07/2010
        /// Metodo de actualizacion de la informacion de datos del solicitante o apoderado
        /// </summary>
        /// <param name="perID"></param>
        /// <param name="strPrimerNombre"></param>
        /// <param name="strSegundoNombre"></param>
        /// <param name="strPrimerApellido"></param>
        /// <param name="strSegundoApellido"></param>
        /// <param name="strTelefono"></param>
        /// <param name="strCelular"></param>
        /// <param name="strFax"></param>
        /// <param name="strEmail"></param>
        /// <param name="strRazonSocial"></param>
        /// <param name="strTarjetaPro"></param>
        /// <param name="int64IdSolicitante"></param>
        public void ActualizarPersonaSolicitante(long perID, string strPrimerNombre, string strSegundoNombre, string strPrimerApellido, 
            string strSegundoApellido, string strTelefono, string strCelular, string strFax, string strEmail, string strRazonSocial, 
            string strTarjetaPro, Int64 int64IdSolicitante, Boolean autorizaCorreo)
        {
            this.Identity.IdUsuario = perID.ToString(); 
            this.Identity.PrimerNombre = strPrimerNombre;
            this.Identity.SegundoNombre = strSegundoNombre;
            this.Identity.PrimerApellido = strPrimerApellido;
            this.Identity.SegundoApellido = strSegundoApellido;
            this.Identity.Telefono = strTelefono;
            this.Identity.Celular = strCelular;
            this.Identity.Fax = strFax;
            this.Identity.CorreoElectronico = strEmail;
            this.Identity.RazonSocial = strRazonSocial;
            this.Identity.TarjetaProfesional = strTarjetaPro;
            this.Identity.IdSolicitante = int64IdSolicitante;
            this.Identity.AutorizaCorreo = autorizaCorreo; //jmartinez adiciono campo autoriza correo

            this.Dalc.ActualizarPersonaSolicitante(ref this.Identity);
        }

        public void ActualizarDireccion(int iIdMunicipio, int iIdCorregimiento, int iIdVereda, string strDireccion, Int64 iPerId, int iPaiId, int iTDIId)
        {
            this.IdentityDir.IdPersona = iPerId;
            this.IdentityDir.MunicipioId = iIdMunicipio;
            this.IdentityDir.CorregimientoId = iIdCorregimiento;
            this.IdentityDir.VeredaId = iIdVereda;
            this.IdentityDir.DireccionPersona = strDireccion;
            this.IdentityDir.PaisId = iPaiId;
            this.IdentityDir.TipoDireccion = iTDIId;

            this.DalcDir.ActualizarDireccionPersonaSol(ref this.IdentityDir);

        }

        public string CambiarClave(string strUsername, string strClaveNueva)
        {
            this.Identity = this.Dalc.BuscarPersonaByUsername(strUsername);
            if (this.Identity.IdApplicationUser != 0)
            {

                if (this.Identity.Password != strClaveNueva)
                {
                    this.Dalc.CambiarClave(Convert.ToInt32(this.Identity.IdApplicationUser), strClaveNueva, "F");
                    return "Su contraseña ha sido cambiada";
                }
                else
                {
                    return "Su nueva contraseña debe ser diferente a la anterior";
                }
            }
            else
            {
                return "El usuario no existe";
            }

        }

        /// <summary>
        /// Hava:
        /// Método que cambia la contraseña de un usuario.
        /// </summary>
        /// <param name="strUsername">string: Usuario</param>
        /// <param name="strClaveAnterior">string: Clave anterior</param>
        /// <param name="strClaveNueva">string: Nueva clave</param>
        /// <returns>Mensaje que acepta o rechaza el cambio</returns>
        public string CambiarClave(string strUsername, string strClaveAnterior, string strClaveNueva, ref int idError)
        {
            this.Identity = this.Dalc.BuscarPersonaByUsername(strUsername);
            if (this.Identity.IdApplicationUser != 0)
            {

                if (this.Identity.Password == strClaveAnterior)
                {
                    if (this.Identity.Password != strClaveNueva)
                    {
                        this.Dalc.CambiarClave(Convert.ToInt32(this.Identity.IdApplicationUser), strClaveNueva, "F");
                        idError = 0;
                        return "Su contraseña ha sido cambiada";
                    }
                    else
                    {
                        idError = 1;
                        return "Su nueva contraseña debe ser diferente a la anterior";
                    }
                }
                else
                {
                    idError = 2;
                    return "La contraseña no es válida. Debe ser mínimo de 8 caracteres, alfanuméricos con mayúsculas - minúsculas y un caracter especial";
                }   
            }
            else
            {
                idError = 3;
                return "El usuario no existe";
            }

        }


        /// <summary>
        /// Realiza el cambio de la segunda contraseña del usuario
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario al cual se le realiza la modificación de la clave</param>
        /// <param name="strClaveAnterior">string que contiene la nueva contraseña</param>
        /// <param name="strClaveNueva">string con la Nueva clave</param>
        /// <returns>
        /// int con el codigo de error presentado:
        /// 0: La contraseña fue cambiada exitosamente
        /// 1: No se especifico la contraseña anterior
        /// 2: La contraseña anterior no es correcta
        /// 3: La contraseña anterior y la nueva son iguales
        /// </returns>
        public int CambiarSegundaClave(int intUsuarioID, string strClaveAnterior, string strClaveNueva)
        {
            return this.Dalc.CambiarSegundaClave(intUsuarioID, strClaveAnterior, strClaveNueva);
        }


        /// <summary>
        /// Verificar si la segunda clave es valida
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <param name="strClave">string con la clave a validar</param>
        /// <returns>bool indicando si la segunda clave es valida</returns>
        public bool ValidarSegundaClave(int intUsuarioID, string strClave)
        {
            return this.Dalc.ValidarSegundaClave(intUsuarioID, strClave);
        }


        /// <summary>
        /// Consultar la información del logo de un usuario
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <returns>PersonaLogoIdentity con la informacion del logo del usuario</returns>
        public PersonaLogoIdentity ConsultarLogo(int intUsuarioID)
        {
            return this.Dalc.ConsultarLogo(intUsuarioID);
        }


        /// <summary>
        /// Realizar el cambio de la información del logo de un usuario
        /// </summary>
        /// <param name="objFirmaPersona">PersonaLogoIdentity con la información del logo del usuario</param>
        public void CambiarLogo(PersonaLogoIdentity objLogoPersona)
        {
            this.Dalc.CambiarLogo(objLogoPersona);
        }


        /// <summary>
        /// Consultar la información de la firma del usuario
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <returns>PersonaFirmaIdentity con la informacion de la firma del usuario</returns>
        public PersonaFirmaIdentity ConsultarFirma(int intUsuarioID)
        {
            return this.Dalc.ConsultarFirma(intUsuarioID);
        }


        /// <summary>
        /// Realizar el cambio de la información de la firma del usuario
        /// </summary>
        /// <param name="objFirmaPersona">PersonaFirmaIdentity con la información de la firma del usuario</param>
        public void CambiarFirma(PersonaFirmaIdentity objFirmaPersona)
        {
            this.Dalc.CambiarFirma(objFirmaPersona);
        }

        /// <summary>
        /// Verificar si el usuario tiene firma registrada
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <returns>bool indicando si tiene o no firma registrada</returns>
        public bool TieneFirma(int intUsuarioID)
        {
            return this.Dalc.TieneFirma(intUsuarioID);
        }


        /// <summary>
        /// Verificar si el usuario tiene segunda clave
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <returns>bool indicando si tiene o no segunda clave</returns>
        public bool TieneSegundaClave(int intUsuarioID)
        {
            return this.Dalc.TieneSegundaClave(intUsuarioID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUsername"></param>
        /// <returns></returns>
        public string ReestablecerClave(string strUsername)
        {
            this.Identity = this.Dalc.BuscarPersonaByUsername(strUsername);

            if (this.Identity.IdApplicationUser != 0)
            {

             /// -1:No existe : puede inscribirse 
             /// 0:Existe Por Aprobar  - En proceso - Esperar mail 
             /// 1:Aprobado No se puede reinscribir 
             /// 2:Existe Rechazado puede Reinscribirse
                
               
                int estado = this.VerificarExistenciaByNumeroIdentificacion(this.Identity.NumeroIdentificacion);

                if (estado == (int)SILPA.Comun.EstadoSolicitudCredencial.Aprobado)
                {
                    string newPassword = generarPassword();
                    this.Dalc.CambiarClave(Convert.ToInt32(this.Identity.IdApplicationUser), SILPA.Comun.EnDecript.Encriptar(newPassword + "*"), "T");
                    
                    this.Dalc.ObtenerPersona(ref this.Identity);
                    this.Identity.NumeroIdentificacion = newPassword+"*";
                    ICorreo.Correo.EnviarMailReestableceClave(this.Identity);
                    //return "Su contraseña ha sido reestablecida";
                    return "contraseña fue restablecida y la información de la misma le llegará al correo electrónico que registro en el sistema";
                }
                else 
                {
                    if (estado == (int)SILPA.Comun.EstadoSolicitudCredencial.EnProceso)
                    {
                        return "Las credenciales están inactivas, el sistema no puede restablecer contraseña";
                    }
                    else
                    {
                            if (estado == (int)SILPA.Comun.EstadoSolicitudCredencial.Rechazado)
                            {
                                return "El usuario se encuentra rechazado";
                            }
                            else 
                            {
                                return "El usuario no existe.";
                            }
                    }

                }

            }
            else
            {
                return "El usuario no existe";
            }
        }

        /// <summary>
        /// HAVA:21-JUN-2010
        /// Determina si ya existe el correo del solicitante que requiere credenciales.
        /// </summary>
        /// <param name="strEmail">str: email</param>
        /// <returns>bool:true /false</returns>
        public bool ExisteCorreo(string strEmail) 
        {
            return this.Dalc.ExisteCorreo(strEmail);
        }

        /// <summary>
        /// JMM - 16/07/2010
        /// Determina si ya existe el correo del solicitante que requiere credenciales para la acrtualizacion de datos.
        /// </summary>
        /// <param name="strEmail">str: email</param>
        /// <returns>bool:true /false</returns>
        public bool ExisteCorreoSol(Int64 iIdPersona, string strEmail)
        {
            return this.Dalc.ExisteCorreoSOL(iIdPersona, strEmail);
        }

        /// <summary>
        /// 11-08-2010 - aegb: CU Emitir Documento Manual
        /// Consulta la informacion de un usuario (persona) de acuerdo con los parametros enviados
        /// </summary>
        /// <param name="usuario">username de la persona</param>
        public PersonaIdentity ConsultarPersona(string usuario)
        {
            PersonaDalc _objPersona = new PersonaDalc();
            return _objPersona.ConsultarPersona(usuario);
        }

        /// <summary>
        /// 11-08-2010 - aegb: CU Emitir Documento Manual
        /// Consulta la informacion de los usuarios relacionados con el numero silpa
        /// </summary>
        /// <param name="numeroSilpa">numero silpa</param>
        public DataTable ConsultarPersonasNumeroSilpa(string numeroSilpa)
        {
            try
            {
	            PersonaDalc _objPersona = new PersonaDalc();
	            return _objPersona.ConsultarPersonasNumeroSilpa(numeroSilpa);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al emitir el Documento Manual.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>        
        /// Obtiene el Identity de persona mediante su número de identificación.
        /// </summary>
        /// <param name="strIdentificacionPersona">string: con el número de identificación de la persona</param>
        /// <returns>PersonaIndentity: objeto que contiene los datos de persona</returns>
        public PersonaIdentity ConsultarPersonasNumeroIdentificacion(string strIdentificacionPersona)
        {
            return this.Dalc.BuscarPersonaByIdentification(strIdentificacionPersona);
        }


        /// <summary>
        /// Obtener el listado de representantes de una persona
        /// </summary>
        /// <param name="p_lngIdPersona">long con el id de los representantes de una persona</param>
        /// <returns>DataTable con la información de los representantes</returns>
        public DataTable ConsultarRepresentantesPersona(long p_lngIdPersona)
        {
            return this.Dalc.ConsultarRepresentantesPersona(p_lngIdPersona);
        }

        /// <summary>
        /// Crear un nuevo representante
        /// </summary>
        /// <param name="p_lngIdPersona">long con el id de la persona</param>
        /// <param name="p_lngIdRepresentante">long con el id del representante</param>
        /// <param name="p_strDescripcion">string con la descripción</param>
        public void CrearRepresentantesPersona(long p_lngIdPersona, long p_lngIdRepresentante, string p_strDescripcion)
        {
            this.Dalc.CrearRepresentantesPersona(p_lngIdPersona, p_lngIdRepresentante, p_strDescripcion);
        }


        /// <summary>
        /// Editar los datos de unrepresentante
        /// </summary>
        /// <param name="p_lngIdRepresentante">long con el id del representante</param>
        /// <param name="p_strDescripcion">string con la descripción</param>
        public void ModificarRepresentantesPersona(long p_lngIdRepresentante, string p_strDescripcion)
        {
            this.Dalc.ModificarRepresentantesPersona(p_lngIdRepresentante, p_strDescripcion);
        }


        /// <summary>
        /// Elimina un representante
        /// </summary>
        /// <param name="p_lngIdRepresentante">long con el id del representante</param>
        public void EliminarRepresentantesPersona(long p_lngIdRepresentante)
        {
            this.Dalc.EliminarRepresentantesPersona(p_lngIdRepresentante);
        }

        #endregion

        public DataTable ConsultarPersonasActivas(string strNombreUsuario = null)
        {
            PersonaDalc _objPersona = new PersonaDalc();
            return _objPersona.ConsultarPersonasActivas(strNombreUsuario);
        }

        public string generarPassword()
        {
            string cadena = "";
            string guardarCadena = "";
            int[] numero = new int[9];
            string[] letras = new string[9];
            Random rnd = new Random();
            int i;
            for (i = 0; i < 9; i++)
            {
                numero[i] = rnd.Next(65, 125);
            }

            for (int x = 0; x < numero.Length; x++)
            {
                string letra = Convert.ToChar(numero[x]).ToString();
                letras[x] = letra == string.Empty ? "_" : letra;
            }

            for (int n = 0; n < 9; n++)
            {
                cadena = letras[n];
                guardarCadena = cadena + guardarCadena;
            }
            return guardarCadena;
        }
    }
}
