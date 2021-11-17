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

public partial class Administracion_Tablasbasicas_TipoTramite : System.Web.UI.Page
{
    protected TipoTramite objTablasBasicas;

    protected void Page_Load(object sender, EventArgs e)
    {

        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
        if (!Page.IsPostBack)
        {
            this.CargarCombos();
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarInformacion(int.Parse(this.cboTipoParametro.SelectedValue), this.txtNombreParametro.Text);
    }

    protected void ConsultarInformacion(int tipo, string nombre)
    {
        try
        {            
            objTablasBasicas = new TipoTramite();
            grdDatos.DataSource = objTablasBasicas.ListarTipoTramite(tipo, nombre);
            grdDatos.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            Mensaje.MostrarMensaje(this.Page, Page.AppRelativeVirtualPath.ToString() + "ConsultarInformacion -- " + "Ha ocurrido un error, comuniquese con el administrador.");
        }        
    }

    protected void CargarCombos()
    {
        CasoProcesoPermitido objTablas = new CasoProcesoPermitido();
        DataTable datos = objTablas.ListarCasoProcesoPermitido(string.Empty, string.Empty, string.Empty);
        //cargar combo para la edicion
        cboTipo.DataSource = datos;
        cboTipo.DataTextField = "PRO_CLAVE_PROCESO";
        cboTipo.DataValueField = "PRO_ID_CASO_PROCESO";
        cboTipo.DataBind();
        cboTipo.Items.Insert(0, new ListItem("Seleccione...", "0"));

        //cargar combo para la adicion
        cboTipoNvo.DataSource = datos;
        cboTipoNvo.DataTextField = "PRO_CLAVE_PROCESO";
        cboTipoNvo.DataValueField = "PRO_ID_CASO_PROCESO";
        cboTipoNvo.DataBind();
        cboTipoNvo.Items.Insert(0, new ListItem("Seleccione...", "0"));

        //cargar combo para la consulta
        cboTipoParametro.DataSource = datos;
        cboTipoParametro.DataTextField = "PRO_CLAVE_PROCESO";
        cboTipoParametro.DataValueField = "PRO_ID_CASO_PROCESO";
        cboTipoParametro.DataBind();
        cboTipoParametro.Items.Insert(0, new ListItem("Seleccione...", "0"));
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtNombre.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre es requerido.");
                this.lblMensajeError.Text = "El nombre es requerido.";
                return;
            }
            if (this.cboTipo.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El proceso es requerido.");
                this.lblMensajeError.Text = "El proceso es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            TipoTramite objConsultaRuia = new TipoTramite();
            DataTable dtlRuia = objConsultaRuia.ListarTipoTramite(0, this.txtNombre.Text);
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + " -- Actualizar() -- " + ex.ToString());
            Mensaje.MostrarMensaje(this, "Error Comuniquese con el administrador");
        }
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txtNombreNvo.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre es requerido.");
                this.lblMensajeError.Text = "El nombre es requerido.";
                return;
            }
            if (this.cboTipoNvo.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El proceso es requerido.");
                this.lblMensajeError.Text = "El proceso es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            TipoTramite objConsultaRuia = new TipoTramite();
            DataTable dtlRuia = objConsultaRuia.ListarTipoTramite(0, this.txtNombreNvo.Text);
            if (dtlRuia.Rows.Count > 0)
            {
                Mensaje.MostrarMensaje(this, "El tipo de trámite ya se encuentra registrado en el sistema.");
                this.lblMensajeError.Text = "El tipo de trámite ya se encuentra registrado en el sistema.";
                return;
            }

            Registrar();
        }  
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "btnRegistra_clik -- " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error, comuniquese con el administrador.");
        }
    }

    private void Registrar()
    {
        try
        {            
            objTablasBasicas = new TipoTramite();

            objTablasBasicas.InsertarTipoTramite(int.Parse(this.cboTipoNvo.Text), this.txtNombreNvo.Text, this.chkMostrarDocu.Checked);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0, string.Empty);
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "Registrar -- " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, Page.AppRelativeVirtualPath.ToString() + "ConsultarInformacion -- " + "Ha ocurrido un error, comuniquese con el administrador.");
        }       
    }

    private void limpiarObjetos()
    {
        lblId.Text = "";
        txtNombre.Text = "";
        txtNombreNvo.Text = "";
        this.cboTipo.SelectedValue = "0";
        this.cboTipoNvo.SelectedValue = "0";
    }

    private void Cancelar()
    {
        ConsultarInformacion(0, string.Empty);
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
            objTablasBasicas = new TipoTramite();
            objTablasBasicas.ActualizarTipoTramite(Convert.ToInt32(lblId.Text), Convert.ToInt32(this.cboTipo.Text), this.txtNombre.Text,this.chkVisible.Checked, this.chkMostrarDocu.Checked);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0, string.Empty);
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + " -- Actualizar() -- " + ex.ToString());
            Mensaje.MostrarMensaje(this, "Error Comuniquese con el administrador");
        }      
      
    }

    private void Eliminar(string strId)
    {
        try
        {
            objTablasBasicas = new TipoTramite();
            objTablasBasicas.EliminarTipoTramite(Convert.ToInt32(strId));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0, string.Empty);
            Mensaje.MostrarMensaje(this, "Registro eliminado exitosamente");
            this.lblMensajeError.Text = "Registro eliminado exitosamente";
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + " -- Eliminar() -- " + ex.ToString());
            Mensaje.MostrarMensaje(this, "Error Comuniquese con el administrador");
        }      
       
    }

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion(int.Parse(this.cboTipoParametro.SelectedValue), this.txtNombreParametro.Text);
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
                    index = index - (pagina *10);
                }

                grdDatos.SelectedIndex = index;
                lblId.Text = grdDatos.SelectedDataKey["ID"].ToString();
                string prueba = grdDatos.SelectedDataKey["ID_TIPO_PROCESO"].ToString();

                try
                {
                    if (grdDatos.SelectedDataKey["ID_TIPO_PROCESO"].ToString() != "")
                        this.cboTipo.SelectedValue = grdDatos.SelectedDataKey["ID_TIPO_PROCESO"].ToString();
                }
                catch { }
                this.txtNombre.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[0].Text);
                this.chkVisible.Checked = ((CheckBox)(grdDatos.Rows[index].Cells[2].Controls[0])).Checked;
                this.chkMostrarDocu.Checked = ((CheckBox)(grdDatos.Rows[index].Cells[3].Controls[0])).Checked;
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
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + " -- Actualizar() -- " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Error Comuniquese con el administrador");
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;
    }
}
