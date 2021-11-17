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
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System.IO;

public partial class NotificacionElectronica_EmitirDocumento : System.Web.UI.Page
{
    public event System.EventHandler SeleccionesBound;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //this.lblFormulario.Text = Path.GetFileName(Request.Url.AbsolutePath);
            //this.llenarControles();
            Mensaje.LimpiarMensaje(this);
            if (!IsPostBack)
            {
                //Si se va a consultar la informacion
                if (this.Request.QueryString["id"] != null)              
                    this.lblId.Text = this.Request.QueryString["id"].ToString();

                //this.lblId.Text = "3";

                TipoDocumentoDalc _tipoDocumentoDalc = new TipoDocumentoDalc();
                this.cboTipoActo.DataSource = _tipoDocumentoDalc.ListarTiposDeDocumento(null, null);
                this.cboTipoActo.DataTextField = "NOMBRE_DOCUMENTO";
                this.cboTipoActo.DataValueField = "ID";
                this.cboTipoActo.DataBind();
                this.cboTipoActo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
             
                this.ConsultarDatos(Convert.ToInt32(this.lblId.Text));
            }
            this.btnAceptar.Attributes.Add("onClick", "return confirm('Despues de guardar no podrá realizar ningún cambio. Está seguro que desea guardar el registro?')");
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (this.lstDocumento.Items.Count <= 0)
                {
                    Mensaje.MostrarMensaje(this, "Debe agregar al menos un(1) Acto Administrativo");
                    return;
                }
                if (this.lstPersonas.Items.Count <= 0)
                {
                    Mensaje.MostrarMensaje(this, "Debe agregar al menos una(1) Persona a notificar");
                    return;
                }

                if (DatosSesion.Usuario != string.Empty)
                {
                    Mensaje.MostrarMensaje(this, "El usuario no existe en el sistema");
                    return;
                }
                PersonaDalc _persona = new PersonaDalc();
                PersonaIdentity _objPersona = new PersonaIdentity();
                NotificacionType _objNotificacion = new NotificacionType();

                //1230262
                _objPersona = _persona.ConsultarPersona(DatosSesion.Usuario);
                if (_objPersona.PersonaId <= 0)
                {
                    Mensaje.MostrarMensaje(this, "El usuario no existe en el sistema");
                    return;
                }

                //documento
                EmitirDocumento _documento = new EmitirDocumento();
                EmitirDocumentoIdentity _objIdentity = new EmitirDocumentoIdentity();
                _objIdentity.AutoridadId = _objPersona.IdAutoridadAmbiental;
                _objIdentity.DescripcionActoAdministrativo = this.txtDescripcionActo.Text.Trim();
                _objIdentity.FechaActoAdministrativo = Convert.ToDateTime(this.txtFechaActo.Text.Trim());
                _objIdentity.ActoAdministrativo = this.txtNumeroActo.Text.Trim();
                _objIdentity.NumeroExpediente = this.txtNumeroExpediente.Text.Trim();
                _objIdentity.NumeroSilpa = this.txtNumeroVital.Text.Trim();
                _objIdentity.TipoDocumentoId = Convert.ToInt32(this.cboTipoActo.SelectedValue);
                _objIdentity.PersonaId = _objPersona.PersonaId;
                this.lblId.Text = _documento.InsertarDocumento(_objIdentity).ToString();

                //Documento personas
                foreach (ListItem li in this.lstPersonas.Items)
                {
                    _documento.InsertarDocumentoPersona(long.Parse(this.lblId.Text), long.Parse(li.Value));
                }

                //Documento detalle            
                _documento.InsertarDocumentoDetalle(long.Parse(this.lblId.Text), this.lstDocumento.Items[0].Text);

                //Se invoca el servicio
                byte[] bArchivo;
                AppSettingsReader reader = new AppSettingsReader();
                string ruta = Server.MapPath("../" + reader.GetValue("ARCHIVOS", typeof(String)).ToString() + this.lstDocumento.Items[0].Text);
                bArchivo = System.IO.File.ReadAllBytes(ruta);
                _objNotificacion.datosArchivo = bArchivo;
                _objNotificacion.nombreArchivo = this.lstDocumento.Items[0].Text;
                _objNotificacion.numProcesoAdministracion = _objIdentity.NumeroExpediente;

                _objNotificacion.numActoAdministrativo = _objIdentity.ActoAdministrativo;
                _objNotificacion.numSILPA = _objIdentity.NumeroSilpa;
                _objNotificacion.parteResolutiva = _objIdentity.DescripcionActoAdministrativo;
                _objNotificacion.fechaActoAdministrativo = _objIdentity.FechaActoAdministrativo;

                _objNotificacion.tipoIdentificacionFuncionario = (enumTipoIdentificacion)Enum.Parse(typeof(enumTipoIdentificacion), _objPersona.TipoDocumentoIdentificacion.Id.ToString(), true); ;
                _objNotificacion.numeroIdentificacionFuncionario = _objPersona.NumeroIdentificacion;
                _objNotificacion.tipoActoAdministrativo = _objIdentity.TipoDocumentoId.ToString();
                _objNotificacion.requiereNotificacion = true;
                _objNotificacion.esEjecutoria = false;
                _objNotificacion.IdPlantillaNotificacion = "-1";

                SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaAutoridades.ListarAutoridades(_objPersona.IdAutoridadAmbiental);
                if (_temp.Tables[0].Rows.Count > 0)
                {
                    _objNotificacion.EntidadPublicaNot = _temp.Tables[0].Rows[0]["AAE_ENT_PUBLICA_NOT"].ToString();
                    _objNotificacion.SistemaEntidadPublicaNot = _temp.Tables[0].Rows[0]["AAE_SIS_ENT_PUBLICA_NOT"].ToString();
                    _objNotificacion.IdDependenciaEntidad = _temp.Tables[0].Rows[0]["AAE_ID_DEPENDENCIA_ENT"].ToString();
                }
                else
                {
                    _objNotificacion.EntidadPublicaNot = string.Empty;
                    _objNotificacion.SistemaEntidadPublicaNot = string.Empty;
                    _objNotificacion.IdDependenciaEntidad = string.Empty;
                }
                _objNotificacion.numActoAdministrativoAsociado = string.Empty;

                DataTable _personas = _documento.ConsultarDocumentoPersonas(long.Parse(this.lblId.Text));
                _objNotificacion.listaPersonas = new PersonaType[_personas.Rows.Count];
                int cont = 0;
                foreach (DataRow rows in _personas.Rows)
                {
                    PersonaType perType = new PersonaType();
                    perType.numeroIdentificacion = rows["PER_NUMERO_IDENTIFICACION"].ToString();
                    perType.primerNombre = rows["PER_PRIMER_NOMBRE"].ToString();
                    perType.primerApellido = rows["PER_PRIMER_APELLIDO"].ToString();
                    perType.segundoNombre = rows["PER_SEGUNDO_NOMBRE"].ToString();
                    perType.segundoApellido = rows["PER_SEGUNDO_APELLIDO"].ToString();
                    perType.razonSocial = rows["PER_RAZON_SOCIAL"].ToString();
                    perType.ipoIdentificacion = (enumTipoIdentificacion)Enum.Parse(typeof(enumTipoIdentificacion), rows["TID_ID"].ToString(), true);

                    _objNotificacion.listaPersonas.SetValue(perType, cont);
                    cont++;
                }

                this.btnAceptar.Enabled = false;
                this.btnAdjuntar.Enabled = false;
                this.btnAdicionar.Enabled = false;
                this.btnQuitar.Enabled = false;
                this.btnCancelar.Enabled = false;
                this.txtNumeroVital.Enabled = false;

                XmlSerializador ser = new XmlSerializador();
                /// Obtiene el objeto Serializado.
                string xmlObject = ser.serializar(_objNotificacion).ToString();
                
                WSPQ03.WSPQ03 wspq03 = new WSPQ03.WSPQ03();
                wspq03.Timeout = 900000;
                wspq03.RecibirDocumento(xmlObject);

                Mensaje.MostrarMensaje(this, "El Acto Administrativo fue enviado a notificación con exito");
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }

    }
    protected void txtNumeroVital_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //1100000123026210901
            if (this.txtNumeroVital.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El Número Vital es requerido");
                return;
            }

            //this.grdDocumentos.DataSource = null;
            //this.grdDocumentos.DataBind();
            this.lstPersonas.Items.Clear();
            this.cboPersonas.Items.Clear();
            this.cboPersonas.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            RadicacionDocumento proceso = new RadicacionDocumento();
            if (proceso.ObtenerProcessInstance(this.txtNumeroVital.Text.Trim()) == string.Empty)
            {
                //this.txtNumeroVital.Text = string.Empty;
                Mensaje.MostrarMensaje(this, "El Número Vital no existe en el sistema");
                return;
            }

            Persona personas = new Persona();
            this.cboPersonas.DataSource = personas.ConsultarPersonasNumeroSilpa(this.txtNumeroVital.Text.Trim());
            this.cboPersonas.DataTextField = "NOMBRE_COMPLETO";
            this.cboPersonas.DataValueField = "PER_ID";
            this.cboPersonas.DataBind();
            this.cboPersonas.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }

    }      

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        //Recorre el listbox buscando el item seleccionado para adicionarlo
        if (this.cboPersonas.SelectedValue != "-1")           
                //Adiciona el item seleccionado
                this.lstPersonas.Items.Add(this.cboPersonas.SelectedItem);  
        
        // Focus
        this.SetFocusControl(this.lstPersonas.ClientID);
        this.OnBound(sender);
    }
    protected void btnQuitar_Click(object sender, EventArgs e)
    {
        //Recorre el listbox buscando el item seleccionado para quitarlo
        for (int cont = this.lstPersonas.Items.Count - 1; cont >= 0; cont--)
        {
            if (this.lstPersonas.Items[cont].Selected)
            {              
                //Remueve el item seleccionado de la lista inicial
                this.lstPersonas.Items.RemoveAt(cont);
            }
        }
        if (this.lstPersonas.Items.Count < 1)
            this.lstPersonas.Text = "";
        // Focus
        this.SetFocusControl(this.lstPersonas.ClientID);
        this.OnBound(sender);
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
    /// Se crea la tabla de session del documento
    /// </summary>
    private void ConsultarDatos(int id)
    {      
        if (id > 0)
        {
            //Consulta la informacion de documentos
            EmitirDocumento _documento = new EmitirDocumento();
            EmitirDocumentoIdentity objIdentity = new EmitirDocumentoIdentity();
            objIdentity.DocumentoId = id;
            _documento.ConsultarDocumento(ref objIdentity);

            this.txtDescripcionActo.Text = objIdentity.DescripcionActoAdministrativo;
            this.txtFechaActo.Text = objIdentity.FechaActoAdministrativo.ToShortDateString();
            this.txtNumeroActo.Text = objIdentity.ActoAdministrativo;
            this.txtNumeroExpediente.Text = objIdentity.NumeroExpediente;
            this.txtNumeroVital.Text = objIdentity.NumeroSilpa;
            this.cboTipoActo.SelectedValue = objIdentity.TipoDocumentoId.ToString();
        
            this.lstPersonas.DataSource = _documento.ConsultarDocumentoPersonas(objIdentity.DocumentoId);
            this.lstPersonas.DataValueField = "PER_ID";
            this.lstPersonas.DataTextField = "NOMBRE_COMPLETO";
            this.lstPersonas.DataBind();

            this.lstDocumento.DataSource = _documento.ConsultarDocumentoDetalle(objIdentity.DocumentoId);
            this.lstDocumento.DataValueField = "EDD_NOMBRE";
            this.lstDocumento.DataTextField = "EDD_NOMBRE";
            this.lstDocumento.DataBind(); 
           
            this.btnAceptar.Enabled = false;
            this.btnAdjuntar.Enabled = false;
            this.btnAdicionar.Enabled = false;
            this.btnQuitar.Enabled = false;
            this.btnCancelar.Enabled = false;
            this.txtNumeroVital.Enabled = false;           
       }       
    }
         

    protected void AdicionarArchivo()
    {
        // Preguntamos si existe algo en el selector de archivos
        if (this.uplAdjuntar.FileName.Length > 0)
        {
            string archivo = this.uplAdjuntar.FileName;
            if (Path.GetExtension(archivo).ToString().ToLower() == ".p7z")
            {
                Random aleatorio = new Random();
                archivo = archivo.Substring(archivo.LastIndexOf("\\") + 1);
                archivo =  aleatorio.Next(1000) + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + archivo;
            
                AppSettingsReader reader = new AppSettingsReader();
                this.uplAdjuntar.PostedFile.SaveAs(Server.MapPath("../" + reader.GetValue("ARCHIVOS", typeof(String)).ToString() + archivo));
                this.lstDocumento.Items.Add(new ListItem(archivo, archivo));
            }
            else
                Mensaje.MostrarMensaje(this,"La extensión del archivo no es válida debe ser p7z");
        }
        else 
            Mensaje.MostrarMensaje(this,"Debe seleccionar un archivo");

    }
    protected void btnAdjuntar_Click(object sender, EventArgs e)
    {
        if (this.lstDocumento.Items.Count > 0)
        {
            Mensaje.MostrarMensaje(this, "No es posible adjuntar mas de un(1) documento");
            return;
        }

        this.AdicionarArchivo();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        //Recorre el listbox buscando el item seleccionado para quitarlo
        for (int cont = this.lstDocumento.Items.Count - 1; cont >= 0; cont--)
        {
            if (this.lstDocumento.Items[cont].Selected)
            {
                //Remueve el item seleccionado de la lista inicial
                this.lstDocumento.Items.RemoveAt(cont);
            }
        }
        if (this.lstDocumento.Items.Count < 1)
            this.lstDocumento.Text = "";
        // Focus
        this.SetFocusControl(this.lstDocumento.ClientID);
        this.OnBound(sender);
    }
}
