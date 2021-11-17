using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using SILPA.EntidadesNegocio.Servicios.Permisos;
//using SILPA.EntidadesNegocio.PermisosAmbientales;


using System.Text.RegularExpressions;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccessoDatos.Generico;

public partial class PermisosAmbientales_Liquidacion_DC : System.Web.UI.Page
{
    //RequerimientoPago _requerimientoPago = new RequerimientoPago();
    //RadicacionDocumento _radicacionDocumento = new RadicacionDocumento();

    protected void Page_Load(object sender, EventArgs e)
    {
        //cargaPrueba();
        Cargar();
        
    }

    private void Cargar()
    {
        //try
        //{
        //    _requerimientoPago = (RequerimientoPago)Session["DocumentoCobro"];
        //    _radicacionDocumento = (RadicacionDocumento)Session["Radicacion"];
        //    CargarDocumento(_requerimientoPago, _radicacionDocumento);
        //}
        //catch (Exception e)
        //{
        //    Page.RegisterStartupScript("_errorMessage", "<script>window.alert('Se produjo un error de sesión:"+e.Message+"') ;</script>");
        //    //ClientScript.RegisterStartupScript("_errorMessage", 0, "<script>window.alert('Se produjo un error de sesión:" + e.Message + "') ;</script>");

        //}

    }

    private void CargarDocumento(RequerimientoPagoIdentity _requerimientoPago, RadicacionDocumento _radicacionDocumento)
    {
//        decimal temp = 0;
//        //Encabezado
//        if (_radicacionDocumento.NumeroSilpa.Substring(0, 3) == "SEG")
//        {
//            //lbl_titulo.Text = "DOCUMENTO DE COBRO DE SEGUIMIENTO"; // Pendiente de cambio
//            lbl_titulo.Text = "FORMULARIO DE PAGO DE SEGUIMIENTO"; // Pendiente de cambio
//        }
//        else if (_radicacionDocumento.NumeroSilpa.Substring(0, 3) == "PER")
//        {
//            //lbl_titulo.Text = "DOCUMENTO DE COBRO DE EVALUACIÓN DE PERMISO"; // Pendiente de cambio
//            lbl_titulo.Text = "FORMULARIO DE PAGO DE EVALUACIÓN DE PERMISO"; // Pendiente de cambio
            
//        }
//        else if (_radicacionDocumento.NumeroSilpa.Substring(0, 3) == "LIQ")
//        {
//            //lbl_titulo.Text = "DOCUMENTO DE COBRO DE EVALUACIÓN DE LIQUIDACIÓN"; // Pendiente de cambio
//            lbl_titulo.Text = "FORMULARIO DE PAGO DE EVALUACIÓN DE LIQUIDACIÓN"; // Pendiente de cambio
//        }
//        else
//        {
//            //lbl_titulo.Text = "DOCUMENTO DE COBRO"; // Pendiente de cambio
//            lbl_titulo.Text = "FORMULARIO DE PAGO"; // Pendiente de cambio
            
//        }
//        lbl_nombre_corporacion.Text = _radicacionDocumento.NombreCorporacion;
//        lbl_nit_corporacion.Text = "NIT " + _radicacionDocumento.NitCorporacion;
//        lbl_concepto.Text = _requerimientoPago.Concepto;

//        //Datos de Solicitante
//        lbl_nombre_solicitante.Text = _radicacionDocumento.NombreSolicitante;
//        lbl_identificacion_solicitante.Text = _radicacionDocumento.IdentificacionSolicitante;
//        lbl_ciudad_solicitante.Text = _radicacionDocumento.CiudadSolicitante;
//        lbl_direccion_solicitante.Text = string.Empty;
////        lbl_direccion_solicitante.Text = _radicacionDocumento.DireccionSolicitante;
        

//        //Datos Factura
//        lbl_numero_referencia.Text = _requerimientoPago.NumeroReferencia;
//        lbl_fecha_expedicion.Text = _requerimientoPago.FechaExpedicion.Year.ToString() + "/" + _requerimientoPago.FechaExpedicion.Month.ToString() + "/" + _requerimientoPago.FechaExpedicion.Day.ToString();
//        DateTime _fechaVencimiento = _requerimientoPago.FechaExpedicion.AddDays(20);
//        lbl_fecha_vencimiento.Text = _fechaVencimiento.Year.ToString() + "/" + _fechaVencimiento.Month.ToString() + "/" + _fechaVencimiento.Day.ToString();

//        //Datos Ubicación Geográfica
//        lbl_departamento.Text = _requerimientoPago.Departamento;
//        lbl_municipio.Text = _requerimientoPago.Municipio;
//        lbl_ciudad.Text = _requerimientoPago.Ciudad;

//        //Datos Conceptos
//        lbl_concepto1.Text = lbl_concepto.Text;
//        lbl_concepto2.Text = "";
//        lbl_concepto3.Text = "";
//        lbl_precio1.Text = String.Format("{0:C}", _requerimientoPago.ValorNumeros);
//        lbl_precio2.Text = "";
//        lbl_precio3.Text = "";
//        //lbl_subtotal.Text = (Double.Parse(lbl_precio1.Text) + Double.Parse(lbl_precio2.Text) + Double.Parse(lbl_precio3.Text)).ToString();
//        lbl_subtotal.Text = String.Format("{0:C}", _requerimientoPago.ValorNumeros);
//        //lbl_descuento.Text = ((Double.Parse(lbl_subtotal.Text)) * (Double.Parse("0.15"))).ToString();
//        lbl_descuento.Text = String.Format("{0:C}",temp);
//        lbl_recargo.Text = String.Format("{0:C}", temp);
//        lbl_iva.Text = String.Format("{0:C}", temp);
//        //lbl_total.Text = (Double.Parse(lbl_subtotal.Text) + Double.Parse(lbl_descuento.Text) + Double.Parse(lbl_recargo.Text) + Double.Parse(lbl_iva.Text)).ToString();
//        lbl_total.Text = _requerimientoPago.ValorNumeros.ToString();
//        //Regex r = new Regex(" \d{0,3},\d{0,3},\d{0,3}.\d{0,2}");
//        decimal moneyvalue = _requerimientoPago.ValorNumeros;
//        string html = String.Format("{0:C}", moneyvalue);
//        lbl_total.Text = html;


//        //Datos Representante corporación
//        //img_firma.ImageUrl = "~/App_Themes/Img/firmaPrototipo.png";
//        lbl_representate_corporacion.Text = "Rodolfo Trujillo Suárez CC.10203040";
//        //lbl_corporacion.Text = lbl_nombre_corporacion.Text;
//        //lbl_corporacion2.Text = lbl_nombre_corporacion.Text;

//        //Datos Bolante Corporación y Bolante Banco
//        lbl_nombre_corporacion2.Text = lbl_nombre_corporacion.Text;
//        lbl_nombre_corporacion3.Text = lbl_nombre_corporacion.Text;
//        lbl_nit_corporacion2.Text = lbl_nit_corporacion.Text;
//        lbl_nit_corporacion3.Text = lbl_nit_corporacion.Text;
//        lbl_telefono_corporacion2.Text = _radicacionDocumento.TelefonoCorporacion;
//        lbl_telefono_corporacion3.Text = lbl_telefono_corporacion2.Text;
//        lbl_fecha_vencimiento2.Text = lbl_fecha_vencimiento.Text;
//        lbl_fecha_vencimiento3.Text = lbl_fecha_vencimiento2.Text;
//        lbl_total2.Text = lbl_total.Text;
//        lbl_total3.Text = lbl_total.Text;
//        lbl_numero_referencia2.Text = lbl_numero_referencia.Text;
//        lbl_numero_referencia3.Text = lbl_numero_referencia.Text;

//        //Datos Código de Barras
//        lbl_numero_barras1.Text = _requerimientoPago.CodigoBarras;
//        lbl_numero_barras2.Text = lbl_numero_barras1.Text;
//        lbl_codigo_barras1.Text = lbl_numero_barras1.Text;
//        lbl_codigo_barras2.Text = lbl_numero_barras1.Text;

//        //Datos Corporación
//        datos_corporacion1.Text = lbl_nombre_corporacion.Text + ", " + lbl_nit_corporacion.Text + ", "
//            + _radicacionDocumento.DireccionCorporacion + lbl_telefono_corporacion2.Text;
//        datos_corporacion2.Text = datos_corporacion1.Text;

    }

    /// <summary>
    /// Carga datos de prueba en un documento de cobro
    /// </summary>
    private void cargaPrueba()
    {
    //    //Encabezado
    //    //lbl_titulo.Text = "DOCUMENTO DE COBRO";
    //    lbl_titulo.Text = "FORMULARIO DE PAGO";
        
    //    lbl_nombre_corporacion.Text = "CORPONET";
    //    lbl_nit_corporacion.Text = "800.100.200-1";
    //    lbl_concepto.Text = "CONSUMO RECURSOS AMBIENTALES";

    //    //Datos de Solicitante
    //    lbl_nombre_solicitante.Text = "EMTENET";
    //    lbl_identificacion_solicitante.Text = "800.300.400-2";
    //    lbl_ciudad_solicitante.Text = "Cali";
        
    //    //Datos Factura
    //    lbl_numero_referencia.Text = "20043235";
    //    lbl_fecha_expedicion.Text = "2000/01/01";
    //    lbl_fecha_vencimiento.Text = "2000/01/15";
        
    //    //Datos Ubicación Geográfica
    //    lbl_departamento.Text = "Valle del Cauca";
    //    lbl_municipio.Text = "Ginebra";
    //    lbl_ciudad.Text = "Ginebra";

    //    //img_firma.ImageUrl = "~/App_Themes/Img/firmaPrototipo.png";
    //    //Datos Conceptos
    //    lbl_concepto1.Text = "Utilización de suelos - 1999/10/11";
    //    lbl_concepto2.Text = "Utilización de suelos - 1999/11/14";
    //    lbl_concepto3.Text = "Utilización de suelos - 1999/12/12";
    //    lbl_precio1.Text = "250,000.00";
    //    lbl_precio2.Text = "230,000.00";
    //    lbl_precio3.Text = "260,000.00";
    //    lbl_subtotal.Text = (Double.Parse(lbl_precio1.Text) + Double.Parse(lbl_precio2.Text) + Double.Parse(lbl_precio3.Text)).ToString();
    //    lbl_descuento.Text = ((Double.Parse(lbl_subtotal.Text))*(Double.Parse("0.15"))).ToString();
    //    lbl_recargo.Text = "0.00";
    //    lbl_iva.Text = "0.00";
    //    lbl_total.Text = (Double.Parse(lbl_subtotal.Text) + Double.Parse(lbl_descuento.Text) + Double.Parse(lbl_recargo.Text) + Double.Parse(lbl_iva.Text)).ToString();

    //    //Datos Representante corporación
    //    //img_firma.ImageUrl = "";
    //    lbl_representate_corporacion.Text = "Adolfo Trujillo Suárez CC.10203040";
    //    //lbl_corporacion.Text = lbl_nombre_corporacion.Text;
    //    //lbl_corporacion2.Text = lbl_nombre_corporacion.Text;

    //    //Datos Bolante Corporación y Bolante Banco
    //    lbl_nombre_corporacion2.Text = lbl_nombre_corporacion.Text;
    //    lbl_nombre_corporacion3.Text = lbl_nombre_corporacion.Text;
    //    lbl_nit_corporacion2.Text = lbl_nit_corporacion.Text;
    //    lbl_nit_corporacion3.Text = lbl_nit_corporacion.Text;
    //    lbl_telefono_corporacion2.Text = "20340220";
    //    lbl_telefono_corporacion3.Text = lbl_telefono_corporacion2.Text;
    //    lbl_fecha_vencimiento2.Text = lbl_fecha_vencimiento.Text;
    //    lbl_fecha_vencimiento3.Text = lbl_fecha_vencimiento2.Text;
    //    lbl_total2.Text = lbl_total.Text;
    //    lbl_total3.Text = lbl_total.Text;
    //    lbl_numero_referencia2.Text = lbl_numero_referencia.Text;
    //    lbl_numero_referencia3.Text = lbl_numero_referencia.Text;

    //    //Datos Código de Barras
    //    lbl_numero_barras1.Text = "CORPNET0000000020000001";
    //    lbl_numero_barras2.Text = lbl_numero_barras1.Text;
    //    lbl_codigo_barras1.Text = lbl_numero_barras1.Text;
    //    lbl_codigo_barras2.Text = lbl_numero_barras1.Text;

    //    //Datos Corporación
    //    datos_corporacion1.Text = lbl_nombre_corporacion.Text + ", " + lbl_nit_corporacion.Text + ", "
    //        + "AV 34 No. 45 - 60 Teléfono: " + lbl_telefono_corporacion2.Text;
    //    datos_corporacion2.Text = datos_corporacion1.Text;


    }
    protected void btn_cargar_Click(object sender, EventArgs e)
    {
       // cargaPrueba();
    }
    protected void btn_pagar_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/PagoElectronico/Pago_Electronico.aspx");
    }
}
