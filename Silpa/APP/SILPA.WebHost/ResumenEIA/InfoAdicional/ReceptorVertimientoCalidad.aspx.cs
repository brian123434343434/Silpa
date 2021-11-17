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
using SoftManagement.Persistencia;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class ResumenEIA_InfoAdicional_ReceptorVertimientoCalidad : System.Web.UI.Page
{
    private int Sitios;
    private int tipo;
    private string _str_btnActualizar;
    private string evtId;
    protected System.Web.UI.WebControls.TextBox TextBox1;
    protected System.Web.UI.WebControls.DropDownList cboGeneral;
    protected System.Web.UI.WebControls.Label Label2;
    protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
    protected System.Web.UI.WebControls.CompareValidator CompareValidator1;
    protected AjaxControlToolkit.MaskedEditExtender MaskedEditExtender1;
    protected AjaxControlToolkit.CalendarExtender CalendarExtender1;
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.pruebaField.Controls.Add(new LiteralControl("<table>"));
        int i = 0;
        int tipo = int.Parse(Page.Request["tip"].ToString());
        int Sitios = int.Parse(Page.Request["sitios"].ToString());
        for (i = 1; i <= Sitios; i++)
        {
            this.pruebaField.Controls.Add(new LiteralControl("<tr><td width='60%'>"));
            Label2 = new Label();
            Label2.ID = "lblSitio" + i.ToString();
            Label2.Attributes.Add("runat", "server");
            Label2.SkinID = "etiqueta_negra";
            Label2.Text = "Concentración Esperada Vertimiento Tipo " + i.ToString();
            this.pruebaField.Controls.Add(Label2);
            this.pruebaField.Controls.Add(new LiteralControl("</td><td width='40%'>"));            
            
            TextBox1 = new TextBox();
            TextBox1.ID = "txtSitio" + i.ToString();
            TextBox1.Attributes.Add("runat", "server");
            TextBox1.Style.Value = "Width='99%';";
            TextBox1.SkinID = "texto_sintamano";
            this.pruebaField.Controls.Add(TextBox1);

            this.RequiredFieldValidator1 = new RequiredFieldValidator();
            this.RequiredFieldValidator1.ID = "rqvSitio" + i.ToString();
            this.RequiredFieldValidator1.Attributes.Add("runat", "server");
            this.RequiredFieldValidator1.ErrorMessage = "Ingrese Información Tipo Vertimiento " + i.ToString();
            this.RequiredFieldValidator1.Display = ValidatorDisplay.Dynamic;
            this.RequiredFieldValidator1.ControlToValidate = "txtSitio" + i.ToString();
            this.RequiredFieldValidator1.ValidationGroup = "Validacion";
            this.RequiredFieldValidator1.Text = "*";
            this.pruebaField.Controls.Add(RequiredFieldValidator1);
            MaskedEditExtender1 = new AjaxControlToolkit.MaskedEditExtender();
            MaskedEditExtender1.ID = "mskSitio" + i.ToString();
            MaskedEditExtender1.Mask = "9999999";
            MaskedEditExtender1.MaskType = AjaxControlToolkit.MaskedEditType.Number;
            MaskedEditExtender1.AutoComplete = false;
            MaskedEditExtender1.TargetControlID = "txtSitio" + i.ToString();
            this.pruebaField.Controls.Add(MaskedEditExtender1);
            
            this.pruebaField.Controls.Add(new LiteralControl("</td></tr>"));     
        }
        this.pruebaField.Controls.Add(new LiteralControl("</table>"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _str_btnActualizar = Page.Request["btn"];
        Sitios = int.Parse(Page.Request["sitios"].ToString());
        tipo = int.Parse(Page.Request["tip"].ToString());
        evtId = Page.Request["EVT_ID"].ToString();
        if (!this.Page.IsPostBack)
        {
            CargarComboMetodoDeter();                        
        }
    }
   
    private void CargarComboMetodoDeter()
    {
        int i = 0;
        DataSet dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EMC_ACTIVO", SqlDbType.Bit, 1, "EMC_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_METODOS_DET_CALIDAD");

        cboMetodoDeterminacion.DataSource = dsDatos;
        cboMetodoDeterminacion.DataTextField = "EMC_METODOS_DET_CALIDAD";
        cboMetodoDeterminacion.DataValueField = "EMC_ID";
        cboMetodoDeterminacion.DataBind();
        cboMetodoDeterminacion.Items.Insert(0, new ListItem("Seleccione..", "-1"));

    }


    protected void btnCancelarSitMonitRuido_Click(object sender, EventArgs e)
    {
        string strScript;
        strScript = "<script language='JavaScript'>self.close()</script>";
        Page.RegisterClientScriptBlock("Pruebas", strScript);
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        
        GuardarInformacionTipo3();       
        this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "asociacion", "<script>window.opener.document.forms[0]." + this._str_btnActualizar + ".click();window.close();</script>");
    }
   
    private void GuardarInformacionTipo3()
    {

        DataSet dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EVT_ID", SqlDbType.Int, 20, "EVT_ID");
        par.Value = evtId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_DESCRIP_VERTIMIENTOS");

        DataSet dsDatos2 = new DataSet();
        List<SqlParameter> parametros3 = new List<SqlParameter>();
        SqlParameter par3 = new SqlParameter("@EPC_ID", SqlDbType.Int, 21, "EPC_ID");
        par3.Value = tipo;
        parametros.Add(par3);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros3,dsDatos2 , "EIH_CALIDAD_ESP_VERTIMIENTO");



        DataRow dr1 = dsDatos2.Tables[0].NewRow();
        dr1["EPC_ID"] = tipo;
        dr1["EMC_ID"] = this.cboMetodoDeterminacion.SelectedValue;
        //quemo 1 Tipo de Determinacion
        dr1["ECV_LIMITE_DETECCION"] = this.txtLimiteDetección.Text;        
        dsDatos2.Tables[0].Rows.Add(dr1);

        Contexto.guardarTabla(dsDatos2, "EIH_CALIDAD_ESP_VERTIMIENTO");

        DataSet dsDatos3 = new DataSet();
        List<SqlParameter> parametros2 = new List<SqlParameter>();
        SqlParameter par2 = new SqlParameter("@EPC_ID", SqlDbType.Int, 21, "EPC_ID");
        par2.Value = tipo;
        parametros2.Add(par2);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros2, dsDatos3, "EIH_CALIDAD_ESP_VERTIMIENTO");

        string ecvId = dsDatos3.Tables[0].Rows[dsDatos3.Tables[0].Rows.Count-1]["ECV_ID"].ToString();

        DataSet dsDatos4 = new DataSet();
        List<SqlParameter> parametros1 = new List<SqlParameter>();
        SqlParameter par1 = new SqlParameter("@ECV_ID", SqlDbType.Int, 21, "ECV_ID");
        par1.Value = ecvId;
        parametros1.Add(par1);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros1, dsDatos4, "EIH_VALOR_ESP_EST_CALIDAD_TIPO_VERT");

        //SE QUEMA EL VALOR PARA DESPUES ASIGNARLO EN UN ENUM
        int i = 0;
        for (i = 1; i <= Sitios; i++)
        {

            DataRow dr2 = dsDatos4.Tables[0].NewRow();
            dr2["EDV_ID"] = dsDatos.Tables[0].Rows[i-1]["EDV_ID"];
            dr2["ECV_ID"] = ecvId;
            TextBox cboSitios = (TextBox)this.pruebaField.FindControl("txtSitio" + i.ToString());
            dr2["EVV_VALOR"] = cboSitios.Text;
            dsDatos4.Tables[0].Rows.Add(dr2);
        }
        Contexto.guardarTabla(dsDatos4, "EIH_VALOR_ESP_EST_CALIDAD_TIPO_VERT");
    }
}
