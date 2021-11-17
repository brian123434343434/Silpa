using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.Comun;
using SILPA.LogicaNegocio.IntegracionCorporaciones;
using SILPA.LogicaNegocio.IntegracionCorporaciones.Entidades;
using SoftManagement.Log;

public partial class Corporaciones_FormaCorporacion : System.Web.UI.Page
{

    #region Propiedades

        /// <summary>
        /// Identificador del solicitante
        /// </summary>
        private int SolicitanteID
        {
            get
            {
                return (int)ViewState["Corporaciones_FormaCorporacion_SolicitanteID"];
            }
            set
            {
                ViewState["Corporaciones_FormaCorporacion_SolicitanteID"] = value;
            }
        }


        /// <summary>
        /// Identificador de la autoridad ambiental
        /// </summary>
        private int AutoridadID
        {
            get
            {
                return (int)ViewState["Corporaciones_FormaCorporacion_AutoridadID"];
            }
            set
            {
                ViewState["Corporaciones_FormaCorporacion_AutoridadID"] = value;
            }
        }


        /// <summary>
        /// Identificador de la opcion de menu
        /// </summary>
        private int OpcionID
        {
            get
            {
                return (int)ViewState["Corporaciones_FormaCorporacion_OpcionID"];
            }
            set
            {
                ViewState["Corporaciones_FormaCorporacion_OpcionID"] = value;
            }
        }


        /// <summary>
        /// Identificador del proceso que se esta realizando
        /// </summary>
        private string SessionRemotaWebID
        {
            get
            {
                return (string)ViewState["Corporaciones_FormaCorporacion_SessionRemotaWebID"];
            }
            set
            {
                ViewState["Corporaciones_FormaCorporacion_SessionRemotaWebID"] = value;
            }
        }


        /// <summary>
        /// Identificador de la sesion local
        /// </summary>
        private string SessionWebID
        {
            get
            {
                return (string)ViewState["Corporaciones_FormaCorporacion_SessionWebID"];
            }
            set
            {
                ViewState["Corporaciones_FormaCorporacion_SessionWebID"] = value;
            }
        }


    #endregion


    #region Metodos Privados

        #region Seguridad

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

                this.SolicitanteID = Convert.ToInt32(Session["IDForToken"]);

                this.SessionWebID = Session.SessionID;

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

        #endregion


        #region Manejo Errores

            /// <summary>
            /// Mostrar el mensaje de error especificado
            /// </summary>
            /// <param name="p_strMensaje">string con el mensaje</param>
            /// <param name="p_blnMensajeSincronico">Indica si se maneja como modal sincronico. Opcional</param>
            private void MostrarMensajeError(string p_strMensaje, bool p_blnMensajeSincronico = false)
            {
                this.mpeErrorProceso.Show();
                this.ltlErrorProceso.Text = p_strMensaje;
                this.cmdAceptarErrorProceso.Visible = !p_blnMensajeSincronico;
                this.cmdAceptarErrorProcesoSincronico.Visible = p_blnMensajeSincronico;
                this.upnlErrorProceso.Update();
            }


            /// <summary>
            /// Ocultar los mensajes
            /// </summary>
            private void OcultarMensaje()
            {
                this.mpeErrorProceso.Hide();
                this.ltlErrorProceso.Text = "";
                this.upnlErrorProceso.Update();
            }

        #endregion


        #region Formulario

            /// <summary>
            /// Obtiene la direccion de IP de acceso de un usuario
            /// </summary>
            /// <returns>string con la direccion IP de acceso</returns>
            private string ObtenerDireccionIP()
            {
                string strIp = "";
                string[] objLstDirecciones = null;

                //Obtener ip
                strIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                //Verificar si obtuvo direccion
                if (!string.IsNullOrEmpty(strIp))
                {
                    objLstDirecciones = strIp.Split(',');
                    if (objLstDirecciones.Length != 0)
                    {
                        strIp = objLstDirecciones[0];
                    }
                }
                else
                {
                    strIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                return strIp;
            }


            /// <summary>
            /// Cargar la informacion del query string que contiene autoridad y opcion
            /// </summary>
            private void CargarInformacionBase()
            {
                this.AutoridadID = (Request.QueryString["corp"] != null ? Convert.ToInt32(EnDecript.DesencriptarDesplazamiento(Request.QueryString["corp"].ToString())) : -1);
                this.OpcionID = (Request.QueryString["men"] != null ? Convert.ToInt32(EnDecript.DesencriptarDesplazamiento(Request.QueryString["men"].ToString())) : -1);

                //Verificar que se especificara informacion
                if (this.AutoridadID <= 0 || this.OpcionID <= 0)
                    throw new Exception("No se especifico infomracion de acceso");
            }


            /// <summary>
            /// Obtiene la url a la cual se debe direccionar el iframes
            /// </summary>
            private void CargarPaginaCorporacion()
            {
                FormaSolicitudAccesoWebEntity objForma = null;
                RespuestaSolicitudAccesoWebEntity objRespuesta = null;

                //Crear forma
                objForma = new FormaSolicitudAccesoWebEntity
                {
                    UsuarioID = this.SolicitanteID,
                    OpcionID = this.OpcionID,
                    SessionID = this.SessionWebID,
                    RolesUsuario = DatosSesion.DatosUsuario.MenuAsociado,
                    IPUsuario = this.ObtenerDireccionIP(),
                    URLRetorno = ConfigurationManager.AppSettings["IntegracionURLRetorno"].ToString()
                };

                //Crear sesion
                objRespuesta = IntegracionCorporacion.GetInstance().ObtenerAccesoPaginaWeb(this.AutoridadID, objForma);

                //Verficar que se obtenga respuesta
                if (objRespuesta != null)
                {
                    //Cargar identificador sesion remota
                    this.SessionRemotaWebID = objRespuesta.SessionWebID;

                    //Direccionar a la URL
                    Response.Redirect(objRespuesta.URLAcceso, false);
                    Response.End();
                }
                else
                {
                    throw new Exception("No se obtuvo acceso para pagina web");
                }
                
            }

            /// <summary>
            /// Carga la informacion de la pagina
            /// </summary>
            private void InicializarPagina()
            {
                //Cargar informacion de autoridad y opcion
                this.CargarInformacionBase();

                this.CargarPaginaCorporacion();
            }

        #endregion


    #endregion


    #region Eventos

        #region Page

            /// <summary>
            /// Eveto que se ejecuta al cargar la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                //this.SolicitanteID = 456;
                //this.SessionWebID = Session.SessionID;

                try
                {
                    if (!IsPostBack)
                    {
                        //Ocultar mensajes de error
                        this.OcultarMensaje();

                        //Inicializar pagina
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
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensajeError("Se presento error cargando información del formulario de la corporación.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Corporaciones_FormaCorporacion :: Page_Load -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                }

            }

        #endregion


        #region Modal Mensaje Error

            /// <summary>
            /// Evento que se encarga de cerrar modal cuando se presenta mensaje de error
            /// </summary>
            protected void cmdAceptarErrorProceso_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Redireccionar a la pagina
                    Response.Redirect(ConfigurationManager.AppSettings["URL_TESTSILPA"].ToString(), false);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensajeError("Se presento error cerrando el modal de errores.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Corporaciones_FormaCorporacion :: cmdAceptarErrorProceso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


    #endregion


    
}