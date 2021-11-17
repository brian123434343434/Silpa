using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResumenEIA_InfoAdicional_FuentSuperCalidad : System.Web.UI.Page
{
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
        int tipoCaracteristica;
        Int32.TryParse(cboTipoCaracteristica.SelectedValue.ToString(), out tipoCaracteristica);
        cboCaracteristica.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaCaracteristica(tipoCaracteristica);
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
            covFuente.Display=ValidatorDisplay.Dynamic;
            covFuente.Text = "*";
            covFuente.ControlToValidate=txtFuente.ID.ToString();
            covFuente.ValidationGroup="Caracteristica";
            covFuente.Operator=ValidationCompareOperator.DataTypeCheck;
            covFuente.Type=ValidationDataType.Double;
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
        txtLimiteDeteccion.Text = "";
        cboMetDeterminacion.SelectedIndex = -1;
    }
    protected void cboTipoCaracteristica_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarCaract();
    }
}
