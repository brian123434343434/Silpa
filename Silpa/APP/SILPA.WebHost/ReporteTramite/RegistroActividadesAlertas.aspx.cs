using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ReporteTramite_RegistroActividadesAlertas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
        }
    }

    private void CargarCombos()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();        
     
        cboActividad.Items.Insert(0, new ListItem("Seleccione...", ""));
        cboDocumentosSila.Items.Insert(0, new ListItem("Seleccione...", ""));

        SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
        DataTable _temp = objtramites.Tramites();
        cboTramite.DataSource = _temp;
        cboTramite.DataValueField = "TRA_ID";
        cboTramite.DataTextField = "TRA_NOMBRE";
        cboTramite.DataBind();
        cboTramite.Items.Insert(0, new ListItem("Seleccione...", ""));


        SILPA.LogicaNegocio.Generico.Persona personas = new SILPA.LogicaNegocio.Generico.Persona();
        DataTable dat = personas.ConsultarPersonasActivas();

           
    }
 
    protected void cboTramite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarActividadesXTramite();
        CargarActividadesTramite();
    }

    private void CargarActividadesXTramite()
    {
        cboActividad.Items.Clear();
        SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
        cboDocumentosSila.Items.Clear();
        if (cboTramite.SelectedValue != "")
        {
            DataTable _temp = objtramites.Actividades(int.Parse(cboTramite.SelectedValue));
            cboActividad.DataSource = _temp;
            cboActividad.DataValueField = "ACT_ID";
            cboActividad.DataTextField = "ACT_NOMBRE";
            cboActividad.DataBind();
        }
        cboActividad.Items.Insert(0, new ListItem("Seleccione...", ""));
        cboDocumentosSila.Items.Insert(0, new ListItem("Seleccione...", ""));        
    }

    private void CargarActividadesTramite()
    {
        DataTable _temp3;
        SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
        if (cboTramite.SelectedValue != "")
        {
            _temp3 = objtramites.ReporteXTramite(int.Parse(cboTramite.SelectedValue));                        
        }
        else
        {
            _temp3 = objtramites.ReporteXTramite(0);                      
        }
        this.grdListaActividades.DataSource = _temp3;
        this.grdListaActividades.DataBind();
    }

    protected void btnGuardarClick(object sender, ImageClickEventArgs e)
    {
        SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
        bool bnegrilla = false;
        if (this.cboNegrilla.SelectedValue == "1")
            bnegrilla = true;
        objtramites.InsertarRelacionAlerta(int.Parse(cboTramite.SelectedValue), this.txtNombre.Text, int.Parse(this.txtDiaLimite.Text), this.cboCaracter.SelectedItem.Text, int.Parse(this.cboActividad.SelectedValue), int.Parse(this.cboDocumentosSila.SelectedValue), this.txtColor.Text, bnegrilla, this.txtColorFondo.Text, this.rblCorrreo.SelectedValue, "0", this.txtCorreoElectronico.Text, this.txtDiaIniciaAlerta.Text);
        CargarActividadesTramite();
    }

    protected void cboActividad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDocumentosXActividad();
    }

    private void CargarDocumentosXActividad()
    {
        cboDocumentosSila.Items.Clear();
        if (cboActividad.SelectedValue != "")
        {
            SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
            DataTable _temp = objtramites.DocumentosSila(int.Parse(cboActividad.SelectedValue));
            cboDocumentosSila.DataSource = _temp;
            cboDocumentosSila.DataValueField = "DOC_ID";
            cboDocumentosSila.DataTextField = "DOC_NOMBRE";
            cboDocumentosSila.DataBind();
        }
        cboDocumentosSila.Items.Insert(0, new ListItem("Seleccione...", ""));
    }
 
    protected void grdListaActividades_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        { 
            int index=int.Parse(e.CommandArgument.ToString());
            Label lblTramite = (Label)this.grdListaActividades.Rows[index].FindControl("lblTramite");
            Label lblActividad = (Label)this.grdListaActividades.Rows[index].FindControl("lblActividad");
            Label lblDocumento = (Label)this.grdListaActividades.Rows[index].FindControl("lblDocumento");
            Label lblColorLetra = (Label)this.grdListaActividades.Rows[index].FindControl("lblColorletra");
            Label lblColorFondo = (Label)this.grdListaActividades.Rows[index].FindControl("lblColorFondo");
            Label lblTipoCorreo = (Label)this.grdListaActividades.Rows[index].FindControl("lblTipoCorreo");
            Label lblIdSolicitante = (Label)this.grdListaActividades.Rows[index].FindControl("lblIdSolicitante");
            CheckBox chkNegrilla = (CheckBox)this.grdListaActividades.Rows[index].FindControl("chkNegrilla");

            this.cboTramite.SelectedValue = lblTramite.Text;
            CargarActividadesXTramite();
            

            this.txtNombre.Text = Server.HtmlDecode(this.grdListaActividades.Rows[index].Cells[1].Text);
            this.txtDiaLimite.Text = Server.HtmlDecode(this.grdListaActividades.Rows[index].Cells[2].Text);
            if (this.grdListaActividades.Rows[index].Cells[3].Text == "Obligatorio")
                this.cboCaracter.SelectedValue = "1";
            else
                this.cboCaracter.SelectedValue = "0";
            //this.cboTiempoAdicional.SelectedItem.Text = Server.HtmlDecode(this.grdListaActividades.Rows[index].Cells[4].Text);
            this.cboActividad.SelectedValue = lblActividad.Text;
            CargarDocumentosXActividad();
            this.cboDocumentosSila.SelectedValue = lblDocumento.Text;
            //this.txtAccionVital.Text = Server.HtmlDecode(this.grdListaActividades.Rows[index].Cells[4].Text);

            this.ColorPickerExtender1.SelectedColor = lblColorLetra.Text;
            if (lblColorLetra.Text == "")
                this.txtColor.Text = "";

            if (chkNegrilla.Checked)
                this.cboNegrilla.SelectedValue = "1";
            else
                this.cboNegrilla.SelectedValue = "0";
            this.ColorPickerExtender2.SelectedColor = lblColorFondo.Text;


            
            if (lblTipoCorreo.Text!="")
                this.rblCorrreo.SelectedValue = lblTipoCorreo.Text;

            this.txtCorreoElectronico.Text = Server.HtmlDecode(this.grdListaActividades.Rows[index].Cells[7].Text);
            this.txtDiaIniciaAlerta.Text = Server.HtmlDecode(this.grdListaActividades.Rows[index].Cells[8].Text);
                        
        }
        else if (e.CommandName == "Eliminar")
        {
            int index=int.Parse(e.CommandArgument.ToString());
            Label lblTramite = (Label)this.grdListaActividades.Rows[index].FindControl("lblTramite");
            Label lblActividad = (Label)this.grdListaActividades.Rows[index].FindControl("lblActividad");
            Label lblDocumento = (Label)this.grdListaActividades.Rows[index].FindControl("lblDocumento");

            SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
            objtramites.EliminarRelacionAlerta(int.Parse(lblTramite.Text), int.Parse(lblActividad.Text), int.Parse(lblDocumento.Text));
            CargarActividadesTramite();
        }

    }

    protected void grdListaActividades_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label div = (Label)e.Row.FindControl("divColorLetra");
            Label lblColorLetra = (Label)e.Row.FindControl("lblColorletra");
            Label div2 = (Label)e.Row.FindControl("divColorFondo");
            Label lblColorFondo= (Label)e.Row.FindControl("lblColorFondo");

            div.Style.Add("background-color", "#" + lblColorLetra.Text);
            div2.Style.Add("background-color", "#" + lblColorFondo.Text);
        }
    } 
   
}

