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

public partial class PermisosAmbientales_Liquidacion_FacturaPago : System.Web.UI.Page
{

    #region Propiedades

        /// <summary>
        /// Información del cobro
        /// </summary>
        private Cobro _objCobro
        {
            get
            {
                return (Cobro)ViewState["Cobro"];
            }
            set
            {
                ViewState["Cobro"] = value;
            }
        }

    #endregion


    #region Metodos Privados

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
        /// Insertar marca de agua
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void CargarMarcaAgua(string p_strMensaje)
        {
            this.ltlMarcaAgua.Text = "<style>";

            this.ltlMarcaAgua.Text += "body::after {" +
                "content: '" + p_strMensaje + "'; " +
                "font-size: 10em; " +
                "/*color: rgba(52, 166, 214, 0.4);*/" +
                "color: #c42a2a; " +
                "z-index: 9999;" +
                "/* aplicar opacidad al texto */" +
                "filter: progid:DXImageTransform.Microsoft.Alpha(opacity=50);" +
                "opacity: .4;" +
                "-moz-opacity: .4;" +
                "/* rotar el texto para que quede diagonal en la página */" +
                "transform: rotate(300deg); " +
                "-webkit-transform: rotate(320deg); " +
                "display: flex;" +
                "align-items: center;" +
                "justify-content: center;" +
                "position: fixed;" +
                "top: 0;" +
                "right: 0;" +
                "bottom: 0;" +
                "left: 0;		" +
                "-webkit-pointer-events: none;" +
                "-moz-pointer-events: none;" +
                "-ms-pointer-events: none;" +
                "-o-pointer-events: none;" +
                "pointer-events: none;" +
                "-webkit-user-select: none;" +
                "-moz-user-select: none;" +
                "-ms-user-select: none;" +
                "-o-user-select: none;" +
                "user-select: none;}";

            this.ltlMarcaAgua.Text += "</style>";
        }


        /// <summary>
        /// Cargara la información a mostrar en la factura
        /// </summary>
        private void CargarInformacionFactura()
        {
            DataTable objInformacionDetalles = null;
            DataRow objInformacionDetalle = null;
            string strDescripcionGeneral = "";
            decimal decValorTotalFactura = 0;

            try
            {			
                //Cargar la información del cobro
                if (Request.QueryString["IDProcessInstance"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["IDProcessInstance"]))
                {
                    //Consultar la información del cobro
                    this._objCobro = new Cobro();
                    this._objCobro.ObtenerValoresObjetos(Convert.ToInt32(Request.QueryString["IDProcessInstance"]));
                }
                else if (Request.QueryString["AL"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["AL"]))
                {
                    //Consultar la información del cobro
                    this._objCobro = new Cobro();
                    this._objCobro.ObtenerValoresCobroAutoliquidacion(Convert.ToInt32(Request.QueryString["AL"]));
                }
                else
                {
                    throw new Exception("No se especifico información para mostrar el comprobante de pago");
                }

                //Cargar datos basicos de la factura
                lblTituloTipoDocumento.Text = "FORMATO DE PAGO";
                lblTituloCorporacion.Text = this._objCobro.objAutAmb.objAutoridadIdentity.TitularCuenta + " - " + this._objCobro.objAutAmb.objAutoridadIdentity.NitTitularCuenta;
                lblTituloNit.Text = this._objCobro.objAutAmb.objAutoridadIdentity.Nombre_Largo + " - " + this._objCobro.objAutAmb.objAutoridadIdentity.NIT;
                
                //Encabezdo
                lblConcepto.Text = "Liquidación por Servicio de Evaluación";
                lblEtiquetaFechaExp.Text = "FECHA DE EXPEDICIÓN";
                lblEtiquetaFechaOportuno.Text = "FECHA DE VENCIMIENTO";
                lblFechaOportuno.Text = this._objCobro.objCobro.FechaVencimiento.ToShortDateString();
                lblEtiquetaFechaVen.Text = "";
                lblEtiquetaFechaVen.Visible = false;
                lblFechaVencimiento.Text = "";
                lblFechaVencimiento.Visible = false;

                //Datos pago
                lblEtiquetaFechaPagoOportuno1.Text = "FECHA LÍMITE DE PAGO";
                lblFechaPagoOportuno1.Text = lblFechaOportuno.Text;
                lblEtiquetaFechaPagoOportuno2.Text = "FECHA LÍMITE DE PAGO";
                lblFechaPagoOportuno2.Text = lblFechaOportuno.Text;
                trFechaVencimiento1.Visible = false;
                lblEtiquetaFechaVencimiento1.Text = "";
                lblFechaVencimiento1.Text = "";
                trFechaVencimiento2.Visible = false;
                lblEtiquetaFechaVencimiento2.Text = "";
                lblFechaVencimiento2.Text = "";
                
                lblNombreBancoField.Text = this._objCobro.objAutAmb.objAutoridadIdentity.NombreBanco;
                lblNombreBancoField2.Text = this._objCobro.objAutAmb.objAutoridadIdentity.NombreBanco;
                lblNumeroCuentaField.Text = this._objCobro.objAutAmb.objAutoridadIdentity.NumeroCuneta;
                lblNroCuentaField2.Text = this._objCobro.objAutAmb.objAutoridadIdentity.NumeroCuneta;
                lblTiqueteCorporacion.Text = "COPIA " + this._objCobro.objAutAmb.objAutoridadIdentity.Nombre_Largo.ToUpper();

                //Datos Personales
                if (this._objCobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.Natural)
                    lblNombre.Text = this._objCobro.objPersona.Identity.PrimerNombre + " " + this._objCobro.objPersona.Identity.SegundoNombre + " " + this._objCobro.objPersona.Identity.PrimerApellido + " " + this._objCobro.objPersona.Identity.SegundoApellido;
                else if ((this._objCobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.JuridicaPublica) || (this._objCobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.JuridicaPrivada))
                    lblNombre.Text = this._objCobro.objPersona.Identity.RazonSocial;
                switch (this._objCobro.objPersona.Identity.TipoDocumentoIdentificacion.Id)
                {
                    case 1:
                        this._objCobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla = "CC";
                        break;
                    case 2:
                        this._objCobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla = "NIT";
                        break;
                    case 4:
                        this._objCobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla = "CEX";
                        break;
                }
                lblIdentificacion.Text = this._objCobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla + " " + this._objCobro.objPersona.Identity.NumeroIdentificacion; //"18703404";
                //lblNumeDoc.Text = this._objCobro.objPersona.Identity.NumeroIdentificacion;
                //lblTipDoc.Text = this._objCobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla;
                lblDepartamento.Text = this._objCobro.objPersona.Identity.DireccionPersona.NombreDepartamento;
                lblMunicipio.Text = this._objCobro.objPersona.Identity.DireccionPersona.NombreMunicipio;
                lblDireccion.Text = this._objCobro.objPersona.Identity.DireccionPersona.DireccionPersona;
                                
                //Datos Información
                lblReferencia.Text = this._objCobro.objCobro.NumReferencia;
                lblFechaExpedicion.Text = this._objCobro.objCobro.FechaExpedicion.ToShortDateString();                

                //Crear tabla que contendrá información del detalle de pago
                objInformacionDetalles = new DataTable();
                objInformacionDetalles.Columns.Add("DESCRIPCION", Type.GetType("System.String"));
                objInformacionDetalles.Columns.Add("VALOR", Type.GetType("System.String"));

                //Ciclo que cargará detalles en la tabla
                foreach (DetalleCobroIdentity objDetalle in this._objCobro.objCobro.ListaConceptoCobro)
                {
                    //Agrgar informacion
                    objInformacionDetalle = objInformacionDetalles.NewRow();
                    objInformacionDetalle["DESCRIPCION"] = objDetalle.Descripcion;
                    objInformacionDetalle["VALOR"] = objDetalle.Valor.ToString("$ #,##0.00");
                    objInformacionDetalles.Rows.Add(objInformacionDetalle);
                    objInformacionDetalles.AcceptChanges();

                    //Incrementar el valor total de la factura
                    decValorTotalFactura = decValorTotalFactura + objDetalle.Valor;
                }

                //Mostrar el detalle
                grdConceptos.DataSource = objInformacionDetalles;
                grdConceptos.DataBind();

                //Mostrar el valor total de la factura
                ((Literal)grdConceptos.FooterRow.FindControl("ltlTotal")).Text = String.Format("{0:C}", decValorTotalFactura);

                //Datos Forma de Pago
                lblDatosCorporacion.Text = this._objCobro.objAutAmb.objAutoridadIdentity.Nombre_Largo + ". " + this._objCobro.objAutAmb.objAutoridadIdentity.Direccion + ". Tel. " + this._objCobro.objAutAmb.objAutoridadIdentity.Telefono;
                lblDatosCorporacion2.Text = lblDatosCorporacion.Text;
                lblDatosCorporacion3.Text = lblDatosCorporacion.Text;

                //Datos Tiquete Corporacion
                lblTipoCuentaField1.Text = this._objCobro.objAutAmb.objAutoridadIdentity.TipoCuenta;
                lblNombreCorporacion1.Text = this._objCobro.objAutAmb.objAutoridadIdentity.TitularCuenta;
                lblNitCorporacion1.Text = this._objCobro.objAutAmb.objAutoridadIdentity.NitTitularCuenta;
                lblTelefonoCorporacion1.Text = this._objCobro.objAutAmb.objAutoridadIdentity.Telefono;
                lblNumeroReferencia1.Text = lblReferencia.Text;
                lblTotalPagar1.Text = String.Format("{0:C}", decValorTotalFactura);

                //Datos Tiquete Banco
                lblTipoCuentaField2.Text = this._objCobro.objAutAmb.objAutoridadIdentity.TipoCuenta;
                lblNombreCorporacion2.Text = this._objCobro.objAutAmb.objAutoridadIdentity.TitularCuenta;
                lblNitCorporacion2.Text = this._objCobro.objAutAmb.objAutoridadIdentity.NitTitularCuenta;
                lblTelefonoCorporacion2.Text = this._objCobro.objAutAmb.objAutoridadIdentity.Telefono;
                lblNumeroReferencia2.Text = lblReferencia.Text;
                lblTotalPagar2.Text = String.Format("{0:C}", decValorTotalFactura);

                //Codigo de barras
                lblCodigoBarras1.Text = this._objCobro.objCobro.CodigoBarras;
                lblCodigoBarras2.Text = this._objCobro.objCobro.CodigoBarras;
                imgCodigoBarras1.ImageUrl = string.Format(@"Manejador.ashx?code={0}&width=470&height=70", lblCodigoBarras1.Text);
                imgCodigoBarras2.ImageUrl = string.Format(@"Manejador.ashx?code={0}&width=470&height=70", lblCodigoBarras2.Text);


                //Cargar la descripcion general del pago
                strDescripcionGeneral = "Realiza Pago " + this._objCobro.objPersona.Identity.NumeroIdentificacion + "; Por Concepto de " + lblConcepto.Text + "; Referencia de pago : " + this._objCobro.objCobro.NumReferencia + "; ";
                if (strDescripcionGeneral.Length > 80)
                    strDescripcionGeneral = strDescripcionGeneral.Substring(0, 79);

                //Insertar marca de agua
                if (this._objCobro.objCobro.FechaVencimiento.Date < DateTime.Today || this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.APROBADA || this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.AVANZADO || this._objCobro.objCobro.EstadoCobro == (int)EnumEstadoCobro.TRANSACCION_FINALIZADA)
                {
                    if (this._objCobro.objCobro.FechaVencimiento.Date < DateTime.Today)
                        this.CargarMarcaAgua("VENCIDA");
                    else
                        this.CargarMarcaAgua("PAGADA");
                }
                else
                {
                    this.ltlMarcaAgua.Text = "";
                }
                
            }
            catch(Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "PermisosAmbientales_Liquidacion_FacturaPago :: CargarInformacionFactura -> Error Inesperado: " + exc.Message);
                this.lblTextoInformacion.Text = "La construccion del Recibo Fallo, por favor intente más tarde o comuníquese con el administrador del sistema.";
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
                //this.CargarInformacionFactura();

                //Cargar datos pagina
                if (this.ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {
                    //Iniciliazar datos de la página
                    this.CargarInformacionFactura();
                }
            }
        }
    
    #endregion

}
