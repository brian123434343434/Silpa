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

public partial class Administracion_Tablasbasicas_VAB_Campos : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.VAB_Campo objTablasBasicas;
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_Campo();
            grdDatos.DataSource = objTablasBasicas.Listar_Campos(strNombre);
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
        txtID.Text = "";
        txtID_Nuevo.Text = "";
        txtDescripcion.Text = "";
        txtDescripcion_Nuevo.Text = "";
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;

        objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_Campo();
        cboTiposDato_Nuevo.DataSource = objTablasBasicas.Cargar_Combo_TDatos();
        cboTiposDato_Nuevo.DataValueField = "ID";
        cboTiposDato_Nuevo.DataTextField = "DESCRIPCION";
        cboTiposDato_Nuevo.DataBind();
        //cmbNombreServidor .Items .Insert (0,new ListItem ("Seleccione...", 0));
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
            txtID.Text = grdDatos.SelectedDataKey["ID"].ToString();

            this.txtID.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[0].Text);
            this.txtDescripcion.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);

            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_Campo();
            cboTipoDato.DataSource = objTablasBasicas.Cargar_Combo_TDatos();
            cboTipoDato.DataValueField = "ID";
            cboTipoDato.DataTextField = "DESCRIPCION";
            cboTipoDato.DataBind();
            this.cboTipoDato.SelectedValue = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[2].Text);
            
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_Campo();
            objTablasBasicas.Eliminar_Campos(strID);
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_Campo();
            objTablasBasicas.Actualizar_Campos(txtID.Text.Trim(), txtDescripcion.Text.Trim(), Convert.ToInt32(cboTipoDato.SelectedValue));
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
            if (Convert.ToInt32(cboTiposDato_Nuevo.SelectedValue) != 0)
            {
                objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.VAB_Campo();
                objTablasBasicas.Insertar_Campos(txtID_Nuevo.Text.Trim(), txtDescripcion_Nuevo.Text.Trim(), Convert.ToInt32(cboTiposDato_Nuevo.SelectedValue));
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
            else
            {
                Mensaje.MostrarMensaje(this, "Debe seleccionar un tipo de dato");
                this.lblMensajeError.Text = "Debe seleccionar un tipo de dato";
            }

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
