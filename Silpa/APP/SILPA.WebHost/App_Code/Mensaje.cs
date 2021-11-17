using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using SoftManagement.Log;

/// <summary>
/// Summary description for Mensaje
/// </summary>
public class Mensaje
{
    /// <summary>
    /// Muestra mensaje al usuario
    /// </summary>
    public static void MostrarMensaje(Page page, string mensaje, bool cerrarVentana)
    {
        if (page.Master != null)
        {
            Label lblMensaje = (Label) page.Master.FindControl("lblMensaje");
            if (lblMensaje != null)
            {
                StringBuilder mensajeMostrarLabel = new StringBuilder(mensaje);
                mensajeMostrarLabel=mensajeMostrarLabel.Replace("\\n", "<br />");
                mensajeMostrarLabel = mensajeMostrarLabel.Replace(Environment.NewLine, "<br />");
                lblMensaje.Text = mensajeMostrarLabel.ToString();
            }
        }
        //Mostrar Mensaje
        StringBuilder mensajeMostrar = new StringBuilder(mensaje);
        mensajeMostrar.Replace(Environment.NewLine, "\\n");       
        mensajeMostrar.Replace("'", "");

        string _mensaje;
        if (!cerrarVentana)
        {
            _mensaje = "<script>alert('{0}.');</script>";
        }
        else
        {
            _mensaje = "<script>alert('{0}.');window.close();</script>";
        }

        page.ClientScript.RegisterStartupScript(page.GetType(), "MensajeAlert", String.Format(_mensaje, mensajeMostrar.ToString()));
    }

    public static void MostrarMensaje(Page page, string mensaje)
    {
        MostrarMensaje(page, mensaje, false);
    }

    /// <summary>
    /// Limpia label de mensaje al usuario
    /// </summary>
    public static void LimpiarMensaje(Page page)
    {
        if (page.Master != null)
        {
            Label lblMensaje = (Label)page.Master.FindControl("lblMensaje");
            if (lblMensaje != null)
                lblMensaje.Text = string.Empty;
        }
    }

    /// <summary>
    /// Muestra mensaje al usuario
    /// </summary>
    public static void ErrorCritico(Page page, Exception ex)
    {
        //muestra mensaje
        string mensaje = "Ha ocurrido un error en el sistema, comuníquese con el administrador";
        MostrarMensaje(page, mensaje);
    
        //Escribe en el log
        SMLog.Escribir(ex);    
    }




}
