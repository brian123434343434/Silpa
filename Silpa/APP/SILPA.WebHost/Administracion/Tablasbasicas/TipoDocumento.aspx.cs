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
//using SILPA.Comun;

public partial class Administracion_Tablasbasicas_TipoDocumento : System.Web.UI.Page
{
    protected TipoDocumento objTablasBasicas;

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
        ConsultarInformacion(this.txtNombreParametro.Text, int.Parse(this.cboTipoParametro.SelectedValue));
    }

    protected void CargarCombos()
    {
        TablasBasicas objTablas = new TablasBasicas();
        DataTable datos = objTablas.Listar_Bpm_Parametros(string.Empty);
        TipoTramite objTablasBasicas = new TipoTramite();


        FlujoNotificacionElectronica objFlujNotElect = new FlujoNotificacionElectronica();
        DataTable dtflujonotif = objFlujNotElect.ConsultaFlujosNotificacion(null, null, "");

        //cargar combo para la edicion
        cboTipo.DataSource = datos;
        cboTipo.DataTextField = "NOMBRE";
        cboTipo.DataValueField = "ID";
        cboTipo.DataBind();
        cboTipo.Items.Insert(0, new ListItem("Seleccione...", "0"));

        //cargar combo para la adicion
        cboTipoNvo.DataSource = datos;
        cboTipoNvo.DataTextField = "NOMBRE";
        cboTipoNvo.DataValueField = "ID";
        cboTipoNvo.DataBind();
        cboTipoNvo.Items.Insert(0, new ListItem("Seleccione...", "0"));

        //cargar combo para la consulta
        cboTipoParametro.DataSource = datos;
        cboTipoParametro.DataTextField = "NOMBRE";
        cboTipoParametro.DataValueField = "ID";
        cboTipoParametro.DataBind();
        cboTipoParametro.Items.Insert(0, new ListItem("Seleccione...", "0"));

        //cargar combo para Tipo Notificación Electrónica
        cboTipoNotElec.DataSource = dtflujonotif;
        cboTipoNotElec.DataTextField = "FLUJO_NOT_ELEC_DESC";
        cboTipoNotElec.DataValueField = "ID_FLUJO_NOT_ELEC";
        cboTipoNotElec.DataBind();
        cboTipoNotElec.Items.Insert(0, new ListItem("Seleccione...", "0"));

        // cargamos el combo de las condiciones especiales de tipo exclusion documental edicion
        cboCodigoExDocEdit.DataSource = objTablasBasicas.ListarCondicionesEspeciales(null, null, 1);
        cboCodigoExDocEdit.DataTextField = "CONDICION";
        cboCodigoExDocEdit.DataValueField = "CON_ID";
        cboCodigoExDocEdit.DataBind();
        cboCodigoExDocEdit.Items.Insert(0, new ListItem("Seleccione...", "0"));

        // cargamos el combo de las condiciones especiales de tipo exclusion documental nuevo
        cboCodigoExDocNew.DataSource = objTablasBasicas.ListarCondicionesEspeciales(null, null, 1);
        cboCodigoExDocNew.DataTextField = "CONDICION";
        cboCodigoExDocNew.DataValueField = "CON_ID";
        cboCodigoExDocNew.DataBind();
        cboCodigoExDocNew.Items.Insert(0, new ListItem("Seleccione...", "0"));
        
    }

    protected void ConsultarInformacion(string nombre, int tipo)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new TipoDocumento();
            grdDatos.DataSource = objTablasBasicas.ListarTipoDocumento(tipo, nombre);
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
            if (this.txtNombre.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre es requerido.");
                this.lblMensajeError.Text = "El nombre es requerido.";
                return;
            }
            if (this.cboTipo.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El parámetro es requerido.");
                this.lblMensajeError.Text = "El parámetro es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            TipoDocumento objConsulta = new TipoDocumento();
            DataTable dtl = objConsulta.ListarTipoDocumento(0, this.txtNombre.Text);
            if (dtl.Rows.Count > 0)
            {
                string strFiltro = "ID <> " + this.lblId.Text;
                DataRow[] dtrRows = dtl.Select(strFiltro);
                if (dtrRows.Length > 0)
                {
                    Mensaje.MostrarMensaje(this, "El tipo de documento ya se encuentra registrado en el sistema.");
                    this.lblMensajeError.Text = "El tipo de documento ya se encuentra registrado en el sistema.";
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
            if (this.txtNombreNvo.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre es requerido.");
                this.lblMensajeError.Text = "El nombre es requerido.";
                return;
            }
            if (this.cboTipoNvo.SelectedValue == "0")
            {
                Mensaje.MostrarMensaje(this, "El parámetro es requerido.");
                this.lblMensajeError.Text = "El parámetro es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            TipoDocumento objConsulta = new TipoDocumento();
            DataTable dtl = objConsulta.ListarTipoDocumento(0, this.txtNombreNvo.Text);

            int count = 0;
            foreach (DataRow theRow in dtl.Rows)
            {
                if (theRow["NOMBRE_DOCUMENTO"].ToString().Trim().ToLower() == this.txtNombreNvo.Text.Trim().ToLower())
                {
                    count += 1;
                    break;
                }
            }

            if (count > 0)
            {
                Mensaje.MostrarMensaje(this, "El tipo de documento ya se encuentra registrado en el sistema.");
                this.lblMensajeError.Text = "El tipo de documento ya se encuentra registrado en el sistema.";
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
            objTablasBasicas = new TipoDocumento();

            int? condicionEspecialID = null;
            if (this.cboCodigoExDocNew.SelectedValue != "0")
                condicionEspecialID = Convert.ToInt32(this.cboCodigoExDocNew.SelectedValue);

            objTablasBasicas.InsertarTipoDocumento(int.Parse(this.cboTipoNvo.SelectedValue), chkEstadoNvo.Checked, this.txtNombreNvo.Text, this.txtConvenioNvo.Text, int.Parse(cboTipoNotElec.SelectedValue), condicionEspecialID);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty, 0);
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
        txtNombre.Text = "";
        chkEstado.Checked = true;
        txtNombreNvo.Text = "";
        chkEstadoNvo.Checked = true;
        this.cboTipo.SelectedValue = "0";
        this.cboTipoNvo.SelectedValue = "0";
        this.cboCodigoExDocNew.SelectedValue = "0";
    }

    private void Cancelar()
    {
        ConsultarInformacion(string.Empty, 0);
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
            objTablasBasicas = new TipoDocumento();
            int? condicionEspecialID = null;
            if (this.cboCodigoExDocEdit.SelectedValue != "0")
                condicionEspecialID = Convert.ToInt32(this.cboCodigoExDocEdit.SelectedValue);

            objTablasBasicas.ActualizarTipoDocumento(Convert.ToInt32(lblId.Text), Convert.ToInt32(this.cboTipo.SelectedValue), this.chkEstado.Checked, this.txtNombre.Text, this.txtConvenio.Text, int.Parse(cboTipoNotElec.SelectedValue), condicionEspecialID);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty, 0);
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Finalizo");
        }
    }

    private void Eliminar(string strId)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Inicio");
            objTablasBasicas = new TipoDocumento();
            objTablasBasicas.EliminarTipoDocumento(Convert.ToInt32(strId));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty, 0);
            Mensaje.MostrarMensaje(this, "Registro eliminado exitosamente");
            this.lblMensajeError.Text = "Registro eliminado exitosamente";
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Finalizo");
        }
    }

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion(this.txtNombreParametro.Text, int.Parse(this.cboTipoParametro.SelectedValue));
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
                //TIS_ID_TIPO,TIS_ACTIVO
                grdDatos.SelectedIndex = index;
                lblId.Text = grdDatos.SelectedDataKey["ID"].ToString();

                string strEstado = grdDatos.SelectedDataKey["HABILITADO_REPOSICION"].ToString();
                if (strEstado == "True")
                {
                    chkEstado.Checked = true;
                }
                else
                {
                    chkEstado.Checked = false;
                }

                string strIdParametro = grdDatos.SelectedDataKey["ID_BPM_PARAMETRO"].ToString();
                if (strIdParametro == "")
                {
                    this.cboTipo.SelectedValue = "0";
                }
                else
                {
                    this.cboTipo.SelectedValue = grdDatos.SelectedDataKey["ID_BPM_PARAMETRO"].ToString();
                }

                string strIdFlujoNotElec = grdDatos.SelectedDataKey["ID_FLUJO_NOT_ELEC"].ToString();

                if (strIdFlujoNotElec == "")
                {
                    this.cboTipoNotElec.SelectedValue = "0";
                }
                else
                {
                    this.cboTipoNotElec.SelectedValue = grdDatos.SelectedDataKey["ID_FLUJO_NOT_ELEC"].ToString();
                }

                string intCondicionEspecial = grdDatos.SelectedDataKey["CON_ID"].ToString();
                if (intCondicionEspecial == "")
                {
                    this.cboCodigoExDocEdit.SelectedValue = "0";
                }
                else
                {
                    this.cboCodigoExDocEdit.SelectedValue = grdDatos.SelectedDataKey["CON_ID"].ToString();
                }

                this.txtNombre.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[0].Text);
                this.txtConvenio.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);

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
            Mensaje.ErrorCritico(this, ex);
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;
    }

    protected void Button1_Click(object sender, EventArgs e)
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
                Mensaje.MostrarMensaje(this, "El parámetro es requerido.");
                this.lblMensajeError.Text = "El parámetro es requerido.";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            TipoDocumento objConsulta = new TipoDocumento();
            DataTable dtl = objConsulta.ListarTipoDocumento(0, this.txtNombre.Text);
            if (dtl.Rows.Count > 0)
            {
                string strFiltro = "ID <> " + this.lblId.Text;
                DataRow[] dtrRows = dtl.Select(strFiltro);
                if (dtrRows.Length > 0)
                {
                    Mensaje.MostrarMensaje(this, "El tipo de documento ya se encuentra registrado en el sistema.");
                    this.lblMensajeError.Text = "El tipo de documento ya se encuentra registrado en el sistema.";
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
}

