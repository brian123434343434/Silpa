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
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System.IO;

public partial class NotificacionElectronica_ConsultarEmitirDocumento : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //this.lblFormulario.Text = Path.GetFileName(Request.Url.AbsolutePath);
            //this.llenarControles();
            //Mensaje.LimpiarMensaje(this);
            if (!IsPostBack)
            {             
                TipoDocumentoDalc _tipoDocumentoDalc = new TipoDocumentoDalc();
                this.cboTipoActo.DataSource = _tipoDocumentoDalc.ListarTiposDeDocumento(null, null);
                this.cboTipoActo.DataTextField = "NOMBRE_DOCUMENTO";
                this.cboTipoActo.DataValueField = "ID";
                this.cboTipoActo.DataBind();
                this.cboTipoActo.Items.Insert(0, new ListItem("Seleccione...", "-1"));              
            }
            //this.btnAceptar.Attributes.Add("onClick", "return confirm('Despues de guardar no podrá realizar ningún cambio. Está seguro que desea guardar el registro?')");
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }
    
         
    /// <summary>
    /// Se crea la tabla de session del documento
    /// </summary>
    private void ConsultarDatos(long personaId)
    {
       
            //Consulta la informacion de documentos
            EmitirDocumento _documento = new EmitirDocumento();
            DataTable _temp = _documento.ConsultarDocumento(personaId, this.txtNumeroVital.Text.Trim(), this.txtNumeroExpediente.Text.Trim(), int.Parse(this.cboTipoActo.SelectedValue), this.txtNumeroActo.Text.Trim(), this.txtFechaDesdeActo.Text, this.txtFechaHastaActo.Text);      

         if (_temp.Rows.Count > 0)
        {
            this.pnlDocumentos.Visible = true;
            this.grdDocumentos.DataSource = _temp;
            this.grdDocumentos.DataBind();

            //GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            //CrearLogAuditoria.Insertar("GEN - EMITIR DOCUMENTO", 2, "Se consulto Emitir Documentos Notificación");

        }
        else       
            this.pnlDocumentos.Visible = false;
          
    }
           
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (DatosSesion.Usuario != string.Empty)
        {
            Mensaje.MostrarMensaje(this, "El usuario no existe en el sistema");
            return;
        }
        PersonaDalc _persona = new PersonaDalc();
        PersonaIdentity _objPersona = new PersonaIdentity();
        NotificacionType _objNotificacion = new NotificacionType();

        //1230262
        _objPersona = _persona.ConsultarPersona(DatosSesion.Usuario);
        if (_objPersona.PersonaId <= 0)
        {
            Mensaje.MostrarMensaje(this, "El usuario no existe en el sistema");
            return;
        }
        this.ConsultarDatos(_objPersona.PersonaId);
    }

    protected void grdDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerDetalle")
        {
            int _indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow _fila = grdDocumentos.Rows[_indice];          
            Response.Redirect("~/NotificacionElectronica/EmitirDocumento.aspx?id=" + _fila.Cells[0].Text, false);
        }
    }
}

