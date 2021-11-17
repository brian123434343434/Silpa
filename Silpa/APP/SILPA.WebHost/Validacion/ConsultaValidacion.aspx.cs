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
using SoftManagement.Log;

public partial class Validacion_ConsultaValidacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        SILPA.LogicaNegocio.ValidadoresBPM.Validacion _validacion = new SILPA.LogicaNegocio.ValidadoresBPM.Validacion();
        DataSet _temp = _validacion.ListarValidacion();
        grdValidacion.DataSource = _temp;
        grdValidacion.DataBind();
    }

    protected void btnActualizarValidacion_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizarValidacion_Click.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.Validacion _validacion = new SILPA.LogicaNegocio.ValidadoresBPM.Validacion();
            _validacion.EditarValidacion(int.Parse(txtCodigoValidacion.Text), txtDescripcionValidacion.Text, txtSentenciaValidacion.Text);
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("VALIDACIÓN", 3, "Se actualizó Validación");

            pnlActualizarValidacion.Visible = false;
            DataSet _temp = _validacion.ListarValidacion();
            grdValidacion.DataSource = _temp;
            grdValidacion.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizarValidacion_Click.Finalizo");
        }
    }

    protected void grdValidacion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Actualizar")
        {
            int _indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow _fila = grdValidacion.Rows[_indice];
            txtCodigoValidacion.Text = _fila.Cells[1].Text;
            txtDescripcionValidacion.Text = _fila.Cells[2].Text;
            txtSentenciaValidacion.Text = _fila.Cells[3].Text;
            pnlActualizarValidacion.Visible = true;
        }
    }

    protected void btnCancelarValidacion_Click(object sender, EventArgs e)
    {
        pnlActualizarValidacion.Visible = false;
    }
}
