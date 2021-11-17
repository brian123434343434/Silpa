using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;

public partial class Seguridad_ImagenLogo : System.Web.UI.Page
{

    #region Metodos Privados

        /// <summary>
        /// Cargar la imagen de firma perteneciente al usuario solicitante
        /// </summary>
        private void CargarImagen()
        {
            Persona objPersona = null;
            PersonaLogoIdentity objLogo = null;
            string strUsuarioID = "";

            try
            {
                //Cargar id usuario
                strUsuarioID = Request.QueryString["Imagen"];

                //Verificar que se halla especificado dato
                if (!string.IsNullOrWhiteSpace(strUsuarioID))
                {
                    //Cargar firma
                    objPersona = new Persona();
                    objLogo = objPersona.ConsultarLogo(int.Parse(strUsuarioID));

                    //Generar response
                    Response.ContentType = objLogo.TipoLogo;
                    Response.OutputStream.Write(objLogo.Logo, 0, objLogo.LongitudLogo);
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Seguridad_ImagenLogo :: CargarImagen -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);
            }
            finally
            {
                //Finalizar respuesta
                Response.End();
            }
        }


    #endregion


    #region Eventos

    /// <summary>
        /// Evento que se ejecuta al cargar la pagina
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Cargar la imagen
            this.CargarImagen();
        }


    #endregion
}