using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AjaxControlToolkit;
using SoftManagement.Log;

public partial class Parametrizacion : System.Web.UI.Page
{
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            CargarCombo(ref CboModulo, CargarInfoBasica("SEN_LISTAR_MODULO"));
            CargarCombo(ref CboContenido , CargarInfoBasica("SEN_LISTAR_CONTENIDO"));  
            dgv_resultado.DataSource = CargarInfoBasica("SEN_LISTAR_MODULO_CONTENIDO");
            dgv_resultado.DataBind();
        }

    }

    private void CargarCombo(ref DropDownList cboData, DataTable data)
    {
        //Asociacion de los datos al objeto
        cboData.DataSource = data;

        //Asignacion de las propiedades de text y value
        cboData.DataValueField  = data.Columns[0].ColumnName.ToString();
        cboData.DataTextField = data.Columns[1].ColumnName.ToString();

        cboData.DataBind(); 

    }

    private static DataTable CargarInfoBasica(string nombresp)
    {
            string _cadena = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
            using (SqlConnection _con = new SqlConnection(_cadena))
            {
                _con.Open();
                using (SqlCommand _com = new SqlCommand(nombresp, _con))
                {
                    _com.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adp = new SqlDataAdapter(); ;
                    DataTable dat = new DataTable();
                    adp.SelectCommand = _com;
                    dat = new DataTable();
                    adp.Fill(dat);
                    return dat;
                }
            }
    }

    private static DataTable CargarInfoBasicaM(int opc, int idBusqueda, string nombresp)
    {
            string _cadena = ConfigurationManager.ConnectionStrings["SILAMCConnectionString"].ConnectionString;
            ParametrosLocalizacion param;
            using (SqlConnection _con = new SqlConnection(_cadena))
            {
                _con.Open();
                using (SqlCommand _com = new SqlCommand(nombresp, _con))
                {
                    _com.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adp = new SqlDataAdapter(); ;
                    DataTable dat = new DataTable();

                    param = new ParametrosLocalizacion();

                    param.Add("@P_PROC", opc, DbType.Decimal, ParameterDirection.Input);
                    param.Add("@P_BUSQUEDA_ID", idBusqueda, DbType.Decimal, ParameterDirection.Input);

                    foreach (System.Data.SqlClient.SqlParameter p in param.Col)
                        _com.Parameters.Add(p);

                    adp.SelectCommand = _com;
                    dat = new DataTable();
                    adp.Fill(dat);
                    return dat;
                }
            }
    }

    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_resultado_error.Text = "";
            lbl_resultado_error.Visible = false;
        }
        catch(Exception ex)
        {
            lbl_resultado_error.Visible = true;
            Mensaje.ErrorCritico(this, ex); 
        }
    }

    public class ParametrosLocalizacion
    {
        private System.Data.SqlClient.SqlParameter _parametro;
        private ArrayList _col = new ArrayList();

        public ArrayList Col
        {
            get { return _col; }
            set { _col = value; }
        }

        public void Add(string nom, object val, DbType tip, ParameterDirection dir)
        {
            _parametro = new System.Data.SqlClient.SqlParameter();
            _parametro.ParameterName = nom;
            _parametro.DbType = tip;
            _parametro.Value = val;
            _parametro.Size = val.ToString().Length;
            _parametro.Direction = dir;

            _col.Add(_parametro);
        }

        public object ValorParametro(string par)
        {
            foreach (System.Data.SqlClient.SqlParameter para in _col)
            {
                if (para.ParameterName.ToUpper() == par.ToUpper())
                {
                    return para.Value;
                }
            }
            return null;
        }
    }

    protected void dgv_resultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cadena = e.CommandName;
        GridViewRow row;
        int index = Convert.ToInt32(e.CommandArgument);
        row = dgv_resultado.Rows[index];

        switch (cadena)
        { 
            case "Add":
                Response.Redirect("ModuloContenido.aspx?modo=0");  
                break;
            case "Edit":
                if (row.Cells[2].Text != "...")
                    Response.Redirect("ModuloContenido.aspx?modo=1&id=" + row.Cells[2].Text);  
                break; 
        }
    }

    protected void dgv_resultado_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void BtnModModulo_Click(object sender, EventArgs e)
    {
        if (CboModulo.SelectedValue.ToString() != "-1")
        {
            Response.Redirect("Modulo.aspx?modo=1&id=" + CboModulo.SelectedValue.ToString());
        }
    }

    protected void BtnModContenido_Click(object sender, EventArgs e)
    {
        if (CboContenido.SelectedValue.ToString()  != "-1")
        {
            Response.Redirect("Contenido.aspx?modo=1&id=" + CboContenido.SelectedValue.ToString());
        }
        //mirm
    }

    protected void dgv_resultado_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}
