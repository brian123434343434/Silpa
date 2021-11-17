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
using SILPA.AccesoDatos.RecursoReposicion;
using SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades;


public partial class Presentar_Recurso : System.Web.UI.Page
{
    #region Constantes

        private const int TAMANO_PAGINA = 10;

    #endregion


    #region Propiedades

        /// <summary>
        /// Identificador del usuario
        /// </summary>
        private string _strUsuarioID
        {
            get
            {
                return (ViewState["_strUsuarioID"] != null ? ViewState["_strUsuarioID"].ToString() : "");
            }
            set
            {
                ViewState["_strUsuarioID"] = value;
            }
        }
        
    #endregion


    #region Metodos Privados


        /// <summary>
        /// Verificar si se encuentra autenticado el usuario
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            if (DatosSesion.Usuario == string.Empty)
            {
                return false;
            }

            string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

            Session["IDForToken"] = (object)idUsuario;

            this._strUsuarioID = Session["IDForToken"].ToString();

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


        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.tblMensaje.Visible = true;
            this.upnlMensaje.Update();
        }

        /// <summary>
        /// Ocultar los mensajes
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.tblMensaje.Visible = false;
            this.upnlMensaje.Update();
        }

        /// <summary>
        /// Consulta el listado de autoridades ambientales existentes y carga en listado especificado
        /// </summary>
        private void ConsultaAutoridadesAmbientales(DropDownList p_objListado)
        {
            SILPA.LogicaNegocio.Generico.Listas objListados = null;

            try
            {
                //Cargar la información de notificaciones
                objListados = new SILPA.LogicaNegocio.Generico.Listas();
                p_objListado.DataSource = objListados.ListarAutoridades(null);
                p_objListado.DataTextField = "AUT_NOMBRE";
                p_objListado.DataValueField = "AUT_ID";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Presentar_Recurso :: ConsultaAutoridadesAmbientales -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue del listado de autoridades ambientales");
            }
        }


        /// <summary>
        /// Limpiar el modal de mensajes de error
        /// </summary>
        private void LimpiarModalMensajesOk()
        {
            //Limpiar datos de modal
            this.ltlTituloModalMensajeOk.Text = "";
            this.ltlMensajeModalMensajeOk.Text = "";
        }

        /// <summary>
        /// Cargar la información inicial de la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Cargar información listados
            this.ConsultaAutoridadesAmbientales(this.cboAutoridadAmbiental);

            //Inicializar fechas
            this.txtFechaDesde.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");            

            //Ocultar resultado
            this.divResultadoBusqueda.Visible = false;

            //Ocultar botones
            this.cmdMostrarTodos.Visible = false;
            this.cmdMostrarPaginado.Visible = false;

            //Limpiar modales
            this.LimpiarModalMensajesOk();
        }


        /// <summary>
        /// Cargar los trigger especificos que se requieren de la grilla de actos administrativos para recursos de reposición
        /// </summary>
        private void CargarTriggersGrillaRecursos()
        {
            ImageButton objImagen = null;

            //Verificar que la grilla contenga información
            if (this.grdActosRecursos.Visible && this.grdActosRecursos.Rows.Count > 0)
            {
                //Ciclo que adiciona los triggers de la grilla de notificaciones
                foreach (GridViewRow objRowNotificacion in this.grdActosRecursos.Rows)
                {
                    //Cargar imagen de descarga
                    objImagen = (ImageButton)objRowNotificacion.FindControl("imgDescargarDocumento");

                    //Si existe el control y se encuentra visible
                    if (objImagen != null && objImagen.Visible)
                    {
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                    }

                    //Cargar imagen de descarga
                    objImagen = (ImageButton)objRowNotificacion.FindControl("imgDescargarDocumentoANLA");

                    //Si existe el control y se encuentra visible
                    if (objImagen != null && objImagen.Visible)
                    {
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objImagen);
                    }
                }
            }
        }


        /// <summary>
        /// Carga la información de actos administrativos de acuerdo a los parametros de busqueda
        /// </summary>
        private void BuscarInformacionActosAdministrativosRecurso()
        {
            Recurso objRecurso = null;
            List<ActoParaRecursoEntity> objLstActosAdministrativos = null;

            try
            {
                objRecurso = new Recurso();
                objLstActosAdministrativos = objRecurso.ObtenerListadoActosAdministrativosRecursoPersona(long.Parse(this._strUsuarioID), this.hdfNumeroVital.Value.Trim(), this.hdfExpediente.Value.Trim(),
                                                                                                          this.hdfNumeroActo.Value.Trim(),
                                                                                                          (!string.IsNullOrWhiteSpace(this.hdfAutoridadAmbiental.Value) ? Convert.ToInt32(this.hdfAutoridadAmbiental.Value.Trim()) : -1),
                                                                                                          Convert.ToDateTime(this.hdfFechaDesde.Value.Trim()), Convert.ToDateTime(this.hdfFechaHasta.Value.Trim()));
                //Cargar listado de notificaciones
                this.grdActosRecursos.DataSource = objLstActosAdministrativos;
                this.grdActosRecursos.DataBind();

                //Cargar triggers
                this.CargarTriggersGrillaRecursos();

                //Actualizar panel
                this.upnlConsultaRecursos.Update();

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Presentar_Recurso :: BuscarInformacionActosAdministrativosRecurso -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error realizando la busqueda de la información de actos administrativos para presentación de recursos");
            }
        }        

    #endregion


    #region Eventos


        #region Page

            /// <summary>
            /// Evento que se ejecuta al momento de cargar la pagina    
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                //this._strUsuarioID = "12540";
                //this._strUsuarioID = "429";

                if (!IsPostBack)
                {
                    //this.InicializarPagina();
                    
                    //Validar sesion de usuario
                    if (this.ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                    else
                    {
                        //Inicializar datos de la página
                        this.InicializarPagina();
                    }
                }
            }

        #endregion


        #region cmdBuscar

            /// <summary>
            /// Evento que realiza la busqueda de los actos admninistrativos disponibles para presentar recurso de repoisición 
            /// basado en los parametros de busqueda establecidos
            /// </summary>
            protected void cmdBuscar_Click(object sender, EventArgs e)
            {
                //Ocultar Mensajes
                this.OcultarMensaje();

                //Cargar la información
                this.hdfNumeroVital.Value = this.txtNumeroVital.Text;
                this.hdfExpediente.Value = this.txtExpediente.Text;
                this.hdfNumeroActo.Value = this.txtNumeroActo.Text;
                this.hdfAutoridadAmbiental.Value = this.cboAutoridadAmbiental.SelectedValue;
                this.hdfFechaDesde.Value = this.txtFechaDesde.Text;
                this.hdfFechaHasta.Value = this.txtFechaHasta.Text;

                //Mostrar panel de resultados
                this.divResultadoBusqueda.Visible = true;

                //Inicializar grilla
                this.grdActosRecursos.PageIndex = 0;
                this.grdActosRecursos.AllowPaging = true;
                this.grdActosRecursos.PageSize = TAMANO_PAGINA;                

                //Ejecutar consulta
                this.BuscarInformacionActosAdministrativosRecurso();

                //Mostrar botones de acuerdo a resultado de la busqueda
                if (this.grdActosRecursos.PageCount > 1)
                {
                    this.cmdMostrarTodos.Visible = true;
                    this.cmdMostrarPaginado.Visible = false;
                }
                else
                {
                    this.cmdMostrarTodos.Visible = false;
                    this.cmdMostrarPaginado.Visible = false;
                }

                //Actualizar panel
                this.upnlConsultaRecursos.Update();
            }

        #endregion


        #region grdActosRecursos

            /// <summary>
            /// Evento que se ejecuta al cambiar la pagina
            /// </summary>
            protected void grdActosRecursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Cambiar pagina
                this.grdActosRecursos.PageIndex = e.NewPageIndex;

                //Consultar la información
                this.BuscarInformacionActosAdministrativosRecurso();
            }

            
            /// <summary>
            /// Evento que descarga el documento indicado
            /// </summary>
            protected void imgDescargarDocumento_Click(object sender, ImageClickEventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        System.IO.FileInfo targetFile = new System.IO.FileInfo(((ImageButton)sender).CommandArgument.ToString());
                        this.Response.Clear();
                        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                        this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                        this.Response.ContentType = "application/octet-stream";
                        this.Response.ContentType = "application/base64";
                        this.Response.WriteFile(targetFile.FullName);
                        this.Response.WriteFile(((ImageButton)sender).CommandArgument.ToString());
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Presentar_Recurso :: imgDescargarDocumento_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }


            /// <summary>
            /// Evento que descarga el documento indicado desde la ANLA
            /// </summary>
            protected void imgDescargarDocumentoANLA_Click(object sender, ImageClickEventArgs e)
            {
                Recurso objRecurso;
                ArchivoRecursoEntity objArchivoRecursoEntity = null;
          
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        //Obtener el documento
                        objRecurso = new Recurso();
                        objArchivoRecursoEntity = objRecurso.ObtenerArchivoRecursoANLA(Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString()));

                        if (objArchivoRecursoEntity != null)
                        {
                            //Retornar PDF
                            Response.Clear();
                            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", objArchivoRecursoEntity.NombreArchivo));
                            Response.ContentType = "application/pdf";
                            Response.BinaryWrite(Convert.FromBase64String(objArchivoRecursoEntity.Archivo));
                            Response.End();

                        }
                        else
                            throw new Exception("No se encontro archivo para retornar");
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Presentar_Recurso :: imgDescargarDocumentoANLA_Click -> Error Inesperado descargando documento desde la ANLA: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }


            /// <summary>
            /// Evento que muestra modal de presentación de recurso
            /// </summary>
            protected void imgInterponerRecurso_Click(object sender, ImageClickEventArgs e)
            {
                int intFilaSeleccionada = 0;

                try
                {                    
                    //Cargar la fila seleccionada
                    intFilaSeleccionada = Convert.ToInt32(((ImageButton)sender).CommandArgument);

                    //Cargar datos a modal de recurso
                    this.ctrRegistrarRecursoReposicion.UsuarioID = this._strUsuarioID;
                    this.ctrRegistrarRecursoReposicion.AutoridadID = Convert.ToInt32(this.grdActosRecursos.DataKeys[intFilaSeleccionada].Values[3]);
                    this.ctrRegistrarRecursoReposicion.NumeroIdentificacionPersona = this.grdActosRecursos.DataKeys[intFilaSeleccionada].Values[6].ToString();
                    this.ctrRegistrarRecursoReposicion.ActoAdministrativoID = Convert.ToInt64(this.grdActosRecursos.DataKeys[intFilaSeleccionada].Values[0]);
                    this.ctrRegistrarRecursoReposicion.PersonaID = Convert.ToInt64(this.grdActosRecursos.DataKeys[intFilaSeleccionada].Values[1]);
                    this.ctrRegistrarRecursoReposicion.FlujoEstadoAvanzarID = Convert.ToInt32(this.grdActosRecursos.DataKeys[intFilaSeleccionada].Values[5]);
                    this.ctrRegistrarRecursoReposicion.EstadoAvanzarID = Convert.ToInt32(this.grdActosRecursos.DataKeys[intFilaSeleccionada].Values[4]);
                    this.ctrRegistrarRecursoReposicion.EstadoPersonaActoID = Convert.ToInt32(this.grdActosRecursos.DataKeys[intFilaSeleccionada].Values[7]);
                    if (Convert.ToInt32(this.grdActosRecursos.DataKeys[intFilaSeleccionada].Values[3]) == (int)AutoridadesAmbientales.ANLA)
                        this.ctrRegistrarRecursoReposicion.FechaNotificacion = Convert.ToDateTime(((Literal)this.grdActosRecursos.Rows[intFilaSeleccionada].FindControl("ltlFechaNotificacion")).Text);
                    this.ctrRegistrarRecursoReposicion.SolicitarSegundaClave = false;

                    //Cargar datos de control
                    this.ctrRegistrarRecursoReposicion.SolicitarInformacionRecurso();

                    //Mostrar modal
                    this.mpeModalPresentarRecurso.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Presentar_Recurso :: imgInterponerRecurso_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cargando información para presentación de recurso de reposición");
                }
            }

        #endregion


        #region cmdMostrarTodos

            /// <summary>
            /// Evento que muestra toda la información de consulta sin paginación
            /// </summary>
            protected void cmdMostrarTodos_Click(object sender, EventArgs e)
            {
                //Inicializar grilla
                this.grdActosRecursos.PageIndex = 0;
                this.grdActosRecursos.AllowPaging = false;

                //Ejecutar consulta
                this.BuscarInformacionActosAdministrativosRecurso();

                //Mostrar botones                
                this.cmdMostrarTodos.Visible = false;
                this.cmdMostrarPaginado.Visible = true;

                //Actualizar panel
                this.upnlConsultaRecursos.Update();
            }

        #endregion


        #region cmdMostrarPaginado

            /// <summary>
            /// Evento que muestra información de consulta con paginación
            /// </summary>
            protected void cmdMostrarPaginado_Click(object sender, EventArgs e)
            {
                //Inicializar grilla
                this.grdActosRecursos.PageIndex = 0;
                this.grdActosRecursos.AllowPaging = true;
                this.grdActosRecursos.PageSize = TAMANO_PAGINA;

                //Ejecutar consulta
                this.BuscarInformacionActosAdministrativosRecurso();

                //Mostrar botones de acuerdo a resultado de la busqueda
                if (this.grdActosRecursos.PageCount > 1)
                {
                    this.cmdMostrarTodos.Visible = true;
                    this.cmdMostrarPaginado.Visible = false;
                }
                else
                {
                    this.cmdMostrarTodos.Visible = false;
                    this.cmdMostrarPaginado.Visible = false;
                }

                //Actualizar panel
                this.upnlConsultaRecursos.Update();
            }

        #endregion


        #region ModalMensajeOk

            /// <summary>
            /// Evento que limpia y cierra el modal de mensajes de ok
            /// </summary>
            protected void cmdAceptarModalMensajeOk_Click(object sender, EventArgs e)
            {
                try
                {
                    //Limpiar variables
                    this.LimpiarModalMensajesOk();

                    //Actualizar panel y mostrar modal
                    this.upnlModalMensajeOk.Update();
                    this.mpeModalMensajeOk.Hide();

                    //Inicializar grilla
                    this.grdActosRecursos.PageIndex = 0;

                    //Ejecutar consulta
                    this.BuscarInformacionActosAdministrativosRecurso();
                }
                catch(Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Presentar_Recurso :: cmdAceptarModalMensajeOk_Click -> Se presento error cerrando y limpiando modal de ok: " + exc.Message + " - " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error cerrando y limpiando modal de ok");
                }
            }

        #endregion


        #region ModalPresentarRecurso


            #region cmdEnviarPresentarRecurso

                /// <summary>
                /// Evento que realiza el envío del recurso de reposición
                /// </summary>
                protected void cmdEnviarPresentarRecurso_Click(object sender, EventArgs e)
                {
                    string strNumeroVital = "";

                    try
                    {
                        //Verificar que la pagina sea valida
                        if (Page.IsValid)
                        {
                            //Registrar informacion de recurso de reposición
                            strNumeroVital = this.ctrRegistrarRecursoReposicion.RegistrarRecursoReposicion(Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue));

                            //Verificar que se obtenga el número vital
                            if (!string.IsNullOrWhiteSpace(strNumeroVital))
                            {
                                //Cerrar modal
                                this.mpeModalPresentarRecurso.Hide();

                                //Recargar listado
                                this.grdActosRecursos.PageIndex = 0;
                                this.BuscarInformacionActosAdministrativosRecurso();

                                //Mostrar modal de acción ok
                                this.LimpiarModalMensajesOk();
                                this.ltlTituloModalMensajeOk.Text = "PRESENTAR RECURSO DE REPOSICIÓN";
                                this.ltlMensajeModalMensajeOk.Text = "Se registro de manera correcta el recurso de reposición presentado con número vital <b>" + strNumeroVital + "</b>. Para continuar haga clic en el botón de \"Aceptar\".";
                                this.upnlModalMensajeOk.Update();
                                this.mpeModalMensajeOk.Show();
                            }
                            else
                            {
                                //Cancelar proceso de presentación de recurso
                                this.ctrRegistrarRecursoReposicion.CancelarProcesoPresentarRecurso();

                                //Cerrar modal
                                this.mpeModalPresentarRecurso.Hide();

                                //Recargar listado
                                this.grdActosRecursos.PageIndex = 0;
                                this.BuscarInformacionActosAdministrativosRecurso();
                            }
                            
                        }
                    }
                    catch (Exception exc)
                    {
                        //Cancelar proceso de presentación de recurso
                        this.ctrRegistrarRecursoReposicion.CancelarProcesoPresentarRecurso();

                        //Cerrar modal
                        this.mpeModalPresentarRecurso.Hide();

                        //Recargar listado
                        this.grdActosRecursos.PageIndex = 0;
                        this.BuscarInformacionActosAdministrativosRecurso();

                        //Mostrar error
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error enviando registro de recurso de reposición')", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Presentar_Recurso :: cmdEnviarPresentarRecurso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                }

            #endregion


            #region cmdCancelarPresentarRecurso

                /// <summary>
                /// Evento que cancel ael proceso de presentación del recurso de reposición
                /// </summary>
                protected void cmdCancelarPresentarRecurso_Click(object sender, EventArgs e)
                {
                    try
                    {                                    
                        //Cancelar proceso depresentación de recurso
                        this.ctrRegistrarRecursoReposicion.CancelarProcesoPresentarRecurso();

                        //Cerrar modal
                        this.mpeModalPresentarRecurso.Hide();
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento cancelando proceso presentacion de recurso')", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Presentar_Recurso :: cmdCancelarPresentarRecurso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                }

            #endregion


        #endregion

    #endregion

                
}
