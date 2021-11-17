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

public partial class Administracion_Tablasbasicas_WSB_Metodo : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo objTablasBasicas;
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo();
            grdDatos.DataSource = objTablasBasicas.Listar_Metodos(strNombre);
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
        //cboNombreServicio.SelectedValue = 0;
        //cboNombreServicio_Nvo.SelectedValue = 0;
        txtNombreMetodo.Text = "";
        txtNombreMetodo_Nvo.Text = "";
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;

        objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo();
        cboNombreServicio_Nvo.DataSource = objTablasBasicas.Cargar_Combo_Servicios();
        cboNombreServicio_Nvo.DataValueField = "WSB_ID";
        cboNombreServicio_Nvo.DataTextField = "WSB_NOMBRE_SERVICIO";
        cboNombreServicio_Nvo.DataBind();
        cboNombreServicio_Nvo.Items.Insert(0, new ListItem("Seleccione...", "0"));   
    }

    protected void grdDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int iIdServicio;
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
            lblID.Text = grdDatos.SelectedDataKey["WSB_ID_METODO"].ToString();
            iIdServicio = Convert.ToInt32(grdDatos.SelectedDataKey["WSB_ID_SERVICIO"]);

            txtNombreMetodo.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[2].Text);
            if (HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[3].Text) == "S")
            {
                chkActivo.Checked = true;
            }
            else
            {
                chkActivo.Checked = false;  
            }

            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo();
            cboNombreServicio.DataSource = objTablasBasicas.Cargar_Combo_Servicios();
            cboNombreServicio.DataValueField = "WSB_ID";
            cboNombreServicio.DataTextField = "WSB_NOMBRE_SERVICIO";
            cboNombreServicio.DataBind();
            cboNombreServicio.SelectedValue = iIdServicio.ToString();      

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
            Eliminar(grdDatos.SelectedDataKey["WSB_ID_METODO"].ToString());
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
            if (Convert.ToInt32(cboNombreServicio_Nvo.SelectedValue) != 0)
            {
                objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo();
                objTablasBasicas.Eliminar_Metodos(Convert.ToInt32(strID));
                grdDatos.SelectedIndex = -1;
                ConsultarInformacion("");
                Mensaje.MostrarMensaje(this, "Registro eliminado exitosamente");
                this.lblMensajeError.Text = "Registro eliminado exitosamente";
            }
            else
            {
                Mensaje.MostrarMensaje(this, "Debe seleccionar un servicio.");
                this.lblMensajeError.Text = "Debe seleccionar un servicio.";
            }

        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Inicio");
            byte iActivo;

            if (chkActivo.Checked == true)
            {
                iActivo = 1;
            }
            else
            {
                iActivo = 0;
            }

            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo();
            objTablasBasicas.Actualizar_Metodos(Convert.ToInt32(lblID.Text), Convert.ToInt32(cboNombreServicio.SelectedValue), txtNombreMetodo.Text, iActivo);
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
            byte iActivo;

            if (chkActivo_Nvo.Checked == true)
            {
                iActivo = 1;
            }
            else
            {
                iActivo = 0;
            }

            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.WSB_Metodo();
            objTablasBasicas.Insertar_Metodos(Convert.ToInt32(cboNombreServicio_Nvo.SelectedValue), txtNombreMetodo_Nvo.Text, iActivo);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion("");
            pnlConsultar.Visible = true;
            pnlMaestro.Visible = true;
            pnlNuevo.Visible = false;
            pnlEditar.Visible = false;
            limpiarObjetos();
            Mensaje.MostrarMensaje(this, "Registro agregado exitosamente");
            this.lblMensajeError.Text = "Registro agregado satisfactoriamente";
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
