using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Persistencia;
using System.Data;
using System.Data.SqlClient;

public partial class ResumenEIA_InfoAdicional_FuentCalAire : System.Web.UI.Page
{
    DataSet dsGrilla = new DataSet();
    int NoFuent = 0;
    int NoProyecto = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["NoFuent"] != null)
        {
            NoFuent = Convert.ToInt32(Request.QueryString["NoFuent"].ToString());
            if (Request.QueryString["NoP"] != null)
            {
                NoProyecto = Convert.ToInt32(Request.QueryString["NoP"].ToString());
                cargarControles();
            }
        }

        if (!IsPostBack)
            cargarCombos();
    }

    protected void cargarCombos()
    {
        cargarCaract();
        cargarMetDeterminacion();
    }
    protected void cargarCaract()
    {

        cboCaracteristica.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaCaracteristica(2);
        cboCaracteristica.DataValueField = "EPC_ID";
        cboCaracteristica.DataTextField = "EPC_PARAMETROS_EST_CALIDAD";
        cboCaracteristica.DataBind();
        cboCaracteristica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarMetDeterminacion()
    {

        cboMetDeterminacion.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaMetDetEstCalidad();
        cboMetDeterminacion.DataValueField = "EMC_ID";
        cboMetDeterminacion.DataTextField = "EMC_METODOS_DET_CALIDAD";
        cboMetDeterminacion.DataBind();
        cboMetDeterminacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void cargarControles()
    {
        for (int i = 0; i < NoFuent; i++)
        {
            //Agrega inicio de fila y de celda
            plhFuentesEstCalAgua.Controls.Add(new LiteralControl("<tr><td width='25%'>"));
            //Crea el TextBox y define sus propiedades
            TextBox txtFuente = new TextBox();
            txtFuente.ID = "txtFuente" + Convert.ToString(i + 1);
            txtFuente.SkinID = "texto_sintamano";
            Unit ancho = new Unit("99%");
            txtFuente.Width = ancho;
            //Crea el Label y define sus propiedades
            Label lblFuente = new Label();
            lblFuente.ID = "lblFuente" + Convert.ToString(i + 1);
            lblFuente.SkinID = "etiqueta_negra";
            lblFuente.Text = "Fuente " + Convert.ToString(i + 1);
            //Crea el control de validación
            CompareValidator covFuente = new CompareValidator();
            covFuente.ID = "covFuente" + Convert.ToString(i + 1);
            covFuente.ErrorMessage = "La información de la caracteristica para esta fuente debe ser un valor Númerico";
            covFuente.Display = ValidatorDisplay.Dynamic;
            covFuente.Text = "*";
            covFuente.ControlToValidate = txtFuente.ID.ToString();
            covFuente.ValidationGroup = "Caracteristica";
            covFuente.Operator = ValidationCompareOperator.DataTypeCheck;
            covFuente.Type = ValidationDataType.Double;
            //Agrega los controles al panel
            plhFuentesEstCalAgua.Controls.Add(lblFuente);
            plhFuentesEstCalAgua.Controls.Add(new LiteralControl("</td><td width='25%'>"));
            plhFuentesEstCalAgua.Controls.Add(txtFuente);
            plhFuentesEstCalAgua.Controls.Add(covFuente);
            plhFuentesEstCalAgua.Controls.Add(new LiteralControl("</td></tr>"));
        }
    }
    protected void btnCancelarCaracteristica_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < NoFuent - 1; i++)
        {
            TextBox txtFuente = (TextBox)plhFuentesEstCalAgua.FindControl("txtFuente" + Convert.ToString(i + 1));
            txtFuente.Text = "";
        }
        cboCaracteristica.SelectedIndex = -1;
        txtConcentPermit.Text = "";
        cboMetDeterminacion.SelectedIndex = -1;
        txtFrecMuestreo.Text = "";
    }

    protected void btnAgregarSitMonitCalAire_Click(object sender, EventArgs e)
    {
        addRegistroSitMonitCalAire();
    }
    protected void cargarGrillaSitMonitCalAire()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@EIC_ID", SqlDbType.Int, 10, "EIP_ID");
        ParametroCarga.Value = NoProyecto;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_PARAM_MUEST_AIRE");
        this.grvCalAire.DataSource = dsGrilla.Tables[0];
        this.grvCalAire.DataBind();
    }

    protected void addRegistroSitMonitCalAire()
    {
        cargarGrillaSitMonitCalAire();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["EIP_ID"] = NoProyecto;
        dr["EPC_ID"] = cboCaracteristica.SelectedValue.ToString();
        dr["EMC_ID"] = cboMetDeterminacion.SelectedValue.ToString();
        dr["EQA_CONCENT_PERMIT"] = txtConcentPermit.Text;
        dr["EQA_FREC_MUESTREO"] = txtFrecMuestreo.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_PARAM_MUEST_AIRE");
        this.cboCaracteristica.SelectedIndex = -1;
        this.cboMetDeterminacion.SelectedIndex = -1;
        this.txtConcentPermit.Text = "";
        this.txtFrecMuestreo.Text = "";
        cargarGrillaSitMonitCalAire();
    }

    protected void grvSitMonitCalAire_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroSitMonitCalAire(e.RowIndex);
    }

    protected void eliminarRegistroSitMonitCalAire(int index)
    {
        cargarGrillaSitMonitCalAire();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_PARAM_MUEST_AIRE");
        cargarGrillaSitMonitCalAire();
    }
}
