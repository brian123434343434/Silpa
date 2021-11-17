using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstadoTramite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
         GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("SILPA", 2, "Se consulto Estado Tramite");

    }
}
