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

public partial class Administracion_Tablasbasicas_ActividadRadicable : System.Web.UI.Page
{
    protected ActividadRadicable objTablasBasicas;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            ConsultarInformacion("");
        }
        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ConsultarInformacion(this.txtNombreParametro.Text);
    }

    protected void ConsultarInformacion(string nombre)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new ActividadRadicable();
            grdDatos.DataSource = objTablasBasicas.ListarActividadRadicable(nombre);
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
            if (!Tools.IsNumeric(this.txtTipo.Text))
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser numérico.");
                this.lblMensajeError.Text = "El valor de forma debe ser numérico.";
                return;
            }
            else if (Convert.ToInt32(this.txtTipo.Text) < 0)
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser mayor o igual a cero(0).");
                this.lblMensajeError.Text = "El valor de forma debe ser mayor o igual a cero(0).";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            ActividadRadicable objConsultaRuia = new ActividadRadicable();
            DataTable dtlRuia = objConsultaRuia.ListarActividadRadicable(this.txtNombre.Text);
            if (dtlRuia.Rows.Count > 0)
            {
                string strFiltro = "ID_ACTIVIDAD <> " + this.lblId.Text + " AND NOMBRE_ACTIVIDAD = '" + this.txtNombre.Text + "'";
                //string strFiltro = "NOMBRE_ACTIVIDAD = '" + this.txtNombre.Text + "'";
                DataRow[] dtrRows = dtlRuia.Select(strFiltro);
                if (dtrRows.Length > 0)
                {
                        Mensaje.MostrarMensaje(this, "La actividad radicable ya se encuentra registrada en el sistema.");
                        this.lblMensajeError.Text = "La actividad radicable ya se encuentra registrada en el sistema.";
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
            if (!Tools.IsNumeric(this.txtTipoNvo.Text))
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser numérico.");
                lblMensajeError.Text = "El valor de forma debe ser numérico.";
                return;
            }
            else if (Convert.ToInt32(this.txtTipoNvo.Text) < 0)
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser mayor o igual a cero(0).");
                this.lblMensajeError.Text = "El valor de forma debe ser mayor o igual a cero(0).";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            ActividadRadicable objConsultaRuia = new ActividadRadicable();
            DataTable dtlRuia = objConsultaRuia.ListarActividadRadicable(this.txtNombreNvo.Text);
            if (dtlRuia.Rows.Count > 0)
            {
                foreach (DataRow _temp in dtlRuia.Rows)
                {
                    if (_temp["NOMBRE_ACTIVIDAD"].ToString() == this.txtNombreNvo.Text)
                    {
                        Mensaje.MostrarMensaje(this, "La actividad radicable ya se encuentra registrada en el sistema.");
                        this.lblMensajeError.Text = "La actividad radicable ya se encuentra registrada en el sistema.";
                        return;
                    }
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Registrar.Inicio");
            objTablasBasicas = new ActividadRadicable();

            objTablasBasicas.InsertarActividadRadicable(int.Parse(this.txtTipoNvo.Text), chkEstadoNvo.Checked, this.txtNombreNvo.Text, int.Parse(this.txtIdNvo.Text));
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
        txtNombre.Text = "";
        chkEstado.Checked = true;
        txtNombreNvo.Text = "";
        chkEstadoNvo.Checked = true;
        this.txtTipo.Text = "0";
        this.txtTipoNvo.Text = "0";
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
            objTablasBasicas = new ActividadRadicable();

            objTablasBasicas.ActualizarActividadRadicable(Convert.ToInt32(lblId.Text), Convert.ToInt32(this.txtTipo.Text), this.chkEstado.Checked, this.txtNombre.Text, Convert.ToInt32(txtId.Text));
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Actualizar.Finalizo");
        }
    }

    private void Eliminar(string strId)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Inicio");
            objTablasBasicas = new ActividadRadicable();
            objTablasBasicas.EliminarActividadRadicable(Convert.ToInt32(strId));
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Eliminar.Finalizo");
        }
    }

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion(this.txtNombreParametro.Text);
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
                lblId.Text = grdDatos.SelectedDataKey["ID_ACTIVIDAD"].ToString();
                string strEstado = grdDatos.SelectedDataKey["ACTIVA"].ToString();
                if (strEstado == "True")
                {
                    chkEstado.Checked = true;
                }
                else
                {
                    chkEstado.Checked = false;
                }
                this.txtId.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[0].Text);
                this.txtNombre.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
                this.txtTipo.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[2].Text);
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
                Eliminar(grdDatos.SelectedDataKey["ID_ACTIVIDAD"].ToString());
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
