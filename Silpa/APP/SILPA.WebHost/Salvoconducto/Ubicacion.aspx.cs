using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CargueSaldo_Ubicacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string puntos = Page.Request.QueryString["puntos"];
            puntos = SeteasPuntos(puntos);
            CrearMapa(puntos);
        }
    }

    private string SeteasPuntos(string puntos)
    {

        return puntos.Replace(",",".").Replace("N:", ",").Replace("E:", ",").Replace(" ","").Remove(0,1);
    }
    private void CrearMapa(string puntos)
    {
        JavaScriptSerializer serializar = new JavaScriptSerializer();
        string objeto = serializar.Serialize(puntos);
        this.hdCoordenadas.Value = objeto;

    }
}