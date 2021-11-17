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

public partial class Administracion_Tablasbasicas_GenTipoUsuarioGrupo : System.Web.UI.Page
{
    protected GenTipoUsuarioGrupo objTablasBasicas;

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
        ConsultarInformacion(int.Parse(this.cboTipoUsuario.SelectedValue), int.Parse(this.cboUserGroup.SelectedValue));
    }

    protected void ConsultarInformacion(int tipoUsuario, int userGroup)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new GenTipoUsuarioGrupo();
            grdDatos.DataSource = objTablasBasicas.ListarTipoUsuarioGrupo(tipoUsuario, userGroup);
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
        GenTipoUsuario objTablas = new GenTipoUsuario();
        UserGroup objTablas2 = new UserGroup();
        DataTable datos = objTablas.ListarGenTipoUsuario(0, 0);
        //cargar combos para la consulta Tipo de usuario
        cboTipoUsuario.DataSource = datos;
        cboTipoUsuario.DataTextField = "DESCRIPCION_TIPO_USUARIO";
        cboTipoUsuario.DataValueField = "ID_TIPO_USUARIO";
        cboTipoUsuario.DataBind();
        cboTipoUsuario.Items.Insert(0, new ListItem("Seleccione...", "0"));
        //cargar combos para la edicion Tipo de usuario
        cboTipoUsuarioEdit.DataSource = datos;
        cboTipoUsuarioEdit.DataTextField = "DESCRIPCION_TIPO_USUARIO";
        cboTipoUsuarioEdit.DataValueField = "ID_TIPO_USUARIO";
        cboTipoUsuarioEdit.DataBind();
        cboTipoUsuarioEdit.Items.Insert(0, new ListItem("Seleccione...", "0"));
        //cargar combos para la adicion Tipo de usuario
        cboTipoUsuarioNvo.DataSource = datos;
        cboTipoUsuarioNvo.DataTextField = "DESCRIPCION_TIPO_USUARIO";
        cboTipoUsuarioNvo.DataValueField = "ID_TIPO_USUARIO";
        cboTipoUsuarioNvo.DataBind();
        cboTipoUsuarioNvo.Items.Insert(0, new ListItem("Seleccione...", "0"));
        //cargar combos para la consulta Grupo Usuario
        datos = objTablas2.ListarGenTipoUsuario(0);
        cboUserGroup.DataSource = datos;
        cboUserGroup.DataTextField = "Name";
        cboUserGroup.DataValueField = "id";
        cboUserGroup.DataBind();
        cboUserGroup.Items.Insert(0, new ListItem("Seleccione...", "0"));
        //cargar combos para la edicion Grupo Usuario
        datos = objTablas2.ListarGenTipoUsuario(0);
        cboUserGroupEdit.DataSource = datos;
        cboUserGroupEdit.DataTextField = "Name";
        cboUserGroupEdit.DataValueField = "id";
        cboUserGroupEdit.DataBind();
        cboUserGroupEdit.Items.Insert(0, new ListItem("Seleccione...", "0"));
        //cargar combos para la adicion Grupo Usuario
        datos = objTablas2.ListarGenTipoUsuario(0);
        cboUserGroupNvo.DataSource = datos;
        cboUserGroupNvo.DataTextField = "Name";
        cboUserGroupNvo.DataValueField = "id";
        cboUserGroupNvo.DataBind();
        cboUserGroupNvo.Items.Insert(0, new ListItem("Seleccione...", "0"));
        
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.cboTipoUsuarioEdit.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El Tipo de Usuario es requerido.");
                this.lblMensajeError.Text = "El Tipo de Usuario es requerido.";
                return;
            }
            if (this.cboUserGroupEdit.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El Grupo de Usuario es requerido.");
                this.lblMensajeError.Text = "El Grupo de Usuario es requerido.";
                return;
            }
            ////Se consulta y valida que no se repita la informacion
            //GenTipoUsuarioGrupo objConsultaRuia = new GenTipoUsuarioGrupo();
            //DataTable dtlRuia = objConsultaRuia.ListarTipoUsuarioGrupo(int.Parse(this.cboTipoUsuarioEdit.SelectedValue), int.Parse(this.cboUserGroupEdit.SelectedValue));
            ////if (dtlRuia.Rows.Count > 0)
            //{
            //    //string strFiltro = "ID <> " + this.lblId.Text;
            //    //DataRow[] dtrRows = dtlRuia.Select(strFiltro);
            //    if (dtrRows.Length > 0)
            //    {
            //        Mensaje.MostrarMensaje(this, "El tipo de Grupo de Usuarios ya se encuentra registrado en el sistema.");
            //        this.lblMensajeError.Text = "El tipo de Grupo de Usuarios ya se encuentra registrado en el sistema.";
            //        return;
            //    }
            //}

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
            if (this.cboTipoUsuarioNvo.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El nombre es requerido.");
                this.lblMensajeError.Text = "El nombre es requerido.";
                return;
            }
            if (this.cboUserGroupNvo.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El proceso es requerido.");
                this.lblMensajeError.Text = "El proceso es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            GenTipoUsuarioGrupo objConsultaRuia = new GenTipoUsuarioGrupo();
            DataTable dtlRuia = objConsultaRuia.ListarTipoUsuarioGrupo(int.Parse(this.cboTipoUsuarioNvo.SelectedValue), int.Parse(this.cboUserGroupNvo.SelectedValue));
            if (dtlRuia.Rows.Count > 0)
            {
                string strFiltro = "ID <> " + this.lblIdTU.Text;
                DataRow[] dtrRows = dtlRuia.Select(strFiltro);
                if (dtrRows.Length > 0)
                {
                    Mensaje.MostrarMensaje(this, "El tipo de Grupo de Usuarios ya se encuentra registrado en el sistema.");
                    this.lblMensajeError.Text = "El tipo de Grupo de Usuarios ya se encuentra registrado en el sistema.";
                    return;
                }
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
            this.lblMensajeError.Text = "entro";
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Inicio");
            objTablasBasicas = new GenTipoUsuarioGrupo();
            objTablasBasicas.InsertarTipoTramite(int.Parse(this.cboTipoUsuarioNvo.SelectedValue), int.Parse(this.cboUserGroupNvo.SelectedValue));
            this.lblMensajeError.Text = "se consulto";
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0, 0);
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
            this.lblMensajeError.Text = "Error:"+ex.Message;
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Finalizo");
        }
    }

    private void limpiarObjetos()
    {

        this.cboTipoUsuarioEdit.SelectedValue = "0";
        this.cboUserGroupEdit.SelectedValue = "0";
        this.cboTipoUsuario.SelectedValue = "0";
        this.cboUserGroup.SelectedValue = "0";
    }

    private void Cancelar()
    {
        ConsultarInformacion(0, 0);
        grdDatos.SelectedIndex = -1;
        pnlConsultar.Visible = true;
        pnlMaestro.Visible = true;
        pnlEditar.Visible = false;
        pnlNuevo.Visible = false;
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
            objTablasBasicas = new GenTipoUsuarioGrupo();
            objTablasBasicas.ActualizarTipoUsuarioGrupo(int.Parse(this.cboTipoUsuarioEdit.SelectedValue), int.Parse(this.cboUserGroupEdit.SelectedValue), Convert.ToInt32(lblIdTU.Text), Convert.ToInt32(lblIdUG.Text));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0, 0);
            pnlConsultar.Visible = true;
            pnlMaestro.Visible = true;
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

    private void Eliminar(string strIdTipoUsuario, string strIdUserGroup)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Inicio");
            objTablasBasicas = new GenTipoUsuarioGrupo();
            objTablasBasicas.EliminarTipoUsuarioGrupo(Convert.ToInt32(strIdTipoUsuario), Convert.ToInt32(strIdUserGroup));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0, 0);
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
        ConsultarInformacion(int.Parse(this.cboTipoUsuario.SelectedValue), int.Parse(this.cboUserGroup.SelectedValue));
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
                lblIdTU.Text = grdDatos.SelectedDataKey["ID_TIPO_USUARIO"].ToString();
                lblIdUG.Text = grdDatos.SelectedDataKey["IDUserGroup"].ToString();
                this.cboTipoUsuarioEdit.SelectedValue = grdDatos.SelectedDataKey["ID_TIPO_USUARIO"].ToString();
                this.cboUserGroupEdit.SelectedValue = grdDatos.SelectedDataKey["IDUserGroup"].ToString();
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
                Eliminar(grdDatos.SelectedDataKey["ID_TIPO_USUARIO"].ToString(), grdDatos.SelectedDataKey["IDUserGroup"].ToString());
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
