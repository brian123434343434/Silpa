using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

using SILPA.LogicaNegocio.Generico;
using SILPA.AccessoDatos.Generico;
//using SILPA.EntidadesNegocio.PermisosAmbientales;
//using SILPA.EntidadesNegocio.Servicios.Permisos;





public partial class PermisosAmbientales_Liquidacion_Liquidar : System.Web.UI.Page
{
    RadicacionDocumento _radicacionDocumento = new RadicacionDocumento();
    RequerimientoPagoIdentity _requerimientoPago = new RequerimientoPagoIdentity();


    protected void Page_Load(object sender, EventArgs e)
    {
        
        lbl_estado.Visible = false;
        //RadicacionDocumento rad = new RadicacionDocumento();
        //rad.CiudadCorporacion = "Medellín";
        //rad.CiudadSolicitante = "Bogotá";
        //rad.CorreoSolicitante = "servicios@emtec.com.co";
        //rad.DireccionCorporacion = "AV 34 # 11 - 10";
        //rad.IdentificacionSolicitante = "800.234.112-1";
        //rad.NitCorporacion = "800.335.124-2";
        //rad.NombreCorporacion = "CORPONET";
        //rad.NombreSolicitante = "EMTEC S.A.";
        //rad.NumeroRadicadoAA = "LIQPE001";
        //rad.NumeroSilpa = "PER-2009082199";
        //rad.TelefonoCorporacion = "7832003";
        //string xml = rad.GetXml();
        //_radicacionDocumento = rad;


    }



    protected void btn_Consultar_Click(object sender, EventArgs e)
    {
        //string _xmlResultado = "";
        //string _strConexion = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
        //string _strQuery = "SELECT * FROM GEN_RADICACION WHERE NUMERO_SILPA = @NUMERO_SILPA";
        //SqlConnection _con = new SqlConnection(_strConexion);
        //_con.Open();
        //SqlCommand _com = new SqlCommand(_strQuery, _con);
        //_com.Parameters.AddWithValue("NUMERO_SILPA", txt_numero.Text);
        //SqlDataReader dr = _com.ExecuteReader();
        //if (dr.HasRows)
        //{   
        //    txt_numero.Enabled = false;
        //    dr.Read();
        //    _xmlResultado = dr["XMLDATOS"].ToString();
        //    lbl_numeroRadicado.Text = dr["NUMERO_RADICADO_AA"].ToString();
        //    Session["id_radicacion"] = Int32.Parse(dr["ID_RADICACION"].ToString());
        //    _radicacionDocumento = (RadicacionDocumento)_radicacionDocumento.GetSerializedObject(_radicacionDocumento, _xmlResultado);
        //    Session["Radicado"] = _radicacionDocumento;
        //   // lbl_nombreSolicitante.Text = _radicacionDocumento.NombreSolicitante;
        //   // lbl_ciudadSolicitante.Text = _radicacionDocumento.CiudadSolicitante;
        //}
        //else
        //{
        //    lbl_estado.Text = "No se encontró el proceso";
        //    lbl_estado.Visible = true;
        //    txt_numero.Enabled = true;
        //}
        //_con.Close();

        //pnl_solicitante.Visible = true;
    }
    protected void btn_liquidar_Click(object sender, EventArgs e)
    {
        
        pnl_liquidar.Visible = true;
    }
    protected void btn_generar_Click(object sender, EventArgs e)
    {
        //Random r = new Random();
        //string xmlDatos = "";
        //_requerimientoPago.Departamento = ddl_departamento.SelectedItem.Text;
        //_requerimientoPago.Municipio = ddl_municipio.SelectedItem.Text;
        ////_requerimientoPago.Concepto = txt_concepto.Text;
        //_requerimientoPago.Concepto = this.ddl_Concepto.SelectedValue.ToString();
        //_requerimientoPago.NumeroSILPA = txt_numero.Text;
        //_requerimientoPago.ValorNumeros = Decimal.Parse(txt_valorNumeros.Text.ToString().Trim());
        //_requerimientoPago.ValorLetras = txt_valorLetras.Text;
        //_requerimientoPago.TipoDocumentoCobro = "Liquidación de Evaluación";
        //_requerimientoPago.NumeroReferencia = r.Next(100000000, 999999999).ToString();
        //_requerimientoPago.FechaExpedicion=DateTime.Today;
        //_requerimientoPago.CodigoBarras = "CORP" + "0" +_requerimientoPago.NumeroReferencia+ DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString();  
        //xmlDatos = _requerimientoPago.GetXml();

        
        //string _strConexion = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
        //string _strQuery = "INSERT INTO GEN_EVALUACION_LIQUIDACION(XMLDATOS,ID_RADICACION,PAGO) VALUES (@XMLDATOS,@ID_RADICACION,@PAGO)";
        //SqlConnection _con = new SqlConnection(_strConexion);
        //_con.Open();
        //SqlCommand _com = new SqlCommand(_strQuery, _con);
        //_com.Parameters.AddWithValue("XMLDATOS", xmlDatos);
        //_com.Parameters.AddWithValue("ID_RADICACION", (int)Session["id_radicacion"]);
        //_com.Parameters.AddWithValue("PAGO", false);
        //_com.ExecuteNonQuery();
        //lbl_estado.Text = "Liquidación Realizada";
        //lbl_estado.Visible = true;
        //pnl_liquidar.Visible = false;
        ////pnl_solicitante.Visible = false;
        
        
    }

    protected void txt_valorNumeros_TextChanged(object sender, EventArgs e)
    {
        SMCifras.Cifras cf = new SMCifras.Cifras(double.Parse(txt_valorNumeros.Text.Trim()));
        txt_valorLetras.Text = cf.retorno();


    }
    protected void sds_Concepto_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void ddl_Concepto_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  Session["concepto"] = this.ddl_Concepto.SelectedValue.ToString();
    }
}
