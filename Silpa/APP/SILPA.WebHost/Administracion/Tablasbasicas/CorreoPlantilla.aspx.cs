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

public partial class Administracion_Tablasbasicas_CorreoPlantilla : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.PlantillaCorreo objTablasBasicas;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";

    }

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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.PlantillaCorreo();
            grdDatos.DataSource = objTablasBasicas.Listar_Plantilla_Correo(strNombre);
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
        //pnlConsultar.Visible = true;
        //pnlMaestro.Visible = true;
        pnlNuevo.Visible = false;
        this.mpeEditarPlantilla.Hide();
        limpiarObjetos();
    }

    private void limpiarObjetos()
    {
        lblID.Text = "";
        txtAsunto.Text = "";
        txtAsunto_Nuevo.Text = "";
        //txtConfirmaEnvio_Nuevo.Text = "";
        //txtConformacionEnvio.Text = "";
        txtDe.Text = "";
        txtDe_Nuevo.Text = "";
        //txtIdCorreoServidor.Text = "";
        //txtIdCorreoServidor_Nuevo.Text ="";
        txtPara.Text = "";
        txtPara_Nuevo.Text = "";
        this.radPlantillaEdicion.Content = "";
        txtPlantilla_Nuevo.Text = "";
        txtNombreParametro.Text = "";
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        //pnlConsultar.Visible = false;
        //pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;

        objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.PlantillaCorreo();
        cmbNombreServidor_Nuevo.DataSource = objTablasBasicas.Cargar_Combo_Servidor();
        cmbNombreServidor_Nuevo.DataValueField = "CORREO_SERVIDOR_ID";
        cmbNombreServidor_Nuevo.DataTextField = "NOMBRE_SERVIDOR";
        cmbNombreServidor_Nuevo.DataBind();
        //cmbNombreServidor .Items .Insert (0,new ListItem ("Seleccione...", 0));
    }

    protected void grdDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Modificar")
        {
            HiddenField hdfPlantilla;
            //visualizar informacion para edicion
            int index = Convert.ToInt32(e.CommandArgument);
            int pagina = grdDatos.PageIndex;
            if (index > 9)
            {
                index = index - (pagina * 10);
            }
            this.mpeEditarPlantilla.Show();
            //TIPO_PERSONA_ID,TIPO_PERSONA_ACTIVO
            grdDatos.SelectedIndex = index;
            lblID.Text = grdDatos.SelectedDataKey["CORREO_PLANTILLA_ID"].ToString();

            this.txtDe.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
            this.txtPara.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[2].Text);
            this.radPlantillaEdicion.Content = ((HiddenField)grdDatos.Rows[index].FindControl("hdfPlantilla")).Value;

            this.txtAsunto.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[4].Text);
            //this.txtIdCorreoServidor.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[5].Text);
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.PlantillaCorreo();
            cmbNombreServidor.DataSource = objTablasBasicas.Cargar_Combo_Servidor();
            cmbNombreServidor.DataValueField = "CORREO_SERVIDOR_ID";
            cmbNombreServidor.DataTextField = "NOMBRE_SERVIDOR";
            cmbNombreServidor.DataBind();
            this.cmbNombreServidor.SelectedValue = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[5].Text);
            //this.txtConformacionEnvio.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[7].Text);
            if (HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[8].Text) == "SI")
            {
                this.cmbConfirmarEnvio.SelectedValue = "1";
            }
            else 
            {
                this.cmbConfirmarEnvio.SelectedValue = "0";
            }
            //pnlConsultar.Visible = false;
            //pnlMaestro.Visible = false;
            

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
            Eliminar(grdDatos.SelectedDataKey["CORREO_PLANTILLA_ID"].ToString());
        }
        if (e.CommandName == "VerPlantilla")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int pagina = grdDatos.PageIndex;
            if (index > 9)
            {
                index = index - (pagina * 10);
            }
            HiddenField hdfPlantilla;
            mpeVerPlantilla.Show();
            hdfPlantilla = (HiddenField)grdDatos.Rows[index].FindControl("hdfPlantilla");
            string strPlantilla = HttpUtility.HtmlDecode(hdfPlantilla.Value);
            this.redtPlantilla.Content = strPlantilla;
            this.redtPlantilla.Enabled = false;
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.PlantillaCorreo();
            objTablasBasicas.Eliminar_Plantilla_Correo(Convert.ToInt32(strID));
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.PlantillaCorreo();
            objTablasBasicas.Actualizar_Plantilla_Correo(Convert.ToInt32(lblID.Text), txtDe.Text.Trim(), txtPara.Text.Trim(), this.radPlantillaEdicion.Content, txtAsunto.Text.Trim(), Convert.ToInt32(this.cmbNombreServidor.SelectedValue), Convert.ToInt32(this.cmbConfirmarEnvio.SelectedValue));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion("");
            //pnlConsultar.Visible = true;
            //pnlMaestro.Visible = true;
            pnlNuevo.Visible = false;
            this.mpeEditarPlantilla.Hide();
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
            if (Convert.ToInt32(cmbNombreServidor.SelectedValue) != 0)
            {
                objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.PlantillaCorreo();
                objTablasBasicas.Insertar_Plantilla_Correo(this.txtDe_Nuevo.Text.Trim(), this.txtPara_Nuevo.Text.Trim(), this.txtPlantilla_Nuevo.Text.Trim(), this.txtAsunto_Nuevo.Text.Trim(), Convert.ToInt32(this.cmbNombreServidor.SelectedValue), Convert.ToInt32(this.cmbConfirmaEnvio_Nuevo.SelectedValue));
                grdDatos.SelectedIndex = -1;
                ConsultarInformacion("");
                //pnlConsultar.Visible = true;
                //pnlMaestro.Visible = true;
                pnlNuevo.Visible = false;
                
                limpiarObjetos();
                Mensaje.MostrarMensaje(this, "Registro agregado exitosamente");
                this.lblMensajeError.Text = "Registro agregado exitosamente";
            }
            else
            {
                Mensaje.MostrarMensaje(this, "Debe seleccionar un servidor de correo");
                this.lblMensajeError.Text = "Debe seleccionar un servidor de correo";
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
    protected void grdDatos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgBVerPlantilla = (ImageButton)e.Row.FindControl("imgBVerPlantilla");
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(imgBVerPlantilla);
        }
    }
}
