using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Generico;
using Silpa.Workflow;
using SILPA.Comun;
using SILPA.AccesoDatos.DAA;
using System.Globalization;

public partial class PermisosAmbientales_Liquidacion_FormularioPagoAutoliquidacion : System.Web.UI.Page
{

    #region Objetos

        /// <summary>
        /// Identificador de la instancia del proceso
        /// </summary>
        private int _intIDProcessInstance
        {
            get
            {
                return (int)ViewState["IDProcessInstance"];
            }
            set
            {
                ViewState["IDProcessInstance"] = value;
            }
        }

        /// <summary>
        /// Identificador de la instancia del proceso
        /// </summary>
        private int _intCobroSolicitudLiquidacionID
        {
            get
            {
                return (int)ViewState["CobroSolicitudLiquidacionID"];
            }
            set
            {
                ViewState["CobroSolicitudLiquidacionID"] = value;
            }
        }

        /// <summary>
        /// Información del cobro
        /// </summary>
        private Cobro _objCobro
        {
            get
            {
                return (Cobro)ViewState["Cobro"];
            }
            set{
                ViewState["Cobro"] = value;
            }
        }


    #endregion


    #region Metodos Privados

        /// <summary>
        /// Ocultar mensaje mostrado
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.divMensajeError.Visible = false;
        }


        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.divMensajeError.Visible = true;
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
        /// Cargar la información del cobro
        /// </summary>
        private void CargarInformacionCobro()
        {
            ParametroDalc objParametroDalc = null;
            ParametroEntity objParametro = null;

            //Consultar la información del cobro
            this._objCobro = new Cobro();
            this._objCobro.ObtenerValoresObjetos(this._intIDProcessInstance);

            //Eliminar identificador de cobro de sesion
            Session["CobroSolicitudLiquidacionPSEID"] = null;

            this.lblTitulo.Text = "INFORMACIÓN DE PAGO LIQUIDACIÓN";
            this.ltlMensajeLiquidacion.Text = "A continuación se presenta la información de pago para cada uno de los conceptos que hace parte de la liquidación.";
            this.tblAutoliquidacion.Visible = true;
            this.ltlDescripcionPago.Text = this._objCobro.objCobro.ListaConceptoCobro[0].Descripcion;
            this.ltlValorPago.Text = string.Format("{0:C}", this._objCobro.objCobro.ListaConceptoCobro[0].Valor);
            this.dvCobroSeguimiento.Visible = false;
            this.ltlDescripcionPagoCS.Text = "";
            this.ltlExpedineteCS.Text = "";
            this.ltlValorPagoCS.Text = "";
            this.ltlFechaLimitePagoCS.Text = "";
            

            //Cargar id de instanca
            this.hdfInstancia.Value = this._intIDProcessInstance.ToString();

            //Verificar si hay permisos
            if (this._objCobro.objCobro.Permisos != null && this._objCobro.objCobro.Permisos.Count > 0)
            {
                this.divPermisos.Visible = true; 

                //Cargar permisos
                this.grdPermisos.DataSource = this._objCobro.objCobro.Permisos;
                this.grdPermisos.DataBind();
            }
            else{
                this.divPermisos.Visible = false;
            }

            //Verificar los estados de pago
            if (this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.APROBADA)
            {
                //Cargar complemento mensaje de error
                objParametro = new ParametroEntity();
                objParametro.IdParametro = -1;
                objParametro.NombreParametro = "PSE_Pago_Exitoso";
                objParametroDalc = new ParametroDalc();
                objParametroDalc.obtenerParametros(ref objParametro);

                //Cargar mensaje de error y ocultar controles
                this.ltlTituloPago.Visible = false;
                this.cboPagar.Visible = false;
                this.cmdVerFactura.Visible = true;
                this.ltlMensajePago.Text = "* Nota:<br />" + string.Format(objParametro.Parametro, this._objCobro.objCobro.NumReferencia, this._objCobro.objCobro.Transaccion);
                this.divMensajePago.Visible = true;
            }
            else if (this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.AVANZADO)
            {
                this.ltlTituloPago.Visible = false;
                this.cboPagar.Visible = false;
                this.cmdVerFactura.Visible = true;
                this.ltlMensajePago.Text = "* Nota:  <br />La actividad ya se encuentra en otro estado. Por favor actualice su bandeja de tareas.";
                this.divMensajePago.Visible = true;
            }
            else if (this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.TRANSACCION_FINALIZADA)
            {
                this.ltlTituloPago.Visible = false;
                this.cboPagar.Visible = false;
                this.cmdVerFactura.Visible = true;
                this.ltlMensajePago.Text = "* Nota:  <br />La actividad se encuentra finalizada. Por favor actualice su bandeja de tareas.";
                this.divMensajePago.Visible = true;
            }
            else if (this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.PENDIENTE)
            {
                //Cargar complemento mensaje de error
                objParametro = new ParametroEntity();
                objParametro.IdParametro = -1;
                objParametro.NombreParametro = "PSE_Pago_Pendiente";
                objParametroDalc = new ParametroDalc();
                objParametroDalc.obtenerParametros(ref objParametro);

                //Cargar mensaje de error y ocultar controles
                this.divPago.Visible = false;
                this.ltlMensajePago.Text = "* Nota:<br />" + string.Format(objParametro.Parametro, this._objCobro.objCobro.NumReferencia, this._objCobro.objCobro.Transaccion);
                this.divMensajePago.Visible = true;
            }
            else
            {
                //Verificar si la factura se encuentra vencida
                if (this._objCobro != null && DateTime.ParseExact(this._objCobro.objCobro.FechaVencimiento.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.Today)
                {
                    this.cmdFinalizarTarea.Visible = true;
                    this.ltlTituloPago.Visible = false;
                    this.cboPagar.Visible = false;
                    this.cmdVerFactura.Visible = false;
                    this.ltlMensajePago.Text = "";
                    this.divMensajePago.Visible = false;
                }
                else
                {
                    this.cmdFinalizarTarea.Visible = false;
                    this.ltlTituloPago.Visible = true;
                    this.cboPagar.Visible = true;
                    this.cmdVerFactura.Visible = false;
                    this.ltlMensajePago.Text = "";
                    this.divMensajePago.Visible = false;
                }
            }
        }


        /// <summary>
        /// Cargar la información del cobro de autoliquidacion
        /// </summary>
        private void CargarInformacionCobroAutoliquidacion()
        {
            ParametroDalc objParametroDalc = null;
            ParametroEntity objParametro = null;

            //Consultar la información del cobro
            this._objCobro = new Cobro();
            this._objCobro.ObtenerValoresCobroAutoliquidacion(this._intCobroSolicitudLiquidacionID);

            //Cargar identificador de cobro en sesion
            Session["CobroSolicitudLiquidacionPSEID"] = this._intCobroSolicitudLiquidacionID;

            //Cargar la información del cobro
            this.lblTitulo.Text = "INFORMACIÓN DE PAGO LIQUIDACIÓN";
            this.ltlMensajeLiquidacion.Text = "A continuación se presenta la información de pago para cada uno de los conceptos que hace parte de la liquidación.";
            this.tblAutoliquidacion.Visible = true;
            this.ltlDescripcionPago.Text = this._objCobro.objCobro.ListaConceptoCobro[0].Descripcion;
            this.ltlValorPago.Text = string.Format("{0:C}", this._objCobro.objCobro.ListaConceptoCobro[0].Valor).Replace(" ", "&nbsp;");
            this.dvCobroSeguimiento.Visible = false;
            this.ltlDescripcionPagoCS.Text = "";
            this.ltlExpedineteCS.Text = "";
            this.ltlValorPagoCS.Text = "";
            this.ltlFechaLimitePagoCS.Text = "";
            
            //Mostrar el listado de pagos
            if (this._objCobro.objCobro.OrigenCobro == OrigenCobroAttribute.GetStringValue(EnumOrigenCobro.AUTOLIQUIDACION))
            {
                this.cboPagar.Items.Clear();
                this.cboPagar.Items.Add(new ListItem("Seleccione....", "-1"));
                this.cboPagar.Items.Add(new ListItem("Débito Bancario PSE", "1"));
                this.cboPagar.Items.Add(new ListItem("Taquilla Banco", "2"));
            }
            else
            {
                this.cboPagar.Items.Clear();
                this.cboPagar.Items.Add(new ListItem("Seleccione....", "-1"));
                this.cboPagar.Items.Add(new ListItem("Débito Bancario PSE", "1"));
            }

            //Verificar si hay permisos
            if (this._objCobro.objCobro.OrigenCobro == OrigenCobroAttribute.GetStringValue(EnumOrigenCobro.AUTOLIQUIDACION) && this._objCobro.objCobro.Permisos != null && this._objCobro.objCobro.Permisos.Count > 0)
            {
                this.divPermisos.Visible = true;

                //Cargar permisos
                this.grdPermisos.DataSource = this._objCobro.objCobro.Permisos;
                this.grdPermisos.DataBind();
            }
            else
            {
                this.divPermisos.Visible = false;
            }

            //Verificar los estados de pago
            if (this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.APROBADA)
            {
                //Cargar complemento mensaje de error
                objParametro = new ParametroEntity();
                objParametro.IdParametro = -1;
                objParametro.NombreParametro = "PSE_Pago_Exitoso";
                objParametroDalc = new ParametroDalc();
                objParametroDalc.obtenerParametros(ref objParametro);

                //Cargar mensaje de error y ocultar controles
                this.ltlTituloPago.Visible = false;
                this.cboPagar.Visible = false;
                this.cmdVerFactura.Visible = true;
                this.ltlMensajePago.Text = "* Nota:<br />" + string.Format(objParametro.Parametro, this._objCobro.objCobro.NumReferencia, this._objCobro.objCobro.Transaccion);
                this.divMensajePago.Visible = true;
            }
            else if (this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.AVANZADO)
            {
                this.ltlTituloPago.Visible = false;
                this.cboPagar.Visible = false;
                this.cmdVerFactura.Visible = true;
                this.ltlMensajePago.Text = "* Nota:  <br />La actividad ya se encuentra en otro estado. Por favor actualice su bandeja de tareas.";
                this.divMensajePago.Visible = true;
            }
            else if (this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.TRANSACCION_FINALIZADA)
            {
                this.ltlTituloPago.Visible = false;
                this.cboPagar.Visible = false;
                this.cmdVerFactura.Visible = true;
                this.ltlMensajePago.Text = "* Nota:  <br />La actividad se encuentra finalizada. Por favor actualice su bandeja de tareas.";
                this.divMensajePago.Visible = true;
            }
            else if (this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.PENDIENTE)
            {
                //Cargar complemento mensaje de error
                objParametro = new ParametroEntity();
                objParametro.IdParametro = -1;
                objParametro.NombreParametro = "PSE_Pago_Pendiente";
                objParametroDalc = new ParametroDalc();
                objParametroDalc.obtenerParametros(ref objParametro);

                //Cargar mensaje de error y ocultar controles
                this.divPago.Visible = false;
                this.ltlMensajePago.Text = "* Nota:<br />" + string.Format(objParametro.Parametro, this._objCobro.objCobro.NumReferencia, this._objCobro.objCobro.Transaccion);
                this.divMensajePago.Visible = true;
            }
            else
            {
				//Verificar si la factura se encuentra vencida
                if (this._objCobro != null && DateTime.ParseExact(this._objCobro.objCobro.FechaVencimiento.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.Today)
                {
                    this.cmdFinalizarTarea.Visible = true;
                    this.ltlTituloPago.Visible = false;
                    this.cboPagar.Visible = false;
                    this.cmdVerFactura.Visible = false;
                    this.ltlMensajePago.Text = "";
                    this.divMensajePago.Visible = false;
                }
                else
                {
                    this.cmdFinalizarTarea.Visible = false;
                    this.ltlTituloPago.Visible = true;
                    this.cboPagar.Visible = true;
                    this.cmdVerFactura.Visible = false;
                    this.ltlMensajePago.Text = "";
                    this.divMensajePago.Visible = false;
                }
            }
        }
        

        /// <summary>
        /// Cargar la información de la página
        /// </summary>
        private void CargarPagina()
        {
            try{
                //Ocultar mensajes
                this.OcultarMensaje();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["IDProcessInstance"]))
                {
                    //Cargar identificador de la instancia
                    this._intIDProcessInstance = Convert.ToInt32(Request.QueryString["IDProcessInstance"]);

                    //Cargar información de cobro
                    this.CargarInformacionCobro();
                }
                else if (!string.IsNullOrWhiteSpace(Request.QueryString["AL"]))
                {
                    //Cargar identificador de la instancia
                    this._intCobroSolicitudLiquidacionID = Convert.ToInt32(Request.QueryString["AL"]);

                    //Cargar información de cobro
                    this.CargarInformacionCobroAutoliquidacion();
                }
                else
                {
                    throw new Exception("No se especifico identificador del proceso de cobro");
                }
            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PermisosAmbientales_Liquidacion_FormularioPagoAutoliquidacion :: CargarPagina -> Error Inesperado: " + exc.Message);

                this.MostrarMensaje("Se presento un error cargando la información del pago");
            }
        }


        /// <summary>
        /// Crear transacción pSE y direccionar  a la pagina correspondiente
        /// </summary>
        private void CrearTransaccionPSE()
        {
            ParametroEntity objParametroEntity = null;
            ParametroDalc objParametroDalc = null;

            //Cargar datos en sesion
            if (this._objCobro.objCobro.NumReferencia.Length > 18)
            {
                Session["TicketID"] = this._objCobro.objCobro.NumReferencia.Substring(0, 18);
            }
            else
            {
                Session["TicketID"] = this._objCobro.objCobro.NumReferencia;
            }
            Session["Amount"] = this._objCobro.objCobro.ListaConceptoCobro[0].Valor.ToString();

            //Se obtiene el codigo del servicio de pse de la tabla de parametros
            objParametroEntity = new ParametroEntity();
            objParametroDalc = new ParametroDalc();
            objParametroEntity.IdParametro = -1;
            objParametroEntity.NombreParametro = "CodigoServicioPSE";
            objParametroDalc.obtenerParametros(ref objParametroEntity);

            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            Session["ServiceCode"] = objParametroEntity.Parametro;
            Session["VATAmount"] = "0";
            Session["PaymentDescription"] = "Realiza Pago " + this._objCobro.objPersona.Identity.NumeroIdentificacion + "; Por Concepto de " + this._objCobro.objCobro.ListaConceptoCobro[0].Descripcion + "; Referencia de pago : " + this._objCobro.objCobro.NumReferencia + "; ";
            if (((string)Session["PaymentDescription"]).Length > 80)
                Session["PaymentDescription"] = ((string)Session["PaymentDescription"]).Substring(0, 79);
            Session["ReferenceNumber1"] = ipAddress;
            Session["ReferenceNumber2"] = this._objCobro.objPersona.Identity.TipoDocumentoIdentificacion.Nombre;
            Session["ReferenceNumber3"] = this._objCobro.objPersona.Identity.NumeroIdentificacion;
            Session["NumeroSilpa"] = this._objCobro.objCobro.NumSILPA;
            Session["PpeCertificateSubject"] = this._objCobro.objAutAmb.objAutoridadIdentity.Ppe_certificate_sub;
            Session["PpeUrl"] = this._objCobro.objAutAmb.objAutoridadIdentity.Ppe_url;
            Session["PpeCode"] = this._objCobro.objAutAmb.objAutoridadIdentity.Ppe_code;
            Session["RazonSocial"] = this._objCobro.objAutAmb.objAutoridadIdentity.Razon_social;
            Session["EstadoPSE"] = this._objCobro.objAutAmb.objAutoridadIdentity.Ppe_code;
            Session["TransaccionPSE"] = this._objCobro.objAutAmb.objAutoridadIdentity.Razon_social;
        }


        /// <summary>
        /// Avanza la actividad de un proceso directamente a otra actividad
        /// </summary>
        /// <param name="strNumeroSilpa">string: identificador del numero silpa</param>
        public void adelantarActividadToActividadBPM(string strNumeroSilpa, PagoElectronico opcionPago)
        {

            try
            {
                SMLog.Escribir(Severidad.Informativo, Page.AppRelativeVirtualPath.ToString() + ".adelantarActividadToActividadBPM.Inicio");
                SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
                SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
                _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, strNumeroSilpa);

                string casoProceso = string.Empty;

                Proceso _objProceso = new Proceso();

                if (Session["IDProcessInstance"] != null)
                {
                    ProcesoDalc pDalc = new ProcesoDalc();
                    _objProceso.PIdentity = pDalc.ObtenerObjProceso((long)Session["IDProcessInstance"]);
                }

                string condicion = string.Empty;
                _objProceso.ObtenerCondicionPagoElectronico();

                if (opcionPago == PagoElectronico.btnPago)
                {
                    condicion = _objProceso.PIdentity.CondicionPago;
                }

                if (opcionPago == PagoElectronico.btnImpresion)
                {
                    condicion = _objProceso.PIdentity.CondicionImprimirReciboPago;
                }

                ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
                int intIDProcessInstance = Convert.ToInt32(_solicitud.IdProcessInstance);
                string mensaje = servicioWorkflow.ValidarActividadActual(intIDProcessInstance, DatosSesion.Usuario, (long)ActividadSilpa.ConsultarPago);
                if (mensaje == "")
                    servicioWorkflow.FinalizarTarea(intIDProcessInstance, ActividadSilpa.ConsultarPago, DatosSesion.Usuario, condicion);
                else
                    SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: FormularioPagoAutoliquidacion::adelantarActividadToActividadBPM - strNumeroSilpa: " + (!string.IsNullOrEmpty(strNumeroSilpa) ? strNumeroSilpa : "null") + " - opcionPago: " + opcionPago.ToString() + "\n\n Error: " + mensaje, "BPM_VAL_CON");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SMLog.Escribir(Severidad.Informativo, Page.AppRelativeVirtualPath.ToString() + ".adelantarActividadToActividadBPM.Finalizo");
            }
        }

    #endregion


    #region Eventos

        /// <summary>
        /// Evento que se ejecuta al cargar la página
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Session["Usuario"] = "429";                    
                //this.CargarPagina();

                //Cargar datos pagina
                if (this.ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {
                    //Iniciliazar datos de la página
                    this.CargarPagina();
                }
            }
        }


        /// <summary>
        /// Evento que se ejecuta al dar clic en una de las formas de pago
        /// </summary>
        protected void cboPagar_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Verificar el tipo de pago
            if (this.cboPagar.SelectedValue == "1")
            {
                this.divPagoPSE.Visible = true;
                this.cmdVerFactura.Visible = false;
                this.cmdFinalizarTarea.Visible = false;
            }
            else if (this.cboPagar.SelectedValue == "2")
            {
                this.divPagoPSE.Visible = false;
                this.cmdVerFactura.Visible = true;
                this.cmdFinalizarTarea.Visible = true;
            }
            else
            {
                this.divPagoPSE.Visible = false;
                this.cmdVerFactura.Visible = false;
                this.cmdFinalizarTarea.Visible = false;
            }
        }

        
        /// <summary>
        /// Evento que se ejecuta al dar clic en el botón de pago PSE. Inicia una nueva sesión para realizar el pago
        /// </summary>
        protected void ImgPse_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Verificar que la transacción no se encuentre aprobada o pendiente
                if (this._objCobro.objCobro.EstadoCobro != (int)EnumEstadoCobro.APROBADA && this._objCobro.objCobro.EstadoCobro != (int)EnumEstadoCobro.PENDIENTE)
                {
                    //Cargar datos PSE
                    this.CrearTransaccionPSE();

                    //Transferir a pagina de PSE
                    Server.Transfer("~/PagoElectronico/ListaBancos.aspx");
                }
            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PermisosAmbientales_Liquidacion_FormularioPagoAutoliquidacion :: ImgPse_Click -> Error Inesperado: " + exc.Message);

                this.MostrarMensaje("Se presento un error creando la transacción PSE.");
            }
        }

        
        /// <summary>
        /// Evento que se ejecuta al dar clic en el boton de ver factura. Redirecciona a la página de factura
        /// </summary>
        protected void cmdVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                //Transferir a pagina de factura
                Server.Transfer("~/PermisosAmbientales/Liquidacion/FacturaPago.aspx?IDProcessInstance=" + this._intIDProcessInstance.ToString());
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PermisosAmbientales_Liquidacion_FormularioPagoAutoliquidacion :: cmdVerFactura_Click -> Error Inesperado: " + exc.Message);

                this.MostrarMensaje("Se presento un error cargando la factura.");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al dar clic en avanzar tarea. Finaliza y avanza la tarea
        /// </summary>
        protected void cmdFinalizarTarea_Click(object sender, EventArgs e)
        {
            CobroIdentity objCobroIdentity = null;

            try{
                //Adelantar la actividad en BPM
                this.adelantarActividadToActividadBPM(this._objCobro.objCobro.NumSILPA, PagoElectronico.btnImpresion);

                //Actualizar estados
                objCobroIdentity = new CobroIdentity();
                objCobroIdentity = this._objCobro.objCobro;
                objCobroIdentity.EstadoCobro = (int)EnumEstadoCobro.AVANZADO;
                this._objCobro.ActualizarCobroEstado(objCobroIdentity);

                //Cerrar ventana
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "cerrarVentana();", true);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PermisosAmbientales_Liquidacion_FormularioPagoAutoliquidacion :: cmdFinalizarTarea_Click -> Error Inesperado: " + exc.Message);

                this.MostrarMensaje("Se presento un error avanzando la tarea.");
            }
        }

    #endregion

    
}