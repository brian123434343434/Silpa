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

public partial class Administracion_Tablasbasicas_Gen_Tipo_Usuario : System.Web.UI.Page
{
    protected GenTipoUsuario objTablasBasicas;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            this.CargarCombos();
        }
        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarInformacion(this.TxtDescTipoUsu.Text);
        
    }

    protected void ConsultarInformacion(string DescTipoUsuEdit)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new GenTipoUsuario();
            grdDatos.DataSource = objTablasBasicas.ListarGenTipoUsuario(DescTipoUsuEdit);
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

    protected void CargarCombos()
    {
        Gen_Participant objTablas = new Gen_Participant();
        DataTable datos = objTablas.ListarParticipantes();
        //Cargar combo para la edicion del participante
        cboParticipantEdit.DataSource=datos;
        cboParticipantEdit.DataTextField = "NAME";
        cboParticipantEdit.DataValueField = "ID";
        cboParticipantEdit.DataBind();
        cboParticipantEdit.Items.Insert(0, new ListItem("Seleccione...", "0"));
        //cargar combo para la adicion
        cboParticipantNvo.DataSource = datos;
        cboParticipantNvo.DataTextField = "NAME";
        cboParticipantNvo.DataValueField = "ID";
        cboParticipantNvo.DataBind();
        cboParticipantNvo.Items.Insert(0, new ListItem("Seleccione...", "0"));
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtDescTipoUsuEdit.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre de tipo de usuario es requerido.");
                this.lblMensajeError.Text = "El nombre de tipo de usuario es requerido.";
                return;
            }
            if (this.cboParticipantEdit.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El participante es requerido.");
                this.lblMensajeError.Text = "El participante es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            GenTipoUsuario objConsultaRuia = new GenTipoUsuario();
            DataTable dtlRuia = objConsultaRuia.ValidarGenTipoUsuario(Convert.ToInt32(cboParticipantEdit.Text), this.txtDescTipoUsuEdit.Text);
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
            if (this.txtDescTipoUsuNvo.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre de tipo de usuario es requerido.");
                this.lblMensajeError.Text = "El nombre de tipo de usuario es requerido.";
                return;
            }
            if (this.cboParticipantNvo.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El participante es requerido.");
                this.lblMensajeError.Text = "El participante es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            
            //this.lblMensajeError.Text = "El tipo de usuario ya se encuentra registrado en el sistema."+ Convert.ToInt32(cboParticipantNvo.Text);
            GenTipoUsuario objConsultaRuia = new GenTipoUsuario();
            DataTable dtlRuia = objConsultaRuia.ValidarGenTipoUsuario(Convert.ToInt32(cboParticipantNvo.Text), this.txtDescTipoUsuNvo.Text);
            if (dtlRuia.Rows.Count > 0)
            {
                Mensaje.MostrarMensaje(this, "El tipo de usuario ya se encuentra registrado en el sistema.");
                this.lblMensajeError.Text = "El tipo de usuario ya se encuentra registrado en el sistema.";
                return;
            }
            
            Registrar();
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
            this.lblMensajeError.Text = "el " + ex.Message;
        }
    }

    private void Registrar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Inicio");
            objTablasBasicas = new GenTipoUsuario();
            objTablasBasicas.InsertarGenTipoUsuario(Convert.ToInt32(cboParticipantNvo.Text), this.txtDescTipoUsuNvo.Text);
            this.lblMensajeError.Text = "paso ";
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
        TxtDescTipoUsu.Text = "";
        txtDescTipoUsuEdit.Text = "";
        this.cboParticipantEdit.SelectedValue = "0";
        txtDescTipoUsuNvo.Text = "";
        this.cboParticipantNvo.SelectedValue = "0";
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
            objTablasBasicas = new GenTipoUsuario();
            objTablasBasicas.ActualizarGenTipoUsuario(Convert.ToInt32(lblId.Text), Convert.ToInt32(this.cboParticipantEdit.Text), this.txtDescTipoUsuEdit.Text);
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
            objTablasBasicas = new GenTipoUsuario();
            objTablasBasicas.EliminarGenTipoUsuario(Convert.ToInt32(strId));
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
        ConsultarInformacion(this.TxtDescTipoUsu.Text);
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
                lblId.Text = grdDatos.SelectedDataKey["ID_TIPO_USUARIO"].ToString();
                //this.cboParticipantEdit.SelectedValue = grdDatos.SelectedDataKey["ID"].ToString();
                txtDescTipoUsuEdit.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[0].Text);
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
                Eliminar(grdDatos.SelectedDataKey["ID_TIPO_USUARIO"].ToString());
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
