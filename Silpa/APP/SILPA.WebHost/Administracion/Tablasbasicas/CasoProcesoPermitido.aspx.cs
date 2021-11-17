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
using System.Globalization;
using SoftManagement.Log;

public partial class Administracion_Tablasbasicas_CasoProcesoPermitido : System.Web.UI.Page
{
    protected CasoProcesoPermitido objTablasBasicas;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            ConsultarInformacion(string.Empty, string.Empty, string.Empty);
           
        }
        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarInformacion(this.txtNombreParametro.Text.Trim(), this.txtFechaDesde.Text.Trim(), this.txtFechaHasta.Text.Trim());
    }

    protected void ConsultarInformacion(string nombre, string fechaInicial, string fechaFinal)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new CasoProcesoPermitido();
            grdDatos.DataSource = objTablasBasicas.ListarCasoProcesoPermitido(nombre, fechaInicial, fechaFinal);
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizar_Click.Inicio");
            if (this.txtNombre.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El nombre es requerido.");
                this.lblMensajeError.Text = "El nombre es requerido.";
                return;
            }
            if (this.txtFecha.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "La fecha es requerida.");
                this.lblMensajeError.Text = "La fecha es requerida.";
                return;
            }
            else
            {
                DateTime fecha;
                if (!DateTime.TryParse(this.txtFecha.Text.Trim(), out fecha))
                {
                    Mensaje.MostrarMensaje(this, "El formato de fecha no es válido.");
                    this.lblMensajeError.Text = "El formato de fecha no es válido";
                    return;
                }
            }
            //Se consulta y valida que no se repita la informacion
            CasoProcesoPermitido objConsultaRuia = new CasoProcesoPermitido();
            DataTable dtlRuia = objConsultaRuia.ListarCasoProcesoPermitido(this.txtNombre.Text, string.Empty, string.Empty);
            if (dtlRuia.Rows.Count > 0)
            {
                //string strFiltro = "PRO_ID_CASO_PROCESO <> " + this.lblId.Text;
                string strFiltro = "PRO_ID_CASO_PROCESO <> " + this.cboNombreCaso.SelectedValue + " AND PRO_CLAVE_PROCESO = '" + this.txtNombre.Text.Trim() + "'";
                DataRow[] dtrRows = dtlRuia.Select(strFiltro);
                if (dtrRows.Length > 0)
                {
                    Mensaje.MostrarMensaje(this, "El caso de proceso permitido ya se encuentra registrado en el sistema.");
                    this.lblMensajeError.Text = "El caso de proceso permitido ya se encuentra registrado en el sistema.";
                    return;
                }
            }

            Actualizar();
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnActualizar_Click.Finalizo");
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
            if (this.txtFechaNvo.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "La fecha es requerida.");
                this.lblMensajeError.Text = "La fecha es requerida.";
                return;
            }
            else
            {
                DateTime fecha;
                if (!DateTime.TryParse(this.txtFechaNvo.Text.Trim(), out fecha))
                {
                    Mensaje.MostrarMensaje(this, "El formato de fecha no es válido.");
                    this.lblMensajeError.Text = "El formato de fecha no es válido.";
                    return;
                }
            }
            //Se consulta y valida que no se repita la informacion
            CasoProcesoPermitido objConsultaRuia = new CasoProcesoPermitido();
            DataTable dtlRuia = objConsultaRuia.ListarCasoProcesoPermitido(this.txtNombreNvo.Text, string.Empty, string.Empty);
            if (dtlRuia.Rows.Count > 0)
            {
                foreach (DataRow _Temp in dtlRuia.Rows)
                {
                    if (_Temp["PRO_CLAVE_PROCESO"].ToString() == txtNombreParametro.Text)
                    {
                        Mensaje.MostrarMensaje(this, "La clave de proceso permitido ya se encuentra registrado en el sistema.");
                        this.lblMensajeError.Text = "La clave de proceso permitido ya se encuentra registrado en el sistema.";
                        return;
                    }
                }
            }
            if (Convert.ToInt32(cboNombreCaso_Nvo.SelectedValue) == 0)
            {
                Mensaje.MostrarMensaje(this, "Debe seleccionar un nombre de caso.");
                this.lblMensajeError.Text = "Debe seleccionar un nombre de caso.";
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
            objTablasBasicas = new CasoProcesoPermitido();

            objTablasBasicas.InsertarCasoProcesoPermitido(Convert.ToInt32(cboNombreCaso_Nvo.SelectedValue), this.txtFechaNvo.Text, chkEstadoNvo.Checked, this.txtNombreNvo.Text, this.chkTipoEntidad_Nvo.Checked);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty, string.Empty, string.Empty);
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
        this.txtFecha.Text = "";
        this.txtFechaNvo.Text = "";
    }

    private void Cancelar()
    {
        ConsultarInformacion(string.Empty, string.Empty, string.Empty);
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
            objTablasBasicas = new CasoProcesoPermitido();

            objTablasBasicas.ActualizarCasoProcesoPermitido(Convert.ToInt32(lblId.Text), Convert.ToInt32(cboNombreCaso.SelectedValue), this.txtFecha.Text, this.chkEstado.Checked, this.txtNombre.Text, this.chkTipoEntidad.Checked);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty, string.Empty, string.Empty);
            pnlConsultar.Visible = true;
            pnlMaestro.Visible = true;
            pnlNuevo.Visible = false;
            pnlEditar.Visible = false;
            Mensaje.MostrarMensaje(this, "Registro modificado exitosamente");
            this.lblMensajeError.Text = "Registro modificado exitosamente";
            limpiarObjetos();
            
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
            objTablasBasicas = new CasoProcesoPermitido();
            objTablasBasicas.EliminarCasoProcesoPermitido(Convert.ToInt32(strId));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(string.Empty, string.Empty, string.Empty);
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
        ConsultarInformacion(this.txtNombreParametro.Text.Trim(), this.txtFechaDesde.Text.Trim(), this.txtFechaHasta.Text.Trim());
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
                lblId.Text = grdDatos.SelectedDataKey["PRO_ID_CASO_PROCESO"].ToString();
                string strEstado = grdDatos.SelectedDataKey["PRO_ACTIVO"].ToString();
                string strTipoEntidad = grdDatos.SelectedDataKey["ID_TIPO_ENTIDAD"].ToString();
                
                if (strEstado == "1")
                {
                    chkEstado.Checked = true;
                }
                else
                {
                    chkEstado.Checked = false;
                }

                if (strTipoEntidad == "1")
                    chkTipoEntidad.Checked = true;
                else
                    chkTipoEntidad.Checked = false;
 
                objTablasBasicas = new CasoProcesoPermitido();
                this.cboNombreCaso.DataSource = objTablasBasicas.CargarCboCasoProcesoPermitido(Convert.ToInt32(lblId.Text));
                this.cboNombreCaso.DataTextField = "Name";
                this.cboNombreCaso.DataValueField = "ID";
                this.cboNombreCaso.DataBind();
                this.cboNombreCaso.SelectedValue = lblId.Text;
                this.txtFecha.Text = DateTime.Parse(HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[2].Text)).ToShortDateString();
                this.txtNombre.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
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
                Eliminar(grdDatos.SelectedDataKey["PRO_ID_CASO_PROCESO"].ToString());
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

        objTablasBasicas = new CasoProcesoPermitido();

        cboNombreCaso_Nvo.DataSource = objTablasBasicas.CargarCboCasoProcesoPermitido(0);
        cboNombreCaso_Nvo.DataTextField = "Name";
        cboNombreCaso_Nvo.DataValueField = "ID";
        cboNombreCaso_Nvo.DataBind();
        cboNombreCaso_Nvo.Items.Insert(0, new ListItem("Seleccione...", "0"));   
    }
}
