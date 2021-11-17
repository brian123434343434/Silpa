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

public partial class Administracion_Tablasbasicas_CondicionesEspeciales : System.Web.UI.Page
{
    protected TipoTramite objTablasBasicas;

    protected void Page_Load(object sender, EventArgs e)
    {

        Mensaje.LimpiarMensaje(this);
        this.lblMensajeModal.Text = "";
        if (!Page.IsPostBack)
        {
            this.CargarCombos();
            this.ConsultarInformacion();
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {

        ConsultarInformacion();
    }

    protected void ConsultarInformacion()
    {
        try
        {            
            int? condicion = null, tipoCondicion = null;
            string codigoCondicion = null;            
            if (this.cboCondicion.SelectedValue != "")
                condicion = int.Parse(this.cboCondicion.SelectedValue);
            if (this.cboTipoCondicion.SelectedValue != "")
                tipoCondicion = int.Parse(this.cboTipoCondicion.SelectedValue);
            objTablasBasicas = new TipoTramite();
            grdDatos.DataSource = objTablasBasicas.ListarCondicionesEspeciales(condicion, codigoCondicion, tipoCondicion);
            grdDatos.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "ConsultarInformacion -- " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page,"Ha ocurrido un error, comuniquese con el administrador.");
        }        
    }

    protected void CargarCombos()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();   
        SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();

        DataTable _temp = objtramites.CondicionesSinActividades();

        
        
        cboCondicion.DataSource = _temp;
        cboCondicion.DataValueField = "ID";
        cboCondicion.DataTextField = "Name";
        cboCondicion.DataBind();
        cboCondicion.Items.Insert(0, new ListItem("Seleccione...", ""));

        cboConIns.DataSource = _temp;
        cboConIns.DataValueField = "ID";
        cboConIns.DataTextField = "Name";
        cboConIns.DataBind();
        cboConIns.Items.Insert(0, new ListItem("Seleccione...", ""));
    
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        try
        {          
            if (this.cboConIns.SelectedValue == "")
            {
                MostrarMensajeModal("La condición es requerida.");
                return;
            }
            if (this.txtCodigoIns.Text == "")
            {
                MostrarMensajeModal("El código condición es requerido.");
                return;
            }
            if (this.cboTipoCondicionNewEdit.SelectedValue == "")
            {
                MostrarMensajeModal("Debe seleccionar el tipo de condición.");
                return;
            }
            Registrar();
        }  
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "btnRegistra_clik -- " + ex.ToString());
            MostrarMensaje("Ha ocurrido un error, comuniquese con el administrador.");
        }
    }

    /// <summary>
    /// Mostrar el mensaje especificado
    /// </summary>
    /// <param name="p_strMensaje">string con el mensaje</param>
    private void MostrarMensajeModal(string p_strMensaje)
    {
        this.lblMensajeModal.Text = p_strMensaje;
        this.divMensajeModal.Visible = true;
        this.upnlFormulario.Update();
    }

    private void MostrarMensaje(string p_strMensaje)
    {
        this.lblMensaje.Text = p_strMensaje;
        this.divMensaje.Visible = true;
        this.upnlMensajes.Update();
    }

    private void Registrar()
    {
        try
        {   
            objTablasBasicas = new TipoTramite();
            if(this.hdfCondicionID.Value != string.Empty)
            {
                objTablasBasicas.ActualizarCondicionesEspeciales(int.Parse(this.cboConIns.SelectedValue), this.txtCodigoIns.Text, int.Parse(this.cboTipoCondicionNewEdit.SelectedValue));
            }
            else
            {
                objTablasBasicas.CrearCondicionesEspeciales(int.Parse(this.cboConIns.SelectedValue),this.txtCodigoIns.Text, int.Parse(this.cboTipoCondicionNewEdit.SelectedValue));
            }
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion();
            
            if (this.hdfCondicionID.Value != string.Empty)
            {
                MostrarMensaje("Registro actualizado exitosamente");
                this.mpeCondicionNewEdit.Hide();
            }
            else
            {
                MostrarMensaje("Registro agregado exitosamente");
                this.mpeCondicionNewEdit.Hide();
            }
            limpiarObjetos();
        }

        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "Registrar -- " + ex.ToString());
            MostrarMensaje(Page.AppRelativeVirtualPath.ToString() + "Condiciones especiales -- " + "Ha ocurrido un error, comuniquese con el administrador.");
        }       
    }

    private void limpiarObjetos()
    {
        hdfCondicionID.Value = "";
        
        this.txtCodigoIns.Text = "";
        this.txtCodigoIns.Text = "";
        this.cboConIns.SelectedValue= "";        
    }

    private void Cancelar()
    {
        grdDatos.SelectedIndex = -1;
        limpiarObjetos();
        mpeCondicionNewEdit.Hide();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Cancelar();

    }

    protected void btnCancelarReg_Click(object sender, EventArgs e)
    {
        Cancelar();
    }   

    private void Eliminar(int condId)
    {
        try
        {
            objTablasBasicas = new TipoTramite();
            objTablasBasicas.EliminarCondicionesEspeciales(condId);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion();
            MostrarMensaje("Registro eliminado exitosamente");
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + " -- Eliminar() -- " + ex.ToString());
           MostrarMensaje("Error Comuniquese con el administrador");
        }      
       
    }

    protected void grdDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdDatos.PageIndex = e.NewPageIndex;
        ConsultarInformacion();
        this.lblMensaje.Text = "";
        this.divMensaje.Visible = false;
        this.upnlMensajes.Update();
    }

    protected void grdDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Modificar")
            {
                //Cargar registro Seleccionado
                int index = Convert.ToInt32(e.CommandArgument);
                int pagina = grdDatos.PageIndex;
                if (index > (grdDatos.PageSize-1))
                {
                    index = index - (pagina * grdDatos.PageSize);
                }
                grdDatos.SelectedIndex = index;
                hdfCondicionID.Value = grdDatos.SelectedDataKey["CON_ID"].ToString();
                this.cboConIns.SelectedValue = hdfCondicionID.Value;
                this.txtCodigoIns.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
                this.cboTipoCondicionNewEdit.SelectedValue = grdDatos.SelectedDataKey["TIPO_CONDICION_ESPECIAL"].ToString();
                cboConIns_SelectedIndexChanged(null, null);
                this.cboConIns.Enabled = false;
                this.txtCodigoIns.Enabled = false;
                this.cmdGuardarCondicion.Text = "Actualizar";
                mpeCondicionNewEdit.Show();

            }
            if (e.CommandName == "Eliminar")
            {
                //Cargar registro Seleccionado
                int index = Convert.ToInt32(e.CommandArgument);
                int pagina = grdDatos.PageIndex;
                if (index > (grdDatos.PageSize - 1))
                {
                    index = index - (pagina * grdDatos.PageSize);
                }
                grdDatos.SelectedIndex = index;                
                Label lblCondicion = (Label)grdDatos.Rows[index].FindControl("lblCondicion");


                Eliminar(int.Parse(lblCondicion.Text));
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + " -- Actualizar() -- " + ex.ToString());
           MostrarMensaje("Error Comuniquese con el administrador");
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        this.lblMensajeModal.Text = "";
        this.divMensajeModal.Visible = false;
        hdfCondicionID.Value = "";
        this.cboConIns.SelectedIndex = 0;
        this.cboTipoCondicionNewEdit.SelectedIndex = 0;
        this.cboConIns.Enabled = true;
        this.cmdGuardarCondicion.Text = "Agregar";
        mpeCondicionNewEdit.Show();
    }
      
    
    protected void cboCondicion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblMensaje.Text = "";
        this.divMensaje.Visible = false;
        this.upnlMensajes.Update();
        if (cboCondicion.SelectedValue != "")
        {
            SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
            string strCodigo = objtramites.CodigoCondicion(int.Parse(this.cboCondicion.SelectedValue));
        }
        
        ConsultarInformacion();
    }

   
    protected void cboConIns_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblMensajeModal.Text = "";
        this.divMensajeModal.Visible = false;
        if (cboConIns.SelectedValue != "")
        {
            SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
            string strCodigo = objtramites.CodigoCondicion(int.Parse(this.cboConIns.SelectedValue));
            this.txtCodigoIns.Text = strCodigo;
        }
    }


    protected void cboTipoCondicion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultarInformacion();
    }
}
