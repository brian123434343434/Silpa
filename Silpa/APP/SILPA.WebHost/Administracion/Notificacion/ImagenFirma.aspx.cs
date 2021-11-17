using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.AccesoDatos.Notificacion;
using SoftManagement.Log;

public partial class Administracion_Notificacion_ImagenFirma : System.Web.UI.Page
{

    #region Metodos Privados

        /// <summary>
        /// Cargar la imagen de firma perteneciente al usuario solicitante
        /// </summary>
        private void CargarImagen()
        {
            FirmaAutoridadNotificacion objFirmaAutoridadNotificacion = null;
            FirmaAutoridadNotificacionEntity objFirma = null;
            string strFirmaID = "";

            try
            {
                //Cargar id usuario
                strFirmaID = Request.QueryString["FIR"];

                //Verificar que se halla especificado dato
                if (!string.IsNullOrWhiteSpace(strFirmaID))
                {
                    //Cargar firma
                    objFirmaAutoridadNotificacion = new FirmaAutoridadNotificacion();
                    objFirma = objFirmaAutoridadNotificacion.ObtenerFirma(Convert.ToInt32(strFirmaID));

                    //Generar response
                    Response.ContentType = objFirma.TipoFirma;
                    Response.OutputStream.Write(objFirma.Firma, 0, objFirma.LongitudFirma);
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_ImagenFirma :: CargarImagen -> Error Inesperado: " + exc.Message);
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