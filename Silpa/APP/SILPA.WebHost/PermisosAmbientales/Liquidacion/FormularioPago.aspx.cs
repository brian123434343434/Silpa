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
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.DAA;
using SILPA.AccesoDatos.Generico;
using Silpa.Workflow;
using SoftManagement.Log;
using SILPA.Comun;

public partial class PermisosAmbientales_Liquidacion_FormularioPago : System.Web.UI.Page
{
    //private string descripcion;
    //Double _total = 0;

    public string proceso;

    public const string CondicionBtnPago =  "pagar";//"VITALCONPRO02LA-09";
    public const string CondicionBtnImprimir = "imprimir";//"VITALCONPRO02LA-06";

    //public enum PagoElectronico { btnPago = 1, btnImpresion = 2} 

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!this.IsPostBack)
        {
            Session["IDProcessInstance"] = long.Parse(Request.QueryString["IDProcessInstance"]);

            if (Request.QueryString["IDActivityInstance"] != null)
                Session["IDActivityInstance"] = int.Parse(Request.QueryString["IDActivityInstance"]);


            if (ValidacionToken() == false)
                Response.Redirect(@"..\..\Utilitario\MensajeValidacion.aspx");
            CargarDatos();
        }
    }

    /// <summary>
    /// Valida si hay token para el usuario que intenta acceder a la página, y si no ha expirado
    /// </summary>
    /// <returns>Verdadero si hay token válido para el usuario</returns>
    private bool ValidacionToken()
    {
        Session["IDForToken"] = Request.QueryString["IdRelated"];

        if (Session["IDForToken"] == null)
            return true;

        //System.IO.File.WriteAllText(@"c:\temp\IdRelated.txt",Request.QueryString["IdRelated"].ToString());

        //Session["IDForToken"] = "7";
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

    private void CargarDatos()
    {
        SILPA.AccesoDatos.Generico.ParametroDalc _parametroDalc = null;
        SILPA.AccesoDatos.Generico.ParametroEntity _parametro = null;
        SILPA.AccesoDatos.Generico.ParametroEntity _parametro2 = null;
        SILPA.AccesoDatos.Generico.ParametroEntity _parametro3 = null;

        //Cargar datos de cobro
        try{
            int intProcessInstace = Convert.ToInt32(Session["IDProcessInstance"].ToString());
            Cobro _cobro = new Cobro();
            _cobro.ObtenerValoresObjetos(intProcessInstace);

            //Si es un cobro de autoliquidación redireccionar apagina de proceso
            if (_cobro.objCobro.OrigenCobro != OrigenCobroAttribute.GetStringValue(EnumOrigenCobro.AUTOLIQUIDACION))
            {

                _parametroDalc = new SILPA.AccesoDatos.Generico.ParametroDalc();
                _parametro = new SILPA.AccesoDatos.Generico.ParametroEntity();
                _parametro2 = new SILPA.AccesoDatos.Generico.ParametroEntity();
                _parametro3 = new SILPA.AccesoDatos.Generico.ParametroEntity();
                _parametro.IdParametro = -1;
                _parametro2.IdParametro = -1;
                _parametro3.IdParametro = -1;
                _parametro.NombreParametro = "Mensaje_Error_Pago";
                _parametro2.NombreParametro = "Mensaje_Pago_Pendiente";
                _parametro3.NombreParametro = "Error_Lista_Banco";

                _parametroDalc.obtenerParametros(ref _parametro);
                _parametroDalc.obtenerParametros(ref _parametro2);
                _parametroDalc.obtenerParametros(ref _parametro3);


                Double _total = 0;
                string descripcion = string.Empty;

                //Datos Encabezado
                lblTituloTipoDocumento.Text = "FORMULARIO DE PAGO";
                //lblTituloCorporacion.Text = _cobro.objAutAmb.objAutoridadIdentity.Nombre;// "MAVDT";
                lblTituloCorporacion.Text = _cobro.objAutAmb.objAutoridadIdentity.Nombre_Largo;// "MAVDT";
                lblTituloNit.Text = _cobro.objAutAmb.objAutoridadIdentity.NIT;
                lblConcepto.Text = _cobro.objCobro.ConceptoCobro.Nombre;// "Audiencia Pública";
                lblNombreBancoField.Text = _cobro.objAutAmb.objAutoridadIdentity.NombreBanco;
                lblNombreBancoField2.Text = _cobro.objAutAmb.objAutoridadIdentity.NombreBanco;
                lblNumeroCuentaField.Text = _cobro.objAutAmb.objAutoridadIdentity.NumeroCuneta;
                lblNroCuentaField2.Text = _cobro.objAutAmb.objAutoridadIdentity.NumeroCuneta;
                //Datos Personales
                if (_cobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.Natural)
                    lblNombre.Text = _cobro.objPersona.Identity.PrimerNombre + " " + _cobro.objPersona.Identity.SegundoNombre + " " + _cobro.objPersona.Identity.PrimerApellido + " " + _cobro.objPersona.Identity.SegundoApellido;// "JUAN CARLOS MENDEZ RODRIGUEZ";
                else if ((_cobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.JuridicaPublica) || (_cobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.JuridicaPrivada))
                    lblNombre.Text = _cobro.objPersona.Identity.RazonSocial;


                switch (_cobro.objPersona.Identity.TipoDocumentoIdentificacion.Id)
                {
                    case 1:
                        _cobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla = "CC";
                        break;
                    case 2:
                        _cobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla = "NIT";
                        break;
                    case 3:
                        //_cobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla = "NIT";
                        break;
                    case 4:
                        _cobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla = "CEX";
                        break;
                }

                lblIdentificacion.Text = _cobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla + " " + _cobro.objPersona.Identity.NumeroIdentificacion; //"18703404";
                lblNumeDoc.Text = _cobro.objPersona.Identity.NumeroIdentificacion;

                lblTipDoc.Text = _cobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla;
                lblDepartamento.Text = _cobro.objPersona.Identity.DireccionPersona.NombreDepartamento;
                lblMunicipio.Text = _cobro.objPersona.Identity.DireccionPersona.NombreMunicipio;// "ABEJORRAL";
                lblDireccion.Text = _cobro.objPersona.Identity.DireccionPersona.DireccionPersona;// "AVENIDA ORIENTAL NRO 32 58";
                lblTipDoc.Text = _cobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla;
                //Datos Información
                lblReferencia.Text = _cobro.objCobro.NumReferencia;// "105211970";
                lblFechaExpedicion.Text = _cobro.objCobro.FechaExpedicion.ToShortDateString();
                lblFechaVencimiento.Text = _cobro.objCobro.FechaVencimiento.ToShortDateString();

                //Cargar conceptos
                DataTable _tabla = new DataTable();
                _tabla.Columns.Add("DESCRIPCION", Type.GetType("System.String"));
                _tabla.Columns.Add("VALOR", Type.GetType("System.String"));


                for (int i = 0; i < _cobro.objCobro.ListaConceptoCobro.Count; i++)
                {
                    DataRow _fila = _tabla.NewRow();
                    _total = _total + Convert.ToDouble(_cobro.objCobro.ListaConceptoCobro[i].Valor);
                    descripcion = _cobro.objCobro.ListaConceptoCobro[i].Descripcion;
                    _fila["DESCRIPCION"] = _cobro.objCobro.ListaConceptoCobro[i].Descripcion;
                    _fila["VALOR"] = _cobro.objCobro.ListaConceptoCobro[i].Valor.ToString("$ #,##0.00");
                    _tabla.Rows.Add(_fila);
                    _tabla.AcceptChanges();
                }
                descripcion = "Realiza Pago " + _cobro.objPersona.Identity.NumeroIdentificacion + "; Por Concepto de " + lblConcepto.Text + "; Referencia de pago : " + _cobro.objCobro.NumReferencia + "; ";
                // descripcion += "Nombre de Proyecto: " + _cobro.objCobro.IndicadorProcesoField + "; ";
                //descripcion += "Identificacion: " + _cobro.objPersona.Identity.NumeroIdentificacion+ "; ";
                if (descripcion.Length > 80)
                    descripcion = descripcion.Substring(0, 79);
                grdConceptos.DataSource = _tabla;
                grdConceptos.DataBind();

                lblTotal.Text = String.Format("{0:C}", _total);

                //Datos Forma de Pago
                //chkEfectivo.Checked = false;
                lblDatosCorporacion.Text = _cobro.objAutAmb.objAutoridadIdentity.Nombre_Largo + ". " + _cobro.objAutAmb.objAutoridadIdentity.Direccion + ". Tel. " + _cobro.objAutAmb.objAutoridadIdentity.Telefono; //"MAVDT, NIT 836.548.790-5, AV 34 # 11 - 10 3417450 ext 101";
                lblDatosCorporacion2.Text = lblDatosCorporacion.Text;

                //Datos Tiquete Corporacion
                lblNombreCorporacion1.Text = lblTituloCorporacion.Text;
                lblNitCorporacion1.Text = lblTituloNit.Text;
                lblTelefonoCorporacion1.Text = _cobro.objAutAmb.objAutoridadIdentity.Telefono;// "3417450 ext 101";
                lblNumeroReferencia1.Text = lblReferencia.Text;
                lblFechaPago1.Text = lblFechaVencimiento.Text;
                lblTotalPagar1.Text = lblTotal.Text;
                //Datos Tiquete Banco
                lblNombreCorporacion2.Text = lblTituloCorporacion.Text;
                lblNitCorporacion2.Text = lblTituloNit.Text;
                lblTelefonoCorporacion2.Text = _cobro.objAutAmb.objAutoridadIdentity.Telefono; //"3417450 ext 101";
                lblNumeroReferencia2.Text = lblReferencia.Text;
                lblFechaPago2.Text = lblFechaVencimiento.Text;
                lblTotalPagar2.Text = lblTotal.Text;

                lblCodigoBarras1.Text = _cobro.objCobro.CodigoBarras;
                lblCodigoBarras2.Text = _cobro.objCobro.CodigoBarras;


                //Cargar Codigos de Barras
                imgCodigoBarras1.ImageUrl = string.Format(@"Manejador.ashx?code={0}&format={1}&width=450&height=65&size=100", lblCodigoBarras1.Text, "E39");
                imgCodigoBarras2.ImageUrl = string.Format(@"Manejador.ashx?code={0}&format={1}&width=450&height=65&size=100", lblCodigoBarras2.Text, "E39");

                this.lblValorTotal.Text = _total.ToString();
                this.lblDescripcion.Text = descripcion;
                this.lblNumeroSilpa.Text = _cobro.objCobro.NumSILPA;
                //20100519 - AEGB: Informacion del certificado de la autoridad ambiental
                this.lblPpeCertificateSubject.Text = _cobro.objAutAmb.objAutoridadIdentity.Ppe_certificate_sub;
                this.lblPpeCode.Text = _cobro.objAutAmb.objAutoridadIdentity.Ppe_code;
                this.lblPpeUrl.Text = _cobro.objAutAmb.objAutoridadIdentity.Ppe_url;
                this.lblRazonSocial.Text = _cobro.objAutAmb.objAutoridadIdentity.Razon_social;
                this.lblTransaccion.Text = _cobro.objCobro.Transaccion.ToString();
                string refer = _cobro.objCobro.NumReferencia;
                this.lblEstado.Text = _cobro.objCobro.EstadoCobro.ToString();

                if (this.lblEstado.Text == "1")
                {
                    this.lblTextoInformacion.Text = "Este Recibo esta en estado Pagado";
                    this.btnPagoElectronico.Visible = false;
                    this.btnImprimir.Visible = false;
                }
                if (this.lblEstado.Text == "2")
                {
                    this.lblTextoInformacion.Text = _parametro2.Parametro.ToString().Replace("{INFO}", _parametro.Parametro).Replace("{REF}", refer).Replace("{CUS}", this.lblTransaccion.Text); ;
                    this.btnPagoElectronico.Visible = false;
                    this.btnImprimir.Visible = false;
                }
                if (this.lblEstado.Text == "5")
                {
                    this.lblTextoInformacion.Text = "Este Recibo ya fue Impreso Para Pago";
                }
            }
            else
            {
                Response.Redirect("FormularioPagoAutoliquidacion.aspx?IDProcessInstance=" + Session["IDProcessInstance"].ToString());
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "Error Pago" + ex.ToString());
            if (_parametro != null)
                this.lblTextoInformacion.Text = "La construccion del Recibo Fallo, por favor intente más tarde o comuníquese" + _parametro.Parametro.ToString();            
            else
                this.lblTextoInformacion.Text = "La construccion del Recibo Fallo, por favor intente más tarde o comuníquese con el administrador del sistema";
        }     
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        //this.adelantarActividadBpm(_cobro.objCobro.NumSILPA);
        int intProcessInstace = Convert.ToInt32(Session["IDProcessInstance"].ToString());
        this.adelantarActividadToActividadBPM(this.lblNumeroSilpa.Text, PagoElectronico.btnImpresion);
        Cobro _cobro = new Cobro();
        _cobro.ObtenerValoresObjetos(intProcessInstace);
        CobroIdentity oCobroIdentity = new CobroIdentity();
        oCobroIdentity=_cobro.objCobro;
        oCobroIdentity.EstadoCobro=5;
        _cobro.ActualizarCobroEstado(oCobroIdentity);
        Response.Redirect("~/PermisosAmbientales/Liquidacion/ImprimirPago.aspx");
    }

    protected void btnPagoElectronico_Click(object sender, EventArgs e)
    {
        if (lblReferencia.Text.Length > 18)
        {
            Session["TicketID"] = lblReferencia.Text.Substring(0, 18);
        }
        else
        {
            Session["TicketID"] = lblReferencia.Text;
        }
        Session["Amount"] = this.lblValorTotal.Text;
        //Se obtiene el codigo del servicio de pse de la tabla de parametros
        ParametroEntity _parametro = new ParametroEntity();
        ParametroDalc parametro = new ParametroDalc();
        _parametro.IdParametro = -1;
        _parametro.NombreParametro = "CodigoServicioPSE";
        parametro.obtenerParametros(ref _parametro);

        //string pathXml = Server.MapPath(_parametro.Parametro);
        string ipAddress = Request.ServerVariables["REMOTE_ADDR"]; 
        Session["ServiceCode"] = _parametro.Parametro;
        Session["VATAmount"] = "0";
        Session["PaymentDescription"] = this.lblDescripcion.Text;
        Session["ReferenceNumber1"] = ipAddress;
        
     
        //string ipAddress2 = Request.UserHostAddress;
        Session["ReferenceNumber2"] = lblTipDoc.Text;
        Session["ReferenceNumber3"] = lblNumeDoc.Text;
        Session["NumeroSilpa"] = this.lblNumeroSilpa.Text;


        //20100519 - AEGB: Informacion del certificado de la autoridad ambiental
        Session["PpeCertificateSubject"] = this.lblPpeCertificateSubject.Text;
        Session["PpeUrl"] = this.lblPpeUrl.Text;
        Session["PpeCode"] = this.lblPpeCode.Text;
        Session["RazonSocial"] = this.lblRazonSocial.Text;


        Session["EstadoPSE"] = this.lblPpeCode.Text;
        Session["TransaccionPSE"] = this.lblRazonSocial.Text;

        //this.adelantarActividadBpm(_cobro.objCobro.NumSILPA);
        
        /// Se traslada este método a la pagina listar banco que es cuando se confirma el pago electrónico
        //this.adelantarActividadToActividadBPM(this.lblNumeroSilpa.Text, PagoElectronico.btnPago); 
        
        Server.Transfer("~/PagoElectronico/ListaBancos.aspx");
    }
        
    public void adelantarActividadBpm(string strNumeroSilpa, int opcion)
    {

        try
        {
            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();


            _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, strNumeroSilpa);

            //JNS 20190822 Se realiza ajuste para escribir log informativo cuando se presente falla en validación de actividades
            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
            int intIDProcessInstance = Convert.ToInt32(_solicitud.IdProcessInstance);
            string mensaje = servicioWorkflow.ValidarActividadActual(intIDProcessInstance, DatosSesion.Usuario, (long)ActividadSilpa.ConsultarPago);
            if (mensaje=="")
                servicioWorkflow.FinalizarTarea(intIDProcessInstance, ActividadSilpa.ConsultarPago, DatosSesion.Usuario);
            else
                SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: FormularioPago::adelantarActividadBpm - strNumeroSilpa: " + (!string.IsNullOrEmpty(strNumeroSilpa) ? strNumeroSilpa : "null") + " - opcion: " + opcion.ToString() + "\n\n Error: " + mensaje, "BPM_VAL_CON");

        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".adelantarActividadBpm.Finalizo");
        }
    }

    public void adelantarActividadBpm(string strNumeroSilpa)
    {

        try
        {
            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();

            _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, strNumeroSilpa);
            ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
            int intIDProcessInstance = Convert.ToInt32(_solicitud.IdProcessInstance);
            string mensaje=servicioWorkflow.ValidarActividadActual(intIDProcessInstance, DatosSesion.Usuario,(long)ActividadSilpa.ConsultarPago);
            if (mensaje=="")
                servicioWorkflow.FinalizarTarea(intIDProcessInstance, ActividadSilpa.ConsultarPago, DatosSesion.Usuario);
            else
                SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: FormularioPago::adelantarActividadBpm - strNumeroSilpa: " + (!string.IsNullOrEmpty(strNumeroSilpa) ? strNumeroSilpa : "null") + "\n\n Error: " + mensaje, "BPM_VAL_CON");
            
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
       
    }

    /// <summary>
    /// Avanza la actividad de un proceso directamente a otra actividad
    /// </summary>
    /// <param name="strNumeroSilpa">string: identificador del numero silpa</param>
    public void adelantarActividadToActividadBPM(string strNumeroSilpa, PagoElectronico opcionPago) 
    {

        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".adelantarActividadToActividadBPM.Inicio");
            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
            _solicitud = _solicitudDalc.ObtenerSolicitud(null, null, strNumeroSilpa);

            string casoProceso = string.Empty;

            Proceso _objProceso = new Proceso();

            if (Session["IDProcessInstance"] != null)
            {
                ProcesoDalc pDalc = new ProcesoDalc();
                //casoProceso = pDalc.ObtenerProceso((long)Session["IDProcessInstance"]);
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
            string mensaje=servicioWorkflow.ValidarActividadActual(intIDProcessInstance,DatosSesion.Usuario, (long)ActividadSilpa.ConsultarPago);
            if (mensaje=="")
                servicioWorkflow.FinalizarTarea(intIDProcessInstance, ActividadSilpa.ConsultarPago, DatosSesion.Usuario, condicion);
            else
                SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: FormularioPago::adelantarActividadToActividadBPM - strNumeroSilpa: " + (!string.IsNullOrEmpty(strNumeroSilpa) ? strNumeroSilpa : "null") + " - opcionPago: " + opcionPago.ToString() + "\n\n Error: " + mensaje, "BPM_VAL_CON");

        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".adelantarActividadToActividadBPM.Finalizo");
        }
    }

    private const Double TasaIva = 0;

    protected void ImgPse_Click(object sender, ImageClickEventArgs e)
    {
        if (this.lblEstado.Text != "1" && this.lblEstado.Text != "3")
            btnPagoElectronico_Click(sender, e);
        

    }
}
