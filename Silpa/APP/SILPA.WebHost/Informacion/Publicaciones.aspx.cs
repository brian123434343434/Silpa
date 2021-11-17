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
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using SILPA.AccesoDatos.Publicacion;
using SILPA.LogicaNegocio.Publicacion;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;

public partial class Informacion_Publicaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            mpeMensajeTercero.Show();
        }

    }
    protected void btnAceptarMensajeTerceo_Click(object sender, EventArgs e)
    {
        try
        {
            //Actualizar modal
            this.upnlMensajeTercero.Update();

            this.mpeMensajeTercero.Hide();
        }
        catch (Exception exc)
        {
            
        }
    }

    
}
