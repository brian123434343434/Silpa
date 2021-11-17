using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SoftManagement.Log;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Notificacion;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.LogicaNegocio.Publicacion;
using SILPA.AccesoDatos.Publicacion;
using System.Data;
using SILPA.Comun;

public partial class NotificacionElectronica_ReporteNotificaciones : System.Web.UI.Page
{
    
    
    #region Propiedades

        /// <summary>
        /// Información del reporte
        /// </summary>
        protected DataSet objReporte { get { return (DataSet)ViewState["DtsDatos"]; } set { ViewState["DtsDatos"] = value; } }

        /// <summary>
        /// Información del reporte notifiaciones
        /// </summary>
        protected DataSet objReporteNotificaciones { get { return (DataSet)ViewState["DtsDatosNotificaciones"]; } set { ViewState["DtsDatosNotificaciones"] = value; } }

    #endregion


    #region Metodos Privados

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

            Session["Usuario"] = Session["IDForToken"];

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
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.divMensaje.Visible = true;
        }


        /// <summary>
        /// Ocultar los mensajes
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.divMensaje.Visible = false;
        }


        private void CargarListaTiposDocumentales()
        {
            TipoDocumentoDalc objTipoDocumentoDalc = null;

            //Consultar el listado de tipos dicumentales y  cargarlos
            objTipoDocumentoDalc = new TipoDocumentoDalc();
            this.cboTipoActo.DataSource = objTipoDocumentoDalc.ListarTiposDeDocumentoNotificacion(null, null);
            this.cboTipoActo.DataTextField = "NOMBRE_DOCUMENTO";
            this.cboTipoActo.DataValueField = "ID";
            this.cboTipoActo.DataBind();
            this.cboTipoActo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }


        /// <summary>
        /// Inicializar la pagina
        /// </summary>
        private void InicializarPagina()
        {
            try
            {
                //Ocultar mensajes
                this.OcultarMensaje();

                //Cargar listado de tipos de actos
                this.CargarListaTiposDocumentales();

                //Inicializar fechas de busqueda
                this.txtFechaDesde.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: InicializarPagina -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error cargando la información inicial de la página");
            }

        }


        /// <summary>
        /// Carga los triggers de los links de detalles
        /// </summary>
        private void CargarTriggerDetalles()
        {
            GridView objDetalles = null;
            UpdatePanel objPanel = null;
            LinkButton objLinkDetalle = null;
            ImageButton objImagen = null;
            AsyncPostBackTrigger objTrigger = null;
            PostBackTrigger objPostBackTrigger = null;

            foreach(GridViewRow objRowReporte in this.grdExpedientes.Rows ){

                //Carga objetos
                objDetalles = (GridView)objRowReporte.FindControl("grdExpedientesDetalles");
                objPanel = (UpdatePanel)objRowReporte.FindControl("upnlReporteDetalle");

                //Si se encuentra el control ejecutar
                if (objDetalles != null && objPanel != null)
                {
                    //Cargar los links
                    foreach (GridViewRow objRowDetalle in objDetalles.Rows)
                    {
                        //Cargar imagen acto administrativo
                        objImagen = (ImageButton)objRowDetalle.FindControl("imgDescargarDocumento");

                        if (objImagen != null && objImagen.Visible)
                        {
                            objPostBackTrigger = new PostBackTrigger();
                            objPostBackTrigger.ControlID = objImagen.UniqueID;
                            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                        }
                        
                        //Cargar link notificacion
                        objLinkDetalle = (LinkButton)objRowDetalle.FindControl("lnkInformacionActo_Click");

                        if (objLinkDetalle != null && objLinkDetalle.Visible)
                        {
                            ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLinkDetalle);
                        }

                        //Cargar link notificacion
                        objLinkDetalle = (LinkButton)objRowDetalle.FindControl("lnkNotificaciones");

                        if (objLinkDetalle != null && objLinkDetalle.Visible)
                        {
                            objTrigger = new AsyncPostBackTrigger();
                            objTrigger.ControlID = objLinkDetalle.UniqueID;
                            objTrigger.EventName = "Click";
                            objPanel.Triggers.Add(objTrigger);
                        }

                        //Cargar link comunicar
                        objLinkDetalle = (LinkButton)objRowDetalle.FindControl("lnkComunicar");

                        if (objLinkDetalle != null && objLinkDetalle.Visible)
                        {
                            objTrigger = new AsyncPostBackTrigger();
                            objTrigger.ControlID = objLinkDetalle.UniqueID;
                            objTrigger.EventName = "Click";
                            objPanel.Triggers.Add(objTrigger);
                        }

                        //Cargar link cumplir
                        objLinkDetalle = (LinkButton)objRowDetalle.FindControl("lnkCumplir");

                        if (objLinkDetalle != null && objLinkDetalle.Visible)
                        {
                            objTrigger = new AsyncPostBackTrigger();
                            objTrigger.ControlID = objLinkDetalle.UniqueID;
                            objTrigger.EventName = "Click";
                            objPanel.Triggers.Add(objTrigger);
                        }

                        //Cargar link publicar
                        objLinkDetalle = (LinkButton)objRowDetalle.FindControl("lnkPublicar");

                        if (objLinkDetalle != null && objLinkDetalle.Visible)
                        {
                            objTrigger = new AsyncPostBackTrigger();
                            objTrigger.ControlID = objLinkDetalle.UniqueID;
                            objTrigger.EventName = "Click";
                            objPanel.Triggers.Add(objTrigger);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Carga los triggers de los links de detalles
        /// </summary>
        private void CargarTriggerNotificacionDetalles()
        {
            GridView objDetalles = null;
            ImageButton objImagen = null;
            PostBackTrigger objPostBackTrigger = null;

            foreach (GridViewRow objRowReporte in this.grdNotificacion.Rows)
            {

                //Carga objetos
                objDetalles = (GridView)objRowReporte.FindControl("grdNotificacionDetalles");

                //Si se encuentra el control ejecutar
                if (objDetalles != null)
                {
                    //Cargar los links
                    foreach (GridViewRow objRowDetalle in objDetalles.Rows)
                    {
                        //Cargar imagen
                        objImagen = (ImageButton)objRowDetalle.FindControl("imgDocumentoPlantilla");
                        if (objImagen != null && objImagen.Visible)
                        {
                            objPostBackTrigger = new PostBackTrigger();
                            objPostBackTrigger.ControlID = objImagen.UniqueID;
                            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                        }

                        //Cargar imagen
                        objImagen = (ImageButton)objRowDetalle.FindControl("imgDocumentoAdicional");
                        if (objImagen != null && objImagen.Visible)
                        {
                            objPostBackTrigger = new PostBackTrigger();
                            objPostBackTrigger.ControlID = objImagen.UniqueID;
                            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                        }

                        //Cargar imagen
                        objImagen = (ImageButton)objRowDetalle.FindControl("imgAdjuntos");
                        if (objImagen != null && objImagen.Visible)
                        {
                            objPostBackTrigger = new PostBackTrigger();
                            objPostBackTrigger.ControlID = objImagen.UniqueID;
                            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Realizar la consulta de las notificaciones requeridas
        /// </summary>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        /// <param name="p_strNumeroExpediente">string con el número de expediente</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_strNombreUsuario">string con el nombre del usuario</param>
        /// <param name="p_strNumeroActoAdministrativo">string con el número de acto administrativo</param>
        /// <param name="p_intTipoActoAdministrativo">int con el tipo de acto administrativo</param>
        /// <param name="p_objFechaInicio">DateTime con la fecha de inicio de busqueda</param>
        /// <param name="p_objFechaFinal">DateTime con la fecha final de busqueda</param>
        /// <param name="p_intUsuarioID">int con el id del usuario que realiza la consulta</param>
        /// <param name="p_blnConsultarNotificaciones">bool indica si se consulta notificaciones</param>
        /// <param name="p_blnConsultarComunicaciones">bool indica si se consulta comunicaciones</param>
        /// <param name="p_blnConsultarCumplase">bool que indica si se consultan cumplase</param>
        /// <param name="p_blnConsultarPublicacion">bool que indica si se consulta publicaciones</param>
        private void RealizarConsultaNotificaciones(string p_strNumeroVital, string p_strNumeroExpediente,
                                                    string p_strNumeroIdentificacion, string p_strNombreUsuario,
                                                    string p_strNumeroActoAdministrativo, int p_intTipoActoAdministrativo,
                                                    DateTime p_objFechaInicio, DateTime p_objFechaFinal,
                                                    int p_intUsuarioID, bool p_blnConsultarNotificaciones,
                                                    bool p_blnConsultarComunicaciones, bool p_blnConsultarCumplase,
                                                    bool p_blnConsultarPublicacion)
        {
            Notificacion objNotificacion = null;

            //Realizar la consulta del resporte
            objNotificacion = new Notificacion();
            this.objReporte = objNotificacion.ConsultarReporteNotificaciones(p_strNumeroVital,
                                                                      p_strNumeroExpediente,
                                                                      p_strNumeroIdentificacion,
                                                                      p_strNombreUsuario,
                                                                      p_strNumeroActoAdministrativo,
                                                                      p_intTipoActoAdministrativo,
                                                                      p_objFechaInicio,
                                                                      p_objFechaFinal,
                                                                      p_intUsuarioID,
                                                                      p_blnConsultarNotificaciones,
                                                                      p_blnConsultarComunicaciones,
                                                                      p_blnConsultarCumplase,
                                                                      p_blnConsultarPublicacion);


            //Cargar datos
            if (this.objReporte != null && this.objReporte.Tables.Count > 0)
                this.grdExpedientes.DataSource = this.objReporte.Tables[0];
            else
                this.grdExpedientes.DataSource = null;
            this.grdExpedientes.DataBind();

            //Cargar triggers detalles
            this.CargarTriggerDetalles();
        }


        /// <summary>
        /// Carga el detalle de una notificación
        /// </summary>
        /// <param name="p_strActoNotificacionID">string con el id del acto de notificación</param>
        private void CargarInformacionBitacora(string p_strActoNotificacionID)
        {
            Notificacion objNotificacion = null;

            //Realizar la consulta del resporte
            objNotificacion = new Notificacion();
            this.objReporteNotificaciones = objNotificacion.ConsultarBitacoraEstadosActoAdministrativo(long.Parse(p_strActoNotificacionID));

            //Cargar datos
            if (this.objReporteNotificaciones != null && this.objReporteNotificaciones.Tables.Count > 0 && this.objReporteNotificaciones.Tables[0].Rows.Count > 0)
            {
                this.grdBitacora.DataSource = this.objReporteNotificaciones.Tables[0];
            }
            else
                this.grdBitacora.DataSource = null;
            this.grdBitacora.PageIndex = 0;
            this.grdBitacora.DataBind();
        }


        /// <summary>
        /// Carga el detalle de una notificación
        /// </summary>
        /// <param name="p_strActoNotificacionID">string con el id del acto de notificación</param>
        private void CargarDetalleNotificacion(string p_strActoNotificacionID)
        {
            Notificacion objNotificacion = null;

            //Realizar la consulta del resporte
            objNotificacion = new Notificacion();
            this.objReporteNotificaciones = objNotificacion.ConsultarReporteDetalleNotificaciones(long.Parse(p_strActoNotificacionID), (int)TipoNotificacion.NOTIFICACION);            

            //Cargar datos
            if (this.objReporteNotificaciones != null && this.objReporteNotificaciones.Tables.Count > 0 && this.objReporteNotificaciones.Tables[0].Rows.Count > 0)
            {
                this.grdNotificacion.DataSource = this.objReporteNotificaciones.Tables[0];
                this.lblExpedienteDetalle.Text = this.objReporteNotificaciones.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
            }
            else
                this.grdNotificacion.DataSource = null;
            this.grdNotificacion.PageIndex = 0;
            this.grdNotificacion.DataBind();

            //Cargar triggers
            this.CargarTriggerNotificacionDetalles();
        }


        /// <summary>
        /// Carga el detalle de una comunicación
        /// </summary>
        /// <param name="p_strActoNotificacionID">string con el id del acto de notificación</param>
        private void CargarDetalleComunicacion(string p_strActoNotificacionID)
        {
            Notificacion objNotificacion = null;

            //Realizar la consulta del resporte
            objNotificacion = new Notificacion();
            this.objReporteNotificaciones = objNotificacion.ConsultarReporteDetalleNotificaciones(long.Parse(p_strActoNotificacionID), (int)TipoNotificacion.COMUNICACION);

            //Cargar datos
            if (this.objReporteNotificaciones != null && this.objReporteNotificaciones.Tables.Count > 0 && this.objReporteNotificaciones.Tables[0].Rows.Count > 0)
            {
                this.grdComunicacion.DataSource = this.objReporteNotificaciones.Tables[0];
                this.lblExpedienteDetalleComunicacion.Text = this.objReporteNotificaciones.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
            }
            else
                this.grdComunicacion.DataSource = null;
            this.grdComunicacion.PageIndex = 0;
            this.grdComunicacion.DataBind();
        }


        /// <summary>
        /// Carga el detalle de una comunicación
        /// </summary>
        /// <param name="p_strActoNotificacionID">string con el id del acto de notificación</param>
        private void CargarDetalleCumplimiento(string p_strActoNotificacionID)
        {
            Notificacion objNotificacion = null;

            //Realizar la consulta del resporte
            objNotificacion = new Notificacion();
            this.objReporteNotificaciones = objNotificacion.ConsultarReporteDetalleNotificaciones(long.Parse(p_strActoNotificacionID), (int)TipoNotificacion.CUMPLASE);

            //Cargar datos
            if (this.objReporteNotificaciones != null && this.objReporteNotificaciones.Tables.Count > 0 && this.objReporteNotificaciones.Tables[0].Rows.Count > 0)
            {
                this.grdCumplimiento.DataSource = this.objReporteNotificaciones.Tables[0];
                this.lblExpedienteDetalleCumplir.Text = this.objReporteNotificaciones.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
            }
            else
                this.grdCumplimiento.DataSource = null;
            this.grdCumplimiento.PageIndex = 0;
            this.grdCumplimiento.DataBind();
        }


        /// <summary>
        /// Carga el detalle de una publicacion
        /// </summary>
        /// <param name="p_lngActoNotificacionID">long con el id de la publicación</param>
        private void CargarDetallePublicacion(long p_lngActoNotificacionID)
        {
            Publicacion objPublicacion = null;
            DataSet objDatosPublicacion = null;
            TraficoDocumento objDocumentos = null;
            List<string> lstNombreArchivos = null;
            List<DetalleDocumentoIdentity> lstDocumentos = null;
            DetalleDocumentoIdentity objDocumento = null;

            //Realizar la consulta de la publicacion
            objPublicacion = new Publicacion();
            objDatosPublicacion = objPublicacion.ConsultarPublicacion(p_lngActoNotificacionID);

            //Cargar datos
            if (objDatosPublicacion != null && objDatosPublicacion.Tables != null && objDatosPublicacion.Tables.Count > 0 && objDatosPublicacion.Tables[0].Rows.Count > 0)
            {
                this.lblPublicacionTitulo.Text = objDatosPublicacion.Tables[0].Rows[0]["TITULO_PUB"].ToString();
                this.lblPublicacionNombreExpediente.Text = objDatosPublicacion.Tables[0].Rows[0]["EXP_NOMBRE"].ToString();
                if (!System.DBNull.Value.Equals(objDatosPublicacion.Tables[0].Rows[0]["FECHA_FIJACION"]))
                    this.lblPublicacionFecha.Text = Convert.ToDateTime(objDatosPublicacion.Tables[0].Rows[0]["FECHA_FIJACION"]).ToString("dd/MM/yyyy hh:mm:ss");
                else
                    this.lblPublicacionFecha.Text = "-";
                if (!System.DBNull.Value.Equals(objDatosPublicacion.Tables[0].Rows[0]["FECHA_DESFIJACION"]))
                    this.lblPublicacionFechaRetiro.Text = Convert.ToDateTime(objDatosPublicacion.Tables[0].Rows[0]["FECHA_DESFIJACION"]).ToString("dd/MM/yyyy hh:mm:ss");
                else
                    this.lblPublicacionFechaRetiro.Text = "-";
                this.lblPublicacionDescripcion.Text = objDatosPublicacion.Tables[0].Rows[0]["DESCRIPCION_PUB"].ToString();

                //Cargar información de documentos
                if (!System.DBNull.Value.Equals(objDatosPublicacion.Tables[0].Rows[0]["RUTA_PUB"]) && !string.IsNullOrWhiteSpace(objDatosPublicacion.Tables[0].Rows[0]["RUTA_PUB"].ToString()))
                {
                    objDocumentos = new TraficoDocumento();
                    lstNombreArchivos = objDocumentos.ListarDocumentosDirectorio(objDatosPublicacion.Tables[0].Rows[0]["RUTA_PUB"].ToString().Trim());

                    //Si hay documentos cargarlos
                    if (lstNombreArchivos != null && lstNombreArchivos.Count > 0)
                    {
                        //Crear listado
                        lstDocumentos = new List<DetalleDocumentoIdentity>();

                        //Ciclo que carga los documentos
                        foreach (string strArchivo in lstNombreArchivos)
                        {
                            objDocumento = new DetalleDocumentoIdentity();
                            objDocumento.Ubicacion = strArchivo;
                            objDocumento.NombreArchivo = System.IO.Path.GetFileName(strArchivo);
                            lstDocumentos.Add(objDocumento);
                        }

                        //Cargar registros en sesion
                        Session["ListaDocumentos"] = lstDocumentos;

                        //Mostrar link
                        this.trDetalleDocumentosPublicacion.Visible = true;
                    }
                    else
                    {
                        //Ocultar link
                        this.trDetalleDocumentosPublicacion.Visible = false;
                    }
                }
                else
                {
                    this.trDetalleDocumentosPublicacion.Visible = false;
                }

            }
            else
            {
                //Limpiar datos
                this.lblPublicacionTitulo.Text = "";
                this.lblPublicacionNombreExpediente.Text = "";
                this.lblPublicacionFecha.Text = "";
                this.lblPublicacionFechaRetiro.Text = "";
                this.lblPublicacionDescripcion.Text = "";
                this.trDetalleDocumentosPublicacion.Visible = false;
            }
        }


        /// <summary>
        /// Limpia información del modal de ver
        /// </summary>
        public void LimpiarModalVerPublicacionEstado()
        {
            //Limpiar campos
            this.ltlNumeroIdentificacionModal.Text = "";
            this.ltlNombrePersonaModal.Text = "";
            this.ltlAutoridadAmbientalModal.Text = "";
            this.ltlTipoTramiteModal.Text = "";
            this.ltlNumeroVitalModal.Text = "";
            this.ltlNumeroExpedienteModal.Text = "";
            this.ltlNombreProyectoModal.Text = "";
            this.ltlTipoDocumentoModal.Text = "";
            this.ltlNumeroDocumentoModal.Text = "";
            this.ltlPublicacionModal.Text = "";
            this.ltlFechaPublicacionModal.Text = "";
            this.ltlFechaDesfijacion.Text = "";
            this.hdfTipoNotificacionModalPublicacion.Value = "";

            //Limpiar grillas
            this.grdDocumentosModal.DataSource = null;
            this.grdDocumentosModal.DataBind();
            this.grdAdjuntosModal.DataSource = null;
            this.grdAdjuntosModal.DataBind();

            //Ocultar div
            this.dvFechaDesfijacion.Visible = false;
            this.dvDocumentosPublicacion.Visible = false;
            this.dvAdjuntosPublicacion.Visible = false;
        }


        /// <summary>
        /// Limpia información del modal de ver
        /// </summary>
        public void LimpiarModalVerDetalleEstado()
        {
            //Limpiar campos controles
            this.ltlNumeroVital.Text = "";
            this.ltlNumeroExpediente.Text = "";
            this.ltlActoAdministrativo.Text = "";
            this.ltlNumeroActo.Text = "";
            this.ltlIdentificacion.Text = "";
            this.ltlUsuario.Text = "";
            this.ltlEstadoActual.Text = "";
            this.ltlFechaEstadoActual.Text = "";
            this.ltlObservacion.Text = "";
            this.ltlEnviarDireccion.Text = "";
            this.grdDirecciones.DataSource = null;
            this.grdDirecciones.DataBind();
            this.ltlEnviarCorreo.Text = "";
            this.grdCorreos.DataSource = null;
            this.grdCorreos.DataBind();
            this.grdDocumentos.DataSource = null;
            this.grdDocumentos.DataBind();
            this.ltlAdjuntarActoAdministrativo.Text = "";
            this.ltlAdjuntarConceptosActoAdministrativo.Text = "";
            this.ltlTextoCorreo.Text = "";
            this.ltlReferencia.Text = "";
            this.ltlFechaReferencia.Text = "";
            this.ltlTipoIdentificacionPersonaNotificar.Text = "";
            this.ltlNumeroIdentificacionPersonaNotificar.Text = "";
            this.ltlNombrePersonaNotificar.Text = "";
            this.ltlCalidadPersonaNotificar.Text = "";

            //Ocultar div
            this.dvAdjuntos.Visible = false;
            this.dvTextoCorreo.Visible = false;
            this.dvReferenciaRecepcion.Visible = false;
            this.dvTipoIdentificacionPersonaNotificar.Visible = false;
            this.dvNumeroIdentificacionPersonaNotificar.Visible = false;
            this.dvNombrePersonaNotificar.Visible = false;
            this.dvCalidadPersonaNotificar.Visible = false;
            this.dvFechaRecepcion.Visible = false;
            this.dvListadoDirecciones.Visible = false;
            this.dvListaCorreos.Visible = false;
            this.dvAdjuntarActoAdministrativo.Visible = false;
            this.dvAdjuntarConceptosActoAdministrativo.Visible = false;
            this.dvDocumentos.Visible = false;
        }

        /// <summary>
        /// Limpiar los componentes del modal de visualización de adjuntos
        /// </summary>
        private void LimpiarModalVerAdjuntos()
        {
            this.grdDocumentosAdjuntosVer.DataSource = null;
            this.grdDocumentosAdjuntosVer.DataBind();
        }

        /// <summary>
        /// Crear tabla de de documentos
        /// </summary>
        /// <returns>DataTable vacio</returns>
        private DataTable CrearTablaDocumentosPublicacionEstados()
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
        /// Cargar los trigger de la grilla especificada
        /// </summary>
        private void CargarTriggersDocumentoGrillaPublicacionEstados(GridView p_objGridView)
        {
            ImageButton objImagen = null;
            PostBackTrigger objPostTrigger = null;

            //Verificar que la grilla contenga información
            if (p_objGridView != null && p_objGridView.Rows.Count > 0)
            {
                //Ciclo que adiciona los triggers de la grilla
                foreach (GridViewRow objRowDocumento in p_objGridView.Rows)
                {
                    //Cargar imagen de descarga
                    objImagen = (ImageButton)objRowDocumento.FindControl("imgDocumentoModal");

                    //Si existe el control y se encuentra visible
                    if (objImagen != null)
                    {
                        objPostTrigger = new PostBackTrigger();
                        objPostTrigger.ControlID = objImagen.UniqueID;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                    }
                }
            }

        }

        /// <summary>
        /// Consultar la información de una publicación y mostrar en pantalla
        /// </summary>
        /// <param name="lngPublicacionEstadoPersonaActoId">long con el identificador de la publicacion</param>
        private void ConsultarInformacionPublicacionEstado(long lngPublicacionEstadoPersonaActoId)
        {
            PublicidadEstados objPublicidadEstados = null;
            DataTable objInformacionPublicacion = null;
            DataTable objDocumentos = null;
            DataRow objDocumento = null;

            //Realizar consulta de la informacion
            objPublicidadEstados = new PublicidadEstados();
            objInformacionPublicacion = objPublicidadEstados.ConsultarInformacionPublicacion(lngPublicacionEstadoPersonaActoId);

            //Verificar que se obtenga informacion
            if (objInformacionPublicacion != null && objInformacionPublicacion.Rows.Count > 0)
            {
                //Cargar datos en campos del modal
                this.ltlNumeroIdentificacionModal.Text = objInformacionPublicacion.Rows[0]["NUMERO_IDENTIFICACION"].ToString();
                this.ltlNombrePersonaModal.Text = objInformacionPublicacion.Rows[0]["NOMBRE_PERSONA"].ToString();
                this.ltlAutoridadAmbientalModal.Text = objInformacionPublicacion.Rows[0]["NOMBRE_AUTORIDAD"].ToString();
                this.ltlTipoTramiteModal.Text = objInformacionPublicacion.Rows[0]["NOMBRE_TIPO_TRAMITE"].ToString();
                this.ltlNumeroVitalModal.Text = objInformacionPublicacion.Rows[0]["NUMERO_VITAL"].ToString();
                this.ltlNumeroExpedienteModal.Text = objInformacionPublicacion.Rows[0]["EXPEDIENTE"].ToString();
                this.ltlNombreProyectoModal.Text = objInformacionPublicacion.Rows[0]["NOMBRE_PROYECTO"].ToString();
                this.ltlTipoDocumentoModal.Text = objInformacionPublicacion.Rows[0]["TIPO_DOCUMENTO"].ToString();
                this.ltlNumeroDocumentoModal.Text = objInformacionPublicacion.Rows[0]["NUMERO_DOCUMENTO"].ToString();
                this.ltlPublicacionModal.Text = objInformacionPublicacion.Rows[0]["PUBLICACION"].ToString();
                this.ltlFechaPublicacionModal.Text = Convert.ToDateTime(objInformacionPublicacion.Rows[0]["FECHA_FIJACION"]).ToString("dd/MM/yyyy");
                if (objInformacionPublicacion.Rows[0]["FECHA_DESFIJACION"] != System.DBNull.Value)
                {
                    this.ltlFechaDesfijacion.Text = Convert.ToDateTime(objInformacionPublicacion.Rows[0]["FECHA_DESFIJACION"]).ToString("dd/MM/yyyy");
                    this.dvFechaDesfijacion.Visible = true;
                }
                if (Convert.ToBoolean(objInformacionPublicacion.Rows[0]["PUBLICA_DOCUMENTO"]) &&
                    ((!string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString()) && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("\\")) ||
                      !string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString()) && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("\\")))
                {
                    //CRear tabla
                    objDocumentos = this.CrearTablaDocumentosPublicacionEstados();

                    //Adicionar documentos
                    if (!string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString()) && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("\\"))
                    {
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().Substring((objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") != -1 ? objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") : objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }
                    if (!string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString()) && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("\\"))
                    {
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().Substring((objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("/") != -1 ? objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("/") : objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Mostrar en grilla
                    this.grdDocumentosModal.DataSource = objDocumentos;
                    this.grdDocumentosModal.DataBind();

                    //Cargar triggers
                    this.CargarTriggersDocumentoGrillaPublicacionEstados(this.grdDocumentosModal);

                    //Mostrar div
                    this.dvDocumentosPublicacion.Visible = true;
                }
                if (Convert.ToBoolean(objInformacionPublicacion.Rows[0]["PUBLICA_ADJUNTO"]) && !string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString()) &&
                    !objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().ToString().EndsWith("\\"))
                {
                    //Cargar datos
                    objDocumento = objDocumentos.NewRow();
                    objDocumento["NOMBRE_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().Substring((objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().LastIndexOf("/") != -1 ? objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().LastIndexOf("/") : objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().LastIndexOf("\\") + 1));
                    objDocumento["RUTA_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString();
                    objDocumentos.Rows.Add(objDocumento);

                    //Mostrar en grilla
                    this.grdAdjuntosModal.DataSource = objDocumentos;
                    this.grdAdjuntosModal.DataBind();

                    //Cargar triggers
                    this.CargarTriggersDocumentoGrillaPublicacionEstados(this.grdAdjuntosModal);

                    //Mostrar div
                    this.dvAdjuntosPublicacion.Visible = true;
                }

            }
            else
            {
                throw new Exception("No se encontro información de la publicación " + lngPublicacionEstadoPersonaActoId.ToString());
            }
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
        /// Cargar los trigger de la grilla especificada
        /// </summary>
        private void CargarTriggersDocumentoGrilla(GridView p_objGridView)
        {
            ImageButton objImagen = null;
            PostBackTrigger objPostTrigger = null;

            //Verificar que la grilla contenga información
            if (p_objGridView != null && p_objGridView.Rows.Count > 0)
            {
                //Ciclo que adiciona los triggers de la grilla
                foreach (GridViewRow objRowDocumento in p_objGridView.Rows)
                {
                    //Cargar imagen de descarga
                    objImagen = (ImageButton)objRowDocumento.FindControl("imgDocumentoModal");

                    //Si existe el control y se encuentra visible
                    if (objImagen != null)
                    {
                        objPostTrigger = new PostBackTrigger();
                        objPostTrigger.ControlID = objImagen.UniqueID;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                    }
                }
            }

        }

        /// <summary>
        /// Cargar la información a mostrar en el modal de detalle estado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
        private void ConsultarInformacionDetalleEstado(long p_lngEstadoPersonaActoID)
        {
            Notificacion objNotificacion = null;
            DataSet objInformacionActo = null;
            SILPA.LogicaNegocio.Notificacion.EstadoNotificacion objEstadoNotificacion = null;
            List<DireccionNotificacionEntity> objLstDirecciones = null;
            List<CorreoNotificacionEntity> objLstCorreos = null;
            DataTable objDocumentos = null;
            DataRow objDocumento = null;
            DataTable objConceptosActo = null;

            //Cargar la información del acto
            objNotificacion = new Notificacion();
            objInformacionActo = objNotificacion.ObtenerInformacionEstadoPersonaActo(p_lngEstadoPersonaActoID);

            //Validar que se obtenga información
            if (objInformacionActo != null && objInformacionActo.Tables != null && objInformacionActo.Tables[0].Rows.Count > 0)
            {
                //Cardar datos
                this.ltlNumeroVital.Text = objInformacionActo.Tables[0].Rows[0]["NUM_VITAL"].ToString();
                this.ltlNumeroExpediente.Text = objInformacionActo.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
                this.ltlActoAdministrativo.Text = objInformacionActo.Tables[0].Rows[0]["TIPO_ACTO_ADMINISTRATIVO"].ToString();
                this.ltlNumeroActo.Text = objInformacionActo.Tables[0].Rows[0]["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString();
                this.ltlIdentificacion.Text = objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString();
                this.ltlUsuario.Text = objInformacionActo.Tables[0].Rows[0]["USUARIO_NOTIFICAR"].ToString();
                this.ltlEstadoActual.Text = objInformacionActo.Tables[0].Rows[0]["ESTADO"].ToString();
                this.ltlFechaEstadoActual.Text = Convert.ToDateTime(objInformacionActo.Tables[0].Rows[0]["FECHA_ESTADO"]).ToString("dd/MM/yyyy HH:mm");
                this.ltlObservacion.Text = (objInformacionActo.Tables[0].Rows[0]["OBSERVACION"] != System.DBNull.Value ? objInformacionActo.Tables[0].Rows[0]["OBSERVACION"].ToString() : "-");                
                this.dvIdentificacion.Visible = true;
                this.dvUsuario.Visible = true;
                
                //Cargar datos de documentos
                objDocumentos = this.CrearTablaDocumentos();
                if (!string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString()) && !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().ToString().EndsWith("/") && !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().ToString().EndsWith("\\"))
                {
                    objDocumento = objDocumentos.NewRow();
                    objDocumento["NOMBRE_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().Substring((objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().LastIndexOf("/") != -1 ? objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().LastIndexOf("/") : objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().LastIndexOf("\\") + 1));
                    objDocumento["RUTA_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString();
                    objDocumentos.Rows.Add(objDocumento);
                }
                if (!string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString()) && !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("/") && !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("\\"))
                {
                    objDocumento = objDocumentos.NewRow();
                    objDocumento["NOMBRE_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().Substring((objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("/") != -1 ? objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("/") : objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("\\") + 1));
                    objDocumento["RUTA_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString();
                    objDocumentos.Rows.Add(objDocumento);
                }

                //Mostrar en grilla
                if (objDocumentos.Rows.Count > 0)
                {
                    this.grdDocumentos.DataSource = objDocumentos;
                    this.grdDocumentos.DataBind();
                    this.dvDocumentos.Visible = true;

                    //Cargar triggers
                    this.CargarTriggersDocumentoGrilla(this.grdDocumentos);
                }
                else
                {
                    this.grdDocumentos.DataSource = null;
                    this.grdDocumentos.DataBind();
                    this.dvDocumentos.Visible = false;
                }

                //Verificar si envía a dirección
                if (objInformacionActo.Tables[0].Rows[0]["ENVIA_DIRECCION"] != System.DBNull.Value && Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ENVIA_DIRECCION"]))
                {
                    this.ltlEnviarDireccion.Text = "Si";

                    //Cargar información de direcciones
                    objEstadoNotificacion = new SILPA.LogicaNegocio.Notificacion.EstadoNotificacion();
                    objLstDirecciones = objEstadoNotificacion.ConsultarDireccionesEstadoPersonaActo(p_lngEstadoPersonaActoID);
                    this.grdDirecciones.DataSource = objLstDirecciones;
                    this.grdDirecciones.DataBind();
                    this.dvListadoDirecciones.Visible = true;                    
                }
                else
                {
                    this.ltlEnviarDireccion.Text = "No";
                    this.grdDirecciones.DataSource = null;
                    this.grdDirecciones.DataBind();
                    this.dvListadoDirecciones.Visible = false;                    
                }

                //Verificar si envía a correo
                if (objInformacionActo.Tables[0].Rows[0]["ENVIA_CORREO"] != System.DBNull.Value && Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ENVIA_CORREO"]))
                {
                    this.ltlEnviarCorreo.Text = "Si";

                    //Cargar información de direcciones
                    objEstadoNotificacion = new SILPA.LogicaNegocio.Notificacion.EstadoNotificacion();
                    objLstCorreos = objEstadoNotificacion.ConsultarCorreosEstadoPersonaActo(p_lngEstadoPersonaActoID);
                    this.grdCorreos.DataSource = objLstCorreos;
                    this.grdCorreos.DataBind();
                    this.dvListaCorreos.Visible = true;

                    //Cargar datos de adjuntos
                    this.ltlAdjuntarActoAdministrativo.Text = (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ADJUNTO_INCLUYE_ACTO"]) ? "Si" : "No");
                    this.dvAdjuntarActoAdministrativo.Visible = true;
                    this.ltlAdjuntarConceptosActoAdministrativo.Text = (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ADJUNTO_INCLUYE_CONCEPTOS_ACTO"]) ? "Si" : "No");
                    this.dvAdjuntarConceptosActoAdministrativo.Visible = true;
                    this.dvAdjuntos.Visible = true;

                    //Crear tabla
                    objDocumentos = this.CrearTablaDocumentos();

                    //Cargar acto administrativo
                    if (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ADJUNTO_INCLUYE_ACTO"]) &&
                       !string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString()) &&
                       !objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().ToString().EndsWith("/") && !objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().ToString().EndsWith("\\"))
                    {

                        //Cargar datos
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().Substring((objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("/") != -1 ? objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("/") : objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Adicionar conceptos en caso de que existan
                    if (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ADJUNTO_INCLUYE_CONCEPTOS_ACTO"]))
                    {
                        //Consultar conceptos
                        objConceptosActo = objNotificacion.ConsultarConceptosAsociadosActoAdministrativo(Convert.ToInt64(objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"]));

                        foreach (DataRow objConcepto in objConceptosActo.Rows)
                        {
                            objDocumento = objDocumentos.NewRow();
                            objDocumento["NOMBRE_DOCUMENTO"] = objConcepto["DOC_ARCHIVO"].ToString();
                            if (Convert.ToInt32(objConcepto["NOT_AUT_ID"]) == (int)AutoridadesAmbientales.ANLA)
                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILA@" + objConcepto["DOC_ID"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_PERSONA"].ToString() + "_" + p_lngEstadoPersonaActoID.ToString();
                            else
                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILAMC@" + objConcepto["DOC_ID"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_PERSONA"].ToString() + "_" + p_lngEstadoPersonaActoID.ToString();
                            objDocumentos.Rows.Add(objDocumento);
                        }
                    }

                    //Cargar adjuntos
                    if (!string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString()) &&
                       !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("/") && !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("\\"))
                    {

                        //Cargar datos
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().Substring((objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") != -1 ? objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") : objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Validar si se incluyeron adjuntos
                    if (objDocumentos.Rows.Count > 0)
                    {
                        //Mostrar en grilla
                        this.grdAdjuntos.DataSource = objDocumentos;
                        this.grdAdjuntos.DataBind();

                        //Cargar triggers
                        this.CargarTriggersDocumentoGrilla(this.grdAdjuntos);

                        //Mostrar div
                        this.dvAdjuntos.Visible = true;
                    }
                    else
                    {
                        //Limpiar grilla
                        this.grdAdjuntos.DataSource = null;
                        this.grdAdjuntos.DataBind();

                        //Ocultar div
                        this.dvAdjuntos.Visible = false;
                    }

                    //Mostrar texto correo
                    this.dvTextoCorreo.Visible = true;
                    this.ltlTextoCorreo.Text = (objLstCorreos.Count > 0 ? objLstCorreos[0].Texto : "-");
                }
                else
                {
                    this.ltlEnviarCorreo.Text = "No";
                    this.grdCorreos.DataSource = null;
                    this.grdCorreos.DataBind();
                    this.dvListaCorreos.Visible = false;
                    this.ltlAdjuntarActoAdministrativo.Text = "";
                    this.dvAdjuntarActoAdministrativo.Visible = false;
                    this.ltlAdjuntarConceptosActoAdministrativo.Text = "";
                    this.dvAdjuntarConceptosActoAdministrativo.Visible = false;
                    this.dvAdjuntos.Visible = false;
                    this.dvTextoCorreo.Visible = false;
                    this.ltlTextoCorreo.Text = "";
                }

                //Referencia
                if (objInformacionActo.Tables[0].Rows[0]["REFERENCIA"] != System.DBNull.Value && !string.IsNullOrWhiteSpace(objInformacionActo.Tables[0].Rows[0]["REFERENCIA"].ToString()))
                {
                    this.dvReferenciaRecepcion.Visible = true;
                    this.ltlReferencia.Text = objInformacionActo.Tables[0].Rows[0]["REFERENCIA"].ToString();
                }
                else
                {
                    this.dvReferenciaRecepcion.Visible = false;
                    this.ltlReferencia.Text = "";
                }

                //Fecha de Referencia
                if (objInformacionActo.Tables[0].Rows[0]["FECHA_REFERENCIA"] != System.DBNull.Value)
                {
                    this.dvFechaRecepcion.Visible = true;
                    this.ltlFechaReferencia.Text = Convert.ToDateTime(objInformacionActo.Tables[0].Rows[0]["FECHA_REFERENCIA"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    this.dvFechaRecepcion.Visible = false;
                    this.ltlFechaReferencia.Text = "";
                }

                //Tipo Identificación Persona Notificar
                if (objInformacionActo.Tables[0].Rows[0]["TIPO_IDENTIFICACION_PERSONA_NOTIFICADA"] != System.DBNull.Value && !string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["TIPO_IDENTIFICACION_PERSONA_NOTIFICADA"].ToString()))
                {
                    this.dvTipoIdentificacionPersonaNotificar.Visible = true;
                    this.ltlTipoIdentificacionPersonaNotificar.Text = objInformacionActo.Tables[0].Rows[0]["TIPO_IDENTIFICACION_PERSONA_NOTIFICADA"].ToString();
                }
                else
                {
                    this.dvTipoIdentificacionPersonaNotificar.Visible = false;
                    this.ltlTipoIdentificacionPersonaNotificar.Text = "";
                }

                //Numero Identificación Persona Notificar
                if (objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_PERSONA_NOTIFICADA"] != System.DBNull.Value && !string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_PERSONA_NOTIFICADA"].ToString()))
                {
                    this.dvNumeroIdentificacionPersonaNotificar.Visible = true;
                    this.ltlNumeroIdentificacionPersonaNotificar.Text = objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_PERSONA_NOTIFICADA"].ToString();
                }
                else
                {
                    this.dvNumeroIdentificacionPersonaNotificar.Visible = false;
                    this.ltlNumeroIdentificacionPersonaNotificar.Text = "";
                }

                //Nombre Identificación Persona Notificar
                if (objInformacionActo.Tables[0].Rows[0]["NOMBRE_PERSONA_NOTIFICADA"] != System.DBNull.Value && !string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["NOMBRE_PERSONA_NOTIFICADA"].ToString()))
                {
                    this.dvNombrePersonaNotificar.Visible = true;
                    this.ltlNombrePersonaNotificar.Text = objInformacionActo.Tables[0].Rows[0]["NOMBRE_PERSONA_NOTIFICADA"].ToString();
                }
                else
                {
                    this.dvNombrePersonaNotificar.Visible = false;
                    this.ltlNombrePersonaNotificar.Text = "";
                }

                //Calidad Identificación Persona Notificar
                if (objInformacionActo.Tables[0].Rows[0]["CALIDAD_PERSONA_NOTIFICADA"] != System.DBNull.Value && !string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["CALIDAD_PERSONA_NOTIFICADA"].ToString()))
                {
                    this.dvCalidadPersonaNotificar.Visible = true;
                    this.ltlCalidadPersonaNotificar.Text = objInformacionActo.Tables[0].Rows[0]["CALIDAD_PERSONA_NOTIFICADA"].ToString();
                }
                else
                {
                    this.dvCalidadPersonaNotificar.Visible = false;
                    this.ltlCalidadPersonaNotificar.Text = "";
                }
            }
            else
            {
                throw new Exception("No se encontro información del estado");
            }
        }


        /// <summary>
        /// Cargar los trigger de la grilla especificada
        /// </summary>
        private void CargarTriggersDocumentosAdjuntosVerGrilla(GridView p_objGridView)
        {
            ImageButton objImagen = null;
            PostBackTrigger objPostTrigger = null;

            //Verificar que la grilla contenga información
            if (p_objGridView != null && p_objGridView.Rows.Count > 0)
            {
                //Ciclo que adiciona los triggers de la grilla
                foreach (GridViewRow objRowDocumento in p_objGridView.Rows)
                {
                    //Cargar imagen de descarga
                    objImagen = (ImageButton)objRowDocumento.FindControl("imgDescargarDocumentoAdjuntoVer");

                    //Si existe el control y se encuentra visible
                    if (objImagen != null)
                    {
                        objPostTrigger = new PostBackTrigger();
                        objPostTrigger.ControlID = objImagen.UniqueID;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                    }
                }
            }

        }

        /// <summary>
        /// Cargar la información a mostrar en el modal de editar estado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado a editar</param>
        private void CargarDatosModalVerAdjuntos(long p_lngEstadoPersonaActoID)
        {
            Notificacion objNotificacion = null;
            DataSet objInformacionActo = null;
            DataTable objDocumentos = null;
            DataTable objConceptosActo = null;
            DataRow objDocumento = null;

            //Cargar la información del acto
            objNotificacion = new Notificacion();
            objInformacionActo = objNotificacion.ObtenerInformacionEstadoPersonaActo(p_lngEstadoPersonaActoID);

            //Validar que se obtenga información
            if (objInformacionActo != null && objInformacionActo.Tables != null && objInformacionActo.Tables[0].Rows.Count > 0)
            {
                //Verificar si envía a correo
                if (objInformacionActo.Tables[0].Rows[0]["ENVIA_CORREO"] != System.DBNull.Value && Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ENVIA_CORREO"]))
                {
                    //Crear tabla
                    objDocumentos = this.CrearTablaDocumentos();

                    //Cargar acto administrativo
                    if (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ADJUNTO_INCLUYE_ACTO"]) &&
                       !string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString()) &&
                       !objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().ToString().EndsWith("/") && !objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().ToString().EndsWith("\\"))
                    {

                        //Cargar datos
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().Substring((objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("/") != -1 ? objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("/") : objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Adicionar conceptos en caso de que existan
                    if (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ADJUNTO_INCLUYE_CONCEPTOS_ACTO"]))
                    {
                        //Crear objeto de notificacion
                        objNotificacion = new Notificacion();

                        //Consultar conceptos
                        objConceptosActo = objNotificacion.ConsultarConceptosAsociadosActoAdministrativo(Convert.ToInt64(objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"]));

                        foreach (DataRow objConcepto in objConceptosActo.Rows)
                        {
                            objDocumento = objDocumentos.NewRow();
                            objDocumento["NOMBRE_DOCUMENTO"] = objConcepto["DOC_ARCHIVO"].ToString();
                            if (Convert.ToInt32(objConcepto["NOT_AUT_ID"]) == (int)AutoridadesAmbientales.ANLA)
                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILA@" + objConcepto["DOC_ID"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_PERSONA"].ToString() + "_" + p_lngEstadoPersonaActoID.ToString();
                            else
                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILAMC@" + objConcepto["DOC_ID"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_PERSONA"].ToString() + "_" + p_lngEstadoPersonaActoID.ToString();
                            objDocumentos.Rows.Add(objDocumento);
                        }
                    }

                    //Cargar adjuntos
                    if (!string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString()) &&
                       !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("/") && !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("\\"))
                    {

                        //Cargar datos
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().Substring((objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") != -1 ? objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") : objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Validar si se incluyeron adjuntos
                    if (objDocumentos.Rows.Count > 0)
                    {
                        //Mostrar en grilla
                        this.grdDocumentosAdjuntosVer.DataSource = objDocumentos;
                        this.grdDocumentosAdjuntosVer.DataBind();

                        //Cargar triggers
                        this.CargarTriggersDocumentosAdjuntosVerGrilla(this.grdDocumentosAdjuntosVer);
                    }
                    else
                    {
                        //Limpiar grilla
                        this.grdDocumentosAdjuntosVer.DataSource = null;
                        this.grdDocumentosAdjuntosVer.DataBind();
                    }
                }
                else
                {
                    //Limpiar grlla
                    this.grdDocumentosAdjuntosVer.DataSource = null;
                    this.grdDocumentosAdjuntosVer.DataBind();
                }
            }
            else
            {
                throw new Exception("No se enocntro información del estado " + p_lngEstadoPersonaActoID.ToString());
            }
        }

        /// <summary>
        /// Retorna una tabla con la información de correos de un estado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
        /// <returns>string con la información de correos</returns>
        private string CrearTablaCorreos(long p_lngEstadoPersonaActoID)
        {
            string strCorreos = "";
            SILPA.LogicaNegocio.Notificacion.EstadoNotificacion objEstadoNotificacion = null;
            List<CorreoNotificacionEntity> objLstCorreos = null;

            //Cargar encabezado tabla
            strCorreos = "<table class='TablaBurbujaNotificacion'>";
            strCorreos += "<tr class='TituloTablaBurbujaNotificacion'><td>Correo</td><td>Fecha Envío</td></tr>";

            //Consultar correos
            objEstadoNotificacion = new SILPA.LogicaNegocio.Notificacion.EstadoNotificacion();
            objLstCorreos = objEstadoNotificacion.ConsultarCorreosEstadoPersonaActo(p_lngEstadoPersonaActoID);

            //Validar si se encontro información
            if (objLstCorreos != null && objLstCorreos.Count > 0)
            {
                foreach (CorreoNotificacionEntity objCorreo in objLstCorreos)
                {
                    strCorreos += "<tr class='ContenidoTablaBurbujaNotificacion'><td>" + objCorreo.Correo.Replace("\"", "'") + "</td><td>" + objCorreo.FechaEnvío.ToString("dd/MM/yyyy HH:mm") + "</td></tr>";
                }


            }
            else
            {
                strCorreos += "<tr class='ContenidoTablaBurbujaNotificacion'><td colspan='2'>No se encuentra información de los correos envíados</td></tr>";
            }


            //Cerrar tabla
            strCorreos += "</table>";

            return strCorreos;
        }

        /// <summary>
        /// Retorna una tabla con la información de direcciones de un estado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
        /// <returns>string con la información de direcciones</returns>
        private string CrearTablaDirecciones(long p_lngEstadoPersonaActoID)
        {
            string strDirecciones = "";

            SILPA.LogicaNegocio.Notificacion.EstadoNotificacion objEstadoNotificacion = null;
            List<DireccionNotificacionEntity> objLstDirecciones = null;

            //Cargar encabezado tabla
            strDirecciones = "<table class='TablaBurbujaNotificacion'>";
            strDirecciones += "<tr class='TituloTablaBurbujaNotificacion'><td>Departamento</td><td>Municipio</td><td>Dirección</td></tr>";

            //Consultar correos
            objEstadoNotificacion = new SILPA.LogicaNegocio.Notificacion.EstadoNotificacion();
            objLstDirecciones = objEstadoNotificacion.ConsultarDireccionesEstadoPersonaActo(p_lngEstadoPersonaActoID);

            //Validar si se encontro información
            if (objLstDirecciones != null && objLstDirecciones.Count > 0)
            {
                foreach (DireccionNotificacionEntity objDireccion in objLstDirecciones)
                {
                    strDirecciones += "<tr class='ContenidoTablaBurbujaNotificacion'><td>" + objDireccion.Departamento.Replace("\"", "'") + "</td><td>" + objDireccion.Municipio.Replace("\"", "'") + "</td><td>" + objDireccion.Direccion.Replace("\"", "'") + "</td></tr>";
                }


            }
            else
            {
                strDirecciones += "<tr class='ContenidoTablaBurbujaNotificacion'><td colspan='3'>No se encuentra información de las direcciones a las cuales se envío información</td></tr>";
            }


            //Cerrar tabla
            strDirecciones += "</table>";

            return strDirecciones;
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
            /// Evento que se ejecuta al cargar la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                //Session["Usuario"] = "434";

                if (!IsPostBack)
                {
                    //InicializarPagina();

                    //Validar sesion de usuario
                    if (this.ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                    else
                    {
                        //Iniciliazar datos de la página
                        InicializarPagina();
                    }
                }
            }

        #endregion


        #region CmdBuscar

            /// <summary>
            /// Evento que se eejcuta al dar clic en el botón de Buscar
            /// </summary>
            protected void cmdBuscar_Click(object sender, EventArgs e)
            {
                string strNumeroVital = "";
                string strNumeroExpediente = "";
                string strNumeroIdentificacion = "";
                string strNombreUsuario = "";
                string strNumeroActoAdministrativo = "";
                int intTipoActoAdministrativo = 0;
                DateTime objFechaInicio = DateTime.Today;
                DateTime objFechaFinal = DateTime.Today;
                int intUsuarioID = 0;
                bool blnConsultarNotioficaciones = false;
                bool blnConsultarComunicaciones = false;
                bool blnConsultarCumplase = false;
                bool blnConsultarPublicacion = false;

                try
                {           
                    //Limpiar mensajes
                    this.OcultarMensaje();

                    //Cargar los datos de busqueda
                    strNumeroVital = this.txtNumeroVital.Text.Trim();
                    strNumeroExpediente = this.txtExpediente.Text.Trim();
                    strNumeroIdentificacion = this.txtIdentificacionUsuario.Text.Trim();
                    strNombreUsuario = this.txtUsuarioNotificar.Text.Trim();
                    strNumeroActoAdministrativo = this.txtNumeroActo.Text.Trim();
                    intTipoActoAdministrativo = (this.cboTipoActo.SelectedValue == "-1" ? 0 : int.Parse(this.cboTipoActo.SelectedValue));
                    intUsuarioID = Convert.ToInt32(Session["Usuario"]);
                    blnConsultarNotioficaciones = true;
                    blnConsultarComunicaciones = true;
                    blnConsultarCumplase = true;
                    blnConsultarPublicacion = true;

                    //Cargar las fechas
                    objFechaInicio = Convert.ToDateTime(this.txtFechaDesde.Text);
                    objFechaFinal = Convert.ToDateTime(this.txtFechaHasta.Text);

                    //Realizar la consulta
                    this.RealizarConsultaNotificaciones(strNumeroVital,
                                                        strNumeroExpediente,
                                                        strNumeroIdentificacion,
                                                        strNombreUsuario,
                                                        strNumeroActoAdministrativo,
                                                        intTipoActoAdministrativo,
                                                        objFechaInicio,
                                                        objFechaFinal,
                                                        intUsuarioID,
                                                        blnConsultarNotioficaciones,
                                                        blnConsultarComunicaciones,
                                                        blnConsultarCumplase,
                                                        blnConsultarPublicacion);                    
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: cmdBuscar_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la busqueda de información");
                }
            }

        #endregion


        #region GrdExpedientes

            /// <summary>
            /// Se ejecuta al cargar la información de los expedientes
            /// </summary>
            protected void grdExpedientes_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                GridView objListaReporte = null;
                DataRowView objFila = null;

                try{

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Cargar la lista reporte
                        objListaReporte = (GridView)e.Row.FindControl("grdExpedientesDetalles");

                        //Cargar datos
                        objFila = (DataRowView)e.Row.DataItem;

                        //Verificar que exista
                        if (objListaReporte != null)
                        {
                            //Cargar los datos de la grilla
                            objListaReporte.DataSource = this.objReporte.Tables[1].Select("EXPEDIENTE='" + objFila["EXPEDIENTE"].ToString() + "' AND (NUM_VITAL = '' OR  NUM_VITAL = '" + objFila["NUM_VITAL"].ToString() + "')").CopyToDataTable();                            
                            objListaReporte.DataBind();
                        }
                    }
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdExpedientes_DataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la carga de datos en el  listado");
                }
            }


            /// <summary>
            /// Evento que se ejecuta al cambiar de pagina
            /// </summary>
            protected void grdExpedientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Asignar nuevo index
                    this.grdExpedientes.PageIndex = e.NewPageIndex;

                    //Cargar datos
                    this.grdExpedientes.DataSource = this.objReporte.Tables[0];
                    this.grdExpedientes.DataBind();

                    //Cargar triggers detalles
                    this.CargarTriggerDetalles();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdExpedientes_PageIndexChanging -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar la información durante el cambio de página. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
            }


        #endregion


        #region GrdExpedientesDetalles


            /// <summary>
            /// Evento que se ejecuta cuando carga la grilla
            /// </summary>
            protected void grdExpedientesDetalles_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                Notificacion objNotificacion = null;
                LinkButton objLinkDetalle = null;
                Image objImagenGris = null;
                DataRowView objFila = null;
                Label objLabel = null;
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
                        blnProcesosPendientesNotificacion = objNotificacion.TieneProcesosAsociadosNoNotificados(Convert.ToDecimal(objFila["ID_NOTIFICACION"]));

                        //Cargar controles de notificacion
                        objLinkDetalle = (LinkButton)e.Row.FindControl("lnkNotificaciones");
                        objImagenGris = (Image)e.Row.FindControl("imgNotificarGris");

                        //Mostrar los botones que correspondan a notificaciones
                        if (Convert.ToBoolean(objFila["ES_NOTIFICACION"]))
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
                        objLinkDetalle = (LinkButton)e.Row.FindControl("lnkComunicar");
                        objImagenGris = (Image)e.Row.FindControl("imgComunicarGris");

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
                        objLinkDetalle = (LinkButton)e.Row.FindControl("lnkCumplir");
                        objImagenGris = (Image)e.Row.FindControl("imgCumplirGris");

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
                        objLinkDetalle = (LinkButton)e.Row.FindControl("lnkPublicar");
                        objImagenGris = (Image)e.Row.FindControl("imgPublicarGris");

                        //Mostrar los botones que correspondan a publicar
                        if (Convert.ToBoolean(objFila["PUBLICACION"]))
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

                        if (blnProcesosPendientesNotificacion && Convert.ToInt32(objFila["NOT_ID_ESTADO_ACTO"]) != (int)NOTEstadosActo.Bloqueada_Anulacion)
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F79F81");
                        else if (Convert.ToInt32(objFila["NOT_ID_ESTADO_ACTO"]) == (int)NOTEstadosActo.Sin_Verificar)
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFFFFF");
                        else if (Convert.ToInt32(objFila["NOT_ID_ESTADO_ACTO"]) == (int)NOTEstadosActo.Verificado_NO_Liberado)
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFFF");
                        else if (Convert.ToInt32(objFila["NOT_ID_ESTADO_ACTO"]) == (int)NOTEstadosActo.Verificado_Liberado_Parcialmente)
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFBF");
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdExpedientesDetalles_RowDataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la carga del detalle de las notificaciones");
                }
            }


            /// <summary>
            /// Evento que se ejecuta al cambiar de pagina
            /// </summary>
            protected void grdExpedientesDetalles_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                string strExpediente = "";
                string strNumeroVITAL = "";

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Obtener el expedente y número vital
                    strExpediente = ((GridView)sender).DataKeys[0].Values[0].ToString();
                    strNumeroVITAL = ((GridView)sender).DataKeys[0].Values[1].ToString();

                    //Mostrar datos
                    ((GridView)sender).PageIndex = e.NewPageIndex;
                    ((GridView)sender).DataSource = this.objReporte.Tables[1].Select("EXPEDIENTE='" + strExpediente + "' AND NUM_VITAL = '" + strNumeroVITAL + "'").CopyToDataTable();
                    ((GridView)sender).DataBind();

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdExpedientesDetalles_PageIndexChanging -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar la información durante el cambio de página del detalle. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally {
                    //Cargar triggers
                    this.CargarTriggerDetalles();
                }
            }        

        #endregion


        #region lnkInformacionActo

            /// <summary>
            /// Evento que muestra información de bitacora
            /// </summary>
            protected void lnkInformacionActo_Click(object sender, EventArgs e)
            {
                try
                {
                    //Cargar los datos de notificación
                    this.CargarInformacionBitacora(((LinkButton)sender).CommandArgument);

                    //Mostrar detalle
                    this.mpeInformacionBitacora.Show();                    
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: lnkInformacionActo_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar la información del acto administrativo. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers
                    this.CargarTriggerDetalles();

                    //Actualizar contenido de updatepanel
                    this.upnlInformacionBitacora.Update();
                }
            }

        #endregion


        #region lnkNotificaciones

            /// <summary>
            /// Evento que se genera al dar clic en el link para observar detalle
            /// </summary>
            protected void lnkNotificaciones_Click(object sender, EventArgs e)
            {
                try
                {
                    //Cargar los datos de notificación
                    this.CargarDetalleNotificacion(((LinkButton)sender).CommandArgument);

                    //Mostrar detalle
                    this.mpeInformacionNotificaciones.Show();                    
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: lnkNotificaciones_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar el detalle de la notificación seleccionada. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers
                    this.CargarTriggerDetalles();

                    //Actualizar contenido de updatepanel
                    this.upnlDetallesNotificacion.Update();
                }
            }

        #endregion


        #region lnkComunicar

            /// <summary>
            /// Evento que se genera al dar clic en el link para observar detalle
            /// </summary>
            protected void lnkComunicar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Cargar los datos de notificación
                    this.CargarDetalleComunicacion(((LinkButton)sender).CommandArgument);

                    //Mostrar detalle
                    this.mpeInformacionComunicar.Show();

                    //Actualizar panel
                    this.upnlDetalleComunicacion.Update();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: lnkComunicar_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar el detalle de la comunicación seleccionada. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers
                    this.CargarTriggerDetalles();

                    //Actualizar contenido de updatepanel
                    this.upnlDetallesNotificacion.Update();
                }
            }

        #endregion


        #region lnkCumplir

            /// <summary>
            /// Evento que se genera al dar clic en el link para observar detalle
            /// </summary>
            protected void lnkCumplir_Click(object sender, EventArgs e)
            {
                try
                {
                    //Cargar los datos de notificación
                    this.CargarDetalleCumplimiento(((LinkButton)sender).CommandArgument);

                    //Mostrar detalle
                    this.mpeInformacionCumplir.Show();

                    //Actualizar update panle
                    this.upnlCumplimiento.Update();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: lnkCumplir_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar el detalle de la ejecución seleccionada. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers
                    this.CargarTriggerDetalles();

                    //Actualizar contenido de updatepanel
                    this.upnlDetallesNotificacion.Update();
                }
            }

        #endregion


        #region lnkPublicar

            /// <summary>
            /// Evento que se genera al dar clic en el link para observar detalle
            /// </summary>
            protected void lnkPublicar_Click(object sender, EventArgs e)
            {
                long lngIdPublicacion = 0;                

                try
                {
                    if (!string.IsNullOrWhiteSpace(((LinkButton)sender).CommandArgument))
                    {
                        //Cargar id de publicacion
                        lngIdPublicacion = Convert.ToInt64(((LinkButton)sender).CommandArgument);

                        //Cargar los datos de notificación
                        this.CargarDetallePublicacion(lngIdPublicacion);

                        //Mostrar detalle
                        this.mpeInformacionPublicar.Show();

                        //Actualizar panel
                        this.upnlDetallePublicacion.Update();
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: lnkPublicar_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar el detalle de la publicación seleccionada. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers
                    this.CargarTriggerDetalles();

                    //Actualizar contenido de updatepanel
                    this.upnlDetallesNotificacion.Update();
                }
            }

        #endregion


        #region grdBitacora

            /// <summary>
            /// Evento que realiza cambio de paginación registros de bitacora
            /// </summary>
            protected void grdBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Asignar nuevo index
                    this.grdBitacora.PageIndex = e.NewPageIndex;

                    //Cargar datos
                    this.grdBitacora.DataSource = this.objReporteNotificaciones.Tables[0];
                    this.grdBitacora.DataBind();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdBitacora_PageIndexChanging -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar la información durante el cambio de página. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers detalles
                    this.CargarTriggerDetalles();

                    //Recargar panel
                    this.upnlInformacionBitacora.Update();
                    this.mpeInformacionBitacora.Show();
                }
            }

        #endregion


        #region GrdNotificacionDetalles


            /// <summary>
            /// Evento que se ejecuta cuando carga la grilla
            /// </summary>
            protected void grdNotificacionDetalles_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                Image objImagen = null;
                Image objImagen2 = null;
                DataRowView objFila = null;

                try
                {

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {

                        //Cargar datos
                        objFila = (DataRowView)e.Row.DataItem;

                        objImagen = (Image)e.Row.FindControl("imgEnvioCorreo");
                        if (objImagen != null && objImagen.Visible)
                        {
                            objImagen.Attributes.Add("title", this.CrearTablaCorreos(Convert.ToInt64(objFila["ID_ESTADO_PERSONA"])));
                        }
                        else if (objImagen != null)
                        {
                            objImagen.Attributes.Add("title", "");
                        }

                        objImagen2 = (Image)e.Row.FindControl("imgEnvioDireccion");
                        if (objImagen2 != null && objImagen2.Visible)
                        {
                            objImagen2.Attributes.Add("title", this.CrearTablaDirecciones(Convert.ToInt64(objFila["ID_ESTADO_PERSONA"])));
                        }
                        else if (objImagen2 != null)
                        {
                            objImagen2.Attributes.Add("title", "");
                        }

                        //Colocar color 
                        if (int.Parse(((Label)e.Row.FindControl("lblDiasVencimiento")).Text) < 0)
                        {
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCCCC");
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdNotificacionDetalles_RowDataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la carga del detalle de los estados del usuario");
                }
            }

        #endregion


        #region GrdNotificacion

            /// <summary>
            /// Se ejecuta al cargar la información de los expedientes
            /// </summary>
            protected void grdNotificacion_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                GridView objListaReporte = null;
                DataRowView objFila = null;
                LinkButton objLink = null;
                DataRow[] objLista = null;

                try{

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Cargar la lista reporte
                        objListaReporte = (GridView)e.Row.FindControl("grdNotificacionDetalles");

                        //Cargar datos
                        objFila = (DataRowView)e.Row.DataItem;

                        //Verificar que exista
                        if (objListaReporte != null)
                        {
                            //Cargar los datos de la grilla
                            objListaReporte.DataSource = this.objReporteNotificaciones.Tables[1].Select("IDENTIFICACION_USUARIO_NOTIFICAR='" + objFila["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString() + "'").CopyToDataTable();
                            objListaReporte.DataBind();

                            //Cargar link
                            objLink = (LinkButton)e.Row.FindControl("lnkVerNotificacion");

                            //Cargar funcion ejecutar
                            objLink.OnClientClick = "javascript:VerNotificacion(" + objFila["ID_ACTO_NOTIFICACION"].ToString() + "," + objFila["ID_PERSONA"].ToString() + ");";

                            //Colocar en color rojo en caso de que se tenga algun estado vencido
                            if (objLink != null)
                            {
                                objLista = this.objReporteNotificaciones.Tables[1].Select("IDENTIFICACION_USUARIO_NOTIFICAR='" + objFila["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString() + "' AND DIAS_PARA_VENCIMIENTO < 0");
                                if (objLista != null && objLista.Length > 0)
                                {
                                    objLink.CssClass = "LnkTextoAlertaModalNot";
                                }
                            }
                        }
                        
                        if (Convert.ToInt32(objFila["NOT_ID_ESTADO_ACTO"]) == (int)NOTEstadosActo.Verificado_Liberado_Parcialmente && Convert.ToInt32(objFila["ID_ESTADO_PERSONA_NOTIFICAR"]) == (int)NOTEstadosActoPersona.Sin_Verificar)
                            ((HtmlGenericControl)e.Row.FindControl("dvHeaderAccordion")).Attributes.Add("style", "background:#FFFFBF");
                        else if (Convert.ToInt32(objFila["ID_ESTADO_PERSONA_NOTIFICAR"]) == (int)NOTEstadosActoPersona.Sin_Verificar)
                            ((HtmlGenericControl)e.Row.FindControl("dvHeaderAccordion")).Attributes.Add("style", "background:#DFFFFF");
                    }
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdNotificacion_RowDataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la carga de datos en el listado de estados de notificación");
                }
            }


            /// <summary>
            /// Evento que se ejecuta al cambiar de pagina
            /// </summary>
            protected void grdNotificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Asignar nuevo index
                    this.grdNotificacion.PageIndex = e.NewPageIndex;

                    //Cargar datos
                    this.grdNotificacion.DataSource = this.objReporteNotificaciones.Tables[0];
                    this.grdNotificacion.DataBind();

                    //Cargar triggers
                    this.CargarTriggerNotificacionDetalles();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdNotificacion_PageIndexChanging -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar la información durante el cambio de página. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers detalles
                    this.CargarTriggerDetalles();

                    //Recargar panel
                    this.upnlDetallesNotificacion.Update();
                    this.mpeInformacionNotificaciones.Show();
                }
            }


        #endregion


        #region grdComunicacionDetalles

            /// <summary>
            /// Evento que se ejecuta cuando carga la grilla
            /// </summary>
            protected void grdComunicacionDetalles_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                Image objImagen = null;
                Image objImagen2 = null;
                DataRowView objFila = null;

                try
                {
                    //Cargar datos
                    objFila = (DataRowView)e.Row.DataItem;

                    objImagen = (Image)e.Row.FindControl("imgEnvioCorreo");
                    if (objImagen != null && objImagen.Visible)
                    {
                        objImagen.Attributes.Add("title", this.CrearTablaCorreos(Convert.ToInt64(objFila["ID_ESTADO_PERSONA"])));
                    }
                    else if (objImagen != null)
                    {
                        objImagen.Attributes.Add("title", "");
                    }

                    objImagen2 = (Image)e.Row.FindControl("imgEnvioDireccion");
                    if (objImagen2 != null && objImagen2.Visible)
                    {
                        objImagen2.Attributes.Add("title", this.CrearTablaDirecciones(Convert.ToInt64(objFila["ID_ESTADO_PERSONA"])));
                    }
                    else if (objImagen2 != null)
                    {
                        objImagen2.Attributes.Add("title", "");
                    }

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Colocar color 
                        if (int.Parse(((Label)e.Row.FindControl("lblDiasVencimiento")).Text) < 0)
                        {
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCCCC");
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdComunicacionDetalles_RowDataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la carga del detalle de los estados del usuario");
                }
            }

        #endregion


        #region lnkCerrarInformacionBitacora

            /// <summary>
            /// Evento que cierra modal que muestra información de la bitacora
            /// </summary>
            protected void lnkCerrarInformacionBitacora_Click(object sender, EventArgs e)
            {
                //Limpiar la grilla de notificaciones
                this.grdBitacora.DataSource = null;
                this.grdBitacora.PageIndex = 0;
                this.grdBitacora.DataBind();

                //Cerrar modal
                this.mpeInformacionBitacora.Hide();

                //Cargar triggers
                this.CargarTriggerDetalles();

                this.upnlInformacionBitacora.Update();
            }

        #endregion


        #region LnkCerrarNotificacion

            /// <summary>
            /// Evento que se ejecuta al cerrar el modal
            /// </summary>
            protected void lnkCerrarNotificacion_Click(object sender, EventArgs e)
            {
                //Limpiar la grilla de notificaciones
                this.grdNotificacion.DataSource = null;
                this.grdNotificacion.PageIndex = 0;
                this.grdNotificacion.DataBind();

                //Cerrar modal
                this.mpeInformacionNotificaciones.Hide();

                //Cargar triggers
                this.CargarTriggerDetalles();

                this.upnlDetallesNotificacion.Update();
            }

        #endregion


        #region GrdComunicacion

            /// <summary>
            /// Se ejecuta al cargar la información de los expedientes
            /// </summary>
            protected void grdComunicacion_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                GridView objListaReporte = null;
                DataRowView objFila = null;
                LinkButton objLink = null;
                DataRow[] objLista = null;

                try{

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Cargar la lista reporte
                        objListaReporte = (GridView)e.Row.FindControl("grdComunicacionDetalles");

                        //Cargar datos
                        objFila = (DataRowView)e.Row.DataItem;

                        //Verificar que exista
                        if (objListaReporte != null)
                        {
                            //Cargar los datos de la grilla
                            objListaReporte.DataSource = this.objReporteNotificaciones.Tables[1].Select("IDENTIFICACION_USUARIO_NOTIFICAR='" + objFila["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString() + "'").CopyToDataTable();
                            objListaReporte.DataBind();
                        }

                        //Cargar link
                        objLink = (LinkButton)e.Row.FindControl("lnkVerNotificacion");

                        //Cargar funcion ejecutar
                        objLink.OnClientClick = "javascript:VerNotificacion(" + objFila["ID_ACTO_NOTIFICACION"].ToString() + "," + objFila["ID_PERSONA"].ToString() + ");";

                        //Colocar en color rojo en caso de que se tenga algun estado vencido
                        if (objLink != null)
                        {
                            objLista = this.objReporteNotificaciones.Tables[1].Select("IDENTIFICACION_USUARIO_NOTIFICAR='" + objFila["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString() + "' AND DIAS_PARA_VENCIMIENTO < 0");
                            if (objLista != null && objLista.Length > 0)
                            {
                                objLink.CssClass = "LnkTextoAlertaModalNot";
                            }
                        }
                    }
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdComunicacion_RowDataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la carga de datos en el listado comunicaciones");
                }
            }


            /// <summary>
            /// Evento que se ejecuta al cambiar de pagina
            /// </summary>
            protected void grdComunicacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Asignar nuevo index
                    this.grdComunicacion.PageIndex = e.NewPageIndex;

                    //Cargar datos
                    this.grdComunicacion.DataSource = this.objReporteNotificaciones.Tables[0];
                    this.grdComunicacion.DataBind();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdComunicacion_PageIndexChanging -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar la información durante el cambio de página. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers detalles
                    this.CargarTriggerDetalles();

                    //Recargar panel
                    this.upnlDetallesNotificacion.Update();
                    this.mpeInformacionComunicar.Show();
                }
            }

        #endregion


        #region grdComunicacionDetalles

            /// <summary>
            /// Evento que se ejecuta cuando carga la grilla
            /// </summary>
            protected void grdCumplimientoDetalles_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                Image objImagen = null;
                Image objImagen2 = null;
                DataRowView objFila = null;

                try
                {
                    //Cargar datos
                    objFila = (DataRowView)e.Row.DataItem;

                    objImagen = (Image)e.Row.FindControl("imgEnvioCorreo");
                    if (objImagen != null && objImagen.Visible)
                    {
                        objImagen.Attributes.Add("title", this.CrearTablaCorreos(Convert.ToInt64(objFila["ID_ESTADO_PERSONA"])));
                    }
                    else if (objImagen != null)
                    {
                        objImagen.Attributes.Add("title", "");
                    }

                    objImagen2 = (Image)e.Row.FindControl("imgEnvioDireccion");
                    if (objImagen2 != null && objImagen2.Visible)
                    {
                        objImagen2.Attributes.Add("title", this.CrearTablaDirecciones(Convert.ToInt64(objFila["ID_ESTADO_PERSONA"])));
                    }
                    else if (objImagen2 != null)
                    {
                        objImagen2.Attributes.Add("title", "");
                    }

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Colocar color 
                        if (int.Parse(((Label)e.Row.FindControl("lblDiasVencimiento")).Text) < 0)
                        {
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCCCC");
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdCumplimientoDetalles_RowDataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la carga del detalle de los estados del usuario");
                }
            }

        #endregion


            
        #region LnkCerrarComunicacion

            /// <summary>
            /// Evento que se ejecuta al cerrar el modal
            /// </summary>
            protected void lnkCerrarComunicacion_Click(object sender, EventArgs e)
            {
                //Limpiar la grilla de notificaciones
                this.grdComunicacion.DataSource = null;
                this.grdComunicacion.PageIndex = 0;
                this.grdComunicacion.DataBind();

                //Cerrar modal
                this.mpeInformacionComunicar.Hide();

                //Cargar triggers
                this.CargarTriggerDetalles();

                //Actualizar panel
                this.upnlDetalleComunicacion.Update();
            }

        #endregion


        #region GrdCumplimiento

            /// <summary>
            /// Se ejecuta al cargar la información de los cumplimientos
            /// </summary>
            protected void grdCumplimiento_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                GridView objListaReporte = null;
                DataRowView objFila = null;
                LinkButton objLink = null;
                DataRow[] objLista = null;

                try{

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Cargar la lista reporte
                        objListaReporte = (GridView)e.Row.FindControl("grdCumplimientoDetalles");

                        //Cargar datos
                        objFila = (DataRowView)e.Row.DataItem;

                        //Verificar que exista
                        if (objListaReporte != null)
                        {
                            //Cargar los datos de la grilla
                            objListaReporte.DataSource = this.objReporteNotificaciones.Tables[1].Select("IDENTIFICACION_USUARIO_NOTIFICAR='" + objFila["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString() + "'").CopyToDataTable();
                            objListaReporte.DataBind();
                        }

                        //Cargar link
                        objLink = (LinkButton)e.Row.FindControl("lnkVerNotificacion");

                        //Cargar funcion ejecutar
                        objLink.OnClientClick = "javascript:VerNotificacion(" + objFila["ID_ACTO_NOTIFICACION"].ToString() + "," + objFila["ID_PERSONA"].ToString() + ");";

                        //Colocar en color rojo en caso de que se tenga algun estado vencido
                        if (objLink != null)
                        {
                            objLista = this.objReporteNotificaciones.Tables[1].Select("IDENTIFICACION_USUARIO_NOTIFICAR='" + objFila["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString() + "' AND DIAS_PARA_VENCIMIENTO < 0");
                            if (objLista != null && objLista.Length > 0)
                            {
                                objLink.CssClass = "LnkTextoAlertaModalNot";
                            }
                        }
                    }
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdCumplimiento_RowDataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error realizando la carga de datos en el listado cumplimientos");
                }
            }


            /// <summary>
            /// Evento que se ejecuta al cambiar de pagina
            /// </summary>
            protected void grdCumplimiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Asignar nuevo index
                    this.grdCumplimiento.PageIndex = e.NewPageIndex;

                    //Cargar datos
                    this.grdCumplimiento.DataSource = this.objReporteNotificaciones.Tables[0];
                    this.grdCumplimiento.DataBind();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ReporteNotificaciones :: grdCumplimiento_PageIndexChanging -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error al cargar la información durante el cambio de página. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
                }
                finally
                {
                    //Cargar triggers detalles
                    this.CargarTriggerDetalles();

                    //Recargar panel
                    this.upnlDetallesNotificacion.Update();
                    this.mpeInformacionCumplir.Show();
                }
            }

        #endregion


        #region LnkCerrarCumplimiento

            /// <summary>
            /// Evento que se ejecuta al cerrar el modal
            /// </summary>
            protected void lnkCerrarCumplimiento_Click(object sender, EventArgs e)
            {
                //Limpiar la grilla de notificaciones
                this.grdCumplimiento.DataSource = null;
                this.grdCumplimiento.PageIndex = 0;
                this.grdCumplimiento.DataBind();

                //Cerrar modal
                this.mpeInformacionCumplir.Hide();

                //Cargar triggers
                this.CargarTriggerDetalles();

                //Actualizar update panle
                this.upnlCumplimiento.Update();
            }

        #endregion


        #region lnkCerrarPublicar

            /// <summary>
            /// Evento que se ejecuta al cerrar el modal
            /// </summary>
            protected void lnkCerrarPublicar_Click(object sender, EventArgs e)
            {
                //Limpiar sesion de documentos
                Session["ListaDocumentos"] = null;

                //Cerrar modal
                this.mpeInformacionPublicar.Hide();

                //Cargar triggers
                this.CargarTriggerDetalles();

                //Actualizar 
                this.upnlDetallePublicacion.Update();
            }

        #endregion


        #region imgDescargarDocumento

            /// <summary>
            /// Evento que realiza la descarga del documento
            /// </summary>
            protected void imgDescargarDocumento_Click(object sender, ImageClickEventArgs e)
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
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgDescargarDocumento_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }

        #endregion


        #region imgDescargarDocumento

            /// <summary>
            /// Evento que muestra los documentos adjuntos
            /// </summary>
            protected void imgAdjuntos_Click(object sender, ImageClickEventArgs e)
            {
                long lngEstadoPersonaActoID = 0;

                try
                {
                    //Limpiar modal inicial
                    this.LimpiarModalVerAdjuntos();

                    //Cargar datos de adjuntos
                    lngEstadoPersonaActoID = long.Parse(((ImageButton)sender).CommandArgument.ToString());
                    this.CargarDatosModalVerAdjuntos(lngEstadoPersonaActoID);

                    //Cargar el modal del cual proviene
                    if (((ImageButton)sender).ID.Contains("Comunicacion"))
                        this.hdfTipoNotificacionModalVerAdjuntos.Value = ((int)TipoNotificacion.COMUNICACION).ToString();
                    else if (((ImageButton)sender).ID.Contains("Cumplase"))
                        this.hdfTipoNotificacionModalVerAdjuntos.Value = ((int)TipoNotificacion.CUMPLASE).ToString();
                    else
                        this.hdfTipoNotificacionModalVerAdjuntos.Value = ((int)TipoNotificacion.NOTIFICACION).ToString();

                    //Actualizar panel
                    this.upnlVerDocumentosAdjuntos.Update();

                    //Cerrar modal
                    if (Convert.ToInt32(this.hdfTipoNotificacionModalVerAdjuntos.Value) == (int)TipoNotificacion.COMUNICACION)
                        this.mpeInformacionComunicar.Hide();
                    else if (Convert.ToInt32(this.hdfTipoNotificacionModalVerAdjuntos.Value) == (int)TipoNotificacion.CUMPLASE)
                        this.mpeInformacionCumplir.Hide();
                    else
                        this.mpeInformacionNotificaciones.Hide();

                    //Mostrar modal
                    this.mpeVerDocumentosAdjuntos.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgAdjuntos_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando informacion de adjuntos");
                }
            }

        #endregion


        #region imngEstadoPublicado

            /// <summary>
            /// Evento que muestra la información de la publicación
            /// </summary>
            protected void imngEstadoPublicado_Click(object sender, ImageClickEventArgs e)
            {
                long lngPublicacionEstadoPersonaActoId = 0;

                try
                {
                    //Cargar identificador
                    lngPublicacionEstadoPersonaActoId = Convert.ToInt64(((ImageButton)sender).CommandArgument);

                    //Limpiar modal
                    this.LimpiarModalVerPublicacionEstado();

                    //Cargar datos de modal
                    this.ConsultarInformacionPublicacionEstado(lngPublicacionEstadoPersonaActoId);

                    //Cargar el modal del cual proviene
                    if (((ImageButton)sender).ID.Contains("Comunicacion"))
                        this.hdfTipoNotificacionModalPublicacion.Value = ((int)TipoNotificacion.COMUNICACION).ToString();
                    else if (((ImageButton)sender).ID.Contains("Cumplase"))
                        this.hdfTipoNotificacionModalPublicacion.Value = ((int)TipoNotificacion.CUMPLASE).ToString();
                    else
                        this.hdfTipoNotificacionModalPublicacion.Value = ((int)TipoNotificacion.NOTIFICACION).ToString();

                    //Actualizar panel
                    this.upnlVerEstado.Update();

                    //Cerrar modal
                    if (Convert.ToInt32(this.hdfTipoNotificacionModalPublicacion.Value) == (int)TipoNotificacion.COMUNICACION)
                        this.mpeInformacionComunicar.Hide();
                    else if (Convert.ToInt32(this.hdfTipoNotificacionModalPublicacion.Value) == (int)TipoNotificacion.CUMPLASE)
                        this.mpeInformacionCumplir.Hide();
                    else
                        this.mpeInformacionNotificaciones.Hide();

                    //Mostrar modal
                    this.mpeVerEstadoPublicacion.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ReporteNotificaciones :: imngEstadoPublicado_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error cargando información detallada de la publicación del estado");
                }
            }

        #endregion


        #region lnkCerrarPublicarEstado

            protected void lnkCerrarPublicarEstado_Click(object sender, EventArgs e)
            {
                int intModalOrigen = Convert.ToInt32(this.hdfTipoNotificacionModalPublicacion.Value);

                //Limpiar modal
                this.LimpiarModalVerPublicacionEstado();

                //Actualizar panel
                this.upnlVerEstado.Update();

                //Cerrar modal
                this.mpeVerEstadoPublicacion.Hide();

                //Mostrar modal principal
                if (intModalOrigen == (int)TipoNotificacion.COMUNICACION)
                {
                    this.upnlDetalleComunicacion.Update();
                    this.mpeInformacionComunicar.Show();
                }
                else if (intModalOrigen == (int)TipoNotificacion.CUMPLASE)
                {
                    this.upnlCumplimiento.Update();
                    this.mpeInformacionCumplir.Show();
                }
                else
                {
                    this.upnlDetallesNotificacion.Update();
                    this.mpeInformacionNotificaciones.Show();
                }
            }

        #endregion


        #region imngVerEstado

            /// <summary>
            /// Evento que muestra la información detallada del estado
            /// </summary>
            protected void imngVerEstado_Click(object sender, ImageClickEventArgs e)
            {
                long lngEstadoPersonaId = 0;

                try
                {
                    //Cargar identificador
                    lngEstadoPersonaId = Convert.ToInt64(((ImageButton)sender).CommandArgument);

                    //Limpiar modal
                    this.LimpiarModalVerDetalleEstado();

                    //Cargar datos de modal
                    this.ConsultarInformacionDetalleEstado(lngEstadoPersonaId);

                    //Cargar el modal del cual proviene
                    if (((ImageButton)sender).ID.Contains("Comunicacion"))
                        this.hdfTipoNotificacionModalVerEstado.Value = ((int)TipoNotificacion.COMUNICACION).ToString();
                    else if (((ImageButton)sender).ID.Contains("Cumplase"))
                        this.hdfTipoNotificacionModalVerEstado.Value = ((int)TipoNotificacion.CUMPLASE).ToString();
                    else
                        this.hdfTipoNotificacionModalVerEstado.Value = ((int)TipoNotificacion.NOTIFICACION).ToString();

                    //Actualizar panel
                    this.upnlVerDetalleEstado.Update();

                    //Cerrar modal
                    if (Convert.ToInt32(this.hdfTipoNotificacionModalVerEstado.Value) == (int)TipoNotificacion.COMUNICACION)
                        this.mpeInformacionComunicar.Hide();
                    else if (Convert.ToInt32(this.hdfTipoNotificacionModalVerEstado.Value) == (int)TipoNotificacion.CUMPLASE)
                        this.mpeInformacionCumplir.Hide();
                    else
                        this.mpeInformacionNotificaciones.Hide();

                    //Mostrar modal
                    this.mpeVerDetalleEstado.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ReporteNotificaciones :: imngEstadoPublicado_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error cargando información detallada del estado");
                }
            }

        #endregion


        #region lnkCerrarVerEstado

            /// <summary>
            /// Evento que cierra el modal de ver estado
            /// </summary>
            protected void lnkCerrarVerEstado_Click(object sender, EventArgs e)
            {
                int intModalOrigen = Convert.ToInt32(this.hdfTipoNotificacionModalVerEstado.Value);

                //Limpiar modal
                this.LimpiarModalVerDetalleEstado();

                //Actualizar panel
                this.upnlVerDetalleEstado.Update();

                //Cerrar modal
                this.mpeVerDetalleEstado.Hide();

                //Mostrar modal principal
                if (intModalOrigen == (int)TipoNotificacion.COMUNICACION)
                {
                    this.upnlDetalleComunicacion.Update();
                    this.mpeInformacionComunicar.Show();
                }
                else if (intModalOrigen == (int)TipoNotificacion.CUMPLASE)
                {
                    this.upnlCumplimiento.Update();
                    this.mpeInformacionCumplir.Show();
                }
                else
                {
                    this.upnlDetallesNotificacion.Update();
                    this.mpeInformacionNotificaciones.Show();
                }
            }

        #endregion


        #region imgDocumentoModal

            /// <summary>
            /// Evento que muestra el documento seleccionado
            /// </summary>
            protected void imgDocumentoModal_Click(object sender, ImageClickEventArgs e)
            {
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
                string[] lstURL = null;
                string[] lstLlaves = null;
                string strEnlace = "";
                EnlaceDocumentoSila objEnlace = null;
                EnlaceDocumentoSilaEntity objEnlaceEntity = null;

                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        //Verificar el origen del archivo
                            if( ((ImageButton)sender).CommandArgument.ToString().StartsWith("CON-SILA@")){

                                //Crear objeto de parametros
                                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

                                //Cargar datos url
                                lstURL = ((ImageButton)sender).CommandArgument.ToString().Split('@');

                                if (lstURL != null && lstURL.Length > 1)
                                {
                                    //Cargar llaves
                                    lstLlaves = lstURL[1].Split('_');

                                    //Cargar datos
                                    objEnlaceEntity = new EnlaceDocumentoSilaEntity
                                    {
                                        EnlaceID = lstURL[1],
                                        ActoNotificacionID = Convert.ToInt64(lstLlaves[1]),
                                        PersonaID = Convert.ToInt64(lstLlaves[2]),
                                        EstadoPersonaID = Convert.ToInt64(lstLlaves[3]),
                                        DocumentoID = Convert.ToInt32(lstLlaves[0]),
                                        Llave = EnDecript.Encriptar(lstURL[1]),
                                        FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_SILA_NOTIFICACION")))
                                    };

                                    //Almacenar enlace
                                    objEnlace = new EnlaceDocumentoSila();
                                    objEnlace.CrearEnlace(objEnlaceEntity);

                                    //Obtener URL                                
                                    strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_VER_CONCEPTOS_SILA_NOTIFICACION") + objEnlaceEntity.Llave;

                                    //Redireccionar
                                    Response.Redirect(strEnlace, false);
                                }
                                else
                                    throw new Exception("No se encontro información URL " + ((ImageButton)sender).CommandArgument.ToString());
                            }
                            else if (((ImageButton)sender).CommandArgument.ToString().StartsWith("CON-SILAMC@"))
                            {

                                //Crear objeto de parametros
                                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

                                //Cargar datos url
                                lstURL = ((ImageButton)sender).CommandArgument.ToString().Split('@');

                                if (lstURL != null && lstURL.Length > 1)
                                {
                                    //Cargar llaves
                                    lstLlaves = lstURL[1].Split('_');

                                    //Cargar datos
                                    objEnlaceEntity = new EnlaceDocumentoSilaEntity
                                    {
                                        EnlaceID = lstURL[1],
                                        ActoNotificacionID = Convert.ToInt64(lstLlaves[1]),
                                        PersonaID = Convert.ToInt64(lstLlaves[2]),
                                        EstadoPersonaID = Convert.ToInt64(lstLlaves[3]),
                                        DocumentoID = Convert.ToInt32(lstLlaves[0]),
                                        Llave = EnDecript.Encriptar(lstURL[1]),
                                        FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_SILAMC_NOTIFICACION")))
                                    };

                                    //Almacenar enlace
                                    objEnlace = new EnlaceDocumentoSila();
                                    objEnlace.CrearEnlace(objEnlaceEntity);

                                    //Obtener URL                                
                                    strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_VER_CONCEPTOS_SILAMC_NOTIFICACION") + objEnlaceEntity.Llave;

                                    //Redireccionar
                                    Response.Redirect(strEnlace, false);
                                }
                                else
                                    throw new Exception("No se encontro información URL " + ((ImageButton)sender).CommandArgument.ToString());
                            }
                            else
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
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ReporteNotificaciones :: imgDocumentoModal_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }

        #endregion


        #region Modal Ver Adjuntos


            #region cmdCerrarVerAdjuntos

                /// <summary>
                /// Evento que cierra modal de ver adjuntos
                /// </summary>
                protected void lnkCerrarVerAdjuntosModal_Click(object sender, EventArgs e)
                {
                    int intModalOrigen = Convert.ToInt32(this.hdfTipoNotificacionModalVerAdjuntos.Value);

                    //Limpiar modal
                    this.LimpiarModalVerAdjuntos();

                    //Actualizar panel
                    this.upnlVerDocumentosAdjuntos.Update();

                    //Cerrar modal
                    this.mpeVerDocumentosAdjuntos.Hide();

                    //Mostrar modal principal
                    if (intModalOrigen == (int)TipoNotificacion.COMUNICACION)
                    {
                        this.upnlDetalleComunicacion.Update();
                        this.mpeInformacionComunicar.Show();
                    }
                    else if (intModalOrigen == (int)TipoNotificacion.CUMPLASE)
                    {
                        this.upnlCumplimiento.Update();
                        this.mpeInformacionCumplir.Show();
                    }
                    else
                    {
                        this.upnlDetallesNotificacion.Update();
                        this.mpeInformacionNotificaciones.Show();
                    }
                }

            #endregion


            #region imgDescargarDocumentoAdjuntoVer

                /// <summary>
                /// Evento que descarga archivo
                /// </summary>
                protected void imgDescargarDocumentoAdjuntoVer_Click(object sender, ImageClickEventArgs e)
                {
                    SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
                    string[] lstURL = null;
                    string[] lstLlaves = null;
                    string strEnlace = "";
                    EnlaceDocumentoSila objEnlace = null;
                    EnlaceDocumentoSilaEntity objEnlaceEntity = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Validar que exista información
                        if (((ImageButton)sender).CommandArgument != null)
                        {
                            //Verificar el origen del archivo
                            if (((ImageButton)sender).CommandArgument.ToString().StartsWith("CON-SILA@"))
                            {

                                //Crear objeto de parametros
                                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

                                //Cargar datos url
                                lstURL = ((ImageButton)sender).CommandArgument.ToString().Split('@');

                                if (lstURL != null && lstURL.Length > 1)
                                {
                                    //Cargar llaves
                                    lstLlaves = lstURL[1].Split('_');

                                    //Cargar datos
                                    objEnlaceEntity = new EnlaceDocumentoSilaEntity
                                    {
                                        EnlaceID = lstURL[1],
                                        ActoNotificacionID = Convert.ToInt64(lstLlaves[1]),
                                        PersonaID = Convert.ToInt64(lstLlaves[2]),
                                        EstadoPersonaID = Convert.ToInt64(lstLlaves[3]),
                                        DocumentoID = Convert.ToInt32(lstLlaves[0]),
                                        Llave = EnDecript.Encriptar(lstURL[1]),
                                        FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_SILA_NOTIFICACION")))
                                    };

                                    //Almacenar enlace
                                    objEnlace = new EnlaceDocumentoSila();
                                    objEnlace.CrearEnlace(objEnlaceEntity);

                                    //Obtener URL                                
                                    strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_VER_CONCEPTOS_SILA_NOTIFICACION") + objEnlaceEntity.Llave;

                                    //Redireccionar
                                    Response.Redirect(strEnlace, false);
                                }
                                else
                                    throw new Exception("No se encontro información URL " + ((ImageButton)sender).CommandArgument.ToString());
                            }
                            else if (((ImageButton)sender).CommandArgument.ToString().StartsWith("CON-SILAMC@"))
                            {

                                //Crear objeto de parametros
                                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

                                //Cargar datos url
                                lstURL = ((ImageButton)sender).CommandArgument.ToString().Split('@');

                                if (lstURL != null && lstURL.Length > 1)
                                {
                                    //Cargar llaves
                                    lstLlaves = lstURL[1].Split('_');

                                    //Cargar datos
                                    objEnlaceEntity = new EnlaceDocumentoSilaEntity
                                    {
                                        EnlaceID = lstURL[1],
                                        ActoNotificacionID = Convert.ToInt64(lstLlaves[1]),
                                        PersonaID = Convert.ToInt64(lstLlaves[2]),
                                        EstadoPersonaID = Convert.ToInt64(lstLlaves[3]),
                                        DocumentoID = Convert.ToInt32(lstLlaves[0]),
                                        Llave = EnDecript.Encriptar(lstURL[1]),
                                        FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_SILAMC_NOTIFICACION")))
                                    };

                                    //Almacenar enlace
                                    objEnlace = new EnlaceDocumentoSila();
                                    objEnlace.CrearEnlace(objEnlaceEntity);

                                    //Obtener URL                                
                                    strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_VER_CONCEPTOS_SILAMC_NOTIFICACION") + objEnlaceEntity.Llave;

                                    //Redireccionar
                                    Response.Redirect(strEnlace, false);
                                }
                                else
                                    throw new Exception("No se encontro información URL " + ((ImageButton)sender).CommandArgument.ToString());
                            }
                            else
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
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgAdjuntos_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error descargando el archivo indicado");
                    }
                }

            #endregion

        #endregion

    #endregion


                
}