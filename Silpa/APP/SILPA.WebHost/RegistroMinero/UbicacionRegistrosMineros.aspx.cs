using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.AccesoDatos.RegistroMinero;
using System.Web.Script.Serialization;

public partial class RegistroMinero_UbicacionRegistrosMineros : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string registrosMineros = Request.QueryString["regmis"];

            int regminID = 0;

            List<object> lstListaLocalizaciones = new List<object>();
            List<object> lstListaNombres = new List<object>();
            string[] arryregistrosMineros = registrosMineros.Split(new char[] { ',' });
            JavaScriptSerializer serializar = new JavaScriptSerializer();
            serializar.MaxJsonLength = 500000000;
            foreach (string registroMineroID in arryregistrosMineros)
            {
                regminID = Convert.ToInt32(registroMineroID);
                RegistroMineroIdentity registroMinero = new RegistroMineroIdentity(regminID);
                registroMinero.Consultar(false);
                registroMinero.ConsultaLocalizaciones();

                Random numeroColor = new Random();
                string Color = String.Format("#{0:X6}", numeroColor.Next(0x1000000));
                string nombre = serializar.Serialize(registroMinero.NombreMina);
                string codigo = serializar.Serialize(registroMinero.CodigoTituloMinero);


                lstListaLocalizaciones.Add(new { localizaciones = registroMinero.LstLocalizaciones, color = Color, Nombre = registroMinero.NombreMina, Codigo = registroMinero.CodigoTituloMinero });
               

            }
             string objeto = serializar.Serialize(lstListaLocalizaciones);
             this.hdCoordenadas.Value = objeto;
            
    
        }
    }

    protected void btnAgrapunto_Click(object sender, EventArgs e)
    {
        double latidud = Convert.ToDouble(this.txtLatitud.Text);
        double longitud = Convert.ToDouble(this.txtLongitud.Text);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Coordenada", string.Format("agregarPunto('{0}','{1}');", latidud, longitud), true);
    }
}
