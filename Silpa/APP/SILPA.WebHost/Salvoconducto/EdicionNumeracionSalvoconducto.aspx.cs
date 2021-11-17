using AjaxControlToolkit;
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

public partial class Salvoconducto_EdicionNumeracionSalvoconducto : System.Web.UI.Page
{

    private AsignarNumeracionSalvoconducto LogicaNegocioSalvoconducto;

    public DataTable dt_AUTORIDAD_AMBIENTAL;

    public DataTable dt_resultado
    {
        get
        {
            return (DataTable)ViewState["dt_resultado"]; ;
        }

        set
        {
            ViewState["dt_resultado"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["Usuario"] = 429;
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
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], cboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        dt_AUTORIDAD_AMBIENTAL = _listaAutoridades.ListarAutoridades(null).Tables[0];

        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _listaEstadosSeries = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        Utilidades.LlenarComboTabla(_listaEstadosSeries.ObtenerEstadosSerie(), CboEstadoSerie, "DESCRIPCION", "ESTADO_SERIE_ID", true);

        this.TxtRangoDesde.Text = "0";
        this.TxtRangoHasta.Text = "0";
        //GridRegistroVacio();
    }

    protected void BtnConsultar_Click(object sender, EventArgs e)
    {
        ConsultarSeries();
    }

    protected void ConsultarSeries()
    {
        DataTable dt = new DataTable();
        //cargo las autoridades ambientales
        DataTable dt_AutoridadAmbiental = new DataTable();

        int ID_AUT_AMBIENTAL = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedItem.Value);

        int? SERIE_DESDE = null;
        if (this.TxtRangoDesde.Text != string.Empty)
            Convert.ToInt32(this.TxtRangoDesde.Text);

        int? SERIE_HASTA = null;
        if (this.TxtRangoHasta.Text != string.Empty)
            Convert.ToInt32(this.TxtRangoHasta.Text);

        DateTime? FECHA_INGRESO_INI = null;
        if (this.TextBoxFechaDesde.Text != string.Empty)
            FECHA_INGRESO_INI = Convert.ToDateTime(this.TextBoxFechaDesde.Text);

        DateTime? FECHA_INGRESO_FIN = null;
        if (this.TextBoxFechaHasta.Text != string.Empty)
            FECHA_INGRESO_FIN = Convert.ToDateTime(this.TextBoxFechaHasta.Text);


        int ESTADO_SERIE_ID = Convert.ToInt32(this.CboEstadoSerie.SelectedItem.Value);


        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        dt_AutoridadAmbiental = _listaAutoridades.ListarAutoridades(null).Tables[0];

        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _ConsultarSeries = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        dt = _ConsultarSeries.ConsultarSerieSalvoConducto(dt_AutoridadAmbiental, ID_AUT_AMBIENTAL, SERIE_DESDE, SERIE_HASTA, FECHA_INGRESO_INI, FECHA_INGRESO_FIN, ESTADO_SERIE_ID);

        if (dt.Rows.Count > 0)
        {
            dt_resultado = new DataTable();
            dt_resultado = dt;
            this.GVASIGANCIONSERIES.Columns[0].Visible = true;
            this.GVASIGANCIONSERIES.DataSource = dt;
            this.GVASIGANCIONSERIES.DataBind();
            CargarTriggers();
        }
        else
        {
            //GridRegistroVacio();
            this.GVASIGANCIONSERIES.Columns[0].Visible = false;
            this.GVASIGANCIONSERIES.DataSource = dt;
            this.GVASIGANCIONSERIES.DataBind();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DescargarArchivo")
        {
            Byte[] arrContent;
            System.IO.FileStream FS;
            FS = null;
            FS = new System.IO.FileStream(Convert.ToString(e.CommandArgument), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Byte[] Input = new byte[FS.Length];
            FS.Read(Input, 0, int.Parse(FS.Length.ToString()));
            arrContent = (byte[])Input;
            string nombreArchivo = FS.Name.Split('\\').Last();
            this.Response.ContentType = "application/octet-stream";
            this.Response.OutputStream.Write(arrContent, 0, arrContent.Length);
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo); //this.GVASIGANCIONSERIES.SelectedRow.Cells[8].ToString());
            FS.Close();
            FS.Dispose();
            FS = null;
        }

        if (e.CommandName == "EditarSerie")
        {
            CargarPantallaEdicion(Convert.ToInt32(e.CommandArgument));
        }

        if (e.CommandName == "BloquearSerie")
        {
            CargarPantallaBloqueo(Convert.ToInt32(e.CommandArgument));
        }
    }

    void CargarPantallaBloqueo(int id_serie)
    {
        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _BloquearSerie = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        _BloquearSerie.V_ID_SERIE = id_serie;
        this.MpeBloqueoSerie.Show();
        var query = from DatosEditar in dt_resultado.AsEnumerable()
                    where DatosEditar.Field<int>("ID_SERIE") == id_serie
                    select new
                    {
                        ID_SERIE = DatosEditar.Field<int>("ID_SERIE"),
                        AUTORIDAD_AMBIENTAL = DatosEditar.Field<string>("AUT_AMBIENTAL"),
                        SERIE_DESDE = DatosEditar.Field<string>("DESDE"),
                        SERIE_HASTA = DatosEditar.Field<string>("HASTA"),
                    };
        foreach (var DatosEditar in query)
        {
            LblIdSerieBloqueo.Text = Convert.ToString(DatosEditar.ID_SERIE);
            LblSerieBloquear.Text = DatosEditar.SERIE_DESDE + " - " + DatosEditar.SERIE_HASTA;
            LblAutAmbientalBloqueo.Text = DatosEditar.AUTORIDAD_AMBIENTAL;
        }

    }

    void CargarPantallaEdicion(int id_serie)
    {
        this.mpeValidarNumeracion.Show();

        var query = from DatosEditar in dt_resultado.AsEnumerable()
                    where DatosEditar.Field<int>("ID_SERIE") == id_serie
                    select new
                    {
                        ID_SERIE = DatosEditar.Field<int>("ID_SERIE"),
                        AUTORIDAD_AMBIENTAL = DatosEditar.Field<string>("AUT_AMBIENTAL"),
                        SERIE_DESDE = DatosEditar.Field<string>("DESDE"),
                        SERIE_HASTA = DatosEditar.Field<string>("HASTA"),
                        CNT_SERIES_ALERTA = DatosEditar.Field<int>("PJE_SERIES_ALERTA"),
                        NOMBRE_ARCHIVO_CREACION_SERIE = DatosEditar.Field<string>("NOMBRE_ARCHIVO_CREACION_SERIE"),
                    };
        foreach (var DatosEditar in query)
        {
            LblIdSerieCreacion.Text = Convert.ToString(DatosEditar.ID_SERIE);
            this.LblAutoridadAmbiental.Text = DatosEditar.AUTORIDAD_AMBIENTAL;
            this.TxtRangoDesdeEdit.Text = DatosEditar.SERIE_DESDE;
            this.TxtRangoHastaEdit.Text = DatosEditar.SERIE_HASTA;
            this.LblTotalSerie.Text = Convert.ToString(Convert.ToDouble(DatosEditar.SERIE_HASTA) - Convert.ToDouble(DatosEditar.SERIE_DESDE));
            this.txtCntAlertaSerie.Text = Convert.ToString(DatosEditar.CNT_SERIES_ALERTA);
        }
    }

    protected void GVASIGANCIONSERIES_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void LnkArchivoAdjuntoCreacion_Click(object sender, EventArgs e)
    {
    }

    protected void LnkArchivoAdjuntoBloqueo_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        int opcion = 0;
        if (!int.TryParse(this.CboEstadoSerie.SelectedItem.Value, out opcion))
        {
            opcion = 0;
        }
        ;
        switch (opcion)
        {
            case 1:
                this.GVASIGANCIONSERIES.Columns[0].Visible = true;
                this.GVASIGANCIONSERIES.Columns[7].Visible = false;
                this.GVASIGANCIONSERIES.Columns[8].Visible = false;
                break;
            case 2:
                this.GVASIGANCIONSERIES.Columns[0].Visible = false;
                this.GVASIGANCIONSERIES.Columns[7].Visible = false;
                this.GVASIGANCIONSERIES.Columns[8].Visible = false;
                break;
            case 4:
                this.GVASIGANCIONSERIES.Columns[0].Visible = true;
                this.GVASIGANCIONSERIES.Columns[7].Visible = false;
                this.GVASIGANCIONSERIES.Columns[8].Visible = false;
                break;
            default:
                this.GVASIGANCIONSERIES.Columns[0].Visible = false;
                this.GVASIGANCIONSERIES.Columns[7].Visible = true;
                this.GVASIGANCIONSERIES.Columns[8].Visible = true;

                break;
        }
    }

    void GridRegistroVacio()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("COMANDO", typeof(string)));
        dt.Columns.Add(new DataColumn("ID_SERIE", typeof(string)));
        dt.Columns.Add(new DataColumn("AUT_AMBIENTAL", typeof(String)));
        dt.Columns.Add(new DataColumn("DESDE", typeof(string)));
        dt.Columns.Add(new DataColumn("HASTA", typeof(string)));
        dt.Columns.Add(new DataColumn("PJE_SERIES_ALERTA", typeof(string)));
        dt.Columns.Add(new DataColumn("ESTADO_SERIE", typeof(String)));
        dt.Columns.Add(new DataColumn("RUTA_ARCHIVO_CREACION_SERIE", typeof(String)));
        dt.Columns.Add(new DataColumn("NOMBRE_ARCHIVO_CREACION_SERIE", typeof(string)));
        dt.Columns.Add(new DataColumn("RUTA_ARCHIVO_BLOQUEO_SERIE", typeof(string)));
        dt.Columns.Add(new DataColumn("NOMBRE_ARCHIVO_BLOQUEO_SERIE", typeof(string)));
        dt.Columns.Add(new DataColumn("MOTIVO_BLOQUEO", typeof(string)));


        DataRow dr = dt.NewRow();
        dr["COMANDO"] = "";
        dr["ID_SERIE"] = "";
        dr["AUT_AMBIENTAL"] = "";
        dr["DESDE"] = "";
        dr["HASTA"] = "";
        dr["PJE_SERIES_ALERTA"] = "";
        dr["ESTADO_SERIE"] = "";
        dr["RUTA_ARCHIVO_CREACION_SERIE"] = "";
        dr["NOMBRE_ARCHIVO_CREACION_SERIE"] = "";
        dr["NOMBRE_ARCHIVO_BLOQUEO_SERIE"] = "";
        dr["RUTA_ARCHIVO_BLOQUEO_SERIE"] = "";
        dr["MOTIVO_BLOQUEO"] = "";
        dt.Rows.Add(dr);

        this.GVASIGANCIONSERIES.Columns[0].Visible = false;
        this.GVASIGANCIONSERIES.DataSource = dt;
        this.GVASIGANCIONSERIES.DataBind();
        CargarTriggers();
    }

    protected void BtnValidarNumeracion_Click(object sender, EventArgs e)
    {
        String msj;

        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _ValidarNumeracion = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        msj = _ValidarNumeracion.ValidarNumeracion(Convert.ToInt32(LblIdSerieCreacion.Text), Convert.ToInt32(TxtRangoDesdeEdit.Text), Convert.ToInt32(TxtRangoHastaEdit.Text), Convert.ToInt32(this.LblTotalSerie.Text));

        if (msj != string.Empty && msj.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('" + msj + "');", true);
            BtnGrabar.Enabled = false;
            this.BtnGrabar.ForeColor = System.Drawing.Color.White;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('El Rango a Cambiar se encuentra Disponible');", true);
            BtnGrabar.Enabled = true;
            this.BtnGrabar.ForeColor = System.Drawing.Color.Black;
        }
    }

    public bool ActualizarSerie(int ID_SERIE)
    {
        bool respuesta;
        if (!Directory.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"CreacionSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value)))
            Directory.CreateDirectory(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"CreacionSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value));

        string ruta_archivos = System.Configuration.ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"CreacionSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value);

        if (FUPLCrearSerie.FileBytes.Length > 5242880) //5mb
        {
            this.LblValidacionArchivoCreacion.Text = "El Archivo Adjunto no debe ser superior a 5Mb";
            respuesta = false;
            return respuesta;
        }


        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _GrabarSerie = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        _GrabarSerie.V_ID_SERIE = ID_SERIE;
        _GrabarSerie.V_SERIE_DESDE = Convert.ToInt32(TxtRangoDesdeEdit.Text);
        _GrabarSerie.V_SERIE_HASTA = Convert.ToInt32(TxtRangoHastaEdit.Text);
        _GrabarSerie.V_CNT_SERIES_ALERTA = Convert.ToInt32(txtCntAlertaSerie.Text);
        _GrabarSerie.V_CODIGO_USUARIO = Convert.ToString(Session["Usuario"]);


        if (FUPLCrearSerie.FileName.Length > 0)
        {
            _GrabarSerie.V_NOMBRE_ARCHIVO_CREACION_SERIE = FUPLCrearSerie.FileName;
            _GrabarSerie.V_RUTA_ARCHIVO_CREACION_SERIE = ruta_archivos;
        }
        else
        {
            _GrabarSerie.V_NOMBRE_ARCHIVO_CREACION_SERIE = "";
            _GrabarSerie.V_RUTA_ARCHIVO_CREACION_SERIE = "";
        }

        respuesta = _GrabarSerie.EditarSerie();
        if (respuesta == true)
        {
            try
            {
                if (Directory.Exists(ruta_archivos) && (FUPLCrearSerie.HasFile))
                {
                    this.FUPLCrearSerie.SaveAs(ruta_archivos + FUPLCrearSerie.FileName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        return respuesta;
    }

    public bool BloquearSerie(int ID_SERIE)
    {

        bool respuesta = false;
        if (!Directory.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"BloqueoSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value)))
            Directory.CreateDirectory(ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"BloqueoSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value));

        string ruta_archivos = System.Configuration.ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"BloqueoSerieSalvoconducto\{0}\", cboAutoridadAmbiental.SelectedItem.Value);

        if (FUPLCargarAdjuntoBloqueo.FileBytes.Length > 5242880)
        {
            this.LblValidacionArchivoBloqueo.Text = "El Archivo Adjunto no debe ser superior a 5Mb";
            respuesta = false;
            return respuesta;
        }


        SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _BloquearSerie = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
        _BloquearSerie.V_ID_SERIE = ID_SERIE;
        _BloquearSerie.V_MOTIVO_BLOQUEO = this.TxtTextoBloqueo.Text;
        _BloquearSerie.V_RUTA_ARCHIVO_BLOQUEO_SERIE = ruta_archivos;
        _BloquearSerie.V_NOMBRE_ARCHIVO_BLOQUEO_SERIE = FUPLCargarAdjuntoBloqueo.FileName;
        _BloquearSerie.V_ESTADO_SERIE_ID = 3;
        respuesta = _BloquearSerie.BloquearSerie();

        if (respuesta == true)
        {
            try
            {
                if (Directory.Exists(ruta_archivos) && (FUPLCargarAdjuntoBloqueo.HasFile))
                {
                    this.FUPLCargarAdjuntoBloqueo.SaveAs(ruta_archivos + this.FUPLCargarAdjuntoBloqueo.FileName);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('El Rango Se Bloqueo con exito');", true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        return respuesta;

    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
        int ID_SERIE = Convert.ToInt32(this.LblIdSerieCreacion.Text);
        this.LblValidacionArchivoCreacion.Text = "";
        if (ActualizarSerie(ID_SERIE))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "alert('El Rango Se Grabo con exito');", true);
            ConsultarSeries();
            Limpiar();
        }
        else
        {
            mpeValidarNumeracion.Show();
        }
    }

    protected void BtnBloquearSerie_Click(object sender, EventArgs e)
    {
        int ID_SERIE = Convert.ToInt32(this.LblIdSerieBloqueo.Text);
        this.LblValidacionArchivoBloqueo.Text = "";
        if (BloquearSerie(ID_SERIE))
        {
            ConsultarSeries();
            Limpiar();
        }
        else
        {
            MpeBloqueoSerie.Show();
        }

    }

    protected void BtnCancelarBloqueo_Click(object sender, EventArgs e)
    {
        MpeBloqueoSerie.Hide();
    }

    protected void BtnCancelarEdicion_Click(object sender, EventArgs e)
    {
        mpeValidarNumeracion.Hide();
    }

    protected void Limpiar()
    {
        TxtTextoBloqueo.Text = "";
        TxtRangoDesdeEdit.Text = "0";
        TxtRangoHastaEdit.Text = "0";
        txtCntAlertaSerie.Text = "0";
        BtnGrabar.Enabled = false;
        BtnGrabar.ForeColor = System.Drawing.Color.White;
    }

    protected void GVASIGANCIONSERIES_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVASIGANCIONSERIES.PageIndex = e.NewPageIndex;
        GVASIGANCIONSERIES.DataSource = dt_resultado;
        GVASIGANCIONSERIES.DataBind();
        CargarTriggers();
    }

    protected void LimpiarFormPpal()
    {
        CboEstadoSerie.Enabled = true;
        TxtRangoDesde.Text = "";
        TxtRangoHasta.Text = "";
        TextBoxFechaDesde.Text = "";
        TextBoxFechaHasta.Text = "";
        GridRegistroVacio();
        this.GVASIGANCIONSERIES.Columns[0].Visible = false;

    }


    protected void SetearFechas()
    {
        this.TextBoxFechaDesde.Text = "";
        this.TextBoxFechaHasta.Text = "";
    }


    protected void BtnCancelarConsulta_Click(object sender, EventArgs e)
    {
        LimpiarFormPpal();
    }

    protected void LnkSetearFEchas_Click(object sender, EventArgs e)
    {
        SetearFechas();
    }

    private void CargarTriggers()
    {
        LinkButton LnkArchivoAdjuntoCreacion = null;
        LinkButton LnkArchivoAdjuntoBloqueo = null;
        PostBackTrigger objPostTrigger = null;

        if (this.GVASIGANCIONSERIES.Visible && this.GVASIGANCIONSERIES.Rows.Count > 0)
        {
            foreach (GridViewRow objRowSerie in this.GVASIGANCIONSERIES.Rows)
            {
                LnkArchivoAdjuntoCreacion = (LinkButton)objRowSerie.FindControl("LnkArchivoAdjuntoCreacion");
                if (LnkArchivoAdjuntoCreacion != null && LnkArchivoAdjuntoCreacion.Visible)
                {
                    objPostTrigger = new PostBackTrigger();
                    objPostTrigger.ControlID = LnkArchivoAdjuntoCreacion.UniqueID;
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(LnkArchivoAdjuntoCreacion);
                }
                LnkArchivoAdjuntoBloqueo = (LinkButton)objRowSerie.FindControl("LnkArchivoAdjuntoBloqueo");
                if (LnkArchivoAdjuntoBloqueo != null && LnkArchivoAdjuntoBloqueo.Visible)
                {
                    objPostTrigger = new PostBackTrigger();
                    objPostTrigger.ControlID = LnkArchivoAdjuntoBloqueo.UniqueID;
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(LnkArchivoAdjuntoBloqueo);
                }

            }
        }
    }



}