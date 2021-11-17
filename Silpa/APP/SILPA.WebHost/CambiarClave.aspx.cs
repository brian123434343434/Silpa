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
using System.Text.RegularExpressions;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using SoftManagement.Log;

public partial class CambiarClave : System.Web.UI.Page
{
    private Persona objPersona;

    protected void Page_Load(object sender, EventArgs e)
    {

        Mensaje.LimpiarMensaje(this);
       
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Regex reg = new Regex(@"^.*(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d\W]).*$");
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAceptar_Click.Inicio");
            if (reg.IsMatch(txtNewClave.Text))
            {
                objPersona = new Persona();

                int idError = 0;
                string msg = objPersona.CambiarClave(txtUsuario.Text, EnDecript.Encriptar(txtClave.Text), EnDecript.Encriptar(txtNewClave.Text), ref idError);
                //lblError.Text = objPersona.CambiarClave(txtUsuario.Text, EnDecript.Encriptar(txtNewClave.Text));


                if (idError == 0)
                {

                    ParametroDalc parametroDalc = new ParametroDalc();
                    ParametroEntity parametro = new ParametroEntity();
                    parametro.IdParametro = -1;
                    parametro.NombreParametro = "login_silpa";
                    parametroDalc.obtenerParametros(ref parametro);
                    Mensaje.MostrarMensaje(this, msg);

                    string strScript = "<script language='JavaScript'>" +
                        "window.location = '" + parametro.Parametro + "'" +
                        "</script>";
                    Page.RegisterStartupScript("PopupScript", strScript);

                    //Response.Redirect(parametro.Parametro);
                }
                else
                {
                    Mensaje.MostrarMensaje(this, msg);
                    this.LimpiarControles();
                }
            }
            else
            {
                Mensaje.MostrarMensaje(this, "La contraseña: debe ser mínimo de 8 caracteres, alfanumérico con altas - bajas y  almenos un caracter especial");
                this.LimpiarControles();
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAceptar_Click.Finalizo");
        }

    }

    /// <summary>
    /// Método que limpia la pantalla
    /// </summary>
    protected void LimpiarControles() 
    {
        this.txtClave.Text = string.Empty;
        this.txtCofirmClave.Text = string.Empty;
        this.txtNewClave.Text = string.Empty;
        this.txtUsuario.Text = string.Empty;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.LimpiarControles();
        ParametroDalc parametroDalc = new ParametroDalc();
        ParametroEntity parametro = new ParametroEntity();
        parametro.IdParametro = -1;
        parametro.NombreParametro = "login_silpa";
        parametroDalc.obtenerParametros(ref parametro);
        Response.Redirect(parametro.Parametro);
    }
}
