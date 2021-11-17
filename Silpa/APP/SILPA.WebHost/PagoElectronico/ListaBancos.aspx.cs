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
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.DAA;
using SILPA.LogicaNegocio.Generico;
using Silpa.Workflow;
using SoftManagement.Log;
using PSEWebServicesClient3;

public partial class PagoElectronico_ListaBancos : System.Web.UI.Page
{
    /// <summary>
    /// Oculta los mensajes de error
    /// </summary>
    private void OcultarMensaje()
    {
        this.lblMensaje.Text = "";
        this.divMensajeError.Visible = false;
        this.upnlMensajeError.Update();
    }

    /// <summary>
    /// Mostrar mensaje de error en pantalla
    /// </summary>
    /// <param name="strMensaje"></param>
    private void MostrarMensaje(string strMensaje)
    {
        this.lblMensaje.Text = strMensaje;
        this.divMensajeError.Visible = true;
        this.upnlMensajeError.Update();
    }

    /// <summary>
    /// Ocultar mensaje de error parte inferior
    /// </summary>
    private void OcultarMensaje2()
    {
        this.lblMenasje2.Text = "";
        this.divMensajeError2.Visible = false;
        this.upnlMensajeError2.Update();
    }

    /// <summary>
    /// Mostrar mensaje de error parte inferior
    /// </summary>
    /// <param name="strMensaje">string con el mensaje de error</param>
    private void MostrarMensaje2(string strMensaje)
    {
        this.lblMenasje2.Text = strMensaje;
        this.divMensajeError2.Visible = true;
        this.upnlMensajeError2.Update();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
        SILPA.AccesoDatos.Generico.ParametroEntity _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
        SILPA.AccesoDatos.Generico.ParametroEntity _parametroPSEHabilitado = new SILPA.AccesoDatos.Generico.ParametroEntity();
        SILPA.AccesoDatos.Generico.ParametroEntity _parametroUsurioTestHabilitado = new SILPA.AccesoDatos.Generico.ParametroEntity();
        SILPA.AccesoDatos.Generico.ParametroEntity _parametroMensajePSEInhabilitado = new SILPA.AccesoDatos.Generico.ParametroEntity();
        _parametro.IdParametro = -1;
        _parametro.NombreParametro = "Error_Lista_Banco";
        _parametroDalc.obtenerParametros(ref _parametro);
        _parametroPSEHabilitado.IdParametro = -1;
        _parametroPSEHabilitado.NombreParametro = "TRANSACCION_PSE_ACTIVO";
        _parametroDalc.obtenerParametros(ref _parametroPSEHabilitado);
        _parametroUsurioTestHabilitado.IdParametro = -1;
        _parametroUsurioTestHabilitado.NombreParametro = "USUARIO_TEST_TRANSACCION_PSE_ACTIVO";
        _parametroDalc.obtenerParametros(ref _parametroUsurioTestHabilitado);
        _parametroMensajePSEInhabilitado.IdParametro = -1;
        _parametroMensajePSEInhabilitado.NombreParametro = "MENSAJE_TRANSACCION_PSE_ACTIVO";
        _parametroDalc.obtenerParametros(ref _parametroMensajePSEInhabilitado);
        string mensaje = string.Empty;
        string strPSEHabilitado = "";
        string strIDUsuarioTestHabilitado = "";

        try
        {
            if (!this.IsPostBack)
            {
                //Cargar datos pagina
                if (this.ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {

                    //Cargar indicador pagos habilitados
                    strPSEHabilitado = (_parametroPSEHabilitado != null && (!string.IsNullOrWhiteSpace(_parametroPSEHabilitado.Parametro)) ? _parametroPSEHabilitado.Parametro : "");
                    strIDUsuarioTestHabilitado = (_parametroUsurioTestHabilitado != null && (!string.IsNullOrWhiteSpace(_parametroUsurioTestHabilitado.Parametro)) ? _parametroUsurioTestHabilitado.Parametro : "");

                    if (!string.IsNullOrWhiteSpace(strPSEHabilitado) && strPSEHabilitado == "1" && (string.IsNullOrWhiteSpace(strIDUsuarioTestHabilitado) || strIDUsuarioTestHabilitado == (string)Session["Usuario"]))
                    {
                        //Inicializar mensajes
                        this.OcultarMensaje();
                        this.OcultarMensaje2();

                        this.btnIntTarde.Visible = false;
                        MainServices pse_ws = PPEController.GetPSEWebservice();
                        getbankListInformationType request = new getbankListInformationType();

                        //Session["RazonSocial"] = "JNS";
                        //Session["Amount"] = "20000";
                        //Session["VATAmount"] = "1600";
                        //Session["PaymentDescription"] = "DESCRIPCION";

                        //Inicializar listado de bancos
                        this.cboEntidadFinanciera.ClearSelection();
                        this.cboEntidadFinanciera.Items.Clear();
                        this.cboEntidadFinanciera.Items.Add(new ListItem("Seleccione...", "-1"));

                        //Consultar listado de bancos
                        request.entityCode = PPEController.GetConfiguration("PPE_CODE");
                        lblValorRazonSocial.Text = Session["RazonSocial"].ToString();
                        lblValorNit.Text = PPEController.GetConfiguration("PPE_CODE");
                        lblValorTotal.Text = Convert.ToDecimal(Session["Amount"]).ToString("$ #,##0.00");
                        lblValorIva.Text = Convert.ToDecimal(Session["VATAmount"]).ToString("$ #,##0.00");
                        lblValorDescripcion.Text = Session["PaymentDescription"].ToString();
                        mensaje += string.Format("request.entityCode:{0} ", request.entityCode);
                        getBankListResponseInformationType[] response = pse_ws.getBankList(request);
                        ArrayList banks = new ArrayList();
                        foreach (getBankListResponseInformationType bank in response)
                            banks.Add(new BankData(bank));

                        //Cargar el listado de bancos
                        this.cboEntidadFinanciera.DataSource = banks;
                        this.cboEntidadFinanciera.DataValueField = "bankcode";
                        this.cboEntidadFinanciera.DataTextField = "bankname";
                        this.cboEntidadFinanciera.DataBind();
                        this.cboEntidadFinanciera.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }
                    else
                    {
                        //LLenar datos de transaccion
                        lblValorRazonSocial.Text = Session["RazonSocial"].ToString();
                        lblValorNit.Text = PPEController.GetConfiguration("PPE_CODE");
                        lblValorTotal.Text = Convert.ToDecimal(Session["Amount"]).ToString("$ #,##0.00");
                        lblValorIva.Text = Convert.ToDecimal(Session["VATAmount"]).ToString("$ #,##0.00");
                        lblValorDescripcion.Text = Session["PaymentDescription"].ToString();

                        //Inhabilitar listados
                        this.cboTipoUsuario.Enabled = false;
                        this.cboEntidadFinanciera.Enabled = false;

                        //Escribir error
                        SMLog.Escribir(Severidad.Informativo, "Servio PSE deshabilitado strPSEHabilitado: " + strPSEHabilitado + " - strIDUsuarioTestHabilitado: " + strIDUsuarioTestHabilitado + " - ");

                        //Mostrar mensaje
                        this.divMensajeError.Visible = true;
                        lblMensaje.Text = _parametroMensajePSEInhabilitado.Parametro + ", por favor intente más tarde o comuniquese con " + _parametro.Parametro;
                        this.btnIntTarde.Visible = true;
                        this.divMensajeError2.Visible = false;

                        //Ocultar boton de pagar y mostrar el de intentar mas tarde
                        this.btnConfirmar.Visible = false;
                        this.btnIntTarde.Visible = true;
                    }                    
                }
            }
        }
        catch (Exception ex)
        {
            //Ocultar boton de pagar y mostrar el de intentar mas tarde
            this.btnConfirmar.Visible = false;
            this.btnIntTarde.Visible = true;

            //Escribir error y mostrar mensaje
            SMLog.Escribir(Severidad.Critico, string.Format("Pago Listar Bancos: {0} Trasa: {1}", ex.ToString(), mensaje));
            Mensaje.MostrarMensaje(this.Page, "No se pudo obtener la lista de bancos, por favor intente más tarde o comuníquese con " + _parametro.Parametro);
        }    
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        SILPA.AccesoDatos.Generico.ParametroDalc objParametroDalc = null;
        SILPA.AccesoDatos.Generico.ParametroEntity objParametro = null;
        Cobro objCobro = null;
        CobroIdentity objCobroIdentity = null;
        TransaccionPSEIdentity objTransaccion = null;
        DetalleTransaccionPSEIdentity objDetalleTransaccion = null;
        MainServices objPseWs = null;
        createTransactionPaymentInformationType objRequest = null;
        createTransactionPaymentResponseInformationType objResponse = null;
        string strMensajeMostrar = "";
        bool blnEsEmulacion = false;
        string strEstadoEmulacion = "";
        SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
        string strValor = "";

        try
        {
            //Ocultar mensajes
            this.OcultarMensaje();
            this.OcultarMensaje2();

            if (this.cboEntidadFinanciera.SelectedValue == "-1")
            {
                //Mostrar mensaje de error
                this.MostrarMensaje("Seleccione un banco.");
                this.MostrarMensaje2("Seleccione un banco.");
            }
            else if (Session["Amount"] == null || Session["TicketID"] == null)
            {
                // si no exite session redirecciona a la pagina de tareas
                Response.Redirect("~/BandejaTareas/BandejaTareas.aspx");
            }
            else
            {
                //Ocultar boton
                this.btnIntTarde.Visible = false;

                //Cargar configuracion de emulacion
                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                strValor = objParametrizacion.ObtenerValorParametroGeneral(-1, "ACTIVAR_EMULACION_PSE");
                if (!string.IsNullOrEmpty(strValor))
                {
                    blnEsEmulacion = (strValor == "0" ? false : true);
                    if (blnEsEmulacion)
                    {
                        strEstadoEmulacion = objParametrizacion.ObtenerValorParametroGeneral(-1, "ESTADO_EMULACION_PSE");
                        if (strEstadoEmulacion == null) strEstadoEmulacion = "";
                    }
                }
                else
                {
                    blnEsEmulacion = false;
                    strEstadoEmulacion = "";
                }

                //Cargar información de parametros
                objParametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
                objParametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
                objParametro.IdParametro = -1;
                objParametro.NombreParametro = "Error_Lista_Banco";
                objParametroDalc.obtenerParametros(ref objParametro);

                //Cargar informacion del cobro
                objCobro = new Cobro();
                objCobroIdentity = objCobro.ObtenerCobroTransaccion(Session["TicketID"].ToString());

                //Validar que se obtenga información del cobro
                if (objCobroIdentity != null && objCobroIdentity.IdCobro > 0)
                {
                    //Cargar información de la transacción
                    objTransaccion = new TransaccionPSEIdentity
                    {
                        CobroID = Decimal.ToInt64(objCobroIdentity.IdCobro),
                        CodigoPSEEntidad = PPEController.GetConfiguration("PPE_CODE"),
                        FechaSolicitud = DateTime.Now,
                        TipoPersona = cboTipoUsuario.SelectedItem.ToString().Trim(),
                        Banco = this.cboEntidadFinanciera.SelectedItem.Text.Trim(),
                        Valor = Convert.ToDecimal(Session["Amount"]),
                        Referencia1 = Session["ReferenceNumber1"].ToString(),
                        Referencia2 = Session["ReferenceNumber2"].ToString(),
                        Referencia3 = Session["ReferenceNumber3"].ToString(),
                        IPTransaccion = Session["ReferenceNumber1"].ToString(),
                        RazonSocialComercio = (Session["RazonSocial"] != null ? Session["RazonSocial"].ToString() : ""),
                        DescripcionTransaccion = Session["PaymentDescription"].ToString(),
                        UrlRetorno = PPEController.GetConfiguration("PPE_URL") + "?ticketID=" + Session["TicketID"].ToString()
                    };

                    //Crear transaccion
                    objTransaccion.TransaccionPSEID = objCobro.CrearTransaccionPSE(objTransaccion);

                    //Validar que se cree transacción
                    if(objTransaccion.TransaccionPSEID > 0)
                    {
                        //Validar si es emulación
                        if (!blnEsEmulacion)
                        {
                            //Cargar datos para creación de transacción en PSE                        
                            objRequest = new createTransactionPaymentInformationType();
                            objRequest.entityCode = objTransaccion.CodigoPSEEntidad;
                            objRequest.financialInstitutionCode = this.cboEntidadFinanciera.SelectedItem.Value.ToString();
                            objRequest.transactionValue = new AmountType();
                            objRequest.transactionValue.currencyISOcode = "ISO";
                            objRequest.transactionValue.Value = objTransaccion.Valor;
                            objRequest.serviceCode = Session["ServiceCode"].ToString();
                            objRequest.ticketId = Session["TicketID"].ToString();
                            objRequest.soliciteDate = objTransaccion.FechaSolicitud;
                            if (cboTipoUsuario.SelectedItem.ToString() == "PERSONAL")
                                objRequest.userType = userTypeListType.Item0;
                            else
                                objRequest.userType = userTypeListType.Item1;
                            objRequest.vatValue = new AmountType();
                            objRequest.vatValue.currencyISOcode = "ISO";
                            objRequest.vatValue.Value = Convert.ToDecimal(Session["VATAmount"]);
                            objRequest.referenceNumber = new String[3];
                            objRequest.referenceNumber[0] = objTransaccion.Referencia1;
                            objRequest.referenceNumber[1] = objTransaccion.Referencia2;
                            objRequest.referenceNumber[2] = objTransaccion.Referencia3;
                            objRequest.paymentDescription = objTransaccion.DescripcionTransaccion;
                            objRequest.entityurl = objTransaccion.UrlRetorno;

                            try
                            {
                                //Crear taransaccion en PSE
                                objPseWs = PPEController.GetPSEWebservice();
                                objResponse = objPseWs.createTransactionPayment(objRequest);
                            }
                            catch (Exception exc)
                            {
                                SMLog.Escribir(Severidad.Critico, "Error Creando Transación en PSE" + exc.ToString());
                                throw exc;
                            }
                        }
                        else
                        {
                            //Crear response
                            objResponse = new createTransactionPaymentResponseInformationType();

                            //Cargar codigo
                            if (strEstadoEmulacion == "SUCCESS")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.SUCCESS;
                            else if (strEstadoEmulacion == "FAIL_ENTITYNOTEXISTSORDISABLED")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_ENTITYNOTEXISTSORDISABLED;
                            else if (strEstadoEmulacion == "FAIL_BANKNOTEXISTSORDISABLED")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_BANKNOTEXISTSORDISABLED;
                            else if (strEstadoEmulacion == "FAIL_SERVICENOTEXISTS")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_SERVICENOTEXISTS;
                            else if (strEstadoEmulacion == "FAIL_INVALIDAMOUNT")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_INVALIDAMOUNT;
                            else if (strEstadoEmulacion == "FAIL_INVALIDSOLICITDATE")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_INVALIDSOLICITDATE;
                            else if (strEstadoEmulacion == "FAIL_BANKUNREACHEABLE")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_BANKUNREACHEABLE;
                            else if (strEstadoEmulacion == "FAIL_NOTCONFIRMEDBYBANK")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_NOTCONFIRMEDBYBANK;
                            else if (strEstadoEmulacion == "FAIL_CANNOTGETCURRENTCYCLE")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_CANNOTGETCURRENTCYCLE;
                            else if (strEstadoEmulacion == "FAIL_ACCESSDENIED")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_ACCESSDENIED;
                            else if (strEstadoEmulacion == "FAIL_TIMEOUT")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_TIMEOUT;
                            else if (strEstadoEmulacion == "FAIL_DESCRIPTIONNOTFOUND")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_DESCRIPTIONNOTFOUND;
                            else if (strEstadoEmulacion == "FAIL_EXCEEDEDLIMIT")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_EXCEEDEDLIMIT;
                            else if (strEstadoEmulacion == "FAIL_TRANSACTIONNOTALLOWED")
                                objResponse.returnCode = createTransactionPaymentResponseReturnCodeList.FAIL_TRANSACTIONNOTALLOWED;
                        }

                        //Verificar que se obtenga datos de response
                        if(objResponse != null)
                        {

                            //Validar que la transacción sea exitosa
                            if (objResponse.returnCode == createTransactionPaymentResponseReturnCodeList.SUCCESS)
                            {
                                if (!blnEsEmulacion)
                                {
                                    //Actualizar transacción en PSE
                                    objTransaccion.UrlPSE = objResponse.bankurl;
                                    objTransaccion.NumeroTransaccion = Convert.ToInt64(objResponse.trazabilityCode);
                                    objCobro.ActualizarTransaccionPSE(objTransaccion);

                                    //Inicializar el estado en pendiente
                                    objCobroIdentity = new CobroIdentity();
                                    objCobroIdentity.NumReferencia = Session["TicketID"].ToString();
                                    objCobroIdentity.Transaccion = Convert.ToInt32(objTransaccion.NumeroTransaccion);
                                    objCobroIdentity.OrigenLlamadoPSE = "LB";
                                    objCobroIdentity.ServicioLlamadoPSE = "createTransactionPaymentInformationType";
                                    objCobroIdentity.ResultadoServicioLlamadoPSE = objResponse.returnCode.ToString();
                                    objCobroIdentity.EstadoPSE = "";
                                    objCobroIdentity.EstadoCobro = 2;
                                    objCobroIdentity.FechaTransaccionBancaria = default(DateTime);
                                    objCobro.ActualizarCobroEstado(objCobroIdentity);

                                    //Redireccionar a pagina de PSE
                                    Response.Redirect(objTransaccion.UrlPSE, true);
                                }
                                else
                                {
                                    //Mostrar mensaje
                                    this.MostrarMensaje("Redirecciono a PSE");
                                }
                            }
                            else
                            {
                                //Ingresar registro fallido
                                objDetalleTransaccion = new DetalleTransaccionPSEIdentity
                                {
                                    TransaccionPSEID = objTransaccion.TransaccionPSEID,
                                    Origen = "LB",
                                    Servicio = "createTransactionPaymentInformationType",
                                    ResultadoPSE = objResponse.returnCode.ToString(),
                                    EstadoPSE = "",
                                    EstadoID = 4,
                                    FechaTransaccionBancaria = default(DateTime)
                                };
                                objCobro.CrearDetalleTransaccionPSE(objDetalleTransaccion);

                                //Mostrar mensaje de acuerdo a la falla
                                if (objResponse.returnCode == createTransactionPaymentResponseReturnCodeList.FAIL_BANKUNREACHEABLE)
                                {
                                    strMensajeMostrar = "La entidad financiera no puede ser contactada para iniciar la transacción, por favor seleccione otra o intente más tarde";
                                    this.btnIntTarde.Visible = true;
                                }
                                else if (objResponse.returnCode == createTransactionPaymentResponseReturnCodeList.FAIL_EXCEEDEDLIMIT)
                                {
                                    strMensajeMostrar = "El monto de la transacción excede los limites establecidos, por favor comuníquese con " + objParametro.Parametro;
                                }
                                else if (objResponse.returnCode == createTransactionPaymentResponseReturnCodeList.FAIL_SERVICENOTEXISTS)
                                {
                                    strMensajeMostrar = "El Servicio no esta creado o no esta asociado a una Cuenta";
                                }                                
                                else
                                {
                                    strMensajeMostrar = "No se pudo crear la transacción, por favor intente más tarde o comuníquese con " + objParametro.Parametro;
                                    this.btnIntTarde.Visible = true;                                    
                                }

                                //Escribir en el log
                                SMLog.Escribir(Severidad.Critico, "Pago, Lista Banco Error," + objResponse.returnCode.ToString());

                                //Mostrar error presentado en pantalla
                                this.MostrarMensaje(strMensajeMostrar);
                            }

                        }
                        else
                        {
                            throw new Exception("No se obtuvo respuesta por parte de PSE");
                        }

                    }
                    else
                    {
                        throw new Exception("No se creo transacción en repositorio local");
                    }

                }
                else
                {
                    throw new Exception("No se encontro información de cobro relacionado a la referencia: " + Session["TicketID"].ToString());
                }
            }

        }
        catch (Exception ex)
        {
            this.divMensajeError.Visible = true;
            lblMensaje.Text = "No se pudo crear la transacción, por favor intente más tarde o comuniquese con " + objParametro.Parametro;
            this.btnIntTarde.Visible = true;
            SMLog.Escribir(Severidad.Critico, "Error Creando Tranacción " + ex.ToString());
        }
        finally
        {
            this.upnlBancos.Update();
        }
    }


}

