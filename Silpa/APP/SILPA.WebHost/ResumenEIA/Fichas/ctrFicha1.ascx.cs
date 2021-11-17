using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using SoftManagement.Log;
using System.Data;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using SoftManagement.Persistencia;
using System.Data.SqlClient;
using SILPA.AccesoDatos.ResumenEIA.Basicas;

public partial class ResumenEIA_ctrFicha1 : System.Web.UI.UserControl
{
    string _usuarioRegistrado = "";
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

    DataSet dsficha1;
    DataSet dsDatosExistentes;

    private void cargarDatasetFicha1()
    {
        dsficha1 = new DataSet();
        List<SqlParameter> parametros = new List<SqlParameter>();
        SqlParameter parametroCarga = new SqlParameter("@EIP_ID", SqlDbType.Int, 10, "EIP_ID");

        parametros.Add(parametroCarga);
        parametroCarga.Value = IDProyecto;
        Contexto.cargarTabla(parametros, dsficha1, "EIH_DATOS_EMPRESA");

    }
    private void PrecargaDatosPersona(string p)
    {
        this.dsDatosExistentes = Ficha1Dalc.cargarInformacionEmpresa(int.Parse (p));
        if (this.dsDatosExistentes.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsDatosExistentes.Tables[0].Rows[0];                      
            this.txtApellidoRepresentante.Text = dr["PER_PRIMER_APELLIDO"].ToString();//
            this.txtDireccion.Text = dr["dip_direccion"].ToString();            
            this.txtFax.Text = dr["PER_FAX"].ToString();
            this.txtNit.Text = dr["PER_NUMERO_IDENTIFICACION"].ToString();
            this.txtNoDocRepresentante.Text = dr["PER_NUMERO_IDENTIFICACION_REP"].ToString();           
            this.txtNombreEmpresa.Text = dr["PER_RAZON_SOCIAL"].ToString();
            this.txtNombreRepresentante.Text = dr["PER_PRIMER_NOMBRE"].ToString();
            this.txtTelefono.Text = dr["PER_TELEFONO"].ToString();
            if (dr["DEP_ID"].ToString()!="")
                this.cboDepartamento.SelectedValue = dr["DEP_ID"].ToString();
            if (dr["MUN_ID"].ToString() != "")
                this.cboMunicipio.SelectedValue = dr["MUN_ID"].ToString();//
            if (dr["PER_TIPO_PERSONA"].ToString()!="")
                this.cboTipoContribuyente.SelectedValue = dr["PER_TIPO_PERSONA"].ToString();
            this.cboTipoDocumentoRepresentante.SelectedValue = dr["TID_ID"].ToString();         
        }

    }
    private void cargarControles()
    {        
        cargarDatasetFicha1();
        if (this.dsficha1.Tables[0].Rows.Count >0 )
        {
            DataRow dr = dsficha1.Tables[0].NewRow();
            dr = dsficha1.Tables[0].Rows[0];
            this.txtActividadCamaraComercio.Text = dr["EDE_ACTIVIDAD_CAMARA_COMERCIO"].ToString ();
            this.txtApellidoRepresentante.Text = dr["EDE_APELLIDO_REP_LEGAL"].ToString();
            this.txtDireccion.Text = dr["EDE_DIRECCION"].ToString();
            this.txtDV.Text = dr["EDE_DV"].ToString();
            this.txtFax.Text = dr["EDE_FAX"].ToString();
            this.txtNit.Text = dr["EDE_NIT_EMPRESA"].ToString();
            this.txtNoDocRepresentante.Text = dr["EDE_NO_DOC_IDENTIDAD"].ToString();
            this.txtNoMatriculaCamaraComercio.Text = dr["EDE_NO_MAT_CAMARA_COMERCIO"].ToString();
            this.txtNombreEmpresa.Text = dr["EDE_NOMBRE_EMPRESA"].ToString();
            this.txtNombreRepresentante.Text = dr["EDE_NOMBRE_REP_LEGAL"].ToString();
            this.txtNoRegCamaraComercio.Text = dr["EDE_NO_REG_CAMARA_COMERCIO"].ToString();
            this.txtTelefono.Text = dr["EDE_TELEFONO"].ToString();
            this.cboDepartamento.SelectedValue = dr["DEP_ID"].ToString();
            if (dr["DEP_ID"].ToString()!="")
                cargarComboMunicipio();
            if (dr["MUN_ID"].ToString()!="")
                this.cboMunicipio.SelectedValue = dr["MUN_ID"].ToString();
            this.cboTipoContribuyente.SelectedValue = dr["ETC_ID"].ToString();
            this.cboTipoDocumentoRepresentante.SelectedValue = dr["ETD_ID"].ToString();
        }

        



    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
           
        }
    }


    public void CargarTodaInfo()
    {
        cargarCombos();

        if (ValidacionToken())
        {
        //    Mensaje.MostrarMensaje(this.Page, "Usuario" + (string)Session["Usuario"]);
            this._usuarioRegistrado = (string)Session["Usuario"];
           // Mensaje.MostrarMensaje(this.Page, "Usuario" + this._usuarioRegistrado);
        }
        if (!string.IsNullOrEmpty(this._usuarioRegistrado))
        {
            this.PrecargaDatosPersona(this._usuarioRegistrado);
        }
        cargarControles();
    }
 
    private bool ValidacionToken()
    {
        //DESCOMENTAR ANTES DEL COMMIT!!!
        //Session["IDForToken"] = Request.QueryString["IdRelated"];
        //Session["IDForToken"] = "7";

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
    #region Métodos de inicialización
    protected void cargarCombos()
    {

        cargarComboTipoContribuyente();
        cargarComboTipoDocumento();
        cargarComboDepartamento();
     //  cargarComboMunicipio();
    }
    protected void cargarComboTipoContribuyente()
    {
        cboTipoContribuyente.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoContribuyente();
        cboTipoContribuyente.DataValueField = "ETC_ID";
        cboTipoContribuyente.DataTextField = "ETC_TIPO_CONTRIBUYENTE";
        cboTipoContribuyente.DataBind();
        cboTipoContribuyente.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarComboTipoDocumento()
    {
        cboTipoDocumentoRepresentante.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaTipoDocumento();
        cboTipoDocumentoRepresentante.DataValueField = "ETD_ID";
        cboTipoDocumentoRepresentante.DataTextField = "ETD_TIPO_DOCUMENTO";
        cboTipoDocumentoRepresentante.DataBind();
        cboTipoDocumentoRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarComboDepartamento()
    {
        cboMunicipio.DataSource=
        cboDepartamento.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaDepartamento();
        cboDepartamento.DataValueField = "DEP_ID";
        cboDepartamento.DataTextField = "DEP_NOMBRE";
        cboDepartamento.DataBind();
        cboDepartamento.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    protected void cargarComboMunicipio()
    {
        int idDepto = -1;
        int.TryParse(cboDepartamento.SelectedValue.ToString(), out idDepto);
        cboMunicipio.DataSource = SILPA.LogicaNegocio.ResumenEIA.Listas.ListaMunicipio(idDepto);
        cboMunicipio.DataValueField = "MUN_ID";
        cboMunicipio.DataTextField = "MUN_NOMBRE";
        cboMunicipio.DataBind();
        cboMunicipio.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }
    #endregion

    protected void cboTipoContribuyente_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboTipoContribuyente.SelectedItem.Text.Contains("Jurídica"))
        {
            phrInfoContJuridico.Visible = true;
            phrInfoCamara.Visible = true;
        }
        else
        {
            phrInfoContJuridico.Visible = false;
            phrInfoCamara.Visible = false;
        }
    }
    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarComboMunicipio();
    }    
    private void actualizarValores()
    {
        DataRow dr;
        cargarDatasetFicha1();
        if (this.dsficha1.Tables[0].Rows.Count > 0)
        {
            dr = dsficha1.Tables[0].Rows[0];
            DescargarCampos(dr);           
        }
        else
        {
            dr = dsficha1.Tables[0].NewRow();
            DescargarCampos(dr);
            this.dsficha1.Tables[0].Rows.Add(dr);

        }

        Contexto.guardarTabla(dsficha1, "EIH_DATOS_EMPRESA");
    }
    private void DescargarCampos(DataRow dr)
    {
        dr["EDE_ACTIVIDAD_CAMARA_COMERCIO"] = this.txtActividadCamaraComercio.Text;
        dr["EDE_APELLIDO_REP_LEGAL"] = this.txtApellidoRepresentante.Text;
        dr["EDE_DIRECCION"] = this.txtDireccion.Text;
        dr["EDE_DV"] = this.txtDV.Text;
        dr["EDE_FAX"] = this.txtFax.Text;
        dr["EDE_NIT_EMPRESA"] = this.txtNit.Text;
        dr["EDE_NO_DOC_IDENTIDAD"] = this.txtNoDocRepresentante.Text;
        dr["EDE_NO_MAT_CAMARA_COMERCIO"] = this.txtNoMatriculaCamaraComercio.Text;
        dr["EDE_NOMBRE_EMPRESA"] = this.txtNombreEmpresa.Text;
        dr["EDE_NOMBRE_REP_LEGAL"] = this.txtNombreRepresentante.Text;
        dr["EDE_NO_REG_CAMARA_COMERCIO"] = this.txtNoRegCamaraComercio.Text;
        dr["EDE_TELEFONO"] = this.txtTelefono.Text;
        dr["DEP_ID"] = this.cboDepartamento.SelectedValue;
        dr["MUN_ID"] = this.cboMunicipio.SelectedValue;
        dr["ETC_ID"] = this.cboTipoContribuyente.SelectedValue;
        dr["ETD_ID"] = this.cboTipoDocumentoRepresentante.SelectedValue;
        dr["EIP_ID"] = IDProyecto;
    }
    protected void cboDepartamento_DataBound(object sender, EventArgs e)
    {
        cargarComboMunicipio();
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        actualizarValores();
    }
}
