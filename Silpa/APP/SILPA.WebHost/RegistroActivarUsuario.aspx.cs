using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Usuario;
using SILPA.AccesoDatos.Usuario;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using System.Threading;

public partial class RegistroActivarUsuario : System.Web.UI.Page
{
    #region Objetos

        /// <summary>
        /// Tipo de mensajes
        /// </summary>
        private enum TipoMensaje
        {
            OK,
            ERROR
        }

    #endregion


    #region Metodos Privados

        /// <summary>
        /// Mostrar mensaje de respuesta
        /// </summary>
        /// <param name="p_objTipoMensaje">TipoMensaje con el tipo de mensaje</param>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(TipoMensaje p_objTipoMensaje, string p_strMensaje)
        {
            if(p_objTipoMensaje == TipoMensaje.ERROR)
                this.imgActivacionUsuario.ImageUrl = "~/images/error.png";
            else
                this.imgActivacionUsuario.ImageUrl = "~/App_Themes/Img/chulo_verde.png";
            this.ltlMensajeActivacionUsuario.Text = p_strMensaje;
        }


        /// <summary>
        /// Realiza la activación del usuario
        /// </summary>
        private void ActivarUsuario()
        {
            EnlaceActivacionUsuario objEnlaceActivacionUsuario;
            EnlaceActivacionUsuarioEntity objEnlace;
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion;
            TipoUsuarioDalc objUsuario;
            EnlaceActivacionUsuario objEnlaceActivacionUsuarioDalc;
            PersonaDalc objPersonaDalc;
            PersonaIdentity objPersona;
            string strEnlace = Request.QueryString["Pub"];
            string strUsuario = Request.QueryString["Us"];
            string strIdentificacion = Request.QueryString["Id"];
            int intTiempoVigenciaEnlace;

            //Verificar que se obtenga llave
            if (!string.IsNullOrWhiteSpace(strEnlace) && !string.IsNullOrWhiteSpace(strUsuario) && !string.IsNullOrWhiteSpace(strIdentificacion))
            {
                //Obtener la información del enlace
                objEnlaceActivacionUsuario = new EnlaceActivacionUsuario();
                objEnlace = objEnlaceActivacionUsuario.ConsultarEnlace(strEnlace);

                //Verificar que se obtenga enlace
                if (objEnlace != null && objEnlace.EnlaceID > 0)
                {
                    //Desencriptar la información obtenida y verificar que las llaves son correctas
                    strEnlace = EnDecript.DesencriptarDesplazamiento(strEnlace);
                    strUsuario = EnDecript.DesencriptarDesplazamiento(strUsuario);
                    strIdentificacion = EnDecript.DesencriptarDesplazamiento(strIdentificacion);

                    //Obtener tiempo
                    objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                    intTiempoVigenciaEnlace = Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_ACTIVACION_USUARIO"));

                    //Verificar que las tres llaves correspondan
                    if (strEnlace == (strUsuario + "_" + strIdentificacion))
                    {
                        //Verificar que los usuarios e identificacion pertnezcan al token de bd
                        if (strUsuario == objEnlace.PersonaID.ToString() && strIdentificacion == objEnlace.NumeroIdentificacion)
                        {
                            //Verificar que los usuarios e identificacion pertnezcan al token de bd
                            if (objEnlace.Activo && objEnlace.FechaUtilizacion == default(DateTime))
                            {
                                if (DateTime.Now <= objEnlace.FechaVigencia)
                                {
                                    //Realizar la activación del usuario
                                    objUsuario = new TipoUsuarioDalc();
                                    objUsuario.ActivarUsuario(new TipoUsuarioIdentity { IdPersona = Convert.ToInt32(objEnlace.PersonaID), IdTipoUsuario = 1 });

                                    //Envia la contraseña al usuario
                                    objPersonaDalc = new PersonaDalc();
                                    objPersona = new PersonaIdentity { PersonaId = objEnlace.PersonaID };
                                    objPersonaDalc.ObtenerPersona(ref objPersona);
                                    SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorreoAprobacionUsuario("", objPersona);

                                    //Inactivar enlace
                                    objEnlaceActivacionUsuarioDalc = new EnlaceActivacionUsuario();
                                    objEnlace.Activo = false;
                                    objEnlace.FechaUtilizacion = DateTime.Now;
                                    objEnlaceActivacionUsuario.EditarEnlace(objEnlace);

                                    this.MostrarMensaje(TipoMensaje.OK, "Se realizó la activación de la cuenta de manera exitosa y se envío por correo electrónico la clave de acceso correspondiente.<br /><br />Para retornar a la página de autenticación haga clic en el botón de Aceptar.");
                                }
                                else
                                {
                                    this.MostrarMensaje(TipoMensaje.ERROR, "La información proporcionada para la activación del usuario no se encuentra vigente, recuerde que la URL de acceso proporcionada presenta una vigencia de " + (intTiempoVigenciaEnlace / 60).ToString() + " horas.<br /><br />En caso de que no haya realizado la activación del usuario durante la vigencia de la URL comuniquese con las líneas de atención al usuario con el fin de verificar el estado de su solicitud de registro.");
                                }
                            }
                            else
                            {
                                this.MostrarMensaje(TipoMensaje.ERROR, "Ya se realizó la activación del usuario por medio de esta página, por favor verifique en su correo electrónico la recepción de la clave de acceso. En caso de no haber recibido el correo correspondiente por favor comuniquese con las líneas de atención al usuario.");
                            }
                        }
                        else
                        {
                            this.MostrarMensaje(TipoMensaje.ERROR, "La información proporcionada no presenta concordancia con la registrada en el sistema. Por favor verificar la URL que fue proporcionada por correo electrónico.");
                        }
                    }
                    else
                    {
                        this.MostrarMensaje(TipoMensaje.ERROR, "La información proporcionada no presenta concordancia. Por favor verificar la URL que fue proporcionada por correo electrónico.");
                    }
                }
                else
                {
                    this.MostrarMensaje(TipoMensaje.ERROR, "La llave de activación proporcionada no es correcta. Por favor verificar la URL que fue proporcionada por correo electrónico.");
                }
            }
            else
            {
                this.MostrarMensaje(TipoMensaje.ERROR, "No se especifico información para realizar la activación del usuario. Por favor verificar la URL que fue proporcionada por correo electrónico.");
            }
        }


    #endregion



    #region Eventos

        #region Page

            /// <summary>
            /// Evento que inicializa la información de la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        this.ActivarUsuario();
                    }
                }
                catch(Exception exc)
                {
                    SMLog.Escribir(Severidad.Critico, "RegistroActivarUsuario :: Page_Load -> Error Inesperado: " + exc.StackTrace);

                    this.MostrarMensaje(TipoMensaje.ERROR, "Se presento un problema durante el proceso de cargue de información para la activación del usuario. En caso de que el problema persista por favor comunicarse con las líneas de atención al usuario.");
                }
            }

        #endregion


        #region cmdAceptarResultadoDatosPersona

            /// <summary>
            /// Evento que direcciona a pagina de autenticación
            /// </summary>
            protected void cmdAceptarResultadoDatosPersona_Click(object sender, EventArgs e)
            {
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion;
                string strURL;

                try
                {
                    objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                    strURL = objParametrizacion.ObtenerValorParametroGeneral(-1, "login_silpa");

                    //Direccionar
                    Response.Redirect(strURL, false);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "RegistroActivarUsuario :: cmdAceptarResultadoDatosPersona_Click -> Error Inesperado: " + exc.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Error direccionando a la página principal');", true);
                }
                finally
                {
                    this.upnlRegistroUsuario.Update();
                }
            }

        #endregion

    #endregion



            
}