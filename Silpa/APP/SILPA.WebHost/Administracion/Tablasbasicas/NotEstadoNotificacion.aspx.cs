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

public partial class Administracion_Tablasbasicas_NotEstadoNotificacion : System.Web.UI.Page
{
    private SILPA.LogicaNegocio.AdmTablasBasicas.Not_Estado_Notificacion objTablasBasicas;
    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
       
        if (!Page.IsPostBack)
        {
            ConsultarInformacion("");
        }
    }

    private void ConsultarInformacion(string strNombre)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.Not_Estado_Notificacion();
            grdDatos.DataSource = objTablasBasicas.Listar_Not_Estado_Notificacion(strNombre);
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
            //ID_ESTADO,ESTADO_ACTIVO,DIAS_VENCIMIENTO,ESTADO_PDI
            grdDatos.SelectedIndex = index;
            lblID.Text = grdDatos.SelectedDataKey["ID_ESTADO"].ToString();
            string strEstado = grdDatos.SelectedDataKey["ESTADO_ACTIVO"].ToString();

            if (grdDatos.SelectedDataKey["MOSTRAR_INFO"] != null)
            {
                if (grdDatos.SelectedDataKey["MOSTRAR_INFO"].ToString() == System.Boolean.TrueString)
                {
                    this.chkMostrarInfo.Checked = true;
                }
            }
            if (grdDatos.SelectedDataKey["ENVIA_CORREO"] != null)
            {
                if (grdDatos.SelectedDataKey["ENVIA_CORREO"].ToString() == System.Boolean.TrueString)
                {
                    this.chkEnviaCorreo.Checked = true;
                }
            }
            this.ChkEstadoPdI.Checked = false;
            if(grdDatos.SelectedDataKey["ESTADO_PDI"]!=null)
            {
                if (grdDatos.SelectedDataKey["ESTADO_PDI"].ToString() == System.Boolean.TrueString)
                {
                    this.ChkEstadoPdI.Checked = true;
                }
            }
            if (grdDatos.SelectedDataKey["ES_PUBLICO"] != null)
            {
                if (grdDatos.SelectedDataKey["ES_PUBLICO"].ToString() == System.Boolean.TrueString)
                {
                    this.chkEsPublico.Checked = true;
                }
            }

            if(grdDatos.SelectedDataKey["DIAS_VENCIMIENTO"]!=null)
            {
                txtDiasVencimientoEdit.Text = grdDatos.SelectedDataKey["DIAS_VENCIMIENTO"].ToString();
            }

            //string strEstado = grdDatos.SelectedDataKey["ESTADO_PDI"].ToString();


            if (strEstado == "True")
            {
                chkEstado.Checked = true;
            }
            else
            {
                chkEstado.Checked = false;
            }
            if (grdDatos.SelectedDataKey["MENSAJE_CORREO"] != null)
            {
                this.txtMensajeCorreo.Text = grdDatos.SelectedDataKey["MENSAJE_CORREO"].ToString();
            }
            txtNombre.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[0].Text);
            this.txtDescripcionMostrar.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
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
            Eliminar(grdDatos.SelectedDataKey["ID_ESTADO"].ToString());
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.Not_Estado_Notificacion();
            objTablasBasicas.Eliminar_Not_Estado_Notificacion(Convert.ToInt32(strID));
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
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.Not_Estado_Notificacion();
            byte deEstado = 1;
            
            byte deEstadoPDI = 1;
            int diasVencimiento = 0;

            if (chkEstado.Checked == false)
            {
                deEstado = 0;
            }

            if (this.ChkEstadoPdI.Checked == false)
            {
                deEstadoPDI = 0;
            }

            if(!String.IsNullOrEmpty(txtDiasVencimientoEdit.Text))
            {
                diasVencimiento = int.Parse(txtDiasVencimientoEdit.Text);
            }

            objTablasBasicas.Actualizar_Not_Estado_Notificacion(Convert.ToInt32(lblID.Text), txtNombre.Text, deEstado, deEstadoPDI,diasVencimiento,this.chkMostrarInfo.Checked,this.chkEnviaCorreo.Checked, this.txtMensajeCorreo.Text, this.chkEsPublico.Checked, this.txtDescripcionMostrar.Text);
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
        txtDescripcion_Nvo.Text = "";
        chkEstado.Checked = false;
        ChkEstadoPdI_Nvo.Checked = false;
        txtDiasVencimiento_Nvo.Text = "0";
        txtDiasVencimientoEdit.Text = "0";
        chkMostrarInfo_Nvo.Checked = false;
        chkMostrarInfo.Checked = false;
        chkEnviaCorreo.Checked = false;
        chkEnviaCorreo_Nvo.Checked = false;
        chkEsPublico.Checked = false;
        chkEsPublico_Nvo.Checked = false;
        this.txtMensajeCorreo_Nvo.Text = "";
    }

    private void Registrar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Inicio");
            objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.Not_Estado_Notificacion();
            byte deEstado = 1;
            byte deEstadoPDI = 1;
            int diasVencimiento = 0;

            if (chkEstado_Nvo.Checked == false)
            {
                deEstado = 0;
            }

            if (this.ChkEstadoPdI_Nvo.Checked == false)
            {
                diasVencimiento = deEstadoPDI = 0;
            }

            if (!String.IsNullOrEmpty(this.txtDiasVencimiento_Nvo.Text))
            {
               diasVencimiento =  int.Parse(this.txtDiasVencimiento_Nvo.Text);
            }


            objTablasBasicas.Insertar_Not_Estado_Notificacion(txtDescripcion_Nvo.Text, deEstado, deEstadoPDI, diasVencimiento, this.chkMostrarInfo_Nvo.Checked ,this.chkEnviaCorreo_Nvo.Checked, this.txtMensajeCorreo_Nvo.Text, this.chkEsPublico_Nvo.Checked, this.txtDescripcionMostrar_Nvo.Text);
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
}
