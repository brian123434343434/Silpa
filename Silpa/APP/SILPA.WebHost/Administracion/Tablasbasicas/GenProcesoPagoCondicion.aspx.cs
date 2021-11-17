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

public partial class Administracion_Tablasbasicas_GenProcesoPagoCondicion : System.Web.UI.Page
{

    protected GenProcesoPagoCondicion objTablasBasicas;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            
        }
        Mensaje.LimpiarMensaje(this);
        this.lblMensajeError.Text = "";
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (!this.txtIdProceso.Text.Equals(""))
        {
            if (!Tools.IsNumeric(this.txtIdProceso.Text))
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser numérico.");
                lblMensajeError.Text = "El valor de forma debe ser numérico.";
                return;
            }
            else
            {
                ConsultarInformacion(int.Parse(this.txtIdProceso.Text), this.txtCodConPago.Text, this.txtCodConImprimir.Text);
            }
        }
        else
            ConsultarInformacion(0, this.txtCodConPago.Text, this.txtCodConImprimir.Text);


    }

    protected void ConsultarInformacion(int intIdProcCase, string strConPago, string strConImprimir)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ConsultarInformacion.Inicio");
            objTablasBasicas = new GenProcesoPagoCondicion();

            grdDatos.DataSource = objTablasBasicas.ListarGenProcesoPagoCondicion(intIdProcCase, strConPago, strConImprimir);
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
            if (this.txtCodConImprimirEdit.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "Es necesario que ingrese el Còdigo de Condiciòn para Imprimir.");
                this.lblMensajeError.Text = "Es necesario que ingrese el Còdigo de Condiciòn para Imprimir.";
                return;
            }
            if (this.txtCodConPagoEdit.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "Es necesario que ingrese el Còdigo de Condiciòn de Pago.");
                this.lblMensajeError.Text = "Es necesario que ingrese el Còdigo de Condiciòn de Pago.";
                return;
            }
            if (this.txtIdProcesoEdit.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "Es necesario que ingrese el ID Caso de Proceso.");
                this.lblMensajeError.Text = "Es necesario que ingrese el ID Caso de Proceso.";
                return;
            }
            if (!Tools.IsNumeric(this.txtIdProcesoEdit.Text))
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser numérico.");
                lblMensajeError.Text = "El valor de forma debe ser numérico.";
                return;
            }
            else if (Convert.ToInt32(this.txtIdProcesoEdit.Text) < 0)
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser mayor o igual a cero(0).");
                this.lblMensajeError.Text = "El valor de forma debe ser mayor o igual a cero(0).";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            GenProcesoPagoCondicion objConsultaRuia = new GenProcesoPagoCondicion();
            DataTable dtlRuia = objConsultaRuia.ListarGenProcesoPagoCondicion(Convert.ToInt32(this.txtIdProcesoEdit.Text), this.txtCodConPagoEdit.Text, this.txtCodConImprimirEdit.Text);
            if (dtlRuia.Rows.Count > 0)
            {
                
                    Mensaje.MostrarMensaje(this, "El Proceso Pago Condiciòn ya se encuentra registrado en el sistema.");
                    this.lblMensajeError.Text = "El Proceso Pago Condiciòn ya se encuentra registrado en el sistema.";
                    return;
                
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
            if (this.txtIdProcesoNvo.Text.Trim() == string.Empty)
            {
                Mensaje.MostrarMensaje(this, "El ID Caso de Proceso.");
                this.lblMensajeError.Text = "El ID Caso de Proceso.";
                return;
            }
            if (!Tools.IsNumeric(this.txtIdProcesoNvo.Text))
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser numérico.");
                lblMensajeError.Text = "El valor de forma debe ser numérico.";
                return;
            }
            else if (Convert.ToInt32(this.txtIdProcesoNvo.Text) < 0)
            {
                Mensaje.MostrarMensaje(this, "El valor de forma debe ser mayor o igual a cero(0).");
                this.lblMensajeError.Text = "El valor de forma debe ser mayor o igual a cero(0).";
                return;
            }
            //Se consulta y valida que no se repita la informacion
            GenProcesoPagoCondicion objConsultaRuia = new GenProcesoPagoCondicion();
            DataTable dtlRuia = objConsultaRuia.ListarGenProcesoPagoCondicion(Convert.ToInt32(this.txtIdProcesoNvo.Text), this.txtCodConPagoNvo.Text, this.txtCodConImprimirNvo.Text);
            if (dtlRuia.Rows.Count > 0)
            {
                Mensaje.MostrarMensaje(this, "El Proceso Pago Condiciòn ya se encuentra registrado en el sistema.");
                this.lblMensajeError.Text = "El Proceso Pago Condiciòn ya se encuentra registrado en el sistema.";
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
            objTablasBasicas = new GenProcesoPagoCondicion();
            objTablasBasicas.InsertarGenProcesoPagoCondicion(Convert.ToInt32(this.txtIdProcesoNvo.Text), this.txtCodConPagoNvo.Text, this.txtCodConImprimirNvo.Text);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0,string.Empty,string.Empty);
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
        this.txtCodConImprimir.Text = "";
        txtCodConImprimirEdit.Text="";
        txtCodConImprimirNvo.Text="";
        txtCodConPago.Text="";
        txtCodConPagoEdit.Text="";
        txtCodConPagoNvo.Text="";
        txtIdProceso.Text="";
        txtIdProcesoEdit.Text="";
        txtIdProcesoNvo.Text = "";
        lblId.Text = "";
        
    }

    private void Cancelar()
    {
        ConsultarInformacion(0, string.Empty, string.Empty);
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
            objTablasBasicas = new GenProcesoPagoCondicion();

            objTablasBasicas.ActualizarGenProcesoPagoCondicion(Convert.ToInt32(lblId.Text), Convert.ToInt32(this.txtIdProcesoEdit.Text),this.txtCodConPagoEdit.Text, this.txtCodConImprimirEdit.Text);
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0,string.Empty,string.Empty);
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
            objTablasBasicas = new GenProcesoPagoCondicion();
            objTablasBasicas.EliminarGenProcesoPagoCondicion(Convert.ToInt32(strId));
            grdDatos.SelectedIndex = -1;
            ConsultarInformacion(0,string.Empty,string.Empty);
            Mensaje.MostrarMensaje(this, "Registro eliminado exitosamente");
            this.lblMensajeError.Text = "Registro eliminado exitosamente";
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            this.lblMensajeError.Text = "error elimi" + ex.Message;
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
        if (!this.txtIdProceso.Text.Equals(""))
        {
            ConsultarInformacion(int.Parse(this.txtIdProceso.Text), this.txtCodConPago.Text, this.txtCodConImprimir.Text);
        }
        else
        {
            ConsultarInformacion(0, this.txtCodConPago.Text, this.txtCodConImprimir.Text);
        }
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
                lblId.Text = grdDatos.SelectedDataKey["ID"].ToString();
                this.txtIdProcesoEdit.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[1].Text);
                this.txtCodConPagoEdit.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[2].Text);
                this.txtCodConImprimirEdit.Text = HttpUtility.HtmlDecode(grdDatos.Rows[index].Cells[3].Text);
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
            this.lblMensajeError.Text = "error boton reg" + ex.Message;
        }
    }
    
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        pnlConsultar.Visible = false;
        pnlMaestro.Visible = false;
        pnlNuevo.Visible = true;
    }

    }

