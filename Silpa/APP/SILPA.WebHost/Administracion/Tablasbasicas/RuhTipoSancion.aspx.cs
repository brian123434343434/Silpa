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
using SILPA.LogicaNegocio.AdmTablasBasicas;
using SoftManagement.Log;

public partial class Administracion_Tablasbasicas_RuhTipoSancion : System.Web.UI.Page
{
    protected RuhTipoSancion objTablasBasicas;

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
        ConsultarInformacion(txtNombreParametro.Text);
    }

    protected void ConsultarInformacion(string strNombreTipoFalta)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new RuhTipoSancion();
            grdDatos.DataSource = objTablasBasicas.Listar_RUH_Tipo_Sancion(strNombreTipoFalta);
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

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        Registrar();
    }

    private void Registrar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Inicio");
            objTablasBasicas = new RuhTipoSancion();
            byte byEstado = 1;
            if (chkEstado.Checked == false)
            {
                byEstado = 0;
            }

            objTablasBasicas.Insertar_RUH_Tipo_Sancion(txtDescripcion_Nvo.Text, byEstado);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(String.Empty);
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

    private void limpiarObjetos()
    {
        lblID.Text = "";
        txtNombre.Text = "";
    }

    private void Cancelar()
    {
        ConsultarInformacion(txtNombreParametro.Text);
        grdDatos.SelectedIndex = -1;
        pnlConsultar.Visible = true;
        pnlMaestro.Visible = true;
        pnlNuevo.Visible = false;
        pnlEditar.Visible = false;
        limpiarObjetos();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Cancelar();

    }

    protected void btnCancelarReg_Click(object sender, EventArgs e)
    {
        Cancelar();
    }

    private void Actualizar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Inicio");
            objTablasBasicas = new RuhTipoSancion();
            byte byEstado = 1;
            if (chkEstado.Checked == false)
            {
                byEstado = 0;
            }

            objTablasBasicas.Actualizar_RUH_Tipo_Falta(Convert.ToInt32(lblID.Text), txtNombre.Text, byEstado);
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

    private void Eliminar(string strId)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Inicio");
            objTablasBasicas = new RuhTipoSancion();
            objTablasBasicas.Eliminar_RUH_Tipo_Falta(Convert.ToInt32(strId));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty);
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

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la informaci?n
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion(txtNombreParametro.Text);
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
            //TIS_ID_TIPO,TIS_ACTIVO
            grdDatos.SelectedIndex = index;
            lblID.Text = grdDatos.SelectedDataKey["TIS_ID_TIPO"].ToString();
            string strEstado = grdDatos.SelectedDataKey["TIS_ACTIVO"].ToString();
            if (strEstado == "True")
            {
                chkEstado.Checked = true;
            }
            else
            {
                chkEstado.Checked = false;
            }
            txtNombre.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[0].Text);
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
            Eliminar(grdDatos.SelectedDataKey["TIS_ID_TIPO"].ToString());
        }
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;
    }
}
