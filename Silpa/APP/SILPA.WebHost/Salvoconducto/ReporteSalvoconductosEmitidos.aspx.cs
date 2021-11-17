using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Generico;
using System.Configuration;
using System.IO;
using Subgurim.Controles;
using System.Data;
using SILPA.LogicaNegocio.Salvoconducto;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Aprovechamiento;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Text;
using Silpa.Workflow.AccesoDatos;

public partial class Salvoconducto_ReporteSalvoconductosEmitidos : System.Web.UI.Page
{
    private string TitularSalvoconducto
    {
        get { return ViewState["TitularSalvoconducto"].ToString(); }
        set { ViewState["TitularSalvoconducto"] = value; }
    }
    public bool usuarioConsulta { get{return(bool)ViewState["usuarioConsulta"];} set{ViewState["usuarioConsulta"] = value;} }
    public DataSet dt_resultado
    {
        get
        {
            return (DataSet)ViewState["dt_resultado"];
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

            //descomentar antes del COMMIT
            //Session["Usuario"] = 32439; MADS
            //Session["Usuario"] = 32511; //CODECHOCO
            //CargarPagina();
            //return;
            if (new Utilidades().ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {

                CargarPagina();
            }
        }
    }



    protected void lnkSolicitante_Click(object sender, EventArgs e)
    {
        this.mpeSolicitantes.Show();
    }





    protected void BtnConsultar_Click(object sender, EventArgs e)
    {
        int AUT_ID = 0;
        DateTime FEC_EXP_DESDE = Convert.ToDateTime("01/01/1900");
        DateTime FEC_EXP_HASTA = Convert.ToDateTime("01/01/1900");
        int TIPO_FILTRO_VIGENCIA = 1;
        string NUMERO_SALVOCONDUCTO = "";
        int DPTO_ORIGEN_ID = 0;
        int MUNICIPIO_ORIGEN_ID = 0;
        int DPTO_DESTINO_ID = 0;
        int MUNICIPIO_DESTINO_ID = 0;
        int TITULAR_ID = 0;
        int TIPO_SALVOCONDUCTO_ID = 0;
        int ESTADO_ID = 0;
        int CLASE_RECURSO_ID = 0;
        bool SN_RESOLUCION_438 = false;

        SeguimientoRutaSalvoconducto clsSeguimientoSalvoconducto = new SeguimientoRutaSalvoconducto();
        DataSet ds = new DataSet();

        if (cboEstadoSalvoconducto.SelectedValue != string.Empty)
        {
            ESTADO_ID = Convert.ToInt32(cboEstadoSalvoconducto.SelectedValue);
        }

        if (int.TryParse(CboAutoridadAmbiental.SelectedValue, out AUT_ID))
        {
            AUT_ID = Convert.ToInt32(CboAutoridadAmbiental.SelectedValue);
        }
        else
        {
            AUT_ID = 0;
        }

        if (!string.IsNullOrEmpty(TxtFecExpDesde.Text) && DateTime.TryParse(TxtFecExpHasta.Text, out FEC_EXP_DESDE))
        {
            FEC_EXP_DESDE = Convert.ToDateTime(TxtFecExpDesde.Text);
        }

        if (!string.IsNullOrEmpty(TxtFecExpHasta.Text) && DateTime.TryParse(TxtFecExpHasta.Text, out FEC_EXP_HASTA))
        {
            FEC_EXP_HASTA = Convert.ToDateTime(TxtFecExpHasta.Text);
        }

        if (ChkTodos.Checked)
        {
            TIPO_FILTRO_VIGENCIA = 0;
        }

        if (ChkVigentes.Checked)
        {
            TIPO_FILTRO_VIGENCIA = 1;
        }

        if (ChkNoVigentes.Checked)
        {
            TIPO_FILTRO_VIGENCIA = 2;
        }

        if (!string.IsNullOrEmpty(TxtNumeroSalvoconducto.Text))
        {
            NUMERO_SALVOCONDUCTO = TxtNumeroSalvoconducto.Text.Trim();
        }

        if (!string.IsNullOrEmpty(CboDepartamentoOrigen.SelectedValue) && Convert.ToInt32(CboDepartamentoOrigen.SelectedValue) > 0)
        {
            DPTO_ORIGEN_ID = Convert.ToInt32(CboDepartamentoOrigen.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboMunicipioOrigen.SelectedValue) && Convert.ToInt32(CboMunicipioOrigen.SelectedValue) > 0)
        {
            MUNICIPIO_ORIGEN_ID = Convert.ToInt32(CboMunicipioOrigen.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboDepartamentoDestino.SelectedValue) && Convert.ToInt32(CboDepartamentoDestino.SelectedValue) > 0)
        {
            DPTO_DESTINO_ID = Convert.ToInt32(CboDepartamentoDestino.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboMunicipioDestino.SelectedValue) && Convert.ToInt32(CboMunicipioDestino.SelectedValue) > 0)
        {
            MUNICIPIO_DESTINO_ID = Convert.ToInt32(CboMunicipioDestino.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboTipoSalvoconducto.SelectedValue))
        {
            TIPO_SALVOCONDUCTO_ID = Convert.ToInt32(CboTipoSalvoconducto.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboClaseRecurso.SelectedValue))
        {
            CLASE_RECURSO_ID = Convert.ToInt32(CboClaseRecurso.SelectedValue);
        }

        if (!string.IsNullOrEmpty(txtSolicitante.Text))
        {
            TITULAR_ID = Convert.ToInt32(TitularSalvoconducto);
        }
        else
        {
            TITULAR_ID = 0;
        }

        if (this.CboClaseSalvoconducto.SelectedValue == "2")
        {
            SN_RESOLUCION_438 = true;
        }

        ds = clsSeguimientoSalvoconducto.ConsultaSalvoconductosEmitidos(AUT_ID, FEC_EXP_DESDE, FEC_EXP_HASTA, TIPO_FILTRO_VIGENCIA, NUMERO_SALVOCONDUCTO, DPTO_ORIGEN_ID, MUNICIPIO_ORIGEN_ID, DPTO_DESTINO_ID, MUNICIPIO_DESTINO_ID, TITULAR_ID, TIPO_SALVOCONDUCTO_ID, ESTADO_ID, CLASE_RECURSO_ID, usuarioConsulta, SN_RESOLUCION_438);
        dt_resultado = ds;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('No hay datos para esta consulta')</script>", false);
            this.GrvSalvoconductosRes438.DataSource = ds;
            this.GrvSalvoconductosRes438.DataBind();
            this.grvEstadoSalvoconducto.DataSource = ds;
            this.grvEstadoSalvoconducto.DataBind();
            return;
        }

        if (SN_RESOLUCION_438)
        {
            this.grvEstadoSalvoconducto.DataSource = null;
            this.grvEstadoSalvoconducto.DataBind();
            this.GrvSalvoconductosRes438.DataSource = ds;
            this.GrvSalvoconductosRes438.DataBind();
            this.lblTotalRegistros.Text = string.Format("Número total de registros : {0}", ((DataSet)this.GrvSalvoconductosRes438.DataSource).Tables[0].Rows.Count);

        }
        else
        {
            this.GrvSalvoconductosRes438.DataSource = null;
            this.GrvSalvoconductosRes438.DataBind();
            this.grvEstadoSalvoconducto.DataSource = ds;
            this.grvEstadoSalvoconducto.DataBind();
            this.lblTotalRegistros.Text = string.Format("Número total de registros : {0}", ((DataSet)this.grvEstadoSalvoconducto.DataSource).Tables[0].Rows.Count);
        }

        CargarTriggers();
    }
    public void CargarPagina()
    {
        usuarioConsulta = false;
        PersonaDalc per = new PersonaDalc();
        PersonaIdentity p = new PersonaIdentity();
        int autID = 0;
        string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(Session["Usuario"].ToString()), out autID);
        p = per.BuscarPersonaByUserId(Session["Usuario"].ToString());
        SILPA.LogicaNegocio.Usuario.Usuario clsusuario = new SILPA.LogicaNegocio.Usuario.Usuario();
        DataTable dtautoridad = clsusuario.ConsultarUsuarioSistemaCompania(p.NumeroIdentificacion);
        var grupo = dtautoridad.AsEnumerable().Where(x => x.Field<string>("nombregrupo").Contains("SeguimientoSUNL") || x.Field<string>("nombregrupo").Contains("Consulta_SUNL")).ToList();
        if(grupo.Count > 0)
        {
            usuarioConsulta = true;
        }
        //autID = 144;

        

        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        ClaseRecurso vClaseRecurso = new ClaseRecurso();
        TipoSalvoconducto clsTipoSalvoconducto = new TipoSalvoconducto();
        //Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], CboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridadesSUNL(null).Tables[0], CboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        Utilidades.LlenarComboLista(vClaseRecurso.ListaClaseRecurso(), CboClaseRecurso, "ClaseRecurso", "ClaseRecursoID", true);
        Utilidades.LlenarComboLista(clsTipoSalvoconducto.ListaTipoSalvoconducto(), CboTipoSalvoconducto, "TipoSalvoconducto", "TipoSalvoconductoID", true);
        Listas _listaTiposId = new Listas();
        DataSet _temp = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacion.DataSource = _temp.Tables[0]; // ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacion, "TID_NOMBRE", "TID_ID", true);

        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], CboDepartamentoOrigen, "DEP_NOMBRE", "DEP_ID", false);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], CboDepartamentoDestino, "DEP_NOMBRE", "DEP_ID", false);
        Utilidades.LlenarComboVacio(this.CboMunicipioOrigen);
        Utilidades.LlenarComboVacio(this.CboMunicipioDestino);

        if (autID == 134 || p.PrimerNombre.ToUpper().Contains("IDEAM") || p.PrimerNombre.ToUpper().Contains("PROCURADURIA") || usuarioConsulta) //para los usuarios de la anla y el mads se debe consultar todas las autoridades ambientales
        {
            usuarioConsulta = true;
            this.CboAutoridadAmbiental.Enabled = true;
        }
        else
        {
            this.CboAutoridadAmbiental.Enabled = false;
            this.CboAutoridadAmbiental.SelectedValue = autID.ToString();
        }

        this.txtSolicitante.Text = "";
        this.TxtNumeroSalvoconducto.Text = "";
        this.TxtFecExpDesde.Text = "";
        this.TxtFecExpHasta.Text = "";
    }

    protected void btnBuscarSolicitante_Click(object sender, EventArgs e)
    {
        PersonaDalc persona = new PersonaDalc();
        DataTable datos = persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text);
        datos = datos.AsEnumerable().Where(x => x.Field<int>("TIENE_GRUPO") != 0).CopyToDataTable();
        if (datos.Rows.Count > 0)
        {
            this.lblNombreSolicitante.Text = datos.Rows[0]["NOMBRE"].ToString();
            this.TitularSalvoconducto = datos.Rows[0]["ID_APPLICATION_USER"].ToString();
            this.lnkSeleccionarSolicitante.Visible = true;
        }
        else
        {
            this.lblNombreSolicitante.Text = "";
            this.lnkSeleccionarSolicitante.Visible = false;
            this.lnkSeleccionarSolicitante.Visible = false;
            lblNombreSolicitante.Text = "No existe";
        }
        this.mpeSolicitantes.Show();
    }

    protected void lnkSeleccionarSolicitante_Click(object sender, EventArgs e)
    {
        this.txtSolicitante.Text = lblNombreSolicitante.Text;
        this.hfIdSolicitante.Value = TitularSalvoconducto;
        LimpiarSolicitante();
        this.mpeSolicitantes.Hide();
    }

    protected void LimpiarSolicitante()
    {
        this.cboTipoIdentificacion.SelectedIndex = 0;
        this.txtNumeroIdentificacion.Text = "";
        this.lblNombreSolicitante.Text = "";
        this.lnkSeleccionarSolicitante.Visible = false;
    }
    protected void btnCerrarVinculosActividad_Click(object sender, EventArgs e)
    {
        LimpiarSolicitante();
        this.mpeSolicitantes.Hide();
    }

    protected void CboDepartamentoOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CboDepartamentoOrigen.SelectedValue == "")
        {
            CboMunicipioOrigen.Items.Clear();
            CboMunicipioOrigen.Items.Insert(0, new ListItem("Todos.", ""));
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(CboDepartamentoOrigen.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, CboMunicipioOrigen, "MUN_NOMBRE", "MUN_ID", false);
            CboMunicipioOrigen.Items.Insert(1, new ListItem("(Todos)", "-1"));
        }
    }
    protected void CboDepartamentoDestino_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CboDepartamentoDestino.SelectedValue == "")
        {
            CboMunicipioDestino.Items.Clear();
            CboMunicipioDestino.Items.Insert(0, new ListItem("Todos.", ""));
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(CboDepartamentoDestino.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, CboMunicipioDestino, "MUN_NOMBRE", "MUN_ID", false);
            CboMunicipioDestino.Items.Insert(1, new ListItem("(Todos)", "-1"));
        }
    }

    public string urlNavegacionVerSalvoconducto(object salvID)
    {
        string parametros = "SalvoConductoID=" + Convert.ToString(salvID) + "&BloqueoSalvoConducto = false  &SnConsultaCiudadano = false"; 
        string query = Utilidades.Encrypt(parametros);
        string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
        return queryEncriptado;
    }


    protected void grvEstadoSalvoconducto_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        #region método para asignar ruta que permita descargar archivo
        if (e.CommandName == "DescargarArchivo")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString().Split(',')[0]) && File.Exists(e.CommandArgument.ToString().Split(',')[0]))
            {
                #region método para asignar ruta que permita descargar archivo
                Byte[] arrContent;
                System.IO.FileStream FS;
                FS = null;
                FS = new System.IO.FileStream(Convert.ToString(e.CommandArgument.ToString().Split(',')[0]), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                #endregion
            }
            else
            {
                #region método para asignar ruta de archcivo por demanda que permita descargar archivo
                SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
                int SalvoID = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[1]);
                string RutaArchivo = clsSalvoconductoNew.GenerarPDFSalvoconducto(SalvoID);
                if (!string.IsNullOrEmpty(RutaArchivo) && File.Exists(RutaArchivo))
                {
                    grvEstadoSalvoconducto.DataSource = dt_resultado;
                    grvEstadoSalvoconducto.DataBind();
                    Byte[] arrContent;
                    System.IO.FileStream FS;
                    FS = null;
                    FS = new System.IO.FileStream(Convert.ToString(RutaArchivo), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                #endregion
            }
        }
        #endregion
    }

    protected void LnkArchivoAdjuntoCreacion_Click(object sender, EventArgs e)
    {

    }

    protected void grvEstadoSalvoconducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvEstadoSalvoconducto.PageIndex = e.NewPageIndex;
        grvEstadoSalvoconducto.DataSource = dt_resultado;
        grvEstadoSalvoconducto.DataBind();
        CargarTriggers();
    }


    protected void imbExportarExcel_Click(object sender, ImageClickEventArgs e)
    {

        //if (this.grvEstadoSalvoconducto.Rows.Count > 0)
        //{
            if (dt_resultado != null)
            {
                if (dt_resultado.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Page pagina = new Page();


                    DataTable dt = new DataTable();
                    dt = GenerarReporte();
                    GridView _grdTmpResiultado = new GridView();
                    _grdTmpResiultado.EnableViewState = false;
                    _grdTmpResiultado.AllowPaging = false;
                    _grdTmpResiultado.DataSource = dt;
                    _grdTmpResiultado.DataBind();

                    HtmlForm form = new HtmlForm();
                    pagina.Controls.Add(form);
                    form.Controls.Add(_grdTmpResiultado);

                    pagina.RenderControl(htw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Informacion.xls");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = Encoding.Default;
                    Response.Write(sb.ToString());
                    Response.End();
                }
            }
        //}
    }

    public DataTable GenerarReporte()
    {
        int AUT_ID = 0;
        DateTime FEC_EXP_DESDE = Convert.ToDateTime("01/01/1900");
        DateTime FEC_EXP_HASTA = Convert.ToDateTime("01/01/1900");
        int TIPO_FILTRO_VIGENCIA = 1;
        string NUMERO_SALVOCONDUCTO = "";
        int DPTO_ORIGEN_ID = 0;
        int MUNICIPIO_ORIGEN_ID = 0;
        int DPTO_DESTINO_ID = 0;
        int MUNICIPIO_DESTINO_ID = 0;
        int TITULAR_ID = 0;
        int TIPO_SALVOCONDUCTO_ID = 0;
        int ESTADO_ID = 0;
        int CLASE_RECURSO_ID = 0;
        bool SN_RESOLUCION_438 = false;

        SeguimientoRutaSalvoconducto clsSeguimientoSalvoconducto = new SeguimientoRutaSalvoconducto();
        if (cboEstadoSalvoconducto.SelectedValue != string.Empty)
        {
            ESTADO_ID = Convert.ToInt32(cboEstadoSalvoconducto.SelectedValue);
        }
        if (int.TryParse(CboAutoridadAmbiental.SelectedValue, out AUT_ID))
        {
            AUT_ID = Convert.ToInt32(CboAutoridadAmbiental.SelectedValue);
        }
        else
        {
            AUT_ID = 0;
        }

        if (!string.IsNullOrEmpty(TxtFecExpDesde.Text) && DateTime.TryParse(TxtFecExpHasta.Text, out FEC_EXP_DESDE))
        {
            FEC_EXP_DESDE = Convert.ToDateTime(TxtFecExpDesde.Text);
        }

        if (!string.IsNullOrEmpty(TxtFecExpHasta.Text) && DateTime.TryParse(TxtFecExpHasta.Text, out FEC_EXP_HASTA))
        {
            FEC_EXP_HASTA = Convert.ToDateTime(TxtFecExpHasta.Text);
        }

        if (ChkTodos.Checked)
        {
            TIPO_FILTRO_VIGENCIA = 0;
        }

        if (ChkVigentes.Checked)
        {
            TIPO_FILTRO_VIGENCIA = 1;
        }

        if (ChkNoVigentes.Checked)
        {
            TIPO_FILTRO_VIGENCIA = 2;
        }

        if (!string.IsNullOrEmpty(TxtNumeroSalvoconducto.Text))
        {
            NUMERO_SALVOCONDUCTO = TxtNumeroSalvoconducto.Text.Trim();
        }

        if (!string.IsNullOrEmpty(CboDepartamentoOrigen.SelectedValue) && Convert.ToInt32(CboDepartamentoOrigen.SelectedValue) > 0)
        {
            DPTO_ORIGEN_ID = Convert.ToInt32(CboDepartamentoOrigen.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboMunicipioOrigen.SelectedValue) && Convert.ToInt32(CboMunicipioOrigen.SelectedValue) > 0)
        {
            MUNICIPIO_ORIGEN_ID = Convert.ToInt32(CboMunicipioOrigen.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboDepartamentoDestino.SelectedValue) && Convert.ToInt32(CboDepartamentoDestino.SelectedValue) > 0)
        {
            DPTO_DESTINO_ID = Convert.ToInt32(CboDepartamentoDestino.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboMunicipioDestino.SelectedValue) && Convert.ToInt32(CboMunicipioDestino.SelectedValue) > 0)
        {
            MUNICIPIO_DESTINO_ID = Convert.ToInt32(CboMunicipioDestino.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboTipoSalvoconducto.SelectedValue))
        {
            TIPO_SALVOCONDUCTO_ID = Convert.ToInt32(CboTipoSalvoconducto.SelectedValue);
        }

        if (!string.IsNullOrEmpty(CboClaseRecurso.SelectedValue))
        {
            CLASE_RECURSO_ID = Convert.ToInt32(CboClaseRecurso.SelectedValue);
        }

        if (!string.IsNullOrEmpty(txtSolicitante.Text))
        {
            TITULAR_ID = Convert.ToInt32(TitularSalvoconducto);
        }
        else
        {
            TITULAR_ID = 0;
        }

        if (this.CboClaseSalvoconducto.SelectedValue == "2")
        {
            SN_RESOLUCION_438 = true;
        }
        return clsSeguimientoSalvoconducto.ConsultaSalvoconductosEmitidosFullInfo(AUT_ID, FEC_EXP_DESDE, FEC_EXP_HASTA, TIPO_FILTRO_VIGENCIA, NUMERO_SALVOCONDUCTO, DPTO_ORIGEN_ID, MUNICIPIO_ORIGEN_ID, DPTO_DESTINO_ID, MUNICIPIO_DESTINO_ID, TITULAR_ID, TIPO_SALVOCONDUCTO_ID, ESTADO_ID, CLASE_RECURSO_ID, usuarioConsulta, SN_RESOLUCION_438);
    }

    protected void LnkLimpiarSolicitante_Click(object sender, EventArgs e)
    {
        TitularSalvoconducto = "";
        this.txtSolicitante.Text = "";
    }

    protected void LnkLimpiarFechas_Click(object sender, EventArgs e)
    {
        this.TxtFecExpDesde.Text = "";
        this.TxtFecExpHasta.Text = "";
    }

    private void CargarTriggers()
    {
        LinkButton LnkArchivoSalvoconducto = null;
        PostBackTrigger objPostTrigger = null;

        if (this.grvEstadoSalvoconducto.Visible && this.grvEstadoSalvoconducto.Rows.Count > 0)
        {
            foreach (GridViewRow objRowSerie in this.grvEstadoSalvoconducto.Rows)
            {
                LnkArchivoSalvoconducto = (LinkButton)objRowSerie.FindControl("LnkArchivoSalvoconducto");
                if (LnkArchivoSalvoconducto != null && LnkArchivoSalvoconducto.Visible)
                {
                    objPostTrigger = new PostBackTrigger();
                    objPostTrigger.ControlID = LnkArchivoSalvoconducto.UniqueID;
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(LnkArchivoSalvoconducto);
                }
            }
        }

        if (this.GrvSalvoconductosRes438.Visible && this.GrvSalvoconductosRes438.Rows.Count > 0)
        {
            foreach (GridViewRow objRowSerie in this.GrvSalvoconductosRes438.Rows)
            {
                LnkArchivoSalvoconducto = (LinkButton)objRowSerie.FindControl("LnkArchivoSalvoconducto");
                if (LnkArchivoSalvoconducto != null && LnkArchivoSalvoconducto.Visible)
                {
                    objPostTrigger = new PostBackTrigger();
                    objPostTrigger.ControlID = LnkArchivoSalvoconducto.UniqueID;
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(LnkArchivoSalvoconducto);
                }
            }
        }
    }

    protected void grvEstadoSalvoconducto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton LnkArchivoSalvoconducto = (LinkButton)e.Row.FindControl("LnkArchivoSalvoconducto");
        Label lblEstadoID = (Label)e.Row.FindControl("lblEstadoID");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!this.CboAutoridadAmbiental.Enabled && lblEstadoID.Text == "2") //esta validacion se realiza cuando el usuario logueado no es autoridad ambiental
            {
                if(usuarioConsulta)
                {
                    LnkArchivoSalvoconducto.Visible = false;
                }
                else
                {
                    LnkArchivoSalvoconducto.Visible = true;
                }
            }
            else
            {
                LnkArchivoSalvoconducto.Visible = false;
            }
        }


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    #region asignar url al link de descarga
        //    Label LblSalvocnductoID = (Label)e.Row.FindControl("LblSalvocnductoID");
        //    LinkButton LnkArchivoSalvoconducto = (LinkButton)e.Row.FindControl("LnkArchivoSalvoconducto");
        //    //DataSet ds = (DataSet)ViewState["dt_resultado"];
        //    //DataRow[] items = ds.Tables[0].Select("SALVOCONDUCTO_ID = " + Convert.ToInt32(LblSalvocnductoID.Text));
        //    //LnkArchivoSalvoconducto.PostBackUrl = items[0]["RUTA_ARCHIVO"].ToString();
        //    #endregion

        //    #region validación de  ruta de archivo
        //    if (!string.IsNullOrEmpty(LnkArchivoSalvoconducto.CommandArgument.ToString().Split(',')[0]))
        //    {
        //        LnkArchivoSalvoconducto.PostBackUrl = LnkArchivoSalvoconducto.CommandArgument.ToString().Split(',')[0].ToString();

        //        #region BORRAR :: método para asignar ruta que permita descargar archivo
        //        //Byte[] arrContent;
        //        //System.IO.FileStream FS;
        //        //FS = null;
        //        //FS = new System.IO.FileStream(Convert.ToString(LnkArchivoSalvoconducto.CommandArgument), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        //        //Byte[] Input = new byte[FS.Length];
        //        //FS.Read(Input, 0, int.Parse(FS.Length.ToString()));
        //        //arrContent = (byte[])Input;
        //        //string nombreArchivo = FS.Name.Split('\\').Last();
        //        //this.Response.ContentType = "application/octet-stream";
        //        //this.Response.OutputStream.Write(arrContent, 0, arrContent.Length);
        //        //this.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo); //this.GVASIGANCIONSERIES.SelectedRow.Cells[8].ToString());
        //        //FS.Close();
        //        //FS.Dispose();
        //        //FS = null;
        //        #endregion
        //    }
        //    else
        //    {
        //        #region método para asignar ruta que permita descargar archivo
        //        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        //        int SalvoID = Convert.ToInt32(LnkArchivoSalvoconducto.CommandArgument.ToString().Split(',')[1]);
        //        string RutaArchivo = clsSalvoconductoNew.GenerarPDFSalvocondcuto(SalvoID);
        //        if (!string.IsNullOrEmpty(RutaArchivo))
        //        {
        //            LnkArchivoSalvoconducto.PostBackUrl = RutaArchivo;

        //            #region BORRAR :: método para asignar ruta que permita descargar archivo
        //            //Byte[] arrContent;
        //            //System.IO.FileStream FS;
        //            //FS = null;
        //            //FS = new System.IO.FileStream(Convert.ToString(RutaArchivo), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        //            //Byte[] Input = new byte[FS.Length];
        //            //FS.Read(Input, 0, int.Parse(FS.Length.ToString()));
        //            //arrContent = (byte[])Input;
        //            //string nombreArchivo = FS.Name.Split('\\').Last();
        //            //this.Response.ContentType = "application/octet-stream";
        //            //this.Response.OutputStream.Write(arrContent, 0, arrContent.Length);
        //            //this.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo); //this.GVASIGANCIONSERIES.SelectedRow.Cells[8].ToString());
        //            //FS.Close();
        //            //FS.Dispose();
        //            //FS = null;
        //            #endregion
        //        }
        //        #endregion
        //    }
        //    #endregion
    }


    protected void GrvSalvoconductosRes438_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvSalvoconductosRes438.PageIndex = e.NewPageIndex;
        GrvSalvoconductosRes438.DataSource = dt_resultado;
        GrvSalvoconductosRes438.DataBind();
        CargarTriggers();
    }



    protected void CboClaseSalvoconducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (this.CboClaseSalvoconducto.SelectedValue)
        {
            case "1":
                trUbicacionDestino.Visible = true;
                this.CboDepartamentoDestino.Enabled = true;
                this.CboMunicipioDestino.Enabled = true;
                LblUbicacionOrigen.Visible = true;
                LblProcedencia.Visible = false;
                break;
            default:
                trUbicacionDestino.Visible = false;
                this.CboDepartamentoDestino.Enabled = false;
                this.CboMunicipioDestino.Enabled = false;
                LblUbicacionOrigen.Visible = false;
                LblProcedencia.Visible = true;
                break;
        }

    }

    protected void GrvSalvoconductosRes438_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region método para asignar ruta que permita descargar archivo
        if (e.CommandName == "DescargarArchivo")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString().Split(',')[0]) && File.Exists(e.CommandArgument.ToString().Split(',')[0]))
            {
                #region método para asignar ruta que permita descargar archivo
                Byte[] arrContent;
                System.IO.FileStream FS;
                FS = null;
                FS = new System.IO.FileStream(Convert.ToString(e.CommandArgument.ToString().Split(',')[0]), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                #endregion
            }

        }
        #endregion
    }
}