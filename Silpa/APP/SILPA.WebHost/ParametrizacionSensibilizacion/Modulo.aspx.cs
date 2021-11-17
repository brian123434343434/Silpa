using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AjaxControlToolkit;
using SoftManagement.Log;

public partial class Modulo : System.Web.UI.Page
{
    string _id = "";
    string _modo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Mensaje.LimpiarMensaje(this);
       
        if (IsPostBack == false)
        {
            if (Request.QueryString.Count > 1)
                _id = Request.QueryString[1];
            _modo = Request.QueryString[0];


            if (_modo == "0")
                lbl_subtitulo.Text = "Contenido de Modulos - (Adicion)";
            else
            {
                lbl_subtitulo.Text = "Contenido de Modulos - (Modificación)";
                DataTable t = new DataTable();
                t = CargarInfoBasicaM(Convert.ToInt32(_id), "SEN_LISTAR_MODULO");

                TxtId.Text = t.Rows[0][0].ToString();
                TxtModulo.Text = t.Rows[0][1].ToString();

                if (t.Rows[0][2].ToString() == "1")
                {
                    ChkActivo.Checked = true;
                    ChkInactivo.Checked = false;
                }
                else
                {
                    ChkActivo.Checked = false;
                    ChkInactivo.Checked = true;
                }
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

    protected void BtnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".BtnAceptar_Click.Inicio");
            string _cadena = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
            ParametrosLocalizacion param;
            lbl_resultado_error.Visible = false;
            lbl_resultado_error.Text = "";
            try
            {
                using (SqlConnection _con = new SqlConnection(_cadena))
                {
                    _con.Open();
                    if (lbl_subtitulo.Text == "Contenido de Modulos - (Modificación)")
                    {
                        using (SqlCommand _com = new SqlCommand("SEN_MODIFICAR_MODULO", _con))
                        {
                            int activo = 0;
                            _com.CommandType = CommandType.StoredProcedure;

                            param = new ParametrosLocalizacion();

                            if (ChkActivo.Checked == true)
                                activo = 1;
                            else
                                activo = 0;

                            param.Add("@P_ID_MODULO", Convert.ToInt32(TxtId.Text), DbType.Decimal, ParameterDirection.Input);
                            param.Add("@P_MODULO", TxtModulo.Text, DbType.String, ParameterDirection.Input);
                            param.Add("@P_MODULO_ACTIVO", activo, DbType.Decimal, ParameterDirection.Input);

                            foreach (System.Data.SqlClient.SqlParameter p in param.Col)
                                _com.Parameters.Add(p);

                            _com.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand _com = new SqlCommand("SEN_ADICIONAR_MODULO", _con))
                        {
                            int activo = 0;
                            _com.CommandType = CommandType.StoredProcedure;

                            param = new ParametrosLocalizacion();

                            if (ChkActivo.Checked == true)
                                activo = 1;
                            else
                                activo = 0;

                            param.Add("@P_MODULO", TxtModulo.Text, DbType.String, ParameterDirection.Input);
                            param.Add("@P_MODULO_ACTIVO", activo, DbType.Decimal, ParameterDirection.Input);

                            foreach (System.Data.SqlClient.SqlParameter p in param.Col)
                                _com.Parameters.Add(p);

                            _com.ExecuteNonQuery();
                        }
                    }

                }
                GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                CrearLogAuditoria.Insertar("PARAMETRIZACIÓN SENSIBILIZACIÓN", 1, "Se almacenó módulo");

                Mensaje.MostrarMensaje(this, "Cambio realizado con éxito");
            }
            catch (Exception ex)
            {
                Mensaje.ErrorCritico(this, ex);
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".BtnAceptar_Click.Finalizo");
        }
    }

    protected void BtnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Parametrizacion.aspx");
    }
}
