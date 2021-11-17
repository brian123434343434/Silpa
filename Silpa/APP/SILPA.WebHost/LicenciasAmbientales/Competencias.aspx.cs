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
using SILPA.Servicios.SolicitudDAA;
using SILPA.Comun;
using SILPA.Servicios.Generico.RadicarDocumento;
using SILPA.LogicaNegocio.Generico;
using Silpa.Workflow;
using SoftManagement.Log;
using SoftManagement.Log;

public partial class LicenciasAmbientales_Competencias : System.Web.UI.Page
{
    private const string CondicionBtnAceptar = "";
    private const string CondicionBtnTDREIA = "VITALCONPRO01-06";
    private const string CondicionSolTDREIA = "VITALCONPRO01-07";
    private const string CondicionBtnSolicitarDaa = "VITALCONPRO01-06";


    protected void Page_Load(object sender, EventArgs e)
    {
        Session["IDProcessInstance"] = long.Parse(Request.QueryString["IDProcessInstance"]);
        Session["IDActivityInstance"] = long.Parse(Request.QueryString["IDActivityInstance"]);
        Session["EntryData"] = Request.QueryString["EntryData"];
        Session["IDEntryData"] = Request.QueryString["IDEntryData"];
        Session["IdRelated"] = Request.QueryString["IdRelated"];
        if (!IsPostBack)
        {
            
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            cboAutoridadAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
            cboAutoridadAmbiental.DataValueField = "AUT_ID";
            cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            cboAutoridadAmbiental.DataBind();
            cboAutoridadAmbiental.Enabled = true;
            optAutoridadAmbiental.SelectedItem.Value = "2";
            EscogerCombo();
            
        }        


        if (ValidacionToken() == false)
            Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
        CargarPagina();
    }

    /// <summary>
    /// Valida si hay token para el usuario que intenta acceder a la página, y si no ha expirado
    /// </summary>
    /// <returns>Verdadero si hay token válido para el usuario</returns>
    private bool ValidacionToken()
    {
        try
        {            
            Session["IDForToken"] = Request.QueryString["IdRelated"];

            string nana = HttpContext.Current.User.Identity.Name;

            SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

            Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));
            using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
            {
                servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
                string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
                if (mensaje.IndexOf("Token invalido") > 0)
                    return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.AppRelativeVirtualPath.ToString() + " ---- Error:  " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un  error comuniquese con el administrador.");
        }
        return false;
    }

    private void EscogerCombo()
    {
        try
        {            
            if (optAutoridadAmbiental.SelectedValue == "2")
            {
                SolicitudFachada _solicitudFachada = new SolicitudFachada();
                cboAutoridadAmbiental.Visible = true;

                cboAutoridadAmbiental.DataSource = _solicitudFachada.ConsultarAutoridadesAmbientales((long)Session["IDProcessInstance"]);
                cboAutoridadAmbiental.DataValueField = "AUT_ID";
                cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
                cboAutoridadAmbiental.DataBind();

                cboAutoridadAmbiental.Visible = true;

                lblMensaje.Visible = true;
                this.pnlPrincipal.Visible = true;

            }
            else
            {
                cboAutoridadAmbiental.Visible = false;        
            }
          
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.AppRelativeVirtualPath.ToString() + " ---- Error:  " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un  error comuniquese con el administrador.");
        }        
    }

    protected void optAutoridadAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {
        EscogerCombo();
    }
    //El botón aceptar solo funciona para el estado 10
    protected void btnAceptar_Click(object sender, EventArgs e)
    {

        try
        {            
            int _idRadicacion = 0;
            SolicitudFachada _solicitudFachada = new SolicitudFachada();
            Proceso _objProceso = new Proceso();

            // 04-jun-10
            SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();
            _servicioBPM.Url = SILPA.Comun.DireccionamientoWS.UrlWS("GattacaBpm");
            //Se asigna la autoridad ambiental seleccionada...
            int _intAutoridadAmbiental = 0;
            if (optAutoridadAmbiental.SelectedItem.Value == "1")
            {
                //_intAutoridadAmbiental = (int)AutoridadesAmbientales.MAVDT;
                AutoridadAmbiental aa = new AutoridadAmbiental();
                _intAutoridadAmbiental = aa.ObtenerIdAutoridadAmbientalMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);
            }
            else if (optAutoridadAmbiental.SelectedItem.Value == "2")
                _intAutoridadAmbiental = Convert.ToInt32(cboAutoridadAmbiental.SelectedItem.Value);

            //Se publica la radicación
            _idRadicacion = _solicitudFachada.RadicarSolicitud((long)Session["IDProcessInstance"], _intAutoridadAmbiental, Convert.ToInt64(Session["IdRelated"]));

            //Se actualiza la solicitud con la AA escogida
            _solicitudFachada.ActualizarAASolicitud((long)Session["IDProcessInstance"], _intAutoridadAmbiental, _idRadicacion);

            //Se publica radicado...

            _objProceso.EstablecerCondicion(Convert.ToInt32(Session["IDActivityInstance"]), "Radicacion");

            /// Comentado 04-jun-2010
            _servicioBPM.EndActivityInstance("Softmanagement", Convert.ToInt64(Session["IdRelated"]), (long)Session["IDActivityInstance"], (long)Session["IDProcessInstance"], _objProceso.IdCondicion, "", "", (string)Session["EntryData"], "0", (string)Session["IDEntryData"]);
            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();

                    _solicitudFachada.ActualizarEstadoSolicitud((long)Session["IDProcessInstance"], (int)EstadoProcesoDAA.Pendiente_Radicacion);

            ActualizarEstadoRadicadoLeido(_idRadicacion);

            //Se esconde el botón para que no sea presionado de nuevo
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;

            RadicacionDocumento objRadicacion = new RadicacionDocumento();
            objRadicacion.ObtenerRadicacion(null, (int.Parse(Request.QueryString["IDProcessInstance"].ToString())));
            SILPA.Servicios.ImpresionFUS.ImpresionFUSFachada.GenerarFus(objRadicacion._objRadDocIdentity.Id, int.Parse(Request.QueryString["IDProcessInstance"].ToString()), objRadicacion._objRadDocIdentity.UbicacionDocumento, objRadicacion._objRadDocIdentity.NumeroVITALCompleto, objRadicacion._objRadDocIdentity.IdSolicitante);

            optAutoridadAmbiental.Visible = false;
            cboAutoridadAmbiental.Visible = false;

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("LICENCIASAMBIENTALES – RADICAR FU", 1, "Se almaceno Competencias");

            /// 04-jun-10
            //lblMensaje.Text = "Solicitud Enviada con éxito, puede cerrar está ventana ahora";
            Mensaje.MostrarMensaje(this, "Solicitud Enviada con éxito, puede cerrar está ventana ahora", true);

        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.AppRelativeVirtualPath.ToString() + " ---- Error:  " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un  error comuniquese con el administrador.");
        }        

    }

    /// <summary>
    /// Actualiza el Estado Leído a False para que las AA puedan leer el Registro de Radicación
    /// </summary>
    /// <param name="idRadicacion">id de Radicado</param>
    protected void ActualizarEstadoRadicadoLeido(int idRadicacion)
    {
        RadicacionDocumento rad = new RadicacionDocumento();
        rad.ObtenerRadicacion(idRadicacion, null);
        if (rad._objRadDocIdentity.Id != 0)
        {
            rad._objRadDocIdentity.Leido = false;
            rad.ActualizarEstadoRadicacion();
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Utilidades.CerrarVentana(this);
    }

    #region Funciones Programador

    private void CargarAutoridadAmbiental()
    {
        SILPA.LogicaNegocio.Generico.AutoridadAmbiental autoridadAmbiental = new SILPA.LogicaNegocio.Generico.AutoridadAmbiental();
        
    }



    #endregion

    protected void CargarPagina()
    {
        try
        {            
            SolicitudFachada _solicitudFachada = new SolicitudFachada();
            Configuracion _objConfiguracion = new Configuracion();

            //int _intEstado= _solicitudFachada.ConsultarProceso(int.Parse(txt_numero_SILPA.Text));
            int _intEstado = _solicitudFachada.ConsultarProceso(int.Parse(Request.QueryString["IDProcessInstance"]));

            Session["Estado"] = _intEstado;

            switch (_intEstado)
            {
                case (int)EstadoProcesoDAA.Enviar_EIA: //E=12 C3

                    lblMensaje.Text = "Su proyecto, obra o actividad no requiere pronunciamiento sobre la necesidad de Diagnóstico Ambiental de Alternativas, para solicitar los Términos de Referencia para el Estudio de Impacto Ambiental de clic sobre el botón Solicitar TDR para EIA";
                    lblMensaje.Visible = true;
                    btnSolicitarTDREIA.Visible = true;
                    btnAceptar.Visible = false;
                    optAutoridadAmbiental.Visible = false;
                    break;
                case (int)EstadoProcesoDAA.Enviar_TDR_DAA: //E=1  c2
                    bool _esConflicto = _solicitudFachada.EsConflicto(Convert.ToInt64(Request.QueryString["IDProcessInstance"]));
                    lblEnlaceDescarga.Visible = true;
                    hplEnlaceDescarga.Visible = true;
                    btnSolicitarDAA.Visible = true;
                    hplEnlaceDescarga.Text = _solicitudFachada.ObtenerURL_TDRDAA(Convert.ToInt64(Request.QueryString["IDProcessInstance"]));
                    hplEnlaceDescarga.NavigateUrl = _solicitudFachada.ObtenerURL_TDRDAA(Convert.ToInt64(Request.QueryString["IDProcessInstance"]));

                    if (_esConflicto)
                    {
                        lblMensaje.Text = "Puede dar clic en el enlace para descargar los TDR para DAA. \n Se ha detectado que su Proyecto, Obra o Actividad tiene jurisdicción en varias Autoridades Ambientales, por tal razón, Si quiere solicitar el pronunciamiento sobre la necesidad de DAA a la Autoridad Ambiental o si quiere solicitar los TDR para EIA usted puede:";
                        optAutoridadAmbiental.Visible = true;
                    }
                    else
                    {
                        lblMensaje.Text = "Puede dar clic en el enlace para descargar los TDR para DAA. \n Si quiere solicitar el pronunciamiento sobre la necesidad de DAA a la Autoridad Ambiental de clic en el botón Solicitar DAA. \n Si quiere solicitar los TDR para EIA de clic en el botón Solicitar TDR para EIA";
                        optAutoridadAmbiental.Visible = false;

                    }
                    //hplEnlaceDescarga.Text = _objConfiguracion.FileTraffic + "TDR_EIA.docx";
                    //hplEnlaceDescarga.NavigateUrl = _objConfiguracion.FileTraffic + "TDR_EIA.docx";
                    //opción no solicitud daa Botones(INICIA CU-EIA-01) op=1 (se muestra enlace de descarga de tdr y botón para continuar con la solicitud EIA
                    lblMensaje.Visible = true;
                    btnSolicitarTDREIA.Visible = true;
                    break;
                case (int)EstadoProcesoDAA.Conflicto_Requiere_DAA: //E=10  c1
                    //conflicto op=10, se determina si es del MAVDT o puede escojer AA
                    lblMensaje.Text = "Se ha detectado que su Proyecto, Obra o Actividad tiene jurisdicción en varias autoridades ambientales, por tal razón debe seleccionar la Autoridad Ambiental a la cual desea enviar su solicitud:";
                    lblMensaje.Visible = true;
                    //MIRM
                    optAutoridadAmbiental.Enabled = true;
                    optAutoridadAmbiental.Visible = false;
                    optAutoridadAmbiental.SelectedItem.Value = "2";
                    cboAutoridadAmbiental.Visible = true;
                    
                    btnAceptar.Visible = true;
                    btnCancelar.Visible = true;
                    break;
                    //*
                case (int)EstadoProcesoDAA.Conflicto_EIA: //E=11  c4
                    lblEnlaceDescarga.Text = "Su proyecto, obra o actividad no requiere pronunciamiento sobre la necesidad de Diagnóstico Ambiental de Alternativas, para solicitar los Términos de Referencia para el Estudio de Impacto Ambiental de clic sobre el botón Solicitar TDR para EIA";
                    lblMensaje.Text = "\n Se ha detectado que su Proyecto, Obra o Actividad tiene jurisdicción en varias autoridades ambientales, por tal razón debe seleccionar la Autoridad Ambiental a la cual desea enviar su solicitud:";
                    lblMensaje.Visible = true;
                    lblEnlaceDescarga.Visible = true;
                    //MIRM
                    optAutoridadAmbiental.Visible = false;
                    optAutoridadAmbiental.SelectedItem.Value = "2"; 
                    cboAutoridadAmbiental.Visible = true;
                    btnSolicitarTDREIA.Visible = true;
                    break;
                    //*
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico,this.AppRelativeVirtualPath.ToString()+" ---- Error:  "+ ex.ToString());
            Mensaje.MostrarMensaje(this.Page,"Ocurrio un  error comuniquese con el administrador.");
        }        
    }

    protected void btnSolicitarTDREIA_Click(object sender, EventArgs e)
    {
        try
        {
           
            Proceso _objProceso = new Proceso();
            SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();
            _servicioBPM.Url = SILPA.Comun.DireccionamientoWS.UrlWS("GattacaBpm");
            SolicitudFachada _solicitudFachada = new SolicitudFachada();
            int _idTipoTramite = _solicitudFachada.ObtenerTramiteEIA();
                        
            AutoridadAmbiental _aa = new AutoridadAmbiental();
            int _intAutoridadAmbiental = _aa.ObtenerIdAutoridadAmbientalMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);
            int _idRadicacion = 0;

            if ((int)Session["Estado"] == (int)EstadoProcesoDAA.Enviar_TDR_DAA) //E=1
            {
                bool _esConflicto = _solicitudFachada.EsConflicto(Convert.ToInt64(Request.QueryString["IDProcessInstance"]));
                if (_esConflicto)
                {
                    if (optAutoridadAmbiental.SelectedItem.Value == "1")
                    {                        
                        _intAutoridadAmbiental = _aa.ObtenerIdAutoridadAmbientalMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);
                    }
                    else if (optAutoridadAmbiental.SelectedItem.Value == "2")
                        _intAutoridadAmbiental = Convert.ToInt32(cboAutoridadAmbiental.SelectedItem.Value);
                }
                else                                    
                    _intAutoridadAmbiental = _solicitudFachada.ObtenerAASolicitud(Int64.Parse(Request.QueryString["IDProcessInstance"]));
                

                if (_idTipoTramite != -1)
                    _solicitudFachada.ActualizarTipoTramite((long)Session["IDProcessInstance"], _idTipoTramite);
                
                
                _idRadicacion = _solicitudFachada.RadicarSolicitud(Int64.Parse(Request.QueryString["IDProcessInstance"]), _intAutoridadAmbiental, Convert.ToInt64(Session["IdRelated"]));

                _solicitudFachada.ActualizarAASolicitud((long)Session["IDProcessInstance"], _intAutoridadAmbiental, Convert.ToInt64(_idRadicacion));
                
                _objProceso.EstablecerCondicion(Convert.ToInt32(Session["IDActivityInstance"]), "Radicacion");
                _servicioBPM.EndActivityInstance("Softmanagement", Convert.ToInt64(Session["IdRelated"]), (long)Session["IDActivityInstance"], (long)Session["IDProcessInstance"], _objProceso.IdCondicion, "", "", (string)Session["EntryData"], "0", (string)Session["IDEntryData"]);
            }
            else if ((int)Session["Estado"] == (int)EstadoProcesoDAA.Enviar_EIA) //E=12
            {
                _intAutoridadAmbiental = _solicitudFachada.ObtenerAASolicitud(Int64.Parse(Request.QueryString["IDProcessInstance"]));
                if (_idTipoTramite != -1)
                    _solicitudFachada.ActualizarTipoTramite((long)Session["IDProcessInstance"], _idTipoTramite);
                
                _idRadicacion = _solicitudFachada.RadicarSolicitud(Int64.Parse(Request.QueryString["IDProcessInstance"]), _intAutoridadAmbiental, Convert.ToInt64(Session["IdRelated"]));
                _solicitudFachada.ActualizarAASolicitud((long)Session["IDProcessInstance"], _intAutoridadAmbiental, Convert.ToInt64(_idRadicacion));
                _objProceso.EstablecerCondicion(Convert.ToInt32(Session["IDActivityInstance"]), "Radicacion");
                _servicioBPM.EndActivityInstance("Softmanagement", Convert.ToInt64(Session["IdRelated"]), (long)Session["IDActivityInstance"], (long)Session["IDProcessInstance"], _objProceso.IdCondicion, "", "", (string)Session["EntryData"], "0", (string)Session["IDEntryData"]);                
            }
            else if ((int)Session["Estado"] == (int)EstadoProcesoDAA.Conflicto_EIA) //E=11  - Conflicto de Competencias sin DAA
            {            
                if (optAutoridadAmbiental.SelectedItem.Value == "1")                             
                    _intAutoridadAmbiental = _aa.ObtenerIdAutoridadAmbientalMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);                
                else if (optAutoridadAmbiental.SelectedItem.Value == "2")
                    _intAutoridadAmbiental = Convert.ToInt32(cboAutoridadAmbiental.SelectedItem.Value);
                
                if (_idTipoTramite != -1)
                    _solicitudFachada.ActualizarTipoTramite((long)Session["IDProcessInstance"], _idTipoTramite);
                
                _idRadicacion = _solicitudFachada.RadicarSolicitud((long)Session["IDProcessInstance"], _intAutoridadAmbiental, Convert.ToInt64(Session["IdRelated"]));                
                _solicitudFachada.ActualizarAASolicitud((long)Session["IDProcessInstance"], _intAutoridadAmbiental, Convert.ToInt64(_idRadicacion));
                _objProceso.EstablecerCondicion(Convert.ToInt32(Session["IDActivityInstance"]), "Radicacion");                
                _servicioBPM.EndActivityInstance("Softmanagement", Convert.ToInt64(Session["IdRelated"]), (long)Session["IDActivityInstance"], (long)Session["IDProcessInstance"], _objProceso.IdCondicion, "", "", (string)Session["EntryData"], "0", (string)Session["IDEntryData"]);       
            }

            _solicitudFachada.ActualizarEstadoSolicitud((long)Session["IDProcessInstance"], (int)EstadoProcesoDAA.Pendiente_Radicacion);
            ActualizarEstadoRadicadoLeido(_idRadicacion);            
            btnSolicitarTDREIA.Visible = false;
            optAutoridadAmbiental.Visible = false;
            cboAutoridadAmbiental.Visible = false;
            btnSolicitarDAA.Visible = false;

            RadicacionDocumento objRadicacion = new RadicacionDocumento();
            objRadicacion.ObtenerRadicacion(null, (int.Parse(Request.QueryString["IDProcessInstance"].ToString())));
            SILPA.Servicios.ImpresionFUS.ImpresionFUSFachada.GenerarFus(objRadicacion._objRadDocIdentity.Id, int.Parse(Request.QueryString["IDProcessInstance"].ToString()), objRadicacion._objRadDocIdentity.UbicacionDocumento, objRadicacion._objRadDocIdentity.NumeroVITALCompleto, objRadicacion._objRadDocIdentity.IdSolicitante);
            
            Mensaje.MostrarMensaje(this, "Solicitud Enviada con éxito, puede cerrar está ventana ahora", true);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.AppRelativeVirtualPath.ToString() + " ---- Error:  " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un  error comuniquese con el administrador.");
        }        
    }

    protected void btnSolicitarDAA_Click(object sender, EventArgs e)
    {
        try
        {
          
            Proceso _objProceso = new Proceso();
            SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();
            _servicioBPM.Url = SILPA.Comun.DireccionamientoWS.UrlWS("GattacaBpm");
            SolicitudFachada _solicitudFachada = new SolicitudFachada();
            //int _intAutoridadAmbiental = (int)AutoridadesAmbientales.MAVDT;
            AutoridadAmbiental _aa = new AutoridadAmbiental();
            int _intAutoridadAmbiental = _aa.ObtenerIdAutoridadAmbientalMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);
            int _idRadicacion = 0;

            if ((int)Session["Estado"] == (int)EstadoProcesoDAA.Enviar_TDR_DAA) //E=1
            {

                bool _esConflicto = _solicitudFachada.EsConflicto(Convert.ToInt64(Request.QueryString["IDProcessInstance"]));
                if (_esConflicto)
                {
                    if (optAutoridadAmbiental.SelectedItem.Value == "1")
                    {
                        //_intAutoridadAmbiental = (int)AutoridadesAmbientales.MAVDT;
                        _intAutoridadAmbiental = _aa.ObtenerIdAutoridadAmbientalMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);
                    }
                    else if (optAutoridadAmbiental.SelectedItem.Value == "2")
                        _intAutoridadAmbiental = Convert.ToInt32(cboAutoridadAmbiental.SelectedItem.Value);

                }
                else
                {
                    //Selecciona Condición Manual VITALCONPRO01-19 y finaliza actividad VITALACTPRO01-017 (AVANZA A VITALACTPRO01-014)
                    _intAutoridadAmbiental = _solicitudFachada.ObtenerAASolicitud(Int64.Parse(Request.QueryString["IDProcessInstance"]));

                    //Page.RegisterStartupScript("","<script type="="javascript:window.close()">");
                }

                //Se publica Radicación
                _idRadicacion = _solicitudFachada.RadicarSolicitud(Int64.Parse(Request.QueryString["IDProcessInstance"]), _intAutoridadAmbiental, Convert.ToInt64(Session["IdRelated"]));

                _solicitudFachada.ActualizarAASolicitud((long)Session["IDProcessInstance"], _intAutoridadAmbiental, Convert.ToInt64(_idRadicacion));
                //Revisar Datos Estáticos:
                //Client = SoftManagement
                //UserID = 1 -> debe traer el usuario autenticado!
                //SelectedContidion = 326 o VITALACTPRO01-017?
                //EntryDAtaType = WebForm
                //Finalizar Actividad:

                _objProceso.EstablecerCondicion(Convert.ToInt32(Session["IDActivityInstance"]), "Radicacion");

                //_servicioBPM.EndActivityInstance("Softmanagement", Convert.ToInt64(Session["IdRelated"]), (long)Session["IDActivityInstance"], (long)Session["IDProcessInstance"],_objProceso.IdCondicion , "", "", (string)Session["EntryData"], "0", (string)Session["IDEntryData"]);

                /// Finalizar con la condición 6
                ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                string mensaje = servicioWorkflow.ValidarActividadActual((long)Session["IDProcessInstance"], DatosSesion.Usuario, (long)ActividadSilpa.ValidarRequiereDAA);
                if (mensaje=="")
                    servicioWorkflow.FinalizarTarea((long)Session["IDProcessInstance"], ActividadSilpa.ValidarRequiereDAA, DatosSesion.Usuario, CondicionBtnSolicitarDaa);
                else
                    SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: Competencias::btnSolicitarDAA_Click - Error: " + mensaje, "BPM_VAL_CON");

            }
            _solicitudFachada.ActualizarEstadoSolicitud((long)Session["IDProcessInstance"], (int)EstadoProcesoDAA.Pendiente_Radicacion);
            ActualizarEstadoRadicadoLeido(_idRadicacion);
            btnSolicitarTDREIA.Visible = false;

            RadicacionDocumento objRadicacion = new RadicacionDocumento();
            objRadicacion.ObtenerRadicacion(null, (int.Parse(Request.QueryString["IDProcessInstance"].ToString())));
            SILPA.Servicios.ImpresionFUS.ImpresionFUSFachada.GenerarFus(objRadicacion._objRadDocIdentity.Id, int.Parse(Request.QueryString["IDProcessInstance"].ToString()), objRadicacion._objRadDocIdentity.UbicacionDocumento, objRadicacion._objRadDocIdentity.NumeroVITALCompleto, objRadicacion._objRadDocIdentity.IdSolicitante);

            optAutoridadAmbiental.Visible = false;
            cboAutoridadAmbiental.Visible = false;
            btnSolicitarDAA.Visible = false;

            Mensaje.MostrarMensaje(this, "Solicitud Enviada con éxito, puede cerrar está ventana ahora", true);

        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.AppRelativeVirtualPath.ToString() + " ---- Error:  " + ex.ToString());
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un  error comuniquese con el administrador.");
        }        

    }

    protected void cboAutoridadAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
