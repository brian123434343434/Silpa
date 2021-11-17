using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;

public partial class Seguridad_ImagenFirma : System.Web.UI.Page
{

    #region Metodos Privados

        /// <summary>
        /// Cargar la imagen de firma perteneciente al usuario solicitante
        /// </summary>
        private void CargarImagen()
        {
            Persona objPersona = null;
            PersonaFirmaIdentity objFirma = null;
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
                    objFirma = objPersona.ConsultarFirma(int.Parse(strUsuarioID));

                    //Generar response
                    Response.ContentType = objFirma.TipoImagen;
                    Response.OutputStream.Write(objFirma.Imagen, 0, objFirma.LongitudImagen);
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Seguridad_ImagenFirma :: CargarImagen -> Error Inesperado: " + exc.Message);
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