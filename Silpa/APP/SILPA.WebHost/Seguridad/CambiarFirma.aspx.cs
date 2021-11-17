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
using SILPA.AccesoDatos.Generico;
using System.Threading;
using System.Globalization;
using System.Configuration;

public partial class PDV_CambiarFirma : System.Web.UI.Page
{

    #region Metodos Privados

        /// <summary>
        /// Ocultar mensaje mostrado
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.tableMensaje.Visible = false;
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


        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.tableMensaje.Visible = true;
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
            Persona objPersona = null;
            PersonaFirmaIdentity objFirma = null;
            PersonaLogoIdentity objLogo = null;

            try
            {
                //Crear objeto interfaz datos
                objPersona = new Persona();

                //Si tiene segunda clave cargar datos sino mostrar error
                if (objPersona.TieneSegundaClave(int.Parse(Session["Usuario"].ToString())))
                {
                    //Cargar
                    objFirma = objPersona.ConsultarFirma(int.Parse(Session["Usuario"].ToString()));

                    //Verificar si se obtuvo datos y cargarlos
                    if (objFirma != null)
                    {
                        this.txtNombre.Text = objFirma.Nombre;
                        this.txtCargo.Text = objFirma.Cargo;
                        this.imgFirma.ImageUrl = "ImagenFirma.aspx?Imagen=" + Session["Usuario"].ToString();
                        this.imgFirma.Visible = true;
                        this.rfvFirma.Visible = false;
                    }
                    else
                    {
                        this.txtNombre.Text = "";
                        this.imgFirma.Visible = false;
                        this.rfvFirma.Visible = true;
                    }

                    //Si es una casa matriz cargar el logo sino ocultarlo
                    if (EsCasaMatriz())
                    {
                        //Cargar Logo
                        objLogo = objPersona.ConsultarLogo(int.Parse(Session["Usuario"].ToString()));

                        //Verificar si se obtuvo datos y cargarlos
                        if (objLogo != null)
                        {
                            this.imgLogo.ImageUrl = "ImagenLogo.aspx?Imagen=" + Session["Usuario"].ToString();
                            this.imgLogo.Visible = true;
                            this.rfvLogo.Visible = false;
                        }
                        else
                        {
                            this.imgLogo.Visible = false;
                            this.rfvLogo.Visible = true;
                        }

                        //Mostrar campo de nombre
                        this.rowNombre.Visible = true;
                        this.txtNombre.Visible = true;
                        this.spnNombre.Visible = true;
                        this.rfvNombre.Visible = true;
                    }
                    else
                    {
                        this.rowLogoTitulo.Visible = false;
                        this.rowLogo.Visible = false;

                        //Mostrar campo de nombre
                        this.txtNombre.Visible = true;
                        this.spnNombre.Visible = true;
                        this.rfvNombre.Visible = true;

                        //Cargar label
                        this.lblTituloNombre.InnerText = "Nombre:";
                        this.lblTituloCargo.InnerText = "Cargo:";
                        this.lblTituloFirma.InnerText = "Imagen Firma:";
                    }

                    //Ocultar mensaje
                    this.tableMensaje.Visible = false;
                    this.tableFirma.Visible = true;
                }
                else
                {
                    //MOstrar mensaje de error
                    if (this.EsCasaMatriz())
                    {
                        if (this.ObtenerIdioma() == "es")
                            this.MostrarMensaje("Para registrar o modificar la firma debe haber registrado la segunda clave.<br /><br />Para registrar su segunda clave haga <a href='CambiarSegundaClave.aspx'>clic aquí.</a>");
                        else if (this.ObtenerIdioma() == "en")
                            this.MostrarMensaje("To register or modify current signature, the second password must be registered.<br /><br /><a href='CambiarSegundaClave.aspx'>Click here</a> to register second password.");
                        else
                            this.MostrarMensaje("Para registrar o modificar la firma debe haber registrado la segunda clave.<br /><br />Para registrar su segunda clave haga <a href='CambiarSegundaClave.aspx'>clic aquí.</a><br /><br /><br /><br />To register or modify current signature, the second password must be registered.<br /><br /><a href='CambiarSegundaClave.aspx'>Click here</a> to register second password.");
                    }
                    else
                    {
                        this.MostrarMensaje("Para registrar o modificar la firma debe haber registrado la segunda clave.<br /><br />Para registrar su segunda clave haga <a href='CambiarSegundaClave.aspx'>clic aquí.</a>");
                    }


                    this.tableFirma.Visible = false;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PDV_CambiarFirma :: InicializarPagina -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error consultando la información inical de la pagina. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
            }
        }


        /// <summary>
        /// Realiza el cambio de la firma
        /// </summary>
        private void CambiarFirma()
        {
            Persona objPersona = new Persona();
            PersonaFirmaIdentity objFirma = null;
            PersonaLogoIdentity objLogo = null;
            System.Drawing.Image objImagen = null;
            string strMensajeError = "";

            try
            {
                //Verificar que se hallan especificado los datos
                if (!string.IsNullOrWhiteSpace(this.txtContrasena.Text) && !string.IsNullOrWhiteSpace(this.txtCargo.Text) &&
                   ((!string.IsNullOrWhiteSpace(this.txtNombre.Text) && EsCasaMatriz()) || (!EsCasaMatriz())))
                {   

                    //Verificar que  la contraseña sea valida
                    if (objPersona.ValidarSegundaClave(int.Parse(Session["Usuario"].ToString()), EnDecript.Encriptar(this.txtContrasena.Text.Trim())))
                    {
                        //Cargar los datos
                        objFirma = new PersonaFirmaIdentity();
                        objFirma.UsuarioID = int.Parse(Session["Usuario"].ToString());
                        objFirma.Nombre = this.txtNombre.Text.Trim().ToUpper();
                        objFirma.Cargo = this.txtCargo.Text.Trim().ToUpper();
                        objFirma.NombreImagen = this.fluFirma.PostedFile.FileName;
                        objFirma.TipoImagen = this.fluFirma.PostedFile.ContentType;
                        objFirma.LongitudImagen = Convert.ToInt32(this.fluFirma.PostedFile.InputStream.Length);
                        objFirma.Imagen = new byte[objFirma.LongitudImagen];
                        this.fluFirma.PostedFile.InputStream.Read(objFirma.Imagen, 0, objFirma.LongitudImagen);

                        //Si contiene archivo validar que tenga tamaño correcto
                        if (objFirma.LongitudImagen > 0)
                        {
                            //Cargar imagen 
                            objImagen = System.Drawing.Image.FromStream(this.fluFirma.PostedFile.InputStream);

                            //Verificar que tenga el tamaño correcto
                            if (objImagen.PhysicalDimension.Width != 300 || objImagen.PhysicalDimension.Height != 60)
                            {
                                if (this.EsCasaMatriz())
                                {
                                    if (this.ObtenerIdioma() == "es")
                                        strMensajeError += "- La imagen de la firma debe tener una dimensión de 300 x 60 pixeles. <br />";
                                    else if (this.ObtenerIdioma() == "en")
                                        strMensajeError += "The size of the signature must be 300 x 60 pixels. <br />";
                                    else
                                        strMensajeError += "- La imagen de la firma debe tener una dimensión de 300 x 60 pixeles / The size of the signature must be 300 x 60 pixels. <br />";
                                }
                                else
                                {
                                    strMensajeError += "- La imagen de la firma debe tener una dimensión de 300 x 60 pixeles. <br />";
                                }
                            }
                        }

                        //Si es casa matriz cargar datos del logo
                        if(EsCasaMatriz())
                        {

                            //Cargar datos del logo
                            objLogo = new PersonaLogoIdentity();
                            objLogo.UsuarioID = int.Parse(Session["Usuario"].ToString());
                            objLogo.NombreLogo = this.fluLogo.PostedFile.FileName;
                            objLogo.TipoLogo = this.fluLogo.PostedFile.ContentType;
                            objLogo.LongitudLogo = Convert.ToInt32(this.fluLogo.PostedFile.InputStream.Length);
                            objLogo.Logo = new byte[objLogo.LongitudLogo];
                            this.fluLogo.PostedFile.InputStream.Read(objLogo.Logo, 0, objLogo.LongitudLogo);

                            //Si contiene archivo validar que tenga tamaño correcto
                            if (objLogo.LongitudLogo > 0)
                            {
                                //Cargar logo
                                objImagen = System.Drawing.Image.FromStream(this.fluLogo.PostedFile.InputStream);

                                //Verificar que tenga el tamaño correcto
                                if (objImagen.PhysicalDimension.Width != 255 || objImagen.PhysicalDimension.Height != 57)
                                {
                                    if (this.ObtenerIdioma() == "es")
                                        strMensajeError += "- La imagen del logo debe tener una dimensión de 255 x 57 pixeles.<br />";
                                    else if (this.ObtenerIdioma() == "en")
                                        strMensajeError += "- The size of the logo must be 255 x 57 pixels.<br />";
                                    else
                                        strMensajeError += "- La imagen del logo debe tener una dimensión de 255 x 57 pixeles / The size of the logo must be 255 x 57 pixels.<br />";
                                }
                            }
                        }

                        //verificar si existen mensajes de error
                        if (string.IsNullOrWhiteSpace(strMensajeError))
                        {
                            //Crear objeto interfaz datos
                            objPersona = new Persona();

                            //Realizar cambio
                            objPersona.CambiarFirma(objFirma);

                            //Guardar logo si es casa matriz
                            if (EsCasaMatriz() && objLogo != null && objLogo.LongitudLogo > 0)
                            {
                                objPersona.CambiarLogo(objLogo);
                            }

                            //INicializar la pagina
                            this.InicializarPagina();

                            //Moostrar mensaje
                            if (this.EsCasaMatriz())
                            {
                                if (this.ObtenerIdioma() == "es")
                                    this.MostrarMensaje("Se realizo el cambio de la firma de manera correcta.");
                                else if (this.ObtenerIdioma() == "en")
                                    this.MostrarMensaje("Change of signature is successful.");
                                else
                                    this.MostrarMensaje("Se realizo el cambio de la firma de manera correcta / Change of signature is successful.");
                            }
                            else
                            {
                                this.MostrarMensaje("Se realizo el cambio de la firma de manera correcta.");
                            }
                        }
                        else
                        {
                            this.MostrarMensaje(strMensajeError);
                        }

                    }
                    else
                    {
                        //Verificar si es casa matriz
                        if (this.EsCasaMatriz())
                        {
                            //Mostrar mensaje según idioma
                            if (this.ObtenerIdioma() == "es")
                                this.MostrarMensaje("La segunda clave se encuentra incorrecta.");
                            else if (this.ObtenerIdioma() == "en")
                                this.MostrarMensaje("Second password is incorrect.");
                            else
                                this.MostrarMensaje("La segunda clave se encuentra incorrecta / Second password is incorrect.");
                        }
                        else
                        {
                            this.MostrarMensaje("La segunda clave se encuentra incorrecta.");
                        }
                    }
                }
                else
                {
                    if (this.EsCasaMatriz())
                    {
                        //Mostrar mensaje según idioma
                        if (this.ObtenerIdioma() == "es")
                            this.MostrarMensaje("Se deben especificar todos los campos de la firma.");
                        else if (this.ObtenerIdioma() == "en")
                            this.MostrarMensaje("All fields must be filled in.");
                        else
                            this.MostrarMensaje("Se deben especificar todos los campos de la firma / All fields must be filled in.");
                    }
                    else
                    {
                        this.MostrarMensaje("Se deben especificar todos los campos de la firma.");
                    }
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PDV_CambiarFirma :: CambiarFirma -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error actualizando la información de la firma. Si el error sigue presentandose por favor comunicarse con el Administrador del Sistema");
            }
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

                //Si es casa matriz mostrar con idioma
                if (this.EsCasaMatriz())
                {
                    this.MasterPageFile = "~/plantillas/SILPAIdioma.master";

                    //Cargar idioma
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


            /// <summary>
            /// Evento que se ejecuta al cargar la página
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {

                if (!IsPostBack || (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("cboIdioma")))
                {
                    //Session["Usuario"] = "429";
                    //Session["Usuario"] = "13274";                    
                    //Session["Usuario"] = "15450";
                    //InicializarPagina();

                    //Validar sesion de usuario
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

                //Realizar el cambio de la firma
                this.CambiarFirma();
            }

        #endregion       

    #endregion
            
            
}