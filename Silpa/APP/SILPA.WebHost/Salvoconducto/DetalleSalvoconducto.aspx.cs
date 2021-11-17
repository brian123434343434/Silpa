using SILPA.LogicaNegocio.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salvoconducto_DetalleSalvoconducto : System.Web.UI.Page
{
    public int SalID { get { return (Int32)ViewState["SalID"]; } set { ViewState["SalID"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarPagina();
        }
    }

    private void CargarPagina()
    {
        
        if (Request.QueryString["SAL"] != null)
        {
            ConsultarSalvoconducto(Request.QueryString["SAL"].ToString());
        }
        if (Request.QueryString["AD"] != null && Request.QueryString["SAL"] != null)
        {
            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                if (this.lblEstado.Text == "Solicitud")
                {
                    this.trAcciones.Visible = true;
                }
                else
                {
                    this.trAcciones.Visible = false;
                }
            }

        }
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
    private void ConsultarSalvoconducto(string pSalvoconductoID)
    {
        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        var salvoconducto = clsSalvoconductoNew.ConsultaSalvoconductoXSalvoconductoId(Convert.ToInt32(pSalvoconductoID));
        if (salvoconducto.SalvoconductoID > 0)
        {
            SalID = salvoconducto.SalvoconductoID;
            lblNumeroSalvoconducto.Text = salvoconducto.Numero;
            lblNumeroVITAL.Text = salvoconducto.NumeroVitalTramite;
            lblAutoridadAmbiental.Text = salvoconducto.AutoridadEmisora;
            lblTipoSalvoconducto.Text = salvoconducto.TipoSalvoconducto;
            lblFechaInicioVigencia.Text = salvoconducto.FechaInicioVigencia == DateTime.MinValue ? string.Empty : salvoconducto.FechaInicioVigencia.Value.ToShortDateString();
            lblFechaFinVigencia.Text = salvoconducto.FechaFinalVigencia == DateTime.MinValue ? string.Empty : salvoconducto.FechaFinalVigencia.Value.ToShortDateString(); ;
            
            lblEstado.Text = salvoconducto.Estado;
            lblClaseRecurso.Text = salvoconducto.ClaseRecurso;
            
            
            lblEstablecimiento.Text = salvoconducto.EstablecimientoProcedencia;
            
            lblFormaOtorgamiento.Text = salvoconducto.FormatOtorgamiento;
            lblFinalidadRecurso.Text = salvoconducto.Finalidad;
            lblDeptoProcedencia.Text = salvoconducto.DepartamentoProcedencia;
            lblMunpioProcedencia.Text = salvoconducto.MunicipioProcedencia;
            lblCorregimientoProcedencia.Text = salvoconducto.CorregimientoProcedencia;
            lblVeredaProcedencia.Text = salvoconducto.VeredaProcedencia;
            grvRutaDesplazamiento.DataSource = salvoconducto.LstRuta;
            grvRutaDesplazamiento.DataBind();
            lblModoTransporte.Text = salvoconducto.Transporte.ModoTransporte;
            lblTipoTransporte.Text = salvoconducto.Transporte.TipoTransporte;
            lblEmpresa.Text = salvoconducto.Transporte.Empresa;
            lblMatricula.Text = salvoconducto.Transporte.NumeroIdentificacionMedioTransporte;
            lblNombreTransportador.Text = salvoconducto.Transporte.NombreTransportador;
            lblTipoIdentificacion.Text = salvoconducto.Transporte.TipoIdentificacionTransportador;
            lblNumeroIdentificacionTransportador.Text = salvoconducto.Transporte.NumeroIdentificacionTransportador;
            grvEspecimenes.DataSource = salvoconducto.LstEspecimen;
            grvEspecimenes.DataBind();
        }
    }
    protected void btnEmitir_Click(object sender, EventArgs e)
    {
        try
        {
            // validamos el estado actual del salvoconducto.
            switch (lblEstado.Text)
            {
                case "Solicitud":
                    //SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
                    //string strNroSalvoconducto = clsSalvoconductoNew.GenerarSalvoconductoMovilizacion(SalID, DatosSesion.Usuario);
                    //if (!strNroSalvoconducto.Contains("Error"))
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "VITAL", "alert('Se generó con existo el Salvoconducto con Nro." + strNroSalvoconducto + "');", true);
                    //    ConsultarSalvoconducto(SalID.ToString());
                    //    this.trAcciones.Visible = false;
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "VITAL", "alert('Se generó un error." + strNroSalvoconducto + "');", true);
                    //}
                    break;
                case "Emitido":
                    //TODO EMITIDO
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "VITAL", "alert('Se generó un error al intentar emitir el Salvoconducto" + ex.Message + "');", true);
            throw;
        }
    }
}