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
using SILPA.Comun;
using SoftManagement.Log;

public partial class Administracion_Tablasbasicas_Gen_Tipo_Documento_Acreditacion : System.Web.UI.Page
{
    protected Gen_Tipo_Doc_Acreditacion objTablasBasicas;

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
        
        ConsultarInformacion(this.txtNombreTipoDocAcreditacion.Text);
        
    }

    protected void ConsultarInformacion(string nombre)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new Gen_Tipo_Doc_Acreditacion();
            grdDatos.DataSource = objTablasBasicas.ListarTipoDocAcreditacion(nombre);
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
        try
        {
            if (this.txtTipoDocAcreditacionEdit.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre es requerido.");
                this.lblMensajeError.Text = "El nombre es requerido.";
                return;
            }
            
            //Se consulta y valida que no se repita la informacion
            Gen_Tipo_Doc_Acreditacion objConsultaRuia = new Gen_Tipo_Doc_Acreditacion();
            DataTable dtlRuia = objConsultaRuia.ListarTipoDocAcreditacion(this.txtTipoDocAcreditacionEdit.Text);
            if (dtlRuia.Rows.Count > 0)
            {
                string strFiltro = "ID <> " + this.lblId.Text;
                DataRow[] dtrRows = dtlRuia.Select(strFiltro);
                if (dtrRows.Length > 0)
                {
                    Mensaje.MostrarMensaje(this, "El tipo de trámite ya se encuentra registrado en el sistema.");
                    this.lblMensajeError.Text = "El tipo de trámite ya se encuentra registrado en el sistema.";
                    return;
                }
            }

            Actualizar();
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtTipoDocAcreditacionNvo.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre es requerido.");
                this.lblMensajeError.Text = "El nombre es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            Gen_Tipo_Doc_Acreditacion objConsultaRuia = new Gen_Tipo_Doc_Acreditacion();
            DataTable dtlRuia = objConsultaRuia.ListarTipoDocAcreditacion(this.txtTipoDocAcreditacionNvo.Text);
            if (dtlRuia.Rows.Count > 0)
            {
                Mensaje.MostrarMensaje(this, "El tipo de documento de acreditaciòn ya se encuentra registrado en el sistema.");
                this.lblMensajeError.Text = "El de documento de acreditaciòn ya se encuentra registrado en el sistema.";
                return;
            }

            Registrar();
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }

    private void Registrar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Inicio");
            objTablasBasicas = new Gen_Tipo_Doc_Acreditacion();

            objTablasBasicas.InsertarTipoDocAcreditacion(this.txtTipoDocAcreditacionNvo.Text);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty);
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
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Finalizo");
        }
    }

    private void limpiarObjetos()
    {

        lblId.Text = "";
        txtNombreTipoDocAcreditacion.Text = "";
        txtTipoDocAcreditacionNvo.Text = "";
        txtTipoDocAcreditacionEdit.Text = "";
    }

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

    private void Actualizar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Inicio");
            objTablasBasicas = new Gen_Tipo_Doc_Acreditacion();
            objTablasBasicas.ActualizarTipoDocAcreditacion(Convert.ToInt32(lblId.Text), this.txtTipoDocAcreditacionEdit.Text);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty);
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
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Inicio");
        }
      

    }

    private void Eliminar(string strId)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Inicio");
            objTablasBasicas = new Gen_Tipo_Doc_Acreditacion();
            objTablasBasicas.EliminarTipoDocAcreditacion(Convert.ToInt32(strId));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty);
            Mensaje.MostrarMensaje(this, "Registro eliminado exitosamente");
            this.lblMensajeError.Text = "Registro eliminado exitosamente";
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Inicio");
        }

    }

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion(this.txtNombreTipoDocAcreditacion.Text);
    }

    protected void grdDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
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
                lblId.Text = grdDatos.SelectedDataKey["ID_TIP_DOC_ACREDITACION"].ToString();
                this.txtTipoDocAcreditacionEdit.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
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
                Eliminar(grdDatos.SelectedDataKey["ID_TIP_DOC_ACREDITACION"].ToString());
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;
    }
}
