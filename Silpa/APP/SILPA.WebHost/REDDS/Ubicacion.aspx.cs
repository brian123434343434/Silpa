using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.AccesoDatos.RegistroMinero;
using System.Web.Script.Serialization;
using SILPA.LogicaNegocio.REDDS;

public partial class RegistroMinero_UbicacionRegistrosMineros : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            int ReddID = Convert.ToInt32(Request.QueryString["ReddID"]);

            List<object> lstListaLocalizaciones = new List<object>();
            List<object> lstListaNombres = new List<object>();
            JavaScriptSerializer serializar = new JavaScriptSerializer();

            Redds clsRedds = new Redds();
            var registroRedds = clsRedds.ConsultaRegistroREDDNumeroVital(null, false, ReddID);
            registroRedds.LstLocalizacion = clsRedds.ConsultaLocalizaciones(ReddID);
            Random numeroColor = new Random();
            string Color = String.Format("#{0:X6}", numeroColor.Next(0x1000000));


            lstListaLocalizaciones.Add(new { localizaciones = registroRedds.LstLocalizacion, color = Color });

             string objeto = serializar.Serialize(lstListaLocalizaciones);
             this.hdCoordenadas.Value = objeto;
            
    
        }
    }
}
