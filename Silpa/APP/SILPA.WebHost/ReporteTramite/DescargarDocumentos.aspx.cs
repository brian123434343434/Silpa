using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Expedientes;
using SoftManagement.Log;
using System.Text;


public partial class ReporteTramite_DescargarDocumentos : System.Web.UI.Page
{
    protected string strRuta;
    protected static string prevPage;
    public DataTable dtDatos { get { return (DataTable)ViewState["dtDatos"]; } set { ViewState["dtDatos"] = value; } }

    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
        if (!this.IsPostBack)
            procesarQueryString();
    }


    /// <summary>
    /// Obtención de la ruta de los documentos correpondientes al tramite
    /// </summary>
    protected void procesarQueryString()
    {
        if (Session["DatoRadicacion"] != null)
        {
            cargarDocumentosRadicacionTotal();
        }

        else if (Session["Ruta"] != null)
        {
            strRuta = Session["Ruta"].ToString();
            cargarDocumentosRadicacion();
            this.CargarTriggersGrillaDocumentos();
        }
        /////*************************************************************************************************************************\\\\\\\\\\\
        /////Cambio inlcuido para descargar documentos con origen SILA y SILAMC para la implemenacion de busqueda tramites publicos   \\\\\\\\\\\
        /////***************************************************************************************************************************\\\\\\\\\\\
        else if (Session["ubicacionCP"] != null && Session["nombreCp"] != null)
        {
            string ubicacionCP = Session["ubicacionCP"].ToString();
            string nombreCp = Session["nombreCp"].ToString();
            fnDescarga(ubicacionCP, nombreCp);
        }
        else if (Session["dirIdSila"] != null && Session["tareaIDSila"] != null)
        {
            string dirIdSila = Session["dirIdSila"].ToString();
            string tareaIDSila = Session["tareaIDSila"].ToString();
            CargarDocumentosSila(dirIdSila, tareaIDSila);
        }
        else if (Session["int_row"] != null)
        {
            string int_row = Session["int_row"].ToString();
            CargarDocumentosSila(int_row, string.Empty);
        }
        else if (Session["dirIdSilaMC"] != null && Session["tareaIDSilaMC"] != null)
        {
            string dirIdSila = Session["dirIdSilaMC"].ToString();
            string tareaIDSila = Session["tareaIDSilaMC"].ToString();
            CargarDocumentosSilaMC(dirIdSila, tareaIDSila);
        }
        else if (Session["int_row_SilaMC"] != null)
        {
            string int_row = Session["int_row_SilaMC"].ToString();
            CargarDocumentosSilaMC(int_row, string.Empty);
        }
        else if (Session["vitalFile"] != null)
        {
            string int_row = Session["vitalFile"].ToString();
            Mensaje.MostrarMensaje(this, "NO se encontro la lista de documentos. Vital:" + int_row.ToString());
        }
        else
        {
            Mensaje.MostrarMensaje(this, "Directorio no disponible, no ha sido posible obtener la lista de documentos.");
            SMLog.Escribir(Severidad.Critico, "DescargarDocumentos::procesarQueryString");
        }
    }

    /// <summary>
    /// Método que obtiene el listado de documentos adjuntos en la radicación
    /// </summary>
    /// <param name="pathDocumento"></param> 
    public void cargarDocumentosRadicacionTotal()
    {
        if (Session["DatoRadicacion"] == null)
        {
            return;
        }
        try
        {
            RadicacionDocumento RadicacionDoc = new RadicacionDocumento();
            DataTable tmpDato = new DataTable();
            tmpDato = (DataTable)Session["DatoRadicacion"];

            dtDatos = new DataTable();
            dtDatos.Columns.Add("NombreArchivo");
            dtDatos.Columns.Add("Ubicacion");
            DataRow drwFila;
            dtDatos.Clear();

            for (int i = 0; i < tmpDato.Rows.Count; i++)
            {
                RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
                string pathDocumento = tmpDato.DefaultView[i][5].ToString();
                string[] ListaArchivos = null;

                if (pathDocumento != string.Empty)
                {
                    SMLog.Escribir(Severidad.Critico, "DescargaDocumentos::cargarDocumentosRadicacionTotal::" + @pathDocumento);
                    if (pathDocumento.Contains("F:") && pathDocumento.Contains("VitalFiletraffic"))
                    {
                        pathDocumento = pathDocumento.Substring(pathDocumento.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                        pathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL"].ToString() + pathDocumento;
                    }
                    else if (pathDocumento.Contains("H:") && pathDocumento.Contains("VitalFiletraffic"))
                    {
                        pathDocumento = pathDocumento.Substring(pathDocumento.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                        pathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_H"].ToString() + pathDocumento;
                    }
                    else if (pathDocumento.Contains("G:") && pathDocumento.Contains("VitalFiletraffic"))
                    {
                        pathDocumento = pathDocumento.Substring(pathDocumento.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                        pathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_G"].ToString() + pathDocumento;
                    }
                    // pathDocumento = @"\\\\SILPA\\VitalFiletrafficH$\20160816091639\55203\11323";
                    if (System.IO.Directory.Exists(pathDocumento))
                    {
                        ListaArchivos = System.IO.Directory.GetFiles(pathDocumento);
                        foreach (var item in ListaArchivos)
                        {
                            drwFila = dtDatos.NewRow();
                            drwFila["NombreArchivo"] = item.Substring(item.LastIndexOf("\\") + 1, item.Length - item.LastIndexOf("\\") - 1);
                            drwFila["Ubicacion"] = pathDocumento;// item.Substring(item.LastIndexOf("VitalFiletraffic")).Replace("VitalFiletraffic", "");
                            dtDatos.Rows.Add(drwFila);
                        }
                    }
                    else
                    {
                        Mensaje.MostrarMensaje(this, "Directorio no disponible.");
                    }
                }
            }
            this.grdVerDocumentos.DataSource = dtDatos;
            this.grdVerDocumentos.DataBind();

            if (dtDatos.Rows.Count == 0)
            {
                Mensaje.MostrarMensaje(this, "Documento no disponible.");
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + "DescargaDocumentos::" + ex.ToString());
            Mensaje.MostrarMensaje(this, "Documento no disponible.");
        }
    }

    public void cargarDocumentosRadicacion()
    {
        try
        {
            if (Session["Ruta"] == null)
            {
                return;
            }
            string strpathDocumento = Session["Ruta"].ToString();
            SMLog.Escribir(Severidad.Critico, "DescargaDocumentos::cargarDocumentosRadicacion::" + @strpathDocumento);

            if (strpathDocumento.Contains("vitalfiletraffic"))
            {
                if (strpathDocumento.Contains("F:"))
                {
                    strpathDocumento = strpathDocumento.Substring(strpathDocumento.LastIndexOf("vitalfiletraffic\\")).Replace("vitalfiletraffic\\", "");
                    strpathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL"].ToString() + strpathDocumento;
                }
                else if (strpathDocumento.Contains("H:"))
                {
                    strpathDocumento = strpathDocumento.Substring(strpathDocumento.LastIndexOf("vitalfiletraffic\\")).Replace("vitalfiletraffic\\", "");
                    strpathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_H"].ToString() + strpathDocumento;
                }
                else if (strpathDocumento.Contains("G:"))
                {
                    strpathDocumento = strpathDocumento.Substring(strpathDocumento.LastIndexOf("vitalfiletraffic\\")).Replace("vitalfiletraffic\\", "");
                    strpathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_G"].ToString() + strpathDocumento;
                }
                strpathDocumento = strpathDocumento.Replace("\\", "//");
                SMLog.Escribir(Severidad.Critico, "DescargaDocumentos::cargarDocumentosRadicacionTransformado1v::" + strpathDocumento);
            }


            if (strpathDocumento.Contains("VitalFiletraffic"))
            {
                if (strpathDocumento.Contains("F:"))
                {
                    strpathDocumento = strpathDocumento.Substring(strpathDocumento.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "");
                    strpathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL"].ToString() + strpathDocumento;
                }
                else if (strpathDocumento.Contains("H:"))
                {
                    strpathDocumento = strpathDocumento.Substring(strpathDocumento.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "");
                    strpathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_H"].ToString() + strpathDocumento;
                }
                else if (strpathDocumento.Contains("G:"))
                {
                    strpathDocumento = strpathDocumento.Substring(strpathDocumento.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "");
                    strpathDocumento = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_G"].ToString() + strpathDocumento;
                }
                strpathDocumento = strpathDocumento.Replace("\\", "//");
                SMLog.Escribir(Severidad.Critico, "DescargaDocumentos::cargarDocumentosRadicacionTransformado2v::" + strpathDocumento);
            }

            SMLog.Escribir(Severidad.Critico, "DescargaDocumentos::cargarDocumentosRadicacion3v::" + strpathDocumento);
            if (System.IO.Directory.Exists(strpathDocumento))
            {
                //string[] ListaArchivos = System.IO.Directory.GetFiles(strpathDocumento.Replace("vitalfiletraffic\\", ""));
                string[] ListaArchivos = System.IO.Directory.GetFiles(strpathDocumento);
                SMLog.Escribir(Severidad.Critico, "DescargaDocumentos::cargarDocumentosRadicacionListado::" + strpathDocumento);
                dtDatos = new DataTable();
                dtDatos.Columns.Add("NombreArchivo");
                dtDatos.Columns.Add("Ubicacion");
                DataRow drwFila;
                dtDatos.Clear();

                //Cargar lista de documentos al gridview
                foreach (string str in ListaArchivos)
                {
                    drwFila = dtDatos.NewRow();
                    drwFila["NombreArchivo"] = System.IO.Path.GetFileName(str);
                    // drwFila["Ubicacion"] = strpathDocumento;                    
                    drwFila["Ubicacion"] = str;
                    dtDatos.Rows.Add(drwFila);
                }

                if (Session["process"] != null)
                {
                    if (File.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "_" + Session["process"] + ".rtf"))
                    {
                        drwFila = dtDatos.NewRow();
                        drwFila["NombreArchivo"] = "_" + Session["process"] + ".rtf";
                        drwFila["Ubicacion"] = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "_" + Session["process"] + ".rtf";
                        dtDatos.Rows.Add(drwFila);
                    }
                }

                if (dtDatos != null)
                {
                    if (dtDatos.Rows.Count > 0)
                    {
                        this.grdVerDocumentos.DataSource = dtDatos;
                        this.grdVerDocumentos.DataBind();

                    }
                    else
                    {
                        Mensaje.MostrarMensaje(this, "Los documentos asociados no se encuentran disponibles. Por favor comuniquese con la Entidad.");
                        SMLog.Escribir(Severidad.Critico, this.Page.Title + "cargarDocumentosRadicacion::No se encontraron documentos asociados al tramite");
                    }
                }
            }
            else
            {
                if (strpathDocumento == "")
                    if (Session["process"] != null)
                        if (File.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "_" + Session["process"] + ".rtf"))
                            strpathDocumento = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "_" + Session["process"] + ".rtf";


                if (System.IO.File.Exists(strpathDocumento))
                {
                    DataTable dtDatos = new DataTable();
                    dtDatos.Columns.Add("NombreArchivo");
                    dtDatos.Columns.Add("Ubicacion");
                    DataRow drwFila;
                    dtDatos.Clear();

                    drwFila = dtDatos.NewRow();
                    drwFila["NombreArchivo"] = System.IO.Path.GetFileName(strpathDocumento);
                    drwFila["Ubicacion"] = Path.GetDirectoryName(strpathDocumento) + "//";// strpathDocumento;
                    dtDatos.Rows.Add(drwFila);

                    if (dtDatos != null)
                    {
                        if (dtDatos.Rows.Count > 0)
                        {
                            this.grdVerDocumentos.DataSource = dtDatos;
                            this.grdVerDocumentos.DataBind();

                        }
                        else
                        {
                            Mensaje.MostrarMensaje(this, "Los documentos asociados no se encuentran disponibles. Por favor comuniquese con la Entidad.");
                            SMLog.Escribir(Severidad.Critico, this.Page.Title + "cargarDocumentosRadicacion::No se encontraron documentos asociados al tramite");
                        }
                    }
                }
                else if (strpathDocumento.Contains("http://"))
                {

                    foreach (string filename in Directory.GetFiles(@strpathDocumento, "*.*"))
                    {
                        DataTable dtDatos = new DataTable();
                        dtDatos.Columns.Add("NombreArchivo");
                        dtDatos.Columns.Add("Ubicacion");
                        DataRow drwFila;
                        dtDatos.Clear();

                        drwFila = dtDatos.NewRow();
                        drwFila["NombreArchivo"] = filename;
                        drwFila["Ubicacion"] = Path.GetDirectoryName(strpathDocumento) + "//";// strpathDocumento;
                        dtDatos.Rows.Add(drwFila);

                        if (dtDatos != null)
                        {
                            if (dtDatos.Rows.Count > 0)
                            {
                                this.grdVerDocumentos.DataSource = dtDatos;
                                this.grdVerDocumentos.DataBind();

                            }
                            else
                            {
                                Mensaje.MostrarMensaje(this, "Los documentos asociados no se encuentran disponibles. Por favor comuniquese con la Entidad.");
                                SMLog.Escribir(Severidad.Critico, this.Page.Title + "cargarDocumentosRadicacion::No se encontraron documentos asociados al tramite");
                            }
                        }
                    }
                    SMLog.Escribir(Severidad.Critico, this.Page.Title + "::VITAL::lbPath::" + strpathDocumento);
                }
                else
                {
                    Mensaje.MostrarMensaje(this, "Los documentos asociados no se encuentran disponibles. Por favor comuniquese con la Entidad.");
                    SMLog.Escribir(Severidad.Critico, this.Page.Title + "cargarDocumentosRadicacion::No se encontro la ruta de los documentos");
                }
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + "cargarDocumentosRadicacion::" + ex.ToString());
            Mensaje.MostrarMensaje(this, "Directorio no disponible.");
        }
    }

    private void CargarDocumentosSila(string dirIdSila, string tareaIDSila)
    {
        try
        {
            DataSet ds = new DataSet();
            ExpedienteSila exSila = new ExpedienteSila();
            ds = exSila.listarDocumentosDS(int.Parse(dirIdSila));

            if (ds.Tables.Count > 0)
            {
                DataTable dtDatos = new DataTable();
                dtDatos.Columns.Add("NombreArchivo");
                dtDatos.Columns.Add("Ubicacion");
                DataRow drwFila;
                dtDatos.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    drwFila = dtDatos.NewRow();
                    drwFila["NombreArchivo"] = dr["DOC_ARCHIVO"].ToString();
                    drwFila["Ubicacion"] = dr["DIRD_ID"].ToString();
                    dtDatos.Rows.Add(drwFila);
                }
                this.grdVerDocumentos.DataSource = dtDatos;
                this.grdVerDocumentos.DataBind();
                Session["int_row"] = null;
                Session["dirIdSila"] = null;
                Session["tareaIDSila"] = null;
            }
            else
            {
                Mensaje.MostrarMensaje(this, "Documento no disponible, se debe validar con la Entidad si el documento se encuentra Notificado.");
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "cargarDocumentosSila-ConsultaPublica:" + ex.ToString());
            throw new Exception(ex.Message);
        }

    }

    private void CargarDocumentosSilaMC(string dirIdSila, string tareaIDSila)
    {
        try
        {
            DataSet ds = new DataSet();
            ExpedienteSila exSila = new ExpedienteSila();
            ds = exSila.ListarDocumentosDSSilaMC(int.Parse(dirIdSila));

            if (ds.Tables.Count > 0)
            {
                DataTable dtDatos = new DataTable();
                dtDatos.Columns.Add("NombreArchivo");
                dtDatos.Columns.Add("Ubicacion");
                DataRow drwFila;
                dtDatos.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    drwFila = dtDatos.NewRow();
                    drwFila["NombreArchivo"] = dr["DOC_ARCHIVO"].ToString();
                    drwFila["Ubicacion"] = dr["DIRD_ID"].ToString();
                    dtDatos.Rows.Add(drwFila);
                }
                this.grdVerDocumentos.DataSource = dtDatos;
                this.grdVerDocumentos.DataBind();
                Session["int_row"] = null;
                Session["dirIdSila"] = null;
                Session["tareaIDSila"] = null;
            }
            else
            {
                Mensaje.MostrarMensaje(this, "Documento no disponible, se debe validar con la Entidad si el documento se encuentra Notificado.");
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, "DescargarDocumentos-CargarDocumentosSilaMC:" + ex.ToString());
            throw new Exception(ex.Message);
        }

    }


    /// <summary>
    /// Eventos del gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdVerDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Descargar")
            {
                if (e.CommandArgument != null)
                {
                    int _indice = Convert.ToInt32(e.CommandArgument);
                    DataKey dk = this.grdVerDocumentos.DataKeys[_indice];
                    string _ubicacion = dk.Values[1].ToString();
                    string _nombre = dk.Values[0].ToString();
                    SMLog.Escribir(Severidad.Critico, "DescargarDocumentos::grdVerDocumentos:: " + _ubicacion + " :: " + _nombre);

                    if (_ubicacion.Contains("VitalFiletraffic"))
                    {
                        if (_ubicacion.Contains("F:"))
                        {
                            _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("\\", "//");
                            _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL"].ToString() + _ubicacion;
                        }
                        else if (_ubicacion.Contains("H:"))
                        {
                            _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("\\", "//");
                            _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_H"].ToString() + _ubicacion;
                        }
                        else if (_ubicacion.Contains("G:"))
                        {
                            _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("\\", "//");
                            _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_G"].ToString() + _ubicacion;
                        }

                        System.IO.FileInfo targetFile = new System.IO.FileInfo(_ubicacion);
                        SMLog.Escribir(Severidad.Critico, "DescargarDocumentos::targetFile:: " + targetFile.FullName);

                        this.Response.Clear();
                        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                        this.Response.ContentType = "application/octet-stream";
                        this.Response.ContentType = "application/base64";
                        this.Response.WriteFile(targetFile.FullName);
                        //this.Response.WriteFile(_nombre);
                        this.Response.Flush();
                        this.Response.End();
                    }
                    else
                    {
                        System.IO.FileInfo targetFile = new System.IO.FileInfo(Server.MapPath(@"~/documentos/" + _nombre)); //FileInfo(this.oConfig.DirectorioFisicoBase + "\\" + this.oConfig.CarpetaTxtFiles + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "_SIFIC_SICOM" + ".txt");
                        SMLog.Escribir(Severidad.Critico, "DescargarDocumentos::FileInfo::  " + targetFile);
                        if (System.IO.File.Exists(targetFile.ToString()))
                        {
                            this.Response.Clear();
                            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                            this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                            this.Response.ContentType = "application/octet-stream";
                            this.Response.WriteFile(targetFile.FullName);
                        }
                        else
                        {
                            Mensaje.MostrarMensaje(this, "Documento no disponible.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + "grdVerDocumentos_RowCommand::" + ex.ToString());
            Mensaje.MostrarMensaje(this, "Documento no disponible.");
        }
    }

    private void fnDescarga(string _ubicacion, string _nombre)
    {
        try
        {
            SMLog.Escribir(Severidad.Informativo, "DescargaDocumentos::ConsultaPublica" + _ubicacion + _nombre);
            if (_ubicacion.Contains("VitalFiletraffic"))
            {
                if (_ubicacion.Contains("F:"))
                {
                    _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                    _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL"].ToString() + _ubicacion;
                }
                else if (_ubicacion.Contains("H:"))
                {
                    _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                    _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_H"].ToString() + _ubicacion;
                }
                else if (_ubicacion.Contains("G:"))
                {
                    _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("//", "\\");
                    _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_G"].ToString() + _ubicacion;
                }
                SMLog.Escribir(Severidad.Critico, "fnDescarga::VitalFiletraffic::" + _ubicacion + _nombre);
                if (System.IO.File.Exists(_ubicacion))
                {
                    System.IO.FileInfo targetFile = new System.IO.FileInfo(_ubicacion);

                    this.Response.Clear();
                    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                    this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                    this.Response.ContentType = "application/octet-stream";
                    //this.Response.ContentType = "application/base64";
                    // this.Response.WriteFile(targetFile.FullName);
                    this.Response.TransmitFile(targetFile.FullName);
                    this.Response.Flush();
                }
                else
                    this.lblMensajeError.Text = "Documento no disponible.";
                // this.Response.WriteFile(_nombre);
            }
            else if (_ubicacion.Contains(ConfigurationManager.AppSettings["DocsSILA"].ToString()) || _ubicacion.Contains(ConfigurationManager.AppSettings["DocsSILAMC"].ToString()))
            {

                if (_ubicacion.Contains(ConfigurationManager.AppSettings["DocsSILAMC"].ToString()))
                {
                    System.IO.FileInfo targetFile = new System.IO.FileInfo(_ubicacion + _nombre);
                    if (System.IO.File.Exists(targetFile.ToString()))
                    {
                        this.Response.Clear();
                        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                        this.Response.ContentType = "application/octet-stream";
                        this.Response.TransmitFile(targetFile.FullName);
                        this.Response.Flush();
                    }
                    else
                    {
                        this.lblMensajeError.Text = "Documento no disponible.";
                        SMLog.Escribir(Severidad.Critico, "fnDescarga::DocsSILAMC::" + _ubicacion + _nombre);
                    }
                }

                else 
                {
                    // FRAMIREZ 27-07-2020
                    // Llamado para obtener url descarga de documento SILA 
                    var urlDescarga = RetornarUrlDescargaDocumentosSila(_nombre);

                    if (string.IsNullOrEmpty(urlDescarga))
                    {
                        this.lblMensajeError.Text = "Url de descarga de documento no disponible.";
                        SMLog.Escribir(Severidad.Critico, "fnDescarga::DocsSILA::" + _nombre);
                        return;
                    }
                    // Se envia la redirección a la página de descarga de documentos de SILA.
                    this.Response.Redirect(urlDescarga, false);

                }
               
            }
            else if (_ubicacion.Contains("radicacionesANLA"))
            {
                if (_nombre != string.Empty && !_ubicacion.EndsWith(_nombre))
                {
                    if (!_ubicacion.Contains("radicacionesANLA"))
                        _ubicacion = _ubicacion + (_ubicacion.EndsWith("\\") || _ubicacion.EndsWith("/") ? "" : "\\") + _nombre;
                }
                _ubicacion = _ubicacion.Replace("F:\\", ConfigurationManager.AppSettings["RUTA_FILE_FOREST"]);
                System.IO.FileInfo toDownload = new System.IO.FileInfo(_ubicacion);
                if (System.IO.File.Exists(toDownload.FullName))
                {
                    HttpContext.Current.Response.Clear();
                    if (_nombre != string.Empty)
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + _nombre);
                    else
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);

                    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.TransmitFile(_ubicacion);
                    HttpContext.Current.Response.End();
                }
                else
                {
                    Mensaje.MostrarMensaje(this, "Documento de correspondencia no existe.");
                    SMLog.Escribir(Severidad.Critico, "fnDescarga::RUTA_FILE_FOREST" + _ubicacion + _nombre);
                }
            }
            else
            {
                System.IO.FileInfo targetFile = new System.IO.FileInfo(Server.MapPath(@"~/documentos/" + _nombre));
                if (System.IO.File.Exists(targetFile.ToString()))
                {
                    this.Response.Clear();
                    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                    this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                    this.Response.ContentType = "application/octet-stream";
                    this.Response.WriteFile(targetFile.FullName);
                    Mensaje.MostrarMensaje(this, "Se ha iniciado la decarga." + _nombre);
                }
                else
                {
                    Mensaje.MostrarMensaje(this, "Documento no disponible." + _nombre);
                    SMLog.Escribir(Severidad.Critico, "fnDescarga::ServerMapPath" + _ubicacion + _nombre);
                }
            }
        }
        catch (Exception ex)
        {
            Mensaje.MostrarMensaje(this, "Excepcion controlada.");
            SMLog.Escribir(Severidad.Critico, "fnDescarga:: " + _ubicacion + _nombre + ex.ToString());
        }
    }


    /// <summary>
    /// Paginación del gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdVerDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdVerDocumentos.DataSource = dtDatos;
        grdVerDocumentos.PageIndex = e.NewPageIndex;
        grdVerDocumentos.DataBind();
        this.CargarTriggersGrillaDocumentos();

    }


    /// <summary>
    /// Cargar los trigger especificos que se requieren de la grilla de notificaciones que no se pueden especificar en diseño de pagina
    /// </summary>
    private void CargarTriggersGrillaDocumentos()
    {
        //ImageButton objImagen = null;
        LinkButton btn = null;
        PostBackTrigger objPostTrigger = null;

        //Verificar que la grilla contenga información
        if (this.grdVerDocumentos.Visible && this.grdVerDocumentos.Rows.Count > 0)
        {
            //Ciclo que adiciona los triggers de la grilla de notificaciones
            foreach (GridViewRow objRowNotificacion in this.grdVerDocumentos.Rows)
            {

                btn = (LinkButton)objRowNotificacion.FindControl("lnkDescargar");

                //Si existe el control y se encuentra visible
                if (btn != null && btn.Visible)
                {
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btn);
                }
            }
        }

    }

    private string RetornarUrlDescargaDocumentosSila(string nombreDocumento) {

        var urlDescarga = string.Empty;
        try
        {
            // FRAMIREZ 27-07-2020
            // Llamado para descarga de docuumento SILA 

            // Recuperar la URL para descarga de documentos
            ParametroEntity _urlDocumentosSila = new ParametroEntity();
            _urlDocumentosSila.IdParametro = -1;
            _urlDocumentosSila.NombreParametro = "URL_DESCARGA_DOCUMENTOS_SILA";
            ParametroDalc parametro = new ParametroDalc();
            parametro.obtenerParametros(ref _urlDocumentosSila);

            urlDescarga = _urlDocumentosSila.Parametro;

            if (!string.IsNullOrEmpty(urlDescarga))
            {
                // Ticks de la hora actual de la solicitud de descarga
                var fechaHoraTick = DateTime.Now.Ticks;

                // Encriptar los valores enviados en la URL
                var docEncript = SILPA.Comun.EnDecript.Encriptar(nombreDocumento);
                var ticksEncript = SILPA.Comun.EnDecript.Encriptar(fechaHoraTick.ToString());

                urlDescarga += "?d=" + docEncript + "&f=" + ticksEncript;    
            }           

            return urlDescarga;

        }
        catch (Exception ex)
        {
            urlDescarga = "";
            SMLog.Escribir(Severidad.Critico, this.Page.Title + "RetornarUrlDescargaDocumentosSila::" + ex.ToString());
            return urlDescarga;
        }    
    }


    protected void btnAtras_Click(object sender, EventArgs e)
    {
        string _respuestaEnvio = "<script language='JavaScript'>" +
               "window.close()</script>";
        Page.RegisterStartupScript("PopupScript", _respuestaEnvio);
    }

    protected void lnkDescargar_Click(object sender, EventArgs e)
    {
        try
        {
            int _indice = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            DataKey dk = this.grdVerDocumentos.DataKeys[_indice];
            string _ubicacion = dk.Values[1].ToString();
            string _nombre = dk.Values[0].ToString();
            SMLog.Escribir(Severidad.Critico, "DescargarDocumentos::grdVerDocumentos:: " + _ubicacion + " :: " + _nombre);

            if (_ubicacion.Contains("VitalFiletraffic"))
            {
                if (_ubicacion.Contains("F:"))
                {
                    _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("\\", "//");
                    _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL"].ToString() + _ubicacion;
                }
                else if (_ubicacion.Contains("H:"))
                {
                    _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("\\", "//");
                    _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_H"].ToString() + _ubicacion;
                }
                else if (_ubicacion.Contains("G:"))
                {
                    _ubicacion = _ubicacion.Substring(_ubicacion.LastIndexOf("VitalFiletraffic\\")).Replace("VitalFiletraffic\\", "").Replace("\\", "//");
                    _ubicacion = ConfigurationManager.AppSettings["RUTA_FILE_TRAFFIC_VIRTUAL_G"].ToString() + _ubicacion;
                }

                if (_ubicacion.LastIndexOf(".") == -1)
                    _ubicacion = _ubicacion + _nombre;

                System.IO.FileInfo targetFile = new System.IO.FileInfo(_ubicacion);
                SMLog.Escribir(Severidad.Critico, "DescargarDocumentos::targetFile:: " + targetFile.FullName);

                this.Response.Clear();
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                this.Response.ContentType = "application/octet-stream";
                this.Response.ContentType = "application/base64";
                this.Response.TransmitFile(targetFile.FullName);
                this.Response.Flush();
            }
            else if (System.IO.Directory.Exists(_ubicacion))
            {
                if (_ubicacion.LastIndexOf(".") == -1)
                    _ubicacion = _ubicacion + _nombre;
                System.IO.FileInfo targetFile = new System.IO.FileInfo(_ubicacion);
                SMLog.Escribir(Severidad.Critico, "DescargarDocumentos::targetFile:: " + targetFile.FullName);
                this.Response.Clear();
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                this.Response.ContentType = "application/octet-stream";
                this.Response.ContentType = "application/base64";
                this.Response.TransmitFile(targetFile.FullName);
                this.Response.Flush();
            }
            else
            {
                System.IO.FileInfo targetFile = new System.IO.FileInfo(Server.MapPath(@"~/documentos/" + _nombre)); //FileInfo(this.oConfig.DirectorioFisicoBase + "\\" + this.oConfig.CarpetaTxtFiles + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "_SIFIC_SICOM" + ".txt");
                SMLog.Escribir(Severidad.Critico, "DescargarDocumentos::FileInfo::  " + targetFile);
                if (System.IO.File.Exists(targetFile.ToString()))
                {
                    this.Response.Clear();
                    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                    this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                    this.Response.ContentType = "application/octet-stream";
                    this.Response.WriteFile(targetFile.FullName);
                }
                else
                {
                    Mensaje.MostrarMensaje(this, "Documento no disponible.");
                }
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, this.Page.Title + "lnkDescargar_Click::" + ex.ToString());
            Mensaje.MostrarMensaje(this, "Documento no disponible.");
        }
    }
}
