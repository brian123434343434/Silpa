using SILPA.AccesoDatos.PINES;
using SILPA.LogicaNegocio.PINES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Tablasbasicas_AccionesActivdadesPINES : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarPagina();
        }
    }
    private void CargarPagina()
    {
        PINES vPINES = new PINES();
        DataTable dtDatos = vPINES.ConsultaActividadesWorkFlowPINES();
        Utilidades.LlenarComboTabla(dtDatos, this.cboActividad, "TAREA", "IDACTIVITY", true);
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Consultar();
    }

    private void Consultar()
    {
        if (this.cboActividad.SelectedValue != "")
        {
            PINES vPINES = new PINES();
            DataTable dtDatos = vPINES.ConsultaAccionActividad(Convert.ToInt32(this.cboActividad.SelectedValue));
            this.grdDatos.DataSource = dtDatos;
            this.grdDatos.DataBind();
        }
        else
        {
            this.grdDatos.DataSource = null;
            this.grdDatos.DataBind();
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        LimpiarNew();
        PINES vPINES = new PINES();
        DataTable dtDatos = vPINES.ConsultaActividadesWorkFlowPINES();
        Utilidades.LlenarComboTabla(dtDatos, this.cboActividadNew, "TAREA", "IDACTIVITY", true);
        this.pnlNuevo.Visible = true;
        
    }
    private void LimpiarEdit()
    {
        this.txtDiasEjecucionEdit.Text = string.Empty;
        this.chkObligatorioEdit.Checked = false;
        this.txtOrdenEdit.Text = string.Empty;
        this.hdfIdAccionEdit.Value = string.Empty;
    }
    private void LimpiarNew()
    {
        this.txtDiasEjecucionNew.Text = string.Empty;
        this.chkObligatorioNew.Checked = false;
        this.cboAccionNew.Items.Clear();
        this.cboAccionNew.Items.Insert(0, new ListItem("Seleccione", ""));
        this.txtOrdenNew.Text = string.Empty;
    }
    protected void cboActividadNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboActividadNew.SelectedValue != "")
        {
            PINES vPINES = new PINES();
            Utilidades.LlenarComboTabla(vPINES.AccionesNoUsadas(Convert.ToInt32(this.cboActividadNew.SelectedValue)), this.cboAccionNew, "DESCRIPCION", "IDACCION", true);
        }
        else
        {
            this.cboAccionNew.Items.Clear();
            this.cboAccionNew.Items.Insert(0, new ListItem("Seleccione", ""));
        }
    }
    protected void btnCancelarReg_Click(object sender, EventArgs e)
    {
        this.pnlNuevo.Visible = false;
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                AccionActividadIdentity vAccionActividadIdentity = new AccionActividadIdentity();
                vAccionActividadIdentity.IdActivity = Convert.ToInt32(this.cboActividadNew.SelectedValue);
                vAccionActividadIdentity.IdAccion = Convert.ToInt32(this.cboAccionNew.SelectedValue);
                vAccionActividadIdentity.DiasEjecucion = Convert.ToInt32(this.txtDiasEjecucionNew.Text);
                vAccionActividadIdentity.Obligatoria = this.chkObligatorioNew.Checked;
                vAccionActividadIdentity.Orden = Convert.ToInt32(this.txtOrdenNew.Text);
                AccionActividad vAccionActividad = new AccionActividad();
                vAccionActividad.Insertar(vAccionActividadIdentity);
                pnlNuevo.Visible = false;
                Consultar();
                ScriptManager.RegisterStartupScript(this,this.GetType(), "Info", "alert('Registro creado con exito')",true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", string.Format("alert('Se genero un error al intentar crear el registro: {0}')", ex.Message), true);
                pnlNuevo.Visible = true;
            }
            
        }
    }
    protected void grdDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Modificar")
            {
                LimpiarEdit();
                PINES vPINES = new PINES();
                DataTable dtDatos = vPINES.ConsultaActividadesWorkFlowPINES();
                Utilidades.LlenarComboTabla(dtDatos, this.cboActividadEdit, "TAREA", "IDACTIVITY", true);
                this.pnlEditar.Visible = true;
                //visualizar informacion para edicion
                pnlEditar.Visible = true;
                //Cargar registro Seleccionado
                int index = Convert.ToInt32(e.CommandArgument);
                int pagina = grdDatos.PageIndex;
                if (index > 9)
                {
                    index = index - (pagina * 10);
                }
                int activity = Convert.ToInt32(grdDatos.DataKeys[index].Value);
                int accion = Convert.ToInt32(((Label)grdDatos.Rows[index].FindControl("lblAccion")).Text);
                AccionActividad vAccionActividad = new AccionActividad();
                AccionActividadIdentity vAccionActividadIdentity = new AccionActividadIdentity { IdActivity = activity, IdAccion = accion };
                vAccionActividad.Consultar(ref vAccionActividadIdentity);
                this.txtDiasEjecucionEdit.Text = vAccionActividadIdentity.DiasEjecucion.ToString();
                this.txtOrdenEdit.Text = vAccionActividadIdentity.Orden != null ? vAccionActividadIdentity.Orden.ToString() : "";
                this.chkObligatorioEdit.Checked = vAccionActividadIdentity.Obligatoria;
                if (!vAccionActividadIdentity.Obligatoria)
                    rfvOrdenEdit.Enabled = false;
                this.cboActividadEdit.SelectedValue = vAccionActividadIdentity.IdActivity.ToString();
                this.txtAccionEdit.Text = grdDatos.Rows[index].Cells[0].Text;
                this.hdfIdAccionEdit.Value = accion.ToString();

            }
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int pagina = grdDatos.PageIndex;
                if (index > 9)
                {
                    index = index - (pagina * 10);
                }
                int activity = Convert.ToInt32(grdDatos.DataKeys[index].Value);
                int accion = Convert.ToInt32(((Label)grdDatos.Rows[index].FindControl("lblAccion")).Text);
                AccionActividad vAccionActividad = new AccionActividad();
                vAccionActividad.Eliminar(new AccionActividadIdentity { IdActivity = activity, IdAccion = accion});
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('Registro eliminado con exito')", true);
                Consultar();
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }

    
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        Validate("Edit");
        if (IsValid)
        {
            try
            {
                AccionActividadIdentity vAccionActividadIdentity = new AccionActividadIdentity();
                vAccionActividadIdentity.IdActivity = Convert.ToInt32(this.cboActividadEdit.SelectedValue);
                vAccionActividadIdentity.IdAccion = Convert.ToInt32(this.hdfIdAccionEdit.Value);
                vAccionActividadIdentity.Obligatoria = this.chkObligatorioEdit.Checked;
                if (this.txtOrdenEdit.Text != string.Empty)
                    vAccionActividadIdentity.Orden = Convert.ToInt32(this.txtOrdenEdit.Text);
                vAccionActividadIdentity.DiasEjecucion = Convert.ToInt32(txtDiasEjecucionEdit.Text);
                AccionActividad vAccionActividad = new AccionActividad();
                vAccionActividad.Actualizar(vAccionActividadIdentity);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('Registro Actualizado con exito')", true);
                Consultar();
                this.pnlEditar.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", string.Format("alert('Se genero un error al intentar Actualizar el registro: {0}')", ex.Message), true);
            }
           
        }
    }

    protected void btnCancelarEdit_Click(object sender, EventArgs e)
    {
        LimpiarEdit();
        this.pnlEditar.Visible = false;
    }
    protected void chkObligatorioEdit_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)sender).Checked)
        {
            this.txtOrdenEdit.Enabled = true;
            rfvOrdenEdit.Enabled = true;
        }
        else
        {
            this.txtOrdenEdit.Enabled = false;
            this.txtOrdenEdit.Text = string.Empty;
            rfvOrdenEdit.Enabled = false;
        }

    }
    protected void chkObligatorioNew_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)sender).Checked)
        {
            this.txtOrdenNew.Enabled = true;
            rfvOrdenNew.Enabled = true;
        }
        else
        {
            this.txtOrdenNew.Enabled = false;
            this.txtOrdenNew.Text = string.Empty;
            rfvOrdenNew.Enabled = false;
        }

    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {

    }
}