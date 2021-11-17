using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salvoconducto_ModuloAdministradorSUNL : System.Web.UI.Page
{
    public string Url;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //USUARIO PARA PRUEBAS
           //Session["Usuario"] = 11075;
           //return;

            //DESCOMENTAR ANTES DEL COMMIT!!!
            if (new Utilidades().ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
        }
    }

    protected void LnkEspecieTipoProducto_Click(object sender, EventArgs e)
    {
        Url = "ModuloAdminEspeciesProductos.aspx";
    }
}