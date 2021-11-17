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

public partial class Validacion_ValidacionCampo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarComboCampo();
            CargarComboValidacion();
        }
    }

    protected void btnAgregarValidacionCampo_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregarValidacionCampo_Click.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.ValidacionCampo _validacionCampo = new SILPA.LogicaNegocio.ValidadoresBPM.ValidacionCampo();
            string _activo = (chkActivo.Checked.ToString() == "True") ? "S" : "N";
            _validacionCampo.InsertarValidacionCampo(cboCampo.SelectedValue,
                int.Parse(cboValidacion.SelectedValue), _activo);
            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("VALIDACIÓN", 1, "Se almacenó Validación Campo");

            cboCampo.SelectedIndex = 0;
            cboValidacion.SelectedIndex = 0;
            chkActivo.Checked = false;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregarValidacionCampo_Click.Finalizo");
        }
    }

    #region Funciones Programador

    /// <summary>
    /// Metodo que carga el listado de campos en el combo cboCampo
    /// </summary>
    private void CargarComboCampo()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboCampo.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.Campo _campos = new SILPA.LogicaNegocio.ValidadoresBPM.Campo();
            DataSet _temp = _campos.ListarCampo();
            foreach (DataRow _fila in _temp.Tables[0].Rows)
            {
                cboCampo.Items.Add(new ListItem(_fila[1].ToString(), _fila[0].ToString()));
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboCampo.Finalizo");
        }
    }

    /// <summary>
    /// Metodo que carga el listado de validaciones en el combo cboValidacion
    /// </summary>
    private void CargarComboValidacion()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboValidacion.Inicio");
            SILPA.LogicaNegocio.ValidadoresBPM.Validacion _validacion = new SILPA.LogicaNegocio.ValidadoresBPM.Validacion();
            DataSet _temp = _validacion.ListarValidacion();
            foreach (DataRow _fila in _temp.Tables[0].Rows)
            {
                cboValidacion.Items.Add(new ListItem(_fila[1].ToString(), _fila[0].ToString()));
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboValidacion.Finalizo");
        }
    }

    #endregion

    protected void btnCancelarValidacionCampo_Click(object sender, EventArgs e)
    {
        cboCampo.SelectedIndex = 0;
        cboValidacion.SelectedIndex = 0;
        chkActivo.Checked = false;
    }
}
