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
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using SoftManagement.Log;
using SILPA.Comun;

public partial class Administracion_Tablasbasicas_CorreoServidor : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.CorreoServidor objTablasBasicas;
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.CorreoServidor();
            grdDatos.DataSource = objTablasBasicas.Listar_Correo_Servidor(strNombre);
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
        txtNombreServidor.Text = "";
        txtNombreServidor_Nuevo.Text = "";
        txtHost.Text = "";
        txtHost_Nuevo.Text = "";
        txtUsuario.Text = "";
        txtUsuario_Nuevo.Text = "";
        txtPuerto.Text = "";
        txtPuerto_Nuevo.Text = "";
        txtContrasena.Text = "";
        txtContrasena_Nuevo.Text = "";
        txtSeparador.Text = "";
        txtSeparador_Nuevo.Text = "";
    }

    protected void btnagregar_Click(object sender, EventArgs e)
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
            lblID.Text = grdDatos.SelectedDataKey["CORREO_SERVIDOR_ID"].ToString();
             
            this.txtNombreServidor.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
            this.txtHost.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[2].Text);
            this.txtUsuario.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[3].Text);
            this.txtContrasena.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[4].Text);
            this.txtPuerto.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[5].Text);
            this.txtSeparador.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[6].Text);

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
            Eliminar(grdDatos.SelectedDataKey["CORREO_SERVIDOR_ID"].ToString());
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.CorreoServidor();
            objTablasBasicas.Eliminar_Correo_Servidor(Convert.ToInt32(strID));
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
            if (txtContrasena.Text == txtConfContrasena.Text)
            {
                string strContrasenaEnc;
                //strContrasenaEnc = Cryptographer.EncryptSymmetric(ConfigurationManager.AppSettings["CRYPTO_PROVIDER"].ToString(), this.txtContrasena.Text.Trim());
                //JMM - 2010-10-01 - Se reemplaza la forma dfe encripcion de la contraseña
                strContrasenaEnc = EnDecript.Encriptar(this.txtContrasena.Text.Trim());
                //JMM

                objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.CorreoServidor();
                objTablasBasicas.Actualizar_Correo_Servidor(Convert.ToInt32(lblID.Text), txtNombreServidor.Text.Trim(), txtHost.Text.Trim(), txtUsuario.Text.Trim(), strContrasenaEnc, txtSeparador.Text.Trim(), Convert.ToInt32(txtPuerto.Text.Trim()));
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
            else
            {
                Mensaje.MostrarMensaje(this, "Las contraseñas ingresadas no coinciden.");
                this.lblMensajeError.Text = "Las contraseñas ingresadas no coinciden.";
            }
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
            if (txtContrasena_Nuevo.Text == txtConfContrasena_Nvo.Text)
            {
                string strContrasenaEnc;
                //strContrasenaEnc = Cryptographer.EncryptSymmetric(ConfigurationManager.AppSettings["CRYPTO_PROVIDER"].ToString(), this.txtContrasena_Nuevo.Text.Trim());
                //JMM - 2010-10-01 - Se reemplaza la forma dfe encripcion de la contraseña
                strContrasenaEnc = EnDecript.Encriptar(this.txtContrasena_Nuevo.Text.Trim());
                //JMM

                objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.CorreoServidor();
                objTablasBasicas.Insertar_Correo_Servidor(txtNombreServidor_Nuevo.Text.Trim(), txtHost_Nuevo.Text.Trim(), txtUsuario_Nuevo.Text.Trim(), strContrasenaEnc, txtSeparador_Nuevo.Text.Trim(), Convert.ToInt32(txtPuerto_Nuevo.Text.Trim()));
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
                Mensaje.MostrarMensaje(this, "Las contraseñas ingresadas no coinciden.");
                this.lblMensajeError.Text = "Las contraseñas ingresas no coinciden.";
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
            this.lblMensajeError.Text = "uy!!!"+ex.Message;
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Finalizo");
        }
    }
}
