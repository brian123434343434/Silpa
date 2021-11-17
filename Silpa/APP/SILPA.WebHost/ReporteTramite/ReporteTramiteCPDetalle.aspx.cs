using SILPA.AccesoDatos.Expedientes;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Publicacion;
using SILPA.Comun;
using SILPA.LogicaNegocio.ConsultaPublica;
using SILPA.LogicaNegocio.Expedientes;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Publicacion;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ReporteTramite_ReporteTramiteCPDetalle : System.Web.UI.Page
{
    #region Propiedades consultaDetalle
    public string strNumSilpa { get { return (string)ViewState["Detalle_numSilpa"]; } set { ViewState["Detalle_numSilpa"] = value; } }
    public string origen { get { return (string)ViewState["Detalle_origen"]; } set { ViewState["Detalle_origen"] = value; } }
    public string tarSolid { get { return (string)ViewState["Detalle_tarSolid"]; } set { ViewState["Detalle_tarSolid"] = value; } }
    public string solicitante { get { return (string)ViewState["Detalle_solicitante"]; } set { ViewState["Detalle_solicitante"] = value; } }
    public string tipoConsulta { get { return (string)ViewState["Detalle_tipoConsulta"]; } set { ViewState["Detalle_tipoConsulta"] = value; } }
    //public Int64 expedienteID { get { return (Int64)ViewState["Detalle_expedienteID"]; } set { ViewState["Detalle_expedienteID"] = value; } }
    //public Int64 idUsuario { get { return (Int64)ViewState["Detalle_idUsuario"]; } set { ViewState["Detalle_idUsuario"] = value; } }
    //public DataTable dtResult { get { return (DataTable)ViewState["Detalle_dtResult"]; } set { ViewState["Detalle_dtResult"] = value; } }

    #region constantes aplicacion
    private static string numSilpa = "";
    public static string strMUrlFormBuilder = "";
    private static long idUsuario = 0;
    private static long exp_id = 0;
    private static DataTable dtResult;
    #endregion

    #endregion

    #region Eventos Pagina
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Se validan los parametros que viene en la URL
            if (Request.QueryString["NumSilpa"] != null && Request.QueryString["NumSilpa"].ToString() != string.Empty)
            {
                strNumSilpa = Request.QueryString["NumSilpa"].ToString();
            }
            if (Request.QueryString["Origen"] != null && Request.QueryString["Origen"].ToString() != string.Empty)
            {
                origen = Request.QueryString["Origen"].ToString();
            }
            if (Request.QueryString["TarSolId"] != null && Request.QueryString["TarSolId"].ToString() != string.Empty)
            {
                tarSolid = Request.QueryString["TarSolId"].ToString();
            }
            if (Request.QueryString["Solicitante"] != null && Request.QueryString["Solicitante"].ToString() != string.Empty)
            {
                solicitante = Request.QueryString["Solicitante"].ToString();
            }
            if (Request.QueryString["TipoConsulta"] != null && Request.QueryString["TipoConsulta"].ToString() != string.Empty)
            {
                tipoConsulta = Request.QueryString["TipoConsulta"].ToString();
            }

            ConsultarDetallesConsultaPublica();
        }

    }
    #endregion

    #region Metodos
    public void ConsultarDetallesConsultaPublica()
    {
        OcularMensaje();
        switch (tipoConsulta)
        {
            case "Todos":
                ScriptManager.RegisterStartupScript(this, this.GetType(), this.Page.Title, string.Format("ConsultarDetalle('{0}','{1}','{2}','{3}')", strNumSilpa, origen, tarSolid, solicitante), true);
                break;
            case "Publicacion":
                ScriptManager.RegisterStartupScript(this, this.GetType(), this.Page.Title, string.Format("ConsultarDetallePublicacion('{0}')", strNumSilpa), true);
                break;
            case "EIA":
                break;
            default:
                MostrarMensaje("No se encontraro registros");
                ScriptManager.RegisterStartupScript(this, this.GetType(), this.Page.Title, "alert('No se cumple con los criterios básicos de busqueda');", true);
                break;
        }
    }
    /// <summary>
    /// Mostra mensjae en label superior
    /// </summary>
    /// <param name="strTextoMensaje"></param>
    private void MostrarMensaje(string strTextoMensaje)
    {
        this.lblMenjase.Text = strTextoMensaje;
        this.lblMenjase.Visible = true;
    }
    private void OcularMensaje()
    {
        this.lblMenjase.Text = string.Empty;
        this.lblMenjase.Visible = false;
    }
    private static string DetalleExpediente(string numeroExpediente, string origen)
    {
        try
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = new Dictionary<string, object>();
            numSilpa = numeroExpediente;
            if (origen == OrigenConsultaPublica.VITAL.ToString())
                row = ConsultaSILPA(numeroExpediente);
            else if (origen == OrigenConsultaPublica.SILAMC.ToString())
                row = ConsultaSILAMC();
            else if (origen == OrigenConsultaPublica.SILA.ToString() || origen == OrigenConsultaPublica.ANLA.ToString())
                row = ConsultaSILA();
            else if (origen == OrigenConsultaPublica.EIA.ToString() || origen == OrigenConsultaPublica.EIA.ToString())
                row = ConsultaEIA(numeroExpediente);
            if (row.Count() > 0)
                rows.Add(row);
            else
                return string.Empty;
            return serializer.Serialize(rows);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::DetalleExpediente" + ex.ToString());
            throw new Exception("DetalleExpediente", ex.InnerException);
        }
    }
    private static string DetallePublicacion(string parametroBusqueda)
    {
        try
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> lstDocPublicados = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Publicacion _listaPublicaciones = new Publicacion();
            DataSet _temp = _listaPublicaciones.ListarPublicacion(int.Parse(parametroBusqueda), null, null, null, null, null, null, null, null, null, null);
            if (_temp.Tables.Count > 0)
            {
                if (_temp.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in _temp.Tables[0].Rows)
                    {
                        lstDocPublicados.Add("lblTitulo", fila["TITULO_PUB"].ToString());
                        lstDocPublicados.Add("lblTramite", fila["TIPO_TRAMITE"].ToString());
                        lstDocPublicados.Add("lblAutoridad", fila["AUTORIDAD_AMBIENTAL"].ToString());
                        lstDocPublicados.Add("lblNombreProyecto", fila["EXP_NOMBRE"].ToString());
                        lstDocPublicados.Add("lblNumeroDocumento", fila["NUM_DOCUMENTO"].ToString());
                        lstDocPublicados.Add("lblExpediente", fila["EXP_CODIGO"].ToString());
                        lstDocPublicados.Add("lblFechaFijacion", fila["FECHA_FIJACION"].ToString());
                        lstDocPublicados.Add("lblFechaDesFijacion", fila["FECHA_DESFIJACION"].ToString());
                        lstDocPublicados.Add("lblRutaPub", fila["Ruta_Pub"].ToString());
                    }
                    rows.Add(lstDocPublicados);
                }
            }
            return serializer.Serialize(rows);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::DetallePublicacion" + ex.ToString());
            throw new Exception("DetalleExpediente", ex.InnerException);
        }
    }
    
    #endregion

    #region WebMetods
    /// <summary>
    /// Consulta el detalle de la solicitud
    /// </summary>
    /// <param name="parametroDetalle"></param>
    /// <param name="idSolicitante"></param>
    /// <param name="sol_id"></param>
    /// <param name="origen"></param>
    [WebMethod]
    public static void ConsultarDetalleSolicitud(string parametroDetalle, string idSolicitante, string sol_id, string origen)
    {
        string resultado = string.Empty;
        if (idSolicitante == "")
            idUsuario = 1;
        else
            idUsuario = Int64.Parse(idSolicitante);
        exp_id = Int64.Parse(sol_id);
        HttpContext context = HttpContext.Current;
        context.Response.Clear();
        context.Response.Flush();
        if (origen != OrigenConsultaPublica.EIA.ToString())
            resultado = DetalleExpediente(parametroDetalle, origen);
        else
            resultado = DetalleEIA(parametroDetalle, origen);
        context.Response.Write(resultado);
        context.Response.End();
    }
    [WebMethod]
    public static void MostrarDocumentos(string lstDocumentosSeguimiento)
    {
        HttpContext context = HttpContext.Current;
        context.Response.Clear();
        context.Response.Flush();
        var lstDocumentos = @lstDocumentosSeguimiento;

        if (lstDocumentos.Contains("lblSol_Numero"))
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Documento documento = jss.Deserialize<Documento>(@lstDocumentos.ToString());
            if (documento.lblSol_Numero != null)
            {
                numSilpa = documento.lblSol_Numero;
                context.Response.Write(MostrarDocumentosVital(documento, context));
            }
        }
        else if (lstDocumentos.Contains("Numero"))
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var glossaryEntry = ser.Deserialize(lstDocumentos.ToString(), typeof(object)) as Dictionary<string, object>;

            string dir_id = (from KeyValuePair<string, object> d in glossaryEntry.AsEnumerable()
                                where d.Key.Equals("DirectorioID")
                                select d.Value).ToList()[0].ToString().Trim();

            string tar_id = (from KeyValuePair<string, object> d in glossaryEntry.AsEnumerable()
                                where d.Key.Equals("TareaID")
                                select d.Value).ToList()[0].ToString().Trim();
            string origen = (from KeyValuePair<string, object> d in glossaryEntry.AsEnumerable()
                                where d.Key.Equals("Origen")
                                select d.Value).ToList()[0].ToString().Trim();
            string numeroDoc = (from KeyValuePair<string, object> d in glossaryEntry.AsEnumerable()
                                where d.Key.Equals("Numero")
                                select d.Value).ToList()[0].ToString().Trim();

            string nombreDoc = (from KeyValuePair<string, object> d in glossaryEntry.AsEnumerable()
                                where d.Key.Equals("TipoObjeto")
                                select d.Value).ToList()[0].ToString().Trim();

            if (origen == OrigenConsultaPublica.SILAMC.ToString())
                context.Response.Write(MostrarDocumentosSilaMC(dir_id, tar_id, numeroDoc, nombreDoc, context));
            else if (origen == OrigenConsultaPublica.SILA.ToString())
                context.Response.Write(MostrarDocumentosSILA(dir_id, tar_id, numeroDoc, nombreDoc, context));
        }
        else if (lstDocumentos.Contains("lstEIA"))
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var glossaryEntry = ser.Deserialize(lstDocumentos.ToString(), typeof(object)) as Dictionary<string, object>;

            string _ubicacion = (from KeyValuePair<string, object> d in glossaryEntry.AsEnumerable()
                                    where d.Key.Equals("lblPath")
                                    select d.Value).ToList()[0].ToString().Trim();
            string patAdicional = _ubicacion.Substring(20, 14);

            string _nombre = (from KeyValuePair<string, object> d in glossaryEntry.AsEnumerable()
                                where d.Key.Equals("lblNombreDocumento")
                                select d.Value).ToList()[0].ToString().Trim();

            string nuevonombre = _nombre.Substring(0, _nombre.Length - 4);
            System.IO.FileInfo targetFile = new System.IO.FileInfo(_ubicacion + _nombre);
            // var tempNombre = targetFile.Name;
            var tempex = targetFile.Extension;
            nuevonombre = _ubicacion + nuevonombre + "_" + patAdicional + tempex;
            context.Response.Write(DescargarDocumento(@nuevonombre, string.Empty, context));

        }
        context.Response.End();
    }
    [WebMethod]
    public static void ConsultarDetallePublicacion(string parametroDetalle)
    {
        HttpContext context = HttpContext.Current;
        var parametroBusqueda = parametroDetalle;
        context.Response.Clear();
        context.Response.Flush();
        context.Response.Write(DetallePublicacion(parametroBusqueda));
        context.Response.End();
    }
    #endregion

    #region consultaSILPA
    private static Dictionary<string, object> ConsultaSILPA(string numeroExpediente)
    {
        Dictionary<string, object> row = new Dictionary<string, object>();
        ConsultaPublica _consultaPublica = new ConsultaPublica();
        DataTable infExpedienteCP = _consultaPublica.ConsultarExpediente(numeroExpediente);
        if (infExpedienteCP.Rows.Count > 0)
        {

            string tempNombreProyecto = string.Empty;
            if (infExpedienteCP.Rows[0]["NOMBRE_PROYECTO"].ToString().Length > 0)
                tempNombreProyecto = infExpedienteCP.Rows[0]["NOMBRE_PROYECTO"].ToString();
            else if (infExpedienteCP.Rows[0]["nombre_expediente"].ToString().Length > 0)
                tempNombreProyecto = infExpedienteCP.Rows[0]["nombre_expediente"].ToString();

            row.Add("lblnumeroExpediente", numeroExpediente);
            row.Add("lblNombreProyectoValue", tempNombreProyecto);
            row.Add("lblDescripcionProyectoValue", infExpedienteCP.Rows[0]["DESCRIPCION_EXPEDIENTE"].ToString());
            row.Add("lblSector", infExpedienteCP.Rows[0]["SEC_NOMBRE"].ToString());
            row.Add("lblSolicitante", infExpedienteCP.Rows[0]["NOMBRE_COMPLETO"].ToString());
            row.Add("lblCorreoElectronicoSolicitante", string.Empty);
            row.Add("lblUbicacion", infExpedienteCP.Rows[0]["MUNICIPIO"].ToString() + "-" + infExpedienteCP.Rows[0]["DEPARTAMENTO"].ToString());
            row.Add("lblCodigoExpediente", infExpedienteCP.Rows[0]["EXPEDIENTE"].ToString());
            row.Add("lblTramite", infExpedienteCP.Rows[0]["TRA_NOMBRE"].ToString());
            row.Add("lblAutoridadAmbiental", infExpedienteCP.Rows[0]["AUT_NOMBRE"].ToString());

        }
        else
        {
            NumeroSilpaDalc numSilpa = new NumeroSilpaDalc();
            DataTable infExpediente = numSilpa.ConsultarExpediente(numeroExpediente);
            if (infExpediente.Rows.Count > 0)
            {
                row.Add("lblnumeroExpediente", numeroExpediente);
                row.Add("lblNombreProyectoValue", infExpediente.Rows[0]["NOMBRE_EXPEDIENTE"].ToString());
                row.Add("lblDescripcionProyectoValue", infExpediente.Rows[0]["DESCRIPCION_EXPEDIENTE"].ToString());
                row.Add("lblSector", infExpediente.Rows[0]["SECTOR"].ToString());
                row.Add("lblSolicitante", infExpediente.Rows[0]["NOMBRE_SOLICITANTE"].ToString());
                row.Add("lblCorreoElectronicoSolicitante", infExpediente.Rows[0]["CORREO_SOLICITANTE"].ToString());
                row.Add("lblUbicacion", infExpediente.Rows[0]["UBICACION"].ToString());
                row.Add("lblCodigoExpediente", infExpediente.Rows[0]["CODIGO_EXPEDIENTE"].ToString());
                row.Add("lblTramite", string.Empty);
            }
            else
            {
                DataTable infExpediente2 = numSilpa.ConsultarExpedienteXCodExo(numeroExpediente);
                if (infExpediente2.Rows.Count > 0)
                {
                    row.Add("lblNombreProyectoValue", infExpediente2.Rows[0]["NOMBRE_EXPEDIENTE"].ToString());
                    row.Add("lblDescripcionProyectoValue", infExpediente2.Rows[0]["DESCRIPCION_EXPEDIENTE"].ToString());
                    row.Add("lblSector", infExpediente2.Rows[0]["SECTOR"].ToString());
                    row.Add("lblSolicitante", infExpediente2.Rows[0]["NOMBRE_SOLICITANTE"].ToString());
                    row.Add("lblCorreoElectronicoSolicitante", infExpediente2.Rows[0]["CORREO_SOLICITANTE"].ToString());
                    row.Add("lblUbicacion", infExpediente2.Rows[0]["UBICACION"].ToString());
                    row.Add("lblCodigoExpediente", infExpediente2.Rows[0]["CODIGO_EXPEDIENTE"].ToString());
                    row.Add("lblTramite", string.Empty);
                }
            }
        }
        if (row.Count() > 0)
        {
            row.Add("lstResumenEstado", ListaResumenSolicitudLineaProceso());
            row.Add("lstExpedientes", ListaExpedientesAsociados());
            row.Add("lsDocumentosSeguimiento", ListaDocumentosSeguimiento());
            row.Add("lsDocumentosEvaluacion", ListaDocumentosEvaluacion());
            row.Add("lsDocumentosInvestigacion", ListaDocumentosInvestigacion());
            row.Add("lsDocumentosCobros", ListaDocumentosCobros());
            row.Add("lsDocumentosOtros", ListaDocumentosOtros());
            row.Add("lsDocumentosRepocision", ListaDocumentosModificacion());
            row.Add("lsDocumentosModificacion", ListaDocumentosReposicion());
        }
        return row;
    }
    /// <summary>
    /// Retorna la lista del resumen de la solicitud
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, object> ListaResumenSolicitud()
    {
        try
        {
            SILPA.LogicaNegocio.AdmTablasBasicas.Solicitudes objAdmTablas = new SILPA.LogicaNegocio.AdmTablasBasicas.Solicitudes();
            Dictionary<string, object> lsRetorno = new Dictionary<string, object>();
            List<ResumenTramite> lstGeneral = new List<ResumenTramite>();
            List<DataTable> listDt = new List<DataTable>();

            listDt = objAdmTablas.BuscarSolicitud(numSilpa);

            foreach (DataTable dt in listDt)
            {
                foreach (DataRow drGeneral in dt.Rows)
                {
                    lstGeneral.Add(new ResumenTramite
                    {
                        id = drGeneral["id"].ToString(),
                        name = drGeneral["name"].ToString(),
                        nivel = drGeneral["nivel"].ToString(),
                        id_Padre = drGeneral["id_padre"].ToString(),
                        ejecutada = drGeneral["Ejecutada"].ToString()
                    });
                }
                lsRetorno.Add("resumenTramite", lstGeneral);
            }
            return lsRetorno;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaExpedientesAsociados" + ex.ToString());
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, object> ListaResumenSolicitudLineaProceso()
    {
        try
        {
            SILPA.LogicaNegocio.AdmTablasBasicas.Solicitudes objAdmTablas = new SILPA.LogicaNegocio.AdmTablasBasicas.Solicitudes();
            Dictionary<string, object> lsRetorno = new Dictionary<string, object>();
            List<ResumenTramite> lstGeneral = new List<ResumenTramite>();
            List<DataTable> listDt = new List<DataTable>();

            listDt = objAdmTablas.BuscarSolicitud(numSilpa);

            foreach (DataTable dt in listDt)
            {
                foreach (DataRow drGeneral in dt.Rows)
                {
                    lstGeneral.Add(new ResumenTramite
                    {
                        id = drGeneral["id"].ToString(),
                        name = drGeneral["name"].ToString(),
                        nivel = drGeneral["nivel"].ToString(),
                        id_Padre = drGeneral["id_padre"].ToString(),
                        ejecutada = drGeneral["Ejecutada"].ToString()
                    });
                }

                ///Procesamos la lista para el objeto del timeline
                List<events> lstevents = new List<events>();
                // hacemos un distinct por cada nivel
                var niveles = lstGeneral.GroupBy(i => i.nivel).Select(group => group.First());

                foreach (var nivel in niveles)
                {
                    if (lstGeneral.Where(x => x.nivel == nivel.nivel && x.ejecutada == "1").Count() > 0)
                    {
                        foreach (ResumenTramite actejecutada in lstGeneral.Where(x => x.nivel == nivel.nivel && x.ejecutada == "1"))
                        {
                            lstevents.Add(new events { date = actejecutada.name, content = "<span class='TareaEjecutada'>Tarea ejecutada</span>" });
                        }
                    }
                    else
                    {
                        foreach (ResumenTramite actejecutada in lstGeneral.Where(x => x.nivel == nivel.nivel && x.ejecutada == "0"))
                        {
                            lstevents.Add(new events { date = actejecutada.name, content = "<span class='TareaPendiente'>Tarea pendiente</span>" });
                        }
                        break;
                    }
                }

                lsRetorno.Add("resumenTramite", lstevents);
            }
            return lsRetorno;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaExpedientesAsociados" + ex.ToString());
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// Retorna la lista del detalle de la actividad
    /// </summary>
    /// <param name="idActividad"></param>
    /// <returns></returns>
    public string ListaDetalleActividadSolicitud(int idActividad)
    {
        try
        {
            SILPA.LogicaNegocio.AdmTablasBasicas.Solicitudes objAdmTablas = new SILPA.LogicaNegocio.AdmTablasBasicas.Solicitudes();

            Dictionary<string, object> lsRetorno = new Dictionary<string, object>();
            List<ActividadCondicion> lstActividadCondicion = new List<ActividadCondicion>();
            Dictionary<string, object> lsActividad = new Dictionary<string, object>();
            List<DataTable> listDt = new List<DataTable>();
            int numeroTabla = 0;
            listDt = objAdmTablas.BuscarDetalleActividadSolicitud(idActividad, strNumSilpa);
            Actividad objActividad = new Actividad();

            foreach (DataTable tabla in listDt)
            {
                if (numeroTabla == 0)
                {
                    foreach (DataRow fila in tabla.Rows)
                    {
                        lstActividadCondicion.Add(new ActividadCondicion
                        {
                            lblIdExpediente = fila["ID_EXPEDIENTE"].ToString(),
                            lblIdDocumento = fila["ID_DOCUMENTO"].ToString(),
                            lblCodigoCondicion = fila["CODIGO_CONDICION"].ToString(),
                            lblCodigoCondicionBPM = fila["CODIGO_CONDICION_BPM"].ToString(),
                            lblDescripcionDocumento = fila["DESCRIPCION_DOCUMENTO"].ToString(),
                            lblPathDocumento = fila["PATH_DOCUMENTO"].ToString(),
                            lblNombreDocumento = fila["NOMBRE_DOCUMENTO"].ToString(),
                            lblNombreCondicion = fila["nombre_condicion"].ToString(),
                            lblFechaCreacion = fila["FECHA_CREACION"].ToString(),
                            lblFechaNotificacion = fila["NOT_FECHA_ACTO"].ToString()
                        });
                    }
                    numeroTabla = 1;
                    lsRetorno.Add("lstActividadCondicion", lstActividadCondicion);
                }
                else
                {
                    foreach (DataRow fila in tabla.Rows)
                    {
                        objActividad.lblIdProcessInstance = fila["IDPROCESSINSTANCE"].ToString();
                        objActividad.lblIdActivity = fila["IDACTIVITY"].ToString();
                        objActividad.lblName = fila["NAME"].ToString();
                        objActividad.lblusuario = fila["USUARIO"].ToString();
                        objActividad.lblStarttime = fila["starttime"].ToString();
                        objActividad.lblendtime = fila["endtime"].ToString();
                        objActividad.lblNombreUsuario = fila["NAME_USUARIO"].ToString();
                    }
                    lsRetorno.Add("lstActividad", objActividad);
                }
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(lsRetorno);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaExpedientesAsociados" + ex.ToString());
            throw new Exception(ex.Message);
        }

    }

    /// <summary>
    /// Retorna la lista de expedientes asociados a la solicitud
    /// </summary>
    /// <returns></returns>
    private static List<String> ListaExpedientesAsociados()
    {
        List<String> lstExpedientes = new List<String>();
        try
        {
            Listas objGenerico = new Listas();
            DataTable dt = new DataTable();
            dt = objGenerico.ListarDocumentosEvaluacion(6, numSilpa, int.Parse(idUsuario.ToString()));
            foreach (DataRow fila in dt.Rows)
            {
                lstExpedientes.Add(fila["EXP_CODIGO"].ToString());
            }
            return lstExpedientes;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaExpedientesAsociados" + ex.ToString());
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// retorna la lista de documentos de Seguimiento
    /// </summary>
    /// <returns>lista de documentos de Seguimiento</returns>
    private static List<Documento> ListaDocumentosSeguimiento()
    {
        List<Documento> lsDocumentosSeguimiento = new List<Documento>();
        try
        {
            SILPA.LogicaNegocio.ReporteTramite.DocumentacionSolicitud objDocumentacion = new SILPA.LogicaNegocio.ReporteTramite.DocumentacionSolicitud();
            DataSet ds = objDocumentacion.ListarDocumentacionSolicitudFUSxPerfil(numSilpa, int.Parse(idUsuario.ToString()));
            //   DataSet ds = objDocumentacion.ListarDocumentacionSolicitudFus(strNumSilpa);
            if (ds.Tables.Count > 0)
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    lsDocumentosSeguimiento.Add(new Documento
                    {
                        lblDocumento = fila["Descripcion"].ToString(),
                        lblIdParticipant = fila["idparticipant"].ToString(),
                        lblSolFechaCreacion = fila["SOL_FECHA_CREACION"].ToString(),
                        lblExpediente = fila["ID_EXPEDIENTE"].ToString(),
                        lblSol_Numero = fila["SOL_NUMERO_SILPA"].ToString(),
                        lblEntryData = fila["EntryData"].ToString(),
                        lblIdEntryData = fila["IdEntryData"].ToString(),
                        lblEntryDataType = fila["EntryDataType"].ToString(),
                        lblIdActivity = fila["IdActivity"].ToString(),
                        lblIdProcessInstance = fila["IdProcessInstance"].ToString(),
                        lblURLDataView = fila["URLDataView"].ToString(),
                        lblRuta = fila["Ruta"].ToString(),
                        lblIdRelated = fila["IdRelated"].ToString()
                    });
                }
            return lsDocumentosSeguimiento;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosSeguimiento" + ex.ToString());
            throw new Exception(ex.Message);
        }
    }

    

    /// <summary>
    /// retorna la lista de documentos de Evaluacion
    /// </summary>
    /// <param name="sortExp"></param>
    /// <returns></returns>
    private static List<Documento> ListaDocumentosEvaluacion()
    {
        List<Documento> lsDocumentosEvaluacion = new List<Documento>();
        try
        {
            SILPA.LogicaNegocio.Generico.Listas objGenerico = new SILPA.LogicaNegocio.Generico.Listas();
            dtResult = objGenerico.ListarDocumentosEvaluacion(1, numSilpa, int.Parse(idUsuario.ToString()));
            foreach (DataRow fila in dtResult.Rows)
            {
                lsDocumentosEvaluacion.Add(new Documento
                {
                    lblDocumento = fila["DESCRIPCION"].ToString(),
                    lblIdParticipant = fila["URL_PARTICIPANTE"].ToString(),
                    lblSolFechaCreacion = fila["FECHA"].ToString(),
                    lblExpediente = fila["ID_EXPEDIENTE"].ToString(),
                    lblSol_Numero = fila["NUMERO_SILPA"].ToString(),
                    lblEntryData = string.Empty,
                    lblIdEntryData = string.Empty,
                    lblEntryDataType = string.Empty,
                    lblIdActivity = string.Empty,
                    lblIdProcessInstance = string.Empty,
                    lblURLDataView = string.Empty,
                    lblRuta = fila["RUTAARCHIVO"].ToString(),
                    lblIdRelated = string.Empty,
                    lblPathDocumento = fila["PATH_DOCUMENTO"].ToString()
                });
            }
            return lsDocumentosEvaluacion;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosEvaluacion" + ex.ToString());
            throw new Exception(ex.Message);

        }
    }

    /// <summary>
    ///  retorna la lista de documentos de Investigacion
    /// </summary>
    /// <returns></returns>
    private static List<Documento> ListaDocumentosInvestigacion()
    {
        List<Documento> lsDocumentosInvestigacion = new List<Documento>();
        try
        {
            Listas objGenerico = new Listas();
            DataTable dt = new DataTable();
            dt = objGenerico.ListarDocumentosEvaluacion(4, numSilpa, int.Parse(idUsuario.ToString()));
            foreach (DataRow fila in dt.Rows)
            {
                lsDocumentosInvestigacion.Add(new Documento
                {
                    lblDocumento = fila["Descripcion"].ToString(),
                    lblIdParticipant = fila["URL_PARTICIPANTE"].ToString(),
                    lblSolFechaCreacion = fila["FECHA"].ToString(),
                    lblExpediente = fila["ID_EXPEDIENTE"].ToString(),
                    lblSol_Numero = fila["NUMERO_SILPA"].ToString(),
                    lblEntryData = string.Empty,
                    lblIdEntryData = string.Empty,
                    lblEntryDataType = string.Empty,
                    lblIdActivity = string.Empty,
                    lblIdProcessInstance = string.Empty,
                    lblURLDataView = string.Empty,
                    lblRuta = fila["RUTAARCHIVO"].ToString(),
                    lblIdRelated = string.Empty,
                    lblPathDocumento = fila["PATH_DOCUMENTO"].ToString()
                });
            }
            return lsDocumentosInvestigacion;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosInvestigacion" + ex.ToString());
            throw new Exception(ex.Message);

        }
    }

    /// <summary>
    ///  retorna la lista de documentos de Cobros
    /// </summary>
    /// <returns>lista de documentos de Cobros</returns>
    private static List<Documento> ListaDocumentosCobros()
    {
        List<Documento> lsDocumentosCobros = new List<Documento>();
        try
        {
            SILPA.LogicaNegocio.Generico.Cobro cobro = new SILPA.LogicaNegocio.Generico.Cobro();
            DataTable dt = new DataTable();
            dt = cobro.ListarCobros(numSilpa, int.Parse(idUsuario.ToString()));
            foreach (DataRow fila in dt.Rows)
            {
                lsDocumentosCobros.Add(new Documento
                {
                    lblDocumento = fila["COB_NUMERO_SILPA"].ToString(),
                    lblIdParticipant = fila["IDParticipant"].ToString(),
                    lblSolFechaCreacion = fila["COB_FECHA_EXPEDICION"].ToString(),
                    lblEntryData = string.Empty,
                    lblIdEntryData = string.Empty,
                    lblEntryDataType = string.Empty,
                    lblIdActivity = string.Empty,
                    lblIdProcessInstance = string.Empty,
                    lblRuta = fila["COB_RUTA_ARCHIVOS"].ToString(),
                    lblURLDataView = string.Empty,
                    lblIdRelated = string.Empty,
                    lblExpediente = fila["COB_NUMERO_EXPEDIENTE"].ToString(),
                    lblPathDocumento = string.Empty
                });
            }
            return lsDocumentosCobros;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosCobros" + ex.ToString());
            throw new Exception(ex.Message);

        }

    }

    /// <summary>
    /// Retorna la lista de los documentos
    /// </summary>
    /// <returns></returns>
    private static List<Documento> ListaDocumentosOtros()
    {
        List<Documento> lsDocumentosOtros = new List<Documento>();
        try
        {
            DataTable dt = new DataTable();
            Listas objGenerico = new Listas();
            dt = objGenerico.ListarDocumentosEvaluacion(5, numSilpa, int.Parse(idUsuario.ToString()));
            foreach (DataRow fila in dt.Rows)
            {
                lsDocumentosOtros.Add(new Documento
                {
                    lblDocumento = fila["Descripcion"].ToString(),
                    lblIdParticipant = fila["URL_PARTICIPANTE"].ToString(),
                    lblSolFechaCreacion = fila["FECHA"].ToString(),
                    lblExpediente = fila["ID_EXPEDIENTE"].ToString(),
                    lblSol_Numero = fila["NUMERO_SILPA"].ToString(),
                    lblEntryData = string.Empty,
                    lblIdEntryData = string.Empty,
                    lblEntryDataType = string.Empty,
                    lblIdActivity = string.Empty,
                    lblIdProcessInstance = string.Empty,
                    lblURLDataView = string.Empty,
                    lblRuta = fila["RUTAARCHIVO"].ToString(),
                    lblIdRelated = string.Empty,
                    lblPathDocumento = fila["PATH_DOCUMENTO"].ToString()
                });
            }
            return lsDocumentosOtros;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::lsDocumentosOtros" + ex.ToString());
            throw new Exception(ex.Message);

        }

    }

    /// <summary>
    /// retorna la lista de documentos de Modificacion
    /// </summary>
    /// <returns>lista de documentos de Modificacion</returns>
    private static List<Documento> ListaDocumentosModificacion()
    {
        List<Documento> lsDocumentosModificacion = new List<Documento>();
        try
        {
            DataTable dt = new DataTable();
            Listas objGenerico = new Listas();
            dt = objGenerico.ListarDocumentosEvaluacion(8, numSilpa, int.Parse(idUsuario.ToString()));
            foreach (DataRow fila in dt.Rows)
            {
                lsDocumentosModificacion.Add(new Documento
                {
                    lblDocumento = fila["Descripcion"].ToString(),
                    lblIdParticipant = fila["URL_PARTICIPANTE"].ToString(),
                    lblSolFechaCreacion = fila["FECHA"].ToString(),
                    lblExpediente = fila["ID_EXPEDIENTE"].ToString(),
                    lblSol_Numero = fila["NUMERO_SILPA"].ToString(),
                    lblEntryData = string.Empty,
                    lblIdEntryData = string.Empty,
                    lblEntryDataType = string.Empty,
                    lblIdActivity = string.Empty,
                    lblIdProcessInstance = string.Empty,
                    lblURLDataView = string.Empty,
                    lblRuta = fila["RUTAARCHIVO"].ToString(),
                    lblIdRelated = string.Empty,
                    lblPathDocumento = fila["PATH_DOCUMENTO"].ToString()
                });
            }
            return lsDocumentosModificacion;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosModificacion" + ex.ToString());
            throw new Exception(ex.Message);

        }
    }

    /// <summary>
    /// retorna la lista de documentos de  Reposicion
    /// </summary>
    /// <returns>ista de documentos de  Reposicion</returns>
    private static List<Documento> ListaDocumentosReposicion()
    {
        List<Documento> lsDocumentosReposicion = new List<Documento>();
        try
        {
            DataTable dt = new DataTable();
            Listas objGenerico = new Listas();
            dt = objGenerico.ListarDocumentosEvaluacion(7, numSilpa, int.Parse(idUsuario.ToString()));
            foreach (DataRow fila in dt.Rows)
            {
                lsDocumentosReposicion.Add(new Documento
                {
                    lblDocumento = fila["Descripcion"].ToString(),
                    lblIdParticipant = fila["URL_PARTICIPANTE"].ToString(),
                    lblSolFechaCreacion = fila["FECHA"].ToString(),
                    lblExpediente = fila["ID_EXPEDIENTE"].ToString(),
                    lblSol_Numero = fila["NUMERO_SILPA"].ToString(),
                    lblEntryData = string.Empty,
                    lblIdEntryData = string.Empty,
                    lblEntryDataType = string.Empty,
                    lblIdActivity = string.Empty,
                    lblIdProcessInstance = string.Empty,
                    lblURLDataView = string.Empty,
                    lblRuta = fila["RUTAARCHIVO"].ToString(),
                    lblIdRelated = string.Empty,
                    lblPathDocumento = fila["PATH_DOCUMENTO"].ToString()
                });
            }
            return lsDocumentosReposicion;

        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosReposicion" + ex.ToString());
            throw new Exception(ex.Message);

        }
    }

    #endregion

    #region ConsultaSILA
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Dictionary<string, object> ConsultaSILA()
    {
        Dictionary<string, object> row = new Dictionary<string, object>();
        try
        {
            ExpedienteSila _expedienteSila = new ExpedienteSila();
            List<ExpedienteSilaEntity> listaExpedienteDetalle = new List<ExpedienteSilaEntity>();
            listaExpedienteDetalle = _expedienteSila.ConsultarDetalleExpedienteSILA(exp_id.ToString());
            foreach (ExpedienteSilaEntity _expedienteIdentity in listaExpedienteDetalle)
            {
                row.Add("lblnumeroExpediente", _expedienteIdentity.exp_id);
                row.Add("lblNombreProyectoValue", _expedienteIdentity.str_expediente_nombre);
                row.Add("lblDescripcionProyectoValue", _expedienteIdentity.str_exp_descripcion);
                row.Add("lblSector", _expedienteIdentity.str_sec_nombre);
                row.Add("lblSolicitanteId", _expedienteIdentity.str_solicitante_id);
                row.Add("lblSolicitante", _expedienteIdentity.str_nombre_solicitante);
                row.Add("lblUbicacion", _expedienteIdentity.str_nombre_ubicacion);
                row.Add("lblCodigoExpediente", _expedienteIdentity.str_exp_codigo);
                row.Add("lblTramiteId", _expedienteIdentity.str_numero_tramite);
                row.Add("lblTramite", _expedienteIdentity.str_nombre_tramite);
                row.Add("lblTramiteEstado", _expedienteIdentity.str_est_nombre);
                row.Add("lblAdmId", _expedienteIdentity.str_numero_adm);
                row.Add("lblAutoridadAmbiental", _expedienteIdentity.str_nombre_autoridad_ambiental);
            }
            row.Add("lstResumenEstado", ListaResumenSolicitudLineaProceso());
            row.Add("lstEtapas", ListaEtapasSila());
            row.Add("lstExpedientesAsociados", ListaExpedientesAsociadosSILA());
            row.Add("lstArchivosForest", ListaArchivosForest());
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ConsultaSILA" + ex.ToString());
            throw new Exception("ConsultaSILA", ex.InnerException);
        }
        return row;
    }

    private static List<string> ListaExpedientesAsociadosSILA()
    {
        List<string> row = new List<string>();
        try
        {
            DataTable listaExpedientesAsociados = new DataTable();
            ExpedienteSila _expedienteSila = new ExpedienteSila();
            listaExpedientesAsociados = _expedienteSila.ListarExpedientesAsociados(exp_id.ToString());
            foreach (DataRow fila in listaExpedientesAsociados.Rows)
            {
                row.Add(fila["EXP_CODIGO"].ToString());
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosReposicion" + ex.ToString());
            throw new Exception("ListaExpedientesAsociadosSILA", ex.InnerException);
        }
        return row;
    }

    /// <summary>
    /// Lista de etapas asociadas a un expediente
    /// </summary>
    /// <returns></returns>
    private static List<Etapa> ListaEtapasSila()
    {
        List<Etapa> lstEtapas = new List<Etapa>();
        try
        {
            Listas objGenerico = new Listas();
            DataTable dtListarEtapas = new DataTable();
            ExpedienteSila _expedienteSila = new ExpedienteSila();
            DataSet _temp1 = _expedienteSila.ListarEtapas(exp_id.ToString());
            if (_temp1.Tables.Count > 0)
                dtListarEtapas = _temp1.Tables[0];
            foreach (DataRow fila in dtListarEtapas.Rows)
            {
                DataTable dtInstanciasActos = new DataTable();
                //dtInstanciasActos = _expedienteSila.ListarInstanciasActos(exp_id.ToString(), string.Empty, string.Empty, fila["EXE_ID"].ToString(), "1,2,3,4,5");
                dtInstanciasActos = _expedienteSila.ListarInstanciasActosValidaNotificacion(exp_id.ToString(), fila["EXE_ID"].ToString());

                List<DetalleEtapa> lstDetalleEtapa = new List<DetalleEtapa>();
                foreach (DataRow filaIA in dtInstanciasActos.Rows)
                {
                    DetalleEtapa dtEtapa = new DetalleEtapa(
                        filaIA["DIRD_ID"].ToString(),
                        filaIA["EXE_NOMBRE"].ToString(),
                        filaIA["DOC_NUMERO"].ToString(),
                        DateTime.Parse(filaIA["DOC_FECHA"].ToString()).ToShortDateString(),
                        filaIA["DOC_FECHA_NOTIFICACION"].ToString(),
                        filaIA["TIPO_OBJETO"].ToString(),
                        filaIA["DOC_DESCRIPCION"].ToString().Replace('"', ' ').Replace('/', ' ').Replace("'", " "),
                        filaIA["TIPO_OBJETO"].ToString(),
                        filaIA["TAR_ID"].ToString(),
                        filaIA["TAAD_ID"].ToString(),
                        "SILA"
                        );
                    lstDetalleEtapa.Add(dtEtapa);
                }
                lstEtapas.Add(new Etapa(
                    int.Parse(fila["ETA_ID"].ToString()),
                    fila["EXE_NOMBRE"].ToString(),
                    fila["EXE_ID"].ToString(),
                    lstDetalleEtapa
                    ));
            }
            return lstEtapas;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaExpedientesAsociados" + ex.ToString());
            throw new Exception("ListaEtapasSILA", ex.InnerException);
        }
    }

    private static List<Documento> ListaArchivosForest()
    {
        List<Documento> lsArchivosForest = new List<Documento>();
        try
        {
            DataTable dtListaArchivosForest = new DataTable();
            ExpedienteSila _expedienteSila = new ExpedienteSila();
            dtListaArchivosForest = _expedienteSila.ArchivosForest(exp_id.ToString());
            foreach (DataRow fila in dtListaArchivosForest.Rows)
            {
                lsArchivosForest.Add(new Documento
                {
                    lblDocumento = fila["NOMBRE_ARCHIVO"].ToString(),
                    lblRuta = fila["RUTA_ARCHIVO"].ToString(),
                    lblSolFechaCreacion = fila["FECHA_CREACION"].ToString(),
                    lblExpediente = fila["EXP_ID"].ToString(),
                    lblSol_Numero = fila["NUM_RAD"].ToString(),
                    lblEntryData = fila["TIPO_DOCUMENTAL_SIGPRO_ID"].ToString(),
                    lblIdEntryData = fila["FEC_RAD"].ToString(),
                    lblIdParticipant = string.Empty,
                    lblEntryDataType = string.Empty,
                    lblIdActivity = string.Empty,
                    lblIdProcessInstance = string.Empty,
                    lblURLDataView = string.Empty,
                    lblIdRelated = string.Empty,
                    lblPathDocumento = string.Empty
                });
            }
            return lsArchivosForest;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaArchivosForest" + ex.ToString());
            throw new Exception("ListaArchivosForest", ex.InnerException);
        }
    }

    #endregion

    #region MostrarDocumentos
    private static string MostrarDocumentosSILA(string directorioID, string tareaID, string numeroDoc, string nombreDoc, HttpContext context)
    {
        string scrRespuesta = string.Empty;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        try
        {
            string _nombre = string.Empty;
            string _ubicacion = directorioID;
            string rutaSILA = ConfigurationManager.AppSettings["DocsSILA"].ToString();
            if (_ubicacion.Contains("VitalFiletraffic"))
            {

                List<DetalleDocumentoIdentity> ListaDocumentos = new List<DetalleDocumentoIdentity>();
                TraficoDocumento tf = new SILPA.Comun.TraficoDocumento();
                List<string> ListaNombreArchivos = tf.ListarDocumentosDirectorio(rutaSILA);

                DescargarDocumento(_ubicacion, nombreDoc, context);
                scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
            }
            else
            {
                DataSet ds = new DataSet();
                ExpedienteSila exSila = new ExpedienteSila();
                ds = exSila.listarDocumentosDS(int.Parse(directorioID));
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    _nombre = fila["DOC_ARCHIVO"].ToString();
                }

                if (_nombre.Length > 0 && !_nombre.Contains(".doc"))
                {
                    DescargarDocumento(rutaSILA, _nombre, context);
                    scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                    scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                }
                else if (_nombre.Contains(".doc"))
                {
                    scrRespuesta = "ERROR: El documento solicitado no esta disponible en el formato admitido según políticas de publicación, para su consulta acercarse a la Entidad. " + _nombre;
                }
                else
                {
                    DataTable dtDirectoriosDocumentoSila = new DataTable();
                    int int_row = -1;
                    dtDirectoriosDocumentoSila = exSila.ListarDirectoriosDocumentoSila(int.Parse(directorioID));
                    foreach (DataRow oDir in dtDirectoriosDocumentoSila.Rows)
                    {
                        int tmp = int.Parse(oDir["TAR_ID"].ToString());
                        dtDirectoriosDocumentoSila = exSila.ListarDirectoriosDocumentoTareaSila(tmp);
                        foreach (DataRow fila in dtDirectoriosDocumentoSila.Rows)
                        {
                            int tempDird = int.Parse(fila["DIRD_ID"].ToString());
                            ds = exSila.listarDocumentosDS(tempDird);
                            foreach (DataRow oDocumento in ds.Tables[0].Rows)
                            {
                                if (oDocumento["DOC_ARCHIVO"].ToString() != string.Empty)
                                {
                                    int_row = int.Parse(oDocumento["DIRD_ID"].ToString());
                                    nombreDoc = oDocumento["DOC_ARCHIVO"].ToString();
                                }
                            }
                        }
                    }
                    if (int_row >= 0)
                    {
                        if (nombreDoc.Contains(".doc"))
                        {
                            scrRespuesta = "ERROR: El documento solicitado no esta disponible en el formato admitido según políticas de publicación, para su consulta acercarse a la Entidad. " + nombreDoc;
                        }
                        else
                        {
                            DescargarDocumento(rutaSILA, nombreDoc,context);
                            scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                            scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                        }
                    }
                    else
                    {
                        scrRespuesta = "ERROR: El documento solicitado no esta disponible. " + _nombre;
                        SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle" + scrRespuesta);
                    }
                }

            }
            return serializer.Serialize(scrRespuesta);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle" + ex.ToString());
            return serializer.Serialize("ERROR: Excepcion controlada al intentar mostrar el documento.");
        }
    }
    private static String MostrarDocumentosSilaMC(string directorioID, string tareaID, string numeroDoc, string nombreDoc, HttpContext context)
    {
        string scrRespuesta = string.Empty;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        try
        {
            string _nombre = string.Empty;
            string _ubicacion = directorioID;
            string rutaSILAMC = ConfigurationManager.AppSettings["DocsSILAMC"].ToString();
            if (_ubicacion.Contains("VitalFiletraffic"))
            {
                DescargarDocumento(_ubicacion, nombreDoc, context);
                scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
            }
            else
            {
                DataSet ds = new DataSet();
                ExpedienteSila exSila = new ExpedienteSila();
                ds = exSila.ListarDocumentosDSSilaMC(int.Parse(directorioID));
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    _nombre = fila["DOC_ARCHIVO"].ToString();
                }

                if (_nombre.Length > 0 && !_nombre.Contains(".doc"))
                {
                    string rutaSILA = ConfigurationManager.AppSettings["DocsSILAMC"].ToString();
                    DescargarDocumento(rutaSILA, _nombre, context);
                    scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                    scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                }
                else if (_nombre.Contains(".doc"))
                {
                    scrRespuesta = "ERROR: El documento solicitado no esta disponible en el formato admitido según políticas de publicación, para su consulta acercarse a la Entidad. " + _nombre;
                }
                else
                {
                    DataTable dtDirectoriosDocumentoSilaMC = new DataTable();
                    int int_row = -1;
                    dtDirectoriosDocumentoSilaMC = exSila.ListarDirectoriosDocumentoSilaMC(int.Parse(directorioID));
                    foreach (DataRow oDir in dtDirectoriosDocumentoSilaMC.Rows)
                    {
                        int tmp = int.Parse(oDir["TAR_ID"].ToString());
                        dtDirectoriosDocumentoSilaMC = exSila.ListarDirectoriosDocumentoTareaSILAMC(tmp);
                        foreach (DataRow fila in dtDirectoriosDocumentoSilaMC.Rows)
                        {
                            int tempDird = int.Parse(fila["DIRD_ID"].ToString());
                            ds = exSila.ListarDocumentosDSSilaMC(tempDird);
                            foreach (DataRow oDocumento in ds.Tables[0].Rows)
                            {
                                if (oDocumento["DOC_ARCHIVO"] != string.Empty)
                                {
                                    int_row = int.Parse(oDocumento["DIRD_ID"].ToString());
                                    nombreDoc = oDocumento["DOC_ARCHIVO"].ToString();
                                }
                            }
                        }
                    }
                    if (int_row >= 0)
                    {
                        // ds = exSila.ListarDocumentosDSSilaMC(int_row);
                        DescargarDocumento(rutaSILAMC, nombreDoc, context);
                    }
                }
            }
            return serializer.Serialize(scrRespuesta);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle" + ex.ToString());
            return serializer.Serialize("ERROR: Excepcion controlada al intentar mostrar el documento.");
        }
    }
    private static string MostrarDocumentosVital(Documento documento, HttpContext context)
    {
        string lbEntryData = documento.lblEntryData;
        string lbEntryDataType = documento.lblEntryDataType;
        string lbDocumento = documento.lblDocumento;
        string lbRuta = documento.lblRuta;
        string lbPath = documento.lblPathDocumento;
        string lbIdEntryData = documento.lblEntryData;
        string lbActivity = documento.lblIdActivity;
        string lbProcessInstance = documento.lblIdProcessInstance;
        string lbIdrelated = documento.lblIdRelated;
        string lbUrl = documento.lblRuta;
        string scrRespuesta = string.Empty;
        string lblURLDataView = documento.lblURLDataView;

        if (lbPath == null && lbRuta != null)
            lbPath = lbRuta;
        if (lbPath.Length == 0 && lblURLDataView.Length > 0)
        {
            lbPath = lblURLDataView;
            lbEntryDataType = "documentos";
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        try
        {
            if (lbEntryDataType == "documentos")
            {
                if (lbRuta != "")
                    context.Session["Ruta"] = lbRuta;
                else if (lbPath != "")
                    context.Session["Ruta"] = lbPath;
                scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                SMLog.Escribir(Severidad.Critico,"ReporteTramiteCPDetalle.aspx" + "::DocumentosVITAL :: " + lbRuta + " :: " + lbPath);
            }
            else if (lbEntryDataType != "")
            {
                strMUrlFormBuilder = strMUrlFormBuilder.Replace("{ENTRY_DATA}", lbEntryData);
                strMUrlFormBuilder = strMUrlFormBuilder.Replace("{IDENTRY_DATA}", lbIdEntryData);
                strMUrlFormBuilder = strMUrlFormBuilder.Replace("{ENTRY_DATA_TYPE}", lbEntryDataType);
                strMUrlFormBuilder = strMUrlFormBuilder.Replace("{IDACTIVITY_INSTANCE}", lbActivity);
                strMUrlFormBuilder = strMUrlFormBuilder.Replace("{PROCESS_INSTANCE}", lbProcessInstance);
                strMUrlFormBuilder = strMUrlFormBuilder.Replace("{ID_RELATED}", lbIdrelated);
                strMUrlFormBuilder = strMUrlFormBuilder.Replace("{IDUSER}", lbIdrelated);
                if (context.Request["Ubic"] == null)
                    context.Response.Redirect(strMUrlFormBuilder);
                else
                {
                    if (string.IsNullOrEmpty(lbRuta))
                    {
                        string ruta = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + '_' + lbProcessInstance + ".rtf";
                        if (System.IO.File.Exists(ruta))
                        {
                            context.Session["Ruta"] = ruta;
                            scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                            scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL::" + ruta + "::" + lbDocumento);
                        }
                        else
                        {
                            scrRespuesta = "ERROR: Esta Solicitud no tiene Documentos, para más información acercarse a la Entidad. ";
                            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL(Esta Solicitud no tiene Documentos)::" + ruta + "::" + lbDocumento);
                        }
                    }
                    else
                    {
                        scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                        scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                        context.Session["Ruta"] = lbRuta;
                        SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL::" + lbRuta + lbDocumento);
                    }
                }
            }
            else if (lbPath.Contains("http://"))
            {
                lbPath = lbPath.Replace("=seg", "=vital");
                scrRespuesta = lbPath;
                SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL::lbPath::" + lbPath + "::" + lbDocumento);
            }
            else
            {
                string filePath = lbUrl;
                if (filePath != "")
                {
                    if (filePath.Contains(":\\") || filePath.Contains("://"))
                    {
                        if (filePath.Contains("VitalFiletraffic"))
                        {
                            if (lbRuta.Contains("F:"))
                            {
                                lbRuta = lbRuta.Substring(lbRuta.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                                lbRuta = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL"].ToString() + lbRuta;
                            }
                            else if (lbRuta.Contains("H:"))
                            {
                                lbRuta = lbRuta.Substring(lbRuta.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                                lbRuta = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_H"].ToString() + lbRuta;
                            }
                            else if (lbRuta.Contains("G:"))
                            {
                                lbRuta = lbRuta.Substring(lbRuta.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                                lbRuta = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_G"].ToString() + lbRuta;
                            }
                            context.Session["Ruta"] = lbRuta;
                            scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                            scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::DocumentosVITAL :: " + lbRuta + " :: " + lbDocumento);
                        }
                        else if (System.IO.Directory.Exists(filePath))
                        {
                            context.Session["Ruta"] = filePath;
                            scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL::filePath" + filePath + "::" + lbDocumento);
                        }
                        else if (filePath.Contains("radicacionesANLA"))
                        {
                            DescargarDocumento(filePath, lbDocumento, context);
                            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL::radicacionesANLA::" + filePath + "::" + lbDocumento);
                            scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                            scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                        }
                        else if (filePath.Contains("origen=seg") || filePath.Contains("origen=vital"))
                        {
                            filePath = filePath.Replace("=seg", "=vital");
                            scrRespuesta = filePath;
                            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL::filePath::La Ruta de los archivos no existe." + filePath + "::" + lbDocumento);
                        }
                        else if (lbRuta.Contains("http://"))
                        {
                            lbRuta = lbRuta.Replace("=seg", "=vital");
                            scrRespuesta = lbRuta;
                            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL::filePath::La Ruta de los archivos no existe." + filePath + "::" + lbDocumento);

                        }
                    }
                    else
                    {
                        if (System.IO.Directory.Exists(lbPath))
                        {
                            string[] ListaArchivos = null;
                            string nombreArchivo = string.Empty;
                            string rutaArchivo = string.Empty;
                            bool encontroArhcivo = false;
                            ListaArchivos = System.IO.Directory.GetFiles(lbPath);
                            foreach (var item in ListaArchivos)
                            {
                                nombreArchivo = filePath.Remove(filePath.LastIndexOf("."), (filePath.Length - filePath.LastIndexOf(".")));
                                if (item.Contains(nombreArchivo))
                                {
                                    rutaArchivo = item;
                                    if (System.IO.File.Exists(rutaArchivo))
                                    {
                                        context.Session["Ruta"] = rutaArchivo;
                                        context.Session["DatoRadicacion"] = null;
                                        scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
                                        scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
                                        encontroArhcivo = true;
                                        break;
                                    }
                                }
                            }
                            if (encontroArhcivo == false)
                            {
                                scrRespuesta = "ERROR: La Ruta de los archivos no existe.";
                                SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "::VITAL::PATH_DOCUMENTO::La Ruta de los archivos no existe." + lbPath + "::" + lbDocumento);
                            }
                        }

                    }
                }
                else
                {
                    scrRespuesta = "ERROR: El directorio solicitado no esta disponible en el formato admitido según políticas de publicación, para su consulta acercarse a la Entidad." + lbRuta;
                    SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + "lbRuta::" + lbRuta + "::" + lbDocumento);
                }
            }
            return serializer.Serialize(scrRespuesta);

        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.aspx" + ex.ToString());
            return serializer.Serialize("ERROR: Excepcion controlada al intentar Buscar el documento.");
        }
    }
    private static string DescargarDocumento(string fullPath, string nombre, HttpContext context)
    {
        try
        {
            string scrRespuesta = string.Empty;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            context.Session["ubicacionCP"] = fullPath;
            context.Session["nombreCp"] = nombre;
            context.Session["DatoRadicacion"] = null;
            context.Session["Ruta"] = null;
            scrRespuesta = context.Request.Url.AbsoluteUri.ToString().Replace("ReporteTramiteCPDetalle.aspx", "DescargarDocumentos.aspx");
            scrRespuesta = scrRespuesta.Substring(0, scrRespuesta.LastIndexOf(".aspx")) + ".aspx";
            return ser.Serialize(scrRespuesta);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ReporteTramiteCPDetalle.apsx" + ex.ToString());
            return "ERROR: Descargando Documento";
        }
    }
    #endregion

    #region ConsultaSILAMC

    private static Dictionary<string, object> ConsultaSILAMC()
    {
        Dictionary<string, object> row = new Dictionary<string, object>();
        try
        {
            ExpedienteSila _expedienteSila = new ExpedienteSila();
            List<ExpedienteSilaEntity> listaExpedienteDetalle = new List<ExpedienteSilaEntity>();
            listaExpedienteDetalle = _expedienteSila.ConsultarDetalleExpedienteSilaMC(exp_id.ToString());
            foreach (ExpedienteSilaEntity _expedienteIdentity in listaExpedienteDetalle)
            {
                numSilpa = _expedienteIdentity.str_exp_numero_vital;
                row.Add("lblnumeroExpediente", _expedienteIdentity.exp_id);
                row.Add("lblNombreProyectoValue", _expedienteIdentity.str_expediente_nombre);
                row.Add("lblDescripcionProyectoValue", _expedienteIdentity.str_exp_descripcion);
                row.Add("lblSector", _expedienteIdentity.str_sec_nombre);
                row.Add("lblSolicitanteId", _expedienteIdentity.str_solicitante_id);
                row.Add("lblSolicitante", _expedienteIdentity.str_nombre_solicitante);
                row.Add("lblUbicacion", _expedienteIdentity.str_nombre_ubicacion);
                row.Add("lblCodigoExpediente", _expedienteIdentity.str_exp_codigo);
                row.Add("lblTramiteId", _expedienteIdentity.str_numero_tramite);
                row.Add("lblTramite", _expedienteIdentity.str_nombre_tramite);
                row.Add("lblTramiteEstado", _expedienteIdentity.str_est_nombre);
                row.Add("lblAdmId", _expedienteIdentity.str_numero_adm);
                row.Add("lblAutoridadAmbiental", _expedienteIdentity.str_nombre_autoridad_ambiental);
            }
            row.Add("lstResumenEstado", ListaResumenSolicitudLineaProceso());
            row.Add("lstEtapas", ListaEtapasSilaMc());
            row.Add("lstExpedientesAsociados", ListaExpedientesAsociadosSilaMC());
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosReposicion" + ex.ToString());
            throw new Exception("ConsultaSILA", ex.InnerException);
        }
        return row;
    }

    private static List<string> ListaExpedientesAsociadosSilaMC()
    {
        List<string> row = new List<string>();
        try
        {
            DataTable listaExpedientesAsociados = new DataTable();
            ExpedienteSila _expedienteSila = new ExpedienteSila();
            listaExpedientesAsociados = _expedienteSila.ListarExpedientesAsociadosSilaMC(exp_id.ToString());
            foreach (DataRow fila in listaExpedientesAsociados.Rows)
            {
                row.Add(fila["EXP_CODIGO"].ToString());
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaDocumentosReposicion" + ex.ToString());
            throw new Exception("ListaExpedientesAsociadosSILA", ex.InnerException);
        }
        return row;
    }

    private static List<Etapa> ListaEtapasSilaMc()
    {
        List<Etapa> lstEtapas = new List<Etapa>();
        try
        {
            Listas objGenerico = new Listas();
            DataTable dtListarEtapas = new DataTable();
            ExpedienteSila _expedienteSila = new ExpedienteSila();
            DataSet _temp1 = _expedienteSila.ListarEtapasSilaMC(exp_id.ToString());
            if (_temp1.Tables.Count > 0)
                dtListarEtapas = _temp1.Tables[0];
            foreach (DataRow fila in dtListarEtapas.Rows)
            {
                DataTable dtInstanciasActos = new DataTable();
                dtInstanciasActos = _expedienteSila.ListarInstanciasActosSilaMC(exp_id.ToString(), string.Empty, string.Empty, fila["EXE_ID"].ToString(), "1,2,3,4,5");
                List<DetalleEtapa> lstDetalleEtapa = new List<DetalleEtapa>();
                foreach (DataRow filaIA in dtInstanciasActos.Rows)
                {
                    DetalleEtapa dtEtapa = new DetalleEtapa(
                        filaIA["DIRD_ID"].ToString(),
                        filaIA["EXE_NOMBRE"].ToString(),
                        filaIA["DOC_NUMERO"].ToString(),
                        DateTime.Parse(filaIA["DOC_FECHA"].ToString()).ToShortDateString(),
                        filaIA["DOC_FECHA_NOTIFICACION"].ToString(),
                        filaIA["TIPO_OBJETO"].ToString(),
                        filaIA["DOC_DESCRIPCION"].ToString().Replace('"', ' ').Replace('/', ' ').Replace("'", " "),
                        filaIA["TIPO_OBJETO"].ToString(),
                        filaIA["TAR_ID"].ToString(),
                        filaIA["TAAD_ID"].ToString(),
                        "SILAMC"
                        );
                    lstDetalleEtapa.Add(dtEtapa);
                }
                lstEtapas.Add(new Etapa(
                    int.Parse(fila["ETA_ID"].ToString()),
                    fila["EXE_NOMBRE"].ToString(),
                    fila["EXE_ID"].ToString(),
                    lstDetalleEtapa
                    ));
            }
            return lstEtapas;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::ListaExpedientesAsociados" + ex.ToString());
            throw new Exception("ListaEtapasSILA", ex.InnerException);
        }
    }
    #endregion

    #region ConsultaEIA
    private static Dictionary<string, object> ConsultaEIA(string numeroExpediente)
    {
        Dictionary<string, object> row = new Dictionary<string, object>();
        ConsultaPublica _consultaPublica = new ConsultaPublica();
        DataTable infExpedienteCP = _consultaPublica.ConsultarExpediente(numeroExpediente);
        if (infExpedienteCP.Rows.Count > 0)
        {

            string tempNombreProyecto = string.Empty;
            if (infExpedienteCP.Rows[0]["NOMBRE_PROYECTO"].ToString().Length > 0)
                tempNombreProyecto = infExpedienteCP.Rows[0]["NOMBRE_PROYECTO"].ToString();
            else if (infExpedienteCP.Rows[0]["nombre_expediente"].ToString().Length > 0)
                tempNombreProyecto = infExpedienteCP.Rows[0]["nombre_expediente"].ToString();

            row.Add("lblnumeroExpediente", numeroExpediente);
            row.Add("lblNombreProyectoValue", tempNombreProyecto);
            row.Add("lblDescripcionProyectoValue", infExpedienteCP.Rows[0]["DESCRIPCION_EXPEDIENTE"].ToString());
            row.Add("lblSector", infExpedienteCP.Rows[0]["SEC_NOMBRE"].ToString());
            row.Add("lblSolicitante", infExpedienteCP.Rows[0]["NOMBRE_COMPLETO"].ToString());
            row.Add("lblCorreoElectronicoSolicitante", string.Empty);
            row.Add("lblUbicacion", infExpedienteCP.Rows[0]["MUNICIPIO"].ToString() + "-" + infExpedienteCP.Rows[0]["DEPARTAMENTO"].ToString());
            row.Add("lblCodigoExpediente", infExpedienteCP.Rows[0]["EXPEDIENTE"].ToString());
            row.Add("lblTramite", infExpedienteCP.Rows[0]["TRA_NOMBRE"].ToString());
            row.Add("lblAutoridadAmbiental", infExpedienteCP.Rows[0]["AUT_NOMBRE"].ToString());

        }
        else
        {
            NumeroSilpaDalc numSilpa = new NumeroSilpaDalc();
            DataTable infExpediente = numSilpa.ConsultarExpediente(numeroExpediente);
            if (infExpediente.Rows.Count > 0)
            {
                row.Add("lblnumeroExpediente", numeroExpediente);
                row.Add("lblNombreProyectoValue", infExpediente.Rows[0]["NOMBRE_EXPEDIENTE"].ToString());
                row.Add("lblDescripcionProyectoValue", infExpediente.Rows[0]["DESCRIPCION_EXPEDIENTE"].ToString());
                row.Add("lblSector", infExpediente.Rows[0]["SECTOR"].ToString());
                row.Add("lblSolicitante", infExpediente.Rows[0]["NOMBRE_SOLICITANTE"].ToString());
                row.Add("lblCorreoElectronicoSolicitante", infExpediente.Rows[0]["CORREO_SOLICITANTE"].ToString());
                row.Add("lblUbicacion", infExpediente.Rows[0]["UBICACION"].ToString());
                row.Add("lblCodigoExpediente", infExpediente.Rows[0]["CODIGO_EXPEDIENTE"].ToString());
                row.Add("lblTramite", string.Empty);
            }
            else
            {
                DataTable infExpediente2 = numSilpa.ConsultarExpedienteXCodExo(numeroExpediente);
                if (infExpediente2.Rows.Count > 0)
                {
                    row.Add("lblNombreProyectoValue", infExpediente2.Rows[0]["NOMBRE_EXPEDIENTE"].ToString());
                    row.Add("lblDescripcionProyectoValue", infExpediente2.Rows[0]["DESCRIPCION_EXPEDIENTE"].ToString());
                    row.Add("lblSector", infExpediente2.Rows[0]["SECTOR"].ToString());
                    row.Add("lblSolicitante", infExpediente2.Rows[0]["NOMBRE_SOLICITANTE"].ToString());
                    row.Add("lblCorreoElectronicoSolicitante", infExpediente2.Rows[0]["CORREO_SOLICITANTE"].ToString());
                    row.Add("lblUbicacion", infExpediente2.Rows[0]["UBICACION"].ToString());
                    row.Add("lblCodigoExpediente", infExpediente2.Rows[0]["CODIGO_EXPEDIENTE"].ToString());
                    row.Add("lblTramite", string.Empty);
                }
            }
        }
        return row;
    }
    private static string DetalleEIA(string numeroExpediente, string origen)
    {
        try
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            ConsultaPublica _consultaPublica = new ConsultaPublica();
            DataTable infExpedienteCP = _consultaPublica.ConsultarExpedienteEIA(exp_id.ToString());

            foreach (DataRow fila in infExpedienteCP.Rows)
            {
                row.Add("lstEIA", '0');
                row.Add("lblTitulo", fila["SOL_IDPROCESSINSTANCE"].ToString());
                row.Add("lblSolicitud", fila["SOL_NUM_SILPA"].ToString());
                row.Add("lblTramiteDescripcion", fila["NOMBRE_TRAMITE"].ToString());
                row.Add("lblTramite", fila["TRA_NOMBRE"].ToString());
                row.Add("lblAutoridad", fila["AUT_NOMBRE"].ToString());
                row.Add("lblSector", fila["SEC_NOMBRE"].ToString());
                row.Add("lblNombreDocumento", fila["NOMBRE_DOCUMENTO"].ToString());
                row.Add("lblFechaCreacion", fila["TAR_FECHA_CREACION"].ToString());
                row.Add("lblFechaFinalizacion", fila["TAR_FECHA_FINALIZACION"].ToString());
                row.Add("lblSolicitante", fila["NOMBRE_SOLICITANTE"].ToString());
                row.Add("lblPath", fila["PATH_DOCUMENTO"].ToString());
                row.Add("lblUbicacionProyecto", fila["UBICACION"].ToString());
            }
            rows.Add(row);
            return serializer.Serialize(rows);
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "ConsultaPublica::DetallePublicacion" + ex.ToString());
            throw new Exception("DetalleExpediente", ex.InnerException);
        }
    }
    #endregion

    #region clases parciales para definir estructuras de los documentos
    /// <summary>
    /// 
    /// </summary>
    public partial class Documento
    {
        private string ruta;
        public string lblSol_Numero { get; set; }
        public string lblDocumento { get; set; }
        public string lblIdParticipant { get; set; }
        public string lblSolFechaCreacion { get; set; }
        public string lblEntryData { get; set; }
        public string lblIdEntryData { get; set; }
        public string lblEntryDataType { get; set; }
        public string lblIdActivity { get; set; }
        public string lblIdProcessInstance { get; set; }
        public string lblRuta { get { return ruta; } set { ruta = setearRuta(value); } }
        public string lblURLDataView { get; set; }
        public string lblIdRelated { get; set; }
        public string lblExpediente { get; set; }
        public string lblPathDocumento { get; set; }

        public string setearRuta(string strRutaArchivo)
        {
            if (!strRutaArchivo.Contains("http") && lblEntryDataType == "documentos")
            {
                return strRutaArchivo.Replace(@"\", @"/");
            }
            else if (lblEntryDataType == null && !strRutaArchivo.Contains("http") && lblIdProcessInstance == null)
            {
                return strRutaArchivo.Replace(@"\", @"/");
            }
            return strRutaArchivo;
        }
    };

    

    
    /// <summary>
    /// 
    /// </summary>
    public partial class Etapa
    {
        public Etapa(int idEtapa, string nombreEtapa, string nombreExeId, List<DetalleEtapa> dtEtapa)
        {
            IdEtapa = idEtapa;
            NombreEtapa = nombreEtapa;
            NombreExeId = nombreExeId;
            DtEtapa = dtEtapa;
        }

        public int IdEtapa { get; private set; }
        public string NombreEtapa { get; private set; }
        public string NombreExeId { get; private set; }
        public List<DetalleEtapa> DtEtapa { get; private set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public partial class DetalleEtapa
    {
        public DetalleEtapa(string directorioID, string etapaNombre, string numero, string fecha, string fechaNotificacion, string tipoObjeto, string descripcion, string documento, string tareaID, string tipoActoAdministrativo, string origen)
        {
            DirectorioID = directorioID;
            EtapaNombre = etapaNombre;
            Numero = numero;
            Fecha = fecha;
            FechaNotificacion = fechaNotificacion;
            TipoObjeto = tipoObjeto;
            Descripcion = descripcion;
            Documento = documento;
            TareaID = tareaID;
            TipoActoAdministrativo = tipoActoAdministrativo;
            Origen = origen;
        }
        public string EtapaNombre { get; private set; }
        public string DirectorioID { get; private set; }
        public string Numero { get; private set; }
        public string Fecha { get; private set; }
        public string FechaNotificacion { get; private set; }
        public string TipoObjeto { get; private set; }
        public string Descripcion { get; private set; }
        public string Documento { get; private set; }
        public string TareaID { get; private set; }
        public string TipoActoAdministrativo { get; private set; }
        public string Origen { get; private set; }

    }
    public partial class ResumenTramite
    {
        public ResumenTramite(string Id, string Name, string Nivel, string Id_Padre, string Ejecutada)
        {
            id = Id;
            name = Name;
            nivel = Nivel;
            id_Padre = Id_Padre;
            ejecutada = Ejecutada;
        }

        public ResumenTramite()
        {
            // TODO: Complete member initialization
        }

        public string id { get; set; }
        public string name { get; set; }
        public string nivel { get; set; }
        public string id_Padre { get; set; }
        public string ejecutada { get; set; }

    }

    public partial class Actividad
    {
        public string lblIdProcessInstance { get; set; } //IDPROCESSINSTANCE
        public string lblIdActivity { get; set; } //IDACTIVITY
        public string lblName { get; set; } // NAME
        public string lblusuario { get; set; } //USUARIO
        public string lblStarttime { get; set; }
        public string lblendtime { get; set; }
        public string lblNombreUsuario { get; set; }

    };

    public partial class ActividadCondicion
    {
        public string lblNumeroVital { get; set; }                  //NUMERO_VITAL
        public string lblIdExpediente { get; set; }                 //ID_EXPEDIENTE
        public string lblIdDocumento { get; set; }                  // ID_DOCUMENTO
        public string lblFechaCreacion { get; set; }                //FECHA_CREACION
        public string lblCodigoCondicion { get; set; }              //CODIGO_CONDICION
        public string lblCodigoCondicionBPM { get; set; }           //CODIGO_CONDICION_BPM
        public string lblDescripcionDocumento { get; set; }         //DESCRIPCION_DOCUMENTO
        public string lblPathDocumento { get; set; }                //PATH_DOCUMENTO
        public string lblNombreDocumento { get; set; }              //NOMBRE_DOCUMENTO
        public string lblNombreCondicion { get; set; }                       //NAME
        public string lblFechaNotificacion { get; set; }

        public string lblEncargado { get; set; }                       //NAME
    };
    private class TemplateDocumento : ITemplate
    {
        protected HtmlGenericControl a;
        private string colname;
        public TemplateDocumento(string listaDocumentos)
        {
            colname = listaDocumentos;
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            HtmlGenericControl img = new HtmlGenericControl("img");
            img.Attributes.Add("src", "../App_Themes/Img/documentos.png");
            a = new HtmlGenericControl("a");
            a.Attributes.Add("title", "Ver documentos");
            a.Attributes.Add("class", "label-xsm");
            a.Attributes.Add("onclick", "MostrarDocumentos(Text='<%# Eval(" + colname + ") %>')");
            a.Controls.Add(img);
            container.Controls.Add(a);
        }
    }

    public class events {
        public string date { get; set; }
        public string content { get; set; }
    }
    #endregion

}
