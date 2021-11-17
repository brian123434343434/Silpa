using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Recurso;
using SILPA.LogicaNegocio.ImpresionesFus;
using SILPA.AccesoDatos.RecursoReposicion;
using System.Data;
using SILPA.AccesoDatos.BPMProcess;
using System.IO;
using System.Xml.Serialization;
using SILPA.LogicaNegocio.Sancionatorio;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Notificacion;
using System.Configuration;
using SILPA.Comun;

public partial class Recurso_RegistrarRecursoPublico : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["RegistroRecurso"] != null)
            {
                RecursoReposicionPresentacion registroRecurso = (RecursoReposicionPresentacion)Session["RegistroRecurso"];

                lblNumeroSILPADato.Text = registroRecurso.NumeroSILPA;

                if (Session["NumeroSilpa"] != null)
                {
                    lblNumeroSilpaReal.Text = Session["NumeroSilpa"].ToString();
                }

                lblNumeroSilpaReal.Text = registroRecurso.NumeroSILPA;

                lblNumeroExpedienteDato.Text = registroRecurso.ProcesoAdministracion;
                lblNumeroActoDato.Text = registroRecurso.NumeroActoAdministrativo;
                lblFechaActoDato.Text = registroRecurso.FechaActo.ToString();
                lblFechaNotiDato.Text = registroRecurso.FechaNotificacion.ToString();

                GenerarArchivoFus objFus = new GenerarArchivoFus();

                lblNombreProyDato.Text = registroRecurso.CampoBorrar;



            }
            //asignar valores al RecursoReposicionRepresentacion
            if (Session["RegistroRecurso"] == null)
            {
                RecursoReposicionPresentacion recRep = new RecursoReposicionPresentacion();
                recRep.NumeroSILPA = lblNumeroSILPADato.Text;
                recRep.ProcesoAdministracion = lblNumeroExpedienteDato.Text;
                recRep.NumeroActoAdministrativo = lblNumeroActoDato.Text;
                Session["RegistroRecurso"] = recRep;
            }
            if (Session["RecursoEntity"] == null)
            {

                RecursoEntity rec = new RecursoEntity();
                rec.ListaDocumentos = new List<RecursoDocumentoEntity>();
                Session.Add("RecursoEntity", typeof(RecursoEntity));
                Session["RecursoEntity"] = rec;

            }
            

            DataTable _documentos = (DataTable)Session["Documentos"];
            if (_documentos != null && _documentos.Rows.Count > 0)
            {
                grdDocumentosRecurso.DataSource = (DataTable)Session["Documentos"];
                grdDocumentosRecurso.DataBind();
                lblMensajeAgregar.Visible = false;

            }
            else
            {
                grdDocumentosRecurso.DataSource = null;
                grdDocumentosRecurso.DataBind();
                grdDocumentosRecurso.Visible = false;
                lblMensajeAgregar.Visible = true;
            }
            if (Session["Descrip"] != null)
            {
                this.txtDescripcion.Text = Session["Descrip"].ToString();
                Session["Descrip"] = null;
            }
        }
    }

    protected void grdDocumentosRecurso_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row;
        int index = Convert.ToInt32(e.CommandArgument);
        if (index < 0)
            return;

        if (e.CommandName == "Actualizar")
        {
            SILPA.AccesoDatos.RecursoReposicion.RecursoEntity recursoEntity = (SILPA.AccesoDatos.RecursoReposicion.RecursoEntity)Session["RecursoEntity"];
            DataTable _documentos = new DataTable();
            _documentos = (DataTable)Session["Documentos"];

            this.Response.Clear();
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + _documentos.Rows[index]["RUTA"]);
            this.Response.AddHeader("Content-Length", recursoEntity.ListaDocumentos[index].Archivo.Length.ToString());
            this.Response.ContentType = "application/octet-stream";
            this.Response.ContentType = "application/base64";

            this.Response.BinaryWrite(recursoEntity.ListaDocumentos[index].Archivo);

        }
        else if (e.CommandName == "Eliminar")
        {
            DataTable _documentos = new DataTable();
            _documentos = (DataTable)Session["Documentos"];
            if (this.grdDocumentosRecurso.Rows.Count != _documentos.Rows.Count)
            {
                if (_documentos.Rows.Count > 0)
                {
                    grdDocumentosRecurso.DataSource = (DataTable)Session["Documentos"];
                    grdDocumentosRecurso.DataBind();
                    lblMensajeAgregar.Visible = false;

                }
                else
                {
                    grdDocumentosRecurso.DataSource = null;
                    grdDocumentosRecurso.DataBind();
                    grdDocumentosRecurso.Visible = false;
                    lblMensajeAgregar.Visible = true;
                }
                return;
            }
            try
            {

                SILPA.AccesoDatos.RecursoReposicion.RecursoEntity recursoEntity = (SILPA.AccesoDatos.RecursoReposicion.RecursoEntity)Session["RecursoEntity"];

                recursoEntity.ListaDocumentos.RemoveAt(index);

                DataRow _row = _documentos.Rows[index];

                _documentos.Rows.Remove(_row);
                _documentos.AcceptChanges();
                Session["Documentos"] = _documentos;

                if (_documentos.Rows.Count > 0)
                {
                    grdDocumentosRecurso.DataSource = (DataTable)Session["Documentos"];
                    grdDocumentosRecurso.DataBind();
                    lblMensajeAgregar.Visible = false;

                }
                else
                {
                    grdDocumentosRecurso.DataSource = null;
                    grdDocumentosRecurso.DataBind();
                    grdDocumentosRecurso.Visible = false;
                    lblMensajeAgregar.Visible = true;
                }

            }
            catch (Exception ex)
            {
                Mensaje.ErrorCritico(this, ex);
            }
        }
    }

    protected void btnAgregarDocumentoRecurso_Click(object sender, EventArgs e)
    {
        CrearVariableDocumentoRecurso();

        Session["Descrip"] = this.txtDescripcion.Text;
       // Response.Redirect("DocumentoRecurso.aspx?modo=0");
        this.mpeAgregarDocumento.Show();

    }

    private void CrearVariableDocumentoRecurso()
    {

        try
        {
            if (Session["Documentos"] == null)
            {
                DataTable _tabla = new DataTable();
                _tabla.Columns.Add("ID", typeof(int));
                _tabla.Columns.Add("ARCHIVO", typeof(string));
                _tabla.Columns.Add("NUMERO_RADICADO", typeof(string));
                Session.Add("Documentos", _tabla);
            }

        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        try
        {
            string numerosilpa;
            string archivosAdjuntos = "";
            Int64 UsuarioQueja = Int64.Parse(Session["IDForToken"].ToString());
            SILPA.LogicaNegocio.Recurso.RecursoReposicionPresentacion rrp = (RecursoReposicionPresentacion)Session["RegistroRecurso"];

            SILPA.AccesoDatos.RecursoReposicion.RecursoEntity recursoEntity = (SILPA.AccesoDatos.RecursoReposicion.RecursoEntity)Session["RecursoEntity"];
            int i = 0;
            if (recursoEntity.ListaDocumentos.Count == 0)
            {
                Mensaje.MostrarMensaje(this, "Favor agregar por lo menos un archivo a la solicitud.");
                return;
            }
            else
            {
                bool tieneArchivo = true;
                for (i = 1; i <= recursoEntity.ListaDocumentos.Count; i++)
                {
                    if (recursoEntity.ListaDocumentos[0].NombreDocumento == "")
                        tieneArchivo = false;
                }
                if (!tieneArchivo)
                {
                    Mensaje.MostrarMensaje(this, "Uno de los registros adjuntos no contien archivo. Favor verifique.");
                    return;
                }
            }

            if (this.lblNumeroSilpaReal.Text != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
            {
                List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
                objValoresList.Add(CargarValores(1, "Bas1", this.lblNombreProyDato.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(2, "Bas1", Session["NumeroSilpa2"].ToString(), 1, new Byte[1]));
                objValoresList.Add(CargarValores(3, "Bas1", this.lblNumeroExpedienteDato.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(4, "Bas1", this.lblNumeroActoDato.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(5, "Bas1", this.lblFechaActoDato.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(6, "Bas1", this.lblFechaNotiDato.Text, 1, new Byte[1]));
                objValoresList.Add(CargarValores(7, "Bas1", this.txtDescripcion.Text, 1, new Byte[1]));

                for (i = 1; i <= recursoEntity.ListaDocumentos.Count; i++)
                {
                    objValoresList.Add(CargarValores(8, "List1", recursoEntity.ListaDocumentos[i - 1].NombreDocumento, i, recursoEntity.ListaDocumentos[i - 1].Archivo));
                    objValoresList.Add(CargarValores(9, "List1", recursoEntity.ListaDocumentos[i - 1].NumeroRadicado.ToString(), i, new Byte[1]));
                    objValoresList.Add(CargarValores(10, "List1", "", i, new Byte[1]));
                }
                objValoresList.Add(CargarValores(8, "List1", "", i + 1, new Byte[1]));
                objValoresList.Add(CargarValores(9, "List1", "", i + 1, new Byte[1]));
                objValoresList.Add(CargarValores(10, "List1", "", i, new Byte[1]));


                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
                serializador.Serialize(memoryStream, objValoresList);
                string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
                Queja _queja = new Queja();
                DataTable ParametrosFormulario = _queja.ObtenerUsuarioRecurso(Session["IDForToken"].ToString()).Tables[0];
                
                Int64 Formularioqueja = Int64.Parse(ParametrosFormulario.Rows[0]["FORM_ID"].ToString());
                string ClientId = ParametrosFormulario.Rows[0]["CLIENT_ID"].ToString();

                numerosilpa = _queja.CrearProcesoQueja(ClientId, Formularioqueja, UsuarioQueja, xml);
                Mensaje.MostrarMensaje(this, "El Numero Vital de la Operacion es : " + NumeroVital(numerosilpa).Replace("<br />", "\\n"));
                
            }
            else
            {
                numerosilpa = this.lblNumeroSilpaReal.Text;
            }
            RecursoDalc recursoDalc = new RecursoDalc();
            recursoEntity.IDRecurso = rrp.IdActoNotificacion;
            recursoEntity.IdActoNotificacion = rrp.IdActoNotificacion;
            recursoEntity.Descripcion = txtDescripcion.Text + "-" + numerosilpa + " - " + rrp.NumeroActoAdministrativo + " - " + lblNumeroExpedienteDato.Text;
            recursoEntity.Estado = new SILPA.AccesoDatos.RecursoReposicion.RecursoEstadoEntity();
            recursoEntity.Estado.IDEstadoRecurso = 2;
            recursoEntity.Acto = new SILPA.AccesoDatos.Notificacion.NotificacionEntity();
            recursoEntity.Acto.IdActoNotificacion = rrp.IdActoNotificacion;
            PersonaIdentity persona = new PersonaIdentity();
            if (this.lblNumeroSilpaReal.Text != ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
            {
                persona.IdApplicationUser = Convert.ToInt64(UsuarioQueja);
            }
            else
            {
                persona.IdApplicationUser = Convert.ToInt64(Session["Usuario"].ToString());
            }
            PersonaDalc personaDalc = new PersonaDalc();
            personaDalc.ObtenerPersona(ref persona);
            recursoEntity.NumeroIdentificacion = persona.NumeroIdentificacion;
            recursoDalc.InsertarRecursoExtendido(ref recursoEntity,
                lblNumeroExpedienteDato.Text,
                lblNumeroSilpaReal.Text, lblNumeroSILPADato.Text, numerosilpa,
                recursoEntity.Acto.NumeroActoAdministrativo);
            if (recursoEntity.ListaDocumentos.Count > 0)
            {
                NotificacionDalc dalc = new NotificacionDalc();
                EstadoPersonaActoEntity personaEstado = dalc.ObtenerEstadoPersonaNotificado(recursoEntity.IdActoNotificacion);          
                
                // optenemos la ruta d los documentos de notificacion 
                documentoAdjuntoType[] documentos = new documentoAdjuntoType[recursoEntity.ListaDocumentos.Count];
                int cont = 0;
                foreach (RecursoDocumentoEntity archivo in recursoEntity.ListaDocumentos)
                {
                    documentoAdjuntoType documento = new documentoAdjuntoType();
                    documento.bytes = archivo.Archivo;
                    documento.nombreArchivo = archivo.NombreDocumento;
                    documentos.SetValue(documento, cont);
                    archivosAdjuntos +=  personaEstado.RutaDocumentos + archivo.NombreDocumento + ";" ;
                    cont++;
                }

                if (personaEstado.RutaDocumentos != null)
                {
                    TraficoDocumento traficoDocumentos = new TraficoDocumento();
                    traficoDocumentos.RecibirDocumentoReenvio(documentos, personaEstado.RutaDocumentos);
                }
            }
            NoficicacionRecursoInterpuesto((int)rrp.IdActoNotificacion, persona.NumeroIdentificacion, archivosAdjuntos);

            // cuando el numero silpa corresponde al numero silpa de notificacion electronica se debe informar sobre el recurso interpuesto.
            if (this.lblNumeroSilpaReal.Text == ConfigurationManager.AppSettings["numero_silpa_notificacion_electronica"].ToString())
            {
                SILPA.LogicaNegocio.ICorreo.Correo correo = new SILPA.LogicaNegocio.ICorreo.Correo();
                string nombrePersona = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
                correo.EnviarCorreoRecursoInterpuestoPublico
                    (ConfigurationManager.AppSettings["destinatario_recurso_interpuesto"].ToString(), nombrePersona, persona.TipoDocumentoIdentificacion.Nombre,
                        persona.NumeroIdentificacion, archivosAdjuntos, this.lblNumeroSilpaReal.Text,
                        this.lblNumeroExpedienteDato.Text);
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "closeDialog();", true);
            Session["RegistroRecurso"] = null;
            Session["RecursoEntity"] = null;
            Session["Documentos"] = null;
            Session["Descrip"] = null;

        }
        catch (Exception ex)
        {
            Mensaje.MostrarMensaje(this.Page, ex.ToString());
        }
    }

    private void NoficicacionRecursoInterpuesto(int numeroActoNotificacion, string numeroIdentificacion, string archivosAdjuntos)
    {

        NotificacionDalc _objNotificacionDalc = new NotificacionDalc();
        _objNotificacionDalc.notificarRecursos(numeroActoNotificacion, numeroIdentificacion, archivosAdjuntos);

    }

    private static string NumeroVital(string numeroSilpa)
    {
        SILPA.AccesoDatos.Generico.NumeroSilpaDalc silpa = new NumeroSilpaDalc();
        string idProceso = silpa.NumeroInstancia(numeroSilpa);
        return silpa.NumeroSilpa(int.Parse(idProceso.Replace("_", "")));
    }

    private ValoresIdentity CargarValores(int id, string grupo, string valor, int orden, Byte[] archivo)
    {
        ValoresIdentity objValores = new ValoresIdentity();
        objValores.Id = id;
        objValores.Grupo = grupo;
        objValores.Valor = valor;
        objValores.Orden = orden;
        objValores.Archivo = archivo;
        return objValores;
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        DataTable _documentos = new DataTable();
        DataRow _fila;
       

        if (fupDocumento.PostedFile.ContentLength > 15360000)
        {
            string strScript1 = "<script language='JavaScript'>alert('El Archivo debe tener un tamaño maximo de 15Mb.')</script>";
            Page.RegisterClientScriptBlock("mensaje", strScript1);
            return;
        }

        try
        {
            _documentos = (DataTable)Session["Documentos"];
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

            if (_documentos != null && _documentos.Rows.Count > 0)
            {
                grdDocumentosRecurso.DataSource = (DataTable)Session["Documentos"];
                grdDocumentosRecurso.DataBind();
                grdDocumentosRecurso.Visible = true;
                lblMensajeAgregar.Visible = false;

            }
            else
            {
                grdDocumentosRecurso.DataSource = null;
                grdDocumentosRecurso.DataBind();
                grdDocumentosRecurso.Visible = false;
                lblMensajeAgregar.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
        }
    }
}
