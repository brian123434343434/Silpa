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

public partial class RepresentanteLegal : System.Web.UI.Page
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
            CargarCombosOrigenRepresentante();

            cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));


            if (_modo == "1")
            {
                cboTipoDocumentoJuridica.Enabled = false;
                txtIdentificacionJuridica.Enabled = false;
                cboDepartamentoOrigenRepresentante.Enabled = false;
                cboMunicipioOrigenRepresentante.Enabled = false;
            
                CargarDatos();
            }
        }
    }

    protected void btnCancelarRepresentante_Click(object sender, EventArgs e)
    {
        string strScript2;
        strScript2 = "<script language='JavaScript'>self.close()</script>";
        Page.RegisterClientScriptBlock("Pruebas2", strScript2);
    }

    protected void btnAceptarRepresentante_Click(object sender, EventArgs e)
    {
        DataTable _representantes = new DataTable();
        DataRow _fila;

        try
        {
            if (Page.IsValid)
            {
                _representantes = (DataTable)Session["Representantes"];
                if (_modo == "0")
                {
                    if (ValidaUsuario())
                    {
                        _fila = _representantes.NewRow();

                        _fila["PRIMER_NOMBRE"] = txtPrimerNombreJuridica.Text;
                        _fila["SEGUNDO_NOMBRE"] = txtSegundoNombreJuridica.Text;
                        _fila["PRIMER_APELLIDO"] = txtPrimerApellidoJuridica.Text;
                        _fila["SEGUNDO_APELLIDO"] = txtSegundoApellidoJuridica.Text;
                        _fila["TARJETA_PROFESIONAL"] = txtTarjetaProfesional.Text;
                        _fila["ID_TIPO_ID"] = cboTipoDocumentoJuridica.SelectedItem.Value;
                        _fila["TIPO_ID"] = cboTipoDocumentoJuridica.SelectedItem.Text;
                        _fila["ID_IDENTIFICACION"] = txtIdentificacionJuridica.Text;
                        _fila["ID_ORIGEN_DEPARTAMENTO"] = cboDepartamentoOrigenRepresentante.SelectedItem.Value;
                        _fila["ORIGEN_DEPARTAMENTO"] = cboDepartamentoOrigenRepresentante.SelectedItem.Text;
                        _fila["ID_ORIGEN_MUNICIPIO"] = cboMunicipioOrigenRepresentante.SelectedItem.Value;
                        _fila["ORIGEN_MUNICIPIO"] = cboMunicipioOrigenRepresentante.SelectedItem.Text;
                        _fila["DIRECCION_CORRESPONDENCIA"] = txtDireccionJuridica.Text;
                        _fila["ID_PAIS"] = cboPaisJuridica.SelectedItem.Value;
                        _fila["PAIS"] = cboPaisJuridica.SelectedItem.Text;
                        _fila["ID_DEPARTAMENTO"] = (cboDepartamentoJuridica.SelectedItem != null) ? cboDepartamentoJuridica.SelectedItem.Value : "";
                        _fila["DEPARTAMENTO"] = (cboDepartamentoJuridica.SelectedValue != "-1") ? cboDepartamentoJuridica.SelectedItem.Text : "";
                        _fila["ID_MUNICIPIO"] = (cboMunicipioJuridica.SelectedItem != null) ? cboMunicipioJuridica.SelectedItem.Value : "";
                        _fila["MUNICIPIO"] = (cboMunicipioJuridica.SelectedValue != "-1") ? cboMunicipioJuridica.SelectedItem.Text : "";
                        _fila["ID_VEREDA"] = (cboVeredaJuridica.SelectedValue != "-1") ? cboVeredaJuridica.SelectedValue : "";
                        _fila["VEREDA"] = (cboVeredaJuridica.SelectedValue != "-1") ? cboVeredaJuridica.SelectedItem.Text : "";
                        _fila["ID_CORREGIMIENTO"] = (cboCorregimientoJuridica.SelectedValue != "-1") ? cboCorregimientoJuridica.SelectedValue : "";
                        _fila["CORREGIMIENTO"] = (cboCorregimientoJuridica.SelectedValue != "-1") ? cboCorregimientoJuridica.SelectedItem.Text : "";
                        _fila["TELEFONO"] = txtTelefonoJuridica.Text;
                        _fila["CELULAR"] = txtCelularJuridica.Text;
                        _fila["FAX"] = txtFaxJuridica.Text;
                        _fila["CORREO"] = txtCorreoJuridica.Text;
                        _fila["ESTADO"] = true;

                        _representantes.Rows.Add(_fila);
                        _representantes.AcceptChanges();

                        Session["Representantes"] = _representantes;
                    }
                    else
                    {
                        string strScript = "<script language='JavaScript'>" +
                            "alert('Ya existe un representante legal con este numero de documento.')" +
                            "</script>";
                        Page.RegisterStartupScript("PopupScript", strScript);
                    }
                    GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                    CrearLogAuditoria.Insertar("SILPA", 1, "Se almaceno Representante Legal");
                }
                else if (_modo == "1")
                {
                    if (_representantes.Select("ID_IDENTIFICACION = '" + _identificacion + "'").Length > 0)
                    {
                        foreach (DataRow _temp in _representantes.Rows)
                        {
                            if (_temp["ID_IDENTIFICACION"].ToString() == _identificacion)
                            {
                                _temp["PRIMER_NOMBRE"] = txtPrimerNombreJuridica.Text;
                                _temp["SEGUNDO_NOMBRE"] = txtSegundoNombreJuridica.Text;
                                _temp["PRIMER_APELLIDO"] = txtPrimerApellidoJuridica.Text;
                                _temp["SEGUNDO_APELLIDO"] = txtSegundoApellidoJuridica.Text;
                                _temp["TARJETA_PROFESIONAL"] = txtTarjetaProfesional.Text;
                                _temp["ID_TIPO_ID"] = cboTipoDocumentoJuridica.SelectedItem.Value;
                                _temp["TIPO_ID"] = cboTipoDocumentoJuridica.SelectedItem.Text;
                                _temp["ID_ORIGEN_DEPARTAMENTO"] = cboDepartamentoOrigenRepresentante.SelectedItem.Value;
                                _temp["ORIGEN_DEPARTAMENTO"] = cboDepartamentoOrigenRepresentante.SelectedItem.Text;
                                _temp["ID_ORIGEN_MUNICIPIO"] = cboMunicipioOrigenRepresentante.SelectedItem.Value;
                                _temp["ORIGEN_MUNICIPIO"] = cboMunicipioOrigenRepresentante.SelectedItem.Text;
                                _temp["DIRECCION_CORRESPONDENCIA"] = txtDireccionJuridica.Text;
                                _temp["ID_PAIS"] = cboPaisJuridica.SelectedItem.Value;
                                _temp["PAIS"] = cboPaisJuridica.SelectedItem.Text;
                                _temp["ID_DEPARTAMENTO"] = (cboDepartamentoJuridica.SelectedValue != "-1") ? cboDepartamentoJuridica.SelectedItem.Value : "";
                                _temp["DEPARTAMENTO"] = (cboDepartamentoJuridica.SelectedValue != "-1") ? cboDepartamentoJuridica.SelectedItem.Text : "";
                                _temp["ID_MUNICIPIO"] = (cboMunicipioJuridica.SelectedValue != "-1") ? cboMunicipioJuridica.SelectedItem.Value : "";
                                _temp["MUNICIPIO"] = (cboMunicipioJuridica.SelectedValue != "-1") ? cboMunicipioJuridica.SelectedItem.Text : "";
                                _temp["ID_VEREDA"] = (cboVeredaJuridica.SelectedValue != "-1") ? cboVeredaJuridica.SelectedValue : "";
                                _temp["VEREDA"] = (cboVeredaJuridica.SelectedValue != "-1") ? cboVeredaJuridica.SelectedItem.Text : "";
                                _temp["ID_CORREGIMIENTO"] = (cboCorregimientoJuridica.SelectedValue != "-1") ? cboCorregimientoJuridica.SelectedValue : "";
                                _temp["CORREGIMIENTO"] = (cboCorregimientoJuridica.SelectedValue != "-1") ? cboCorregimientoJuridica.SelectedItem.Text : "";
                                _temp["TELEFONO"] = txtTelefonoJuridica.Text;
                                _temp["CELULAR"] = txtCelularJuridica.Text;
                                _temp["FAX"] = txtFaxJuridica.Text;
                                _temp["CORREO"] = txtCorreoJuridica.Text;
                                _temp["ESTADO"] = chkEstado.Checked;

                                _representantes.AcceptChanges();
                                Session["Representantes"] = _representantes;
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

    protected void cboPaisJuridica_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisJuridica_SelectedIndexChanged.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            if (cboPaisJuridica.SelectedValue == _configuracion.IdPaisPredeterminado.ToString())
            {

                SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
                //cboDepartamentoJuridica.Enabled = true;
                //cboMunicipioJuridica.Enabled = true;
                cboDepartamentoJuridica.DataSource = _temp;
                cboDepartamentoJuridica.DataValueField = "DEP_ID";
                cboDepartamentoJuridica.DataTextField = "DEP_NOMBRE";
                cboDepartamentoJuridica.DataBind();
                cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                //cboDepartamentoJuridica.SelectedIndex = 0;
                CargarComboMunicipios(cboDepartamentoJuridica.SelectedItem.Value, null);
                //covDepartamentoJuridica.Enabled = true;
                //covMunicipioJuridica.Enabled = true;
            }
            else
            {
                //cboDepartamentoJuridica.Enabled = false;
                //cboMunicipioJuridica.Enabled = false;
                cboDepartamentoJuridica.Items.Clear();
                cboMunicipioJuridica.Items.Clear();
                cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboMunicipioJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                //covDepartamentoJuridica.Enabled = false;
                //covMunicipioJuridica.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisJuridica_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboDepartamentoJuridica_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoJuridica_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamentoJuridica.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioJuridica.DataSource = _temp;
            cboMunicipioJuridica.DataValueField = "MUN_ID";
            cboMunicipioJuridica.DataTextField = "MUN_NOMBRE";
            cboMunicipioJuridica.DataBind();
            cboMunicipioJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioJuridica.Enabled = true;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoJuridica_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboDepartamentoOrigenRepresentante_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoOrigenRepresentante_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamentoOrigenRepresentante.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioOrigenRepresentante.DataSource = _temp;
            cboMunicipioOrigenRepresentante.DataValueField = "MUN_ID";
            cboMunicipioOrigenRepresentante.DataTextField = "MUN_NOMBRE";
            cboMunicipioOrigenRepresentante.DataBind();
            cboMunicipioOrigenRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioOrigenRepresentante.Enabled = true;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoOrigenRepresentante_SelectedIndexChanged.Finalizo");
        }
    }

    #region Funciones Programador

    /// <summary>
    /// Funcion que carga los datos de un representante para su actualizacion
    /// </summary>
    private void CargarDatos()
    {
        DataTable _representantes = new DataTable();
        //DataRow _fila;
        
        try
        {
            _representantes = (DataTable)Session["Representantes"];
            if (_representantes.Select("ID_IDENTIFICACION = '" + _identificacion + "'").Length > 0)
            {                
                foreach (DataRow _temp in _representantes.Rows)
                {                    
                    if (_temp["ID_IDENTIFICACION"].ToString() == _identificacion)
                    {
                        txtPrimerNombreJuridica.Text = _temp["PRIMER_NOMBRE"].ToString();
                        txtSegundoNombreJuridica.Text = _temp["SEGUNDO_NOMBRE"].ToString();
                        txtPrimerApellidoJuridica.Text = _temp["PRIMER_APELLIDO"].ToString();
                        txtSegundoApellidoJuridica.Text = _temp["SEGUNDO_APELLIDO"].ToString();
                        txtTarjetaProfesional.Text = _temp["TARJETA_PROFESIONAL"].ToString();
                        cboTipoDocumentoJuridica.SelectedIndex = retornarIndice(cboTipoDocumentoJuridica,_temp["ID_TIPO_ID"].ToString());
                        txtIdentificacionJuridica.Text = _temp["ID_IDENTIFICACION"].ToString();
                        CargarComboDepartamentoOrigen(_temp["ID_ORIGEN_DEPARTAMENTO"].ToString(), _temp["ID_ORIGEN_MUNICIPIO"].ToString());
                        txtDireccionJuridica.Text = _temp["DIRECCION_CORRESPONDENCIA"].ToString();
                        cboPaisJuridica.SelectedIndex = retornarIndice(cboPaisJuridica,_temp["ID_PAIS"].ToString());
                        CargarCombosContacto(
                                _temp["ID_PAIS"].ToString(),
                                _temp["ID_DEPARTAMENTO"].ToString(),
                                _temp["ID_MUNICIPIO"].ToString(),
                                _temp["ID_CORREGIMIENTO"].ToString(),
                                _temp["ID_VEREDA"].ToString()
                            );

                        //CargarComboDepartamentos(_temp["ID_PAIS"].ToString(), _temp["ID_DEPARTAMENTO"].ToString(), _temp["ID_MUNICIPIO"].ToString());
                        //txtVeredaJuridica.Text = _temp["VEREDA"].ToString();
                        //txtCorregimientoJuridica.Text = _temp["CORREGIMIENTO"].ToString();
                        txtTelefonoJuridica.Text = _temp["TELEFONO"].ToString();
                        txtCelularJuridica.Text = _temp["CELULAR"].ToString();
                        txtFaxJuridica.Text = _temp["FAX"].ToString();
                        txtCorreoJuridica.Text = _temp["CORREO"].ToString();
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

            //cboTipoDocumentoJuridica.DataSource = _listaTiposId.ListaTipoIdentificacion();
            cboTipoDocumentoJuridica.DataSource = ListaDocumentos(_Temp, "TPE_ID = " + Convert.ToString((int)SILPA.Comun.TipoPersona.RepresentanteLegal));
            cboTipoDocumentoJuridica.DataValueField = "TID_ID";
            cboTipoDocumentoJuridica.DataTextField = "TID_NOMBRE";
            cboTipoDocumentoJuridica.DataBind();
            cboTipoDocumentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
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
            cboPaisJuridica.DataSource = _temp;
            cboPaisJuridica.DataValueField = "PAI_ID";
            cboPaisJuridica.DataTextField = "PAI_NOMBRE";
            cboPaisJuridica.DataBind();
            cboPaisJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            cboPaisJuridica.SelectedValue = _configuracion.IdPaisPredeterminado.ToString();
            //cboDepartamentoJuridica.Enabled = false;
            //cboMunicipioJuridica.Enabled = false;

            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _tempDepto = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
            //cboDepartamentoJuridica.Enabled = true;
            //cboMunicipioJuridica.Enabled = true;
            cboDepartamentoJuridica.DataSource = _tempDepto;
            cboDepartamentoJuridica.DataValueField = "DEP_ID";
            cboDepartamentoJuridica.DataTextField = "DEP_NOMBRE";
            cboDepartamentoJuridica.DataBind();
            cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            //cboDepartamentoJuridica.SelectedIndex = 0;
            CargarComboMunicipios(cboDepartamentoJuridica.SelectedItem.Value, null);
            //covDepartamentoJuridica.Enabled = true;
            //covMunicipioJuridica.Enabled = true;
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
    /// Funcion que carga el combo de Departamentos y selecciona uno segun los datos del Representante Legal.
    /// </summary>
    /// <param name="_indicePais"></param>
    /// <param name="_indiceDepartamento"></param>
    private void CargarComboDepartamentos(string _indicePais, string _indiceDepartamento,string _indiceMunicipio)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarComboDepartamentos.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            if (_configuracion.IdPaisPredeterminado.ToString() == _indicePais)
            {
                SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaDepartamentos.ListarDepartamentos(int.Parse(_indicePais));
                cboDepartamentoJuridica.Enabled = true;
                cboMunicipioJuridica.Enabled = true;
                cboDepartamentoJuridica.DataSource = _temp;
                cboDepartamentoJuridica.DataValueField = "DEP_ID";
                cboDepartamentoJuridica.DataTextField = "DEP_NOMBRE";
                cboDepartamentoJuridica.DataBind();
                cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboDepartamentoJuridica.SelectedIndex = retornarIndice(cboDepartamentoJuridica, _indiceDepartamento);
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
    /// Funcion que carga el combo de Municipios y selecciona uno segun los datos del Representante Legal.
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
                cboMunicipioJuridica.DataSource = _temp;
                cboMunicipioJuridica.DataValueField = "MUN_ID";
                cboMunicipioJuridica.DataTextField = "MUN_NOMBRE";
                cboMunicipioJuridica.DataBind();
                cboMunicipioJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                if (_indiceMunicipio != null)
                    cboMunicipioJuridica.SelectedIndex = retornarIndice(cboMunicipioJuridica, _indiceMunicipio);
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
    /// Carga los combos para seleccionar el lugar de expedicion del Documento en representante
    /// </summary>
    private void CargarCombosOrigenRepresentante()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosOrigenRepresentante.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();

            DataSet _temp1 = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
            cboDepartamentoOrigenRepresentante.DataSource = _temp1;
            cboDepartamentoOrigenRepresentante.DataValueField = "DEP_ID";
            cboDepartamentoOrigenRepresentante.DataTextField = "DEP_NOMBRE";
            cboDepartamentoOrigenRepresentante.DataBind();
            cboDepartamentoOrigenRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            DataSet _temp2 = _listaMunicipios.ListaMunicipios(null, int.Parse(cboDepartamentoOrigenRepresentante.SelectedValue), null);
            cboMunicipioOrigenRepresentante.DataSource = _temp2;
            cboMunicipioOrigenRepresentante.DataValueField = "MUN_ID";
            cboMunicipioOrigenRepresentante.DataTextField = "MUN_NOMBRE";
            cboMunicipioOrigenRepresentante.DataBind();
            cboMunicipioOrigenRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarCombosOrigenRepresentante.Finalizo");
        }
    }

    /// <summary>
    /// Carga el combo del departamento de origen del representante, selecciona el departameno y carga los municipios
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
            //cboDepartamentoOrigenRepresentante.Enabled = true;
            //cboMunicipioOrigenRepresentante.Enabled = true;
            cboDepartamentoOrigenRepresentante.DataSource = _temp;
            cboDepartamentoOrigenRepresentante.DataValueField = "DEP_ID";
            cboDepartamentoOrigenRepresentante.DataTextField = "DEP_NOMBRE";
            cboDepartamentoOrigenRepresentante.DataBind();
            cboDepartamentoOrigenRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboDepartamentoOrigenRepresentante.SelectedIndex = retornarIndice(cboDepartamentoOrigenRepresentante, _indiceDepartamento);
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
                cboMunicipioOrigenRepresentante.DataSource = _temp;
                cboMunicipioOrigenRepresentante.DataValueField = "MUN_ID";
                cboMunicipioOrigenRepresentante.DataTextField = "MUN_NOMBRE";
                cboMunicipioOrigenRepresentante.DataBind();
                cboMunicipioOrigenRepresentante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                if (_indiceMunicipio != null)
                    cboMunicipioOrigenRepresentante.SelectedIndex = retornarIndice(cboMunicipioOrigenRepresentante, _indiceMunicipio);
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
            if (_configuracion.IdPaisPredeterminado.ToString() == _idPais)
            {
                SILPA.LogicaNegocio.Generico.Listas _listas = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listas.ListarDepartamentos(int.Parse(_idPais));
                cboDepartamentoJuridica.Enabled = true;
                cboMunicipioJuridica.Enabled = true;
                cboDepartamentoJuridica.DataSource = _temp;
                cboDepartamentoJuridica.DataValueField = "DEP_ID";
                cboDepartamentoJuridica.DataTextField = "DEP_NOMBRE";
                cboDepartamentoJuridica.DataBind();
                cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboDepartamentoJuridica.SelectedIndex = retornarIndice(cboDepartamentoJuridica, _idDepart);

                _temp = _listas.ListaMunicipios(null, int.Parse(_idDepart), null);
                cboMunicipioJuridica.DataSource = _temp;
                cboMunicipioJuridica.DataValueField = "MUN_ID";
                cboMunicipioJuridica.DataTextField = "MUN_NOMBRE";
                cboMunicipioJuridica.DataBind();
                cboMunicipioJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboMunicipioJuridica.SelectedIndex = retornarIndice(cboMunicipioJuridica, _idMunic);

                _temp = _listas.ListarCorregimientos(int.Parse(_idMunic), null);
                cboCorregimientoJuridica.DataSource = _temp;
                cboCorregimientoJuridica.DataValueField = "COR_ID";
                cboCorregimientoJuridica.DataTextField = "COR_NOMBRE";
                cboCorregimientoJuridica.DataBind();
                cboCorregimientoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboCorregimientoJuridica.SelectedIndex = retornarIndice(cboCorregimientoJuridica, _idCorreg);

                if (_idCorreg != "")
                {
                    cboCorregimientoJuridica.SelectedIndex = retornarIndice(cboCorregimientoJuridica, _idCorreg);

                    _temp = _listas.ListarVeredas(int.Parse(_idMunic), int.Parse(_idCorreg), null);
                    cboVeredaJuridica.DataSource = _temp;
                    cboVeredaJuridica.DataValueField = "VER_ID";
                    cboVeredaJuridica.DataTextField = "VER_NOMBRE";
                    cboVeredaJuridica.DataBind();
                    cboVeredaJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    if (_idVereda != "")
                        cboVeredaJuridica.SelectedIndex = retornarIndice(cboVeredaJuridica, _idVereda);
                }
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

    protected void cboMunicipioJuridica_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioJuridica_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoMun = int.Parse(cboMunicipioJuridica.SelectedItem.Value);
            DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
            cboCorregimientoJuridica.DataSource = _temp;
            cboCorregimientoJuridica.DataValueField = "COR_ID";
            cboCorregimientoJuridica.DataTextField = "COR_NOMBRE";
            cboCorregimientoJuridica.DataBind();
            cboCorregimientoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioJuridica_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboCorregimientoJuridica_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoJuridica_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoCor = int.Parse(cboCorregimientoJuridica.SelectedItem.Value);
            int _codigoMun = int.Parse(cboMunicipioJuridica.SelectedItem.Value);
            DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
            cboVeredaJuridica.DataSource = _temp;
            cboVeredaJuridica.DataValueField = "VER_ID";
            cboVeredaJuridica.DataTextField = "VER_NOMBRE";
            cboVeredaJuridica.DataBind();
            cboVeredaJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoJuridica_SelectedIndexChanged.Finalizo");
        }
    }

    protected bool ValidaUsuario()
    {
        if ((DataTable)Session["Representantes"] != null &&
            ((DataTable)Session["Representantes"]).Rows.Count != 0)
        {
            DataTable _representante = new DataTable();
            _representante = (DataTable)Session["Representantes"];

            if (_representante.Select("ID_IDENTIFICACION = '" + txtIdentificacionJuridica.Text.Trim() + "'").Length > 0)
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

