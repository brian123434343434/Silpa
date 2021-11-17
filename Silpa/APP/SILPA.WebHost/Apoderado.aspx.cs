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

public partial class Apoderado : System.Web.UI.Page
{
    private string _modo;
    private string _identificacion;
    private string _str_btnActualizar;

    protected void Page_Load(object sender, EventArgs e)
    {
        _modo = Request.QueryString["modo"];
        _str_btnActualizar = Request.QueryString["btnId"];
        
        if (Request.QueryString.Count > 1)
            _identificacion = Request.QueryString["id"];        

        if (!IsPostBack)
        {
            CargarPaises();
            CargarTiposIdentificacion();
            CargarCombosOrigenApoderado();
            
            cboDepartamentoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));            

            if (_modo == "1")
            {
                cboTipoDocumentoApoderado.Enabled = false;
                txtNumeroIdentificacionApoderado.Enabled = false;
                cboDepartamentoOrigenApoderado.Enabled = false;
                cboMunicipioOrigenApoderado.Enabled = false;

                CargarDatos();
            }
        }
    }

    protected void btnCancelarApoderado_Click(object sender, EventArgs e)
    {
        string strScript2;
        strScript2 = "<script language='JavaScript'>self.close()</script>";
        Page.RegisterClientScriptBlock("Pruebas2", strScript2);
    }

    protected void btnAceptarApoderado_Click(object sender, EventArgs e)
    {
        DataTable _apoderados = new DataTable();
        DataRow _fila;

        try
        {
            if (Page.IsValid)
            {
                _apoderados = (DataTable)Session["Apoderados"];
                if (_modo == "0")
                {
                    if (ValidaUsuario())
                    {
                        _fila = _apoderados.NewRow();

                        _fila["PRIMER_NOMBRE"] = txtPrimerNombreApoderado.Text;
                        _fila["SEGUNDO_NOMBRE"] = txtSegundoNombreApoderado.Text;
                        _fila["PRIMER_APELLIDO"] = txtPrimerApellidoApoderado.Text;
                        _fila["SEGUNDO_APELLIDO"] = txtSegundoApellidoApoderado.Text;
                        _fila["TIP_DOC_ACREDITACION"] = cboTipoDocumentoACreditacion.SelectedValue.ToString();
                        _fila["NOM_DOC_ACREDITACION"] = cboTipoDocumentoACreditacion.SelectedItem.ToString();
                        _fila["NO_DOC_ACREDITACION"] = txtNoAcreditacion.Text;
                        _fila["ID_TIPO_ID"] = cboTipoDocumentoApoderado.SelectedItem.Value;
                        _fila["TIPO_ID"] = cboTipoDocumentoApoderado.SelectedItem.Text;
                        _fila["ID_IDENTIFICACION"] = txtNumeroIdentificacionApoderado.Text;
                        _fila["ID_ORIGEN_DEPARTAMENTO"] = cboDepartamentoOrigenApoderado.SelectedItem.Value;
                        _fila["ORIGEN_DEPARTAMENTO"] = cboDepartamentoOrigenApoderado.SelectedItem.Text;
                        _fila["ID_ORIGEN_MUNICIPIO"] = cboMunicipioOrigenApoderado.SelectedItem.Value;
                        _fila["ORIGEN_MUNICIPIO"] = cboMunicipioOrigenApoderado.SelectedItem.Text;
                        _fila["DIRECCION_CORRESPONDENCIA"] = txtDireccionApoderado.Text;
                        _fila["ID_PAIS"] = cboPaisApoderado.SelectedItem.Value;
                        _fila["PAIS"] = cboPaisApoderado.SelectedItem.Text;
                        _fila["ID_DEPARTAMENTO"] = (cboDepartamentoApoderado.SelectedValue != "-1") ? cboDepartamentoApoderado.SelectedItem.Value : "";
                        _fila["DEPARTAMENTO"] = (cboDepartamentoApoderado.SelectedValue != "-1") ? cboDepartamentoApoderado.SelectedItem.Text : "";
                        _fila["ID_MUNICIPIO"] = (cboMunicipioApoderado.SelectedValue != "-1") ? cboMunicipioApoderado.SelectedItem.Value : "";
                        _fila["MUNICIPIO"] = (cboMunicipioApoderado.SelectedValue != "-1") ? cboMunicipioApoderado.SelectedItem.Text : "";
                        _fila["ID_VEREDA"] = (cboVeredaApoderado.SelectedValue != "-1") ? cboVeredaApoderado.SelectedValue : "";
                        _fila["VEREDA"] = (cboVeredaApoderado.SelectedValue != "-1") ? cboVeredaApoderado.SelectedItem.Text : "";
                        _fila["ID_CORREGIMIENTO"] = (cboCorregimientoApoderado.SelectedValue != "-1") ? cboCorregimientoApoderado.SelectedValue : "";
                        _fila["CORREGIMIENTO"] = (cboCorregimientoApoderado.SelectedValue != "-1") ? cboCorregimientoApoderado.SelectedItem.Text : "";
                        _fila["TELEFONO"] = txtTelefonoApoderado.Text;
                        _fila["CELULAR"] = txtCelularApoderado.Text;
                        _fila["FAX"] = txtFaxApoderado.Text;
                        _fila["CORREO"] = txtCorreoApoderado.Text;
                        _fila["ESTADO"] = true;

                        _apoderados.Rows.Add(_fila);
                        _apoderados.AcceptChanges();
                        Session["Apoderados"] = _apoderados;
                    }
                    else
                    {
                        string strScript = "<script language='JavaScript'>" +
                        "alert('Ya existe un apoderado con este numero de documento.')" +
                        "</script>";
                        Page.RegisterStartupScript("PopupScript", strScript);
                    }

                    GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                    CrearLogAuditoria.Insertar("SILPA", 1, "Se almaceno Apoderado");

                }
                else if (_modo == "1")
                {
                    if (_apoderados.Select("ID_IDENTIFICACION = '" + _identificacion + "'").Length > 0)
                    {
                        foreach (DataRow _temp in _apoderados.Rows)
                        {
                            if (_temp["ID_IDENTIFICACION"].ToString() == _identificacion)
                            {
                                _temp["PRIMER_NOMBRE"] = txtPrimerNombreApoderado.Text;
                                _temp["SEGUNDO_NOMBRE"] = txtSegundoNombreApoderado.Text;
                                _temp["PRIMER_APELLIDO"] = txtPrimerApellidoApoderado.Text;
                                _temp["SEGUNDO_APELLIDO"] = txtSegundoApellidoApoderado.Text;                                
                                _temp["TIP_DOC_ACREDITACION"] = cboTipoDocumentoACreditacion.SelectedValue.ToString();
                                _temp["NOM_DOC_ACREDITACION"] = cboTipoDocumentoACreditacion.SelectedItem.ToString();
                                _temp["NO_DOC_ACREDITACION"] = txtNoAcreditacion.Text;
                                _temp["ID_TIPO_ID"] = cboTipoDocumentoApoderado.SelectedItem.Value;
                                _temp["TIPO_ID"] = cboTipoDocumentoApoderado.SelectedItem.Text;
                                _temp["ID_IDENTIFICACION"] = txtNumeroIdentificacionApoderado.Text;
                                _temp["ID_ORIGEN_DEPARTAMENTO"] = cboDepartamentoOrigenApoderado.SelectedItem.Value;
                                _temp["ORIGEN_DEPARTAMENTO"] = cboDepartamentoOrigenApoderado.SelectedItem.Text;
                                _temp["ID_ORIGEN_MUNICIPIO"] = cboMunicipioOrigenApoderado.SelectedItem.Value;
                                _temp["ORIGEN_MUNICIPIO"] = cboMunicipioOrigenApoderado.SelectedItem.Text;
                                _temp["DIRECCION_CORRESPONDENCIA"] = txtDireccionApoderado.Text;
                                _temp["ID_PAIS"] = cboPaisApoderado.SelectedItem.Value;
                                _temp["PAIS"] = cboPaisApoderado.SelectedItem.Text;
                                _temp["ID_DEPARTAMENTO"] = (cboDepartamentoApoderado.SelectedValue != "-1") ? cboDepartamentoApoderado.SelectedItem.Value : "";
                                _temp["DEPARTAMENTO"] = (cboDepartamentoApoderado.SelectedValue != "-1") ? cboDepartamentoApoderado.SelectedItem.Text : "";
                                _temp["ID_MUNICIPIO"] = (cboMunicipioApoderado.SelectedValue != "-1") ? cboMunicipioApoderado.SelectedItem.Value : "";
                                _temp["MUNICIPIO"] = (cboMunicipioApoderado.SelectedValue != "-1") ? cboMunicipioApoderado.SelectedItem.Text : "";
                                _temp["ID_VEREDA"] = (cboVeredaApoderado.SelectedValue != "-1") ? cboVeredaApoderado.SelectedValue : "";
                                _temp["VEREDA"] = (cboVeredaApoderado.SelectedValue != "-1") ? cboVeredaApoderado.SelectedItem.Text : "";
                                _temp["ID_CORREGIMIENTO"] = (cboCorregimientoApoderado.SelectedValue != "-1") ? cboCorregimientoApoderado.SelectedValue : "";
                                _temp["CORREGIMIENTO"] = (cboCorregimientoApoderado.SelectedValue != "-1") ? cboCorregimientoApoderado.SelectedItem.Text : "";
                                _temp["TELEFONO"] = txtTelefonoApoderado.Text;
                                _temp["CELULAR"] = txtCelularApoderado.Text;
                                _temp["FAX"] = txtFaxApoderado.Text;
                                _temp["CORREO"] = txtCorreoApoderado.Text;
                                _temp["ESTADO"] = chkEstado.Checked;

                                _apoderados.AcceptChanges();
                                Session["Apoderados"] = _apoderados;
                            }
                        }
                    }
                }
                this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "asociacion", "<script>window.opener.document.forms[0]." + this._str_btnActualizar + ".click();window.close();</script>");
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
        string strScript1;
        strScript1 = "<script language='JavaScript'>self.close()</script>";
        Page.RegisterClientScriptBlock("Pruebas1", strScript1);
    }

    protected void cboPaisApoderado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisApoderado_SelectedIndexChanged.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            if (cboPaisApoderado.SelectedValue == _configuracion.IdPaisPredeterminado.ToString())
            {
                SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
                //cboDepartamentoApoderado.Enabled = true;
                //cboMunicipioApoderado.Enabled = true;
                cboDepartamentoApoderado.DataSource = _temp;
                cboDepartamentoApoderado.DataValueField = "DEP_ID";
                cboDepartamentoApoderado.DataTextField = "DEP_NOMBRE";
                cboDepartamentoApoderado.DataBind();
                cboDepartamentoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                //cboDepartamentoApoderado.SelectedIndex = 0;
                CargarComboMunicipios(cboDepartamentoApoderado.SelectedItem.Value, null);
                //covDepartamentoApoderado.Enabled = true;
                //covMunicipioApoderado.Enabled = true;
            }
            else
            {
                //cboDepartamentoApoderado.Enabled = false;
                //cboMunicipioApoderado.Enabled = false;
                cboDepartamentoApoderado.Items.Clear();
                cboDepartamentoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboMunicipioApoderado.Items.Clear();
                cboMunicipioApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                //covDepartamentoApoderado.Enabled = false;
                //covMunicipioApoderado.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisApoderado_SelectedIndexChanged.Finalizo");        
        }
    }

    protected void cboDepartamentoApoderado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoApoderado_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamentoApoderado.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioApoderado.DataSource = _temp;
            cboMunicipioApoderado.DataValueField = "MUN_ID";
            cboMunicipioApoderado.DataTextField = "MUN_NOMBRE";
            cboMunicipioApoderado.DataBind();
            cboMunicipioApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioApoderado.Enabled = true;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoApoderado_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboDepartamentoOrigenApoderado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoOrigenApoderado_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamentoOrigenApoderado.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioOrigenApoderado.DataSource = _temp;
            cboMunicipioOrigenApoderado.DataValueField = "MUN_ID";
            cboMunicipioOrigenApoderado.DataTextField = "MUN_NOMBRE";
            cboMunicipioOrigenApoderado.DataBind();
            cboMunicipioOrigenApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioOrigenApoderado.Enabled = true;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoOrigenApoderado_SelectedIndexChanged.Finalizo");
        }
    }

    #region Funciones Programador
    
    /// <summary>
    /// Funcion que carga los datos de un apoderado para su actualizacion
    /// </summary>
    private void CargarDatos()
    {
        DataTable _apoderados = new DataTable();
        //DataRow _fila;

        try
        {
            _apoderados = (DataTable)Session["Apoderados"];
            //TODO test cedula logueado, borrar
            //_identificacion = "3216547";
            if (_apoderados.Select("ID_IDENTIFICACION = '" + _identificacion + "'").Length > 0)
            {
                foreach (DataRow _temp in _apoderados.Rows)
                {
                    if (_temp["ID_IDENTIFICACION"].ToString() == _identificacion)
                    {
                        txtPrimerNombreApoderado.Text = _temp["PRIMER_NOMBRE"].ToString();
                        txtSegundoNombreApoderado.Text = _temp["SEGUNDO_NOMBRE"].ToString();
                        txtPrimerApellidoApoderado.Text = _temp["PRIMER_APELLIDO"].ToString();
                        txtSegundoApellidoApoderado.Text = _temp["SEGUNDO_APELLIDO"].ToString();
                        cboTipoDocumentoACreditacion.SelectedValue = _temp["TIP_DOC_ACREDITACION"].ToString();
                        txtNoAcreditacion.Text = _temp["NO_DOC_ACREDITACION"].ToString();                        
                        cboTipoDocumentoApoderado.SelectedIndex = retornarIndice(cboTipoDocumentoApoderado, _temp["ID_TIPO_ID"].ToString());
                        txtNumeroIdentificacionApoderado.Text = _temp["ID_IDENTIFICACION"].ToString();                      
                        //TODO Descomentariar, test
                        CargarComboDepartamentoOrigen(_temp["ID_ORIGEN_DEPARTAMENTO"].ToString(), _temp["ID_ORIGEN_MUNICIPIO"].ToString());
                        txtDireccionApoderado.Text = _temp["DIRECCION_CORRESPONDENCIA"].ToString();
                        cboPaisApoderado.SelectedIndex = retornarIndice(cboPaisApoderado, _temp["ID_PAIS"].ToString());
                        CargarCombosContacto(
                                _temp["ID_PAIS"].ToString(),
                                _temp["ID_DEPARTAMENTO"].ToString(),
                                _temp["ID_MUNICIPIO"].ToString(),
                                _temp["ID_CORREGIMIENTO"].ToString(),
                                _temp["ID_VEREDA"].ToString()
                            );

                        //CargarComboDepartamentos(_temp["ID_PAIS"].ToString(), _temp["ID_DEPARTAMENTO"].ToString(), _temp["ID_MUNICIPIO"].ToString());
                        //txtVeredaApoderado.Text = _temp["VEREDA"].ToString();
                        //txtCorregimientoApoderado.Text = _temp["CORREGIMIENTO"].ToString();
                        txtTelefonoApoderado.Text = _temp["TELEFONO"].ToString();
                        txtCelularApoderado.Text = _temp["CELULAR"].ToString();
                        txtFaxApoderado.Text = _temp["FAX"].ToString();
                        txtCorreoApoderado.Text = _temp["CORREO"].ToString();
                        chkEstado.Checked = Convert.ToBoolean(_temp["ESTADO"]);

                        tr_Estado.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }

    /// <summary>
    /// Funcion que carga los tipos de identificacion
    /// </summary>
    private void CargarTiposIdentificacion()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarTiposIdentificacion.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaTiposId = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _Temp = _listaTiposId.ListaTipoIdentificacionXTipoPersona();

            //cboTipoDocumentoApoderado.DataSource = _listaTiposId.ListaTipoIdentificacion();
            cboTipoDocumentoApoderado.DataSource = ListaDocumentos(_Temp, "TPE_ID = " + Convert.ToString((int)SILPA.Comun.TipoPersona.Apoderado));
            cboTipoDocumentoApoderado.DataValueField = "TID_ID";
            cboTipoDocumentoApoderado.DataTextField = "TID_NOMBRE";
            cboTipoDocumentoApoderado.DataBind();
            cboTipoDocumentoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarTiposIdentificacion.Finalizo");
        }
    }

    /// <summary>
    /// Función que retorna el indice del valor de la opcion que se encuentra en el combo especificado
    /// </summary>
    /// <param name="_combo"></param>
    /// <param name="_valor"></param>
    /// <returns></returns>
    private int retornarIndice(DropDownList _combo, string _valor)
    {
        foreach (ListItem _item in _combo.Items)
        {
            _item.Selected = false;
            if (_item.Value == _valor)
            {
                _item.Selected = true;
                return _combo.SelectedIndex;
            }
        }
        return -1;
    }

    /// <summary>
    /// Funcion que carga el listado de paises
    /// </summary>
    private void CargarPaises()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarPaises.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaPaises = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _temp = _listaPaises.ListarPaises(null);
            cboPaisApoderado.DataSource = _temp;
            cboPaisApoderado.DataValueField = "PAI_ID";
            cboPaisApoderado.DataTextField = "PAI_NOMBRE";
            cboPaisApoderado.DataBind();
            cboPaisApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            cboPaisApoderado.SelectedValue = _configuracion.IdPaisPredeterminado.ToString();
            //cboDepartamentoApoderado.Enabled = false;
            //cboMunicipioApoderado.Enabled = false;

            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _tempDepto = _listaDepartamentos.ListarDepartamentos(Convert.ToInt32(cboPaisApoderado.SelectedValue));
            cboDepartamentoApoderado.DataSource = _tempDepto;
            cboDepartamentoApoderado.DataValueField = "DEP_ID";
            cboDepartamentoApoderado.DataTextField = "DEP_NOMBRE";
            cboDepartamentoApoderado.DataBind();
            cboDepartamentoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            CargarComboMunicipios(cboDepartamentoApoderado.SelectedItem.Value, null);
            //covDepartamentoApoderado.Enabled = true;
            //covMunicipioApoderado.Enabled = true;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarPaises.Finalizo");
        }
    }

    /// <summary>
    /// Funcion que carga el combo de Departamentos y selecciona uno segun los datos del Apoderado.
    /// </summary>
    /// <param name="_indicePais"></param>
    /// <param name="_indiceDepartamento"></param>
    private void CargarComboDepartamentos(string _indicePais, string _indiceDepartamento, string _indiceMunicipio)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboDepartamentos.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            if (_configuracion.IdPaisPredeterminado.ToString() == _indicePais)
            {
                SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaDepartamentos.ListarDepartamentos(int.Parse(_indicePais));
                cboDepartamentoApoderado.Enabled = true;
                cboMunicipioApoderado.Enabled = true;
                cboDepartamentoApoderado.DataSource = _temp;
                cboDepartamentoApoderado.DataValueField = "DEP_ID";
                cboDepartamentoApoderado.DataTextField = "DEP_NOMBRE";
                cboDepartamentoApoderado.DataBind();
                cboDepartamentoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboDepartamentoApoderado.SelectedIndex = retornarIndice(cboDepartamentoApoderado, _indiceDepartamento);
                CargarComboMunicipios(_indiceDepartamento, _indiceMunicipio);
            }
            else
                return;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboDepartamentos.Finalizo");
        }
    }

    /// <summary>
    /// Funcion que carga el combo de Municipios y selecciona uno segun los datos del Apoderado.
    /// </summary>
    /// <param name="_indiceDepartamento"></param>
    /// <param name="_indiceMunicipio"></param>

    private void CargarComboMunicipios(string _indiceDepartamento, string _indiceMunicipio)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboMunicipios.Inicio");
            if (_indiceDepartamento != null)
            {
                SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaMunicipios.ListaMunicipios(null, int.Parse(_indiceDepartamento), null);
                cboMunicipioApoderado.DataSource = _temp;
                cboMunicipioApoderado.DataValueField = "MUN_ID";
                cboMunicipioApoderado.DataTextField = "MUN_NOMBRE";
                cboMunicipioApoderado.DataBind();
                cboMunicipioApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                if (_indiceMunicipio != null)
                    cboMunicipioApoderado.SelectedIndex = retornarIndice(cboMunicipioApoderado, _indiceMunicipio);
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboMunicipios.Finalizo");
        }
    }

    /// <summary>
    /// Carga los combos para seleccionar el lugar de expedicion del Documento en apoderado
    /// </summary>
    private void CargarCombosOrigenApoderado()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosOrigenApoderado.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.LogicaNegocio.Generico.Listas _listaTipoAcreditacion = new SILPA.LogicaNegocio.Generico.Listas();

            DataSet _temp1 = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
            cboDepartamentoOrigenApoderado.DataSource = _temp1;
            cboDepartamentoOrigenApoderado.DataValueField = "DEP_ID";
            cboDepartamentoOrigenApoderado.DataTextField = "DEP_NOMBRE";
            cboDepartamentoOrigenApoderado.DataBind();
            cboDepartamentoOrigenApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            DataSet _temp2 = _listaMunicipios.ListaMunicipios(null, int.Parse(cboDepartamentoOrigenApoderado.SelectedValue), null);
            cboMunicipioOrigenApoderado.DataSource = _temp2;
            cboMunicipioOrigenApoderado.DataValueField = "MUN_ID";
            cboMunicipioOrigenApoderado.DataTextField = "MUN_NOMBRE";
            cboMunicipioOrigenApoderado.DataBind();
            cboMunicipioOrigenApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            DataSet _temp3 = _listaTipoAcreditacion.ListaAcreditacion();
            cboTipoDocumentoACreditacion.DataSource = _temp3;
            cboTipoDocumentoACreditacion.DataValueField = "ACREDITACION_ID";
            cboTipoDocumentoACreditacion.DataTextField = "ACREDITACION_NOMBRE";
            cboTipoDocumentoACreditacion.DataBind();            
            cboTipoDocumentoACreditacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosOrigenApoderado.Finalizo");
        }
    }

    /// <summary>
    /// Carga el combo del departamento de origen del apoderado, selecciona el departameno y carga los municipios
    /// y selecciona el municipio.
    /// </summary>
    /// <param name="_indiceDepartamento">Indice del departamento</param>
    /// <param name="_indiceMunicipio">Indice del municipio</param>
    private void CargarComboDepartamentoOrigen(string _indiceDepartamento, string _indiceMunicipio)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboDepartamentoOrigen.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
            cboDepartamentoOrigenApoderado.Enabled = true;
            cboMunicipioOrigenApoderado.Enabled = true;
            cboDepartamentoOrigenApoderado.DataSource = _temp;
            cboDepartamentoOrigenApoderado.DataValueField = "DEP_ID";
            cboDepartamentoOrigenApoderado.DataTextField = "DEP_NOMBRE";
            cboDepartamentoOrigenApoderado.DataBind();
            cboDepartamentoOrigenApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboDepartamentoOrigenApoderado.SelectedIndex = retornarIndice(cboDepartamentoOrigenApoderado, _indiceDepartamento);
            CargarComboMunicipioOrigen(_indiceDepartamento, _indiceMunicipio);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboDepartamentoOrigen.Finalizo");
        }
    }

    /// <summary>
    /// Carga el combo del municipio de origen y selecciona un municipio
    /// </summary>
    /// <param name="_indiceDepartamento">Indice del departamento</param>
    /// <param name="_indiceMunicipio">Indice del municipio</param>
    private void CargarComboMunicipioOrigen(string _indiceDepartamento, string _indiceMunicipio)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboMunicipioOrigen.Inicio");
            if (_indiceDepartamento != null)
            {
                SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaMunicipios.ListaMunicipios(null, int.Parse(_indiceDepartamento), null);
                cboMunicipioOrigenApoderado.DataSource = _temp;
                cboMunicipioOrigenApoderado.DataValueField = "MUN_ID";
                cboMunicipioOrigenApoderado.DataTextField = "MUN_NOMBRE";
                cboMunicipioOrigenApoderado.DataBind();
                cboMunicipioOrigenApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                if (_indiceMunicipio != null)
                    cboMunicipioOrigenApoderado.SelectedIndex = retornarIndice(cboMunicipioOrigenApoderado, _indiceMunicipio);
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboMunicipioOrigen.Finalizo");
        }
    }
    
    private void CargarCombosContacto(string _idPais, string _idDepart, string _idMunic, string _idCorreg, string _idVereda)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosContacto.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();

            DataSet _temp = null;

            if (_configuracion.IdPaisPredeterminado.ToString() == _idPais)
            {
                SILPA.LogicaNegocio.Generico.Listas _listas = new SILPA.LogicaNegocio.Generico.Listas();

                if (!String.IsNullOrEmpty(_idPais))
                {
                    _temp = _listas.ListarDepartamentos(int.Parse(_idPais));
                    cboDepartamentoApoderado.Enabled = true;
                    cboMunicipioApoderado.Enabled = true;
                    cboDepartamentoApoderado.DataSource = _temp;
                    cboDepartamentoApoderado.DataValueField = "DEP_ID";
                    cboDepartamentoApoderado.DataTextField = "DEP_NOMBRE";
                    cboDepartamentoApoderado.DataBind();
                    cboDepartamentoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    cboDepartamentoApoderado.SelectedIndex = retornarIndice(cboDepartamentoApoderado, _idDepart);
                }

                if (!String.IsNullOrEmpty(_idDepart))
                {
                    _temp = _listas.ListaMunicipios(null, int.Parse(_idDepart), null);
                    cboMunicipioApoderado.DataSource = _temp;
                    cboMunicipioApoderado.DataValueField = "MUN_ID";
                    cboMunicipioApoderado.DataTextField = "MUN_NOMBRE";
                    cboMunicipioApoderado.DataBind();
                    cboMunicipioApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                }


                if (!String.IsNullOrEmpty(_idMunic))
                {
                    cboMunicipioApoderado.SelectedIndex = retornarIndice(cboMunicipioApoderado, _idMunic);
                    _temp = _listas.ListarCorregimientos(int.Parse(_idMunic), null);
                    cboCorregimientoApoderado.DataSource = _temp;
                    cboCorregimientoApoderado.DataValueField = "COR_ID";
                    cboCorregimientoApoderado.DataTextField = "COR_NOMBRE";
                    cboCorregimientoApoderado.DataBind();
                    cboCorregimientoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                }

                //if(_idCorreg != "")
                if (!String.IsNullOrEmpty(_idCorreg))
                    cboCorregimientoApoderado.SelectedIndex = retornarIndice(cboCorregimientoApoderado, _idCorreg);

                if (!String.IsNullOrEmpty(_idCorreg))
                {
                    _temp = _listas.ListarVeredas(int.Parse(_idMunic), int.Parse(_idCorreg), null);
                    cboVeredaApoderado.DataSource = _temp;
                    cboVeredaApoderado.DataValueField = "VER_ID";
                    cboVeredaApoderado.DataTextField = "VER_NOMBRE";
                    cboVeredaApoderado.DataBind();
                    cboVeredaApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                }
                if (!String.IsNullOrEmpty(_idVereda))
                    cboVeredaApoderado.SelectedIndex = retornarIndice(cboVeredaApoderado, _idVereda);
                //if (_idVereda != "")
                //  cboVeredaApoderado.SelectedIndex = retornarIndice(cboVeredaApoderado, _idVereda);
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosContacto.Finalizo");
        }
    }

    #endregion      
    
    protected void cboCorregimientoApoderado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoApoderado_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoCor = int.Parse(cboCorregimientoApoderado.SelectedItem.Value);
            int _codigoMun = int.Parse(cboMunicipioApoderado.SelectedItem.Value);
            DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
            cboVeredaApoderado.DataSource = _temp;
            cboVeredaApoderado.DataValueField = "VER_ID";
            cboVeredaApoderado.DataTextField = "VER_NOMBRE";
            cboVeredaApoderado.DataBind();
            cboVeredaApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoApoderado_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboMunicipioApoderado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioApoderado_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoMun = int.Parse(cboMunicipioApoderado.SelectedItem.Value);
            DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
            cboCorregimientoApoderado.DataSource = _temp;
            cboCorregimientoApoderado.DataValueField = "COR_ID";
            cboCorregimientoApoderado.DataTextField = "COR_NOMBRE";
            cboCorregimientoApoderado.DataBind();
            cboCorregimientoApoderado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioApoderado_SelectedIndexChanged.Finalizo");
        }
    }

    protected bool ValidaUsuario()
    {
        if ((DataTable)Session["Apoderados"] != null &&
                ((DataTable)Session["Apoderados"]).Rows.Count != 0)
        {
            DataTable _apoderados = new DataTable();
            _apoderados = (DataTable)Session["Apoderados"];

            if (_apoderados.Select("ID_IDENTIFICACION = '" + txtNumeroIdentificacionApoderado.Text.Trim() + "'").Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }

    /// <summary>
    /// JMM - 14/07/2010
    /// Metodo pora cargar los combos de tipo de documento segun el tipo de persona.
    /// </summary>
    /// <param name="dsTipoIdentificacion"></param>
    /// <param name="strCondicion"></param>
    /// <returns></returns>
    private DataTable ListaDocumentos(DataSet dsTipoIdentificacion, string strCondicion)
    {
        DataTable _dtIdentificacion = new DataTable();
        DataRow _Fila;

        _dtIdentificacion.Columns.Add("TPE_ID", Type.GetType("System.String"));
        _dtIdentificacion.Columns.Add("TID_ID", Type.GetType("System.String"));
        _dtIdentificacion.Columns.Add("TID_NOMBRE", Type.GetType("System.String"));
        _dtIdentificacion.Columns.Add("TID_ACTIVO", Type.GetType("System.String"));

        foreach (DataRow _registro in dsTipoIdentificacion.Tables[0].Select(strCondicion))
        {
            _Fila = _dtIdentificacion.NewRow();
            _Fila["TPE_ID"] = _registro[0];
            _Fila["TID_ID"] = _registro[1];
            _Fila["TID_NOMBRE"] = _registro[2];
            _Fila["TID_ACTIVO"] = _registro[3];

            _dtIdentificacion.Rows.Add(_Fila);
        }

        return _dtIdentificacion;
    }
}
