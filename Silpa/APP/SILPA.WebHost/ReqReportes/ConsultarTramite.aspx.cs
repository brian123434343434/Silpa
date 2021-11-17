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

public partial class Salvoconducto_ConsultarSalvoconductoWeb : System.Web.UI.Page
{
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            id = Request.QueryString["opc"];
            CargarCombo(ref this.CboTipoTramite, CargarInfoBasica("TRA_SS_LST_TRAMITE"));
            CargarCombo(ref this.CboAutoridadAmbiental, CargarInfoBasica("AUT_SS_LST_AUTORIDAD"));
            CargarCombo(ref this.CboSector, CargarInfoBasica("SEC_SS_LST_SECTOR"));
            CargarCombo(ref this.CboTipoProyecto, CargarInfoBasica("SEC_SS_LST_SECTOR"));
            CargarCombo(ref this.CboEtapa, CargarInfoBasica("ETA_SS_LST_ETAPA"));
            CargarCombo(ref this.CboActividad, CargarInfoBasica("ACT_SS_LST_ACTIVIDAD"));
            CargarCombo(ref this.CboEstado, CargarInfoBasica("TRA_SS_LST_TRAMITE_ESTADO"));

            //SEC_SS_LST_SECTOR
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
            string _cadena = ConfigurationManager.ConnectionStrings["SILAMCConnectionString"].ConnectionString;
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

    private DataTable Resultado()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Resultado.Inicio");
            string _cadena = ConfigurationManager.ConnectionStrings["SILAMCConnectionString"].ConnectionString;
            ParametrosLocalizacion param;
            using (SqlConnection _con = new SqlConnection(_cadena))
            {
                _con.Open();
                using (SqlCommand _com = new SqlCommand("LST_SS_LST_TRAMITE", _con))
                {
                    _com.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adp = new SqlDataAdapter(); ;
                    DataTable dat = new DataTable();

                    param = new ParametrosLocalizacion();

                    param.Add("@P_FECHA_INICIAL", TxtFechaInicial.Text, DbType.String, ParameterDirection.Input);
                    param.Add("@P_FECHA_FINAL", TxtFechaFinal.Text, DbType.String, ParameterDirection.Input);
                    param.Add("@P_TRA_ID", CboTipoTramite.SelectedValue, DbType.Decimal, ParameterDirection.Input);
                    param.Add("@P_SEC_ID", CboSector.SelectedValue, DbType.Decimal, ParameterDirection.Input);
                    param.Add("@P_EST_ID", CboEstado.SelectedValue, DbType.Decimal, ParameterDirection.Input);
                    param.Add("@P_EXPEDIENTE", TxtExpediente.Text, DbType.String, ParameterDirection.Input);
                    if (id.Length > 0)
                        param.Add("@P_TERCER", id, DbType.Decimal, ParameterDirection.Input);

                    foreach (System.Data.SqlClient.SqlParameter p in param.Col)
                        _com.Parameters.Add(p);

                    adp.SelectCommand = _com;
                    dat = new DataTable();
                    adp.Fill(dat);
                    return dat;
                }
            }
        }

        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            throw;
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Resultado.Finalizo");
        }
    }

    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_resultado_error.Text = "";
            lbl_resultado_error.Visible = false;


            dgv_resultado.DataSource = Resultado();
            dgv_resultado.DataBind();

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("REQREPORTE", 2, "Se consulto Consultar Tramite");

        }
        catch(Exception ex)
        {
            lbl_resultado_error.Visible = true;
            Mensaje.ErrorCritico(this, ex); 
        }
    }   

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static CascadingDropDownNameValue[] GetDropDownContents(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        DataTable dat = CargarInfoBasica("LOC_SS_LST_LOCALIZACION");

        foreach (DataRow r in dat.Rows)
        {
            values.Add(new CascadingDropDownNameValue(r[1].ToString(), r[0].ToString()));
        }
        return values.ToArray();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static CascadingDropDownNameValue[] GetDropDownContents2(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        StringDictionary lector = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int dep;


        if (!lector.ContainsKey("Departamento") || !Int32.TryParse(lector["Departamento"], out dep))
        {
            return null;
        }
        DataTable dat = CargarInfoBasicaM(1, dep, "LOC_SS_LST_LOCALIZACION");
        foreach (DataRow r in dat.Rows)
        {
            values.Add(new CascadingDropDownNameValue(r[1].ToString(), r[0].ToString()));
        }
        return values.ToArray();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static CascadingDropDownNameValue[] GetDropDownContents3(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        StringDictionary lector = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int ciudad;

        if (!lector.ContainsKey("Ciudad") || !Int32.TryParse(lector["Ciudad"], out ciudad))
        {
            return null;
        }
        DataTable dat = CargarInfoBasicaM(2, ciudad, "LOC_SS_LST_LOCALIZACION");
        foreach (DataRow r in dat.Rows)
        {
            values.Add(new CascadingDropDownNameValue(r[1].ToString(), r[0].ToString()));
        }
        return values.ToArray();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static CascadingDropDownNameValue[] GetDropDownContents4(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        StringDictionary lector = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int ciudad;

        if (!lector.ContainsKey("Ciudad") || !Int32.TryParse(lector["Ciudad"], out ciudad))
        {
            return null;
        }
        DataTable dat = CargarInfoBasicaM(3, ciudad, "LOC_SS_LST_LOCALIZACION");
        foreach (DataRow r in dat.Rows)
        {
            values.Add(new CascadingDropDownNameValue(r[1].ToString(), r[0].ToString()));
        }
        return values.ToArray();
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
        if (e.CommandName == "Select")
        {
            string cadena = "";
            int index = Convert.ToInt32(e.CommandArgument);

            if (id == null)
            {
                GridViewRow row = dgv_resultado.Rows[index];
                cadena = "Tramites.aspx?tramite=" + row.Cells[1].Text;
                Response.Redirect(cadena);
            }
        }
    }

    protected void dgv_resultado_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
