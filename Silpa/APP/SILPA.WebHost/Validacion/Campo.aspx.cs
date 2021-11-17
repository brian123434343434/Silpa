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

public partial class Validacion_Campo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Page_Load.Inicio");
                SILPA.LogicaNegocio.ValidadoresBPM.TipoDato _tipoDato = new SILPA.LogicaNegocio.ValidadoresBPM.TipoDato();
                DataSet _temp = _tipoDato.ListarTipoDato();
                foreach (DataRow _fila in _temp.Tables[0].Rows)
                {
                    cboTipoDato.Items.Add(new ListItem(_fila[1].ToString(), _fila[0].ToString()));
                }
            }
            catch (Exception ex)
            {
                SMLog.Escribir(ex);
            }
            finally
            {
                SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Page_Load.Finalizo");
            }
        }
    }

    protected void btnAgregarCampo_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregarCampo_Click.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.Campo _campo = new SILPA.LogicaNegocio.ValidadoresBPM.Campo();
            _campo.InsertarCampo(txtCodigoCampo.Text, txtDescripcionCampo.Text, int.Parse(cboTipoDato.SelectedValue));
            txtCodigoCampo.Text = "";
            txtDescripcionCampo.Text = "";

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("VALIDACIÓN", 1, "Se almaceno Campo");
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregarCampo_Click.Finalizo");
        }
    }

    protected void btnCancelarCampo_Click(object sender, EventArgs e)
    {
        txtCodigoCampo.Text = "";
        txtDescripcionCampo.Text = "";
    }
}
