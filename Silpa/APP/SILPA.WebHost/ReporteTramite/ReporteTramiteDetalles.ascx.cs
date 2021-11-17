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
using SoftManagement.Log;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using SILPA.LogicaNegocio.AdmTablasBasicas;
using System.Linq;
using SILPA.LogicaNegocio.REDDS;


public partial class ReporteTramite_ReporteTramiteDetalles : System.Web.UI.UserControl
{
    private string strNumSilpa = "";
    private string strExpCod = "";
    private DataTable dtResult;
    public string strMUrlFormBuilder = "";
    private long idUsuario = 0;
    private static string prevPage;

    public DataSet dsCalendariosVS()
    { 
        DataSet dsRes= new DataSet();
        if (ViewState["dsCalendarios"] == null)
        {
            SILPA.LogicaNegocio.ReporteTramite.ReporteTramite objtramites = new SILPA.LogicaNegocio.ReporteTramite.ReporteTramite();
            dsRes =objtramites.ConsultarFechasCalendatios(strNumSilpa);
            ViewState["dsCalendarios"] = dsRes;
        }
        else
            dsRes = (DataSet)ViewState["dsCalendarios"];

        return dsRes;
    }

    protected void grdInvestigacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Cambiar la pagina con la información
        this.grdInvestigacion.PageIndex = e.NewPageIndex;
        fillGrdInvestigacion("");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        strNumSilpa = Request.QueryString.Get("NSilpa");

        if (Request.QueryString.Get("Exp") != null)
        {
            strExpCod = Request.QueryString.Get("Exp");
        }

        ParametroEntity _parametro = new ParametroEntity();
        ParametroDalc parametro = new ParametroDalc();
        _parametro.IdParametro = -1;
        _parametro.NombreParametro = "URL_FORMBUILDER";
        parametro.obtenerParametros(ref _parametro);

        strMUrlFormBuilder = _parametro.Parametro;
        tbcDetalles.Width = 1100;
        Label7.Text = "";
        if (Page.Request["Ubic"] != null)
        {
            string Id = Request.QueryString.Get("Id");
            if (!String.IsNullOrEmpty(Id))
                idUsuario = Int64.Parse(Request.QueryString.Get("Id"));
            tbcDetalles.Width = 750;
        }
        else
        {
            try
            {
                idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario);
                // idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario("80213682");
            }
            catch (Exception ex)
            {

                string Id = Request.QueryString.Get("Id");
                if (!String.IsNullOrEmpty(Id))
                    idUsuario = Int64.Parse(Request.QueryString.Get("Id"));
                //no existe el id usuario
                //idUsuario = -90
                btnAtras.Visible = false;
                tbcDetalles.Width = 500;
            }
        }
        var objTablasBasicas = new TipoTramite();
        SILPA.AccesoDatos.DAA.SolicitudDAAEIADalc _solicitudDalc = new SILPA.AccesoDatos.DAA.SolicitudDAAEIADalc();
        SILPA.AccesoDatos.DAA.SolicitudDAAEIAIdentity solicitudf = new SILPA.AccesoDatos.DAA.SolicitudDAAEIAIdentity();
        solicitudf.NumeroSilpa = strNumSilpa;
        _solicitudDalc.ObtenerSolicitud(ref solicitudf);
        var tipoTramite = objTablasBasicas.ListarTipoTramite(0, "").AsEnumerable().Where(x => x.Field<int>("ID") == solicitudf.IdTipoTramite).ToList();
        if (tipoTramite.Count() > 0)
        {
            var tramite = tipoTramite.First();
            hdfMostrarDocumentos.Value = tramite.Field<bool>("MOSTRAR_DOCUMENTOS").ToString();
        }

        if (!IsPostBack)
        {
            if (Request.UrlReferrer == null)
                prevPage = Request.Url.ToString();
            else
                prevPage = Request.UrlReferrer.ToString();
            this.lblNumeroSilpa.Text = strNumSilpa;
            
            NumeroSilpaDalc numSilpa = new NumeroSilpaDalc();
            DataTable infExpediente = numSilpa.ConsultarExpediente(strNumSilpa);
           
            if (infExpediente.Rows.Count > 0)
            {
                this.trDescripcionProyecto.Visible = true;
                this.trNombreProyecto.Visible = true;
                this.lblNombreProyectoValue.Text = infExpediente.Rows[0]["NOMBRE_EXPEDIENTE"].ToString();
                this.lblDescripcionProyectoValue.Text = infExpediente.Rows[0]["DESCRIPCION_EXPEDIENTE"].ToString();
                this.lblSector.Text = infExpediente.Rows[0]["SECTOR"].ToString();
                this.lblSolicitante.Text = infExpediente.Rows[0]["NOMBRE_SOLICITANTE"].ToString();
                this.lblCorreoElectronicoSolicitante.Text = infExpediente.Rows[0]["CORREO_SOLICITANTE"].ToString();
                this.lblUbicacion.Text = infExpediente.Rows[0]["UBICACION"].ToString();
                this.lblCodigoExpediente.Text = infExpediente.Rows[0]["CODIGO_EXPEDIENTE"].ToString();
                if (infExpediente.Rows[0]["UBICACION"].ToString() != "")
                    this.btnMapas.Visible = true;

            }
            else
            {
                if (strExpCod != "")
                {
                    DataTable infExpediente2 = numSilpa.ConsultarExpedienteXCodExo(strExpCod);

                    if (infExpediente2.Rows.Count > 0)
                    {
                        this.trDescripcionProyecto.Visible = true;
                        this.trNombreProyecto.Visible = true;
                        this.lblNombreProyectoValue.Text = infExpediente2.Rows[0]["NOMBRE_EXPEDIENTE"].ToString();
                        this.lblDescripcionProyectoValue.Text = infExpediente2.Rows[0]["DESCRIPCION_EXPEDIENTE"].ToString();
                        this.lblSector.Text = infExpediente2.Rows[0]["SECTOR"].ToString();
                        this.lblSolicitante.Text = infExpediente2.Rows[0]["NOMBRE_SOLICITANTE"].ToString();
                        this.lblCorreoElectronicoSolicitante.Text = infExpediente2.Rows[0]["CORREO_SOLICITANTE"].ToString();
                        this.lblUbicacion.Text = infExpediente2.Rows[0]["UBICACION"].ToString();
                        this.lblCodigoExpediente.Text = infExpediente2.Rows[0]["CODIGO_EXPEDIENTE"].ToString();
                        if (infExpediente2.Rows[0]["UBICACION"].ToString() != "")
                            this.btnMapas.Visible = true;
                    }
                    else
                    {
                        this.InfoSila.Visible = false;
                    }
                }
                else
                {
                    this.InfoSila.Visible = false;
                }
            }
            var tramite = tipoTramite.First();
            hdfMostrarDocumentos.Value = tramite.Field<bool>("MOSTRAR_DOCUMENTOS").ToString();
            if (tramite.Field<string>("PRO_CLAVE_PROCESO") == "REDDS")
            {
                acpnInfoRedd.Visible = true;
                ConsultarRegistroRedd(strNumSilpa);
            }
            else
            {
                acpnInfoRedd.Visible = false;
            }
                

            CargarCalendarios();
  
            llenarGrillas();
            CargarImagenes();
           
        }
    }

    private void ConsultarRegistroRedd(string strNumSilpa)
    {
        Redds clsRedds = new Redds();
        var registroRedds = clsRedds.ConsultaRegistroREDDNumeroVital(strNumSilpa, false, null);
        this.lblNombreProyectoRedds.Text = registroRedds.NombreIniciativa;
        this.lblSolicitanteRedds.Text = registroRedds.NombreRazonSocial;
        this.LBLReddID.Text = registroRedds.ReddsID.ToString();
        registroRedds.LstLocalizacion = clsRedds.ConsultaLocalizaciones(registroRedds.ReddsID);
        if(registroRedds.LstLocalizacion.Count > 0)
            btnMapaRedd.Visible = true;
    }

    private void CargarCalendarios()
    {
        ViewState["dsCalendarios"] = null;
        DataSet dsCalendarios = dsCalendariosVS();
        if (dsCalendarios.Tables[1].Rows.Count > 0)
        {
            if (dsCalendarios.Tables[1].Rows[0]["FECHA_LIMITE"].ToString() != "")
            {
                DateTime FechaInicial = DateTime.Parse(dsCalendarios.Tables[1].Rows[0]["FECHA_LIMITE"].ToString());
                DateTime FechaFinal = DateTime.Parse(dsCalendarios.Tables[1].Rows[dsCalendarios.Tables[1].Rows.Count-1]["FECHA_LIMITE"].ToString());
                int i = 0;
                int j= 1;
                int k= 1;
                this.PrimerMes.Visible = false;
                this.SegundoMes.Visible = false;
                this.TercerMes.Visible = false;
                this.CuartoMes.Visible = false;
                this.QuintoMes.Visible = false;
                this.SextoMes.Visible = false;
                if (FechaInicial != DateTime.MinValue)
                {               
                    string[] FECHAS = new string[100];
                    foreach( DataRow row in dsCalendarios.Tables[3].Rows)
                    {
                        if(row["FECHA_LIMITE"].ToString()!="")
                        {
                            bool existe=false;
                            for (k = 0; k < i; k++)
                            {
                                if (FECHAS[k] == DateTime.Parse(row["FECHA_LIMITE"].ToString()).ToString("yyyy/MM"))
                                    existe = true;
                            }
                            if (!existe)
                            {
                                FECHAS[i]=DateTime.Parse(row["FECHA_LIMITE"].ToString()).ToString("yyyy/MM");
                                i++;
                            }
                        }
                        
                    }
                    
                    for (k=0;k<i;k++)
                    {
                        FechaInicial=DateTime.Parse(FECHAS[k]+"/01");                                             
                        switch (j)
                        {
                            case 1:
                                this.PrimerMes.VisibleDate = FechaInicial;
                                this.PrimerMes.Visible = true;
                                j++;
                                break;
                            case 2:
                                this.SegundoMes.VisibleDate = FechaInicial;
                                this.SegundoMes.Visible = true;
                                j++;
                                break;
                            case 3:
                                this.TercerMes.VisibleDate = FechaInicial;
                                this.TercerMes.Visible = true;
                                j++;
                                break;
                            case 4:
                                this.CuartoMes.VisibleDate = FechaInicial;
                                this.CuartoMes.Visible = true;
                                j++;
                                break;
                            case 5:
                                this.QuintoMes.VisibleDate = FechaInicial;
                                this.QuintoMes.Visible = true;
                                j++;
                                break;
                            case 6:
                                this.SextoMes.VisibleDate = FechaInicial;
                                this.SextoMes.Visible = true;
                                j++;
                                break;
                        }                                                  

                    }
                    if (j!=6)
                    {
                        this.btnSiguienteCalendario.Visible = false;
                        this.btnAnteriosCalendario.Visible = false;
                    }                  
                }
      

                this.grvConvenciones1.DataSource = dsCalendarios.Tables[1];
                this.grvConvenciones1.DataBind();
       

                this.grvDetalleActual.DataSource = dsCalendarios.Tables[2];
                this.grvDetalleActual.DataBind();
            }
            else
            {
                this.Calendar.Visible = false;
            }
        }
        else
        {
            this.Calendar.Visible = false;
        }

    }

    private void CargarImagenes()
    {
        ParametroDalc objParametro = new ParametroDalc();
        ParametroEntity objParametroEntity = new ParametroEntity();
        //rricaurte
        //Se quema el 46 mientras se enumera
        objParametroEntity.IdParametro = 46;
        objParametroEntity.NombreParametro = "-1";
        objParametro.obtenerParametros(ref objParametroEntity);
        this.imgSolicitantel.ImageUrl = objParametroEntity.Parametro;


        objParametroEntity.IdParametro = 47;
        objParametroEntity.NombreParametro = "-1";
        objParametro.obtenerParametros(ref objParametroEntity);
        this.imgAutAmb.ImageUrl = objParametroEntity.Parametro;

        objParametroEntity.IdParametro = 48;
        objParametroEntity.NombreParametro = "-1";
        objParametro.obtenerParametros(ref objParametroEntity);
        this.imgPDIl.ImageUrl = objParametroEntity.Parametro;

        objParametroEntity.IdParametro = 49;
        objParametroEntity.NombreParametro = "-1";
        objParametro.obtenerParametros(ref objParametroEntity);
        this.imgEntExt.ImageUrl = objParametroEntity.Parametro;

    }
    private void llenarGrillas()
    {

        if (strNumSilpa == "" || strNumSilpa == null)
            return;

        VisualizarPanels();
        fillGrdFus("");
        fillGrdEvaluacion("");
        fillGrdSeguimiento("");
        fillGrdInvestigacion("");
        fillGrdCobros("");
        fillGrdOtros("");
        fillGrdReposicion("");
        fillGrdModificacion("");

    }

    private void VisualizarPanels()
    {
        this.TabPanel1.Visible = true;
        this.TabPanel2.Visible = true;
        this.TabPanel3.Visible = true;
        this.TabPanel4.Visible = true;
        this.TabPanel5.Visible = true;
        this.tbSeguimiento.Visible = true;
        this.TabPanel7.Visible = true;
    }
    private void writeFile(string file)
    {

        string filename = file.Substring(file.LastIndexOf("\\") + 1, file.Length - file.LastIndexOf("\\") - 1);

        if (!System.IO.File.Exists(file))
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('El archivo " + filename + " no existe.')</script>");
            this.Label7.Text = "El archivo " + filename + " no existe.";
            return;
        }

        System.IO.FileInfo targetFile = new System.IO.FileInfo(file);
        this.Response.Clear();
        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
        this.Response.ContentType = "application/octet-stream";
        this.Response.ContentType = "application/base64";
        this.Response.WriteFile(targetFile.FullName);       

    }

    private DataTable getFilesFromDatatTaBle(DataTable dtSource)
    {

        DataTable dtResult1 = new DataTable();
        string filePath = "";
        DataRow drArchivos = null;
        bool exists = false;
        string[] arrFiles;

        dtResult1.Columns.Add("URL_PARTICIPANTE");
        dtResult1.Columns.Add("FECHA");
        dtResult1.Columns.Add("NOMBRE_DOCUMENTO");
        dtResult1.Columns.Add("DESCRIPCION");
        dtResult1.Columns.Add("FULL_PATH");

        foreach (DataRow drDocumentos in dtSource.Rows)
        {

            filePath = (drDocumentos["PATH_DOCUMENTO"] == null ? "" : drDocumentos["PATH_DOCUMENTO"].ToString());

            if (filePath != "")
            {

                exists = false;

                if (!System.IO.Directory.Exists(filePath))
                {
                    if (System.IO.File.Exists(filePath))
                    {

                        drArchivos = dtResult1.NewRow();

                        drArchivos["FECHA"] = drDocumentos["FECHA_CREACION"];
                        drArchivos["NOMBRE_DOCUMENTO"] = filePath.Substring(filePath.LastIndexOf("\\") + 1, filePath.Length - filePath.LastIndexOf("\\") - 1);
                        ;
                        drArchivos["DESCRIPCION"] = drDocumentos["DESCRIPCION"];
                        drArchivos["URL_PARTICIPANTE"] = drDocumentos["LOGO_PARTICIPANTE"];
                        drArchivos["FULL_PATH"] = filePath;

                        foreach (DataRow drTemporal in dtResult1.Rows)
                        {

                            //no se agregarán archivos que contengan el mismo path
                            if (drTemporal["DESCRIPCION"].ToString() == drArchivos["DESCRIPCION"].ToString() && drTemporal["FULL_PATH"].ToString() == drArchivos["FULL_PATH"].ToString())
                            {
                                exists = true;
                                break;
                            }

                        }

                        if (!exists)
                            dtResult1.Rows.Add(drArchivos);

                    }

                    continue;
                }

                arrFiles = System.IO.Directory.GetFiles(filePath);

                foreach (string strFile in arrFiles)
                {
                    drArchivos = dtResult1.NewRow();

                    drArchivos["FECHA"] = drDocumentos["FECHA_CREACION"];
                    drArchivos["NOMBRE_DOCUMENTO"] = strFile.Substring(strFile.LastIndexOf("\\") + 1, strFile.Length - strFile.LastIndexOf("\\") - 1);
                    
                    drArchivos["DESCRIPCION"] = drDocumentos["DESCRIPCION"];
                    drArchivos["URL_PARTICIPANTE"] = drDocumentos["LOGO_PARTICIPANTE"];
                    drArchivos["FULL_PATH"] = strFile;

                    foreach (DataRow drTemporal in dtResult1.Rows)
                    {

                        //no se agregarán archivos que contengan el mismo path
                        if (drTemporal["DESCRIPCION"].ToString() == drArchivos["DESCRIPCION"].ToString() && drTemporal["FULL_PATH"].ToString() == drArchivos["FULL_PATH"].ToString())
                        {
                            exists = true;
                            break;
                        }

                    }

                    if (!exists)
                        dtResult1.Rows.Add(drArchivos);
                }
            }
        }

        return dtResult1;
    }

    private void fillGrdEvaluacion(string sortExp)
    {
        try
        {
            //   SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".fillGrdEvaluacion.Inicio");
            SILPA.LogicaNegocio.Generico.Listas objGenerico = new SILPA.LogicaNegocio.Generico.Listas();

            //"1" es el id asignado a Evaluacion en la tabla EST_ESTADO_TAB
            dtResult = objGenerico.ListarDocumentosEvaluacion(1, strNumSilpa, int.Parse(idUsuario.ToString()));
            //  dtResult = getFilesFromDatatTaBle(dtResult);
            if (OcultaPanels(dtResult, this.TabPanel2))
            {
                if (sortExp != "")
                    dtResult.DefaultView.Sort = sortExp + " ASC";
                this.headerEval.Visible = true;
                this.headerEval.Text = "Evaluación (" + dtResult.Rows.Count.ToString() + ")";
                grdEvaluacion.DataSource = dtResult;
                grdEvaluacion.DataBind();
            }
            else
                this.headerEval.Visible = false;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + ex.ToString());
            // Mensaje.MostrarMensaje(this.Page, "Error al cargar datos comuniquese con el Administrador");
        }

    }

    private void fillGrdFus(string sortExp)
    {
        try
        {        
            SILPA.LogicaNegocio.ReporteTramite.DocumentacionSolicitud objDocumentacion = new SILPA.LogicaNegocio.ReporteTramite.DocumentacionSolicitud();
            DataSet ds = objDocumentacion.ListarDocumentacionSolicitudFUSxPerfil(strNumSilpa, int.Parse(idUsuario.ToString()));
            //JACOSTA 20160225. SETEAMOS LA RUTA ORIGEN EN LA URL DE LOS DOCUMENTOS QUE SE ENCUENTRA EN SILA ANLA
            foreach (DataRow idtrInfo in ds.Tables[0].Rows)
            {
                if (idtrInfo["ruta"].ToString().Contains("origen=seg"))
                {
                    idtrInfo["ruta"] = idtrInfo["ruta"].ToString().Replace("origen=seg","origen=vital");
                }
            }
            if (ds.Tables.Count == 0)
                return;
            dtResult = ds.Tables[0];
            if (OcultaPanels(dtResult, this.TabPanel1))
            {
                this.headerSol.Visible = true;
                if (Request.QueryString["Ubic"]==null)
                    this.headerSol.Text = "Solicitud (" + dtResult.Rows.Count.ToString() + ")";
                else
                    this.headerSol.Text = "Solicitud (" + (dtResult.Rows.Count -1).ToString() + ")";
                if (sortExp != "")
                    dtResult.DefaultView.Sort = sortExp + " ASC";
                grdFUS.DataSource = dtResult;
                grdFUS.DataBind();
                if (!ValidacionToken())
                {
                    grdFUS.Columns[4].Visible = Convert.ToBoolean(this.hdfMostrarDocumentos.Value);
                }
               
            }
            else
                this.headerSol.Visible = false;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ex.ToString());
            //   Mensaje.MostrarMensaje(this.Page, "Error al cargar datos comuniquese con el Administrador");
        }

    }

    private void fillGrdCobros(string sortExp)
    {
        try
        {
            SILPA.LogicaNegocio.Generico.Cobro cobro = new SILPA.LogicaNegocio.Generico.Cobro();
            dtResult = cobro.ListarCobros(strNumSilpa, int.Parse(idUsuario.ToString()));
            if (OcultaPanels(dtResult, this.TabPanel5))
            {
                DataTable dtResult1 = new DataTable();
                dtResult1.Columns.Add("COB_NUMERO_SILPA");
                dtResult1.Columns.Add("COB_FECHA_EXPEDICION");
                dtResult1.Columns.Add("COB_RUTA_ARCHIVOS");
                dtResult1.Columns.Add("COB_NUMERO_EXPEDIENTE");
                dtResult1.Columns.Add("COB_NOMBRE_DOCUMENTO");
                dtResult1.Columns.Add("IDParticipant");
                string filePath = "";

                foreach (DataRow drDocumentos in dtResult.Rows)
                {

                    DataRow drArchivos = null;
                    drArchivos = dtResult1.NewRow();

                    drArchivos["COB_NUMERO_SILPA"] = drDocumentos["COB_NUMERO_SILPA"];
                    drArchivos["COB_FECHA_EXPEDICION"] = String.Format("{0:yyyy/MM/dd} ", DateTime.Parse(drDocumentos["COB_FECHA_EXPEDICION"].ToString()));
                    drArchivos["COB_NUMERO_EXPEDIENTE"] = drDocumentos["COB_NUMERO_EXPEDIENTE"];
                    drArchivos["COB_NOMBRE_DOCUMENTO"] = drDocumentos["COB_RUTA_ARCHIVOS"];
                    drArchivos["COB_RUTA_ARCHIVOS"] = drDocumentos["COB_RUTA_ARCHIVOS"];
                    drArchivos["IDParticipant"] = drDocumentos["IDParticipant"];
                    dtResult1.Rows.Add(drArchivos);
                }
                this.headerCobr.Visible = true;
                if (sortExp != "")
                    dtResult1.DefaultView.Sort = sortExp + " ASC";
                this.headerCobr.Text = "Pagos y Cobros (" + dtResult1.Rows.Count.ToString() + ")";
                grdCobros.DataSource = dtResult1;
                grdCobros.DataBind();
            }
            else
                this.headerCobr.Visible = false;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + ex.ToString());
            //   Mensaje.MostrarMensaje(this.Page, "Error al cargar datos comuniquese con el Administrador");
        }

    }

    private void fillGrdOtros(string sortExp)
    {
        try
        {
            Listas objGenerico = new Listas();            
            dtResult = objGenerico.ListarDocumentosEvaluacion(5, strNumSilpa, int.Parse(idUsuario.ToString()));
            if (OcultaPanels(dtResult, this.TabPanel3))
            {
                if (sortExp != "")
                    dtResult.DefaultView.Sort = sortExp + " ASC";
                this.headerOtro.Visible = true;
                this.headerOtro.Text = "Otros (" + dtResult.Rows.Count.ToString() + ")";
                grdOtros.DataSource = dtResult;
                grdOtros.DataBind();                   
            }
            else
                this.headerOtro.Visible = false;
            
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + ex.ToString());          
        }
    }
    private void fillGrdModificacion(string sortExp)
    {
        try
        {
            Listas objGenerico = new Listas();
            dtResult = objGenerico.ListarDocumentosEvaluacion(8, strNumSilpa, int.Parse(idUsuario.ToString()));
            if (OcultaPanels(dtResult, this.TabPanel6))
            {
                if (sortExp != "")
                    dtResult.DefaultView.Sort = sortExp + " ASC";
                this.headerModificacion.Visible = true;
                this.headerModificacion.Text = "Modificación (" + dtResult.Rows.Count.ToString() + ")";
                grdModificacion.DataSource = dtResult;
                grdModificacion.DataBind();
            }
            else
                this.headerModificacion.Visible = false;

        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + ex.ToString());
        }
    }

    private void fillGrdReposicion(string sortExp)
    {
        try
        {
            Listas objGenerico = new Listas();
            dtResult = objGenerico.ListarDocumentosEvaluacion(7, strNumSilpa, int.Parse(idUsuario.ToString()));
            if (OcultaPanels(dtResult, this.TabPanel7))
            {
                if (sortExp != "")
                    dtResult.DefaultView.Sort = sortExp + " ASC";
                this.HeaderReposicion.Visible = true;
                this.HeaderReposicion.Text = "Reposición (" + dtResult.Rows.Count.ToString() + ")";
                grdRepos.DataSource = dtResult;
                grdRepos.DataBind();

                DataTable dt = new DataTable();
                dt.Columns.Add("NUMERO_VITAL");
                dt.Columns.Add("PER_ID");
                foreach (DataRow row in dtResult.Rows)
                {
                    if (dt.Select("NUMERO_VITAL=" + row["TAR_VALOR"].ToString()).Length == 0)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["NUMERO_VITAL"] = row["TAR_VALOR"].ToString();
                        newRow["PER_ID"] = idUsuario.ToString();
                        dt.Rows.Add(newRow);
                    }
                }

            }
            else
                this.HeaderReposicion.Visible = false;

        }
        catch (Exception ex)
        {

            SMLog.Escribir(Severidad.Critico, this.Page.Title + ex.ToString());
            //   Mensaje.MostrarMensaje(this.Page, "Error al cargar datos comuniquese con el Administrador");
        }
    }
    
    private void fillGrdSeguimiento(string sortExp)
    {
        try
        {
            Listas objGenerico = new Listas();
            //2 es el id asignado a seguimiento en la tabla 
            dtResult = objGenerico.ListarDocumentosEvaluacion(2, strNumSilpa, int.Parse(idUsuario.ToString()));
            //   dtResult = getFilesFromDatatTaBle(dtResult);
            if (OcultaPanels(dtResult, this.tbSeguimiento))
            {
                if (sortExp != "")
                    dtResult.DefaultView.Sort = sortExp + " ASC";
                this.headerSegui.Visible = true;
                this.headerSegui.Text = "Seguimiento (" + dtResult .Rows.Count.ToString()+ ")";
                grdSeguimiento.DataSource = dtResult;
                grdSeguimiento.DataBind();
            }
            else
                this.headerSegui.Visible = false;
        }
        catch (Exception ex)
        {

            SMLog.Escribir(Severidad.Critico, this.Page.Title + ex.ToString());
            //   Mensaje.MostrarMensaje(this.Page, "Error al cargar datos comuniquese con el Administrador");
        }
    }

    private void fillGrdInvestigacion(string sortExp)
    {
        try
        {
            Listas objGenerico = new Listas();

            

            DataTable dtRes = objGenerico.ListarDocumentosEvaluacion(6, strNumSilpa, int.Parse(idUsuario.ToString()));
            this.cboExpedientesAsociados.DataSource = dtRes;
            this.cboExpedientesAsociados.DataTextField = "EXP_CODIGO";
            this.cboExpedientesAsociados.DataValueField = "EXP_CODIGO";
            this.cboExpedientesAsociados.DataBind();
            this.cboExpedientesAsociados.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            if (dtRes.Rows.Count > 0)
            {
                this.cboExpedientesAsociados.Visible = true;
                this.lblTexxto.Visible = true;
            }
            else
            {
                this.cboExpedientesAsociados.Visible = false;
                this.lblTexxto.Visible = false;
            }
            //3 es el id asignado a investigacion en la tabla EST_ESTADO_TAB
            dtResult = objGenerico.ListarDocumentosEvaluacion(4, strNumSilpa, int.Parse(idUsuario.ToString()));
            //dtResult = getFilesFromDatatTaBle(dtResult);
            if (OcultaPanels(dtResult, this.TabPanel4))
            {
                if (sortExp != "")
                    dtResult.DefaultView.Sort = sortExp + " ASC";


                this.headerInvs.Visible = true;
                this.headerInvs.Text = "Investigación (" + dtResult.Rows.Count.ToString() + ")";
                grdInvestigacion.DataSource = dtResult;
                grdInvestigacion.DataBind();                
            }
            else
            {
                this.headerInvs.Visible = false;
              
            }
            this.grdInvestigacion.PageSize = 10;
                
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + ex.ToString());
            //    Mensaje.MostrarMensaje(this.Page, "Error al cargar datos comuniquese con el Administrador");
        }
    }

    private bool OcultaPanels(DataTable dt, AjaxControlToolkit.TabPanel tbPan)
    {
        bool result = false;        
        if (dtResult.Rows.Count > 0)        
            result = true;        
        else        
            tbPan.Visible = false;                 
        return result;
    }

    protected void grdCobros_onSorting(object sender, GridViewSortEventArgs e)
    {

        fillGrdCobros(e.SortExpression);

    }

    protected void grdOtros_onSorting(object sender, GridViewSortEventArgs e)
    {

        fillGrdOtros(e.SortExpression);

    }
    protected void grdModificacion_onSorting(object sender, GridViewSortEventArgs e)
    {
        fillGrdModificacion(e.SortExpression);
    }
    protected void grdEvaluacion_onSorting(object sender, GridViewSortEventArgs e)
    {

        fillGrdEvaluacion(e.SortExpression);

    }

    protected void grdFUS_onSorting(object sender, GridViewSortEventArgs e)
    {

        fillGrdFus(e.SortExpression);

    }

    protected void grdInvestigacion_onSorting(object sender, GridViewSortEventArgs e)
    {

        fillGrdInvestigacion(e.SortExpression);

    }

    protected void grdSeguimiento_onSorting(object sender, GridViewSortEventArgs e)
    {

        fillGrdSeguimiento(e.SortExpression);

    }

    protected void grdRepos_onSorting(object sender, GridViewSortEventArgs e)
    {

        fillGrdReposicion(e.SortExpression);

    }

    protected void grdSeguimiento_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "viewFile")
        {
            Session["DatoRadicacion"] = null;
            GridView source = (GridView)sender;
            Label lblFileName = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblArchivo");
            Label lblFileName2 = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbArchivo2");
        
            Label lblRutaDocumento = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblRutaDocumento");
            if (lblRutaDocumento.Text == "")
            {
                if (lblFileName.Text != "")
                {
                    string strScript = "window.open('" + lblFileName.Text.Replace("seg", "vital") + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    //                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                }
                else {

                    if (lblFileName2.Text != "")
                    {
                        string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                        string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";
                        Session["Ruta"] = lblFileName2.Text;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    }
           


                }
            }
            else
                writeFile(lblFileName.Text);
        }

    }

    protected void grdAsocRep_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Editar")
        {
            GridView source = (GridView)sender;
            Label lblIdPer = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblIdUser");
            Label lblNumeroVITAL = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblNumeroVITAL");

            if (Page.Request["Ubic"] == null)
                Response.Redirect("reportetramiteDos.aspx?NSilpa=" + lblNumeroVITAL.Text + "&Id=" + lblIdPer.Text + "");
            else
                Response.Redirect("ReporteTramiteDetallesCiudadano.aspx?NSilpa=" + lblNumeroVITAL.Text + "&Id=" + lblIdPer.Text + "");
        }

    }

    protected void grdCobros_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "viewFile")
        {
            Session["DatoRadicacion"] = null;
            GridView source = (GridView)sender;            
            Label lblFileName = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblArchivo");
            
            string strScript = "";
            if (Page.Request["Ubic"] == null)
                strScript="window.open('" + lblFileName.Text.Substring(3, lblFileName.Text.Length - 3) + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
            else
                strScript = "window.open('../" + lblFileName.Text.Substring(3, lblFileName.Text.Length - 3) + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
            //Response.Redirect(lblFileName.Text.Substring(3, lblFileName.Text.Length - 3));
            // writeFile( lblFileName.Text );
        }

    }

    protected void grdOtros_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "viewFile")
        {
            Session["DatoRadicacion"] = null;
            GridView source = (GridView)sender;
            Label lblFileName = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblArchivo");
            Label lblFileName2 = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbArchivo2");
        
            Label lblRutaDocumento = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblRutaDocumento");
        
            if (lblRutaDocumento.Text == "")
            {
                if (lblFileName.Text != "")
                {
                    string strScript = "window.open('" + lblFileName.Text.Replace("seg", "vital") + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                }
                else
                {
                    if (lblFileName2.Text != "")
                    {
                        string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                        string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";
                        Session["Ruta"] = lblFileName2.Text;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    }
                }
            }
            else
                writeFile(lblFileName.Text);

        }

    }

    protected void grdRepos_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "viewFile")
        {
            Session["DatoRadicacion"] = null;
            GridView source = (GridView)sender;
            Label lblFileName = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblArchivo");
            Label lblRutaDocumento = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblRutaDocumento");
            Label lblFileName2 = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbArchivo2");
        
            if (lblRutaDocumento.Text == "")
            {
                if (lblFileName.Text != "")
                {
                    string strScript = "window.open('" + lblFileName.Text.Replace("seg", "vital") + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                }
                else
                {
                    if (lblFileName2.Text != "")
                    {
                        string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                        string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";
                        Session["Ruta"] = lblFileName2.Text;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
           
                    }
                }
            }
            else
                writeFile(lblFileName.Text);
        }

    }

    protected void grdEvaluacion_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "viewFile")
        {
            Session["DatoRadicacion"] = null;
            GridView source = (GridView)sender;
            Label lblFileName = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbArchivo");
            Label lblFileName2 = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbArchivo2");
        
            Label lblRutaDocumento = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblRutaDocumento");
          
            if (lblRutaDocumento.Text == "")
            {
                if (lblFileName.Text != "")
                {
                    string strScript = "window.open('" + lblFileName.Text.Replace("seg", "vital") + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                }
                else{
                    if (lblFileName2.Text != "")
                    {
                        string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                        string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";
                        Session["Ruta"] = lblFileName2.Text;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    }
           

                }
            }
            else
                writeFile(lblFileName.Text);
        }

    }

    protected void grdInvestigacion_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "viewFile")
        {
            Session["DatoRadicacion"] = null;
            GridView source = (GridView)sender;
            Label lblFileName = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblArchivo");
            Label lblFileName2 = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lbArchivo2");
        
            Label lblRutaDocumento = (Label)source.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("Label4");
            if (lblRutaDocumento.Text == "")
            {
                if (lblFileName.Text != "")
                {
                    string strScript = "window.open('" + lblFileName.Text.Replace("seg", "vital") + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                }
                else
                {
                    if (lblFileName2.Text != "")
                    {
                        string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                        string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";
                        Session["Ruta"] = lblFileName2.Text;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    }
                }
            }
            else            
                writeFile(lblFileName.Text);
        
        }

    }

    protected void btnAtras_Click(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);         
    }

    protected void lbDocumentos_Click(object sender, CommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        Label lbEntryData = new Label();
        Label lbIdEntryData = new Label();
        Label lbEntryDataType = new Label();
        Label lbActivity = new Label();
        Label lbProcessInstance = new Label();
        Label lbRuta = new Label();
        Label lbUrl = new Label();
        Label lbIdrelated = new Label();


        SILPA.LogicaNegocio.DAA.SolicitudDAAEIA objTramite = new SILPA.LogicaNegocio.DAA.SolicitudDAAEIA();
        AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
        DataSet autId = obj.ListarAutoridadAmbientalXNumeroVital(this.lblNumeroSilpa.Text);

        Correspondencia CorrespRadic = new Correspondencia();
        DataTable DatoRadicacion = new DataTable();

        DataTable dtCorrespondencia = CorrespRadic.CorresPondenciaPorExpediente(strNumSilpa, int.Parse(autId.Tables[0].DefaultView[0][0].ToString())).Tables[0];
        
        //validamos si es un registro redds
        if (this.grdFUS.Rows[index].Cells[2].Text.Contains("Registro REDDS"))
        {
            DataRow dtrwPathRedds = dtCorrespondencia.NewRow();
            dtrwPathRedds["PATH_DOCUMENTO"] = ConfigurationManager.AppSettings["FILE_TRAFFIC"] + string.Format(@"Redds\{0}\", (this.grdFUS.Rows[index].Cells[2].Text.Split('-'))[1].ToString().Trim());
            dtCorrespondencia.Rows.Add(dtrwPathRedds);
        }

//        Session["DatoRadicacion"] = dtCorrespondencia;
        Session["DatoRadicacion"] = null;
      
        lbEntryData = (Label)this.grdFUS.Rows[index].FindControl("lbEntryData");
        lbIdEntryData = (Label)this.grdFUS.Rows[index].FindControl("lbIdEntryData");
        lbEntryDataType = (Label)this.grdFUS.Rows[index].FindControl("lbEntryDataType");
        lbActivity = (Label)this.grdFUS.Rows[index].FindControl("lbIdActivityInstance");
        lbProcessInstance = (Label)this.grdFUS.Rows[index].FindControl("lbProcessInstance");
        lbRuta = (Label)this.grdFUS.Rows[index].FindControl("lbRuta");
        lbUrl = (Label)this.grdFUS.Rows[index].FindControl("lbUrl");
        lbIdrelated = (Label)this.grdFUS.Rows[index].FindControl("lbIdRelated");
        Session["process"] = lbProcessInstance.Text;
        if (lbEntryDataType.Text == "documentos")        
        {
            string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
            string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";                   
            Session["Ruta"] = lbRuta.Text;                        
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
        }
        else if (lbEntryDataType.Text != "")        
        {
            strMUrlFormBuilder = strMUrlFormBuilder.Replace("{ENTRY_DATA}", lbEntryData.Text);
            strMUrlFormBuilder = strMUrlFormBuilder.Replace("{IDENTRY_DATA}", lbIdEntryData.Text);
            strMUrlFormBuilder = strMUrlFormBuilder.Replace("{ENTRY_DATA_TYPE}", lbEntryDataType.Text);
            strMUrlFormBuilder = strMUrlFormBuilder.Replace("{IDACTIVITY_INSTANCE}", lbActivity.Text);
            strMUrlFormBuilder = strMUrlFormBuilder.Replace("{PROCESS_INSTANCE}", lbProcessInstance.Text);
            strMUrlFormBuilder = strMUrlFormBuilder.Replace("{ID_RELATED}", lbIdrelated.Text);
            strMUrlFormBuilder = strMUrlFormBuilder.Replace("{IDUSER}", lbIdrelated.Text);
            if (Page.Request["Ubic"] == null)
                Response.Redirect(strMUrlFormBuilder);
            else
            {
                if (string.IsNullOrEmpty(lbRuta.Text))
                {
                    string ruta = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + '_' + lbProcessInstance.Text + ".rtf";
                    if (System.IO.File.Exists(ruta))
                    {
                        string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                        string strScript = "window.open('" + urlPage + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                        Session["Ruta"] = ruta;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                    }
                    else
                    {
                        Mensaje.MostrarMensaje(this.Page, "Esta Solicitud no tiene Documentos");
                        Label7.Text = "Esta Solicitud no tiene Documentos.";
                    }
                }
                else
                {
                    string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                    string strScript = "window.open('"+urlPage+"','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";                   
                    Session["Ruta"] = lbRuta.Text;
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                    
                }
            }
        }
        else
        {
             string filePath = lbUrl.Text;
             if (filePath != "")
             {
                 if (filePath.Contains(":\\"))
                 {
                     if (System.IO.Directory.Exists(filePath))
                     {
                         string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("/ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                         string strScript = "window.open('" + urlPage + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                         Session["Ruta"] = filePath;
                         this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                         //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                     }
                     else
                     {
                         this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('La Ruta de los archivos no existe - 1.')</script>");
                         Label7.Text = "La Ruta de los archivos no existe.";
                     }
                 }
                 else
                 {
                     int rutas = 0;
                     bool encontroArhcivo = false;
                     string[] ListaArchivos = null;
                     string rutaArchivo = string.Empty;
                     string nombreArchivo = string.Empty;
                     foreach (DataRow tdrCorrespondencia in dtCorrespondencia.Rows)
                     {
                        string path = tdrCorrespondencia["PATH_DOCUMENTO"].ToString();
                        if (System.IO.Directory.Exists(path))
                        {
                            ListaArchivos = System.IO.Directory.GetFiles(path);
                            foreach (var item in ListaArchivos)
                            {
                                nombreArchivo = filePath.Remove(filePath.LastIndexOf("."), (filePath.Length - filePath.LastIndexOf(".")));
                                if (item.Contains(nombreArchivo))
                                {
                                    rutaArchivo = item;
                                    if (System.IO.File.Exists(rutaArchivo))
                                    {
                                        string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("/ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                                        string strScript = "window.open('" + urlPage + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                                        Session["Ruta"] = rutaArchivo;
                                        Session["DatoRadicacion"] = null;
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
                                        encontroArhcivo = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!encontroArhcivo)
                        {
                            rutas++;
                        }
                        else
                        {
                            break;
                        }
                        if (rutas == dtCorrespondencia.Rows.Count)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('La Ruta de los archivos no existe - 2.')</script>");
                            Label7.Text += "La Ruta de los archivos no existe. - 2";
                        }
                     }
                 }
             }
             else
             {
                 if (lbRuta.Text != "")
                 {
                     if (lbRuta.Text.Contains("http"))
                     {
                         string strScript = "window.open('" + lbRuta.Text + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                         this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                     }
                     else
                     {
                         string ruta = string.Empty;
                         string nombrearchivo = string.Empty;
                         string rutacompleta = lbRuta.Text;
                         nombrearchivo = rutacompleta.Split('\\').Last();
                         if (nombrearchivo.Trim() != string.Empty)
                         {
                             ruta = rutacompleta.Replace(nombrearchivo, "");
                             Utilidades util = new Utilidades();
                             util.DescargarArchivo(ruta, nombrearchivo, this.Response);
                         }
                         else
                         {
                             string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                             string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";
                             Session["Ruta"] = lbRuta.Text;
                             this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                         }
                     }
                 }
                 else
                 {
                     this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('La Ruta de los archivos no existe - 3.')</script>");
                     Label7.Text = "La Ruta de los archivos no existe.";
                 }
             }
        }


    }

    protected void cboExpedientesAsociados_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboExpedientesAsociados.SelectedIndex != -1)
        {

            
            DateTime fechaInicial = Convert.ToDateTime(SqlDateTime.MinValue.ToString());            
            DateTime fechaFinal = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());

            SILPA.LogicaNegocio.DAA.SolicitudDAAEIA objTramite = new SILPA.LogicaNegocio.DAA.SolicitudDAAEIA();
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            DataSet autId=obj.ListarAutoridadAmbientalXNumeroVital(this.lblNumeroSilpa.Text);
            DataTable res = objTramite.ListarTramitesRPT(false, fechaInicial, fechaFinal, -1, "", int.Parse(autId.Tables[0].Rows[0]["AUT_ID"].ToString()), "", -1, -1, this.cboExpedientesAsociados.SelectedItem.ToString(), "", "", 0, "0", "0", "-1");
            if (res.Rows.Count == 1)
            {
                if (Page.Request["Ubic"] == null)               
                    Response.Redirect("ReporteTramiteDos.aspx?NSilpa=" + res.Rows[0]["SOL_NUMERO_SILPA"].ToString() + "&Id=" + res.Rows[0]["SOL_ID_SOLICITANTE"].ToString());
                else
                    Response.Redirect("ReporteTramiteDetallesCiudadano.aspx?NSilpa=" + res.Rows[0]["SOL_NUMERO_SILPA"].ToString() + "&Id=" + res.Rows[0]["SOL_ID_SOLICITANTE"].ToString() + "&Ubic=Ext");
            }
            else
            {
                this.grdSolAsoc.DataSource = res;
                this.grdSolAsoc.DataBind();
            }
        }
    }

    protected void grdSolAsoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DETALLE")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label lbNumeroSilpa = (Label)this.grdSolAsoc.Rows[index].FindControl("lbNumeroSilpa");
            Label lbIdSol = (Label)this.grdSolAsoc.Rows[index].FindControl("lblIdSol");
            if (Page.Request["Ubic"] == null)        
                Response.Redirect("ReporteTramiteDos.aspx?NSilpa=" + lbNumeroSilpa.Text + "&Id=" + lbIdSol.Text);
            else
                Response.Redirect("ReporteTramiteDetallesCiudadano.aspx?NSilpa=" + lbNumeroSilpa.Text + "&Id=" + lbIdSol.Text + "&Ubic=Ext");
        }
    }

    protected void btnMisTra_Click(object sender, EventArgs e)
    {   
        if (Page.Request["Ubic"]==null)
            Response.Redirect("reportetramite.aspx");
        else
            Response.Redirect("ReporteTramiteCiudadano.aspx?Ubic=ext");       
            
    }
    protected void grdFUS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lblDetalle = (LinkButton)e.Row.FindControl("lbDocumentos");

            Label lbEntryDataType = (Label)e.Row.FindControl("lbEntryDataType");
            if (lbEntryDataType.Text == "VBFormBuilder")
                if (Page.Request["Ubic"] == null)
                    lblDetalle.Text = "Ver Formulario";
                else
                    e.Row.Visible = false;            
        }
            
    }

    protected void PrimerMes_DayRender(object sender, DayRenderEventArgs e)
    {
        EventoMarcacion(e);
    }
    protected void SegundoMes_DayRender(object sender, DayRenderEventArgs e)
    {
        EventoMarcacion(e);
    }

    protected void TercerMes_DayRender(object sender, DayRenderEventArgs e)
    {
        EventoMarcacion(e);
    }
    protected void CuartoMes_DayRender(object sender, DayRenderEventArgs e)
    {
        EventoMarcacion(e);
    }

    private void EventoMarcacion(DayRenderEventArgs e)
    {
        DataSet dsCalendarios = dsCalendariosVS();
        

        if (e.Day.IsWeekend)
        {
            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#F9FCFB");
        }
        DateTime FechaInicial = DateTime.Parse(dsCalendarios.Tables[1].Rows[0]["FECHA_LIMITE"].ToString());

        if (e.Day.Date >= FechaInicial.Date &&
          e.Day.Date <= DateTime.Now.Date)
        {
            if (!e.Day.IsWeekend)
            {
                if (!e.Day.IsOtherMonth)
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F0F0");
                }
            }
        }
        string Filtro = "FES_FECHA='" + String.Format("{0:yyyy/MM/dd} ", e.Day.Date) + "'";
        if (dsCalendarios.Tables[0].Select(Filtro).Length > 0)
        {
            if (!e.Day.IsOtherMonth)
            {
                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#F9FCFB");
            }
        }
        string Filtro2 = "FECHA_LIMITE_DATE='" + String.Format("{0:yyyyMMdd} ", e.Day.Date) + "'";
        if (dsCalendarios.Tables[3].Select(Filtro2).Length > 0)
        {
            if (!e.Day.IsOtherMonth)
            {
                DataRow[] rows = dsCalendarios.Tables[3].Select(Filtro2);     
                if(rows[0]["Color_Fondo"].ToString()!="")
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml('#' + rows[0]["Color_Fondo"].ToString());
                e.Cell.ToolTip = rows[0]["Nombre"].ToString();
            
            }
        }
        else
        {
            string Filtro3 = "ENDTIME='" + String.Format("{0:yyyyMMdd} ", e.Day.Date) + "'";
            if (dsCalendarios.Tables[3].Select(Filtro3).Length > 0)
            {
                DataRow[] rows = dsCalendarios.Tables[3].Select(Filtro3);                
                e.Cell.ForeColor = System.Drawing.Color.Blue;
                e.Cell.Font.Bold = true;
                e.Cell.ToolTip = rows[0]["Activity"].ToString();
            
            }
        }
        if (e.Day.Date == DateTime.Now.Date)
        {

            e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00B050");
            e.Cell.Font.Bold = true;
        }
        //if (e.Day.Date == DateTime.ParseExact("20111011", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture))
        //{
        //    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#D7E4BC");
        //    e.Cell.ToolTip = "Auto que declara reunida la información";
        //}



    }
    protected void QuintoMes_DayRender(object sender, DayRenderEventArgs e)
    {
        EventoMarcacion(e);
    }
    protected void SextoMes_DayRender(object sender, DayRenderEventArgs e)
    {
        EventoMarcacion(e);
    }

    protected void btnSiguienteCalendario_Click(object sender, ImageClickEventArgs e)
    {
        this.PrimerMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(1);
        this.SegundoMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(1);
        this.TercerMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(2);
        this.CuartoMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(3);
        this.QuintoMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(4);
        this.SextoMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(5);
    }
    protected void btnAnteriosCalendario_Click(object sender, ImageClickEventArgs e)
    {
        this.PrimerMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(-1);
        this.SegundoMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(1);
        this.TercerMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(2);
        this.CuartoMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(3);
        this.QuintoMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(4);
        this.SextoMes.VisibleDate = this.PrimerMes.VisibleDate.AddMonths(5);
    }

    protected void grvConvenciones1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDescripcionEstado = (Label)e.Row.FindControl("lblDescripcionEstado");
            Label lblColorFondo = (Label)e.Row.FindControl("lblColorFondo");
            Label lblColor = (Label)e.Row.FindControl("lblColor");
            Image imgEstado = (Image)e.Row.FindControl("imgEstado");

            string Estado = lblDescripcionEstado.Text.Substring(0, 1);
            lblDescripcionEstado.Text = lblDescripcionEstado.Text.Substring(2, lblDescripcionEstado.Text.Length-3);
            if(lblColorFondo.Text!="")
                lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + lblColorFondo.Text);

            if (Estado == "1")
                imgEstado.ImageUrl = "~/App_themes/Img/pending.png";
            else if (Estado == "2")
                imgEstado.ImageUrl = "~/App_themes/Img/warningmis.png";
            else if (Estado == "3")
                imgEstado.ImageUrl = "~/App_themes/Img/alerta.png";
            else if (Estado == "4")
                imgEstado.ImageUrl = "~/App_themes/Img/ok.png";
            
        }
    }
    protected void grvConvenciones2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblColorFondo = (Label)e.Row.FindControl("lblColorFondo");
            Label lblColor = (Label)e.Row.FindControl("lblColor");
            lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + lblColorFondo.Text);
        }
    }
    protected void grvConvenciones3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblColorFondo = (Label)e.Row.FindControl("lblColorFondo");
            Label lblColor = (Label)e.Row.FindControl("lblColor");
            lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#"+lblColorFondo.Text);
        }
    }

    protected void grvDetalles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumento = (Label)e.Row.FindControl("lblDocumento");
            Label lblNotificado = (Label)e.Row.FindControl("lblNotificado");
            ImageButton btnDocs = (ImageButton)e.Row.FindControl("btnDocs");
            if (lblDocumento.Text=="")
                btnDocs.Visible = false;
            else if (lblDocumento.Text != "" && lblNotificado.Text.ToUpper() == "S")
            {
                btnDocs.Visible = true;
            }
            else
            {
                btnDocs.Visible = false;
            }
        }
    }

    protected void grvDetalles_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Documentos")
        {
            Session["DatoRadicacion"] = null;
            string RutaArchivos = e.CommandArgument.ToString();
            if (RutaArchivos.Contains("adm_expedientes"))
            {
                string strScript = "window.open('" + RutaArchivos.Replace("seg", "vital") + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=550,height=450')";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                //                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowInfo", strScript, true);
            }
            else
            {
                string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "DescargarDocumentos.aspx");
                string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";
                Session["Ruta"] = RutaArchivos;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
            }
        }

    }

    protected void btnMapas_Click(object sender, ImageClickEventArgs e)
    {
        string urlPage = this.Request.Url.AbsoluteUri.ToString().Replace(this.Request.Url.Query.ToString(), "").Replace("ReporteTramiteDos.aspx", "DescargarDocumentos.aspx").Replace("ReporteTramiteDetallesCiudadano.aspx", "Ubicacion.aspx?nVit="+this.lblNumeroSilpa.Text+"&cExp="+this.lblCodigoExpediente.Text);
        string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";        
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
    }
    protected void btnMapaRedd_Click(object sender, ImageClickEventArgs e)
    {
        string urlPage = "http://"+ this.Request.Url.Authority + this.Request.ApplicationPath + "/REDDS/Ubicacion.aspx?ReddID=" + this.LBLReddID.Text;
        string strScript = "window.open('" + urlPage + "','pruebas','location=center,resizable=yes,scrollbars=yes,width=1120px,height=600px')";
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
    }
    private bool ValidacionToken()
    {
        //DESCOMENTAR ANTES DEL COMMIT!!!
        //Session["IDForToken"] = Request.QueryString["IdRelated"];
        //Session["IDForToken"] = "7";

        if (DatosSesion.Usuario == string.Empty)
        {
            return false;
        }
        string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

        Session["IDForToken"] = (object)idUsuario;

        Session["Usuario"] = Session["IDForToken"];

        SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

        Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));

        using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
        {
            servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
            string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
            //string mensaje = "Token valido";

            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }
}
