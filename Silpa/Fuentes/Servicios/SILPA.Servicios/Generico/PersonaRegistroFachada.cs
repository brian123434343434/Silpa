using SILPA.Comun;
using SILPA.Servicios.Generico.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.LogicaNegocio;
using SoftManagement.Log;
using SILPA.Servicios.Generico.Enum;
using System.Text.RegularExpressions;
using SILPA.LogicaNegocio.Generico;
using System.Data;
using SILPA.AccesoDatos.Generico;

namespace SILPA.Servicios.Generico
{
    public class PersonaRegistroFachada
    {
        #region Propiedades

            /// <summary>
            /// Instancia del objeto
            /// </summary>
            private static PersonaRegistroFachada Instancia;

        #endregion


        #region Metodos Privados

            /// <summary>
            /// Verificar si el correo electrónico es valido
            /// </summary>
            /// <param name="p_strCorreo">string con el correo electrónico</param>
            /// <returns>bool en caso de que sea valido, false en caso contrario</returns>
            private bool CorreoElectronicoValido(string p_strCorreo)
            {
                bool blnValido = false;
                string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

                if (Regex.IsMatch(p_strCorreo, expresion))
                {
                    if (Regex.Replace(p_strCorreo, expresion, String.Empty).Length == 0)
                    {
                        blnValido = true;
                    }                    
                }

                return blnValido;
            }


            /// <summary>
            /// Verifica si la autoridad ambiental existe
            /// </summary>
            /// <param name="p_intAutoridadAmbientalID">int con el identificador de la autoridad ambiental</param>
            /// <returns>bool en caso de que exista, false en caso contrario</returns>
            private bool ExisteAutoridadAmbiental(int p_intAutoridadAmbientalID)
            {
                Listas objListas = new Listas();
                bool blnValido = false;
                DataSet objAutoridades = objListas.ListarAutoridades(p_intAutoridadAmbientalID);

                if (objAutoridades != null && objAutoridades.Tables.Count > 0 && objAutoridades.Tables[0].Rows.Count > 0)
                    blnValido = true;

                return blnValido;
            }


            /// <summary>
            /// Verifica si el tipo de persona existe
            /// </summary>
            /// <param name="p_intTipoPersonaID">int con el identificador del tipo de persona</param>
            /// <returns>bool en caso de que exista, false en caso contrario</returns>
            private bool ExisteTipoPersona(int p_intTipoPersonaID)
            {
                Listas objListas = new Listas();
                bool blnValido = false;
                DataSet objTipoPersona = objListas.ListarTipoPersona(p_intTipoPersonaID);

                if (objTipoPersona != null && objTipoPersona.Tables.Count > 0 && objTipoPersona.Tables[0].Rows.Count > 0)
                    blnValido = true;

                return blnValido;
            }

            /// <summary>
            /// Verifica si el tipo de persona existe
            /// </summary>
            /// <param name="p_intTipoPersonaID">int con el identificador del tipo de persona</param>
            /// <param name="p_intTipoDocumentoID">int con el identificador del tipo de documento</param>
            /// <returns>bool en caso de que exista, false en caso contrario</returns>
            private bool ExisteTipoDocumento(int p_intTipoPersonaID, int p_intTipoDocumentoID)
            {
                Listas objListas = new Listas();
                bool blnValido = false;
                DataSet objTipoDocumento = objListas.ListaTipoIdentificacionXTipoPersona();
                DataRow[] objTiposDocumentos = null;

                if (objTipoDocumento != null && objTipoDocumento.Tables.Count > 0 && objTipoDocumento.Tables[0].Rows.Count > 0)
                {
                    objTiposDocumentos = objTipoDocumento.Tables[0].Select("TPE_ID = " + p_intTipoPersonaID.ToString() + " AND TID_ID = " + p_intTipoDocumentoID.ToString() + " AND TID_ACTIVO = 1");
                    if (objTiposDocumentos != null && objTiposDocumentos.Length > 0)
                        blnValido = true;
                }

                return blnValido;
            }


            /// <summary>
            /// Verifica si país existe
            /// </summary>
            /// <param name="p_intPaisID">int con el identificador del país</param>
            /// <returns>bool en caso de que exista, false en caso contrario</returns>
            private bool PaisExiste(int p_intPaisID)
            {
                Listas objListas = new Listas();
                bool blnValido = false;
                DataSet objPaises = objListas.ListarPaises(p_intPaisID);

                if (objPaises != null && objPaises.Tables.Count > 0 && objPaises.Tables[0].Rows.Count > 0)
                    blnValido = true;

                return blnValido;
            }


            /// <summary>
            /// Verifica si el departamento existe
            /// </summary>
            /// <param name="p_intPaisID">int con el identificador del departamento</param>
            /// <returns>bool en caso de que exista, false en caso contrario</returns>
            private bool DepartamentoExiste(int p_intDepartamentoID)
            {
                Departamento objDepartamento = new Departamento();
                bool blnValido = false;
                List<DepartamentoIdentity> objDepartamentos = objDepartamento.ConsultarDepartamentos(null, p_intDepartamentoID);

                if (objDepartamentos != null && objDepartamentos.Count > 0)
                    blnValido = true;

                return blnValido;
            }


            /// <summary>
            /// Verifica si el municipio existe
            /// </summary>
            /// <param name="p_intPaisID">int con el identificador del municipio</param>
            /// <returns>bool en caso de que exista, false en caso contrario</returns>
            private bool MunicipioExiste(int p_intDepartamentoID, int p_intMunicipioID)
            {
                Municipio objMunicipio = new Municipio();
                bool blnValido = false;
                DataSet objMunicipios = objMunicipio.ListarMunicipios(p_intMunicipioID, p_intDepartamentoID, null);

                if (objMunicipios != null && objMunicipios.Tables.Count > 0 && objMunicipios.Tables[0].Rows.Count > 0)
                    blnValido = true;

                return blnValido;
            }


            /// <summary>
            /// Verifica que la información requerida para registro y modificación es valida
            /// </summary>
            /// <param name="p_objPersonaRegistroEntity">PersonaRegistroEntity con la información de la persona</param>
            /// <returns>string con mensaje de error en caso de que se presente falla</returns>
            private string ValidarInformacion(PersonaRegistroEntity p_objPersonaRegistroEntity)
            {
                string strMensajeValidacion = "";

                if (p_objPersonaRegistroEntity.AutoridadAmbientalID <= 0 || !this.ExisteAutoridadAmbiental(p_objPersonaRegistroEntity.AutoridadAmbientalID))
                {
                    strMensajeValidacion = "La autoridad ambiental especificada no existe";
                }
                else if (p_objPersonaRegistroEntity.TipoPersonaID <= 0 || !this.ExisteTipoPersona(p_objPersonaRegistroEntity.TipoPersonaID))
                {
                    strMensajeValidacion = "La autoridad ambiental especificada no existe";
                }
                else if (p_objPersonaRegistroEntity.TipoDocumentoID <= 0 || !this.ExisteTipoDocumento(p_objPersonaRegistroEntity.TipoPersonaID, p_objPersonaRegistroEntity.TipoDocumentoID))
                {
                    strMensajeValidacion = "El tipo de documento especificado no existe";
                }
                else if (string.IsNullOrEmpty(p_objPersonaRegistroEntity.NumeroDocumento.Trim()))
                {
                    strMensajeValidacion = "No se especifico número de documento";
                }
                else if (p_objPersonaRegistroEntity.PaisContactoID <= 0 || !this.PaisExiste(p_objPersonaRegistroEntity.PaisContactoID))
                {
                    strMensajeValidacion = "El país de contacto especificado no existe";
                }
                else if (p_objPersonaRegistroEntity.PaisContactoID == (int)PaisEnum.COLOMBIA && (p_objPersonaRegistroEntity.DepartamentoContactoID <= 0 || !this.DepartamentoExiste(p_objPersonaRegistroEntity.DepartamentoContactoID)))
                {
                    strMensajeValidacion = "El departamento especificado no existe";
                }
                else if (p_objPersonaRegistroEntity.PaisContactoID == (int)PaisEnum.COLOMBIA && (p_objPersonaRegistroEntity.MunicipioContactoID <= 0 || !this.MunicipioExiste(p_objPersonaRegistroEntity.DepartamentoContactoID, p_objPersonaRegistroEntity.MunicipioContactoID)))
                {
                    strMensajeValidacion = "El municipio especificado no existe";
                }
                else if (string.IsNullOrEmpty(p_objPersonaRegistroEntity.DireccionContacto.Trim()))
                {
                    strMensajeValidacion = "No se especifico dirección de contacto";
                }                
                else if (string.IsNullOrEmpty(p_objPersonaRegistroEntity.CorreoElectronico.Trim()))
                {
                    strMensajeValidacion = "No se especifico correo electrónico";
                }
                else if (!this.CorreoElectronicoValido(p_objPersonaRegistroEntity.CorreoElectronico.Trim()))
                {
                    strMensajeValidacion = "El correo electrónico especificado no es válido";
                }

                if (string.IsNullOrEmpty(strMensajeValidacion))
                {
                    //Validar campos exclusivos por tipo de persona
                    if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural)
                    {
                        if (string.IsNullOrEmpty(p_objPersonaRegistroEntity.PrimerNombre.Trim()))
                        {
                            strMensajeValidacion = "No se especifico el primer nombre de la persona";
                        }
                        else if (string.IsNullOrEmpty(p_objPersonaRegistroEntity.PrimerApellido.Trim()))
                        {
                            strMensajeValidacion = "No se especifico el primer apellido de la persona";
                        }
                        else if (p_objPersonaRegistroEntity.DepartamentoOrigenDocumentoID > 0 && !this.DepartamentoExiste(p_objPersonaRegistroEntity.DepartamentoOrigenDocumentoID))
                        {
                            strMensajeValidacion = "El departamento de expedición del documento no existe";
                        }
                        else if (p_objPersonaRegistroEntity.MunicipioOrigenDocumentoID > 0 && !this.MunicipioExiste(p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID, p_objPersonaRegistroEntity.MunicipioOrigenDocumentoID))
                        {
                            strMensajeValidacion = "El municipio de expedición del documento no existe";
                        }
                        else if (p_objPersonaRegistroEntity.PaisCorrespondenciaID <= 0 || !this.PaisExiste(p_objPersonaRegistroEntity.PaisCorrespondenciaID))
                        {
                            strMensajeValidacion = "El país de contacto especificado no existe";
                        }
                        else if (p_objPersonaRegistroEntity.PaisCorrespondenciaID == (int)PaisEnum.COLOMBIA && (p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID <= 0 || !this.DepartamentoExiste(p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID)))
                        {
                            strMensajeValidacion = "El departamento especificado no existe";
                        }
                        else if (p_objPersonaRegistroEntity.PaisCorrespondenciaID == (int)PaisEnum.COLOMBIA && (p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID <= 0 || !this.MunicipioExiste(p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID, p_objPersonaRegistroEntity.MunicipioCorrespondenciaID)))
                        {
                            strMensajeValidacion = "El municipio especificado no existe";
                        }
                        else if (string.IsNullOrEmpty(p_objPersonaRegistroEntity.DireccionCorrespondencia.Trim()))
                        {
                            strMensajeValidacion = "No se especifico dirección de contacto";
                        }
                    }
                    else if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.JuridicaPublica)
                    {
                        if (!string.IsNullOrEmpty(p_objPersonaRegistroEntity.RazonSocial))
                            strMensajeValidacion = "No se especifico la razón social";
                    }
                    else if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.JuridicaPrivada)
                    {
                        if (!string.IsNullOrEmpty(p_objPersonaRegistroEntity.RazonSocial))
                        {
                            strMensajeValidacion = "No se especifico la razón social";
                        }
                        else if (p_objPersonaRegistroEntity.PaisCorrespondenciaID <= 0 || !this.PaisExiste(p_objPersonaRegistroEntity.PaisCorrespondenciaID))
                        {
                            strMensajeValidacion = "El país de contacto especificado no existe";
                        }
                        else if (p_objPersonaRegistroEntity.PaisCorrespondenciaID == (int)PaisEnum.COLOMBIA && (p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID <= 0 || !this.DepartamentoExiste(p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID)))
                        {
                            strMensajeValidacion = "El departamento especificado no existe";
                        }
                        else if (p_objPersonaRegistroEntity.PaisCorrespondenciaID == (int)PaisEnum.COLOMBIA && (p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID <= 0 || !this.MunicipioExiste(p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID, p_objPersonaRegistroEntity.MunicipioCorrespondenciaID)))
                        {
                            strMensajeValidacion = "El municipio especificado no existe";
                        }
                        else if (string.IsNullOrEmpty(p_objPersonaRegistroEntity.DireccionCorrespondencia.Trim()))
                        {
                            strMensajeValidacion = "No se especifico dirección de contacto";
                        }
                    }
                }

                return strMensajeValidacion;
            }



            /// <summary>
            /// Verifica que la información requerida para el resgistro de la persona sea valida
            /// </summary>
            /// <param name="p_objPersonaRegistroEntity">PersonaRegistroEntity con la información de la persona</param>
            /// <returns>string con mensaje de error en caso de que se presente falla</returns>
            private string ValidarInformacionRegistro(PersonaRegistroEntity p_objPersonaRegistroEntity)
            {
                SILPA.LogicaNegocio.Generico.Persona objPersona = new SILPA.LogicaNegocio.Generico.Persona();
                string strMensajeValidacion = "";
                int intEstadoPersona = 0;

                strMensajeValidacion = this.ValidarInformacion(p_objPersonaRegistroEntity);

                //Verificar si se presento error sino verificar información especifica
                if (string.IsNullOrEmpty(strMensajeValidacion))
                {
                    intEstadoPersona = objPersona.VerificarExistenciaByNumeroIdentificacion(p_objPersonaRegistroEntity.NumeroDocumento);

                    if (intEstadoPersona == (int)SILPA.Comun.EstadoSolicitudCredencial.EnProceso)
                    {
                        strMensajeValidacion = "Ya existe una solicitud en proceso. Debe esperar un mensaje de correo electrónico con la activación de su solicitud";
                    }
                    else if (intEstadoPersona == (int)SILPA.Comun.EstadoSolicitudCredencial.Aprobado)
                    {
                        strMensajeValidacion = "Ya existe un usuario registrado con los mismos datos de identificación";
                    }
                }

                return strMensajeValidacion;
            }


            /// <summary>
            /// Verifica que la información requerida para la modificación de la persona sea valida
            /// </summary>
            /// <param name="p_objPersonaRegistroEntity">PersonaRegistroEntity con la información de la persona</param>
            /// <returns>string con mensaje de error en caso de que se presente falla</returns>
            private string ValidarInformacionModificacion(PersonaRegistroEntity p_objPersonaRegistroEntity)
            {
                SILPA.LogicaNegocio.Generico.Persona objPersona = new SILPA.LogicaNegocio.Generico.Persona();
                string strMensajeValidacion = "";
                int intEstadoPersona = 0;

                strMensajeValidacion = this.ValidarInformacion(p_objPersonaRegistroEntity);

                //Verificar si se presento error sino verificar información especifica
                if (string.IsNullOrEmpty(strMensajeValidacion))
                {
                    //Obtener la persona especificada
                    objPersona.ObternerPersonaByUserIdApp(p_objPersonaRegistroEntity.PersonaVITALID.ToString(), false);
                    if (objPersona.Identity.PersonaId <= 0)
                        strMensajeValidacion = "La persona especificada para la modificación no existe.";

                    else if (objPersona.ExisteCorreoSol(objPersona.Identity.IdApplicationUser, p_objPersonaRegistroEntity.CorreoElectronico))
                    {
                        strMensajeValidacion = "Ya existe un usuario con este Correo electrónico:  " + p_objPersonaRegistroEntity.CorreoElectronico + ".  Por favor, ingrese un Correo electrónico diferente.";
                    }
                    else if (p_objPersonaRegistroEntity.NumeroDocumento != objPersona.Identity.NumeroIdentificacion)
                    {
                        intEstadoPersona = objPersona.VerificarExistenciaByNumeroIdentificacion(p_objPersonaRegistroEntity.NumeroDocumento);

                        if (intEstadoPersona == (int)SILPA.Comun.EstadoSolicitudCredencial.EnProceso)
                        {
                            strMensajeValidacion = "Ya existe una solicitud en proceso. Debe esperar un mensaje de correo electrónico con la activación de su solicitud";
                        }
                        else if (intEstadoPersona == (int)SILPA.Comun.EstadoSolicitudCredencial.Aprobado)
                        {
                            strMensajeValidacion = "Ya existe un usuario registrado con los mismos datos de identificación";
                        }
                    }
                }

                return strMensajeValidacion;
            }


            /// <summary>
            /// Crea una nueva persona en el sistema
            /// </summary>
            /// <param name="p_objPersonaRegistroEntity">PersonaRegistroEntity con la información de la persona</param>
            /// <returns>PersonaIdentity con la información de la persona creada</returns>
            private PersonaIdentity CrearPersona(PersonaRegistroEntity p_objPersonaRegistroEntity)
            {
                SILPA.LogicaNegocio.Generico.Persona objPersona = null;
                SILPA.LogicaNegocio.Generico.SolicitudCredenciales objSolicitudCredenciales = null;
                Configuracion objConfiguracion = null;
                TipoUsuarioDalc objTipoUsuarioDalc = null;
                string strNombreTipoPersona = "";
                long lngPersonaID = 0;

                try
                {
                    //Crear objeto
                    objPersona = new SILPA.LogicaNegocio.Generico.Persona();
                    objPersona.IdentityDir.DireccionPersona = p_objPersonaRegistroEntity.DireccionContacto.Trim();

                    //Cargar el tipo de persona
                    if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural)
                        strNombreTipoPersona = "Natural";
                    else if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.JuridicaPublica)
                        strNombreTipoPersona = "Juridica Publica";
                    else if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.JuridicaPrivada)
                        strNombreTipoPersona = "Juridica Privada";

                    //Insertar la persona
                    lngPersonaID = objPersona.InsertarPersona(p_objPersonaRegistroEntity.NumeroDocumento.Trim(), p_objPersonaRegistroEntity.NumeroDocumento.Trim(),
                                                              EnDecript.Encriptar(p_objPersonaRegistroEntity.NumeroDocumento.Trim() + "*"),
                                                              (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.PrimerNombre.Trim() : ""),
                                                              (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.SegundoNombre.Trim() : ""),
                                                              (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.PrimerApellido.Trim() : ""),
                                                              (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.SegundoApellido.Trim() : ""), 1,
                                                              strNombreTipoPersona, p_objPersonaRegistroEntity.NumeroDocumento.Trim(),
                                                              p_objPersonaRegistroEntity.CorreoElectronico.Trim(),
                                                              p_objPersonaRegistroEntity.TipoDocumentoID,
                                                              (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.MunicipioOrigenDocumentoID.ToString() : ""),
                                                              p_objPersonaRegistroEntity.PaisContactoID,
                                                              p_objPersonaRegistroEntity.Telefono.Trim(), p_objPersonaRegistroEntity.Celular.Trim(),
                                                              p_objPersonaRegistroEntity.Fax.Trim(),
                                                              (p_objPersonaRegistroEntity.TipoPersonaID != (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.RazonSocial.Trim() : ""),
                                                              p_objPersonaRegistroEntity.TipoPersonaID, "", 0,
                                                              p_objPersonaRegistroEntity.AutoridadAmbientalID, "", "", 
                                                              99999, "False", "True", "NoImage.gif", "F",
                                                              p_objPersonaRegistroEntity.DepartamentoContactoID,
                                                              p_objPersonaRegistroEntity.MunicipioContactoID,
                                                              -1,
                                                              -1, (int)TipoSolicitante.Solicitante,
                                                              p_objPersonaRegistroEntity.AutorizaEnvioNotificaciones);

                    //Verificar que se obtenga el identificador de la persona
                    if (lngPersonaID > 0)
                    {
                        //Insertar la dirección de expedición del documento
                        if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural)
                        {
                            objConfiguracion = new Configuracion();
                            objPersona.InsertarDireccion(objConfiguracion.IdPaisPredeterminado,
                                                         p_objPersonaRegistroEntity.DepartamentoOrigenDocumentoID,
                                                         p_objPersonaRegistroEntity.MunicipioOrigenDocumentoID,
                                                        -1, -1,
                                                        (int)SILPA.Comun.TipoDireccion.Expedicion_Documento,
                                                        string.Empty
                                                       );
                        }

                        // Se insertan las Direcciones de Correspondencia
                        objPersona.InsertarDireccion(p_objPersonaRegistroEntity.PaisCorrespondenciaID,
                                                       p_objPersonaRegistroEntity.DepartamentoCorrespondenciaID,
                                                       p_objPersonaRegistroEntity.MunicipioCorrespondenciaID,
                                                       -1,
                                                       -1,
                                                       (int)SILPA.Comun.TipoDireccion.Correspondencia,
                                                       p_objPersonaRegistroEntity.DireccionCorrespondencia.Trim()
                                                       );

                        //Aprobación de manera automatica del proceso
                        objSolicitudCredenciales = new SILPA.LogicaNegocio.Generico.SolicitudCredenciales();
                        objSolicitudCredenciales.InsertarSolicitudCredenciales(lngPersonaID, 0);

                        //Realizar la activación del usuario
                        objTipoUsuarioDalc = new TipoUsuarioDalc();
                        objTipoUsuarioDalc.ActivarUsuario(new TipoUsuarioIdentity { IdPersona = Convert.ToInt32(lngPersonaID), IdTipoUsuario = p_objPersonaRegistroEntity.TipoPersonaID });

                        //Envia la contraseña al usuario
                        objPersona.ObternerPersonaByIdSolicitante(lngPersonaID.ToString());
                        SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorreoAprobacionUsuario("", objPersona.Identity);
                    }
                    else
                    {
                        throw new Exception("No se creo de manera correcta la persona");
                    }
                }
                catch(Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PersonaFachada :: CrearPersona -> Error creando la persona: " + exc.Message + " - " + (exc.StackTrace != null ? exc.StackTrace.ToString() : ""));

                    throw exc;
                }

                return objPersona.Identity;
            }


            /// <summary>
            /// Modificar la información de una persona
            /// </summary>
            /// <param name="p_objPersonaRegistroEntity">PersonaRegistroEntity con la información de la persona</param>
            /// <returns>PersonaIdentity con la información de la persona actualizada</returns>
            private PersonaIdentity ModificarPersona(PersonaRegistroEntity p_objPersonaRegistroEntity)
            {
                SILPA.LogicaNegocio.Generico.Persona objPersona = null;
                Configuracion objConfiguracion = null;

                try
                {
                    //Crear objeto
                    objPersona = new SILPA.LogicaNegocio.Generico.Persona();
                    objPersona.ObternerPersonaByUserIdApp(p_objPersonaRegistroEntity.PersonaVITALID.ToString(), true);

                    objPersona.ActualizarPersonaSolicitante(objPersona.Identity.PersonaId,
                                                            (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.PrimerNombre.Trim() : ""),
                                                            (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.SegundoNombre.Trim() : ""),
                                                            (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.PrimerApellido.Trim() : ""),
                                                            (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.SegundoApellido.Trim() : ""),
                                                            p_objPersonaRegistroEntity.Telefono, p_objPersonaRegistroEntity.Celular, p_objPersonaRegistroEntity.Fax,
                                                            p_objPersonaRegistroEntity.CorreoElectronico.Trim(),
                                                            (p_objPersonaRegistroEntity.TipoPersonaID != (int)TipoPersona.Natural ? p_objPersonaRegistroEntity.RazonSocial.Trim() : ""), 
                                                            "", objPersona.Identity.IdSolicitante, p_objPersonaRegistroEntity.AutorizaEnvioNotificaciones);

                    //Insertar la dirección de expedición del documento
                    if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.Natural)
                    {
                        objConfiguracion = new Configuracion();
                        objPersona.ActualizarDireccion(p_objPersonaRegistroEntity.MunicipioOrigenDocumentoID, -1,
                                                        -1, string.Empty, objPersona.Identity.PersonaId,
                                                        objConfiguracion.IdPaisPredeterminado, Convert.ToInt32(SILPA.Comun.TipoDireccion.Expedicion_Documento));

                        objPersona.ActualizarDireccion(p_objPersonaRegistroEntity.MunicipioCorrespondenciaID, -1,
                                                       -1, p_objPersonaRegistroEntity.DireccionCorrespondencia.Trim(), objPersona.Identity.PersonaId,
                                                        p_objPersonaRegistroEntity.PaisCorrespondenciaID, Convert.ToInt32(SILPA.Comun.TipoDireccion.Correspondencia));

                    }
                    else if (p_objPersonaRegistroEntity.TipoPersonaID == (int)TipoPersona.JuridicaPrivada)
                    {
                        objPersona.ActualizarDireccion(p_objPersonaRegistroEntity.MunicipioCorrespondenciaID, -1,
                                                       -1, p_objPersonaRegistroEntity.DireccionCorrespondencia.Trim(), objPersona.Identity.PersonaId,
                                                        p_objPersonaRegistroEntity.PaisCorrespondenciaID, Convert.ToInt32(SILPA.Comun.TipoDireccion.Correspondencia));

                    }

                    objPersona.ActualizarDireccion(p_objPersonaRegistroEntity.MunicipioContactoID, -1,
                                                    -1, p_objPersonaRegistroEntity.DireccionContacto, objPersona.Identity.PersonaId,
                                                    p_objPersonaRegistroEntity.PaisContactoID, Convert.ToInt32(SILPA.Comun.TipoDireccion.Domicilio));
                    
                    //Consultar la persona con la informción actualizada
                    objPersona.ObternerPersonaByUserIdApp(p_objPersonaRegistroEntity.PersonaVITALID.ToString(), true);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PersonaFachada :: ModificarPersona -> Error modificando la persona: " + exc.Message + " - " + (exc.StackTrace != null ? exc.StackTrace.ToString() : ""));

                    throw exc;
                }

                return objPersona.Identity;
            }


        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar una instancia del objeto
            /// </summary>
            /// <returns>PersonaRegistroFachada con la instancia del objeto</returns>
            public static PersonaRegistroFachada GetInstance()
            {
                try
                {
                    //Generar instancia
                    if (Instancia == null)
                        Instancia = new PersonaRegistroFachada();
                }
                catch(Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PersonaFachada :: GetInstance -> Error cargando instancia del objeto: " + exc.Message + " - " + (exc.StackTrace != null ? exc.StackTrace.ToString() : ""));

                    throw exc;
                }

                return Instancia;
            }


            /// <summary>
            /// Registra una persona en el sistema de VITAL
            /// </summary>
            /// <param name="p_strInformacionPersona">string con el XML de información de la persona</param>
            /// <returns>string con el resultado del proceso y la información de la persona</returns>
            public string RegistrarPersona(string p_strInformacionPersona)
            {
                XmlSerializador objSerializador = null;
                PersonaRegistroEntity objPersonaRegistroEntity = null;
                PersonaRespuestaRegistroEntity objPersonaRespuestaRegistroEntity = new PersonaRespuestaRegistroEntity();
                string strMensajeValidacion = "";

                try
                {
                    //Inicializar objeto de respuesta
                    objPersonaRespuestaRegistroEntity.InformacionPersona = null;


                    //Verificar que el contenido no sea nulo
                    if (!string.IsNullOrEmpty(p_strInformacionPersona))
                    {
                        //Cargar la informacion
                        objSerializador = new XmlSerializador();
                        objPersonaRegistroEntity = (PersonaRegistroEntity)objSerializador.Deserializar(new PersonaRegistroEntity(), p_strInformacionPersona);

                        //Verificar la información suministrada de la persona
                        strMensajeValidacion = this.ValidarInformacionRegistro(objPersonaRegistroEntity);
                        if (string.IsNullOrEmpty(strMensajeValidacion))
                        {
                            objPersonaRespuestaRegistroEntity.InformacionPersona = this.CrearPersona(objPersonaRegistroEntity);
                            objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.OK).ToString();
                            objPersonaRespuestaRegistroEntity.Mensaje = "";
                        }
                        else
                        {
                            objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.INFORMACION_EQUIVOCADA).ToString();
                            objPersonaRespuestaRegistroEntity.Mensaje = strMensajeValidacion;
                        }
                    }
                    else
                    {
                        //Cargar mensaje de error
                        objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.INFORMACION_EQUIVOCADA).ToString();
                        objPersonaRespuestaRegistroEntity.Mensaje = "No se especifico la información requerida para el registro de la persona";
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PersonaFachada :: RegistrarPersona -> Error registrando la persona: " + exc.Message + " - " + (exc.StackTrace != null ? exc.StackTrace.ToString() : ""));

                    //Cargar mensaje de error
                    objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.ERROR).ToString();
                    objPersonaRespuestaRegistroEntity.Mensaje = "Se presento un problema durante el registro de la persona";
                }

                return objPersonaRespuestaRegistroEntity.GetXml();
            }


            /// <summary>
            /// Actualiza la información de una persona en el sistema de VITAL
            /// </summary>
            /// <param name="p_strInformacionPersona">string con el XML de información de la persona</param>
            /// <returns>string con el resultado del proceso y la información de la persona</returns>
            public string ActualizarPersona(string p_strInformacionPersona)
            {
                XmlSerializador objSerializador = null;
                PersonaRegistroEntity objPersonaRegistroEntity = null;
                PersonaRespuestaRegistroEntity objPersonaRespuestaRegistroEntity = new PersonaRespuestaRegistroEntity();
                string strMensajeValidacion = "";

                try
                {
                    //Inicializar objeto de respuesta
                    objPersonaRespuestaRegistroEntity.InformacionPersona = null;


                    //Verificar que el contenido no sea nulo
                    if (!string.IsNullOrEmpty(p_strInformacionPersona))
                    {
                        //Cargar la informacion
                        objSerializador = new XmlSerializador();
                        objPersonaRegistroEntity = (PersonaRegistroEntity)objSerializador.Deserializar(new PersonaRegistroEntity(), p_strInformacionPersona);

                        //Verificar la información suministrada de la persona
                        strMensajeValidacion = this.ValidarInformacionModificacion(objPersonaRegistroEntity);
                        if (string.IsNullOrEmpty(strMensajeValidacion))
                        {
                            objPersonaRespuestaRegistroEntity.InformacionPersona = this.ModificarPersona(objPersonaRegistroEntity);
                            objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.OK).ToString();
                            objPersonaRespuestaRegistroEntity.Mensaje = "";
                        }
                        else
                        {
                            objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.INFORMACION_EQUIVOCADA).ToString();
                            objPersonaRespuestaRegistroEntity.Mensaje = strMensajeValidacion;
                        }
                    }
                    else
                    {
                        //Cargar mensaje de error
                        objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.INFORMACION_EQUIVOCADA).ToString();
                        objPersonaRespuestaRegistroEntity.Mensaje = "No se especifico la información requerida para la actualización de la persona";
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PersonaFachada :: ActualizarPersona -> Error actualizando la persona: " + exc.Message + " - " + (exc.StackTrace != null ? exc.StackTrace.ToString() : ""));

                    //Cargar mensaje de error
                    objPersonaRespuestaRegistroEntity.Codigo = ((int)PersonaRespuestaEnum.ERROR).ToString();
                    objPersonaRespuestaRegistroEntity.Mensaje = "Se presento un problema durante la actualización de la persona";
                }

                return objPersonaRespuestaRegistroEntity.GetXml();
            }

        #endregion

    }
}
