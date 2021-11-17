using SILPA.LogicaNegocio.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salvoconducto_PuntosControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region recibir parámetros de url
        string SalvoconductoID = "";
        string logID = "";
        if (Request.QueryString["enc"] != null && Request.QueryString["enc"].ToString() != "")
        {
            #region obtengo el valor de cada uno de los parámetros [modo: encriptado]
            string encryptedQueryString = Request.QueryString["enc"].Replace(" ", "+");
            string decryptedQueryString = Utilidades.Decrypt(encryptedQueryString);
            string[] QueryStringParameters = decryptedQueryString.Split(new char[] { '&' });

            SalvoconductoID = QueryStringParameters[0].Substring(QueryStringParameters[0].IndexOf("=") + 1);
            logID = QueryStringParameters[1].Substring(QueryStringParameters[1].IndexOf("=") + 1); 
            #endregion

            #region carga inro_odtial del formulario [modo: encriptado]
            if (!Page.IsPostBack)
            {
                //Session["Usuario"] = 32635;
                //CargarPagina(SalvoconductoID, logID);
                if (new Utilidades().ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {
                    if (!string.IsNullOrEmpty(SalvoconductoID) && !string.IsNullOrEmpty(logID))
                    {
                        CargarPagina(SalvoconductoID, logID);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "backPage", "history.back(); return false;", true);
                    }
                }
                #endregion
            }
        }
        else if (Request.QueryString["SalID"] != null && Request.QueryString["LogID"] != null)
        {
            #region carga inro_odtial del formulario [modo: normal]
            SalvoconductoID = Request.QueryString["SalID"].ToString();
            logID = Request.QueryString["LogID"].ToString(); 
            if (!Page.IsPostBack)
            {
                if (new Utilidades().ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {
                    if (!string.IsNullOrEmpty(SalvoconductoID) && !string.IsNullOrEmpty(logID))
                    {
                        CargarPagina(SalvoconductoID, logID);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "backPage", "history.back(); return false;", true);
                    }
                }
            }
            #endregion
            else
            {
                #region redirijo al usuario a la página de Login
                string _strPagina = "window.top.location.href='../Utilitario/MensajeValidacion.aspx'";
                Utilidades.AlertWindow(this.Page, "", _strPagina, (int)Utilidades._modo_apertura_formulario.ejecutarScript);
                #endregion
            }
        }
        #endregion


        
    }
    protected void btnAgrapunto_Click(object sender, EventArgs e)
    {
        //double latidud = Convert.ToDouble(this.txtLatitud.Text);
        //double longitud = Convert.ToDouble(this.txtLongitud.Text);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Coordenada", string.Format("agregarPunto('{0}','{1}');", latidud, longitud), true);
    }
    [System.Web.Services.WebMethod()]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GuardarPuntoControl(decimal lat, decimal lng, string count, string logID)
    {
        try
        {
            PuntoControl clsPuntoControl = new PuntoControl();
            clsPuntoControl.InsertarPuntoControl(Convert.ToInt32(logID), lat, lng, Convert.ToInt32(count) + 1);
        }
        catch (Exception ex)
        {

        }

        return "exito";
    }

    private void CargarPagina(string salvoID, string logID)
    {
        if (salvoID != null && salvoID != string.Empty)
        {
            List<object> lstListaPuntosControl = new List<object>();
            JavaScriptSerializer serializar = new JavaScriptSerializer();

            PuntoControl clsPuntoControl = new PuntoControl();
            var lstPuntosControl = clsPuntoControl.ListaPuntosControl(Convert.ToInt32(salvoID));
            string objeto = serializar.Serialize(lstPuntosControl);
            this.hdCoordenadas.Value = objeto;
            this.hdContadorPunto.Value = lstPuntosControl.Count.ToString();
            this.hdLogID.Value = logID;
        }
    }
}