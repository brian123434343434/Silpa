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
using SILPA.AccesoDatos.Generico;

public partial class ReporteTramite_Mapa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {


            GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

            GoogleMapForASPNet1.GoogleMapObject.Width = "800px";
            GoogleMapForASPNet1.GoogleMapObject.Height = "600px";

            GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 16;

            string numeroVital=Page.Request.QueryString["nVit"];
            string numeroExpediente = Page.Request.QueryString["cExp"];

            NumeroSilpaDalc numSilpa = new NumeroSilpaDalc();
            DataSet infExpediente = numSilpa.ConsultarCoordenadas(numeroVital,numeroExpediente);

            int i = 1;
            int punto_unico = 0;
            foreach (DataRow row in infExpediente.Tables[0].Rows)
            {
                punto_unico = int.Parse(row["id_punto"].ToString());
                string nombrePunto = row["nombre"].ToString();
                DataRow[] coordenadas = infExpediente.Tables[0].Select("id_punto=" + punto_unico);

                if (coordenadas.Length == 1)
                {
                    if (i==1)
                        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", double.Parse(coordenadas[0]["Norte"].ToString()), double.Parse(coordenadas[0]["Este"].ToString()));

                    GooglePoint GP1 = new GooglePoint();
                    GP1.ID = "GP" + i.ToString();
                    i++;
                    GP1.Latitude = double.Parse(coordenadas[0]["Norte"].ToString());                    
                    GP1.Longitude = double.Parse(coordenadas[0]["Este"].ToString());
                    GP1.ToolTip = nombrePunto;
                    GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
                }
                else if (coordenadas.Length == 2)
                {
                    if (i==1)
                        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", double.Parse(coordenadas[0]["Norte"].ToString()), double.Parse(coordenadas[0]["Este"].ToString()));

                    GooglePoint GP1 = new GooglePoint();
                    GP1.ID = "GP" + i.ToString();
                    i++;
                    GP1.Latitude = double.Parse(coordenadas[0]["Norte"].ToString());
                    GP1.Longitude = double.Parse(coordenadas[0]["Este"].ToString());
                    GP1.ToolTip = nombrePunto;
                    GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);

                    GooglePoint GP2 = new GooglePoint();
                    GP2.ID = "GP"+i.ToString();
                    GP2.Latitude = double.Parse(coordenadas[1]["Norte"].ToString());
                    GP2.Longitude = double.Parse(coordenadas[1]["Este"].ToString());
                    GP2.ToolTip = nombrePunto;
                    GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP2);


                    GooglePolyline PL1 = new GooglePolyline();
                    PL1.ID = "PL1";                    
                    PL1.ColorCode = "#C9DFAF";
                    
                    PL1.Width = 5;

                    PL1.Points.Add(GP1);
                    PL1.Points.Add(GP2);                    
                   
                    GoogleMapForASPNet1.GoogleMapObject.Polylines.Add(PL1);

                    break;

                }
                else if (coordenadas.Length > 2)
                {
                    if (i==1)
                        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", double.Parse(coordenadas[0]["Norte"].ToString()), double.Parse(coordenadas[0]["Este"].ToString()));

                    GooglePolygon PG1 = new GooglePolygon();
                    PG1.ID = "PG1";
                    //Give Hex code for line color
                    PG1.FillColor = "#C9DFAF";
                    PG1.FillOpacity = 0.4;
                    PG1.StrokeColor = "#C9DFAF";
                    PG1.StrokeOpacity = 1;
                    PG1.StrokeWeight = 2;

                    //Add polygon to GoogleMap object
                    
                    int j=0;
                    for(j=0;j<coordenadas.Length;j++)
                    {
                        GooglePoint GP1 = new GooglePoint();
                        GP1.ID = "GP" + i.ToString();
                        i++;
                        GP1.Latitude = double.Parse(coordenadas[j]["Norte"].ToString());
                        GP1.Longitude = double.Parse(coordenadas[j]["Este"].ToString());
                        GP1.ToolTip = nombrePunto;
                        GP1.InfoHTML = "Aca puede ingresar detalla";
                        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);

                        PG1.Points.Add(GP1);
                    }

                    GoogleMapForASPNet1.GoogleMapObject.Polygons.Add(PG1);

                    break;             

                }


            }
        }
    }
}
