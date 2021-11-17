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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using SILPA.Servicios;
using SILPA.Comun;
using SILPA.LogicaNegocio.AudienciaPublica;
using SILPA.LogicaNegocio.ICorreo;
using SILPA.AccesoDatos.AudienciaPublica;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using SoftManagement.ServicioCorreoElectronico;

public partial class Informacion_Publicaciones : System.Web.UI.Page
{

#region "Declaracion variables"
    private string _rutaServerFileTraffic;
    private string _numeroRadicadoAA;
    private string _numeroAUP;
    private string _numeroSILPA;
   
    private string _ubicacionDocumento;
    public string ruta = "";

    private List<Byte[]> _bytes;
    private List<string> _archivo;
    private List<int> _tamanio;

    public string NumeroVital { get { return _numeroAUP; } set { _numeroAUP = value; } }
    public string NumeroRadicadoAA { get { return _numeroRadicadoAA; } set { _numeroRadicadoAA = value; } }
    public string UbicacionDocumento { get { return _ubicacionDocumento; } set { _ubicacionDocumento = value; } }
    public string RutaServerFileTraffic { get { return _rutaServerFileTraffic; } set { _rutaServerFileTraffic = value; } }

#endregion

#region "Rutinas"
    
    /// <summary>
    /// Cargar tipo de documento
    /// </summary>
    private void CargarTipoDocumento()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarTipoDocumento.Inicio");
            SILPA.LogicaNegocio.Generico.Listas _listaTipoDocumento = new SILPA.LogicaNegocio.Generico.Listas();
            cboTipoDocumento.DataSource = _listaTipoDocumento.ListaTipoIdentificacion();
            cboTipoDocumento.DataValueField = "TID_ID";
            cboTipoDocumento.DataTextField = "TID_NOMBRE";
            cboTipoDocumento.DataBind();
            cboTipoDocumento.Items.Insert(0, new ListItem("Seleccione...", " "));
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarTipoDocumento.Finalizo");
        }
    }

    /// <summary>
    /// Obtener el Número de audiencia pública
    /// </summary>
    protected void procesarQueryString()
    {
       
        if (Request.QueryString["Numero_AuP"] != null)
        {
            _numeroAUP = Request.QueryString["Numero_AuP"];
            lbl_titulo_principal.Text = "Inscripción a Audiencia pública No. " + _numeroAUP;
            lblNoAUP.Text = _numeroAUP;     
        }
        else { _numeroAUP = ""; }

        if (Request.QueryString["Numero_SILPA"] != null)
        {
            _numeroSILPA = Request.QueryString["Numero_SILPA"];
            lblNoSILPA.Text = _numeroSILPA;     
        }
        else { _numeroSILPA = ""; }

        

    }
    
    ///// <summary>
    ///// Enviar correo electronico con archivos adjuntos
    ///// </summary>
    //public void enviarCorrespondencia(string numSILPAInscripcion)
    //{

    //    try
    //    {
    //        lblMensajeError.Text = ""; 
    //        PersonaIdentity _persona = new PersonaIdentity();
    //        CorreoAudienciaIdentity _datos = new CorreoAudienciaIdentity();

    //        _persona.CorreoElectronico = txtEmail.Text;
    //        _persona.PrimerNombre = txtPrimerNombre.Text;
    //        _persona.SegundoNombre = txtSegundoNombre.Text;
    //        _persona.PrimerApellido = txtPrimerApellido.Text;
    //        _persona.SegundoApellido = txtsegundoApellido.Text;
    //        _datos.NumeroSilpaSolicitud = lblNoSILPA.Text;
    //        _datos.NumeroAudienciaPublica = lblNoAUP.Text;
    //        _datos.NumeroSilpaInscripcion = numSILPAInscripcion;

    //        string ListaNombresArchivo = string.Empty;

    //        if (Session["ListaBytes"] != null && Session["ListaArchivos"] != null)
    //        {
    //            string[] ListaArchivos = System.IO.Directory.GetFiles(ruta);

    //            //Crear lista de archivos que se enviaran por correo electronico
    //            foreach (string str in ListaArchivos)
    //            {
    //                ListaNombresArchivo = ListaNombresArchivo + " , " + System.IO.Path.GetFileName(str);
    //            }

    //            _datos.nombreArchivos = ListaNombresArchivo;
    //        }
    //        else
    //        {
    //            _datos.nombreArchivos = "";
    //        }

    //        //Enviar el mensaje de correo electronico al destinatario indicado
    //        SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorrespondenciaCiudadano(_datos, _persona);
    //    }
    //    catch(Exception ex )
    //    {
    //        lblMensajeError.Text = "Ocurrio un error enviando la correspondencia,"+ex.Message;  

            //Enviar el mensaje de correo electronico al destinatario indicado
            //SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorrespondenciaCiudadano(_datos, _persona);
    //    }
    //    catch(Exception ex )
    //    {
    //        lblMensajeError.Text = "Ocurrio un error enviando la correspondencia,"+ex.Message;  
       
    ////    }

    //}
    
    ///// <summary>
    ///// Enviar correo electronico con archivos adjuntos
    ///// </summary>
    //public void enviarCorrespondenciaAA(string correoAA)
    //{

    //    try
    //    {
    //        lblMensajeError.Text = ""; 
    //        CorreoAudienciaIdentity _datos = new CorreoAudienciaIdentity();

    //        _datos.NumeroSilpaSolicitud = lblNoSILPA.Text;
    //        _datos.NumeroAudienciaPublica = lblNoAUP.Text;


    //        string ListaNombresArchivo = string.Empty;

    //        if (Session["ListaBytes"] != null && Session["ListaArchivos"] != null)
    //        {
    //            string[] ListaArchivos = System.IO.Directory.GetFiles(ruta);

    //            //Crear lista de archivos que se enviaran por correo electronico
    //            foreach (string str in ListaArchivos)
    //            {
    //                ListaNombresArchivo = ListaNombresArchivo + " , " + System.IO.Path.GetFileName(str);
    //            }

    //            _datos.nombreArchivos = ListaNombresArchivo;

    //            //Obtener la ruta de los archivos que se adjuntaran al mensaje
    //            List<string> LstArchivosAdjuntos = new List<string>();

    //            for (int i = 0; i < ListaArchivos.Length; i++)
    //            {
    //                LstArchivosAdjuntos.Add(ListaArchivos[i]);

    //            }
    //            _datos.listaArchivos = LstArchivosAdjuntos;

    //        }
    //        else
    //        {
    //            _datos.nombreArchivos = "";
    //            _datos.listaArchivos = null;
    //        }

    //        //Enviar el mensaje de correo electronico al destinatario indicado
    //        SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorrespondenciaAutoridadAmbiental(_datos, correoAA);

            //Enviar el mensaje de correo electronico al destinatario indicado
            //SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorrespondenciaAutoridadAmbiental(_datos, _emailAA);

    //    }
    //    catch(Exception ex )
    //    {
    //        lblMensajeError.Text = "Ocurrio un error enviando la correspondencia," + ex.Message;  
    //    }
    //}


    
#endregion


#region "Eventos"
    protected void Page_Load(object sender, EventArgs e)
    {
       txtEntidadComunidad.Attributes.Add("onkeypress", " ValidarCaracteres(this,2000);");
       txtEntidadComunidad.Attributes.Add("onkeyup", " ValidarCaracteres(this, 2000);");

        if (!Page.IsPostBack)
        {
            procesarQueryString();
            CargarTipoDocumento();
            CargarDepartamento(this.cboDepartamento,this.cboMunicipioAudiencia);
        }

        this.RutaServerFileTraffic = ConfigurationSettings.AppSettings["FILE_TRAFFIC"];
        Random random = new Random();
        this.NumeroVital = "SOL1" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + random.Next(9999).ToString();
    }

    /// <summary>
    ///  Boton que permite Guardar el registro de inscripción de audiencia pública
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdAceptar_Click(object sender, EventArgs e)
    {
        try
        {            
            DataTable dtResultado = new DataTable();

            AudienciaPublica ingresarEntityAudiencia = new AudienciaPublica();
            AudienciaPublica objCorreo = new AudienciaPublica();


            if (lblNoAUP.Text != "" && lblNoSILPA.Text != "")
            {
                if (cboTipoDocumento.SelectedIndex != 0)
                {

                    lblMensajeError.Text = "";

                    TraficoDocumento objDocumentos = new TraficoDocumento();
                    AudienciaPublica objAudiencia = new AudienciaPublica();                    
                    List<string> listaDocumentos=new List<string>(); 
                    if (Session["ListaBytes"] != null && Session["ListaArchivos"] != null)
                    {
                        listaDocumentos= (List<string>)Session["ListaArchivos"];
                        //Obtener la ruta de los  archivos adjuntos para la inscripción a al audiencia pública
                        objDocumentos.RecibirDocumento(lblNoSILPA.Text.Trim(), "INSCRIPCION_AUDIENCIA", (List<byte[]>)Session["ListaBytes"], ref listaDocumentos, ref ruta);
                    }


                    //Ingresar la incripcion a la audiencia pública
                    try
                    {

                        lblMensajeError.Text = "";
                        
                        this.txtDe.Text = this.cboDepartamento.SelectedItem.Text +" - "+ this.cboMunicipioAudiencia.SelectedItem.Text;

                        dtResultado = objAudiencia.IngresarInscripcionAudiencia(int.Parse(lblNoAUP.Text), lblNoSILPA.Text,
                         txtPrimerNombre.Text, txtSegundoNombre.Text, txtPrimerApellido.Text, txtsegundoApellido.Text,
                         int.Parse(cboTipoDocumento.SelectedValue), txtCedula.Text, txtDe.Text, txtEmail.Text,
                         txtEntidadComunidad.Text, ruta);
                        lblMensajeError.Text = "";

                        DataTable CorreoAA=objAudiencia.consultarCorreoAA(int.Parse(dtResultado.Rows[0]["AUD_AUT_ID"].ToString()));
                        if (dtResultado != null)
                        {
                            if (dtResultado.Rows.Count != 0)
                            {

                                if (dtResultado.Rows[0]["MENSAJE"].ToString() != "")
                                {
                                    lbl_respuesta.Text = dtResultado.Rows[0]["MENSAJE"].ToString() + lblNoAUP.Text;
                                    GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                                    CrearLogAuditoria.Insertar("AUDIENCIA PÚBLICA", 1, "Inscripción audiencia publica");

                                    ServicioCorreoElectronico correo = new ServicioCorreoElectronico();
                                    correo.Para.Add(CorreoAA.Rows[0]["AUT_CORREO_CORRESPONDENCIA"].ToString());
                                    correo.Tokens.Add("MENSAJE", lbl_respuesta.Text);
                                    int i=0;
                                    for(i=0; i<listaDocumentos.Count;i++)
                                        correo.Adjuntos.Add(listaDocumentos[i].ToString());                                    
                                    SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
                                    SILPA.AccesoDatos.Generico.ParametroEntity _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
                                    _parametro.IdParametro = -1;
                                    _parametro.NombreParametro = "plantilla_Ins_AUD";
                                    _parametroDalc.obtenerParametros(ref _parametro);

                                    correo.Enviar(int.Parse(_parametro.Parametro));
                                }                               

                                string strScript = "<script language='JavaScript'>" +
                                                   "location.href = '" + "DetallesInscripcionAudienciaPublica.aspx" + "'" +
                                                   "</script>";

                                LimpiarCampos();                                
                       

                            }
                            else
                            {
                                lbl_respuesta.Text = "La inscripción a audiencia publica No. " + lblNoAUP.Text + " no se pudo realizar, intente nuevamente";
                            }
                        }
                        else
                        {
                            lbl_respuesta.Text = "La inscripción a audiencia publica No. " + lblNoAUP.Text + " no se pudo realizar, intente nuevamente";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensajeError.Text = "Ocurrio un error al ingresar el registro," + ex.Message;

                    }
                }
                else
                {
                    lblMensajeError.Text = "¡Debe seleccionar un tipo de documento de identidad!";
                }
            }
            else
            {
                lblMensajeError.Text = "¡No existe un número de audiencia pública o un número SILPA asociado a la inscripción!";
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }     
    }

    /// <summary>
    /// Boton adjuntar archivos a la lista de archivos para enviar al fileTraffic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdAdjuntar_Click(object sender, EventArgs e)
    {
        try
        {
            string _nombreArchivo = uplAdjuntarArchivo.PostedFile.FileName;
            if (ViewState["i"] == null)
            {
                ViewState["i"] = 0;
                _bytes = new List<byte[]>();
                _archivo = new List<string>();
                _tamanio = new List<int>();
                Session.Add("ListaBytes", _bytes);
                Session.Add("ListaArchivos", _archivo);
                Session.Add("TamanioArchivos", _tamanio);
            }

            // Valiad si fue seleccionado un archivo
            if (_nombreArchivo != null && _nombreArchivo != string.Empty)
            {

                Int64 _tamanioArchivos = 0;

                _bytes = (List<byte[]>)Session["ListaBytes"];
                _archivo = (List<string>)Session["ListaArchivos"];
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
                }
                else
                {
                    lblMensajeArchivos.Visible = true;
                    lblMensajeArchivos.Text = "El tamaño de los archivos supera el limite permitido. ";
                }
            }
        }
        catch (Exception ex)
        {
            lblMensajeError.Text = "Ocurrio un error al tratar de adjuntar el archivo," + ex.Message;

        }
    }

    /// <summary>
    /// Boton Eliminar archivos a la lista de archivos para enviar al fileTraffic
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdEliminar_Click(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            lblMensajeError.Text = "Ocurrio un error al eliminar el archivo de la lista," + ex.Message;

        }
    }

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
        //CargarComboMunicipios(cboMunicipioQuejoso, cboDepartamentoQuejoso.SelectedItem.Value);

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
    /// Limpia los campos de la pagina
    /// </summary>
    public void LimpiarCampos()
    {
        this.cboDepartamento.SelectedIndex = 0;
        this.cboMunicipioAudiencia.SelectedIndex = 0;
        this.cboTipoDocumento.SelectedIndex = 0;
        this.txtCedula.Text = string.Empty;
        this.txtEmail.Text = string.Empty;
        this.txtPrimerApellido.Text= string.Empty;
        this.txtsegundoApellido.Text = string.Empty;
        this.txtPrimerNombre.Text = string.Empty;
        this.txtSegundoNombre.Text = string.Empty;
        this.txtDe.Text = string.Empty;
        this.txtEntidadComunidad.Text = string.Empty;
        this.lstListaArchivos.Items.Clear();
    }

#endregion



    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarComboMunicipios(this.cboMunicipioAudiencia, this.cboDepartamento.SelectedItem.Value);
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnRegresar1_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("MenuAudienciaPublica.aspx");
    }
}
