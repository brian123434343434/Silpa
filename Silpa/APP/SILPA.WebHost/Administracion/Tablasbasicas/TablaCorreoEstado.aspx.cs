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

public partial class Administracion_Tablasbasicas_TablaCorreoEstado : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.EstadoCorreo  objTablasBasicas;
    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
        if (!Page.IsPostBack)
        {
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.EstadoCorreo();
            grdDatos.DataSource = objTablasBasicas.Listar_Estado_Correo(strNombre);
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
        txtNombre.Text = "";
        txtNombre_Nvo.Text = "";
    }
    
    protected void  btnagregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;
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
            lblID.Text = grdDatos.SelectedDataKey["CORREO_ESTADO_ID"].ToString();

            txtNombre.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
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
            Eliminar(grdDatos.SelectedDataKey["CORREO_ESTADO_ID"].ToString());
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.EstadoCorreo();
            objTablasBasicas.Eliminar_Estado_Correo(Convert.ToInt32(strID));
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.EstadoCorreo();
            objTablasBasicas.Actualizar_Estado_Correo(Convert.ToInt32(lblID.Text), txtNombre.Text);
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.EstadoCorreo();
            objTablasBasicas.Insertar_Estado_Correo(txtNombre_Nvo.Text.Trim());
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
