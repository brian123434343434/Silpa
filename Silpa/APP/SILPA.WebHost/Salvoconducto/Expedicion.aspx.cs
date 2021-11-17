using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salvoconducto_Expedicion : System.Web.UI.Page
{
    #region ViewsState
    public int autoridadAmbientalUsuario { set { ViewState["autoridadAmbientalUsuario"] = value; } get { return (int)ViewState["autoridadAmbientalUsuario"]; } }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["Usuario"] = 32511;
            //CargarPagina();
            //return;
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
        PersonaDalc per = new PersonaDalc();
        PersonaIdentity p = new PersonaIdentity();
        int autID = 0;
        string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(Session["Usuario"].ToString()), out autID);
        //autID = 0;
        autoridadAmbientalUsuario = autID;
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], cboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        ClaseRecurso vClaseRecurso = new ClaseRecurso();
        Utilidades.LlenarComboLista(vClaseRecurso.ListaClaseRecurso(), cboClaseRecurso, "ClaseRecurso", "ClaseRecursoID", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentosPorAutoridadAmbiental(_configuracion.IdPaisPredeterminado, autID).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
        Utilidades.LlenarComboVacio(cboMunicipio);
        this.cboAutoridadAmbiental.SelectedValue = autID.ToString();
        this.cboAutoridadAmbiental.Enabled = false;
        
        

    }
    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDepartamento.SelectedValue == "")
        {
            Utilidades.LlenarComboVacio(cboMunicipio);
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, cboMunicipio, "MUN_NOMBRE", "MUN_ID", true);
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Consultar();
    }
    private void Consultar()
    {
        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        DateTime? dtFechaInicio = null, dtFechaFin = null;
        int? intClaseRecursoID = null;
        if (this.cboClaseRecurso.SelectedValue != "")
            intClaseRecursoID = Convert.ToInt32(this.cboClaseRecurso.SelectedValue);
        if (this.txtFechaSolicitudDesde.Text != string.Empty)
            dtFechaInicio = Convert.ToDateTime(this.txtFechaSolicitudDesde.Text.Trim() + " 00:00:00");
        if (this.txtFechaSolicitudHasta.Text != string.Empty)
            dtFechaFin = Convert.ToDateTime(this.txtFechaSolicitudHasta.Text.Trim() + " 23:59:59");
        var lstSolicitudSalvoconductos = clsSalvoconductoNew.ListaSalvoconducto(dtFechaInicio, dtFechaFin, Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue), null, 1, null, intClaseRecursoID, autoridadAmbientalUsuario, null);
        if (this.cboDepartamento.SelectedValue != string.Empty)
            lstSolicitudSalvoconductos = lstSolicitudSalvoconductos.Where(x => x.DepartamentoOrigenID == Convert.ToInt32(this.cboDepartamento.SelectedValue)).ToList();
        if (this.cboMunicipio.SelectedValue != string.Empty)
            lstSolicitudSalvoconductos = lstSolicitudSalvoconductos.Where(x => x.MunicipioOrigenID == Convert.ToInt32(this.cboMunicipio.SelectedValue)).ToList();
        this.grvSalvoconductos.DataSource = lstSolicitudSalvoconductos;
        this.grvSalvoconductos.DataBind();
    }
    protected void grvSalvoconductos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }


 

    public string urlNavegacion(object salvID)
    {
        string parametros = "SalvoConductoID=" + Convert.ToString(salvID);
        string query = Utilidades.Encrypt(parametros);
        string queryEncriptado = "../Salvoconducto/AprobacionSalvoconducto.aspx" + query;
        return queryEncriptado;
    }

    protected void lnkVerSalvoconducto_Click(object sender, EventArgs e)
    {

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
    protected void grvSalvoconductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvSalvoconductos.PageIndex = e.NewPageIndex;
        Consultar();
    }
}