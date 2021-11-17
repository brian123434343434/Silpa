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
using SILPA.LogicaNegocio.Aprovechamiento;

public partial class Salvoconducto_ReporteAprovechamientos : System.Web.UI.Page
{
    private string TitularAprovechamiento
    {
        get { return ViewState["TitularAprovechamiento"].ToString(); }
        set { ViewState["TitularAprovechamiento"] = value; }
    }

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

            //DESCOMENTAR ANTES DEL COMMIT
            //Session["Usuario"] = 32439;
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
        DataTable DatosConsulta = ConsultarDatos();
        if (DatosConsulta.Rows.Count == 0 || DatosConsulta == null)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('No hay datos para esta consulta')</script>", false);
            return;
        }
                this.GrvEstadoAprovechamiento.DataSource = null;
                this.GrvEstadoAprovechamiento.EmptyDataText = string.Empty;
                this.GrvEstadoAprovechamiento.DataBind();


                if (DatosConsulta.Rows.Count == 0)
                    this.grvEstadoSalvoconducto.EmptyDataText = "No hay datos para esta consulta";
                this.grvEstadoSalvoconducto.DataSource = DatosConsulta;
                this.grvEstadoSalvoconducto.DataBind();
                this.lblTotalRegistros.Text = string.Format("Número total de registros : {0}", ((DataTable)this.grvEstadoSalvoconducto.DataSource).Rows.Count);
                CargarTriggers();
    }
    protected DataTable ConsultarDatos()
    {
        int? autoridadID = null;
        string strNumeroActo = null;
        int? intSolicitanteId = null;
        DateTime? fechaDesde = null;
        DateTime? fechaHasta = null;
        int? intClaseRecursoID = null;
        int? intFormaOtorgamientoID = null;
        int? intModoAdquisicionID = null;
        int? intDepartametnoProID = null;
        int? intMunicipipoProID = null;
        string strNombrePredio = null;

        Aprovechamiento clsAprovechamiento = new Aprovechamiento();

        if (this.CboAutoridadAmbiental.SelectedValue != string.Empty)
        {
            autoridadID = Convert.ToInt32(this.CboAutoridadAmbiental.SelectedValue);
        }
        if (this.TxtNumeroActoAdministrativo.Text.Trim() != string.Empty)
        {
            strNumeroActo = TxtNumeroActoAdministrativo.Text.Trim();
        }
        if (this.hfIdSolicitante.Value != string.Empty)
        {
            intSolicitanteId = Convert.ToInt32(this.hfIdSolicitante.Value);
        }
        if (this.TxtFecExpDesde.Text.Trim() != string.Empty)
        {
            fechaDesde = Convert.ToDateTime(this.TxtFecExpDesde.Text.Trim());
        }
        if (this.TxtFecExpHasta.Text.Trim() != string.Empty)
        {
            fechaHasta = Convert.ToDateTime(this.TxtFecExpHasta.Text.Trim());
        }
        if (this.cboClaseRecurso.SelectedValue != string.Empty)
        {
            intClaseRecursoID = Convert.ToInt32(this.cboClaseRecurso.SelectedValue);
        }
        if (this.cboFormaOtorgamiento.SelectedValue != string.Empty)
        {
            intFormaOtorgamientoID = Convert.ToInt32(this.cboFormaOtorgamiento.SelectedValue);
        }
        if (this.cboModoAdquisicion.SelectedValue != string.Empty)
        {
            intModoAdquisicionID = Convert.ToInt32(this.cboModoAdquisicion.SelectedValue);
        }
        if (this.CboDepartamentoProcedencia.SelectedValue != "-1")
        {
            intDepartametnoProID = Convert.ToInt32(this.CboDepartamentoProcedencia.SelectedValue);
        }
        if (this.CboMunicipioProcedencia.SelectedValue != string.Empty)
        {
            intMunicipipoProID = Convert.ToInt32(this.CboMunicipioProcedencia.SelectedValue);
        }
        if (this.txtNombrePredio.Text.Trim() != string.Empty)
        {
            strNombrePredio = this.txtNombrePredio.Text.Trim();
        }

        return clsAprovechamiento.ConsultaAprovechamientosCargados(autoridadID, strNumeroActo, intSolicitanteId, fechaDesde, fechaHasta, intClaseRecursoID, intFormaOtorgamientoID, intModoAdquisicionID, intDepartametnoProID, intMunicipipoProID, strNombrePredio);
    }
    public void CargarPagina()
    {

        PersonaDalc per = new PersonaDalc();
        PersonaIdentity p = new PersonaIdentity();
        int autID = 0;
        string autNombre = per.ObtenerAutoridadPorPersona(long.Parse(Session["Usuario"].ToString()), out autID);
        p = per.BuscarPersonaByUserId(Session["Usuario"].ToString());

        //autID = 144;


        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        ClaseRecurso vClaseRecurso = new ClaseRecurso();
        TipoAprovechamiento vTipoAprovechamiento = new TipoAprovechamiento();
        TipoSalvoconducto clsTipoSalvoconducto = new TipoSalvoconducto();
        TipoAprovechamientoIdentity clsTipoAprovechamiento = new TipoAprovechamientoIdentity();
        //Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], CboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);

        //Utilidades.LlenarComboLista(vTipoAprovechamiento.ListaTipoDocumento(), CboTipoAprovechamiento, "TipoAprovechamiento", "TipoAprovechamientoId", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridadesSUNL(null).Tables[0], CboAutoridadAmbiental, "AUT_NOMBRE", "AUT_ID", true);
        Utilidades.LlenarComboLista(vClaseRecurso.ListaClaseRecurso(), cboClaseRecurso, "ClaseRecurso", "ClaseRecursoID", true);
        Listas _listaTiposId = new Listas();
        DataSet _temp = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacion.DataSource = _temp.Tables[0]; // ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacion, "TID_NOMBRE", "TID_ID", true);

        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], CboDepartamentoProcedencia, "DEP_NOMBRE", "DEP_ID", false);
        Utilidades.LlenarComboVacio(this.CboMunicipioProcedencia);
        Utilidades.LlenarComboVacio(this.cboFormaOtorgamiento);
        Utilidades.LlenarComboVacio(this.cboModoAdquisicion);
        

        if (autID == 134 || p.PrimerNombre.ToUpper().Contains("IDEAM") || p.PrimerNombre.ToUpper().Contains("PROCURADURIA")) //para los usuarios de la anla y el mads se debe consultar todas las autoridades ambientales
        {
            this.CboAutoridadAmbiental.Enabled = true;
        }
        else
        {
            this.CboAutoridadAmbiental.Enabled = false;
            this.CboAutoridadAmbiental.SelectedValue = autID.ToString();
        }

        this.txtSolicitante.Text = "";
        this.TxtNumeroActoAdministrativo.Text = "";
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
            this.TitularAprovechamiento = datos.Rows[0]["ID_APPLICATION_USER"].ToString();
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
        this.hfIdSolicitante.Value = TitularAprovechamiento;
        this.mpeSolicitantes.Hide();
    }

    protected void LimpiarSolicitante()
    {
        this.cboTipoIdentificacion.SelectedIndex = 0;
        this.txtNumeroIdentificacion.Text = "";
        this.lblNombreSolicitante.Text = "";
        this.lnkSeleccionarSolicitante.Visible = false;
        this.hfIdSolicitante.Value = string.Empty;
    }
    protected void btnCerrarVinculosActividad_Click(object sender, EventArgs e)
    {
        LimpiarSolicitante();
        this.mpeSolicitantes.Hide();
    }

    protected void CboDepartamentoProcedencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CboDepartamentoProcedencia.SelectedValue == "")
        {
            CboMunicipioProcedencia.Items.Clear();
            CboMunicipioProcedencia.Items.Insert(0, new ListItem("Todos.", ""));
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(CboDepartamentoProcedencia.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboTabla(dtMunicipios, CboMunicipioProcedencia, "MUN_NOMBRE", "MUN_ID", false);
        }
    }
    
    public string urlNavegacionVerAprovechamiento(object salvID)
    {
        string parametros = "AprovechamientoID=" + Convert.ToString(salvID) + "&BloqueoSalvoConducto = false"; ;
        string query = Utilidades.Encrypt(parametros);
        string queryEncriptado = "../Salvoconducto/ConsultaDetalleAprovechamiento.aspx" + query;
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
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
                FS.Close();
                FS.Dispose();
                FS = null;
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
        grvEstadoSalvoconducto.DataSource = ConsultarDatos();
        grvEstadoSalvoconducto.DataBind();
        CargarTriggers();
    }


    protected void imbExportarExcel_Click(object sender, ImageClickEventArgs e)
    {
        int TipoAprovechamientoID = 0;
        int? autoridadID = null;
        string strNumeroActo = null;
        int? intSolicitanteId = null;
        DateTime? fechaDesde = null;
        DateTime? fechaHasta = null;
        int? intClaseRecursoID = null;
        int? intFormaOtorgamientoID = null;
        int? intModoAdquisicionID = null;
        int? intDepartametnoProID = null;
        int? intMunicipipoProID = null;
        string strNombrePredio = null;

        Aprovechamiento clsAprovechamiento = new Aprovechamiento();

         TipoAprovechamientoID = 1;
        if (this.CboAutoridadAmbiental.SelectedValue != string.Empty)
        {
            autoridadID = Convert.ToInt32(this.CboAutoridadAmbiental.SelectedValue);
        }
        if (this.TxtNumeroActoAdministrativo.Text.Trim() != string.Empty)
        {
            strNumeroActo = TxtNumeroActoAdministrativo.Text.Trim();
        }
        if (this.hfIdSolicitante.Value != string.Empty)
        {
            intSolicitanteId = Convert.ToInt32(this.hfIdSolicitante.Value);
        }
        if (this.TxtFecExpDesde.Text.Trim() != string.Empty)
        {
            fechaDesde = Convert.ToDateTime(this.TxtFecExpDesde.Text.Trim());
        }
        if (this.TxtFecExpHasta.Text.Trim() != string.Empty)
        {
            fechaHasta = Convert.ToDateTime(this.TxtFecExpHasta.Text.Trim());
        }
        if (this.cboClaseRecurso.SelectedValue != string.Empty)
        {
            intClaseRecursoID = Convert.ToInt32(this.cboClaseRecurso.SelectedValue);
        }
        if (this.cboFormaOtorgamiento.SelectedValue != string.Empty)
        {
            intFormaOtorgamientoID = Convert.ToInt32(this.cboFormaOtorgamiento.SelectedValue);
        }
        if (this.cboModoAdquisicion.SelectedValue != string.Empty)
        {
            intModoAdquisicionID = Convert.ToInt32(this.cboModoAdquisicion.SelectedValue);
        }
        if (this.CboDepartamentoProcedencia.SelectedValue != "-1")
        {
            intDepartametnoProID = Convert.ToInt32(this.CboDepartamentoProcedencia.SelectedValue);
        }
        if (this.CboMunicipioProcedencia.SelectedValue != string.Empty)
        {
            intMunicipipoProID = Convert.ToInt32(this.CboMunicipioProcedencia.SelectedValue);
        }
        if (this.txtNombrePredio.Text.Trim() != string.Empty)
        {
            strNombrePredio = this.txtNombrePredio.Text.Trim();
        }

        DataTable dtDatos = clsAprovechamiento.ConsultaAprovechamientosCargadosFullInfo(autoridadID, strNumeroActo, intSolicitanteId, fechaDesde, fechaHasta, intClaseRecursoID, intFormaOtorgamientoID, intModoAdquisicionID, intDepartametnoProID, intMunicipipoProID, strNombrePredio);

        if (dtDatos != null)
        {
            if (dtDatos.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                GridView _grdTmpResiultado = new GridView();
                _grdTmpResiultado.EnableViewState = false;
                _grdTmpResiultado.AllowPaging = false;
                _grdTmpResiultado.DataSource = dtDatos;
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
    }

    public DataTable GenerarReporte()
    {
        int TipoAprovechamientoID = 0;
        int? autoridadID = null;
        string strNumeroActo = null;
        int? intSolicitanteId = null;
        DateTime? fechaDesde = null;
        DateTime? fechaHasta = null;
        int? intClaseRecursoID = null;
        int? intFormaOtorgamientoID = null;
        int? intModoAdquisicionID = null;
        int? intDepartametnoProID = null;
        int? intMunicipipoProID = null;
        string strNombrePredio = null;

        Aprovechamiento clsAprovechamiento = new Aprovechamiento();
        TipoAprovechamientoID = 1;


        if (this.CboAutoridadAmbiental.SelectedValue != string.Empty)
        {
            autoridadID = Convert.ToInt32(this.CboAutoridadAmbiental.SelectedValue);
        }
        if (this.TxtNumeroActoAdministrativo.Text.Trim() != string.Empty)
        {
            strNumeroActo = TxtNumeroActoAdministrativo.Text.Trim();
        }
        if (this.hfIdSolicitante.Value != string.Empty)
        {
            intSolicitanteId = Convert.ToInt32(this.hfIdSolicitante.Value);
        }
        if (this.TxtFecExpDesde.Text.Trim() != string.Empty)
        {
            fechaDesde = Convert.ToDateTime(this.TxtFecExpDesde.Text.Trim());
        }
        if (this.TxtFecExpHasta.Text.Trim() != string.Empty)
        {
            fechaHasta = Convert.ToDateTime(this.TxtFecExpHasta.Text.Trim());
        }
        if (this.cboClaseRecurso.SelectedValue != string.Empty)
        {
            intClaseRecursoID = Convert.ToInt32(this.cboClaseRecurso.SelectedValue);
        }
        if (this.cboFormaOtorgamiento.SelectedValue != string.Empty)
        {
            intFormaOtorgamientoID = Convert.ToInt32(this.cboFormaOtorgamiento.SelectedValue);
        }
        if (this.cboModoAdquisicion.SelectedValue != string.Empty)
        {
            intModoAdquisicionID = Convert.ToInt32(this.cboModoAdquisicion.SelectedValue);
        }
        if (this.CboDepartamentoProcedencia.SelectedValue != string.Empty)
        {
            intDepartametnoProID = Convert.ToInt32(this.CboDepartamentoProcedencia.SelectedValue);
        }
        if (this.CboMunicipioProcedencia.SelectedValue != string.Empty)
        {
            intMunicipipoProID = Convert.ToInt32(this.CboMunicipioProcedencia.SelectedValue);
        }
        if (this.txtNombrePredio.Text.Trim() != string.Empty)
        {
            strNombrePredio = this.txtNombrePredio.Text.Trim();
        }

        return clsAprovechamiento.ConsultaAprovechamientosCargadosFullInfo(autoridadID, strNumeroActo, intSolicitanteId, fechaDesde, fechaHasta, intClaseRecursoID, intFormaOtorgamientoID, intModoAdquisicionID, intDepartametnoProID, intMunicipipoProID, strNombrePredio);
        
    }

    protected void LnkLimpiarSolicitante_Click(object sender, EventArgs e)
    {
        this.hfIdSolicitante.Value = "";
        TitularAprovechamiento = "";
        this.txtSolicitante.Text = "";
        LimpiarSolicitante();
    }

    protected void LnkLimpiarFechas_Click(object sender, EventArgs e)
    {
        this.TxtFecExpDesde.Text = "";
        this.TxtFecExpHasta.Text = "";
    }

    private void CargarTriggers()
    {
        LinkButton LnkArchivoAprovechamiento = null;
        PostBackTrigger objPostTrigger = null;

        if (this.grvEstadoSalvoconducto.Visible && this.grvEstadoSalvoconducto.Rows.Count > 0)
        {
            foreach (GridViewRow objRowSerie in this.grvEstadoSalvoconducto.Rows)
            {
                LnkArchivoAprovechamiento = (LinkButton)objRowSerie.FindControl("LnkArchivoAprovechamiento");
                if (LnkArchivoAprovechamiento != null && LnkArchivoAprovechamiento.Visible)
                {
                    objPostTrigger = new PostBackTrigger();
                    objPostTrigger.ControlID = LnkArchivoAprovechamiento.UniqueID;
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(LnkArchivoAprovechamiento);
                }
            }
        }
    }
    protected void cboClaseRecurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboClaseRecurso.SelectedValue != string.Empty)
        {
            ClaseAprovechamiento vClaseAprovechamiento = new ClaseAprovechamiento();
            ModoAdquisicionRecurso vModoAdquisicionRecurso = new ModoAdquisicionRecurso();
            ClaseProducto vClaseProducto = new ClaseProducto();
            FormaOtorgamiento vFormaOtorgamiento = new FormaOtorgamiento();
            //Utilidades.LlenarComboLista(vClaseAprovechamiento.ListaClaseAprovechamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue)),cboClaseAprovechamiento, "ClaseAprovechamiento", "ClaseAprovechamientoId", true);
            Utilidades.LlenarComboLista(vModoAdquisicionRecurso.ListaModoAdquisicionRecurso(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), false,null), cboModoAdquisicion, "ModAdqRecurso", "ModAdqRecursoID", true);
            Utilidades.LlenarComboLista(vFormaOtorgamiento.ListaFormaOtorgamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), false).Where(x => x.FormaOtorgamientoId != 10 && x.FormaOtorgamientoId != 11), cboFormaOtorgamiento, "FormaOtorgamiento", "FormaOtorgamientoID", true);
        }
        else
        {
            Utilidades.LlenarComboVacio(this.cboFormaOtorgamiento);
            Utilidades.LlenarComboVacio(cboModoAdquisicion);
            
        }
    }

    protected void GrvEstadoAprovechamiento_RowCommand(object sender, GridViewCommandEventArgs e)
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
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
                FS.Close();
                FS.Dispose();
                FS = null;
                #endregion
            }
        }
        #endregion
    }

    protected void GrvEstadoAprovechamiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvEstadoAprovechamiento.PageIndex = e.NewPageIndex;
        GrvEstadoAprovechamiento.DataSource = ConsultarDatos();
        GrvEstadoAprovechamiento.DataBind();
        CargarTriggersTipoAprovechamiento();
    }

    protected void LnkArchivoTipoAprovechamiento_Click(object sender, EventArgs e)
    {

    }

    protected void cboFormaOtorgamiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClaseAprovechamiento vClaseAprovechamiento = new ClaseAprovechamiento();
        ModoAdquisicionRecurso vModoAdquisicionRecurso = new ModoAdquisicionRecurso();
        ClaseProducto vClaseProducto = new ClaseProducto();
        FormaOtorgamiento vFormaOtorgamiento = new FormaOtorgamiento();
        Utilidades.LlenarComboLista(vModoAdquisicionRecurso.ListaModoAdquisicionRecurso(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), false, Convert.ToInt32(this.cboFormaOtorgamiento.SelectedValue)), cboModoAdquisicion, "ModAdqRecurso", "ModAdqRecursoID", true);
    }

    private void CargarTriggersTipoAprovechamiento()
    {
        LinkButton LnkArchivoTipoAprovechamiento = null;
        PostBackTrigger objPostTrigger = null;

        if (this.GrvEstadoAprovechamiento.Visible && this.GrvEstadoAprovechamiento.Rows.Count > 0)
        {
            foreach (GridViewRow objRowSerie in this.GrvEstadoAprovechamiento.Rows)
            {
                LnkArchivoTipoAprovechamiento = (LinkButton)objRowSerie.FindControl("LnkArchivoTipoAprovechamiento");
                if (LnkArchivoTipoAprovechamiento != null && LnkArchivoTipoAprovechamiento.Visible)
                {
                    objPostTrigger = new PostBackTrigger();
                    objPostTrigger.ControlID = LnkArchivoTipoAprovechamiento.UniqueID;
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(LnkArchivoTipoAprovechamiento);
                }
            }
        }
    }

}