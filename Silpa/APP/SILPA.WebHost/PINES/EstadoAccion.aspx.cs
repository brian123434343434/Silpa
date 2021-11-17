using SILPA.LogicaNegocio.PINES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PINES_EstadoAccion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarEstadoActividad();
        }
    }

    private void CargarEstadoActividad()
    {
        if (Request.QueryString["PI"] != string.Empty && Request.QueryString["IDA"] != string.Empty)
        {
            AccionActividad clsAccionActividad = new AccionActividad();
            this.grvEstadoAccion.DataSource = clsAccionActividad.ConsultaEstadoActivityProcessInstance(Convert.ToInt32(Request.QueryString["PI"]), Convert.ToInt32(Request.QueryString["IDA"]));
            this.grvEstadoAccion.DataBind();
        }
    }
    protected void grvEstadoAccion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblFechaVencimiento = (Label)e.Row.FindControl("lblFechaVencimiento");
            Label lblFechaFinalizacion = (Label)e.Row.FindControl("lblFechaFinalizacion");
            Image imgEstadoAccion = (Image)e.Row.FindControl("imgEstadoAccion");
            if (lblFechaFinalizacion.Text == string.Empty)
            {
                if (DateTime.Today <= Convert.ToDateTime(lblFechaVencimiento.Text))
                {
                    imgEstadoAccion.ImageUrl = "~/App_Themes/Img/BotonVerde.png";
                }
                else
                {
                    imgEstadoAccion.ImageUrl = "~/App_Themes/Img/BotonRojo.png";
                }
            }
            else
            {
                if (Convert.ToDateTime(lblFechaFinalizacion.Text) <= Convert.ToDateTime(lblFechaVencimiento.Text))
                {
                    imgEstadoAccion.ImageUrl = "~/App_Themes/Img/Chulo_verde.png";
                }
                else
                {
                    imgEstadoAccion.ImageUrl = "~/App_Themes/Img/Chulo_rojo.png";
                }
            }
        }
    }
}