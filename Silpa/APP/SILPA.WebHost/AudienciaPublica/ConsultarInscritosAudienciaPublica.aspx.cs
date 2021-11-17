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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using SILPA.LogicaNegocio.AudienciaPublica;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;
using SoftManagement.Log;

public partial class Informacion_Publicaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {          
            CargarAutoridades();          
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool _validar = true;

            //para fecha informativa
            if (this.txtFechaInformativa.Text != "")
            {
                Page.Validate("validarFechaInformativa");
                _validar = Page.IsValid;
            }
            //para fecha audiencia
            if (this.txtFechaAudiencia.Text != "")
            {
                Page.Validate("validarFechaAudiencia");
                _validar = Page.IsValid;
            }

            if (_validar)
                CargarListaInscritos();

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("AUDIENCIA PÚBLICA", 2, "Se consulto inscritos audiencia pública");
        }
    }  

    #region Funciones Programador

    /// <summary>
    /// Funcion que carga el listado de Autoridades Ambientales.
    /// </summary>
    private void CargarAutoridades()
    {
        try
        {            
            Listas _listaAutoridades = new Listas();
            //Autoridad ambiental
            this.cboAutoridadAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
            this.cboAutoridadAmbiental.DataValueField = "AUT_ID";
            this.cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            this.cboAutoridadAmbiental.DataBind();
            this.cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, " ConsultaInscritosAudienciaPublica.aspx--- Consultar" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");            
        }        
    }
    
    private void CargarListaInscritos()
    {
        try
        {            
            DateTime? fechaInformativa = null;
            DateTime? fechaAudiencia = null;
            if (this.txtFechaInformativa.Text != string.Empty)
                fechaInformativa = Convert.ToDateTime(this.txtFechaInformativa.Text);
            if (this.txtFechaAudiencia.Text != string.Empty)
                fechaAudiencia = Convert.ToDateTime(this.txtFechaAudiencia.Text);
            InscripcionAudiencia _lista = new InscripcionAudiencia();
            DataSet _temp = _lista.ListarInscritosAudiencia(this.txtNumeroSilpa.Text.Trim(),
                Convert.ToInt32(this.cboAutoridadAmbiental.Text),
                this.txtNombreProyecto.Text.Trim(), this.txtNumeroExpediente.Text.Trim(),
                fechaInformativa, fechaAudiencia, -1);

            grdInscritos.DataSource = _temp;
            grdInscritos.DataBind();
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, " ConsultaInscritosAudienciaPublica.aspx--- Consultar" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");            
        }     
    }

    #endregion

    protected void grdInscritos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdInscritos.PageIndex = e.NewPageIndex;
        this.CargarListaInscritos();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("MenuAudienciaPublica.aspx");
    }
}
