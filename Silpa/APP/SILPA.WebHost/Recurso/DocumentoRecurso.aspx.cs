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
using SILPA.AccesoDatos.RecursoReposicion;

public partial class Recurso_DocumentoRecurso : System.Web.UI.Page
{
    private string _modo;
    private string _id;

    protected void Page_Load(object sender, EventArgs e)
    {
         _modo = Request.QueryString["modo"];        

        if (Request.QueryString.Count > 1)
            _id = Request.QueryString["id"];

        if (!IsPostBack)
        {
            if(Session["ErrorArchivo"] != null)
                if (Session["ErrorArchivo"].ToString() != "")
                {
                    string strScript1 = "<script language='JavaScript'>alert('El Archivo debe tener un tamaño maximo de 15Mb.')</script>";
                    Page.RegisterClientScriptBlock("mensaje", strScript1);
                    Session["ErrorArchivo"] = "";
                }
            if (_modo == "1")
            {
               
                CargarDatos();
            }
        }

    }

    private void CargarDatos()
    {
        DataTable _representantes = new DataTable();
        DataRow _fila;

        try
        {
            _representantes = (DataTable)Session["Representantes"];
            if (_representantes.Select("ID = '" + _id + "'").Length > 0)
            {
                foreach (DataRow _temp in _representantes.Rows)
                {
                    if (_temp["ID"].ToString() == _id)
                    {
                        _temp["ARCHIVO"] = fupDocumento.FileName;
                        _temp["NUMERO_RADICADO"] = txtNumeroRadicado.Text;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }

    private int retornarIndice(DropDownList _combo, string _valor)
    {
        foreach (ListItem _item in _combo.Items)
        {
            _item.Selected = false;
            if (_item.Value == _valor)
            {
                _item.Selected = true;
                return _combo.SelectedIndex;
            }
        }
        return -1;
    }


    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        DataTable _documentos = new DataTable();
        DataRow _fila;


        if (fupDocumento.PostedFile.ContentLength > 716800000)
        { 
              string strScript1 = "<script language='JavaScript'>alert('El Archivo debe tener un tamaño maximo de 700Mb.')</script>";
              Page.RegisterClientScriptBlock("mensaje", strScript1);
              return;
        }

        try
        {
            _documentos = (DataTable)Session["Documentos"];
            if (_modo == "0")
            {

                _fila = _documentos.NewRow();
                _fila["ID"] = _documentos.Rows.Count + 1;
                _fila["ARCHIVO"] = fupDocumento.FileName;
                _fila["NUMERO_RADICADO"] = txtNumeroRadicado.Text;
 
                _documentos.Rows.Add(_fila);
                _documentos.AcceptChanges();
                Session["Documentos"] = _documentos;


                //asignamos los valores al entity de documentoRecurso dentro del recurso que se encuentra en la sesion

                RecursoEntity rec = (RecursoEntity)Session["RecursoEntity"];
                RecursoDocumentoEntity documento = new RecursoDocumentoEntity();
                documento.NumeroRadicado = txtNumeroRadicado.Text;
                // Falta asignar bytes del documento y nombre
                documento.Archivo = fupDocumento.FileBytes;
                documento.NombreDocumento = fupDocumento.FileName;
                rec.ListaDocumentos.Add(documento);
                Session["RecursoEntity"] = rec;
            }
            else if (_modo == "1")
            {
                if (_documentos.Select("ID = '" + _id + "'").Length > 0)
                {
                    foreach (DataRow _temp in _documentos.Rows)
                    {
                        if (_temp["ID"].ToString() == _id)
                        {
                            _temp["ARCHIVO"] = fupDocumento.FileName;
                            _temp["NUMERO_RADICADO"]=txtNumeroRadicado.Text;
                            
                            _documentos.AcceptChanges();
                            Session["Documentos"] = _documentos;                            
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
        //string strScript1;
        /*strScript1 = "<script language='JavaScript'>window.close()</script>";
        Page.RegisterClientScriptBlock("Pruebas1", strScript1);*/
        Response.Redirect("RegistrarRecurso.aspx");
    
    }
}
