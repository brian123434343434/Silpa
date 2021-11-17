using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using SILPA.AccesoDatos.Publicacion;
using SILPA.LogicaNegocio.Publicacion;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;

public partial class Informacion_ConsultaPublicacion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this.Page);
        

        if (!IsPostBack)
        {
            if (DatosSesion.Usuario == string.Empty)
                this.grdPublicaciones.Columns[11].Visible = false;

            CargarTipoTramite();
            CargarAutoridades();
            CargarSectores();
            CargarTipoDocumento();

            txtFechaInicial.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        DateTime? fechaInicial = null;
        DateTime? fechaFinal = null;

        if (txtFechaInicial.Text != string.Empty)
        {
            DateTime fecha;
            if (!DateTime.TryParse(this.txtFechaInicial.Text.Trim(), out fecha))
            {
                Mensaje.MostrarMensaje(this.Page, "El valor del campo Fecha Desde no es válido");
                return;
            }
            fechaInicial = fecha;
        }
        if (txtFechaFinal.Text != string.Empty)
        {
            DateTime fecha;
            if (!DateTime.TryParse(this.txtFechaFinal.Text.Trim(), out fecha))
            {
                Mensaje.MostrarMensaje(this.Page, "El valor del campo Fecha Hasta no es válido");
                return;
            }
            fechaFinal = fecha;
        }

        if (txtFechaFinal.Text != string.Empty && txtFechaInicial.Text != string.Empty)
        {
            if (fechaFinal < fechaInicial)
            {
                Mensaje.MostrarMensaje(this.Page, "El valor del campo Fecha Hasta, debe ser posterior o igual al valor del campo Fecha Desde");
                return;
            }
        }

        CargarPublicaciones();
        GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
        CrearLogAuditoria.Insertar("INFORMACIÓN", 2, "Se consulto Publicación");

    }

    protected void grdPublicaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerDetalle")
        {
            int _indice = Convert.ToInt32(e.CommandArgument);
            //GridViewRow _fila = grdPublicaciones.Rows[_indice];
            DataKey dk = this.grdPublicaciones.DataKeys[_indice];

            // id_publicación
            long _codigoPublicacion = long.Parse(dk.Values[0].ToString());

            //Int64 _codigoPublicacion = Convert.ToInt64(_fila.Cells[1].Text);
            Session.Add("codPublicacion", "");
            Session["codPublicacion"] = _codigoPublicacion.ToString();

            //CargarVistaDetalle(_codigoPublicacion);

            /// hava:22-abr-10
            /// preparando la session para listar los documentos desde el panel de detalle
            string archivo = dk.Values[1].ToString();
            List<DetalleDocumentoIdentity> ListaDocumentos = new List<DetalleDocumentoIdentity>();

            TraficoDocumento tf = new SILPA.Comun.TraficoDocumento();
            List<string> ListaNombreArchivos = tf.ListarDocumentosDirectorio(archivo);


            if (ListaNombreArchivos != null)
            {
                foreach (string str in ListaNombreArchivos)
                {
                    DetalleDocumentoIdentity doc = new DetalleDocumentoIdentity();
                    doc.Ubicacion = str;
                    doc.NombreArchivo = System.IO.Path.GetFileName(str);
                    ListaDocumentos.Add(doc);
                }

            }
            CargarPublicaciones();
            Session["ListaDocumentos"] = ListaDocumentos;

            CargarVistaDetalle(_codigoPublicacion);
            //Page.Response.Redirect("DetalleDocumentos.aspx");
        }


        if (e.CommandName == "VerDocumento")
        {
            try
            {
                int _indice = Convert.ToInt32(e.CommandArgument);
                //GridViewRow _fila = grdPublicaciones.Rows[_indice];

                DataKey dk = this.grdPublicaciones.DataKeys[_indice];

                //string idPublicaicon = dk.Values[0].ToString();
                string archivo = dk.Values[1].ToString();
                if (!String.IsNullOrEmpty(archivo))
                {
                    List<DetalleDocumentoIdentity> ListaDocumentos = new List<DetalleDocumentoIdentity>();
                    TraficoDocumento tf = new SILPA.Comun.TraficoDocumento();
                    List<string> ListaNombreArchivos = tf.ListarDocumentosDirectorio(archivo);

                    foreach (string str in ListaNombreArchivos)
                    {
                        DetalleDocumentoIdentity doc = new DetalleDocumentoIdentity();
                        doc.Ubicacion = str;
                        doc.NombreArchivo = System.IO.Path.GetFileName(str);
                        ListaDocumentos.Add(doc);
                    }

                    Session["ListaDocumentos"] = ListaDocumentos;
                    
                    string strScript = "window.open('" + "DetalleDocumentos.aspx" + "','pruebas','location=no,resizable=yes,scrollbars=yes,width=1001,height=500')";

                    //Page.RegisterStartupScript("PopupScript", strScript);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "DetalleDocumento", strScript, true);
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>" + strScript + "</script>");
                    //Page.Response.Redirect("DetalleDocumentos.aspx");
                    CargarPublicaciones();
                }
                else
                {
                    Mensaje.MostrarMensaje(this.Page, "No hay documentos asociados.");
                }
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje(this.Page, "No existe el documento asociado.");
                return;
            }

        }
    }

    protected void cboTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        //JDG 2010/28/02 Esto se debe redefinir despues de reunión para validar tipos de documentos
        //cboObjetoDocumento.Items.Clear();
        //cboObjetoDocumento.Items.Add(new ListItem("Seleccione...", "-1"));
        //if (cboTipoDocumento.SelectedValue != "-1")
        //{            
        //    Listas _listaObjetoDocumento = new Listas();
        //    DataSet _temp = _listaObjetoDocumento.ListarActoAdministrativo(null, Convert.ToInt16(cboTipoDocumento.SelectedValue));            
        //    foreach (DataRow _fila in _temp.Tables[0].Rows)
        //    {
        //        ListItem _item = new ListItem(_fila["AAD_NOMBRE"].ToString(), _fila["AAD_ID"].ToString());
        //        cboObjetoDocumento.Items.Add(_item);
        //    }
        //}
    }

    #region Funciones Programador

    /// <summary>
    /// Funcion que carga el listado de Autoridades Ambientales.
    /// </summary>
    private void CargarAutoridades()
    {
        Listas _listaAutoridades = new Listas();
        DataSet _temp = _listaAutoridades.ListarAutoridades(null);
        foreach (DataRow _fila in _temp.Tables[0].Rows)
        {
            ListItem _item = new ListItem(_fila["AUT_NOMBRE"].ToString(), _fila["AUT_ID"].ToString());
            cboAutoridadAmbiental.Items.Add(_item);
        }
    }

    /// <summary>
    /// Funcion que carga el listado de sectores en el combo correspondiente.
    /// </summary>
    private void CargarSectores()
    {
        try
        {            
            SILPA.LogicaNegocio.Generico.Listas _listaSectores = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _temp = _listaSectores.ListarSectorTipoProyecto(null, -1);
            foreach (DataRow _fila in _temp.Tables[0].Rows)
            {
                ListItem _item = new ListItem(_fila["SEC_NOMBRE"].ToString(), _fila["SEC_ID"].ToString());
                cboSectorPublicacion.Items.Add(_item);
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "-- CargarSectores  --" + ex);
            Mensaje.MostrarMensaje(this.Page,"Ocurrio un error comuniquese con el administrador.");
        }        
    }

    private void CargarPublicaciones()
    {
        try
        {           
            String strFechaInicial = "";
            String strFechaFinal = "";

            if (txtFechaInicial.Text != "")
            {
                strFechaInicial = ConvFechas(Convert.ToDateTime(txtFechaInicial.Text));
            }

            if (txtFechaFinal.Text != "")
            {
                strFechaFinal = ConvFechas(Convert.ToDateTime(txtFechaFinal.Text));
            }

            DateTime? fechaFinal = null;
            if (txtFechaFinal.Text != string.Empty)
                fechaFinal = Convert.ToDateTime(txtFechaFinal.Text).AddDays(1);
            Publicacion _listaPublicaciones = new Publicacion();
            DataSet _temp = _listaPublicaciones.ListarPublicacion(null,
                Convert.ToInt16(cboTipoTramite.SelectedValue),
                Convert.ToInt16(cboAutoridadAmbiental.SelectedValue),
                Convert.ToInt16(cboSectorPublicacion.SelectedValue),
                ((txtNombreProyecto.Text == "") ? null : txtNombreProyecto.Text),
                Convert.ToInt16(cboTipoDocumento.SelectedValue),
                ((txtNumeroDocumento.Text == "") ? null : txtNumeroDocumento.Text),
                ((txtNumeroExpediente.Text == "") ? null : txtNumeroExpediente.Text),
                //((txtFechaInicial.Text == "") ? null : txtFechaInicial.Text),
                ((strFechaInicial == "") ? null : strFechaInicial),
                ((strFechaFinal == "") ? null : strFechaFinal),
                //((fechaFinal == null) ? null : fechaFinal.ToString()),
                null);
            if (_temp.Tables[0].Rows.Count > 0)
            {
                grdPublicaciones.DataSource = _temp;
                grdPublicaciones.DataBind();
                pnlPublicaciones.Visible = true;
                dvwDocumento.Visible = false;
                pnlComentarios.Visible = false;
                pnlAgregarComentario.Visible = false;
            }
            else
            {
                pnlPublicaciones.Visible = false;
                dvwDocumento.Visible = false;
                pnlComentarios.Visible = false;
                pnlAgregarComentario.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "-- CargarPublicaciones  --" + ex);
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un error comuniquese con el administrador.");
        }        
    }

    /// <summary>
    /// Función que carga el listado de Tipos de Documento en el respectivo combo.
    /// </summary>
    private void CargarTipoTramite()
    {
        Listas _listaTipoTramite = new Listas();
        DataSet _temp = _listaTipoTramite.ListarTipoTramite(null);
        foreach (DataRow _fila in _temp.Tables[0].Rows)
        {
            ListItem _item = new ListItem(_fila["NOMBRE_TIPO_TRAMITE"].ToString(), _fila["ID_TIPO_PROCESO"].ToString());
            cboTipoTramite.Items.Add(_item);
        }
    }

    /// <summary>
    /// Función que carga el listado de Tipos de Documento en el respectivo combo.
    /// </summary>
    private void CargarTipoDocumento()
    {
        try
        {            
            TipoDocumentoDalc _tipoDocumentoDalc = new TipoDocumentoDalc();
            DataSet _temp = _tipoDocumentoDalc.ListarTiposDeDocumento(null, null);
            foreach (DataRow _fila in _temp.Tables[0].Rows)
            {
                ListItem _item = new ListItem(_fila["NOMBRE_DOCUMENTO"].ToString(), _fila["ID"].ToString());
                cboTipoDocumento.Items.Add(_item);
            }
        }
        catch (Exception ex)
        {

            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "-- CargarTipoDocumento  --" + ex);
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un error comuniquese con el administrador.");
        }                
    }

    /// <summary>
    /// Funcion que carga la publicacion en vista detalle
    /// </summary>
    /// <param name="_codigoPublicacion">Identificador de la publicacion</param>
    private void CargarVistaDetalle(Int64 _codigoPublicacion)
    {
        try
        {           
            Publicacion _listaPublicaciones = new Publicacion();
            DataSet _temp = _listaPublicaciones.ListarPublicacion(_codigoPublicacion, null, null, null, null, null, null, null, null, null, null);
            Session["Resultado"] = _temp;
            dvwDocumento.DataSource = _temp;
            dvwDocumento.DataBind();

            pnlPublicaciones.Visible = false;
            dvwDocumento.Visible = true;
            lblComentarioError.Text = "No ha insertado texto para el comentario";
            lblComentarioError.Visible = false;

            string _vigencia = _temp.Tables[0].Rows[0][3].ToString();
            string _tipo = _temp.Tables[0].Rows[0][19].ToString();

            Publicacion _listaComentarios = new Publicacion();
            DataSet _comentarios = _listaComentarios.ListarComentario(null, _codigoPublicacion);

            if (_comentarios.Tables[0].Rows.Count > 0)
            {
                pnlComentarios.Visible = true;
                grdComentarios.DataSource = _comentarios;
                grdComentarios.DataBind();
            }

            if ((int)_temp.Tables[0].Rows[0][12] == 1 || (int)_temp.Tables[0].Rows[0][12] == 2)
            {
                pnlAgregarComentario.Visible = true;
            }
            else
            {
                pnlAgregarComentario.Visible = false;
            }
          
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "-- CargarVistaDetalle  --" + ex);
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un error comuniquese con el administrador.");
        }        
    }

    #endregion

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {            
            if (txtComentario.Text == "")
                lblComentarioError.Visible = true;
            else
            {
                lblComentarioError.Visible = false;
                Int64 _codigoPub = Convert.ToInt64(Session["codPublicacion"].ToString());
                Publicacion _publicacion = new Publicacion(_codigoPub, txtComentario.Text);
                _publicacion.InsertarComentario();
                txtComentario.Text = "";
                CargarVistaDetalle(_codigoPub);
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + "-- Agregar_Click  --" + ex);
            Mensaje.MostrarMensaje(this.Page, "Ocurrio un error comuniquese con el administrador.");
        }
    }

    protected void grdPublicaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdPublicaciones.PageIndex = e.NewPageIndex;
        this.CargarPublicaciones();
    }

    protected void grdPublicaciones_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                LinkButton lnkEliminar = (LinkButton)e.Row.FindControl("lnkEliminar");
                lnkEliminar.Attributes.Add("onClick", "return confirm('Esta seguro que desea eliminar el registro?')");
            }
        }
    }

    protected void grdPublicaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lblId = (Label)this.grdPublicaciones.Rows[e.RowIndex].FindControl("lblId");
            Publicacion _publicacion = new Publicacion();
            _publicacion.EliminarPublicacion(Convert.ToInt64(lblId.Text));
            this.CargarPublicaciones();
        }
        catch
        {
        }
    }

    protected void dvwDocumento_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName == "Descargar")
        {
          
            DataSet datos = (DataSet)Session["Resultado"];
            string ruta = Convert.ToString(datos.Tables[0].Rows[0]["RUTA_PUB"]);
            string[] archivos = System.IO.Directory.GetFiles(ruta);
            string _nombreArchivo = archivos[0];         
            System.IO.FileInfo targetFile = new System.IO.FileInfo(_nombreArchivo);
            this.Response.Clear();
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
            this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
            this.Response.ContentType = "application/octet-stream";
            this.Response.ContentType = "application/base64";
            this.Response.WriteFile(targetFile.FullName);
            this.Response.WriteFile(_nombreArchivo);

        }
    }

    /// <summary>
    /// Funcion que recibe como parametro una fecha y la devuelve como string en formato ANSI
    /// </summary>
    /// <param name="dtFecha"></param>
    /// <returns></returns>
    private string ConvFechas(DateTime dtFecha)
    {
        string strFecha;
        string strMes;
        string strDia;

        if (dtFecha.Month < 10)
        {
            strMes = "0" + dtFecha.Month.ToString();
        }
        else
        {
            strMes = dtFecha.Month.ToString();
        }

        if (dtFecha.Day < 10)
        {
            strDia = "0" + dtFecha.Day.ToString();
        }
        else
        {
            strDia = dtFecha.Day.ToString();
        }

        strFecha = dtFecha.Year.ToString() + strMes + strDia;

        return strFecha;
    }
}
