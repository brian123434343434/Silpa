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
using SILPA.Comun;
using SoftManagement.Log;

public partial class PermisosAmbientales_Liquidacion_ImprimirPago : System.Web.UI.Page
{
    private Cobro _cobro;
    private string descripcion;
    Double _total = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (ValidacionToken() == false)
            Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
        CargarDatos();
    }

    /// <summary>
    /// Valida si hay token para el usuario que intenta acceder a la página, y si no ha expirado
    /// </summary>
    /// <returns>Verdadero si hay token válido para el usuario</returns>
    private bool ValidacionToken()
    {
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
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarDatos.Inicio");
            int intProcessInstace = Convert.ToInt32(Session["IDProcessInstance"].ToString());
            _cobro = new Cobro();
            _cobro.ObtenerValoresObjetos(intProcessInstace);

            //Datos Encabezado
            lblTituloTipoDocumento.Text = "FORMULARIO DE PAGO";
            lblTituloCorporacion.Text = _cobro.objAutAmb.objAutoridadIdentity.Nombre_Largo;// "MAVDT";
            lblTituloNit.Text = _cobro.objAutAmb.objAutoridadIdentity.NIT;
            lblConcepto.Text = _cobro.objCobro.ConceptoCobro.Nombre;// "Audiencia Pública";

            //Datos Personales
            if (_cobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.Natural)
                lblNombre.Text = _cobro.objPersona.Identity.PrimerNombre + " " + _cobro.objPersona.Identity.SegundoNombre + " " + _cobro.objPersona.Identity.PrimerApellido + " " + _cobro.objPersona.Identity.SegundoApellido;// "JUAN CARLOS MENDEZ RODRIGUEZ";
            else if ((_cobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.JuridicaPublica) || (_cobro.objPersona.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.JuridicaPrivada))
                lblNombre.Text = _cobro.objPersona.Identity.RazonSocial;
            lblIdentificacion.Text = _cobro.objPersona.Identity.TipoDocumentoIdentificacion.Sigla + " " + _cobro.objPersona.Identity.NumeroIdentificacion; //"18703404";
            lblDepartamento.Text = _cobro.objPersona.Identity.DireccionPersona.NombreDepartamento;
            lblMunicipio.Text = _cobro.objPersona.Identity.DireccionPersona.NombreMunicipio;// "ABEJORRAL";
            lblDireccion.Text = _cobro.objPersona.Identity.DireccionPersona.DireccionPersona;// "AVENIDA ORIENTAL NRO 32 58";

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
                descripcion = descripcion + "; " + _cobro.objCobro.ListaConceptoCobro[i].Descripcion;
                _fila["DESCRIPCION"] = _cobro.objCobro.ListaConceptoCobro[i].Descripcion;
                _fila["VALOR"] = _cobro.objCobro.ListaConceptoCobro[i].Valor.ToString("$ #,##0.00");
                _tabla.Rows.Add(_fila);
                _tabla.AcceptChanges();
            }
            grdConceptos.DataSource = _tabla;
            grdConceptos.DataBind();

            lblTotal.Text = String.Format("{0:C}", _total);

            //Datos Forma de Pago
            chkEfectivo.Checked = false;
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
            Response.Write("<script>window.__doPostBack('','');</script>");
            string strScript = "<script language='JavaScript'>" + "window.print();</script>";
            this.Page.RegisterStartupScript("Imprimir", strScript);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarDatos.Finalizo");
        }
    }
}
