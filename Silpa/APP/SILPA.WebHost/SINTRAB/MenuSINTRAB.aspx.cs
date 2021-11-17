using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using SILPA.LogicaNegocio.MenuSINTRAB;
using SILPA.AccesoDatos.SINTRAB;


public partial class SINTRAB_MenuSINTRAB : System.Web.UI.Page
{


    MenuSINTRAB ObjMenuSINTRAB = null;
    List<MenuSINTRABIdentity.Menu> ObjLstMenuSINTRABIdentity = null;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.Request["Ubic"] == null)
            if (DatosSesion.Usuario != "")
                this.Page.MasterPageFile = "~/plantillas/SILPA.master";

        if (!IsPostBack)
        {
            ObjMenuSINTRAB = new MenuSINTRAB();
            ObjLstMenuSINTRABIdentity = new List<MenuSINTRABIdentity.Menu>();
            ObjLstMenuSINTRABIdentity = ObjMenuSINTRAB.ObtenerMenuSINTRAB();

            string html = string.Empty;
            int cont = 0;

            html = "<body ontouchstart='' style='background-color:#EBEBEB'>";

            html = html + "<ul id='css3menusintrab' class='topmenu'>";
            html = html + "<li class='switch'><label onclick='' for='css3menu-switcher'></label></li>";
            //html = html + "<li class='topmenu'>";
            foreach (var Modulos in ObjLstMenuSINTRABIdentity)
            {
                //menu
                html = html + "<li class='topmenu'>";
                html = html + " <a href = '#' style ='width:auto;' ><span>" + Modulos.NOMBRE_MENU + "</span></a>";

                //submenu
                html = html + "<ul>";
                foreach (var Submodulos in Modulos.ObjLstSubmenus)
                {
                    if (cont == 0)
                    {
                        html = html + "<li class='subfirst'><a href= " + Submodulos.TXT_URL + " target='_blank'>" + Submodulos.NOMBRE_SUBMENU + "</a></li>";
                        cont++;
                    }
                    else
                    {
                        html = html + "<li><a href= " + Submodulos.TXT_URL + " target='_blank'>" + Submodulos.NOMBRE_SUBMENU + "</a></li>";
                        cont++;
                    }

                }
                html = html + "</ul>";
                html = html + "</li>";
                cont = 0;
            }
            html = html + "</ul>";
            html = html + "</body>";

            PlaceHolder1.Controls.Add(new LiteralControl(html));
        }

      
    }
}
