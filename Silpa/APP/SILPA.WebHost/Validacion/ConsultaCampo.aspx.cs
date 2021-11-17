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

public partial class Validacion_ConsultaCampo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SILPA.LogicaNegocio.ValidadoresBPM.Campo _campo = new SILPA.LogicaNegocio.ValidadoresBPM.Campo();
        DataSet _temp = _campo.ListarCampo();
        grdCampo.DataSource = _temp;
        grdCampo.DataBind();

        if(!IsPostBack)
        {
            SILPA.LogicaNegocio.ValidadoresBPM.TipoDato _tipoDato = new SILPA.LogicaNegocio.ValidadoresBPM.TipoDato();
            _temp = _tipoDato.ListarTipoDato();
            foreach (DataRow _fila in _temp.Tables[0].Rows)
            {
                cboTipoDato.Items.Add(new ListItem(_fila[1].ToString(), _fila[0].ToString()));
            }
        }
    }

    protected void btnActualizarCampo_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizarCampo_Click.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.Campo _campo = new SILPA.LogicaNegocio.ValidadoresBPM.Campo();
            _campo.EditarCampo(txtCodigoCampo.Text, txtDescripcionCampo.Text, int.Parse(cboTipoDato.SelectedValue));

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("VALIDACIÓN", 3, "Se actualizó Campo");

            pnlActualizarCampo.Visible = false;
            DataSet _temp = _campo.ListarCampo();
            grdCampo.DataSource = _temp;
            grdCampo.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizarCampo_Click.Finalizo");
        }
    }

    protected void grdCampo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Actualizar")
        {
            int _indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow _fila = grdCampo.Rows[_indice];
            txtCodigoCampo.Text = _fila.Cells[1].Text;
            txtDescripcionCampo.Text = _fila.Cells[2].Text;
            cboTipoDato.SelectedIndex = retornarIndice(_fila.Cells[3].Text);
            pnlActualizarCampo.Visible = true;
        }
    }

    protected void btnCancelarCampo_Click(object sender, EventArgs e)
    {
        pnlActualizarCampo.Visible = false;
    }

    #region Funciones Programador

    private int retornarIndice(string _valor)
    {
        foreach (ListItem _item in cboTipoDato.Items)
        {
            _item.Selected = false;
            if (_item.Value == _valor)
            {
                _item.Selected = true;
                return cboTipoDato.SelectedIndex;             
            }
        }
        return -1;
    }

    #endregion
}

