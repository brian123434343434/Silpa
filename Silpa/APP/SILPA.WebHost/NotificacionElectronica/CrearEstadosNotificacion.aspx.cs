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
using SILPA.Servicios.Generico;
using SILPA.Servicios;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Notificacion;
using SILPA.Comun;
using System.IO;
using System.Collections.Generic;
using SoftManagement.Log;

public partial class NotificacionElectronica_CrearEstadosNotificacion : System.Web.UI.Page
{
    public event System.EventHandler SeleccionesBound;
    public int _idAA = -1;
    private long idPersona;
    private int idEstado;
    private long idActoNotificacion;
    private DateTime fechaEstado;
    private string numeroSilpa;
    private string accion;
    private DatoEstadoPersona data;
    public qrystring qrystr;
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
                this.idActoNotificacion,this.idEstado,
                Int32.Parse(this.ddlEstado.SelectedValue.ToString()), 
                this.idPersona, 
                this.fechaEstado, this.numeroSilpa,
                this.txtTextoCorreo.Text,
                this.fupAdjunto.FileName.ToString(),
                fupAdjunto.FileBytes,
                this.accion, enviaCorreo,this.qrystr.Usuario,
                this.qrystr.expediente, this.txtNumeroActo.Text, idAA, true);

        // si no existe error se ejecuta avanzar tarea.
        if(result==String.Empty)
        {
            SILPA.Servicios.NotificacionFachada notFach = new SILPA.Servicios.NotificacionFachada();

            if (this.ddlEstado.SelectedItem.Text == "EJECUTORIADA")
            {
                notFach.ComponenteNotManual(this.ddlEstado.SelectedValue.ToString(), this.ddlEstado.SelectedItem.Text, this.qrystr.EsPDI, this.idActoNotificacion, "Ejecutoriar", this.idPersona);
            }
            else
            {
                List<string> objProceso = notFach.ActualizarProcesos(int.Parse(this.ddlEstado.SelectedValue), this.ddlEstado.SelectedItem.Text, this.qrystr.EsPDI, this.idActoNotificacion, this.idPersona);
                //string strRespuesta = notFach.ComponenteNotManual(this.ddlEstado.SelectedValue.ToString(), this.ddlEstado.SelectedItem.Text, this.data.DatoPDI, this.idActoNotificacion, "Consultar", this.idPersona);
                //SMLog.Escribir(Severidad.Informativo, "xmlRespuestaNotManual:" + strRespuesta);
            }
        }

        int accionAuditoria = 1;
        string strDetalle = string.Empty;
        //auditoria  -- acción realizada: (1)Almacenar, (2)Consultar, (3)Editar, (4)Eliminar.
        if (this.accion == "Crear") { accionAuditoria = 1; strDetalle = "Se creó el estado " + this.ddlEstado.SelectedItem.Text.ToString(); }
        if (this.accion == "Editar") { accionAuditoria = 3; strDetalle = "Se editó el estado de notificación: " + this.idEstado.ToString() + " el nuevo estado es:" + this.ddlEstado.SelectedValue.ToString(); }
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
        int idEstado=-1;
        //string fechaEstado = String.Empty;
        result = not.ObtenerEstadoActual(Convert.ToInt64(this.qrystr.idPersona), Convert.ToInt64(this.qrystr.idActoNot), out idEstado, out fechaEstado);
        return result;
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            Mensaje.LimpiarMensaje(this);
            if (!IsPostBack)
            {

                if (Session["ClNotificacion"] != null)
                {
                    qrystr = (qrystring)Session["ClNotificacion"];
                }
                this.ObtenerQueryString();
                this.CargarDatos();

                EstadoNotificacionDalc dalc = new EstadoNotificacionDalc();

                ConsultarEstadoSiguiente(Convert.ToInt32(this.qrystr.idEstado));
                /*List<EstadoNotificacionEntity> dsList = dalc.ListarEstadosNotificacion();

                EstadoNotificacionEntity EstEjecutoria =
                dsList.Find(delegate(EstadoNotificacionEntity est)
                { return est.ID == (int)SILPA.Comun.EstadoNotificacion.EJECUTORIADA; });

                int valorprueba = (int)SILPA.Comun.EstadoNotificacion.EJECUTORIADA;

                int index = dsList.FindIndex(delegate(EstadoNotificacionEntity est)
                { return est.ID == (int)SILPA.Comun.EstadoNotificacion.EJECUTORIADA; });

                dsList.RemoveAt(index);

                //this.ddlEstado.DataSource = dalc.ListarEstadosNotificacion();
                this.ddlEstado.DataSource = dsList;
                this.ddlEstado.DataTextField = "Estado";
                this.ddlEstado.DataValueField = "ID";
                this.ddlEstado.DataBind();
                this.ddlEstado.Items.Insert(0, new ListItem("Seleccione...", "-1"));*/


                this.txtFechaEstado.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                //this.ConsultarDatos(Convert.ToInt32(this.lblId.Text));
            }
            //this.btnAceptar.Attributes.Add("onClick", "return confirm('Despues de guardar no podrá realizar ningún cambio. Está seguro que desea guardar el registro?')");
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }
  


    private void SetFocusControl(string ControlName)
    {
        string script = "<script language=\"javascript\">var control = document.getElementById(\"" + ControlName + "\"); if( control != null ){control.focus();}</script>";
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Focus", script);
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {

        try
        {
            DateTime dtHoraNuevo= DateTime.Parse(this.txtFechaEstado.Text);
            DateTime dtHoraActual = DateTime.Parse(this.txtFechaEstadoActual.Text);
            if (dtHoraActual > dtHoraNuevo)
            {
                Mensaje.MostrarMensaje(this.Page, "La fecha del estado no puede ser menor a la fecha del estado actual");
                return;
            }
        }
        catch
        { 
            Mensaje.MostrarMensaje(this.Page,"El formato de fecha no es correcto");
            return;
        }

        //this.data   = (DatoEstadoPersona)ViewState["data"];
        //this.accion = this.data.Accion;
        
        this.idPersona = long.Parse(this.qrystr.idPersona);

        this.idEstado = int.Parse(this.qrystr.idEstado);
        
        this.idActoNotificacion = long.Parse(this.qrystr.idActoNot);

        
        this.numeroSilpa = this.data.NumeroSilpa.ToString();
        this.fechaEstado = Convert.ToDateTime(this.txtFechaEstado.Text);

        string error = string.Empty;
        string msg = string.Empty;

        error = this.CrearEditarEstado(chkEnviarCorreo.Checked);

        string crearEditar= " Creación de estado ";
        
        
        if (accion == "Editar") 
        { 
            crearEditar = " Edición de estado ";
        }

        /// No ocurrió error!!
        if (String.IsNullOrEmpty(error))
        {
            msg = "Proceso de " + crearEditar + " realizado correctamente.";

            Mensaje.MostrarMensaje(this, msg);

            string strScript = "<script language='JavaScript'>" +
                               "location.href = '" + "../NotificacionElectronica/ConsultarEstadosNotificacion.aspx" + "'" +
                               "</script>";
            Page.RegisterStartupScript("PopupScript", strScript);

            //string popupScript = "<script>self.close();</script>";
            //if (!this.Page.ClientScript.IsStartupScriptRegistered("Cerrar"))
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Cerrar", popupScript);

        }
        else 
        {
            msg = error;
            this.Limpiar();
            Mensaje.MostrarMensaje(this, msg);
        }
    }

    /// <summary>
    /// Limpia los controles
    /// </summary>
    public void Limpiar() 
    {
        this.txtFechaEstado.Text = string.Empty;
        this.ddlEstado.SelectedIndex = 0;
        this.txtTextoCorreo.Text = string.Empty;
    }

    public void ObtenerQueryString()
    {
        DatoEstadoPersona data = new DatoEstadoPersona();
        
        /// dato que determina si se esta editando o creando el registro
        if (Request.QueryString["cmd"] != null) 
        {
            data.Accion = Request.QueryString["cmd"].ToString();
        }

        if (Request.QueryString["numeroSilpa"]!=null)
        {
            //this.numeroSilpa= Request.QueryString["numeroSilpa"].ToString();
            //ViewState["numeroSilpa"] = Request.QueryString["numeroSilpa"].ToString();
            data.NumeroSilpa = Request.QueryString["numeroSilpa"].ToString();
        }

        if (Request.QueryString["TipoActo"] != null)
        {
            data.TipoActo = Request.QueryString["TipoActo"].ToString();
        }

        if (Request.QueryString["idEstado"]!=null)
        {
            //this.idEstado = int.Parse(Request.QueryString["idEstado"].ToString());
            //ViewState["idEstado"] = int.Parse(Request.QueryString["idEstado"].ToString());
            data.idEstado = int.Parse(Request.QueryString["idEstado"].ToString());

        }
        if (Request.QueryString["idPersona"] != null) 
        {
            //this.idPersona = long.Parse(Request.QueryString["idPersona"].ToString());
            //ViewState["idPersona"] = long.Parse(Request.QueryString["idPersona"].ToString());
            data.idPersona = long.Parse(Request.QueryString["idPersona"].ToString());

        }
        if (Request.QueryString["idActoNot"] != null)
        {
            //this.idActoNotificacion = long.Parse(Request.QueryString["idActoNot"].ToString());
            //ViewState["idActoNot"] = long.Parse(Request.QueryString["idActoNot"].ToString());
            data.idActoNot = long.Parse(Request.QueryString["idActoNot"].ToString());
        }

        if (Request.QueryString["FechaEstadoNotificado"] != null)
        {
            //this.idActoNotificacion = long.Parse(Request.QueryString["idActoNot"].ToString());
            //ViewState["FechaEstadoNotificado"] = Request.QueryString["FechaEstadoNotificado"].ToString();
            data.FechaEstadoNotificado = Request.QueryString["FechaEstadoNotificado"].ToString();
        }

        if (Request.QueryString["NombreFuncionario"] != null)
        {
            //ViewState["NombreFuncionario"] = Request.QueryString["NombreFuncionario"].ToString();
            data.NombreFuncionario = Request.QueryString["NombreFuncionario"].ToString();
        }

        if (Request.QueryString["Expediente"] != null)
        {
            //ViewState["Expediente"] = Request.QueryString["Expediente"].ToString();
            data.Expediente = Request.QueryString["Expediente"].ToString();
        }

        if (Request.QueryString["EstadoNotificado"] != null)
        {
            // ViewState["EstadoNotificado"] = Request.QueryString["EstadoNotificado"].ToString();
            data.EstadoNotificado = Request.QueryString["EstadoNotificado"].ToString();
        }

        if (Request.QueryString["Usuario"] != null)
        {
            //ViewState["Usuario"] = Request.QueryString["Usuario"].ToString();
            data.Usuario = Request.QueryString["Usuario"].ToString();
        }

        if (Request.QueryString["Identificacion"] != null)
        {
            //ViewState["Identificacion"] = Request.QueryString["Identificacion"].ToString();
            data.IdentificacionUsuario = Request.QueryString["Identificacion"].ToString();
        }

        if (Request.QueryString["EstadoActual"] != null)
        {
            //ViewState["EstadoActual"] = Request.QueryString["EstadoActual"].ToString();
            data.EstadoActual = Request.QueryString["EstadoActual"].ToString();
        }

        if (Request.QueryString["DiasVence"] != null)
        {
            //ViewState["DiasVence"] = Request.QueryString["DiasVence"].ToString();
            data.DiasVence = Request.QueryString["DiasVence"].ToString();
        }

        if (Request.QueryString["EsPDI"] != null)
        {
            //ViewState["EsPDI"] = Request.QueryString["EsPDI"].ToString();
            data.DatoPDI = Request.QueryString["EsPDI"].ToString();
        }
        if (Request.QueryString["IDProcesoNot"] != null)
        {
            //ViewState["IDProcesoNot"] = Request.QueryString["IDProcesoNot"].ToString();
            data.IDProcesoNot = Request.QueryString["IDProcesoNot"].ToString();
        }

        if (Request.QueryString["FechaActo"] != null)
        {
            data.FechaActo = Request.QueryString["FechaActo"].ToString();
        }
        

        if (Request.QueryString["NumeroActo"] != null)
        {
            data.NumeroActo = Request.QueryString["NumeroActo"].ToString();
        }

        if (Request.QueryString["Autoridad"] != null)
        {
            data.Autoridad = Request.QueryString["Autoridad"].ToString();
        }

        ViewState["data"] = data;
    }

    public void CargarDatos()
    {
        //this.data = (DatoEstadoPersona)ViewState["data"];
        
        if (accion == "Editar")
        {
            this.lbl_titulo_principal.Text = accion;
            if (int.Parse(this.qrystr.idEstado) != (int)(int)SILPA.Comun.EstadoNotificacion.EJECUTORIADA)              
                this.ddlEstado.SelectedValue = this.qrystr.idEstado; 
            this.txtFechaEstado.Text = this.qrystr.fechaEstadoNotificado;
        }

        this.txtNumeroVital.Text = this.qrystr.numeroSilpa; // this.data.NumeroSilpa;
        this.txtExpediente.Text = this.qrystr.expediente; // this.data.Expediente;
        this.txtFechaEstado.Text = this.qrystr.fechaEstadoNotificado; // this.data.FechaEstadoNotificado;
        this.txtEstadoActual.Text = this.qrystr.EstadoNotificado;// this.data.EstadoNotificado;
        this.txtUsuario.Text = this.qrystr.Usuario; // this.data.Usuario;
        this.txtIdentUsuario.Text = this.qrystr.Identificacion;// this.data.IdentificacionUsuario;
        this.txtDiasVence.Text = this.qrystr.DiasVence;// this.data.DiasVence;
        this.txtDatoPDI.Text = this.qrystr.EsPDI;// this.data.DatoPDI; 
        this.txtIDProcesoNot.Text = this.qrystr.IDProcesoNot; // this.data.IDProcesoNot;
        this.txtFechaActo.Text = this.qrystr.FechaActo; // this.data.FechaActo;
        this.txtNumeroActo.Text = this.qrystr.NumeroActo;// this.data.NumeroActo;

        DateTime? fechaEstado = null;
        this.txtEstadoActual.Text = this.CargarUltimoEstado(out fechaEstado);
        this.txtFechaEstadoActual.Text = fechaEstado.ToString();
        this.txtTipoActoAdministrativo.Text = this.qrystr.TipoActo; // this.data.TipoActo;

        this.txtAutoridadAmbiental.Text = this.qrystr.Autoridad;// this.data.Autoridad;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
  
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
     
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.Limpiar();
        this.Response.Redirect("ConsultarEstadosNotificacion.aspx");
    }
    protected void ConsultarEstadoSiguiente(int estadoID)
    {
        //TODO: Consulta y llena el combo de Estado Siguiente.
        EstadoNotificacionDalc estadoNotificacionDalc = new EstadoNotificacionDalc();
        List<EstadoFlujoEntity> listaEstadosFlujo = new List<EstadoFlujoEntity>();// TODO: JACOSTA estadoNotificacionDalc.ListaSiguienteEstado(estadoID, true);

        DateTime dtFechaEstado = Convert.ToDateTime(this.qrystr.fechaEstadoNotificado);
        TimeSpan diferencia = DateTime.Today - dtFechaEstado;
        int diasdiferencia = diferencia.Days;

        List<EstadoFlujoEntity> listaEstadosPosibles = new List<EstadoFlujoEntity>();
        foreach (EstadoFlujoEntity estadoflujo in listaEstadosFlujo)
        {
            if (estadoflujo.NroDiasTransicion == 0)
                listaEstadosPosibles.Add(estadoflujo);
            else if (estadoflujo.NroDiasTransicion >= diasdiferencia)
                listaEstadosPosibles.Add(estadoflujo);
        }
        // ----------OJO: cambiar a la lista armada cuando se tenga terminado la notificacion electronica listaEstadosPosibles
        this.ddlEstado.DataSource = listaEstadosPosibles;
        this.ddlEstado.DataTextField = "NombreEstado";
        this.ddlEstado.DataValueField = "EstadoID";
        this.ddlEstado.DataBind();
    }
}

[Serializable()]
public class DatoEstadoPersona
{
    public string Accion;
    public string NumeroSilpa;
    public string Expediente;
    public string FechaActo;
    public string TipoActo;
    public string NumeroActo;
    public string Usuario;
    public string IdentificacionUsuario;
    public string EstadoActual;
    public string FechaEstadoActual;
    public string DiasVence;
    public string DatoPDI;
    public string IDProcesoNot;

    public int idEstado;
    public long idPersona;
    public long idActoNot;
    public string FechaEstadoNotificado;
    public string NombreFuncionario;
    public string EstadoNotificado;
    public string Autoridad;
}
