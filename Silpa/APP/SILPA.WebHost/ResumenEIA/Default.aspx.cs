using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using SoftManagement.Log;
using System.Data;
using SILPA.LogicaNegocio.ResumenEIA;
using SoftManagement.Persistencia;
using System.Data.SqlClient;


public partial class EntradaEIA_Default : System.Web.UI.Page
{
    public int IDProyecto
    {
        get
        {
            if (ViewState["IdProyecto"] != null)
                return (int)ViewState["IdProyecto"];
            return -1;
        }

        set { ViewState["IdProyecto"] = value; }

    }

    public string _usuarioRegistrado
    {
        get
        {
            if (ViewState["Usuario"] != null)
                return (string)ViewState["Usuario"];
            return null;
        }

        set { ViewState["Usuario"] = value; }

    }    
    private bool ModoCarga;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            _usuarioRegistrado=null;
            if (Request.QueryString["IDProyecto"] != null && Request.QueryString["Ubic"] == null)
            {                
                if (ValidacionToken())
                {
                    this._usuarioRegistrado = (string)Session["Usuario"];
                }
                if (_usuarioRegistrado != null)
                {
                    IDProyecto = int.Parse((string)Request.QueryString["IDProyecto"]);
                    if (IDProyecto == 0)
                    {
                        crearProyecto();
                    }
                }
                else
                {
                    IDProyecto = int.Parse((string)Request.QueryString["IDProyecto"]);
                }
            }
            
            ctrFicha1.IDProyecto = IDProyecto;
            ctrFicha2.IDProyecto = IDProyecto;
            ctrFicha3.IDProyecto = IDProyecto; 
            ctrFicha4.IDProyecto = IDProyecto;
            ctrFicha5.IDProyecto = IDProyecto;
            ctrFicha6.IDProyecto = IDProyecto;
            ctrFicha7.IDProyecto = IDProyecto;
            ctrFicha8.IDProyecto = IDProyecto;
            ctrFicha9.IDProyecto = IDProyecto;
            ctrFicha10.IDProyecto = IDProyecto;
            ctrFicha11.IDProyecto = IDProyecto;


            lblNombreProyecto.Text = "Número de proyecto: " + Convert.ToString(IDProyecto);

            this.ctrFicha1.CargarTodaInfo();

            DataSet dsTabla = new DataSet();
            dsTabla=CargarProyecto();
            if (dsTabla.Tables[0].Rows.Count > 0)
            {               
                if (dsTabla.Tables[0].Rows[0]["EIP_NUMERO_VITAL"].ToString() == "")
                {
                    lblNumeroVital.Text = "Número Vital: NO ASIGNADO";
                    this.btnGuardar.Visible = true;
                }
                else
                {
                    lblNumeroVital.Text = "Número Vital: " + dsTabla.Tables[0].Rows[0]["EIP_NUMERO_VITAL"].ToString();
                    this.btnFinalizar.Visible = true;
                }
                if (dsTabla.Tables[0].Rows[0]["EIP_ESTADO_PROYECTO"].ToString() == "1")
                    ModoCarga = true;
                else if (dsTabla.Tables[0].Rows[0]["EIP_ESTADO_PROYECTO"].ToString() == "2")
                {
                    this.btnGuardar.Visible = false;
                    this.btnFinalizar.Visible = false;
                }

            }
            else
                lblNumeroVital.Text = "Número Vital: NO ASIGNADO";
            
            tbFormularioEIA.Enabled = ModoCarga;            
        }

    }

    protected void crearProyecto()
    {        
        IDProyecto = int.Parse(GeneralResumenEIA.GenerarProyectoEIA());
    }

    protected void btnSiguiente_Click(object sender, EventArgs e)
    {
        this.btnAnterior.Visible = true;
        this.tbFormularioEIA.ActiveTabIndex = this.tbFormularioEIA.ActiveTabIndex+1;
        if (this.tbFormularioEIA.ActiveTabIndex > 10)
            this.btnSiguiente.Visible = false;
    }
    protected void btnAnterior_Click(object sender, EventArgs e)
    {
        this.btnSiguiente.Visible = true;
        this.tbFormularioEIA.ActiveTabIndex = this.tbFormularioEIA.ActiveTabIndex - 1;
        if (this.tbFormularioEIA.ActiveTabIndex == 0)
            this.btnAnterior.Visible = false;
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (_usuarioRegistrado != null)
        {
            string idTramite = ObtenerParametroTramiteResumenEIA();

            DataSet dsTabla = CargarProyecto();
            Contexto.cargarTabla(dsTabla, "EIH_INFORMACION_PROYECTO");
          
            if (dsTabla.Tables[0].Rows.Count > 0)
            {
                if (dsTabla.Tables[0].Rows[0]["EIP_NUMERO_VITAL"].ToString() == "")
                {
                    string NumeroVital = GeneralResumenEIA.GenerarNumeroVitalProyectoEIA(int.Parse(_usuarioRegistrado), int.Parse(idTramite));
                    dsTabla.Tables[0].Rows[0]["EIP_NUMERO_VITAL"] = NumeroVital;
                    dsTabla.Tables[0].Rows[0]["EIP_USER_ID"] = _usuarioRegistrado;
                    Contexto.guardarTabla(dsTabla, "EIH_INFORMACION_PROYECTO");
                    lblNumeroVital.Text = "Número Vital: " + dsTabla.Tables[0].Rows[0]["EIP_NUMERO_VITAL"].ToString();
                    Mensaje.MostrarMensaje(this.Page, "Numero Vital Asignado: " + dsTabla.Tables[0].Rows[0]["EIP_NUMERO_VITAL"].ToString());
                    this.btnFinalizar.Visible = true;
                    this.btnGuardar.Visible = false;
                }   
            }
            else
                Mensaje.MostrarMensaje(this.Page, "No se puede asignar Numero Vital a este idProyecto");
        }
    }

   
    private string ObtenerParametroTramiteResumenEIA()
    {
        DataSet dsTabla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@NOMBRE_PARAMETRO", SqlDbType.VarChar, 20, "NOMBRE_PARAMETRO");
        ParametroCarga.Value = "Resumen_EIA";
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsTabla, "GEN_PARAMETRO");
        return dsTabla.Tables[0].Rows[0]["PARAMETRO"].ToString();
    }

    private DataSet CargarProyecto()
    {
        DataSet dsTabla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = IDProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta,dsTabla, "EIH_INFORMACION_PROYECTO");
        return dsTabla;
    }

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

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        DataSet dsTabla = CargarProyecto();
        Contexto.cargarTabla(dsTabla, "EIH_INFORMACION_PROYECTO");

        if (dsTabla.Tables[0].Rows.Count > 0)        
        {                         
            dsTabla.Tables[0].Rows[0]["EIP_ESTADO_PROYECTO"] = 2;
            Contexto.guardarTabla(dsTabla, "EIH_INFORMACION_PROYECTO");                
            Mensaje.MostrarMensaje(this.Page, "Proyecto Finalizado Con Exito");
            this.btnFinalizar.Visible = false;
            this.btnGuardar.Visible = false;
            this.tbFormularioEIA.Enabled = false;

        }
        else
            Mensaje.MostrarMensaje(this.Page, "No se puede Finalizar el Proyecto");
    }

    protected void ActiveTabChanged(object sender, EventArgs e)
    {
        string preba = "";
        switch (tbFormularioEIA.ActiveTabIndex)
        {
            case 0:
                this.ctrFicha1.CargarTodaInfo();
                break;
            case 1:
                this.ctrFicha2.CargarTodaInfo();
                break;
            case 2:
                this.ctrFicha3.CargarTodaInfo();
                break;
            case 3:
                this.ctrFicha4.CargarTodaInfo();
                break;
            case 4:
                this.ctrFicha5.CargarTodaInfo();
                break;
            case 5:
                this.ctrFicha6.CargarTodaInfo();
                break;
            case 6:
                this.ctrFicha7.CargarTodaInfo();
                break;
            case 7:
                this.ctrFicha8.CargarTodaInfo();
                break;
            case 8:
                this.ctrFicha9.CargarTodaInfo();
                break;
            case 9:
                this.ctrFicha10.CargarTodaInfo();
                break;
            case 10:
                this.ctrFicha11.CargarTodaInfo();
                break;
        }
    }
    protected void tbFormularioEIA_ActiveTabChanged(object sender, EventArgs e)
    {

    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        string _respuestaEnvio = "<script language='JavaScript'>" +
              "window.close()</script>";
        Page.RegisterStartupScript("PopupScript", _respuestaEnvio);
    }
}
