using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Salvoconducto;
using SILPA.LogicaNegocio.Aprovechamiento;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Aprovechamiento;
using SILPA.LogicaNegocio.Generico;
using System.Data;

public partial class Salvoconducto_BuscarSalvoconducto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["Usuario"] = 429;
            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                CargarPagina();
            }
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
    public void CargarPagina()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        TipoSalvoconducto vTipoSalvoconducto = new TipoSalvoconducto();
        SalvoconductoNew vSalvoconductoNew = new SalvoconductoNew();
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], cboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        ClaseRecurso vClaseRecurso = new ClaseRecurso();
        Utilidades.LlenarComboLista(vTipoSalvoconducto.ListaTipoSalvoconducto(), cboTipoSalvoconducto, "TipoSalvoconducto", "TipoSalvoconductoID", true);
        Listas _listaTiposId = new Listas();
        DataSet _temp = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacion.DataSource = _temp.Tables[0]; // ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacion, "TID_NOMBRE", "TID_ID", true);
        Utilidades.LlenarComboLista(vSalvoconductoNew.ListaEstadoSalvoconducto(), cboEstado, "Estado", "EstadoID", true);
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Consultar();
    }
    public void Consultar()
    {
        int? intAutoridadId = null, intSolicitanteID = null, intEstadoID = null, intTipoSlavoconducto = null;
        SalvoconductoNew vSalvoconductoNew = new SalvoconductoNew();
        if (this.cboTipoIdentificacion.SelectedValue != string.Empty && this.txtNumeroIdentificacion.Text.Trim() != string.Empty)
        {
            PersonaDalc persona = new PersonaDalc();
            DataTable datos = persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text);
            if (datos.Rows.Count > 0)
            {
                intSolicitanteID = Convert.ToInt32(datos.Rows[0]["PER_ID"]);
            }
        }
        if (this.cboAutoridadAmbiental.SelectedValue != string.Empty)
            intAutoridadId = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue);
        if (this.cboEstado.SelectedValue != string.Empty)
            intEstadoID = Convert.ToInt32(this.cboEstado.SelectedValue);
        if (this.cboTipoSalvoconducto.SelectedValue != string.Empty)
            intTipoSlavoconducto = Convert.ToInt32(this.cboTipoSalvoconducto.SelectedValue);

        //this.grvSalvoconductos.DataSource = vSalvoconductoNew.ListaSalvoconducto(Convert.ToDateTime(this.txtFechaSolicitudDesde.Text), Convert.ToDateTime(this.txtFechaSolicitudHasta.Text), intAutoridadId, intSolicitanteID, intEstadoID, intTipoSlavoconducto, null);
        //this.grvSalvoconductos.DataBind();
    }
    protected void grvSalvoconductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvSalvoconductos.PageIndex = e.NewPageIndex;
        Consultar();
    }
}