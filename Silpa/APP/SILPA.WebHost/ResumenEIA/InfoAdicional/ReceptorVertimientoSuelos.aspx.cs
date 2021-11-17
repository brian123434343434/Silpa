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
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class ResumenEIA_InfoAdicional_ReceptorVertimientoSuelos : System.Web.UI.Page
{
    private int Sitios;
    private int tipo;
    private string _str_btnActualizar;
    private string epvId;
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
            this.pruebaField.Controls.Add(new LiteralControl("<tr><td width='25%'>"));
            Label2 = new Label();
            Label2.ID = "lblSitio" + i.ToString();
            Label2.Attributes.Add("runat", "server");
            Label2.SkinID = "etiqueta_negra";
            Label2.Text = "Sitio " + i.ToString();
            this.pruebaField.Controls.Add(Label2);
            this.pruebaField.Controls.Add(new LiteralControl("</td><td width='75%'>"));

            if (tipo == 95 || tipo == 99)
            {
                cboGeneral = new DropDownList();
                cboGeneral.ID = "cboSitio" + i.ToString();
                cboGeneral.Attributes.Add("runat", "server");
                cboGeneral.Style.Value = "Width='99%';";
                cboGeneral.SkinID = "lista_desplegable";
                this.pruebaField.Controls.Add(cboGeneral);

                this.CompareValidator1 = new CompareValidator();
                this.CompareValidator1.ID = "rqvSitio" + i.ToString();
                this.CompareValidator1.Attributes.Add("runat", "server");
                this.CompareValidator1.ErrorMessage = "Ingrese Información Sitio " + i.ToString();
                this.CompareValidator1.Display = ValidatorDisplay.Dynamic;
                this.CompareValidator1.ControlToValidate = "cboSitio" + i.ToString();
                this.CompareValidator1.ValueToCompare = "-1";
                this.CompareValidator1.ValidationGroup = "Validacion";
                this.CompareValidator1.Operator = ValidationCompareOperator.NotEqual;
                this.CompareValidator1.Text = "*";
                this.pruebaField.Controls.Add(CompareValidator1);
            }
            else
            {
                TextBox1 = new TextBox();
                TextBox1.ID = "txtSitio" + i.ToString();
                TextBox1.Attributes.Add("runat", "server");
                TextBox1.Style.Value = "Width='99%';";
                TextBox1.SkinID = "texto_sintamano";
                this.pruebaField.Controls.Add(TextBox1);

                this.RequiredFieldValidator1 = new RequiredFieldValidator();
                this.RequiredFieldValidator1.ID = "rqvSitio" + i.ToString();
                this.RequiredFieldValidator1.Attributes.Add("runat", "server");
                this.RequiredFieldValidator1.ErrorMessage = "Ingrese Información Sitio " + i.ToString();
                this.RequiredFieldValidator1.Display = ValidatorDisplay.Dynamic;
                this.RequiredFieldValidator1.ControlToValidate = "txtSitio" + i.ToString();
                this.RequiredFieldValidator1.ValidationGroup = "Validacion";
                this.RequiredFieldValidator1.Text = "*";
                this.pruebaField.Controls.Add(RequiredFieldValidator1);

                if (tipo == 96)
                {
                    CalendarExtender1 = new AjaxControlToolkit.CalendarExtender();
                    CalendarExtender1.ID = "cleSitio" + i.ToString();
                    CalendarExtender1.Format = "dd/MM/yyyy";
                    CalendarExtender1.TargetControlID = "txtSitio" + i.ToString();
                    this.pruebaField.Controls.Add(CalendarExtender1);
                }
                if (tipo == 97 || tipo == 98)
                {
                    MaskedEditExtender1 = new AjaxControlToolkit.MaskedEditExtender();
                    MaskedEditExtender1.ID = "mskSitio" + i.ToString();
                    MaskedEditExtender1.Mask = "9999";
                    MaskedEditExtender1.MaskType = AjaxControlToolkit.MaskedEditType.Number;
                    MaskedEditExtender1.AutoComplete = false;
                    MaskedEditExtender1.TargetControlID = "txtSitio" + i.ToString();
                    this.pruebaField.Controls.Add(MaskedEditExtender1);
                }

            }
            this.pruebaField.Controls.Add(new LiteralControl("</td></tr>"));
        }
        this.pruebaField.Controls.Add(new LiteralControl("</table>"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _str_btnActualizar = Page.Request["btn"];
        Sitios = int.Parse(Page.Request["sitios"].ToString());
        tipo = int.Parse(Page.Request["tip"].ToString());
        epvId = Page.Request["EPV_ID"].ToString();
        if (!this.Page.IsPostBack)
        {

            if (tipo == 95 || tipo == 96 || tipo == 97 || tipo == 98 || tipo == 99)
            {
                this.trMetodoDeter.Visible = false;
                this.trLimiteDetec.Visible = false;
                this.trMetales.Visible = false;
                this.trInfo.Visible = false;
            }
            else
            {
                CargarComboMetodoDeter();
                if (tipo != 106)
                    this.trMetales.Visible = false;

            }
            if (tipo == 95)
            {
                CargarCombosTipo1();
            }
            if (tipo == 99)
            {
                CargarCombosTipo2();
            }
        }
    }

    private void CargarCombosTipo1()
    {
        int i = 0;
        DataSet dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@ETM_ACTIVO", SqlDbType.Bit, 1, "ETM_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_TIPO_MUESTRA_CARACT_MUESTREO");

        for (i = 1; i <= Sitios; i++)
        {
            DropDownList cboSitios = (DropDownList)this.pruebaField.FindControl("cboSitio" + i.ToString());
            cboSitios.DataSource = dsDatos;
            cboSitios.DataTextField = "ETM_TIPO_MUESTRA";
            cboSitios.DataValueField = "ETM_ID";
            cboSitios.DataBind();
            cboSitios.Items.Insert(0, new ListItem("Seleccione..", "-1"));
        }
    }
    private void CargarCombosTipo2()
    {
        int i = 0;
        DataSet dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPM_ACTIVO", SqlDbType.Bit, 1, "EPM_ACTIVO");
        par.Value = 1;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIB_PERIODO_MUESTREO");

        for (i = 1; i <= Sitios; i++)
        {
            DropDownList cboSitios = (DropDownList)this.pruebaField.FindControl("cboSitio" + i.ToString());
            cboSitios.DataSource = dsDatos;
            cboSitios.DataTextField = "EPM_PERIODO_MUESTREO";
            cboSitios.DataValueField = "EPM_ID";
            cboSitios.DataBind();
            cboSitios.Items.Insert(0, new ListItem("Seleccione..", "-1"));
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
        if (tipo == 95 || tipo == 99)
        {
            GuardarInformacionTipo1();
        }
        else if (tipo == 96 || tipo == 97 || tipo == 98)
        {
            GuardarInformacionTipo2();
        }
        else
        {
            GuardarInformacionTipo3();
        }
        this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "asociacion", "<script>window.opener.document.forms[0]." + this._str_btnActualizar + ".click();window.close();</script>");
    }
    private void GuardarInformacionTipo1()
    {
        DataSet dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EFA_ID", SqlDbType.Int, 21, "EFA_ID");
        par.Value = epvId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_PARAM_MUEST_AGUA_VERT");

        //SE QUEMA EL VALOR PARA DESPUES ASIGNARLO EN UN ENUM
        int i = 0;
        for (i = 1; i <= Sitios; i++)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EPC_ID"] = tipo;
            dr["EFA_ID"] = epvId;
            dr["ECE_ID"] = (i + 3).ToString();
            DropDownList cboSitios = (DropDownList)this.pruebaField.FindControl("cboSitio" + i.ToString());
            dr["EFV_VALOR"] = cboSitios.SelectedValue;
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_PARAM_MUEST_AGUA_VERT");
    }
    private void GuardarInformacionTipo2()
    {
        DataSet dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPV_ID", SqlDbType.Int, 21, "EPV_ID");
        par.Value = epvId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_PARAM_MUEST_SUELO_VERT");

        //SE QUEMA EL VALOR PARA DESPUES ASIGNARLO EN UN ENUM
        int i = 0;
        for (i = 1; i <= Sitios; i++)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EPC_ID"] = tipo;
            dr["EPV_ID"] = epvId;
            dr["ECE_ID"] = (i + 3).ToString();
            TextBox cboSitios = (TextBox)this.pruebaField.FindControl("txtSitio" + i.ToString());
            dr["EFQ_VALOR"] = cboSitios.Text;
            dr["EFQ_METAL"] = DBNull.Value;
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_PARAM_MUEST_SUELO_VERT");
    }
    private void GuardarInformacionTipo3()
    {
        DataSet dsDatos = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter par = new SqlParameter("@EPV_ID", SqlDbType.Int, 21, "EPV_ID");
        par.Value = epvId;
        parametros.Add(par);
        SoftManagement.Persistencia.Contexto.cargarTabla(parametros, dsDatos, "EIH_PARAM_MUEST_SUELO_VERT");

        string metal = null;

        if (!string.IsNullOrEmpty(this.txtMetal.Text))
            metal = this.txtMetal.Text;

        DataRow dr1 = dsDatos.Tables[0].NewRow();
        dr1["EPC_ID"] = tipo;
        dr1["EPV_ID"] = epvId;
        //quemo 1 Tipo de Determinacion
        dr1["ECE_ID"] = 1;
        dr1["EFQ_VALOR"] = this.cboMetodoDeterminacion.SelectedValue;
        dr1["EFQ_METAL"] = metal;
        dsDatos.Tables[0].Rows.Add(dr1);

        DataRow dr2 = dsDatos.Tables[0].NewRow();
        dr2["EPC_ID"] = tipo;
        dr2["EPV_ID"] = epvId;
        //quemo 2 Limite de determinacion
        dr2["ECE_ID"] = 2;
        dr2["EFQ_VALOR"] = this.txtLimiteDetección.Text;
        dr2["EFQ_METAL"] = metal;
        dsDatos.Tables[0].Rows.Add(dr2);

        DataRow dr3 = dsDatos.Tables[0].NewRow();
        dr3["EPC_ID"] = tipo;
        dr3["EPV_ID"] = epvId;
        //quemo 2 Informacion Adicional
        dr3["ECE_ID"] = 3;
        dr3["EFQ_VALOR"] = this.txtInfo.Text;
        dr3["EFQ_METAL"] = metal;
        dsDatos.Tables[0].Rows.Add(dr3);
        
        
        //SE QUEMA EL VALOR PARA DESPUES ASIGNARLO EN UN ENUM
        int i = 0;
        for (i = 1; i <= Sitios; i++)
        {
            DataRow dr = dsDatos.Tables[0].NewRow();
            dr["EPC_ID"] = tipo;
            dr["EPV_ID"] = epvId;
            dr["ECE_ID"] = (i + 3).ToString();
            TextBox cboSitios = (TextBox)this.pruebaField.FindControl("txtSitio" + i.ToString());
            dr["EFQ_VALOR"] = cboSitios.Text;
            dr["EFQ_METAL"] = metal;
            dsDatos.Tables[0].Rows.Add(dr);
        }
        Contexto.guardarTabla(dsDatos, "EIH_PARAM_MUEST_SUELO_VERT");
    }
}
