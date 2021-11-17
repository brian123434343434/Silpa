using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;

public partial class controles_cltCaptcha : System.Web.UI.UserControl
{
    #region Propiedades

        /// <summary>
        /// Número de recargas realizadas
        /// </summary>
        private int Recargas
        {
            get
            {
                return (ViewState["Recargas"] != null ? (int)ViewState["Recargas"] : 1);
            }
            set
            {
                ViewState["Recargas"] = value;
            }
        }


        /// <summary>
        /// Número de intentos
        /// </summary>
        private int Intentos
        {
            get
            {
                return (ViewState["Intentos"] != null ? (int)ViewState["Intentos"] : 1);
            }
            set
            {
                ViewState["Intentos"] = value;
            }
        }

        /// <summary>
        /// Grupo de validación de los controles
        /// </summary>
        public string ValidationGroup
        {
            get
            {
                return (string)ViewState["ValidationGroup"];
            }
            set
            {
                ViewState["ValidationGroup"] = value;
                this.txtCaptcha.ValidationGroup = value;
            }
        }

    #endregion


    #region Metodos Privados

        /// <summary>
        /// Verifica si el texto capturado por el usuario es valido
        /// </summary>
        /// <returns>bool con true en caso de que sea valido, false en caso contrario</returns>
        private bool TextoValido()
        {
            bool blnValido = false;

            try
            {
                if (Session["CaptchaCode"] != null)
                {
                    if(this.txtCaptcha.Text.Trim() == Session["CaptchaCode"].ToString())
                        blnValido = true;
                }
                else
                    throw new Exception("No se encontro información del captcha");
            }
            catch(Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "controles_cltCaptcha :: TextoValido -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                //Escalar excepcion
                throw exc;
            }

            return blnValido;
        }

    #endregion


    #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgRecargar_Click(null, null);
            }
        }

        /// <summary>
        /// Carga la imagen de captcha
        /// </summary>
        protected void imgRecargar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Cargar imagen
                this.txtCaptcha.Text = "";
                this.imgCaptcha.ImageUrl = "~/Captcha.ashx?r=" + this.Recargas.ToString();
                this.Recargas++;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "controles_cltCaptcha :: imgRecargar_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                //Escribir mensaje en pantalla
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alerta", "<script>alert('Se presento un error cargando la imagen con el texto de verificación.')</script>");
            }
            finally
            {
                this.upnlCaptcha.Update();
            }
        }

        /// <summary>
        /// Verifica si el texto de la imagen de verificación concuerda con el texto ingresado por el usuario
        /// </summary>
        protected void cvCaptcha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (this.TextoValido())
                {
                    args.IsValid = true;
                    this.Intentos = 1;
                    this.imgRecargar_Click(null, null);
                }
                else
                {
                    args.IsValid = false;
                    if (this.Intentos > 3)
                    {
                        this.Intentos = 1;
                        this.imgRecargar_Click(null, null);
                    }
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alerta", "<script>alert('El texto ingresado no concuerda con el texto de la imagen de verificación.')</script>");
                }
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alerta", "<script>alert('Se presento error durante la verificación del texto captcha.')</script>");
            }
            finally
            {
                this.Intentos++;
            }
        }

    #endregion

        
}