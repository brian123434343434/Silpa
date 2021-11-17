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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using SILPA.AccesoDatos.Publicacion;
using SILPA.LogicaNegocio.Publicacion;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.AccesoDatos.Notificacion;
using SILPA.LogicaNegocio.Recurso;
using SoftManagement.Log;

public partial class Presentar_Recurso : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {


        //Session["Usuario"] = "7";
        //DESCOMENTAR ANTES DEL COMMIT!!!
         //Session["Usuario"] = Request.QueryString["IdRelated"];
        
        
        
        if (ValidacionToken() == false)
            Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");

        if (!IsPostBack)
        {
            escribirArchivo();
        }
        else
        {
           
        }
    }

    public void escribirArchivo()
    {
        string cadena = Request.QueryString["idActo"];
        decimal idActoNotificacion = Convert.ToDecimal(cadena);
        Notificacion not = new Notificacion();
        string ruta = not.ConsultarRuta(idActoNotificacion);

        TraficoDocumento traf = new TraficoDocumento();
        if (System.IO.File.Exists(ruta))
        {
            traf.LeerDocumento(ruta);
            byte[] bytes = traf.Bytes;
            using (MemoryStream ms = new MemoryStream())
            {

                ms.Write(bytes, 0, bytes.Length);

                ms.Position = 0;

                Response.Clear();

                Response.AddHeader("Content-Disposition", "attachment; filename=" + ruta);

                Response.AddHeader("Content-Length", ms.Length.ToString());

                Response.ContentType = "application/octet-stream"; //octet-stream";

                Response.BinaryWrite(ms.ToArray());

                Response.Flush();

                Response.Close();

            }
        }
        else
            Mensaje.MostrarMensaje(this.Page, "No se encontro el archivo " + ruta);
    }

    /// <summary>
    /// Valida si hay token para el usuario que intenta acceder a la página, y si no ha expirado
    /// </summary>
    /// <returns>Verdadero si hay token válido para el usuario</returns>
    private bool ValidacionToken()
    {
        //DESCOMENTAR ANTES DEL COMMIT!!!
           //Session["IDForToken"] = Request.QueryString["IdRelated"];
        //Session["IDForToken"] = "7";

        Session["IDForToken"] = Session["Usuario"];

        SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();
        Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));
        using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
        {
            servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
            string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());            
            if (mensaje.IndexOf("Token invalido") > 0)
                return false;
        }
        return true;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {        
        Buscar();
        pnlNotificacion.Visible = true;
    }

    //protected void grdPublicaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "VerDetalle")
    //    {
    //        int _indice = Convert.ToInt32(e.CommandArgument);
    //        //GridViewRow _fila = grdPublicaciones.Rows[_indice];
    //        DataKey dk = this.grdPublicaciones.DataKeys[_indice];
            
    //        // id_publicación
    //        long _codigoPublicacion = long.Parse(dk.Values[0].ToString());

    //        //Int64 _codigoPublicacion = Convert.ToInt64(_fila.Cells[1].Text);
    //        Session.Add("codPublicacion", "");
    //        Session["codPublicacion"] = _codigoPublicacion.ToString();

    //        //CargarVistaDetalle(_codigoPublicacion);
    //    }


    //    if (e.CommandName == "VerDocumento")
    //    {

    //        int _indice = Convert.ToInt32(e.CommandArgument);
    //        //GridViewRow _fila = grdPublicaciones.Rows[_indice];

    //        DataKey dk = this.grdPublicaciones.DataKeys[_indice];

    //        //string idPublicaicon = dk.Values[0].ToString();
    //        string archivo = dk.Values[1].ToString();
    //        List<DetalleDocumentoIdentity> ListaDocumentos = new List<DetalleDocumentoIdentity>();

    //        TraficoDocumento tf = new SILPA.Comun.TraficoDocumento();
    //        List<string> ListaNombreArchivos = tf.ListarDocumentosDirectorio(archivo);

    //        foreach (string str in ListaNombreArchivos)
    //        {
    //            DetalleDocumentoIdentity doc = new DetalleDocumentoIdentity();
    //            doc.Ubicacion = str;
    //            doc.NombreArchivo = System.IO.Path.GetFileName(str);
    //            ListaDocumentos.Add(doc);
    //        }

    //        Session["ListaDocumentos"] = ListaDocumentos;
    //        Page.Response.Redirect("DetalleDocumentos.aspx");
    //    }
    //}

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
    
    ///// <summary>
    ///// Funcion que carga el listado de sectores en el combo correspondiente.
    ///// </summary>
    //private void CargarSectores()
    //{
    //    SILPA.LogicaNegocio.Generico.Listas _listaSectores = new SILPA.LogicaNegocio.Generico.Listas();
    //    DataSet _temp = _listaSectores.ListarSectorTipoProyecto(null, -1);        
    //    foreach (DataRow _fila in _temp.Tables[0].Rows)
    //    {
    //        ListItem _item = new ListItem(_fila["SEC_NOMBRE"].ToString(), _fila["SEC_ID"].ToString());
    //        cboSectorPublicacion.Items.Add(_item);
    //    }
    //}

    private void Buscar()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Buscar.Inicio");
            Listas _listaActos = new Listas();
            DateTime? fechaInicial = null;
            DateTime? fechaFinal = null;
            string Acto = null;
            string Expediente = null;
            string NumeroSILPA = null;

            if (!txtFechaInicial.Text.Equals(string.Empty) && !txtFechaFinal.Text.Equals(string.Empty))
            {

                fechaInicial = Convert.ToDateTime(txtFechaInicial.Text);
                fechaFinal = Convert.ToDateTime(txtFechaFinal.Text);
            }
            if (!cboActoAdministrativo.SelectedItem.Value.Equals("-1"))
                Acto = cboActoAdministrativo.SelectedItem.Text.Trim();
            if (!cboNumeroExpediente.SelectedItem.Value.Equals("-1"))
                Expediente = cboNumeroExpediente.SelectedItem.Text.Trim();
            if (!cboNumeroSILPA.SelectedItem.Value.Equals("-1"))
                NumeroSILPA = cboNumeroSILPA.SelectedItem.Text.Trim();


            //List<NotificacionEntity> listaActos = _listaActos.ListarActosNotificacion(NumeroSILPA, Expediente, Acto, fechaInicial, fechaFinal, Convert.ToInt32(Session["Usuario"]));
            ListarActosRecursosReposicion listar = new ListarActosRecursosReposicion();
            //especificamos del campo de parametrizacion en la tabla GEN_PARAMETRO
            listar.NombreParametro = ConfigurationSettings.AppSettings["maximos_dias_recurso_reposicion"];
            List<RecursoReposicionPresentacion> listasRecursos = listar.listarActosRecursos(NumeroSILPA, Expediente, Acto, fechaInicial, fechaFinal, Session["Usuario"].ToString());

            //grdActos.DataSource = listaActos;
            grdActos.DataSource = listasRecursos;
            grdActos.DataBind();

            //_listaActos.ListarActosNotificacionUsuario(

            //    grdPublicaciones.DataSource = _temp;
            //    grdPublicaciones.DataBind();
            //    pnlPublicaciones.Visible = true;
            //    dvwDocumento.Visible = false;
            //    pnlComentarios.Visible = false;
            //    pnlAgregarComentario.Visible = false;
            //}
            //else
            //{            
            //    pnlPublicaciones.Visible = false;
            //    dvwDocumento.Visible = false;
            //    pnlComentarios.Visible = false;
            //    pnlAgregarComentario.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);   
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".Buscar.Finalizo");
        }
    }

    /// <summary>
    /// Carga la Lista de Expedientes asociados a los actos administrativos del actor
    /// </summary>
    private void CargarListas()
    {
        Listas _listaNumeroSILPA = new Listas();
        List<NotificacionEntity> listaNotificacion = _listaNumeroSILPA.ListarActosNotificacion(Session["Usuario"].ToString());

        
        cboNumeroSILPA.DataSource = listaNotificacion;
        cboNumeroSILPA.DataTextField="NumeroSILPA";
        cboNumeroSILPA.DataValueField = "NumeroSILPA";
        cboNumeroSILPA.DataBind();
        
        cboNumeroExpediente.DataSource = listaNotificacion;
        cboNumeroExpediente.DataTextField = "ProcesoAdministracion";
        cboNumeroExpediente.DataValueField = "ProcesoAdministracion";
        cboNumeroExpediente.DataBind();

        cboActoAdministrativo.DataSource = listaNotificacion;
        cboActoAdministrativo.DataTextField = "NumeroActoAdministrativo";
        cboActoAdministrativo.DataValueField = "NumeroActoAdministrativo";
        cboActoAdministrativo.DataBind();
    
    }
    

    #endregion        


    //protected void grdPublicaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    this.grdPublicaciones.PageIndex = e.NewPageIndex;
    //    this.Buscar();
    //}
    //protected void grdPublicaciones_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
    //        {
    //            LinkButton lnkEliminar = (LinkButton)e.Row.FindControl("lnkEliminar");
    //            lnkEliminar.Attributes.Add("onClick", "return confirm('Esta seguro que desea eliminar el registro?')");
    //        }
    //    }
    //}
    //protected void grdPublicaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        Label lblId = (Label)this.grdPublicaciones.Rows[e.RowIndex].FindControl("lblId");
    //        Publicacion _publicacion = new Publicacion();
    //        _publicacion.EliminarPublicacion(Convert.ToInt64(lblId.Text));
    //        this.Buscar();
    //    }
    //    catch
    //    {
    //    }
    //} 

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //COnseguimos el acto de notificacion que se seleccionò
        decimal idActoNotificacion = grdActos.SelectedIndex;
        Notificacion not = new Notificacion();
        string ruta = not.ConsultarRuta(idActoNotificacion);
        int s = 2;

        /*string archivo = "C:\test.txt";
        string filePath = "C:/test.txt";
        System.IO.FileInfo _targetFile = new System.IO.FileInfo(filePath); 
     
        // clear the current output content from the buffer
        Response.Clear();
        // add the header that specifies the default filename for the Download/
        // SaveAs dialog
        Response.AddHeader("Content-Disposition", "attachment; filename=" + _targetFile.Name);
        // add the header that specifies the file size, so that the browser
        // can show the download progress
        Response.AddHeader("Content-Length", _targetFile.Length.ToString());
        // specify that the response is a stream that cannot be read by the
        // client and must be downloaded
        Response.ContentType = "application/octet-stream";
        // send the file stream to the client
        Response.WriteFile(_targetFile.FullName);
        // stop the execution of this page
        Response.End();/*g

        */

    }


    /*Descarga de documento Acto administrativo*/

   /* protected void grdActos_SelectedIndexChanged(object sender, EventArgs e)
    {
        string s = (grdActos.SelectedDataKey.Value.ToString());
        Notificacion not = new Notificacion();
        string rutaDocumento = not.ConsultarRuta(Convert.ToDecimal(s));
        TraficoDocumento trafico = new TraficoDocumento();
        
        trafico.LeerDocumento(rutaDocumento);
        byte[] array = trafico.Bytes;


        


        System.IO.FileInfo targetFile = new System.IO.FileInfo(rutaDocumento);
        this.Response.Clear();
        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
        this.Response.ContentType = "application/octet-stream";
        this.Response.ContentType = "application/base64";
        this.Response.WriteFile(targetFile.FullName);
        this.Response.WriteFile(rutaDocumento);
      
    
    
        /*
        Response.Clear();
        Response.ClearHeaders();
        Response.ClearContent();
        Response.ContentType = "application/octet-stream";
        Response.ContentType = "application/base64";
        Response.AddHeader("content-transfer-encoding", "binary") ;
        //Response.BufferOutput = true;
        //Response.AddHeader("content-disposition", "attachment; filename=" + rutaDocumento);
        Response.ContentEncoding = System.Text.Encoding.GetEncoding(1251);
        Response.BinaryWrite(array);
        //Response.End();*/

    //}
    protected void grdActos_SelectedIndexChanged(object sender, EventArgs e)
    {
        string s = "asdf";
    }
}
