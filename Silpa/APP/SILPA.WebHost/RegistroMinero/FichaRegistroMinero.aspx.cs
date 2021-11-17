using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.AccesoDatos.RegistroMinero;
using System.Configuration;

public partial class RegistroMinero_FichaRegistroMinero : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaEditar();
        }
    }

    private void CargaEditar()
    {
        if (Request.QueryString["idreg"] == null)
        {
            return;
        }

        RegistroMineroIdentity RegistroMinero = new RegistroMineroIdentity(int.Parse(Request.QueryString["idreg"]));
        RegistroMinero.Consultar(true);

        LtipoRegistro.Text = RegistroMinero.NombreTipoRegistroMinero;
        LnroActoAdministrativo.Text = RegistroMinero.NroActoAdministrativo;
        LfechaActoAdministrativo.Text = RegistroMinero.FechaActoAdministrativo.Value.ToShortDateString();
        LnroExpediente.Text = RegistroMinero.NroExpediente;
        LautoridadAmbiental.Text = RegistroMinero.NombreAutoridadAmbiental;
        LcodRegistroMineria.Text = RegistroMinero.CodigoTituloMinero;
        LfechaExpiracion.Text = RegistroMinero.FechaExpiracion.Value.ToShortDateString();

        Lvigencia.Text = RegistroMinero.Vigencia.ToString();

        if (RegistroMinero.FechaVigencia != null)
        {
            Lvigencia.Text += " " + RegistroMinero.FechaVigencia.Value.ToShortDateString().ToString();
        }

        LEstado.Text = RegistroMinero.NombreEstado;
        LnombreProyecto.Text = RegistroMinero.NombreProyecto;
        Lareahectareas.Text = RegistroMinero.AreaHectareas.ToString();
        LnombreMina.Text = RegistroMinero.NombreMina;
        Lobservaciones.Text = RegistroMinero.Observaciones;
        Lnota.Text = "Nota de Responsabilidad: Los datos contenidos en este registro fueron ingresados por la Autoridada Ambiental " + RegistroMinero.NombreAutoridadAmbiental + ", por lo tanto la responsabilidad de la veracidad de la información es de esta Entidad; cualquier aclaración al respecto debe dirigirse directamente a la misma. la Ventanilla Interal de Tramites Ambientales en Línea, actúa como un mecanismo de consolidación.";
        Lcabecera.Text = " FICHA DESCRIPTIVA PARA " + RegistroMinero.NombreTipoRegistroMinero.ToUpper() + " DEL PROYECTO " + RegistroMinero.NombreProyecto.ToUpper() + ", MINA " + RegistroMinero.NombreMina.ToUpper();
        ViewState["Archivo"] = RegistroMinero.Archivo;

        ///Carga viñetas de operador
        Loperador.Text = "<ul>";
        foreach (TitularIdentity item in RegistroMinero.LstTitulares)
        {
            Loperador.Text += "<li>" + item.NombreTitular + " CC:" + item.Nroidentificacion + "</li>";
        }
        Loperador.Text += "<ul>";

        ///Carga viñetas de minerales
        Lminerales.Text = "<ul>";
        foreach (MineralIdentity item in RegistroMinero.LstMinerales)
        {
            Lminerales.Text += "<li>" + item.NombreMineral + "</li>";
        }
        Lminerales.Text += "<ul>";

        ///Carga viñetas de ubicación
        Lubicacion.Text = "<ul>";
        foreach (UbicacionIdentity item in RegistroMinero.LstUbicacion)
        {
            Lubicacion.Text += "<li>" + item.NombreDepartamento + " - " + item.NombreMunicipio + "</li>";
        }
        Lubicacion.Text += "<ul>";

        ///Carga viñetas de ubicación

        Llocalizaciones.Text = "<ul>";

        foreach (Localizacion item in RegistroMinero.LstLocalizaciones)
        {
            Llocalizaciones.Text += "<li>" + item.NombreLocalizacion;
            Llocalizaciones.Text += "<ul>";

            foreach (var icor in item.LstCoordenadas)
            {
                Llocalizaciones.Text += "<li>" + icor.CoordenadaNorte + " , " + icor.CoordenadaEste + "</li>";
            }

            Llocalizaciones.Text += "</ul>";
        }

        Llocalizaciones.Text += "</ul>";


        BitacoraRegistroMineroIdentity bitacoraRegistro = new BitacoraRegistroMineroIdentity();
        bitacoraRegistro.RegistroMineroID = RegistroMinero.RegistroMineroID;
        bitacoraRegistro.UsuarioID = Convert.ToInt32(Session["Usuario"]);
        bitacoraRegistro.Accion = "CONSULTAR";
        bitacoraRegistro.Descripcion = RegistroMinero.GetXml();
        bitacoraRegistro.AutoridadID = RegistroMinero.AutoridadAmbiental;
        bitacoraRegistro.Insertar();

    }
    protected void Bdescargar_Click(object sender, EventArgs e)
    {
        if ((string)ViewState["Archivo"].ToString() != "")
        {
            string Ruta = ConfigurationManager.AppSettings["REGISTRO_MINERO_ARCHIVOS"] + @"/Minero/";
            Response.Redirect(Ruta + ViewState["Archivo"].ToString());
        }
    }
}