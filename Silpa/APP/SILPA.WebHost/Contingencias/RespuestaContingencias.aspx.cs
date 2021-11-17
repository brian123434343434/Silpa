using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;

public partial class Contingencias_RespuestaContingencias : System.Web.UI.Page
{
  
    #region Propiedades

        /// <summary>
        /// Listado de permisos de la solicitud
        /// </summary>
        private int SolicitanteID
        {
            get
            {
                return (int)ViewState["_intSolicitanteID_Contingencias_RespuestaContingencias"];
            }
            set
            {
                ViewState["_intSolicitanteID_Contingencias_RespuestaContingencias"] = value;
            }
        }


        /// <summary>
        /// Numero Vital
        /// </summary>
        private string NumeroVital
        {
            get
            {
                return (Session["Contingencias_NumeroVital"] != null ? Session["Contingencias_NumeroVital"].ToString() : "");
            }
            set
            {
                Session["Contingencias_NumeroVital"] = value;
            }
        }

        /// <summary>
        /// Autoridad Ambiental
        /// </summary>
        private string AutoridadAmbiental
        {
            get
            {
                return (Session["Contingencias_Autoridad_Ambiental"] != null ? Session["Contingencias_Autoridad_Ambiental"].ToString() : "");
            }
            set
            {
                Session["Contingencias_Autoridad_Ambiental"] = value;
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


        #region Datos Formulario


            /// <summary>
            /// Cargar información de respuesta en la página
            /// </summary>
            private void CargarPagina()
            {
                //Verificar que se tenga datos de tipo de respuesta y número vital
                if (!string.IsNullOrWhiteSpace(this.NumeroVital) && !string.IsNullOrWhiteSpace(this.AutoridadAmbiental))
                {
                    this.ltlRespuestaSolicitud.Text = "Su reporte inicial de contingencias ha sido enviado a la autoridad ambiental <b>" + this.AutoridadAmbiental + "</b>, mediante número de seguimiento VITAL <b />" + this.NumeroVital + "</b>.";
                }
                else
                {
                    throw new Exception("No se tiene información de tipo de autoridad ambiental y/o número vital para poder mostrar la respuesta");
                }
            }


        #endregion


    #endregion


    #region Eventos

        #region Formulario

            #region Page

                /// <summary>
                /// Evento que se ejecuta al cargar la pagina
                /// </summary>
                protected void Page_Load(object sender, EventArgs e)
                {
                    //this.SolicitanteID = 24627;
                    //this.SolicitanteID = 429;
                    //this.NumeroVital = "5654646546465";
                    //this.AutoridadAmbiental = "ANLA";

                    try
                    {
                        if (!IsPostBack)
                        {
                            //Ocultar mensajes de error
                            this.OcultarMensaje();

                            //Inicializar pagina
                            //this.CargarPagina();

                            //Validar sesion de usuario
                            if (this.ValidacionToken() == false)
                            {
                                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                            }
                            else
                            {
                                //Iniciliazar datos de la página
                                this.CargarPagina();
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error cargando respuesta de solicitud de contingencias.", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Contingencias_RespuestaContingencias :: Page_Load -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    }
                }

            #endregion  


            #region cmdAceptar

                protected void cmdAceptar_Click(object sender, EventArgs e)
                {
                    try
                    {

                        Response.Redirect("FormularioContingencias.aspx", false);

                    }
                    catch (Exception exc)
                    {
                        //MOstrar mensaje de error en pantalla
                        this.MostrarMensajeError("Se presento error direccionando a página de solicitud.", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Contingencias_RespuestaContingencias :: cmdAceptar_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                    finally
                    {
                        //Limpiar sesion
                        this.AutoridadAmbiental = null;
                        this.NumeroVital = null;
                    }
                }

            #endregion

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
                    Response.Redirect("FormularioCambioMenores.aspx", false);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensajeError("Se presento error cerrando el modal de errores.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "CambiosMenores_FormularioCambioMenores :: cmdAceptarErrorProceso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Limpiar sesion
                    this.AutoridadAmbiental = null;
                    this.NumeroVital = null;
                }
            }

        #endregion


    #endregion



}