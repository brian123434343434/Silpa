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

public partial class Liquidacion_FormularioAutoliquidacion : System.Web.UI.Page
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
        /// Listado de permisos de la solicitud
        /// </summary>
        private List<TramiteLiquidacionEntity> TramitesLiquidacion
        {
            get
            {
                return (List<TramiteLiquidacionEntity>)ViewState["_objTramitesLiquidacion"];
            }
            set
            {
                ViewState["_objTramitesLiquidacion"] = value;
            }
        }


        /// <summary>
        /// Listado de permisos de la solicitud
        /// </summary>
        private List<PermisoSolicitudLiquidacionEntity> PermisosLiquidacion
        {
            get
            {
                return (List<PermisoSolicitudLiquidacionEntity>)ViewState["_objPermisosLiquidacion"];
            }
            set
            {
                ViewState["_objPermisosLiquidacion"] = value;
            }
        }


        /// <summary>
        /// Listado de ubicaciones de la solicitud
        /// </summary>
        private List<UbicacionSolicitudLiquidacionEntity> UbicacionesLiquidacion
        {
            get
            {
                return (List<UbicacionSolicitudLiquidacionEntity>)ViewState["_objUbicacionesLiquidacion"];
            }
            set
            {
                ViewState["_objUbicacionesLiquidacion"] = value;
            }
        }


        /// <summary>
        /// Listado de rutas logisticas de la solicitud
        /// </summary>
        private List<RutaLogisticaSolicitudLiquidacionEntity> RutasLogisticasLiquidacion
        {
            get
            {
                return (List<RutaLogisticaSolicitudLiquidacionEntity>)ViewState["_objRutasLogisticasLiquidacion"];
            }
            set
            {
                ViewState["_objRutasLogisticasLiquidacion"] = value;
            }
        }


        /// <summary>
        /// Listado de coordenadas de ubicación
        /// </summary>
        private List<CoordenadaUbicacionLiquidacionEntity> CoordenadasUbicacionLiquidacion
        {
            get
            {
                return (List<CoordenadaUbicacionLiquidacionEntity>)ViewState["_objCoordenadasUbicacionLiquidacion"];
            }
            set
            {
                ViewState["_objCoordenadasUbicacionLiquidacion"] = value;
            }
        }


        /// <summary>
        /// Listado de autoridades ambientales a los cuales se dirige una solicitud
        /// </summary>
        private List<AutoridadAmbientalIdentity> AutoridadesAmbientalesSolicitud
        {
            get
            {
                return (List<AutoridadAmbientalIdentity>)ViewState["_objAutoridadesAmbientalesSolicitud"];
            }
            set
            {
                ViewState["_objAutoridadesAmbientalesSolicitud"] = value;
            }
        }

        /// <summary>
        /// Campos dinamicos que deben ser mostrados
        /// </summary>
        private DataSet CamposFormulario
        {
            get
            {
                return (DataSet)ViewState["_objCamposFormulario"];
            }
            set
            {
                ViewState["_objCamposFormulario"] = value;
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


        #region Listados y Desplegables

            /// <summary>
            /// Cargar el listado de tipos de solicitud activos
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoTiposSolicitud(DropDownList p_objListado)
            {
                TipoSolicitudLiquidacion objTipoSolicitudLiquidacion = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objTipoSolicitudLiquidacion = new TipoSolicitudLiquidacion();
                p_objListado.DataSource = objTipoSolicitudLiquidacion.ConsultarTiposSolicitud(true);
                p_objListado.DataValueField = "TipoSolicitudID";
                p_objListado.DataTextField = "TipoSolicitud";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de clases de solicitudes pertenecientes a un tipo de solicitud
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            /// <param name="p_intTipoSolicitudID">int con el tipo de solicitud</param>        
            private void CargarListadoClasesSolicitudes(DropDownList p_objListado, int p_intTipoSolicitudID)
            {
                ClaseSolicitudLiquidacion objClaseSolicitudLiquidacion = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objClaseSolicitudLiquidacion = new ClaseSolicitudLiquidacion();
                p_objListado.DataSource = objClaseSolicitudLiquidacion.ConsultarClaseSolicitudesTipoSolicitud(p_intTipoSolicitudID);
                p_objListado.DataValueField = "ClaseSolicitudID";
                p_objListado.DataTextField = "ClaseSolicitud";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de tramites perteneciente a una solicitud
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de la solicitud</param>
            /// <param name="p_intSolicitudID">int con el identificadro de la solicitud</param>        
            private void CargarListadoTramites(DropDownList p_objListado, int p_intTipoSolicitudID, int p_intSolicitudID)
            {
                TramiteLiquidacion objTramiteLiquidacion = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tramites
                objTramiteLiquidacion = new TramiteLiquidacion();
                p_objListado.DataSource = objTramiteLiquidacion.ConsultarTramites(p_intTipoSolicitudID, p_intSolicitudID);
                p_objListado.DataValueField = "TramiteID";
                p_objListado.DataTextField = "Tramite";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }

            /// <summary>
            /// Cargar el listado de sectores
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoSectores(DropDownList p_objListado)
            {
                SectorLiquidacion objSectores = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de sectores
                objSectores = new SectorLiquidacion();
                p_objListado.DataSource = objSectores.ConsultarSectores(true);
                p_objListado.DataValueField = "SectorID";
                p_objListado.DataTextField = "Sector";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de autoridades de proyectos pertenecientes a un sector
            /// </summary>
            /// <param name="p_intSectorID">Identificador del sector al cual pertenece los proyectos</param>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoProyectos(DropDownList p_objListado, int p_intSectorID)
            {
                ProyectoLiquidacion objProyecto = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de proyectos
                if (p_intSectorID > 0)
                {
                    objProyecto = new ProyectoLiquidacion();
                    p_objListado.DataSource = objProyecto.ConsultarProyectosSector(p_intSectorID, true);
                    p_objListado.DataValueField = "ProyectoID";
                    p_objListado.DataTextField = "Proyecto";
                    p_objListado.DataBind();
                }
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de autoridades de actividades relacionadas a un proyecto
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            /// <param name="p_intProyectoID">int con el identificador del proyecto</param>
            private void CargarListadoActividades(DropDownList p_objListado, int p_intProyectoID)
            {
                ProyectoLiquidacion objProyecto = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de actividades
                if (p_intProyectoID > 0)
                {
                    objProyecto = new ProyectoLiquidacion();
                    p_objListado.DataSource = objProyecto.ConsultarActividadesProyecto(p_intProyectoID);
                    p_objListado.DataValueField = "ActividadID";
                    p_objListado.DataTextField = "Actividad";
                    p_objListado.DataBind();
                }
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de regiones
            /// </summary>
            /// <param name="p_objListado">CheckBoxList con el listado a llenar</param>
            private void CargarListadoRegiones(CheckBoxList p_objListadoChecks)
            {
                RegionLiquidacion objRegion = null;

                //Limpiar listado
                p_objListadoChecks.ClearSelection();
                p_objListadoChecks.Items.Clear();

                //Cargar el listado de regiones
                objRegion = new RegionLiquidacion();
                p_objListadoChecks.DataSource = objRegion.ConsultarRegiones(true);
                p_objListadoChecks.DataValueField = "RegionID";
                p_objListadoChecks.DataTextField = "Region";
                p_objListadoChecks.DataBind();
            }


            /// <summary>
            /// Cargar el listado de oceanos
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoOceanos(DropDownList p_objListado)
            {
                OceanoLiquidacion objOceanos = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de actividades
                objOceanos = new OceanoLiquidacion();
                p_objListado.DataSource = objOceanos.ConsultarOceanos(null);
                p_objListado.DataValueField = "OceanoID";
                p_objListado.DataTextField = "Oceano";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de permisos
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoPermisos(DropDownList p_objListado)
            {
                PermisoLiquidacion objPermisoLiquidacion = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tramites
                objPermisoLiquidacion = new PermisoLiquidacion();
                p_objListado.DataSource = objPermisoLiquidacion.ConsultarPermisos();
                p_objListado.DataValueField = "PermisoID";
                p_objListado.DataTextField = "Permiso";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de autoridades ambientales para permisos
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoAutoridadesPermisos(DropDownList p_objListado)
            {
                AutoridadAmbiental objAutoridadAmbiental = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tramites
                objAutoridadAmbiental = new AutoridadAmbiental();
                p_objListado.DataSource = objAutoridadAmbiental.ListarAutoridadAmbientalPermisos();
                p_objListado.DataValueField = "AUT_ID";
                p_objListado.DataTextField = "AUT_NOMBRE";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de departamentos para indicar ubicación
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoDepartamentosUbicacion(DropDownList p_objListado)
            {
                Departamento objDepartamento = null;

                //Limpiar listado
                p_objListado.ClearSelection();

                p_objListado.Items.Clear();
                //Cargar el listado de departamentos
                objDepartamento = new Departamento();
                p_objListado.DataSource = objDepartamento.ListarDepartamentosLiquidacion();
                p_objListado.DataValueField = "Id";
                p_objListado.DataTextField = "Nombre";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "S"));
            }


            /// <summary>
            /// Cargar el listado de municipios para indicar ubicación
            /// </summary>
            /// <param name="p_intDepartamentoID">int con el identificador del deparatamento al cual debe pertenecer los municipios</param>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoMunicipiosUbicacion(DropDownList p_objListado, int p_intDepartamentoID)
            {
                Listas objListas = null;
                DataSet objMunicipios = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de municipios
                objListas = new Listas();
                objMunicipios = objListas.ListaMunicipios(null, p_intDepartamentoID, null);
                if (objMunicipios != null && objMunicipios.Tables.Count > 0)
                {
                    objMunicipios.Tables[0].DefaultView.RowFilter = "AUT_ID <> " + ((int)AutoridadesAmbientales.ANLA).ToString();
                    objMunicipios.Tables[0].DefaultView.Sort = "MUN_NOMBRE ASC";

                    //Agregar opciones al listado de municipios
                    foreach(DataRow objMUnicipio in objMunicipios.Tables[0].DefaultView.ToTable().Rows)
                    {
                        if (p_objListado.Items.Count == 0 || !p_objListado.Items.Contains(new ListItem(objMUnicipio["MUN_NOMBRE"].ToString(), objMUnicipio["MUN_ID"].ToString())))
                        {
                            p_objListado.Items.Add(new ListItem(objMUnicipio["MUN_NOMBRE"].ToString(), objMUnicipio["MUN_ID"].ToString()));
                        }
                    }
                }
                else
                {
                    p_objListado.DataSource = objMunicipios;
                }

                // Colocar opcion de seleccione
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "S"));
            }


            /// <summary>
            /// Cargar el listado de tipos de geometria
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoTiposGeometriaCoordenadas(DropDownList p_objListado)
            {
                TipoGeometriaCoordenadaLiquidacion objTipoGeometria = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de geometria
                objTipoGeometria = new TipoGeometriaCoordenadaLiquidacion();
                p_objListado.DataSource = objTipoGeometria.ConsultarTiposGeometria(true);
                p_objListado.DataValueField = "TipoGeometriaID";
                p_objListado.DataTextField = "TipoGeometria";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de tipos de coordenada
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoTiposCoordenada(DropDownList p_objListado)
            {
                TipoCoordenadaUbicacionLiquidacion objTipoCooordenada = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de coordenada
                objTipoCooordenada = new TipoCoordenadaUbicacionLiquidacion();
                p_objListado.DataSource = objTipoCooordenada.ConsultarTiposCoordenada(true);
                p_objListado.DataValueField = "TipoCoordenadaID";
                p_objListado.DataTextField = "TipoCoordenada";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de tipos de coordenada
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoOrigenesMagna(DropDownList p_objListado)
            {
                OrigenMagnaCoordenadaLiquidacion objOrigenMagna = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de origenes magna
                objOrigenMagna = new OrigenMagnaCoordenadaLiquidacion();
                p_objListado.DataSource = objOrigenMagna.ConsultarOrigenesMagna(true);
                p_objListado.DataValueField = "OrigenMagnaID";
                p_objListado.DataTextField = "OrigenMagna";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de tipos de coordenada
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoMediosTransporte(DropDownList p_objListado)
            {
                MedioTransporteLiquidacion objMedioTransporteLiquidacion = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de origenes magna
                objMedioTransporteLiquidacion = new MedioTransporteLiquidacion();
                p_objListado.DataSource = objMedioTransporteLiquidacion.ConsultarMediosTransporte(true);
                p_objListado.DataValueField = "MedioTransporteID";
                p_objListado.DataTextField = "MedioTransporte";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de departamentos de origen de la ruta de acuerdo al medio de transporte
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
            private void CargarDepartamentoOrigenRuta(DropDownList p_objListado, int p_intMedioTransporteID)
            {
                Departamento objDepartamento = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de departamentos
                objDepartamento = new Departamento();
                p_objListado.DataSource = objDepartamento.ListarDepartamentosMedioTransporteLiquidacion(p_intMedioTransporteID);
                p_objListado.DataValueField = "Id";
                p_objListado.DataTextField = "Nombre";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "S"));
            }


            /// <summary>
            /// Cargar el listado de municipios de origen pertenecientes a un departamento de acuerdo a la autoridad y medio de transporte
            /// </summary>            
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            /// <param name="p_intDepartamentoID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
            private void CargarListadoMunicipiosOrigenRuta(DropDownList p_objListado, int p_intDepartamentoID, int p_intMedioTransporteID)
            {
                Municipio objMunicipio = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de departamentos
                objMunicipio = new Municipio();
                p_objListado.DataSource = objMunicipio.ListarMuncicipiosDepartamentoMedioTransporteLiquidacion(p_intDepartamentoID, p_intMedioTransporteID);
                p_objListado.DataValueField = "Id";
                p_objListado.DataTextField = "NombreMunicipio";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "S"));
            }


            /// <summary>
            /// Cargar el listado de departamentos destino de la ruta de acuerdo al municipio de origen y medio de transporte
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            /// <param name="p_intMunicipioOrigen">int con el municipio de origen</param>
            /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
            private void CargarDepartamentoDestinoRuta(DropDownList p_objListado, int p_intMunicipioOrigen, int p_intMedioTransporteID)
            {
                Departamento objDepartamento = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de departamentos
                objDepartamento = new Departamento();
                p_objListado.DataSource = objDepartamento.ListarDepartamentosDestinoMedioTransporteLiquidacion(p_intMunicipioOrigen, p_intMedioTransporteID);
                p_objListado.DataValueField = "Id";
                p_objListado.DataTextField = "Nombre";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "S"));
            }


            /// <summary>
            /// Cargar el listado de municipios destino pertenecientes a un departamento de acuerdo al municipio de origen, autoridad y medio de transporte
            /// </summary>            
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            /// <param name="p_intMunicipioOrigen">int con identificador del municipio de origen</param>
            /// <param name="p_intDepartamentoID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
            private void CargarListadoMunicipiosDestinoRuta(DropDownList p_objListado, int p_intMunicipioOrigen, int p_intDepartamentoID, int p_intMedioTransporteID)
            {
                Municipio objMunicipio = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de departamentos
                objMunicipio = new Municipio();
                p_objListado.DataSource = objMunicipio.ListarMuncicipiosDestinoDepartamentoMedioTransporteLiquidacion(p_intMunicipioOrigen, p_intDepartamentoID, p_intMedioTransporteID);
                p_objListado.DataValueField = "Id";
                p_objListado.DataTextField = "NombreMunicipio";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "S"));
            }

        #endregion


        #region Manejo Campos Formulario

            
            /// <summary>
            /// Limpiar los campos del modal de permisos
            /// </summary>
            private void LimpiarCamposModalAgregarPermiso()
            {
                //Limpiar desplegables
                this.cboPermisoAgregarPermiso.ClearSelection();
                this.cboPermisoAgregarPermiso.Items.Clear();
                this.cboAutoridadAgregarPermiso.ClearSelection();
                this.cboAutoridadAgregarPermiso.Items.Clear();
            }


            /// <summary>
            /// Limpiar los campos del modal de ubicaciones
            /// </summary>
            private void LimpiarCamposModalAgregarUbicacion()
            {
                //Limpiar campos
                this.cboDepartamentoAgregarUbicacion.ClearSelection();
                this.cboDepartamentoAgregarUbicacion.Items.Clear();
                this.cboMunicipioAgregarUbicacion.ClearSelection();
                this.cboMunicipioAgregarUbicacion.Items.Clear();
                this.txtCorregimientoAgregarUbicacion.Text = "";
                this.txtVeredaAgregarUbicacion.Text = "";

                //Inicializar listados
                this.cboDepartamentoAgregarUbicacion.Items.Insert(0, new ListItem("Seleccione.", "S"));
                this.cboMunicipioAgregarUbicacion.Items.Insert(0, new ListItem("Seleccione.", "S"));

                //Inicializar listado de coordenadas
                this.CoordenadasUbicacionLiquidacion = new List<CoordenadaUbicacionLiquidacionEntity>();

                //Inicializar grilla
                this.grdCoordenadasAgregarUbicacion.DataSource = this.CoordenadasUbicacionLiquidacion;
                this.grdCoordenadasAgregarUbicacion.DataBind();
            }


            /// <summary>
            /// Limpiar los campos del modal de coordenadas
            /// </summary>
            private void LimpiarCamposModalAgregarCoordenada()
            {
                //Limpiar campos
                this.txtLocalizacionAgregarCoordenada.Text = "";
                this.txtNorteAgregarCoordenada.Text = "";
                this.txtEsteAgregarCoordenada.Text = "";
                this.cboTipoGeometriaAgregarCoordenada.ClearSelection();
                this.cboTipoGeometriaAgregarCoordenada.Items.Clear();
                this.cboTipoCoordenadaAgregarCoordenada.ClearSelection();
                this.cboTipoCoordenadaAgregarCoordenada.Items.Clear();
                this.cboOrigenMagnaSirgasAgregarCoordenada.ClearSelection();
                this.cboOrigenMagnaSirgasAgregarCoordenada.Items.Clear();
            }


            /// <summary>
            /// Limpiar los campos del modal de rutas
            /// </summary>
            private void LimpiarCamposModalAgregarRuta()
            {
                //Limpiar campos
                this.cboMedioTransporteAgregarRuta.ClearSelection();
                this.cboMedioTransporteAgregarRuta.Items.Clear();
                this.cboDepartamentoPartidaAgregarRuta.ClearSelection();
                this.cboDepartamentoPartidaAgregarRuta.Items.Clear();
                this.cboDepartamentoPartidaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                this.cboMunicipioPartidaAgregarRuta.ClearSelection();
                this.cboMunicipioPartidaAgregarRuta.Items.Clear();
                this.cboMunicipioPartidaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                this.cboDepartamentoLlegadaAgregarRuta.ClearSelection();
                this.cboDepartamentoLlegadaAgregarRuta.Items.Clear();
                this.cboDepartamentoLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                this.cboMunicipioLlegadaAgregarRuta.ClearSelection();
                this.cboMunicipioLlegadaAgregarRuta.Items.Clear();
                this.cboMunicipioLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                this.txtTiempoAgregarRuta.Text = "";

                //Activiar desplegables
                this.cboDepartamentoPartidaAgregarRuta.Enabled = true;
                this.cboMunicipioPartidaAgregarRuta.Enabled = true;
            }


            /// <summary>
            /// Limpia los campos del formulario
            /// </summary>
            private void LimpiarCamposFormulario()
            {
                //Limpiar campos desplegable
                this.cboTipoSolicitudLiquidacion.ClearSelection();
                this.cboTipoSolicitudLiquidacion.Items.Clear();
                this.cboSolicitudLiquidacion.ClearSelection();
                this.cboSolicitudLiquidacion.Items.Clear();
                this.cboTramiteLiquidacion.ClearSelection();
                this.cboTramiteLiquidacion.Items.Clear();
                this.cboSectorLiquidacion.ClearSelection();
                this.cboSectorLiquidacion.Items.Clear();
                this.cboProyectoLiquidacion.ClearSelection();
                this.cboProyectoLiquidacion.Items.Clear();
                this.cboActividadLiquidacion.ClearSelection();
                this.cboActividadLiquidacion.Items.Clear();
                this.cboProyectoPINELiquidacion.ClearSelection();
                this.cboAguasMaritimasLiquidacion.ClearSelection();
                this.cboCualAguaMaritimaLiquidacion.ClearSelection();
                this.cboCualAguaMaritimaLiquidacion.Items.Clear();
                this.cboAutoridadAmbientalLiquidacion.ClearSelection();
                this.cboAutoridadAmbientalLiquidacion.Items.Clear();

                //Limpiar check box list
                this.chklRegionAutoliquidacion.ClearSelection();
                this.chklRegionAutoliquidacion.Items.Clear();

                //Limpiar campos de texto
                this.txtNombreProyectoLiquidacion.Text = "";
                this.txtDescripcionProyectoLiquidacion.Text = "";
                this.txtValorProyectoLiquidacion.Text = "";
                this.txtValorProyectoLetrasLiquidacion.Text = "";
                this.txtValorModificacionLiquidacion.Text = "";
                this.txtValorModificacionLetrasLiquidacion.Text = "";

                //Inicializar listados
                this.TramitesLiquidacion = new List<TramiteLiquidacionEntity>();
                this.PermisosLiquidacion = new List<PermisoSolicitudLiquidacionEntity>();
                this.UbicacionesLiquidacion = new List<UbicacionSolicitudLiquidacionEntity>();
                this.RutasLogisticasLiquidacion = new List<RutaLogisticaSolicitudLiquidacionEntity>();

                //Inicializar grillas
                this.ActualizarGrillas();
            }


            /// <summary>
            /// Limpiar los campos del modal de confirmación de envío de solicitud
            /// </summary>
            private void LimpiarCamposModalConfirmarEnvioSolicitud()
            {
                //Limpiar campos
                this.ltlTerminosConfirmarEnvioSolicitud.Text = "";
                this.chkAceptarTerminoCondiciones.Checked = false;
            }


            /// <summary>
            /// Actualiza grillas del formulario
            /// </summary>
            private void ActualizarGrillas()
            {
                this.grdPermisosLiquidacion.DataSource = this.PermisosLiquidacion;
                this.grdPermisosLiquidacion.DataBind();
                this.grdUbicacionLiquidacion.DataSource = this.UbicacionesLiquidacion;
                this.grdUbicacionLiquidacion.DataBind();
                this.grdRutaLiquidacion.DataSource = this.RutasLogisticasLiquidacion;
                this.grdRutaLiquidacion.DataBind();
            }

            
            /// <summary>
            /// Mostrar los campos para licencia ambiental
            /// </summary>
            /// <param name="p_intSolicitudID">int con el identificador de solicitud escogida</param>
            private void MostrarCamposLicenciaAmbiental(int p_intSolicitudID)
            {
                #region Descripción de Solicitud

                    //Mostrar el listado de tramites
                    this.ltlTramiteLiquidacion.Text = "Trámite:";
                    this.spnTramiteLiquidacion.Attributes.Remove("title");
                    this.spnTramiteLiquidacion.Attributes.Add("title", "Trámite sobre el cual desea realizar la liquidación.");
                    this.CargarListadoTramites(this.cboTramiteLiquidacion, (int)AutoliquidacionTipoSolicitud.LICENCIA_AMBIENTAL, p_intSolicitudID);
                    this.trTramiteLiquidacion.Visible = true;

                    //Cargar el listado de sectores
                    this.CargarListadoSectores(this.cboSectorLiquidacion);
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
                    this.spnNombreProyectoLiquidacion.Attributes.Remove("title");
                    this.spnNombreProyectoLiquidacion.Attributes.Add("title", "Nombre del proyecto, obra o actividad que se va a realizar y por el cual se presenta la solicitud de liquidación.");
                    this.spnDescripcionProyectoLiquidacion.Attributes.Remove("title");
                    this.spnDescripcionProyectoLiquidacion.Attributes.Add("title", "Descripción breve del instrumento de manejo y control ambiental objeto del servicio de liquidación.");
                    this.spnValorProyectoLiquidacion.Attributes.Remove("title");
                    this.spnValorProyectoLiquidacion.Attributes.Add("title", "Valor del proyecto, obra o actividad en pesos colombianos.");
                    this.spnValorProyectoLetrasLiquidacion.Attributes.Remove("title");
                    this.spnValorProyectoLetrasLiquidacion.Attributes.Add("title", "Valor del proyecto, obra o actividad en letras.");                    
                    this.spnValorModificacionLiquidacion.Attributes.Remove("title");
                    this.spnValorModificacionLiquidacion.Attributes.Add("title", "Valor de modificación del proyecto, obra o actividad en pesos colombianos.");
                    this.spnValorModificacionLetrasLiquidacion.Attributes.Remove("title");
                    this.spnValorModificacionLetrasLiquidacion.Attributes.Add("title", "Valor en letras de la modificación del proyecto, obra o actividad.");
                    this.rfvNombreProyectoLiquidacion.ErrorMessage = "Debe ingresar el nombre del proyecto, obra o actividad a realizar.";
                    this.rfvDescripcionProyectoLiquidacion.ErrorMessage = "Debe ingresar una breve descripción del proyecto, obra o actividad a realizar.";
                    this.rfvValorProyectoLiquidacion.ErrorMessage = "Debe ingresar el valor del proyecto.";
                    this.rfvValorModificacionLiquidacion.ErrorMessage = "Debe ingresar el valor de la modificación.";

                    //Inicializar los controles descripción de proyecto
                    this.CargarListadoProyectos(this.cboProyectoLiquidacion,  -1);
                    this.CargarListadoActividades(this.cboActividadLiquidacion, -1);
                    this.txtNombreProyectoLiquidacion.Text = "";
                    this.txtDescripcionProyectoLiquidacion.Text = "";
                    this.txtValorProyectoLiquidacion.Text = "";
                    this.txtValorProyectoLetrasLiquidacion.Text = "";
                    this.txtValorModificacionLiquidacion.Text = "";
                    this.txtValorModificacionLetrasLiquidacion.Text = "";

                    //Mostrar controles descripción de proyecto
                    this.trProyectoLiquidacion.Visible = true;
                    this.trActividadLiquidacion.Visible = true;
                    this.trNombreProyectoLiquidacion.Visible = true;
                    this.trDescripcionProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLetrasLiquidacion.Visible = true;
                    if (p_intSolicitudID == (int)AutoliquidacionSolicitud.Modificacion)
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
                    this.cboProyectoPINELiquidacion.ClearSelection();
                    this.trProyectoPINELiquidacion.Visible = false;

                    //Mostrar la seccion de información del proyecto
                    this.tblDescripcionProyecto.Visible = true;

                #endregion

                #region Permisos Requeridos

                    //Inicializar campos y listados
                    this.ltlTituloPermisosRequeridos.Text = "Relación de Permisos, Autorizaciones y/o Concesiones Ambientales Requeridos";
                    this.spnPermisosLiquidacion.Attributes.Remove("title");
                    this.spnPermisosLiquidacion.Attributes.Add("title", "Listado de permisos, autorizaciones y/o concesiones ambientales requeridos para la realización del proyecto, obra o actividad.<br/>Para ingresar la información sobre los permisos requeridos hacer clic sobre el botón \"Agregar\".");
                    this.PermisosLiquidacion = new List<PermisoSolicitudLiquidacionEntity>();
                    this.grdPermisosLiquidacion.DataSource = this.PermisosLiquidacion;
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
                    this.spnRegionLiquidacion.Attributes.Remove("title");
                    this.spnRegionLiquidacion.Attributes.Add("title", "Región donde se va a desarrollar el proyecto, obra o actividad.");
                    this.spnLocalizacionLiquidacion.Attributes.Remove("title");
                    this.spnLocalizacionLiquidacion.Attributes.Add("title", "Ubicación del proyecto, obra o actividad.<br/>Para ingresar la información sobre la ubicación hacer clic sobre el botón \"Agregar\".");
                    this.spnAguasMaritimasLiquidacion.Attributes.Remove("title");
                    this.spnAguasMaritimasLiquidacion.Attributes.Add("title", "Indica si el proyecto, obra o actividad se desarrolla en aguas marítimas.");
                    this.spnCualAguaMaritimaLiquidacion.Attributes.Remove("title");
                    this.spnCualAguaMaritimaLiquidacion.Attributes.Add("title", "Aguas marítimas sobre las cuales se desarrolla el proyecto, obra o actividad.");
                    
                    //Inicializar campos de la seccion de ubicación
                    this.CargarListadoRegiones(this.chklRegionAutoliquidacion);
                    this.UbicacionesLiquidacion = new List<UbicacionSolicitudLiquidacionEntity>();
                    this.grdUbicacionLiquidacion.DataSource = this.UbicacionesLiquidacion;
                    this.grdUbicacionLiquidacion.DataBind();
                    this.cboAguasMaritimasLiquidacion.ClearSelection();
                    this.cboAutoridadAmbientalLiquidacion.ClearSelection();
                    this.cboAutoridadAmbientalLiquidacion.Items.Clear();
                    
                    //Mostrar campos de ubicación
                    this.trAguasMaritimasLiquidacion.Visible = true;

                    //Ocultar campos sección de ubicación
                    this.trCualAguaMaritimaLiquidacion.Visible = false;

                    //Ocultar o mostrar listado de autoridades
                    this.MostrarCamposAutoridadesAmbientales();
                    
                    //Mostrar seccion de ubicación
                    this.tblLocalizacionProyecto.Visible = true;

                #endregion

                #region Ruta

                    //Inicializar label y mensajes de la ruta
                    this.ltlTituloRutaLogistica.Text = "Ruta para Acceder a la Localización del Proyecto, Obra o Actividad";
                    this.ltlRutaLogisticaLiquidacion.Text = "Ruta:";
                    this.spnRutaLogisticaLiquidacion.Attributes.Remove("title");
                    this.spnRutaLogisticaLiquidacion.Attributes.Add("title", "Ruta para acceder a la localización donde se encuentra ubicada el proyecto, obra o actividad.<br/>Para ingresar la información sobre la ruta logística hacer clic sobre el botón \"Agregar\".");

                    //Inicializar los campos de la ruta
                    this.RutasLogisticasLiquidacion = new List<RutaLogisticaSolicitudLiquidacionEntity>();
                    this.grdRutaLiquidacion.DataSource = this.RutasLogisticasLiquidacion;
                    this.grdRutaLiquidacion.DataBind();

                    //Ocultar sección de ruta
                    this.tblRutaLogistica.Visible = false;

                #endregion

            }


            /// <summary>
            /// Mostrar los campos para permisos
            /// </summary>
            /// <param name="p_intSolicitudID">int con el identificador de solicitud escogida</param>
            private void MostrarCamposPermisos(int p_intSolicitudID)
            {
                #region Descripción de Solicitud

                    //Mostrar el listado de tramites
                    this.ltlTramiteLiquidacion.Text = "Permiso:";
                    this.spnTramiteLiquidacion.Attributes.Remove("title");
                    this.spnTramiteLiquidacion.Attributes.Add("title", "Permiso, autorización o concesión sobre la cual solicita liquidación.");
                    this.CargarListadoTramites(this.cboTramiteLiquidacion, (int)AutoliquidacionTipoSolicitud.PERMISO, p_intSolicitudID);
                    this.trTramiteLiquidacion.Visible = true;

                    //Ocultar el listado de sectores
                    this.cboSectorLiquidacion.ClearSelection();
                    this.cboSectorLiquidacion.Items.Clear();
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
                    this.spnNombreProyectoLiquidacion.Attributes.Remove("title");
                    this.spnNombreProyectoLiquidacion.Attributes.Add("title", "Nombre del permiso, autorización y/o concesión ambiental que se va a realizar y por el cual se presenta la solicitud de liquidación.");
                    this.spnDescripcionProyectoLiquidacion.Attributes.Remove("title");
                    this.spnDescripcionProyectoLiquidacion.Attributes.Add("title", "Descripción breve del Permiso, Autorización y/o Concesión Ambiental objeto del servicio de liquidación.");
                    this.spnValorProyectoLiquidacion.Attributes.Remove("title");
                    this.spnValorProyectoLiquidacion.Attributes.Add("title", "Costo del proyecto en pesos colombianos.");
                    this.spnValorProyectoLetrasLiquidacion.Attributes.Remove("title");
                    this.spnValorProyectoLetrasLiquidacion.Attributes.Add("title", "Costo del proyecto en letras.");
                    this.spnProyectoPINELiquidacion.Attributes.Remove("title");
                    this.spnProyectoPINELiquidacion.Attributes.Add("title", "Indica si el proyecto al cual pertenece el permiso, autorización y/o concesión es un proyecto PINE validado por la comisión intersectorial de infraestructura y proyectos estratégicos - CIIPE.");
                    this.spnValorModificacionLiquidacion.Attributes.Remove("title");
                    this.spnValorModificacionLiquidacion.Attributes.Add("title", "Valor de modificación del proyecto en pesos colombianos.");
                    this.spnValorModificacionLetrasLiquidacion.Attributes.Remove("title");
                    this.spnValorModificacionLetrasLiquidacion.Attributes.Add("title", "Valor en letras de la modificación del proyecto.");
                    this.rfvNombreProyectoLiquidacion.ErrorMessage = "Debe ingresar el nombre del permiso, autorización y/o concesión ambiental.";
                    this.rfvDescripcionProyectoLiquidacion.ErrorMessage = "Debe ingresar una breve descripción del permiso, autorización y/o concesión ambiental.";
                    this.rfvValorProyectoLiquidacion.ErrorMessage = "Debe ingresar el costo del proyecto.";
                    this.rfvProyectoPINELiquidacion.ErrorMessage = "Debe indicar si es un proyecto PINE.";
                    this.rfvValorModificacionLiquidacion.ErrorMessage = "Debe ingresar el valor de la modificación.";

                    //Inicializar los controles descripción de proyecto
                    this.cboProyectoLiquidacion.ClearSelection();
                    this.cboProyectoLiquidacion.Items.Clear();
                    this.cboActividadLiquidacion.ClearSelection();
                    this.cboActividadLiquidacion.Items.Clear();                    
                    this.txtNombreProyectoLiquidacion.Text = "";
                    this.txtDescripcionProyectoLiquidacion.Text = "";
                    this.txtValorProyectoLiquidacion.Text = "";
                    this.txtValorProyectoLetrasLiquidacion.Text = "";
                    this.txtValorModificacionLiquidacion.Text = "";
                    this.txtValorModificacionLetrasLiquidacion.Text = "";
                    this.cboProyectoPINELiquidacion.ClearSelection();
                    
                    //Mostrar controles descripción de proyecto                    
                    this.trNombreProyectoLiquidacion.Visible = true;
                    this.trDescripcionProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLetrasLiquidacion.Visible = true;
                    this.trProyectoPINELiquidacion.Visible = true;
                    if (p_intSolicitudID == (int)AutoliquidacionSolicitud.Modificacion)
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
                    this.spnPermisosLiquidacion.Attributes.Remove("title");
                    this.spnPermisosLiquidacion.Attributes.Add("title", "");
                    this.PermisosLiquidacion = new List<PermisoSolicitudLiquidacionEntity>();
                    this.grdPermisosLiquidacion.DataSource = this.PermisosLiquidacion;
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
                    this.spnRegionLiquidacion.Attributes.Remove("title");
                    this.spnRegionLiquidacion.Attributes.Add("title", "Región donde se va a desarrollar el proyecto, obra o actividad al cual pertenece el permiso, autorización y/o concesión.");
                    this.spnLocalizacionLiquidacion.Attributes.Remove("title");
                    this.spnLocalizacionLiquidacion.Attributes.Add("title", "Ubicación del proyecto, obra o actividad al cual pertenece el permiso, autorización y/o concesión.<br/>Para ingresar la información sobre la ubicación hacer clic sobre el botón \"Agregar\".");
                    this.spnAguasMaritimasLiquidacion.Attributes.Remove("title");
                    this.spnAguasMaritimasLiquidacion.Attributes.Add("title", "Indica si el proyecto, obra o actividad al cual pertenece el permiso, autorización y/o concesión se desarrolla en aguas marítimas.");
                    this.spnCualAguaMaritimaLiquidacion.Attributes.Remove("title");
                    this.spnCualAguaMaritimaLiquidacion.Attributes.Add("title", "Aguas marítimas sobre las cuales se desarrolla el proyecto, obra o actividad al cual pertenece el permiso, autorización y/o concesión.");

                    //Inicializar campos de la seccion de ubicación
                    this.CargarListadoRegiones(this.chklRegionAutoliquidacion);
                    this.UbicacionesLiquidacion = new List<UbicacionSolicitudLiquidacionEntity>();
                    this.grdUbicacionLiquidacion.DataSource = this.UbicacionesLiquidacion;
                    this.grdUbicacionLiquidacion.DataBind();
                    this.cboAguasMaritimasLiquidacion.ClearSelection();
                    this.cboAutoridadAmbientalLiquidacion.ClearSelection();
                    this.cboAutoridadAmbientalLiquidacion.Items.Clear();

                    //Mostrar campos de ubicación
                    this.trAguasMaritimasLiquidacion.Visible = true;

                    //Ocultar campos sección de ubicación
                    this.trCualAguaMaritimaLiquidacion.Visible = false;

                    //Ocultar o mostrar listado de autoridades
                    this.MostrarCamposAutoridadesAmbientales();

                    //Mostrar seccion de ubicación
                    this.tblLocalizacionProyecto.Visible = true;

                #endregion

                #region Ruta

                    //Inicializar label y mensajes de la ruta
                    this.ltlTituloRutaLogistica.Text = "Ruta Logística para Acceder a los Puntos Solicitados para el Uso y/o Aprovechamiento de los Recursos Naturales";
                    this.ltlRutaLogisticaLiquidacion.Text = "Ruta Logística:";
                    this.spnRutaLogisticaLiquidacion.Attributes.Remove("title");
                    this.spnRutaLogisticaLiquidacion.Attributes.Add("title", "Ruta Logística para acceder a los puntos solicitados para el uso y/o aprovechamiento de los recursos naturales.<br/>Para ingresar la información sobre la ruta logística hacer clic sobre el botón \"Agregar\".");

                    //Inicializar los campos de la ruta
                    this.RutasLogisticasLiquidacion = new List<RutaLogisticaSolicitudLiquidacionEntity>();
                    this.grdRutaLiquidacion.DataSource = this.RutasLogisticasLiquidacion;
                    this.grdRutaLiquidacion.DataBind();

                    //Ocultar sección de ruta
                    this.tblRutaLogistica.Visible = false;

                #endregion

            }


            /// <summary>
            /// Mostrar los campos para otros instrumentos
            /// </summary>
            /// <param name="p_intSolicitudID">int con el identificador de solicitud escogida</param>
            private void MostrarCamposOtrosInstrumentos(int p_intSolicitudID)
            {
                #region Descripción de Solicitud

                    //Mostrar el listado de tramites
                    this.ltlTramiteLiquidacion.Text = "Trámite:";
                    this.spnTramiteLiquidacion.Attributes.Remove("title");
                    this.spnTramiteLiquidacion.Attributes.Add("title", "Trámite sobre el cual desea realizar la liquidación.");
                    this.CargarListadoTramites(this.cboTramiteLiquidacion, (int)AutoliquidacionTipoSolicitud.OTROS_INSTRUMENTOS, p_intSolicitudID);
                    this.trTramiteLiquidacion.Visible = true;

                    //Ocultar el listado de sectores
                    this.cboSectorLiquidacion.ClearSelection();
                    this.cboSectorLiquidacion.Items.Clear();
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
                    this.spnNombreProyectoLiquidacion.Attributes.Remove("title");
                    this.spnNombreProyectoLiquidacion.Attributes.Add("title", "Nombre del proyecto, obra o actividad que se va a realizar y por el cual se presenta la solicitud de liquidación.");
                    this.spnDescripcionProyectoLiquidacion.Attributes.Remove("title");
                    this.spnDescripcionProyectoLiquidacion.Attributes.Add("title", "Descripción breve del instrumento de manejo y control ambiental objeto del servicio de liquidación.");
                    this.spnValorProyectoLiquidacion.Attributes.Remove("title");
                    this.spnValorProyectoLiquidacion.Attributes.Add("title", "Costo del proyecto, obra o actividad en pesos colombianos.");
                    this.spnValorProyectoLetrasLiquidacion.Attributes.Remove("title");
                    this.spnValorProyectoLetrasLiquidacion.Attributes.Add("title", "Costo del proyecto, obra o actividad en letras.");
                    this.spnValorModificacionLiquidacion.Attributes.Remove("title");
                    this.spnValorModificacionLiquidacion.Attributes.Add("title", "Valor de modificación del proyecto, obra o actividad en pesos colombianos.");
                    this.spnValorModificacionLetrasLiquidacion.Attributes.Remove("title");
                    this.spnValorModificacionLetrasLiquidacion.Attributes.Add("title", "Valor en letras de la modificación del proyecto, obra o actividad.");
                    this.rfvNombreProyectoLiquidacion.ErrorMessage = "Debe ingresar el nombre del proyecto, obra o actividad a realizar.";
                    this.rfvDescripcionProyectoLiquidacion.ErrorMessage = "Debe ingresar una breve descripción del proyecto, obra o actividad a realizar.";
                    this.rfvValorProyectoLiquidacion.ErrorMessage = "Debe ingresar el costo del proyecto.";
                    this.rfvValorModificacionLiquidacion.ErrorMessage = "Debe ingresar el valor de la modificación.";

                    //Inicializar los controles descripción de proyecto
                    this.cboProyectoLiquidacion.ClearSelection();
                    this.cboProyectoLiquidacion.Items.Clear();
                    this.cboActividadLiquidacion.ClearSelection();
                    this.cboActividadLiquidacion.Items.Clear();
                    this.txtNombreProyectoLiquidacion.Text = "";
                    this.txtDescripcionProyectoLiquidacion.Text = "";
                    this.txtValorProyectoLiquidacion.Text = "";
                    this.txtValorProyectoLetrasLiquidacion.Text = "";
                    this.txtValorModificacionLiquidacion.Text = "";
                    this.txtValorModificacionLetrasLiquidacion.Text = "";
                    this.cboProyectoPINELiquidacion.ClearSelection();

                    //Mostrar controles descripción de proyecto                    
                    this.trNombreProyectoLiquidacion.Visible = true;
                    this.trDescripcionProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLiquidacion.Visible = true;
                    this.trValorProyectoLetrasLiquidacion.Visible = true;
                    if (p_intSolicitudID == (int)AutoliquidacionSolicitud.Modificacion)
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

                    //Inicializar campos y listados
                    this.ltlTituloPermisosRequeridos.Text = "";
                    this.spnPermisosLiquidacion.Attributes.Remove("title");
                    this.spnPermisosLiquidacion.Attributes.Add("title", "");
                    this.PermisosLiquidacion = new List<PermisoSolicitudLiquidacionEntity>();
                    this.grdPermisosLiquidacion.DataSource = this.PermisosLiquidacion;
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
                    this.spnRegionLiquidacion.Attributes.Remove("title");
                    this.spnRegionLiquidacion.Attributes.Add("title", "Región donde se va a desarrollar el proyecto, obra o actividad.");
                    this.spnLocalizacionLiquidacion.Attributes.Remove("title");
                    this.spnLocalizacionLiquidacion.Attributes.Add("title", "Ubicación del proyecto, obra o actividad.<br/>Para ingresar la información sobre la ubicación hacer clic sobre el botón \"Agregar\".");
                    this.spnAguasMaritimasLiquidacion.Attributes.Remove("title");
                    this.spnAguasMaritimasLiquidacion.Attributes.Add("title", "");
                    this.spnCualAguaMaritimaLiquidacion.Attributes.Remove("title");
                    this.spnCualAguaMaritimaLiquidacion.Attributes.Add("title", "");

                    //Inicializar campos de la seccion de ubicación
                    this.CargarListadoRegiones(this.chklRegionAutoliquidacion);
                    this.UbicacionesLiquidacion = new List<UbicacionSolicitudLiquidacionEntity>();
                    this.grdUbicacionLiquidacion.DataSource = this.UbicacionesLiquidacion;
                    this.grdUbicacionLiquidacion.DataBind();
                    this.cboAguasMaritimasLiquidacion.ClearSelection();
                    this.cboAutoridadAmbientalLiquidacion.ClearSelection();
                    this.cboAutoridadAmbientalLiquidacion.Items.Clear();

                    //Ocultar campos sección de ubicación
                    this.trCualAguaMaritimaLiquidacion.Visible = false;                    
                    this.trAguasMaritimasLiquidacion.Visible = false;

                    //Ocultar o mostrar listado de autoridades
                    this.MostrarCamposAutoridadesAmbientales();

                    //Mostrar seccion de ubicación
                    this.tblLocalizacionProyecto.Visible = true;

                #endregion

                #region Ruta

                    //Inicializar label y mensajes de la ruta
                    this.ltlTituloRutaLogistica.Text = "Ruta Logística para Acceder a los Puntos Solicitados para el Uso y/o Aprovechamiento de los Recursos Naturales";
                    this.ltlRutaLogisticaLiquidacion.Text = "Ruta Logística:";
                    this.spnRutaLogisticaLiquidacion.Attributes.Remove("title");
                    this.spnRutaLogisticaLiquidacion.Attributes.Add("title", "Ruta Logística para acceder a los puntos solicitados para el uso y/o aprovechamiento de los recursos naturales.<br/>Para ingresar la información sobre la ruta logística hacer clic sobre el botón \"Agregar\".");

                    //Inicializar los campos de la ruta
                    this.RutasLogisticasLiquidacion = new List<RutaLogisticaSolicitudLiquidacionEntity>();
                    this.grdRutaLiquidacion.DataSource = this.RutasLogisticasLiquidacion;
                    this.grdRutaLiquidacion.DataBind();

                    //Ocultar sección de ruta
                    this.tblRutaLogistica.Visible = false;

                #endregion

            }

            /// <summary>
            /// Mostrar campos según tipo de solicitud y actividad(solicitud) especificada
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud</param>
            /// <param name="p_intSolicitudID">int con el identificador de la solicitud</param>
            private void MostrarCamposSolicitudTipoSolicitud(int p_intTipoSolicitudID, int p_intSolicitudID)
            {
                //Verificar 
                if (p_intTipoSolicitudID == (int)AutoliquidacionTipoSolicitud.LICENCIA_AMBIENTAL)
                {
                    this.MostrarCamposLicenciaAmbiental(p_intSolicitudID);
                }
                else if (p_intTipoSolicitudID == (int)AutoliquidacionTipoSolicitud.PERMISO)
                {
                    this.MostrarCamposPermisos(p_intSolicitudID);
                }
                else if (p_intTipoSolicitudID == (int)AutoliquidacionTipoSolicitud.OTROS_INSTRUMENTOS)
                {
                    this.MostrarCamposOtrosInstrumentos(p_intSolicitudID);
                }
            }

            
            /// <summary>
            /// Mostrar los campos que se relacionan con las autoridades ambientales
            /// </summary>
            private void MostrarCamposAutoridadesAmbientales()
            {
                //Limpiar desplegable de autoridades ambientales
                this.cboAutoridadAmbientalLiquidacion.ClearSelection();
                this.cboAutoridadAmbientalLiquidacion.Items.Clear();

                //Limpiar el listado de rutas
                this.RutasLogisticasLiquidacion = new List<RutaLogisticaSolicitudLiquidacionEntity>();
                this.grdRutaLiquidacion.DataSource = this.RutasLogisticasLiquidacion;
                this.grdRutaLiquidacion.DataBind();

                //Ocultar el listado de competencia autoridades y de rutas
                this.tblInformacionCompetenciaAutoridad.Visible = false;
                this.trAutoridadAmbientalLiquidacion.Visible = false;
                this.tblRutaLogistica.Visible = false;

                //Ocultar o mostrar listado de autoridades
                if (this.AutoridadesAmbientalesSolicitud != null && this.AutoridadesAmbientalesSolicitud.Count == 1)
                {
                    //Si es permiso u otro indtrumento y la autoridad no es ANLA mostrar competencia
                    if (this.cboTipoSolicitudLiquidacion.SelectedValue != "-1" && this.cboTipoSolicitudLiquidacion.SelectedValue != ((int)AutoliquidacionTipoSolicitud.LICENCIA_AMBIENTAL).ToString())
                    {
                        //Mostrar pregunta de competencia
                        this.tblInformacionCompetenciaAutoridad.Visible = true;
                        this.trAutoridadAmbientalLiquidacion.Visible = false;
                    }
                    else
                    {
                        this.tblRutaLogistica.Visible = true;                      
                    }
                }
                else if (this.AutoridadesAmbientalesSolicitud != null && this.AutoridadesAmbientalesSolicitud.Count > 1)
                {
                    //Cargar el listado de autoridades
                    this.cboAutoridadAmbientalLiquidacion.DataSource = this.AutoridadesAmbientalesSolicitud;
                    this.cboAutoridadAmbientalLiquidacion.DataValueField = "IdAutoridad";
                    this.cboAutoridadAmbientalLiquidacion.DataTextField = "Nombre";
                    this.cboAutoridadAmbientalLiquidacion.DataBind();
                    this.cboAutoridadAmbientalLiquidacion.Items.Insert(0, new ListItem("Seleccione.", "-1"));

                    //Mostrar pregunta de competencia o listado de autoridades
                    this.tblInformacionCompetenciaAutoridad.Visible = true;
                    this.trAutoridadAmbientalLiquidacion.Visible = true;
                }
            }


            /// <summary>
            /// Mostrar el mensaje de confirmación para las autoridades ambientales
            /// </summary>
            private string ObtenerMensajeAutoridadAmbiental(int p_inAutoridadID)
            {
                string strNombreAutoridad = "";
                string strMensaje = "";

                //Cargar el nombre de la autoridad ambiental
                strNombreAutoridad = (this.AutoridadesAmbientalesSolicitud.Where(autoridad => autoridad.IdAutoridad == p_inAutoridadID).ToList())[0].Nombre;

                //Cargar mensaje
                strMensaje = "Se evíara la solicitud de <b>" + cboSolicitudLiquidacion.SelectedItem.Text + "</b> para <b>" + this.cboTipoSolicitudLiquidacion.SelectedItem.Text + "</b> " +
                             " a la autoridad ambiental <b>" + strNombreAutoridad + "</b> para su estudio, calculo de la liquidación y cobro correspondiente.<br /><br />" +
                             "Una vez reciba el valor del cobro por parte de <b>" + strNombreAutoridad + "</b> el pago deberá realizarlo por los canales que la autoridad ambiental ofrezca para tal fin.<br /><br />Recuerde que en caso de encontrar inconsistencias en la información suministrada o no concordar con la documentación que posteriormente se exija para el comienzo de los procesos asociados a la liquidación se puede presentar cobros adicionales que se ajusten a la nueva información.";

                return strMensaje;
            }


            /// <summary>
            /// Oculta todos los campos y secciones del formulario
            /// </summary>
            private void OcultarSeccionesFormulario()
            {
                //Mostrar seccion de datos de solicitud y ocultar campos
                this.tblDescripcionSolicitud.Visible = true;
                this.trSolicitudLiquidacion.Visible = false;
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

                //Ocultar competencia y campos
                this.tblInformacionCompetenciaAutoridad.Visible = false;
                this.trAutoridadAmbientalLiquidacion.Visible = false;
            }


            /// <summary>
            /// Cargar la información inicial de la pagina
            /// </summary>
            private void InicializarPagina()
            {
                try
                {
                    //Limpiar campos del formulario
                    this.LimpiarCamposFormulario();
                    
                    //Cargar el listado de tipos de solicitud
                    this.CargarListadoTiposSolicitud(this.cboTipoSolicitudLiquidacion);

                    //Ocultar campos y secciones del formulario
                    this.OcultarSeccionesFormulario();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento error cargando información inicial del formulario");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: InicializarPagina -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }        

        #endregion


        #region Manejo de Información


            /// <summary>
            /// Cargar el listado de autoridades ambientales que se encuentra relacionados a la informacion de la solicitud
            /// </summary>
            /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud</param>
            /// <param name="p_intTramiteID">int con el identificador del tramite que se esta realizando</param>
            /// <param name="p_intTipoProyectoID">int con el tipo de proyecto</param>
            /// <param name="p_lstUbicaciones">List con la información de ubicaciones</param>
            /// <returns>List con la información de las entidades a las cuales se relaciona</returns>
            private void CargarAutoridadesAmbientalesSolicitud(int p_intTipoSolicitudID, int p_intTramiteID, int p_intTipoProyectoID, List<UbicacionSolicitudLiquidacionEntity> p_lstUbicaciones)
            {
                Autoliquidacion objAutoliquidacion = null;
                List<int> objLstIdUbicaciones = null;

                //Cargar el listado de ubicaciones
                if (p_lstUbicaciones != null && p_lstUbicaciones.Count > 0)
                {
                    //Crear listado
                    objLstIdUbicaciones = new List<int>();

                    //Ciclo que carga el identificador de municipios
                    foreach (UbicacionSolicitudLiquidacionEntity objUbicacion in p_lstUbicaciones)
                    {
                        objLstIdUbicaciones.Add(objUbicacion.Municipio.Id);
                    }
                }

                //Obtener el listado de autoridades
                objAutoliquidacion = new Autoliquidacion();
                this.AutoridadesAmbientalesSolicitud = objAutoliquidacion.ConsultarAutoridadAmbientalSolicitud(p_intTipoSolicitudID, p_intTramiteID, p_intTipoProyectoID, objLstIdUbicaciones);
            }


            /// <summary>
            /// Carga la información capturada en la solictud en el objeto correspondiente
            /// </summary>
            /// <returns>SolicitudLiquidacionEntity con la información capturada en la solicitud</returns>
            private SolicitudLiquidacionEntity CargarInformacionSolicitud()
            {
                int intAutoridadID = 0;
                string strNombreAutoridad = "";
                SolicitudLiquidacionEntity objSolicitudLiquidacion = null;
     
                //Cargar los datos de la entidad
                if (this.trAutoridadAmbientalLiquidacion.Visible && this.cboAutoridadAmbientalLiquidacion.SelectedValue != "-1")
                {
                    intAutoridadID = Convert.ToInt32(this.cboAutoridadAmbientalLiquidacion.SelectedValue);
                    strNombreAutoridad = (this.AutoridadesAmbientalesSolicitud.Where(autoridad => autoridad.IdAutoridad == intAutoridadID).ToList())[0].Nombre;
                }
                else if (this.AutoridadesAmbientalesSolicitud != null && this.AutoridadesAmbientalesSolicitud.Count == 1)
                {
                    intAutoridadID = this.AutoridadesAmbientalesSolicitud[0].IdAutoridad;
                    strNombreAutoridad = (this.AutoridadesAmbientalesSolicitud.Where(autoridad => autoridad.IdAutoridad == intAutoridadID).ToList())[0].Nombre;
                }
                else
                {
                    throw new Exception("No se obtuvo el identificador de la entidad");
                }                

                //Crear el objeto
                objSolicitudLiquidacion = new SolicitudLiquidacionEntity();

                //Cargar los datos de la solicitud
                objSolicitudLiquidacion.SolicitanteID = this.SolicitanteID;
                objSolicitudLiquidacion.TipoSolicitud = new TipoSolicitudLiquidacionEntity { TipoSolicitudID = Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue), TipoSolicitud = this.cboTipoSolicitudLiquidacion.SelectedItem.Text };
                objSolicitudLiquidacion.ClaseSolicitud = new ClaseSolicitudLiquidacionEntity { ClaseSolicitudID = Convert.ToInt32(this.cboSolicitudLiquidacion.SelectedValue), ClaseSolicitud = this.cboSolicitudLiquidacion.SelectedItem.Text };
                if (this.trTramiteLiquidacion.Visible && this.cboTramiteLiquidacion.SelectedValue != "-1")
                    objSolicitudLiquidacion.Tramite = new TramiteLiquidacionEntity { TramiteID = Convert.ToInt32(this.cboTramiteLiquidacion.SelectedValue), Tramite = this.cboTramiteLiquidacion.SelectedItem.Text };
                else
                    objSolicitudLiquidacion.Tramite = null;
                if (this.trSectorLiquidacion.Visible && this.cboSectorLiquidacion.SelectedValue != "-1")
                    objSolicitudLiquidacion.Sector = new SectorLiquidacionEntity { SectorID = Convert.ToInt32(this.cboSectorLiquidacion.SelectedValue), Sector = this.cboSectorLiquidacion.SelectedItem.Text };
                else
                    objSolicitudLiquidacion.Sector = null;
                objSolicitudLiquidacion.AutoridadAmbiental = new AutoridadAmbientalIdentity { IdAutoridad = intAutoridadID, Nombre = strNombreAutoridad };                
                if (this.trProyectoLiquidacion.Visible && this.cboProyectoLiquidacion.SelectedValue != "-1")
                    objSolicitudLiquidacion.Proyecto = new ProyectoLiquidacionEntity { ProyectoID = Convert.ToInt32(this.cboProyectoLiquidacion.SelectedValue), Proyecto = this.cboProyectoLiquidacion.SelectedItem.Text };
                else
                    objSolicitudLiquidacion.Proyecto = null;
                if (this.trActividadLiquidacion.Visible && this.cboActividadLiquidacion.SelectedValue != "-1")
                    objSolicitudLiquidacion.Actividad = new ActividadLiquidacionEntity { ActividadID = Convert.ToInt32(this.cboActividadLiquidacion.SelectedValue), Actividad = this.cboActividadLiquidacion.SelectedItem.Text };
                else
                    objSolicitudLiquidacion.Actividad = null;
                objSolicitudLiquidacion.NombreProyecto = this.txtNombreProyectoLiquidacion.Text.Trim();
                objSolicitudLiquidacion.DescripcionProyecto = this.txtDescripcionProyectoLiquidacion.Text.Trim();                
                objSolicitudLiquidacion.ValorProyecto = (!string.IsNullOrWhiteSpace(this.txtValorProyectoLiquidacion.Text.Trim()) ? Convert.ToDecimal(this.txtValorProyectoLiquidacion.Text.Trim()) : 0);
                objSolicitudLiquidacion.ValorProyectoLetras = SILPA.LogicaNegocio.Utilidad.Utilidades.NumeroALetras(this.txtValorProyectoLiquidacion.Text.Trim()).ToUpper();
                if (objSolicitudLiquidacion.ClaseSolicitud.ClaseSolicitudID == (int)AutoliquidacionSolicitud.Modificacion)
                {
                    objSolicitudLiquidacion.ValorModificacion = (!string.IsNullOrWhiteSpace(this.txtValorModificacionLiquidacion.Text.Trim()) ? Convert.ToDecimal(this.txtValorModificacionLiquidacion.Text.Trim()) : 0);
                    objSolicitudLiquidacion.ValorModificacionLetras = SILPA.LogicaNegocio.Utilidad.Utilidades.NumeroALetras(this.txtValorModificacionLiquidacion.Text.Trim()).ToUpper();
                }
                else
                {
                    objSolicitudLiquidacion.ValorModificacion = -1;
                    objSolicitudLiquidacion.ValorModificacionLetras = "";
                }
                if (this.trProyectoPINELiquidacion.Visible && this.cboProyectoPINELiquidacion.SelectedValue != "-1")
                    objSolicitudLiquidacion.ProyectoPINE = (this.cboProyectoPINELiquidacion.SelectedValue == "1" ? true : false);
                else
                    objSolicitudLiquidacion.ProyectoPINE = null;
                if (this.trAguasMaritimasLiquidacion.Visible && this.cboAguasMaritimasLiquidacion.SelectedValue != "-1")
                    objSolicitudLiquidacion.ProyectoAguasMaritimas = (this.cboAguasMaritimasLiquidacion.SelectedValue == "1" ? true : false);
                else
                    objSolicitudLiquidacion.ProyectoAguasMaritimas = null;

                if (this.trCualAguaMaritimaLiquidacion.Visible && this.cboCualAguaMaritimaLiquidacion.SelectedValue != "-1")
                    objSolicitudLiquidacion.Oceano = new OceanoLiquidacionEntity { OceanoID = Convert.ToInt32(this.cboCualAguaMaritimaLiquidacion.SelectedValue), Oceano = this.cboCualAguaMaritimaLiquidacion.SelectedItem.Text };
                else
                    objSolicitudLiquidacion.Oceano = null;                
                objSolicitudLiquidacion.NumeroVITAL = "";
                objSolicitudLiquidacion.EstadoSolicitud = null;

                //Ciclo que carga las regiones que fueron seleccionadas
                foreach (ListItem objOpcion in this.chklRegionAutoliquidacion.Items)
                {
                    //Verificar si se encuentra seleccionado
                    if (objOpcion.Selected)
                    {
                        //Verificar si el listado se creo
                        if (objSolicitudLiquidacion.Regiones == null)
                            objSolicitudLiquidacion.Regiones = new List<RegionSolicitudLiquidacionEntity>();

                        //Cargar region
                        objSolicitudLiquidacion.Regiones.Add(new RegionSolicitudLiquidacionEntity { Region = new RegionLiquidacionEntity { RegionID = Convert.ToInt32(objOpcion.Value), Region = objOpcion.Text } });
                    }
                }
                
                //Cargar listados
                objSolicitudLiquidacion.Permisos = this.PermisosLiquidacion;
                objSolicitudLiquidacion.Ubicaciones = this.UbicacionesLiquidacion;
                objSolicitudLiquidacion.Rutas = this.RutasLogisticasLiquidacion;

                return objSolicitudLiquidacion;
            }


            /// <summary>
            /// Radicar la solicitud de liquidación
            /// </summary>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity con la información de la solicitud</param>
            /// <returns>int con el identificador de la solicitud de liquidación</returns>
            private int RadicarSolicitudLiquidacion(SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                Autoliquidacion objAutoliquidacion = null;

                //Crear objeto de radicación
                objAutoliquidacion = new Autoliquidacion();
                
                //Retornar respuesta de liquidación
                return objAutoliquidacion.RadicarSolicitudLiquidacion(p_objSolicitudLiquidacion);
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


        #region cboTipoSolicitudLiquidacion

            /// <summary>
            /// Carga el listado de solicitudes dependiendo del tipo de solicitud
            /// </summary>
            protected void cboTipoSolicitudLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar listados
                    this.TramitesLiquidacion = new List<TramiteLiquidacionEntity>();
                    this.PermisosLiquidacion = new List<PermisoSolicitudLiquidacionEntity>();
                    this.UbicacionesLiquidacion = new List<UbicacionSolicitudLiquidacionEntity>();
                    this.RutasLogisticasLiquidacion = new List<RutaLogisticaSolicitudLiquidacionEntity>();
                    this.CoordenadasUbicacionLiquidacion = new List<CoordenadaUbicacionLiquidacionEntity>();
                    this.AutoridadesAmbientalesSolicitud = null;

                    //Ocultar todos los campos
                    this.OcultarSeccionesFormulario();

                    //Verificar si se selecciono opción del listado
                    if (((DropDownList)sender).SelectedValue != "-1")
                    {
                        //Cargar listado de solicitudes
                        this.CargarListadoClasesSolicitudes(this.cboSolicitudLiquidacion, Convert.ToInt32(((DropDownList)sender).SelectedValue));

                        //Mostrar el listado de solicitudes
                        this.trSolicitudLiquidacion.Visible = true;

                        //Cargar el listado de autoridades ambientales relacionados
                        this.CargarAutoridadesAmbientalesSolicitud(Convert.ToInt32(((DropDownList)sender).SelectedValue), -1, -1, null);
                    }                    
                    
                    //Actualizar panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando el listado de solicitudes");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboTipoSolicitudLiquidacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region cboSolicitudLiquidacion

            /// <summary>
            /// Carga el listado de tramites acorde a la solicitud y tipo de liquidación
            /// </summary>            
            protected void cboSolicitudLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Verificar si selecciono una opción
                    if (((DropDownList)sender).SelectedValue != "-1")
                    {
                        //Mostrar campos relacionados a la solicitud
                        this.MostrarCamposSolicitudTipoSolicitud(Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue), Convert.ToInt32(((DropDownList)sender).SelectedValue));
                    }
                    else
                    {
                        //Ocultar todos los campos
                        this.OcultarSeccionesFormulario();

                        //Cargar listado de solicitudes
                        this.CargarListadoClasesSolicitudes(this.cboSolicitudLiquidacion, Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue));

                        //Mostrar el listado de solicitudes
                        this.trSolicitudLiquidacion.Visible = true;
                    }

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando campos de formulario");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboSolicitudLiquidacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region cboTramiteLiquidacion

            /// <summary>
            /// Carga el listado de autoridades relacionados a la información existente en la solicitud
            /// </summary>            
            protected void cboTramiteLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Modificar mensajes de ayuda si es beneficio tributario
                    if (((DropDownList)sender).SelectedItem.Text.ToLower().Contains("beneficios tributarios"))
                    {
                        this.ltlValorProyectoLiquidacion.Text = "Valor del Beneficio en Pesos Colombianos";
                        this.ltlValorModificacionLiquidacion.Text = "Valor de Modificación del Beneficio en Pesos Colombianos:";
                        this.spnValorProyectoLiquidacion.Attributes.Remove("title");
                        this.spnValorProyectoLiquidacion.Attributes.Add("title", "Valor del beneficio tributario en pesos colombianos.");
                        this.spnValorProyectoLetrasLiquidacion.Attributes.Remove("title");
                        this.spnValorProyectoLetrasLiquidacion.Attributes.Add("title", "Valor del beneficio tributario en letras.");
                        this.rfvValorProyectoLiquidacion.ErrorMessage = "Debe ingresar el valor del beneficio tributario.";
                        this.spnValorModificacionLiquidacion.Attributes.Remove("title");
                        this.spnValorModificacionLiquidacion.Attributes.Add("title", "Valor de la modificación del beneficio tributario en pesos colombianos.");
                        this.spnValorModificacionLetrasLiquidacion.Attributes.Remove("title");
                        this.spnValorModificacionLetrasLiquidacion.Attributes.Add("title", "Valor en letras de la modificación del beneficio tributario.");
                        this.rfvValorModificacionLiquidacion.ErrorMessage = "Debe ingresar el valor de la modificación del beneficio tributario.";

                    }
                    else
                    {
                        this.ltlValorProyectoLiquidacion.Text = "Costo del Proyecto en Pesos Colombianos";
                        this.ltlValorModificacionLiquidacion.Text = "Valor de Modificación en Pesos Colombianos:";
                        this.spnValorProyectoLiquidacion.Attributes.Remove("title");
                        this.spnValorProyectoLiquidacion.Attributes.Add("title", "Costo del proyecto, obra o actividad en pesos colombianos.");
                        this.spnValorProyectoLetrasLiquidacion.Attributes.Remove("title");
                        this.spnValorProyectoLetrasLiquidacion.Attributes.Add("title", "Costo del proyecto, obra o actividad en letras.");
                        this.rfvValorProyectoLiquidacion.ErrorMessage = "Debe ingresar el costo del proyecto.";
                        this.spnValorModificacionLiquidacion.Attributes.Remove("title");
                        this.spnValorModificacionLiquidacion.Attributes.Add("title", "Valor de modificación del proyecto, obra o actividad en pesos colombianos.");
                        this.spnValorModificacionLetrasLiquidacion.Attributes.Remove("title");
                        this.spnValorModificacionLetrasLiquidacion.Attributes.Add("title", "Valor en letras de la modificación del proyecto, obra o actividad.");
                        this.rfvValorModificacionLiquidacion.ErrorMessage = "Debe ingresar el valor de la modificación.";
                    }


                    //Consultar autoridades ambientales
                    this.CargarAutoridadesAmbientalesSolicitud(Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue),
                                                               Convert.ToInt32(((DropDownList)sender).SelectedValue),
                                                               (this.trActividadLiquidacion.Visible ? Convert.ToInt32(this.cboActividadLiquidacion.SelectedValue) : -1),
                                                               this.UbicacionesLiquidacion);

                    //Mostrar u ocultar el listado de autoridades ambientales
                    this.MostrarCamposAutoridadesAmbientales();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando información de autoridades ambientales");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboTramiteLiquidacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region cboProyectoLiquidacion

            /// <summary>
            /// Evento que carga el listado de proyectos pertenecientes a un sector
            /// </summary>
            protected void cboSectorLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar el listado de proyectos
                    this.CargarListadoProyectos(this.cboProyectoLiquidacion, Convert.ToInt32(((DropDownList)sender).SelectedValue));

                    //Cargar actividades 
                    this.CargarListadoActividades(this.cboActividadLiquidacion, -1);

                    //Consultar autoridades ambientales
                    this.CargarAutoridadesAmbientalesSolicitud(Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue),
                                                               (this.trTramiteLiquidacion.Visible ? Convert.ToInt32(this.cboTramiteLiquidacion.SelectedValue) : -1),
                                                               -1,
                                                               this.UbicacionesLiquidacion);

                    //Mostrar u ocultar el listado de autoridades ambientales
                    this.MostrarCamposAutoridadesAmbientales();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando el listado de proyectos");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboSectorLiquidacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region cboProyectoLiquidacion

            /// <summary>
            /// Evento que permite cargar el listado de actividades de acuerdo al proyecto seleccionado
            /// </summary>
            protected void cboProyectoLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar actividades de acuerdo a seleccion                    
                    this.CargarListadoActividades(this.cboActividadLiquidacion, Convert.ToInt32(((DropDownList)sender).SelectedValue));

                    //Consultar autoridades ambientales
                    this.CargarAutoridadesAmbientalesSolicitud(Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue),
                                                               (this.trTramiteLiquidacion.Visible ? Convert.ToInt32(this.cboTramiteLiquidacion.SelectedValue) : -1),
                                                               -1,
                                                               this.UbicacionesLiquidacion);

                    //Mostrar u ocultar el listado de autoridades ambientales
                    this.MostrarCamposAutoridadesAmbientales();                  
                    
                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando el listado de actividades");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboProyectoLiquidacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region cboActividadLiquidacion

            /// <summary>
            /// Carga el listado de autoridades relacionados a la información existente en la solicitud
            /// </summary>            
            protected void cboActividadLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Consultar autoridades ambientales
                    this.CargarAutoridadesAmbientalesSolicitud(Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue),
                                                               (this.trTramiteLiquidacion.Visible ? Convert.ToInt32(this.cboTramiteLiquidacion.SelectedValue) : -1),
                                                               Convert.ToInt32(((DropDownList)sender).SelectedValue),
                                                               this.UbicacionesLiquidacion);

                    //Mostrar u ocultar el listado de autoridades ambientales
                    this.MostrarCamposAutoridadesAmbientales();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando información de autoridades ambientales");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboActividadLiquidacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region cboAutoridadAmbientalLiquidacion

            /// <summary>
            /// Evento que muestra campos según el tipo solicitud, solicitud y autoridad ambiental
            /// </summary>
            protected void cboAutoridadAmbientalLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
            {  
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar el listado de rutas
                    this.RutasLogisticasLiquidacion = new List<RutaLogisticaSolicitudLiquidacionEntity>();
                    this.grdRutaLiquidacion.DataSource = this.RutasLogisticasLiquidacion;
                    this.grdRutaLiquidacion.DataBind();

                    //Ocultar el listado de autoridades y de rutas
                    this.tblRutaLogistica.Visible = false;

                    //Verificar que seleccionará un tipo de solicitud
                    if (((DropDownList)sender).SelectedValue != "-1")
                    {
                        //Mostrar ruta logistica
                        this.tblRutaLogistica.Visible = true;
                    }
                    else
                    {
                        //Ocultar ruta logistica
                        this.tblRutaLogistica.Visible = false;
                    }

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando campos de autoridad ambiental");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboAutoridadAmbientalLiquidacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region cboAguasMaritimasLiquidacion

            /// <summary>
            /// Evento que muestra u oculta el desplegable de oceanos
            /// </summary>
            protected void cboAguasMaritimasLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Verificar que seleccionará un tipo de solicitud
                    if (((DropDownList)sender).SelectedValue == "1")
                    {
                        //Cargar el listado de oceanos
                        this.CargarListadoOceanos(this.cboCualAguaMaritimaLiquidacion);

                        //Mostrar listado de aguas maritimas
                        this.trCualAguaMaritimaLiquidacion.Visible = true;
                    }
                    else
                    {
                        //Ocultar listado de aguas maritimas
                        this.cboCualAguaMaritimaLiquidacion.ClearSelection();
                        this.cboCualAguaMaritimaLiquidacion.Items.Clear();
                        this.trCualAguaMaritimaLiquidacion.Visible = false;                        
                    }

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando listado de oceanos");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboAguasMaritimasLiquidacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                
            }

        #endregion
    
        
        #region cmdEnviar

            /// <summary>
            /// Envia la solicitud de liquidación
            /// </summary>
            protected void cmdEnviar_Click(object sender, EventArgs e)
            {
                int intAutoridadID = 0;

                try
                {
                    //Verificar que se encuentre correctamente diligenciado el formulario
                    if (Page.IsValid)
                    {
                        //Limpiar campos del modal
                        this.LimpiarCamposModalConfirmarEnvioSolicitud();

                        //Cargar el identificador de la autoridad ambiental
                        if (this.trAutoridadAmbientalLiquidacion.Visible && this.cboAutoridadAmbientalLiquidacion.SelectedValue != "-1")
                        {
                            intAutoridadID = Convert.ToInt32(this.cboAutoridadAmbientalLiquidacion.SelectedValue);
                        }
                        else if (this.AutoridadesAmbientalesSolicitud != null && this.AutoridadesAmbientalesSolicitud.Count == 1)
                        {
                            intAutoridadID = this.AutoridadesAmbientalesSolicitud[0].IdAutoridad;
                        }
                        else
                        {
                            throw new Exception("No se especifico la autoridad ambiental.");
                        }

                        //Verificar que la autoridad sea diferente de la ANLA
                        if (intAutoridadID != (int)AutoridadesAmbientales.ANLA)
                        {
                            //Cargar mensaje patra autoridades ambientales
                            this.ltlTerminosConfirmarEnvioSolicitud.Text = this.ObtenerMensajeAutoridadAmbiental(intAutoridadID);

                            //Actualizar y mostrar modal
                            this.upnlConfirmarEnvioSolicitud.Update();
                            this.mpeConfirmarEnvioSolicitud.Show();
                        }
                        else
                        {
                            throw new Exception("Autoridad ambiental ANLA no soportada por el sistema.");
                        }

                    }
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un cargando el modal de confirmación de envío de información.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdEnviar_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion


        #region cmdCancelar

            /// <summary>
            /// Evento que cancela el proceso de solicitud de liquidación
            /// </summary>
            protected void cmdCancelar_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar los mensajes
                    this.OcultarMensaje();

                    //Limpiar campos
                    this.LimpiarCamposFormulario();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();

                    //Direccionar al listado de solicitudes
                    Response.Redirect("SolicitudesAutoliquidacion.aspx", false);

                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento error cancelando el proceso de liquidación.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdCancelar_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion

        
        #region grdPermisosLiquidacion

            /// <summary>
            /// Evento que muestra modal para ingresar un nuevo permiso
            /// </summary>
            protected void cmdAgregarPermisoLiquidacion_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarPermiso();

                    //Cargar listado de permisos
                    this.CargarListadoPermisos(this.cboPermisoAgregarPermiso);

                    //Cargar listado de autoridades competentes cuando no es permiso
                    this.CargarListadoAutoridadesPermisos(this.cboAutoridadAgregarPermiso);

                    //Actualizar panel de modal
                    this.upnlAgregarPermiso.Update();

                    //Cerrar modal
                    this.mpeAgregarPermiso.Show();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando información de modal");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdAgregarPermisoLiquidacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarPermiso();

                    //Actualizar panel de modal
                    this.upnlAgregarPermiso.Update();

                    //Cerrar modal
                    this.mpeAgregarPermiso.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }                
            }


            /// <summary>
            /// Evento que elimina un permiso del listado
            /// </summary>
            protected void lnkEliminarPermiso_Click(object sender, EventArgs e)
            {
                int intPosicion = 0;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar posición del registro a eliminar
                    intPosicion = Convert.ToInt32(((LinkButton)sender).CommandArgument);

                    //Eliminar registro del listado
                    this.PermisosLiquidacion.RemoveAt(intPosicion);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error eliminando permiso del listado");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: lnkEliminarPermiso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion

    
        #region grdUbicacionLiquidacion

            /// <summary>
            /// Evento que muestra modal para ingreso información de ruta
            /// </summary>
            protected void cmdAgregarUbicacionLiquidacion_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarUbicacion();

                    //Cargar los listados correspondientes
                    this.CargarListadoDepartamentosUbicacion(this.cboDepartamentoAgregarUbicacion);

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
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdAgregarUbicacionLiquidacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();

                    //Cerrar modal
                    this.mpeAgregarUbicacion.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }  
            }


            /// <summary>
            /// Evento que elimina un permiso del listado
            /// </summary>
            protected void lnkEliminarUbicacion_Click(object sender, EventArgs e)
            {
                int intPosicion = 0;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar posición del registro a eliminar
                    intPosicion = Convert.ToInt32(((LinkButton)sender).CommandArgument);

                    //Eliminar registro del listado
                    this.UbicacionesLiquidacion.RemoveAt(intPosicion);

                    //Consultar autoridades ambientales
                    this.CargarAutoridadesAmbientalesSolicitud(Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue),
                                                               (this.trTramiteLiquidacion.Visible ? Convert.ToInt32(this.cboTramiteLiquidacion.SelectedValue) : -1),
                                                               (this.trActividadLiquidacion.Visible ? Convert.ToInt32(this.cboActividadLiquidacion.SelectedValue) : -1),
                                                               this.UbicacionesLiquidacion);

                    //Mostrar u ocultar el listado de autoridades ambientales
                    this.MostrarCamposAutoridadesAmbientales();

                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error eliminando ubicación del listado");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: lnkEliminarUbicacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());                    
                }
                finally
                {
                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion


        #region grdRutaLiquidacion

            /// <summary>
            /// Evento que muestra modal para ingreso información de ruta
            /// </summary>
            protected void cmdAgregarRutaLiquidacion_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Cargar listado de medios de transporte
                    this.CargarListadoMediosTransporte(this.cboMedioTransporteAgregarRuta);

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();

                    //Mostrar modal
                    this.mpeAgregarRuta.Show();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando información de modal de rutas");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdAgregarRutaLiquidacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();

                    //Cerrar modal
                    this.mpeAgregarRuta.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }  
            }


            /// <summary>
            /// Evento que elimina un permiso del listado
            /// </summary>
            protected void lnkEliminarRuta_Click(object sender, EventArgs e)
            {
                int intPosicion = 0;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar posición del registro a eliminar
                    intPosicion = Convert.ToInt32(((LinkButton)sender).CommandArgument);

                    //Eliminar registro del listado
                    this.RutasLogisticasLiquidacion.RemoveAt(intPosicion);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error eliminando ruta del listado");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: lnkEliminarRuta_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion


        #region Modal Permiso

            /// <summary>
            /// Evento que agrega nuevo permiso a listado y recarga información de listados
            /// </summary>
            protected void cmdAgregarPermiso_Click(object sender, EventArgs e)
            {
                PermisoSolicitudLiquidacionEntity objPermiso;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();
                                        
                    //Cargar información del permiso
                    objPermiso = new PermisoSolicitudLiquidacionEntity
                    {
                        Permiso = new PermisoLiquidacionEntity
                        {
                            PermisoID = Convert.ToInt32(this.cboPermisoAgregarPermiso.SelectedValue),
                            Permiso = this.cboPermisoAgregarPermiso.SelectedItem.Text
                        },
                        AutoridadID = Convert.ToInt32(this.cboAutoridadAgregarPermiso.SelectedValue),
                        Autoridad = this.cboAutoridadAgregarPermiso.SelectedItem.Text,
                        NumeroPermisos = 1
                    };

                    //Si no se encuentra en el listado adicionar
                    if (this.PermisosLiquidacion.Where(permiso => permiso.Permiso.PermisoID == objPermiso.Permiso.PermisoID && permiso.AutoridadID == objPermiso.AutoridadID).ToList().Count == 0)
                    {
                        //Agregar al listado
                        this.PermisosLiquidacion.Add(objPermiso);
                    }
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error agregando nuevo permiso al listado");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdAgregarPermiso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarPermiso();

                    //Actualizar panel de modal
                    this.upnlAgregarPermiso.Update();

                    //Cerrar modal
                    this.mpeAgregarPermiso.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

            /// <summary>
            /// Evento que cierra modal y recarga informacion de listados
            /// </summary>
            protected void cmdCancelarPermiso_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarPermiso();

                    //Actualizar panel de modal
                    this.upnlAgregarPermiso.Update();

                    //Cerrar modal
                    this.mpeAgregarPermiso.Hide();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cerrando modal");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdAgregarPermiso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion

    
        #region Modal Ruta


            /// <summary>
            /// Evento que carga los departamentos que correspondan con el medio de transporte seleccionado
            /// </summary>
            protected void cboMedioTransporteAgregarRuta_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();
                    
                    //Limpiar desplegables
                    this.cboDepartamentoPartidaAgregarRuta.ClearSelection();
                    this.cboDepartamentoPartidaAgregarRuta.Items.Clear();
                    this.cboDepartamentoPartidaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                    this.cboMunicipioPartidaAgregarRuta.ClearSelection();
                    this.cboMunicipioPartidaAgregarRuta.Items.Clear();
                    this.cboMunicipioPartidaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                    this.cboDepartamentoLlegadaAgregarRuta.ClearSelection();
                    this.cboDepartamentoLlegadaAgregarRuta.Items.Clear();
                    this.cboDepartamentoLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                    this.cboMunicipioLlegadaAgregarRuta.ClearSelection();
                    this.cboMunicipioLlegadaAgregarRuta.Items.Clear();
                    this.cboMunicipioLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));

                    //Activiar desplegables
                    this.cboDepartamentoPartidaAgregarRuta.Enabled = true;
                    this.cboMunicipioPartidaAgregarRuta.Enabled = true;

                    //Verificar que se seelccione una opción
                    if (((DropDownList)sender).SelectedValue != "-1")
                    {

                        //Cargar el listado de departamentos de origen
                        this.CargarDepartamentoOrigenRuta(this.cboDepartamentoPartidaAgregarRuta, Convert.ToInt32(((DropDownList)sender).SelectedValue));

                    }                    

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando el listado de departamentos relacionados al medio de tranporte");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboMedioTransporteAgregarRuta_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();

                    //Cerrar modal
                    this.mpeAgregarRuta.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }


            /// <summary>
            /// Evento que carga los municipios de acuerdo al departamento seleccionado
            /// </summary>
            protected void cboDepartamentoPartidaAgregarRuta_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();
                    
                    //Limpiar desplegables
                    this.cboMunicipioPartidaAgregarRuta.ClearSelection();
                    this.cboMunicipioPartidaAgregarRuta.Items.Clear();
                    this.cboMunicipioPartidaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                    this.cboDepartamentoLlegadaAgregarRuta.ClearSelection();
                    this.cboDepartamentoLlegadaAgregarRuta.Items.Clear();
                    this.cboDepartamentoLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                    this.cboMunicipioLlegadaAgregarRuta.ClearSelection();
                    this.cboMunicipioLlegadaAgregarRuta.Items.Clear();
                    this.cboMunicipioLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));

                    //Verificar que se seelccione una opción
                    if (((DropDownList)sender).SelectedValue != "-1")
                    {
                        
                        //Cargar el listado de departamentos de origen
                        this.CargarListadoMunicipiosOrigenRuta(this.cboMunicipioPartidaAgregarRuta, Convert.ToInt32(((DropDownList)sender).SelectedValue), Convert.ToInt32(this.cboMedioTransporteAgregarRuta.SelectedValue));
                    }                    

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando el listado de municipios relacionados al departamento de origen");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboDepartamentoPartidaAgregarRuta_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();

                    //Cerrar modal
                    this.mpeAgregarRuta.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }


            /// <summary>
            /// Evento que carga los deparatmentos de llegada de acurdo al muncipio y medio de tranporte seleccionado
            /// </summary>
            protected void cboMunicipioPartidaAgregarRuta_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar desplegables
                    this.cboDepartamentoLlegadaAgregarRuta.ClearSelection();
                    this.cboDepartamentoLlegadaAgregarRuta.Items.Clear();
                    this.cboDepartamentoLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));
                    this.cboMunicipioLlegadaAgregarRuta.ClearSelection();
                    this.cboMunicipioLlegadaAgregarRuta.Items.Clear();
                    this.cboMunicipioLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));

                    //Verificar que se seelccione una opción
                    if (((DropDownList)sender).SelectedValue != "-1")
                    {
                        //Cargar el listado de departamentos de origen
                        this.CargarDepartamentoDestinoRuta(this.cboDepartamentoLlegadaAgregarRuta, Convert.ToInt32(((DropDownList)sender).SelectedValue), Convert.ToInt32(this.cboMedioTransporteAgregarRuta.SelectedValue));
                    }

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando el listado de departamentos destino relacionados al medio de tranporte");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboMunicipioPartidaAgregarRuta_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();

                    //Cerrar modal
                    this.mpeAgregarRuta.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }


            /// <summary>
            /// Evento que carga los municipios de llegada de acuerdo al depratameto de llegada seleccionado
            /// </summary>
            protected void cboDepartamentoLlegadaAgregarRuta_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar desplegables
                    this.cboMunicipioLlegadaAgregarRuta.ClearSelection();
                    this.cboMunicipioLlegadaAgregarRuta.Items.Clear();
                    this.cboMunicipioLlegadaAgregarRuta.Items.Insert(0, new ListItem("Seleccione.", "S"));

                    //Verificar que se seelccione una opción
                    if (((DropDownList)sender).SelectedValue != "-1")
                    {
                        
                        //Cargar el listado de departamentos de origen
                        this.CargarListadoMunicipiosDestinoRuta(this.cboMunicipioLlegadaAgregarRuta, Convert.ToInt32(this.cboMunicipioPartidaAgregarRuta.SelectedValue), Convert.ToInt32(((DropDownList)sender).SelectedValue), Convert.ToInt32(this.cboMedioTransporteAgregarRuta.SelectedValue));
                    }

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando el listado de municipios destino relacionados al departamento de origen");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboDepartamentoLlegadaAgregarRuta_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();

                    //Cerrar modal
                    this.mpeAgregarRuta.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

            /// <summary>
            /// Evento que agrga una nueva ruta al listado
            /// </summary>
            protected void cdmAgregarRuta_Click(object sender, EventArgs e)
            {
                RutaLogisticaSolicitudLiquidacionEntity objRuta;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar información del permiso
                    objRuta = new RutaLogisticaSolicitudLiquidacionEntity
                    {
                        MedioTransporte = new MedioTransporteLiquidacionEntity
                                              {
                                                  MedioTransporteID = Convert.ToInt32(this.cboMedioTransporteAgregarRuta.SelectedValue),
                                                  MedioTransporte = this.cboMedioTransporteAgregarRuta.SelectedItem.Text
                                              },
                        DepartamentoOrigen = new DepartamentoIdentity 
                                                 {
                                                     Id = Convert.ToInt32(this.cboDepartamentoPartidaAgregarRuta.SelectedValue),
                                                     Nombre = this.cboDepartamentoPartidaAgregarRuta.SelectedItem.Text
                                                 },
                        MunicipioOrigen = new MunicipioIdentity 
                                                {
                                                    Id = Convert.ToInt32(this.cboMunicipioPartidaAgregarRuta.SelectedValue),
                                                    NombreMunicipio = this.cboMunicipioPartidaAgregarRuta.SelectedItem.Text
                                                },
                        DepartamentoDestino = new DepartamentoIdentity 
                                                    {
                                                        Id = Convert.ToInt32(this.cboDepartamentoLlegadaAgregarRuta.SelectedValue),
                                                        Nombre = this.cboDepartamentoLlegadaAgregarRuta.SelectedItem.Text
                                                    },
                        MunicipioDestino = new MunicipioIdentity 
                                                {
                                                    Id = Convert.ToInt32(this.cboMunicipioLlegadaAgregarRuta.SelectedValue),
                                                    NombreMunicipio = this.cboMunicipioLlegadaAgregarRuta.SelectedItem.Text
                                                },
                        TiempoAproximadoTrayecto = this.txtTiempoAgregarRuta.Text.Trim()
                    };

                    //Si no se encuentra en el listado adicionar
                    if (this.RutasLogisticasLiquidacion.Where(ruta => ruta.MunicipioOrigen.Id == objRuta.MunicipioOrigen.Id && ruta.MunicipioDestino.Id == objRuta.MunicipioDestino.Id).ToList().Count == 0)
                    {
                        //Agregar al listado
                        this.RutasLogisticasLiquidacion.Add(objRuta);
                    }
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error agregando nueva ruta al listado");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cdmAgregarRuta_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();

                    //Cerrar modal
                    this.mpeAgregarRuta.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }                
            }


            /// <summary>
            /// Evento que cierra y limpia los campos del modal
            /// </summary>
            protected void cmdCancelarAgregarRuta_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarRuta();

                    //Actualizar panel de modal
                    this.upnlAgregarRuta.Update();

                    //Cerrar modal
                    this.mpeAgregarRuta.Hide();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cerrando modal de rutas");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdCancelarAgregarRuta_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion


        #region Modal Ubicación

            /// <summary>
            /// Evento que muestra listado de municipios pertenecientes al departamento
            /// </summary>
            protected void cboDepartamentoAgregarUbicacion_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar campos
                    this.cboMunicipioAgregarUbicacion.ClearSelection();
                    this.cboMunicipioAgregarUbicacion.Items.Clear();

                    //Inicializar listados
                    this.cboMunicipioAgregarUbicacion.Items.Insert(0, new ListItem("Seleccione.", "S"));

                    //Verificar si se selecciono una opción
                    if (((DropDownList)sender).SelectedValue != "S")
                    {
                        //Cargar listado de municipios
                        this.CargarListadoMunicipiosUbicacion(this.cboMunicipioAgregarUbicacion, Convert.ToInt32(((DropDownList)sender).SelectedValue));
                    }                    

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando municipios");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cboDepartamentoAgregarUbicacion_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarUbicacion();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();

                    //Cerrar modal
                    this.mpeAgregarUbicacion.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }


            /// <summary>
            /// Evento que adiciona una nueva ubicación al listado
            /// </summary>
            protected void cdmAgregarUbicacion_Click(object sender, EventArgs e)
            {
                UbicacionSolicitudLiquidacionEntity objUbicacion = null;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar información obligatoria coordenada
                    objUbicacion = new UbicacionSolicitudLiquidacionEntity
                    {
                        Departamento = new DepartamentoIdentity
                        {
                            Id = Convert.ToInt32(this.cboDepartamentoAgregarUbicacion.SelectedValue),
                            Nombre = this.cboDepartamentoAgregarUbicacion.SelectedItem.Text
                        },
                        Municipio = new MunicipioIdentity
                        {
                            Id = Convert.ToInt32(this.cboMunicipioAgregarUbicacion.SelectedValue),
                            NombreMunicipio = this.cboMunicipioAgregarUbicacion.SelectedItem.Text
                        },
                        Coordenadas = this.CoordenadasUbicacionLiquidacion
                    };

                    //Cargar información opcional de la ubicación                    
                    objUbicacion.Corregimiento = this.txtCorregimientoAgregarUbicacion.Text.Trim();
                    objUbicacion.Vereda = this.txtVeredaAgregarUbicacion.Text.Trim();
                    
                    //Agregar al listado
                    this.UbicacionesLiquidacion.Add(objUbicacion);

                    //Consultar autoridades ambientales
                    this.CargarAutoridadesAmbientalesSolicitud(Convert.ToInt32(this.cboTipoSolicitudLiquidacion.SelectedValue),
                                                               (this.trTramiteLiquidacion.Visible ? Convert.ToInt32(this.cboTramiteLiquidacion.SelectedValue) : -1),
                                                               (this.trActividadLiquidacion.Visible ? Convert.ToInt32(this.cboActividadLiquidacion.SelectedValue) : -1),
                                                               this.UbicacionesLiquidacion);

                    //Mostrar u ocultar el listado de autoridades ambientales
                    this.MostrarCamposAutoridadesAmbientales();

                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error agregando nueva ubicación al listado");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cdmAgregarUbicacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarUbicacion();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();

                    //Cerrar modal
                    this.mpeAgregarUbicacion.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }


            /// <summary>
            /// Evento que cierra modal de ubicación
            /// </summary>
            protected void cmdCancelarAgregarUbicacion_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarUbicacion();

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
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdCancelarAgregarUbicacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }


            /// <summary>
            /// Evento que muestra el modal de ingreso de coordenadas
            /// </summary>
            protected void cmdAgregarCoordenadaAgregarUbicacion_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cerrar modal de ubicación
                    this.mpeAgregarUbicacion.Hide();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarCoordenada();

                    //Cargar los listados del modal
                    this.CargarListadoTiposGeometriaCoordenadas(this.cboTipoGeometriaAgregarCoordenada);
                    this.CargarListadoTiposCoordenada(this.cboTipoCoordenadaAgregarCoordenada);
                    this.CargarListadoOrigenesMagna(this.cboOrigenMagnaSirgasAgregarCoordenada);

                    //Actualizar panel de modal
                    this.upnlAgregarCoordenada.Update();

                    //Cerrar modal
                    this.mpeAgregarCoordenada.Show();
                }
                catch (Exception exc)
                {
                    //Mostrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando modal de coordenadas");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdAgregarCoordenadaAgregarUbicacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarCoordenada();
                    this.LimpiarCamposModalAgregarUbicacion();

                    //Actualizar panel de modal
                    this.upnlAgregarCoordenada.Update();
                    this.upnlAgregarUbicacion.Update();
                    
                    //Cerrar modal
                    this.mpeAgregarCoordenada.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }                
            }


            /// <summary>
            /// Evento que elimina una coordenada del listado
            /// </summary>
            protected void lnkEliminarCoordenadaAgregarUbicacion_Click(object sender, EventArgs e)
            {
                int intPosicion = 0;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar posición del registro a eliminar
                    intPosicion = Convert.ToInt32(((LinkButton)sender).CommandArgument);

                    //Eliminar registro del listado
                    this.CoordenadasUbicacionLiquidacion.RemoveAt(intPosicion);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error eliminando coordenada del listado");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: lnkEliminarCoordenadaAgregarUbicacion_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarUbicacion();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();

                    //Cerrar modal
                    this.mpeAgregarUbicacion.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                finally
                {
                    //Actualizar grillas de coordenada
                    this.grdCoordenadasAgregarUbicacion.DataSource = this.CoordenadasUbicacionLiquidacion;
                    this.grdCoordenadasAgregarUbicacion.DataBind();

                    //Actualizar panel de ubicacion
                    this.upnlAgregarUbicacion.Update();
                }
            }

        #endregion


        #region Modal Coordenadas

            /// <summary>
            /// Evento que agrega la coordenada al listado
            /// </summary>
            protected void cdmAgregarCoordenada_Click(object sender, EventArgs e)
            {
                CoordenadaUbicacionLiquidacionEntity objCoordenada = null;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar información obligatoria coordenada
                    objCoordenada = new CoordenadaUbicacionLiquidacionEntity
                    {
                        Localizacion = this.txtLocalizacionAgregarCoordenada.Text.Trim(),
                        Norte = txtNorteAgregarCoordenada.Text.Trim(),
                        Este = this.txtEsteAgregarCoordenada.Text.Trim()
                    };

                    //Cargar información opcional de la coordenada
                    if (this.cboTipoGeometriaAgregarCoordenada.SelectedValue != "-1")
                    {
                        objCoordenada.TipoGeometria = new TipoGeometriaCoordenadaLiquidacionEntity
                        {
                            TipoGeometriaID = Convert.ToInt32(this.cboTipoGeometriaAgregarCoordenada.SelectedValue),
                            TipoGeometria = this.cboTipoGeometriaAgregarCoordenada.SelectedItem.Text
                        };
                    }
                    else
                    {
                        objCoordenada.TipoGeometria = null;
                    }
                    if (this.cboTipoCoordenadaAgregarCoordenada.SelectedValue != "-1")
                    {
                        objCoordenada.TipoCoordenada = new TipoCoordenadaUbicacionLiquidacionEntity
                        {
                            TipoCoordenadaID = Convert.ToInt32(this.cboTipoCoordenadaAgregarCoordenada.SelectedValue),
                            TipoCoordenada = this.cboTipoCoordenadaAgregarCoordenada.SelectedItem.Text
                        };
                    }
                    else
                    {
                        objCoordenada.TipoCoordenada = null;
                    }
                    if (this.cboOrigenMagnaSirgasAgregarCoordenada.SelectedValue != "-1")
                    {
                        objCoordenada.OrigenMagna = new OrigenMagnaCoordenadaLiquidacionEntity
                        {
                            OrigenMagnaID = Convert.ToInt32(this.cboOrigenMagnaSirgasAgregarCoordenada.SelectedValue),
                            OrigenMagna = this.cboOrigenMagnaSirgasAgregarCoordenada.SelectedItem.Text
                        };
                    }
                    else
                    {
                        objCoordenada.OrigenMagna = null;
                    }

                    //Si no se encuentra en el listado adicionar
                    if (this.CoordenadasUbicacionLiquidacion.Where(coordenada => coordenada.Localizacion.ToLower() == objCoordenada.Localizacion.ToLower() && coordenada.Norte == objCoordenada.Norte && coordenada.Este == objCoordenada.Este).ToList().Count == 0)
                    {
                        //Agregar al listado
                        this.CoordenadasUbicacionLiquidacion.Add(objCoordenada);
                    }

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarCoordenada();

                    //Actualizar panel de modal
                    this.upnlAgregarCoordenada.Update();

                    //Cerrar modal
                    this.mpeAgregarCoordenada.Hide();

                    //Actualizar grillas de coordenada
                    this.grdCoordenadasAgregarUbicacion.DataSource = this.CoordenadasUbicacionLiquidacion;
                    this.grdCoordenadasAgregarUbicacion.DataBind();

                    //Actualizar panel de ubicacion
                    this.upnlAgregarUbicacion.Update();

                    //Mostrar modal de ubicación
                    this.mpeAgregarUbicacion.Show();
                    
                }
                catch (Exception exc)
                {
                    //Mostrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error agregando nueva coordenada al listado");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cdmAgregarCoordenada_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarCoordenada();
                    this.LimpiarCamposModalAgregarUbicacion();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();
                    this.upnlAgregarCoordenada.Update();

                    //Cerrar modal
                    this.mpeAgregarCoordenada.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }


            /// <summary>
            /// Modal que cierra modal y cancela proceso de ingreso de coordenada
            /// </summary>
            protected void cmdCancelarAgregarCoordenada_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarCoordenada();

                    //Actualizar panel de modal
                    this.upnlAgregarCoordenada.Update();

                    //Cerrar modal
                    this.mpeAgregarCoordenada.Hide();

                    //Actualizar grillas de coordenada
                    this.grdCoordenadasAgregarUbicacion.DataSource = this.CoordenadasUbicacionLiquidacion;
                    this.grdCoordenadasAgregarUbicacion.DataBind();

                    //Actualizar panel de ubicacion
                    this.upnlAgregarUbicacion.Update();

                    //Mostrar modal de ubicación
                    this.mpeAgregarUbicacion.Show();

                }
                catch (Exception exc)
                {
                    //Mostrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cerrando modal");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdCancelarAgregarCoordenada_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Limpiar los campos del modal
                    this.LimpiarCamposModalAgregarUbicacion();
                    this.LimpiarCamposModalAgregarUbicacion();

                    //Actualizar panel de modal
                    this.upnlAgregarUbicacion.Update();
                    this.upnlAgregarCoordenada.Update();

                    //Cerrar modal
                    this.mpeAgregarCoordenada.Hide();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion


        #region Modal ConfirmarSolicitud

            /// <summary>
            /// Realiza envío de solicitud a corporación y en caso requerido realiza la autorliquidación correspondiente
            /// </summary>
            protected void cmdAceptarConfirmarEnvioSolicitud_Click(object sender, EventArgs e)
            {
                SolicitudLiquidacionEntity objSolicitud = null;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Obtener la información de la solicitud
                    objSolicitud = this.CargarInformacionSolicitud();

                    //Radicar la solicitud
                    Session["intSolicitudLiquidacionID"] = this.RadicarSolicitudLiquidacion(objSolicitud);

                    //Redireccionar a página de respuesta
                    Response.Redirect("RespuestaSolicitudLiquidacion.aspx", false);
                }
                catch (RadicacionAutoliquidacionException)
                {
                    //Cerrar el modal actual
                    this.LimpiarCamposModalConfirmarEnvioSolicitud();
                    this.upnlConfirmarEnvioSolicitud.Update();
                    this.mpeConfirmarEnvioSolicitud.Hide();

                    //Mostrar mensaje de error
                    this.ltlErrorProceso.Text = "Se presento un error al momento de radicar la solicitud ante la autoridad ambiental.<br /><br /> Los datos de la solicitud han sido almacenados y no deberá llenar nuevamente el formulario.<br /><br />Por favor trate de enviar la solicitud más tarde y si se presenta error por favor comuniquese con el administrador del sistema.";

                    //Actualizar modal y mostrarlo
                    this.upnlErrorProceso.Update();
                    this.mpeErrorProceso.Show();

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error almacenando la información de la solicitud");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdAceptarConfirmarEnvioSolicitud_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();

                    //Cerrar el modal actual
                    this.LimpiarCamposModalConfirmarEnvioSolicitud();
                    this.upnlConfirmarEnvioSolicitud.Update();
                    this.mpeConfirmarEnvioSolicitud.Hide();

                }
                
            }


            /// <summary>
            /// CAncel proceso de envío de solicitud y cierra modal de confirmación
            /// </summary>
            protected void cmdCancelarConfirmarEnvioSolicitud_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Limpiar campos del modal
                    this.LimpiarCamposModalConfirmarEnvioSolicitud();

                    //Actualizar panel de modal
                    this.upnlConfirmarEnvioSolicitud.Update();

                    //Cerrar modal
                    this.mpeConfirmarEnvioSolicitud.Hide();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cerrando modal de confirmación de envío");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdCancelarConfirmarEnvioSolicitud_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion


        #region Modal Error Proceso

            
            /// <summary>
            /// Evento que cierra modal y redirecciona a pagina de listado
            /// </summary>
            protected void cmdAceptarErrorProceso_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar los mensajes
                    this.OcultarMensaje();

                    //Limpiar mensaje modal
                    this.ltlErrorProceso.Text = "";

                    //Actualizar modal
                    this.upnlErrorProceso.Update();

                    //Cerrar modal
                    this.mpeErrorProceso.Show();

                    //Limpiar campos
                    this.LimpiarCamposFormulario();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();

                    //Direccionar al listado de solicitudes
                    Response.Redirect("SolicitudesAutoliquidacion.aspx", false);

                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento error cerrando modal de error.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cmdAceptarErrorProceso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Actualizar grillas
                    this.ActualizarGrillas();

                    //Actualizar el update panel
                    this.upnlFormulario.Update();
                }
            }

        #endregion


    #endregion


    #region Validaciones

        /// <summary>
        /// Verifica que se haya ingresado las ubicaciones
        /// </summary>
        protected void cvLocalizacionLiquidacion_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                //Verificar si se especifico listado de ubicaciones
                if (this.UbicacionesLiquidacion.Count == 0)
                {
                    //Marcar como incorrecto
                    args.IsValid = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Debe ingresar al listado por lo menos una ubicación.')", true);
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cvLocalizacionLiquidacion_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando el ingreso de ubicaciones");

                //Retornar como falso
                args.IsValid = false;
            }
            
        }


        /// <summary>
        /// Verificar en el listado de ubicaciones si se contiene ubicaciones diferentes a Bogotá
        /// </summary>
        private bool ContieneUbicacionesDiferentesBogota()
        {
            bool blnContiene = false;

            //Verificar que se tiene ubicaciones
            if (this.UbicacionesLiquidacion != null && this.UbicacionesLiquidacion.Count > 0)
            {
                //Verificar si se tiene ubicación diferente a Bogotá
                if (this.UbicacionesLiquidacion.Where(ubicacion => ubicacion.Municipio.Id != 11001 && ubicacion.Municipio.Id != 110011 && ubicacion.Municipio.Id != 110012).ToList().Count > 0)
                {
                    blnContiene = true;
                }
                else
                {
                    blnContiene = false;
                }
            }

            return blnContiene;
        }


        /// <summary>
        /// Verifica que la información contenida en el listado sea valida
        /// </summary>
        protected void cvRutaLogisticaLiquidacion_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int intAutoridadID = 0;
            bool blnContenido = true;

            try
            {
                if (this.trAutoridadAmbientalLiquidacion.Visible && this.cboAutoridadAmbientalLiquidacion.SelectedValue != "-1")
                {
                    intAutoridadID = Convert.ToInt32(this.cboAutoridadAmbientalLiquidacion.SelectedValue);
                }
                else if (this.AutoridadesAmbientalesSolicitud != null && this.AutoridadesAmbientalesSolicitud.Count == 1)
                {
                    intAutoridadID = this.AutoridadesAmbientalesSolicitud[0].IdAutoridad;
                }
                else
                {
                    throw new Exception("No se especifico la autoridad ambiental.");
                }
                
                //Verificar que se contenga en el listado el municipio especificado en la ubicación
                if (this.RutasLogisticasLiquidacion.Count > 0)
                {
                    blnContenido = true;
                    foreach (UbicacionSolicitudLiquidacionEntity objUbicacion in this.UbicacionesLiquidacion)
                    {
                        // No se verifica como ubicación bogotá
                        if (objUbicacion.Municipio.Id != 11001 && objUbicacion.Municipio.Id != 110011 && objUbicacion.Municipio.Id != 110012)
                        {
                            blnContenido = blnContenido && (this.RutasLogisticasLiquidacion.Where(ruta => ruta.MunicipioDestino.Id == objUbicacion.Municipio.Id).ToList().Count > 0);
                        }
                    }

                    //Si no esta contenido mostrar error
                    if (!blnContenido)
                    {
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('- Los municipios especificados en la ubicación deben estar contenidos en la ruta.')", true);
                    }
                    else
                    {
                        args.IsValid = true;
                    }
                }
                else
                {
                    args.IsValid = true;
                }
                
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Liquidacion_FormularioAutoliquidacion :: cvRutaLogisticaLiquidacion_ServerValidate -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se presento error validando listado de ruta");

                //Retornar como falso
                args.IsValid = false;
            }
        }

    #endregion

}