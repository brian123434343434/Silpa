using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SoftManagement.Log;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Notificacion;
using System.Data;
using SILPA.AccesoDatos.Notificacion;
using System.Configuration;
using SILPA.Comun;
using SILPA.Servicios;
using SILPA.LogicaNegocio.Excepciones;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.AdmTablasBasicas;

public partial class NotificacionElectronica_ConsultaNotificaciones : System.Web.UI.Page
{

    #region Propiedades

        /// <summary>
        /// Identificador del solicitante
        /// </summary>
        private int SolicitanteID
        {
            get
            {
                return (int)ViewState["_intSolicitanteID"];
            }
            set
            {
                ViewState["_intSolicitanteID"] = value;
            }
        }


        /// <summary>
        /// Contiene la información de configuración de estados
        /// </summary>
        private EstadoFlujoNotificacionEntity _objConfiguracionEstadoFlujo
        {
            get
            {
                return (EstadoFlujoNotificacionEntity)ViewState["_objConfiguracionEstadoFlujo"];
            }
            set
            {
                ViewState["_objConfiguracionEstadoFlujo"] = value;
            }
        }


        /// <summary>
        /// Contiene la información de direcciones de un usuario
        /// </summary>
        private Object _Direcciones
        {
            get
            {
                return (Object)ViewState["_objDirecciones"];
            }
            set
            {
                ViewState["_objDirecciones"] = value;
            }
        }

        /// <summary>
        /// Contiene la información de direcciones de un usuario
        /// </summary>
        private Object _Correos
        {
            get
            {
                return (Object)ViewState["_objCorreos"];
            }
            set
            {
                ViewState["_objCorreos"] = value;
            }
        }


    #endregion


    #region Metodos Privados

        /// <summary>
        /// Limpiar el fileupload indicado
        /// </summary>
        /// <param name="p_objControl">Control con el fileupload a limpiar</param>
        private void LimpiarFileUpload(Control p_objControl)
        {
            int intCont = 0;
            bool blnLimpio = false;

            //Ciclo que busca datos en sesion y limpia el contenido
            for (intCont = 0; intCont < Session.Keys.Count && !blnLimpio; intCont++)
            {
                if (Session.Keys[intCont].Contains(p_objControl.ClientID))
                {
                    Session.Remove(Session.Keys[intCont]);
                    blnLimpio = true;
                }
            }
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

            this.SolicitanteID = Convert.ToInt32(Session["IDForToken"]);

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
            this.divMensaje.Visible = true;
            this.upnlMensaje.Update();
        }


        /// <summary>
        /// Ocultar los mensajes
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.divMensaje.Visible = false;
            this.upnlMensaje.Update();
        }


        /// <summary>
        /// Obtiene los tipos de actos administrativos y los carga en el desplegable
        /// </summary>
        private void ConsultaTiposActos()
        {
            TipoDocumentoDalc objipoDocumentoDalc = null;

            try
            {
                //Cargar la información de los tipos documentales
                objipoDocumentoDalc = new TipoDocumentoDalc();
                this.cboTipoActo.DataSource = objipoDocumentoDalc.ListarTiposDeDocumentoNotificacion(null, null);
                this.cboTipoActo.DataTextField = "NOMBRE_DOCUMENTO";
                this.cboTipoActo.DataValueField = "ID";
                this.cboTipoActo.DataBind();
                this.cboTipoActo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: ConsultaTiposActos -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de tipos de actos");
            }
        }


        /// <summary>
        /// Cargar el listado de tipos de identificación en el desplegable indicado
        /// </summary>
        private void ConsultaTiposIdentificacion(DropDownList p_objListado)
        {
            NOT_TipoIdentificacion objTiposIdentificacion = null;

            try
            {
                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar la información
                objTiposIdentificacion = new NOT_TipoIdentificacion();
                p_objListado.DataSource = objTiposIdentificacion.Listar_Tipo_Documento("");
                p_objListado.DataTextField = "NTI_DESCRIPCION";
                p_objListado.DataValueField = "ID_TIPO_IDENTIFICACION";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: ConsultaTiposIdentificacion -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de tipos de identificacion");
            }
        }


        /// <summary>
        /// Consulta los flujos de notificación relacionados al usuario que ingresa
        /// </summary>
        private void ConsultaFlujosNotificacion()
        {
            SILPA.LogicaNegocio.AdmTablasBasicas.FlujoNotificacionElectronica objFlujoNotificacionElectronica = null;

            try
            {
                //Cargar la información de notificaciones
                objFlujoNotificacionElectronica = new SILPA.LogicaNegocio.AdmTablasBasicas.FlujoNotificacionElectronica();
                this.cboFlujoNotificacion.DataSource = objFlujoNotificacionElectronica.ConsultaFlujosNotificacionUsuario(this.SolicitanteID);
                this.cboFlujoNotificacion.DataTextField = "FLUJO_NOT_ELEC_DESC";
                this.cboFlujoNotificacion.DataValueField = "ID_FLUJO_NOT_ELEC";
                this.cboFlujoNotificacion.DataBind();
                this.cboFlujoNotificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "ConsultaFlujosNotificacion :: ConsultaEstadosNotificacion -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de flujos de notificación");
            }
        }


        /// <summary>
        /// Obtiene los estados de notificación
        /// </summary>
        /// <param name="p_intFlujoNotificacion">int con el identificador del flujo de notificacion</param>
        private void ConsultaEstadosNotificacion(int p_intFlujoNotificacion)
        {
            SILPA.LogicaNegocio.Notificacion.EstadoNotificacion objEstadoNotificacion = null;
            List<EstadoNotificacionEntity> objListadoNotificaciones = null;
            int intCont = 0;

            try
            {
                this.cboEstadoNotificacion.ClearSelection();
                this.cboEstadoNotificacion.Items.Clear();

                //consultar notificaciones
                objEstadoNotificacion = new SILPA.LogicaNegocio.Notificacion.EstadoNotificacion();
                objListadoNotificaciones = objEstadoNotificacion.ListarEstadosNotificacion(this.SolicitanteID, p_intFlujoNotificacion);

                if (objListadoNotificaciones != null){

                    if (p_intFlujoNotificacion > 1)
                    {
                        //Cargar la información de notificaciones
                        this.cboEstadoNotificacion.DataSource = objListadoNotificaciones;
                        this.cboEstadoNotificacion.DataTextField = "Descripcion";
                        this.cboEstadoNotificacion.DataValueField = "ID";
                        this.cboEstadoNotificacion.DataBind();
                        this.cboEstadoNotificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }
                    else
                    {
                        //Cargar opciones
                        foreach (EstadoNotificacionEntity objEstado in objListadoNotificaciones)
                        {
                            this.cboEstadoNotificacion.Items.Add(new ListItem(objEstado.Descripcion, objEstado.ID + "-" + intCont.ToString()));
                            intCont++;
                        }
                        this.cboEstadoNotificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }
                }
                else
                {
                    this.cboEstadoNotificacion.Items.Add(new ListItem("Seleccione...", "-1"));
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: ConsultaEstadosNotificacion -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de estados de notificación");
            }
        }


        /// <summary>
        /// Precargar información en caso de que se especifique la persona
        /// </summary>
        private void PreCargaInformacion()
        {
            Notificacion objNotificacion = null;
            DataSet objInformacionPersonaActo = null;
            string strActoID = "";
            string strPersonaID = "";

            //Cargar querystring
            strActoID = Request.QueryString["Act"];
            strPersonaID = Request.QueryString["Per"];

            //Si se especifico id precargar datos
            if (!string.IsNullOrWhiteSpace(strActoID) && !string.IsNullOrWhiteSpace(strPersonaID))
            {
                //Consultar información de persona y actoa administrativo
                try
                {
                    //Consultar información 
                    objNotificacion = new Notificacion();
                    objInformacionPersonaActo = objNotificacion.ObtenerEstadoActoPersona(Convert.ToInt64(strActoID), Convert.ToInt64(strPersonaID));

                    //Validar que se obtenga datos
                    if (objInformacionPersonaActo != null && objInformacionPersonaActo.Tables.Count > 0 && objInformacionPersonaActo.Tables[0].Rows.Count > 0)
                    {
                        //Cargar datos
                        this.txtNumeroVital.Text = objInformacionPersonaActo.Tables[0].Rows[0]["NUM_VITAL"].ToString();
                        this.txtExpediente.Text = objInformacionPersonaActo.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
                        this.txtIdentificacionUsuario.Text = objInformacionPersonaActo.Tables[0].Rows[0]["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString();
                        this.txtNumeroActo.Text = objInformacionPersonaActo.Tables[0].Rows[0]["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString();
                        this.txtFechaDesde.Text = Convert.ToDateTime(objInformacionPersonaActo.Tables[0].Rows[0]["NOT_FECHA_ACTO"]).ToString("dd/MM/yyyy");
                        this.txtFechaHasta.Text = Convert.ToDateTime(objInformacionPersonaActo.Tables[0].Rows[0]["NOT_FECHA_ACTO"]).ToString("dd/MM/yyyy");

                        //Realizar busqueda
                        this.cmdBuscar_Click(null, null);
                    }
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: PreCargaInformacion -> Error Inesperado: " + exc.Message);
                }
            }

        }


        /// <summary>
        /// Cargar la información inicial de la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Cargar información listados
            this.ConsultaTiposActos();
            this.ConsultaEstadosNotificacion(-1);
            this.ConsultaFlujosNotificacion();

            //Inicializar fechas
            this.txtFechaDesde.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");

            //Precargar información
            this.PreCargaInformacion();

        }


        /// <summary>
        /// Carga la información de actos administrativos de acuerdo a los parametros de busqueda
        /// </summary>
        private void BuscarInformacionActosDministrativos()
        {
            Notificacion objNotificacion = null;
            DataSet objNotificaciones = null;
            DataRow[] objNotificacionesFiltradas = null;
            int intEstadoID = -1;
            int? intDiasVencimientoDesde = null;
            int? intDiasVencimientoHasta = null;

            try
            {
                //Cargar el identificador del estado
                if (!this.hdfEstadoNotificacion.Value.Contains("-") || this.hdfEstadoNotificacion.Value == "-1")
                {
                    intEstadoID = (!string.IsNullOrWhiteSpace(this.hdfEstadoNotificacion.Value) ? Convert.ToInt32(this.hdfEstadoNotificacion.Value.Trim()) : -1);
                }
                else{
                    intEstadoID = Convert.ToInt32( this.hdfEstadoNotificacion.Value.Split('-')[0] );
                }

                //Cargar días de vencimiento
                if(!string.IsNullOrWhiteSpace(this.hdfDiasVencimientoInicial.Value))
                    intDiasVencimientoDesde = Convert.ToInt32(this.hdfDiasVencimientoInicial.Value.Trim());
                if(!string.IsNullOrWhiteSpace(this.hdfDiasVencimientoFinal.Value))
                    intDiasVencimientoHasta = Convert.ToInt32(this.hdfDiasVencimientoFinal.Value.Trim());

                //Realizar Consulta
                objNotificacion = new Notificacion();
                objNotificaciones = objNotificacion.ObtenerListadoActosAdministrativosNotificacion(this.hdfNumeroVital.Value.Trim(), this.hdfExpediente.Value.Trim(),
                                                                                                    this.hdfIdentificacionUsuario.Value.Trim(), this.hdfUsuarioNotificar.Value.Trim(),
                                                                                                    this.hdfNumeroActo.Value.Trim(), (!string.IsNullOrWhiteSpace(this.hdfTipoActo.Value) ? Convert.ToInt32(this.hdfTipoActo.Value.Trim()) : -1),
                                                                                                    intDiasVencimientoDesde, intDiasVencimientoHasta,
                                                                                                    -1, "",
                                                                                                    (!string.IsNullOrWhiteSpace(this.hdfFlujoNotificacion.Value) ? Convert.ToInt32(this.hdfFlujoNotificacion.Value.Trim()) : -1),
                                                                                                    intEstadoID, (this.hdfEstadoActual.Value == "1" ? true : false),
                                                                                                    Convert.ToDateTime(this.hdfFechaDesde.Value.Trim()), Convert.ToDateTime(this.hdfFechaHasta.Value.Trim()),
                                                                                                    this.SolicitanteID);

                if (!string.IsNullOrWhiteSpace(this.hdfFlujoNotificacion.Value) && this.hdfFlujoNotificacion.Value != "-1")
                    this.grdNotificaciones.DataSource = objNotificaciones;
                else
                {
                    if (string.IsNullOrEmpty(this.hdfEstadoNotificacion.Value) || this.hdfEstadoNotificacion.Value == "-1")
                        this.grdNotificaciones.DataSource = objNotificaciones;
                    else{
                        objNotificacionesFiltradas = objNotificaciones.Tables[0].Select("ESTADO_ACTUAL = '" + this.hdfEstadoNotificacionDescripcion.Value.Trim() + "'");

                        if (objNotificacionesFiltradas != null && objNotificacionesFiltradas.Length > 0)
                            this.grdNotificaciones.DataSource = objNotificacionesFiltradas.CopyToDataTable();
                        else
                            this.grdNotificaciones.DataSource = new DataTable();
                    }
                }

                this.grdNotificaciones.DataBind();

                //Cargar triggers
                this.CargarTriggersGrillaNotificaciones();

                //Actualizar panel
                this.upnlConsultaNotificaciones.Update();

            }
            catch(Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: BuscarInformacionActosDministrativos -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error realizando la busqueda de la información de actos administrativos a notificar");
            }
        }


        /// <summary>
        /// Carga la información de actos administrativos de acuerdo a los parametros de busqueda
        /// </summary>
        private void BuscarInformacionEstadosActoPersona(GridView objGrilla, long p_lngActoID, long p_lngPersonaID)
        {
            Notificacion objNotificacion = null;

            try
            {
                //Realizar Consulta
                objNotificacion = new Notificacion();
                objGrilla.DataSource = objNotificacion.ObtenerListadoEstadosActoPersona(p_lngActoID, p_lngPersonaID);
                objGrilla.DataBind();
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: BuscarInformacionEstadosActoPersona -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error realizando la busqueda de la información de los estados");
            }
        }

        
        /// <summary>
        /// Cargar los trigger especificos que se requieren de la grilla de notificaciones que no se pueden especificar en diseño de pagina
        /// </summary>
        private void CargarTriggersGrillaNotificaciones()
        {
            ImageButton objImagen = null;
            PostBackTrigger objPostTrigger = null;
            GridView objGrilla = null;

            //Verificar que la grilla contenga información
            if (this.grdNotificaciones.Visible && this.grdNotificaciones.Rows.Count > 0)
            {
                //Ciclo que adiciona los triggers de la grilla de notificaciones
                foreach (GridViewRow objRowNotificacion in this.grdNotificaciones.Rows)
                {
                    //Cargar imagen de descarga
                    objImagen = (ImageButton)objRowNotificacion.FindControl("imgDescargarDocumento");

                    //Si existe el control y se encuentra visible
                    if (objImagen != null && objImagen.Visible)
                    {
                        objPostTrigger = new PostBackTrigger();
                        objPostTrigger.ControlID = objImagen.UniqueID;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                    }

                    //Obtener grilla de estados 
                    objGrilla = (GridView)objRowNotificacion.FindControl("grdEstadosNotificacion");

                    //Verificar grilla
                    if (objGrilla != null && objGrilla.Visible && objGrilla.Rows.Count > 0)
                    {
                        //Ciclo que adiciona los triggers de la grilla de estados
                        foreach (GridViewRow objRowEstado in objGrilla.Rows)
                        {
                            //Cargar imagen de plantilla
                            objImagen = (ImageButton)objRowEstado.FindControl("imgDocumentoPlantilla");

                            //Si existe el control y se encuentra visible
                            if (objImagen != null && objImagen.Visible)
                            {
                                objPostTrigger = new PostBackTrigger();
                                objPostTrigger.ControlID = objImagen.UniqueID;
                                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                            }

                            //Cargar imagen de documento adicional
                            objImagen = (ImageButton)objRowEstado.FindControl("imgDocumentoAdicional");

                            //Si existe el control y se encuentra visible
                            if (objImagen != null && objImagen.Visible)
                            {
                                objPostTrigger = new PostBackTrigger();
                                objPostTrigger.ControlID = objImagen.UniqueID;
                                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                            }
                        }
                    }
                }
            }

        }


        /// <summary>
        /// Cargar el listado de estados futuros
        /// </summary>
        /// <param name="p_lngActoID">long con el identificador del acto</param>
        /// <param name="p_intEstadoActualID">long con el identificador de la persona</param>
        /// <param name="p_intFlujoID">int con el identificador del flujo</param>
        private void CargarEstadosFuturos(long p_lngActoID, long p_lngPersonaID, int p_intEstadoActualID, int p_intFlujoID)
        {
            List<EstadoFlujoEntity> objListaEstadosFlujo = null;
            List<EstadoFlujoEntity> objListaEstadosPosibles = null;
            EstadoNotificacionDalc objEstadoNotificacionDalc = null;
            Notificacion objNotificacion = null;
            int intDiasDiferencia = 0;
            bool blnPendientesNotificacion = false;
            bool blnPendientes = false;

            //Consultar posibles estados
            objEstadoNotificacionDalc = new EstadoNotificacionDalc();
            objListaEstadosFlujo = objEstadoNotificacionDalc.ListaSiguienteEstadoAdm(p_intEstadoActualID, true, false, p_intFlujoID);

            //Calcular los dias que lleva la solicitud en el estado actual
            objNotificacion = new Notificacion();
            intDiasDiferencia = objNotificacion.ObtenerNumeroDiasNotificacion(p_lngPersonaID.ToString(), p_lngActoID.ToString());

            //Validar existen pendientes en el acto
            blnPendientesNotificacion = objNotificacion.ExistePendientesFinalizarNotificacionActo(p_lngActoID);
            blnPendientes = objNotificacion.ExistePendientesFinalizarActo(p_lngActoID);

            //Cargar los estados posibles de acuedo a los días de diferencia
            objListaEstadosPosibles = new List<EstadoFlujoEntity>();
            foreach (EstadoFlujoEntity objEstado in objListaEstadosFlujo)
            {
                if (objEstado.EstadoEjecutoria)
                {
                    if (!blnPendientesNotificacion)
                    {
                        if (objEstado.NroDiasTransicion >= intDiasDiferencia || objEstado.NroDiasTransicion <= 0)
                            objListaEstadosPosibles.Add(objEstado);
                    }
                }
                else if (objEstado.EstadoFinalPublicidad)
                {
                    if (!blnPendientes)
                    {
                        if (objEstado.NroDiasTransicion >= intDiasDiferencia || objEstado.NroDiasTransicion <= 0)
                            objListaEstadosPosibles.Add(objEstado);
                    }
                }
                else if (objEstado.NroDiasTransicion >= intDiasDiferencia || objEstado.NroDiasTransicion <= 0)
                    objListaEstadosPosibles.Add(objEstado);
            }
            
            //Cargar el listado de estados posibles
            objListaEstadosPosibles = objListaEstadosPosibles.OrderBy(item => item.NombreEstado).ToList();
            this.cboEstado.DataSource = objListaEstadosPosibles;
            this.cboEstado.DataTextField = "NombreEstado";
            this.cboEstado.DataValueField = "EstadoID";
            this.cboEstado.DataBind();
            this.cboEstado.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }


        /// <summary>
        /// Limpia la información del modal de avanzar
        /// </summary>
        /// <param name="p_blnLimpiarDatosNotificacion">bool que indica si se elmina la información de la notificación</param>
        private void LimpiarModalAvanzar(bool p_blnLimpiarDatosNotificacion = true)
        {
            if (p_blnLimpiarDatosNotificacion)
            {
                //Limpiar campos controles
                this.ltlNumeroVital.Text = "";
                this.ltlNumeroExpediente.Text = "";
                this.ltlActoAdministrativo.Text = "";
                this.ltlNumeroActo.Text = "";
                this.ltlIdentificacion.Text = "";
                this.ltlUsuario.Text = "";
                this.ltlEstadoActual.Text = "";
                this.cboEstado.ClearSelection();
                this.cboEstado.Items.Clear();
                this.txtObservacion.Text = "";
                this.hdfActoID.Value = "";
                this.hdfPersonaIdentificacion.Value = "";
                this.hdfPersonaID.Value = "";
                this.hdfCodigoExpedienteActo.Value = "";
                this.hdfNumeroVitalActo.Value = "";
                this.hdfAutoridadAmbiental.Value = "";
                this.hdfEstadoActualID.Value = "";
                this.hdfFlujoID.Value = "";
            }

            this._objConfiguracionEstadoFlujo = null;
            this.LimpiarFileUpload(this.fuplDocumentoAdicional);
            this.chkEnviarDireccion.Checked = false;
            this.chkEnviarDireccion.Enabled = true;
            this.chkEnviarCorreo.Checked = false;
            this.chkEnviarCorreo.Enabled = true;
            this.LimpiarFileUpload(this.fuplAdjunto);
            this.txtTextoCorreo.Text = "";
            this.txtReferenciaRecepcion.Text = "";
            this.txtFechaRecepcionDocumento.Text = "";
            this._Direcciones = null;
            this.grdDirecciones.DataSource = null;
            this.grdDirecciones.DataBind();
            this._Correos = null;
            this.grdCorreos.DataSource = null;
            this.grdCorreos.DataBind();
            if (this.rptDatosUsuariosAvanzar.Items.Count > 0)
            {
                foreach (RepeaterItem objItem in this.rptDatosUsuariosAvanzar.Items)
                {
                    this.LimpiarFileUpload((AjaxControlToolkit.AsyncFileUpload)objItem.FindControl("fuplRptAdjunto"));
                }
            }
            this.rptDatosUsuariosAvanzar.Visible = false;
            this.rptDatosUsuariosAvanzar.DataSource = null;
            this.rptDatosUsuariosAvanzar.DataBind();
            this.cboFirma.ClearSelection();
            this.cboFirma.Items.Clear();
            this.rfvReferenciaRecepcion.Enabled = false;
            this.rfvFechaRecepcionDocumento.Enabled = false;
            this.chkAdjuntarActoAdministrativo.Checked = false;
            this.dvAdjuntarConceptosActoAdministrativo.Visible = false;
            this.chkAdjuntarConceptosActoAdministrativo.Checked = false;
            this.cboTipoIdentificacionPersonaNotificar.ClearSelection();
            this.cboTipoIdentificacionPersonaNotificar.Items.Clear();
            this.txtNumeroIdentificacionPersonaNotificar.Text = "";
            this.txtNombrePersonaNotificar.Text = "";
            this.txtCalidadPersonaNotificar.Text = "";

            //Ocultar div
            this.dvDocumentoAdicional.Visible = false;
            this.dvEnviarDireccion.Visible = false;
            this.dvEnviarCorreo.Visible = false;
            this.dvAdjuntos.Visible = false;
            this.dvAdjuntarActoAdministrativo.Visible = false;
            this.dvTextoCorreo.Visible = false;
            this.dvReferenciaRecepcion.Visible = false;
            this.dvFechaRecepcion.Visible = false;
            this.dvListadoDirecciones.Visible = false;
            this.dvListaCorreos.Visible = false;
            this.dvFirmasDocumento.Visible = false;
            this.dvTipoIdentificacionPersonaNotificar.Visible = false;
            this.dvNumeroIdentificacionPersonaNotificar.Visible = false;
            this.dvNombrePersonaNotificar.Visible = false;
            this.dvCalidadPersonaNotificar.Visible = false;
        }


        /// <summary>
        /// Limpia la información del modal de editar
        /// </summary>
        private void LimpiarModalEditar()
        {
            
            //Limpiar campos controles
            this.ltlNumeroVitalEditar.Text = "";
            this.ltlNumeroExpedienteEditar.Text = "";
            this.ltlActoAdministrativoEditar.Text = "";
            this.ltlNumeroActoEditar.Text = "";
            this.ltlIdentificacionEditar.Text = "";
            this.ltlUsuarioEditar.Text = "";
            this.ltlEstadoEditar.Text = "";
            this.txtObservacionEditar.Text = "";
            this.hdfEstadoPersonaActoID.Value = "";
            this.hdfAutoridadAmbientalEditar.Value = "";
            this.hdfPersonaIDEditar.Value = "";
            this.hdfEstadoActualIDEditar.Value = "";
            this.hdfPersonaIdentificacionEditar.Value = "";
            this.hdfCodigoExpedienteActoEditar.Value = "";
            this.hdfNumeroVitalActoEditar.Value = "";
            this.hdfFechaEstadoAnterior.Value = "";

            this._objConfiguracionEstadoFlujo = null;
            this.LimpiarFileUpload(this.fuplDocumentoAdicionalEditar);
            this.chkEnviarDireccionEditar.Checked = false;
            this.chkEnviarDireccionEditar.Enabled = true;
            this.chkEnviarCorreoEditar.Checked = false;
            this.chkEnviarCorreoEditar.Enabled = true;
            this._Direcciones = null;
            this.grdDireccionesEditar.DataSource = null;
            this.grdDireccionesEditar.DataBind();
            this._Correos = null;
            this.grdCorreosEditar.DataSource = null;
            this.grdCorreosEditar.DataBind();
            this.LimpiarFileUpload(this.fuplAdjuntoEditar);
            this.txtTextoCorreoEditar.Text = "";
            this.txtReferenciaRecepcionEditar.Text = "";
            this.txtFechaRecepcionDocumentoEditar.Text = "";
            this.cboFirmaEditar.ClearSelection();
            this.cboFirmaEditar.Items.Clear();
            this.rfvReferenciaRecepcionEditar.Enabled = false;
            this.rfvFechaRecepcionDocumentoEditar.Enabled = false;
            this.chkAdjuntarActoAdministrativoEditar.Checked = false;
            this.dvAdjuntarConceptosActoAdministrativoEditar.Visible = false;
            this.chkAdjuntarConceptosActoAdministrativoEditar.Checked = false;
            this.cboTipoIdentificacionPersonaNotificarEditar.ClearSelection();
            this.cboTipoIdentificacionPersonaNotificarEditar.Items.Clear();
            this.txtNumeroIdentificacionPersonaNotificarEditar.Text = "";
            this.txtNombrePersonaNotificarEditar.Text = "";
            this.txtCalidadPersonaNotificarEditar.Text = "";

            //Ocultar div
            this.dvDocumentoAdicionalEditar.Visible = false;
            this.dvEnviarDireccionEditar.Visible = false;
            this.dvEnviarCorreoEditar.Visible = false;
            this.dvAdjuntosEditar.Visible = false;
            this.dvTextoCorreoEditar.Visible = false;
            this.dvReferenciaRecepcionEditar.Visible = false;
            this.dvFechaRecepcionEditar.Visible = false;
            this.dvListadoDireccionesEditar.Visible = false;
            this.dvListaCorreosEditar.Visible = false;
            this.dvFirmasDocumentoEditar.Visible = false;
            this.dvAdjuntarActoAdministrativoEditar.Visible = false;
            this.dvTipoIdentificacionPersonaNotificarEditar.Visible = false;
            this.dvNumeroIdentificacionPersonaNotificarEditar.Visible = false;
            this.dvNombrePersonaNotificarEditar.Visible = false;
            this.dvCalidadPersonaNotificarEditar.Visible = false;
        }


        /// <summary>
        /// Limpiar los componentes del modal de visualización de adjuntos
        /// </summary>
        private void LimpiarModalVerAdjuntos()
        {
            this.grdDocumentosAdjuntosVer.DataSource = null;
            this.grdDocumentosAdjuntosVer.DataBind();
        }

        /// <summary>
        /// Cargar la información a mostrar en el modal de avance de estado
        /// </summary>
        /// <param name="p_intAutoridadAmbiental">int con el identificador de la autoridad ambiental</param>
        /// <param name="p_lngActoID">long con el identificador del acto</param>
        /// <param name="p_lngPersonaID">long con la identificación de la persona</param>
        private void CargarDatosModalAvanzar(int p_intAutoridadAmbiental, long p_lngActoID, long p_lngPersonaID) 
        {
            Notificacion objNotificacion = null;
            DataSet objInformacionActo = null;

            //Cargar la información del acto
            objNotificacion = new Notificacion();
            objInformacionActo = objNotificacion.ObtenerEstadoActoPersona(p_lngActoID, p_lngPersonaID);
            
            //Validar que se obtenga información
            if (objInformacionActo != null && objInformacionActo.Tables != null && objInformacionActo.Tables[0].Rows.Count > 0)
            {
                //Cardar datos
                this.hdfActoID.Value = p_lngActoID.ToString();
                this.hdfPersonaID.Value = p_lngPersonaID.ToString();
                this.hdfAutoridadAmbiental.Value = p_intAutoridadAmbiental.ToString();
                this.hdfCodigoExpedienteActo.Value = objInformacionActo.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
                this.hdfNumeroVitalActo.Value = objInformacionActo.Tables[0].Rows[0]["NUM_VITAL"].ToString();
                this.hdfEstadoActualID.Value = objInformacionActo.Tables[0].Rows[0]["ID_ESTADO_ACTUAL"].ToString();
                this.hdfFechaEstadoActual.Value = Convert.ToDateTime(objInformacionActo.Tables[0].Rows[0]["NPE_FECHA_NOTIFICADO"]).ToString("dd/MM/yyyy HH:mm");
                this.hdfFlujoID.Value = objInformacionActo.Tables[0].Rows[0]["ID_FLUJO_NOT_ELEC"].ToString();
                this.hdfEsPDI.Value = (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["NPE_ESTADO_CAMBIO_PDI"].ToString()) ? "SI" : "NO");
                this.ltlNumeroVital.Text = objInformacionActo.Tables[0].Rows[0]["NUM_VITAL"].ToString();
                this.ltlNumeroExpediente.Text = objInformacionActo.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
                this.ltlActoAdministrativo.Text = objInformacionActo.Tables[0].Rows[0]["TIPO_ACTO_ADMINISTRATIVO"].ToString();
                this.ltlNumeroActo.Text = objInformacionActo.Tables[0].Rows[0]["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString();
                this.ltlIdentificacion.Text = objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString();
                this.hdfPersonaIdentificacion.Value = objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString();
                this.ltlUsuario.Text = objInformacionActo.Tables[0].Rows[0]["USUARIO_NOTIFICAR"].ToString();
                this.ltlEstadoActual.Text = objInformacionActo.Tables[0].Rows[0]["ESTADO_ACTUAL"].ToString();
                this.ltlFechaEstadoActual.Text = Convert.ToDateTime(objInformacionActo.Tables[0].Rows[0]["NPE_FECHA_NOTIFICADO"]).ToString("dd/MM/yyyy HH:mm");
                this.txtFechaEstadoNotificacion.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                this.dvIdentificacion.Visible = true;
                this.dvUsuario.Visible = true;

                //Cargar listado de estados futuros
                this.CargarEstadosFuturos(p_lngActoID, p_lngPersonaID, Convert.ToInt32(objInformacionActo.Tables[0].Rows[0]["ID_ESTADO_ACTUAL"]), Convert.ToInt32(objInformacionActo.Tables[0].Rows[0]["ID_FLUJO_NOT_ELEC"]));
            }
            else
            {
                throw new Exception("No se encontro información de la notificación");
            }
        }


        /// <summary>
        /// Cargar la información a mostrar en el modal de editar estado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado a editar</param>
        private void CargarDatosModalEditar(long p_lngEstadoPersonaActoID)
        {
            Notificacion objNotificacion = null;
            EstadoFlujoNotificacion objEstadoFlujo = null;
            DataSet objInformacionActo = null;
            FirmaAutoridadPlantillaNotificacion objFirmaAutoridadPlantillaNotificacion = null;
            List<FirmaAutoridadNotificacionEntity> objLstFirmas = null;

            //Cargar la información del acto
            objNotificacion = new Notificacion();
            objInformacionActo = objNotificacion.ObtenerInformacionEstadoPersonaActo(p_lngEstadoPersonaActoID);

            //Validar que se obtenga información
            if (objInformacionActo != null && objInformacionActo.Tables != null && objInformacionActo.Tables[0].Rows.Count > 0)
            {
                //Cardar datos
                this.hdfEstadoPersonaActoID.Value = p_lngEstadoPersonaActoID.ToString();
                this.ltlNumeroVitalEditar.Text = objInformacionActo.Tables[0].Rows[0]["NUM_VITAL"].ToString();
                this.ltlNumeroExpedienteEditar.Text = objInformacionActo.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
                this.ltlActoAdministrativoEditar.Text = objInformacionActo.Tables[0].Rows[0]["TIPO_ACTO_ADMINISTRATIVO"].ToString();
                this.ltlNumeroActoEditar.Text = objInformacionActo.Tables[0].Rows[0]["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString();
                this.ltlIdentificacionEditar.Text = objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString();
                this.ltlUsuarioEditar.Text = objInformacionActo.Tables[0].Rows[0]["USUARIO_NOTIFICAR"].ToString();
                this.ltlEstadoEditar.Text = objInformacionActo.Tables[0].Rows[0]["ESTADO"].ToString();
                this.txtFechaEstadoNotificacionEditar.Text = Convert.ToDateTime(objInformacionActo.Tables[0].Rows[0]["FECHA_ESTADO"]).ToString("dd/MM/yyyy HH:mm");
                this.txtObservacion.Text = (objInformacionActo.Tables[0].Rows[0]["OBSERVACION"] != System.DBNull.Value ? objInformacionActo.Tables[0].Rows[0]["OBSERVACION"].ToString() : "");
                this.hdfAutoridadAmbientalEditar.Value = objInformacionActo.Tables[0].Rows[0]["ID_AUTORIDAD"].ToString();
                this.hdfPersonaIDEditar.Value = objInformacionActo.Tables[0].Rows[0]["ID_PERSONA"].ToString();
                this.hdfEstadoActualIDEditar.Value = objInformacionActo.Tables[0].Rows[0]["ID_ESTADO"].ToString();
                this.hdfPersonaIdentificacionEditar.Value = objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_USUARIO_NOTIFICAR"].ToString();
                this.hdfCodigoExpedienteActoEditar.Value = objInformacionActo.Tables[0].Rows[0]["EXPEDIENTE"].ToString();
                this.hdfNumeroVitalActoEditar.Value = objInformacionActo.Tables[0].Rows[0]["NUM_VITAL"].ToString();
                this.hdfFechaEstadoAnterior.Value = Convert.ToDateTime(objInformacionActo.Tables[0].Rows[0]["FECHA_ESTADO_ANTERIOR"]).ToString("dd/MM/yyyy HH:mm");
                this.dvIdentificacion.Visible = true;
                this.dvUsuario.Visible = true;

                //Cargar configuración del estado
                objEstadoFlujo = new EstadoFlujoNotificacion();
                this._objConfiguracionEstadoFlujo = objEstadoFlujo.ConsultarConfiguracionEstadoNotificacionElectronica(Convert.ToInt32(objInformacionActo.Tables[0].Rows[0]["ID_FLUJO_NOT_ELEC"]), Convert.ToInt32(objInformacionActo.Tables[0].Rows[0]["ID_ESTADO"]));

                //Validar que se obtenga información
                if (this._objConfiguracionEstadoFlujo != null && this._objConfiguracionEstadoFlujo.Activo)
                {
                    //Validar si se envía acto administrativo
                    if (this._objConfiguracionEstadoFlujo.PermitiAnexarActoAdministrativo)
                    {
                        this.dvAdjuntarActoAdministrativoEditar.Visible = true;
                        this.chkAdjuntarActoAdministrativoEditar.Checked = true;

                        if (this._objConfiguracionEstadoFlujo.PermitiAnexarConceptosActoAdministrativo && objNotificacion.ActoTieneConceptosAsociados(Convert.ToInt64(this.hdfActoID.Value)))
                        {
                            this.dvAdjuntarConceptosActoAdministrativoEditar.Visible = true;
                            this.chkAdjuntarConceptosActoAdministrativoEditar.Checked = false;
                        }
                        else
                        {
                            this.dvAdjuntarConceptosActoAdministrativoEditar.Visible = false;
                            this.chkAdjuntarConceptosActoAdministrativoEditar.Checked = false;
                        }
                    }
                    else
                    {
                        this.dvAdjuntarActoAdministrativoEditar.Visible = false;
                        this.chkAdjuntarActoAdministrativoEditar.Checked = false;
                        this.dvAdjuntarConceptosActoAdministrativoEditar.Visible = false;
                        this.chkAdjuntarConceptosActoAdministrativoEditar.Checked = false;
                    }

                    //Mostrar envia notificacion fisica
                    if (this._objConfiguracionEstadoFlujo.EnviaNotificacionFisica)
                    {
                        this.dvEnviarDireccionEditar.Visible = true;
                    }

                    //Mostrar envío de correo
                    if (this._objConfiguracionEstadoFlujo.EnviaCorreoAvanceManual)
                    {
                        this.dvEnviarCorreoEditar.Visible = true;
                    }

                    //Mostrar captura de recpción de información
                    if (this._objConfiguracionEstadoFlujo.SolicitarReferenciaRecepcionNotificacion)
                    {
                        this.dvReferenciaRecepcionEditar.Visible = true;
                        this.dvFechaRecepcionEditar.Visible = true;
                        this.txtReferenciaRecepcionEditar.Text = objInformacionActo.Tables[0].Rows[0]["REFERENCIA"].ToString();
                        if (objInformacionActo.Tables[0].Rows[0]["FECHA_REFERENCIA"] != System.DBNull.Value)
                            this.txtFechaRecepcionDocumentoEditar.Text = Convert.ToDateTime(objInformacionActo.Tables[0].Rows[0]["FECHA_REFERENCIA"]).ToString("dd/MM/yyyy HH:mm");

                        if (this._objConfiguracionEstadoFlujo.ReferenciaRecepcionNotificacionObligatoria)
                        {
                            this.rfvReferenciaRecepcionEditar.Enabled = true;
                            this.rfvFechaRecepcionDocumentoEditar.Enabled = true;
                        }
                        else
                        {
                            this.rfvReferenciaRecepcionEditar.Enabled = false;
                            this.rfvFechaRecepcionDocumentoEditar.Enabled = false;
                        }
                    }

                    //Validar si presenta plantilla
                    if (this._objConfiguracionEstadoFlujo.GeneraPlantilla)
                    {
                        //Realizar la consulta de las firmas de la plantilla
                        objFirmaAutoridadPlantillaNotificacion = new FirmaAutoridadPlantillaNotificacion();
                        objLstFirmas = objFirmaAutoridadPlantillaNotificacion.ListarFirmasPlantilla(this._objConfiguracionEstadoFlujo.PlantillaID);

                        //Validar si se tiene más de una firma registrada en la plantilla
                        if (objLstFirmas != null && objLstFirmas.Count > 1)
                        {
                            this.cboFirmaEditar.DataSource = objLstFirmas;
                            this.cboFirmaEditar.DataTextField = "NombreAutorizado";
                            this.cboFirmaEditar.DataValueField = "FirmaAutoridadID";
                            this.cboFirmaEditar.DataBind();
                            this.cboFirmaEditar.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            this.dvFirmasDocumentoEditar.Visible = true;
                        }
                        else
                        {
                            this.cboFirmaEditar.ClearSelection();
                            this.cboFirmaEditar.Items.Clear();
                            this.dvFirmasDocumentoEditar.Visible = false;
                        }
                    }
                    else
                    {
                        this.cboFirmaEditar.ClearSelection();
                        this.cboFirmaEditar.Items.Clear();
                        this.dvFirmasDocumentoEditar.Visible = false;
                    }

                    //Validar si el estado carga información de persona notificada
                    if (this._objConfiguracionEstadoFlujo.SolicitarInformacionPersonaNotificar)
                    {
                        //Cargar listado de tipos de identificacion
                        this.ConsultaTiposIdentificacion(this.cboTipoIdentificacionPersonaNotificarEditar);

                        //Cargar datos
                        this.cboTipoIdentificacionPersonaNotificarEditar.SelectedValue = objInformacionActo.Tables[0].Rows[0]["ID_TIPO_IDENTIFICACION_PERSONA_NOTIFICADA"].ToString();
                        this.txtNumeroIdentificacionPersonaNotificarEditar.Text = objInformacionActo.Tables[0].Rows[0]["IDENTIFICACION_PERSONA_NOTIFICADA"].ToString();
                        this.txtNombrePersonaNotificarEditar.Text = objInformacionActo.Tables[0].Rows[0]["NOMBRE_PERSONA_NOTIFICADA"].ToString();
                        this.txtCalidadPersonaNotificarEditar.Text = objInformacionActo.Tables[0].Rows[0]["CALIDAD_PERSONA_NOTIFICADA"].ToString();
                        this.dvTipoIdentificacionPersonaNotificarEditar.Visible = true;
                        this.dvNumeroIdentificacionPersonaNotificarEditar.Visible = true;
                        this.dvNombrePersonaNotificarEditar.Visible = true;
                        this.dvCalidadPersonaNotificarEditar.Visible = true;
                    }
                    else
                    {
                        this.cboTipoIdentificacionPersonaNotificarEditar.ClearSelection();
                        this.cboTipoIdentificacionPersonaNotificarEditar.Items.Clear();
                        this.txtNumeroIdentificacionPersonaNotificarEditar.Text = "";
                        this.txtNombrePersonaNotificarEditar.Text = "";
                        this.txtCalidadPersonaNotificarEditar.Text = "";
                        this.dvTipoIdentificacionPersonaNotificarEditar.Visible = false;
                        this.dvNumeroIdentificacionPersonaNotificarEditar.Visible = false;
                        this.dvNombrePersonaNotificarEditar.Visible = false;
                        this.dvCalidadPersonaNotificarEditar.Visible = false;
                    }

                    //Documento adicional
                    this.dvDocumentoAdicionalEditar.Visible = this._objConfiguracionEstadoFlujo.DocumentoAdicional;

                }
                else
                {
                    throw new Exception("No existe información de configuración del estado a editar: " + p_lngEstadoPersonaActoID);
                }
            }
            else
            {
                throw new Exception("No se encontro información del estado");
            }
        }


        /// <summary>
        /// Crear tabla de de documentos
        /// </summary>
        /// <returns>DataTable vacio</returns>
        private DataTable CrearTablaDocumentos()
        {
            DataTable objDocumentos = null;

            //CRear tabla
            objDocumentos = new DataTable();

            //Adicionar columnas
            objDocumentos.Columns.Add("NOMBRE_DOCUMENTO", Type.GetType("System.String"));
            objDocumentos.Columns.Add("RUTA_DOCUMENTO", Type.GetType("System.String"));

            return objDocumentos;
        }


        /// <summary>
        /// Cargar los trigger de la grilla especificada
        /// </summary>
        private void CargarTriggersDocumentosAdjuntosVerGrilla(GridView p_objGridView)
        {
            ImageButton objImagen = null;
            PostBackTrigger objPostTrigger = null;

            //Verificar que la grilla contenga información
            if (p_objGridView != null && p_objGridView.Rows.Count > 0)
            {
                //Ciclo que adiciona los triggers de la grilla
                foreach (GridViewRow objRowDocumento in p_objGridView.Rows)
                {
                    //Cargar imagen de descarga
                    objImagen = (ImageButton)objRowDocumento.FindControl("imgDescargarDocumentoAdjuntoVer");

                    //Si existe el control y se encuentra visible
                    if (objImagen != null)
                    {
                        objPostTrigger = new PostBackTrigger();
                        objPostTrigger.ControlID = objImagen.UniqueID;
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                    }
                }
            }

        }

        /// <summary>
        /// Cargar la información a mostrar en el modal de editar estado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado a editar</param>
        private void CargarDatosModalVerAdjuntos(long p_lngEstadoPersonaActoID)
        {
            Notificacion objNotificacion = null;
            DataSet objInformacionActo = null;
            DataTable objDocumentos = null;
            DataTable objConceptosActo = null;
            DataRow objDocumento = null;

            //Cargar la información del acto
            objNotificacion = new Notificacion();
            objInformacionActo = objNotificacion.ObtenerInformacionEstadoPersonaActo(p_lngEstadoPersonaActoID);

            //Validar que se obtenga información
            if (objInformacionActo != null && objInformacionActo.Tables != null && objInformacionActo.Tables[0].Rows.Count > 0)
            {
                //Verificar si envía a correo
                if (objInformacionActo.Tables[0].Rows[0]["ENVIA_CORREO"] != System.DBNull.Value && Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ENVIA_CORREO"]))
                {
                    //Crear tabla
                    objDocumentos = this.CrearTablaDocumentos();

                    //Cargar acto administrativo
                    if (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ADJUNTO_INCLUYE_ACTO"]) &&
                       !string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString()) &&
                       !objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().ToString().EndsWith("/") && !objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().ToString().EndsWith("\\"))
                    {

                        //Cargar datos
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().Substring((objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("/") != -1 ? objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("/") : objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_ACTO_ADMINISTRATIVO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Adicionar conceptos en caso de que existan
                    if (Convert.ToBoolean(objInformacionActo.Tables[0].Rows[0]["ADJUNTO_INCLUYE_CONCEPTOS_ACTO"]))
                    {
                        //Crear objeto de notificacion
                        objNotificacion = new Notificacion();

                        //Consultar conceptos
                        objConceptosActo = objNotificacion.ConsultarConceptosAsociadosActoAdministrativo(Convert.ToInt64(objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"]));

                        foreach (DataRow objConcepto in objConceptosActo.Rows)
                        {
                            objDocumento = objDocumentos.NewRow();
                            objDocumento["NOMBRE_DOCUMENTO"] = objConcepto["DOC_ARCHIVO"].ToString();
                            if (Convert.ToInt32(objConcepto["NOT_AUT_ID"]) == (int)AutoridadesAmbientales.ANLA)
                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILA@" + objConcepto["DOC_ID"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_PERSONA"].ToString() + "_" + p_lngEstadoPersonaActoID.ToString();
                            else
                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILAMC@" + objConcepto["DOC_ID"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"].ToString() + "_" + objInformacionActo.Tables[0].Rows[0]["ID_PERSONA"].ToString() + "_" + p_lngEstadoPersonaActoID.ToString();
                            objDocumentos.Rows.Add(objDocumento);
                        }
                    }

                    //Cargar adjuntos
                    if (!string.IsNullOrEmpty(objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString()) &&
                       !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("/") && !objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("\\"))
                    {

                        //Cargar datos
                        objDocumento = objDocumentos.NewRow();
                        objDocumento["NOMBRE_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().Substring((objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") != -1 ? objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") : objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = objInformacionActo.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString();
                        objDocumentos.Rows.Add(objDocumento);
                    }

                    //Validar si se incluyeron adjuntos
                    if (objDocumentos.Rows.Count > 0)
                    {
                        //Mostrar en grilla
                        this.grdDocumentosAdjuntosVer.DataSource = objDocumentos;
                        this.grdDocumentosAdjuntosVer.DataBind();

                        //Cargar triggers
                        this.CargarTriggersDocumentosAdjuntosVerGrilla(this.grdDocumentosAdjuntosVer);
                    }
                    else
                    {
                        //Limpiar grilla
                        this.grdDocumentosAdjuntosVer.DataSource = null;
                        this.grdDocumentosAdjuntosVer.DataBind();
                    }
                }
                else{
                    //Limpiar grlla
                    this.grdDocumentosAdjuntosVer.DataSource = null;
                    this.grdDocumentosAdjuntosVer.DataBind();
                }
            }
            else{
                throw new Exception("No se enocntro información del estado " + p_lngEstadoPersonaActoID.ToString());
            }            
        }


        /// <summary>
        /// Mostrar la información en el modal de acuerdo al estado seleccionado
        /// </summary>
        /// <param name="p_intEstadoID">int con el identificador del estado</param>
        /// <param name="p_intFlujoID">int con el identificador del flujo</param>
        private void CargarDatosEstadoModalAvanzar(int p_intEstadoID, int p_intFlujoID)
        {
            EstadoFlujoNotificacion objEstadoFlujo = null;
            DataTable objPersonas = null;
            DataRow[] objPersonasNotificar = null;
            Notificacion objNotificacion = null;
            FirmaAutoridadPlantillaNotificacion objFirmaAutoridadPlantillaNotificacion = null;
            List<FirmaAutoridadNotificacionEntity> objLstFirmas = null;

            //Limpiar el modal
            this.LimpiarModalAvanzar(false);

            //Cargar configuración de estado
            objEstadoFlujo = new EstadoFlujoNotificacion();
            this._objConfiguracionEstadoFlujo = objEstadoFlujo.ConsultarConfiguracionEstadoNotificacionElectronica(p_intFlujoID, p_intEstadoID);

            //Validar que se obtenga información
            if (this._objConfiguracionEstadoFlujo != null && this._objConfiguracionEstadoFlujo.Activo)
            {
                //Validar si el estado es de ejecutoria
                if (!this._objConfiguracionEstadoFlujo.EsEjecutoria && !this._objConfiguracionEstadoFlujo.EsFinalPublicidad)
                {
                    //Recargar datos del modal
                    this.CargarDatosModalAvanzar(Convert.ToInt32(this.hdfAutoridadAmbiental.Value), Convert.ToInt64(this.hdfActoID.Value), Convert.ToInt64(this.hdfPersonaID.Value));

                    //Reiniciar repeater
                    this.rptDatosUsuariosAvanzar.Visible = false;
                    this.rptDatosUsuariosAvanzar.DataSource = null;
                    this.rptDatosUsuariosAvanzar.DataBind();

                    //Seleccionar estado
                    this.cboEstado.SelectedValue = p_intEstadoID.ToString();

                    //Verificar si se muestra sección para mostrar el acto administrativo
                    if (this._objConfiguracionEstadoFlujo.PermitiAnexarActoAdministrativo)
                    {
                        this.dvAdjuntarActoAdministrativo.Visible = true;
                        this.chkAdjuntarActoAdministrativo.Checked = true;

                        //Crear objeto notificacion
                        objNotificacion = new Notificacion();

                        if (this._objConfiguracionEstadoFlujo.PermitiAnexarConceptosActoAdministrativo && objNotificacion.ActoTieneConceptosAsociados(Convert.ToInt64(this.hdfActoID.Value)))
                        {
                            this.dvAdjuntarConceptosActoAdministrativo.Visible = true;
                            this.chkAdjuntarConceptosActoAdministrativo.Checked = false;
                        }
                        else
                        {
                            this.dvAdjuntarConceptosActoAdministrativo.Visible = false;
                            this.chkAdjuntarConceptosActoAdministrativo.Checked = false;
                        }
                    }
                    else
                    {
                        this.dvAdjuntarActoAdministrativo.Visible = false;
                        this.chkAdjuntarActoAdministrativo.Checked = false;
                        this.dvAdjuntarConceptosActoAdministrativo.Visible = false;
                        this.chkAdjuntarConceptosActoAdministrativo.Checked = false;
                    }


                    //Mostrar envia notificacion fisica
                    if (this._objConfiguracionEstadoFlujo.EnviaNotificacionFisica)
                        this.dvEnviarDireccion.Visible = true;

                    //Mostrar envío de correo
                    if (this._objConfiguracionEstadoFlujo.EnviaCorreoAvanceManual)
                        this.dvEnviarCorreo.Visible = true;

                    //Mostrar captura de recpción de información
                    if (this._objConfiguracionEstadoFlujo.SolicitarReferenciaRecepcionNotificacion)
                    {
                        this.dvReferenciaRecepcion.Visible = true;
                        this.dvFechaRecepcion.Visible = true;

                        if (this._objConfiguracionEstadoFlujo.ReferenciaRecepcionNotificacionObligatoria)
                        {
                            this.rfvReferenciaRecepcion.Enabled = true;
                            this.rfvFechaRecepcionDocumento.Enabled = true;
                        }
                        else
                        {
                            this.rfvReferenciaRecepcion.Enabled = false;
                            this.rfvFechaRecepcionDocumento.Enabled = false;
                        }
                    }

                    //Validar si presenta plantilla
                    if (this._objConfiguracionEstadoFlujo.GeneraPlantilla)
                    {
                        //Realizar la consulta de las firmas de la plantilla
                        objFirmaAutoridadPlantillaNotificacion = new FirmaAutoridadPlantillaNotificacion();
                        objLstFirmas = objFirmaAutoridadPlantillaNotificacion.ListarFirmasPlantilla(this._objConfiguracionEstadoFlujo.PlantillaID);

                        //Validar si se tiene más de una firma registrada en la plantilla
                        if (objLstFirmas != null && objLstFirmas.Count > 1)
                        {
                            this.cboFirma.DataSource = objLstFirmas;
                            this.cboFirma.DataTextField = "NombreAutorizado";
                            this.cboFirma.DataValueField = "FirmaAutoridadID";
                            this.cboFirma.DataBind();
                            this.cboFirma.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            this.dvFirmasDocumento.Visible = true;
                        }
                        else
                        {
                            this.cboFirma.ClearSelection();
                            this.cboFirma.Items.Clear();
                            this.dvFirmasDocumento.Visible = false;
                        }
                    }
                    else
                    {
                        this.cboFirma.ClearSelection();
                        this.cboFirma.Items.Clear();
                        this.dvFirmasDocumento.Visible = false;
                    }

                    //Verificar si solicita datos de persona a notificar
                    if (this._objConfiguracionEstadoFlujo.SolicitarInformacionPersonaNotificar)
                    {
                        //Cargar listado de tipos de identificacion
                        this.ConsultaTiposIdentificacion(this.cboTipoIdentificacionPersonaNotificar);

                        //Mostrar campos
                        this.dvTipoIdentificacionPersonaNotificar.Visible = true;
                        this.dvNumeroIdentificacionPersonaNotificar.Visible = true;
                        this.dvNombrePersonaNotificar.Visible = true;
                        this.dvCalidadPersonaNotificar.Visible = true;
                    }
                    else
                    {
                        this.cboTipoIdentificacionPersonaNotificar.ClearSelection();
                        this.cboTipoIdentificacionPersonaNotificar.Items.Clear();
                        this.txtNumeroIdentificacionPersonaNotificar.Text = "";
                        this.txtNombrePersonaNotificar.Text = "";
                        this.txtCalidadPersonaNotificar.Text = "";
                        this.dvTipoIdentificacionPersonaNotificar.Visible = false;
                        this.dvNumeroIdentificacionPersonaNotificar.Visible = false;
                        this.dvNombrePersonaNotificar.Visible = false;
                        this.dvCalidadPersonaNotificar.Visible = false;
                    }
                }
                else if (this._objConfiguracionEstadoFlujo.EsEjecutoria)
                {
                    //Ocultar datos basicos de usuario
                    this.dvIdentificacion.Visible = false;
                    this.dvUsuario.Visible = false;

                    //Verificar si debe capturar información por person
                    if (this._objConfiguracionEstadoFlujo.EnviaCorreoAvanceManual || this._objConfiguracionEstadoFlujo.EnviaNotificacionFisica || this._objConfiguracionEstadoFlujo.SolicitarReferenciaRecepcionNotificacion)
                    {
                        //Consultar listado de usuarios  participantes
                        objNotificacion = new Notificacion();
                        objPersonas = objNotificacion.ConsultarUsuariosActo(Convert.ToInt64(this.hdfActoID.Value));
                        if (objPersonas != null)
                        {
                            objPersonasNotificar = objPersonas.Select("ID_TIPO_NOTIFICACION = " + ((int)TipoNotificacion.NOTIFICACION).ToString());
                        }

                        //Verificar que existan datos de las personas participantes
                        if (objPersonasNotificar != null && objPersonasNotificar.Length > 0)
                        {
                            //Cargar datos repeater
                            this.rptDatosUsuariosAvanzar.Visible = true;
                            this.rptDatosUsuariosAvanzar.DataSource = objPersonasNotificar.CopyToDataTable();
                            this.rptDatosUsuariosAvanzar.DataBind();
                        }
                        else
                        {
                            throw new Exception("No existe información de participantes del acto " + this.hdfActoID.Value);
                        }
                    }
                    else
                    {
                        this.rptDatosUsuariosAvanzar.Visible = false;
                        this.rptDatosUsuariosAvanzar.DataSource = null;
                        this.rptDatosUsuariosAvanzar.DataBind();
                    }

                    //Ocultar datos persona notificar
                    this.dvTipoIdentificacionPersonaNotificar.Visible = false;
                    this.dvNumeroIdentificacionPersonaNotificar.Visible = false;
                    this.dvNombrePersonaNotificar.Visible = false;
                    this.dvCalidadPersonaNotificar.Visible = false;
                }
                else if (this._objConfiguracionEstadoFlujo.EsFinalPublicidad)
                {
                    //Ocultar datos basicos de usuario
                    this.dvIdentificacion.Visible = false;
                    this.dvUsuario.Visible = false;

                    //Verificar si debe capturar información por person
                    if (this._objConfiguracionEstadoFlujo.EnviaCorreoAvanceManual || this._objConfiguracionEstadoFlujo.EnviaNotificacionFisica || this._objConfiguracionEstadoFlujo.SolicitarReferenciaRecepcionNotificacion)
                    {
                        //Consultar listado de usuarios  participantes
                        objNotificacion = new Notificacion();
                        objPersonas = objNotificacion.ConsultarUsuariosActo(Convert.ToInt64(this.hdfActoID.Value));

                        //Verificar que existan datos de las personas participantes
                        if (objPersonas != null && objPersonas.Rows.Count > 0)
                        {
                            //Cargar datos repeater
                            this.rptDatosUsuariosAvanzar.Visible = true;
                            this.rptDatosUsuariosAvanzar.DataSource = objPersonas;
                            this.rptDatosUsuariosAvanzar.DataBind();
                        }
                        else
                        {
                            throw new Exception("No existe información de participantes del acto " + this.hdfActoID.Value);
                        }
                    }
                    else
                    {
                        this.rptDatosUsuariosAvanzar.Visible = false;
                        this.rptDatosUsuariosAvanzar.DataSource = null;
                        this.rptDatosUsuariosAvanzar.DataBind();
                    }

                    //Ocultar datos persona notificar
                    this.dvTipoIdentificacionPersonaNotificar.Visible = false;
                    this.dvNumeroIdentificacionPersonaNotificar.Visible = false;
                    this.dvNombrePersonaNotificar.Visible = false;
                    this.dvCalidadPersonaNotificar.Visible = false;
                }

                //Documento adicional
                this.dvDocumentoAdicional.Visible = this._objConfiguracionEstadoFlujo.DocumentoAdicional;
            }
            else
            {
                throw new Exception("No existe información de configuración del estado " + p_intEstadoID.ToString() + " para el flujo " + p_intFlujoID.ToString());
            }
        }


        /// <summary>
        /// Obtiene el listado de direcciones
        /// </summary>
        /// <param name="p_objListado">DropDownList con el listado en el cual se cargaran los datos</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        private void ConsultarDirecciones(DropDownList p_objListado, long p_lngPersonaID)
        {
            PersonaNotificar objPersona = null;
            
            objPersona = new PersonaNotificar();
            p_objListado.DataSource = objPersona.ObtenerListadoDireccionesNotificar(p_lngPersonaID);
            p_objListado.DataTextField = "DIP_DIRECCION";
            p_objListado.DataValueField = "DIP_ID";
            p_objListado.DataBind();
            
            p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }

        /// <summary>
        /// Obtiene el listado de tipos de direcciones
        /// </summary>
        /// <param name="p_strTipoDireccionID">string con el tipo de dirección</param>        
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_lngDireccionID">long con identificador de la dirección</param>
        private void ConsultarDireccion(long p_lngPersonaID, long p_lngDireccionID, ref string p_strDireccionPertenece, ref string p_strDepartamento, ref string p_strMunicipio)
        {
            PersonaNotificar objPersona = null;
            DataRow objDireccion = null;

            //Cargar la información de los tipos documentales
            objPersona = new PersonaNotificar();
            objDireccion = objPersona.ObtenerInformacionDireccionPersona(p_lngPersonaID, p_lngDireccionID);

            //Verificar que se obtengan datos
            if (objDireccion != null)
            {
                p_strDireccionPertenece = objDireccion["PERTENECE"].ToString().Trim();
                p_strDepartamento = objDireccion["DEP_NOMBRE"].ToString().Trim();
                p_strMunicipio = objDireccion["MUN_NOMBRE"].ToString().Trim();
            }
            else
            {
                throw new Exception("No se encontro información para la dirección " + p_lngDireccionID + " de la persona " + p_lngPersonaID);
            }
        }


        /// <summary>
        /// Obtiene el listado de correos
        /// </summary>
        /// <param name="p_objListado">DropDownList con el listado en el cual se cargaran los datos</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_strNumeroVital">string con el numero vital</param>
        private void ConsultarCorreos(DropDownList p_objListado, long p_lngPersonaID, string p_strNumeroVital)
        {
            PersonaNotificar objPersona = null;

            //Cargar datos
            objPersona = new PersonaNotificar();
            p_objListado.DataSource = objPersona.ObtenerListadoCorreosNotificar(p_lngPersonaID, (p_strNumeroVital != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString() ? p_strNumeroVital : ""));
            p_objListado.DataTextField = "PER_CORREO_ELECTRONICO";
            p_objListado.DataValueField = "PER_CORREO_ELECTRONICO";
            p_objListado.DataBind();
            
            p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }


        /// <summary>
        /// Avanza la notificación a nuevo estado
        /// </summary>
        private void AvanzarNotificacion()
        {
            Notificacion objNotificacion = null;
            NotificacionFachada objNotificacionFachada = null;
            EstadoFlujoNotificacion objEstadoFlujo = null;
            EstadoFlujoNotificacionEntity objConfiguracionEstado = null;
            GenerarLogAuditoria objCrearLogAuditoria = null;
            List<DireccionNotificacionEntity> lstDirecciones = null;
            List<CorreoNotificacionEntity> lstCorreos = null;
            DataTable objPersonas = null;
            DataRow[] objPersonasNotificar = null;
            string strNombreAdjunto = "";
            byte[] objAdjunto = null;
            string strNombreDocumentoAdicional = "";
            byte[] objDocumentoAdicional = null;

            //Cargar configuración del estado
            objEstadoFlujo = new EstadoFlujoNotificacion();
            objConfiguracionEstado = objEstadoFlujo.ConsultarConfiguracionEstadoNotificacionElectronica(Convert.ToInt32(this.hdfFlujoID.Value), Convert.ToInt32(this.cboEstado.SelectedValue));

            //Validar si cargo archivo adicional
            if (objConfiguracionEstado.DocumentoAdicional && this.fuplDocumentoAdicional.PostedFile != null && this.fuplDocumentoAdicional.HasFile && this.fuplDocumentoAdicional.FileBytes.Length > 0)
            {
                //Cargar documento adicional
                strNombreDocumentoAdicional = "DAD_" + this.hdfActoID.Value + this.hdfPersonaID.Value + "_" + this.fuplDocumentoAdicional.FileName.ToString();
                objDocumentoAdicional = this.fuplDocumentoAdicional.FileBytes;
            }
            else
            {
                strNombreDocumentoAdicional = "";
                objDocumentoAdicional = null;
            }

            //Verificar si es un estado de ejecutoria
            if (objConfiguracionEstado.EsEjecutoria)
            {
                //Crear objeto manejo de notificaciones
                objNotificacion = new Notificacion();

                //Verificar si existe pendientes
                if (!objNotificacion.ExistePendientesFinalizarNotificacionActo(Convert.ToInt64(this.hdfActoID.Value)))
                {
                    //Se crea objetos manipulación de información
                    objNotificacionFachada = new SILPA.Servicios.NotificacionFachada();
                    objCrearLogAuditoria = new GenerarLogAuditoria();

                    //Verificar si el repeater se encuentra visible basados en configuracion
                    if (objConfiguracionEstado.EnviaCorreoAvanceManual || objConfiguracionEstado.EnviaNotificacionFisica || objConfiguracionEstado.SolicitarReferenciaRecepcionNotificacion)
                    {
                        //Realizar el registro para cada uno de los usuarios
                        if (this.rptDatosUsuariosAvanzar.Items.Count > 0)
                        {
                            foreach (RepeaterItem objItemUsuario in this.rptDatosUsuariosAvanzar.Items)
                            {
                                //Validar si cargo archivo
                                if (((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).PostedFile != null && ((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).HasFile && ((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).FileBytes.Length > 0)
                                {
                                    strNombreAdjunto = "ADJ_" + this.hdfActoID.Value + ((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value + "_" + ((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).FileName.ToString();
                                    objAdjunto = ((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).FileBytes;
                                }

                                //Cargar el listado de direcciones
                                if (this._Direcciones != null)
                                    lstDirecciones = ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value)).ToList();
                                else
                                    lstDirecciones = null;

                                //Cargar el listado de correos
                                if (this._Correos != null)
                                    lstCorreos = ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.PersonaID == Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value)).ToList();
                                else
                                    lstCorreos = null;

                                objNotificacion.CrearEstadoPersonaActo(Convert.ToInt64(this.hdfActoID.Value), Convert.ToInt32(((HiddenField)objItemUsuario.FindControl("hdRptfFlujoID")).Value), Convert.ToInt32(this.hdfEstadoActualID.Value), Convert.ToInt32(this.cboEstado.SelectedValue),
                                                                       Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value), Convert.ToInt32(this.hdfAutoridadAmbiental.Value), Convert.ToDateTime(this.txtFechaEstadoNotificacion.Text), this.hdfNumeroVitalActo.Value, this.txtObservacion.Text, ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString(),
                                                                       this.ltlNumeroExpediente.Text, this.ltlNumeroActo.Text, strNombreDocumentoAdicional, objDocumentoAdicional,
                                                                       ((CheckBox)objItemUsuario.FindControl("chkRptEnviarDireccion")).Checked, lstDirecciones,
                                                                       ((CheckBox)objItemUsuario.FindControl("chkRptEnviarCorreo")).Checked, lstCorreos, (((CheckBox)objItemUsuario.FindControl("chkRptEnviarCorreo")).Checked ? ((TextBox)objItemUsuario.FindControl("txtRptTextoCorreo")).Text.Trim() : ""),
                                                                       (((HtmlGenericControl)objItemUsuario.FindControl("dvRptAdjuntarActoAdministrativo")).Visible ? ((CheckBox)objItemUsuario.FindControl("chkRptAdjuntarActoAdministrativo")).Checked : false),
                                                                       (((HtmlGenericControl)objItemUsuario.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible ? ((CheckBox)objItemUsuario.FindControl("chkRptAdjuntarConceptosActoAdministrativo")).Checked : false),
                                                                       (((CheckBox)objItemUsuario.FindControl("chkRptEnviarCorreo")).Checked ? strNombreAdjunto : ""), objAdjunto,
                                                                       ((TextBox)objItemUsuario.FindControl("txtRptReferenciaRecepcion")).Text, (!string.IsNullOrEmpty(((TextBox)objItemUsuario.FindControl("txtRptFechaRecepcionDocumento")).Text) ? Convert.ToDateTime(((TextBox)objItemUsuario.FindControl("txtRptFechaRecepcionDocumento")).Text) : default(DateTime)), this.SolicitanteID, true,
                                                                       (this.dvFirmasDocumento.Visible ? Convert.ToInt32(this.cboFirma.SelectedValue) : -1), -1, "", "", "");

                                //Actualizar proceso PDI
                                objNotificacionFachada.ComponenteNotManual(this.cboEstado.SelectedValue.ToString(), this.cboEstado.SelectedItem.Text, this.hdfEsPDI.Value, Convert.ToInt64(this.hdfActoID.Value), "Ejecutoriar", Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value));

                                //Insertar en log
                                objCrearLogAuditoria.Insertar("SILPA", 1, "Se creó el estado " + this.cboEstado.SelectedItem.Text.ToString() + "para el usuario " + Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value) + " acto " + Convert.ToInt64(this.hdfActoID.Value));
                            }
                        }
                        else
                        {
                            throw new Exception("No se encontro información de las personas participantes en el acto " + this.hdfActoID.Value);
                        }
                    }
                    else
                    {
                        //Consultar listado de usuarios  participantes
                        objPersonas = objNotificacion.ConsultarUsuariosActo(Convert.ToInt64(this.hdfActoID.Value));
                        if (objPersonas != null)
                        {
                            objPersonasNotificar = objPersonas.Select("ID_TIPO_NOTIFICACION = " + ((int)TipoNotificacion.NOTIFICACION).ToString());
                        }

                        //Ciclo que genera el estado de ejecutoria
                        foreach (DataRow objPersona in objPersonasNotificar)
                        {
                            objNotificacion.CrearEstadoPersonaActo(Convert.ToInt64(this.hdfActoID.Value), objConfiguracionEstado.FlujoID, Convert.ToInt32(this.hdfEstadoActualID.Value), Convert.ToInt32(this.cboEstado.SelectedValue),
                                                                   Convert.ToInt64(objPersona["ID_PERSONA"]), Convert.ToInt32(this.hdfAutoridadAmbiental.Value), Convert.ToDateTime(this.txtFechaEstadoNotificacion.Text), this.hdfNumeroVitalActo.Value, this.txtObservacion.Text, ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString(),
                                                                   this.ltlNumeroExpediente.Text, this.ltlNumeroActo.Text, strNombreDocumentoAdicional, objDocumentoAdicional,
                                                                   false, null,
                                                                   false, null, "",
                                                                   false,
                                                                   false,
                                                                   "", null,
                                                                   "", default(DateTime), this.SolicitanteID, true,
                                                                   (this.dvFirmasDocumento.Visible ? Convert.ToInt32(this.cboFirma.SelectedValue) : -1), -1, "", "", "");

                            //Actualizar proceso PDI
                            objNotificacionFachada.ComponenteNotManual(this.cboEstado.SelectedValue.ToString(), this.cboEstado.SelectedItem.Text, this.hdfEsPDI.Value, Convert.ToInt64(this.hdfActoID.Value), "Ejecutoriar", Convert.ToInt64(objPersona["ID_PERSONA"]));

                            //Insertar en log
                            objCrearLogAuditoria.Insertar("SILPA", 1, "Se creó el estado " + this.cboEstado.SelectedItem.Text.ToString() + "para el usuario " + Convert.ToInt64(objPersona["ID_PERSONA"]) + " acto " + Convert.ToInt64(this.hdfActoID.Value));
                        }
                    }
                }
                else
                {
                    throw new Exception("Existen procesos de notificación sin finalizar");
                }
            }
            //Verificar si es un estado de final de publicidad
            else if (objConfiguracionEstado.EsFinalPublicidad)
            {
                //Crear objeto manejo de notificaciones
                objNotificacion = new Notificacion();

                //Verificar si existe pendientes
                if (!objNotificacion.ExistePendientesFinalizarActo(Convert.ToInt64(this.hdfActoID.Value)))
                {
                    //Se crea objetos manipulación de información
                    objNotificacionFachada = new SILPA.Servicios.NotificacionFachada();
                    objCrearLogAuditoria = new GenerarLogAuditoria();

                    //Verificar si el repeater se encuentra visible basados en configuracion
                    if (objConfiguracionEstado.EnviaCorreoAvanceManual || objConfiguracionEstado.EnviaNotificacionFisica || objConfiguracionEstado.SolicitarReferenciaRecepcionNotificacion)
                    {
                        //Realizar el registro para cada uno de los usuarios
                        if (this.rptDatosUsuariosAvanzar.Items.Count > 0)
                        {
                            foreach (RepeaterItem objItemUsuario in this.rptDatosUsuariosAvanzar.Items)
                            {
                                //Validar si cargo archivo
                                if (((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).PostedFile != null && ((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).HasFile && ((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).FileBytes.Length > 0)
                                {
                                    strNombreAdjunto = "ADJ_" + this.hdfActoID.Value + ((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value + "_" + ((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).FileName.ToString();
                                    objAdjunto = ((AjaxControlToolkit.AsyncFileUpload)objItemUsuario.FindControl("fuplRptAdjunto")).FileBytes;
                                }

                                //Cargar el listado de direcciones
                                if (this._Direcciones != null)
                                    lstDirecciones = ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value)).ToList();
                                else
                                    lstDirecciones = null;

                                //Cargar el listado de correos
                                if (this._Correos != null)
                                    lstCorreos = ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.PersonaID == Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value)).ToList();
                                else
                                    lstCorreos = null;

                                objNotificacion.CrearEstadoPersonaActo(Convert.ToInt64(this.hdfActoID.Value), Convert.ToInt32(((HiddenField)objItemUsuario.FindControl("hdRptfFlujoID")).Value), Convert.ToInt32(this.hdfEstadoActualID.Value), Convert.ToInt32(this.cboEstado.SelectedValue),
                                                                       Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value), Convert.ToInt32(this.hdfAutoridadAmbiental.Value), Convert.ToDateTime(this.txtFechaEstadoNotificacion.Text), this.hdfNumeroVitalActo.Value, this.txtObservacion.Text, ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString(),
                                                                       this.ltlNumeroExpediente.Text, this.ltlNumeroActo.Text, strNombreDocumentoAdicional, objDocumentoAdicional,
                                                                       ((CheckBox)objItemUsuario.FindControl("chkRptEnviarDireccion")).Checked, lstDirecciones,
                                                                       ((CheckBox)objItemUsuario.FindControl("chkRptEnviarCorreo")).Checked, lstCorreos,
                                                                       (((CheckBox)objItemUsuario.FindControl("chkRptEnviarCorreo")).Checked ? ((TextBox)objItemUsuario.FindControl("txtRptTextoCorreo")).Text.Trim() : ""),
                                                                       (((HtmlGenericControl)objItemUsuario.FindControl("dvRptAdjuntarActoAdministrativo")).Visible ? ((CheckBox)objItemUsuario.FindControl("chkRptAdjuntarActoAdministrativo")).Checked : false),
                                                                       (((HtmlGenericControl)objItemUsuario.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible ? ((CheckBox)objItemUsuario.FindControl("chkRptAdjuntarConceptosActoAdministrativo")).Checked : false),
                                                                       (((CheckBox)objItemUsuario.FindControl("chkRptEnviarCorreo")).Checked ? strNombreAdjunto : ""), objAdjunto,
                                                                       ((TextBox)objItemUsuario.FindControl("txtRptReferenciaRecepcion")).Text, (!string.IsNullOrEmpty(((TextBox)objItemUsuario.FindControl("txtRptFechaRecepcionDocumento")).Text) ? Convert.ToDateTime(((TextBox)objItemUsuario.FindControl("txtRptFechaRecepcionDocumento")).Text) : default(DateTime)), this.SolicitanteID, true,
                                                                       (this.dvFirmasDocumento.Visible ? Convert.ToInt32(this.cboFirma.SelectedValue) : -1), -1, "", "", "");

                                //Actualizar proceso                            
                                objNotificacionFachada.ComponenteNotManual(this.cboEstado.SelectedValue.ToString(), this.cboEstado.SelectedItem.Text, this.hdfEsPDI.Value, Convert.ToInt64(this.hdfActoID.Value), "Ejecutoriar", Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value));

                                //Insertar en log
                                objCrearLogAuditoria.Insertar("SILPA", 1, "Se creó el estado " + this.cboEstado.SelectedItem.Text.ToString() + "para el usuario " + Convert.ToInt64(((HiddenField)objItemUsuario.FindControl("hdfRptPersonaID")).Value) + " acto " + Convert.ToInt64(this.hdfActoID.Value));
                            }
                        }
                        else
                        {
                            throw new Exception("No se encontro información de las personas participantes en el acto " + this.hdfActoID.Value);
                        }
                    }
                    else
                    {
                        //Consultar listado de usuarios  participantes
                        objPersonas = objNotificacion.ConsultarUsuariosActo(Convert.ToInt64(this.hdfActoID.Value));

                        //Ciclo que genera el estado de ejecutoria
                        foreach (DataRow objPersona in objPersonas.Rows)
                        {
                            objNotificacion.CrearEstadoPersonaActo(Convert.ToInt64(this.hdfActoID.Value), objConfiguracionEstado.FlujoID, Convert.ToInt32(this.hdfEstadoActualID.Value), Convert.ToInt32(this.cboEstado.SelectedValue),
                                                                   Convert.ToInt64(objPersona["ID_PERSONA"]), Convert.ToInt32(this.hdfAutoridadAmbiental.Value), Convert.ToDateTime(this.txtFechaEstadoNotificacion.Text), this.hdfNumeroVitalActo.Value, this.txtObservacion.Text, ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString(),
                                                                   this.ltlNumeroExpediente.Text, this.ltlNumeroActo.Text, strNombreDocumentoAdicional, objDocumentoAdicional,
                                                                   false, null,
                                                                   false, null, "",
                                                                   false,
                                                                   false,
                                                                   "", null,
                                                                   "", default(DateTime), this.SolicitanteID, true,
                                                                   (this.dvFirmasDocumento.Visible ? Convert.ToInt32(this.cboFirma.SelectedValue) : -1), -1, "", "", "");

                            //Actualizar proceso PDI
                            objNotificacionFachada.ComponenteNotManual(this.cboEstado.SelectedValue.ToString(), this.cboEstado.SelectedItem.Text, this.hdfEsPDI.Value, Convert.ToInt64(this.hdfActoID.Value), "Ejecutoriar", Convert.ToInt64(objPersona["ID_PERSONA"]));

                            //Insertar en log
                            objCrearLogAuditoria.Insertar("SILPA", 1, "Se creó el estado " + this.cboEstado.SelectedItem.Text.ToString() + "para el usuario " + Convert.ToInt64(objPersona["ID_PERSONA"]) + " acto " + Convert.ToInt64(this.hdfActoID.Value));
                        }
                    }
                }
                else
                {
                    throw new Exception("Existen procesos de publicidad sin finalizar");
                }
            }
            else
            {
                //Validar si cargo archivo
                if (this.fuplAdjunto.PostedFile != null && this.fuplAdjunto.HasFile && this.fuplAdjunto.FileBytes.Length > 0)
                {
                    strNombreAdjunto = "ADJ_" + this.hdfActoID.Value + this.hdfPersonaID.Value + "_" + this.fuplAdjunto.FileName.ToString();
                    objAdjunto = this.fuplAdjunto.FileBytes;
                }

                //Crear nuevo estado
                objNotificacion = new Notificacion();
                objNotificacion.CrearEstadoPersonaActo(Convert.ToInt64(this.hdfActoID.Value), Convert.ToInt32(this.hdfFlujoID.Value), Convert.ToInt32(this.hdfEstadoActualID.Value), Convert.ToInt32(this.cboEstado.SelectedValue),
                                                       Convert.ToInt64(this.hdfPersonaID.Value), Convert.ToInt32(this.hdfAutoridadAmbiental.Value), Convert.ToDateTime(this.txtFechaEstadoNotificacion.Text), this.hdfNumeroVitalActo.Value, this.txtObservacion.Text, ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString(),
                                                       this.ltlNumeroExpediente.Text, this.ltlNumeroActo.Text, strNombreDocumentoAdicional, objDocumentoAdicional, this.chkEnviarDireccion.Checked, (List<DireccionNotificacionEntity>)this._Direcciones,
                                                       this.chkEnviarCorreo.Checked, ((List<CorreoNotificacionEntity>)this._Correos), (this.chkEnviarCorreo.Checked ? this.txtTextoCorreo.Text.Trim() : ""),                                                        
                                                       (this.dvAdjuntarActoAdministrativo.Visible ? this.chkAdjuntarActoAdministrativo.Checked : false),
                                                       (this.dvAdjuntarConceptosActoAdministrativo.Visible ? this.chkAdjuntarConceptosActoAdministrativo.Checked : false),
                                                       (this.chkEnviarCorreo.Checked ? strNombreAdjunto : ""), objAdjunto, this.txtReferenciaRecepcion.Text, (!string.IsNullOrEmpty(this.txtFechaRecepcionDocumento.Text) ? Convert.ToDateTime(this.txtFechaRecepcionDocumento.Text) : default(DateTime)), this.SolicitanteID, true,
                                                       (this.dvFirmasDocumento.Visible ? Convert.ToInt32(this.cboFirma.SelectedValue) : -1), 
                                                       (objConfiguracionEstado.SolicitarInformacionPersonaNotificar ? Convert.ToInt32(this.cboTipoIdentificacionPersonaNotificar.SelectedValue) : -1 ),
                                                       (objConfiguracionEstado.SolicitarInformacionPersonaNotificar ? this.txtNumeroIdentificacionPersonaNotificar.Text : "" ),
                                                       (objConfiguracionEstado.SolicitarInformacionPersonaNotificar ? this.txtNombrePersonaNotificar.Text : "" ),
                                                       (objConfiguracionEstado.SolicitarInformacionPersonaNotificar ? this.txtCalidadPersonaNotificar.Text : "" ));

                //Actualizar proceso
                objNotificacionFachada = new SILPA.Servicios.NotificacionFachada();
                objNotificacionFachada.ActualizarProcesos(int.Parse(this.cboEstado.SelectedValue), this.cboEstado.SelectedItem.Text, this.hdfEsPDI.Value, Convert.ToInt64(this.hdfActoID.Value), Convert.ToInt64(this.hdfPersonaID.Value));

                //Insertar en el log
                objCrearLogAuditoria = new GenerarLogAuditoria();
                objCrearLogAuditoria.Insertar("SILPA", 1, "Se creó el estado " + this.cboEstado.SelectedItem.Text.ToString() + "para el usuario " + Convert.ToInt64(this.hdfPersonaID.Value) + " acto " + Convert.ToInt64(this.hdfActoID.Value));
            }            
        }


        /// <summary>
        /// Edita la información del estado
        /// </summary>
        private void EditarNotificacion()
        {
            Notificacion objNotificacion = null;
            DataSet objInformacionEstadoActo = null;
            string strNombreAdjunto = "";
            byte[] objAdjunto = null;
            string strNombreDocumentoAdicional = "";
            byte[] objDocumentoAdicional = null;

            //Validar si cargo archivo adicional
            if (this.dvDocumentoAdicionalEditar.Visible && this.fuplDocumentoAdicionalEditar.PostedFile != null && this.fuplDocumentoAdicionalEditar.HasFile && this.fuplDocumentoAdicionalEditar.FileBytes.Length > 0)
            {
                //Cargar documento adicional
                strNombreDocumentoAdicional = "DAD_" + this.hdfActoID.Value + this.hdfPersonaID.Value + "_" + this.fuplDocumentoAdicionalEditar.FileName.ToString();
                objDocumentoAdicional = this.fuplDocumentoAdicionalEditar.FileBytes;
            }
            else
            {
                strNombreDocumentoAdicional = "";
                objDocumentoAdicional = null;
            }

            //Validar si cargo archivo
            if (this.fuplAdjunto.PostedFile != null && this.fuplAdjunto.HasFile && this.fuplAdjunto.FileBytes.Length > 0)
            {
                strNombreAdjunto = "ADJ_" + this.hdfActoID.Value + this.hdfPersonaID.Value + "_" + this.fuplAdjunto.FileName.ToString();
                objAdjunto = this.fuplAdjunto.FileBytes;
            }

            //Consultar informacion estado actual
            objNotificacion = new Notificacion();
            objInformacionEstadoActo = objNotificacion.ObtenerInformacionEstadoPersonaActo(Convert.ToInt64(this.hdfEstadoPersonaActoID.Value));

            //Verificar que se obtenga datos del estado
            if (objInformacionEstadoActo != null && objInformacionEstadoActo.Tables != null && objInformacionEstadoActo.Tables[0].Rows.Count > 0)
            {
                //Editar estado
                objNotificacion = new Notificacion();
                objNotificacion.EditarEstadoPersonaActo(Convert.ToInt64(this.hdfEstadoPersonaActoID.Value), Convert.ToInt64(objInformacionEstadoActo.Tables[0].Rows[0]["ID_ACTO_NOTIFICACION"]), Convert.ToInt32(objInformacionEstadoActo.Tables[0].Rows[0]["ID_FLUJO_NOT_ELEC"]), Convert.ToInt32(objInformacionEstadoActo.Tables[0].Rows[0]["ID_ESTADO"]),
                                                        Convert.ToInt64(objInformacionEstadoActo.Tables[0].Rows[0]["ID_PERSONA"]), Convert.ToInt32(objInformacionEstadoActo.Tables[0].Rows[0]["ID_AUTORIDAD"]), Convert.ToDateTime(this.txtFechaEstadoNotificacionEditar.Text), objInformacionEstadoActo.Tables[0].Rows[0]["NUM_VITAL"].ToString(), this.txtObservacionEditar.Text, ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString(),
                                                        this.ltlUsuarioEditar.Text, this.ltlNumeroExpedienteEditar.Text, this.ltlNumeroActoEditar.Text, strNombreDocumentoAdicional, objDocumentoAdicional,
                                                        this.chkEnviarDireccionEditar.Checked, (List<DireccionNotificacionEntity>)this._Direcciones,
                                                        this.chkEnviarCorreoEditar.Checked, (List<CorreoNotificacionEntity>)this._Correos, (this.chkEnviarCorreoEditar.Checked ? this.txtTextoCorreoEditar.Text.Trim() : ""),                                                         
                                                        (this.dvAdjuntarActoAdministrativoEditar.Visible ? this.chkAdjuntarActoAdministrativoEditar.Checked : false),
                                                        (this.dvAdjuntarConceptosActoAdministrativoEditar.Visible ? this.chkAdjuntarConceptosActoAdministrativoEditar.Checked : false),
                                                        (this.chkEnviarCorreoEditar.Checked ? strNombreAdjunto : ""), objAdjunto, this.txtReferenciaRecepcionEditar.Text, (!string.IsNullOrEmpty(this.txtFechaRecepcionDocumentoEditar.Text) ? Convert.ToDateTime(this.txtFechaRecepcionDocumentoEditar.Text) : default(DateTime)), this.SolicitanteID,
                                                        (this.dvFirmasDocumentoEditar.Visible ? Convert.ToInt32(this.cboFirmaEditar.SelectedValue) : -1),
                                                        (this.cboTipoIdentificacionPersonaNotificar.Visible ? Convert.ToInt32(this.cboTipoIdentificacionPersonaNotificar.SelectedValue) : -1),
                                                        (this.dvNumeroIdentificacionPersonaNotificar.Visible ? this.txtNumeroIdentificacionPersonaNotificar.Text : ""),
                                                        (this.dvNombrePersonaNotificar.Visible ? this.txtNombrePersonaNotificar.Text : ""),
                                                        (this.dvCalidadPersonaNotificar.Visible ? this.txtCalidadPersonaNotificar.Text : ""));

                GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                CrearLogAuditoria.Insertar("SILPA", 1, "Se realizo la edicición del estado " + this.ltlEstadoEditar.Text);
            }
            else
            {
                throw new Exception("No se pudo obtener información del estado para edición");
            }
        }


        /// <summary>
        /// Eliminar el estado especificado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID"></param>
        private void EliminarEstado(long p_lngEstadoPersonaActoID)
        {
            EstadoNotificacionDalc objEstadoNotificacionDalc = null;
            GenerarLogAuditoria objGenerarLogAuditoria = null;

            //Eliminar el estado
            objEstadoNotificacionDalc = new EstadoNotificacionDalc();
            objEstadoNotificacionDalc.EliminarEstadoPersonaActo(p_lngEstadoPersonaActoID);

            //Ingresar log
            objGenerarLogAuditoria = new GenerarLogAuditoria();
            objGenerarLogAuditoria.Insertar("SILPA", 4, "Se eliminó el estado de notificacion.");
        }

        /// <summary>
        /// Cargar el listado de direcciones en la grilla
        /// </summary>
        private void CargarDatosDirecciones(GridView p_grdDirescciones, List<DireccionNotificacionEntity> p_lstDirecciones, long p_lngPersonaID)
        {
            //Verificar si listado es vacio
            if (p_lstDirecciones == null || p_lstDirecciones.Count == 0)
            {
                //Cargar mensaje de ingreso de titulares
                p_lstDirecciones = new List<DireccionNotificacionEntity>();
                p_lstDirecciones.Add(new DireccionNotificacionEntity());
                p_grdDirescciones.DataSource = p_lstDirecciones;
                p_grdDirescciones.ShowFooter = true;
                p_grdDirescciones.DataBind();
                p_grdDirescciones.Rows[0].Cells.Clear();
                p_grdDirescciones.Rows[0].Cells.Add(new TableCell());
                p_grdDirescciones.Rows[0].Cells[0].ColumnSpan = 6;
                p_grdDirescciones.Rows[0].Cells[0].Text = "No se han ingresado direcciones";
            }
            else
            {
                //Cargar datos
                p_grdDirescciones.DataSource = p_lstDirecciones;
                p_grdDirescciones.DataBind();
            }

        }


        /// <summary>
        /// Cargar el listado de direcciones en la grilla
        /// </summary>
        private void CargarDatosCorreos(GridView p_grdCorreos, List<CorreoNotificacionEntity> p_lstCorreos, long p_lngPersonaID)
        {
            //Verificar si listado es vacio
            if (p_lstCorreos == null || p_lstCorreos.Count == 0)
            {
                //Cargar mensaje de ingreso de titulares
                p_lstCorreos = new List<CorreoNotificacionEntity>();
                p_lstCorreos.Add(new CorreoNotificacionEntity());
                p_grdCorreos.DataSource = p_lstCorreos;
                p_grdCorreos.ShowFooter = true;
                p_grdCorreos.DataBind();
                p_grdCorreos.Rows[0].Cells.Clear();
                p_grdCorreos.Rows[0].Cells.Add(new TableCell());
                p_grdCorreos.Rows[0].Cells[0].ColumnSpan = 3;
                p_grdCorreos.Rows[0].Cells[0].Text = "No se han ingresado correos electrónicos";
            }
            else
            {
                //Cargar datos
                p_grdCorreos.DataSource = p_lstCorreos;
                p_grdCorreos.DataBind();
            }

        }


        /// <summary>
        /// Refresca todas las grillas del repeater
        /// </summary>
        /// <param name="p_strGrillaExcepcion">string con el client id de la grilla que no se desea actualizar</param>
        private void RefrescarGrillasRepeater(string p_strGrillaExcepcion = "")
        {
            //Verificar que contenga informacion
            if (this.rptDatosUsuariosAvanzar.Items.Count > 0)
            {
                //Ciclo que recorre cada uno de los items
                foreach (RepeaterItem objItem in this.rptDatosUsuariosAvanzar.Items)
                {
                    //Verifica visibilidad correos
                    if (((HtmlGenericControl)objItem.FindControl("dvRptEnviarCorreo")).Visible && ((CheckBox)objItem.FindControl("chkRptEnviarCorreo")).Checked)
                    {
                        if (((GridView)objItem.FindControl("grdRptCorreos")).ClientID != p_strGrillaExcepcion)
                        {
                            //Recarga listado
                            if (this._Correos != null)
                                this.CargarDatosCorreos((GridView)objItem.FindControl("grdRptCorreos"), ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.PersonaID == Convert.ToInt64(((HiddenField)objItem.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objItem.FindControl("hdfRptPersonaID")).Value));                                
                            else
                                this.CargarDatosCorreos((GridView)objItem.FindControl("grdRptCorreos"), null, Convert.ToInt64(((HiddenField)objItem.FindControl("hdfRptPersonaID")).Value));

                            //Cargar desplegables footer
                            ((DropDownList)((GridView)objItem.FindControl("grdRptCorreos")).FooterRow.FindControl("cboRptGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                            //Cargar el validation group
                            ((RequiredFieldValidator)((GridView)objItem.FindControl("grdRptCorreos")).FooterRow.FindControl("rfvRptGrdGrupoCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objItem.ItemIndex;
                            ((RequiredFieldValidator)((GridView)objItem.FindControl("grdRptCorreos")).FooterRow.FindControl("rfvRptGrdCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objItem.ItemIndex;
                            ((LinkButton)((GridView)objItem.FindControl("grdRptCorreos")).FooterRow.FindControl("lnkRptAdicionarCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objItem.ItemIndex;
                        }
                    }

                    //Verifica visibilidad direcciones
                    if (((HtmlGenericControl)objItem.FindControl("dvRptEnviarDireccion")).Visible && ((CheckBox)objItem.FindControl("chkRptEnviarDireccion")).Checked)
                    {
                        if (((GridView)objItem.FindControl("grdRptDirecciones")).ClientID != p_strGrillaExcepcion)
                        {
                            //Recarga listado
                            if (this._Direcciones != null)
                                this.CargarDatosDirecciones((GridView)objItem.FindControl("grdRptDirecciones"), ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objItem.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objItem.FindControl("hdfRptPersonaID")).Value));
                            else
                                this.CargarDatosDirecciones((GridView)objItem.FindControl("grdRptDirecciones"), null, Convert.ToInt64(((HiddenField)objItem.FindControl("hdfRptPersonaID")).Value));

                            //Cargar desplegables footer
                            ((DropDownList)((GridView)objItem.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));


                            //Cargar el validation group
                            ((RequiredFieldValidator)((GridView)objItem.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdTipoDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objItem.ItemIndex;
                            ((RequiredFieldValidator)((GridView)objItem.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objItem.ItemIndex;
                            ((LinkButton)((GridView)objItem.FindControl("grdRptDirecciones")).FooterRow.FindControl("lnkRptAdicionar")).ValidationGroup = "RptAvanzarModalDirecciones" + objItem.ItemIndex;
                        }
                    }
                }
            }

        }

        
    #endregion


    #region Eventos


        #region Page

            /// <summary>
            /// Evento que se ejecuta al cargar la pagina. Se encarga de mostrar la información inicial de la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                //this.SolicitanteID = 434;

                if (!IsPostBack)
                {                                        
                    //this.InicializarPagina();

                    //Validar sesion de usuario
                    if (this.ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                    else
                    {
                        //Iniciliazar datos de la página
                        this.InicializarPagina();
                    }
                }
            }

        #endregion


        #region cboFlujoNotificacion

            /// <summary>
            /// Evento que carga el listado de estados de acuerdo al flujo seleccionado
            /// </summary>
            protected void cboFlujoNotificacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    if (!string.IsNullOrWhiteSpace(this.cboFlujoNotificacion.SelectedValue))
                    {
                        //Cargar el listado de estados
                        this.ConsultaEstadosNotificacion(Convert.ToInt32(this.cboFlujoNotificacion.SelectedValue));
                    }
                    else
                    {
                        this.cboEstadoNotificacion.ClearSelection();
                        this.cboEstadoNotificacion.Items.Clear();
                        this.cboEstadoNotificacion.Items.Add(new ListItem("Seleccione...", "-1"));
                    }

                    //Actualizar el panel del modal
                    this.upnlBuscar.Update();

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgEditar_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando información para edición del estado");
                }
            }

        #endregion


        #region cmdBuscar

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de Buscar. Busca la información de las actos administrativos
            /// </summary>
            protected void cmdBuscar_Click(object sender, EventArgs e)
            {
                //Ocultar Mensajes
                this.OcultarMensaje();

                //Cargar la información
                this.hdfNumeroVital.Value = this.txtNumeroVital.Text;
                this.hdfExpediente.Value = this.txtExpediente.Text;
                this.hdfIdentificacionUsuario.Value = this.txtIdentificacionUsuario.Text;
                this.hdfUsuarioNotificar.Value = this.txtUsuarioNotificar.Text;
                this.hdfNumeroActo.Value = this.txtNumeroActo.Text;
                this.hdfTipoActo.Value = this.cboTipoActo.SelectedValue;
                this.hdfDiasVencimientoInicial.Value = this.txtDiasVencimientoInicial.Text;
                this.hdfDiasVencimientoFinal.Value = this.txtDiasVencimientoFinal.Text;
                this.hdfFlujoNotificacion.Value = this.cboFlujoNotificacion.SelectedValue;
                this.hdfEstadoNotificacion.Value = this.cboEstadoNotificacion.SelectedValue;
                this.hdfEstadoNotificacionDescripcion.Value = this.cboEstadoNotificacion.SelectedItem.Text;
                this.hdfFechaDesde.Value = this.txtFechaDesde.Text;
                this.hdfFechaHasta.Value = this.txtFechaHasta.Text;
                this.hdfEstadoActual.Value =  (this.chkEstadoActual.Checked ? "1" : "0");

                //Inicializar grilla
                this.grdNotificaciones.PageIndex = 0;

                //Ejecutar consulta
                this.BuscarInformacionActosDministrativos();
            }

        #endregion


        #region grdNotificaciones
            
            /// <summary>
            /// Evento que se ejecuta al dar clic el cambio de pagina. CAmbia la página
            /// </summary>
            protected void grdNotificaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Cambiar pagina
                this.grdNotificaciones.PageIndex = e.NewPageIndex;

                //Consultar la información
                this.BuscarInformacionActosDministrativos();
            }


            /// <summary>
            /// Evento que se ejecuta al cargar la grilla. Permite la manipulación de la informción de las filas
            /// </summary>
            protected void grdNotificaciones_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                DataRowView objDatosFila = null;
                HyperLink objHyperLink = null;

                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {

                        //Cargar datos de la fila
                        objDatosFila = (DataRowView)e.Row.DataItem;

                        //Verificar que se obtenga datos de la fila
                        if (objDatosFila != null)
                        {

                            //Cambiar colores de grilla
                            if (Convert.ToInt32(objDatosFila["NOT_ID_ESTADO_ACTO"]) == (int)NOTEstadosActo.Verificado_Liberado_Parcialmente)
                            {
                                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFBF");
                            }
                            else if (Convert.ToInt32(objDatosFila["DIAS_PARA_VENCIMIENTO"]) < 0)
                            {
                                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCCCC");
                            }

                            //Cargar datos de estados
                            this.BuscarInformacionEstadosActoPersona((GridView)e.Row.FindControl("grdEstadosNotificacion"), Convert.ToInt64(objDatosFila["ID_ACTO_NOTIFICACION"]), Convert.ToInt64(objDatosFila["ID_PERSONA"]));

                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: grdNotificaciones_RowDataBound -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando datos de notificación");
                }
            }

        #endregion


        #region imgDescargarDocumento

            /// <summary>
            /// Evento que se ejecuta al dar clic en imagen documento. Descarga el documento indicado
            /// </summary>
            protected void imgDescargarDocumento_Click(object sender, ImageClickEventArgs e)
            {
                try
                {
                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        System.IO.FileInfo targetFile = new System.IO.FileInfo(((ImageButton)sender).CommandArgument.ToString());
                        this.Response.Clear();
                        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                        this.Response.ContentType = "application/octet-stream";
                        this.Response.ContentType = "application/base64";
                        this.Response.WriteFile(targetFile.FullName);
                        this.Response.WriteFile(((ImageButton)sender).CommandArgument.ToString());
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgDescargarDocumento_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
                
            }

        #endregion


        #region grdEstadosNotificacion
            
            /// <summary>
            /// Evento que se ejecuta al dar clic el cambio de pagina. CAmbia la página
            /// </summary>
            protected void grdEstadosNotificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                long lngActoID = 0;
                long lngPersonaID = 0;

                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    //Obtener el acto administrativo y la persona
                    lngActoID = long.Parse(((GridView)sender).DataKeys[0].Values[0].ToString());
                    lngPersonaID = long.Parse(((GridView)sender).DataKeys[0].Values[1].ToString());

                    //Mostrar datos
                    ((GridView)sender).PageIndex = e.NewPageIndex;
                    this.BuscarInformacionEstadosActoPersona(((GridView)sender), lngActoID, lngPersonaID);

                    //Cargar triggers
                    this.CargarTriggersGrillaNotificaciones();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: grdEstadosNotificacion_PageIndexChanging -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cambiando pagina");
                }
            }


            /// <summary>
            /// Evento que descarga plantilla generada
            /// </summary>
            protected void imgDocumentoPlantilla_Click(object sender, ImageClickEventArgs e)
            {
                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        System.IO.FileInfo targetFile = new System.IO.FileInfo(((ImageButton)sender).CommandArgument.ToString());
                        this.Response.Clear();
                        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                        this.Response.ContentType = "application/octet-stream";
                        this.Response.ContentType = "application/base64";
                        this.Response.WriteFile(targetFile.FullName);
                        this.Response.WriteFile(((ImageButton)sender).CommandArgument.ToString());
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgDocumentoPlantilla_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }


            /// <summary>
            /// Evento que descarga documento adjunto
            /// </summary>
            protected void imgDocumentoAdicional_Click(object sender, ImageClickEventArgs e)
            {
                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        System.IO.FileInfo targetFile = new System.IO.FileInfo(((ImageButton)sender).CommandArgument.ToString());
                        this.Response.Clear();
                        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                        this.Response.ContentType = "application/octet-stream";
                        this.Response.ContentType = "application/base64";
                        this.Response.WriteFile(targetFile.FullName);
                        this.Response.WriteFile(((ImageButton)sender).CommandArgument.ToString());
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgDocumentoAdicional_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }


            /// <summary>
            /// Evento que descarga archivos adjuntos
            /// </summary>
            protected void imgAdjuntos_Click(object sender, ImageClickEventArgs e)
            {
                long lngEstadoPersonaActoID = 0;

                try
                {
                    //Limpiar modal inicial
                    this.LimpiarModalVerAdjuntos();

                    //Cargar datos de adjuntos
                    lngEstadoPersonaActoID = long.Parse(((ImageButton)sender).CommandArgument.ToString());
                    this.CargarDatosModalVerAdjuntos(lngEstadoPersonaActoID);

                    //Actualizar modal
                    this.upnlVerDocumentosAdjuntos.Update();

                    //Mostrar modal
                    this.mpeVerDocumentosAdjuntos.Show();
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgAdjuntos_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando informacion de adjuntos");
                }
            }

        #endregion


        #region imgAvanzar
            
            /// <summary>
            /// Evento que se ejecuta al dar clic en avanzar. Muestra pantalla de avance de estado
            /// </summary>
            protected void imgAvanzar_Click(object sender, ImageClickEventArgs e)
            {
                int intFila = 0;
                long lngActoID = 0;
                long lngPersonaID = 0;
                int intAutoridadAmbiental = 0;
                int intEstadoActualID = 0;

                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    //Limpiar modal
                    this.LimpiarModalAvanzar();

                    //Cargar la fila desde la cual hicieron clic
                    intFila = Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString());

                    //Cargar las llaves para acceder a la información
                    lngActoID = long.Parse(this.grdNotificaciones.DataKeys[intFila].Values[0].ToString());
                    lngPersonaID = long.Parse(this.grdNotificaciones.DataKeys[intFila].Values[1].ToString());
                    intEstadoActualID = int.Parse(this.grdNotificaciones.DataKeys[intFila].Values[2].ToString());
                    intAutoridadAmbiental = Convert.ToInt32(this.grdNotificaciones.DataKeys[intFila].Values[3].ToString());
                                        
                    //Cargar datos modal
                    this.CargarDatosModalAvanzar(intAutoridadAmbiental, lngActoID, lngPersonaID);

                    //Actualizar el panel del modal
                    this.upnlAvanzarEstado.Update();

                    //Mostrar pantalla modal
                    this.mpeAvanzarEstado.Show();
                    
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgAvanzar_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando información para avance del estado");
                }
            }

        #endregion
    

        #region imgAvanzarBloqueado

            /// <summary>
            /// Evento que limpia y cierra modal de información de bloqueo
            /// </summary>
            protected void cmdModalInformacionBloqueoAceptar_Click(object sender, EventArgs e)
            {
                //Limpiar mensaje
                this.ltlMensajeInformacionBloqueo.Text = "";

                //Actualizar el panel del modal
                this.upnlModalInformacionBloqueo.Update();

                //Ocultar pantalla
                this.mpeModalInformacionBloqueo.Hide();
            }

        #endregion


        #region Modal Avanzar

            #region cmdCancelar

                /// <summary>
                /// Evento que cierra la ventana de avznce de estado
                /// </summary>
                protected void cmdCancelar_Click(object sender, EventArgs e)
                {
                    
                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Cargar triggers
                        this.CargarTriggersGrillaNotificaciones();

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Ocultar pantalla modal
                        this.mpeAvanzarEstado.Hide();
                        
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cmdCancelar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error realizando la cancelación del proceso de avance de notificación");
                    }
                }

            #endregion


            #region cboEstado
                
                /// <summary>
                /// Evento que se ejecuta al seleccionar alguno de los estados
                /// </summary>
                protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Si selecciono estado mostrar datos correspondientes
                        if (this.cboEstado.SelectedValue != "-1")
                        {
                            //Cargar los datos del estado        
                            this.CargarDatosEstadoModalAvanzar(Convert.ToInt32(this.cboEstado.SelectedValue), Convert.ToInt32(this.hdfFlujoID.Value));
                        }
                        else
                        {
                            //Recargar datos del modal
                            this.CargarDatosModalAvanzar(Convert.ToInt32(this.hdfAutoridadAmbiental.Value), Convert.ToInt64(this.hdfActoID.Value), Convert.ToInt64(this.hdfPersonaID.Value));

                            //Limpiar modal
                            this.LimpiarModalAvanzar(false);
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cboEstado_SelectedIndexChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información para el nuevo estado");
                    }
                }

            #endregion


            #region chkEnviarDireccion

                /// <summary>
                /// Evento que se ejecuta al cambiar selección de envío de dirección. Muestra controles según configuración
                /// </summary>
                protected void chkEnviarDireccion_CheckedChanged(object sender, EventArgs e)
                {
                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        if (this.chkEnviarDireccion.Checked)
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosDirecciones(this.grdDirecciones, null, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            this.ConsultarDirecciones((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion"), Convert.ToInt64(this.hdfPersonaID.Value));

                            //Mostrar listado de direcciones
                            this.dvListadoDirecciones.Visible = true;
                        }
                        else
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosDirecciones(this.grdDirecciones, null, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Ocultar grilla
                            this.dvListadoDirecciones.Visible = false;
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: chkEnviarDireccion_CheckedChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de direcciones");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreo.Visible && this.chkEnviarCorreo.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }

            #endregion


            #region chkEnviarCorreo

                /// <summary>
                /// Evento que se ejecuta al cambiar selección de envío de correo. Muestra controles según configuración
                /// </summary>
                protected void chkEnviarCorreo_CheckedChanged(object sender, EventArgs e)
                {
                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        if (this.chkEnviarCorreo.Checked)
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosCorreos(this.grdCorreos, null, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            this.ConsultarCorreos((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo"), Convert.ToInt64(this.hdfPersonaID.Value), this.hdfNumeroVitalActo.Value);

                            //Mostrar div de correo
                            this.dvListaCorreos.Visible = true;                            
                            this.dvTextoCorreo.Visible = true;

                            //Verificar si se muestra adjuntos
                            if (this._objConfiguracionEstadoFlujo.AnexaAdjunto)
                                this.dvAdjuntos.Visible = true;
                            else
                                this.dvAdjuntos.Visible = false;
                        }
                        else
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosCorreos(this.grdCorreos, null, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Ocultar div de correo
                            this.dvListaCorreos.Visible = false;
                            this.dvAdjuntos.Visible = false;
                            this.dvTextoCorreo.Visible = false;
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: chkEnviarCorreo_CheckedChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de correos");
                    }
                    finally
                    {
                        if (this.dvEnviarDireccion.Visible && this.chkEnviarDireccion.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }

            #endregion


            #region grdDirecciones

                /// <summary>
                /// Evento que se ejcuta al seleccionar un grupo de dirección
                /// </summary>
                protected void cboGrdTipoDireccion_SelectedIndexChanged(object sender, EventArgs e)
                {
                    string strSeleccion = "";

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar selección
                        strSeleccion = ((DropDownList)sender).SelectedValue;

                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Seleccionar tipo de grupo
                        ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdTipoDireccion")).SelectedValue = strSeleccion;

                        if (strSeleccion != "-1")
                        {
                            //Cargar datos grupos de correos de la persona
                            this.ConsultarDirecciones((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion"), Convert.ToInt64(this.hdfPersonaID.Value));
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cboGrdTipoDireccion_SelectedIndexChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando listados de direcciones");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreo.Visible && this.chkEnviarCorreo.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }


                /// <summary>
                /// Evento que se ejecuta al seleccionar una dirección. Carga información adicional de la dirección
                /// </summary>
                protected void cboGrdDireccion_SelectedIndexChanged(object sender, EventArgs e)
                {
                    string strSeleccionDireccion = "";
                    string strDireccionPertenece = "";
                    string strDepartamento = "";
                    string strMunicipio = "";

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar selecciones
                        strSeleccionDireccion = ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).SelectedValue;

                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                        //Cargar desplegables footer
                        this.ConsultarDirecciones((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion"), Convert.ToInt64(this.hdfPersonaID.Value));

                        //Seleccionar
                        ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).SelectedValue = strSeleccionDireccion;

                        if (strSeleccionDireccion != "-1")
                        {
                            //Cargar datos de dirección
                            this.ConsultarDireccion(Convert.ToInt64(this.hdfPersonaID.Value), Convert.ToInt64(strSeleccionDireccion), ref strDireccionPertenece, ref strDepartamento, ref strMunicipio);

                            //Cargar datos en literales
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlPertenece")).Text = strDireccionPertenece;
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlDepartamento")).Text = strDepartamento;
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlMunicipio")).Text = strMunicipio;
                        }
                        else
                        {
                            //Limpiar campos
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlPertenece")).Text = "";
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlDepartamento")).Text = "";
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlMunicipio")).Text = "";
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cboGrdDireccion_SelectedIndexChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de la direccion");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreo.Visible && this.chkEnviarCorreo.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }


                /// <summary>
                /// Evento que adiciona una nueva dirección a la lista
                /// </summary>
                protected void lnkAdicionar_Click(object sender, EventArgs e)
                {
                    DireccionNotificacionEntity objDireccion = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar datos
                        objDireccion = new DireccionNotificacionEntity
                        {
                            DireccionID = Convert.ToInt64(((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).SelectedValue),
                            Pertenece = ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlPertenece")).Text,
                            Departamento = ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlDepartamento")).Text,
                            Municipio = ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlMunicipio")).Text,
                            Direccion = ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).SelectedItem.Text
                        };

                        //Cargar al listado
                        if (this._Direcciones == null)
                            this._Direcciones = new List<DireccionNotificacionEntity>();

                        //Si la dirección ya fue adicionada no ingresarla
                        if (((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.DireccionID == objDireccion.DireccionID).ToList().Count == 0)
                            ((List<DireccionNotificacionEntity>)this._Direcciones).Add(objDireccion);

                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkAdicionar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando dirección al listado");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreo.Visible && this.chkEnviarCorreo.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }


                /// <summary>
                /// Evento que elimina una dirección del listado
                /// </summary>
                protected void lnkEliminar_Click(object sender, EventArgs e)
                {
                    DireccionNotificacionEntity objDireccion = null;
                    long lngDireccionID = 0;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar el identificador de la dirección
                        lngDireccionID = Convert.ToInt64(((LinkButton)sender).CommandArgument);

                        //Obtener registro a eliminar
                        objDireccion = ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.DireccionID == lngDireccionID).ToList()[0];

                        //Eliminar registro del listado
                        ((List<DireccionNotificacionEntity>)this._Direcciones).Remove(objDireccion);

                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                        //Inicializar
                        ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkEliminar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando eliminando dirección del listado");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreo.Visible && this.chkEnviarCorreo.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }

            #endregion

            
            #region grdCorreos

                /// <summary>
                /// Evento que elimina un correo electronico del listado
                /// </summary>
                protected void lnkEliminarCorreo_Click(object sender, EventArgs e)
                {
                    CorreoNotificacionEntity objCorreo = null;
                    string strCorreo = "";

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar el identificador de la dirección
                        strCorreo = ((LinkButton)sender).CommandArgument;

                        //Obtener registro a eliminar
                        objCorreo = ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.Correo == strCorreo).ToList()[0];

                        //Eliminar registro del listado
                        ((List<CorreoNotificacionEntity>)this._Correos).Remove(objCorreo);

                        //Recarga listado
                        this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkEliminar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando eliminando dirección del listado");
                    }
                    finally
                    {
                        if (this.dvEnviarDireccion.Visible && this.chkEnviarDireccion.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }

                /// <summary>
                /// Evento que adiciona un correo electronico del listado
                /// </summary>
                protected void lnkAdicionarCorreo_Click(object sender, EventArgs e)
                {
                    CorreoNotificacionEntity objCorreo = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar datos
                        objCorreo = new CorreoNotificacionEntity
                        {
                            Grupo = ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdGrupoCorreo")).SelectedItem.Text,
                            Correo = ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).SelectedValue,
                        };

                        //Crear listado
                        if (this._Correos == null)
                            this._Correos = new List<CorreoNotificacionEntity>();

                        //Si el correo ya fue adicionado no ingresarlo
                        if (((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.Correo == objCorreo.Correo).ToList().Count == 0)
                            ((List<CorreoNotificacionEntity>)this._Correos).Add(objCorreo);

                        //Recarga listado
                        this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkAdicionarCorreo_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando dirección de correo electrónico al listado");
                    }
                    finally
                    {
                        if (this.dvEnviarDireccion.Visible && this.chkEnviarDireccion.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }
                

            #endregion


            #region cmdAvanzar

                /// <summary>
                /// Evento que avanza la notificación
                /// </summary>
                protected void cmdAvanzar_Click(object sender, EventArgs e)
                {
                    try
                    {
                        if (Page.IsValid)
                        {
                            //Avanzar la notificación
                            this.AvanzarNotificacion();

                            //Limpiar modal
                            this.LimpiarModalAvanzar();

                            //Cargar triggers
                            this.CargarTriggersGrillaNotificaciones();

                            //Actualizar modal
                            this.upnlAvanzarEstado.Update();

                            //Cerrar el modal
                            this.mpeAvanzarEstado.Hide();

                            //Recargar información en pantalla
                            this.grdNotificaciones.PageIndex = 0;
                            this.BuscarInformacionActosDministrativos();
                        }
                        
                    }
                    catch(NotificacionException){
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error registrando el avance de la actividad");
                    }
                    catch(Exception exc){
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cmdAvanzar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error realizando el avance de la actividad");
                    }
                }

            #endregion


            #region rptDatosUsuariosAvanzar

                /// <summary>
                /// Evento que se ejecuta al cargar el listado de usuarios
                /// </summary>
                protected void rptDatosUsuariosAvanzar_ItemDataBound(object sender, RepeaterItemEventArgs e)
                {
                    Notificacion objNotificacion = null;

                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        //Ocultar todos los controles                        
                        ((HtmlGenericControl)e.Item.FindControl("dvRptListadoDirecciones")).Visible = false;
                        ((HtmlGenericControl)e.Item.FindControl("dvRptEnviarCorreo")).Visible = false;
                        ((HtmlGenericControl)e.Item.FindControl("dvRptListaCorreos")).Visible = false;
                        ((HtmlGenericControl)e.Item.FindControl("dvRptAdjuntos")).Visible = false;
                        ((HtmlGenericControl)e.Item.FindControl("dvRptAdjuntarActoAdministrativo")).Visible = false;
                        ((HtmlGenericControl)e.Item.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible = false;                        
                        ((HtmlGenericControl)e.Item.FindControl("dvRptTextoCorreo")).Visible = false;
                        ((HtmlGenericControl)e.Item.FindControl("dvRptReferenciaRecepcion")).Visible = false;
                        ((HtmlGenericControl)e.Item.FindControl("dvRptFechaRecepcion")).Visible = false;
                        ((RequiredFieldValidator)e.Item.FindControl("rfvRptReferenciaRecepcion")).Enabled = false;

                        //Verificar si se entrega acto administrativo
                        if (this._objConfiguracionEstadoFlujo.PermitiAnexarActoAdministrativo)
                        {
                            ((HtmlGenericControl)e.Item.FindControl("dvRptAdjuntarActoAdministrativo")).Visible = true;
                            ((CheckBox)e.Item.FindControl("chkRptAdjuntarActoAdministrativo")).Checked = true;

                            //Crear objeto notificacion
                            objNotificacion = new Notificacion();

                            if (this._objConfiguracionEstadoFlujo.PermitiAnexarConceptosActoAdministrativo && objNotificacion.ActoTieneConceptosAsociados(Convert.ToInt64(this.hdfActoID.Value)))
                            {
                                ((HtmlGenericControl)e.Item.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible = true;
                                ((CheckBox)e.Item.FindControl("chkRptAdjuntarConceptosActoAdministrativo")).Checked = false;
                            }
                            else
                            {
                                ((HtmlGenericControl)e.Item.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible = true;
                                ((CheckBox)e.Item.FindControl("chkRptAdjuntarConceptosActoAdministrativo")).Checked = false;
                            }
                        }
                        else
                        {
                            ((HtmlGenericControl)e.Item.FindControl("dvRptAdjuntarActoAdministrativo")).Visible = false;
                            ((CheckBox)e.Item.FindControl("chkRptAdjuntarActoAdministrativo")).Checked = false;
                            ((HtmlGenericControl)e.Item.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible = false;
                            ((CheckBox)e.Item.FindControl("chkRptAdjuntarConceptosActoAdministrativo")).Checked = false;
                        }

                        //Mostrar envia notificacion fisica
                        ((HtmlGenericControl)e.Item.FindControl("dvRptEnviarDireccion")).Visible = this._objConfiguracionEstadoFlujo.EnviaNotificacionFisica;
                        
                        //Mostrar envío de correo
                        ((HtmlGenericControl)e.Item.FindControl("dvRptEnviarCorreo")).Visible = this._objConfiguracionEstadoFlujo.EnviaCorreoAvanceManual;

                        //Mostrar captura de recpción de información
                        if (this._objConfiguracionEstadoFlujo.SolicitarReferenciaRecepcionNotificacion)
                        {
                            ((HtmlGenericControl)e.Item.FindControl("dvRptReferenciaRecepcion")).Visible = true;
                            ((HtmlGenericControl)e.Item.FindControl("dvRptFechaRecepcion")).Visible = true;

                            if (this._objConfiguracionEstadoFlujo.ReferenciaRecepcionNotificacionObligatoria)
                                ((RequiredFieldValidator)e.Item.FindControl("rfvRptReferenciaRecepcion")).Enabled = true;
                        }
                        else
                        {
                            ((HtmlGenericControl)e.Item.FindControl("dvRptReferenciaRecepcion")).Visible = false;
                            ((HtmlGenericControl)e.Item.FindControl("dvRptFechaRecepcion")).Visible = false;
                            ((RequiredFieldValidator)e.Item.FindControl("rfvRptReferenciaRecepcion")).Enabled = false;
                        }
                    }

                }


                /// <summary>
                /// Evento que se activa al dar clic en check box. Activa o desactiva campos de direcciones
                /// </summary>
                protected void chkRptEnviarDireccion_CheckedChanged(object sender, EventArgs e)
                {
                    RepeaterItem objRepeater = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((CheckBox)sender).NamingContainer);

                        if (((CheckBox)sender).Checked)
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), null, Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                            //Cargar desplegables footer
                            ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                            //Cargar el validation group
                            ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdTipoDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                            ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                            ((LinkButton)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("lnkRptAdicionar")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                            

                            //Mostrar dirección
                            ((HtmlGenericControl)objRepeater.FindControl("dvRptListadoDirecciones")).Visible = true;

                        }
                        else
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), null, Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                            //Ocultar grilla
                            ((HtmlGenericControl)objRepeater.FindControl("dvRptListadoDirecciones")).Visible = false;
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: chkRptEnviarDireccion_CheckedChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de direcciones");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptDirecciones")).ClientID);
                    }
                }


                /// <summary>
                /// Evento que carga el listado de direcciones de acuerdo al grupo seleccionado
                /// </summary>
                protected void cboRptGrdTipoDireccion_SelectedIndexChanged(object sender, EventArgs e)
                {
                    RepeaterItem objRepeater = null;
                    string strSeleccion = "";

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((GridView)((GridViewRow)((DropDownList)sender).NamingContainer).NamingContainer).NamingContainer);

                        //Cargar selección
                        strSeleccion = ((DropDownList)sender).SelectedValue;

                        //Recarga listado
                        if (this._Direcciones != null)
                            this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));
                        else
                            this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), null, Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Cargar desplegables footer
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Cargar el validation group
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdTipoDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((LinkButton)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("lnkRptAdicionar")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;

                        //Seleccionar tipo de grupo
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdTipoDireccion")).SelectedValue = strSeleccion;

                        if (strSeleccion != "-1")
                        {
                            //Cargar datos grupos de correos de la persona
                            this.ConsultarDirecciones((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion"), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cboRptGrdTipoDireccion_SelectedIndexChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando listados de direcciones");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptDirecciones")).ClientID);
                    }
                }


                /// <summary>
                /// Evento que carga la información de la dirección
                /// </summary>
                protected void cboRptGrdDireccion_SelectedIndexChanged(object sender, EventArgs e)
                {
                    RepeaterItem objRepeater = null;
                    string strGrupo = "";
                    string strSeleccionDireccion = "";
                    string strDireccion = "";
                    string strDepartamento = "";
                    string strMunicipio = "";

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((GridView)((GridViewRow)((DropDownList)sender).NamingContainer).NamingContainer).NamingContainer);

                        //Cargar selecciones
                        strGrupo = ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdTipoDireccion")).SelectedValue;
                        strSeleccionDireccion = ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).SelectedValue;

                        //Recarga listado
                        if (this._Direcciones != null)
                            this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));
                        else
                            this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), null, Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Cargar desplegables footer
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Cargar el validation group
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdTipoDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((LinkButton)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("lnkRptAdicionar")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;

                        //Cargar datos grupos de correos de la persona
                        this.ConsultarDirecciones((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion"), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Seleccionar
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdTipoDireccion")).SelectedValue = strGrupo;
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).SelectedValue = strSeleccionDireccion;

                        if (strSeleccionDireccion != "-1")
                        {
                            //Cargar datos de dirección
                            this.ConsultarDireccion(Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value), Convert.ToInt64(strSeleccionDireccion), ref strDireccion, ref strDepartamento, ref strMunicipio);

                            //Cargar datos en literales
                            ((Literal)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("ltlPertenece")).Text = strDireccion;
                            ((Literal)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("ltlDepartamento")).Text = strDepartamento;
                            ((Literal)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("ltlMunicipio")).Text = strMunicipio;
                        }
                        else
                        {
                            //Limpiar campos
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlPertenece")).Text = "";
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlDepartamento")).Text = "";
                            ((Literal)this.grdDirecciones.FooterRow.FindControl("ltlMunicipio")).Text = "";
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cboRptGrdDireccion_SelectedIndexChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de la direccion");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptDirecciones")).ClientID);
                    }
                }


                /// <summary>
                /// Evento que adiciona una nueva dirección
                /// </summary>
                protected void lnkRptAdicionar_Click(object sender, EventArgs e)
                {
                    RepeaterItem objRepeater = null;
                    DireccionNotificacionEntity objDireccion = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((GridView)((GridViewRow)((LinkButton)sender).NamingContainer).NamingContainer).NamingContainer);

                        //Cargar datos
                        objDireccion = new DireccionNotificacionEntity
                        {
                            PersonaID = Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value),
                            DireccionID = Convert.ToInt64(((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).SelectedValue),
                            Pertenece = ((Literal)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("ltlPertenece")).Text,
                            Departamento = ((Literal)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("ltlDepartamento")).Text,
                            Municipio = ((Literal)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("ltlMunicipio")).Text,
                            Direccion = ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).SelectedItem.Text
                        };

                        //Cargar al listado
                        if (this._Direcciones == null)
                            this._Direcciones = new List<DireccionNotificacionEntity>();

                        //Si la dirección ya fue adicionada no ingresarla
                        if (((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.DireccionID == objDireccion.DireccionID && direccion.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList().Count == 0)
                            ((List<DireccionNotificacionEntity>)this._Direcciones).Add(objDireccion);

                        //Recarga listado
                        this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Cargar desplegables footer
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Cargar el validation group
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdTipoDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((LinkButton)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("lnkRptAdicionar")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkRptAdicionar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando dirección al listado");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptDirecciones")).ClientID);
                    }
                }


                /// <summary>
                /// Evento que elimina una dirección
                /// </summary>
                protected void lnkRptEliminar_Click(object sender, EventArgs e)
                {
                    RepeaterItem objRepeater = null;
                    DireccionNotificacionEntity objDireccion = null;
                    long lngDireccionID = 0;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((GridView)((GridViewRow)((LinkButton)sender).NamingContainer).NamingContainer).NamingContainer);

                        //Cargar el identificador de la dirección
                        lngDireccionID = Convert.ToInt64(((LinkButton)sender).CommandArgument);

                        //Obtener registro a eliminar
                        objDireccion = ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.DireccionID == lngDireccionID && direccion.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList()[0];

                        //Eliminar registro del listado
                        ((List<DireccionNotificacionEntity>)this._Direcciones).Remove(objDireccion);

                        //Recarga listado
                        this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Cargar desplegables footer
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Cargar el validation group
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdTipoDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((LinkButton)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("lnkRptAdicionar")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkEliminar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando eliminando dirección del listado");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptDirecciones")).ClientID);
                    }
                }


                /// <summary>
                /// Evento que se ejecuta al cambiar selección de envío de correo. Muestra controles según configuración
                /// </summary>
                protected void chkRptEnviarCorreo_CheckedChanged(object sender, EventArgs e)
                {
                    RepeaterItem objRepeater = null;
                    Notificacion objNotificacion = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((CheckBox)sender).NamingContainer);

                        if (((CheckBox)sender).Checked)
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosCorreos((GridView)objRepeater.FindControl("grdRptCorreos"), null, Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                            //Cargar desplegables footer
                            ((DropDownList)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("cboRptGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                            //Cargar el validation group
                            ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("rfvRptGrdGrupoCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;
                            ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("rfvRptGrdCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;
                            ((LinkButton)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("lnkRptAdicionarCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;

                            //Mostrar div de correo
                            ((HtmlGenericControl)objRepeater.FindControl("dvRptListaCorreos")).Visible = true;
                            ((HtmlGenericControl)objRepeater.FindControl("dvRptTextoCorreo")).Visible = true;

                            //Verificar si se muestra adjuntos
                            if (this._objConfiguracionEstadoFlujo.AnexaAdjunto)
                                ((HtmlGenericControl)objRepeater.FindControl("dvRptAdjuntos")).Visible = true;
                            else
                                ((HtmlGenericControl)objRepeater.FindControl("dvRptAdjuntos")).Visible = false;
                        }
                        else
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosCorreos(this.grdCorreos, null, Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                            //Ocultar div de correo
                            ((HtmlGenericControl)objRepeater.FindControl("dvRptListaCorreos")).Visible = false;
                            ((HtmlGenericControl)objRepeater.FindControl("dvRptAdjuntos")).Visible = false;
                            ((HtmlGenericControl)objRepeater.FindControl("dvRptTextoCorreo")).Visible = false;
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar fileupload
                        if (objRepeater != null)
                            this.LimpiarFileUpload((AjaxControlToolkit.AsyncFileUpload)objRepeater.FindControl("fuplRptAdjunto"));

                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: chkRptEnviarCorreo_CheckedChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de correos");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptCorreos")).ClientID);
                    }
                }


                /// <summary>
                /// Evento que adiciona un nuevo correo
                /// </summary>
                protected void lnkRptAdicionarCorreo_Click(object sender, EventArgs e)
                {
                    RepeaterItem objRepeater = null;
                    CorreoNotificacionEntity objCorreo = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((GridView)((GridViewRow)((LinkButton)sender).NamingContainer).NamingContainer).NamingContainer);

                        //Cargar datos
                        objCorreo = new CorreoNotificacionEntity
                        {
                            PersonaID = Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value),
                            Grupo = ((DropDownList)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("cboRptGrdGrupoCorreo")).SelectedItem.Text,
                            Correo = ((DropDownList)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("cboRptGrdCorreo")).SelectedValue,
                        };

                        //Crear listado
                        if (this._Correos == null)
                            this._Correos = new List<CorreoNotificacionEntity>();

                        //Si el correo ya fue adicionado no ingresarlo
                        if (((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.Correo == objCorreo.Correo && correo.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList().Count == 0)
                            ((List<CorreoNotificacionEntity>)this._Correos).Add(objCorreo);

                        //Recarga listado
                        this.CargarDatosCorreos(((GridView)objRepeater.FindControl("grdRptCorreos")), ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Cargar desplegables footer
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("cboRptGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Cargar el validation group
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("rfvRptGrdGrupoCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("rfvRptGrdCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;
                        ((LinkButton)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("lnkRptAdicionarCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkRptAdicionarCorreo_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando dirección de correo electrónico al listado");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptCorreos")).ClientID);
                    }
                }


                /// <summary>
                /// Evento que elimina un correo
                /// </summary>
                protected void lnkRptEliminarCorreo_Click(object sender, EventArgs e)
                {
                    RepeaterItem objRepeater = null;
                    CorreoNotificacionEntity objCorreo = null;
                    string strCorreo = "";

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((GridView)((GridViewRow)((LinkButton)sender).NamingContainer).NamingContainer).NamingContainer);

                        //Cargar el identificador de la dirección
                        strCorreo = ((LinkButton)sender).CommandArgument;

                        //Obtener registro a eliminar
                        objCorreo = ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.Correo == strCorreo && correo.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList()[0];

                        //Eliminar registro del listado
                        ((List<CorreoNotificacionEntity>)this._Correos).Remove(objCorreo);

                        //Recarga listado
                        this.CargarDatosCorreos((GridView)objRepeater.FindControl("grdRptCorreos"), ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Cargar desplegables footer
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("cboRptGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeAvanzarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkRptEliminarCorreo_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando eliminando dirección del listado");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptCorreos")).ClientID);
                    }
                }


                /// <summary>
                /// Evento que habilita o deshabilita el check de adjuntar concepto
                /// </summary>
                protected void chkRptAdjuntarActoAdministrativo_CheckedChanged(object sender, EventArgs e)
                {
                    Notificacion objNotificacion = null;
                    RepeaterItem objRepeater = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar repeater
                        objRepeater = (RepeaterItem)(((CheckBox)sender).NamingContainer);

                        if (((CheckBox)sender).Checked)
                        {
                            //Crear objetoNotificacion
                            objNotificacion = new Notificacion();

                            if (this._objConfiguracionEstadoFlujo.PermitiAnexarConceptosActoAdministrativo && objNotificacion.ActoTieneConceptosAsociados(Convert.ToInt64(this.hdfActoID.Value)))
                            {
                                ((HtmlGenericControl)objRepeater.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible = true;
                                ((CheckBox)objRepeater.FindControl("chkRptAdjuntarConceptosActoAdministrativo")).Checked = false;
                            }
                            else
                            {
                                ((HtmlGenericControl)objRepeater.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible = false;
                                ((CheckBox)objRepeater.FindControl("chkRptAdjuntarConceptosActoAdministrativo")).Checked = false;
                            }
                        }
                        else
                        {
                            ((HtmlGenericControl)objRepeater.FindControl("dvRptAdjuntarConceptosActoAdministrativo")).Visible = false;
                            ((CheckBox)objRepeater.FindControl("chkRptAdjuntarConceptosActoAdministrativo")).Checked = false;
                        }

                        //Actualizar el panel del modal
                        this.upnlAvanzarEstado.Update();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalAvanzar();

                        //Actualizar modal
                        this.upnlAvanzarEstado.Update();

                        //Cerrar el modal
                        this.mpeAvanzarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: chkRptAdjuntarActoAdministrativo_CheckedChanged -> Error Inesperado: " + exc.StackTrace);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando datos conceptos");
                    }
                    finally
                    {
                        this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptCorreos")).ClientID);
                    }
                }

            #endregion


            #region chkAdjuntarActoAdministrativo

                /// <summary>
                /// Evento que habilita o deshabilita el check de adjuntar concepto
                /// </summary>
                protected void chkAdjuntarActoAdministrativo_CheckedChanged(object sender, EventArgs e)
                {
                    Notificacion objNotificacion = null;

                    //Comprobar check de acto administrativo
                    if (this.chkAdjuntarActoAdministrativo.Checked)
                    {
                        //Crear objetoNotificacion
                        objNotificacion = new Notificacion();

                        //Verificar si se pueden solicitar conceptos
                        if (this._objConfiguracionEstadoFlujo.PermitiAnexarConceptosActoAdministrativo && objNotificacion.ActoTieneConceptosAsociados(Convert.ToInt64(this.hdfActoID.Value)))
                        {
                            this.dvAdjuntarConceptosActoAdministrativo.Visible = true;
                            this.chkAdjuntarConceptosActoAdministrativo.Checked = false;
                        }
                        else
                        {
                            this.dvAdjuntarConceptosActoAdministrativo.Visible = false;
                            this.chkAdjuntarConceptosActoAdministrativo.Checked = false;
                        }
                    }
                    else
                    {
                        this.dvAdjuntarConceptosActoAdministrativo.Visible = false;
                        this.chkAdjuntarConceptosActoAdministrativo.Checked = false;
                    }

                    //Actualizar modal
                    this.upnlAvanzarEstado.Update();
                }

            #endregion


        #endregion


        #region imgEditar

            /// <summary>
            /// Evento que despliega el modal de edición del estado
            /// </summary>
            protected void imgEditar_Click(object sender, ImageClickEventArgs e)
            {
                long lngEstadoPersonaActoID = 0;

                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    //Limpiar modal
                    this.LimpiarModalEditar();

                    //Cargar datos
                    lngEstadoPersonaActoID = long.Parse(((ImageButton)sender).CommandArgument.ToString());
                    this.CargarDatosModalEditar(lngEstadoPersonaActoID);

                    //Actualizar el panel del modal
                    this.upnlEditarEstado.Update();

                    //Mostrar pantalla modal
                    this.mpeEditarEstado.Show();

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgEditar_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando información para edición del estado");
                }
            }

        #endregion


        #region imgEliminar

            /// <summary>
            /// Evento que elimina la información de un estado
            /// </summary>
            protected void imgEliminar_Click(object sender, ImageClickEventArgs e)
            {
                long lngEstadoPersonaActoID = 0;

                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    //Cargar identificador
                    lngEstadoPersonaActoID = long.Parse(((ImageButton)sender).CommandArgument.ToString());

                    //Eliminar el estado
                    this.EliminarEstado(lngEstadoPersonaActoID);

                    //Recargar información en pantalla
                    this.grdNotificaciones.PageIndex = 0;
                    this.BuscarInformacionActosDministrativos();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgEliminar_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error eliminando información del estado");
                }
            }

        #endregion


        #region Modal Editar


            #region cmdCancelarEditar

                /// <summary>
                /// Evento que cierra modal de edición
                /// </summary>
                protected void cmdCancelarEditar_Click(object sender, EventArgs e)
                {

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Cargar triggers
                        this.CargarTriggersGrillaNotificaciones();

                        //Actualizar el panel del modal
                        this.upnlEditarEstado.Update();

                        //Ocultar pantalla modal
                        this.mpeEditarEstado.Hide();
                        
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cmdCancelarEditar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error realizando la cancelación del proceso de edición de estado");
                    }
                }

            #endregion

            
            #region chkEnviarDireccionEditar

                /// <summary>
                /// Evento que se ejecuta al cambiar selección de envío de dirección. Muestra controles según configuración
                /// </summary>
                protected void chkEnviarDireccionEditar_CheckedChanged(object sender, EventArgs e)
                {
                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        if (this.chkEnviarDireccionEditar.Checked)
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosDirecciones(this.grdDireccionesEditar, null, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Mostrar listado de direcciones
                            this.dvListadoDireccionesEditar.Visible = true;
                        }
                        else
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosDirecciones(this.grdDireccionesEditar, null, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Ocultar grilla
                            this.dvListadoDireccionesEditar.Visible = false;
                        }

                        //Actualizar el panel del modal
                        this.upnlEditarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeEditarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: chkEnviarDireccionEditar_CheckedChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de direcciones");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreoEditar.Visible && this.chkEnviarCorreoEditar.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreosEditar, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }

            #endregion


            #region chkEnviarCorreoEditar

                /// <summary>
                /// Evento que se ejecuta al cambiar selección de envío de correo. Muestra controles según configuración
                /// </summary>
                protected void chkEnviarCorreoEditar_CheckedChanged(object sender, EventArgs e)
                {
                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        if (this.chkEnviarCorreoEditar.Checked)
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosCorreos(this.grdCorreosEditar, null, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                            //Mostrar div de correo
                            this.dvListaCorreosEditar.Visible = true;
                            this.dvTextoCorreoEditar.Visible = true;

                            //Verificar si se muestra adjuntos
                            if (this._objConfiguracionEstadoFlujo.AnexaAdjunto)
                                this.dvAdjuntosEditar.Visible = true;
                            else
                                this.dvAdjuntosEditar.Visible = false;
                        }
                        else
                        {
                            //Inicializar información de direcciones
                            this.CargarDatosCorreos(this.grdCorreosEditar, null, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Ocultar div de correo
                            this.dvListaCorreosEditar.Visible = false;
                            this.dvAdjuntosEditar.Visible = false;
                            this.dvTextoCorreoEditar.Visible = false;
                        }

                        //Actualizar el panel del modal
                        this.upnlEditarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeEditarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: chkEnviarCorreoEditar_CheckedChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de correos");
                    }
                    finally
                    {
                        if (this.dvEnviarDireccionEditar.Visible && this.chkEnviarDireccionEditar.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosDirecciones(this.grdDireccionesEditar, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));
                        }
                    }
                }

            #endregion


            #region grdDireccionesEditar

                /// <summary>
                /// Evento que se ejecuta al seleccionar una dirección. Carga información adicional de la dirección
                /// </summary>
                protected void cboGrdDireccionEditar_SelectedIndexChanged(object sender, EventArgs e)
                {
                    string strSeleccionDireccion = "";
                    string strDireccion = "";
                    string strDepartamento = "";
                    string strMunicipio = "";

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar selecciones
                        strSeleccionDireccion = ((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar")).SelectedValue;

                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDireccionesEditar, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Cargar datos grupos de correos de la persona
                        this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Seleccionar
                        ((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar")).SelectedValue = strSeleccionDireccion;

                        if (strSeleccionDireccion != "-1")
                        {
                            //Cargar datos de dirección
                            this.ConsultarDireccion(Convert.ToInt64(this.hdfPersonaIDEditar.Value), Convert.ToInt64(strSeleccionDireccion), ref strDireccion, ref strDepartamento, ref strMunicipio);

                            //Cargar datos en literales
                            ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlPertenece")).Text = strDireccion;
                            ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlDepartamento")).Text = strDepartamento;
                            ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlMunicipio")).Text = strMunicipio;
                        }
                        else
                        {
                            //Limpiar campos
                            ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlPertenece")).Text = "";
                            ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlDepartamento")).Text = "";
                            ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlMunicipio")).Text = "";
                        }

                        //Actualizar el panel del modal
                        this.upnlEditarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeEditarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cboGrdDireccionEditar_SelectedIndexChanged -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando información de la direccion");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreoEditar.Visible && this.chkEnviarCorreoEditar.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreosEditar, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }


                /// <summary>
                /// Evento que adiciona una nueva dirección a la lista
                /// </summary>
                protected void lnkAdicionarDireccionEditar_Click(object sender, EventArgs e)
                {
                    DireccionNotificacionEntity objDireccion = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar datos
                        objDireccion = new DireccionNotificacionEntity
                        {
                            DireccionID = Convert.ToInt64(((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar")).SelectedValue),
                            Pertenece = ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlPertenece")).Text,
                            Departamento = ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlDepartamento")).Text,
                            Municipio = ((Literal)this.grdDireccionesEditar.FooterRow.FindControl("ltlMunicipio")).Text,
                            Direccion = ((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar")).SelectedItem.Text
                        };

                        //Cargar al listado
                        if (this._Direcciones == null)
                            this._Direcciones = new List<DireccionNotificacionEntity>();

                        //Si la dirección ya fue adicionada no ingresarla
                        if (((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.DireccionID == objDireccion.DireccionID).ToList().Count == 0)
                            ((List<DireccionNotificacionEntity>)this._Direcciones).Add(objDireccion);

                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDireccionesEditar, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Cargar desplegables footer
                        this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Actualizar el panel del modal
                        this.upnlEditarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeEditarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkAdicionarDireccionEditar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando dirección al listado");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreoEditar.Visible && this.chkEnviarCorreoEditar.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreosEditar, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }


                /// <summary>
                /// Evento que elimina una dirección del listado
                /// </summary>
                protected void lnkEliminarDireccionEditar_Click(object sender, EventArgs e)
                {
                    DireccionNotificacionEntity objDireccion = null;
                    long lngDireccionID = 0;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar el identificador de la dirección
                        lngDireccionID = Convert.ToInt64(((LinkButton)sender).CommandArgument);

                        //Obtener registro a eliminar
                        objDireccion = ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.DireccionID == lngDireccionID).ToList()[0];

                        //Eliminar registro del listado
                        ((List<DireccionNotificacionEntity>)this._Direcciones).Remove(objDireccion);

                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDireccionesEditar, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Inicializar
                        this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Actualizar el panel del modal
                        this.upnlEditarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeEditarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkEliminarDireccionEditar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando eliminando dirección del listado");
                    }
                    finally
                    {
                        if (this.dvEnviarCorreoEditar.Visible && this.chkEnviarCorreoEditar.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosCorreos(this.grdCorreosEditar, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                    }
                }

            #endregion

            
            #region grdCorreosEditar

                
                /// <summary>
                /// Evento que elimina un correo electronico del listado
                /// </summary>
                protected void lnkEliminarCorreoEditar_Click(object sender, EventArgs e)
                {
                    CorreoNotificacionEntity objCorreo = null;
                    string strCorreo = "";

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar el identificador de la dirección
                        strCorreo = ((LinkButton)sender).CommandArgument;

                        //Obtener registro a eliminar
                        objCorreo = ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.Correo == strCorreo).ToList()[0];

                        //Eliminar registro del listado
                        ((List<CorreoNotificacionEntity>)this._Correos).Remove(objCorreo);

                        //Recarga listado
                        this.CargarDatosCorreos(this.grdCorreosEditar, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Actualizar el panel del modal
                        this.upnlEditarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeEditarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkEliminarCorreoEditar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando eliminando dirección del listado");
                    }
                    finally
                    {
                        if (this.dvEnviarDireccion.Visible && this.chkEnviarDireccion.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosDirecciones(this.grdDireccionesEditar, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));
                        }
                    }
                }

                /// <summary>
                /// Evento que adiciona un correo electronico del listado
                /// </summary>
                protected void lnkAdicionarCorreoEditar_Click(object sender, EventArgs e)
                {
                    CorreoNotificacionEntity objCorreo = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Cargar datos
                        objCorreo = new CorreoNotificacionEntity
                        {
                            Grupo = ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdGrupoCorreoEditar")).SelectedItem.Text,
                            Correo = ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).SelectedValue,
                        };

                        //Crear listado
                        if (this._Correos == null)
                            this._Correos = new List<CorreoNotificacionEntity>();

                        //Si el correo ya fue adicionado no ingresarlo
                        if (((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.Correo == objCorreo.Correo).ToList().Count == 0)
                            ((List<CorreoNotificacionEntity>)this._Correos).Add(objCorreo);

                        //Recarga listado
                        this.CargarDatosCorreos(this.grdCorreosEditar, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Actualizar el panel del modal
                        this.upnlEditarEstado.Update();

                        //Mostrar pantalla modal
                        this.mpeEditarEstado.Show();
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: lnkAdicionarCorreoEditar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error cargando dirección de correo electrónico al listado");
                    }
                    finally
                    {
                        if (this.dvEnviarDireccion.Visible && this.chkEnviarDireccion.Checked)
                        {
                            //Recarga listado
                            this.CargarDatosDirecciones(this.grdDireccionesEditar, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                            //Cargar desplegables footer
                            this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));
                        }
                    }
                }
                

            #endregion


            #region cmdEditar

                /// <summary>
                /// Evento que realiza la edición de la información de un estado
                /// </summary>
                protected void cmdEditar_Click(object sender, EventArgs e)
                {
                    try
                    {
                        if (Page.IsValid)
                        {
                            //Edita el estado
                            this.EditarNotificacion();

                            //Limpiar modal
                            this.LimpiarModalEditar();

                            //Cargar triggers
                            this.CargarTriggersGrillaNotificaciones();

                            //Actualizar modal
                            this.upnlEditarEstado.Update();

                            //Cerrar el modal
                            this.mpeEditarEstado.Hide();

                            //Recargar información en pantalla
                            this.grdNotificaciones.PageIndex = 0;
                            this.BuscarInformacionActosDministrativos();
                        }
                       
                    }
                    catch (NotificacionException)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error editando información del estado");
                    }
                    catch (Exception exc)
                    {
                        //Limpiar modal
                        this.LimpiarModalEditar();

                        //Actualizar modal
                        this.upnlEditarEstado.Update();

                        //Cerrar el modal
                        this.mpeEditarEstado.Hide();

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cmdEditar_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error realizando el edición del estado");
                    }
                }

            #endregion


            #region chkAdjuntarActoAdministrativoEditar

                /// <summary>
                /// Evento que habilita o deshaibilita los controles relacionados con el envío de acto administrativo
                /// </summary>
                protected void chkAdjuntarActoAdministrativoEditar_CheckedChanged(object sender, EventArgs e)
                {
                    if (this.chkAdjuntarActoAdministrativoEditar.Checked)
                    {
                        if (this._objConfiguracionEstadoFlujo.PermitiAnexarConceptosActoAdministrativo)
                        {
                            this.dvAdjuntarConceptosActoAdministrativoEditar.Visible = true;
                            this.chkAdjuntarConceptosActoAdministrativoEditar.Checked = false;
                        }
                        else
                        {
                            this.dvAdjuntarConceptosActoAdministrativoEditar.Visible = false;
                            this.chkAdjuntarConceptosActoAdministrativoEditar.Checked = false;
                        }
                    }
                    else
                    {
                        this.dvAdjuntarConceptosActoAdministrativoEditar.Visible = false;
                        this.chkAdjuntarConceptosActoAdministrativoEditar.Checked = false;
                    }
                }

            #endregion


        #endregion


        #region Modal Ver Adjuntos


            #region cmdCerrarVerAdjuntos

                /// <summary>
                /// Evento que cierra modal de ver adjuntos
                /// </summary>
                protected void cmdCerrarVerAdjuntos_Click(object sender, EventArgs e)
                {
                    //Limpiar el modal
                    this.LimpiarModalVerAdjuntos();

                    //Recargar panel
                    this.upnlVerDocumentosAdjuntos.Update();

                    //Cerrar modal
                    this.mpeVerDocumentosAdjuntos.Hide();
                }

            #endregion


            #region imgDescargarDocumentoAdjuntoVer

                /// <summary>
                /// Evento que descarga archivo
                /// </summary>
                protected void imgDescargarDocumentoAdjuntoVer_Click(object sender, ImageClickEventArgs e)
                {
                    SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
                    string[] lstURL = null;
                    string[] lstLlaves = null;
                    string strEnlace = "";
                    EnlaceDocumentoSila objEnlace = null;
                    EnlaceDocumentoSilaEntity objEnlaceEntity = null;

                    try
                    {
                        //Ocultar Mensajes
                        this.OcultarMensaje();

                        //Validar que exista información
                        if (((ImageButton)sender).CommandArgument != null)
                        {
                            //Verificar el origen del archivo
                            if( ((ImageButton)sender).CommandArgument.ToString().StartsWith("CON-SILA@")){

                                //Crear objeto de parametros
                                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

                                //Cargar datos url
                                lstURL = ((ImageButton)sender).CommandArgument.ToString().Split('@');

                                if (lstURL != null && lstURL.Length > 1)
                                {
                                    //Cargar llaves
                                    lstLlaves = lstURL[1].Split('_');

                                    //Cargar datos
                                    objEnlaceEntity = new EnlaceDocumentoSilaEntity
                                    {
                                        EnlaceID = lstURL[1],
                                        ActoNotificacionID = Convert.ToInt64(lstLlaves[1]),
                                        PersonaID = Convert.ToInt64(lstLlaves[2]),
                                        EstadoPersonaID = Convert.ToInt64(lstLlaves[3]),
                                        DocumentoID = Convert.ToInt32(lstLlaves[0]),
                                        Llave = EnDecript.Encriptar(lstURL[1]),
                                        FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_SILA_NOTIFICACION")))
                                    };

                                    //Almacenar enlace
                                    objEnlace = new EnlaceDocumentoSila();
                                    objEnlace.CrearEnlace(objEnlaceEntity);

                                    //Obtener URL                                
                                    strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_VER_CONCEPTOS_SILA_NOTIFICACION") + objEnlaceEntity.Llave;

                                    //Redireccionar
                                    Response.Redirect(strEnlace, false);
                                }
                                else
                                    throw new Exception("No se encontro información URL " + ((ImageButton)sender).CommandArgument.ToString());
                            }
                            else if (((ImageButton)sender).CommandArgument.ToString().StartsWith("CON-SILAMC@"))
                            {

                                //Crear objeto de parametros
                                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

                                //Cargar datos url
                                lstURL = ((ImageButton)sender).CommandArgument.ToString().Split('@');

                                if (lstURL != null && lstURL.Length > 1)
                                {
                                    //Cargar llaves
                                    lstLlaves = lstURL[1].Split('_');

                                    //Cargar datos
                                    objEnlaceEntity = new EnlaceDocumentoSilaEntity
                                    {
                                        EnlaceID = lstURL[1],
                                        ActoNotificacionID = Convert.ToInt64(lstLlaves[1]),
                                        PersonaID = Convert.ToInt64(lstLlaves[2]),
                                        EstadoPersonaID = Convert.ToInt64(lstLlaves[3]),
                                        DocumentoID = Convert.ToInt32(lstLlaves[0]),
                                        Llave = EnDecript.Encriptar(lstURL[1]),
                                        FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_SILAMC_NOTIFICACION")))
                                    };

                                    //Almacenar enlace
                                    objEnlace = new EnlaceDocumentoSila();
                                    objEnlace.CrearEnlace(objEnlaceEntity);

                                    //Obtener URL                                
                                    strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_VER_CONCEPTOS_SILAMC_NOTIFICACION") + objEnlaceEntity.Llave;

                                    //Redireccionar
                                    Response.Redirect(strEnlace, false);
                                }
                                else
                                    throw new Exception("No se encontro información URL " + ((ImageButton)sender).CommandArgument.ToString());
                            }
                            else
                            {
                                System.IO.FileInfo targetFile = new System.IO.FileInfo(((ImageButton)sender).CommandArgument.ToString());
                                this.Response.Clear();
                                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                                this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                                this.Response.ContentType = "application/octet-stream";
                                this.Response.ContentType = "application/base64";
                                this.Response.WriteFile(targetFile.FullName);
                                this.Response.WriteFile(((ImageButton)sender).CommandArgument.ToString());
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: imgAdjuntos_Click -> Error Inesperado: " + exc.Message);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se presento error descargando el archivo indicado");
                    }
                }

            #endregion

        #endregion


    #endregion


    #region Custom Validator

                /// <summary>
        /// Verifica que la fecha del estado no sea menor a la fecha de estado actual
        /// </summary>
        protected void cvFechaEstadoNotificacion_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime objFechaActual = default(DateTime);
            DateTime objFechaEstado = default(DateTime);

            try
            {
                if (!string.IsNullOrWhiteSpace(this.txtFechaEstadoNotificacion.Text) && !string.IsNullOrWhiteSpace(this.hdfFechaEstadoActual.Value))
                {
                    //Cargar fechas
                    objFechaEstado = Convert.ToDateTime(this.txtFechaEstadoNotificacion.Text);
                    objFechaActual = Convert.ToDateTime(this.hdfFechaEstadoActual.Value);


                    //Verificar fecha
                    if (objFechaEstado < objFechaActual)
                    {
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- La fecha del estado no puede ser menor a la fecha del estado actual')", true);
                    }
                    else
                    {
                        //Verificar que la fecha del estado no se amayor a la del día
                        if (objFechaEstado > DateTime.Now)
                        {
                            args.IsValid = false;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- La fecha del estado no puede ser mayor a la fecha actual')", true);
                        }
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cvFechaEstadoNotificacion_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando fechas de estados");

                //Retornar como falso
                args.IsValid = false;
            }
            finally
            {
                if (this.dvEnviarDireccion.Visible && this.chkEnviarDireccion.Checked)
                {
                    //Recarga listado
                    this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                    //Cargar desplegables footer
                    ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                }

                if (this.dvEnviarCorreo.Visible && this.chkEnviarCorreo.Checked)
                {
                    //Recarga listado
                    this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                    //Cargar desplegables footer
                    ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                }
            }
        }


        /// <summary>
        /// Valida que la fecha del estado no sea menor que la fecha del estado anterior
        /// </summary>
        protected void cvFechaEstadoNotificacionEditar_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime objFechaAnterior = default(DateTime);
            DateTime objFechaEstado = default(DateTime);

            try
            {
                if (!string.IsNullOrWhiteSpace(this.txtFechaEstadoNotificacionEditar.Text) && !string.IsNullOrWhiteSpace(this.hdfFechaEstadoAnterior.Value))
                {
                    //Cargar fechas
                    objFechaEstado = Convert.ToDateTime(this.txtFechaEstadoNotificacionEditar.Text);
                    objFechaAnterior = Convert.ToDateTime(this.hdfFechaEstadoAnterior.Value);


                    //Verificar fecha
                    if (objFechaEstado < objFechaAnterior)
                    {
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- La fecha del estado no puede ser menor a la fecha del estado anterior')", true);
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cvFechaEstadoNotificacionEditar_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando fechas de estados durante edición");

                //Retornar como falso
                args.IsValid = false;
            }   
        }


        /// <summary>
        /// Verifica que se ingrese información de direcciones
        /// </summary>
        protected void cvGrillaDirecciones_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (this.dvEnviarDireccion.Visible && this.chkEnviarDireccion.Checked)
                {
                    if(this._Direcciones == null || ((List<DireccionNotificacionEntity>)this._Direcciones).Count == 0){
                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Marcar como incorrecto
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Debe ingresar por lo menos una dirección')", true);
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cvGrillaDirecciones_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando el ingreso de direcciones");

                //Retornar como falso
                args.IsValid = false;
            }
            finally
            {
                if (this.dvEnviarCorreo.Visible && this.chkEnviarCorreo.Checked)
                {
                    //Recarga listado
                    this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                    //Cargar desplegables footer
                    ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                }
            }
        }


        /// <summary>
        /// Validar que se incluyan correos
        /// </summary>
        protected void cvGrdCorreos_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (this.dvEnviarCorreo.Visible && this.chkEnviarCorreo.Checked)
                {
                    if (this._Correos == null || ((List<CorreoNotificacionEntity>)this._Correos).Count == 0)
                    {
                        //Recarga listado
                        this.CargarDatosCorreos(this.grdCorreos, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaID.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdCorreos.FooterRow.FindControl("cboGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Marcar como incorrecto
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Debe ingresar por lo menos una dirección de correo electrónico')", true);
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cvGrillaDirecciones_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando el ingreso de direcciones");

                //Retornar como falso
                args.IsValid = false;
            }
            finally
            {
                if (this.dvEnviarDireccion.Visible && this.chkEnviarDireccion.Checked)
                {
                    //Recarga listado
                    this.CargarDatosDirecciones(this.grdDirecciones, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaID.Value));

                    //Cargar desplegables footer
                    ((DropDownList)this.grdDirecciones.FooterRow.FindControl("cboGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                }
            }
        }


        /// <summary>
        /// Verifica que se ingrese información de direcciones
        /// </summary>
        protected void cvGrillaDireccionesEditar_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (this.dvEnviarDireccionEditar.Visible && this.chkEnviarDireccionEditar.Checked)
                {
                    if (this._Direcciones == null || ((List<DireccionNotificacionEntity>)this._Direcciones).Count == 0)
                    {
                        //Recarga listado
                        this.CargarDatosDirecciones(this.grdDireccionesEditar, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Cargar desplegables footer
                        this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Marcar como incorrecto
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Debe ingresar por lo menos una dirección')", true);
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cvGrillaDireccionesEditar_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando el ingreso de direcciones");

                //Retornar como falso
                args.IsValid = false;
            }
            finally
            {
                if (this.dvEnviarCorreoEditar.Visible && this.chkEnviarCorreoEditar.Checked)
                {
                    //Recarga listado
                    this.CargarDatosCorreos(this.grdCorreosEditar, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                    //Cargar desplegables footer
                    ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));
                }
            }
        }


        /// <summary>
        /// Validar que se incluyan correos
        /// </summary>
        protected void cvGrdCorreosEditar_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (this.dvEnviarCorreoEditar.Visible && this.chkEnviarCorreoEditar.Checked)
                {
                    if (this._Correos == null || ((List<CorreoNotificacionEntity>)this._Correos).Count == 0)
                    {
                        //Recarga listado
                        this.CargarDatosCorreos(this.grdCorreosEditar, (List<CorreoNotificacionEntity>)this._Correos, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                        //Cargar desplegables footer
                        ((DropDownList)this.grdCorreosEditar.FooterRow.FindControl("cboGrdCorreoEditar")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Marcar como incorrecto
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Debe ingresar por lo menos una dirección de correo electrónico')", true);
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cvGrdCorreosEditar_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando el ingreso de direcciones");

                //Retornar como falso
                args.IsValid = false;
            }
            finally
            {
                if (this.dvEnviarDireccionEditar.Visible && this.chkEnviarDireccionEditar.Checked)
                {
                    //Recarga listado
                    this.CargarDatosDirecciones(this.grdDireccionesEditar, (List<DireccionNotificacionEntity>)this._Direcciones, Convert.ToInt64(this.hdfPersonaIDEditar.Value));

                    //Cargar desplegables footer
                    this.ConsultarDirecciones((DropDownList)this.grdDireccionesEditar.FooterRow.FindControl("cboGrdDireccionEditar"), Convert.ToInt64(this.hdfPersonaIDEditar.Value));
                }
            }
        }


        /// <summary>
        /// Verifica que se ingrese información de direcciones
        /// </summary>
        protected void cvRptGrillaDirecciones_ServerValidate(object source, ServerValidateEventArgs args)
        {
            RepeaterItem objRepeater = null;
            List<DireccionNotificacionEntity> objDirecciones = null;

            try
            {
                //Cargar repeater
                objRepeater = (RepeaterItem)((CustomValidator)source).NamingContainer;

                if (((HtmlGenericControl)objRepeater.FindControl("dvRptEnviarDireccion")).Visible && ((CheckBox)objRepeater.FindControl("chkRptEnviarDireccion")).Checked)
                {
                    //Cargar datos del listado
                    if (this._Direcciones != null)
                        objDirecciones = ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList();

                    if (objDirecciones == null || objDirecciones.Count == 0)
                    {
                        if (this._Direcciones != null)
                            this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), ((List<DireccionNotificacionEntity>)this._Direcciones).Where(direccion => direccion.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));
                        else
                            this.CargarDatosDirecciones((GridView)objRepeater.FindControl("grdRptDirecciones"), null, Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Cargar desplegables footer
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("cboRptGrdDireccion")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Cargar el validation group
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdTipoDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("rfvRptGrdDireccion")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;
                        ((LinkButton)((GridView)objRepeater.FindControl("grdRptDirecciones")).FooterRow.FindControl("lnkRptAdicionar")).ValidationGroup = "RptAvanzarModalDirecciones" + objRepeater.ItemIndex;

                        //Marcar como incorrecto
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Debe ingresar por lo menos una dirección para el usuario " + (objRepeater.ItemIndex + 1).ToString() + "')", true);
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cvRptGrillaDirecciones_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando el ingreso de direcciones");

                //Retornar como falso
                args.IsValid = false;
            }
            finally
            {
                this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptDirecciones")).ClientID);
            }
        }

        /// <summary>
        /// Verifica que se ingrese información de correos
        /// </summary>
        protected void cvRptGrdCorreos_ServerValidate(object source, ServerValidateEventArgs args)
        {
            RepeaterItem objRepeater = null;
            List<CorreoNotificacionEntity> objCorreos = null;

            try
            {
                //Cargar repeater
                objRepeater = (RepeaterItem)((CustomValidator)source).NamingContainer;

                if (((HtmlGenericControl)objRepeater.FindControl("dvRptEnviarCorreo")).Visible && ((CheckBox)objRepeater.FindControl("chkRptEnviarCorreo")).Checked)
                {
                    //Cargar datos del listado
                    if (this._Correos != null)
                        objCorreos = ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList();

                    if (objCorreos == null || objCorreos.Count == 0)
                    {
                        //Recarga listado
                        if (this._Correos != null)
                            this.CargarDatosCorreos((GridView)objRepeater.FindControl("grdRptCorreos"), ((List<CorreoNotificacionEntity>)this._Correos).Where(correo => correo.PersonaID == Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value)).ToList(), Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));
                        else
                            this.CargarDatosCorreos((GridView)objRepeater.FindControl("grdRptCorreos"), null, Convert.ToInt64(((HiddenField)objRepeater.FindControl("hdfRptPersonaID")).Value));

                        //Cargar desplegables footer
                        ((DropDownList)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("cboRptGrdCorreo")).Items.Insert(0, new ListItem("Seleccione...", "-1"));

                        //Cargar el validation group
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("rfvRptGrdGrupoCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;
                        ((RequiredFieldValidator)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("rfvRptGrdCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;
                        ((LinkButton)((GridView)objRepeater.FindControl("grdRptCorreos")).FooterRow.FindControl("lnkRptAdicionarCorreo")).ValidationGroup = "RptAvanzarModalCorreos" + objRepeater.ItemIndex;

                        //Marcar como incorrecto
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Debe ingresar por lo menos una dirección de correo electrónico para el usuario " + (objRepeater.ItemIndex + 1).ToString() + "')", true);
                    }
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_ConsultaNotificaciones :: cvRptGrdCorreos_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando el ingreso de direcciones");

                //Retornar como falso
                args.IsValid = false;
            }
            finally
            {
                this.RefrescarGrillasRepeater(((GridView)objRepeater.FindControl("grdRptCorreos")).ClientID);
            }
        }

    #endregion

}