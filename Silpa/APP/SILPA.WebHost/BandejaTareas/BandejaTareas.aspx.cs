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
using SILPA.LogicaNegocio.Generico;
using Silpa.Workflow.AccesoDatos;
using Silpa.Workflow;
using SoftManagement.Log;
using System.Collections.Generic;
using SILPA.Comun;
//using Gattaca.Application.eSecurity;


public partial class BandejaTareas_BandejaTareas : System.Web.UI.Page
{
    private const string urlAutoridadAmbiental = "InternalParticipant/WorkItem.aspx?IDActivityInstance={0}";
    private const string urlSolicitante = "Public/buildFormInstance.aspx?IdProcessInstance={0}&IdActivityInstance={1}&UserID={2}&Case={3}&Form={4}&EntryDataType={5}&EntryData={6}&IdEntryData={7}&ID={8}&op=&IdUser={9}&IdRelated={10}&ClientName={11}&URL={12}";

    public static string URL_TESTSILPA = System.Configuration.ConfigurationManager.AppSettings["URL_TESTSILPA"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //VALIDAMOS SI EL USUARIO QUE ESTA INGRESANDO PERTENECE AL PROYECTO DE PINES
            List<string> lstEmpresas = ApplicationUserDao.ObtenerEmpresaUsuario(DatosSesion.Usuario);            

            if (lstEmpresas.Contains(ConfigurationManager.AppSettings["CODE_PINES"].ToString())) // si es un proyecto de pines se redirecciona a la badeja de proyectos para pines ya que el tratamiento de las actividades es distinto
            {
                Response.Redirect("~/BandejaTareas/BandejaTareasPINES.aspx");
            }

            CargarTipoTramite();
            ConsultarTareasSinIniciar();
        }
    }

    private void CargarTipoTramite()
    {
        try
        {            
            Listas _listaTipoTramite = new Listas();
            DataSet _temp = _listaTipoTramite.ListarTipoTramite(null);

            cboTipoTramiteF.DataSource = _temp.Tables[0];
            cboTipoTramiteF.DataTextField = "NOMBRE_TIPO_TRAMITE";
            cboTipoTramiteF.DataValueField = "ID"; //ID_TIPO_PROCESO
            cboTipoTramiteF.DataBind();
            cboTipoTramiteF.Items.Insert(0, new ListItem("(Seleccione)", ""));


            cboTipoTramite.DataSource = _temp.Tables[0];
            cboTipoTramite.DataTextField = "NOMBRE_TIPO_TRAMITE";
            cboTipoTramite.DataValueField = "ID"; //ID_TIPO_PROCESO
            cboTipoTramite.DataBind();
            cboTipoTramite.Items.Insert(0, new ListItem("(Seleccione)", ""));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ex);
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }      
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarTareasSinIniciar();
    }

    protected void btnBuscarFinalizadas_Click(object sender, EventArgs e)
    {
        ConsultarTareasFinalizadas();
    }

    private void ConsultarTareasSinIniciar()
    {
        try
        {            
            int? tipoTramite = null;
            DateTime? fechaInicio = null;
            DateTime? fechaFinal = null;

            if (cboTipoTramite.SelectedValue != "")
                tipoTramite = Convert.ToInt32(cboTipoTramite.SelectedValue);
            if (this.txtFechaInicial.Text != "")
                fechaInicio = Convert.ToDateTime(this.txtFechaInicial.Text);
            if (this.txtFechaFinal.Text != "")
                fechaFinal = Convert.ToDateTime(this.txtFechaFinal.Text);
            
            this.gdvTareasSinIniciar.DataSource = BandejaTareasDao.ConsultarTareasSinIniciar(DatosSesion.Usuario, this.txtNumeroVital.Text, this.txtNumeroExpediente.Text, tipoTramite, fechaInicio, fechaFinal);            
            this.gdvTareasSinIniciar.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico,Page.AppRelativeVirtualPath.ToString() +ex);
            Mensaje.MostrarMensaje(this.Page,"Ha ocurrido un error comuniquese con el administrador");
        }        
    }

    private void ConsultarTareasFinalizadas()
    {
        try
        {            
            int? tipoTramite = null;
            DateTime? fechaInicio = null;
            DateTime? fechaFinal = null;

            if (cboTipoTramiteF.SelectedValue != "")
                tipoTramite = Convert.ToInt32(cboTipoTramiteF.SelectedValue);
            if (this.txtFechaInicialF.Text != "")
                fechaInicio = Convert.ToDateTime(this.txtFechaInicialF.Text);
            if (this.txtFechaFinalF.Text != "")
                fechaFinal = Convert.ToDateTime(this.txtFechaFinalF.Text);
            this.gdvTareasFinalizadas.DataSource = BandejaTareasDao.ConsultarTareasFinalizadas(DatosSesion.Usuario, this.txtNumeroVitalF.Text, this.txtNumeroExpedienteF.Text, tipoTramite, fechaInicio, fechaFinal);            
            this.gdvTareasFinalizadas.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ex);
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }        
    }

    protected void gdvTareasSinIniciar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gdvTareasSinIniciar.PageIndex = e.NewPageIndex;
        this.ConsultarTareasSinIniciar();
        this.tbcContenedor.ActiveTab = this.tabTareasSinIniciar;
    }

    protected void gdvTareasFinalizadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gdvTareasFinalizadas.PageIndex = e.NewPageIndex;
        this.ConsultarTareasFinalizadas();
        this.tbcContenedor.ActiveTab = this.tabTareasFinalizadas;


    }
    
    protected void gdvTareasSinIniciar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblUrlDataView;
        Label lblEntryDataType;
        HyperLink hypAutoridadAmbiental;
        Label lblActivityInstance;
        Label lblModeloDistribucion;
        Label lblProcessInstance;
        Label lblEntryDataTypeProcess;
        Label lblEntryData;
        Label lblIdEntryData;
        Label lblIdRelated;
        Label lblIdProcess;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView objDataRow = null;

            if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
            {
                //Cargar datos de la fila
                objDataRow = (DataRowView)e.Row.DataItem;

                lblUrlDataView = (Label)e.Row.FindControl("lblUrlDataView");
                lblEntryDataType = (Label)e.Row.FindControl("lblEntryDataType");
                lblActivityInstance = (Label)e.Row.FindControl("lblActivityInstance");
                hypAutoridadAmbiental = (HyperLink)e.Row.FindControl("hypAutoridadAmbiental");
                lblModeloDistribucion = (Label)e.Row.FindControl("lblModeloDistribucion");
                lblProcessInstance = (Label)e.Row.FindControl("lblProcessInstance");
                lblEntryDataTypeProcess = (Label)e.Row.FindControl("lblEntryDataTypeProcess");
                lblEntryData = (Label)e.Row.FindControl("lblEntryData");
                lblIdEntryData = (Label)e.Row.FindControl("lblIdEntryData");
                lblIdRelated = (Label)e.Row.FindControl("lblIdRelated");
                lblIdProcess = (Label)e.Row.FindControl("lblIdProcess");

                //Verificar si el la actividad es de prueba dinamica
                if (!string.IsNullOrWhiteSpace(objDataRow["NUMERO_VITAL"].ToString()) && objDataRow["NUMERO_VITAL"].ToString().Trim().StartsWith(ConfigurationManager.AppSettings["PDVTramite"].ToString()))
                {
                    //Verificar si se obtuvo URL
                    if (Convert.ToInt32(objDataRow["IDActivity"]) == Convert.ToInt32(ConfigurationManager.AppSettings["PDVOficioRetorno"].ToString()))
                    {
                        hypAutoridadAmbiental.Enabled = false;
                        hypAutoridadAmbiental.NavigateUrl = "";
                        hypAutoridadAmbiental.Target = "";
                    }
                    else
                    {
                        //Obtener link
                        hypAutoridadAmbiental.NavigateUrl = ObtenerURLSinIniciar(Convert.ToInt32(lblProcessInstance.Text), Convert.ToInt32(lblActivityInstance.Text), Convert.ToInt32(lblModeloDistribucion.Text), lblEntryDataType.Text, lblUrlDataView.Text, lblEntryDataTypeProcess.Text, lblEntryData.Text, lblIdEntryData.Text, Convert.ToInt32(lblIdRelated.Text), Convert.ToInt32(lblIdProcess.Text), hypAutoridadAmbiental);
                    }
                }
                else
                {
                    hypAutoridadAmbiental.NavigateUrl = ObtenerURLSinIniciar(Convert.ToInt32(lblProcessInstance.Text), Convert.ToInt32(lblActivityInstance.Text), Convert.ToInt32(lblModeloDistribucion.Text), lblEntryDataType.Text, lblUrlDataView.Text, lblEntryDataTypeProcess.Text, lblEntryData.Text, lblIdEntryData.Text, Convert.ToInt32(lblIdRelated.Text), Convert.ToInt32(lblIdProcess.Text), hypAutoridadAmbiental);
                }
                
            }
        }
    }


    public string ObtenerURLSinIniciar(int idProcessInstance, int idActivityInstance, int modeloAsignacion, string entryDataType, string url, string entryDataTypeProcess, string entryData, string idEntryData, int idRelated, int idProcessId, HyperLink hypDetalle)
    {
        try
        {            
            string parametros;                        
            long userId = ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario);            

            //Verificar si el proceso es una solicitud para anexar informacion de solicitud y es prueba dinamica                        
            hypDetalle.Target = "";
            switch (entryDataType.ToUpper())
            {
                case "WEBFORM": // Es un formulario personalizado.
                    hypDetalle.Target = "_blank";
                    parametros = "IDProcessInstance={0}&IDActivityInstance={1}&EntryDataType={2}&EntryData={3}&IDEntryData={4}&IdRelated={5}";
                    return url + String.Format(parametros, idProcessInstance, idActivityInstance, entryDataTypeProcess, "", idEntryData, idRelated);
                    break;
                case "VBFORMBUILDERCOM": // Es un formulario de Gattaca
                    if (modeloAsignacion == 1) // De forma automática a todos los participantes asignados (AA)
                        return ConfigurationManager.AppSettings["URL_BPM"] + string.Format(urlAutoridadAmbiental, idActivityInstance);
                    else // De forma automática al usuario que inició el proceso
                    {
                        //hypDetalle.Target = "_blank";
                        //return ConfigurationManager.AppSettings["URL_FORMBUILDER"] + string.Format(urlSolicitante, idProcessInstance, idActivityInstance, userId, idProcessId, entryData, BandejaTareasConfig.EntryDataType, BandejaTareasConfig.EntryData, BandejaTareasConfig.IDEntryData, BandejaTareasConfig.ID, userId, idRelated, ServicioWorkflowConfig.Cliente, Request.Url);
                        return ConfigurationManager.AppSettings["URL_BPM"] + string.Format(urlAutoridadAmbiental, idActivityInstance);
                    }
                    break;
                default:
                    return ConfigurationManager.AppSettings["URL_BPM"] + string.Format(urlAutoridadAmbiental, idActivityInstance);

            }            
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ex);
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
            return "";
        }        
    }

    public string ObtenerURLFinalizadas(int idProcessInstance, int idActivityInstance, int modeloAsignacion, string entryDataType, string url, string entryDataTypeProcess, string entryData, string idEntryData, int idRelated, int idProcessId, HyperLink hypDetalle)
    {
        try
        {            
            string parametros;
            long userId = ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario);
            hypDetalle.Target = "";

            switch (entryDataType.ToUpper())
            {
                case "WEBFORM": // Es un formulario personalizado.
                    hypDetalle.Target = "_blank";
                    parametros = "IDProcessInstance={0}&IDActivityInstance={1}&EntryDataType={2}&EntryData={3}&IDEntryData={4}&IdRelated={5}";
                    return url + String.Format(parametros, idProcessInstance, idActivityInstance, entryDataTypeProcess, entryData, idEntryData, idRelated);
                    break;
                case "VBFORMBUILDERCOM": // Es un formulario de Gattaca
                    {
                        return ConfigurationManager.AppSettings["URL_BPM"] + string.Format(urlAutoridadAmbiental, idActivityInstance);
                    }
                //if (modeloAsignacion == 1) // De forma automática a todos los participantes asignados (AA)
                //    return ConfigurationManager.AppSettings["URL_BPM"] + string.Format(urlAutoridadAmbiental, idActivityInstance);
                //else // De forma automática al usuario que inició el proceso
                //{
                //    return ConfigurationManager.AppSettings["URL_FORMBUILDER"] + string.Format(urlSolicitante, idProcessInstance, idActivityInstance, userId, idProcessId, entryData, BandejaTareasConfig.EntryDataType, BandejaTareasConfig.EntryData, BandejaTareasConfig.IDEntryData, BandejaTareasConfig.ID, userId, idRelated, ServicioWorkflowConfig.Cliente, Request.Url);
                //}
                //break;
                default:
                    return ConfigurationManager.AppSettings["URL_BPM"] + string.Format(urlAutoridadAmbiental, idActivityInstance);

            }
        }        
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ex);
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
            return "";
        }      
    }
             
   
    protected void gdvTareasFinalizadas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblUrlDataView;
        Label lblEntryDataType;
        HyperLink hypAutoridadAmbiental;
        Label lblActivityInstance;
        Label lblModeloDistribucion;
        Label lblProcessInstance;
        Label lblEntryDataTypeProcess;
        Label lblEntryData;
        Label lblIdEntryData;
        Label lblIdRelated;
        Label lblIdProcess;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
                {
                    lblUrlDataView = (Label)e.Row.FindControl("lblUrlDataView");
                    lblEntryDataType = (Label)e.Row.FindControl("lblEntryDataType");
                    lblActivityInstance = (Label)e.Row.FindControl("lblActivityInstance");
                    hypAutoridadAmbiental = (HyperLink)e.Row.FindControl("hypAutoridadAmbiental");
                    lblModeloDistribucion = (Label)e.Row.FindControl("lblModeloDistribucion");
                    lblProcessInstance = (Label)e.Row.FindControl("lblProcessInstance");
                    lblEntryDataTypeProcess = (Label)e.Row.FindControl("lblEntryDataTypeProcess");
                    lblEntryData = (Label)e.Row.FindControl("lblEntryData");
                    lblIdEntryData = (Label)e.Row.FindControl("lblIdEntryData");
                    lblIdRelated = (Label)e.Row.FindControl("lblIdRelated");
                    lblIdProcess = (Label)e.Row.FindControl("lblIdProcess");

                    hypAutoridadAmbiental.NavigateUrl = ObtenerURLFinalizadas(Convert.ToInt32(lblProcessInstance.Text), Convert.ToInt32(lblActivityInstance.Text), Convert.ToInt32(lblModeloDistribucion.Text), lblEntryDataType.Text, lblUrlDataView.Text, lblEntryDataTypeProcess.Text, lblEntryData.Text, lblIdEntryData.Text, Convert.ToInt32(lblIdRelated.Text), Convert.ToInt32(lblIdProcess.Text), hypAutoridadAmbiental);
                    //<asp:HyperLink ID="hypAutoridadAmbiental" runat="server" Text='<%# Bind("TAREA") %>' Target="_blank" NavigateUrl = <%# ObtenerURL(Convert.ToInt32( Eval("IDACTIVITY_INSTANCE")), Convert.ToInt32(Eval("distributionmode"))) %> >HyperLink</asp:HyperLink>

                }
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ex);
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        
        }       
    }
    //protected void Button1_Click1(object sender, EventArgs e)
    //{
    //    this.TextBox13.Text = SILPA.Comun.EnDecript.Desencriptar("b5cdab51c66a2e6e763487db44f9e8c525f2079c991f778c");

    //}

    /// <summary>
    /// evento que me permite regresar a la página anteriormente navegada
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnPreviousPage_Click(object sender, ImageClickEventArgs e)
    {
        #region armar Ulr para retornar a la págnina Home de TestSilpa
        //// Url: http://licencias.anla.gov.co/XCARS/SILPA/TESTSILPA/Security/default.aspx
        ////string v_prevPage = @"SILPA/TESTSILPA/Security/default.aspx";
        //string v_prevPage = URL_TESTSILPA;  //@"BandejaTareas/Test_ACHM.aspx";
        //string v_Dominio = HttpContext.Current.Request.Url.Authority;   // http://licencias.anla.gov.co
        //string v_UrlAbsoluta = HttpContext.Current.Request.Url.AbsoluteUri;
        //string[] v_UrlSeparado = v_UrlAbsoluta.Split('/');
        //#region validar que el dominio en donde se esté ejecutando el sistema no sea local
        //string v_UrlVirtualDirectory = string.Empty;
        //if (!v_UrlAbsoluta.Contains("localhost"))
        //{
        //    v_UrlVirtualDirectory = v_UrlSeparado[3] + @"/";
        //}
        //#endregion
        ////string v_Url = v_Dominio + v_UrlVirtualDirectory + v_prevPage;
        //string v_Url = @"../" + v_UrlVirtualDirectory + v_prevPage;

        string v_Url = URL_TESTSILPA;
        Response.Redirect(v_Url);
        #endregion
    }
}
