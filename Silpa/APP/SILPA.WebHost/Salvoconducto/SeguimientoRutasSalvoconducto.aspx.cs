using SILPA.AccesoDatos.Salvoconducto;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
public partial class Salvoconducto_SeguiminetoRutasSalvoconducto : System.Web.UI.Page
{
    private int SalvoconductoID { get { return (int)ViewState["SalvoconductoID"]; } set { ViewState["SalvoconductoID"] = value; } }
    private int LogID { get { return (int)ViewState["LogID"]; } set { ViewState["LogID"] = value; } }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["Usuario"] = 32635;
            //CargarPagina();
            if (new Utilidades().ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                CargarPagina();
            }
        }
    }

    public void CargarPagina()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], CboDptoUbicacion, "DEP_NOMBRE", "DEP_ID", true);
        Utilidades.LlenarComboVacio(this.CboMunicipioUbicacion);
    }

    protected void BtnValidarSalvoconducto_Click(object sender, EventArgs e)
    {
        LogID = 0; 
        SalvoconductoID = 0;
        SeguimientoRutaSalvoconducto clsSeguimientoSalvoconducto = new SeguimientoRutaSalvoconducto();
        List<SeguimientoRutaSalvoconductoIdentity> SeguimientoRutaSalvoconducto;
        SeguimientoRutaSalvoconducto = clsSeguimientoSalvoconducto.ValidarSalvoconducto(this.TxtNumeroSalvoconducto.Text, this.TxtCodigoSeguridad.Text);
        //if (Convert.ToBoolean(SeguimientoRutaSalvoconducto[0].Sn_Error))
        //{
        //    this.grvEstadoSalvoconducto.Columns[6].Visible = false;
        //}
        //else
        //{
        //    this.grvEstadoSalvoconducto.Columns[6].Visible = true;
        //}
        if (Convert.ToBoolean(SeguimientoRutaSalvoconducto[0].SalvoConductoID > 0))
        {
            SalvoconductoID = SeguimientoRutaSalvoconducto[0].SalvoConductoID;
            LogID = clsSeguimientoSalvoconducto.GrabarLogConsulta(SalvoconductoID, Convert.ToInt32(CboDptoUbicacion.SelectedValue), Convert.ToInt32(CboMunicipioUbicacion.SelectedValue), this.TxtNombreRevisor.Text, this.TxtNumeroIdentificacionRevisor.Text, Convert.ToInt32(Session["Usuario"]));
        }
        

        grvEstadoSalvoconducto.DataSource = SeguimientoRutaSalvoconducto;
        grvEstadoSalvoconducto.DataBind();
    }


    protected void grvEstadoSalvoconducto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerSalvoconducto")
        {
            string parametros = "SalvoConductoID=" + e.CommandArgument.ToString();
            string query = Utilidades.Encrypt(parametros);
            string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
            string _strPagina = queryEncriptado;
            Response.Redirect(queryEncriptado);
            return;
        }
    }

    public string urlNavegacionVerSalvoconducto(object salvID)
    {
        string parametros = "SalvoConductoID=" + Convert.ToString(salvID) + "&BloqueoSalvoConducto = true ";
        string query = Utilidades.Encrypt(parametros);
        string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
        return queryEncriptado;
    }

    public string urlNavegacionAgregarPtoControl(object salvID)
    {
        string parametros = "SalID=" + Convert.ToString(salvID) + "&LogID = " + Convert.ToString(LogID);
        string query = Utilidades.Encrypt(parametros);
        string queryEncriptado = "../Salvoconducto/PuntosControl.aspx" + query;
        return queryEncriptado;
    }

    protected void CboDptoUbicacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CboDptoUbicacion.SelectedValue == "" || CboDptoUbicacion.SelectedValue == "-1")
        {
            CboMunicipioUbicacion.Items.Clear();
            CboMunicipioUbicacion.Items.Insert(0, new ListItem("Seleccione..", ""));
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(CboDptoUbicacion.SelectedValue), null).Tables[0];
            if (dtMunicipios.Rows.Count > 0)
            {
                dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            }
            Utilidades.LlenarComboTabla(dtMunicipios, CboMunicipioUbicacion, "MUN_NOMBRE", "MUN_ID", false);
        }
    }



    protected void grvEstadoSalvoconducto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (SalvoconductoID == 0)
            {
                HtmlTable tblComandos = (HtmlTable)e.Row.FindControl("tblComandos");
                tblComandos.Attributes.Add("style", "display:none");
            }
        }
    }
}