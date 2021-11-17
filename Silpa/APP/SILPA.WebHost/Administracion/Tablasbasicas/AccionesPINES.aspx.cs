using SILPA.AccesoDatos.PINES;
using SILPA.LogicaNegocio.PINES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Tablasbasicas_AccionesPINES : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        Validate("Edit");
        if (IsValid)
        {
            AccionIdentity vAccionIdentity = new AccionIdentity();
            vAccionIdentity.IDAccion = Convert.ToInt32(this.hdfIdAccionEdit.Value);
            vAccionIdentity.NombreAccion = this.txtAccionEdit.Text;
            Accion vAccion = new Accion();
            try
            {
                vAccion.Actualizar(vAccionIdentity);
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
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        Validate("New");
        if (IsValid)
        {
            try
            {
                AccionIdentity vAccionIdentity = new AccionIdentity();
                vAccionIdentity.NombreAccion = this.txtAccionNew.Text;
                Accion vAccion = new Accion();
                vAccion.Insertar(vAccionIdentity);
                pnlNuevo.Visible = false;
                Consultar();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('Registro creado con exito')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", string.Format("alert('Se genero un error al intentar crear el registro: {0}')", ex.Message), true);
                pnlNuevo.Visible = true;
            }

        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Consultar();
    }

    private void Consultar()
    {
        Accion vAccion = new Accion();
        var LstAcciones = vAccion.ListaAcciones(this.txtNombreAccion.Text != string.Empty? this.txtNombreAccion.Text:null);
        this.grdDatos.DataSource = LstAcciones;
        this.grdDatos.DataBind();
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        LimpiarNew();
        this.pnlNuevo.Visible = true;
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("TablasBasicas.aspx");
    }
    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdDatos.PageIndex = e.NewPageIndex;
        Consultar();
    }
    protected void grdDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Modificar")
            {
                LimpiarEdit();
                
                
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
                this.txtAccionEdit.Text = grdDatos.Rows[index].Cells[0].Text;
                this.hdfIdAccionEdit.Value = Convert.ToInt32(grdDatos.DataKeys[index].Value).ToString();

            }
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int pagina = grdDatos.PageIndex;
                if (index > 9)
                {
                    index = index - (pagina * 10);
                }
                int Idaccion = Convert.ToInt32(grdDatos.DataKeys[index].Value);
                Accion vAccion = new Accion();
                vAccion.Eliminar(new SILPA.AccesoDatos.PINES.AccionIdentity { IDAccion = Idaccion});
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('Registro eliminado con exito')", true);
                Consultar();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", string.Format("alert('{0}')", ex.Message),true);
        }
    }

    private void LimpiarEdit()
    {
        this.hdfIdAccionEdit.Value = "";
        this.txtAccionEdit.Text = string.Empty;
    }
    private void LimpiarNew()
    {
        this.txtAccionNew.Text = string.Empty;
    }
    protected void btnCancelarEdit_Click(object sender, EventArgs e)
    {
        LimpiarEdit();
        this.pnlEditar.Visible = false;
    }
    protected void btnCancelarReg_Click(object sender, EventArgs e)
    {
        LimpiarNew();
        this.pnlNuevo.Visible = false;
    }
}