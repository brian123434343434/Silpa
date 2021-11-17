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
using System.Data.SqlClient;
using SoftManagement.Log;

public partial class ModuloContenido : System.Web.UI.Page
{
    string _id = "";
    string _modo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            //cargar Informacion de los combos.
            CargarCombo(ref this.CboModulo, CargarInfoBasica("SEN_LISTAR_MODULO"));
            CargarCombo(ref this.CboTipoContenido, CargarInfoBasica("SEN_LISTAR_CONTENIDO"));
            if (Request.QueryString.Count > 1)
                _id = Request.QueryString[1];
            _modo = Request.QueryString[0];

            if (_modo == "0")
            {
                lbl_subtitulo.Text = "Contenido de Modulos - (Adicion)";
                TxtArchivos.Visible = false;
                BtnEditar.Visible = false;
                FlpArchivo.Visible = true;
            }
            else
            {
                TxtArchivos.Visible = true;
                BtnEditar.Visible = true;
                FlpArchivo.Visible = false;
                lbl_subtitulo.Text = "Contenido de Modulos - (Modificación)";
                if (_id != "")
                    CargarForma(Convert.ToInt32(_id));

            }
        }
    }

    private void CargarForma(int id)
    { 
        try
        {
            DataTable t = new DataTable();
            t = CargarInfoBasicaM(id, "SEN_LISTAR_MODULO_CONTENIDO");
            TxtId.Text = t.Rows[0]["ID_CONTENIDO"].ToString();
            TxtArchivos.Text = t.Rows[0]["DIRECCION"].ToString();
            CboModulo.SelectedValue = t.Rows[0]["ID_MODULO"].ToString() ;
            CboTipoContenido.SelectedValue = t.Rows[0]["ID_TIPO_CONTENIDO"].ToString();
            if (t.Rows[0]["CONTENIDO_ACTIVO"].ToString() == "0")
            {
                ChkActivo.Checked = false;
                ChkInactivo.Checked = true;
            }
            else
            {
                ChkActivo.Checked = true;
                ChkInactivo.Checked = false;
            }
        }
        catch(Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
        
    }

    private void CargarCombo(ref DropDownList cboData, DataTable data)
    {
        //Asociacion de los datos al objeto
        cboData.DataSource = data;

        //Asignacion de las propiedades de text y value
        cboData.DataValueField = data.Columns[0].ColumnName.ToString();
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

    private static DataTable CargarInfoBasicaM(int idBusqueda, string nombresp)
    {
            string _cadena = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
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

                    param.Add("@P_ID_MODULO", idBusqueda, DbType.Decimal, ParameterDirection.Input);

                    foreach (System.Data.SqlClient.SqlParameter p in param.Col)
                        _com.Parameters.Add(p);

                    adp.SelectCommand = _com;
                    dat = new DataTable();
                    adp.Fill(dat);
                    return dat;
                }
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

    private bool ValidarTamano(HttpPostedFile f)
    {
        string tamano = ConfigurationManager.AppSettings["FileSizeSensibilizacion"].ToString();

        if (f.ContentLength > Convert.ToInt32(tamano))
            return false;
        return true;
    }

    private string SubirArchivo()
    {
        //string direccionDestino = ConfigurationManager.AppSettings["DestinoSensibilizacion"].ToString();
        string direccionDestino = Server.MapPath(@"Contenido");
        string retorno = string.Empty;
        HttpPostedFile posted; 
        try
        {
            if (FlpArchivo.Visible == true)
            {
                if (FlpArchivo.HasFile == true)
                {
                    posted = FlpArchivo.PostedFile;
                    if (ValidarTamano(posted) == false)
                        return "El archivo sobre pasa el tamaño especificado para el sistema";
                    direccionDestino += @"\" + FlpArchivo.FileName.ToString();
                    FlpArchivo.SaveAs(direccionDestino);
                    retorno = "";
                }
                else
                    retorno = "Debe seleccionar un archivo";
            }
            else
                retorno = ""; 
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
        return retorno;
    }

    protected void BtnAceptar_Click(object sender, EventArgs e)
    {
        string _cadena = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
        ParametrosLocalizacion param;
        lbl_resultado_error.Visible = false;
        lbl_resultado_error.Text = "";
        int activo = 0;
        string cadenaArchivo = SubirArchivo();


        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".BtnAceptar_Click.Inicio");
            if (cadenaArchivo != "")
            {
                lbl_resultado_error.Visible = true;
                lbl_resultado_error.Text = cadenaArchivo;
                return;
            }
            //Conexión
            using (SqlConnection _con = new SqlConnection(_cadena))
            {
                _con.Open();
                if (lbl_subtitulo.Text == "Contenido de Modulos - (Modificación)")
                {
                    using (SqlCommand _com = new SqlCommand("SEN_EDICION_CONTENIDO_MODULO", _con))
                    {
                        _com.CommandType = CommandType.StoredProcedure;

                        param = new ParametrosLocalizacion();

                        if (ChkActivo.Checked == true)
                            activo = 1;
                        else
                            activo = 0;

                        param.Add("@P_ID_CONTENIDO", TxtId.Text, DbType.Decimal, ParameterDirection.Input);
                        param.Add("@P_ID_MODULO", CboModulo.SelectedValue, DbType.Decimal, ParameterDirection.Input);
                        param.Add("@P_ID_TIPO_CONTENIDO", CboTipoContenido.SelectedValue, DbType.Decimal, ParameterDirection.Input);
                        param.Add("@P_DIRECCION", NombreArchivo(), DbType.String, ParameterDirection.Input);
                        param.Add("@P_CONTENIDO_ACTIVO", activo, DbType.Decimal, ParameterDirection.Input);

                        foreach (System.Data.SqlClient.SqlParameter p in param.Col)
                            _com.Parameters.Add(p);

                        _com.ExecuteNonQuery();
                    }

                }
                else
                {
                    using (SqlCommand _com = new SqlCommand("SEN_ADICION_CONTENIDO_MODULO", _con))
                    {
                        _com.CommandType = CommandType.StoredProcedure;

                        param = new ParametrosLocalizacion();

                        if (ChkActivo.Checked == true)
                            activo = 1;
                        else
                            activo = 0;

                        param.Add("@P_ID_MODULO", CboModulo.SelectedValue, DbType.Decimal, ParameterDirection.Input);
                        param.Add("@P_ID_TIPO_CONTENIDO", CboTipoContenido.SelectedValue, DbType.Decimal, ParameterDirection.Input);
                        param.Add("@P_DIRECCION", NombreArchivo(), DbType.String, ParameterDirection.Input);
                        param.Add("@P_CONTENIDO_ACTIVO", activo, DbType.Decimal, ParameterDirection.Input);

                        foreach (System.Data.SqlClient.SqlParameter p in param.Col)
                            _com.Parameters.Add(p);

                        _com.ExecuteNonQuery();
                    }
                }
            }

            lbl_resultado_error.Visible = true;
            lbl_resultado_error.Text = "Adición realizada con éxito";

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("PARAMETRIZACIÓN SENSIBILIZACIÓN", 1, "Se almacenó Módulo Contenido");

        }
        catch (Exception ex)
        {
            lbl_resultado_error.Visible = true;
            Mensaje.ErrorCritico(this, ex);
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".BtnAceptar_Click.Finalizo");
        }
    }

    private string NombreArchivo()
    {
        if (FlpArchivo.Visible == false)
            return TxtArchivos.Text;
        else
            return FlpArchivo.PostedFile.FileName.ToString();    
    }

    protected void BtnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Parametrizacion.aspx");
    }

    protected void BtnEditar_Click(object sender, EventArgs e)
    {
        FlpArchivo.Visible = true; 
    }
}
