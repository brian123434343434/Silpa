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
using SILPA.LogicaNegocio;
using SoftManagement.Log;

public partial class Administracion_Tablasbasicas_BpmParametros : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.TablasBasicas objTablasBasicas;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TablasBasicas();
            //cargar combo para le edicion  
            cboTipo.DataTextField = "DESCRIPCION";
            cboTipo.DataValueField = "TIPOID";
            cboTipo.DataSource = objTablasBasicas.Listar_Tipos_Parametros();
            cboTipo.DataBind();
            //Cargar combo de Nuevos
            cboTipo_Nvo.DataTextField = "DESCRIPCION";
            cboTipo_Nvo.DataValueField = "TIPOID";
            cboTipo_Nvo.DataSource = objTablasBasicas.Listar_Tipos_Parametros();
            cboTipo_Nvo.DataBind();
            ConsultarInformacion("");
        }
        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
    }

    private void ConsultarInformacion(string strNombre)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TablasBasicas();
            grdDatos.DataSource = objTablasBasicas.Listar_Bpm_Parametros(strNombre);
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

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarInformacion(txtNombreParametro.Text);
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        Registrar();
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

            grdDatos.SelectedIndex = index;
            lblID.Text = grdDatos.SelectedDataKey["ID"].ToString();
            cboTipo.SelectedValue = grdDatos.SelectedDataKey["TIPO"].ToString();
            txtNombre.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
            txtValor.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[2].Text);
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

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion(txtNombreParametro.Text);
    }

    private void Eliminar(string strID)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Inicio");
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TablasBasicas();
            objTablasBasicas.Eliminar_Bpm_Parametros(Convert.ToInt16(strID));
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TablasBasicas();
            objTablasBasicas.Actualizar_Bpm_Parametros(Convert.ToInt32(lblID.Text), Convert.ToInt32(cboTipo.SelectedValue), txtNombre.Text, Convert.ToInt32(txtValor.Text));
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

    private void limpiarObjetos()
    {
        lblID.Text = "";
        txtNombre.Text = "";
        txtValor.Text = "";
        cboTipo.SelectedValue = "-1";
    }

    private void Registrar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Inicio");
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TablasBasicas();
            objTablasBasicas.Insertar_Bpm_Parametros(Convert.ToInt32(cboTipo_Nvo.SelectedValue), txtNombre_Nvo.Text, Convert.ToInt32(txtValor_Nvo.Text));
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

    //    private void pruebaMensaje()
    //    {
    //        String js = @"
    //                                    if(confirm('Esta Seguro de eliminar de la lista')==true)
    //                                        document.getElementById('" + hdfResultado.ClientID + @"').value='true';
    //                                    else
    //                                        document.getElementById('" + hdfResultado.ClientID + @"').value='false';
    //                                    ";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", js, true);

    //        if (hdfResultado.Value == "false")
    //        {
    //            Mensaje.MostrarMensaje(this, "Si");    
    //        }
    //        else
    //            Mensaje.MostrarMensaje(this, "No");    




    //    }
    private void Cancelar()
    {
        ConsultarInformacion(string.Empty);
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
}
