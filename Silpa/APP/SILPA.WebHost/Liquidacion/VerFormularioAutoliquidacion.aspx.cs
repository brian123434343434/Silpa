using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.Comun;
using SoftManagement.Log;
using SILPA.AccesoDatos.Liquidacion.Entity;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.Liquidacion;
using SILPA.LogicaNegocio.Generico;
using System.Data;
using SILPA.LogicaNegocio.Excepciones;

public partial class Liquidacion_VerFormularioAutoliquidacion : System.Web.UI.Page
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


        /// <summary>
        /// Información de la solicitud de liquidación
        /// </summary>
        private SolicitudLiquidacionEntity SolicitudLiquidacion
        {
            get
            {
                return (SolicitudLiquidacionEntity)ViewState["_objSolicitudLiquidacion"];
            }
            set
            {
                ViewState["_objSolicitudLiquidacion"] = value;
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


        #region Manejo Campos Formulario

            
            /// <summary>
            /// Limpiar los campos del modal de ubicaciones
            /// </summary>
            private void LimpiarCamposModalUbicacion()
            {
                //Limpiar campos
                this.ltlDepartamentoUbicacionValor.Text = "";
                this.ltlMunicipioUbicacionValor.Text = "";
                this.ltlCorregimientoUbicacionValor.Text = "";
                this.ltlVeredaUbicacionValor.Text = "";
            }

            
            /// <summary>
            /// Limpia los campos del formulario
            /// </summary>
            private void LimpiarCamposFormulario()
            {
                //Limpiar campos
                this.ltlSolicitudLiquidacionValor.Text = "";
                this.ltlTipoSolicitudLiquidacionValor.Text = "";
                this.ltlTramiteLiquidacionValor.Text = "";
                this.ltlSectorLiquidacionValor.Text = "";
                this.ltlProyectoLiquidacionValor.Text = "";
                this.ltlActividadLiquidacionValor.Text = "";                
                this.ltlValorProyectoLiquidacionValor.Text = "";
                this.ltlValorProyectoLetrasLiquidacionValor.Text = "";
                this.ltlValorModificacionLiquidacionValor.Text = "";
                this.ltlValorModificacionLetrasLiquidacionValor.Text = "";
                this.ltlProyectoPINELiquidacionValor.Text = "";
                this.ltlRegionAutoliquidacionValor.Text = "";
                this.ltlAguasMaritimasLiquidacionValor.Text = "";
                this.ltlCualAguaMaritimaLiquidacionValor.Text = "";
                this.ltlAutoridadAmbientalLiquidacionValor.Text = "";

                //Limpiar campos de texto
                this.txtNombreProyectoLiquidacion.Text = "";
                this.txtDescripcionProyectoLiquidacion.Text = "";
            }

            
            /// <summary>
            /// Mostrar los campos para licencia ambiental
            /// </summary>
            private void MostrarCamposLicenciaAmbiental()
            {
                #region Descripción de Solicitud

                    //Mostrar datos solicitud
                    this.ltlTipoSolicitudLiquidacionValor.Text = this.SolicitudLiquidacion.TipoSolicitud.TipoSolicitud;
                    this.ltlSolicitudLiquidacionValor.Text = this.SolicitudLiquidacion.ClaseSolicitud.ClaseSolicitud;
    
                    //Mostrar la información del tramite
                    this.ltlTramiteLiquidacion.Text = "Trámite:";
                    this.ltlTramiteLiquidacionValor.Text = this.SolicitudLiquidacion.Tramite.Tramite;
                    this.trTramiteLiquidacion.Visible = true;

                    //Mostrar informacion de sectores
                    this.ltlSectorLiquidacionValor.Text = this.SolicitudLiquidacion.Sector.Sector;
                    this.trSectorLiquidacion.Visible = true;

                #endregion

                #region Descripción del Proyecto

                    //Inicializar label y mensajes de descripción del proyecto
                    this.ltlTituloDescripcionProyecto.Text = "Descripción del Proyecto";
                    this.ltlNombreProyectoLiquidacion.Text = "Nombre del Proyecto, Obra o Actividad:";
                    this.ltlDescripcionProyectoLiquidacion.Text = "Descripción breve del Instrumento de Manejo y Control:";
                    this.ltlValorProyectoLiquidacion.Text = "Valor de Proyecto en Pesos Colombianos:";
                    this.ltlValorProyectoLetrasLiquidacion.Text = "Valor en Letras:";
                    this.ltlValorModificacionLiquidacion.Text = "Valor de Modificación en Pesos Colombianos:";
                    this.ltlValorModificacionLetrasLiquidacion.Text = "Valor de Modificación en Letras:";

                    //Mostrar la información del proyecto
                    this.ltlProyectoLiquidacionValor.Text = this.SolicitudLiquidacion.Proyecto.Proyecto;
                    this.ltlActividadLiquidacionValor.Text = this.SolicitudLiquidacion.Actividad.Actividad;
                    this.txtNombreProyectoLiquidacion.Text = this.SolicitudLiquidacion.NombreProyecto;
                    this.txtDescripcionProyectoLiquidacion.Text = this.SolicitudLiquidacion.DescripcionProyecto;
                    this.ltlValorProyectoLiquidacionValor.Text = string.Format("{0:C}", this.SolicitudLiquidacion.ValorProyecto);
                    this.ltlValorProyectoLetrasLiquidacionValor.Text = this.SolicitudLiquidacion.ValorProyectoLetras;
                    this.ltlValorModificacionLiquidacionValor.Text = string.Format("{0:C}", this.SolicitudLiquidacion.ValorModificacion);
                    this.ltlValorModificacionLetrasLiquidacionValor.Text = this.SolicitudLiquidacion.ValorModificacionLetras;

                    //Mostrar controles descripción de proyecto
                    this.trProyectoLiquidacion.Visible = true;
                    this.trActividadLiquidacion.Visible = true;
                    this.trNombreProyectoLiquidacion.Visible = true;
                    this.trDescripcionProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLetrasLiquidacion.Visible = true;
                    if (this.SolicitudLiquidacion.ClaseSolicitud.ClaseSolicitudID == (int)AutoliquidacionSolicitud.Modificacion)
                    {
                        this.trValorModificacionLiquidacion.Visible = true;
                        this.trValorModificacionLetrasLiquidacion.Visible = true;
                    }
                    else
                    {
                        this.trValorModificacionLiquidacion.Visible = false;
                        this.trValorModificacionLetrasLiquidacion.Visible = false;
                    }

                    //Ocultar controles descripción de proyecto
                    this.ltlProyectoPINELiquidacionValor.Text = "";
                    this.trProyectoPINELiquidacion.Visible = false;

                    //Mostrar la seccion de información del proyecto
                    this.tblDescripcionProyecto.Visible = true;

                #endregion

                #region Permisos Requeridos

                    //Inicializar campos y listados
                    this.ltlTituloPermisosRequeridos.Text = "Relación de Permisos, Autorizaciones y/o Concesiones Ambientales Requeridos";
                    this.grdPermisosLiquidacion.DataSource = (this.SolicitudLiquidacion.Permisos != null ? this.SolicitudLiquidacion.Permisos : new List<PermisoSolicitudLiquidacionEntity>());
                    this.grdPermisosLiquidacion.DataBind();
                
                    //Mostrar seccion de permisos
                    this.tblPermisosRequeridos.Visible = true; 

                #endregion
                
                #region Ubicación

                    //Inicializar label y mensajes de ubicación
                    this.ltlTituloLocalizacionProyecto.Text = "Localización del Proyecto, Obra o Actividad";
                    this.ltlRegionLiquidación.Text = "Región:";
                    this.ltlUbicaciónLiquidacion.Text = "Ubicación:";
                    this.ltlAguasMaritimasLiquidacion.Text = "¿Proyecto en Aguas Marítimas?:";
                    this.ltlCualAguaMaritimaLiquidacion.Text = "¿Cual?:";                    
                    
                    //Mostrar valores de la sección de ubicación
                    if(this.SolicitudLiquidacion.Regiones != null && this.SolicitudLiquidacion.Regiones.Count > 0)
                    {
                        //Ciclo que muestra las regiones
                        foreach(RegionSolicitudLiquidacionEntity objRegion in this.SolicitudLiquidacion.Regiones)
                        {
                            if(string.IsNullOrWhiteSpace(this.ltlRegionAutoliquidacionValor.Text))
                                this.ltlRegionAutoliquidacionValor.Text = objRegion.Region.Region;
                            else
                                this.ltlRegionAutoliquidacionValor.Text += " / " + objRegion.Region.Region;
                        }
                    }
                    else
                    {
                        this.ltlRegionAutoliquidacionValor.Text = "-";
                    }                    
                    this.grdUbicacionLiquidacion.DataSource = (this.SolicitudLiquidacion.Ubicaciones != null ? this.SolicitudLiquidacion.Ubicaciones : new List<UbicacionSolicitudLiquidacionEntity>());
                    this.grdUbicacionLiquidacion.DataBind();
                    this.ltlAguasMaritimasLiquidacionValor.Text = (this.SolicitudLiquidacion.ProyectoAguasMaritimas != null && this.SolicitudLiquidacion.ProyectoAguasMaritimas.Value ? "SI" : "NO");
                    if (this.SolicitudLiquidacion.ProyectoAguasMaritimas != null && this.SolicitudLiquidacion.ProyectoAguasMaritimas.Value)
                        this.ltlCualAguaMaritimaLiquidacionValor.Text = this.SolicitudLiquidacion.Oceano.Oceano;

                    //Mostrar datos de la autoridad ambiental
                    this.ltlAutoridadAmbientalLiquidacionValor.Text = this.SolicitudLiquidacion.AutoridadAmbiental.Nombre;    

                    //Mostrar campos de ubicación
                    this.trAguasMaritimasLiquidacion.Visible = true;

                    //Ocultar campos sección de ubicación
                    this.trCualAguaMaritimaLiquidacion.Visible = (this.SolicitudLiquidacion.ProyectoAguasMaritimas != null && this.SolicitudLiquidacion.ProyectoAguasMaritimas.Value);
                    
                    //Mostrar seccion de ubicación
                    this.tblLocalizacionProyecto.Visible = true;

                #endregion

                #region Ruta

                    //Inicializar label y mensajes de la ruta
                    this.ltlTituloRutaLogistica.Text = "Ruta para Acceder a la Localización del Proyecto, Obra o Actividad";
                    this.ltlRutaLogisticaLiquidacion.Text = "Ruta:";

                    //Mostrar los campos de la ruta
                    this.grdRutaLiquidacion.DataSource = (this.SolicitudLiquidacion.Rutas != null ? this.SolicitudLiquidacion.Rutas : new List<RutaLogisticaSolicitudLiquidacionEntity>());
                    this.grdRutaLiquidacion.DataBind();

                    //Mostrar sección de ruta
                    this.tblRutaLogistica.Visible = true;

                #endregion

            }


            /// <summary>
            /// Mostrar los campos para permisos
            /// </summary>
            private void MostrarCamposPermisos()
            {
                #region Descripción de Solicitud

                    //Mostrar datos solicitud
                    this.ltlTipoSolicitudLiquidacionValor.Text = this.SolicitudLiquidacion.TipoSolicitud.TipoSolicitud;
                    this.ltlSolicitudLiquidacionValor.Text = this.SolicitudLiquidacion.ClaseSolicitud.ClaseSolicitud;

                    //Mostrar la información del tramite
                    this.ltlTramiteLiquidacion.Text = "Permiso:";
                    this.ltlTramiteLiquidacionValor.Text = this.SolicitudLiquidacion.Tramite.Tramite;
                    this.trTramiteLiquidacion.Visible = true;

                    //Mostrar informacion de sectores
                    this.ltlSectorLiquidacionValor.Text = "";
                    this.trSectorLiquidacion.Visible = false;

                #endregion

                #region Descripción del Proyecto

                    //Inicializar label y mensajes de descripción del proyecto
                    this.ltlTituloDescripcionProyecto.Text = "Descripción del Permiso, Autorización y/o Concesión";
                    this.ltlNombreProyectoLiquidacion.Text = "Nombre del Permiso, Autorización y/o Concesión Ambiental:";
                    this.ltlDescripcionProyectoLiquidacion.Text = "Descripción breve del Permiso, Autorización y/o Concesión Ambiental:";
                    this.ltlValorProyectoLiquidacion.Text = "Costo del Proyecto en Pesos Colombianos:";
                    this.ltlValorProyectoLetrasLiquidacion.Text = "Valor en Letras:";
                    this.ltlValorModificacionLiquidacion.Text = "Valor de Modificación en Pesos Colombianos:";
                    this.ltlValorModificacionLetrasLiquidacion.Text = "Valor de Modificación en Letras:";
                    this.ltlProyectoPINELiquidacion.Text = "Proyecto PINE:";

                    //Mostrar la información del proyecto
                    this.ltlProyectoLiquidacionValor.Text = "";
                    this.ltlActividadLiquidacionValor.Text = "";
                    this.txtNombreProyectoLiquidacion.Text = this.SolicitudLiquidacion.NombreProyecto;
                    this.txtDescripcionProyectoLiquidacion.Text = this.SolicitudLiquidacion.DescripcionProyecto;
                    this.ltlValorProyectoLiquidacionValor.Text = string.Format("{0:C}", this.SolicitudLiquidacion.ValorProyecto);
                    this.ltlValorProyectoLetrasLiquidacionValor.Text = this.SolicitudLiquidacion.ValorProyectoLetras;
                    this.ltlValorModificacionLiquidacionValor.Text = string.Format("{0:C}", this.SolicitudLiquidacion.ValorModificacion);
                    this.ltlValorModificacionLetrasLiquidacionValor.Text = this.SolicitudLiquidacion.ValorModificacionLetras;
                    this.ltlProyectoPINELiquidacionValor.Text = (this.SolicitudLiquidacion.ProyectoPINE.Value != null && this.SolicitudLiquidacion.ProyectoPINE.Value ? "SI" : "NO");

                    //Mostrar controles descripción de proyecto                    
                    this.trNombreProyectoLiquidacion.Visible = true;
                    this.trDescripcionProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLetrasLiquidacion.Visible = true;
                    this.trProyectoPINELiquidacion.Visible = true;
                    if (this.SolicitudLiquidacion.ClaseSolicitud.ClaseSolicitudID == (int)AutoliquidacionSolicitud.Modificacion)
                    {
                        this.trValorModificacionLiquidacion.Visible = true;
                        this.trValorModificacionLetrasLiquidacion.Visible = true;
                    }
                    else
                    {
                        this.trValorModificacionLiquidacion.Visible = false;
                        this.trValorModificacionLetrasLiquidacion.Visible = false;
                    }

                    //Ocultar controles descripción de proyecto
                    this.trProyectoLiquidacion.Visible = false;
                    this.trActividadLiquidacion.Visible = false;

                    //Mostrar la seccion de información del proyecto
                    this.tblDescripcionProyecto.Visible = true;

                #endregion

                #region Permisos Requeridos

                    //Inicializar campos y listados
                    this.ltlTituloPermisosRequeridos.Text = "";
                    this.grdPermisosLiquidacion.DataSource = (this.SolicitudLiquidacion.Permisos != null ? this.SolicitudLiquidacion.Permisos : new List<PermisoSolicitudLiquidacionEntity>());
                    this.grdPermisosLiquidacion.DataBind();

                    //Ocultar seccion de permisos
                    this.tblPermisosRequeridos.Visible = false;

                #endregion

                #region Ubicación

                    //Inicializar label y mensajes de ubicación
                    this.ltlTituloLocalizacionProyecto.Text = "Localización del Permiso, Autorización y/o Concesión";
                    this.ltlRegionLiquidación.Text = "Región:";
                    this.ltlUbicaciónLiquidacion.Text = "Ubicación:";
                    this.ltlAguasMaritimasLiquidacion.Text = "¿Proyecto en Aguas Marítimas?:";
                    this.ltlCualAguaMaritimaLiquidacion.Text = "¿Cual?:";                    

                    //Mostrar valores de la sección de ubicación
                    if (this.SolicitudLiquidacion.Regiones != null && this.SolicitudLiquidacion.Regiones.Count > 0)
                    {
                        //Ciclo que muestra las regiones
                        foreach (RegionSolicitudLiquidacionEntity objRegion in this.SolicitudLiquidacion.Regiones)
                        {
                            if (string.IsNullOrWhiteSpace(this.ltlRegionAutoliquidacionValor.Text))
                                this.ltlRegionAutoliquidacionValor.Text = objRegion.Region.Region;
                            else
                                this.ltlRegionAutoliquidacionValor.Text += " / " + objRegion.Region.Region;
                        }
                    }
                    else
                    {
                        this.ltlRegionAutoliquidacionValor.Text = "-";
                    }
                    this.grdUbicacionLiquidacion.DataSource = (this.SolicitudLiquidacion.Ubicaciones != null ? this.SolicitudLiquidacion.Ubicaciones : new List<UbicacionSolicitudLiquidacionEntity>());
                    this.grdUbicacionLiquidacion.DataBind();
                    this.ltlAguasMaritimasLiquidacionValor.Text = (this.SolicitudLiquidacion.ProyectoAguasMaritimas != null && this.SolicitudLiquidacion.ProyectoAguasMaritimas.Value ? "SI" : "NO");
                    if (this.SolicitudLiquidacion.ProyectoAguasMaritimas != null && this.SolicitudLiquidacion.ProyectoAguasMaritimas.Value)
                        this.ltlCualAguaMaritimaLiquidacionValor.Text = this.SolicitudLiquidacion.Oceano.Oceano;    

                    //Mostrar datos de la autoridad ambiental
                    this.ltlAutoridadAmbientalLiquidacionValor.Text = this.SolicitudLiquidacion.AutoridadAmbiental.Nombre;    

                    //Mostrar campos de ubicación
                    this.trAguasMaritimasLiquidacion.Visible = true;

                    //Ocultar campos sección de ubicación
                    this.trCualAguaMaritimaLiquidacion.Visible = (this.SolicitudLiquidacion.ProyectoAguasMaritimas != null && this.SolicitudLiquidacion.ProyectoAguasMaritimas.Value);
                    
                    //Mostrar seccion de ubicación
                    this.tblLocalizacionProyecto.Visible = true;

                #endregion

                #region Ruta

                    //Inicializar label y mensajes de la ruta
                    this.ltlTituloRutaLogistica.Text = "Ruta Logística para Acceder a los Puntos Solicitados para el Uso y/o Aprovechamiento de los Recursos Naturales";
                    this.ltlRutaLogisticaLiquidacion.Text = "Ruta Logística:";

                    //Mostra la ruta
                    this.grdRutaLiquidacion.DataSource = (this.SolicitudLiquidacion.Rutas != null ? this.SolicitudLiquidacion.Rutas : new List<RutaLogisticaSolicitudLiquidacionEntity>());
                    this.grdRutaLiquidacion.DataBind();

                    //Mostrar sección de ruta
                    this.tblRutaLogistica.Visible = true;

                #endregion

            }


            /// <summary>
            /// Mostrar los campos para otros instrumentos
            /// </summary>
            private void MostrarCamposOtrosInstrumentos()
            {
                #region Descripción de Solicitud

                    //Mostrar datos solicitud
                    this.ltlTipoSolicitudLiquidacionValor.Text = this.SolicitudLiquidacion.TipoSolicitud.TipoSolicitud;
                    this.ltlSolicitudLiquidacionValor.Text = this.SolicitudLiquidacion.ClaseSolicitud.ClaseSolicitud;

                    //Mostrar el listado de tramites
                    this.ltlTramiteLiquidacion.Text = "Trámite:";
                    this.ltlTramiteLiquidacionValor.Text = this.SolicitudLiquidacion.Tramite.Tramite;
                    this.trTramiteLiquidacion.Visible = true;

                    //Ocultar el listado de sectores
                    this.ltlSectorLiquidacionValor.Text = "";
                    this.trSectorLiquidacion.Visible = false;

                #endregion

                #region Descripción del Proyecto

                    //Inicializar label y mensajes de descripción del proyecto
                    this.ltlTituloDescripcionProyecto.Text = "Descripción del Proyecto";
                    this.ltlNombreProyectoLiquidacion.Text = "Nombre del Proyecto, Obra o Actividad:";
                    this.ltlDescripcionProyectoLiquidacion.Text = "Descripción breve del Instrumento de Manejo y Control:";
                    this.ltlValorProyectoLiquidacion.Text = "Costo del Proyecto en Pesos Colombianos";
                    this.ltlValorProyectoLetrasLiquidacion.Text = "Valor en Letras";
                    this.ltlValorModificacionLiquidacion.Text = "Valor de Modificación en Pesos Colombianos:";
                    this.ltlValorModificacionLetrasLiquidacion.Text = "Valor de Modificación en Letras:";

                    //Mostrar la información del proyecto
                    this.ltlProyectoLiquidacionValor.Text = "";
                    this.ltlActividadLiquidacionValor.Text = "";
                    this.txtNombreProyectoLiquidacion.Text = this.SolicitudLiquidacion.NombreProyecto;
                    this.txtDescripcionProyectoLiquidacion.Text = this.SolicitudLiquidacion.DescripcionProyecto;
                    this.ltlValorProyectoLiquidacionValor.Text = string.Format("{0:C}", this.SolicitudLiquidacion.ValorProyecto);
                    this.ltlValorProyectoLetrasLiquidacionValor.Text = this.SolicitudLiquidacion.ValorProyectoLetras;
                    this.ltlValorModificacionLiquidacionValor.Text = string.Format("{0:C}", this.SolicitudLiquidacion.ValorModificacion);
                    this.ltlValorModificacionLetrasLiquidacionValor.Text = this.SolicitudLiquidacion.ValorModificacionLetras;

                    //Mostrar controles descripción de proyecto                    
                    this.trNombreProyectoLiquidacion.Visible = true;
                    this.trDescripcionProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLetrasLiquidacion.Visible = true;
                    if (this.SolicitudLiquidacion.ClaseSolicitud.ClaseSolicitudID == (int)AutoliquidacionSolicitud.Modificacion)
                    {
                        this.trValorModificacionLiquidacion.Visible = true;
                        this.trValorModificacionLetrasLiquidacion.Visible = true;
                    }
                    else
                    {
                        this.trValorModificacionLiquidacion.Visible = false;
                        this.trValorModificacionLetrasLiquidacion.Visible = false;
                    }

                    //Ocultar controles descripción de proyecto
                    this.trProyectoLiquidacion.Visible = false;
                    this.trActividadLiquidacion.Visible = false;
                    this.trProyectoPINELiquidacion.Visible = false;

                    //Mostrar la seccion de información del proyecto
                    this.tblDescripcionProyecto.Visible = true;

                #endregion

                #region Permisos Requeridos

                    //Mostrar permisos
                    this.ltlTituloPermisosRequeridos.Text = "";
                    this.grdPermisosLiquidacion.DataSource = (this.SolicitudLiquidacion.Permisos != null ? this.SolicitudLiquidacion.Permisos : new List<PermisoSolicitudLiquidacionEntity>());
                    this.grdPermisosLiquidacion.DataBind();

                    //Ocultar seccion de permisos
                    this.tblPermisosRequeridos.Visible = false;

                #endregion

                #region Ubicación

                    //Inicializar label y mensajes de ubicación
                    this.ltlTituloLocalizacionProyecto.Text = "Localización del Proyecto, Obra o Actividad";
                    this.ltlRegionLiquidación.Text = "Región:";
                    this.ltlUbicaciónLiquidacion.Text = "Ubicación:";
                    this.ltlAguasMaritimasLiquidacion.Text = "";
                    this.ltlCualAguaMaritimaLiquidacion.Text = "";                    

                    //Mostrar valores de la sección de ubicación
                    if (this.SolicitudLiquidacion.Regiones != null && this.SolicitudLiquidacion.Regiones.Count > 0)
                    {
                        //Ciclo que muestra las regiones
                        foreach (RegionSolicitudLiquidacionEntity objRegion in this.SolicitudLiquidacion.Regiones)
                        {
                            if (string.IsNullOrWhiteSpace(this.ltlRegionAutoliquidacionValor.Text))
                                this.ltlRegionAutoliquidacionValor.Text = objRegion.Region.Region;
                            else
                                this.ltlRegionAutoliquidacionValor.Text += " / " + objRegion.Region.Region;
                        }
                    }
                    else
                    {
                        this.ltlRegionAutoliquidacionValor.Text = "-";
                    }
                    this.grdUbicacionLiquidacion.DataSource = (this.SolicitudLiquidacion.Ubicaciones != null ? this.SolicitudLiquidacion.Ubicaciones : new List<UbicacionSolicitudLiquidacionEntity>());
                    this.grdUbicacionLiquidacion.DataBind();
                    this.ltlAguasMaritimasLiquidacionValor.Text = "";                 
                    this.ltlCualAguaMaritimaLiquidacionValor.Text = "";

                    //Mostrar datos de la autoridad ambiental
                    this.ltlAutoridadAmbientalLiquidacionValor.Text = this.SolicitudLiquidacion.AutoridadAmbiental.Nombre;

                    //Ocultar campos sección de ubicación
                    this.trCualAguaMaritimaLiquidacion.Visible = false;                    
                    this.trAguasMaritimasLiquidacion.Visible = false;

                    //Mostrar seccion de ubicación
                    this.tblLocalizacionProyecto.Visible = true;

                #endregion

                #region Ruta

                    //Inicializar label y mensajes de la ruta
                    this.ltlTituloRutaLogistica.Text = "Ruta Logística para Acceder a los Puntos Solicitados para el Uso y/o Aprovechamiento de los Recursos Naturales";
                    this.ltlRutaLogisticaLiquidacion.Text = "Ruta Logística:";

                    //Mostrar los campos de la ruta
                    this.grdRutaLiquidacion.DataSource = (this.SolicitudLiquidacion.Rutas != null ? this.SolicitudLiquidacion.Rutas : new List<RutaLogisticaSolicitudLiquidacionEntity>());
                    this.grdRutaLiquidacion.DataBind();

                    //Mostrar sección de ruta
                    this.tblRutaLogistica.Visible = true;

                #endregion

            }           


            /// <summary>
            /// Oculta todos los campos y secciones del formulario
            /// </summary>
            private void OcultarSeccionesFormulario()
            {
                //Mostrar seccion de datos de solicitud y ocultar campos
                this.tblDescripcionSolicitud.Visible = true;
                this.trSolicitudLiquidacion.Visible = true;
                this.trTramiteLiquidacion.Visible = false;
                this.trSectorLiquidacion.Visible = false;

                //Ocultar la información de descripción del proyecto
                this.tblDescripcionProyecto.Visible = false;

                //Ocultar información de permisos
                this.tblPermisosRequeridos.Visible = false;

                //Ocultar informacion de ubicacion
                this.tblLocalizacionProyecto.Visible = false;

                //Ocultar información de ruta
                this.tblRutaLogistica.Visible = false;
            }

            
            /// <summary>
            /// Mostrar la información de la solicitud de liquidación
            /// </summary>
            private void MostrarInformacionSolicitudLiquidacion()
            {
                //Verificar el tipo de solicitud realizado para mostrar la información
                if (this.SolicitudLiquidacion.TipoSolicitud.TipoSolicitudID == (int)AutoliquidacionTipoSolicitud.LICENCIA_AMBIENTAL)
                {
                    this.MostrarCamposLicenciaAmbiental();
                }
                else if (this.SolicitudLiquidacion.TipoSolicitud.TipoSolicitudID == (int)AutoliquidacionTipoSolicitud.PERMISO)
                {
                    this.MostrarCamposPermisos();
                }
                else if (this.SolicitudLiquidacion.TipoSolicitud.TipoSolicitudID == (int)AutoliquidacionTipoSolicitud.OTROS_INSTRUMENTOS)
                {
                    this.MostrarCamposOtrosInstrumentos();
                }
            }


            /// <summary>
            /// Cargar la información inicial de la pagina
            /// </summary>
            private void InicializarPagina()
            {
                int intSolicitudLiquidacionID = 0;

                try
                {
                    //Limpiar campos del formulario
                    this.LimpiarCamposFormulario();
                    
                    //Ocultar campos y secciones del formulario
                    this.OcultarSeccionesFormulario();

                    //Verificar que se indique el id de la solicitud
                    intSolicitudLiquidacionID = (Session["intSolicitudLiquidacionID"] != null && !string.IsNullOrWhiteSpace(Session["intSolicitudLiquidacionID"].ToString()) ? Convert.ToInt32(Session["intSolicitudLiquidacionID"]) : 0);
                    if(intSolicitudLiquidacionID > 0)
                    {
                        //Cargar el listado de tipos de solicitud
                        this.ConsultarInformacionSolicitud(intSolicitudLiquidacionID);

                        //Verificar que la solicitud de liquidación exista
                        if (this.SolicitudLiquidacion != null && this.SolicitudLiquidacion.SolicitudLiquidacionID > 0)
                        {
                            this.MostrarInformacionSolicitudLiquidacion();
                        }
                        else
                        {
                            throw new Exception("La solicitud de liquidación especificada no existe");
                        }
                    }
                    else
                    {
                        throw new Exception("No se especifico identificador de la solicitud de liquidación");
                    }
                    
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento error cargando información inicial del formulario");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_VerFormularioAutoliquidacion :: InicializarPagina -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }        

        #endregion


        #region Manejo de Información


            /// <summary>
            /// Consultar la información de liquidación
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificadro de la solicitud</param>
            private void ConsultarInformacionSolicitud(int p_intTipoSolicitudID)
            {
                Autoliquidacion objAutoliquidacion = null;
                
                //Cargar la información de lá solicitud
                objAutoliquidacion = new Autoliquidacion();
                this.SolicitudLiquidacion = objAutoliquidacion.ConsultarSolicitudLiquidacion(p_intTipoSolicitudID);
            }

        #endregion

    #endregion


    #region Eventos

        #region Page
    
            /// <summary>
            /// Evento que se ejecuta al cargar la pagina
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

                
        #region grdUbicacionLiquidacion

            /// <summary>
            /// Mostrar la información de una ubicación
            /// </summary>
            protected void lnkVerUbicacion_Click(object sender, EventArgs e)
            {
                UbicacionSolicitudLiquidacionEntity objUbicacion = null;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalUbicacion();

                    //Cargar la ubicacion seleccionada
                    objUbicacion = this.SolicitudLiquidacion.Ubicaciones.Where(ubicacion => ubicacion.UbicacionSolicitudID == Convert.ToInt32(((LinkButton)sender).CommandArgument)).ToList()[0];

                    //Cargar informacion de ubicación
                    this.ltlDepartamentoUbicacionValor.Text = objUbicacion.Departamento.Nombre;
                    this.ltlMunicipioUbicacionValor.Text = objUbicacion.Municipio.NombreMunicipio;
                    this.ltlCorregimientoUbicacionValor.Text = (!string.IsNullOrWhiteSpace(objUbicacion.Corregimiento) ? objUbicacion.Corregimiento : "-");
                    this.ltlVeredaUbicacionValor.Text = (!string.IsNullOrWhiteSpace(objUbicacion.Vereda) ? objUbicacion.Vereda : "-");

                    //Cargar el listado de coordenadas
                    this.grdCoordenadasAgregarUbicacion.DataSource = (objUbicacion.Coordenadas != null ? objUbicacion.Coordenadas : new List<CoordenadaUbicacionLiquidacionEntity>());
                    this.grdCoordenadasAgregarUbicacion.DataBind();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();

                    //Cerrar modal
                    this.mpeAgregarUbicacion.Show();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando información de modal de ubicación");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_VerFormularioAutoliquidacion :: lnkVerUbicacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalUbicacion();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();

                    //Cerrar modal
                    this.mpeAgregarUbicacion.Hide();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }  
            }

        #endregion


        #region Modal Ubicación
            
            /// <summary>
            /// Evento que cierra modal de ubicación
            /// </summary>
            protected void cmdAceptarAgregarUbicacion_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalUbicacion();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();

                    //Cerrar modal
                    this.mpeAgregarUbicacion.Hide();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cerrando modal de ubicaciones");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_VerFormularioAutoliquidacion :: cmdAceptarAgregarUbicacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }


            /// <summary>
            /// Mostrar la información de las coordenadas de una ubicación
            /// </summary>
            protected void lnkVerCoordenadaAgregarUbicacion_Click(object sender, EventArgs e)
            {
                
            }

        #endregion


    #endregion

}