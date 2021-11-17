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

public partial class Validacion_ConsultaValidacionCampo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SILPA.LogicaNegocio.ValidadoresBPM.ValidacionCampo _validacionCampo = new SILPA.LogicaNegocio.ValidadoresBPM.ValidacionCampo();
        DataSet _temp = _validacionCampo.ListarValidacionCampo();
        grdValidacionCampo.DataSource = _temp;
        grdValidacionCampo.DataBind();        
    }

    protected void btnActualizarValidacionCampo_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizarValidacionCampo_Click.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.ValidacionCampo _validacionCampo = new SILPA.LogicaNegocio.ValidadoresBPM.ValidacionCampo();
            string _activo = (chkActivo.Checked.ToString() == "True") ? "S" : "N";
            _validacionCampo.EditarValidacionCampo(int.Parse(txtCodigoValidacionCampo.Text), txtCampo.Text,
                int.Parse(txtValidacion.Text), txtFechaInsercion.Text, _activo);
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("VALIDACIÓN", 3, "Se actualizó Validación Campo");

            pnlActualizarValidacionCampo.Visible = false;
            DataSet _temp = _validacionCampo.ListarValidacionCampo();
            grdValidacionCampo.DataSource = _temp;
            grdValidacionCampo.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizarValidacionCampo_Click.Finalizo");
        }
    }

    protected void grdValidacionCampo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Actualizar")
        {
            int _indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow _fila = grdValidacionCampo.Rows[_indice];
            txtCodigoValidacionCampo.Text = _fila.Cells[1].Text;
            txtCampo.Text = _fila.Cells[2].Text;
            txtValidacion.Text = _fila.Cells[3].Text;
            char[] caracter1 = {' '};
            string[] _temporal = _fila.Cells[4].Text.Split(caracter1);
            char[] caracter2 = { '/' };
            string[] _fecha = _temporal[0].Split(caracter2);
            txtFechaInsercion.Text = _fecha[2]+"/"+_fecha[1]+"/"+_fecha[0];            
            chkActivo.Checked = (_fila.Cells[5].Text == "S") ? true : false;            
            pnlActualizarValidacionCampo.Visible = true;
        }
    }    

    protected void btnCancelarValidacionCampo_Click(object sender, EventArgs e)
    {
        pnlActualizarValidacionCampo.Visible = false;
    }
}
