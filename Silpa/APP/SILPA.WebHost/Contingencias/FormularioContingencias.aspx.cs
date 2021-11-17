using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;
using SILPA.Comun;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;
using SILPA.LogicaNegocio.Encuestas.Encuesta;
using SILPA.AccesoDatos.Encuestas.Contingencias.Entity;
using SILPA.LogicaNegocio.Encuestas.Contingencias;
using SILPA.LogicaNegocio.Encuestas.Encuesta.Enum;
using SILPA.LogicaNegocio.Encuestas.Contingencias.Enum;
using SILPA.LogicaNegocio.Excepciones;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Generico;

public partial class Contingencias_FormularioContingencias : System.Web.UI.Page
{

    #region Propiedades

        /// <summary>
        /// Listado de permisos de la solicitud
        /// </summary>
        private int SolicitanteID
        {
            get
            {
                return (int)ViewState["_intSolicitanteID_Contingencias_FormularioContingencias"];
            }
            set
            {
                ViewState["_intSolicitanteID_Contingencias_FormularioContingencias"] = value;
            }
        }


        /// <summary>
        /// Listado de preguntas de la solicitud
        /// </summary>
        private FormularioEncuestasEntity FormularioEncuesta
        {
            get
            {
                return (FormularioEncuestasEntity)Session["_FormularioEncuesta_Contingencias_FormularioContingencias"];
            }
            set
            {
                Session["_FormularioEncuesta_Contingencias_FormularioContingencias"] = value;
            }
        }

        /// <summary>
        /// Numero Vital
        /// </summary>
        private string NumeroVital
        {
            get
            {
                return (Session["Contingencias_NumeroVital"] != null ? Session["Contingencias_NumeroVital"].ToString() : "");
            }
            set
            {
                Session["Contingencias_NumeroVital"] = value;
            }
        }

        /// <summary>
        /// Autoridad Ambiental
        /// </summary>
        private string AutoridadAmbiental
        {
            get
            {
                return (Session["Contingencias_Autoridad_Ambiental"] != null ? Session["Contingencias_Autoridad_Ambiental"].ToString() : "");
            }
            set
            {
                Session["Contingencias_Autoridad_Ambiental"] = value;
            }
        }

    #endregion


    #region Metodos Privados


        #region Seguridad

            /// <summary>
            /// Verificar si se encuentra autenticado el usuario
            /// </summary>
            /// <returns></returns>
            private bool ValidacionToken()
            {
                if (DatosSesion.Usuario == string.Empty)
                {
                    return false;
                }

                string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

                Session["IDForToken"] = (object)idUsuario;

                this.SolicitanteID = Convert.ToInt32(Session["IDForToken"]);

                SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

                Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));

                using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
                {
                    servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
                    string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
                    //string mensaje = "Token valido";

                    if (mensaje.IndexOf("Token invalido") > 0)
                        return false;
                }
                return true;
            }

        #endregion


        #region Manejo Errores

            /// <summary>
            /// Mostrar el mensaje de error especificado
            /// </summary>
            /// <param name="p_strMensaje">string con el mensaje</param>
            /// <param name="p_blnMensajeSincronico">Indica si se maneja como modal sincronico. Opcional</param>
            private void MostrarMensajeError(string p_strMensaje, bool p_blnMensajeSincronico = false)
            {
                this.mpeErrorProceso.Show();
                this.ltlErrorProceso.Text = p_strMensaje;
                this.cmdAceptarErrorProceso.Visible = !p_blnMensajeSincronico;
                this.cmdAceptarErrorProcesoSincronico.Visible = p_blnMensajeSincronico;
                this.upnlErrorProceso.Update();
            }


            /// <summary>
            /// Ocultar los mensajes
            /// </summary>
            private void OcultarMensaje()
            {
                this.mpeErrorProceso.Hide();
                this.ltlErrorProceso.Text = "";
                this.upnlErrorProceso.Update();
            }

        #endregion


        #region Listados y Desplegables


            /// <summary>
            /// Obtener el listado de sectores al cual pertenece un proyecto
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado donde se cargaran las opciones</param>
            private void CargarListadoSectores(DropDownList p_objListado)
            {
                SectorEncuestas objSectorEncuestas = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objSectorEncuestas = new SectorEncuestas();
                p_objListado.DataSource = objSectorEncuestas.ConsultarSectores(true);
                p_objListado.DataValueField = "SectorID";
                p_objListado.DataTextField = "Sector";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Obtener el listado de etapas del proyecto
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado donde se cargaran las opciones</param>
            private void CargarListadoEtapasProyecto(DropDownList p_objListado)
            {
                EtapaProyectoContingencias objEtapaContingencia = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objEtapaContingencia = new EtapaProyectoContingencias();
                p_objListado.DataSource = objEtapaContingencia.ConsultarEtapasProyectoContingencias();
                p_objListado.DataValueField = "EtapaProyectoID";
                p_objListado.DataTextField = "EtapaProyecto";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de expediente de un solicitante
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado</param>
            private void CargarListadoExpedientes(DropDownList p_objListado, int p_intAutoridadID)
            {
                ExpedienteEncuestas objExpedienteContingencia = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objExpedienteContingencia = new ExpedienteEncuestas();
                p_objListado.DataSource = objExpedienteContingencia.ConsultarExpedientesSolicitanteSectorAutoridad(this.SolicitanteID, -1, p_intAutoridadID);
                p_objListado.DataValueField = "ExpedienteCodigo";
                p_objListado.DataTextField = "ExpedienteCodigo";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Obtener el listado de autoridades ambientales
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado donde se cargaran las opciones</param>
            private void CargarListadoAutoridades(DropDownList p_objListado)
            {
                AutoridadAmbientalEncuestas objAutoridades = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objAutoridades = new AutoridadAmbientalEncuestas();
                p_objListado.DataSource = objAutoridades.ConsultarAutoridadAmbientales();
                p_objListado.DataValueField = "AutoridadID";
                p_objListado.DataTextField = "Autoridad";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Obtener el listado de niveles de emergencia
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado donde se cargaran las opciones</param>
            private void CargarListadoNivelesEmergencia(DropDownList p_objListado)
            {
                NivelEmergenciaContingencias objNivelesEmergencia = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objNivelesEmergencia = new NivelEmergenciaContingencias();
                p_objListado.DataSource = objNivelesEmergencia.ConsultarNivelesEmergenciaContingencias();
                p_objListado.DataValueField = "NivelEmergenciaID";
                p_objListado.DataTextField = "NivelEmergencia";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Obtener el listado de departamentos
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado donde se cargaran las opciones</param>
            private void CargarListadoDepartamentos(DropDownList p_objListado)
            {
                Departamento objDepartamento = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objDepartamento = new Departamento();
                p_objListado.DataSource = objDepartamento.ConsultarDepartamentos(null, null).Where(dep => dep.Id > 0).ToList();
                p_objListado.DataValueField = "Id";
                p_objListado.DataTextField = "Nombre";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Obtener el listado de departamentos
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado donde se cargaran las opciones</param>
            /// <param name="p_intMunicipioID">int con el identificador del municipio</param>
            private void CargarListadoMunicipios(DropDownList p_objListado, int p_intMunicipioID)
            {
                Municipio objMunicipio = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                if (p_intMunicipioID > 0)
                {
                    objMunicipio = new Municipio();
                    p_objListado.DataSource = objMunicipio.ListarMunicipios(null, p_intMunicipioID, null);
                    p_objListado.DataValueField = "MUN_ID";
                    p_objListado.DataTextField = "MUN_NOMBRE";
                    p_objListado.DataBind();
                }
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }



        #endregion


        #region Manejo Objetos Pagina


            /// <summary>
            /// Limpia el cuestionario de preguntas
            /// </summary>
            private void LimpiarCuestionario()
            {
                //Limpiar objeto
                this.FormularioEncuesta = null;

                //Limpiar repeater
                this.rptCuestionario.DataSource = null;
                this.rptCuestionario.DataBind();
            }


            /// <summary>
            /// Limpiar campos basicos del formulario
            /// </summary>
            private void LimpiarDatosBasicosFormulario()
            {
                //Limpiar desplegables
                this.cboExpediente.ClearSelection();
                this.cboExpediente.Items.Clear();
                this.txtExpedienteCorporacion.Text = "";
                this.txtNombreProyectoCorporacion.Text = "";
                this.cboSector.ClearSelection();
                this.cboEtapaProyecto.ClearSelection();
                this.cboNivelEmergencia.ClearSelection();

                //Limpiar campos
                this.txtEtapaProyectoOtro.Text = "";
                this.txtEtapaProyectoOtro.Attributes.Add("style", "display: none;");
                this.ltlNombreProyecto.Text = "";                
                this.txtNombreResponsable.Text = "";
                this.txtTelefonoResponsable.Text = "";
                this.txtEmailResponsable.Text = "";

                //Ocultar secciones
                this.trNivelEmergencia.Visible = false;
                this.trExpediente.Visible = false;
                this.trNombreProyecto.Visible = false;
                this.trSector.Visible = false;
                this.trEtapaProyecto.Visible = false;
                this.trNombreResponsable.Visible = false;
                this.trTelefonoResponsable.Visible = false;
                this.trEmailResponsable.Visible = false;
                this.tblCuestionario.Visible = false;
            }

            /// <summary>
            /// Limpia e inicializa el formulario
            /// </summary>
            private void LimpiarCamposFormulario()
            {
                //Limpiar datos de formulario
                this.FormularioEncuesta = null;
                this.NumeroVital = null;
                this.AutoridadAmbiental = null;

                //Limpiar desplegables
                this.cboExpediente.ClearSelection();
                this.cboExpediente.Items.Clear();
                this.cboEtapaProyecto.ClearSelection();
                this.cboEtapaProyecto.Items.Clear();

                //Limpiar campos basicos
                this.LimpiarDatosBasicosFormulario();

                //Limpiar cuestionario
                this.LimpiarCuestionario();
            }


            /// <summary>
            /// Cargar la información inicial de la pagina
            /// </summary>
            private void InicializarPagina()
            {
                //Limpiar los campos del formulario de busqueda
                this.LimpiarCamposFormulario();

                //Cargar el listados
                this.CargarListadoAutoridades(this.cboAutoridadAmbiental);
                this.CargarListadoSectores(this.cboSector);
                this.CargarListadoEtapasProyecto(this.cboEtapaProyecto);
                this.CargarListadoNivelesEmergencia(this.cboNivelEmergencia);
            }


        #endregion


        #region Manejo Datos Cuestionario


            /// <summary>
            /// Cargar la informacion del expediente seleccionado
            /// </summary>
            /// <param name="p_strCodigoExpediente">string con el codigo del expediente</param>
            /// <param name="p_intAutoridadAmbiental">int con el identificador de la autoridad ambiental</param>
            private void CargarDatosExpediente(string p_strCodigoExpediente, int p_intAutoridadAmbiental)
            {
                ExpedienteEncuestas objExpedienteContingencia = null;
                ExpedienteEncuestasEntity objExpediente = null;

                //Obtener los datos del expediente
                objExpedienteContingencia = new ExpedienteEncuestas();
                objExpediente = objExpedienteContingencia.ConsultarExpedienteAutoridad(p_strCodigoExpediente, p_intAutoridadAmbiental);

                //Cargar los datos
                if (objExpediente != null)
                {
                    this.ltlNombreProyecto.Text = objExpediente.ExpedienteNombre;
                }
                else
                {
                    this.ltlNombreProyecto.Text = "";
                }

            }


            /// <summary>
            /// Consulta y muestra  el cuestionario perteneciente a un sector especifico
            /// </summary>
            /// <param name="p_intSectorID">int con el identificador del sector</param>
            private void CargarCuestionarioSector(int p_intSectorID)
            {
                FormularioEncuestas objFormularioEncuesta = null;

                //Obtener la información del cuestionario a mostrar
                objFormularioEncuesta = new FormularioEncuestas();
                this.FormularioEncuesta = objFormularioEncuesta.ConsultarFormularioSector( (int)FormularioEncuestasEnum.Contingencias_Inicial,  p_intSectorID);

                //Verificar que se obtenga datos
                if (this.FormularioEncuesta != null && this.FormularioEncuesta.FormularioID > 0)
                {
                    //Cargar los sectores
                    this.rptCuestionario.DataSource = this.FormularioEncuesta.Secciones;
                    this.rptCuestionario.DataBind();
                }
                else
                {
                    throw new Exception("No se encontro configuración de formulario para el sector " + p_intSectorID.ToString());
                }
            }


            /// <summary>
            /// Cargar opciones en string con estructura json
            /// </summary>
            /// <param name="p_objLstOpciones">List con la informacion de opciones</param>
            /// <returns>string con la informacion</returns>
            private string CargarOpcionesMostrarPreguntas(List<PreguntaHabilitaOpcionEncuestasEntity> p_objLstOpciones)
            {
                string strDatos = "[";

                //Verificar que se tenga datos
                if (p_objLstOpciones != null && p_objLstOpciones.Count > 0)
                {
                    foreach (PreguntaHabilitaOpcionEncuestasEntity objOpcion in p_objLstOpciones)
                    {
                        if (strDatos != "[") strDatos += ",";
                        strDatos += "{\"PreguntaID\": " + objOpcion.PreguntaID + ",\"OpcionID\": " + objOpcion.OpcionID + ",\"EsOpcional\": " + (objOpcion.EsOpcional ? "1" : "0") + "}";
                    }
                }

                strDatos += "]";

                return strDatos;
            }


        #endregion


        #region Almacenamiento Datos Cuestionario


            /// <summary>
            /// Cargar el listado de opciones 
            /// </summary>
            /// <param name="p_objRadios">Radio con las opciones</param>
            /// <returns>List con las opciones capturas</returns>
            private List<OpcionPreguntaSolicitudContingenciasEntity> CargarListadoOpcionesRadio(int p_intPreguntaID, RadioButtonList p_objRadios, TextBox p_objTextOtro)
            {
                List<OpcionPreguntaSolicitudContingenciasEntity> objOpciones = new List<OpcionPreguntaSolicitudContingenciasEntity>();
                OpcionPreguntaSolicitudContingenciasEntity objOpcion = null;

                //Ciclo que recorre items
                foreach(ListItem objItem in p_objRadios.Items)
                {
                    objOpcion = new OpcionPreguntaSolicitudContingenciasEntity
                    {
                        Pregunta = new PreguntaEncuestasEntity { PreguntaID = p_intPreguntaID },
                        OpcionPregunta = new OpcionPreguntaEncuestasEntity { OpcionPreguntaID = Convert.ToInt32(objItem.Value) },
                        RespuestaOpcion = ""
                    };

                    if (objItem.Value == Request.Form[p_objRadios.UniqueID + p_intPreguntaID.ToString() + "_"])
                    {
                        objOpcion.Selecciono = true;

                        if (p_objTextOtro != null)
                        {
                            objOpcion.RespuestaOpcion = Request.Form[p_objTextOtro.UniqueID + p_intPreguntaID.ToString() + "_" + objItem.Value + "_"];
                        }
                    }
                    else
                    {
                        objOpcion.Selecciono = false;
                    }

                    objOpciones.Add(objOpcion);
                }

                return objOpciones;
            }

            
            /// <summary>
            /// Cargar el listado de opciones 
            /// </summary>
            /// <param name="p_objRadios">Radio con las opciones</param>
            /// <returns>List con las opciones capturas</returns>
            private List<OpcionPreguntaSolicitudContingenciasEntity> CargarListadoOpcionesMultiples(int p_intPreguntaID, Repeater p_objOpcionesMultiples, TextBox p_objTextOtro)
            {
                List<OpcionPreguntaSolicitudContingenciasEntity> objOpciones = new List<OpcionPreguntaSolicitudContingenciasEntity>();
                HtmlInputCheckBox objCheck = null;
                OpcionPreguntaSolicitudContingenciasEntity objOpcion = null;

                //Ciclo que recorre items
                foreach (RepeaterItem objItem in p_objOpcionesMultiples.Items)
                {
                    //Cargar Check
                    objCheck = (HtmlInputCheckBox)objItem.FindControl("rdOpcionMultiple");

                    if (objCheck.Value == Request.Form[objCheck.UniqueID + p_intPreguntaID.ToString() + "_"])
                    {
                        objOpcion = new OpcionPreguntaSolicitudContingenciasEntity
                        {
                            Pregunta = new PreguntaEncuestasEntity { PreguntaID = p_intPreguntaID },
                            OpcionPregunta = new OpcionPreguntaEncuestasEntity { OpcionPreguntaID = Convert.ToInt32(objCheck.Value) },
                            RespuestaOpcion = "",
                            Selecciono = true
                        };

                        if (p_objTextOtro != null)
                        {
                            objOpcion.RespuestaOpcion = Request.Form[p_objTextOtro.UniqueID + p_intPreguntaID.ToString() + "_" + objCheck.Value + "_"];
                        }
                    }
                    else
                    {
                        objOpcion = new OpcionPreguntaSolicitudContingenciasEntity
                        {
                            Pregunta = new PreguntaEncuestasEntity { PreguntaID = p_intPreguntaID },
                            OpcionPregunta = new OpcionPreguntaEncuestasEntity { OpcionPreguntaID = Convert.ToInt32(objCheck.Value) },
                            RespuestaOpcion = "",
                            Selecciono = false
                        };
                    }
                    

                    //Adicionar al listado
                    objOpciones.Add(objOpcion);
                }

                return objOpciones;
            }


            /// <summary>
            /// Cargar el listado de opciones  de texto
            /// </summary>
            /// <param name="p_objRepeater">Repeater con las opciones</param>
            /// <returns>List con las opciones capturas</returns>
            private List<OpcionPreguntaSolicitudContingenciasEntity> CargarListadoOpcionesTexto(int p_intPreguntaID, Repeater p_objRepeater)
            {
                TextBox objTextBoxTexto = null;
                HiddenField objHiddenField = null;

                List<OpcionPreguntaSolicitudContingenciasEntity> objOpciones = new List<OpcionPreguntaSolicitudContingenciasEntity>();
                OpcionPreguntaSolicitudContingenciasEntity objOpcion = null;

                //Ciclo que recorre items
                foreach (RepeaterItem objItem in p_objRepeater.Items)
                {
                    //Cargar el identificador
                    objHiddenField = (HiddenField)objItem.FindControl("hdfOpcionPreguntaID");                    

                    //Cargar campos
                    objTextBoxTexto = (TextBox)objItem.FindControl("txtOpcionTexto");
                    if (objTextBoxTexto != null && !string.IsNullOrWhiteSpace(objTextBoxTexto.Text))
                    {
                        objOpcion = new OpcionPreguntaSolicitudContingenciasEntity
                        {
                            Pregunta = new PreguntaEncuestasEntity { PreguntaID = p_intPreguntaID },
                            OpcionPregunta = new OpcionPreguntaEncuestasEntity { OpcionPreguntaID = Convert.ToInt32(objHiddenField.Value) },
                            RespuestaOpcion = objTextBoxTexto.Text
                        };

                        objOpciones.Add(objOpcion);
                    }
                    else
                    {
                        objTextBoxTexto = (TextBox)objItem.FindControl("txtOpcionTextoNumerico");
                        if (objTextBoxTexto != null && !string.IsNullOrWhiteSpace(objTextBoxTexto.Text))
                        {
                            objOpcion = new OpcionPreguntaSolicitudContingenciasEntity
                            {
                                Pregunta = new PreguntaEncuestasEntity { PreguntaID = p_intPreguntaID },
                                OpcionPregunta = new OpcionPreguntaEncuestasEntity { OpcionPreguntaID = Convert.ToInt32(objHiddenField.Value) },
                                RespuestaOpcion = objTextBoxTexto.Text
                            };

                            objOpciones.Add(objOpcion);
                        }
                    }                    
                }

                return objOpciones;
            }

            /// <summary>
            /// Cargar la informacion del documento generado
            /// </summary>
            /// <returns>DocumentoPreguntaSolicitudContingenciasEntity con la informacion del documento</returns>
            private DocumentoPreguntaSolicitudContingenciasEntity CargarDocumentosFormulario(int p_intPreguntaID, FileUpload p_objFileUpload)
            {
                DocumentoPreguntaSolicitudContingenciasEntity objDocumento = null;
                string strCarpetaTemporal = "";

                //Cargar la carpeta temporal donde se almacenara los archivos
                strCarpetaTemporal = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + string.Format(@"{0}\{1}\", ConfigurationManager.AppSettings["CarpetaTemporalSolicitudContingencias"].ToString(), this.SolicitanteID.ToString());

                //Crear la carpeta temporal en caso de que no exista
                if (!Directory.Exists(strCarpetaTemporal))
                    Directory.CreateDirectory(strCarpetaTemporal);

                //Verificar que se obtenga archivo
                if (Request.Files[p_objFileUpload.UniqueID + p_intPreguntaID.ToString() + "_"] != null && Request.Files[p_objFileUpload.UniqueID + p_intPreguntaID.ToString() + "_"].FileName != "")
                {
                    //Cargar datos del documento
                    objDocumento = new DocumentoPreguntaSolicitudContingenciasEntity
                    {
                        Pregunta = new PreguntaEncuestasEntity { PreguntaID = p_intPreguntaID },
                        Ubicacion = strCarpetaTemporal,
                        NombreDocumentoUsuario = Request.Files[p_objFileUpload.UniqueID + p_intPreguntaID.ToString() + "_"].FileName,
                        NombreDocumento = string.Format("DocumentoPregunta{0}_{1}_{2}{3}", this.SolicitanteID.ToString(), p_intPreguntaID.ToString(), DateTime.Now.ToString("yyyyMMddhhmmssmmm"), Request.Files[p_objFileUpload.UniqueID + p_intPreguntaID.ToString() + "_"].FileName.Trim().Substring(Request.Files[p_objFileUpload.UniqueID + p_intPreguntaID.ToString() + "_"].FileName.Trim().LastIndexOf(".")))
                    };

                    //Guardar archivo
                    Request.Files[p_objFileUpload.UniqueID + p_intPreguntaID.ToString() + "_"].SaveAs(objDocumento.Ubicacion + objDocumento.NombreDocumento);
                }

                return objDocumento;
            }


            /// <summary>
            /// Cargar la informacion capturada por el usuario de cada una de las preguntas
            /// </summary>
            /// <returns>List con el listado de preguntas</returns>
            private List<PreguntaSolicitudContingenciasEntity> CargarPreguntasFormulario()
            {
                List<PreguntaSolicitudContingenciasEntity> objLstPreguntas = null;
                PreguntaSolicitudContingenciasEntity objPregunta = null;
                HiddenField objHiddenField = null;
                Repeater objRepeater = null;
                RadioButtonList objRadioButtonList = null;
                Repeater objRepeaterTexto = null;
                TextBox objTextBox = null;
                DropDownList objDropDownList = null;
                CoordenadasPreguntaSolicitudContingenciasEntity objCoordenadas = null;
                DocumentoPreguntaSolicitudContingenciasEntity objDocumento = null;
                FileUpload objFileUpload = null;
                int intPreguntaID = 0;
                int intTipoPreguntaID = 0;
                int intDepartamentoID = 0;
                int intMunicipioID = 0;
                double dblValor = 0;
                string strHora = "";

                //Recorrer el repeater para encontrar cada uno de los datos capturados
                if (this.rptCuestionario.Items.Count > 0)
                {
                    //Crear listado de preguntas
                    objLstPreguntas = new List<PreguntaSolicitudContingenciasEntity>();

                    //Ciclo que recorre cada uno de los items
                    foreach (RepeaterItem objItem in this.rptCuestionario.Items)
                    {
                        //Cargar datos contenidos para cada unas de las preguntas
                        objRepeater = (Repeater)objItem.FindControl("rptPreguntasCuestionario");
                        if (objRepeater != null && objRepeater.Items.Count > 0)
                        {
                            foreach(RepeaterItem objItemPregunta in objRepeater.Items)
                            {
                                //Cargar el identificador de la pregunta
                                objHiddenField = (HiddenField)objItemPregunta.FindControl("hdfPreguntaID");
                                if (objHiddenField != null && objHiddenField.Value != "")
                                    intPreguntaID = Convert.ToInt32(objHiddenField.Value);

                                //Verificar si la pregunta se mostro
                                objHiddenField = (HiddenField)objItemPregunta.FindControl("hdfPreguntaMostrada");
                                if (objHiddenField != null && Request.Form[objHiddenField.UniqueID + intPreguntaID.ToString()] != null && Request.Form[objHiddenField.UniqueID + intPreguntaID.ToString()] == "1")
                                {
                                    //Crear la pregunta
                                    objPregunta = new PreguntaSolicitudContingenciasEntity { 
                                        Pregunta = new PreguntaEncuestasEntity { PreguntaID = intPreguntaID },
                                        OpcionesPregunta = new List<OpcionPreguntaSolicitudContingenciasEntity>(),
                                        RespuestasPregunta = new List<RespuestaPreguntaSolicitudContingenciasEntity>(),
                                        LocalizacionesPregunta = new List<LocalizacionPreguntaSolicitudContingenciasEntity>(),
                                        CoordenadasPregunta = new List<CoordenadasPreguntaSolicitudContingenciasEntity>(),
                                        DocumentosPregunta = new List<DocumentoPreguntaSolicitudContingenciasEntity>()
                                    };

                                    //Cargar el identificador de la pregunta
                                    objHiddenField = (HiddenField)objItemPregunta.FindControl("hdfTipoPregunta");
                                    if (objHiddenField != null && objHiddenField.Value != "")
                                        intTipoPreguntaID = Convert.ToInt32(objHiddenField.Value);

                                    //Cargar los datos al sistema de acuerdo 
                                    switch (intTipoPreguntaID)
                                    {
                                        case (int)TipoPreguntaEncuestasEnum.Seleccion_Unica:

                                            //Cargar objetos
                                            objRepeaterTexto = (Repeater)objItemPregunta.FindControl("rptOpcionesPregunta");
                                            objRadioButtonList = (RadioButtonList)objItemPregunta.FindControl("rdOpcionUnica");
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtOpcionOtro");
                                            if (objRadioButtonList != null && objRadioButtonList.Visible && objRadioButtonList.Items.Count > 0)
                                            {
                                                objPregunta.OpcionesPregunta = this.CargarListadoOpcionesRadio(intPreguntaID, objRadioButtonList, objTextBox);
                                            }
                                            else if (objRepeaterTexto != null && objRepeaterTexto.Visible && objRepeaterTexto.Items.Count > 0)
                                            {
                                                objPregunta.OpcionesPregunta = this.CargarListadoOpcionesTexto(intPreguntaID, objRepeaterTexto);
                                            }
                                            break;
                                        case (int)TipoPreguntaEncuestasEnum.Campo_Abierto:
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtRespuestaPregunta");
                                            if (objTextBox != null)
                                            {
                                                objPregunta.RespuestasPregunta.Add(new RespuestaPreguntaSolicitudContingenciasEntity
                                                {
                                                    Pregunta = new PreguntaEncuestasEntity { PreguntaID = intPreguntaID },
                                                    Respuesta = objTextBox.Text.Trim()
                                                });
                                            }
                                            break;
                                        case (int)TipoPreguntaEncuestasEnum.Coordenadas:

                                            objCoordenadas = new CoordenadasPreguntaSolicitudContingenciasEntity { Pregunta = new PreguntaEncuestasEntity { PreguntaID = intPreguntaID } };
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtGradosLongitud");
                                            if (objTextBox != null && objTextBox.Text != "")
                                            {
                                                objCoordenadas.GradosLongitud = Convert.ToInt32(objTextBox.Text.Replace(".", "")) * -1;
                                            }
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtMinutosLongitud");
                                            if (objTextBox != null && objTextBox.Text != "")
                                            {
                                                double.TryParse(objTextBox.Text.Replace(".", ""), NumberStyles.Any, CultureInfo.CurrentCulture, out dblValor);
                                                objCoordenadas.MinutosLongitud = Convert.ToDecimal(dblValor);
                                            }
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtSegundosLongitud");
                                            if (objTextBox != null && objTextBox.Text != "")
                                            {
                                                double.TryParse(objTextBox.Text.Replace(".", ""), NumberStyles.Any, CultureInfo.CurrentCulture, out dblValor);
                                                objCoordenadas.SegundosLongitud = Convert.ToDecimal(dblValor);
                                            }
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtGradosLatitud");
                                            if (objTextBox != null && objTextBox.Text != "")
                                            {
                                                objCoordenadas.GradosLatitud = Convert.ToInt32(objTextBox.Text.Replace(".", "")) * -1;
                                            }
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtMinutosLatitud");
                                            if (objTextBox != null && objTextBox.Text != "")
                                            {
                                                double.TryParse(objTextBox.Text.Replace(".", ""), NumberStyles.Any, CultureInfo.CurrentCulture, out dblValor);
                                                objCoordenadas.MinutosLatitud = Convert.ToDecimal(dblValor);
                                            }
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtSegundosLatitud");
                                            if (objTextBox != null && objTextBox.Text != "")
                                            {
                                                double.TryParse(objTextBox.Text.Replace(".", ""), NumberStyles.Any, CultureInfo.CurrentCulture, out dblValor);
                                                objCoordenadas.SegundosLatitud = Convert.ToDecimal(dblValor);
                                            }
                                            objPregunta.CoordenadasPregunta.Add(objCoordenadas);
                                            break;
                                        case (int)TipoPreguntaEncuestasEnum.Ubicacion_Geografica:

                                            objDropDownList = (DropDownList)objItemPregunta.FindControl("cboDepartamentoPregunta");
                                            if (objDropDownList != null)
                                            {
                                                intDepartamentoID = Convert.ToInt32(objDropDownList.SelectedValue);    
                                            }
                                            objDropDownList = (DropDownList)objItemPregunta.FindControl("cboCiudadPregunta");
                                            if (objDropDownList != null)
                                            {
                                                intMunicipioID = Convert.ToInt32(objDropDownList.SelectedValue);    
                                            }
                                            objPregunta.LocalizacionesPregunta.Add(new LocalizacionPreguntaSolicitudContingenciasEntity
                                            {
                                                Pregunta = new PreguntaEncuestasEntity { PreguntaID = intPreguntaID },
                                                Departamento = intDepartamentoID.ToString(),
                                                Ciudad = intMunicipioID.ToString()
                                            });

                                            break;
                                        case (int)TipoPreguntaEncuestasEnum.Documento:

                                            objFileUpload = (FileUpload)objItemPregunta.FindControl("fuplDocumentoPregunta");
                                            if(objFileUpload != null)
                                            {
                                                objDocumento = this.CargarDocumentosFormulario(intPreguntaID, objFileUpload);
                                                if (objDocumento != null)
                                                    objPregunta.DocumentosPregunta.Add(objDocumento);
                                            }

                                            break;

                                        case (int)TipoPreguntaEncuestasEnum.Calendario:
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtFechaPregunta");
                                            if (objTextBox != null)
                                            {
                                                objPregunta.RespuestasPregunta.Add(new RespuestaPreguntaSolicitudContingenciasEntity
                                                {
                                                    Pregunta = new PreguntaEncuestasEntity { PreguntaID = intPreguntaID },
                                                    Respuesta = objTextBox.Text.Trim()
                                                });
                                            }
                                            break;
                                        case (int)TipoPreguntaEncuestasEnum.Hora:

                                            strHora = "";

                                            objDropDownList = (DropDownList)objItemPregunta.FindControl("cboHora");
                                            if (objDropDownList != null)
                                            {
                                                strHora = objDropDownList.SelectedValue;    
                                            }
                                            objDropDownList = (DropDownList)objItemPregunta.FindControl("cboMInutos");
                                            if (objDropDownList != null)
                                            {
                                                strHora = strHora + ":" + objDropDownList.SelectedValue;
                                            }

                                            if (!string.IsNullOrWhiteSpace(strHora))
                                            {
                                                objPregunta.RespuestasPregunta.Add(new RespuestaPreguntaSolicitudContingenciasEntity
                                                {
                                                    Pregunta = new PreguntaEncuestasEntity { PreguntaID = intPreguntaID },
                                                    Respuesta = strHora
                                                });
                                            }
                                            break;

                                        case (int)TipoPreguntaEncuestasEnum.Seleccion_Multiple:
                                            
                                            //Cargar objetos
                                            objRepeaterTexto = (Repeater)objItemPregunta.FindControl("rptOpcioneMultiples");
                                            objTextBox = (TextBox)objItemPregunta.FindControl("txtTextoOpcionMultiple");
                                            if (objRepeaterTexto != null && objRepeaterTexto.Visible && objRepeaterTexto.Items.Count > 0)
                                            {
                                                objPregunta.OpcionesPregunta = this.CargarListadoOpcionesMultiples(intPreguntaID, objRepeaterTexto, objTextBox);
                                            }

                                            break;

                                    };

                                    objLstPreguntas.Add(objPregunta);
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("No existen preguntas en el formulario para ser almacenadas");
                }

                return objLstPreguntas;
            }


            /// <summary>
            /// Carga los datos capturados por los usuarios en el formulario
            /// </summary>
            /// <returns>SolicitudContingenciaEntity con la información de la solicitud</returns>
            private SolicitudContingenciasEntity CargarDatosSolicitud()
            {
                SolicitudContingenciasEntity objSolicitudContingenciaEntity = null;

                //Crear objeto formulario y cargar datos basicos
                objSolicitudContingenciaEntity = new SolicitudContingenciasEntity
                {
                    FormularioID = (int)FormularioEncuestasEnum.Contingencias_Inicial,
                    AutoridadID = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue),
                    SolicitanteID = this.SolicitanteID,
                    SectorID = Convert.ToInt32(cboSector.SelectedValue),
                    NombreResponsable = this.txtNombreResponsable.Text.Trim(),
                    NumeroTelefonicoResponsable = this.txtTelefonoResponsable.Text.Trim(),
                    EmailResponsable = this.txtEmailResponsable.Text.Trim(),
                    EtapaProyecto = new EtapaProyectoContingenciasEntity { EtapaProyectoID = Convert.ToInt32(this.cboEtapaProyecto.SelectedValue) },
                    EtapaProyectoOtro = (Convert.ToInt32(this.cboEtapaProyecto.SelectedValue) == (int)EtapaProyectoContingenciasEnum.Otro ? this.txtEtapaProyectoOtro.Text.Trim() : ""),
                    NivelEmergenciaContingenciaID = this.cboNivelEmergencia.SelectedValue,
                    Activo = true
                };

                //Cargar el expediente y el nombre del proyecto
                if(Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue) == (int)AutoridadesAmbientales.ANLA)
                {
                    objSolicitudContingenciaEntity.Expediente = new ExpedienteEncuestasEntity { ExpedienteCodigo = this.cboExpediente.SelectedValue, AutoridadID = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue), ExpedienteNombre = this.ltlNombreProyecto.Text.Trim(), SolicitanteID = this.SolicitanteID };
                    objSolicitudContingenciaEntity.nombreProyecto = this.ltlNombreProyecto.Text.Trim();
                }
                else
                {
                    objSolicitudContingenciaEntity.Expediente = new ExpedienteEncuestasEntity { ExpedienteCodigo = this.txtExpedienteCorporacion.Text.Trim(), AutoridadID = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue), ExpedienteNombre = this.txtNombreProyectoCorporacion.Text.Trim(), SolicitanteID = this.SolicitanteID };
                    objSolicitudContingenciaEntity.nombreProyecto = this.txtNombreProyectoCorporacion.Text.Trim();
                }


                //Cargar el listado de preguntas
                objSolicitudContingenciaEntity.Preguntas = this.CargarPreguntasFormulario();

                //Verificar que se cargue información de las preguntas del proyecto
                if (objSolicitudContingenciaEntity.Preguntas == null || objSolicitudContingenciaEntity.Preguntas.Count == 0)
                    throw new Exception("No se pudo obtener la información de las preguntas del formulario.");

                return objSolicitudContingenciaEntity;
            }


            /// <summary>
            /// Almacenar la informacion de la solicitud
            /// </summary>
            /// <param name="objSolicitudContingenciaEntity">SolicitudContingenciaEntity con la informacion de la solicitud a guardar</param>
            private string AlmacenarInformacionSolicitud(SolicitudContingenciasEntity objSolicitudContingenciaEntity)
            {
                SolicitudContingencias objSolicitudContingencia = null;
                string strNumeroVital = "";

                //Almacenra la información de cambio menor
                objSolicitudContingencia = new SolicitudContingencias();
                strNumeroVital = objSolicitudContingencia.InsertarSolicitudContingencias(ref objSolicitudContingenciaEntity);

                return strNumeroVital;
            }


            /// <summary>
            /// Guardar la información de la solicitud
            /// </summary>        
            private void GuardarInformacionCuestionario()
            {
                SolicitudContingenciasEntity objSolicitudContingenciaEntity = null;

                //Cargar los datos de la solicitud
                objSolicitudContingenciaEntity = this.CargarDatosSolicitud();

                //Almacenar la información de la solicitud en sesion
                this.NumeroVital = this.AlmacenarInformacionSolicitud(objSolicitudContingenciaEntity);
                this.AutoridadAmbiental = this.cboAutoridadAmbiental.SelectedItem.Text;
            }


        #endregion


    #endregion


    #region Eventos

        #region Formulario

            #region Page

                /// <summary>
                /// Evento que se ejecuta al cargar la pagina
                /// </summary>
                protected void Page_Load(object sender, EventArgs e)
                {
                    //this.SolicitanteID = 456;
                    //this.SolicitanteID = 429;

                    try
                    {
                        if (!IsPostBack)
                        {
                            //Ocultar mensajes de error
                            this.OcultarMensaje();

                            //Inicializar pagina
                            //this.InicializarPagina();

                            //Validar sesion de usuario
                            if (this.ValidacionToken() == false)
                            {
                                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                            }
                            else
                            {
                                //Iniciliazar datos de la página
                                this.InicializarPagina();
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error cargando información del formulario.");

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Contingencias_FormularioContingencias :: Page_Load -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    }
                }

            #endregion  


            #region Informacion Basica


                /// <summary>
                /// Evento que carga el listado de expedientes
                /// </summary>
                protected void cboAutoridadAmbiental_SelectedIndexChanged(object sender, EventArgs e)
                {
                    string strAutoridad = "";

                    try
                    {
                        if (((DropDownList)sender).SelectedValue != "-1")
                        {
                            //Cargar el expediente
                            strAutoridad = ((DropDownList)sender).SelectedValue;

                            //Limpiar datos basicos
                            this.LimpiarDatosBasicosFormulario();

                            //Limpiar cuestinario
                            this.LimpiarCuestionario();

                            //Cargar el identificador
                            ((DropDownList)sender).SelectedValue = strAutoridad;

                            //Mostrar seccion de expedientes
                            this.trExpediente.Visible = true;

                            //Verificar si la autoridad ambiental es la ANLA
                            if (strAutoridad == ((int)AutoridadesAmbientales.ANLA).ToString())
                            {
                                //Cargar listado de expedientes
                                this.CargarListadoExpedientes(this.cboExpediente, Convert.ToInt32(strAutoridad));

                                //Mostrar desplegable y ocultar campos de texto
                                this.cboExpediente.Visible = true;
                                this.rfvExpediente.Enabled = true;
                                this.txtExpedienteCorporacion.Visible = false;
                                this.cvExpedienteCorporacion.Enabled = false;
                                this.cvNombreProyectoCorporacion.Enabled = false;
                            }
                            else
                            {
                                //Ocultar desplegable y mostrar campos de texto
                                this.cboExpediente.Visible = false;
                                this.rfvExpediente.Enabled = false;
                                this.txtExpedienteCorporacion.Visible = true;
                                this.cvExpedienteCorporacion.Enabled = true;

                                //Limpiar desplegables
                                this.cboSector.ClearSelection();
                                this.cboEtapaProyecto.ClearSelection();
                                this.cboNivelEmergencia.ClearSelection();

                                //Mostrar campos
                                this.trNombreProyecto.Visible = true;
                                this.trSector.Visible = true;
                                this.trEtapaProyecto.Visible = true;
                                this.trNombreResponsable.Visible = true;
                                this.trTelefonoResponsable.Visible = true;
                                this.trEmailResponsable.Visible = true;
                                this.trNivelEmergencia.Visible = true;

                                //Mostrar los campos de texto del nombre del proyecto
                                this.ltlNombreProyecto.Visible = false;
                                this.txtNombreProyectoCorporacion.Visible = true;
                                this.cvNombreProyectoCorporacion.Enabled = true;
                            }
                        }
                        else
                        {
                            //Limpiar datos basicos
                            this.LimpiarDatosBasicosFormulario();

                            //Limpiar cuestinario
                            this.LimpiarCuestionario();
                        }
                    }
                    catch (Exception exc)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error cargando información de la autoridad ambiental.");

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Contingencias_FormularioContingencias :: cboAutoridadAmbiental_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    }
                }


                /// <summary>
                /// Evnto que carga informacion del expediente y del cuestionario
                /// </summary>
                protected void cboExpediente_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        if (((DropDownList)sender).SelectedValue != "-1")
                        {
                            //Cargar datos de un expediente
                            this.CargarDatosExpediente(((DropDownList)sender).SelectedValue, Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue));
                            this.ltlNombreProyecto.Visible = true;

                            //Limpiar desplegables
                            this.cboSector.ClearSelection();
                            this.cboEtapaProyecto.ClearSelection();
                            this.cboNivelEmergencia.ClearSelection();

                            //Limpiar cuestinario
                            this.LimpiarCuestionario();

                            //Mostrar campos
                            this.trNombreProyecto.Visible = true;
                            this.trSector.Visible = true;
                            this.trEtapaProyecto.Visible = true;
                            this.trNombreResponsable.Visible = true;
                            this.trTelefonoResponsable.Visible = true;
                            this.trEmailResponsable.Visible = true;
                            this.trNivelEmergencia.Visible = true;
                        }
                        else
                        {
                            //Limpiar desplegables
                            this.cboSector.ClearSelection();
                            this.cboEtapaProyecto.ClearSelection();
                            this.cboNivelEmergencia.ClearSelection();

                            //Limpiar campos
                            this.txtEtapaProyectoOtro.Text = "";
                            this.ltlNombreProyecto.Text = "";
                            this.txtNombreResponsable.Text = "";
                            this.txtTelefonoResponsable.Text = "";
                            this.txtEmailResponsable.Text = "";

                            //Limpiar cuestinario
                            this.LimpiarCuestionario();

                            //Ocultar campos
                            this.trNombreProyecto.Visible = false;
                            this.trSector.Visible = false;
                            this.trEtapaProyecto.Visible = false;
                            this.trNombreResponsable.Visible = false;
                            this.trTelefonoResponsable.Visible = false;
                            this.trEmailResponsable.Visible = false;
                            this.trNivelEmergencia.Visible = false;
                        }

                        //Ocultar los campos de texto del nombre del proyecto
                        this.txtNombreProyectoCorporacion.Text = "";
                        this.txtNombreProyectoCorporacion.Visible = false;
                        this.cvNombreProyectoCorporacion.Enabled = false;
                    }
                    catch (Exception exc)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error cargando información del expediente.");

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Contingencias_FormularioContingencias :: cboExpediente_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    }
                }

                /// <summary>
                /// Evento que carga las preguntas del formulario
                /// </summary>
                protected void cboSector_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        if (((DropDownList)sender).SelectedValue != "-1")
                        {
                            this.LimpiarCuestionario();
                            this.CargarCuestionarioSector(Convert.ToInt32(((DropDownList)sender).SelectedValue));
                            this.tblCuestionario.Visible = true;
                        }
                        else
                        {
                            this.LimpiarCuestionario();
                        }
                    }
                    catch (Exception exc)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error cargando el cuestionario asociado al sector.");

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Contingencias_FormularioContingencias :: cboSector_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    }
                }


            #endregion


            #region Informacion Cuestionario

                /// <summary>
                /// Evento que carga los datos de una seccion
                /// </summary>
                protected void rptCuestionario_ItemDataBound(object sender, RepeaterItemEventArgs e)
                {
                    Repeater objRepeater = null;
                    SeccionEncuestasEntity objSeccionEncuesta = null;

                    //Verificar si es item con información
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        //Obtener información de preguntas del sector
                        objSeccionEncuesta = (SeccionEncuestasEntity)e.Item.DataItem;

                        if (objSeccionEncuesta.SeccionID > 0 && objSeccionEncuesta.Preguntas != null)
                        {
                            //Cargar repeater
                            objRepeater = (Repeater)e.Item.FindControl("rptPreguntasCuestionario");

                            //Cargar la informacion de las preguntas
                            if (objRepeater != null){
                                objRepeater.DataSource = objSeccionEncuesta.Preguntas;
                                objRepeater.DataBind();
                            }
                        }
                    }
                }


                /// <summary>
                /// Formatea campos de información de la pregunta
                /// </summary>
                protected void rptPreguntasCuestionario_ItemDataBound(object sender, RepeaterItemEventArgs e)
                {
                    Repeater objRepeater = null;
                    RadioButtonList objRadios = null;
                    TextBox objTextBox = null;
                    TextBox objTextBoxMultiple = null;                    
                    PreguntaEncuestasEntity objPregunta = null;
                    DropDownList objDropDownList = null;
                    HiddenField objHiddenField = null;
                    RequiredFieldValidator objRequiredFieldValidator = null;
                    CustomValidator objCustomValidator = null;
                    FileUpload objFileUpload = null;
                    HtmlControl objTabla = null;
                    Repeater objCheckBoxList = null;

                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        //Obtener información de preguntas del sector
                        objPregunta = (PreguntaEncuestasEntity)e.Item.DataItem;

                        //Cargar las opciones que para visibilidad de preguntas
                        objHiddenField = (HiddenField)e.Item.FindControl("hdfCondicionesMostrarPregunta");
                        if (objHiddenField != null)
                        {
                            objHiddenField.Value = this.CargarOpcionesMostrarPreguntas(objPregunta.OpcionesHabilitanPregunta);
                        }

                        objHiddenField = (HiddenField)e.Item.FindControl("hdfPreguntaMostrada");
                        if (objHiddenField != null)
                        {
                            objHiddenField.ID = "hdfPreguntaMostrada" + objPregunta.PreguntaID.ToString();
                            objHiddenField.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objHiddenField = (HiddenField)e.Item.FindControl("hdfPreguntaObligatoria");
                        if (objHiddenField != null)
                        {
                            objHiddenField.ID = "hdfPreguntaObligatoria" + objPregunta.PreguntaID.ToString();
                            objHiddenField.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        //Cargar objetos
                        objRepeater = (Repeater)e.Item.FindControl("rptOpcionesPregunta");
                        objTabla = (HtmlControl)e.Item.FindControl("tblOpcionesUnicas");
                        objRadios = (RadioButtonList)e.Item.FindControl("rdOpcionUnica");
                        objTextBox = (TextBox)e.Item.FindControl("txtOpcionOtro");
                        objTextBoxMultiple = (TextBox)e.Item.FindControl("txtTextoOpcionMultiple");
                        objRadios = (RadioButtonList)e.Item.FindControl("rdOpcionUnica");
                        objCheckBoxList = (Repeater)e.Item.FindControl("rptOpcioneMultiples");

                        //Ocultar tablas
                        objTabla.Visible = false;
                        objRepeater.Visible = false;
                        objCheckBoxList.Visible = false;

                        //Cargar objetos de acuerdo a tipo de pregunta
                        if (objPregunta.PreguntaID > 0 && objPregunta.TipoPregunta.TipoPreguntaID == (int)TipoPreguntaEncuestasEnum.Seleccion_Unica && objPregunta.OpcionesPregunta != null)
                        {
                            //Cargar la informacion de las preguntas
                            if (objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio || op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList().Count > 0)
                            {
                                //Mostrar tabla
                                objTabla.Visible = true;

                                if (objRadios != null)
                                {
                                    objRadios.ID = "rdOpcionUnica" + objPregunta.PreguntaID.ToString() + "_";
                                    objRadios.DataSource = objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio || op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList();
                                    objRadios.DataTextField = "TextoOpcion";
                                    objRadios.DataValueField = "OpcionPreguntaID";
                                    objRadios.DataBind();
                                    objRadios.Visible = true;
                                }

                                if (objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList().Count > 0)
                                {
                                    if (objTextBox != null)
                                    {
                                        objTextBox.ID = "txtOpcionOtro" + objPregunta.PreguntaID.ToString() + "_" + objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList()[0].OpcionPreguntaID + "_";
                                        objTextBox.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                                        objTextBox.Visible = true;
                                        objCustomValidator = (CustomValidator)e.Item.FindControl("cvOpcionOtro");
                                        if (objCustomValidator != null && objCustomValidator.Enabled)
                                        {
                                            objCustomValidator.ID = "cvOpcionOtro" + objPregunta.PreguntaID.ToString() + "_" + objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList()[0].OpcionPreguntaID + "_";
                                            objCustomValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                                            objCustomValidator.Enabled = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (objTextBox != null)
                                    {
                                        objTextBox.Visible = false;
                                        objCustomValidator = (CustomValidator)e.Item.FindControl("cvOpcionOtro");
                                        if (objCustomValidator != null && objCustomValidator.Enabled)
                                        {
                                            objCustomValidator.Enabled = false;
                                        }
                                    }
                                }
                            }
                            else if (objRepeater != null && objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Abierto || op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Numerico).ToList().Count > 0)
                            {
                                objRepeater.Visible = true;
                                objTabla.Visible = false;
                                objRepeater.DataSource = objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Abierto || op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Numerico).ToList();
                                objRepeater.DataBind();
                            }
                        }
                        else if (objPregunta.PreguntaID > 0 && objPregunta.TipoPregunta.TipoPreguntaID == (int)TipoPreguntaEncuestasEnum.Seleccion_Multiple && objPregunta.OpcionesPregunta != null)
                        {
                            if (objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio || op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList().Count > 0)
                            {
                                if (objCheckBoxList != null)
                                {
                                    objCheckBoxList.DataSource = objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio || op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList();
                                    objCheckBoxList.DataBind();
                                    objCheckBoxList.Visible = true;
                                }

                                if (objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList().Count > 0)
                                {
                                    if (objTextBoxMultiple != null)
                                    {
                                        objTextBoxMultiple.ID = "txtTextoOpcionMultiple" + objPregunta.PreguntaID.ToString() + "_" + objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList()[0].OpcionPreguntaID + "_";
                                        objTextBoxMultiple.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                                        objTextBoxMultiple.Visible = true;
                                        objCustomValidator = (CustomValidator)e.Item.FindControl("cvTextoOpcionMultiple");
                                        if (objCustomValidator != null && objCustomValidator.Enabled)
                                        {
                                            objCustomValidator.ID = "cvTextoOpcionMultiple" + objPregunta.PreguntaID.ToString() + "_" + objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Radio_Otro).ToList()[0].OpcionPreguntaID + "_";
                                            objCustomValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                                            objCustomValidator.Enabled = true;
                                        }
                                    }
                                }

                            }
                            else if (objRepeater != null && objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Abierto || op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Numerico).ToList().Count > 0)
                            {
                                objRepeater.Visible = true;
                                objRepeater.DataSource = objPregunta.OpcionesPregunta.Where(op => op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Abierto || op.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Numerico).ToList();
                                objRepeater.DataBind();
                            }
                        }
                        else
                        {
                            //Verificar si es de ubicación
                            if (objPregunta.PreguntaID > 0 && objPregunta.TipoPregunta.TipoPreguntaID == (int)TipoPreguntaEncuestasEnum.Ubicacion_Geografica)
                            {
                                objDropDownList = (DropDownList)e.Item.FindControl("cboDepartamentoPregunta");
                                if (objDropDownList != null)
                                    this.CargarListadoDepartamentos(objDropDownList);
                                objDropDownList = (DropDownList)e.Item.FindControl("cboCiudadPregunta");
                                if (objDropDownList != null)
                                    this.CargarListadoMunicipios(objDropDownList, -1);
                            }
                        }

                        //Cargar los validadores custom
                        objCustomValidator = (CustomValidator)e.Item.FindControl("cvOpcionUnica");
                        if (objCustomValidator != null && objCustomValidator.Enabled)
                        {
                            objCustomValidator.ID = "cvOpcionUnica" + objPregunta.PreguntaID.ToString() + "_";
                            objCustomValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        //Cargar los validadores custom
                        objCustomValidator = (CustomValidator)e.Item.FindControl("cvOpcionMultiple");
                        if (objCustomValidator != null && objCustomValidator.Enabled)
                        {
                            objCustomValidator.ID = "cvOpcionMultiple" + objPregunta.PreguntaID.ToString() + "_";
                            objCustomValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        //Cargar los identificadores a los validadores
                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvRespuestaPregunta");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "RespuestaPregunta";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvDepartamentoPregunta");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "DepartamentoPregunta";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvDepartamentoPregunta");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "DepartamentoPregunta";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvCiudadPregunta");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "CiudadPregunta";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvGradosLongitud");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "GradosLongitud";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvMinutosLongitud");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "MinutosLongitud";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvSegundosLongitud");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "SegundosLongitud";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvGradosLatitud");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "GradosLatitud";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvMinutosLatitud");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "MinutosLatitud";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvSegundosLatitud");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "SegundosLatitud";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvFechaPregunta");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            objRequiredFieldValidator.ID = "rfv" + objPregunta.PreguntaID.ToString() + "_" + "FechaPregunta";
                            objRequiredFieldValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        //Cargar datos de manejo de archivos
                        objFileUpload = (FileUpload)e.Item.FindControl("fuplDocumentoPregunta");
                        if (objFileUpload != null && objFileUpload.Enabled)
                        {
                            objFileUpload.ID = "fuplDocumentoPregunta" + objPregunta.PreguntaID.ToString() + "_";
                            objFileUpload.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        objCustomValidator = (CustomValidator)e.Item.FindControl("cvDocumentoPregunta");
                        if (objCustomValidator != null && objCustomValidator.Enabled)
                        {
                            objCustomValidator.ID = "cvDocumentoPregunta" + objPregunta.PreguntaID.ToString() + "_";
                            objCustomValidator.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        }

                        
                    }
                }


                /// <summary>
                /// Formatea campos de información
                /// </summary>
                protected void rptOpcionesPregunta_ItemDataBound(object sender, RepeaterItemEventArgs e)
                {
                    TextBox objTextBox = null;
                    RequiredFieldValidator objRequiredFieldValidator = null;
                    OpcionPreguntaEncuestasEntity objOpcion = null;

                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        //Obtener información de preguntas del sector
                        objOpcion = (OpcionPreguntaEncuestasEntity)e.Item.DataItem;

                        objTextBox = (TextBox)e.Item.FindControl("txtOpcionTexto");
                        if (objTextBox != null)
                        {
                            if (objOpcion.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Abierto)
                            {
                                objTextBox.Visible = true;
                            }
                            else
                                objTextBox.Visible = false;
                        }

                        objTextBox = (TextBox)e.Item.FindControl("txtOpcionTextoNumerico");
                        if (objTextBox != null)
                        {
                            if (objOpcion.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Numerico)
                            {
                                objTextBox.Visible = true;
                            }
                            else
                                objTextBox.Visible = false;
                        }


                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvOpcionTexto");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            if (objOpcion.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Abierto)
                            {
                                objRequiredFieldValidator.ID = "rfv" + objOpcion.PreguntaID.ToString() + "_" + "OpcionTexto";
                                objRequiredFieldValidator.Enabled = true;
                            }
                            else
                                objRequiredFieldValidator.Enabled = false;
                        }

                        objRequiredFieldValidator = (RequiredFieldValidator)e.Item.FindControl("rfvOpcionTextoNumerico");
                        if (objRequiredFieldValidator != null && objRequiredFieldValidator.Enabled)
                        {
                            if (objOpcion.TipoOpcion.TipoOpcionPreguntaID == (int)TipoOpcionPreguntaEncuestasEnum.Texto_Numerico)
                            {
                                objRequiredFieldValidator.ID = "rfv" + objOpcion.PreguntaID.ToString() + "_" + "OpcionTextoNumerico";
                                objRequiredFieldValidator.Enabled = true;
                            }
                            else
                                objRequiredFieldValidator.Enabled = false;
                        }
                    }
                }


                /// <summary>
                /// Cargar el listado de departamentos
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                protected void cboDepartamentoPregunta_SelectedIndexChanged(object sender, EventArgs e)
                {
                    DropDownList objMunicipios = null;

                    try
                    {
                        //Cargar listado
                        objMunicipios = (DropDownList)this.Page.FindControl((((DropDownList)sender).UniqueID).Replace("cboDepartamentoPregunta", "cboCiudadPregunta"));

                        if (objMunicipios != null)
                        {
                            if (((DropDownList)sender).SelectedValue != "-1")
                            {
                                this.CargarListadoMunicipios(objMunicipios, Convert.ToInt32(((DropDownList)sender).SelectedValue));
                            }
                            else
                            {
                                this.CargarListadoMunicipios(objMunicipios, -1);
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error cargando los departamentos.");

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Contingencias_FormularioContingencias :: cboSector_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    }
                }


                /// <summary>
                /// Formatear datos del repeater
                /// </summary>
                protected void rptOpcioneMultiples_ItemDataBound(object sender, RepeaterItemEventArgs e)
                {
                    HtmlInputCheckBox objCheck = null;                    
                    OpcionPreguntaEncuestasEntity objOpcion = null;

                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        //Obtener información de preguntas del sector
                        objOpcion = (OpcionPreguntaEncuestasEntity)e.Item.DataItem;

                        objCheck = (HtmlInputCheckBox)e.Item.FindControl("rdOpcionMultiple");
                        if (objCheck != null)
                        {
                            objCheck.ID = "rdOpcionMultiple" + objOpcion.PreguntaID.ToString() + "_";
                        }
                    }
                }


            #endregion


            #region Almacenamiento de Datos

                /// <summary>
                /// Almaceen la informacion del cuestionario
                /// </summary>
                protected void cmdEnviar_Click(object sender, EventArgs e)
                {
                    try
                    {
                        //Guardar la información de la solicitud
                        this.GuardarInformacionCuestionario();

                        //Direccionar a pagina de respuesta
                        Response.Redirect("RespuestaContingencias.aspx", false);
                    }
                    catch (RadicacionSolicitudContingenciaException)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error registrando el reporte en VITAL. La información diligenciada <b>NO</b> fue almacenada en nuestro sistema. Por favor trate nuevamente y si el error persiste comuniquese con el administrador del sistema.", true);

                        //Limpiar formulario
                        this.LimpiarCamposFormulario();

                        //Actualizar panel
                        this.upnlFormulario.Update();
                    }
                    catch (DocumentoSolicitudContingenciaException)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento durante el proceso de almacenamiento de archivos en VITAL. Por favor comuniquese con el administrador del sistema para conocer el estado de su solicitud.", true);

                        //Limpiar formulario
                        this.LimpiarCamposFormulario();

                        //Actualizar panel
                        this.upnlFormulario.Update();
                    }
                    catch (Exception exc)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error enviando la información del reporte. La información diligenciada <b>NO</b> fue almacenada en nuestro sistema. Por favor trate nuevamente y si el error persiste comuniquese con el administrador del sistema.", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Contingencias_FormularioContingencias :: cmdEnviar_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                        //Limpiar formulario
                        this.LimpiarCamposFormulario();

                        //Actualizar panel
                        this.upnlFormulario.Update();
                    }
                }

            #endregion


        #endregion


        #region Modal Mensaje Error

                /// <summary>
            /// Evento que se encarga de cerrar modal cuando se presenta mensaje de error
            /// </summary>
            protected void cmdAceptarErrorProceso_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar formulario
                    //this.LimpiarCamposFormulario();

                    //Redireccionar a la pagina
                    Response.Redirect("FormularioContingencias.aspx", false);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensajeError("Se presento error cerrando el modal de errores.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Contingencias_FormularioContingencias :: cmdAceptarErrorProceso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


    #endregion


            
}