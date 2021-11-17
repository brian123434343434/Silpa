using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using SILPA.EntidadesNegocio.Servicios.Permisos;
//using SILPA.EntidadesNegocio.PermisosAmbientales;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccessoDatos.Generico;

public partial class PermisosAmbientales_Liquidacion_DocumentoCobro : System.Web.UI.Page
{
    RadicacionDocumento _radicacionDocumento = new RadicacionDocumento();
    RequerimientoPagoIdentity _requerimientoPago = new RequerimientoPagoIdentity();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        //string _xmlResultadoDocumento = "";
        //string _xmlResultadoRadicacion = "";
        //string _strConexion = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
        //string _strQuery = "select rad.id_radicacion as RADICACION, EP.XMLDATOS AS DATOS_EP, RAD.XMLDATOS AS DATOS_RAD, RAD.NUMERO_SILPA, EP.PAGO FROM GEN_EVALUACION_LIQUIDACION EP INNER JOIN GEN_RADICACION RAD ON "+
        //    "EP.ID_RADICACION = RAD.ID_RADICACION WHERE RAD.NUMERO_SILPA=@NUMERO_SILPA ";
        //SqlConnection _con = new SqlConnection(_strConexion);
        //_con.Open();
        //SqlCommand _com = new SqlCommand(_strQuery, _con);
        //_com.Parameters.AddWithValue("NUMERO_SILPA", txt_numeroSILPA.Text);
        //SqlDataReader dr = _com.ExecuteReader();
        //if (dr.HasRows)
        //{
        //    txt_numeroSILPA.Enabled = false;
        //    dr.Read();
        //    if (Boolean.Parse(dr["PAGO"].ToString()))
        //    {
        //        lbl_estado.Text = "Su proceso ya fue pagado";
        //        btn_generar.Visible = false;
        //        txt_numeroSILPA.Enabled = true;
        //    }
        //    else
        //    {
        //        _xmlResultadoDocumento = dr["DATOS_EP"].ToString();
        //        _xmlResultadoRadicacion = dr["DATOS_RAD"].ToString();
        //        _requerimientoPago = (RequerimientoPago)_requerimientoPago.GetSerializedObject(_requerimientoPago, _xmlResultadoDocumento);
        //        _radicacionDocumento = (RadicacionDocumento)_radicacionDocumento.GetSerializedObject(_radicacionDocumento, _xmlResultadoRadicacion);
        //        Session["DocumentoCobro"] = _requerimientoPago;
        //        Session["Radicacion"] = _radicacionDocumento;
        //        //Session["id_radicacion"] = 199;//_radicacionDocumento.NumeroRadicadoAA;
        //        Session["id_radicacion"] = int.Parse(dr["RADICACION"].ToString());
				
        //        lbl_estado.Text = "Su proceso ya fue liquidado, presione el botón para generar  el Documento de Cobro e imprimir o proceder a pago en línea";
        //        btn_generar.Visible = true;
        //    }
        //}
        //else
        //{
        //    lbl_estado.Text = "Su proceso aún no ha sido liquidado o no se encuentra registrado en el sistema";
        //    btn_generar.Visible = false;
        //    txt_numeroSILPA.Enabled = true;
        //}
    }
    protected void btn_generar_Click(object sender, EventArgs e)
    {
        Response.Redirect("DC.aspx");
    }
}
