using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page
{
    #region Propiedades


        /// <summary>
        /// Listado de permisos de la solicitud
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

                    if (mensaje.IndexOf("Token invalido") > 0)
                        return false;
                }
                return true;
            }

        #endregion

    #endregion


    #region Eventos


        #region Pagina

            /// <summary>
            /// Carga la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    //Validar sesion de usuario
                    if (this.ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                }
            }
    
        #endregion
    #endregion


}
