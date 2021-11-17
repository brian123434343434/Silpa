using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Salvoconducto_ConsultarSalvoconductoWeb : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        string _strConexion = ConfigurationManager.ConnectionStrings["eFormBuilderConnectionString"].ConnectionString;
        string _strQuery = "select fieldbyforminstance.idForminstance, Field.Type, Field.Text,  "
                           +"fieldbyforminstance.Value, Field.sourcesql from fieldbyforminstance "
                           +"inner join field on fieldbyforminstance.IdField = Field.Id "
                           +"where fieldbyforminstance.value = @VALOR";
        SqlConnection _con = new SqlConnection(_strConexion);
        _con.Open();
        SqlCommand _com = new SqlCommand(_strQuery, _con);
        _com.Parameters.AddWithValue("VALOR", txt_numero.Text);
        SqlDataReader dr = _com.ExecuteReader();
        if (dr.HasRows)
        {
            lbl_resultado_error.Visible = false;
            dr.Read();
            string instancia = dr["idFormInstance"].ToString();
            _strQuery = "select Field.Text as ' ',  fieldbyforminstance.Value as ' ' "
                        + "from fieldbyforminstance "
                        + "inner join field on fieldbyforminstance.IdField = Field.Id "
                        + "where fieldbyforminstance.idFormInstance = @INSTANCIA";
            SqlConnection _con2 = new SqlConnection(_strConexion);
            _con2.Open();
            SqlCommand _com2 = new SqlCommand(_strQuery, _con2);
            _com2.Parameters.AddWithValue("INSTANCIA", Int32.Parse(instancia));
            SqlDataAdapter da = new SqlDataAdapter(_com2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv_resultado.DataSource = dt;
            dgv_resultado.DataBind();

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("RUAS - SALVOCONDUCTO", 2, "Se consulto Salvoconducto Web");

            dgv_resultado.Visible = true;
            _con2.Close();

        }
        else
        {
            dgv_resultado.Visible = false;
            lbl_resultado_error.Visible = true;
        }
        _con.Close();
        pnl_resultado.Visible = true;

    }
}
