using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Notificacion;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.LogicaNegocio.Recurso;
using SILPA.LogicaNegocio.Generico;
using System.Data;
using SILPA.Comun;

public partial class NotificacionElectronica_ConsultaNotificacionPublico : System.Web.UI.Page
{
    public event System.EventHandler SeleccionesBound;
    public long _idApplicationUser = -1;
    public string _usuarioRegistrado = string.Empty;
    public PersonaIdentity personaIdentity;
    public List<EstadosNotificacionSelect> lstEstadosNotificacion;

    public PersonaIdentity personaIdentityView
    {
        get { return (PersonaIdentity)ViewState["personaIdentity"]; }
        set { ViewState["personaIdentity"] = value; }
    }

    public InfoActoNotificacion infoActoNotificacion
    {
        get { return (InfoActoNotificacion)ViewState["infoActoNotificacion"]; }
        set { ViewState["infoActoNotificacion"] = value; }
    }

    public bool EsNotificacionElectronica
    {
        get { return (bool)ViewState["EsNotificacionElectronica"]; }
        set { ViewState["EsNotificacionElectronica"] = value; }
    }

    public bool EsNotificacionElectronica_AA
    {
        get { return (bool)ViewState["EsNotificacionElectronica_AA"]; }
        set { ViewState["EsNotificacionElectronica_AA"] = value; }
    }

    public bool EsNotificacionElectronica_EXP
    {
        get { return (bool)ViewState["EsNotificacionElectronica_EXP"]; }
        set { ViewState["EsNotificacionElectronica_EXP"] = value; }
    }
    public int paginado;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.paginado = 10;

            if (ConfigurationManager.AppSettings["PaginadoGrilla"] != null)
            {
                this.paginado = Int32.Parse(ConfigurationManager.AppSettings["PaginadoGrilla"].ToString());
            }

            this.grdEstadosNotificacion.PageSize = paginado;

            Mensaje.LimpiarMensaje(this);
            if (!IsPostBack)
            {
                //Session["Usuario"] = "15456";
                //Session["IDForToken"] = "15456";

                if (ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {
                    //Si se va a consultar la informacion
                    this._usuarioRegistrado = (string)Session["Usuario"];

                    //SoftManagement.Log.SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "hava:validacion4: " + this._usuarioRegistrado);

                    PersonaDalc per = new PersonaDalc();
                    this.personaIdentity = per.BuscarPersonaByUserId(this._usuarioRegistrado);
                    personaIdentityView = personaIdentity;

                    PersonaIdentity p = new PersonaIdentity();
                    //p = per.ConsultarPersona(this._usuarioRegistrado);
                    int idAA = 0;
                    string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(this._usuarioRegistrado), out idAA);
                    //this.personaIdentity.IdAutoridadAmbiental = idAA;
                    //ViewState["IDAA"] = this.personaIdentity.IdAutoridadAmbiental;

                    ViewState["IDAppUser"] = long.Parse(this._usuarioRegistrado);

                    //SoftManagement.Log.SMLog.Escribir(SoftManagement.Log.Severidad.Informativo, "hava:validacion5: " +this._usuarioRegistrado);


                    ViewState["NombreFuncionario"] =
                    this.personaIdentity.PrimerNombre + " " +
                    this.personaIdentity.SegundoNombre + " " +
                    this.personaIdentity.PrimerApellido + " " +
                    this.personaIdentity.SegundoApellido;

                    ViewState["Identificacion"] = this.personaIdentity.NumeroIdentificacion;

                    TipoDocumentoDalc _tipoDocumentoDalc = new TipoDocumentoDalc();
                    //this.ddlTipoActo.DataSource = _tipoDocumentoDalc.ListarTiposDeDocumento(null, null);
                    this.ddlTipoActo.DataSource = _tipoDocumentoDalc.ListarTiposDeDocumentoNotificacion(null, null);
                    this.ddlTipoActo.DataTextField = "NOMBRE_DOCUMENTO";
                    this.ddlTipoActo.DataValueField = "ID";
                    this.ddlTipoActo.DataBind();
                    this.ddlTipoActo.Items.Insert(0, new ListItem("Seleccione...", "-1"));

                    EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                    List<EstadoNotificacionEntity> dsList = dalc.ListarEstadosNotificacionPublico();

                    this.cboEstadoNotificacion.DataSource = dsList;
                    this.cboEstadoNotificacion.DataTextField = "Descripcion";
                    this.cboEstadoNotificacion.DataValueField = "ID";
                    this.cboEstadoNotificacion.DataBind();
                    this.cboEstadoNotificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));

                    SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
                    cboAutoAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
                    cboAutoAmbiental.DataValueField = "AUT_ID";
                    cboAutoAmbiental.DataTextField = "AUT_NOMBRE";
                    cboAutoAmbiental.DataBind();
                    cboAutoAmbiental.Items.Insert(0, new ListItem("Seleccione.", "-1"));

                    // Consultamos los datos del solicitante en SILAMC

                    SolicitanteDalc solicitanteDalc = new SolicitanteDalc();
                    SolicitanteEntity solicitanteEntity = new SolicitanteEntity();
                    solicitanteEntity = solicitanteDalc.ConsultaSolicitante(Convert.ToInt32(this.personaIdentity.IdApplicationUser),null);

                    // validamos si el usuario tiene el permiso de notificacion electronica
                    EsNotificacionElectronica = solicitanteEntity.EsNotificacionElectronica;
                    EsNotificacionElectronica_AA = solicitanteEntity.EsNotificacionElectronica_AA;
                    EsNotificacionElectronica_EXP = solicitanteEntity.EsNotificacionElectronica_EXP;
                }
            }
            if ((Request.Params["__EVENTARGUMENT"] != null))
            {
                if (Request.Params["__EVENTARGUMENT"].ToString() == "Actualizar")
                {
                    ConsultarEstadosNotificacion();
                }
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }
    /// <summary>
    /// Método para verificar si el quejoso es usuario registrado.
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
            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Verificar que la segunda clave sea valida
    /// </summary>
    /// <returns>bool indicando si la clave es valida</returns>
    private bool SegundaClaveValida()
    {
        Persona objPersona = null;

        //Crear objeto de manejo de datos
        objPersona = new Persona();

        //Retornar resultado validacion
        return objPersona.ValidarSegundaClave(int.Parse(Session["Usuario"].ToString()), EnDecript.Encriptar(this.txtContrasena.Text.Trim()));
    }

    /// <summary>
    /// Consulta los estados de notificación
    /// </summary>
    public void ConsultarEstadosNotificacion()
    {
        this.pnlDocumentos.Visible = true;
        Notificacion not = new Notificacion();
        DateTime? fd = Convert.ToDateTime("01/01/1900 00:00:00");
        DateTime now = DateTime.Today;
        DateTime? fh = new DateTime(now.Year,now.Month,now.Day,23,59,59);

        int tipoActo = -1;
        int? idAutoAmbiental = null;
        int estadoActual = -1;
        string numeroIdentificacion = "";


        if (!String.IsNullOrEmpty(txtFechaDesde.Text))
        {
            fd = Convert.ToDateTime(txtFechaDesde.Text);
            fd = new DateTime(fd.Value.Year, fd.Value.Month, fd.Value.Day, 0, 0, 0);
        }
        if (!String.IsNullOrEmpty(txtFechaHasta.Text))
        {
            fh = Convert.ToDateTime(txtFechaHasta.Text);
            fh = new DateTime(fh.Value.Year, fh.Value.Month, fh.Value.Day, 23, 59, 59);
        }

        if (!String.IsNullOrEmpty(this.ddlTipoActo.SelectedValue))
        {
            tipoActo = Int32.Parse(this.ddlTipoActo.SelectedValue.ToString());
        }
        if (this.cboAutoAmbiental.SelectedValue != "-1")
        {
            idAutoAmbiental = Convert.ToInt32(this.cboAutoAmbiental.SelectedValue);
        }

        estadoActual = Convert.ToInt32(this.chkEstadoActual.Checked);

        if (ViewState["IDAppUser"] != null) { this._idApplicationUser = long.Parse(ViewState["IDAppUser"].ToString()); }

        if (ViewState["Identificacion"] != null) { numeroIdentificacion = ViewState["Identificacion"].ToString(); }
        this.lstEstadosNotificacion = new List<EstadosNotificacionSelect>();

        int estadoNotificacion = 0;

        // un cambio de prueba

        if (this.cboEstadoNotificacion.SelectedValue != "")
            estadoNotificacion = int.Parse(this.cboEstadoNotificacion.SelectedValue);

        //SMLog.Escribir(Severidad.Informativo, "User:" + this._idApplicationUser.ToString() + " Exp: " + this.txtExpediente.Text);

        this.lstEstadosNotificacion = not.ObtenerNotificacionesPublico(this.txtNumeroVital.Text, this.txtExpediente.Text, fd, fh, tipoActo,
            txtNumeroActo.Text, numeroIdentificacion, idAutoAmbiental, estadoActual, estadoNotificacion);

        string msg = "";        
        if (this.lstEstadosNotificacion.Count > 0)
        {
            if (!EsNotificacionElectronica && !EsNotificacionElectronica_AA && !EsNotificacionElectronica_EXP)
            {
                msg = "No se encuentra inscrito actualmente a notificicación electrónica.\n La información mostrada corresponde al historial de notificaciones electrónicas que posee.";
                Mensaje.MostrarMensaje(this, msg);
            }

            EnlazarDatos();
            this.pnlDocumentos.Visible = true;
        }
        else
        {
            if (!EsNotificacionElectronica && !EsNotificacionElectronica_AA && !EsNotificacionElectronica_EXP)
                msg = "No se encuentra inscrito actualmente a notificicación electrónica y no posee información historica que cumpla con los parámetros de búsqueda ingresados.";
            else
                msg = "No se encontraron notificaciones electrónicas que cumplan con los parámetros de búsqueda ingresados.";
            this.pnlDocumentos.Visible = false;
            this.lbl_total.Visible = false;
            this.lbl_de.Visible = false;
            this.lbl_numero_pagina.Visible = false;
            this.lbl_numero_paginas.Visible = false;
            Mensaje.MostrarMensaje(this, msg);
        }
    }
    /// <summary>
    /// Enlaza los datos de la consulta a la grilla
    /// </summary>
    public void EnlazarDatos()
    {
        this.grdEstadosNotificacion.DataSource = this.lstEstadosNotificacion;
        this.grdEstadosNotificacion.DataBind();
        int intPagina = this.grdEstadosNotificacion.PageIndex + 1;
        int totalRegistros = this.lstEstadosNotificacion.Count;

        this.lbl_total.Visible = true;
        this.lbl_total.Text = "Número total de registros: " + totalRegistros.ToString();

        this.lbl_numero_pagina.Text = intPagina.ToString();
        this.lbl_numero_paginas.Text = this.grdEstadosNotificacion.PageCount.ToString();

        this.lbl_pagina.Visible = (this.grdEstadosNotificacion.Rows.Count > 0);
        this.lbl_de.Visible = (this.grdEstadosNotificacion.Rows.Count > 0);
        this.lbl_numero_pagina.Visible = (this.grdEstadosNotificacion.Rows.Count > 0);
        this.lbl_numero_paginas.Visible = (this.grdEstadosNotificacion.Rows.Count > 0);
        this.lbl_total.Visible = (this.grdEstadosNotificacion.Rows.Count > 0);
    }
    /// <summary>
    /// Edición de datos de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdEstadosNotificacion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        this.infoActoNotificacion = new InfoActoNotificacion();
        //Cargar registro Seleccionado
        int index = Convert.ToInt32(e.CommandArgument);
        // Se crea el estado.
        if (e.CommandName == "Avanzar")
        {
            int indexMod = index % ((GridView)sender).DataKeys.Count;
            //grdEstadosNotificacion.SelectedIndex = indexMod;
            cargarDataKeys(indexMod);
            //TODO: Popup flujo de estados de notificacion.
            ConsultarEstadoSiguiente(int.Parse(this.infoActoNotificacion.idEstado));
            this.mpeAvanzarEstado.Show();
        }
        if (e.CommandName == "Descargar")
        {
            int indexMod = index % ((GridView)sender).DataKeys.Count;
            //grdEstadosNotificacion.SelectedIndex = indexMod;
            cargarDataKeys(indexMod);
            if (e.CommandArgument != null)
            {
                System.IO.FileInfo targetFile = new System.IO.FileInfo(this.infoActoNotificacion.Ubicacion);
                this.Response.Clear();
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                this.Response.ContentType = "application/octet-stream";
                this.Response.ContentType = "application/base64";
                this.Response.WriteFile(targetFile.FullName);
                this.Response.WriteFile(this.infoActoNotificacion.Ubicacion);
            }
        }
    }
    private void cargarDataKeys(int index)
    {
        this.grdEstadosNotificacion.SelectedIndex = index;
        if (this.grdEstadosNotificacion.SelectedDataKey["IdPersonaNotificar"] != null)
        {
            this.infoActoNotificacion.idPersona = this.grdEstadosNotificacion.SelectedDataKey["IdPersonaNotificar"].ToString();
        }

        this.infoActoNotificacion.idEstado = this.grdEstadosNotificacion.SelectedDataKey["IdEstadoNotificado"].ToString();
        this.infoActoNotificacion.numeroSilpa = this.grdEstadosNotificacion.SelectedDataKey["NumeroSilpa"].ToString();
        this.infoActoNotificacion.idActoNot = this.grdEstadosNotificacion.SelectedDataKey["IdActoNotificacion"].ToString();
        this.infoActoNotificacion.fechaEstadoNotificado = this.grdEstadosNotificacion.SelectedDataKey["FechaEstadoNotificado"].ToString();
        this.infoActoNotificacion.expediente = this.grdEstadosNotificacion.SelectedDataKey["Expediente"].ToString();
        this.infoActoNotificacion.FechaActo = this.grdEstadosNotificacion.SelectedDataKey["FechaActo"].ToString();
        this.infoActoNotificacion.TipoActo = this.grdEstadosNotificacion.SelectedDataKey["TipoActoAdministrativo"].ToString();
        this.infoActoNotificacion.NumeroActo = this.grdEstadosNotificacion.SelectedDataKey["NumeroActoAdministrativo"].ToString();
        this.infoActoNotificacion.Usuario = this.grdEstadosNotificacion.SelectedDataKey["UsuarioNotificar"].ToString();
        this.infoActoNotificacion.Identificacion = this.grdEstadosNotificacion.SelectedDataKey["NumeroIdentificacionUsuario"].ToString();
        this.infoActoNotificacion.DiasVence = this.grdEstadosNotificacion.SelectedDataKey["DiasVencimiento"].ToString();
        this.infoActoNotificacion.Ubicacion = this.grdEstadosNotificacion.SelectedDataKey["Archivo"].ToString();
        this.infoActoNotificacion.Autoridad = this.grdEstadosNotificacion.SelectedDataKey["NombreAutoridad"].ToString();
        this.infoActoNotificacion.IdAutoridad = this.grdEstadosNotificacion.SelectedDataKey["IdAutoridad"].ToString();
        this.infoActoNotificacion.ArchivosAdjuntos = this.grdEstadosNotificacion.SelectedDataKey["ArchivosAdjuntos"].ToString();

        int i = this.grdEstadosNotificacion.DataKeys.Count;

        if (this.grdEstadosNotificacion.SelectedDataKey["IdProcesoNotificacion"] != null)
        {
            this.infoActoNotificacion.IDProcesoNot = this.grdEstadosNotificacion.SelectedDataKey["IdProcesoNotificacion"].ToString();
        }

        if (this.grdEstadosNotificacion.SelectedDataKey["EstadoNotificado"] != null)
        {
            this.infoActoNotificacion.EstadoNotificado = this.grdEstadosNotificacion.SelectedDataKey["EstadoNotificado"].ToString();
        }


        if (System.Boolean.TrueString == this.grdEstadosNotificacion.SelectedDataKey["EstadoCambioPDI"].ToString())
        {
            this.infoActoNotificacion.EsPDI = "SI";
        }
        else
        {
            this.infoActoNotificacion.EsPDI = "NO";
        }
    }
    [Serializable]
    public class InfoActoNotificacion
    {

        public string qrys;
        public string idPersona = "-1";
        public string idEstado = "-1";
        public string numeroSilpa = string.Empty;
        public string expediente = string.Empty;
        public string idActoNot = "-1";
        public string fechaEstadoNotificado = DateTime.Now.ToString();

        public string FechaActo = DateTime.Now.ToString();
        public string TipoActo = string.Empty;
        public string NumeroActo = string.Empty;
        public string Usuario = string.Empty;
        public string Identificacion = string.Empty;
        public string DiasVence = string.Empty;
        public string EsPDI = string.Empty;
        public string IDProcesoNot = string.Empty;
        public string EstadoNotificado = string.Empty;
        public string Ubicacion = string.Empty;
        public string Autoridad = string.Empty;
        public string IdAutoridad = string.Empty;
        public string ArchivosAdjuntos = string.Empty;
    }
    public void Limpiar()
    {
        this.cboAutoAmbiental.SelectedIndex = 0;
        this.txtExpediente.Text = string.Empty;
        this.txtFechaDesde.Text = string.Empty;
        this.txtFechaHasta.Text = string.Empty;
        this.txtNumeroActo.Text = string.Empty;
        this.txtNumeroVital.Text = string.Empty;
        this.ddlTipoActo.SelectedIndex = 0;

    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.ConsultarEstadosNotificacion();
    }
    protected void btnCancelar_Click1(object sender, EventArgs e)
    {
        this.pnlDocumentos.Visible = false;
        this.Limpiar();

    }
    protected void grdEstadosNotificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEstadosNotificacion.PageIndex = e.NewPageIndex;
        int intPageActual = grdEstadosNotificacion.PageIndex + 1;
        this.lbl_numero_pagina.Text = intPageActual.ToString();
        this.ConsultarEstadosNotificacion();
    }
    protected void grdEstadosNotificacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
             // Evaluamos el estado del acto administrativo
            if (Boolean.Parse(((Label)e.Row.FindControl("LblMostrarInformacion")).Text))
            {
                ((Label)e.Row.FindControl("LblTipoActo")).Visible = false;
                //((Label)e.Row.FindControl("LblNumeroActo")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnkDescargar")).Visible = false;
            }
            ((Button)e.Row.FindControl("btnAvanzarEstado")).CommandArgument = e.Row.RowIndex.ToString();
            ((LinkButton)e.Row.FindControl("lnkDescargar")).CommandArgument = e.Row.RowIndex.ToString();
            if ((EsNotificacionElectronica)||(EsNotificacionElectronica_AA)||(EsNotificacionElectronica_EXP))
            {
                if (((Label)e.Row.FindControl("LblTieneActividadSiguiente")).Text == "0")
                {
                    ((Button)e.Row.FindControl("btnAvanzarEstado")).Visible = false;
                }

                if (((Label)e.Row.FindControl("LblValidarDias")).Text != "")
                {
                    if (((Label)e.Row.FindControl("LblDiasVencimiento")).Text == "0")
                    {
                        ((Button)e.Row.FindControl("btnAvanzarEstado")).Visible = false;
                    }
                }
                else
                {
                    ((Button)e.Row.FindControl("btnAvanzarEstado")).Visible = false;
                }

              
            }
            else
            {
                ((Button)e.Row.FindControl("btnAvanzarEstado")).Visible = false;
            }
            if (((Label)e.Row.FindControl("LblEstadoNotificadoId")).Text == "4")
            {
                string archivos = ((Label)e.Row.FindControl("lblArchivosAdjuntos")).Text;
                string[] achivosAdjuntos = archivos.Split(Convert.ToChar(";"));
                List<DocumentoRuta> listaArchivos = new List<DocumentoRuta>();
                if (achivosAdjuntos != null)
                {
                    if (achivosAdjuntos.Length > 0)
                    {
                        foreach (string archivo in achivosAdjuntos)
                        {
                            if (archivo != string.Empty)
                            {
                                System.IO.FileInfo documento = new System.IO.FileInfo(archivo);
                                listaArchivos.Add(new DocumentoRuta(documento.Name,archivo));
                            }
                        }
                        if(listaArchivos.Count > 0)
                        {
                            DataList dtlArchivosRecurso = (DataList)e.Row.FindControl("dtlArchivosRecurso");
                            dtlArchivosRecurso.Visible = true;
                            dtlArchivosRecurso.DataSource = listaArchivos;
                            dtlArchivosRecurso.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void ConsultarEstadoSiguiente(int estadoID)
    {
        //TODO: Consulta y llena el combo de Estado Siguiente.
        EstadoNotificacionDalc estadoNotificacionDalc = new EstadoNotificacionDalc();
        NotificacionDalc dalc = new NotificacionDalc();
        Notificacion objNotificacion = new Notificacion();
        NotificacionEntity entity = dalc.ObtenerActo(new object[] { this.infoActoNotificacion.idActoNot, null, null, null, null, null, null, null, null, null, null });
        //Cargar el flujo que se debe ejecutar
        if (entity.AplicaRecursoReposicion != null)
        {
            if (entity.AplicaRecursoReposicion == true)
            {
                entity.IdTipoActo.IdFlujoNotElec = 1; //Flujo con recurso
            }
            else
            {
                entity.IdTipoActo.IdFlujoNotElec = 2;  //Flujo sin recurso
            }
        }
        else
        {
            //En caso de que el flujo sea nulo cargarlo con recurso
            if (entity.IdTipoActo.IdFlujoNotElec == null)
            {
                entity.IdTipoActo.IdFlujoNotElec = 1;
            }
        }

        List<EstadoFlujoEntity> listaEstadosFlujo = estadoNotificacionDalc.ListaSiguienteEstado(estadoID, true, true, entity.IdTipoActo.IdFlujoNotElec.Value);
       
        //Calcular los dias que lleva la solicitud en el estado actual
        int diasdiferencia = objNotificacion.ObtenerNumeroDiasNotificacion(this.infoActoNotificacion.idPersona, this.infoActoNotificacion.idActoNot);

        List<EstadoFlujoEntity> listaEstadosPosibles = new List<EstadoFlujoEntity>();
        foreach (EstadoFlujoEntity estadoflujo in listaEstadosFlujo)
        {
            if (estadoflujo.NroDiasTransicion >= diasdiferencia || estadoflujo.NroDiasTransicion <= 0)
                listaEstadosPosibles.Add(estadoflujo);
        }
        this.cboSigEstado.DataSource = listaEstadosPosibles;
        this.cboSigEstado.DataTextField = "NombreEstado";
        this.cboSigEstado.DataValueField = "EstadoID";
        this.cboSigEstado.DataBind();
        this.cboSigEstado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void btnAvanzar_Click(object sender, EventArgs e)
    {
        //Verificar que la segunda clave sea valida
        if (this.SegundaClaveValida())
        {
            //TODO: Validamos si la actividad seleccionada llama a otro formulario para poder avanzar el estado.
            EstadoNotificacionDalc estadoNotificacionDalc = new EstadoNotificacionDalc();
            EstadoFlujoEntity estado = estadoNotificacionDalc.ConsultaEstadoFlujo(Convert.ToInt32(this.cboSigEstado.SelectedValue), Convert.ToInt32(this.infoActoNotificacion.idEstado));
            if (estado.URL.Trim() != string.Empty)
            {
                ListarActosRecursosReposicion listar = new ListarActosRecursosReposicion();
                List<RecursoReposicionPresentacion> listasRecursos = listar.listarActosRecursos(infoActoNotificacion.numeroSilpa, infoActoNotificacion.expediente, infoActoNotificacion.NumeroActo, null, null, Session["Usuario"].ToString());
                if (listasRecursos.Count > 0)
                {
                    Session["RegistroRecurso"] = listasRecursos[0];
                    Listas _listaNumeroSILPA = new Listas();
                    DataSet listaNotificacion = _listaNumeroSILPA.ListarActosNotificacion(Session["Usuario"].ToString(), "", "", "");
                    Session["NumeroSilpa2"] = infoActoNotificacion.numeroSilpa;

                    foreach (DataRow row in listaNotificacion.Tables[0].Rows)
                    {
                        if (row["NUMERO_VITAL_NOMBRE"].ToString() == listasRecursos[0].NumeroSILPA)
                        {
                            Session["NumeroSilpa"] = row["NOT_NUMERO_SILPA"].ToString();
                        }
                    }
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Notificación Electrónica", "popup('" + estado.URL + "');", true);
                }
                else
                {
                    Mensaje.MostrarMensaje(this, "No tiene ningun recurso para Interponer.");
                }
            }
            else
            {
                EstadoNotificacionEntity estadoNotificacion = estadoNotificacionDalc.ListarEstadoNotificacion(new object[] { estado.EstadoID });
                CrearEditarEstado(false);
            }
            ConsultarEstadosNotificacion();
        }
        else
        {
            Mensaje.MostrarMensaje(this, "La segunda clave se encuentra incorrecta.");
        }
    }

    /// <summary>
    /// Crea un estado persona acto
    /// </summary>
    public string CrearEditarEstado(bool enviaCorreo)
    {
        PersonaDalc per = null;
        Notificacion not = new Notificacion();
        int idAA = 0;
        string result = String.Empty;

        //Obtener autoridad ambiental
        per = new PersonaDalc();
        per.ObtenerAutoridadPorPersona(long.Parse((string)Session["Usuario"]), out idAA);

        result = not.CrearEstadoPersonaActo
            (
                long.Parse(this.infoActoNotificacion.idActoNot), 
                Convert.ToInt32(this.infoActoNotificacion.idEstado),
                Int32.Parse(this.cboSigEstado.SelectedValue.ToString()),
                long.Parse(this.infoActoNotificacion.idPersona),
                DateTime.Now, this.infoActoNotificacion.numeroSilpa,
                this.personaIdentityView.CorreoElectronico,
                null,
                null,
                "Crear", enviaCorreo, ViewState["NombreFuncionario"].ToString(),
                this.infoActoNotificacion.expediente, this.infoActoNotificacion.NumeroActo, idAA, false);

        //if (this.infoActoNotificacion.numeroSilpa != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
        //{
            // si no existe error se ejecuta avanzar tarea.
            if (result == String.Empty)
            {
                SILPA.Servicios.NotificacionFachada notFach = new SILPA.Servicios.NotificacionFachada();

                if (this.cboSigEstado.SelectedItem.Text == "EJECUTORIADA")
                {
                    notFach.ComponenteNotManual(this.cboSigEstado.SelectedValue.ToString(), this.cboSigEstado.SelectedItem.Text, this.infoActoNotificacion.EsPDI, long.Parse(this.infoActoNotificacion.idActoNot), "Ejecutoriar", long.Parse(this.infoActoNotificacion.idPersona));
                }
                else
                {
                    List<string> objProceso = notFach.ActualizarProcesos(int.Parse(this.cboSigEstado.SelectedValue), this.cboSigEstado.SelectedItem.Text, this.infoActoNotificacion.EsPDI, long.Parse(this.infoActoNotificacion.idActoNot), long.Parse(this.infoActoNotificacion.idPersona));
                }
            }
        //}
        int accionAuditoria = 1;
        string strDetalle = string.Empty;
        accionAuditoria = 1; strDetalle = "Se creó el estado " + this.cboSigEstado.SelectedItem.Text.ToString();
        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("SILPA", accionAuditoria, strDetalle);
        return result;
    }


    protected void dtlArchivosRecurso_ItemCommand(object source, DataListCommandEventArgs e)
    {
        // buscamos el label que contiene la ruta del archivo para descargarlo
        Label lblRutaArchivo = (Label)e.Item.FindControl("lblRutaArchivo");
        System.IO.FileInfo targetFile = new System.IO.FileInfo(lblRutaArchivo.Text);
        this.Response.Clear();
        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
        this.Response.ContentType = "application/octet-stream";
        this.Response.ContentType = "application/base64";
        this.Response.WriteFile(targetFile.FullName);
        this.Response.WriteFile(lblRutaArchivo.Text);
    }
}
