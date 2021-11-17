using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Notificacion;
using SILPA.Comun;
using System.IO;
using System.Collections.Generic;
using SoftManagement.Log;

public partial class NotificacionElectronica_EmitirDocumento : System.Web.UI.Page
{
    public event System.EventHandler SeleccionesBound;
    public long _idApplicationUser = -1;
    public string _usuarioRegistrado = string.Empty;
    public PersonaIdentity personaIdentity;
    public List<EstadosNotificacionSelect> lstEstadosNotificacion;

    public qrystring qrystr
    {
        get { return (qrystring)ViewState["qrystr"]; }
        set { ViewState["qrystr"] = value; }
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
                //Session["Usuario"] = "460";

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

                    TipoDocumentoDalc _tipoDocumentoDalc = new TipoDocumentoDalc();
                    //this.ddlTipoActo.DataSource = _tipoDocumentoDalc.ListarTiposDeDocumento(null, null);
                    this.ddlTipoActo.DataSource = _tipoDocumentoDalc.ListarTiposDeDocumentoNotificacion(null, null);
                    this.ddlTipoActo.DataTextField = "NOMBRE_DOCUMENTO";
                    this.ddlTipoActo.DataValueField = "ID";
                    this.ddlTipoActo.DataBind();
                    this.ddlTipoActo.Items.Insert(0, new ListItem("Seleccione...", "-1"));

                    this.ddlProvienePDI.Items.Clear();
                    ListItem item1 = new ListItem("Seleccione...","-1");
                    ListItem item2 = new ListItem("Si","1");
                    ListItem item3 = new ListItem("No","0");

                    this.ddlProvienePDI.Items.Add(item1);
                    this.ddlProvienePDI.Items.Add(item2);
                    this.ddlProvienePDI.Items.Add(item3);
                    EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
                    List<EstadoNotificacionEntity> dsList = dalc.ListarEstadosNotificacion();

                    this.cboEstadoNotificacion.DataSource=dsList;
                    this.cboEstadoNotificacion.DataTextField = "Descripcion";
                    this.cboEstadoNotificacion.DataValueField = "ID";
                    this.cboEstadoNotificacion.DataBind();
                    this.cboEstadoNotificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));

	                txtFechaDesde.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
	                txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            //this.btnAceptar.Attributes.Add("onClick", "return confirm('Despues de guardar no podrá realizar ningún cambio. Está seguro que desea guardar el registro?')");
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
    }
    protected void txtNumeroVital_TextChanged(object sender, EventArgs e)
    {

    }      
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
    }
    protected void btnQuitar_Click(object sender, EventArgs e)
    {
      
    }

    private void SetFocusControl(string ControlName)
    {
        string script = "<script language=\"javascript\">var control = document.getElementById(\"" + ControlName + "\"); if( control != null ){control.focus();}</script>";
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Focus", script);
    }
    protected virtual void OnBound(object sender)
    {
        // Dispara el evento Selecciones Bound 
        if (this.SeleccionesBound != null)
            this.SeleccionesBound(sender, new EventArgs());
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
    


    protected void AdicionarArchivo()
    {
    }

    protected void btnAdjuntar_Click(object sender, EventArgs e)
    {
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Consulta los estados de notificación
    /// </summary>
    public void ConsultarEstadosNotificacion()
    {
        this.pnlDocumentos.Visible = true;
        Notificacion not = new Notificacion();

        //DateTime fd = null;
        //DateTime fh = null;

        DateTime fd = Convert.ToDateTime("01/01/1900");
        DateTime fh = Convert.ToDateTime("01/01/1900");
        
        int tipoActo = -1;
        int diasVenciiento = 0;
        int provienePDI = -1;
        int estadoActual = -1;


        if (!String.IsNullOrEmpty(txtFechaDesde.Text))
        {
            //fd = txtFechaDesde.Text;
            fd = Convert.ToDateTime(txtFechaDesde.Text);
        }
        if (!String.IsNullOrEmpty(txtFechaHasta.Text))
        {
            //fh = txtFechaHasta.Text;
            fh = Convert.ToDateTime(txtFechaHasta.Text);
        }

        if (!String.IsNullOrEmpty(this.ddlTipoActo.SelectedValue))
        {
            tipoActo = Int32.Parse(this.ddlTipoActo.SelectedValue.ToString());
        }

        if (!String.IsNullOrEmpty(txtDiasVenimiento.Text))
        {
            diasVenciiento = Int32.Parse(txtDiasVenimiento.Text.ToString());
        }

        if (!String.IsNullOrEmpty(txtDiasVenimiento.Text))
        {
            diasVenciiento = Int32.Parse(txtDiasVenimiento.Text.ToString());
        }

        //provienePDI = Int32.Parse(ddlProvienePDI.SelectedValue.ToString());

        if (ddlProvienePDI.SelectedIndex == 0) 
        {
            provienePDI = -1;
        }

        if (ddlProvienePDI.SelectedIndex == 1)
        {
            provienePDI = 1;
        }


        if (ddlProvienePDI.SelectedIndex == 2)
        {
            provienePDI = 0;
        }


        estadoActual = Convert.ToInt32(this.chkEstadoActual.Checked);

        //this._idAA = this.personaIdentity.IdAutoridadAmbiental;
        if (ViewState["IDAppUser"] != null) { this._idApplicationUser = long.Parse(ViewState["IDAppUser"].ToString()); }

        this.lstEstadosNotificacion = new List<EstadosNotificacionSelect>();

        //string datos = this._idApplicationUser.ToString() + " : " +
        //    this.txtNumeroVital.Text + " : " + this.txtExpediente.Text + " " + fd.ToString() + " " + fh.ToString() 
        //    + " : " + tipoActo.ToString() + " : " +
        //    txtNumeroActo.Text + " : " + txtUsuarioNotificar.Text + " : " + txtIdentificacionUsuario.Text + " : " + diasVenciiento.ToString() + " : " +
        //    provienePDI.ToString() + " : " + txtProcesoNotificacion.Text + " : " + estadoActual.ToString();
        int estadoNotificacion=0;

        // un cambio de prueba

        if (this.cboEstadoNotificacion.SelectedValue != "")
            estadoNotificacion = int.Parse(this.cboEstadoNotificacion.SelectedValue);

        SMLog.Escribir(Severidad.Informativo, "User:" + this._idApplicationUser.ToString() + " Exp: " + this.txtExpediente.Text);

        this.lstEstadosNotificacion = not.ObtenerEstadosNotificacion(this._idApplicationUser,
            this.txtNumeroVital.Text, this.txtExpediente.Text, fd, fh, tipoActo,
            txtNumeroActo.Text, txtUsuarioNotificar.Text, txtIdentificacionUsuario.Text,
            diasVenciiento,
            provienePDI,
            txtProcesoNotificacion.Text, estadoActual,estadoNotificacion);

        if (this.lstEstadosNotificacion.Count > 0)
        {
            EnlazarDatos();
            this.pnlDocumentos.Visible = true;

        }
        else 
        {
           string msg = "No se encontraron resultados";
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

    public void CrearEstado() 
    { 

    }

    public void ModificarEstado() { }

    /// <summary>
    /// Elimina un estado de la tabla: NOT_ESTADO_PERSONA_ACTO
    /// Mediante su identificador.
    /// </summary>
    /// <param name="idItem">long: identificador de la tabla: NOT_ESTADO_PERSONA_ACTO</param>
    public void EliminarEstado(long idItem)
    {
        int item = 0;
        int itemSelected = -1;
        EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();
        dalc.EliminarEstadoPersonaActo(idItem);

        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("SILPA", 4, "Se eliminó el estado de notificacion.");

        ConsultarEstadosNotificacion();

    }



    /// <summary>
    /// Edición de datos de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdEstadosNotificacion_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        this.qrystr = new qrystring();
        ///  paginado de las grillas por defecto
        //int paginado = 5;
        
        //if (ConfigurationManager.AppSettings["PaginadoGrilla"] != null) 
        //{
        //    paginado = Int32.Parse(ConfigurationManager.AppSettings["PaginadoGrilla"].ToString());
        //}

       // ((GridView)sender).PageSize = paginado;

        //Cargar registro Seleccionado
        int index = Convert.ToInt32(e.CommandArgument);

        //int indexMod = index % paginado;
        int indexMod = index % ((GridView)sender).DataKeys.Count;
        //if (indexMod > 0)
        //    indexMod--;

        //DataKey dk = this.grdEstadosNotificacion.DataKeys[indexMod];

        //int pagina = grdEstadosNotificacion.PageIndex;
        //if (index > paginado-1)
        //{
        //    index = index - (pagina * paginado);
        //}
        


        
        // Se crea el estado.
        if (e.CommandName == "Crear")
        {
            LimpiarAvance();
            grdEstadosNotificacion.SelectedIndex = indexMod;
            cargarDataKeys(indexMod);
            //Response.Redirect(@"../NotificacionElectronica/CrearEstadosNotificacion.aspx?cmd=Crear" + this.qrystr.qrys);
            //Response.Redirect(@"../NotificacionElectronica/CrearEstadosNotificacion.aspx?cmd=Crear" + this.qrystr.qrys);
            CargarDatos("Crear");
            this.mpeAvanzarEstado.Show();
        }
        // Se edita el estado.
        if (e.CommandName == "Editar")
        {
            LimpiarAvance();
            grdEstadosNotificacion.SelectedIndex = indexMod;
            cargarDataKeys(indexMod);

            if(this.grdEstadosNotificacion.SelectedDataKey["EstadoCambioPDI"]!=null)
            {
                if (this.grdEstadosNotificacion.SelectedDataKey["EstadoCambioPDI"].ToString() == System.Boolean.FalseString)
                {

                    CargarDatos("Editar");
                    this.mpeAvanzarEstado.Show();
                }
                else 
                {
                    string popupScript = "<script>alert('No es posible Editar un registro generado desde el SISTEMA PDI.')</script>";
                    if (!this.Page.ClientScript.IsStartupScriptRegistered("MensajeEditar"))
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MensajeEditar", popupScript);
                }
            }
        }
        // Se elimina el estado.
        if (e.CommandName == "Eliminar")
        {
            LimpiarAvance();
            grdEstadosNotificacion.SelectedIndex = indexMod;
            cargarDataKeys(indexMod);
            //  no es cambio pdi: se puede eliminar
            if (this.grdEstadosNotificacion.SelectedDataKey["EstadoCambioPDI"].ToString() == System.Boolean.FalseString) 
            {
                Int64 k = Int64.Parse(this.grdEstadosNotificacion.SelectedDataKey["ID"].ToString());
                this.EliminarEstado(k);

            }else
            {
                string popupScript = "<script>alert('No es posible Eliminar un registro generado desde el SISTEMA PDI.')</script>";
                if (!this.Page.ClientScript.IsStartupScriptRegistered("MensajeEliminar"))
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MensajeEliminar", popupScript);
            }
        }

        if (e.CommandName == "Descargar")
        {
            if (e.CommandArgument != null)
            {
                grdEstadosNotificacion.SelectedIndex = indexMod;
                cargarDataKeys(indexMod);
                System.IO.FileInfo targetFile = new System.IO.FileInfo(this.qrystr.Ubicacion);
                this.Response.Clear();
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                this.Response.ContentType = "application/octet-stream";
                this.Response.ContentType = "application/base64";
                this.Response.WriteFile(targetFile.FullName);
                this.Response.WriteFile(this.qrystr.Ubicacion);
            }
        }

        if (e.CommandName == "ModificarFechaEstado")
        {
            grdEstadosNotificacion.SelectedIndex = indexMod;
            cargarDataKeys(indexMod);
            //string IdPersona = this.qrystr.idPersona;
            //string EstadoID = this.qrystr.idEstado;
            this.mpeModificarFechaEstado.Show();
            this.txtEditFechaEstado.Text = ((Label)this.grdEstadosNotificacion.Rows[indexMod].FindControl("lblFechaEstadoNoficado")).Text;
        }
    }

    private void cargarDataKeys(int index) 
    {

        //grdEstadosNotificacion.SelectedIndex = index;
        // ID,IdSolicitud,IdEstadoNotificado,EstadoCambioPDI,IdActoNotificacion,
        //NumeroSilpa,IdApplicationUser,IdPersonaNotificar,FechaEstadoNotificado,Expediente,
        //TipoActoAdministrativo,FechaActo,UsuarioNotificar,NumeroIdentificacionUsuario,NumeroActoAdministrativo,
        //DiasVencimiento,
        //IdProcesoNotificacion,EstadoNotificado,Archivo,NombreAutoridad,IdAutoridad
        //DataKey dk = this.grdPublicaciones.DataKeys[_indice];

       // DataKey dk  = this.grdEstadosNotificacion.DataKeys[0];

        if (this.grdEstadosNotificacion.SelectedDataKey["IdPersonaNotificar"] != null)
        {
            this.qrystr.idPersona = this.grdEstadosNotificacion.SelectedDataKey["IdPersonaNotificar"].ToString();
        }

        this.qrystr.idEstado = this.grdEstadosNotificacion.SelectedDataKey["IdEstadoNotificado"].ToString();
        this.qrystr.numeroSilpa = this.grdEstadosNotificacion.SelectedDataKey["NumeroSilpa"].ToString();
        this.qrystr.idActoNot = this.grdEstadosNotificacion.SelectedDataKey["IdActoNotificacion"].ToString();
        this.qrystr.fechaEstadoNotificado = this.grdEstadosNotificacion.SelectedDataKey["FechaEstadoNotificado"].ToString();
        this.qrystr.expediente = this.grdEstadosNotificacion.SelectedDataKey["Expediente"].ToString();
        this.qrystr.FechaActo = this.grdEstadosNotificacion.SelectedDataKey["FechaActo"].ToString();
        this.qrystr.TipoActo = this.grdEstadosNotificacion.SelectedDataKey["TipoActoAdministrativo"].ToString();
        this.qrystr.NumeroActo = this.grdEstadosNotificacion.SelectedDataKey["NumeroActoAdministrativo"].ToString();
        this.qrystr.Usuario = this.grdEstadosNotificacion.SelectedDataKey["UsuarioNotificar"].ToString();
        this.qrystr.Identificacion = this.grdEstadosNotificacion.SelectedDataKey["NumeroIdentificacionUsuario"].ToString();
        this.qrystr.DiasVence = this.grdEstadosNotificacion.SelectedDataKey["DiasVencimiento"].ToString();
        this.qrystr.Ubicacion = this.grdEstadosNotificacion.SelectedDataKey["Archivo"].ToString();
        this.qrystr.Autoridad = this.grdEstadosNotificacion.SelectedDataKey["NombreAutoridad"].ToString();
        this.qrystr.IdAutoridad = this.grdEstadosNotificacion.SelectedDataKey["IdAutoridad"].ToString();

        int i = this.grdEstadosNotificacion.DataKeys.Count;

        if (this.grdEstadosNotificacion.SelectedDataKey["IdProcesoNotificacion"] != null)
        {
            this.qrystr.IDProcesoNot = this.grdEstadosNotificacion.SelectedDataKey["IdProcesoNotificacion"].ToString();
        }

        if (this.grdEstadosNotificacion.SelectedDataKey["EstadoNotificado"] != null)
        {
            this.qrystr.EstadoNotificado = this.grdEstadosNotificacion.SelectedDataKey["EstadoNotificado"].ToString();
        }


        if (System.Boolean.TrueString == this.grdEstadosNotificacion.SelectedDataKey["EstadoCambioPDI"].ToString())
        {
            this.qrystr.EsPDI = "SI";
        }
        else
        {
            this.qrystr.EsPDI = "NO";
        }
    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Validate("Consulta");
        if(IsValid)
            this.ConsultarEstadosNotificacion();       
    }

    protected void grdEstadosNotificacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        EstadosNotificacionSelect objEstado = null;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Button)e.Row.FindControl("btnAvanzarEstado")).CommandArgument = e.Row.RowIndex.ToString();
            ((Button)e.Row.FindControl("btnEditarEstado")).CommandArgument = e.Row.RowIndex.ToString();
            ((Button)e.Row.FindControl("btnEliminarEstado")).CommandArgument = e.Row.RowIndex.ToString();
            ((LinkButton)e.Row.FindControl("lnkDescargar")).CommandArgument = e.Row.RowIndex.ToString();
            ((ImageButton)e.Row.FindControl("btniEditarFecha")).CommandArgument = e.Row.RowIndex.ToString();

            //Cargar Datos
            objEstado = (EstadosNotificacionSelect)e.Row.DataItem;


            if (((Label)e.Row.FindControl("lblActoNotiElect")).Text == "Si")
            {
                ((Label)e.Row.FindControl("TxtlblNotiElect")).Text = "Si";

                if (((Label)e.Row.FindControl("lblNotiElect")).Text == "Si")
                {
                    ((Label)e.Row.FindControl("TxtlblTipoNotiElect")).Text = "Completa";
                }
                else
                {
                    if (((Label)e.Row.FindControl("lblNotiElectXAA")).Text == "Si")
                    {
                        ((Label)e.Row.FindControl("TxtlblTipoNotiElect")).Text = "Por Autoridad Ambiental";
                    }
                    else
                    {
                        if (((Label)e.Row.FindControl("lblNotiElectXEXP")).Text == "Si")
                        {
                            ((Label)e.Row.FindControl("TxtlblTipoNotiElect")).Text = "Por Expediente";
                        }
                        else
                        {
                            ((Label)e.Row.FindControl("TxtlblTipoNotiElect")).Text = "No Aplica";
                        }
                    }
                }
            }
            else
            {
                ((Label)e.Row.FindControl("TxtlblNotiElect")).Text = "No";
            }
            
            
            if (((Label)e.Row.FindControl("LblTieneActividadSiguiente")).Text == "0")
            {
                ((Button)e.Row.FindControl("btnAvanzarEstado")).Visible = false;
            }
            else
            {
                ((Button)e.Row.FindControl("btnAvanzarEstado")).Visible = true;
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
                                listaArchivos.Add(new DocumentoRuta(documento.Name, archivo));
                            }
                        }
                        if (listaArchivos.Count > 0)
                        {
                            DataList dtlArchivosRecurso = (DataList)e.Row.FindControl("dtlArchivosRecurso");
                            dtlArchivosRecurso.Visible = true;
                            dtlArchivosRecurso.DataSource = listaArchivos;
                            dtlArchivosRecurso.DataBind();
                        }
                    }
                }
            }


            if (objEstado.EstadoActual && objEstado.EsModificable)
            {                
                ((Button)e.Row.FindControl("btnEliminarEstado")).Visible = true;
                ((Button)e.Row.FindControl("btnEditarEstado")).Visible = true;
            }
            else if (objEstado.EsModificable)
            {
                ((Button)e.Row.FindControl("btnEditarEstado")).Visible = true;
                ((Button)e.Row.FindControl("btnEliminarEstado")).Visible = false;
            }
            else
            {
                ((Button)e.Row.FindControl("btnEditarEstado")).Visible = false;
                ((Button)e.Row.FindControl("btnEliminarEstado")).Visible = false;
            }            
        }
    }

    public void Limpiar()
    {
        this.txtDiasVenimiento.Text = string.Empty;
        this.txtExpediente.Text = string.Empty;
        this.txtFechaDesde.Text = string.Empty;
        this.txtFechaHasta.Text = string.Empty;
        this.txtIdentificacionUsuario.Text = string.Empty;
        this.txtNumeroActo.Text = string.Empty;
        this.txtNumeroVital.Text = string.Empty;
        this.txtProcesoNotificacion.Text = string.Empty;
        this.txtUsuarioNotificar.Text = string.Empty;
        this.ddlProvienePDI.SelectedIndex = 0;
        this.ddlTipoActo.SelectedIndex = 0;

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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
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
                Convert.ToInt64(this.qrystr.idActoNot), Convert.ToInt32(this.qrystr.idEstado),
                Int32.Parse(this.ddlEstado.SelectedValue.ToString()),
                Convert.ToInt64(this.qrystr.idPersona),
                Convert.ToDateTime(this.txtFechaEstado.Text), this.qrystr.numeroSilpa,
                this.txtTextoCorreo.Text,
                this.fupAdjunto.FileName.ToString(),
                fupAdjunto.FileBytes,
                ViewState["accion"].ToString(), enviaCorreo, this.qrystr.Usuario,
                this.qrystr.expediente, this.txtNumeroActo.Text, idAA, true);

        // si no existe error se ejecuta avanzar tarea.
        if (result == String.Empty)
        {
            SILPA.Servicios.NotificacionFachada notFach = new SILPA.Servicios.NotificacionFachada();

            if (this.ddlEstado.SelectedItem.Text == "EJECUTORIADA")
            {
                notFach.ComponenteNotManual(this.ddlEstado.SelectedValue.ToString(), this.ddlEstado.SelectedItem.Text, this.qrystr.EsPDI, Convert.ToInt64(this.qrystr.idActoNot), "Ejecutoriar", Convert.ToInt32(this.qrystr.idPersona));
            }
            else
            {
                List<string> objProceso = notFach.ActualizarProcesosCorporaciones(int.Parse(this.ddlEstado.SelectedValue), this.ddlEstado.SelectedItem.Text, this.qrystr.EsPDI, Convert.ToInt64(this.qrystr.idActoNot), Convert.ToInt64(this.qrystr.idPersona));
                //string strRespuesta = notFach.ComponenteNotManual(this.ddlEstado.SelectedValue.ToString(), this.ddlEstado.SelectedItem.Text, this.data.DatoPDI, this.idActoNotificacion, "Consultar", this.idPersona);
                //SMLog.Escribir(Severidad.Informativo, "xmlRespuestaNotManual:" + strRespuesta);
            }
        }

        int accionAuditoria = 1;
        string strDetalle = string.Empty;
        //auditoria  -- acción realizada: (1)Almacenar, (2)Consultar, (3)Editar, (4)Eliminar.
        if (ViewState["accion"].ToString() == "Crear") { accionAuditoria = 1; strDetalle = "Se creó el estado " + this.ddlEstado.SelectedItem.Text.ToString(); }
        if (ViewState["accion"].ToString() == "Editar") { accionAuditoria = 3; strDetalle = "Se editó el estado de notificación: " + this.qrystr.idEstado + " el nuevo estado es:" + this.ddlEstado.SelectedValue.ToString(); }
        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("SILPA", accionAuditoria, strDetalle);

        return result;
    }


    /// <summary>
    /// Hava: 17-Nov-2010
    /// Obtiene el último estado creado para el acto administrativo
    /// </summary>
    /// <returns>string: Nombre del último estado de notificación creado</returns>
    public string CargarUltimoEstado(out DateTime? fechaEstado)
    {
        string result = String.Empty;
        Notificacion not = new Notificacion();
        int idEstado = -1;
        //string fechaEstado = String.Empty;
        result = not.ObtenerEstadoActual(Convert.ToInt64(this.qrystr.idPersona), Convert.ToInt64(this.qrystr.idActoNot), out idEstado, out fechaEstado);
        return result;
    }
    protected void btnAceptarAvance_Click(object sender, EventArgs e)
    {

        try
        {
            DateTime dtHoraNuevo = DateTime.Parse(this.txtFechaEstado.Text);
            DateTime dtHoraActual = DateTime.Parse(this.txtFechaEstadoActualAvan.Text);
            if (dtHoraActual > dtHoraNuevo)
            {
                Mensaje.MostrarMensaje(this.Page, "La fecha del estado no puede ser menor a la fecha del estado actual");
                return;
            }
        }
        catch
        {
            Mensaje.MostrarMensaje(this.Page, "El formato de fecha no es correcto");
            return;
        }

        string error = string.Empty;
        string msg = string.Empty;

        error = this.CrearEditarEstado(chkEnviarCorreo.Checked);
        if (String.IsNullOrEmpty(error))
        {
            Mensaje.MostrarMensaje(this, "Avance realizado con exito");
            ConsultarEstadosNotificacion();
        }
        else
        {
            Mensaje.MostrarMensaje(this, error);
            this.mpeAvanzarEstado.Show();
        }
        

    }
    public void CargarDatos(string accion)
    {
        //this.data = (DatoEstadoPersona)ViewState["data"];
        ViewState["accion"] = accion;
        this.txtNumeroVitalAvan.Text = this.qrystr.numeroSilpa; // this.data.NumeroSilpa;
        this.txtNumeroExpAvan.Text = this.qrystr.expediente; // this.data.Expediente;
        this.txtEstadoActualAvan.Text = this.qrystr.EstadoNotificado;// this.data.EstadoNotificado;
        this.txtUsuarioAvan.Text = this.qrystr.Usuario; // this.data.Usuario;
        this.txtIdentUsuarioAvan.Text = this.qrystr.Identificacion;// this.data.IdentificacionUsuario;
        this.txtDiasVenceAvan.Text = this.qrystr.DiasVence;// this.data.DiasVence;
        this.txtDatoPDIAvan.Text = this.qrystr.EsPDI;// this.data.DatoPDI; 
        this.txtIDProcesoNotAvan.Text = this.qrystr.IDProcesoNot; // this.data.IDProcesoNot;
        this.txtFechaActoAvan.Text = this.qrystr.FechaActo; // this.data.FechaActo;
        this.txtNumeroActoAdmiAvan.Text = this.qrystr.NumeroActo;// this.data.NumeroActo;

        DateTime? fechaEstado = null;
        this.txtEstadoActualAvan.Text = this.CargarUltimoEstado(out fechaEstado);
        this.txtFechaEstadoActualAvan.Text = fechaEstado.ToString();
        this.txtTipoActoAdministrativoAvan.Text = this.qrystr.TipoActo; // this.data.TipoActo;
        this.txtFechaEstado.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");

        this.txtAutoridadAmbientalAvan.Text = this.qrystr.Autoridad;// this.data.Autoridad;
        ConsultarEstadoSiguiente(Convert.ToInt32(this.qrystr.idEstado));
        this.ddlEstado.Enabled = true;
        if (accion == "Editar")
        {
            this.ddlEstado.Items.Clear();
            this.ddlEstado.Items.Insert(0, new ListItem(qrystr.EstadoNotificado, qrystr.idEstado));
            this.ddlEstado.SelectedValue = this.qrystr.idEstado;
            this.ddlEstado.Enabled = false;
            this.txtFechaEstado.Text = Convert.ToDateTime(this.qrystr.fechaEstadoNotificado).ToString("dd/MM/yyy HH:mm");
        }
    }
    protected void ConsultarEstadoSiguiente(int estadoID)
    {
        //TODO: Consulta y llena el combo de Estado Siguiente.
        EstadoNotificacionDalc estadoNotificacionDalc = new EstadoNotificacionDalc();
        NotificacionDalc dalc = new NotificacionDalc();
        Notificacion objNotificacion = new Notificacion();
        NotificacionEntity entity = dalc.ObtenerActo(new object[] { this.qrystr.idActoNot, null, null, null, null, null, null, null, null, null, null });
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
        List<EstadoFlujoEntity> listaEstadosFlujo = estadoNotificacionDalc.ListaSiguienteEstadoAdm(estadoID, true, false, entity.IdTipoActo.IdFlujoNotElec.Value);

        //Calcular los dias que lleva la solicitud en el estado actual
        int diasdiferencia = objNotificacion.ObtenerNumeroDiasNotificacion(this.qrystr.idPersona, this.qrystr.idActoNot);

        List<EstadoFlujoEntity> listaEstadosPosibles = new List<EstadoFlujoEntity>();
        foreach (EstadoFlujoEntity estadoflujo in listaEstadosFlujo)
        {
            if (estadoflujo.NroDiasTransicion >= diasdiferencia || estadoflujo.NroDiasTransicion <= 0)
                listaEstadosPosibles.Add(estadoflujo);
        }
        // ----------OJO: cambiar a la lista armada cuando se tenga terminado la notificacion electronica listaEstadosPosibles
        this.ddlEstado.DataSource = listaEstadosPosibles;
        this.ddlEstado.DataTextField = "NombreEstado";
        this.ddlEstado.DataValueField = "EstadoID";
        this.ddlEstado.DataBind();
        this.ddlEstado.Items.Insert(0, new ListItem("Selecione...", "-1"));
    }
    private void LimpiarAvance()
    {
        this.txtNumeroVitalAvan.Text = "";
        this.txtNumeroExpAvan.Text = "";
        this.txtFechaEstado.Text = "";
        this.txtEstadoActualAvan.Text = "";
        this.txtUsuarioAvan.Text = "";
        this.txtIdentUsuarioAvan.Text = "";
        this.txtDiasVenceAvan.Text = "";
        this.txtDatoPDIAvan.Text = "";
        this.txtIDProcesoNotAvan.Text = "";
        this.txtFechaActoAvan.Text = "";
        this.txtNumeroActoAdmiAvan.Text = "";
        this.txtFechaEstadoActualAvan.Text = "";
        this.txtTipoActoAdministrativoAvan.Text = "";
        this.txtAutoridadAmbientalAvan.Text = "";
        this.txtTextoCorreo.Text = "";
        this.chkEnviarCorreo.Checked = false;
        this.ddlEstado.Items.Clear();
    }
    protected void btnCancelarModificarFechaEstado_Click(object sender, EventArgs e)
    {

    }
    protected void btnModificarFechaEstado_Click(object sender, EventArgs e)
    {
        EstadoNotificacionDalc estadoNotificacionDalc = new EstadoNotificacionDalc();
        estadoNotificacionDalc.ModificarFechaEstadoPersonNotificacion(Convert.ToDecimal(this.qrystr.idPersona), Convert.ToDateTime(this.txtEditFechaEstado.Text), Convert.ToInt32(this.qrystr.idEstado));
        this.ConsultarEstadosNotificacion();
    }
}