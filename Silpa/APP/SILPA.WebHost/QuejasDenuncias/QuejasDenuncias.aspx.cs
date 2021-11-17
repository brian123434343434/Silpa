using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SILPA.AccesoDatos.ImpresionesFus;
using System.IO;
using SILPA.Servicios.Generico;
using SILPA.Servicios.SolicitudDAA;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using SILPA.LogicaNegocio;
using SILPA.Servicios;
using SILPA.AccesoDatos.BPMProcess;
using System.Xml.Serialization;
using SILPA.LogicaNegocio.Sancionatorio;
using SILPA.LogicaNegocio.DAA;
using SoftManagement.ServicioCorreoElectronico;
using System.Threading;

public partial class QuejasDenuncias_QuejasDenuncias : System.Web.UI.Page
{
    private List<Byte[]> _bytes;
    private List<string> _archivo;
    private List<int> _tamanio;
    private string _usuarioQuejosoRegistrado;
    private List<string> listaCoordenadas;
    private List<string> listaNombreArchivos;    

    protected void Page_Load(object sender, EventArgs e)
    {

        this._usuarioQuejosoRegistrado = string.Empty;
        listaCoordenadas = new List<string>();
        Label lblMensaje = (Label)this.Page.Master.FindControl("lblMensaje");
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnAdjuntar);
        lblMensaje.Text = "";
        if (Page.Request["Ubic"] == null)
        {
            //if (ValidacionToken())
            //{
                this._usuarioQuejosoRegistrado = (string)Session["Usuario"];
            //}
        }
        
        if (!IsPostBack)
        {
            this.IdUser.Text = "";
            this.tb_Quejoso.Visible = true;
            this.listaCoordenadas = new List<string>();
            this.listaNombreArchivos = new List<string>();

            if (!string.IsNullOrEmpty(this._usuarioQuejosoRegistrado))
                trCaptcha.Visible = false;         

            ViewState["listaNombreArchivos"] = this.listaNombreArchivos;
            ViewState["listaCoordenadas "] = this.listaCoordenadas;

            CargarDepartamento(cboDepartamentoQueja, cboMunicipioQueja);
            CargarDepartamento(cboDepartamentoOrigen, cboMunicipioOrigen);
            CargarDepartamento(cboDepartamentoOrigenDenunciante, cboMunicipioOrigenDenunciante);
            CargarDepartamento(cboDepartamentoQuejoso, cboMunicipioQuejoso);
            CargarDepartamento(cboDepartamentoInfractor, cboMunicipioInfractor);
            CargarPaises();
            
            CargarTiposIdentificacion();
            
            //CargarSectores();
            CargarZonas();
            CargarAreasHidrograficas();
            CargarRecursos();
         //   CargarAutoridades();

            cboAutoridadQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1")); 
            cboVeredaQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboDepartamentoQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboDepartamentoInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboZonaHidrografica.Items.Insert(0, new ListItem("Seleccione...", "0"));
            cboSubZonaHidrografica.Items.Insert(0, new ListItem("Seleccione...", "0"));
            lblMensajeCoordenada.Visible = false;
            lblMensajeArchivos.Visible = false;
            if (!string.IsNullOrEmpty(this._usuarioQuejosoRegistrado))
                CargarDatosUsuarioRegistrado();
        }
    }

    /// <summary>
    /// Método para verificar si el quejoso es usuario registrado.
    /// </summary>
    /// <returns></returns>
    private bool ValidacionToken()
    {
        //DESCOMENTAR ANTES DEL COMMIT!!!
        //Session["IDForToken"] = Request.QueryString["IdRelated"];
        //Session["IDForToken"] = "7";

        if (DatosSesion.Usuario == string.Empty) 
        {
            return false;
        }
        string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

        Session["IDForToken"] = (object)idUsuario;

        Session["Usuario"] = Session["IDForToken"];

        SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

        Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));

        using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
        {
            servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
            string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
            //string mensaje = "Token valido";
           
            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }
    
    /// <summary>
    /// Carga los datos de un usuario registrado que presenta queja
    /// </summary>
    private void CargarDatosUsuarioRegistrado()
    {
        if(!String.IsNullOrEmpty(this._usuarioQuejosoRegistrado))
        {
            Persona objPersona = new Persona();
            objPersona.CargarDatosPersona(this._usuarioQuejosoRegistrado);
            this.IdUser.Text = objPersona.PersonaIdentity.IdApplicationUser.ToString();
            this.txtPrimerNombreQuejoso.Text = objPersona.PersonaIdentity.PrimerNombre;
            this.txtSegundoNombreQuejoso.Text = objPersona.PersonaIdentity.SegundoNombre;
            this.txtPrimerApellidoQuejoso.Text = objPersona.PersonaIdentity.PrimerApellido;
            this.txtSegundoApellidoQuejoso.Text = objPersona.PersonaIdentity.SegundoApellido;
            DireccionPersonaDalc objDireccion = new DireccionPersonaDalc();
            DireccionPersonaIdentity objDir = new DireccionPersonaIdentity();
            objDir.IdPersona = objPersona.PersonaIdentity.IdApplicationUser;
            objDireccion.ObtenerDireccionPersona(ref objDir);

            this.cboPaisQuejoso.SelectedValue = objDir.PaisId.ToString();
            if (!string.IsNullOrEmpty(objDir.DepartamentoId.ToString()))
                this.cboDepartamentoQuejoso.SelectedValue = objDir.DepartamentoId.ToString();
            if (!string.IsNullOrEmpty(objDir.MunicipioId.ToString()))
                this.cboMunicipioQuejoso.SelectedValue = objDir.MunicipioId.ToString();
            if (!string.IsNullOrEmpty(objDir.CorregimientoId.ToString()))
                this.cboCorregimientoQuejoso.SelectedValue = objDir.CorregimientoId.ToString();
            if (!string.IsNullOrEmpty(objDir.VeredaId.ToString()))
                this.cboVeredaQuejoso.SelectedValue = objDir.VeredaId.ToString();

            this.txtCorreoQuejoso.Text = objPersona.PersonaIdentity.CorreoElectronico;
            this.txtTelefonoQuejoso.Text = objPersona.PersonaIdentity.Telefono;

            this.tb_Quejoso.Visible = false;
        }
        
    }

    protected void cboPaisQuejoso_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        //    SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisQuejoso_SelectedIndexChanged.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            if (cboPaisQuejoso.SelectedValue == _configuracion.IdPaisPredeterminado.ToString())
            {
                SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
                cboDepartamentoQuejoso.Enabled = true;
                cboMunicipioQuejoso.Enabled = true;
                cboDepartamentoQuejoso.DataSource = _temp;
                cboDepartamentoQuejoso.DataValueField = "DEP_ID";
                cboDepartamentoQuejoso.DataTextField = "DEP_NOMBRE";
                cboDepartamentoQuejoso.DataBind();
                cboDepartamentoQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboDepartamentoQuejoso.SelectedIndex = 0;
                CargarComboMunicipios(cboMunicipioQuejoso, cboDepartamentoQuejoso.SelectedItem.Value);
            }
            else
            {
                cboDepartamentoQuejoso.Enabled = false;
                cboMunicipioQuejoso.Enabled = false;
                cboDepartamentoQuejoso.Items.Clear();
                cboMunicipioQuejoso.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisQuejoso_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboDepartamentoQuejoso_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoQuejoso_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamentoQuejoso.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioQuejoso.DataSource = _temp;
            cboMunicipioQuejoso.DataValueField = "MUN_ID";
            cboMunicipioQuejoso.DataTextField = "MUN_NOMBRE";
            cboMunicipioQuejoso.DataBind();
            cboMunicipioQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioQuejoso.Enabled = true;
          
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            //SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoQuejoso_SelectedIndexChanged.Finalizo");
        }
    }

    protected void btnAdjuntar_Click(object sender, EventArgs e)
    {
        string _nombreArchivo = uplAdjuntarArchivo.PostedFile.FileName;

        if (uplAdjuntarArchivo.PostedFile.ContentLength == 0)
        {
            lblMensajeArchivos.Visible = true;
            lblMensajeArchivos.Text = "El archivo no contiene información, no se adjuntará ";
            return;
        }
        else 
        {
            lblMensajeArchivos.Visible = false;
        }


        if (ViewState["i"] == null)
        {        
            ViewState["i"] = 0;
            _bytes=new List<byte[]>();
            _archivo=new List<string>();
            _tamanio = new List<int>();
            Session.Add("ListaBytes", _bytes);
            Session.Add("ListaArchivos", _archivo);
            Session.Add("TamanioArchivos", _tamanio);            
        }
        
        // Valiad si fue seleccionado un archivo
        if (_nombreArchivo != null && _nombreArchivo != string.Empty)
        {           

            Int64 _tamanioArchivos = 0;
            
            _bytes=(List<byte[]>)Session["ListaBytes"];
            _archivo=(List<string>)Session["ListaArchivos"];
            _tamanio = (List<int>)Session["TamanioArchivos"];
            
            
            for (int j = 0; j < _tamanio.Count; j++)
            {
                _tamanioArchivos += _tamanio[j];
            }

            _tamanioArchivos += uplAdjuntarArchivo.PostedFile.ContentLength;

            if (_tamanioArchivos < 5242880)
            {
                    //Adiciona el archivo al listbox
                    lstListaArchivos.Items.Add(uplAdjuntarArchivo.PostedFile.FileName);

                    if (ViewState["listaNombreArchivos"] != null) 
                    {
                        listaNombreArchivos = (List<string>)ViewState["listaNombreArchivos"];
                    }
                    
                    listaNombreArchivos.Add(uplAdjuntarArchivo.PostedFile.FileName);


                    _bytes.Add(uplAdjuntarArchivo.FileBytes);
                    _archivo.Add(uplAdjuntarArchivo.PostedFile.FileName);
                    _tamanio.Add(uplAdjuntarArchivo.PostedFile.ContentLength);

                    Session["ListaBytes"] = (List<byte[]>)_bytes;
                    Session["ListaArchivos"] = (List<string>)_archivo;
                    Session["TamanioArchivos"] = (List<int>)_tamanio;

                    //almacena un indice por cada archivo agregado
                    int i = Convert.ToInt32(ViewState["i"]);

                    //almacena el control de uplAdjuntarArchivo en una nueva variable de session
                    Session["AdjuntarArchivo" + i] = uplAdjuntarArchivo;

                    //Incrementa el contador
                    i++;

                    // Establece el viewState al ultimo control
                    ViewState["i"] = i;
                    ViewState["listaNombreArchivos"] = listaNombreArchivos;
            }
            else
            {
                lblMensajeArchivos.Visible = true;
                lblMensajeArchivos.Text = "El tamaño de los archivos supera el limite permitido. ";
            }
        }

  

    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        Int64 _tamanioArchivos = 0;

        if (lstListaArchivos.SelectedIndex > -1)
        {
            int _indiceArchivo = lstListaArchivos.SelectedIndex;
            Session.Remove("AdjuntarArchivo" + _indiceArchivo);
            _bytes = (List<byte[]>)Session["ListaBytes"];
            _archivo = (List<string>)Session["ListaArchivos"];
            _tamanio = (List<int>)Session["TamanioArchivos"];
            _bytes.RemoveAt(lstListaArchivos.SelectedIndex);
            _archivo.RemoveAt(lstListaArchivos.SelectedIndex);
            _tamanio.RemoveAt(lstListaArchivos.SelectedIndex);
            lstListaArchivos.Items.Remove(lstListaArchivos.SelectedValue);

            for (int j = 0; j < _tamanio.Count; j++)
            {
                _tamanioArchivos += _tamanio[j];
            }
                        
            Session["ListaBytes"] = (List<byte[]>)_bytes;
            Session["ListaArchivos"] = (List<string>)_archivo;
            Session["TamanioArchivos"] = (List<int>)_tamanio;

            lblMensajeArchivos.Visible = false;
        }
    }

    protected void cboMunicipioQueja_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioQueja_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoMun = int.Parse(cboMunicipioQueja.SelectedItem.Value);
            DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
            cboCorregimientoQueja.DataSource = _temp;
            cboCorregimientoQueja.DataValueField = "COR_ID";
            cboCorregimientoQueja.DataTextField = "COR_NOMBRE";
            cboCorregimientoQueja.DataBind();
            cboCorregimientoQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));


            SILPA.LogicaNegocio.Generico.AutoridadAmbiental _autoridad = new SILPA.LogicaNegocio.Generico.AutoridadAmbiental();
            _temp = _autoridad.ListarAAXJurisdiccion(int.Parse(cboMunicipioQueja.SelectedValue));
          

            

            cboAutoridadQueja.DataSource = _temp;
            cboAutoridadQueja.DataValueField = "AUT_ID";
            cboAutoridadQueja.DataTextField = "AUT_NOMBRE";
            cboAutoridadQueja.DataBind();
            //Quemo El dato Bogota para recibir el codigo del ministerio
            if (_temp.Tables.Count > 0)
            {
                if (_temp.Tables[0].Rows.Count == 0)
                {
                    Jurisdiccion objJur = new Jurisdiccion();
                    DataSet ds = objJur.ListaJurisdiccion(int.Parse(cboMunicipioQueja.SelectedValue));
                    Mensaje.MostrarMensaje(this.Page, "Al seleccionar el municipio " + this.cboMunicipioQueja.SelectedItem.ToString() + " se encontró que la autoridad " + ds.Tables[0].Rows[0]["AUT_NOMBRE"].ToString() + " debe atender la solicitud, ésta autoridad ambiental no se encuentra integrada en la ventanilla amiental por lo que usted debe dirigirse personalmente a ella");
                }
                _temp = _autoridad.ListarAAXJurisdiccion(int.Parse("11001"));
                if (!cboAutoridadQueja.Items.Contains(new ListItem(_temp.Tables[0].Rows[0]["AUT_NOMBRE"].ToString(), _temp.Tables[0].Rows[0]["AUT_ID"].ToString())))
                    cboAutoridadQueja.Items.Insert(0, new ListItem(_temp.Tables[0].Rows[0]["AUT_NOMBRE"].ToString(), _temp.Tables[0].Rows[0]["AUT_ID"].ToString()));
            }
            else 
                Mensaje.MostrarMensaje(this.Page,"Este municipio no tiene Autoridad Autonoma Relacionada.");
            cboAutoridadQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
          
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioQueja_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboMunicipioQuejoso_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioQuejoso_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoMun = int.Parse(cboMunicipioQuejoso.SelectedItem.Value);
            DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
            cboCorregimientoQuejoso.DataSource = _temp;
            cboCorregimientoQuejoso.DataValueField = "COR_ID";
            cboCorregimientoQuejoso.DataTextField = "COR_NOMBRE";
            cboCorregimientoQuejoso.DataBind();
            cboCorregimientoQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
        //    SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioQuejoso_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboCorregimientoQueja_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoQueja_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoCor = int.Parse(cboCorregimientoQueja.SelectedItem.Value);
            int _codigoMun = int.Parse(cboMunicipioQueja.SelectedItem.Value);
            DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
            cboVeredaQueja.DataSource = _temp;
            cboVeredaQueja.DataValueField = "VER_ID";
            cboVeredaQueja.DataTextField = "VER_NOMBRE";
            cboVeredaQueja.DataBind();
            cboVeredaQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoQueja_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboCorregimientoQuejoso_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoQuejoso_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoCor = int.Parse(cboCorregimientoQuejoso.SelectedItem.Value);
            int _codigoMun = int.Parse(cboMunicipioQuejoso.SelectedItem.Value);
            DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
            cboVeredaQuejoso.DataSource = _temp;
            cboVeredaQuejoso.DataValueField = "VER_ID";
            cboVeredaQuejoso.DataTextField = "VER_NOMBRE";
            cboVeredaQuejoso.DataBind();
            cboVeredaQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoQuejoso_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboAreaHidrografica_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
       //     SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboAreaHidrografica_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaZonasHidrograficas = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoArea = int.Parse(cboAreaHidrografica.SelectedValue);
            DataSet _temp = _listaZonasHidrograficas.ListarZonaHidrografica(null, _codigoArea);
            cboZonaHidrografica.DataSource = _temp;
            cboZonaHidrografica.DataValueField = "ZHI_ID";
            cboZonaHidrografica.DataTextField = "ZHI_NOMBRE";
            cboZonaHidrografica.DataBind();
            cboZonaHidrografica.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboAreaHidrografica_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboZonaHidrografica_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          //  SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboZonaHidrografica_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaSubZonasHidrogr = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoZona = int.Parse(cboZonaHidrografica.SelectedValue);
            DataSet _temp = _listaSubZonasHidrogr.ListarSubZonaHidrografica(null, _codigoZona);
            cboSubZonaHidrografica.DataSource = _temp;
            cboSubZonaHidrografica.DataValueField = "SHI_ID";
            cboSubZonaHidrografica.DataTextField = "SHI_NOMBRE";
            cboSubZonaHidrografica.DataBind();
            cboSubZonaHidrografica.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
         //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboZonaHidrografica_SelectedIndexChanged.Finalizo");
        }
    }

    #region Funciones Programador

    /// <summary>
    /// Funcion que carga los Departamentos en el combo especifico y llama a la funcion que carga los Municipios.
    /// </summary>
    /// <param name="_comboDepartamento"></param>
    /// <param name="_comboMunicipio"></param>
    private void CargarDepartamento(DropDownList _comboDepartamento, DropDownList _comboMunicipio)
    {
        SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
        _comboDepartamento.DataSource = _temp;
        _comboDepartamento.DataValueField = "DEP_ID";
        _comboDepartamento.DataTextField = "DEP_NOMBRE";
        _comboDepartamento.DataBind();
        _comboDepartamento.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        CargarComboMunicipios(_comboMunicipio, _comboDepartamento.SelectedItem.Value);

    }

    /// <summary>
    /// Funcion que carga los Municipios en el combo especifico.
    /// </summary>
    /// <param name="_combo"></param>
    /// <param name="_indiceDepartamento"></param>
    private void CargarComboMunicipios(DropDownList _combo, string _indiceDepartamento)
    {
        if (_indiceDepartamento != null)
        {
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, int.Parse(_indiceDepartamento), null);
            _combo.DataSource = _temp;
            _combo.DataValueField = "MUN_ID";
            _combo.DataTextField = "MUN_NOMBRE";
            _combo.DataBind();
            _combo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
    }
  
    /// <summary>
    /// Funcion que carga los tipos de identificacion
    /// </summary>
    private void CargarTiposIdentificacion()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaTiposId = new SILPA.LogicaNegocio.Generico.Listas();
        cboTipoIdentificacionInfractor.DataSource = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacionInfractor.DataValueField = "TID_ID";
        cboTipoIdentificacionInfractor.DataTextField = "TID_NOMBRE";
        cboTipoIdentificacionInfractor.DataBind();
        cboTipoIdentificacionInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        cboTipoIdentificacionDenunciante.DataSource = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacionDenunciante.DataValueField = "TID_ID";
        cboTipoIdentificacionDenunciante.DataTextField = "TID_NOMBRE";
        cboTipoIdentificacionDenunciante.DataBind();
        cboTipoIdentificacionDenunciante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    /// <summary>
    /// Funcion que carga el listado de paises en el combo de País.
    /// </summary>
    private void CargarPaises()
    {
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        SILPA.LogicaNegocio.Generico.Listas _listaPaises = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet _temp = _listaPaises.ListarPaises(null);
        cboPaisQuejoso.DataSource = _temp;
        cboPaisQuejoso.DataValueField = "PAI_ID";
        cboPaisQuejoso.DataTextField = "PAI_NOMBRE";
        cboPaisQuejoso.DataBind();
        cboPaisQuejoso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        cboPaisQuejoso.SelectedValue = Convert.ToString(_configuracion.IdPaisPredeterminado);
        //cboDepartamentoQuejoso.Enabled = false;
        //cboMunicipioQuejoso.Enabled = false;

        cboPaisInfractor.DataSource = _temp;
        cboPaisInfractor.DataValueField = "PAI_ID";
        cboPaisInfractor.DataTextField = "PAI_NOMBRE";
        cboPaisInfractor.DataBind();
        cboPaisInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        cboPaisInfractor.SelectedValue = Convert.ToString(_configuracion.IdPaisPredeterminado);
        //cboDepartamentoInfractor.Enabled = false;
       //cboMunicipioInfractor.Enabled = false;
    }

    private void CargarAutoridades()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet _temp = _listaAutoridades.ListarAutoridades(null);
        cboAutoridadQueja.DataSource = _temp;
        cboAutoridadQueja.DataValueField = "AUT_ID";
        cboAutoridadQueja.DataTextField = "AUT_NOMBRE";
        cboAutoridadQueja.DataBind();
            
    }

    /// <summary>
    /// Funcion que carga el listado de sectores en el combo correspondiente
    /// </summary>
    //private void CargarSectores()
    //{
    //    SILPA.LogicaNegocio.Generico.Listas _listaSectores = new SILPA.LogicaNegocio.Generico.Listas();
    //    DataSet _temp = _listaSectores.ListarSectorTipoProyecto(null, -1);
    //    cboSectorQueja.DataSource = _temp;
    //    cboSectorQueja.DataValueField = "SEC_ID";
    //    cboSectorQueja.DataTextField = "SEC_NOMBRE";
    //    cboSectorQueja.DataBind();
    //    cboSectorQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    //}

    private void CargarZonas()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaZonas = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet _temp = _listaZonas.ListarTipoUbicacion(null);
        cboZonaQueja.DataSource = _temp;
        cboZonaQueja.DataValueField = "UBI_ID";
        cboZonaQueja.DataTextField = "UBI_NOMBRE";
        cboZonaQueja.DataBind();
        cboZonaQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
    }

    private void CargarAreasHidrograficas()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAreas = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet _temp = _listaAreas.ListarAreaHidrografica(null);
        cboAreaHidrografica.DataSource = _temp;
        cboAreaHidrografica.DataValueField = "AHI_ID";
        cboAreaHidrografica.DataTextField = "AHI_NOMBRE";
        cboAreaHidrografica.DataBind();
        cboAreaHidrografica.Items.Insert(0, new ListItem("Seleccione...", "0"));
    }

    private void CargarRecursos()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaRecursos = new SILPA.LogicaNegocio.Generico.Listas();
        DataSet _temp = _listaRecursos.ListarRecursosSancionatorio(null);
        chkRecursosQueja.DataSource = _temp;
        chkRecursosQueja.DataValueField = "REC_ID_RECURSO";
        chkRecursosQueja.DataTextField = "REC_NOMBRE";
        chkRecursosQueja.DataBind();
    }

    #endregion    
    
    protected void btnAgregarCoordenada_Click(object sender, EventArgs e)
    {

        
        if (txtCoordenadaX.Text != "" && txtCoordenadaY.Text != "")
        {
            lstCoordenadas.Items.Add(txtCoordenadaX.Text + "-" + txtCoordenadaY.Text);

            if (ViewState["listaCoordenadas"] != null)
            {
                listaCoordenadas = (List<string>)ViewState["listaCoordenadas"];
            }
            
            listaCoordenadas.Add(txtCoordenadaX.Text + "-" + txtCoordenadaY.Text);

            lblMensajeCoordenada.Visible = false;
            txtCoordenadaX.Text = "";
            txtCoordenadaY.Text = "";
        }
        else
        {
            lblMensajeCoordenada.Visible = true;
            lblMensajeCoordenada.Text = "Es necesario digitar las dos coordenadas";
        }

        ViewState["listaCoordenadas"] = listaCoordenadas;

    }

    protected void btnEliminarCoordenada_Click(object sender, EventArgs e)
    {
        if (lstCoordenadas.Items.Count > 0)
        {
            if (lstCoordenadas.SelectedItem != null)
            {
                lstCoordenadas.Items.Remove(lstCoordenadas.SelectedItem);
                lblMensajeCoordenada.Visible = false;
            }
            else
            {
                lblMensajeCoordenada.Visible = true;
                lblMensajeCoordenada.Text = "Seleccione una coordenada a eliminar";
            }
        }
        else
        {
            lblMensajeCoordenada.Visible = true;
            lblMensajeCoordenada.Text = "No hay coordenadas para eliminar";
        }

    }

    protected void cvaOtroRecurso_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtOtroRecurso.Text == "")
            args.IsValid = true;
        else
            args.IsValid = false;
    }

    protected void btnEnviarQueja_Click(object sender, EventArgs e)
    {
        try
        {

            if (Page.IsValid)
            {
                  IngresarQueja();
                 
            }
        }
        catch (Exception ex)
        {
            Mensaje.MostrarMensaje(this, "Ha ocurrido un error en el proceso comuniquese con el Administrador");
            SMLog.Escribir(Severidad.Critico, this.Page.Title + ex.ToString());
            

        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnEnviarQueja_Click.Finalizo");
          

            
        }
    }

    private void IngresarQueja()
    {
        List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
        objValoresList.Add(CargarValores(1, "Bas", this.txtDescripcionQueja.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(2, "Bas", this.txtDireccionDescripcion.Text, 1, new Byte[1]));
        int i;
        string Coordenadas = "";
        for (i = 1; i <= this.lstCoordenadas.Items.Count; i++)
        {
            Coordenadas = Coordenadas + this.lstCoordenadas.Items[i - 1].ToString() + ";";
        }
        objValoresList.Add(CargarValores(3, "Bas", Coordenadas.Replace(',', '.'), 1, new Byte[1]));
        objValoresList.Add(CargarValores(4, "Bas", this.cboAutoridadQueja.SelectedValue, 1, new Byte[1]));

        for (i = 1; i <= this.chkRecursosQueja.Items.Count; i++)
        {
            bool valor = this.chkRecursosQueja.Items[i - 1].Selected;
            string Texto = this.chkRecursosQueja.Items[i - 1].Text;
            if (valor)
            {
                switch (Texto)
                {
                    case "Flora":
                        objValoresList.Add(CargarValores(5, "Bas", "1", 1, new Byte[1]));
                        break;
                    case "Fauna":
                        objValoresList.Add(CargarValores(6, "Bas", "1", 1, new Byte[1]));
                        break;
                    case "Aire":
                        objValoresList.Add(CargarValores(7, "Bas", "1", 1, new Byte[1]));
                        break;
                    case "Agua":
                        objValoresList.Add(CargarValores(8, "Bas", "1", 1, new Byte[1]));
                        break;
                    case "Suelo":
                        objValoresList.Add(CargarValores(9, "Bas", "1", 1, new Byte[1]));
                        break;
                    case "Paisaje":
                        objValoresList.Add(CargarValores(10, "Bas", "1", 1, new Byte[1]));
                        break;
                    case "Otro":
                        objValoresList.Add(CargarValores(11, "Bas", "1", 1, new Byte[1]));
                        break;
                }
            }
            else
            {
                switch (Texto)
                {
                    case "Flora":
                        objValoresList.Add(CargarValores(5, "Bas", "0", 1, new Byte[1]));
                        break;
                    case "Fauna":
                        objValoresList.Add(CargarValores(6, "Bas", "0", 1, new Byte[1]));
                        break;
                    case "Aire":
                        objValoresList.Add(CargarValores(7, "Bas", "0", 1, new Byte[1]));
                        break;
                    case "Agua":
                        objValoresList.Add(CargarValores(8, "Bas", "0", 1, new Byte[1]));
                        break;
                    case "Suelo":
                        objValoresList.Add(CargarValores(9, "Bas", "0", 1, new Byte[1]));
                        break;
                    case "Paisaje":
                        objValoresList.Add(CargarValores(10, "Bas", "0", 1, new Byte[1]));
                        break;
                    case "Otro":
                        objValoresList.Add(CargarValores(11, "Bas", "0", 1, new Byte[1]));
                        break;
                }
            }
        }
        objValoresList.Add(CargarValores(12, "Bas", this.txtOtroRecurso.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(13, "Bas", this.txtPrimerNombreInfractor.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(14, "Bas", this.txtSegundoNombreInfractor.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(15, "Bas", this.txtPrimerApellidoInfractor.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(16, "Bas", this.txtSegundoApellidoInfractor.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(17, "Bas", this.cboTipoIdentificacionInfractor.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(18, "Bas", this.txtIdentificacionInfractor.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(19, "Bas", this.cboDepartamentoOrigen.SelectedItem + "-" + this.cboMunicipioOrigen.SelectedItem, 1, new Byte[1]));
        objValoresList.Add(CargarValores(20, "Bas", this.txtDireccionInfractor.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(21, "Bas", this.cboPaisInfractor.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(22, "Bas", this.cboDepartamentoInfractor.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(23, "Bas", this.cboMunicipioInfractor.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(24, "Bas", this.cboCorregimientoInfractor.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(25, "Bas", this.cboVeredaInfractor.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(26, "Bas", this.txtTelefonoInfractor.Text, 1, new Byte[1]));

        objValoresList.Add(CargarValores(27, "Bas", this.txtPrimerNombreQuejoso.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(28, "Bas", this.txtSegundoNombreQuejoso.Text, 1, new Byte[1]));

        objValoresList.Add(CargarValores(29, "Bas", this.txtPrimerApellidoQuejoso.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(30, "Bas", this.txtSegundoApellidoQuejoso.Text, 1, new Byte[1]));


        objValoresList.Add(CargarValores(31, "Bas", this.cboTipoIdentificacionDenunciante.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(32, "Bas", this.txtIdentificacionDenunciante.Text, 1, new Byte[1]));


        objValoresList.Add(CargarValores(33, "Bas", this.txtDireccionQuejoso.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(34, "Bas", this.cboPaisQuejoso.SelectedValue, 1, new Byte[1]));


        objValoresList.Add(CargarValores(35, "Bas", this.cboDepartamentoQuejoso.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(36, "Bas", this.cboMunicipioQuejoso.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(37, "Bas", this.cboCorregimientoQuejoso.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(38, "Bas", this.cboVeredaQuejoso.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(39, "Bas", this.txtCorreoQuejoso.Text, 1, new Byte[1]));
        objValoresList.Add(CargarValores(40, "Bas", this.txtTelefonoQuejoso.Text, 1, new Byte[1]));

        SILPA.LogicaNegocio.Generico.AutoridadAmbiental _autoridad = new SILPA.LogicaNegocio.Generico.AutoridadAmbiental();
        DataSet _temp = _autoridad.ListarAAXJurisdiccion(int.Parse(cboMunicipioQueja.SelectedValue));
        if (_temp.Tables[0].Rows.Count > 0)
        {
            if (this.cboAutoridadQueja.SelectedValue == _temp.Tables[0].Rows[0]["AUT_ID"].ToString())
            {
                objValoresList.Add(CargarValores(46, "List2", this.cboDepartamentoQueja.SelectedValue, 1, new Byte[1]));
                objValoresList.Add(CargarValores(47, "List2", this.cboMunicipioQueja.SelectedValue, 1, new Byte[1]));
            }
            else
            {
                objValoresList.Add(CargarValores(46, "List2", "11", 1, new Byte[1]));
                objValoresList.Add(CargarValores(47, "List2", "11001", 1, new Byte[1]));
            }
        }
        else
        {
            objValoresList.Add(CargarValores(46, "List2", "11", 1, new Byte[1]));
            objValoresList.Add(CargarValores(47, "List2", "11001", 1, new Byte[1]));
        }

        objValoresList.Add(CargarValores(48, "List2", this.cboVeredaQueja.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(49, "List2", this.cboCorregimientoQueja.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(50, "List2", this.cboAreaHidrografica.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(51, "List2", this.cboZonaHidrografica.SelectedValue, 1, new Byte[1]));
        objValoresList.Add(CargarValores(52, "List2", this.cboSubZonaHidrografica.SelectedValue, 1, new Byte[1]));
        List<byte[]> _bytes = (List<byte[]>)Session["ListaBytes"];
        List<string> _archivo = (List<string>)Session["ListaArchivos"];
        if (Session["ListaArchivos"] != null)
        {
            for (i = 1; i <= _archivo.Count; i++)
            {
                string nombreArchivo;
                string[] cadenaArchivo = _archivo[i - 1].Replace("\\", "/").Split('/');
                nombreArchivo = cadenaArchivo[cadenaArchivo.Length - 1];
                objValoresList.Add(CargarValores(41, "List1", nombreArchivo, i, _bytes[i - 1]));
                objValoresList.Add(CargarValores(42, "List1", i.ToString(), i, new Byte[1]));
                objValoresList.Add(CargarValores(43, "List1", "", i, new Byte[1]));
                objValoresList.Add(CargarValores(44, "List1", "", i, new Byte[1]));
                objValoresList.Add(CargarValores(45, "List1", "", i, new Byte[1]));
            }
        }
        objValoresList.Add(CargarValores(41, "List1", "", 1, new Byte[1]));
        objValoresList.Add(CargarValores(42, "List1", "", 1, new Byte[1]));
        objValoresList.Add(CargarValores(43, "List1", "", 1, new Byte[1]));
        objValoresList.Add(CargarValores(44, "List1", "", 1, new Byte[1]));
        objValoresList.Add(CargarValores(45, "List1", "", 1, new Byte[1]));

        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
        serializador.Serialize(memoryStream, objValoresList);
        string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
        Queja _queja = new Queja();
        //Estos parametros hay que parametrizarlos
        DataTable ParametrosFormulario = _queja.ObtenerUsuarioQueja().Tables[0];
        Int64 UsuarioQueja;
        if (string.IsNullOrEmpty(this._usuarioQuejosoRegistrado))
            UsuarioQueja = Int64.Parse(ParametrosFormulario.Rows[0]["ID_APPLICATION_USER"].ToString());
        else
            UsuarioQueja = Int64.Parse(this.IdUser.Text);
        Int64 Formularioqueja = Int64.Parse(ParametrosFormulario.Rows[0]["FORM_ID"].ToString());
        string ClientId = ParametrosFormulario.Rows[0]["CLIENT_ID"].ToString();
        string numerosilpa;

        long idQueja = -1;

        if (!string.IsNullOrEmpty(this._usuarioQuejosoRegistrado))
        {
            numerosilpa = _queja.CrearProcesoQueja(ClientId, Formularioqueja, UsuarioQueja, xml);
        }
        else
        {
            if (this.txtIdentificacionDenunciante.Text.Trim() == "")
            {
                numerosilpa = _queja.CrearProcesoQueja(ClientId, Formularioqueja, UsuarioQueja, xml);
                if (numerosilpa.Contains("ERROR"))
                    return;
                _queja.InsertarQuejas(numerosilpa, this.txtPrimerNombreQuejoso.Text,
                this.txtSegundoNombreQuejoso.Text, this.txtPrimerApellidoQuejoso.Text,
                this.txtSegundoApellidoQuejoso.Text, this.txtCorreoQuejoso.Text);
            }
            else
            {
                numerosilpa = _queja.CrearProcesoQueja(ClientId, Formularioqueja, UsuarioQueja, xml, this.txtPrimerNombreQuejoso.Text + " " + this.txtSegundoNombreQuejoso.Text + " " + this.txtPrimerApellidoQuejoso.Text + " " + this.txtSegundoApellidoQuejoso.Text);
                if (numerosilpa.Contains("ERROR"))
                    return;
                _queja.InsertarQuejas(numerosilpa, this.txtPrimerNombreQuejoso.Text,
                this.txtSegundoNombreQuejoso.Text, this.txtPrimerApellidoQuejoso.Text,
                this.txtSegundoApellidoQuejoso.Text, this.txtCorreoQuejoso.Text);
            }
        }

        if (Session["ListaBytes"] != null)
            Session.Remove("ListaBytes");
        if (Session["ListaArchivos"] != null)
            Session.Remove("ListaArchivos");
        if (Session["TamanioArchivos"] != null)
            Session.Remove("TamanioArchivos");
        lstListaArchivos.Items.Clear();
        ViewState["i"] = null;

        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("QUEJAS DENUNCIAS", 1, "Se almaceno Quejas y Denuncias");

        string msg;
        string Line1 = "RESULTADO ";
        string Line2 = "Proceso realizado correctamente ";
        string Line3 = "El número vital asignado a su proceso es el " + numerosilpa;
        string Line4 = "Su solicítud será gestionado por la Autoridad Ambiental " + this.cboAutoridadQueja.SelectedItem.ToString();
        msg = "".PadLeft(37) + Line1 + "\\n";
        msg += "".PadLeft(25) + Line2 + "\\n";
        msg += "".PadLeft(35 - Line3.Length / 2) + Line3 + "\\n";
        msg += "".PadLeft(35 - Line4.Length / 2) + Line4;

        //               Mensaje.MostrarMensaje(this, msg);

        ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), Guid.NewGuid().ToString(), string.Format("alert('{0}.');", msg), true);

        if (this.txtCorreoQuejoso.Text != "")
        {
            ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
            correo.Para.Add(this.txtCorreoQuejoso.Text);
            correo.Tokens.Add("NOMBRE_SOLICITANTE", this.txtPrimerNombreQuejoso.Text + ' ' + this.txtSegundoNombreQuejoso.Text + ' ' + this.txtPrimerApellidoQuejoso.Text + ' ' + this.txtSegundoApellidoQuejoso.Text);
            correo.Tokens.Add("MENSAJE", msg.Replace("\\n", "<br/>"));
            //correo.Adjuntos.Add(str_NombreArchivo);
            //traigo el id de la tabla parametros
            SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
            SILPA.AccesoDatos.Generico.ParametroEntity _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
            _parametro.IdParametro = -1;
            _parametro.NombreParametro = "plantilla_quejas";
            _parametroDalc.obtenerParametros(ref _parametro);

            correo.Enviar(int.Parse(_parametro.Parametro));
        }
        //Limpiar rricaurte
        Limpiar();


        //Page.RegisterStartupScript("PopupScript", strScript);
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "location.href = '" + "../../ventanillasilpa/" + "'", true);
    }


    private ValoresIdentity CargarValores(int id,string grupo,string valor,int orden,Byte[] archivo)
    {
        ValoresIdentity objValores = new ValoresIdentity();
        objValores.Id = id;
        objValores.Grupo = grupo;
        objValores.Valor = valor;
        objValores.Orden = orden;
        objValores.Archivo = archivo;
        return objValores;
    }


    /// <summary>
    /// Limpiar todos los objetos de la pagina para Regresarlos a la posicion inicial.
    /// </summary>
    /// 

    private void Limpiar()
    {
        this.txtDescripcionQueja.Text = "";
        //this.uplAdjuntarArchivo.Dispose();
        this.lstListaArchivos.Items.Clear();
        this.txtDireccionDescripcion.Text = "";
        this.cboDepartamentoQueja.SelectedIndex = -1;
        this.cboMunicipioQueja.SelectedIndex = -1;
        this.cboZonaQueja.SelectedIndex = -1;
        this.cboCorregimientoQueja.SelectedIndex = -1;
        this.cboVeredaQueja.SelectedIndex = -1;
        this.cboAreaHidrografica.SelectedIndex = -1;
        this.cboZonaHidrografica.SelectedIndex = -1;
        this.cboSubZonaHidrografica.SelectedIndex = -1;
        this.cboAutoridadQueja.SelectedIndex = -1;
        this.txtCoordenadaX.Text = "";
        this.txtCoordenadaY.Text = "";
        this.lstCoordenadas.Items.Clear();
        this.txtOtroRecurso.Text = "";
        int i = 0;
        for (i = 0; i < this.chkRecursosQueja.Items.Count; i++)
        {
            this.chkRecursosQueja.Items[i].Selected = false;
        }
        this.txtPrimerApellidoInfractor.Text = "";
        this.txtSegundoApellidoInfractor.Text = "";
        this.txtPrimerNombreInfractor.Text = "";
        this.txtSegundoNombreInfractor.Text = "";
        this.cboTipoIdentificacionInfractor.SelectedIndex = -1;
        this.txtIdentificacionInfractor.Text = "";
        this.cboDepartamentoOrigen.SelectedIndex = -1;
        this.cboDepartamentoQuejoso.SelectedIndex = -1;
        this.cboMunicipioOrigen.SelectedIndex = -1;
        this.cboMunicipioQuejoso.SelectedIndex = -1;
        this.txtDireccionInfractor.Text = "";
        this.txtTelefonoInfractor.Text = "";

        this.txtPrimerNombreQuejoso.Text = "";
        this.txtSegundoNombreQuejoso.Text = "";
        this.txtPrimerApellidoQuejoso.Text = "";
        this.txtSegundoApellidoQuejoso.Text = "";
        this.cboTipoIdentificacionInfractor.SelectedIndex = -1;
        this.txtIdentificacionInfractor.Text = "";
        this.txtDireccionQuejoso.Text = "";
        this.cboPaisQuejoso.SelectedIndex = -1;
        this.cboDepartamentoQuejoso.SelectedIndex = -1;
        this.cboMunicipioQuejoso.SelectedIndex = -1;
        this.cboCorregimientoQuejoso.SelectedIndex = -1;
        this.cboVeredaQuejoso.SelectedIndex = -1;
        this.txtCorreoQuejoso.Text = "";
        this.txtTelefonoQuejoso.Text = "";
        this.txtIdentificacionDenunciante.Text = "";
        this.cboTipoIdentificacionDenunciante.SelectedIndex = -1;
        this.cboDepartamentoOrigenDenunciante.SelectedIndex = -1;
        this.cboMunicipioOrigenDenunciante.Items.Clear();
        this.cboMunicipioOrigenDenunciante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        this.cboMunicipioOrigenDenunciante.SelectedIndex = -1;
    }


    protected void cboDepartamentoOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          //  SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoOrigen_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamentoOrigen.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioOrigen.DataSource = _temp;
            cboMunicipioOrigen.DataValueField = "MUN_ID";
            cboMunicipioOrigen.DataTextField = "MUN_NOMBRE";
            cboMunicipioOrigen.DataBind();
            cboMunicipioOrigen.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            //SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoOrigen_SelectedIndexChanged.Finalizo");
        }
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        foreach (ListItem _item in chkRecursosQueja.Items)
        {
            if (_item.Selected && _item.Text == "Otro")
            {
                if (txtOtroRecurso.Text == "")
                {
                    args.IsValid = false;
                    return;
                }
            }
        }
        args.IsValid = true;
    }

    /// <summary>
    /// Método que valida si se ha insertado algún dato en los campos del infractor
    /// </summary>
    /// <returns>Verdadero si se inserto algo, falso en caso contrario</returns>
    private bool ValidarInfractor()
    {
        bool _temp = false;
        if (txtPrimerNombreInfractor.Text != "")
            _temp = true;
        if (txtSegundoNombreInfractor.Text != "")
            _temp = true;
        if (txtPrimerApellidoInfractor.Text != "")
            _temp = true;
        if (txtSegundoApellidoInfractor.Text != "")
            _temp = true;
        if (cboTipoIdentificacionInfractor.SelectedValue!="-1")
            _temp = true;
        if (txtIdentificacionInfractor.Text != "")
            _temp = true;
        if (txtIdentificacionInfractor.Text != "")
            _temp = true;
        if (cboMunicipioOrigen.SelectedValue != "-1")
            _temp = true;
        if (txtDireccionInfractor.Text != "")
            _temp = true;
        if (txtTelefonoInfractor.Text != "")
            _temp = true;

        return _temp;
    }

    private bool ValidarDenunciante()
    {
        bool _temp = false;
        if (txtPrimerNombreQuejoso.Text != "")
            _temp = true;
        if (txtSegundoNombreQuejoso.Text != "")
            _temp = true;
        if (txtPrimerApellidoQuejoso.Text != "")
            _temp = true;
        if (txtSegundoApellidoQuejoso.Text != "")
            _temp = true;
        if (txtDireccionQuejoso.Text!="")
            _temp = true;        
        if (cboMunicipioQuejoso.SelectedValue != "-1")
            _temp = true;
        if (cboCorregimientoQuejoso.SelectedValue != "-1")
            _temp = true;
        if (cboVeredaQuejoso.SelectedValue != "")
            _temp = true;
        if(txtCorreoQuejoso.Text != "")
            _temp = true;
        if(txtTelefonoQuejoso.Text != "")
            _temp = true;

        return _temp;
    }

    protected void btnCancelarQueja_Click(object sender, EventArgs e)
    {
        string _respuestaEnvio;
        if (string.IsNullOrEmpty(this._usuarioQuejosoRegistrado))
        {
            Page.Response.Redirect("~/../VentanillaSilpa");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "window.close();", true);
            
        }
        
    }

    private void GenerarRTF(string numeroSilpa, string usuario,int idradicacion)
    {
        //string fecha = DateTime.Now.ToString("yyyyMMdd");
        //string nombreDirectorio = ConfigurationManager.AppSettings["DireccionFus"] + fecha + "\\" + numeroSilpa+  "\\"+ usuario + "\\";

        //string nombreArchivo = numeroSilpa +"_" + DateTime.Now.ToString("yyyyMMddmmss") + ".rtf";
        //if (!(Directory.Exists(nombreDirectorio)))
        //{
        //    Directory.CreateDirectory(nombreDirectorio);
        //}

        string fecha = DateTime.Now.ToString("yyyyMMdd");
        string nombreDirectorio = ConfigurationManager.AppSettings["DireccionFus"].ToString();
        string nombreArchivo = "_" + numeroSilpa + ".rtf";
        if (!(Directory.Exists(nombreDirectorio)))
        {
            Directory.CreateDirectory(nombreDirectorio);
        }

        List<ImpresionArchivoFus> datos = new List<ImpresionArchivoFus>();

        datos.Add(new ImpresionArchivoFus("Presente su Queja o Denuncia: \n ", ""));

        datos.Add(new ImpresionArchivoFus(lblDescripcionQueja.Text, txtDescripcionQueja.Text));

        datos.Add(new ImpresionArchivoFus("ARCHIVOS ADJUNTOS \n",""));

        if (ViewState["listaNombreArchivos"] != null) 
        {
            listaNombreArchivos = (List<string>)ViewState["listaNombreArchivos"];
        }
        
        //foreach (ListItem item in lstListaArchivos.Items)
        //{
        //    datos.Add(new ImpresionArchivoFus(item.Text,""));
        //}

        foreach (string str in listaNombreArchivos)
        {
            datos.Add(new ImpresionArchivoFus(str, ""));
        }

        datos.Add(new ImpresionArchivoFus("LUGAR DE OCURRENCIA DE LOS HECHOS:\n", ""));
        datos.Add(new ImpresionArchivoFus(lblDireccionDescripcion.Text, txtDireccionDescripcion.Text));
        datos.Add(new ImpresionArchivoFus(lblDepartamentoQueja.Text, cboDepartamentoQueja.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblMunicipioQuejas.Text, cboMunicipioQueja.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblZonaQueja.Text, cboZonaQueja.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblCorregimientoQueja.Text, cboCorregimientoQueja.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblVeredaQueja.Text, cboVeredaQueja.SelectedItem.Text));

        datos.Add(new ImpresionArchivoFus(lblCuencaQueja.Text+ ": \n", ""));

        datos.Add(new ImpresionArchivoFus(lblAreaHidrografica.Text, cboAreaHidrografica.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblZonaHidrografica.Text, cboZonaHidrografica.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblSubZonaHidrografica.Text, cboSubZonaHidrografica.SelectedItem.Text));

        datos.Add(new ImpresionArchivoFus(lblAutoridadQueja.Text, cboAutoridadQueja.SelectedItem.Text));
        //datos.Add(new ImpresionArchivoFus(lblSectorQueja.Text, cboSectorQueja.SelectedItem.Text));

        datos.Add(new ImpresionArchivoFus("--------------------------------------------------------------\n", ""));
        datos.Add(new ImpresionArchivoFus("COORDENADAS:", ""));

        if (ViewState["listaCoordenadas"] != null)
        {
            listaCoordenadas = (List<string>)ViewState["listaCoordenadas"];
        }

        //foreach (ListItem item in lstCoordenadas.Items)
        //{
        //    datos.Add(new ImpresionArchivoFus(item.Text, ""));
        //}

        foreach (string strc in listaCoordenadas)
        {
            datos.Add(new ImpresionArchivoFus(strc, ""));
        }

        datos.Add(new ImpresionArchivoFus("--------------------------------------------------------------\n", ""));

        datos.Add(new ImpresionArchivoFus(lblRecursoQueja.Text, chkRecursosQueja.SelectedIndex !=-1? chkRecursosQueja.SelectedItem.Text:""));

        datos.Add(new ImpresionArchivoFus("DATOS DEL PRESUNTO INFRACTOR: \n", ""));
        datos.Add(new ImpresionArchivoFus(lblPrimerNombreInfractor.Text, txtPrimerNombreInfractor.Text));
        datos.Add(new ImpresionArchivoFus(lblSegundoNombreInfractor.Text, txtSegundoNombreInfractor.Text));

        datos.Add(new ImpresionArchivoFus(lblPrimerApellidoInfractor.Text, txtPrimerApellidoInfractor.Text));
        datos.Add(new ImpresionArchivoFus(lblSegundoApellidoInfractor.Text, txtSegundoApellidoInfractor.Text));
        datos.Add(new ImpresionArchivoFus(lblTipoIdentificacionInfractor.Text, cboTipoIdentificacionInfractor.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblIdentificacionInfractor.Text, txtIdentificacionInfractor.Text));

        datos.Add(new ImpresionArchivoFus("--------------------------------------------------------------\n", ""));
        //origen de documento
        datos.Add(new ImpresionArchivoFus(lblOrigenIdentificacionInfractor.Text, cboDepartamentoOrigen.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus("", cboMunicipioOrigen.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblDireccionInfractor.Text, txtDireccionInfractor.Text));
        datos.Add(new ImpresionArchivoFus(lblTelefonoInfractor.Text, txtTelefonoInfractor.Text));

        datos.Add(new ImpresionArchivoFus("--------------------------------------------------------------\n", ""));
        datos.Add(new ImpresionArchivoFus("IDENTIFICACION DEL QUEJOSOS O DENUNCIANTE: \n", ""));

        datos.Add(new ImpresionArchivoFus(lblPrimerNombreQuejoso.Text, txtPrimerNombreQuejoso.Text));
        datos.Add(new ImpresionArchivoFus(lblSegundoNombreQuejoso.Text, txtSegundoNombreQuejoso.Text));
        datos.Add(new ImpresionArchivoFus(lblPrimerApellidoQuejoso.Text, txtPrimerApellidoQuejoso.Text));
        datos.Add(new ImpresionArchivoFus(lblSegundoApellidoQuejoso.Text, txtSegundoApellidoQuejoso.Text));

        datos.Add(new ImpresionArchivoFus(lblDireccionQuejoso.Text, txtDireccionQuejoso.Text));
        datos.Add(new ImpresionArchivoFus(lblPaisQuejoso.Text, cboPaisQuejoso.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblDepartamentoQuejoso.Text, cboDepartamentoQuejoso.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblMunicipioQuejoso.Text, cboMunicipioQuejoso.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblCorregimientoQuejoso.Text, cboCorregimientoQuejoso.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblVeredaQuejoso.Text, cboVeredaQuejoso.SelectedItem.Text));
        datos.Add(new ImpresionArchivoFus(lblCorreoQuejoso.Text, txtCorreoQuejoso.Text));
        datos.Add(new ImpresionArchivoFus(lblTelefonoQuejoso.Text, txtTelefonoQuejoso.Text));
        


        // Escribir el Archivo
        using (StreamWriter arc = new StreamWriter(nombreDirectorio + nombreArchivo, false))
        {
            foreach (ImpresionArchivoFus fus in datos)
            {
                arc.WriteLine(fus.strCampo.ToUpper().ToString() + "\t" + fus.strValor);
            }
        }

        //Actualizar Ruta del Documento
        RadicacionDocumentoDalc rad = new RadicacionDocumentoDalc();
        rad.ActualizarRadicacionRuta(idradicacion,nombreDirectorio);
    }

    protected void cboDepartamentoQueja_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoQueja_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamentoQueja.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioQueja.DataSource = _temp;
            cboMunicipioQueja.DataValueField = "MUN_ID";
            cboMunicipioQueja.DataTextField = "MUN_NOMBRE";
            cboMunicipioQueja.DataBind();
            cboMunicipioQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
          
            //cboCorregimientoQuejoso.DataValueField = "COR_ID";
            //cboCorregimientoQuejoso.DataTextField = "COR_NOMBRE";
            cboAutoridadQueja.DataSource = new DataTable();
            cboAutoridadQueja.DataBind();
            cboAutoridadQueja.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {

            SMLog.Escribir(ex);
        }
        finally
        {
            //SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoQueja_SelectedIndexChanged.Finalizo");
        }
    }

    protected void cboDepartamentoOrigenDenunciante_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoOrigenDenunciante_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(this.cboDepartamentoOrigenDenunciante.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioOrigenDenunciante.DataSource = _temp;
            cboMunicipioOrigenDenunciante.DataValueField = "MUN_ID";
            cboMunicipioOrigenDenunciante.DataTextField = "MUN_NOMBRE";
            cboMunicipioOrigenDenunciante.DataBind();
            cboMunicipioOrigenDenunciante.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoOrigenDenunciante_SelectedIndexChanged.Finalizo");
        }
    }
    protected void cboTipoIdentificacionInfractor_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (int.Parse(cboTipoIdentificacionInfractor.SelectedValue) - 1 == (int)SILPA.Comun.TipoIdentificacion.CedulaExtranjeria)
        {
            this.cboDepartamentoOrigen.Enabled=false;
            this.cboMunicipioOrigen.Enabled=false;
        }
        else
        {
            this.cboDepartamentoOrigen.Enabled=true;
            this.cboMunicipioOrigen.Enabled=true;
        }       

    }
    protected void cboTipoIdentificacionDenunciante_SelectedIndexChanged(object sender, EventArgs e)
    {
        int prueba = (int)SILPA.Comun.TipoIdentificacion.CedulaExtranjeria;
        if (int.Parse(cboTipoIdentificacionDenunciante.SelectedValue) - 1 == (int)SILPA.Comun.TipoIdentificacion.CedulaExtranjeria)
        {
            this.cboDepartamentoOrigenDenunciante.Enabled=false;
            this.cboMunicipioOrigenDenunciante.Enabled=false;
        }
        else
        {
            this.cboDepartamentoOrigenDenunciante.Enabled=true;
            this.cboMunicipioOrigenDenunciante.Enabled=true;
        }
    }
    protected void cboTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblPrimerNombreInfractor.Text = "Primer Nombre:";
        this.lblSegundoNombreInfractor.Visible = true;
        this.txtSegundoNombreInfractor.Visible = true;
        this.lblPrimerApellidoInfractor.Visible = true;
        this.txtPrimerApellidoInfractor.Visible = true;
        this.lblSegundoApellidoInfractor.Visible = true;
        this.txtSegundoApellidoInfractor.Visible = true;
        if (int.Parse(this.cboTipoPersona.SelectedValue) == 2)
        {
            this.lblPrimerNombreInfractor.Text = "Razon Social:";
            this.lblSegundoNombreInfractor.Visible = false;
            this.txtSegundoNombreInfractor.Visible = false;
            this.lblPrimerApellidoInfractor.Visible = false;
            this.txtPrimerApellidoInfractor.Visible = false;
            this.lblSegundoApellidoInfractor.Visible = false;
            this.txtSegundoApellidoInfractor.Visible = false;
        }
        
    }
    protected void cboPaisInfractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //    SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisQuejoso_SelectedIndexChanged.Inicio");
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            if (cboPaisInfractor.SelectedValue == _configuracion.IdPaisPredeterminado.ToString())
            {
                SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
                cboDepartamentoInfractor.Enabled = true;
                cboMunicipioInfractor.Enabled = true;
                cboDepartamentoInfractor.DataSource = _temp;
                cboDepartamentoInfractor.DataValueField = "DEP_ID";
                cboDepartamentoInfractor.DataTextField = "DEP_NOMBRE";
                cboDepartamentoInfractor.DataBind();
                cboDepartamentoInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                cboDepartamentoInfractor.SelectedIndex = 0;
                CargarComboMunicipios(cboMunicipioInfractor, cboDepartamentoInfractor.SelectedItem.Value);
            }
            else
            {
                cboDepartamentoQuejoso.Enabled = false;
                cboMunicipioQuejoso.Enabled = false;
                cboDepartamentoQuejoso.Items.Clear();
                cboMunicipioQuejoso.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisQuejoso_SelectedIndexChanged.Finalizo");
        }
    }
    protected void cboDepartamentoInfractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoInfractor_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoDep = int.Parse(cboDepartamentoInfractor.SelectedItem.Value);
            DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
            cboMunicipioInfractor.DataSource = _temp;
            cboMunicipioInfractor.DataValueField = "MUN_ID";
            cboMunicipioInfractor.DataTextField = "MUN_NOMBRE";
            cboMunicipioInfractor.DataBind();
            cboMunicipioInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMunicipioInfractor.Enabled = true;

        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            //SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoInfractor_SelectedIndexChanged.Finalizo");
        }

    }
    protected void cboMunicipioInfractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioInfractor_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoMun = int.Parse(cboMunicipioInfractor.SelectedItem.Value);
            DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
            cboCorregimientoInfractor.DataSource = _temp;
            cboCorregimientoInfractor.DataValueField = "COR_ID";
            cboCorregimientoInfractor.DataTextField = "COR_NOMBRE";
            cboCorregimientoInfractor.DataBind();
            cboCorregimientoInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            //    SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioInfractor_SelectedIndexChanged.Finalizo");
        }

    }
    protected void cboCorregimientoInfractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoInfractor_SelectedIndexChanged.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
            int _codigoCor = int.Parse(cboCorregimientoInfractor.SelectedItem.Value);
            int _codigoMun = int.Parse(cboMunicipioInfractor.SelectedItem.Value);
            DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
            cboVeredaInfractor.DataSource = _temp;
            cboVeredaInfractor.DataValueField = "VER_ID";
            cboVeredaInfractor.DataTextField = "VER_NOMBRE";
            cboVeredaInfractor.DataBind();
            cboVeredaInfractor.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoInfractor_SelectedIndexChanged.Finalizo");
        }
    }
}
