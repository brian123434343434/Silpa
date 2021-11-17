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

public partial class PermisosAmbientales_Liquidacion_DocumentoPruebas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CargarTabla();
    }

    private void CargarTabla()
    {
        DataTable _tabla = new DataTable();
        _tabla.Columns.Add("DESCRIPCION", Type.GetType("System.String"));
        _tabla.Columns.Add("VALOR",Type.GetType("System.Double"));

        DataRow _fila = _tabla.NewRow();
        _fila["DESCRIPCION"] = "Concepto 1";
        _fila["VALOR"]=456.01;
        _tabla.Rows.Add(_fila);
        _tabla.AcceptChanges();               

        grdConceptos.DataSource = _tabla;
        grdConceptos.DataBind();

        //Response.Write(grdConceptos.Columns.Count);
    }
}
