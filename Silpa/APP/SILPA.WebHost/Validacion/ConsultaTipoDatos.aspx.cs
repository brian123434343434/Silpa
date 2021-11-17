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

public partial class Validacion_ConsultaTipoDatos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SILPA.LogicaNegocio.ValidadoresBPM.TipoDato _tipoDato = new SILPA.LogicaNegocio.ValidadoresBPM.TipoDato();
        DataSet _temp = _tipoDato.ListarTipoDato();
        grdTiposDatos.DataSource = _temp;
        grdTiposDatos.DataBind();

    }

    protected void btnActualizarTipoDato_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizarTipoDato_Click.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.TipoDato _tipoDato = new SILPA.LogicaNegocio.ValidadoresBPM.TipoDato();
            _tipoDato.EditarTipoDato(int.Parse(txtCodigoTipoDato.Text), txtDescripcionTipoDato.Text);

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("VALIDACIÓN", 3, "Se actualizó Tipo Dato");

            pnlActualizarTipoDato.Visible = false;
            DataSet _temp = _tipoDato.ListarTipoDato();
            grdTiposDatos.DataSource = _temp;
            grdTiposDatos.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizarTipoDato_Click.Finalizo");
        }
    }

    protected void grdTiposDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Actualizar")
        {
            int _indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow _fila = grdTiposDatos.Rows[_indice];
            txtCodigoTipoDato.Text = _fila.Cells[1].Text;
            txtDescripcionTipoDato.Text = _fila.Cells[2].Text;
            pnlActualizarTipoDato.Visible = true;
        }
    }

    protected void btnCancelarTipoDato_Click(object sender, EventArgs e)
    {
        pnlActualizarTipoDato.Visible = false;
    }
}
