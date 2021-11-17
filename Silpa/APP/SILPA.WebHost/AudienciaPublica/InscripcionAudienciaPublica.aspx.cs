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
using SILPA.LogicaNegocio.AudienciaPublica;
using SoftManagement.Log;

public partial class Informacion_Publicaciones : System.Web.UI.Page
{

#region "eventos"

    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
       
        if (!Page.IsPostBack)
        {
            grdPublicaciones.AllowPaging = true;
            grdPublicaciones.PageSize = 5;
            CargarAutoridades();

        }
    }
    
    /// <summary>
    /// Boton que permite consultar el listado de audiencias a celebrar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                cargarDatosConsultaAudiencia();
                GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                CrearLogAuditoria.Insertar("AUDIENCIA PÚBLICA", 2, "Se consulto inscritos audiencia pública");
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico," AudienciaPublica.aspx--- Consultar" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }        

    }

    /// <summary>
    /// Paginación del gridview publicaciones
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdPublicaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPublicaciones.PageIndex = e.NewPageIndex;
        cargarDatosConsultaAudiencia();
        grdPublicaciones.DataBind();
    }

    /// <summary>
    /// Selección de registros del gridview publicaciones
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdPublicaciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = grdPublicaciones.SelectedIndex;
        string numeroSILPA = grdPublicaciones.SelectedDataKey["NUMERO_SILPA"].ToString();
              
        Response.Redirect("../AudienciaPublica/DetallesInscripcionAudienciaPublica.aspx?Numero_Silpa=" + numeroSILPA);

    }

#endregion

#region "rutinas"    

    /// <summary>
    /// Cargar las autoridades ambientales disponibles
    /// </summary>
    private void CargarAutoridades()
    {
        try
        {            
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            cboAutoAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
            cboAutoAmbiental.DataValueField = "AUT_ID";
            cboAutoAmbiental.DataTextField = "AUT_NOMBRE";
            cboAutoAmbiental.DataBind();
            cboAutoAmbiental.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, " AudienciaPublica.aspx--- Consultar" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }        
    }
    
    /// <summary>
    /// Función para consultar el listado de audiencias a celebrar
    /// </summary>
    /// <param name="strNumSILPA"></param>
    /// <param name="strAutoridadAmbiental"></param>
    /// <param name="strNombreProyecto"></param>
    /// <param name="intNumeroExpediente"></param>
    /// <param name="dateFechaReunion"></param>
    /// <param name="dateFechaAudiencia"></param>
    protected void consultarAudiencia(
        string strNumSILPA, Nullable<int> intAutoridadAmbiental,
        string strNombreProyecto, string strNumeroExpediente,
        Nullable<DateTime> dateFechaReunion, Nullable<DateTime> dateFechaAudiencia)
    {
        try
        {            
            AudienciaPublica objAudienciaPublicaNegocio = new AudienciaPublica();
            DataTable dtAudiencias = new DataTable();


            //Ejecutar consulta de listado de adiencias a celebrar
            dtAudiencias = objAudienciaPublicaNegocio.ListarAudienciasPublicas(
                strNumSILPA,
                intAutoridadAmbiental,
                strNombreProyecto,
                strNumeroExpediente,
                dateFechaReunion,
                dateFechaAudiencia);

            if (dtAudiencias != null)
            {
                if (dtAudiencias.Rows.Count > 0)
                {

                    grdPublicaciones.DataSource = dtAudiencias;
                    grdPublicaciones.DataBind();
                }
                else
                {
                    grdPublicaciones.DataSource = null;
                    grdPublicaciones.DataBind();

                    Mensaje.MostrarMensaje(this, "No se encotraron resultados para los datos de búsqueda ingresados");
                }
            }
            else
            {
                grdPublicaciones.DataSource = null;
                grdPublicaciones.DataBind();

                Mensaje.MostrarMensaje(this, "No se encontraron resultados para los datos de búsqueda ingresados");
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, " AudienciaPublica.aspx--- Consultar" + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ha ocurrido un error comuniquese con el administrador");
        }        
    }

    /// <summary>
    /// Cargar datos de consulta audiencia pública
    /// </summary>
    public void cargarDatosConsultaAudiencia()
    {
        DateTime? fechaReunion = null;
        DateTime? fechaAudiencia = null;
        int? autoridadAmbiental = null;


        if (cboAutoAmbiental.SelectedIndex != 0)
        {
            autoridadAmbiental = int.Parse(cboAutoAmbiental.SelectedValue);

        }
        else
        {
            autoridadAmbiental = null;
        }

        //Validar ingreso de fecha reunión informativa o fecha de audiencia pública
        if (txtFechaReunionInfo.Text != "" && txtFechaReunionAudiencia.Text != "")
        {
            Page.Validate("validarFechaReunion");
            if (Page.IsValid)
            {
                fechaReunion = Convert.ToDateTime(txtFechaReunionInfo.Text);

                Page.Validate("validarFechaAudiencia");
                if (Page.IsValid)
                {
                    fechaAudiencia = Convert.ToDateTime(txtFechaReunionAudiencia.Text);

                    //Consultar listado audiencias a celebrar
                    consultarAudiencia(txtNumSILPA.Text.Trim(),
                                        autoridadAmbiental,
                                        txtNombre.Text.Trim(),
                                        txtNumeroExpediente.Text.Trim(),
                                        fechaReunion,
                                        fechaAudiencia);
                }
            }
        }
        else
        {
            //Validar ingreso de fecha reunión informativa o fecha de audiencia pública
            if (txtFechaReunionInfo.Text == "" && txtFechaReunionAudiencia.Text != "")
            {
                Page.Validate("validarFechaAudiencia");
                if (Page.IsValid)
                {
                    fechaAudiencia = Convert.ToDateTime(txtFechaReunionAudiencia.Text);

                    //Consultar listado audiencias a celebrar
                    consultarAudiencia(txtNumSILPA.Text.Trim(),
                                        autoridadAmbiental,
                                         txtNombre.Text.Trim(),
                                         txtNumeroExpediente.Text.Trim(),
                                         fechaReunion,
                                         fechaAudiencia);
                }
            }
            else
            {
                //Validar ingreso de fecha reunión informativa o fecha de audiencia pública
                if (txtFechaReunionInfo.Text != "" && txtFechaReunionAudiencia.Text == "")
                {
                    Page.Validate("validarFechaReunion");
                    if (Page.IsValid)
                    {
                        fechaReunion = Convert.ToDateTime(txtFechaReunionInfo.Text);

                        //Consultar listado audiencias a celebrar
                        consultarAudiencia(txtNumSILPA.Text.Trim(),
                                         autoridadAmbiental,
                                         txtNombre.Text.Trim(),
                                         txtNumeroExpediente.Text.Trim(),
                                         fechaReunion,
                                         fechaAudiencia);

                    }
                }
                else
                {
                    //Consultar listado audiencias a celebrar
                    consultarAudiencia(txtNumSILPA.Text.Trim(),
                                         autoridadAmbiental,
                                         txtNombre.Text.Trim(),
                                         txtNumeroExpediente.Text.Trim(),
                                         fechaReunion,
                                         fechaAudiencia);
                }

            }



        }

    }

#endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("MenuAudienciaPublica.aspx");
    }
}
