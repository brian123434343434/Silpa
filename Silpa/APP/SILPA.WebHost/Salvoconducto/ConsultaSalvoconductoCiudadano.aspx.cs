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
using System.Configuration;
using System.Net;
using System.IO;

public partial class Salvoconducto_SeguiminetoRutasSalvoconducto : System.Web.UI.Page
{
    private int SalvoconductoID { get { return (int)ViewState["SalvoconductoID"]; } set { ViewState["SalvoconductoID"] = value; } }
    private int LogID { get { return (int)ViewState["LogID"]; } set { ViewState["LogID"] = value; } }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }



    protected void BtnValidarSalvoconducto_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (ValidarCaptcha())
            {
                SeguimientoRutaSalvoconducto clsSeguimientoSalvoconducto = new SeguimientoRutaSalvoconducto();
                List<SeguimientoRutaSalvoconductoIdentity> SeguimientoRutaSalvoconducto;
                SeguimientoRutaSalvoconducto = clsSeguimientoSalvoconducto.ValidarSalvoconducto(this.TxtNumeroSalvoconducto.Text, string.Empty, this.TxtDocumentoSolicitante.Text);

                this.BtnNuevaBusqueda.Visible = true;
                this.BtnValidarSalvoconducto.Visible = false;

                if (SeguimientoRutaSalvoconducto != null && SeguimientoRutaSalvoconducto.Count > 0)
                {
                    if (Convert.ToBoolean(SeguimientoRutaSalvoconducto[0].SalvoConductoID > 0))
                    {
                        SalvoconductoID = SeguimientoRutaSalvoconducto[0].SalvoConductoID;
                        string parametros = "SalvoConductoID=" + Convert.ToString(SalvoconductoID);
                        string query = Utilidades.Encrypt(parametros);
                        string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconductoWeb.aspx" + query;
                        Response.Redirect(queryEncriptado);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('El salvoconducto no Existe')</script>", false);
                        lblCaptchaMessage.Text = "";
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('El salvoconducto no Existe')</script>", false);
                    lblCaptchaMessage.Text = "";
                    return;
                }
            }
            else
            {
                lblCaptchaMessage.Text = "El Valor del Captcha no es Correcto";
            }
        }
    }


    //protected void grvEstadoSalvoconducto_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "VerSalvoconducto")
    //    {
    //        string parametros = "SalvoConductoID=" + e.CommandArgument.ToString();
    //        string query = Utilidades.Encrypt(parametros);
    //        string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
    //        string _strPagina = queryEncriptado;
    //        Response.Redirect(queryEncriptado);
    //        return;
    //    }
    //}

    public string urlNavegacionVerSalvoconducto(object salvID)
    {
        string parametros = "SalvoConductoID=" + Convert.ToString(salvID) + "&BloqueoSalvoConducto = false &SnConsultaCiudadano = true";
        string query = Utilidades.Encrypt(parametros);
        string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
        return queryEncriptado;
    }



    public bool ValidarCaptcha()
    {
        if (this.txtCodigoVerificacion.Text.ToLower() == Session["CaptchaVerify"].ToString())
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    protected void BtnNuevaBusqueda_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Salvoconducto/ConsultaSalvoconductoCiudadano.aspx");
    }
}