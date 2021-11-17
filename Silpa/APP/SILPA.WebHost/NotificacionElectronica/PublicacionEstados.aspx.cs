using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Notificacion;
using System.Data;
using SILPA.AccesoDatos.Notificacion;
using System.Configuration;
using SILPA.Comun;
using SILPA.Servicios;
using SILPA.LogicaNegocio.Excepciones;
using SILPA.LogicaNegocio.Generico;

public partial class NotificacionElectronica_PublicacionEstados : System.Web.UI.Page
{

    #region Metodos Privados

        /// <summary>
        /// Cargar la información inicial de la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Cargar información listados
            this.ConsultaTiposActos();
            this.CargarTipoTramite();
            this.CargarAutoridades(this.cboAutoridad);            

            //Inicializar fechas
            this.txtFechaDesde.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");

            //Ocultar y limpiar grilla
            this.grdEstadosNotificaciones.DataSource = null;
            this.grdEstadosNotificaciones.DataBind();
            this.divEstadosNotificaciones.Visible = false;
        }


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
        /// Cargar el listado de formatos existes
        /// </summary>
        /// <param name="p_objLista">DropDownList con la lista en la cual se cargara la información</param>
        private void CargarAutoridades(DropDownList p_objLista)
        {
            Notificacion objNotificacion = null;
            DataSet objAutoridades = null;

            //Consultar las autoridades
            objNotificacion = new Notificacion();
            objAutoridades = objNotificacion.ListarAutoridadAmbientalNotificacion();

            //Cargar informacion en desplegables
            p_objLista.DataSource = objAutoridades;
            p_objLista.DataTextField = "AUT_NOMBRE";
            p_objLista.DataValueField = "AUT_ID";
            p_objLista.DataBind();
            p_objLista.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }


        /// <summary>
        /// Función que carga el listado de Tipos de Documento en el respectivo combo.
        /// </summary>
        private void CargarTipoTramite()
        {
            Listas objListas = null;

            try
            {
                //Cargar la información de los tipos documentales
                objListas = new Listas();
                this.cboTipoTramite.DataSource = objListas.ListarTipoTramite(null);
                this.cboTipoTramite.DataTextField = "NOMBRE_TIPO_TRAMITE";
                this.cboTipoTramite.DataValueField = "ID_TIPO_PROCESO";
                this.cboTipoTramite.DataBind();
                this.cboTipoTramite.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PublicacionEstados :: CargarTipoTramite -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de tipos de tarmites");
            }
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
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: ConsultaTiposActos -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de tipos de actos");
            }
        }


        /// <summary>
        /// Obtener la información de estados publicados de acuerdo a los parametros de busqueda
        /// </summary>
        private void BuscarInformacionEstadosPublicados()
        {
            PublicidadEstados objPublicidadEstados = null;

            try
            {
                //Realizar la busqueda
                objPublicidadEstados = new PublicidadEstados();
                this.grdEstadosNotificaciones.DataSource = objPublicidadEstados.ConsultarInformacionEstadosPublicados(Convert.ToInt32(this.hdfTipoTramite.Value),
                                                                                                                      Convert.ToInt32(this.hdfAutoridad.Value),
                                                                                                                      this.hdfNumeroVital.Value.Trim(),
                                                                                                                      this.hdfExpediente.Value.Trim(),
                                                                                                                      Convert.ToInt32(this.hdfTipoActo.Value),
                                                                                                                      this.hdfNumeroActo.Value.Trim(),
                                                                                                                      this.hdfIdentificacionUsuario.Value.Trim(),
                                                                                                                      Convert.ToBoolean(this.hdfIncluirHistoricas.Value),
                                                                                                                      Convert.ToDateTime(this.hdfFechaDesde.Value),
                                                                                                                      Convert.ToDateTime(this.hdfFechaHasta.Value));
                this.grdEstadosNotificaciones.DataBind();
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PublicacionEstados :: BuscarInformacionEstadosPublicados -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando la busqueda de información de publicaciones");
            }
        }


        /// <summary>
        /// Limpia información del modal de ver
        /// </summary>
        public void LimpiarModalVer()
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
            this.ltlFechaDocumentoModal.Text = "";
            this.ltlPublicacionModal.Text = "";
            this.ltlFechaPublicacionModal.Text = "";
            this.ltlFechaDesfijacion.Text = "";

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
        /// Consultar la información de una publicación y mostrar en pantalla
        /// </summary>
        /// <param name="lngPublicacionEstadoPersonaActoId">long con el identificador de la publicacion</param>
        private void ConsultarInformacionPublicacion(long lngPublicacionEstadoPersonaActoId)
        {
            PublicidadEstados objPublicidadEstados = null;
            DataTable objInformacionPublicacion = null;
            DataTable objDocumentos = null;
            DataRow objDocumento = null;
            Notificacion objNotificacion = null;
            DataTable objConceptosActo = null;

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
                this.ltlFechaDocumentoModal.Text = objInformacionPublicacion.Rows[0]["FECHA_DOCUMENTO"].ToString();
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
                    objDocumentos = this.CrearTablaDocumentos();

                    //Adicionar documentos
                    if(!string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString()) && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("\\")){
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().Substring((objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") != -1 ? objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") : objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }
                    if(!string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString()) && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("\\")){
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().Substring((objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("/") != -1 ? objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("/") : objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Mostrar en grilla
                    this.grdDocumentosModal.DataSource = objDocumentos;
                    this.grdDocumentosModal.DataBind();

                    //Cargar triggers
                    this.CargarTriggersDocumentoGrilla(this.grdDocumentosModal);

                    //Mostrar div
                    this.dvDocumentosPublicacion.Visible = true;
                }
                if (Convert.ToBoolean(objInformacionPublicacion.Rows[0]["PUBLICA_ADJUNTO"]))
                {

                    //Crear tabla
                    objDocumentos = this.CrearTablaDocumentos();

                    //Cargar acto administrativo
                    if(Convert.ToBoolean(objInformacionPublicacion.Rows[0]["ADJUNTO_INCLUYE_ACTO"]) &&
                       !string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString()) &&
                       !objInformacionPublicacion.Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().ToString().EndsWith("\\")){

                        //Cargar datos
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().Substring((objInformacionPublicacion.Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("/") != -1 ? objInformacionPublicacion.Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("/") : objInformacionPublicacion.Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Adicionar conceptos en caso de que existan
                    if (Convert.ToBoolean(objInformacionPublicacion.Rows[0]["ADJUNTO_INCLUYE_CONCEPTOS_ACTO"]))
                    {
                        //Crear objeto de notificacion
                        objNotificacion = new Notificacion();

                        //Consultar conceptos
                        objConceptosActo = objNotificacion.ConsultarConceptosAsociadosActoAdministrativo(Convert.ToInt64(objInformacionPublicacion.Rows[0]["ID_ACTO_NOTIFICACION"]));

                        foreach (DataRow objConcepto in objConceptosActo.Rows)
                        {
                            objDocumento = objDocumentos.NewRow();
                            objDocumento["NOMBRE_DOCUMENTO"] = objConcepto["DOC_ARCHIVO"].ToString();
                            if (Convert.ToInt32(objConcepto["NOT_AUT_ID"]) == (int)AutoridadesAmbientales.ANLA)
                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILA@" + objConcepto["DOC_ID"].ToString() + "_" + objInformacionPublicacion.Rows[0]["ID_ACTO_NOTIFICACION"].ToString() + "_" + objInformacionPublicacion.Rows[0]["ID_PERSONA"].ToString() + "_" + objInformacionPublicacion.Rows[0]["ID_ESTADO_PERSONA_ACTO"].ToString();
                            else
                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILAMC@" + objConcepto["DOC_ID"].ToString() + "_" + objInformacionPublicacion.Rows[0]["ID_ACTO_NOTIFICACION"].ToString() + "_" + objInformacionPublicacion.Rows[0]["ID_PERSONA"].ToString() + "_" + objInformacionPublicacion.Rows[0]["ID_ESTADO_PERSONA_ACTO"].ToString();
                            objDocumentos.Rows.Add(objDocumento);
                        }
                    }

                    //Cargar adjuntos
                    if(!string.IsNullOrEmpty(objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString()) &&
                       !objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().ToString().EndsWith("/") && !objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().ToString().EndsWith("\\")){

                        //Cargar datos
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().Substring((objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().LastIndexOf("/") != -1 ? objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().LastIndexOf("/") : objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionPublicacion.Rows[0]["RUTA_ADJUNTO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Validar si se incluyeron adjuntos
                    if (objDocumentos.Rows.Count > 0)
                    {
                        //Mostrar en grilla
                        this.grdAdjuntosModal.DataSource = objDocumentos;
                        this.grdAdjuntosModal.DataBind();

                        //Cargar triggers
                        this.CargarTriggersDocumentoGrilla(this.grdAdjuntosModal);

                        //Mostrar div
                        this.dvAdjuntosPublicacion.Visible = true;
                    }
                    else
                    {
                        //Limpiar grilla
                        this.grdAdjuntosModal.DataSource = null;
                        this.grdAdjuntosModal.DataBind();

                        //Ocultar div
                        this.dvAdjuntosPublicacion.Visible = false;
                    }
                }

            }
            else
            {
                throw new Exception("No se encontro información de la publicación " + lngPublicacionEstadoPersonaActoId.ToString());
            }
        }

    #endregion


    #region Eventos

        #region Page

            /// <summary>
            /// Evento que se ejecuta antes de cargar la pagina
            /// </summary>
            private void Page_PreInit(object sender, EventArgs e)
            {
                if (Page.Request["Ubic"] == null)
                    if (DatosSesion.Usuario != "")
                        this.Page.MasterPageFile = "~/plantillas/SILPA.master";

            }

            /// <summary>
            /// Inicializa la  pagina al momento de la carga de esta
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    this.InicializarPagina();
                }
            }

        #endregion


        #region btnRegresar
    
            /// <summary>
            /// Retorna a pagina origen
            /// </summary>
            protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
            {
                if (Page.Request["Ubic"] == null)
                    if (DatosSesion.Usuario != "")
                        Response.Redirect("../../SILPA/TESTSILPA/security/default.aspx");
                    else
                        Page.Response.Redirect("~/../ventanillasilpa");
                else
                    Page.Response.Redirect("~/../ventanillasilpa");
            }

        #endregion


        #region btnRegresar
    
            /// <summary>
            /// Realiza la busqueda de la información de publicaciones de acuerdo a alos parametros de busqueda suministrados
            /// </summary>
            protected void cmdBuscar_Click(object sender, EventArgs e)
            {
                //Ocultar Mensajes
                this.OcultarMensaje();

                //Cargar la información
                this.hdfTipoTramite.Value = this.cboTipoTramite.SelectedValue;
                this.hdfAutoridad.Value = this.cboAutoridad.SelectedValue;
                this.hdfNumeroVital.Value = this.txtNumeroVital.Text;
                this.hdfExpediente.Value = this.txtExpediente.Text;
                this.hdfTipoActo.Value = this.cboTipoActo.SelectedValue;
                this.hdfNumeroActo.Value = this.txtNumeroActo.Text;
                this.hdfIdentificacionUsuario.Value = this.txtIdentificacionUsuario.Text;
                this.hdfIncluirHistoricas.Value = this.chkIncluirHistoricas.Checked.ToString();
                this.hdfFechaDesde.Value = this.txtFechaDesde.Text;
                this.hdfFechaHasta.Value = this.txtFechaHasta.Text;

                //Cambiar pagina
                this.grdEstadosNotificaciones.PageIndex = 0;

                //Consultar la información
                this.BuscarInformacionEstadosPublicados();

                //Actualizar pantalla
                this.divEstadosNotificaciones.Visible = true;
                this.upnlEstadosNotificaciones.Update();
            }

        #endregion


        #region grdEstadosNotificaciones

            /// <summary>
            /// Evento que realiza el cambio de pagina
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdEstadosNotificaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Cambiar pagina
                this.grdEstadosNotificaciones.PageIndex = e.NewPageIndex;

                //Consultar la información
                this.BuscarInformacionEstadosPublicados();
            }


            /// <summary>
            /// Evento que muestra la información detallada de una publicación
            /// </summary>
            protected void lnkVer_Click(object sender, EventArgs e)
            {
                long lngPublicacionEstadoPersonaActoId = 0;

                try
                {
                    //Cargar identificador
                    lngPublicacionEstadoPersonaActoId = Convert.ToInt64(((LinkButton)sender).CommandArgument);

                    //Limpiar modal
                    this.LimpiarModalVer();

                    //Cargar datos de modal
                    this.ConsultarInformacionPublicacion(lngPublicacionEstadoPersonaActoId);

                    //Actualizar panel
                    this.upnlVerEstado.Update();

                    //Mostrar modal
                    this.mpeVerEstado.Show();
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PublicacionEstados :: lnkVer_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error cargando información detallada de la publicación");
                }

            }

        #endregion


        #region Ver Modal


            /// <summary>
            /// Evento que limpia y cierra ventana modal
            /// </summary>
            protected void cmdAceptarModal_Click(object sender, EventArgs e)
            {
                //Limpiar modal
                this.LimpiarModalVer();

                //Actualizar panel
                this.upnlVerEstado.Update();

                //Cerrar modal
                this.mpeVerEstado.Hide();
            }

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
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_PublicacionEstados :: imgDocumentoModal_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }

        #endregion

    #endregion


            
}