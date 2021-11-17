using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using SILPA.AccesoDatos.IntegracionCorporaciones.Entidades;
using SILPA.LogicaNegocio.IntegracionCorporaciones;
using SILPA.LogicaNegocio.IntegracionCorporaciones.Entidades;

public partial class Salir : System.Web.UI.Page
{
    #region Metodos Privados

        /// <summary>
        /// Finaliza las sesiones remotas conectadas al sistema
        /// </summary>
        private void FinalizarSesionesRemotas()
        {
            FormaFinalizarSesionEntity objForma = null;
            List<IntegracionCorporacionEntity> objLstAutoridades = null;

            try
            {
                //Obtener el listado de menus de las corporaciones
                objLstAutoridades = IntegracionCorporacion.GetInstance().ObtenerCorporacionesIntegradas();

                //Verificar si existen corporaciones integradas
                if(objLstAutoridades != null && objLstAutoridades.Count > 0)
                {
                    //Ciclo que marca como cerradas las sesiones remotas
                    foreach (IntegracionCorporacionEntity objAutoridad in objLstAutoridades)
                    {
                        //Crear forma
                        objForma = new FormaFinalizarSesionEntity
                        {
                            SessionWebID = "",
                            SessionID = Session.SessionID
                        };

                        //Cerrar sesion
                        IntegracionCorporacion.GetInstance().FinalizarSesionWeb(objAutoridad.AutoridadID, objForma);
                    }
                }

            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "Se presento error cerrando sesiones de las autoridades ambientales. " + exc.Message + " -- " + (exc.InnerException != null ? exc.InnerException.ToString() : ""));
            }
        }


        /// <summary>
        /// Finaliza sesiones y direcciona a pagina Gattaca de cierre
        /// </summary>
        private void TerminarSesiones()
        {
            ParametroDalc parametroDalc = new ParametroDalc();
            ParametroEntity parametro = new ParametroEntity();
            parametro.IdParametro = -1;
            parametro.NombreParametro = "principal_silpa";
            parametroDalc.obtenerParametros(ref parametro);
            hlk_inicio.NavigateUrl = parametro.Parametro;
            if (Request.QueryString["out"] == null)
            {
                Page.Response.Redirect(ConfigurationManager.AppSettings["URLSalirSilpa"].ToString());
            }
            else
                if (Request.QueryString["out"].ToString() == "S")
                {
                    ////21-sep-2010 - aegb
                    Session["User"] = Session["UserOld"];
                    Session["UserOld"] = null;
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);

                    Response.Redirect(parametro.Parametro, false);
                }
            Page.Response.Redirect(ConfigurationManager.AppSettings["URLSalirSilpa"].ToString(), false);
        }

    #endregion


    #region Eventos

        /// <summary>
        /// Evento que se ejecuta al cargar la pagina inicial
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Finalizar sesiones remotas
                this.FinalizarSesionesRemotas();

                //Finalizar sesiones
                this.TerminarSesiones();
            }
        }

    #endregion
    
}
