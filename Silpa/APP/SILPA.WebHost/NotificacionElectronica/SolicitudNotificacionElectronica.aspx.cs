using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.Comun;
using SoftManagement.Log;
using System.Data;
using SILPA.AccesoDatos.Notificacion;
using SILPA.AccesoDatos.BPMProcess;
using System.IO;
using System.Xml.Serialization;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using System.Configuration;


public partial class NotificacionElectronica_SolicitudNotificacionElectronica : System.Web.UI.Page
{

    #region Propiedades

    /// <summary>
    /// Información del usuario que se encuentra autenticado
    /// </summary>
    private PersonaIdentity Persona
    {
        get { return (PersonaIdentity)ViewState["personaIdentity"]; }
        set { ViewState["personaIdentity"] = value; }
    }

    #endregion


    #region Metodos Privados

    /// <summary>
    /// Verificar que la segunda clave sea valida
    /// </summary>
    /// <returns>bool indicando si la clave es valida</returns>
    private bool SegundaClaveValida()
    {
        Persona objPersona = null;

        //Crear objeto de manejo de datos
        objPersona = new Persona();

        //Retornar resultado validacion
        return objPersona.ValidarSegundaClave(int.Parse(Session["Usuario"].ToString()), EnDecript.Encriptar(this.txtContrasena.Text.Trim()));
    }

    /// <summary>
    /// Verificar si se encuentra autenticado el usuario
    /// </summary>
    /// <returns></returns>
    private bool ValidacionToken()
    {
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
    /// Mostrar el mensaje especificado
    /// </summary>
    /// <param name="p_strMensaje">string con el mensaje</param>
    private void MostrarMensaje(string p_strMensaje)
    {
        this.lblMensaje.Text = p_strMensaje;
        this.tableMensaje.Visible = true;
    }


    /// <summary>
    /// Inicializar mensajes
    /// </summary>
    private void InicializarMensaje()
    {
        this.lblMensaje.Text = "";
        this.tableMensaje.Visible = false;
    }


    /// <summary>
    /// Seleccionar el tipo de notificación que tiene un usuario
    /// </summary>
    private void SeleccionarTipoExpediente()
    {
        SolicitudNotificacion objSolitudNotificacion = null;
        PersonaNotExpedienteEntity objNotificaciones = null;

        //Consultar los tipos de notificacion
        objSolitudNotificacion = new SolicitudNotificacion();
        objNotificaciones = objSolitudNotificacion.ListarTipoNotificadosPersona(Convert.ToInt32(this.Persona.IdApplicationUser));

        //Seleccionar el tipo de notificación
        if (objNotificaciones != null)
        {
            this.RdbNotificarTodos.Checked = objNotificaciones.ES_NOTIFICACION_ELECTRONICA;
            this.RdbNotificarExpediente.Checked = objNotificaciones.ES_NOTIFICACION_ELECTRONICA_X_EXP;
            this.RdbNotificarPresencial.Checked = (!objNotificaciones.ES_NOTIFICACION_ELECTRONICA && !objNotificaciones.ES_NOTIFICACION_ELECTRONICA_X_EXP && !objNotificaciones.ES_NOTIFICACION_ELECTRONICA_AA);
        }
        else
        {
            //Como es null no tiene registro por lo cual se marca opción tradicional
            this.RdbNotificarPresencial.Checked = true;
        }
    }


    /// <summary>
    /// Obtener el listado de expedientes registrados por el usuario para notificación electronica
    /// </summary>
    /// <returns>LIst con los expedientes registrados. Se carga en listado de items</returns>
    private List<ListItem> ObtenerListadoExpedientesRegistrados()
    {
        SolicitudNotificacion objSolitudNotificacion = null;
        List<NotExpedientesEntity> lstExpedientes = null;
        List<ListItem> objListaExpedientes = null;
        string strNombreExpediente = "";
        string strClaveExpediente = "";

        //Consultar el listado de expedientes a notificar
        objSolitudNotificacion = new SolicitudNotificacion();
        lstExpedientes = objSolitudNotificacion.ConsultarExpedienteNotificarPersona(Convert.ToInt32(this.Persona.IdApplicationUser));

        //Cargar el listado de expedientes
        if (lstExpedientes != null)
        {
            //Crear listado que contiene informacion 
            objListaExpedientes = new List<ListItem>();

            foreach (NotExpedientesEntity objExpediente in lstExpedientes)
            {
                //Cargar nombre y clave de identificación del expediente
                strNombreExpediente = objExpediente.DESC_SOL_ID_AA + " - " + objExpediente.DESC_EXPEDIENTE;
                strClaveExpediente = objExpediente.SOL_ID_SOLICITANTE + "@" + objExpediente.DESC_SOL_ID_SOLICITANTE.Trim() + "@" + objExpediente.SOL_ID_AA + "@" + objExpediente.SOL_NUMERO_SILPA.Trim() + "@" + objExpediente.ID_EXPEDIENTE.Trim();

                //Adicionar expediente al listado
                objListaExpedientes.Add(new ListItem(strNombreExpediente, strClaveExpediente));
            }
        }

        return objListaExpedientes;
    }


    /// <summary>
    /// Obtener el listado de todos los expedientes a los cuales tiene acceso el usuario
    /// </summary>
    /// <returns>LIst con los expedientes a los cuales tiene acceso el usuario. Se carga en listado de items</returns>
    private List<ListItem> ObtenerListadoExpedientesRelacionados()
    {
        SolicitudNotificacion objSolitudNotificacion = null;
        List<NotExpedientesEntity> lstExpedientes = null;
        List<ListItem> objListaExpedientes = null;
        string strNombreExpediente = "";
        string strClaveExpediente = "";

        //Consultar el listado de expedientes a notificar
        objSolitudNotificacion = new SolicitudNotificacion();
        lstExpedientes = objSolitudNotificacion.ConsultarExpedienteAutoridadPersona(Convert.ToInt32(this.Persona.IdApplicationUser), 0);

        //Cargar el listado de expedientes
        if (lstExpedientes != null)
        {
            //Crear listado que contiene informacion 
            objListaExpedientes = new List<ListItem>();

            foreach (NotExpedientesEntity objExpediente in lstExpedientes)
            {
                //Cargar nombre y clave de identificación del expediente
                strNombreExpediente = objExpediente.DESC_SOL_ID_AA + " - " + objExpediente.DESC_EXPEDIENTE;
                strClaveExpediente = objExpediente.SOL_ID_SOLICITANTE + "@" + objExpediente.DESC_SOL_ID_SOLICITANTE.Trim() + "@" + objExpediente.SOL_ID_AA + "@" + objExpediente.SOL_NUMERO_SILPA.Trim() + "@" + objExpediente.ID_EXPEDIENTE.Trim();

                //Adicionar expediente al listado
                objListaExpedientes.Add(new ListItem(strNombreExpediente, strClaveExpediente));
            }
        }

        return objListaExpedientes;
    }


    /// <summary>
    /// Cargar el listado de expedientes inscritos
    /// </summary>
    private void CargarListadoExpedientes()
    {
        List<ListItem> objListaExpedientes = null;

        //Limpiar listado de expedientes
        this.lstExpedientesNotificar.Items.Clear();

        //Verificar si se encuentra seleccionada la notificación por expediente
        if (this.RdbNotificarExpediente.Checked)
        {
            //Consultar el listado de expedientes a notificar
            objListaExpedientes = this.ObtenerListadoExpedientesRegistrados();

            //Cargar el listado de expedientes
            if (objListaExpedientes != null && objListaExpedientes.Count > 0)
            {
                //Adicionar expedientes al listado
                this.lstExpedientesNotificar.Items.AddRange(objListaExpedientes.ToArray());
            }
            else
            {
                this.lstExpedientesNotificar.Visible = false;
            }

            //Mostrar el listado de expedientes
            this.rowExpedientes.Visible = true;

            //Mostrar fila de botones
            this.rowBotonesExpedientes.Visible = true;

            //Verificar si se tiene expedientes cargados sino ocultar controles
            if (this.lstExpedientesNotificar.Items != null && this.lstExpedientesNotificar.Items.Count > 0)
            {
                this.rowSeleccionarTodosExpedientes.Visible = true;
                this.btnEliminarExpediente.Visible = true;
            }
            else
            {
                this.rowSeleccionarTodosExpedientes.Visible = false;
                this.btnEliminarExpediente.Visible = false;
            }

            // Quitar selección de expedientes
            this.chkSeleccionarTodosExpedientes.Checked = false;
        }
        else if (this.RdbNotificarTodos.Checked)
        {
            //Consultar el listado de expedientes a notificar
            objListaExpedientes = this.ObtenerListadoExpedientesRelacionados();

            //Cargar el listado de expedientes
            this.lstExpedientesNotificar.Visible = true;
            if (objListaExpedientes != null)
            {
                //Adicionar expedientes al listado
                this.lstExpedientesNotificar.Items.AddRange(objListaExpedientes.ToArray());
            }

            //Mostrar el listado de expedientes
            this.rowExpedientes.Visible = true;

            //Ocultar botones
            this.rowSeleccionarTodosExpedientes.Visible = false;
            this.rowBotonesExpedientes.Visible = false;
        }
        else
        {   //Ocultar listado de expedientes
            this.rowExpedientes.Visible = false;
        }

    }


    /// <summary>
    /// Cargar el listado de expedientes de una autoridad que referencian a un usuario
    /// </summary>
    private void CargarListadoExpedientesAutoridad()
    {
        SolicitudNotificacion objSolitudNotificacion = null;
        List<NotExpedientesEntity> lstExpedientes = null;
        ListItem objItem = null;
        string strClaveExpediente = "";

        //Consultar el listado de expedientes
        objSolitudNotificacion = new SolicitudNotificacion();
        lstExpedientes = objSolitudNotificacion.ConsultarExpedienteAutoridadPersona(Convert.ToInt32(this.Persona.IdApplicationUser), Convert.ToInt32(cboAutoridadAmbiental.SelectedValue));

        //Inicializar listado
        this.cboExpedientes.Items.Clear();
        this.cboExpedientes.Items.Add(new ListItem("Seleccione.", "-1"));

        //Adicionar opciones a listado
        if (lstExpedientes != null && lstExpedientes.Count > 0)
        {
            foreach (NotExpedientesEntity objExpediente in lstExpedientes)
            {
                //Cargar clave del expediente
                strClaveExpediente = objExpediente.SOL_ID_SOLICITANTE + "@" + objExpediente.DESC_SOL_ID_SOLICITANTE.Trim() + "@" + objExpediente.SOL_ID_AA + "@" + objExpediente.SOL_NUMERO_SILPA + "@" + objExpediente.ID_EXPEDIENTE;

                //Crear item
                objItem = lstExpedientesNotificar.Items.FindByValue(strClaveExpediente);

                //Verificar que no se encuentre ingresado en el listado de expedientes
                if (objItem == null)
                {
                    this.cboExpedientes.Items.Add(new ListItem(objExpediente.DESC_SOL_ID_AA + " - " + objExpediente.DESC_EXPEDIENTE, strClaveExpediente));
                }
            }

            //Verificar si se cargo expedientes
            if (this.cboExpedientes.Items.Count > 1)
            {
                //Cargar opcion de todos
                this.cboExpedientes.Items.Insert(1, new ListItem("TODOS", "0"));
            }
        }

    }


    /// <summary>
    /// Seleccionar el tipo de notificación que tiene un usuario
    /// </summary>
    private void CargarAutoridadesAmbientales()
    {
        Notificacion objNotificacion = null;

        //Consultar y cargar listado de autoridades
        objNotificacion = new Notificacion();
        this.cboAutoridadAmbiental.DataSource = objNotificacion.ListarAutoridadAmbientalNotificacion(true);
        this.cboAutoridadAmbiental.DataValueField = "AUT_ID";
        this.cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
        this.cboAutoridadAmbiental.DataBind();
        this.cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione.", "-1"));

        //Verificar si se cargo autoridades ambientales
        if (this.cboAutoridadAmbiental.Items.Count > 1)
        {
            //Cargar opcion de todos
            this.cboAutoridadAmbiental.Items.Insert(1, new ListItem("TODOS", "0"));
        }
    }


    /// <summary>
    /// Inicializar listado de expedientes por autoridad
    /// </summary>
    private void InicializarListaExpedientesAutoridad()
    {
        this.cboExpedientes.Items.Clear();
        this.cboExpedientes.Items.Add(new ListItem("Seleccione.", "-1"));
    }


    /// <summary>
    /// Cargar la información de la página
    /// </summary>
    private void CargarPagina()
    {
        Persona objPersona = null;

        try
        {
            //Inicializar mensajes
            this.InicializarMensaje();

            //Crear objeto de persona
            objPersona = new Persona();

            //Verificar si tiene registrada segunda firma
            if (objPersona.TieneSegundaClave(int.Parse(Session["Usuario"].ToString())))
            {

                //Cargar información del usuario                
                objPersona.PersonaByUserId(Session["Usuario"].ToString());
                this.Persona = objPersona.Identity;

                //Inicializar tipo de notificación
                this.SeleccionarTipoExpediente();

                //Cargar el listado de expedientes
                this.CargarListadoExpedientes();

                //Cargar el listado de autoridades ambientales
                this.CargarAutoridadesAmbientales();

                //Inicializar listado de expedientes
                this.InicializarListaExpedientesAutoridad();

                //Check de terminos y condiciones no seleccionado
                this.chkAceptoTerminos.Checked = false;

                //Mostrar div de formulario
                this.divFormulario.Visible = true;
                this.divFormularioBotones.Visible = true;

                //Ocultar div de resultado
                this.divRespuesta.Visible = false;

                //Ocultar div de mensaje de error
                this.divMensajeError.Visible = false;

                //Limpiar sesion
                Session["paginaRetorno"] = null;
            }
            else
            {
                //Ocultar div de formulario
                this.divFormulario.Visible = false;
                this.divFormularioBotones.Visible = false;

                //Ocultar div de resultado
                this.divRespuesta.Visible = false;

                //Mostrar div de mensaje de error
                this.divMensajeError.Visible = true;

                //Crear variable de sesión con nombre de retorno
                Session["paginaRetorno"] = "NotificacionElectronica/SolicitudNotificacionElectronica.aspx";
            }
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_SolicitudNotificacionElectronica :: CargarPagina -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            //Cargar mensaje de error                        
            this.MostrarMensaje("Se genero un error cargando la información de la notificación electrónica");
        }

    }


    /// <summary>
    /// Cargar el listado de expedientes seleccionados por el usuario
    /// </summary>
    /// <returns>List con el listado de expedientes</returns>
    private List<NotExpedientesEntity> CargarExpedientesRegistrar()
    {
        List<NotExpedientesEntity> objListaExpedientes = null;
        NotExpedientesEntity objExpediente = null;
        string[] objDatosExpediente = null;

        //Verificar que se hallan cargado expedientes
        if (this.lstExpedientesNotificar.Items.Count > 0)
        {
            //Crear listado
            objListaExpedientes = new List<NotExpedientesEntity>();

            //Ciclo que recorre las opciones y carga los expedientes
            foreach (ListItem objExpedienteIngresado in this.lstExpedientesNotificar.Items)
            {
                //Cargar los datos del expediente
                objDatosExpediente = objExpedienteIngresado.Value.Split('@');

                //Cargar datos en objeto
                objExpediente = new NotExpedientesEntity();
                objExpediente.SOL_ID_SOLICITANTE = int.Parse(objDatosExpediente[0]);
                objExpediente.DESC_SOL_ID_SOLICITANTE = objDatosExpediente[1].Trim();
                objExpediente.SOL_ID_AA = int.Parse(objDatosExpediente[2]);
                if (!string.IsNullOrEmpty(objDatosExpediente[3]))
                {
                    objExpediente.SOL_NUMERO_SILPA = objDatosExpediente[3];
                    objExpediente.ID_SOLICITUD = objDatosExpediente[3];
                }
                else
                {
                    objExpediente.SOL_NUMERO_SILPA = ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"];
                    objExpediente.ID_SOLICITUD = ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"];
                }
                objExpediente.ID_EXPEDIENTE = objDatosExpediente[4];

                //Adicionar a listado
                objListaExpedientes.Add(objExpediente);
            }
        }

        return objListaExpedientes;
    }


    /// <summary>
    /// Cargar el listado de expedientes seleccionados por el usuario
    /// </summary>
    /// <returns>List con el listado de expedientes</returns>
    private List<NotExpedientesEntity> CargarExpedientesEliminados()
    {
        List<NotExpedientesEntity> objListaExpedientes = null;
        NotExpedientesEntity objExpediente = null;
        string[] objDatosExpediente = null;
        List<ListItem> objListadoExpedientesSeleccionados = null;
        List<ListItem> objListadoExpedientesRegistrados = null;
        IEnumerable<ListItem> objListadoExpedientesEliminados = null;


        //Cargar el listado de item seleccionados
        objListadoExpedientesSeleccionados = this.lstExpedientesNotificar.Items.Cast<ListItem>().ToArray().ToList();

        //Consultar listado de expedientes actuales registrados
        objListadoExpedientesRegistrados = this.ObtenerListadoExpedientesRegistrados();

        //Obtener los expedientes que no se eliminaron
        if (objListadoExpedientesRegistrados != null)
        {
            objListadoExpedientesEliminados = objListadoExpedientesRegistrados.Except(objListadoExpedientesSeleccionados);

            //Ciclo que carga datos de eliminados
            foreach (ListItem objExpedienteEliminado in objListadoExpedientesEliminados)
            {
                //Cargar los datos del expediente
                objDatosExpediente = objExpedienteEliminado.Value.Split('@');

                //Cargar datos en objeto
                objExpediente = new NotExpedientesEntity();
                objExpediente.SOL_ID_SOLICITANTE = int.Parse(objDatosExpediente[0]);
                objExpediente.DESC_SOL_ID_SOLICITANTE = objDatosExpediente[1].Trim();
                objExpediente.SOL_ID_AA = int.Parse(objDatosExpediente[2]);
                objExpediente.SOL_NUMERO_SILPA = objDatosExpediente[3];
                objExpediente.ID_SOLICITUD = objDatosExpediente[3];
                objExpediente.ID_EXPEDIENTE = objDatosExpediente[4];

                //Adicionar a listado
                if (objListaExpedientes == null) objListaExpedientes = new List<NotExpedientesEntity>();
                objListaExpedientes.Add(objExpediente);
            }
        }


        return objListaExpedientes;
    }


    /// <summary>
    /// Cargar la información basica de notificación
    /// </summary>
    /// <returns>PersonaNotExpedienteEntity con la información basica de la notificación</returns>
    private PersonaNotExpedienteEntity CargarDatosBasicosNotificacion()
    {
        PersonaNotExpedienteEntity objDatosNotificacion = null;

        //Cargar datos usuario
        objDatosNotificacion = new PersonaNotExpedienteEntity();
        objDatosNotificacion.Activo = true;
        objDatosNotificacion.Fecha = DateTime.Now;
        objDatosNotificacion.PERSONA_IDENTIFICACION = this.Persona.NumeroIdentificacion;
        objDatosNotificacion.PERSONA_TIPO_IDENTIFICACION = Convert.ToInt32(this.Persona.TipoDocumentoIdentificacion.Id.ToString());
        objDatosNotificacion.PERSONA_NOMBRE_COMPLETO = this.Persona.PrimerNombre + " " + this.Persona.SegundoNombre + " " + this.Persona.PrimerApellido + " " + this.Persona.SegundoApellido;
        objDatosNotificacion.PERSONA_PER_ID = Convert.ToInt32(this.Persona.IdApplicationUser);

        //Cargar datos tipo notificacion
        objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA = this.RdbNotificarTodos.Checked;
        objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_AA = false;
        objDatosNotificacion.ES_NOTIFICACION_ELECTRONICA_X_EXP = this.RdbNotificarExpediente.Checked;

        return objDatosNotificacion;
    }

    /// <summary>
    /// Guardar la información de notificación configurada por el usuario
    /// </summary>
    /// <returns>string con el número vital resultado del almacenaminto del resultado</returns>
    private string GuardarInformacionNotificacion()
    {
        string strNumeroVital = "";
        PersonaNotExpedienteEntity objDatosNotificacion = null;
        List<NotExpedientesEntity> objListaExpedientes = null;
        List<NotExpedientesEntity> objListaExpedientesEliminados = null;
        SolicitudNotificacion objSolicitudNotificacion = null;

        //Cargar datos de notificacion
        objDatosNotificacion = this.CargarDatosBasicosNotificacion();

        //Si se selecciono la opción por expediente cargar el listado de expedientes a notificar
        if (this.RdbNotificarExpediente.Checked)
        {

            //Cargar  los expedientes a registrar
            objListaExpedientes = this.CargarExpedientesRegistrar();

            //Cargar los expedientes eliminados
            objListaExpedientesEliminados = this.CargarExpedientesEliminados();
        }
        else if (this.RdbNotificarPresencial.Checked)
        {
            //Cargar los expedientes eliminados
            objListaExpedientesEliminados = this.CargarExpedientesEliminados();
        }

        //Guardar la información de la notificación
        objSolicitudNotificacion = new SolicitudNotificacion();
        strNumeroVital = objSolicitudNotificacion.GuardarSolicitudNotificacion(objDatosNotificacion, objListaExpedientes, objListaExpedientesEliminados);

        return strNumeroVital;
    }


    #endregion


    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Session["Usuario"] = "15456";
            //Session["Usuario"] = "15518";
            //Session["Usuario"] = "15449";
            //this.CargarPagina();

            //Iniciliazar datos de la página
            if (this.ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                //Cargar datos de la pagina
                this.CargarPagina();
            }
        }

    }


    /// <summary>
    /// Evento que se ejecuta cuando se cambia el tipo de notificación. Muestra la información de acuerdo al tipo de notificación seleccionada
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TipoSolicitud_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //Inicializar mensajes
            this.InicializarMensaje();

            //Cargar el listado de expedientes
            this.CargarListadoExpedientes();
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_SolicitudNotificacionElectronica :: TipoSolicitud_CheckedChanged -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            //Cargar mensaje de error                        
            this.MostrarMensaje("Se genero un error cargando la información de la notificación electrónica seleccionada");
        }
    }


    /// <summary>
    /// Evento que se ejecuta el boton de Aceptar de un Expediente. Adiciona a listado el expediente seleccionado
    /// </summary>
    protected void btnAdicionarExpedienteModal_Click(object sender, EventArgs e)
    {
        try
        {
            //Inicializar mensajes
            this.InicializarMensaje();

            //Verificar que no exista errores
            if (Page.IsValid)
            {

                //Verificar que se halla seleccionado opciones
                if (this.cboAutoridadAmbiental.SelectedValue != "-1" && this.cboExpedientes.SelectedValue != "-1")
                {
                    //Verificar si son todos los expedientes del listado
                    if (this.cboExpedientes.SelectedValue == "0")
                    {
                        foreach (ListItem objExpediente in this.cboExpedientes.Items)
                        {
                            //Verificar que sea un expediente
                            if (objExpediente.Value != "0" && objExpediente.Value != "-1")
                            {
                                //Adicionar expediente al listado
                                this.lstExpedientesNotificar.Items.Add(new ListItem(objExpediente.Text, objExpediente.Value));
                            }
                        }
                    }
                    else
                    {
                        //Adicionar expediente al listado
                        this.lstExpedientesNotificar.Items.Add(new ListItem(this.cboExpedientes.SelectedItem.Text, this.cboExpedientes.SelectedValue));
                    }

                    //Mostrar opción de seleccionar todos
                    this.rowSeleccionarTodosExpedientes.Visible = true;

                    //Mostrar boton de quitar expediente
                    this.btnEliminarExpediente.Visible = true;

                    //Seleccionar primera opción
                    this.cboAutoridadAmbiental.SelectedIndex = 0;

                    //Inicializar listado de expedientes
                    this.InicializarListaExpedientesAutoridad();

                    //Limpiar check de seleccionar todos
                    this.chkSeleccionarTodosExpedientes.Checked = false;

                    //Mostrar listado de expedientes
                    this.lstExpedientesNotificar.Visible = true;
                }
            }
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_SolicitudNotificacionElectronica :: btnAdicionarExpedienteModal_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            //Cargar mensaje de error                        
            this.MostrarMensaje("Se genero un error adicionando un nuevo expediente");
        }
    }


    /// <summary>
    /// Evento que se ejecuta al dar cancelar en el modal. Limpia los campos del modal
    /// </summary>
    protected void btnCancelarExpedienteModal_Click(object sender, EventArgs e)
    {
        try
        {
            //Inicializar mensajes
            this.InicializarMensaje();

            //Seleccionar primera opción
            this.cboAutoridadAmbiental.SelectedIndex = 0;

            //Inicializar listado de expedientes
            this.InicializarListaExpedientesAutoridad();
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_SolicitudNotificacionElectronica :: btnCancelarExpedienteModal_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            //Cargar mensaje de error                        
            this.MostrarMensaje("Se genero un error adicionando un nuevo expediente");
        }
    }


    /// <summary>
    /// Evento que se ejecuta al hacer clic en desasociar expediente. Retira del listado de expedientes a notificar los expedientes seleccionados
    /// </summary>
    protected void btnEliminarExpediente_Click(object sender, EventArgs e)
    {
        ListItem[] objListaItems = null;

        try
        {
            //Inicializar mensajes
            this.InicializarMensaje();

            //Recorrer lista
            if (this.lstExpedientesNotificar.Items != null)
            {
                //Cargar listado de items
                objListaItems = new ListItem[this.lstExpedientesNotificar.Items.Count];
                this.lstExpedientesNotificar.Items.CopyTo(objListaItems, 0);


                //Ciclo que recorre items y elimina del listado
                foreach (ListItem objItem in objListaItems)
                {
                    //Verificar si esta seleccionado
                    if (objItem.Selected)
                    {
                        this.lstExpedientesNotificar.Items.Remove(objItem);
                    }
                }

                //Si se elimino todos los expedientes ocultar listado
                if (this.lstExpedientesNotificar.Items.Count == 0)
                {
                    this.lstExpedientesNotificar.Visible = false;
                }
                else
                {
                    this.lstExpedientesNotificar.Visible = true;
                }
            }

            //Verificar si quedo con expedientes seleccionados.
            if (this.lstExpedientesNotificar.Items != null && this.lstExpedientesNotificar.Items.Count > 0)
            {
                this.rowSeleccionarTodosExpedientes.Visible = true;
                this.btnEliminarExpediente.Visible = true;
            }
            else
            {
                this.rowSeleccionarTodosExpedientes.Visible = false;
                this.btnEliminarExpediente.Visible = false;
            }

            //Limpiar check de seleccionar todos
            this.chkSeleccionarTodosExpedientes.Checked = false;
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_SolicitudNotificacionElectronica :: btnEliminarExpediente_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            //Cargar mensaje de error                        
            this.MostrarMensaje("Se genero un error quitando los expedientes del listado");
        }
    }


    /// <summary>
    /// Evento que se ejecuta cuando se selecciona una autoridad ambiental. Carga el listado de expedientes dependientes para notificación
    /// </summary>
    protected void cboAutoridadAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Inicializar mensajes
            this.InicializarMensaje();

            //Verificar que seleccione alguna opción sino limpiar desplegable de expedientes
            if (this.cboAutoridadAmbiental.SelectedValue != "-1")
            {
                this.CargarListadoExpedientesAutoridad();
            }
            else
            {
                this.InicializarListaExpedientesAutoridad();
            }
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_SolicitudNotificacionElectronica :: cboAutoridadAmbiental_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            //Cargar mensaje de error                        
            this.MostrarMensaje("Se genero un error cargando la información de los expedientes");
        }
    }


    /// <summary>
    /// Evento que se ejecuta al dar clic en el botón de Aceptar del formualrio. Guarda la configuración de notificación
    /// </summary>
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        string strNumeroVital = "";

        try
        {
            //Inicializar mensajes
            this.InicializarMensaje();

            //Verificar que no exista errores
            if (Page.IsValid)
            {
                //Verificar que la segunda clave sea valida
                if (this.SegundaClaveValida())
                {
                    //Guardar la información de notificación
                    strNumeroVital = this.GuardarInformacionNotificacion();

                    //Verificar que se halla obtenido número vital
                    if (!string.IsNullOrWhiteSpace(strNumeroVital))
                    {
                        //Cargar el número vital
                        this.ltlNumeroVITAL.Text = strNumeroVital;

                        //Ocultar div de formulario
                        this.divFormulario.Visible = false;
                        this.divFormularioBotones.Visible = false;

                        //Mostrar div de resultado
                        this.divRespuesta.Visible = true;
                    }
                    else
                    {
                        //Limpiar número vital
                        this.ltlNumeroVITAL.Text = "";

                        //Mostrar div de formulario
                        this.divFormulario.Visible = false;
                        this.divFormularioBotones.Visible = false;

                        //Ocultar div de resultado
                        this.divRespuesta.Visible = false;

                        //Generar excepción
                        throw new Exception("Se presento error en el almacenamiento de la información. Número VITAL no Generado");
                    }
                }
                else
                {
                    //Mostrar mensaje de error
                    this.MostrarMensaje("La segunda clave se encuentra incorrecta.");
                }
            }
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_SolicitudNotificacionElectronica :: btnAceptar_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            //Cargar mensaje de error                        
            this.MostrarMensaje("Se genero un error guardando la información de notificación");
        }
    }


    /// <summary>
    /// Evento que se ejecuta al dar clic en el botón de Aceptar de la respuesta. Muestra nuevamente el formulario
    /// </summary>
    protected void btnAceptarRespuesta_Click(object sender, EventArgs e)
    {
        try
        {
            //Cargar nuevamente la pagina
            this.CargarPagina();
        }
        catch (Exception exc)
        {
            //Escribir error
            SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_SolicitudNotificacionElectronica :: btnAceptarRespuesta_Click -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

            //Cargar mensaje de error                        
            this.MostrarMensaje("Se genero un error cargando la información de la página");
        }
    }

    #endregion


}