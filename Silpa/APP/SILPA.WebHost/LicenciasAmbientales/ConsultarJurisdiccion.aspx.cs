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

public partial class LicenciasAmbientales_ConsultarJurisdiccion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlJurisdicciones.Visible = false;
            lblMensaje.Visible = false;
            CargarCombosLugares();
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
            foreach (DataRow _fila in _temp1.Tables[0].Rows)
            {
                cboDepartamento.Items.Add(new ListItem(_fila["DEP_NOMBRE"].ToString(), _fila["DEP_ID"].ToString()));
            }

            DataSet _temp2 = _listaMunicipios.ListaMunicipios(null, 5, null);
            foreach (DataRow _fila in _temp2.Tables[0].Rows)
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
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosLugares.Finalizo");
        }
    }


    #endregion    

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnBuscar_Click.Inicio");
            if (cboMunicipio.SelectedValue == "-1")
            {
                SILPA.LogicaNegocio.DAA.Jurisdiccion _jurisdiccion = new SILPA.LogicaNegocio.DAA.Jurisdiccion();
                DataSet _temp = _jurisdiccion.ListaJurisdiccion(null);
                if (_temp.Tables[0].Rows.Count != 0)
                {
                    lblMensaje.Visible = false;
                    pnlJurisdicciones.Visible = true;
                    grdJurisdicciones.DataSource = _temp;
                    grdJurisdicciones.DataBind();

                    GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                    CrearLogAuditoria.Insertar("LICENCIASAMBIENTALES – RADICAR FU", 2, "Se consulto Jurisdicción");

                }
                else
                {
                    lblMensaje.Visible = true;
                    pnlJurisdicciones.Visible = false;
                }
            }
            else
            {
                SILPA.LogicaNegocio.DAA.Jurisdiccion _jurisdiccion = new SILPA.LogicaNegocio.DAA.Jurisdiccion();
                DataSet _temp = _jurisdiccion.ListaJurisdiccion(int.Parse(cboMunicipio.SelectedValue));
                if (_temp.Tables[0].Rows.Count != 0)
                {
                    lblMensaje.Visible = false;
                    pnlJurisdicciones.Visible = true;
                    grdJurisdicciones.DataSource = _temp;
                    grdJurisdicciones.DataBind();
                }
                else
                {
                    lblMensaje.Visible = true;
                    pnlJurisdicciones.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnBuscar_Click.Finalizo");
        }
    }
}
