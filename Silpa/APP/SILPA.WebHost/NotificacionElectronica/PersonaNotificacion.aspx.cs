using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using SILPA.AccesoDatos.Notificacion;
using SILPA.LogicaNegocio.Notificacion;
using System.Data;
using SILPA.Comun;
using System.Web.UI.HtmlControls;
using System.IO;
using SILPA.LogicaNegocio.Generico;
using System.Globalization;

public partial class NotificacionElectronica_PersonaNotificacion : System.Web.UI.Page
{

    #region Objetos


        /// <summary>
        /// Identificador del solicitante
        /// </summary>
        private int SolicitanteID
        {
            get
            {
                return (int)ViewState["_intSolicitanteID"];
            }
            set
            {
                ViewState["_intSolicitanteID"] = value;
            }
        }

        /// <summary>
        /// Acciones que se pueden realizar con las personas
        /// </summary>
        private enum AccionesPersonas
        {
            Adicionar = 1,
            Editar = 2,
            Eliminar = 3
        }

        
        /// <summary>
        /// Listado de flujos
        /// </summary>
        private List<FlujoNotificacionElectronicaEntity> _objLstFlujosNotificacion
        {
            get
            {
                return (List<FlujoNotificacionElectronicaEntity>)ViewState["_objLstFlujosNotificacion"];
            }
            set
            {
                ViewState["_objLstFlujosNotificacion"] = value;
            }
        }

    #endregion

    
    #region Metodos Privados

        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.divMensaje.Visible = true;
            this.upnlMensaje.Update();
        }


        /// <summary>
        /// Ocultar los mensajes
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.divMensaje.Visible = false;
            this.upnlMensaje.Update();
        }


        /// <summary>
        /// Limpiar modal de cambio de estado
        /// </summary>
        private void LimpiarModalCambioEstadoActo()
        {
            //Limpiar desplgable
            this.cboEstadoCambiarEstadoActo.ClearSelection();
            this.cboEstadoCambiarEstadoActo.Items.Clear();

            //Limpiar mensajes
            this.lblTextoProcesosRelacionadosCambioEstado.Text = "";
            this.lblMensajeCambioEstado.Text = "";
            this.lblTextoProcesosRelacionadosCambioEstado.Visible = false;
            this.lblMensajeCambioEstado.Visible = false;

            //Limpiar grilla
            this.grdActosAdministrativosCambioEstado.DataSource = null;
            this.grdActosAdministrativosCambioEstado.DataBind();
            this.grdActosAdministrativosCambioEstado.Visible = false;

            //Limpiar hidden
            this.hdfActoIDModalCambiarEstado.Value = "";
            this.hdfEstadoIDModalCambiarEstado.Value = "";
        }


        /// <summary>
        /// Limpiar modal de cambio de estado
        /// </summary>
        private void LimpiarModalCambioConfiguracionActo()
        {
            //Limpiar campos
            this.ltlExpedienteCambiarConfiguracionActo.Text = "";
            this.ltlNumeroVitalCambiarConfiguracionActo.Text = "";
            this.ltlTipoActoCambiarConfiguracionActo.Text = "";
            this.ltlNumeroActoCambiarConfiguracionActo.Text = "";
            this.ltlFechaActoCambiarConfiguracionActo.Text = "";
            this.chkNotificarCambiarConfiguracionActo.Checked = false;
            this.chkComunicarCambiarConfiguracionActo.Checked = false;
            this.chkCumplirCambiarConfiguracionActo.Checked = false;
            this.chkPublicarCambiarConfiguracionActo.Checked = false;
            this.chkRecursoCambiarConfiguracionActo.Checked = false;           

            //Limpiar hidden
            this.hdfActoIDModalCambiarConfiguracion.Value = "";
            this.hdfPublicaModalCambiarConfiguracion.Value = "";
            this.hdfRutaModalCambiarConfiguracion.Value = "";
            this.hdfOrigenModalCambiarConfiguracion.Value = "";

            //Ocultar desplegable
            this.dvArchivoActoCambiarConfiguracionActo.Visible = false;
            this.cboArchivoActoCambiarConfiguracionActo.ClearSelection();
            this.cboArchivoActoCambiarConfiguracionActo.Items.Clear();
        }


        /// <summary>
        /// Limpiar modal de visualización de documentos 
        /// </summary>
        private void LimpiarModalVerDocumentosActo()
        {
            this.grdDocumentosActoAdministrativoVer.DataSource = null;
            this.grdDocumentosActoAdministrativoVer.DataBind();
        }


        /// <summary>
        /// Limpiar modal de visualización de documentos 
        /// </summary>
        private void LimpiarModalConfigurarPersonas()
        {
            //Limpiar grilla
            this.grdPersonasConfigurarPersonas.DataSource = null;
            this.grdPersonasConfigurarPersonas.DataBind();

            //Limpiar campos
            this.ltlTituloConfigurarPersonas.Text = "";
            this.hdfActoIDConfigurarPersonas.Value = "";
            this.hdfEstadoActoIDConfigurarPersonas.Value = "";
            this.hdfTipoNotificacionIDConfigurarPersonas.Value = "";
        }


        /// <summary>
        /// Limpia los campos del modal de agrgar editar persona
        /// </summary>
        private void LimpiarModalAgregarEditarPersona()
        {
            //LImpiar campos
            this.ltlTituloAgregarEditarUsuario.Text = "";
            this.cboTipoIdentificacionAgregarEditarUsuario.ClearSelection();
            this.cboTipoIdentificacionAgregarEditarUsuario.Items.Clear();
            this.txtNroIdentificacionAgregarEditarUsuario.Text = "";
            this.ltlUsuarioAgregarEditarUsuario.Text = "";
            this.cboTipoNotificacionAgregarEditarUsuario.ClearSelection();
            this.cboTipoNotificacionAgregarEditarUsuario.Items.Clear();
            this.cboFlujoTipoNotificacionAgregarEditarUsuario.ClearSelection();
            this.cboFlujoTipoNotificacionAgregarEditarUsuario.Items.Clear();
            this.cboEstadoFlujoAgregarEditarUsuario.ClearSelection();
            this.cboEstadoFlujoAgregarEditarUsuario.Items.Clear();
            this.txtFechaEstadoAgregarEditarUsuario.Text = "";
            this.ltlActoAgregarEditarUsuario.Text = "";
            this.ltlFechaActoAgregarEditarUsuario.Text = "";

            //Limpiar hidden
            this.hdfAccionRealizarAgregarEditarUsuario.Value = "";
            this.hdfActoIdAgregarEditarUsuario.Value = "";
            this.hdfEstadoActoIDAgregarEditarUsuario.Value = "";
            this.hdfTipoNotificacionIDAgregarEditarUsuario.Value = "";
            this.hdfPersonaIDAgregarEditarUsuario.Value = "";
            this.hdfNumeroIdentificacionAgregarEditarUsuario.Value = "";

            //Lismpiar listado
            this._objLstFlujosNotificacion = null;
        }


        /// <summary>
        /// Limpia los campos del modal de agrgar editar persona
        /// </summary>
        private void LimpiarModalEliminarPersona()
        {
            //Limpiar campos
            this.ltlUsuarioEliminarPersona.Text = "";
            this.hdfActoIdEliminarPersona.Value = "";
            this.hdfPersonaIDEliminarPersona.Value = "";
            this.hdfEstadoActoIDEliminarPersona.Value = "";
            this.hdfTipoNotificacionIDEliminarPersona.Value = "";
            this.txtMotivoEliminacionEliminarPersona.Text = "";
        }

        /// <summary>
        /// Cargar la información inicial de la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Cargar información listados
            this.ConsultaTiposActos();
            this.ConsultaEstadosActo(this.cboEstadoActoAdministrativo);

            //Inicializar fechas
            this.txtFechaDesde.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");

            //Ocultar div de consulta
            this.divActosAdministrativos.Visible = false;

            //Inicializar grilla de consulta
            this.grdActosAdministrativos.DataSource = null;
            this.grdActosAdministrativos.DataBind();

            //Limpiar modales
            this.LimpiarModalCambioEstadoActo();
            this.LimpiarModalVerDocumentosActo();
            this.LimpiarModalCambioConfiguracionActo();
            this.LimpiarModalConfigurarPersonas();
            this.LimpiarModalAgregarEditarPersona();
        }


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


        /// <summary>
        /// Obtiene los tipos de actos administrativos y los carga en el desplegable
        /// </summary>
        private void ConsultaTiposActos()
        {
            TipoDocumentoDalc objipoDocumentoDalc = null;

            try
            {
                //Cargar la información de los tipos documentales
                objipoDocumentoDalc = new TipoDocumentoDalc();
                this.cboTipoActo.DataSource = objipoDocumentoDalc.ListarTiposDeDocumentoNotificacion(null, null);
                this.cboTipoActo.DataTextField = "NOMBRE_DOCUMENTO";
                this.cboTipoActo.DataValueField = "ID";
                this.cboTipoActo.DataBind();
                this.cboTipoActo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: ConsultaTiposActos -> Error Inesperado: " + exc.StackTrace);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de tipos de actos");
            }
        }


        /// <summary>
        /// Obtiene el listado de estados que puede tomar un acto administrativo
        /// </summary>
        private void ConsultaEstadosActo(DropDownList objListado)
        {
            EstadoActo objEstadoActo = null;

            try
            {
                //Cargar la información de los tipos documentales
                objEstadoActo = new EstadoActo();
                objListado.DataSource = objEstadoActo.ObtenerListaEstados();
                objListado.DataTextField = "Estado";
                objListado.DataValueField = "EstadoID";
                objListado.DataBind();
                objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: ConsultaTiposActos -> Error Inesperado: " + exc.StackTrace);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de tipos de actos");
            }
        }


        /// <summary>
        /// Carga los triggers de los links de detalles
        /// </summary>
        private void CargarTriggerActosAdministrativos()
        {
            LinkButton objLink = null;
            ImageButton objImagen = null;

            foreach (GridViewRow objRowReporte in this.grdActosAdministrativos.Rows)
            {

                //Cargar imagen
                objImagen = (ImageButton)objRowReporte.FindControl("imgGrdDescargarDocumento");
                if (objImagen != null && objImagen.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                }

                //Cargar link
                objLink = (LinkButton)objRowReporte.FindControl("lnkGrdDescargarDocumento");
                if (objLink != null && objLink.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLink);
                }

                //Cargar link
                objLink = (LinkButton)objRowReporte.FindControl("lnkGrdNotificaciones");
                if (objLink != null && objLink.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLink);
                }

                //Cargar link
                objLink = (LinkButton)objRowReporte.FindControl("lnkGrdComunicar");
                if (objLink != null && objLink.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLink);
                }

                //Cargar link
                objLink = (LinkButton)objRowReporte.FindControl("lnkGrdCumplir");
                if (objLink != null && objLink.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLink);
                }

                objLink = (LinkButton)objRowReporte.FindControl("lnkGrdCambiarConfiguracion");
                if (objLink != null && objLink.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLink);
                }

                //Cargar link
                objLink = (LinkButton)objRowReporte.FindControl("lnkGrdCambiarEstado");
                if (objLink != null && objLink.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLink);
                }
            }
        }


        /// <summary>
        /// Carga los triggers de los links de detalles
        /// </summary>
        private void CargarTriggerDocumentosActoAdministrativo()
        {
            ImageButton objImagen = null;

            foreach (GridViewRow objRowReporte in this.grdDocumentosActoAdministrativoVer.Rows)
            {

                //Cargar imagen
                objImagen = (ImageButton)objRowReporte.FindControl("imgDescargarDocumentoActoAdmnistrativoVer");
                if (objImagen != null && objImagen.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                }                
            }
        }
    

        /// <summary>
        /// Obtener la información de los actos administrativos que cumplan con los parametros de busqueda
        /// </summary>
        private void ObtenerInformacionActosAdministrativos()
        {
            Notificacion objNotificacion = null;
            
            //Realizar la consulta de los actos administrativos
            objNotificacion = new Notificacion();
            this.grdActosAdministrativos.DataSource = objNotificacion.ConsultarInformacionActosAdministrativosPublicaciones( this.hdfNumeroVital.Value, this.hdfExpediente.Value,
                                                                                                                             this.hdfIdentificacionUsuario.Value, this.hdfUsuarioNotificar.Value,
                                                                                                                             this.hdfNumeroActo.Value, (!string.IsNullOrWhiteSpace(this.hdfTipoActo.Value) ? Convert.ToInt32(this.hdfTipoActo.Value) : 0),
                                                                                                                             (!string.IsNullOrWhiteSpace(this.hdfEstadoActoAdministrativo.Value) ? Convert.ToInt32(this.hdfEstadoActoAdministrativo.Value) : 0),
                                                                                                                             (!string.IsNullOrWhiteSpace(this.hdfFechaDesde.Value) ? Convert.ToDateTime(this.hdfFechaDesde.Value) : default(DateTime)),
                                                                                                                             (!string.IsNullOrWhiteSpace(this.hdfFechaHasta.Value) ? Convert.ToDateTime(this.hdfFechaHasta.Value) : default(DateTime)),
                                                                                                                             this.SolicitanteID);

            this.grdActosAdministrativos.DataBind();
            this.divActosAdministrativos.Visible = true;

            //Cargar triggers
            this.CargarTriggerActosAdministrativos();
        }


        /// <summary>
        /// Crear tabla de de documentos
        /// </summary>
        /// <returns>DataTable vacio</returns>
        private DataTable CrearTablaDocumentos()
        {
            DataTable objDocumentos = null;

            //CRear tabla
            objDocumentos = new DataTable();

            //Adicionar columnas
            objDocumentos.Columns.Add("NOMBRE_DOCUMENTO", Type.GetType("System.String"));
            objDocumentos.Columns.Add("RUTA_DOCUMENTO", Type.GetType("System.String"));

            return objDocumentos;
        }


        /// <summary>
        /// Obetner el listado de archivos de una carpeta y cargarlos en grilla de documentos
        /// </summary>
        /// <param name="p_strCarpeta">string con la ruta de la carpeta</param>
        /// <param name="p_objGrilla">GridView con la grilla en la cual se cargará el listado de documentos</param>
        private void CargarDocumentosCarpeta(string p_strCarpeta, GridView p_objGrilla)
        {
            DataTable objDocumentos = null;
            DataRow objDocumento = null;
            string[] strArchivos = null;

            //Verificar que la carpeta exista
            if (Directory.Exists(p_strCarpeta))
            {
                //Crear tabla de documentos
                objDocumentos = this.CrearTablaDocumentos();

                //Cargar listado de archivos
                strArchivos = Directory.GetFiles(p_strCarpeta);

                //Ciclo que carga en tabla los archivos
                foreach (string strArchivo in strArchivos)
                {
                    //Cargar datos
                    objDocumento = objDocumentos.NewRow();
                    objDocumento["NOMBRE_DOCUMENTO"] = strArchivo.Substring((strArchivo.LastIndexOf("/") != -1 ? strArchivo.LastIndexOf("/") : strArchivo.LastIndexOf("\\") + 1));
                    objDocumento["RUTA_DOCUMENTO"] = strArchivo;
                    objDocumentos.Rows.Add(objDocumento);
                }

                //Cargar documentos
                p_objGrilla.DataSource = objDocumentos;
                p_objGrilla.DataBind();
            }
            else
            {
                //Limpiar grilla
                p_objGrilla.DataSource = null;
                p_objGrilla.DataBind();
            }
        }


        /// <summary>
        /// Realizar la modificación del estado de un acto administrativo
        /// </summary>
        /// <param name="p_lngActoID">long con el identificador del acto</param>
        /// <param name="p_intEstadoActoID">int con el identificador del nuevo estado</param>
        private void ModificarEstadoActo(long p_lngActoID, int p_intEstadoActoID)
        {
            Notificacion objNotificacion = null;

            //Realizar modificación del estado
            objNotificacion = new Notificacion();
            objNotificacion.ModificarEstadoActoAdministrativo(p_lngActoID, p_intEstadoActoID, this.SolicitanteID);
        }


        /// <summary>
        /// Cargar la información de la configuración del acto administrativo
        /// </summary>
        /// <param name="p_lngActoID">long con el identificador del acto administrativo</param>
        /// <param name="p_lngPublicacionID">long con la identificación de la publicación</param>
        /// <param name="p_strOrigenDatos">string con el origen de la informacion</param>
        private void ObtenerConfiguracionActoAdministrativo(long p_lngActoID, string p_strOrigenDatos)
        {
            Notificacion objNotificacion = null;
            DataTable objConfiguracion = null;
            
            //Realizar consulta configuracion
            objNotificacion = new Notificacion();
            objConfiguracion = objNotificacion.ObtenerConfiguracionActoAdministrativo(p_lngActoID, p_strOrigenDatos, this.SolicitanteID);

            if (objConfiguracion != null && objConfiguracion.Rows.Count > 0)
            {
                this.ltlExpedienteCambiarConfiguracionActo.Text = objConfiguracion.Rows[0]["EXPEDIENTE"].ToString();
                this.ltlNumeroVitalCambiarConfiguracionActo.Text = objConfiguracion.Rows[0]["NUM_VITAL"].ToString();
                this.ltlTipoActoCambiarConfiguracionActo.Text = objConfiguracion.Rows[0]["TIPO_ACTO_ADMINISTRATIVO"].ToString();
                this.ltlNumeroActoCambiarConfiguracionActo.Text = objConfiguracion.Rows[0]["NUMERO_ACTO_ADMINISTRATIVO"].ToString();
                this.ltlFechaActoCambiarConfiguracionActo.Text = Convert.ToDateTime(objConfiguracion.Rows[0]["FECHA_ACTO_ADMINISTRATIVO"]).ToString("dd/MM/yyyy");

                //Cargar configuracion
                this.chkNotificarCambiarConfiguracionActo.Checked = Convert.ToBoolean(objConfiguracion.Rows[0]["ES_NOTIFICACION"]);
                this.chkComunicarCambiarConfiguracionActo.Checked = Convert.ToBoolean(objConfiguracion.Rows[0]["ES_COMUNICACION"]);
                this.chkCumplirCambiarConfiguracionActo.Checked = Convert.ToBoolean(objConfiguracion.Rows[0]["ES_CUMPLASE"]);
                this.chkPublicarCambiarConfiguracionActo.Checked = Convert.ToBoolean(objConfiguracion.Rows[0]["PUBLICACION"]);
                this.chkRecursoCambiarConfiguracionActo.Checked = Convert.ToBoolean(objConfiguracion.Rows[0]["APLICA_RECURSO"]);
            }
            else
            {
                throw new Exception("No se encontro información pra acto " + p_lngActoID.ToString() + " origen " + p_strOrigenDatos);
            }
        }


        /// <summary>
        /// Modificar la configuración del acto administrativo
        /// </summary>
        /// <param name="p_lngActoID">long con el identificador del acto administrativo a modificar</param>
        /// <param name="p_strOrigen">string con el origen de información inical del acto</param>
        /// <param name="p_blnEsNotificacion">bool que indica si presenta notificaciones</param>
        /// <param name="p_blnEsComunicacion">bool que indica si presenta comunicaciones</param>
        /// <param name="p_blnEsCumplase">bool que indica si el acto debe ser cumplido</param>
        /// <param name="p_blnpublica">bool que indica si el acto debe ser publicado</param>
        /// <param name="p_blnAplicaRecurso">boo que indica si el acto aplica recurso de reposición</param>
        /// <param name="p_strRutaArchivos">string con las rutas der los archivos</param>
        /// <param name="p_strNombreArchivo">string con el nombre del archivo correspondiente al acto administrativo</param>
        private void ModificarConfiguracionActo(long p_lngActoID, string p_strOrigen, bool p_blnEsNotificacion, bool p_blnEsComunicacion, bool p_blnEsCumplase, bool p_blnpublica, bool p_blnAplicaRecurso, string p_strRutaArchivos, string p_strNombreArchivo)
        {
            Notificacion objNotificacion = null;
            string strRutaActoAdministrativo = "";

            //Cargar ruta archivo a guardar
            if (p_strOrigen == "PUB" && (p_blnEsNotificacion || p_blnEsComunicacion || p_blnEsCumplase))
            {
                strRutaActoAdministrativo = p_strNombreArchivo;
            }
            else if (p_strOrigen != "PUB" && !p_blnEsNotificacion && !p_blnEsComunicacion && !p_blnEsCumplase && p_blnpublica)
            {
                strRutaActoAdministrativo = p_strRutaArchivos.Substring(0, (p_strRutaArchivos.LastIndexOf("/") != -1 ? p_strRutaArchivos.LastIndexOf("/") : p_strRutaArchivos.LastIndexOf("\\")));
            }
            else
            {
                strRutaActoAdministrativo = p_strRutaArchivos;
            }

            //Realizar modificación de la configuracion
            objNotificacion = new Notificacion();
            objNotificacion.ModificarConfiguracionActoAdministrativo(p_lngActoID, p_strOrigen, p_blnEsNotificacion, p_blnEsComunicacion, p_blnEsCumplase, p_blnpublica, p_blnAplicaRecurso, strRutaActoAdministrativo, this.SolicitanteID);
        }


        /// <summary>
        /// Cargar el listado de archivos en desplegable indicado
        /// </summary>
        /// <param name="p_strRutaARchivos">string con la ruta donde se encuentran los archivos</param>
        /// <param name="p_objListado">DropDownList con desplegable en el cual se cargara la información</param>
        private void CargarListadoArchivos(string p_strRutaArchivos, DropDownList p_objListado)
        {
            string[] strLstArchivos = null;


            //Limpiar listado
            p_objListado.ClearSelection();
            p_objListado.Items.Clear();

            //Validar si directorio existe
            //Verificar que la carpeta exista
            if (Directory.Exists(p_strRutaArchivos))
            {
                //Cargar listado de archivos
                strLstArchivos = Directory.GetFiles(p_strRutaArchivos);

                //Ciclo que carga en tabla los archivos
                foreach (string strArchivo in strLstArchivos)
                {
                    //Cargar datos
                    p_objListado.Items.Add(new ListItem(strArchivo.Substring((strArchivo.LastIndexOf("/") != -1 ? strArchivo.LastIndexOf("/") : strArchivo.LastIndexOf("\\") + 1)), strArchivo));
                }
            }

            //Adicionar opción de seleccione
            p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }


        /// <summary>
        /// Carga los triggers de los links y detalles
        /// </summary>
        private void CargarTriggerConfigurarPersonas()
        {
            LinkButton objLink = null;
            CheckBox objCheck = null;

            foreach (GridViewRow objRowPersona in this.grdPersonasConfigurarPersonas.Rows)
            {

                //Cargar link
                objLink = (LinkButton)objRowPersona.FindControl("lnkEditarGrdPersonas");
                if (objLink != null && objLink.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLink);
                }

                //Cargar link
                objLink = (LinkButton)objRowPersona.FindControl("lnkEliminarGrdPersonas");
                if (objLink != null && objLink.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLink);
                }

                //Cargar check
                objCheck = (CheckBox)objRowPersona.FindControl("chkVerificadoGrdPersonas");
                if (objCheck != null && objCheck.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objCheck);
                }
            }
        }


        /// <summary>
        /// Cargar la información de personas del acto administrativo
        /// </summary>
        /// <param name="p_lngActoID">long con la información del acto administrativo</param>
        /// <param name="p_intEstadoActo">int con el id del estado del acto administrativo</param>
        /// <param name="p_intTipoNotificacion">int con el tipo de notificación que manejaran los usuarios</param>
        private void CargarDatosModalConfigurarPersonas(long p_lngActoID, int p_intEstadoActo, int p_intTipoNotificacion)
        {
            PersonaNotificar objPersonaNotificar = null;

            //Limpiar modal
            this.LimpiarModalConfigurarPersonas();

            //Cargar datos
            this.hdfActoIDConfigurarPersonas.Value = p_lngActoID.ToString();
            this.hdfEstadoActoIDConfigurarPersonas.Value = p_intEstadoActo.ToString();
            this.hdfTipoNotificacionIDConfigurarPersonas.Value = p_intTipoNotificacion.ToString();

            //De acurdo al tipo mostrar título
            if (p_intTipoNotificacion == (int)TipoNotificacion.NOTIFICACION)
            {
                this.ltlTituloConfigurarPersonas.Text = "CONFIGURAR PERSONAS NOTIFICAR";
            }
            else if (p_intTipoNotificacion == (int)TipoNotificacion.COMUNICACION)
            {
                this.ltlTituloConfigurarPersonas.Text = "CONFIGURAR PERSONAS COMUNICAR";
            }
            else if (p_intTipoNotificacion == (int)TipoNotificacion.CUMPLASE)
            {
                this.ltlTituloConfigurarPersonas.Text = "CONFIGURAR PERSONAS CUMPLIR";
            }

            //Si esta finalizado se oculta botones
            if (p_intEstadoActo == (int)NOTEstadosActo.Verificado_Liberado)
            {
                this.cmdAdicionarConfigurarPersonas.Visible = false;
                this.grdPersonasConfigurarPersonas.Columns[6].Visible = false;
                this.grdPersonasConfigurarPersonas.Columns[7].Visible = false;
                this.grdPersonasConfigurarPersonas.Columns[8].Visible = false;
            }
            else
            {
                this.cmdAdicionarConfigurarPersonas.Visible = true;
                this.grdPersonasConfigurarPersonas.Columns[6].Visible = true;
                this.grdPersonasConfigurarPersonas.Columns[7].Visible = true;
                this.grdPersonasConfigurarPersonas.Columns[8].Visible = true;
            }

            //Consultar las personas notificar
            objPersonaNotificar = new PersonaNotificar();
            this.grdPersonasConfigurarPersonas.DataSource = objPersonaNotificar.ObtenerListadoPersonasNotificarActoAdmin(p_lngActoID, p_intTipoNotificacion);
            this.grdPersonasConfigurarPersonas.DataBind();

            //Cargar triggers
            this.CargarTriggerConfigurarPersonas();
        }


        /// <summary>
        /// Modificar el estado de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_intEstadoPersonaID">int con el nuevom estado de la persona</param>
        private void ModificarEstadoPersona(long p_lngPersonaID, int p_intEstadoPersonaID)
        {
            PersonaNotificar objPersonaNotificar = null;

            //Modificar el estado
            objPersonaNotificar = new PersonaNotificar();
            objPersonaNotificar.ModificarEstadoPersona(p_lngPersonaID, p_intEstadoPersonaID);
        }


        /// <summary>
        /// Cargar el listado de tipos de identificación
        /// </summary>
        /// <param name="p_objListado">DropDownList con el listado en el cual se cargaran los tipos de identificación</param>
        private void CargarTiposIdentificacion(DropDownList p_objListado)
        {
            Listas objListaTiposId = null;
            DataSet objTiposIdentificacion = null;

            //Consultar listado de tipos de identificación
            objListaTiposId = new Listas();
            objTiposIdentificacion = objListaTiposId.ListaTipoIdentificacion();

            p_objListado.DataSource = objTiposIdentificacion.Tables[0];
            p_objListado.DataValueField = "TID_ID";
            p_objListado.DataTextField = "TID_NOMBRE";
            p_objListado.DataBind();
            p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }


        /// <summary>
        /// Cargar el listado de tipos de notificacion
        /// </summary>
        /// <param name="p_objListado">DropDownList con el listado en el cual se cargaran los tipos de notificacion</param>
        /// <param name="p_lngActoID">long con el identificador del acto administrativo</param>
        private void CargarTiposNotificacion(DropDownList p_objListado, long p_lngActoID, string p_strOrigen)
        {
            Notificacion objNotificacion = null;
            List<TipoNotificacionEntity> listadoTiposNotificacion = null;
            DataTable objDatosConfiguracion = null;

            //Cargar tipos notificacion
            objNotificacion = new Notificacion();
            listadoTiposNotificacion = objNotificacion.ListarTiposNotificacion();

            //Cargar configuracion acto administrativo
            objDatosConfiguracion = objNotificacion.ObtenerConfiguracionActoAdministrativo(p_lngActoID, p_strOrigen, this.SolicitanteID);

            //Ciclo que carga las opciones
            foreach (TipoNotificacionEntity objTipoNotificacion in listadoTiposNotificacion)
            {
                //Validar la opción a ingresar
                if ( (objTipoNotificacion.TipoNotificacionID == (int)TipoNotificacion.NOTIFICACION && Convert.ToBoolean(objDatosConfiguracion.Rows[0]["ES_NOTIFICACION"]) ) ||
                     (objTipoNotificacion.TipoNotificacionID == (int)TipoNotificacion.COMUNICACION && Convert.ToBoolean(objDatosConfiguracion.Rows[0]["ES_COMUNICACION"])) ||
                    (objTipoNotificacion.TipoNotificacionID == (int)TipoNotificacion.CUMPLASE && Convert.ToBoolean(objDatosConfiguracion.Rows[0]["ES_CUMPLASE"])))
                {
                    p_objListado.Items.Add(new ListItem(objTipoNotificacion.TipoNotificacion, objTipoNotificacion.TipoNotificacionID.ToString()));
                }
            }

            //Adicionar opción de insertar
            p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }

        
        /// <summary>
        /// Cargar listado de tipos de notificación
        /// </summary>
        /// <param name="p_objListado">DropDownList con el listado en el cual se cargaran los flujos</param>
        /// <param name="p_intTipoNotificacion">int con el tipo de notificacion</param>
		/// <param name="p_intFlujoID">int con el identificador del flujo. Opcional</param>
        private void CargarListadoFlujos(DropDownList p_objListado, long p_lngActoID, int p_intTipoNotificacion, int p_intFlujoID = -1)
        {
            Notificacion objNotificacion = null;
            PersonaDalc objPersona = null;
            int intAutID = 0;

            //Obtener autoridad
            objPersona = new PersonaDalc();
            objPersona.ObtenerAutoridadPorPersona(this.SolicitanteID, out intAutID);

            //Consultar flujos
            objNotificacion = new Notificacion();
            this._objLstFlujosNotificacion = objNotificacion.ConsulaFlujosNotificacionElectronica(p_lngActoID, p_intTipoNotificacion, intAutID);

            //Cargar estados
            p_objListado.DataSource = this._objLstFlujosNotificacion.Select(x => new { x.FlujoNotificacionElectronica, x.FlujoNotificacionElectronicaID }).Distinct();
            p_objListado.DataValueField = "FlujoNotificacionElectronicaID";
            p_objListado.DataTextField = "FlujoNotificacionElectronica";
            p_objListado.DataBind();
            p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            //Verificar si se especifico id de flujo
            if (p_intFlujoID > 0 && this._objLstFlujosNotificacion.Where(flujo => flujo.FlujoNotificacionElectronicaID == p_intFlujoID).ToList().Count > 0)
            {
                p_objListado.SelectedValue = p_intFlujoID.ToString();
            }

        }

        /// <summary>
        /// Cargar la información del modal para ingresar o editar una persona
        /// </summary>
        /// <param name="p_objAccionRealizar">AccionesPersonas con la acción que se debe realizar</param>
        /// <param name="p_lngActoId">long con el identificador del acto administrativo</param>
        /// <param name="p_intEstadoActoID">int con el estado del acto administrativo</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona. Solo para edición</param>
        private void CargarDatosModalAgregarEditarPersona(AccionesPersonas p_objAccionRealizar, long p_lngActoId, int p_intEstadoActoID, int p_intTipoNotificacionID, long p_lngPersonaID = 0)
        {
            Notificacion objNotificacion = null;
            NotificacionEntity objNotificacionEntity = null;
            PersonaNotificar objPersonaNotificar = null;
            DataTable objDatosPersona = null;

            //Consultar el listado de personas del acto administrativo
            objNotificacion = new Notificacion();
            objNotificacionEntity = objNotificacion.ConsultarInformacionActo(p_lngActoId);

            //Cargar datos acto
            this.ltlActoAgregarEditarUsuario.Text = objNotificacionEntity.NumeroActoAdministrativo;
            this.ltlFechaActoAgregarEditarUsuario.Text = objNotificacionEntity.FechaActo.Value.ToString("dd/MM/yyyy HH:mm");            
            
            //Cargar los listados default
            this.CargarTiposIdentificacion(this.cboTipoIdentificacionAgregarEditarUsuario);
            this.CargarTiposNotificacion(this.cboTipoNotificacionAgregarEditarUsuario, p_lngActoId, "NOT");

            //Cargar datos basicos
            this.hdfAccionRealizarAgregarEditarUsuario.Value = ((int)p_objAccionRealizar).ToString();
            this.hdfActoIdAgregarEditarUsuario.Value = p_lngActoId.ToString();
            this.hdfEstadoActoIDAgregarEditarUsuario.Value = p_intEstadoActoID.ToString();
            this.hdfTipoNotificacionIDAgregarEditarUsuario.Value = p_intTipoNotificacionID.ToString();
            this.hdfPersonaIDAgregarEditarUsuario.Value = p_lngPersonaID.ToString();

            //Cargar datos para insertar /editar
            if (p_objAccionRealizar == AccionesPersonas.Adicionar)
            {
                //Cargar titulo
                this.ltlTituloAgregarEditarUsuario.Text = "ADICIONAR PERSONA";

                //Seleccionar tipo notificación
                this.cboTipoNotificacionAgregarEditarUsuario.SelectedValue = p_intTipoNotificacionID.ToString();
                this.cboTipoNotificacionAgregarEditarUsuario.Enabled = false;

                //Cargar el listado de flujos
                this.CargarListadoFlujos(this.cboFlujoTipoNotificacionAgregarEditarUsuario, p_lngActoId, p_intTipoNotificacionID);

                //Cargar listados vacios
                this.cboEstadoFlujoAgregarEditarUsuario.ClearSelection();
                this.cboEstadoFlujoAgregarEditarUsuario.Items.Clear();
                this.cboEstadoFlujoAgregarEditarUsuario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            else if (p_objAccionRealizar == AccionesPersonas.Editar)
            {
                //Cargar titulo
                this.ltlTituloAgregarEditarUsuario.Text = "EDITAR PERSONA";

                //Seleccionar tipo notificación
                this.cboTipoNotificacionAgregarEditarUsuario.SelectedValue = p_intTipoNotificacionID.ToString();                

                //Consultar información de persona
                objPersonaNotificar = new PersonaNotificar();
                objDatosPersona = objPersonaNotificar.ObtenerListadoPersonasNotificarActoAdmin(p_lngActoId, p_intTipoNotificacionID, p_lngPersonaID);

                //Seleccionar tipo de identificacion
                this.cboTipoIdentificacionAgregarEditarUsuario.SelectedValue = objDatosPersona.Rows[0]["ID_TIPO_IDENTIFICACION"].ToString();

                //Cargar número de identificación
                this.txtNroIdentificacionAgregarEditarUsuario.Text = objDatosPersona.Rows[0]["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString();
                this.hdfNumeroIdentificacionAgregarEditarUsuario.Value = objDatosPersona.Rows[0]["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString();

                //Cargar nombre de usuario
                this.ltlUsuarioAgregarEditarUsuario.Text = objDatosPersona.Rows[0]["USUARIO_NOTIFICAR"].ToString();

                //Cargar el listado de flujos
                this.CargarListadoFlujos(this.cboFlujoTipoNotificacionAgregarEditarUsuario, p_lngActoId, p_intTipoNotificacionID);

                //Seleccionar flujo
                this.cboFlujoTipoNotificacionAgregarEditarUsuario.SelectedValue = objDatosPersona.Rows[0]["FLUJO_ID"].ToString();

                //Cargar estados
                this.cboFlujoTipoNotificacionAgregarEditarUsuario_SelectedIndexChanged(null, null);

                //Verificar si el estado actual es el mismo que el inicial
                if (this.cboFlujoTipoNotificacionAgregarEditarUsuario.SelectedValue != "-1")
                {
                    if (objDatosPersona.Rows[0]["ID_ESTADO_INICIAL"].ToString() == objDatosPersona.Rows[0]["ID_ESTADO_ACTUAL"].ToString())
                    {
                        //Cargar estado
                        this.cboEstadoFlujoAgregarEditarUsuario.SelectedValue = objDatosPersona.Rows[0]["ID_ESTADO_INICIAL"].ToString();

                        //Cargar flecha
                        this.txtFechaEstadoAgregarEditarUsuario.Text = Convert.ToDateTime(objDatosPersona.Rows[0]["FECHA_ESTADO_INICIAL"]).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        //Agrgar estado actual
                        this.cboEstadoFlujoAgregarEditarUsuario.Items.Add(new ListItem(objDatosPersona.Rows[0]["ESTADO_ACTUAL"].ToString(), objDatosPersona.Rows[0]["ID_ESTADO_ACTUAL"].ToString()));

                        //Cargar estado
                        this.cboEstadoFlujoAgregarEditarUsuario.SelectedValue = objDatosPersona.Rows[0]["ID_ESTADO_ACTUAL"].ToString();

                        //Cargar flecha
                        this.txtFechaEstadoAgregarEditarUsuario.Text = Convert.ToDateTime(objDatosPersona.Rows[0]["FECHA_ESTADO_ACTUAL"]).ToString("dd/MM/yyyy HH:mm");
                    }

                }

                //Verificar si comenzo proceso
                if (!Convert.ToBoolean(objDatosPersona.Rows[0]["COMENZO_PROCESO"]) && Convert.ToInt32(objDatosPersona.Rows[0]["ID_ESTADO_PERSONA_NOTIFICAR"]) == (int)NOTEstadosActoPersona.Sin_Verificar)  
                {
                    this.cboTipoNotificacionAgregarEditarUsuario.Enabled = true;
                    this.cboFlujoTipoNotificacionAgregarEditarUsuario.Enabled = true;
                    this.cboEstadoFlujoAgregarEditarUsuario.Enabled = true;
                    this.txtFechaEstadoAgregarEditarUsuario.Enabled = true;                    
                }
                else
                {
                    this.cboTipoNotificacionAgregarEditarUsuario.Enabled = false;
                    this.cboFlujoTipoNotificacionAgregarEditarUsuario.Enabled = false;
                    this.cboEstadoFlujoAgregarEditarUsuario.Enabled = false;
                    this.txtFechaEstadoAgregarEditarUsuario.Enabled = false;
                }
            }
        }


        /// <summary>
        /// Validar la existencia de usuario y cargar sus datos
        /// </summary>
        /// <param name="p_intTipoIdentificacion">int con el tipo de identificación</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        private void ValidarExistenciaUsuario(int p_intTipoIdentificacion, string p_strNumeroIdentificacion)
        {
            PersonaDalc objPersona = null;
            DataTable objDatosPersonas = null;

            //Consultar datos persona
            objPersona = new PersonaDalc();
            objDatosPersonas = objPersona.ConsultaPersonaByNumeroIdentificacionSILAVITAL(p_strNumeroIdentificacion, p_intTipoIdentificacion);

            //Validar que existan datos
            if (objDatosPersonas != null && objDatosPersonas.Rows.Count > 0)
            {
                if (p_intTipoIdentificacion == 1)
                    this.ltlUsuarioAgregarEditarUsuario.Text = string.Format("{0} {1} {2} {3}", objDatosPersonas.Rows[0]["PER_PRIMER_NOMBRE"], objDatosPersonas.Rows[0]["PER_SEGUNDO_NOMBRE"], objDatosPersonas.Rows[0]["PER_PRIMER_APELLIDO"], objDatosPersonas.Rows[0]["PER_SEGUNDO_APELLIDO"]);
                else
                    this.ltlUsuarioAgregarEditarUsuario.Text = objDatosPersonas.Rows[0]["PER_RAZON_SOCIAL"].ToString();
                this.hdfNumeroIdentificacionAgregarEditarUsuario.Value = p_strNumeroIdentificacion;
            }
            else
            {
                this.ltlUsuarioAgregarEditarUsuario.Text = string.Empty;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- No se encontro persona especificada')", true);
                this.hdfNumeroIdentificacionAgregarEditarUsuario.Value = "";
            }
        }


        /// <summary>
        /// Crea una nueva persona
        /// </summary>
        /// <param name="p_lngActoID">long con el acto administrativo donde se ingresará al usuario</param>
        /// <param name="p_intTipoIdentificacion">int con el tipo de identificación</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_strNombrePersona">string con el nombre de la persona</param>
        /// <param name="p_intTipoNotificacion">int con el tipo de notificación a efectuar</param>
        /// <param name="p_intFlujoID">int con el flujo al cual pertenece la persona</param>
        /// <param name="p_intEstadoID">int con el identificador del estado inicial del usuario</param>
        /// <param name="p_objFechaEstado">DateTime con la fecha del estado</param>
        private void CrearPersona(long p_lngActoID, int p_intTipoIdentificacion, string p_strNumeroIdentificacion, string p_strNombrePersona, 
                                  int p_intTipoNotificacion, int p_intFlujoID, int p_intEstadoID, DateTime p_objFechaEstado)
        {
            PersonaNotificarEntity objPersonaNotificarEntity = null;
            Notificacion objNotificacion = null;


            //Cargar datos de la persona
            objPersonaNotificarEntity = new PersonaNotificarEntity();
            objPersonaNotificarEntity.IdActoNotificar = Convert.ToDecimal(p_lngActoID);
            objPersonaNotificarEntity.TipoIdentificacion = new TipoIdentificacionEntity { Id = p_intTipoIdentificacion };
            objPersonaNotificarEntity.NumeroIdentificacion = p_strNumeroIdentificacion;            
            objPersonaNotificarEntity.TipoPersona = new TipoPersonaEntity { ID = 0 };
            objPersonaNotificarEntity.NumeroNIT = 0;
            objPersonaNotificarEntity.DigitoVerificacionNIT = 0;
            if (p_intTipoIdentificacion == (int)TipoIdentificacion.Nit)
            {
                objPersonaNotificarEntity.RazonSocial = p_strNombrePersona.Trim().ToUpper();
            }
            else
            {
                string[] nombreCompleto = p_strNombrePersona.Trim().ToUpper().Split(' ');
                objPersonaNotificarEntity.PrimerNombre = nombreCompleto.Length > 0 ? nombreCompleto[0] : "";
                objPersonaNotificarEntity.SegundoNombre = nombreCompleto.Length > 1 ? nombreCompleto[1] : "";
                objPersonaNotificarEntity.PrimerApellido = nombreCompleto.Length > 2 ? nombreCompleto[2] : "";
                objPersonaNotificarEntity.SegundoApellido = nombreCompleto.Length > 3 ? nombreCompleto[3] : "";                
            }
            objPersonaNotificarEntity.EstadoNotificado = new EstadoNotificacionEntity { ID = p_intEstadoID };
            objPersonaNotificarEntity.FechaNotificado = p_objFechaEstado;
            objPersonaNotificarEntity.EsNotificacionElectronica = false;
            objPersonaNotificarEntity.TipoNotificacionId = p_intTipoNotificacion;
            objPersonaNotificarEntity.FlujoNotificacionId = p_intFlujoID;
            objPersonaNotificarEntity.EstadoPersonaID = (int)NOTEstadosActoPersona.Sin_Verificar;

            //Crear persona
            objNotificacion = new Notificacion();
            objNotificacion.AgregarPersonaProcesoNotificacion(objPersonaNotificarEntity);
        }



        /// <summary>
        /// Modificar la información de una persona
        /// </summary>
        /// <param name="p_lngActoID">long con el acto administrativo donde se ingresará al usuario</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_intTipoIdentificacion">int con el tipo de identificación</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_strNombrePersona">string con el nombre de la persona</param>
        /// <param name="p_intTipoNotificacion">int con el tipo de notificación a efectuar</param>
        /// <param name="p_intFlujoID">int con el flujo al cual pertenece la persona</param>
        /// <param name="p_intEstadoID">int con el identificador del estado inicial del usuario</param>
        /// <param name="p_objFechaEstado">DateTime con la fecha del estado</param>
        private void EditarPersona(long p_lngActoID, long p_lngPersonaID, int p_intTipoIdentificacion, string p_strNumeroIdentificacion, string p_strNombrePersona,
                                  int p_intTipoNotificacion, int p_intFlujoID, int p_intEstadoID, DateTime p_objFechaEstado)
        {
            PersonaNotificarEntity objPersonaNotificarEntity = null;
            Notificacion objNotificacion = null;


            //Cargar datos de la persona
            objPersonaNotificarEntity = new PersonaNotificarEntity();
            objPersonaNotificarEntity.IdActoNotificar = Convert.ToDecimal(p_lngActoID);
            objPersonaNotificarEntity.Id = Convert.ToDecimal(p_lngPersonaID);
            objPersonaNotificarEntity.TipoIdentificacion = new TipoIdentificacionEntity { Id = p_intTipoIdentificacion };
            objPersonaNotificarEntity.NumeroIdentificacion = p_strNumeroIdentificacion;
            objPersonaNotificarEntity.TipoPersona = new TipoPersonaEntity { ID = 0 };
            objPersonaNotificarEntity.NumeroNIT = 0;
            objPersonaNotificarEntity.DigitoVerificacionNIT = 0;
            if (p_intTipoIdentificacion == (int)TipoIdentificacion.Nit)
            {
                objPersonaNotificarEntity.RazonSocial = p_strNombrePersona.Trim().ToUpper();
            }
            else
            {
                string[] nombreCompleto = p_strNombrePersona.Trim().ToUpper().Split(' ');
                objPersonaNotificarEntity.PrimerNombre = nombreCompleto.Length > 0 ? nombreCompleto[0] : "";
                objPersonaNotificarEntity.SegundoNombre = nombreCompleto.Length > 1 ? nombreCompleto[1] : "";
                objPersonaNotificarEntity.PrimerApellido = nombreCompleto.Length > 2 ? nombreCompleto[2] : "";
                objPersonaNotificarEntity.SegundoApellido = nombreCompleto.Length > 3 ? nombreCompleto[3] : "";
            }
            objPersonaNotificarEntity.EstadoNotificado = new EstadoNotificacionEntity { ID = p_intEstadoID };
            objPersonaNotificarEntity.FechaNotificado = p_objFechaEstado;
            objPersonaNotificarEntity.EsNotificacionElectronica = false;
            objPersonaNotificarEntity.TipoNotificacionId = p_intTipoNotificacion;
            objPersonaNotificarEntity.FlujoNotificacionId = p_intFlujoID;

            //Actualizar persona
            objNotificacion = new Notificacion();
            objNotificacion.ActualizarPersonaProcesoNotificacion(objPersonaNotificarEntity);
        }


        /// <summary>
        /// Modificar la información de una persona
        /// </summary>
        /// <param name="p_lngActoID">long con el acto administrativo donde se ingresará al usuario</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_strMotivoEliminacion">string con el motivo de liminacion</param>
        private void EliminarPersona(long p_lngActoID, long p_lngPersonaID, string p_strMotivoEliminacion)
        {
            Notificacion objNotificacion = null;

            //Actualizar persona
            objNotificacion = new Notificacion();
            objNotificacion.EliminarPersonaProcesoNoticiacion(p_lngActoID, p_lngPersonaID, p_strMotivoEliminacion, this.SolicitanteID.ToString());
        }


        /// <summary>
        /// Retorna una tabla con la información de los procesos relacionados
        /// </summary>
        /// <param name="objLstProcesoRelacionados">List con la información de los procesos relacionados</param>
        /// <returns>string con la información de reasignaciones</returns>
        private string CrearTablaProcesosAsociados(List<NotificacionEntity> objLstProcesoRelacionados)
        {
            string strProcesosRelacionados = "";

            //Cargar encabezado tabla
            strProcesosRelacionados = "<table class='TablaBurbujaNotificacion'>";
            strProcesosRelacionados += "<tr class='TituloTablaBurbujaNotificacion'><td>Número VITAL</td><td>Expediente</td><td>Número Acto Administrativo</td><td>Fecha Acto Administrativo</td></tr>";

            //Verificar si se obtuvo datos
            if (objLstProcesoRelacionados != null && objLstProcesoRelacionados.Count > 0)
            {
                foreach (NotificacionEntity objProceso in objLstProcesoRelacionados)
                {
                    strProcesosRelacionados += "<tr class='ContenidoTablaBurbujaNotificacion'><td style='text-align: center'>" + (!string.IsNullOrWhiteSpace(objProceso.NumeroSILPA) ? objProceso.NumeroSILPA : "-") + "</td><td style='text-align: center'>" + (!string.IsNullOrWhiteSpace(objProceso.ProcesoAdministracion) ? objProceso.ProcesoAdministracion : "-") + "</td><td style='text-align: center'>" + objProceso.NumeroActoAdministrativo + "</td><td style='text-align: center'>" + (objProceso.FechaActo != null && objProceso.FechaActo != default(DateTime) ? objProceso.FechaActo.Value.ToString("dd/MM/yyyy") : "-") + "</td></tr>";
                }
            }
            else
            {
                strProcesosRelacionados += "<tr class='ContenidoTablaBurbujaNotificacion'><td colspan='3'>No se encuentra información de procesos pendientes de finalizar proceso de notificación</td></tr>";
            }

            //Cerrar tabla
            strProcesosRelacionados += "</table>";

            return strProcesosRelacionados;
        }


        /// <summary>
        /// Obtiene HTML con información relacionada al acto administrativo
        /// </summary>
        /// <param name="p_decActoID">decimal con el identifcador del acto administrativo</param>
        /// <param name="p_strNumeroDocumento">string con el número de documento</param>
        /// <param name="p_strCodigoExpediente">string con el código del expediente</param>
        /// <returns>string con la html de información de información relacionada al acto administrativo</returns>
        private string CrearTablasInformativasActoAdministrativo(decimal p_decActoID, string p_strNumeroDocumento, string p_strCodigoExpediente)
        {
            Notificacion objNotificacion = null;
            List<NotificacionEntity> objLstProcesoRelacionados = null;         
            string strInformacion = "";

            //Crear objeto de consultas de notificaciones
            objNotificacion = new Notificacion();

            //Obtener información de actos administrativos asociados
            objLstProcesoRelacionados = objNotificacion.ObtenerProcesosAsociadosNoNotificados(p_decActoID);

            strInformacion = "<table class='TablaBurbujaNotificacion'>";

            //Generar HTML de procesos
            if (objLstProcesoRelacionados != null && objLstProcesoRelacionados.Count > 0)
            {
                strInformacion += "<tr class='TituloSeccionBurbujaNotificacion'><td>Procesos de Notificación <b>NO</b> finalizados:</td></tr>" +
                                  "<tr><td>" + CrearTablaProcesosAsociados(objLstProcesoRelacionados) + "</td></tr>";
            }

            strInformacion += "</table>";

            return strInformacion;
        }

    #endregion


    #region Eventos


        #region Page

            /// <summary>
            /// Evento que se ejecuta al realizar el cargue de la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                //this.SolicitanteID = 434;

                if (!IsPostBack)
                {
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

        #endregion


        #region cmdBuscar

            /// <summary>
            /// Evento que realiza la busqueda de información de los actos administrativos
            /// </summary>
            protected void cmdBuscar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cargar infromacion
                    this.hdfNumeroVital.Value = this.txtNumeroVital.Text.Trim();
                    this.hdfExpediente.Value = this.txtExpediente.Text.Trim();
                    this.hdfIdentificacionUsuario.Value = this.txtIdentificacionUsuario.Text.Trim();
                    this.hdfUsuarioNotificar.Value = this.txtUsuarioNotificar.Text.Trim();
                    this.hdfNumeroActo.Value =  this.txtNumeroActo.Text.Trim();
                    this.hdfTipoActo.Value = this.cboTipoActo.SelectedValue;
                    this.hdfEstadoActoAdministrativo.Value = this.cboEstadoActoAdministrativo.SelectedValue;
                    this.hdfFechaDesde.Value = this.txtFechaDesde.Text.Trim();
                    this.hdfFechaHasta.Value = this.txtFechaHasta.Text.Trim();

                    //Realizar la busqueda
                    this.grdActosAdministrativos.PageIndex = 0;
                    this.ObtenerInformacionActosAdministrativos();

                    //Actualizar panel
                    this.upnlConsultaActosAdministrativos.Update();
                }
                catch(Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdBuscar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la busqueda de la informaciòn de los actos administrativos");
                }

            }

        #endregion


        #region grdActosAdministrativos

            /// <summary>
            /// Evento que se ejecuta al cargar la información en la grilla
            /// </summary>
            protected void grdActosAdministrativos_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                Notificacion objNotificacion = null;
                DataRowView objFila = null;
                LinkButton objLinkDetalle = null;
                Image objImagenGris = null;
                Image objImagen = null;
                Literal objLiteral = null;
                HyperLink objHyperLink = null;
                bool blnProcesosPendientesNotificacion = false;

                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Cargar datos
                        objFila = (DataRowView)e.Row.DataItem;

                        //Cargar si existen proceso pendientes de notificación anteriores
                        objNotificacion = new Notificacion();
                        blnProcesosPendientesNotificacion = objNotificacion.TieneProcesosAsociadosNoNotificados(Convert.ToDecimal(objFila["ID_ACTO_NOTIFICACION"]));

                        //Cambiar datos de reasignaciones realizadas
                        objHyperLink = (HyperLink)e.Row.FindControl("lnkGrdExpediente");
                        objLiteral = (Literal)e.Row.FindControl("ltlGrdExpediente");
                        if (objHyperLink != null && (Convert.ToBoolean(objFila["CAMBIO_EXPEDIENTE"]) || blnProcesosPendientesNotificacion))
                        {
                            objHyperLink.Attributes.Add("title", this.CrearTablasInformativasActoAdministrativo(Convert.ToDecimal(objFila["ID_ACTO_NOTIFICACION"]), objFila["NUMERO_ACTO_ADMINISTRATIVO"].ToString(), objFila["EXPEDIENTE"].ToString()));
                            objHyperLink.Visible = true;
                            objLiteral.Visible = false;
                        }
                        else if (objHyperLink != null)
                        {
                            objHyperLink.Attributes.Add("title", "");
                            objHyperLink.Visible = false;
                            objLiteral.Visible = true;
                        }

                        //Cargar controles de notificacion
                        objLinkDetalle = (LinkButton)e.Row.FindControl("lnkGrdNotificaciones");
                        objImagenGris = (Image)e.Row.FindControl("imgGrdNotificarGris");

                        //Mostrar los botones que correspondan a notificaciones
                        if (Convert.ToBoolean(objFila["ES_NOTIFICACION"]) && !Convert.ToBoolean(objFila["ES_CUMPLASE"]))
                        {
                            if (objLinkDetalle != null)
                            {
                                objLinkDetalle.Visible = true;
                            }

                            if (objImagenGris != null)
                                objImagenGris.Visible = false;
                        }
                        else
                        {
                            if (objLinkDetalle != null)
                                objLinkDetalle.Visible = false;
                            if (objImagenGris != null)
                                objImagenGris.Visible = true;
                        }

                        //Cargar controles de comunicar
                        objLinkDetalle = (LinkButton)e.Row.FindControl("lnkGrdComunicar");
                        objImagenGris = (Image)e.Row.FindControl("imgGrdComunicarGris");                        

                        //Mostrar los botones que correspondan a comunicar
                        if (Convert.ToBoolean(objFila["ES_COMUNICACION"]))
                        {
                            if (objLinkDetalle != null)
                            {
                                objLinkDetalle.Visible = true;
                            }
                            if (objImagenGris != null)
                                objImagenGris.Visible = false;
                        }
                        else
                        {
                            if (objLinkDetalle != null)
                                objLinkDetalle.Visible = false;
                            if (objImagenGris != null)
                                objImagenGris.Visible = true;
                        }

                        //Cargar controles de cumplase
                        objLinkDetalle = (LinkButton)e.Row.FindControl("lnkGrdCumplir");
                        objImagenGris = (Image)e.Row.FindControl("imgGrdCumplirGris");
                        objImagen = (Image)e.Row.FindControl("imgGrdCumplirNotificar");
                        objImagen.Visible = false;

                        //Mostrar los botones que correspondan a cumplir
                        if (Convert.ToBoolean(objFila["ES_CUMPLASE"]))
                        {
                            if (objLinkDetalle != null)
                            {
                                objLinkDetalle.Visible = true;
                            }

                            if (objImagenGris != null)
                                objImagenGris.Visible = false;
                        }
                        else
                        {
                            if (objLinkDetalle != null)
                                objLinkDetalle.Visible = false;
                            if (objImagenGris != null)
                                objImagenGris.Visible = true;                            
                        }

                        //Cargar controles de publicar
                        objImagen = (Image)e.Row.FindControl("imgGrdPublicar");
                        objImagenGris = (Image)e.Row.FindControl("imgGrdPublicarGris");

                        //Mostrar los botones que correspondan a publicar
                        if (Convert.ToBoolean(objFila["PUBLICACION"]))
                        {
                            if (objImagen != null)
                            {
                                objImagen.Visible = true;
                            }

                            if (objImagenGris != null)
                                objImagenGris.Visible = false;
                        }
                        else
                        {
                            if (objImagen != null)
                                objImagen.Visible = false;
                            if (objImagenGris != null)
                                objImagenGris.Visible = true;
                        }


                        //Cargar controles de recurso
                        objImagen = (Image)e.Row.FindControl("imgGrdRecurso");
                        objImagenGris = (Image)e.Row.FindControl("imgGrdRecursoGris");

                        //Mostrar los botones que correspondan a publicar
                        if (Convert.ToBoolean(objFila["APLICA_RECURSO"]))
                        {
                            if (objImagen != null)
                            {
                                objImagen.Visible = true;
                            }

                            if (objImagenGris != null)
                                objImagenGris.Visible = false;
                        }
                        else
                        {
                            if (objImagen != null)
                                objImagen.Visible = false;
                            if (objImagenGris != null)
                                objImagenGris.Visible = true;
                        }


                        //Cambiar colores
                        if (blnProcesosPendientesNotificacion && Convert.ToInt32(objFila["ID_ESTADO_ACTO"]) != (int)NOTEstadosActo.Bloqueada_Anulacion)
                            ((HtmlGenericControl)e.Row.FindControl("divHeaderAccordionActos")).Attributes.Add("style", "background:#F79F81 !important");
                        else if (Convert.ToInt32(objFila["ID_ESTADO_ACTO"]) == (int)NOTEstadosActo.Sin_Verificar)
                            ((HtmlGenericControl)e.Row.FindControl("divHeaderAccordionActos")).Attributes.Add("style", "background:#DFFFFF !important");
                        else if (Convert.ToInt32(objFila["ID_ESTADO_ACTO"]) == (int)NOTEstadosActo.Verificado_NO_Liberado)
                            ((HtmlGenericControl)e.Row.FindControl("divHeaderAccordionActos")).Attributes.Add("style", "background:#DFDFFF !important");
                        else if (Convert.ToInt32(objFila["ID_ESTADO_ACTO"]) == (int)NOTEstadosActo.Verificado_Liberado_Parcialmente)
                            ((HtmlGenericControl)e.Row.FindControl("divHeaderAccordionActos")).Attributes.Add("style", "background:#FFFFBF !important");
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: grdActosAdministrativos_RowDataBound -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error cargando datos en grilla");
                }
            }


            /// <summary>
            /// Evento que realiza el cambio de pagina de grilla
            /// </summary>
            protected void grdActosAdministrativos_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cambiar pagina
                    this.grdActosAdministrativos.PageIndex = e.NewPageIndex;

                    //Consultar la información
                    this.ObtenerInformacionActosAdministrativos();

                    //Actualizar panel
                    this.upnlConsultaActosAdministrativos.Update();

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdBuscar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la paginación de la informaciòn de los actos administrativos");
                }
            }

        
            /// <summary>
            /// Evento que realiza apertura del modal para cambio de configuración
            /// </summary>
            protected void lnkGrdCambiarConfiguracion_Click(object sender, EventArgs e)
            {
                string[] lstStrParametros = null;

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cargar parametros de ingreso
                    lstStrParametros = (((LinkButton)sender).CommandArgument).Split('@');

                    //Limpiar modal
                    this.LimpiarModalCambioConfiguracionActo();

                    //Cargar valores para edición
                    this.hdfActoIDModalCambiarConfiguracion.Value = lstStrParametros[0];
                    this.hdfOrigenModalCambiarConfiguracion.Value = lstStrParametros[1];
                    this.hdfPublicaModalCambiarConfiguracion.Value = lstStrParametros[2];
                    this.hdfRutaModalCambiarConfiguracion.Value = lstStrParametros[3];

                    //Cargar información de acto administrativo en modal
                    this.ObtenerConfiguracionActoAdministrativo(Convert.ToInt64(this.hdfActoIDModalCambiarConfiguracion.Value), lstStrParametros[1]);

                    //Actualizar modal y mostrar
                    this.upnlModalCambiarConfiguracionActo.Update();
                    this.mpeModalCambiarConfiguracionActo.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: lnkGrdEditar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Limpiar modal
                    this.LimpiarModalCambioConfiguracionActo();

                    //Actualizar modal y cerrar
                    this.upnlModalCambiarConfiguracionActo.Update();
                    this.mpeModalCambiarConfiguracionActo.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error cargando información para cambio de configuración");
                }
                finally
                {
                    this.CargarTriggerActosAdministrativos();
                }
            }


            /// <summary>
            /// Evento que realiza apertura de modal para cambio de estado
            /// </summary>
            protected void lnkGrdCambiarEstado_Click(object sender, EventArgs e)
            {
                Notificacion objNotificacion = null;
                string[] lstStrParametros = null;

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cargar parametros de ingreso
                    lstStrParametros = (((LinkButton)sender).CommandArgument).Split('-');

                    //Limpiar modal
                    this.LimpiarModalCambioEstadoActo();

                    //Cargar valores para edición
                    this.hdfActoIDModalCambiarEstado.Value = lstStrParametros[0];
                    this.hdfEstadoIDModalCambiarEstado.Value = lstStrParametros[1];

                    //Verificar si se tiene procesos relacionados
                    objNotificacion = new Notificacion();
                    if (objNotificacion.TieneProcesosAsociadosNoNotificados(Convert.ToDecimal(this.hdfActoIDModalCambiarEstado.Value)))
                    {
                        //Cargar mensajes
                        this.lblTextoProcesosRelacionadosCambioEstado.Visible = true;
                        this.lblTextoProcesosRelacionadosCambioEstado.Text = "El acto administrativo presenta los siguientes actos relacionados que  no han culminado el proceso de notificación:<br/><br/>";
                        this.lblMensajeCambioEstado.Visible = true;
                        this.lblMensajeCambioEstado.Text = "<br/>En caso de querer continuar con el cambio de estado del acto administrativo seleccione el nuevo estado y haga clic en el botón de \"Modificar\". Recuerde que el cambio de estado puede afectar la información visualizada en el módulo de trabajo de notificaciones.";

                        //Cargar grilla
                        this.grdActosAdministrativosCambioEstado.DataSource = objNotificacion.ObtenerProcesosAsociadosNoNotificados(Convert.ToDecimal(this.hdfActoIDModalCambiarEstado.Value));
                        this.grdActosAdministrativosCambioEstado.DataBind();
                        this.grdActosAdministrativosCambioEstado.Visible = true;
                    }
                    else
                    {
                        this.lblMensajeCambioEstado.Visible = true;
                        this.lblMensajeCambioEstado.Text = "Para realizar el cambio de estado del acto administrativo seleccione el nuevo estado y haga clic en el botón de \"Modificar\". Recuerde que el cambio de estado puede afectar la información visualizada en el módulo de trabajo de notificaciones.";
                    }

                    //Cargar estados acto
                    this.ConsultaEstadosActo(this.cboEstadoCambiarEstadoActo);
                    this.cboEstadoCambiarEstadoActo.SelectedValue = this.hdfEstadoIDModalCambiarEstado.Value;                    

                    //Actualizar modal y mostrar
                    this.upnlModalCambiarEstadoActo.Update();
                    this.mpeModalCambiarEstadoActo.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: lnkGrdCambiarEstado_Click -> Error Inesperado: " + exc.StackTrace);

                    //Limpiar modal
                    this.LimpiarModalCambioEstadoActo();

                    //Actualizar modal y cerrar
                    this.upnlModalCambiarEstadoActo.Update();
                    this.mpeModalCambiarEstadoActo.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error cargando información para cambio de estado");
                }
                finally
                {
                    this.CargarTriggerActosAdministrativos();
                }
            }


            /// <summary>
            /// Evento que realiza descarga de documento o permite visualizar contenido de carpeta
            /// </summary>
            protected void imgGrdDescargarDocumento_Click(object sender, ImageClickEventArgs e)
            {
                try
                {
                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        System.IO.FileInfo targetFile = new System.IO.FileInfo(((ImageButton)sender).CommandArgument.ToString());
                        this.Response.Clear();
                        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                        this.Response.ContentType = "application/octet-stream";
                        this.Response.ContentType = "application/base64";
                        this.Response.WriteFile(targetFile.FullName);
                        this.Response.WriteFile(((ImageButton)sender).CommandArgument.ToString());
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: imgGrdDescargarDocumento_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }

            
            /// <summary>
            /// Evento que muestra modal con listado de archivos de carpeta especificada
            /// </summary>
            protected void lnkGrdDescargarDocumento_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Limpiar modal
                    this.LimpiarModalVerDocumentosActo();

                    //Cargar documentos de carpeta
                    this.CargarDocumentosCarpeta(((LinkButton)sender).CommandArgument.ToString(), this.grdDocumentosActoAdministrativoVer);

                    //Cargar triggers
                    this.CargarTriggerDocumentosActoAdministrativo();

                    //Actualizar panel y mostrar modal
                    this.upnlVerDocumentoActoAdministrativo.Update();
                    this.mpeVerDocumentoActoAdministrativo.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: lnkGrdDescargarDocumento_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento cargando datos de archivos");
                }
                finally
                {
                    this.CargarTriggerActosAdministrativos();
                }
            }


            /// <summary>
            /// Evento que muestra el listado de personas a notificar
            /// </summary>
            protected void lnkGrdNotificaciones_Click(object sender, EventArgs e)
            {
                string[] lstParametros = null;

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cargar listado de parametros
                    lstParametros = ((LinkButton)sender).CommandArgument.ToString().Split('@');

                    //Cargar datos
                    this.CargarDatosModalConfigurarPersonas(Convert.ToInt64(lstParametros[0]), Convert.ToInt32(lstParametros[1]), (int)TipoNotificacion.NOTIFICACION);

                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: lnkGrdNotificaciones_Click -> Error Inesperado: " + exc.StackTrace);

                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando información de personas a notificar");
                }
                finally
                {
                    this.CargarTriggerActosAdministrativos();
                }
            }


            /// <summary>
            /// Evento que muestra el listado de personas a comunicar
            /// </summary>
            protected void lnkGrdComunicar_Click(object sender, EventArgs e)
            {
                string[] lstParametros = null;

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cargar listado de parametros
                    lstParametros = ((LinkButton)sender).CommandArgument.ToString().Split('@');

                    //Cargar datos
                    this.CargarDatosModalConfigurarPersonas(Convert.ToInt64(lstParametros[0]), Convert.ToInt32(lstParametros[1]), (int)TipoNotificacion.COMUNICACION);

                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: lnkGrdComunicar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando información de personas a comunicar");
                }
                finally
                {
                    this.CargarTriggerActosAdministrativos();
                }
            }


            /// <summary>
            /// Evento que muestra el listado de personas en cumplase
            /// </summary>
            protected void lnkGrdCumplir_Click(object sender, EventArgs e)
            {
                string[] lstParametros = null;

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cargar listado de parametros
                    lstParametros = ((LinkButton)sender).CommandArgument.ToString().Split('@');

                    //Cargar datos
                    this.CargarDatosModalConfigurarPersonas(Convert.ToInt64(lstParametros[0]), Convert.ToInt32(lstParametros[1]), (int)TipoNotificacion.CUMPLASE);

                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: lnkGrdCumplir_Click -> Error Inesperado: " + exc.StackTrace);

                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando información de personas a cumplir");
                }
                finally
                {
                    this.CargarTriggerActosAdministrativos();
                }
            }

        #endregion


        #region ModalCambiarEstadoActo

            /// <summary>
            /// Evento que realiza cambio de estado
            /// </summary>
            protected void cmdModalCambiarEstadoActoAceptar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Verificar que se haya ingresado la información de manera correcta
                    if (Page.IsValid)
                    {
                        //Modificar acto administrativo
                        this.ModificarEstadoActo(Convert.ToInt64(this.hdfActoIDModalCambiarEstado.Value), Convert.ToInt32(this.cboEstadoCambiarEstadoActo.SelectedValue));

                        //Limpiar modal
                        this.LimpiarModalCambioEstadoActo();

                        //Actualizar modal y cerrar
                        this.upnlModalCambiarEstadoActo.Update();
                        this.mpeModalCambiarEstadoActo.Hide();

                        //Realizar consulta de actoas administrativos
                        this.ObtenerInformacionActosAdministrativos();

                        //Actualizar panel
                        this.upnlConsultaActosAdministrativos.Update();
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdModalCambiarEstadoActoCancelar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Limpiar modal
                    this.LimpiarModalCambioEstadoActo();

                    //Actualizar modal y cerrar
                    this.upnlModalCambiarEstadoActo.Update();
                    this.mpeModalCambiarEstadoActo.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando cambio de estado");
                }
                finally
                {
                    this.CargarTriggerActosAdministrativos();
                }
            }


            /// <summary>
            /// Evento que limpia modal y cancel proceso de cambio de estado
            /// </summary>
            protected void cmdModalCambiarEstadoActoCancelar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Limpiar modal
                    this.LimpiarModalCambioEstadoActo();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdModalCambiarEstadoActoCancelar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando cancelación proceso de cambio de estado");
                }
                finally
                {
                    //Actualizar modal y cerrar
                    this.upnlModalCambiarEstadoActo.Update();
                    this.mpeModalCambiarEstadoActo.Hide();
                    this.CargarTriggerActosAdministrativos();
                }
            }

            /// <summary>
            /// Valida que la configuración de personas se encuentre correcta para el estado seleccionado
            /// </summary>
            protected void cvEstadoCambiarEstadoActo_ServerValidate(object source, ServerValidateEventArgs args)
            {
                Notificacion objNotificacion = null;
                NotificacionEntity objNotificacionEntity = null;
                string strMensajeError = "";

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    if (!string.IsNullOrWhiteSpace(this.cboEstadoCambiarEstadoActo.SelectedValue) && this.cboEstadoCambiarEstadoActo.SelectedValue != "-1")
                    {
                        //Consultar el listado de personas del acto administrativo
                        objNotificacion = new Notificacion();
                        objNotificacionEntity = objNotificacion.ConsultarInformacionActo(Convert.ToDecimal(this.hdfActoIDModalCambiarEstado.Value));

                        //Verificxar configuración si se va a liberar notificaciones
                        if (Convert.ToInt32(this.cboEstadoCambiarEstadoActo.SelectedValue) == (int)NOTEstadosActo.Verificado_Liberado)
                        {
                            //Validar que todas las personas se encuentren verificadas
                            if (objNotificacionEntity.ListaPersonas.Where(persona => persona.EstadoPersonaID == (int)NOTEstadosActoPersona.Sin_Verificar).ToList().Count() > 0)
                            {
                                args.IsValid = false;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Antes de liberar el acto administrativo debe verificar todas las personas a notificar')", true);
                            }
                            else
                            {
                                //Validar que según configuración existan personas
                                if (objNotificacionEntity.EsNotificacion.Value)
                                {
                                    if (objNotificacionEntity.ListaPersonas.Where(persona => persona.TipoNotificacionId == (int)TipoNotificacion.NOTIFICACION).ToList().Count() == 0)
                                    {
                                        strMensajeError = "- Acto administrativo debe tener personas para notificar. \\n";
                                    }
                                }
                                if (objNotificacionEntity.EsComunicacion.Value)
                                {
                                    if (objNotificacionEntity.ListaPersonas.Where(persona => persona.TipoNotificacionId == (int)TipoNotificacion.COMUNICACION).ToList().Count() == 0)
                                    {
                                        strMensajeError = "- Acto administrativo debe tener personas para comunicar. \\n";
                                    }
                                }
                                if (objNotificacionEntity.EsCumplase.Value && !objNotificacionEntity.EsNotificacion.Value)
                                {
                                    if (objNotificacionEntity.ListaPersonas.Where(persona => persona.TipoNotificacionId == (int)TipoNotificacion.CUMPLASE).ToList().Count() == 0)
                                    {
                                        strMensajeError = "- Acto administrativo debe tener personas efectuar el cumplimiento del acto administrativo. \\n";
                                    }
                                }

                                //Mostrar mensaje de error
                                if (!string.IsNullOrWhiteSpace(strMensajeError))
                                {
                                    args.IsValid = false;
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('" + strMensajeError + " Por favor verificar la configuración del acto administrativo')", true);
                                }
                                else
                                {
                                    args.IsValid = true;
                                }
                            }
                        }
                        else
                        {
                            args.IsValid = true;
                        }
                    }
                    else
                    {
                        args.IsValid = false;
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cvEstadoCambiarEstadoActo_ServerValidate -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error verificando configuración de personas para el estado indicado");
                }
            }

        #endregion


        #region ModalDocumentoActoAdmnistrativoVer

            /// <summary>
            /// Evento que permite la descarga de los archivos
            /// </summary>
            protected void imgDescargarDocumentoActoAdmnistrativoVer_Click(object sender, ImageClickEventArgs e)
            {
                try
                {
                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        System.IO.FileInfo targetFile = new System.IO.FileInfo(((ImageButton)sender).CommandArgument.ToString());
                        this.Response.Clear();
                        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                        this.Response.ContentType = "application/octet-stream";
                        this.Response.ContentType = "application/base64";
                        this.Response.WriteFile(targetFile.FullName);
                        this.Response.WriteFile(((ImageButton)sender).CommandArgument.ToString());
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: imgDescargarDocumentoActoAdmnistrativoVer_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }


            /// <summary>
            /// Evento que cierra lel modal
            /// </summary>
            protected void cmdCerrarVerDocumentoActoAdministrativo_Click(object sender, EventArgs e)
            {
                //Limpiar modal
                this.LimpiarModalVerDocumentosActo();

                //Actualizar panel y cerrar modal
                this.upnlVerDocumentoActoAdministrativo.Update();
                this.mpeVerDocumentoActoAdministrativo.Hide();
                this.CargarTriggerActosAdministrativos();
            }

        #endregion


        #region ModalCambiarConfiguracionActo

            /// <summary>
            /// Verifica que la información ingresada se encuentre correcta
            /// </summary>
            protected void cvCambiarConfiguracionActo_ServerValidate(object source, ServerValidateEventArgs args)
            {
                Notificacion objNotificacion = null;
                NotificacionEntity objNotificacionEntity = null;
                List<PersonaNotificarEntity> objLstPersonas = null;      
                string strMensajeError = "";
                
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Verificar que se seleccione alguna de las opciones de configuración
                    if (!this.chkNotificarCambiarConfiguracionActo.Checked && !this.chkComunicarCambiarConfiguracionActo.Checked &&
                       !this.chkCumplirCambiarConfiguracionActo.Checked && !this.chkPublicarCambiarConfiguracionActo.Checked)
                    {
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Debe seleccionar por lo menos una de las opciones de configuración para el acto administrativo')", true);
                    }
                    else if(this.hdfOrigenModalCambiarConfiguracion.Value != "PUB")
                    {
                        //Consultar el listado de personas del acto administrativo
                        objNotificacion = new Notificacion();
                        objNotificacionEntity = objNotificacion.ConsultarInformacionActo(Convert.ToDecimal(this.hdfActoIDModalCambiarConfiguracion.Value));

                        //Verificar si no esta seleccionado notificación y hay personas a notificar
                        objLstPersonas = objNotificacionEntity.ListaPersonas.Where(persona => persona.TipoNotificacionId == (int)TipoNotificacion.NOTIFICACION).ToList();
                        if (!this.chkNotificarCambiarConfiguracionActo.Checked && objLstPersonas.Count > 0)
                        {
                            //Agrupar y validar conteos
                            var objGrupos = objLstPersonas.GroupBy(persona => persona.NumeroIdentificacion);

                            //Ciclo que verifica si existe mas de un estado
                            foreach (var objGrupo in objGrupos)
                            {
                                if (objGrupo.Count() > 1)
                                {
                                    strMensajeError = "- La opción de Notificar debe encontrarse activa ya que existen personas que comenzaron proceso de notificación. \\n";
                                }
                            }
                        }

                        //Verificar si no esta seleccionado comunicar y hay personas a comunicar
                        objLstPersonas = objNotificacionEntity.ListaPersonas.Where(persona => persona.TipoNotificacionId == (int)TipoNotificacion.COMUNICACION).ToList();
                        if (!this.chkComunicarCambiarConfiguracionActo.Checked && objLstPersonas.Count > 0)
                        {
                            //Agrupar y validar conteos
                            var objGrupos = objLstPersonas.GroupBy(persona => persona.NumeroIdentificacion);

                            //Ciclo que verifica si existe mas de un estado
                            foreach (var objGrupo in objGrupos)
                            {
                                if (objGrupo.Count() > 1)
                                {
                                    strMensajeError = "- La opción de Comunicar debe encontrarse activa ya que existen personas que comenzaron proceso de comunicación. \\n";
                                }
                            }
                        }

                        //Verificar si no esta seleccionado cumplase y hay personas cumplase
                        objLstPersonas = objNotificacionEntity.ListaPersonas.Where(persona => persona.TipoNotificacionId == (int)TipoNotificacion.CUMPLASE).ToList();
                        if (!this.chkCumplirCambiarConfiguracionActo.Checked && objLstPersonas.Count > 0)
                        {
                            //Agrupar y validar conteos
                            var objGrupos = objLstPersonas.GroupBy(persona => persona.NumeroIdentificacion);

                            //Ciclo que verifica si existe mas de un estado
                            foreach (var objGrupo in objGrupos)
                            {
                                if (objGrupo.Count() > 1)
                                {
                                    strMensajeError = "- La opción de Cumplir debe encontrarse activa ya que existen personas que comenzaron proceso de cumplase. \\n";
                                }
                            }
                        }

                        //Validar si cambio configuración de recurso de reposición
                        if (objNotificacionEntity.AplicaRecursoReposicion != this.chkRecursoCambiarConfiguracionActo.Checked)
                        {
                            //Agrupar y validar conteos
                            var objGrupos = objNotificacionEntity.ListaPersonas.GroupBy(persona => persona.NumeroIdentificacion);

                            //Ciclo que verifica si existe mas de un estado
                            foreach (var objGrupo in objGrupos)
                            {
                                if (objGrupo.Count() > 1)
                                {
                                    strMensajeError = "- No se puede cambiar parámetro de recurso de reposición si alguna de las personas comenzo proceso de notificación, comunicación y/o cumplase. \\n";
                                }
                            }

                        }

                        //Verificar si parametro de publicación cambio validar si ya finalizo el proceso completo de publicidad para todas las personas
                        if ((this.hdfPublicaModalCambiarConfiguracion.Value == "1" ? true : false) != this.chkRecursoCambiarConfiguracionActo.Checked)
                        {
                            if (objNotificacionEntity.ListaPersonas.Where(persona => persona.EstadoActualNotificado && persona.EstadoNotificado.ID == 18).ToList().Count > 0)
                            {
                                strMensajeError = "- No se puede cambiar parámetro de publicación si todas las personas finalizaron proceso de publicidad. \\n";
                            }
                        }

                        //Validar si se presento error
                        if (!string.IsNullOrWhiteSpace(strMensajeError))
                        {
                            args.IsValid = false;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('" + strMensajeError + "')", true);
                        }
                        else
                        {
                            args.IsValid = true;
                        }
                    }       
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cvCambiarConfiguracionActo_ServerValidate -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error verificando información de configuración ingresada");
                }
            }


            /// <summary>
            /// Evento que realiza el cambio de información de la configuración del acto administrativo
            /// </summary>
            protected void cmdModalCambiarConfiguracionActoAceptar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de erro
                    this.OcultarMensaje();

                    //Verificar que se haya ingresado la información de manera correcta
                    if (Page.IsValid)
                    {
                        //Modificar acto administrativo
                        this.ModificarConfiguracionActo(Convert.ToInt64(this.hdfActoIDModalCambiarConfiguracion.Value),
                                                        this.hdfOrigenModalCambiarConfiguracion.Value,
                                                        this.chkNotificarCambiarConfiguracionActo.Checked,
                                                        this.chkComunicarCambiarConfiguracionActo.Checked,
                                                        this.chkCumplirCambiarConfiguracionActo.Checked,
                                                        this.chkPublicarCambiarConfiguracionActo.Checked,
                                                        this.chkRecursoCambiarConfiguracionActo.Checked,
                                                        this.hdfRutaModalCambiarConfiguracion.Value,
                                                        (this.cboArchivoActoCambiarConfiguracionActo.Items.Count > 1 ? this.cboArchivoActoCambiarConfiguracionActo.SelectedValue : ""));

                        //Limpiar modal
                        this.LimpiarModalCambioConfiguracionActo();

                        //Actualizar modal y cerrar
                        this.upnlModalCambiarConfiguracionActo.Update();
                        this.mpeModalCambiarConfiguracionActo.Hide();

                        //Realizar consulta de actoas administrativos
                        this.ObtenerInformacionActosAdministrativos();

                        //Actualizar panel
                        this.upnlConsultaActosAdministrativos.Update();
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdModalCambiarConfiguracionActoAceptar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Limpiar modal
                    this.LimpiarModalCambioConfiguracionActo();

                    //Actualizar modal y cerrar
                    this.upnlModalCambiarConfiguracionActo.Update();
                    this.mpeModalCambiarConfiguracionActo.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error guardando la nueva configuración del acto administrativo");
                }
                finally
                {
                    this.CargarTriggerActosAdministrativos();
                }
            }

            /// <summary>
            /// Evento que cierra y cancela configuración del acto administrativo
            /// </summary>
            protected void cmdModalCambiarConfiguracionActoCancelar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Limpiar modal
                    this.LimpiarModalCambioConfiguracionActo();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdModalCambiarConfiguracionActoCancelar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando cancelación proceso de cambio de configuración");
                }
                finally
                {
                    //Actualizar modal y cerrar
                    this.upnlModalCambiarConfiguracionActo.Update();
                    this.mpeModalCambiarConfiguracionActo.Hide();
                    this.CargarTriggerActosAdministrativos();
                }
            }


            /// <summary>
            /// Evento que habilita o deshabilita desplegable de archivos y verifica checks
            /// </summary>
            protected void chkParametroCambiarConfiguracionActo_CheckedChanged(object sender, EventArgs e)
            {
                //Verificar si el origen es publicaciones
                if (this.hdfOrigenModalCambiarConfiguracion.Value == "PUB")
                {
                    if (((CheckBox)sender).Checked)
                    {
                        //Cargar listado de archivos
                        this.CargarListadoArchivos(this.hdfRutaModalCambiarConfiguracion.Value, this.cboArchivoActoCambiarConfiguracionActo);

                        //Mostrar campo
                        this.dvArchivoActoCambiarConfiguracionActo.Visible = true;
                    }
                    else if (!this.chkNotificarCambiarConfiguracionActo.Checked && !this.chkCumplirCambiarConfiguracionActo.Checked && !this.chkCumplirCambiarConfiguracionActo.Checked)
                    {
                        //Limpiar y ocultar
                        this.cboArchivoActoCambiarConfiguracionActo.ClearSelection();
                        this.cboArchivoActoCambiarConfiguracionActo.Items.Clear();
                        this.dvArchivoActoCambiarConfiguracionActo.Visible = false;
                    }
                }                
                else
                {
                    //Limpiar y ocultar
                    this.cboArchivoActoCambiarConfiguracionActo.ClearSelection();
                    this.cboArchivoActoCambiarConfiguracionActo.Items.Clear();
                    this.dvArchivoActoCambiarConfiguracionActo.Visible = false;
                }

                //Si esta activo cumplase inactivar comunicar y notificar
                if (((CheckBox)sender).ID.IndexOf("chkCumplirCambiarConfiguracionActo") != -1 && this.chkCumplirCambiarConfiguracionActo.Checked)
                {
                    this.chkNotificarCambiarConfiguracionActo.Checked = false;
                    this.chkComunicarCambiarConfiguracionActo.Checked = false;
                    this.chkRecursoCambiarConfiguracionActo.Checked = false;
                }

                //Si no hay notificación inactivar recurso
                if (!this.chkNotificarCambiarConfiguracionActo.Checked)
                    this.chkRecursoCambiarConfiguracionActo.Checked = false;

                //Si esta activo notificación o comunicación inactivar cumplase
                if (this.chkNotificarCambiarConfiguracionActo.Checked || this.chkComunicarCambiarConfiguracionActo.Checked)
                    this.chkCumplirCambiarConfiguracionActo.Checked = false;
                
            }


            /// <summary>
            /// Activa opciones de notificación
            /// </summary>
            protected void chkRecursoCambiarConfiguracionActo_CheckedChanged(object sender, EventArgs e)
            {
                if (((CheckBox)sender).Checked)
                {
                    this.chkNotificarCambiarConfiguracionActo.Checked = true;
                    this.chkCumplirCambiarConfiguracionActo.Checked = false;
                }               
            }


        #endregion


        #region ModalConfigurarPersonas


            /// <summary>
            /// Evento que cierra modal de configuracion de personas
            /// </summary>
            protected void cmdModalConfigurarPersonasCancelar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Limpiar modal
                    this.LimpiarModalConfigurarPersonas();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdModalConfigurarPersonasCancelar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error limpiando modal de configuración de personas");
                }
                finally
                {
                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Hide();
                    this.CargarTriggerActosAdministrativos();
                }
            }


            /// <summary>
            /// Evento que se ejecuta y da formato a la información mostrada al cargar listado de personas
            /// </summary>
            protected void grdPersonas_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                DataRowView objFila = null;
                LinkButton objLink = null;
                Literal objLiteral = null;
                CheckBox objCheck = null;

                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Cargar datos
                        objFila = (DataRowView)e.Row.DataItem;

                        //Mostrar botón de eliminar
                        objLink = (LinkButton)e.Row.FindControl("lnkEliminarGrdPersonas");
                        objLiteral = (Literal)e.Row.FindControl("ltlEliminarGrdPersonas");

                        //Mostrar los botones que correspondan a notificaciones
                        if (Convert.ToBoolean(objFila["COMENZO_PROCESO"]) || Convert.ToInt32(objFila["ID_ESTADO_PERSONA_NOTIFICAR"]) == (int)NOTEstadosActoPersona.Verificado)
                        {
                            if (objLiteral != null)
                            {
                                objLiteral.Visible = true;
                            }

                            if (objLink != null)
                                objLink.Visible = false;
                        }
                        else
                        {
                            if (objLiteral != null)
                                objLiteral.Visible = false;
                            if (objLink != null)
                                objLink.Visible = true;
                        }

                        //Cargar checkbox
                        objCheck = (CheckBox)e.Row.FindControl("chkVerificadoGrdPersonas");
                        if (objCheck != null && this.grdPersonasConfigurarPersonas.Columns[9].Visible)
                        {
                            //Cargar valor
                            objCheck.Attributes.Add("value", objFila["ID_PERSONA"].ToString());

                            //Seleccionar
                            if (Convert.ToInt32(objFila["ID_ESTADO_PERSONA_NOTIFICAR"]) == (int)NOTEstadosActoPersona.Verificado)
                            {
                                objCheck.Checked = true;
                            }
                            else
                            {
                                objCheck.Checked = false;
                            }
                        }

                        //Cambiar colores
                        if (Convert.ToInt32(objFila["ID_ESTADO_PERSONA_NOTIFICAR"]) == (int)NOTEstadosActoPersona.Sin_Verificar)
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFFFFF");
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: grdPersonas_RowDataBound -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error cargando datos en grilla");
                }
            }


            /// <summary>
            /// Evento que cambia estado de persona
            /// </summary>
            protected void chkVerificadoGrdPersonas_CheckedChanged(object sender, EventArgs e)
            {

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cambiar el estado
                    this.ModificarEstadoPersona(Convert.ToInt64(((CheckBox)sender).Attributes["value"]), (((CheckBox)sender).Checked ? (int)NOTEstadosActoPersona.Verificado : (int)NOTEstadosActoPersona.Sin_Verificar));

                    //Cargar datos modal
                    this.CargarDatosModalConfigurarPersonas(Convert.ToInt64(this.hdfActoIDConfigurarPersonas.Value),
                                                            Convert.ToInt32(this.hdfEstadoActoIDConfigurarPersonas.Value),
                                                            Convert.ToInt32(this.hdfTipoNotificacionIDConfigurarPersonas.Value));

                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: chkVerificadoGrdPersonas_CheckedChanged -> Error Inesperado: " + exc.StackTrace);

                    //Actualizar modal
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error modificando estado de persona");
                }                
            }


            /// <summary>
            /// Evento que abre modal de insertar editar persona
            /// </summary>
            protected void cmdAdicionarConfigurarPersonas_Click(object sender, EventArgs e)
            {
                try
                {
                    //Limpiar modal
                    this.LimpiarModalAgregarEditarPersona();

                    //Cerrar modal anterior
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar datos modal
                    //Cargar datos de modal
                    this.CargarDatosModalAgregarEditarPersona(AccionesPersonas.Adicionar, Convert.ToInt64(this.hdfActoIDConfigurarPersonas.Value), Convert.ToInt32(this.hdfEstadoActoIDConfigurarPersonas.Value), Convert.ToInt32(this.hdfTipoNotificacionIDConfigurarPersonas.Value));

                    //Mostrar modal
                    this.upnlModalAgregarEditarUsuario.Update();
                    this.mpeModalAgregarEditarUsuario.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdAdicionarConfigurarPersonas_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando datos modal de adicionar persona");
                }
            }


            /// <summary>
            /// Evento que abre modal de editar persona
            /// </summary>
            protected void lnkEditarGrdPersonas_Click(object sender, EventArgs e)
            {
                try
                {
                    //Limpiar modal
                    this.LimpiarModalAgregarEditarPersona();



                    //Cerrar modal anterior
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar datos de modal
                    this.CargarDatosModalAgregarEditarPersona(AccionesPersonas.Editar, Convert.ToInt64(this.hdfActoIDConfigurarPersonas.Value), Convert.ToInt32(this.hdfEstadoActoIDConfigurarPersonas.Value), Convert.ToInt32(this.hdfTipoNotificacionIDConfigurarPersonas.Value), Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString()));

                    //Mostrar modal
                    this.upnlModalAgregarEditarUsuario.Update();
                    this.mpeModalAgregarEditarUsuario.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: lnkEditarGrdPersonas_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando datos modal de editar persona");
                }
            }


            /// <summary>
            /// Evento que muestra modal de elimianr persona
            /// </summary>
            protected void lnkEliminarGrdPersonas_Click(object sender, EventArgs e)
            {
                string[] lstStrParametros = null;

                try
                {
                    //Cerrar modal anterior
                    this.mpeModalConfigurarPersonas.Hide();

                    //Limpiar modal
                    this.LimpiarModalEliminarPersona();

                    //Cargar parametros de ingreso
                    lstStrParametros = (((LinkButton)sender).CommandArgument).Split('@');

                    //Cargar datos al modal de eliminar
                    this.ltlUsuarioEliminarPersona.Text = lstStrParametros[2];
                    this.hdfActoIdEliminarPersona.Value = lstStrParametros[0];
                    this.hdfPersonaIDEliminarPersona.Value = lstStrParametros[1];
                    this.hdfEstadoActoIDEliminarPersona.Value = this.hdfEstadoActoIDConfigurarPersonas.Value;
                    this.hdfTipoNotificacionIDEliminarPersona.Value = this.hdfTipoNotificacionIDConfigurarPersonas.Value;

                    //Mostrar modal
                    this.upnlModalEliminarPersona.Update();
                    this.mpeModalEliminarPersona.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: lnkEliminarGrdPersonas_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando datos modal de eliminar persona");
                }
            }


        #endregion


        #region ModalAgregarEditarPersona


            /// <summary>
            /// Evento que adiciona o edita los datos de una persona
            /// </summary>
            protected void cmdAgregarEditarUsuarioAdicionar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Limpiar mensajes
                    this.OcultarMensaje();

                    //Verificar que no exista información erronea
                    if (Page.IsValid)
                    {

                        //Veriricar el tipo de acción a realizar
                        if (Convert.ToInt32(this.hdfAccionRealizarAgregarEditarUsuario.Value) == (int)AccionesPersonas.Adicionar)
                        {
                            this.CrearPersona(Convert.ToInt64(this.hdfActoIdAgregarEditarUsuario.Value), Convert.ToInt32(this.cboTipoIdentificacionAgregarEditarUsuario.SelectedValue),
                                              this.hdfNumeroIdentificacionAgregarEditarUsuario.Value, this.ltlUsuarioAgregarEditarUsuario.Text, Convert.ToInt32(this.hdfTipoNotificacionIDAgregarEditarUsuario.Value),
                                              Convert.ToInt32(this.cboFlujoTipoNotificacionAgregarEditarUsuario.SelectedValue), Convert.ToInt32(this.cboEstadoFlujoAgregarEditarUsuario.SelectedValue),
                                              DateTime.ParseExact(this.txtFechaEstadoAgregarEditarUsuario.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                        }
                        else if (Convert.ToInt32(this.hdfAccionRealizarAgregarEditarUsuario.Value) == (int)AccionesPersonas.Editar)
                        {
                            this.EditarPersona(Convert.ToInt64(this.hdfActoIdAgregarEditarUsuario.Value), Convert.ToInt64(this.hdfPersonaIDAgregarEditarUsuario.Value),
                                              Convert.ToInt32(this.cboTipoIdentificacionAgregarEditarUsuario.SelectedValue), this.hdfNumeroIdentificacionAgregarEditarUsuario.Value, this.ltlUsuarioAgregarEditarUsuario.Text, Convert.ToInt32(this.cboTipoNotificacionAgregarEditarUsuario.SelectedValue),
                                              Convert.ToInt32(this.cboFlujoTipoNotificacionAgregarEditarUsuario.SelectedValue), Convert.ToInt32(this.cboEstadoFlujoAgregarEditarUsuario.SelectedValue),
                                              DateTime.ParseExact(this.txtFechaEstadoAgregarEditarUsuario.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                        }

                        //realizar consulta de personas
                        this.CargarDatosModalConfigurarPersonas(Convert.ToInt64(this.hdfActoIdAgregarEditarUsuario.Value), Convert.ToInt32(this.hdfEstadoActoIDAgregarEditarUsuario.Value), Convert.ToInt32(this.hdfTipoNotificacionIDAgregarEditarUsuario.Value));

                        //Limpiar y cerrar modal
                        this.LimpiarModalAgregarEditarPersona();
                        this.upnlModalAgregarEditarUsuario.Update();
                        this.mpeModalAgregarEditarUsuario.Hide();

                        //Abrir modal anterior
                        this.upnlModalConfigurarPersonas.Update();
                        this.mpeModalConfigurarPersonas.Show();
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdAgregarEditarUsuarioAdicionar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cerrar modales
                    this.LimpiarModalAgregarEditarPersona();
                    this.LimpiarModalConfigurarPersonas();
                    this.upnlModalConfigurarPersonas.Update();
                    this.upnlModalAgregarEditarUsuario.Update();                    
                    this.mpeModalAgregarEditarUsuario.Hide();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error creando / editando persona");
                }
            }

            
            /// <summary>
            /// Evento que cierra modal de agrgar o editar una persona
            /// </summary>
            protected void cmdAgregarEditarUsuarioCerrar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Limpiar modal
                    this.LimpiarModalAgregarEditarPersona();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdAgregarEditarUsuarioCerrar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error limpiando modal de adicionar editar persona");
                }
                finally
                {
                    //Actualizar modal
                    this.upnlModalAgregarEditarUsuario.Update();
                    this.mpeModalAgregarEditarUsuario.Hide();

                    //Mostrar modal anterior
                    this.mpeModalConfigurarPersonas.Show();
                }
            }

            
            /// <summary>
            /// Evento que carga los flujos de acuerdo al tipo de notificación
            /// </summary>
            protected void cboTipoNotificacionAgregarEditarUsuario_SelectedIndexChanged(object sender, EventArgs e)
            {
                
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Verificar si se selecciono tipo de notificación
                    if (!string.IsNullOrWhiteSpace(this.cboTipoNotificacionAgregarEditarUsuario.SelectedValue) && this.cboTipoNotificacionAgregarEditarUsuario.SelectedValue != "-1")
                    {
                        //Cargar informacion listado
                        this.CargarListadoFlujos(this.cboFlujoTipoNotificacionAgregarEditarUsuario, Convert.ToInt64(this.hdfActoIdAgregarEditarUsuario.Value), Convert.ToInt32(this.cboTipoNotificacionAgregarEditarUsuario.SelectedValue));
                    }
                    else
                    {
                        //Limpiar listados
                        this.cboFlujoTipoNotificacionAgregarEditarUsuario.ClearSelection();
                        this.cboFlujoTipoNotificacionAgregarEditarUsuario.Items.Clear();
                        this.cboFlujoTipoNotificacionAgregarEditarUsuario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }

                    //Limpiar estados
                    this.cboEstadoFlujoAgregarEditarUsuario.ClearSelection();
                    this.cboEstadoFlujoAgregarEditarUsuario.Items.Clear();
                    this.cboEstadoFlujoAgregarEditarUsuario.Items.Insert(0, new ListItem("Seleccione...", "-1"));

                    //Actualizar modal
                    this.upnlModalAgregarEditarUsuario.Update();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cboTipoNotificacionAgregarEditarUsuario_SelectedIndexChanged -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando listado de flujos");
                }
                
            }


            /// <summary>
            /// Evento que carga estados de acuerdo al flujo
            /// </summary>
            protected void cboFlujoTipoNotificacionAgregarEditarUsuario_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Verificar que seleccionen datos
                    if (!string.IsNullOrWhiteSpace(this.cboFlujoTipoNotificacionAgregarEditarUsuario.SelectedValue) && this.cboFlujoTipoNotificacionAgregarEditarUsuario.SelectedValue != "-1" )
                    {
                        //Cargar estados
                        this.cboEstadoFlujoAgregarEditarUsuario.DataSource = this._objLstFlujosNotificacion.Select(x => new { x.EstadoInicialFlujo, x.EstadoInicialFlujoID, x.FlujoNotificacionElectronicaID }).Where(x => x.FlujoNotificacionElectronicaID == Convert.ToInt32(this.cboFlujoTipoNotificacionAgregarEditarUsuario.SelectedValue)).Distinct();
                        this.cboEstadoFlujoAgregarEditarUsuario.DataValueField = "EstadoInicialFlujoID";
                        this.cboEstadoFlujoAgregarEditarUsuario.DataTextField = "EstadoInicialFlujo";
                        this.cboEstadoFlujoAgregarEditarUsuario.DataBind();
                        this.cboEstadoFlujoAgregarEditarUsuario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }
                    else
                    {
                        //Limpiar estados
                        this.cboEstadoFlujoAgregarEditarUsuario.ClearSelection();
                        this.cboEstadoFlujoAgregarEditarUsuario.Items.Clear();
                        this.cboEstadoFlujoAgregarEditarUsuario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }

                    //Actualizar modal
                    this.upnlModalAgregarEditarUsuario.Update();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cboFlujoTipoNotificacionAgregarEditarUsuario_SelectedIndexChanged -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando listado de estados");
                }

                

            }

            /// <summary>
            /// Evento que verifica la existencia del usuario
            /// </summary>
            protected void cmdValidarUsuarioAgregarEditarUsuario_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Verificar pagina
                    if(Page.IsValid){
                        //Verificar que seleccionen datos
                        if (!string.IsNullOrWhiteSpace(this.cboTipoIdentificacionAgregarEditarUsuario.SelectedValue) && this.cboTipoIdentificacionAgregarEditarUsuario.SelectedValue != "-1" &&
                            !string.IsNullOrWhiteSpace(this.txtNroIdentificacionAgregarEditarUsuario.Text))
                        {
                     
                            //Consultar información de usuario
                            this.ValidarExistenciaUsuario(Convert.ToInt32(this.cboTipoIdentificacionAgregarEditarUsuario.SelectedValue), this.txtNroIdentificacionAgregarEditarUsuario.Text);
                        }
                        else
                        {
                            //Limpiar campos
                            this.ltlUsuarioAgregarEditarUsuario.Text = "";
                            this.hdfNumeroIdentificacionAgregarEditarUsuario.Value = "";
                        }

                        //Actualizar modal
                        this.upnlModalAgregarEditarUsuario.Update();
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdValidarUsuarioAgregarEditarUsuario_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error verificando existencia del usuario");
                }

            }


            /// <summary>
            /// Evento que verifica que usuario ingresado no se encuentre en listado de personas del acto
            /// </summary>
            protected void cvValidarUsuario_ServerValidate(object source, ServerValidateEventArgs args)
            {
                Notificacion objNotificacion = null;
                NotificacionEntity objNotificacionEntity = null;

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Verificar si cambio el usuario ingresado
                    if (this.txtNroIdentificacionAgregarEditarUsuario.Text != this.hdfNumeroIdentificacionAgregarEditarUsuario.Value)
                    {
                        //Consultar el listado de personas del acto administrativo
                        objNotificacion = new Notificacion();
                        objNotificacionEntity = objNotificacion.ConsultarInformacionActo(Convert.ToDecimal(this.hdfActoIdAgregarEditarUsuario.Value));

                        //Validar la accion realizada
                        if(Convert.ToInt32(this.hdfAccionRealizarAgregarEditarUsuario.Value) == (int)AccionesPersonas.Adicionar)
                        {
                            //Verifica si existe usuario
                            if (objNotificacionEntity.ListaPersonas != null && objNotificacionEntity.ListaPersonas.Where(persona => persona.NumeroIdentificacion.Trim() == this.txtNroIdentificacionAgregarEditarUsuario.Text.Trim()).ToList().Count() > 0)
                            {
                                args.IsValid = false;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- El número de identificación ya se encuentra registrado en el acto administrativo.')", true);
                            }
                            else
                            {
                                args.IsValid = true;
                            }

                        }
                        else if(Convert.ToInt32(this.hdfAccionRealizarAgregarEditarUsuario.Value) == (int)AccionesPersonas.Editar)
                        {

                            //Verifica si existe usuario
                            if (objNotificacionEntity.ListaPersonas.Where(persona => persona.NumeroIdentificacion.Trim() == this.txtNroIdentificacionAgregarEditarUsuario.Text.Trim()).ToList().Count() > 0)
                            {
                                //Verificar si existe usuario y la persona es la misma que se encuentra editando
                                if (objNotificacionEntity.ListaPersonas.Where(persona => persona.Id == Convert.ToDecimal(this.hdfPersonaIDAgregarEditarUsuario.Value) && persona.NumeroIdentificacion.Trim() == this.txtNroIdentificacionAgregarEditarUsuario.Text.Trim()).ToList().Count() > 0)
                                {
                                    args.IsValid = true;
                                }
                                else
                                {
                                    args.IsValid = false;
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- El número de identificación ya se encuentra registrado en el acto administrativo.')", true);
                                }
                            }
                            else
                            {
                                args.IsValid = true;
                            }


                        }
                    }                    
                    else
                    {
                        args.IsValid = true;
                    }
                }
                catch(Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdValidarUsuarioAgregarEditarUsuario_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cerrar modales
                    this.LimpiarModalAgregarEditarPersona();
                    this.LimpiarModalConfigurarPersonas();
                    this.upnlModalConfigurarPersonas.Update();
                    this.upnlModalAgregarEditarUsuario.Update();
                    this.mpeModalAgregarEditarUsuario.Hide();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error verificando validez del usuario");
                }
            }


            protected void cvAgregarEditarUsuario_ServerValidate(object source, ServerValidateEventArgs args)
            {
                Notificacion objNotificacion = null;
                NotificacionEntity objNotificacionEntity = null;
                DateTime objFechaCapturada = default(DateTime);

                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //
                    if (!string.IsNullOrWhiteSpace(this.ltlUsuarioAgregarEditarUsuario.Text))
                    {
                        //Validar que haya validado el usuario
                        if (this.txtNroIdentificacionAgregarEditarUsuario.Text.Trim() == this.hdfNumeroIdentificacionAgregarEditarUsuario.Value.Trim())
                        {
                            //Consultar la información del acto administrativo
                            objNotificacion = new Notificacion();
                            objNotificacionEntity = objNotificacion.ConsultarInformacionActo(Convert.ToDecimal(this.hdfActoIdAgregarEditarUsuario.Value));

                            //Cargar fecha capturada
                            objFechaCapturada = DateTime.ParseExact(this.txtFechaEstadoAgregarEditarUsuario.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                            //Verificar que la fecha del estado sea valida
                            if (objFechaCapturada < objNotificacionEntity.FechaActo.Value)
                            {
                                args.IsValid = false;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- La fecha del estado debe ser mayor o igual a la fecha del acto administrativo.')", true);
                            }
                            else
                            {
                                args.IsValid = true;
                            }
                        }
                        else
                        {
                            args.IsValid = false;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Antes de guardar la información de la persona debe validar el número de identificación.')", true);
                        }
                    }
                    else
                    {
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- La persona ingresada debe ser validada y encontrarse registrada en los sistemas de información.')", true);
                    }                    
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdValidarUsuarioAgregarEditarUsuario_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cerrar modales
                    this.LimpiarModalAgregarEditarPersona();
                    this.LimpiarModalConfigurarPersonas();
                    this.upnlModalConfigurarPersonas.Update();
                    this.upnlModalAgregarEditarUsuario.Update();
                    this.mpeModalAgregarEditarUsuario.Hide();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error verificando validez del usuario");
                }
            }

        #endregion


        #region ModalEliminarPersona


            /// <summary>
            /// Evento que elimina la persona
            /// </summary>
            protected void cmdModalEliminarPersonaEliminar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Eliminar la persona
                    this.EliminarPersona(Convert.ToInt64(this.hdfActoIdEliminarPersona.Value), Convert.ToInt64(this.hdfPersonaIDEliminarPersona.Value), this.txtMotivoEliminacionEliminarPersona.Text.Trim());

                    //realizar consulta de personas
                    this.CargarDatosModalConfigurarPersonas(Convert.ToInt64(this.hdfActoIdEliminarPersona.Value), Convert.ToInt32(this.hdfEstadoActoIDEliminarPersona.Value), Convert.ToInt32(this.hdfTipoNotificacionIDEliminarPersona.Value));

                    //Limpiar y cerrar modal
                    this.LimpiarModalEliminarPersona();
                    this.upnlModalEliminarPersona.Update();
                    this.mpeModalEliminarPersona.Hide();

                    //Abrir modal anterior
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Show();

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdModalEliminarPersonaEliminar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Limpiar y cerrar
                    this.LimpiarModalEliminarPersona();
                    this.LimpiarModalConfigurarPersonas();
                    this.upnlModalEliminarPersona.Update();
                    this.mpeModalEliminarPersona.Hide();
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error eliminando persona");
                }
            }

            
            /// <summary>
            /// Evento que cancela el proceso de eliminación de una persona
            /// </summary>
            protected void cmdModalEliminarPersonaCancelar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Limpiar y cerrar
                    this.LimpiarModalEliminarPersona();
                    this.upnlModalEliminarPersona.Update();
                    this.mpeModalEliminarPersona.Hide();

                    //Abrir modal anterior
                    this.mpeModalConfigurarPersonas.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PersonaNotificacion :: cmdModalEliminarPersonaCancelar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Limpiar y cerrar
                    this.LimpiarModalEliminarPersona();
                    this.LimpiarModalConfigurarPersonas();
                    this.upnlModalEliminarPersona.Update();
                    this.mpeModalEliminarPersona.Hide();
                    this.upnlModalConfigurarPersonas.Update();
                    this.mpeModalConfigurarPersonas.Hide();

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error limpiando y cerrando modal de eliminación");
                }                
            }

        #endregion


    #endregion

            
}