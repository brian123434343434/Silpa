using SILPA.AccesoDatos.DAA;
using SILPA.LogicaNegocio.DAA;
using SILPA.LogicaNegocio.Generico;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administracion_Tablasbasicas_ReasignarSolicitud : System.Web.UI.Page
{
    #region Objetos


        /// <summary>
        /// Identificador del solicitante
        /// </summary>
        private int SolicitanteID
        {
            get
            {
                return (int)ViewState["_intSolicitanteID"];
            }
            set
            {
                ViewState["_intSolicitanteID"] = value;
            }
        }

    #endregion

    
    #region Metodos Privados

        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.divMensaje.Visible = true;
            this.upnlMensaje.Update();
        }


        /// <summary>
        /// Ocultar los mensajes
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.divMensaje.Visible = false;
            this.upnlMensaje.Update();
        }


        /// <summary>
        /// Limpiar modal de cambio de estado
        /// </summary>
        private void LimpiarModalReasignarSolicitud()
        {
            //Limpiar desplgable
            this.cboAutoridadAmbiental.ClearSelection();
            this.cboAutoridadAmbiental.Items.Clear();
        }

        /// <summary>
        /// Limpiar los datos de la solicitud
        /// </summary>
        private void LimpiarDatosSolicitud()
        {
            //Ocultar div de consulta
            this.dvNoExisteDatos.Visible = false;
            this.dvDatosSolicitud.Visible = false;

            //Inicializar grilla de consulta
            this.ltlSolicitudID.Text = "";
            this.ltlAutoridadAmbiental.Text = "";
            this.ltlNumeroVital.Text = "";
            this.ltlTipoTramite.Text = "";
            this.ltlSolicitante.Text = "";
            this.ltlFechaSolicitud.Text = "";
            this.hdfAutoridadID.Value = "";
            this.hdfNumeroVital.Value = "";
        }

        /// <summary>
        /// Cargar la información inicial de la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Ocultar mensaje
            this.OcultarMensaje();

            //Limpiar campo de busqueda
            this.txtNumeroVital.Text = "";

            //Limpiar datos de la solicitud
            this.LimpiarDatosSolicitud();

            //Limpiar modales
            this.LimpiarModalReasignarSolicitud();
        }


        /// <summary>
        /// Verificar si se encuentra autenticado el usuario
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            string[] strLstRoles;

            if (DatosSesion.Usuario == string.Empty)
            {
                return false;
            }

            string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

            Session["IDForToken"] = (object)idUsuario;

            this.SolicitanteID = Convert.ToInt32(Session["IDForToken"]);

            SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

            Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));

            using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
            {
                servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
                string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());

                if (mensaje.IndexOf("Token invalido") > 0)
                    return false;
            }

            //TODO Incluir si solo se requiere en el admninstrador 
            //Verificar que el usuario sea un administrador
            //strLstRoles = DatosSesion.DatosUsuario.MenuAsociado.Split('-');
            //if (strLstRoles.Length > 0 && strLstRoles.Where(role => role == "1").ToList().Count > 0)
            //    return true;
            //else
            //    return false;

            return true;
        }

        /// <summary>
        /// Obtiene la información de la solicitud asociada a un numero vital
        /// </summary>
        private void ObtenerSolicitud()
        {
            //Obtener la informacion de la solicitud
            DAA objDAA = new DAA();
            DAASolicitudEntity objDAASolicitudEntity = objDAA.ObtenerSolicitudNumeroVITAL(this.hdfNumeroVital.Value);

            //Cargar la informacion en pagina
            this.LimpiarDatosSolicitud();
            if (objDAASolicitudEntity != null && objDAASolicitudEntity.SolicitudID > 0)
            {
                //Cargar datos de la solicitud
                this.ltlSolicitudID.Text = objDAASolicitudEntity.SolicitudID.ToString();
                this.hdfAutoridadID.Value = objDAASolicitudEntity.AutoridadID.ToString();
                this.ltlAutoridadAmbiental.Text = objDAASolicitudEntity.Autoridad;
                this.hdfNumeroVital.Value = objDAASolicitudEntity.NumeroVITAL;
                this.ltlNumeroVital.Text = objDAASolicitudEntity.NumeroVITAL;
                this.ltlTipoTramite.Text = objDAASolicitudEntity.TipoTramite;
                this.ltlSolicitante.Text = objDAASolicitudEntity.Solicitante;
                this.ltlFechaSolicitud.Text = objDAASolicitudEntity.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
                this.dvDatosSolicitud.Visible = true;
            }
            else
            {
                this.dvNoExisteDatos.Visible = true;
            }
        }

        /// <summary>
        /// Carga listado de autoridades ambientales en el listado.
        /// </summary>
        /// <param name="p_objListado">DropDownList con el listado donde se cargaran las autoridades ambientales</param>
        /// <param name="p_strAutoridadQuitar">string con el identificador de la autoridad ambiental que no se incluira en el listado. Opcional.</param>
        private void CargarListadoAutoridades(DropDownList p_objListado, string p_strAutoridadIDQuitar = "")
        {
            AutoridadAmbiental objAutoridadAmbiental = new AutoridadAmbiental();
            DataSet objAutoridades = objAutoridadAmbiental.ListarAutoridadAmbiental(null);
            objAutoridades.Tables[0].DefaultView.Sort = "AUT_DESCRIPCION ASC"; 
            p_objListado.ClearSelection();
            p_objListado.Items.Clear();
            p_objListado.DataSource = objAutoridades.Tables[0].DefaultView;
            p_objListado.DataValueField = "AUT_ID";
            p_objListado.DataTextField = "AUT_DESCRIPCION";
            p_objListado.DataBind();
            if (!string.IsNullOrWhiteSpace(p_strAutoridadIDQuitar))
                p_objListado.Items.Remove(p_objListado.Items.FindByValue(p_strAutoridadIDQuitar));
            p_objListado.Items.Insert(0, new ListItem("Seleccione .. ", "-1"));
        }


        /// <summary>
        /// Realizar la reasignación de la solicitud
        /// </summary>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental a la cual se realiza la reasignacion</param>
        /// <param name="p_strNumeroVITAL">string con  el número VITAL</param>
        private void ReasignarSolicitud(int p_intAutoridadID, string p_strNumeroVITAL)
        {
            DAA objDAA = new DAA();

            //Realizar la reasignación
            objDAA.ReasignarSolicitud(p_strNumeroVITAL, p_intAutoridadID, p_SolicitanteID: this.SolicitanteID);
        }

    #endregion


    #region Eventos


        #region Page

            /// <summary>
            /// Evento que se ejecuta al realizar el cargue de la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                //this.SolicitanteID = 1;

                try
                {
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
                            //Iniciliazar datos de la página
                            this.InicializarPagina();
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Tablasbasicas_ReasignarSolicitud :: Page_Load -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un problema cargando la información de la página");
                }
            }

        #endregion


        #region cmdBuscar

            /// <summary>
            /// Evento que realiza la busqueda de información de los actos administrativos
            /// </summary>
            protected void cmdBuscar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cargar infromacion
                    this.hdfNumeroVital.Value = this.txtNumeroVital.Text.Trim();

                    //Realizar la busqueda
                    this.ObtenerSolicitud();

                    //Actualizar panel
                    this.upnlConsultaSolicitudes.Update();
                }
                catch(Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Tablasbasicas_ReasignarSolicitud :: cmdBuscar_Click -> Error realizando la búsqueda de información: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un problema consultando la información de la solicitud");
                }

            }

        #endregion


        #region cmdLimpiar

            /// <summary>
            /// Evento que limpia toda la  página
            /// </summary>
            protected void cmdLimpiar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Inicializar la pagina
                    this.InicializarPagina();

                    //Actualizar paneles
                    this.upnlBuscar.Update();
                    this.upnlConsultaSolicitudes.Update();
                }
                catch(Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Tablasbasicas_ReasignarSolicitud :: cmdLimpiar_Click -> Error inicializando la página: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un problema limpiando la página");
                }

            }

        #endregion


        #region cmdReasignar

            /// <summary>
            /// Evento que muestra el modal para realizar la reasignación
            /// </summary>
            protected void cmdReasignar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Inicializar el modal
                    this.LimpiarModalReasignarSolicitud();

                    this.CargarListadoAutoridades(this.cboAutoridadAmbiental, this.hdfAutoridadID.Value);

                    //Actualizar el modal
                    this.upnlModalReasignarSolicitud.Update();
                    this.mpeModalReasignarSolicitud.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Tablasbasicas_ReasignarSolicitud :: cmdReasignar_Click -> Error Inesperado: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un problema cargando la información de las autoridades ambientales a las cuales se puede asignar la solicitud.");
                }
            }

        #endregion


        #region Modal Reasignacion

            /// <summary>
            /// Evento que realiza la reasignación de la solicitud
            /// </summary>
            protected void cmdModalReasignarSolicitudAceptar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Reasignar Solicitud
                    this.ReasignarSolicitud(Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue), this.hdfNumeroVital.Value);

                    //Realizar la busqueda
                    this.ObtenerSolicitud();

                    //Actualizar panel
                    this.upnlConsultaSolicitudes.Update();

                    //Abrir modal de confirmacion
                    this.mpeReasignacionCorrecta.Show();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Tablasbasicas_ReasignarSolicitud :: cmdModalReasignarSolicitudAceptar_Click -> Error realizando la reasignación: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un problema realizando la reasignación de la solicitud.");
                }
                finally
                {
                    try
                    {
                        //Inicializar el modal
                        this.LimpiarModalReasignarSolicitud();

                        //Actualizar el modal
                        this.upnlModalReasignarSolicitud.Update();
                        this.mpeModalReasignarSolicitud.Hide();
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Administracion_Tablasbasicas_ReasignarSolicitud :: cmdModalReasignarSolicitudAceptar_Click -> Error cerrando modal: " + exc.StackTrace);

                        //Cargar mensaje de error                        
                        this.MostrarMensaje("Se genero un problema cargando cerrando el modal de reasignación.");
                    }
                }
            }


            /// <summary>
            /// Evento que cierra el modal de reasignación
            /// </summary>
            protected void cmdModalReasignarSolicitudCancelar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Inicializar el modal
                    this.LimpiarModalReasignarSolicitud();

                    //Actualizar el modal
                    this.upnlModalReasignarSolicitud.Update();
                    this.mpeModalReasignarSolicitud.Hide();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Tablasbasicas_ReasignarSolicitud :: cmdModalReasignarSolicitudCancelar_Click -> Error cerrando modal: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un problema cargando cerrando el modal de reasignación.");
                }
            }

            /// <summary>
            /// Cierra el modal de reasignación
            /// </summary>
            protected void cmdAceptarReasignacionCorrecta_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Cerrar modal de confirmacion
                    this.mpeReasignacionCorrecta.Hide();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Tablasbasicas_ReasignarSolicitud :: cmdAceptarReasignacionCorrecta_Click -> Error cerrando modal: " + exc.StackTrace);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un problema cerrando el modal de confirmación.");
                }
            }

        #endregion

    #endregion

        
}