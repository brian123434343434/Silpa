using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.AccesoDatos.Liquidacion.Entity;
using SILPA.LogicaNegocio.Liquidacion;
using SILPA.LogicaNegocio.Liquidacion.Enum;
using SILPA.Comun;
using SILPA.LogicaNegocio.Liquidacion.Entidades;
using System.Globalization;

public partial class Liquidacion_RespuestaSolicitudLiquidacion : System.Web.UI.Page
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
                    //string mensaje = "Token valido";

                    if (mensaje.IndexOf("Token invalido") > 0)
                        return false;
                }
                return true;
            }

        #endregion


        #region Manejo Errores

            /// <summary>
            /// Mostrar el mensaje especificado
            /// </summary>
            /// <param name="p_strMensaje">string con el mensaje</param>
            private void MostrarMensaje(string p_strMensaje)
            {
                this.lblMensaje.Text = p_strMensaje;
                this.tblMensaje.Visible = true;
                this.upnlMensaje.Update();
            }


            /// <summary>
            /// Ocultar los mensajes
            /// </summary>
            private void OcultarMensaje()
            {
                this.lblMensaje.Text = "";
                this.tblMensaje.Visible = false;
                this.upnlMensaje.Update();
            }

        #endregion


        #region Manejo Datos Pagina

            /// <summary>
            /// Limpiar los campos de la pagina de respuesta
            /// </summary>
            private void LimpiarCamposPagina()
            {
                //Limpiar valores
                this.ltlRespuestaRadicacion.Text = "";
                this.ltlFechaSolicitud.Text = "";
                this.ltlAutoridad.Text = "";
                this.ltlDescripcionDatosBasicosSolicitud.Text = "";
                this.ltlDescripcionProyecto.Text = "";
                this.ltlReferenciaPago.Text = "";
                this.ltlValorNumeros.Text = "";
                this.ltlValorLetras.Text = "";
                this.ltlValorProyecto.Text = "";
                this.ltlSalarioMinimo.Text = "";
                this.ltlRelacionSalario.Text = "";
                this.ltlTarifaMaxima.Text = "";
                this.ltlValorMaximoCobrar.Text = "";
                this.ltlResolucion.Text = "";
                this.ltlNumeroTabla.Text = "";
                this.ltlNombreMicroTabla.Text = "";
                this.ltlTituloTablaResolucion.Text = "";
                this.ltlValorTotalTiquetes.Text = "";
                this.ltlValorServicio.Text = "";
                this.ltlValorAdministracion.Text = "";
                this.ltlValorTotal.Text = "";
                this.rptValoresMicrotabla.DataSource = null;
                this.rptValoresMicrotabla.DataBind();
                this.rptPermisosANLA.DataSource = null;
                this.rptPermisosANLA.DataBind();
                this.rptTiquetes.DataSource = null;
                this.rptTiquetes.DataBind();                
                this.rptPermisos.DataSource = null;
                this.rptPermisos.DataBind();

                //Ocultar información
                this.upnlRespuestaRadicacion.Visible = false;
                this.upnlRespuestaAutoliquidacion.Visible = false;
                this.trDatosLey633.Visible = false;
                this.trTablaLey633.Visible = false;
                this.trDatosResolucion.Visible = false;
                this.trTablaResolucion.Visible = false;
                this.trPermisosANLASolicitados.Visible = false;
                this.trListaPermisos.Visible = false;
                this.trTiquetes.Visible = false;
                this.trListadoTiquetes.Visible = false;
                this.trDatosPermisos.Visible = false;
                this.rptPermisos.Visible = false;
                this.trTextoCondiciones.Visible = false;
            }


            /// <summary>
            /// Mostrar la respuesta de radicación de solicitud
            /// </summary>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity con la información de la solicitud</param>
            private void MostrarRespuestaRadicacion(SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                //Mostrar panel de respuestas de radicacion
                this.upnlRespuestaRadicacion.Visible = true;

                //Cargar mensaje de respuesta
                this.ltlRespuestaRadicacion.Text = "Se realizo la radicación de la solicitud de <b>" + p_objSolicitudLiquidacion.ClaseSolicitud.ClaseSolicitud + "</b> de <b>" + p_objSolicitudLiquidacion.TipoSolicitud.TipoSolicitud + "</b> " +
                                                   "ante la autoridad <b>" + p_objSolicitudLiquidacion.AutoridadAmbiental.Nombre + "</b> el día <b>" + p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("dd") + " de " + p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("yyyy") + "</b> con el número VITAL <b>" + p_objSolicitudLiquidacion.NumeroVITAL + "</b>." +
                                                   "<br /><br />En caso de requerir mayor información sobre el proceso por favor comunicarse con la autoridad ambiental.";
                
                //Actualizar panel
                this.upnlRespuestaRadicacion.Update();
            }


            /// <summary>
            /// Mostrar la respuesta de autoliquidación
            /// </summary>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity con la información de la solicitud</param>
            /// <param name="p_objLiquidacion">AutoliquidacionLiquidacionEntity con la información de la liquidación</param>
            private void MostrarRespuestaAutoliquidacion(SolicitudLiquidacionEntity p_objSolicitudLiquidacion, AutoliquidacionLiquidacionEntity p_objLiquidacion)
            {
                //Mostrar panel de respuestas de radicacion
                this.upnlRespuestaAutoliquidacion.Visible = true;

                //Cargar datos basicos de la solicitud
                this.ltlFechaSolicitud.Text = "<b>" + p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("dd") + " de " + p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-CO")) + " de " + p_objSolicitudLiquidacion.FechaRadicacionVITAL.ToString("yyyy") + "</b>";
                this.ltlAutoridad.Text = "<b>" + p_objSolicitudLiquidacion.AutoridadAmbiental.Nombre + "</b>";

                if (p_objSolicitudLiquidacion.TipoSolicitud.TipoSolicitudID == (int)AutoliquidacionTipoSolicitud.LICENCIA_AMBIENTAL)
                {
                    this.ltlDescripcionDatosBasicosSolicitud.Text = "de <b>Licencia Ambiental</b> para el servicio de <b>" + p_objSolicitudLiquidacion.ClaseSolicitud.ClaseSolicitud + "</b>" +
                                                                    " y el trámite <b>" + p_objSolicitudLiquidacion.Tramite.Tramite + "</b> del proyecto";
                }
                else if (p_objSolicitudLiquidacion.TipoSolicitud.TipoSolicitudID == (int)AutoliquidacionTipoSolicitud.PERMISO)
                {
                    this.ltlDescripcionDatosBasicosSolicitud.Text = "para el servicio de <b>" + p_objSolicitudLiquidacion.ClaseSolicitud.ClaseSolicitud + 
                                                                    "</b> del <b>Permiso Ambiental</b>, <b>" + p_objSolicitudLiquidacion.Tramite.Tramite + "</b>,";
                }
                else if (p_objSolicitudLiquidacion.TipoSolicitud.TipoSolicitudID == (int)AutoliquidacionTipoSolicitud.OTROS_INSTRUMENTOS)
                {
                    this.ltlDescripcionDatosBasicosSolicitud.Text = "para el servicio de <b>" + p_objSolicitudLiquidacion.ClaseSolicitud.ClaseSolicitud +
                                                                    "</b> del <b>Instrumento</b>, <b>" + p_objSolicitudLiquidacion.Tramite.Tramite + "</b>,";
                }

                this.ltlDescripcionProyecto.Text = "<b>" + p_objSolicitudLiquidacion.DescripcionProyecto.Replace("\n", "<br />") + "</b>";
                this.ltlReferenciaPago.Text = p_objLiquidacion.ReferenciaPago;
                this.ltlValorNumeros.Text = p_objLiquidacion.ValorLiquidacion;
                this.ltlValorLetras.Text = SILPA.LogicaNegocio.Utilidad.Utilidades.NumeroALetras(decimal.Parse(p_objLiquidacion.ValorLiquidacion, NumberStyles.Currency).ToString()).ToUpper() + " MCTE"; ;

                //Verificar si se muestra información de ley
                if (p_objLiquidacion.ResolucionID == (int)AutoliquidacionResoluciones.Ley_633)
                {
                    //Mostrar datos de ley 633
                    this.trDatosLey633.Visible = true;
                    this.trTablaLey633.Visible = true;

                    //Cargar datos a mostrar
                    this.ltlValorProyecto.Text = p_objLiquidacion.Detalle633.Valorproyecto;
                    this.ltlSalarioMinimo.Text = p_objLiquidacion.Detalle633.ValorSalario;
                    this.ltlRelacionSalario.Text = p_objLiquidacion.Detalle633.Relacion;
                    this.ltlTarifaMaxima.Text = p_objLiquidacion.Detalle633.TarifaAplicar;
                    this.ltlValorMaximoCobrar.Text = p_objLiquidacion.Detalle633.ValorTotal;
                }
                else
                {
                    //Mostrar datos
                    this.trDatosResolucion.Visible = true;
                    this.trTablaResolucion.Visible = true;
                    this.trPermisosANLASolicitados.Visible = p_objLiquidacion.PermisosANLA && p_objLiquidacion.Detalle0324.Permisos != null && p_objLiquidacion.Detalle0324.Permisos.Count > 0;
                    this.trListaPermisos.Visible = p_objLiquidacion.PermisosANLA && p_objLiquidacion.Detalle0324.Permisos != null && p_objLiquidacion.Detalle0324.Permisos.Count > 0;
                    this.trTiquetes.Visible = p_objLiquidacion.Detalle0324.Tiquetes != null && p_objLiquidacion.Detalle0324.Tiquetes.Count > 0;
                    this.trListadoTiquetes.Visible = p_objLiquidacion.Detalle0324.Tiquetes != null && p_objLiquidacion.Detalle0324.Tiquetes.Count > 0;
                    
                    //Cargar datos a mostrar
                    this.ltlResolucion.Text = "<b>" + p_objLiquidacion.Resolucion + "</b>"; 
                    this.ltlNumeroTabla.Text = "<b>" + p_objLiquidacion.Detalle0324.NumeroTabla + "</b>";
                    this.ltlNombreMicroTabla.Text = "<b>" + p_objLiquidacion.Detalle0324.NombreMicroTabla + "</b>";
                    this.ltlTituloTablaResolucion.Text = "Tabla No. " + p_objLiquidacion.Detalle0324.NumeroTabla + " - " + p_objLiquidacion.Detalle0324.NombreMicroTabla;
                    this.rptValoresMicrotabla.DataSource = p_objLiquidacion.Detalle0324.DatosMicrotabla;
                    this.rptValoresMicrotabla.DataBind();
                    if (this.trListaPermisos.Visible)
                    {
                        this.rptPermisosANLA.DataSource = p_objLiquidacion.Detalle0324.Permisos;
                        this.rptPermisosANLA.DataBind();
                    }
                    if(this.trListadoTiquetes.Visible)
                    {
                        this.ltlValorTotalTiquetes.Text = p_objLiquidacion.Detalle0324.ValorTiquetes;
                        this.rptTiquetes.DataSource = p_objLiquidacion.Detalle0324.Tiquetes;
                        this.rptTiquetes.DataBind();
                    }
                    this.ltlValorServicio.Text = p_objLiquidacion.Detalle0324.ValorServicio;
                    this.ltlValorAdministracion.Text = p_objLiquidacion.Detalle0324.ValorAdministracion;
                    this.ltlValorTotal.Text = p_objLiquidacion.Detalle0324.ValorTotal;
                }

                //Verificar si hay permisos
                if(p_objLiquidacion.Detalle0324.Permisos != null && p_objLiquidacion.Detalle0324.Permisos.Count > 0)
                {
                    //Mostrar datos de los permisos
                    this.trDatosPermisos.Visible = true;
                    this.rptPermisos.Visible = true;

                    //Ciclo que carga en los permisos el valor en letras
                    foreach (AutoliquidacionDatosPermisosEntity objPermiso in p_objLiquidacion.Detalle0324.Permisos)
                    {
                        objPermiso.ValorTotalLetras = SILPA.LogicaNegocio.Utilidad.Utilidades.NumeroALetras(decimal.Parse(objPermiso.ValorTotal, NumberStyles.Currency).ToString()).ToUpper() + " MCTE";
                    }

                    //Cargar datos de permisos
                    this.rptPermisos.DataSource = p_objLiquidacion.Detalle0324.Permisos;
                    this.rptPermisos.DataBind();
                }

                //Mostrar texto condiciones
                this.trTextoCondiciones.Visible = true;

                //Actualizar panel
                this.upnlRespuestaAutoliquidacion.Update();
            }


            /// <summary>
            /// Carga la información inicial de la pagina
            /// </summary>
            private void InicializarPagina()
            {
                int intSolicitudLiquidacion = 0;
                Autoliquidacion objAutoliquidacion = null;
                SolicitudLiquidacionEntity objSolicitudLiquidacion = null;
                AutoliquidacionLiquidacionEntity objLiquidacion = null;

                try
                {
                    this.LimpiarCamposPagina();

                    //Cargar de sesion el identificador de la solicitud
                    intSolicitudLiquidacion = (Session["intSolicitudLiquidacionID"] != null && !string.IsNullOrWhiteSpace(Session["intSolicitudLiquidacionID"].ToString()) ? Convert.ToInt32(Session["intSolicitudLiquidacionID"]) : 0);

                    //Verificar que el identificador sea mayor a cero
                    if (intSolicitudLiquidacion > 0)
                    {
                        //Consultar la información de la solicitud
                        objAutoliquidacion = new Autoliquidacion();
                        objSolicitudLiquidacion = objAutoliquidacion.ConsultarSolicitudLiquidacion(intSolicitudLiquidacion);

                        //Verificar si se obtuvo datos
                        if (objSolicitudLiquidacion != null)
                        {   
                            this.MostrarRespuestaRadicacion(objSolicitudLiquidacion);
                        }
                        else
                        {
                            throw new Exception("No se encontro información de la solicitud");
                        }
                    }
                    else
                    {
                        throw new Exception("No se especifico identificador de la solicitud");
                    }
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento error cargando información de la solicitud de liquidación");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_RespuestaSolicitudLiquidacion :: InicializarPagina -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }


        #endregion


    #endregion


    #region Eventos

        #region Page

            /// <summary>
            /// Evento que se carga al cargar la pagina. Carga la información de la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                //this.SolicitanteID = 429;

                if (!IsPostBack)
                {
                    //Ocultar mensajes de error
                    this.OcultarMensaje();

                    //Inicializar pagina
                    //this.InicializarPagina();

                    //Validar sesion de usuario
                    if (this.ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                    else
                    {
                        //Iniciliazar datos de la página
                        this.InicializarPagina();
                    }
                }
            }

        #endregion

        #region cmdVolver

            /// <summary>
            /// Retorna a la pagina de listado de solicitudes
            /// </summary>
            protected void cmdVolver_Click(object sender, EventArgs e)
            {
                //Limpiar sesion
                Session["intSolicitudLiquidacionID"] = null;

                //Direccionar al listado de solicitudes
                Response.Redirect("SolicitudesAutoliquidacion.aspx", false);
            }

        #endregion

    #endregion
            
}