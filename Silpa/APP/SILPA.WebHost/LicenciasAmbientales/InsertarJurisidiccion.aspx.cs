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
using SoftManagement.Log;

public partial class LicenciasAmbientales_InsertarJurisidiccion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarAutoridades();
            CargarCombosLugares();
        }
    }

    #region Funciones Programador ...

    /// <summary>
    /// Función que carga los combos de departamento y municipio
    /// </summary>
    private void CargarCombosLugares()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosLugares.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();

            DataSet _temp1 = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
            cboDepartamento.DataSource = _temp1;
            cboDepartamento.DataValueField = "DEP_ID";
            cboDepartamento.DataTextField = "DEP_NOMBRE";
            cboDepartamento.DataBind();

            DataSet _temp2 = _listaMunicipios.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null);
            cboMunicipio.DataSource = _temp2;
            cboMunicipio.DataValueField = "MUN_ID";
            cboMunicipio.DataTextField = "MUN_NOMBRE";
            cboMunicipio.DataBind();
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

    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamento_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamento.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipio.DataSource = _temp;
            cboMunicipio.DataValueField = "MUN_ID";
            cboMunicipio.DataTextField = "MUN_NOMBRE";
            cboMunicipio.DataBind();
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

    /// <summary>
    /// Funcion que carga el listado de Autoridades Ambientales
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

    #endregion
    
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregar_Click.Inicio");
            SILPA.LogicaNegocio.DAA.Jurisdiccion _jurisdiccion = new SILPA.LogicaNegocio.DAA.Jurisdiccion();
            _jurisdiccion.InsertarJurisdiccion(int.Parse(cboAutoridadAmbiental.SelectedValue),
                int.Parse(cboMunicipio.SelectedValue));

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("LICENCIASAMBIENTALES – RADICAR FU", 1, "Se almaceno Jurisdicción");
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAgregar_Click.Finalizo");
        }

    }
}
