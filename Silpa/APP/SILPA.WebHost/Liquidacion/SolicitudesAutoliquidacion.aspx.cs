using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.AccesoDatos.Liquidacion.Entity;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Liquidacion;
using SILPA.LogicaNegocio.Liquidacion.Enum;
using System.Data;
using SILPA.Comun;
using SILPA.LogicaNegocio.Excepciones;

public partial class Liquidacion_SolicitudesAutoliquidacion : System.Web.UI.Page
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


        #region Listados y Desplegables

            /// <summary>
            /// Cargar el listado de estados de la solicitud
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoAutoridadesAmbientales(DropDownList p_objListado)
            {
                AutoridadAmbiental objAutoridadAmbiental = null;
                DataSet objAutoridades = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objAutoridadAmbiental = new AutoridadAmbiental();
                objAutoridades = objAutoridadAmbiental.ListarAutoridadAmbiental(null);
                objAutoridades.Tables[0].DefaultView.RowFilter = "AUT_ID <> " + ((int)AutoridadesAmbientales.ANLA).ToString();
                objAutoridades.Tables[0].DefaultView.Sort = "AUT_NOMBRE ASC";
                p_objListado.DataSource = objAutoridades.Tables[0].DefaultView.ToTable();
                p_objListado.DataValueField = "AUT_ID";
                p_objListado.DataTextField = "AUT_NOMBRE";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


            /// <summary>
            /// Cargar el listado de estados de la solicitud
            /// </summary>
            /// <param name="p_objListado">DropDownList con el listado a llenar</param>
            private void CargarListadoEstadosSolicitud(DropDownList p_objListado)
            {
                EstadoSolicitudLiquidacion objEstadoSolicitudLiquidacion = null;

                //Limpiar listado
                p_objListado.ClearSelection();
                p_objListado.Items.Clear();

                //Cargar el listado de tipos de solicitudes
                objEstadoSolicitudLiquidacion = new EstadoSolicitudLiquidacion();
                p_objListado.DataSource = objEstadoSolicitudLiquidacion.ConsultarEstadosSolicitud(true);
                p_objListado.DataValueField = "EstadoSolicitudID";
                p_objListado.DataTextField = "EstadoSolicitud";
                p_objListado.DataBind();
                p_objListado.Items.Insert(0, new ListItem("Seleccione.", "-1"));
            }


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

        #endregion


        #region Manejo Objetos Pagina


            /// <summary>
            /// Limpiar los campos del formulario de busqueda
            /// </summary>
            private void LimpiarCamposFormularioBusqueda()
            {
                //Limpiar campos
                this.cboAutoridad.ClearSelection();
                this.txtNumeroVITAL.Text = "";
                this.cboTipoSolicitud.ClearSelection();
                this.cboClaseSolicitud.ClearSelection();
                this.txtNombreProyecto.Text = "";
                this.cboEstadoSolicitud.ClearSelection();
                this.txtFechaSoicitudDesde.Text = "";
                this.txtFechaSoicitudHasta.Text = "";
                this.hdfAutoridadIDBuscar.Value = "";
                this.hdfNumeroVitalBuscar.Value = "";
                this.hdfTipoSolicitudIDBuscar.Value = "";
                this.hdfClaseSOlicitudIDBuscar.Value = "";
                this.hdfNombreProyectoBuscar.Value = "";
                this.hdfEstadoSolicitudIDBuscar.Value = "";
                this.hdfFechaDesdeBuscar.Value = "";
                this.hdfFechaHastaBuscar.Value = "";

                //Inicializar grilla
                this.grdSolicitudes.DataSource = new List<SolicitudLiquidacionEntity>();
                this.grdSolicitudes.DataBind();
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
            /// Cargar la información inicial de la pagina
            /// </summary>
            private void InicializarPagina()
            {
                try
                {
                    //Limpiar los campos del formulario de busqueda
                    this.LimpiarCamposFormularioBusqueda();

                    //Cargar el listado de autoridades ambientales
                    this.CargarListadoAutoridadesAmbientales(this.cboAutoridad);

                    //Cargar el listado de tipos de solicitud
                    this.CargarListadoTiposSolicitud(this.cboTipoSolicitud);

                    //Inicializar el listado de clases de solicitud
                    this.cboClaseSolicitud.Items.Insert(0, new ListItem("Seleccione.", "-1"));

                    //CArgar el listado de estados de solicitud
                    this.CargarListadoEstadosSolicitud(this.cboEstadoSolicitud);

                    //Inicializar campos de fechas
                    this.txtFechaSoicitudDesde.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
                    this.txtFechaSoicitudHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    //Realizar la busqueda de la información de solicitudes
                    this.cmdBuscarSolicitud_Click(null, null);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento error inicializando información de la página");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: InicializarPagina -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }


            /// <summary>
            /// Mostrar el mensaje de confirmación para las autoridades ambientales
            /// </summary>
            /// <param name="p_objSolicitudLiquidacionEntity">SolicitudLiquidacionEntity con la información de la solicitud de liquidación</param>
            private string ObtenerMensajeAutoridadAmbiental(SolicitudLiquidacionEntity p_objSolicitudLiquidacionEntity)
            {
                string strMensaje = "";

                //Cargar mensaje
                strMensaje = "Se reenviará la solicitud de <b>" + p_objSolicitudLiquidacionEntity.ClaseSolicitud.ClaseSolicitud + "</b> para <b>" + p_objSolicitudLiquidacionEntity.TipoSolicitud.TipoSolicitud + "</b> " +
                             " a la autoridad ambiental <b>" + p_objSolicitudLiquidacionEntity.AutoridadAmbiental.Nombre + "</b> para su estudio, calculo de la liquidación y cobro correspondiente.<br /><br />" +
                             "Una vez reciba el valor del cobro por parte de <b>" + p_objSolicitudLiquidacionEntity.AutoridadAmbiental.Nombre + "</b> el pago deberá realizarlo por los canales que la autoridad ambiental ofrezca para tal fin.<br /><br />Recuerde que en caso de encontrar inconsistencias en la información suministrada o no concordar con la documentación que posteriormente se exija para el comienzo de los procesos asociados a la liquidación se puede presentar cobros adicionales que se ajusten a la nueva información.";

                return strMensaje;
            }

        #endregion


        #region Manejo de Datos

            /// <summary>
            /// Consultar el listado de solicitudes que cumplen con los parametros de busqueda especificados
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud</param>
            /// <param name="p_intClaseSolicitudID">int con la clase de solicitud. Opcional</param>
            /// <param name="p_strNombreProyecto">string con el nombre del proyecto</param>
            /// <param name="p_strNumeroVital">string con el numero vital</param>
            /// <param name="p_intEstadoSolicitudID">int con el identificador del estado de la solicitud</param>
            /// <param name="p_objFechaCreacionInicio">DataTime con la fecha de creación. Rango Inicial</param>
            /// <param name="p_objFechaCreacionFin">DataTime con la fecha de creación. Rango Final</param>
            private void BuscarSolicitudesLiquidacion(int p_intAutoridadID, int p_intTipoSolicitudID,
                                                      int p_intClaseSolicitudID, string p_strNombreProyecto, string p_strNumeroVital,
                                                      int p_intEstadoSolicitudID, DateTime p_objFechaCreacionInicio, DateTime p_objFechaCreacionFin)
            {
                Autoliquidacion objAutoliquidacion = null;
                List<SolicitudLiquidacionEntity> objLstLiquidacion = null;

                //Realizar la consulta de las solicitudes
                objAutoliquidacion = new Autoliquidacion();
                objLstLiquidacion = objAutoliquidacion.ConsultarListadoSolicitudesLiquidacion(this.SolicitanteID, p_intAutoridadID, p_intTipoSolicitudID,
                                                                                              p_intClaseSolicitudID, p_strNombreProyecto, p_strNumeroVital,
                                                                                              p_intEstadoSolicitudID, p_objFechaCreacionInicio, p_objFechaCreacionFin);

                //Ordenar solicitud
                if (objLstLiquidacion != null)
                {
                    objLstLiquidacion = objLstLiquidacion.OrderByDescending(liquidacion => liquidacion.FechaCreacion).ToList();
                }

                //Verificar si se muestra la columna de reenviar
                if (objLstLiquidacion != null && objLstLiquidacion.Where(solicitud => solicitud.EstadoSolicitud.EstadoSolicitudID == (int)EstadoSolicitudEnum.Solicitud_Pendiente_Radicacion_Solicitud || solicitud.EstadoSolicitud.EstadoSolicitudID == (int)EstadoSolicitudEnum.Solicitud_Pendiente_Generación_Cobro).ToList().Count > 0)
                    this.grdSolicitudes.Columns[9].Visible = true;
                else
                    this.grdSolicitudes.Columns[9].Visible = false;

                //Cargar datos en la grilla de liquidación
                this.grdSolicitudes.DataSource = (objLstLiquidacion != null ? objLstLiquidacion : new List<SolicitudLiquidacionEntity>());
                this.grdSolicitudes.DataBind();
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
                //this.SolicitanteID = 24627;
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


        #region cmdNuevaSolicitud

            /// <summary>
            /// Evento que direcciona a formulario de solicitud
            /// </summary>
            protected void cmdNuevaSolicitud_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //REdireccionar a pagina
                    Response.Redirect("FormularioAutoliquidacion.aspx", false);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento enviando a formulario de solicitud de liquidación.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: cmdNuevaSolicitud_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    
                }
            }

        #endregion


        #region cboTipoSolicitud

            /// <summary>
            /// Evento que carga las clases de solicitud
            /// </summary>
            protected void cboTipoSolicitud_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Verificar si se selecciono opción del listado
                    if (((DropDownList)sender).SelectedValue != "-1")
                    {
                        //Cargar listado de solicitudes
                        this.CargarListadoClasesSolicitudes(this.cboClaseSolicitud, Convert.ToInt32(((DropDownList)sender).SelectedValue));
                    }
                    else
                    {
                        //Limpiar desplegable de clases
                        this.cboClaseSolicitud.ClearSelection();
                        this.cboClaseSolicitud.Items.Clear();
                        this.cboClaseSolicitud.Items.Insert(0, new ListItem("Seleccione.", "-1"));
                    }

                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando el listado de clases de solicitudes");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: cboTipoSolicitud_SelectedIndexChanged -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region cmdBuscarSolicitud

            /// <summary>
            /// Realizar la busqueda de las solicitudes registradas en el sistema para la solicitud
            /// </summary>
            protected void cmdBuscarSolicitud_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar los parametros de busqueda
                    this.hdfAutoridadIDBuscar.Value = this.cboAutoridad.SelectedValue;
                    this.hdfNumeroVitalBuscar.Value = this.txtNumeroVITAL.Text;
                    this.hdfTipoSolicitudIDBuscar.Value = this.cboTipoSolicitud.SelectedValue;
                    this.hdfClaseSOlicitudIDBuscar.Value = this.cboClaseSolicitud.SelectedValue;
                    this.hdfNombreProyectoBuscar.Value = this.txtNombreProyecto.Text.Trim();
                    this.hdfEstadoSolicitudIDBuscar.Value = this.cboEstadoSolicitud.SelectedValue;
                    this.hdfFechaDesdeBuscar.Value = this.txtFechaSoicitudDesde.Text;
                    this.hdfFechaHastaBuscar.Value = this.txtFechaSoicitudHasta.Text;

                    //Realizar la busqueda de las solicitudes
                    this.BuscarSolicitudesLiquidacion(Convert.ToInt32(this.hdfAutoridadIDBuscar.Value), Convert.ToInt32(this.hdfTipoSolicitudIDBuscar.Value),
                                                        Convert.ToInt32(this.hdfClaseSOlicitudIDBuscar.Value), this.hdfNombreProyectoBuscar.Value, this.hdfNumeroVitalBuscar.Value,
                                                        Convert.ToInt32(this.hdfEstadoSolicitudIDBuscar.Value), DateTime.ParseExact(this.hdfFechaDesdeBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(this.hdfFechaHastaBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));


                    //Actualizar el panel
                    this.upnlSolicitudeLiquidacion.Update();

                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error buscando solicitudes asociadas a los parametros de busqueda");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: cmdBuscarSolicitud_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }

        #endregion


        #region grdSolicitudes

            /// <summary>
            /// Evento que realiza el cambio de pagina
            /// </summary>
            protected void grdSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Asignar nuevo index
                    this.grdSolicitudes.PageIndex = e.NewPageIndex;

                    //Realizar la busqueda de las solicitudes
                    this.BuscarSolicitudesLiquidacion(Convert.ToInt32(this.hdfAutoridadIDBuscar.Value), Convert.ToInt32(this.hdfTipoSolicitudIDBuscar.Value),
                                                        Convert.ToInt32(this.hdfClaseSOlicitudIDBuscar.Value), this.hdfNombreProyectoBuscar.Value, this.hdfNumeroVitalBuscar.Value,
                                                        Convert.ToInt32(this.hdfEstadoSolicitudIDBuscar.Value), DateTime.ParseExact(this.hdfFechaDesdeBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(this.hdfFechaHastaBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));


                    //Actualizar el panel
                    this.upnlSolicitudeLiquidacion.Update();
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error realizando el cambio de pagina de las solicitudes de liquidación");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: grdSolicitudes_PageIndexChanging -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }


            /// <summary>
            /// Formatea información de grilla al momento de generarla
            /// </summary>
            protected void grdSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                Autoliquidacion objAutoliquidacion = null;
                SolicitudLiquidacionEntity objSolicitud = null;
                List<CobroSolicitudLiquidacionEntity> objCobros = null;
                GridView objGrilla = null;

                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {

                        //Cargar datos de la fila
                        objSolicitud = (SolicitudLiquidacionEntity)e.Row.DataItem;

                        //Verificar que se obtenga datos de la fila
                        if (objSolicitud != null)
                        {
                            //Crear objeto de consulta
                            objAutoliquidacion = new Autoliquidacion();

                            //Obtener grilla
                            objGrilla = (GridView)e.Row.FindControl("grdCobrosSolicitudLiquidacion");

                            //Verificar que la grilla no sea nula
                            if (objGrilla != null)
                            {
                                //Consultar los cobros asociados
                                objCobros = objAutoliquidacion.ObtenerCobrosSolicitudLiquidacion(objSolicitud.SolicitudLiquidacionID);

                                //Verificar que no este pendiente de valuar
                                if (objSolicitud.EstadoSolicitud.EstadoSolicitudID != (int)EstadoSolicitudEnum.Solicitud_Pendiente_Radicacion_Solicitud &&
                                   objSolicitud.EstadoSolicitud.EstadoSolicitudID != (int)EstadoSolicitudEnum.Solicitud_Pendiente_Generación_Cobro)
                                {
                                    objGrilla.EmptyDataText = "Favor contactarse con la " + objSolicitud.AutoridadAmbiental.Nombre + " para que les suministre la información relaciona con el valor que debe cancelar.";
                                }
                                else if (objSolicitud.EstadoSolicitud.EstadoSolicitudID == (int)EstadoSolicitudEnum.Solicitud_Pendiente_Radicacion_Solicitud)
                                {
                                    objGrilla.EmptyDataText = "No existen cobros asociados a la solicitud debido a que esta no ha sido radicada por falla en el sistema.";
                                }
                                else if (objSolicitud.EstadoSolicitud.EstadoSolicitudID == (int)EstadoSolicitudEnum.Solicitud_Pendiente_Generación_Cobro)
                                {
                                    objGrilla.EmptyDataText = "No se han generado los cobros asociados a la solicitud por falla en el sistema.";
                                }

                                objGrilla.DataSource = objCobros;
                                objGrilla.DataBind();
                            }

                        }
                    }
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error formateando la información de la grilla de solicitudes");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: grdSolicitudes_RowDataBound -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }


            /// <summary>
            /// Direcciona a pagina para visualización de la solicitud
            /// </summary>
            protected void lnkVer_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Cargar el identificador de la solicitud
                    Session["intSolicitudLiquidacionID"] = ((LinkButton)sender).CommandArgument;

                    //Redireccionar a página de respuesta
                    Response.Redirect("VerFormularioAutoliquidacion.aspx", false);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando datos para mostrar respuesta de solicitud");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: lnkVer_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }


            /// <summary>
            /// Direcciona para visualizar la respuesta a una solicitud
            /// </summary>
            protected void lnkVerRespuesta_Click(object sender, EventArgs e)
            {
                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();
                   
                    //Cargar el identificador de la solicitud
                    Session["intSolicitudLiquidacionID"] = ((LinkButton)sender).CommandArgument;

                    //Redireccionar a página de respuesta
                    Response.Redirect("RespuestaSolicitudLiquidacion.aspx", false);
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error cargando datos para mostrar respuesta de solicitud");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: lnkVer_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
            }


            /// <summary>
            /// Mostrar el modal de reenvío de la solicitud
            /// </summary>
            protected void lnkReenviar_Click(object sender, EventArgs e)
            {
                Autoliquidacion objAutoliquidacion = null;
                SolicitudLiquidacionEntity objSolicitudLiquidacionEntity = null;

                try
                {
                    //Cargar el identificador de la solicitud de liquidación a reenviar
                    this.OcultarMensaje();

                    //Limpiar campos del modal
                    this.LimpiarCamposModalConfirmarEnvioSolicitud();

                    //Cargar el identificador de la solicitud
                    this.hdfSolicitudLiquidacionIDConfirmarEnvioSolicitud.Value = ((LinkButton)sender).CommandArgument;

                    //Cargar la información de lá solicitud
                    objAutoliquidacion = new Autoliquidacion();
                    objSolicitudLiquidacionEntity = objAutoliquidacion.ConsultarSolicitudLiquidacion(Convert.ToInt32(this.hdfSolicitudLiquidacionIDConfirmarEnvioSolicitud.Value));

                    //Verificar que se obtenga datos de la solicitud
                    if (objSolicitudLiquidacionEntity != null && objSolicitudLiquidacionEntity.SolicitudLiquidacionID > 0)
                    {
                        //Verificar que el estado de la solicitud sea para reenvio
                        if (objSolicitudLiquidacionEntity.EstadoSolicitud.EstadoSolicitudID == (int)EstadoSolicitudEnum.Solicitud_Pendiente_Radicacion_Solicitud ||
                            objSolicitudLiquidacionEntity.EstadoSolicitud.EstadoSolicitudID == (int)EstadoSolicitudEnum.Solicitud_Pendiente_Generación_Cobro)
                        {
                            //Cargar mensaje patra autoridades ambientales
                            this.ltlTerminosConfirmarEnvioSolicitud.Text = this.ObtenerMensajeAutoridadAmbiental(objSolicitudLiquidacionEntity);
                            
                            //Actualizar y mostrar modal
                            this.upnlConfirmarEnvioSolicitud.Update();
                            this.mpeConfirmarEnvioSolicitud.Show();
                        }
                        else
                        {
                            //Asignar nuevo index
                            this.grdSolicitudes.PageIndex = 0;

                            //Realizar la busqueda de las solicitudes
                            this.BuscarSolicitudesLiquidacion(Convert.ToInt32(this.hdfAutoridadIDBuscar.Value), Convert.ToInt32(this.hdfTipoSolicitudIDBuscar.Value),
                                                                Convert.ToInt32(this.hdfClaseSOlicitudIDBuscar.Value), this.hdfNombreProyectoBuscar.Value, this.hdfNumeroVitalBuscar.Value,
                                                                Convert.ToInt32(this.hdfEstadoSolicitudIDBuscar.Value), DateTime.ParseExact(this.hdfFechaDesdeBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(this.hdfFechaHastaBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));


                            //Actualizar el panel
                            this.upnlSolicitudeLiquidacion.Update();
                        }
                    }
                    else
                    {
                        throw new Exception("No se encontro información de la solicitud de liquidación " + this.hdfSolicitudLiquidacionIDConfirmarEnvioSolicitud.Value);
                    }
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento error cargando el modal de confirmación de reenvío de información.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: lnkReenviar_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Asignar nuevo index
                    this.grdSolicitudes.PageIndex = 0;

                    //Realizar la busqueda de las solicitudes
                    this.BuscarSolicitudesLiquidacion(Convert.ToInt32(this.hdfAutoridadIDBuscar.Value), Convert.ToInt32(this.hdfTipoSolicitudIDBuscar.Value),
                                                        Convert.ToInt32(this.hdfClaseSOlicitudIDBuscar.Value), this.hdfNombreProyectoBuscar.Value, this.hdfNumeroVitalBuscar.Value,
                                                        Convert.ToInt32(this.hdfEstadoSolicitudIDBuscar.Value), DateTime.ParseExact(this.hdfFechaDesdeBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(this.hdfFechaHastaBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));


                    //Actualizar el panel
                    this.upnlSolicitudeLiquidacion.Update();
                }                
            }        

        #endregion


        #region Modal ConfirmarSolicitud

            /// <summary>
            /// Realiza envío de solicitud a corporación y en caso requerido realiza la autorliquidación correspondiente
            /// </summary>
            protected void cmdAceptarConfirmarEnvioSolicitud_Click(object sender, EventArgs e)
            {
                Autoliquidacion objAutoliquidacion = null;

                try
                {
                    //Ocultar mensajes
                    this.OcultarMensaje();

                    //Radicar la solicitud de liquidacion
                    objAutoliquidacion = new Autoliquidacion();
                    objAutoliquidacion.ReenviarSolicitudLiquidacion(Convert.ToInt32(this.hdfSolicitudLiquidacionIDConfirmarEnvioSolicitud.Value));

                    //Cargar el identificador de la solicitud
                    Session["intSolicitudLiquidacionID"] = this.hdfSolicitudLiquidacionIDConfirmarEnvioSolicitud.Value;

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
                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento un error almacenando la información de la solicitud");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: cmdAceptarConfirmarEnvioSolicitud_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

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
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: cmdCancelarConfirmarEnvioSolicitud_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Realizar la busqueda de las solicitudes
                    this.BuscarSolicitudesLiquidacion(Convert.ToInt32(this.hdfAutoridadIDBuscar.Value), Convert.ToInt32(this.hdfTipoSolicitudIDBuscar.Value),
                                                        Convert.ToInt32(this.hdfClaseSOlicitudIDBuscar.Value), this.hdfNombreProyectoBuscar.Value, this.hdfNumeroVitalBuscar.Value,
                                                        Convert.ToInt32(this.hdfEstadoSolicitudIDBuscar.Value), DateTime.ParseExact(this.hdfFechaDesdeBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(this.hdfFechaHastaBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));


                    //Actualizar el panel
                    this.upnlSolicitudeLiquidacion.Update();
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
                    this.mpeErrorProceso.Hide();

                }
                catch (Exception exc)
                {
                    //MOstrar mensaje de error en pantalla
                    this.MostrarMensaje("Se presento error cerrando modal de error.");

                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Liquidacion_SolicitudesAutoliquidacion :: cmdAceptarErrorProceso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                }
                finally
                {
                    //Asignar nuevo index
                    this.grdSolicitudes.PageIndex = 0;

                    //Realizar la busqueda de las solicitudes
                    this.BuscarSolicitudesLiquidacion(Convert.ToInt32(this.hdfAutoridadIDBuscar.Value), Convert.ToInt32(this.hdfTipoSolicitudIDBuscar.Value),
                                                        Convert.ToInt32(this.hdfClaseSOlicitudIDBuscar.Value), this.hdfNombreProyectoBuscar.Value, this.hdfNumeroVitalBuscar.Value,
                                                        Convert.ToInt32(this.hdfEstadoSolicitudIDBuscar.Value), DateTime.ParseExact(this.hdfFechaDesdeBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(this.hdfFechaHastaBuscar.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));


                    //Actualizar el panel
                    this.upnlSolicitudeLiquidacion.Update();
                }
            }

        #endregion


    #endregion

            
}