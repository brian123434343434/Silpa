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

public partial class EIA_Consultar_Formularios : System.Web.UI.Page
{
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (ValidacionToken())
            {
                this._usuarioRegistrado = (string)Session["Usuario"];
            }
            SILPA.LogicaNegocio.EIA.EIA objEIA = new SILPA.LogicaNegocio.EIA.EIA();
            cboNumeroVital.DataSource = objEIA.ConsultaListaNumeroVital(int.Parse(this._usuarioRegistrado));
            cboNumeroVital.DataValueField = "NUMERO_VITAL";
            cboNumeroVital.DataTextField = "NUMERO_VITAL";
            cboNumeroVital.DataBind();
            cboNumeroVital.Items.Insert(0, new ListItem("Seleccione.", "-1"));
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

    protected void btnConsultar_Click(object sender, EventArgs e)
    {       
        Consultar();
    }
    protected void grdReporte_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DETALLE")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Label lblEipId = (Label)this.grdReporte.Rows[index].FindControl("lblEipId");
            Response.Redirect("Default.aspx?IDProyecto=" + lblEipId.Text);
        }
    }
    protected void grdReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //grdReporte.PageIndexChanged();
        grdReporte.PageIndex = e.NewPageIndex;
        Consultar();        
        grdReporte.PageSize = 5;
        //CargarSector();
    }

    private void Consultar()
    {        
        SILPA.LogicaNegocio.EIA.EIA objEIA = new SILPA.LogicaNegocio.EIA.EIA();
        string numeroVital = "";
        if (this.cboNumeroVital.SelectedValue != "-1")
            numeroVital = this.cboNumeroVital.SelectedValue;
        this.grdReporte.DataSource = objEIA.ConsultarFormularios(numeroVital, int.Parse(this._usuarioRegistrado), null, null);
        this.grdReporte.EmptyDataText = "No se encontraron registros para esta consulta.";
        this.grdReporte.DataBind();
    }
}
