using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Salvoconducto;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Generico;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Linq;

public partial class Salvoconducto_AsignarNumeracionSalvoconducto : System.Web.UI.Page
{
    public DataTable dt_AUTORIDAD_AMBIENTAL = new DataTable();
    private AsignarNumeracionSalvoconducto LogicaNegocioSalvoconducto;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["Usuario"] = 429;
            //this.CargarPagina();
            if (new Utilidades().ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                this.CargarPagina();
            }
        }
    }


    protected void CargarPagina()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
      //Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], cboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridadesSUNL(null).Tables[0], cboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        dt_AUTORIDAD_AMBIENTAL = _listaAutoridades.ListarAutoridades(null).Tables[0];

        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _listaEstadosSeries = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        //Utilidades.LlenarComboTabla(_listaEstadosSeries.ObtenerEstadosSerieActivo(), CboEstadoSerie, "DESCRIPCION", "ESTADO_SERIE_ID", false);
        this.txtPjeAlertaSerie.Text = "0";
        this.TxtRangoDesde.Text = "0";
        this.TxtRangoHasta.Text = "0";
        this.txtPjeAlertaSerie.Text = "0";
        this.BtnGrabar.Enabled = false;
        this.BtnGrabar.ForeColor = System.Drawing.Color.White;
        this.TxtRangoDesde.ReadOnly = false;
        this.TxtRangoHasta.ReadOnly = false;
        this.TxtRangoDesde.ForeColor = System.Drawing.Color.Black;
        this.TxtRangoHasta.ForeColor = System.Drawing.Color.Black;
    }

    protected void BtnValidarNumeracion_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(TxtRangoDesde.Text) == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('El Rango Desde debe ser mayor a cero');", true);
            return;
        }

        DataTable dt = new DataTable();
        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _ValidarSerie = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        _ValidarSerie.V_SERIE_DESDE = Convert.ToInt32(TxtRangoDesde.Text);
        _ValidarSerie.V_SERIE_HASTA = Convert.ToInt32(TxtRangoHasta.Text);
        dt = _ValidarSerie.ValidarNumeracionSalvoconducto();

        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable dtResultado = new DataTable();
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            dt_AUTORIDAD_AMBIENTAL = _listaAutoridades.ListarAutoridades(null).Tables[0];

            var query = from listAutAmbiental in dt_AUTORIDAD_AMBIENTAL.AsEnumerable()
                        join series in dt.AsEnumerable()
                        on listAutAmbiental.Field<int>("AUT_ID") equals series.Field<int>("ID_AUT_AMBIENTAL")
                        select new
                        {
                            AUTORIDAD_AMBIENTAL = listAutAmbiental.Field<string>("AUT_NOMBRE"),
                            SERIE_DESDE = series.Field<int>("SERIE_DESDE"),
                            SERIE_HASTA = series.Field<int>("SERIE_HASTA"),
                            SERIE_DESCRIPCION = series.Field<string>("DESCRIPCION")
                        };


            dtResultado.Columns.Add(new DataColumn("Autoridad_Ambiental", typeof(string)));
            dtResultado.Columns.Add(new DataColumn("Numeracion", typeof(string)));
            dtResultado.Columns.Add(new DataColumn("Estado", typeof(string)));


            foreach (var Series in query)
            {
                DataRow dr = dtResultado.NewRow();
                dr["Autoridad_Ambiental"] = Series.AUTORIDAD_AMBIENTAL;
                dr["Numeracion"] = Convert.ToString(Series.SERIE_DESDE) + "-" + Convert.ToString(Series.SERIE_HASTA);
                dr["Estado"] = Series.SERIE_DESCRIPCION;
                dtResultado.Rows.Add(dr);
            }

            mpeValidarNumeracion.Show();
            DivEstadosNumeracion.Visible = true;
            GRVListadoNumeracion.DataSource = dtResultado;
            GRVListadoNumeracion.DataBind();
            this.BtnGrabar.Enabled = false;
            this.BtnGrabar.ForeColor = System.Drawing.Color.White;
            this.TxtRangoDesde.ReadOnly = false;
            this.TxtRangoHasta.ReadOnly = false;
            this.TxtRangoDesde.ForeColor = System.Drawing.Color.Black;
            this.TxtRangoHasta.ForeColor = System.Drawing.Color.Black;

        }
        else
        {
            mpeValidarNumeracion.Hide();
            DivEstadosNumeracion.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('El Rango Esta Disponible para asignar, por Favor Proceder');", true);
            this.BtnGrabar.Enabled = true;
            this.BtnGrabar.ForeColor = System.Drawing.Color.Black;
            this.TxtRangoDesde.ReadOnly = true;
            this.TxtRangoHasta.ReadOnly = true;
            this.TxtRangoDesde.ForeColor = System.Drawing.Color.Blue;
            this.TxtRangoHasta.ForeColor = System.Drawing.Color.Blue;
        }
    }

    protected void BtnGrabar_Click(object sender, EventArgs e)
    {
        GrabarSerie();
    }

    public void GrabarSerie()
    {
        bool respuesta;
        string msj = "";


        if (FUPLCrearSerie.HasFile) //5Mb
        {
            if (FUPLCrearSerie.FileBytes.Length > 5242880)
            {
                msj = "El archivo adjunto supera el tope maximo de tamaño debe ser inferior a 5 Mb";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                return;
            }
        }


        if (!Directory.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"CreacionSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value)))
            Directory.CreateDirectory(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"CreacionSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value));
        string ruta_archivos = System.Configuration.ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"CreacionSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value);

        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _GrabarSerie = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        _GrabarSerie.V_ID_AUT_AMBIENTAL = Convert.ToInt32(cboAutoridadAmbiental.SelectedItem.Value);
        _GrabarSerie.V_SERIE_DESDE = Convert.ToInt32(TxtRangoDesde.Text);
        _GrabarSerie.V_SERIE_HASTA = Convert.ToInt32(TxtRangoHasta.Text);
        _GrabarSerie.V_CNT_SERIES_ALERTA = Convert.ToInt32(txtPjeAlertaSerie.Text);
        _GrabarSerie.V_NOMBRE_ARCHIVO_CREACION_SERIE = this.FUPLCrearSerie.FileName;
        _GrabarSerie.V_RUTA_ARCHIVO_CREACION_SERIE = ruta_archivos;
        _GrabarSerie.V_CODIGO_USUARIO = Convert.ToString(Session["Usuario"]);
        respuesta = _GrabarSerie.GrabarSerie();
        if (respuesta == true)
        {
            try
            {
                if (Directory.Exists(ruta_archivos) && (FUPLCrearSerie.HasFile))
                {
                    this.FUPLCrearSerie.SaveAs(ruta_archivos + this.FUPLCrearSerie.FileName);
                }
                msj = "El rango de " + this.TxtRangoDesde.Text + " - " + this.TxtRangoHasta.Text + " Fue Grabado Exitosamente";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
                CargarPagina();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    protected void Salir_Click(object sender, EventArgs e)
    {
        mpeValidarNumeracion.Hide();
    }

    protected void BtnCancelar_Click(object sender, EventArgs e)
    {
        this.CargarPagina();
    }





}