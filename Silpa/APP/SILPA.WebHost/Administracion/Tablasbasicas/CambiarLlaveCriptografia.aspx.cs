using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Usuario;


public partial class Administracion_Tablasbasicas_CambiarLlaveCriptografia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConsultaLlaveActual();
        }
    }
    private string Llave
    {
        set { ViewState["Llave"] = value; }
        get { return (string)ViewState["Llave"]; }
    }
    protected void btnGuardar_Click(Object sender, EventArgs e)
    {
        if (IsValid)
        {

            ParametroDalc parametroDalc = new ParametroDalc();
            ParametroEntity parametro = new ParametroEntity();
            parametro.IdParametro = 38;
            parametro.NombreParametro = "-1";
            parametroDalc.obtenerParametros(ref parametro);
            parametro.Parametro = this.txtNewLlave.Text;
            VolveraGenerarContrasenas(this.txtNewLlave.Text);
            parametroDalc.actualizarParametro(parametro);
            ConsultaLlaveActual();
            this.txtNewLlave.Text = "";
        }
    }
    private void VolveraGenerarContrasenas(string newLlave)
    {
        UsuarioDalc dalc = new UsuarioDalc();
        dalc.VolverAGenerarContrasenasEncriptadas(newLlave);
    }
    private void ConsultaLlaveActual()
    {
        ParametroDalc parametroDalc = new ParametroDalc();
        ParametroEntity parametro = new ParametroEntity();
        parametro.IdParametro = 38;
        parametro.NombreParametro = "-1";
        parametroDalc.obtenerParametros(ref parametro);
        this.txtLlaveActuall.Text = parametro.Parametro;
        this.Llave = parametro.Parametro;
    }
}
