using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.Comun;
using SILPA.LogicaNegocio.Generico;
using System.Threading;
using System.Globalization;
using SILPA.AccesoDatos.Generico;
using System.Configuration;

public partial class PDV_CambiarSegundaClave : System.Web.UI.Page
{

    #region Metodos Privados

        /// <summary>
        /// Ocultar mensaje mostrado
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.rowMensaje.Visible = false;
        }

        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.rowMensaje.Visible = true;
        }


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

            Session["Usuario"] = Session["IDForToken"];

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
        /// Inicializa información de la pagina
        /// </summary>
        private void InicializarPagina()
        {
            Persona objPersona = new Persona();

            try
            {
                //Crear objeto interfaz datos
                objPersona = new Persona();

                //Verificar si tiene segunda clave
                if (objPersona.TieneSegundaClave(int.Parse(Session["Usuario"].ToString())))
                {
                    this.rowContrasena.Visible = true;
                }
                else
                {
                    this.rowContrasena.Visible = false;
                }

                //Ocultar mensaje
                this.rowMensaje.Visible = false;

                //Cargar en hidden si origen es de pagina
                this.hdfOrigen.Value = (!string.IsNullOrWhiteSpace(Request.Params["r"]) ? Request.Params["r"] : "");

                //Mostrar boton de retorno
                if (!string.IsNullOrWhiteSpace(this.hdfOrigen.Value) && this.hdfOrigen.Value == "p")
                {
                    this.cmdVolver.Visible = true;
                }
                else
                {
                    this.cmdVolver.Visible = false;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PDV_CambiarSegundaClave :: InicializarPagina -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error cargando la información inicial de la página. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
            }
        }


        /// <summary>
        /// Obtener el mensaje a mostrar según el codigo de respuesta obtenido al momento del cambio
        /// </summary>
        /// <param name="p_intCodigoRespuesta">int con el codigo de respuesta</param>
        /// <returns>string con el mensaje</returns>
        private string ObtenerMensajeCambioSegundaClave(int p_intCodigoRespuesta)
        {
            string strMensaje = "";
            string strIdioma = "es";

            //Cargar idioma si es casa matriz
            if (this.EsCasaMatriz())
                strIdioma = this.ObtenerIdioma();

            //Cargar mensaje segun codigo
            if (p_intCodigoRespuesta == 0)
            {
                //Verificar si se había especificado contraseña anterior
                if (this.rowContrasena.Visible)
                {
                    if (strIdioma == "es")
                        strMensaje = "La contraseña fue cambiada exitosamente.";
                    else if (strIdioma == "en")
                        strMensaje = "Change of password is successful.";
                    else
                        strMensaje = "La contraseña fue cambiada exitosamente / Change of password is successful.";
                }
                else
                {
                    if (strIdioma == "es")
                        strMensaje = "La contraseña fue creada exitosamente.";
                    else if (strIdioma == "en")
                        strMensaje = "Password was created successfully.";
                    else
                        strMensaje = "La contraseña fue creada exitosamente. / Password was created successfully.";
                }
            }
            else if (p_intCodigoRespuesta == 1)
            {
                if (strIdioma == "es")
                    strMensaje = "No se especifico la contraseña anterior.";
                else if (strIdioma == "en")
                    strMensaje = "Previous password was not entered.";
                else
                    strMensaje = "No se especifico la contraseña anterior / Previous password was not entered.";                
            }
            else if (p_intCodigoRespuesta == 2)
            {
                if (strIdioma == "es")
                    strMensaje = "La contraseña anterior no es correcta.";
                else if (strIdioma == "en")
                    strMensaje = "Previous password is not correct.";
                else
                    strMensaje = "La contraseña anterior no es correcta / Previous password is not correct.";
            }
            else if (p_intCodigoRespuesta == 3)
            {
                if (strIdioma == "es")
                    strMensaje = "La contraseña anterior y la nueva son iguales.";
                else if (strIdioma == "en")
                    strMensaje = "Previous and new passwords are the same.";
                else
                    strMensaje = "La contraseña anterior y la nueva son iguales / Previous and new passwords are the same.";
            }

            return strMensaje;
        }


        /// <summary>
        /// Inicializa información de la pagina
        /// </summary>
        private void CambiarContrasena()
        {
            Persona objPersona = new Persona();
            int intRespuesta = 0;

            try
            {
                //Verificar que se hallan especificado los datos de las claves
                if (this.rowContrasena.Visible && string.IsNullOrWhiteSpace(this.txtContrasena.Text))
                {
                    if (this.EsCasaMatriz())
                    {
                        if (this.ObtenerIdioma() == "es")
                            this.MostrarMensaje("No se especificó la contraseña anterior.");
                        else if (this.ObtenerIdioma() == "en")
                            this.MostrarMensaje("Previous password was not entered.");
                        else
                            this.MostrarMensaje("No se especificó la contraseña anterior / Previous password was not entered.");
                    }
                    else
                    {
                        this.MostrarMensaje("No se especificó la contraseña anterior");
                    }
                }
                else
                {
                    //Verificar que se halla especificado la nueva contraseña
                    if (string.IsNullOrWhiteSpace(this.txtNuevaContrasena.Text))
                    {
                        if (this.EsCasaMatriz())
                        {
                            if (this.ObtenerIdioma() == "es")
                                this.MostrarMensaje("No se especificó la nueva contraseña.");
                            else if (this.ObtenerIdioma() == "en")
                                this.MostrarMensaje("New password was not entered.");
                            else
                                this.MostrarMensaje("No se especificó la nueva contraseña / New password was not entered.");
                        }
                        else
                        {
                            this.MostrarMensaje("No se especificó la nueva contraseña");
                        }
                    }
                    else
                    {
                        //Crear objeto interfaz datos
                        objPersona = new Persona();

                        //Realizar el cambio de la contraseña
                        intRespuesta = objPersona.CambiarSegundaClave(int.Parse(Session["Usuario"].ToString()), EnDecript.Encriptar(this.txtContrasena.Text.Trim()), EnDecript.Encriptar(this.txtNuevaContrasena.Text.Trim()));
                        
                        //Mostrar el mensaje de respuesta
                        if (intRespuesta == 0)
                        {
                            this.ltlResultado.Text = this.ObtenerMensajeCambioSegundaClave(intRespuesta);
                        }
                        else
                        {
                            this.MostrarMensaje(this.ObtenerMensajeCambioSegundaClave(intRespuesta));
                        }
                    }

                    //Limpiar los campos
                    this.txtContrasena.Text = "";
                    this.txtNuevaContrasena.Text = "";
                    this.txtConfirmacionContrasena.Text = "";

                    //Mostrar mensaje de respuesta
                    if (intRespuesta == 0)
                    {
                        this.divRespuesta.Visible = true;
                        this.divFormulario.Visible = false;
                    }
                    else
                    {
                        this.divRespuesta.Visible = false;
                        this.divFormulario.Visible = true;
                    }
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PDV_CambiarSegundaClave :: CambiarContrasena -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error cambiando la información de la contraseña. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
            }
        }


        /// <summary>
        /// Indica si el grupo del usuario pertenece a una casa matriz
        /// </summary>
        /// <returns>bool con true en caso de que el usuario sea una casa matriz</returns>
        private bool EsCasaMatriz()
        {
            string[] lstGrupos = null;

            //Cargar el listado de grupos
            lstGrupos = DatosSesion.DatosUsuario.MenuAsociado.Split('-');

            return lstGrupos.Contains(ConfigurationManager.AppSettings["PDVRoleCasaMatriz"].ToString());
        }


        /// <summary>
        /// Obtener el idioma seleccionado
        /// </summary>
        /// <returns>string con el idioma</returns>
        private string ObtenerIdioma()
        {
            //DropDownList objIdioma = null;
            string strIdioma = "";

            //Cargar el idioma
            if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("cboIdioma"))
            {
                strIdioma = Request.Form[Request.Form["__EVENTTARGET"]];
            }
            else if (Session["Idioma"] != null)
            {
                strIdioma = Session["Idioma"].ToString();
            }


            return strIdioma;
        }

    #endregion


    #region Eventos


        #region Page

            /// <summary>
            /// Evento que se ejecuta antes de cargar la pagina
            /// </summary>            
            protected void Page_PreInit(object sender, EventArgs e)
            {
                CultureInfo objCultura = null;
                string strIdioma = "";
                string menu = "";

                //Cargar menu
                menu = Request.Params["m"];
                if (string.IsNullOrWhiteSpace(menu)) menu = "";


                //Verificar si es menu vacio
                if (menu != "0")
                {
                    //Si es casa matriz mostrar con idioma
                    if (this.EsCasaMatriz())
                    {
                        this.MasterPageFile = "~/plantillas/SILPAIdioma.master";

                        //Cargar el idioma
                        strIdioma = this.ObtenerIdioma();

                        objCultura = new CultureInfo(strIdioma);
                        Thread.CurrentThread.CurrentCulture = objCultura;
                        Thread.CurrentThread.CurrentUICulture = objCultura;
                        this.InitializeCulture();

                    }
                    else
                    {
                        this.MasterPageFile = "~/plantillas/SILPA.master";

                        objCultura = new CultureInfo("es");
                        Thread.CurrentThread.CurrentCulture = objCultura;
                        Thread.CurrentThread.CurrentUICulture = objCultura;
                        this.InitializeCulture();
                    }
                }
                else
                {
                    this.MasterPageFile = "~/plantillas/SILPASinMenuFlash.master";

                    objCultura = new CultureInfo("es");
                    Thread.CurrentThread.CurrentCulture = objCultura;
                    Thread.CurrentThread.CurrentUICulture = objCultura;
                    this.InitializeCulture();
                }
            }


            /// <summary>
            /// Evento que se ejecuta al cargar la página
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {

                if (!IsPostBack || (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("cboIdioma")))
                {
                    //Session["Usuario"] = "15449";
                    //Session["Usuario"] = "15450";
                    //InicializarPagina();

                    //Validar usuario
                    if (this.ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                    else
                    {
                        //Iniciliazar datos de la página
                        InicializarPagina();
                    }
                }
            }


        #endregion


        #region CmdAceptar
            
            /// <summary>
            /// Evento que se ejecuta al dar clic en Aceptar. 
            /// Almacena la nueva clave
            /// </summary>
            protected void cmdAceptar_Click(object sender, EventArgs e)
            {
                //Inicializar Mensaje
                this.OcultarMensaje();

                //Realizar el cambio de la clave
                this.CambiarContrasena();
            }

        #endregion       


        #region cmdAceptarRespuesta
            
            /// <summary>
            /// Evento que se ejecuta al dar clic en Aceptar. 
            /// Almacena la nueva clave
            /// </summary>
            protected void cmdAceptarRespuesta_Click(object sender, EventArgs e)
            {
                string strPaginaRetorno = "";
                ParametroDalc objParametroDalc = null;
                ParametroEntity objParametro = null;

                //Cargar pagina retorno
                strPaginaRetorno = (Session["paginaRetorno"] != null ? Session["paginaRetorno"].ToString() : "");

                if (!string.IsNullOrWhiteSpace(this.hdfOrigen.Value) && this.hdfOrigen.Value == "p" && !string.IsNullOrWhiteSpace(strPaginaRetorno))
                {
                    //Redireccionar a pagina
                    Response.Redirect("../" + strPaginaRetorno);
                }
                else
                {
                    //Consultar pagina de retorno
                    objParametroDalc = new ParametroDalc();
                    objParametro = new ParametroEntity();
                    objParametro.IdParametro = -1;
                    objParametro.NombreParametro = "URL_INICIO_VITAL";
                    objParametroDalc.obtenerParametros(ref objParametro);

                    //Redireccionar a pagina
                    Response.Redirect(objParametro.Parametro);
                    
                }
            }
            

        #endregion


        #region cmdVolver

            /// <summary>
            /// Evento que se ejecuta al dar click en el botón de volver
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void cmdVolver_Click(object sender, EventArgs e)
            {
                string strPaginaRetorno = "";
                ParametroDalc objParametroDalc = null;
                ParametroEntity objParametro = null;

                //Cargar pagina retorno
                strPaginaRetorno = (Session["paginaRetorno"] != null ? Session["paginaRetorno"].ToString() : "");

                //Existe pagina retornar a esta sino volver a default
                if (!string.IsNullOrWhiteSpace(strPaginaRetorno))
                {
                    Response.Redirect("../" + strPaginaRetorno);
                }
                else
                {
                    //Consultar pagina de retorno
                    objParametroDalc = new ParametroDalc();
                    objParametro = new ParametroEntity();
                    objParametro.IdParametro = -1;
                    objParametro.NombreParametro = "URL_INICIO_VITAL";
                    objParametroDalc.obtenerParametros(ref objParametro);

                    //Redireccionar a pagina
                    Response.Redirect(objParametro.Parametro);
                }
            }

        #endregion

    #endregion




}