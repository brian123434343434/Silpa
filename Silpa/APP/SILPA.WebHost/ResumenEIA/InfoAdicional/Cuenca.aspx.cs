using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Persistencia;
using System.Data;
using System.Data.SqlClient;

public partial class ResumenEIA_Fichas_InfoAdCuenca : System.Web.UI.Page
{
    //int idCuenca;
    DataSet dsGrilla;
    public int IdCuenca
    {
        get 
        {
            return (int)ViewState["idCuenca"];
        }
        set 
        {
            ViewState["idCuenca"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int idCuenca = 0;
        if (Request.QueryString["Cuenca"] != null && int.TryParse(Request.QueryString["Cuenca"], out idCuenca))
        {
            IdCuenca = idCuenca;
            if (!IsPostBack)
            {
                cargarDatos();
                cargarCombos();
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myCloseScript", "alert('Parámetros de ingreso incorrectos, intente nuevamente.'); window.close()", true);
        }
    }
    protected void btnCancelarInfoAdicional_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "myCloseScript", "alert('Datos guardados exitosamente.'); window.close()", true);
    }
    protected void cargarCombos()
    {
        cargarMesCaudalMin();
        cargarMesCaudalMax();
    }

    protected List<string> meses()
    {
        List<string> listaMeses = new List<string>();
        string mes = "";
        for (int i = 0; i < 11; i++)
        {
            mes = System.DateTime.Parse("01-JAN-1970").AddMonths(i).ToString("MMMM");
            listaMeses.Add(char.ToUpper(mes[0]) + mes.Substring(1));
        }
        return listaMeses;
    }

    protected void cargarMesCaudalMin()
    {
        cboMesCaudalMin.DataSource = meses();
        cboMesCaudalMin.DataBind();
        cboMesCaudalMin.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarMesCaudalMax()
    {
        cboMesCaudalMax.DataSource = meses();
        cboMesCaudalMax.DataBind();
        cboMesCaudalMax.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    protected void cargarDatos()
    {
        grvSubCuenca.DataSource = null;
        grvSubCuenca.DataBind();
        grvMicroCuenca.DataSource = null;
        grvMicroCuenca.DataBind();
        grvCuerpoLentico.DataSource = null;
        grvCuerpoLentico.DataBind();
        grvInvFAgua.DataSource = null;
        grvInvFAgua.DataBind();

        cargarGrillaSubCuenca();
        cargarGrillaMicroCuenca();
        cargarGrillaCuerpoLentico();

    }
    #region Sub Cuenca
    protected void btnNuevaSubCuenca_Click(object sender, EventArgs e)
    {
        plhSubCuenca.Visible = true;
        plhSubCuenca.Focus();
    }
    protected void btnCancelarSubCuenca_Click(object sender, EventArgs e)
    {
        plhSubCuenca.Visible = false;
    }
    protected void btnAgregarSubCuenca_Click(object sender, EventArgs e)
    {
        addRegistroSubCuenca();
    }
    protected void cargarGrillaSubCuenca()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@ECH_ID", SqlDbType.Int, 10, "ECH_ID");
        ParametroCarga.Value = IdCuenca;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_SUBCUENCAS_CUENCA");
        this.grvSubCuenca.DataSource = dsGrilla.Tables[0];
        this.grvSubCuenca.DataBind();
        
    }

    protected void addRegistroSubCuenca()
    {
        cargarGrillaSubCuenca();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["ECH_ID"] = IdCuenca;
        dr["ESC_CODIGO_MAPA"] = txtCodigoMapaSubCuenca.Text;
        dr["ESC_NOMBRE"] = txtNombreSubCuenca.Text;
        dr["ESC_AREA"] = txtAreaSubCuenca.Text;
        dr["ESC_USO_PRINCIPAL"] = txtUsoPricSubCuenca.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_SUBCUENCAS_CUENCA");
        this.txtCodigoMapaSubCuenca.Text = "";
        this.txtNombreSubCuenca.Text = "";
        this.txtAreaSubCuenca.Text = "";
        this.txtUsoPricSubCuenca.Text = "";
        cargarGrillaSubCuenca();
        plhSubCuenca.Visible = false;
        
    }

    protected void grvSubCuenca_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroSubCuenca(e.RowIndex);
    }

    protected void eliminarRegistroSubCuenca(int index)
    {
        cargarGrillaSubCuenca();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_SUBCUENCAS_CUENCA");
        cargarGrillaSubCuenca();
        plhSubCuenca.Visible = false;
    }
    #endregion
    #region MicroCuenca
    protected void btnNuevaMicroCuenca_Click(object sender, EventArgs e)
    {
        plhMicroCuenca.Visible = true;
        plhMicroCuenca.Focus();
    }
    protected void btnCancelarMicroCuenca_Click(object sender, EventArgs e)
    {
        plhMicroCuenca.Visible = false;
    }
    protected void btnAgregarMicroCuenca_Click(object sender, EventArgs e)
    {
        addRegistroMicroCuenca();
    }
    protected void cargarGrillaMicroCuenca()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@ECH_ID", SqlDbType.Int, 10, "ECH_ID");
        ParametroCarga.Value = IdCuenca;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_MICROCUENCAS_CUENCA");
        this.grvMicroCuenca.DataSource = dsGrilla.Tables[0];
        this.grvMicroCuenca.DataBind();
    }

    protected void addRegistroMicroCuenca()
    {
        cargarGrillaMicroCuenca();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["ECH_ID"] = IdCuenca;
        dr["EMC_CODIGO_MAPA"] = txtCodigoMapaMicroCuenca.Text;
        dr["EMC_NOMBRE"] = txtNombreMicroCuenca.Text;
        dr["EMC_AREA"] = txtAreaMicroCuenca.Text;
        dr["EMC_USO_PRINCIPAL"] = txtUsoPricMicroCuenca.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_MICROCUENCAS_CUENCA");
        this.txtCodigoMapaMicroCuenca.Text = "";
        this.txtNombreMicroCuenca.Text = "";
        this.txtAreaMicroCuenca.Text = "";
        this.txtUsoPricMicroCuenca.Text = "";
        cargarGrillaMicroCuenca();
        plhMicroCuenca.Visible = false;
    }

    protected void grvMicroCuenca_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroMicroCuenca(e.RowIndex);
    }

    protected void eliminarRegistroMicroCuenca(int index)
    {
        cargarGrillaMicroCuenca();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_MICROCUENCAS_CUENCA");
        cargarGrillaMicroCuenca();
        plhMicroCuenca.Visible = false;
    }
    #endregion
    #region CuerpoLentico
    protected void btnNuevaCuerpoLentico_Click(object sender, EventArgs e)
    {
        plhCuerpoLentico.Visible = true;
        plhCuerpoLentico.Focus();
    }
    protected void btnCancelarCuerpoLentico_Click(object sender, EventArgs e)
    {
        plhCuerpoLentico.Visible = false;
    }
    protected void btnAgregarCuerpoLentico_Click(object sender, EventArgs e)
    {
        addRegistroCuerpoLentico();
    }
    protected void cargarGrillaCuerpoLentico()
    {
        dsGrilla = new DataSet();
        List<SqlParameter> parametrosConsulta = new List<SqlParameter>();
        SqlParameter ParametroCarga = new SqlParameter("@ECH_ID", SqlDbType.Int, 10, "ECH_ID");
        ParametroCarga.Value = IdCuenca;
        parametrosConsulta.Add(ParametroCarga);
        Contexto.cargarTabla(parametrosConsulta, dsGrilla, "EIH_CUERPOS_LENTICOS_CUENCA");
        this.grvCuerpoLentico.DataSource = dsGrilla.Tables[0];
        this.grvCuerpoLentico.DataBind();
    }

    protected void addRegistroCuerpoLentico()
    {
        cargarGrillaCuerpoLentico();
        DataRow dr = dsGrilla.Tables[0].NewRow();
        dr["ECH_ID"] = IdCuenca;
        dr["ECL_NOMBRE_CUERPO_LENTICO"] = txtNombreCuerpoLentico.Text;
        dr["ECL_AREA_CUERPO_LENTICO"] = txtAreaCuerpoLentico.Text;
        dr["ECL_USO_PRINCIPAL"] = txtUsoPricCuerpoLentico.Text;
        dsGrilla.Tables[0].Rows.Add(dr);
        Contexto.guardarTabla(dsGrilla, "EIH_CUERPOS_LENTICOS_CUENCA");
        this.txtNombreCuerpoLentico.Text = "";
        this.txtAreaCuerpoLentico.Text = "";
        this.txtUsoPricCuerpoLentico.Text = "";
        cargarGrillaCuerpoLentico();
        plhCuerpoLentico.Visible = false;
    }

    protected void grvCuerpoLentico_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        eliminarRegistroCuerpoLentico(e.RowIndex);
    }

    protected void eliminarRegistroCuerpoLentico(int index)
    {
        cargarGrillaCuerpoLentico();
        this.dsGrilla.Tables[0].Rows[index].Delete();
        Contexto.guardarTabla(dsGrilla, "EIH_CUERPOS_LENTICOS_CUENCA");
        cargarGrillaCuerpoLentico();
        plhCuerpoLentico.Visible = false;
    }
    #endregion
    protected void btnNuevaInvFAgua_Click(object sender, EventArgs e)
    {
        plhInvFAgua.Visible = true;
        plhInvFAgua.Focus();
    }
    protected void btnCancelarInvFAgua_Click(object sender, EventArgs e)
    {
        plhInvFAgua.Visible = false;
    }
}
