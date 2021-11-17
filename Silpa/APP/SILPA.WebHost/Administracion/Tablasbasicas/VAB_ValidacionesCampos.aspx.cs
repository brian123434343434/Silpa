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

public partial class Administracion_Tablasbasicas_VAB_ValidacionesCampos : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.VAB_ValidacionCampo objTablasBasicas;
    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
        if (!Page.IsPostBack)
        {
            ConsultarInformacion("");
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarInformacion(txtNombreParametro.Text.Trim());
    }

    private void ConsultarInformacion(string strNombre)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_ValidacionCampo();
            grdDatos.DataSource = objTablasBasicas.Listar_ValidacionCampo(strNombre);
            grdDatos.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Finalizo");
        }
    }

    private void Cancelar()
    {
        ConsultarInformacion(txtNombreParametro.Text.Trim());
        grdDatos.SelectedIndex = -1;
        pnlConsultar.Visible = true;
        pnlMaestro.Visible = true;
        pnlNuevo.Visible = false;
        pnlEditar.Visible = false;
        limpiarObjetos();
    }

    private void limpiarObjetos()
    {
        lblID.Text = "";
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_ValidacionCampo();
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;

        objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_ValidacionCampo();
        cboCampos_Nvo.DataSource = objTablasBasicas.Cargar_Combo_Campo();
        cboCampos_Nvo.DataTextField = "DESCRIPCION";
        cboCampos_Nvo.DataValueField = "ID";
        cboCampos_Nvo.DataBind();

        cboValidaciones_Nvo.DataSource = objTablasBasicas.Cargar_Combo_Validaciones();
        cboValidaciones_Nvo.DataTextField = "DESCRIPCION";
        cboValidaciones_Nvo.DataValueField = "ID";
        cboValidaciones_Nvo.DataBind();
    }

    protected void grdDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            //visualizar informacion para edicion
            pnlEditar.Visible = true;
            //Cargar registro Seleccionado
            int index = Convert.ToInt32(e.CommandArgument);
            int pagina = grdDatos.PageIndex;
            if (index > 9)
            {
                index = index - (pagina * 10);
            }
            //TIPO_PERSONA_ID,TIPO_PERSONA_ACTIVO
            grdDatos.SelectedIndex = index;
            lblID.Text = grdDatos.SelectedDataKey["ID"].ToString();

            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_ValidacionCampo();
            cboCampos.DataSource = objTablasBasicas.Cargar_Combo_Campo();
            cboCampos.DataTextField = "DESCRIPCION";
            cboCampos.DataValueField = "ID";
            cboCampos.DataBind();

            cboCampos.SelectedValue = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);

            cboValidaciones.DataSource = objTablasBasicas.Cargar_Combo_Validaciones();
            cboValidaciones.DataTextField = "DESCRIPCION";
            cboValidaciones.DataValueField = "ID";
            cboValidaciones.DataBind();

            cboValidaciones.SelectedValue = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[3].Text);

            if (HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[5].Text) == "S")
            {
                chkActivo.Checked = true;
            }
            else
            {
                chkActivo.Checked = false;
            }

            pnlConsultar.Visible = false;
            pnlMaestro.Visible = false;

        }
        if (e.CommandName == "Eliminar")
        {
            //Cargar registro Seleccionado
            int index = Convert.ToInt32(e.CommandArgument);
            int pagina = grdDatos.PageIndex;
            if (index > 9)
            {
                index = index - (pagina * 10);
            }
            grdDatos.SelectedIndex = index;
            Eliminar(grdDatos.SelectedDataKey["ID"].ToString());
        }

    }

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion(txtNombreParametro.Text.Trim());
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        Registrar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Cancelar();
    }

    protected void btnCancelarReg_Click(object sender, EventArgs e)
    {
        Cancelar();
    }

    private void Eliminar(string strID)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Inicio");
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_ValidacionCampo();
            objTablasBasicas.Eliminar_ValidacionCampo(Convert.ToInt32(strID));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion("");
            Mensaje.MostrarMensaje(this, "Registro eliminado exitosamente");
            this.lblMensajeError.Text = "Registro eliminado exitosamente";
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Finalizo");
        }
    }

    private void Actualizar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Inicio");
            string strActivo;

            if (chkActivo.Checked == true)
            {
                strActivo = "S";
            }
            else
            {
                strActivo = "N";
            }

            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_ValidacionCampo();
            objTablasBasicas.Actualizar_ValidacionCampo(Convert.ToInt32(lblID.Text), cboCampos.SelectedValue, Convert.ToInt32(cboValidaciones.SelectedValue), strActivo);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion("");
            pnlConsultar.Visible = true;
            pnlMaestro.Visible = true;
            pnlNuevo.Visible = false;
            pnlEditar.Visible = false;
            limpiarObjetos();
            Mensaje.MostrarMensaje(this, "Registro modificado exitosamente");
            this.lblMensajeError.Text = "Registro modificado exitosamente";
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Finalizo");
        }
    }

    private void Registrar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Inicio");
            string strActivo;

            if (chkActivo.Checked == true)
            {
                strActivo = "S";
            }
            else
            {
                strActivo = "N";
            }

            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_ValidacionCampo();
            objTablasBasicas.Insertar_ValidacionCampo(cboCampos_Nvo.SelectedValue, Convert.ToInt32(cboValidaciones_Nvo.SelectedValue), strActivo);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion("");
            pnlConsultar.Visible = true;
            pnlMaestro.Visible = true;
            pnlNuevo.Visible = false;
            pnlEditar.Visible = false;
            limpiarObjetos();
            Mensaje.MostrarMensaje(this, "Registro agregado exitosamente");
            this.lblMensajeError.Text = "Registro agregado exitosamente";
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
            SMLog.Escribir(ex);
        }
        finally 
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Finalizo");
        }
    }

}
