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
using System.Collections.Generic;
using System.Data.SqlClient;
using SILPA.LogicaNegocio.ReporteTramite;
using SoftManagement.Log;

public partial class ReporteTramiteWeb : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
       
        if (!IsPostBack)
        {
            CargarCombosLugares();
            CargarAutoridades();
            CargarTipoTramite();

            grdReporte.AllowPaging = true;
            grdReporte.PageSize = 5; 

        }
    }

    protected void grdReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReporte.PageIndex = e.NewPageIndex;
        cargarConsultaReporte(); 
    }

    /// <summary>
    /// Seleccion en dropdownlist departamentos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamento_SelectedIndexChanged.Inicio");
            //Cargar los municipios del departamento seleccionado
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamento.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);

            cboMunicipio.Items.Clear();
            cboMunicipio.Items.Add(new ListItem("Seleccione...", "-1"));

            foreach (DataRow _fila in _temp.Tables[0].Rows)
            {
                cboMunicipio.Items.Add(new ListItem(_fila["MUN_NOMBRE"].ToString(), _fila["MUN_ID"].ToString()));
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);            
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamento_SelectedIndexChanged.Finalizo");
        }
    }

    #region Funciones del programador ...

    ///<sumary>
    /// Funcion para cargar los tipos de tramite 
    /// </sumary>
    private void CargarTipoTramite()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarTipoTramite.Inicio");
            //Cargar los tipos de tramite
            SILPA.LogicaNegocio.Generico.Listas _listaTipoTramite = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _tempTramite = _listaTipoTramite.ListarTipoTramite(null);
            cboTipoTramite.DataSource = _tempTramite.Tables[0];
            cboTipoTramite.DataValueField = "ID";
            cboTipoTramite.DataTextField = "NOMBRE_TIPO_TRAMITE";
            cboTipoTramite.DataBind();
            cboTipoTramite.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarTipoTramite.Finalizo");
        }
    }

    ///<sumary>
    /// Funcion que carga los combos de departamentos
    /// </sumary>
    private void CargarCombosLugares()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosLugares.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();

            DataSet _temp1 = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);

            foreach (DataRow _fila in _temp1.Tables[0].Rows)
            {
                cboDepartamento.Items.Add(new ListItem(_fila["DEP_NOMBRE"].ToString(), _fila["DEP_ID"].ToString()));
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosLugares.Finalizo");
        }
      
    }

    /// <summary>
    /// Cargar las autoridades ambientales disponibles
    /// </summary>
    private void CargarAutoridades()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarAutoridades.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            cboAutoridadAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
            cboAutoridadAmbiental.DataValueField = "AUT_ID";
            cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            cboAutoridadAmbiental.DataBind();
            cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarAutoridades.Finalizo");
        }
    }   

    /// <summary>
    /// Función para consultar los reportes por diferentes criterios de búsqueda
    /// </summary>
    private void cargarConsultaReporte()
    {
        ReporteTramite objReporte = new ReporteTramite();
        DataView dvReporte = new DataView();

        int? tipoTramite = null;
        int? AA = null;
        DateTime? fechaInicial = null;
        DateTime? fechaFinal = null;


        if (int.Parse(cboTipoTramite.SelectedValue) != -1)
        {
            tipoTramite = int.Parse(cboTipoTramite.SelectedValue);
        }

        if (int.Parse(cboAutoridadAmbiental.SelectedValue) != -1)
        {
            AA = int.Parse(cboAutoridadAmbiental.SelectedValue);
        }


        if (txtFechaFinal.Text != "" && txtFechaInicial.Text == "")
            lblErrorFechas.Text = "Debe ingresar la fecha inicial";
        else
            lblErrorFechas.Text = "";

        if (txtFechaFinal.Text == "" && txtFechaInicial.Text != "")
            lblErrorFechas.Text = "Debe ingresar la fecha final";
        else
            lblErrorFechas.Text = "";

        if (txtFechaInicial.Text != "" && txtFechaFinal.Text != "")
        {      
            Page.Validate("validarFechas");
            if (Page.IsValid)
            {
                fechaInicial = Convert.ToDateTime(txtFechaInicial.Text);
                fechaFinal = Convert.ToDateTime(txtFechaFinal.Text);
                             

                    //dvReporte = objReporte.ListarReporteTramite(fechaInicial, fechaFinal, tipoTramite, AA, txtNombreProyecto.Text.Trim());

                    if (dvReporte != null && dvReporte.Count > 0)
                    {                                               
                        grdReporte.DataSource = dvReporte;
                        grdReporte.DataBind();
                    }
                    else
                    {
                        grdReporte.DataSource = null;
                        grdReporte.DataBind();
                         Mensaje.MostrarMensaje(this, "No se encontró ningún resultado para los datos de búsqueda ingresados");

                    }
               
            }                      
        }
        else
        {
                //dvReporte = objReporte.ListarReporteTramite(fechaInicial, fechaFinal, tipoTramite, AA, txtNombreProyecto.Text.Trim());

                if (dvReporte != null && dvReporte.Count > 0)
                {                     
                    grdReporte.DataSource = dvReporte;
                    grdReporte.DataBind();
                }
                else
                {
                    grdReporte.DataSource = null;
                    grdReporte.DataBind();
                     Mensaje.MostrarMensaje(this, "No se encontró ningún resultado para los datos de búsqueda ingresados");

                }           
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {

        cargarConsultaReporte(); 
        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("REPORTE TRAMITE", 2, "Se consulto Reporte Tramire Web");

    }

    protected void grdReporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = grdReporte.SelectedIndex;
        string numeroExpediente = grdReporte.DataKeys[index].Value.ToString();

        Response.Redirect("../ReporteTramite/ReporteTramiteDos.aspx?Numero_Expediente="+numeroExpediente );

    }
    #endregion
}
    





