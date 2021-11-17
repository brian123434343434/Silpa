using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.AccesoDatos.Notificacion;
using SILPA.LogicaNegocio.Notificacion;
using System.Data;
using SILPA.AccesoDatos.RecursoReposicion;
using SILPA.Comun;
using System.Configuration;
using System.IO;
using SILPA.LogicaNegocio.Recurso;
using SILPA.LogicaNegocio.Generico;
using SILPA.Servicios;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.BPMProcess;
using System.Xml.Serialization;
using SILPA.LogicaNegocio.Sancionatorio;
using SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones;
using SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades;

public partial class Recurso_Controles_RegistrarRecursoReposicion : System.Web.UI.UserControl
{

    #region Propiedades

        #region Privados

            /// <summary>
            /// Listado de documentos anexos
            /// </summary>
            private List<RecursoDocumentoEntity> _lstDocumentos
            {
                get
                {
                    return (List<RecursoDocumentoEntity>)Session["_lstDocumentos"];
                }
                set
                {
                    Session["_lstDocumentos"] = value;
                }
            }

            /// <summary>
            /// Numero vital relacionado al acto administrativo
            /// </summary>
            private string _strNumeroVital
            {
                get
                {
                    return (string)ViewState["_strNumeroVital"];
                }
                set
                {
                    ViewState["_strNumeroVital"] = value;
                }
            }

            /// <summary>
            /// Identificador de la autoridad ambiental
            /// </summary>
            private int _intAutoridadAmbientalID
            {
                get
                {
                    return (int)(ViewState["_intAutoridadAmbientalID"] != null ? ViewState["_intAutoridadAmbientalID"] : 0);
                }
                set
                {
                    ViewState["_intAutoridadAmbientalID"] = value;
                }
            }

            /// <summary>
            /// Nombre de la autoridad ambiental
            /// </summary>
            private string _strAutoridadAmbiental
            {
                get
                {
                    return (string)ViewState["_strAutoridadAmbiental"];
                }
                set
                {
                    ViewState["_strAutoridadAmbiental"] = value;
                }
            }

            /// <summary>
            /// Codigo del expediente
            /// </summary>
            private string _strExpediente
            {
                get
                {
                    return (string)ViewState["_strExpediente"];
                }
                set
                {
                    ViewState["_strExpediente"] = value;
                }
            }

            /// <summary>
            /// Numero de acto administrativo
            /// </summary>
            private string _strNumeroActo
            {
                get
                {
                    return (string)ViewState["_strNumeroActo"];
                }
                set
                {
                    ViewState["_strNumeroActo"] = value;
                }
            }

            /// <summary>
            /// Fecha del acto administrativo
            /// </summary>
            private DateTime _objFechaActo
            {
                get
                {
                    return (ViewState["_strFechaActo"] != null ? (DateTime)ViewState["_strFechaActo"] : default(DateTime));
                }
                set
                {
                    ViewState["_strFechaActo"] = value;
                }
            }

            /// <summary>
            /// Fecha de notificación del acto administrativo
            /// </summary>
            private DateTime _objFechaNotificacion
            {
                get
                {
                    return (ViewState["_objFechaNotificacion"] != null ? (DateTime)ViewState["_objFechaNotificacion"] : default(DateTime));
                }
                set
                {
                    ViewState["_objFechaNotificacion"] = value;
                }
            }

  


        #endregion


        #region Publicos

            /// <summary>
            /// Identificador de la autoridad
            /// </summary>
            public int AutoridadID
            {
                get
                {
                    return (ViewState["AutoridadPresentarRecursoID"] != null ? Convert.ToInt32(ViewState["AutoridadPresentarRecursoID"]) : -1);
                }
                set
                {
                    ViewState["AutoridadPresentarRecursoID"] = value;
                }
            }


            /// <summary>
            /// Identificador del usuario
            /// </summary>
            public string UsuarioID
            {
                get
                {
                    return (ViewState["UsuarioPresentarRecursoID"] != null ? ViewState["UsuarioPresentarRecursoID"].ToString() : "");
                }
                set
                {
                    ViewState["UsuarioPresentarRecursoID"] = value;
                }
            }

            /// <summary>
            /// Identificador del acto administrativo
            /// </summary>
            public long ActoAdministrativoID
            {
                get
                {
                    return (ViewState["ActoAdministrativoPresentarRecursoID"] != null ? (long)ViewState["ActoAdministrativoPresentarRecursoID"] : 0);
                }
                set
                {
                    ViewState["ActoAdministrativoPresentarRecursoID"] = value;
                }
            }


            /// <summary>
            /// Identificador de la persona que presenta recurso
            /// </summary>
            public long PersonaID
            {
                get
                {
                    return (ViewState["PersonaPresentarRecursoID"] != null ? (long)ViewState["PersonaPresentarRecursoID"] : 0);
                }
                set
                {
                    ViewState["PersonaPresentarRecursoID"] = value;
                }
            }


            /// <summary>
            /// Numero de identificacion de la persona sobre la cual se presenta el recurso
            /// </summary>
            public string NumeroIdentificacionPersona
            {
                get
                {
                    return (ViewState["NumeroIdentificacionPersonaPresentarRecurso"] != null ? (string)ViewState["NumeroIdentificacionPersonaPresentarRecurso"] : "");
                }
                set
                {
                    ViewState["NumeroIdentificacionPersonaPresentarRecurso"] = value;
                }
            }

            /// <summary>
            /// Identificador del dlujo al cual pertenece el estado al cual se debe avanzar
            /// </summary>
            public int FlujoEstadoAvanzarID
            {
                get
                {
                    return (ViewState["FlujoEstadoAvanzarPresentarRecursoID"] != null ? (int)ViewState["FlujoEstadoAvanzarPresentarRecursoID"] : 0);
                }
                set
                {
                    ViewState["FlujoEstadoAvanzarPresentarRecursoID"] = value;
                }
            }

            /// <summary>
            /// Identificador del estado al cual se debe avanzar
            /// </summary>
            public int EstadoAvanzarID
            {
                get
                {
                    return (ViewState["EstadoAvanzarPresentarRecursoID"] != null ? (int)ViewState["EstadoAvanzarPresentarRecursoID"] : 0);
                }
                set
                {
                    ViewState["EstadoAvanzarPresentarRecursoID"] = value;
                }
            }

            /// <summary>
            /// Identificador del estado al cual se debe avanzar
            /// </summary>
            public int EstadoPersonaActoID
            {
                get
                {
                    return (ViewState["EstadoPersonaActoPresentarRecursoID"] != null ? (int)ViewState["EstadoPersonaActoPresentarRecursoID"] : 0);
                }
                set
                {
                    ViewState["EstadoPersonaActoPresentarRecursoID"] = value;
                }
            }

            /// <summary>
            /// Validation Group al cual se relaciona segunda clave
            /// </summary>
            public string ValidationGroup
            {
                get
                {
                    return (ViewState["ValidationGroupPresentarRecurso"] != null ? (string)ViewState["ValidationGroupPresentarRecurso"] : "");
                }
                set
                {
                    //Cargar datos
                    ViewState["ValidationGroupPresentarRecurso"] = value;

                    //Cargar validation group del control
                    this.rfvDescripcionPresentarRecurso.ValidationGroup = value;
                    this.rfvSegundaContrasenaPresentarRecurso.ValidationGroup = value;
                    this.cvSegundaContrasena.ValidationGroup = value;
                    this.cvListaDocumentosPresentarRecurso.ValidationGroup = value;
                }
            }

            
            /// <summary>
            /// Indica si se debe solicitar de la segunda clave
            /// </summary>
            public bool SolicitarSegundaClave
            {
                get
                {
                    return (ViewState["SolicitarSegundaClavePresentarRecurso"] != null ? (bool)ViewState["SolicitarSegundaClavePresentarRecurso"] : true);
                }
                set
                {
                    //Cargar datos
                    ViewState["SolicitarSegundaClavePresentarRecurso"] = value;

                    //Cargar validation group del control
                    this.trSegundaClave.Visible = value;
                    this.rfvSegundaContrasenaPresentarRecurso.Enabled = value;
                    this.cvSegundaContrasena.Enabled = value;
                }
            }

            public DateTime? FechaNotificacion
            {
                get
                {
                    return (ViewState["FechaNotificacionPresentarRecursoID"] != null ? (DateTime?)ViewState["FechaNotificacionPresentarRecursoID"] : null);
                }
                set
                {
                    ViewState["FechaNotificacionPresentarRecursoID"] = value;
                }
            }

            
        #endregion


    #endregion


    #region Metodos Privados

        /// <summary>
        /// Limpiar modal de presentar recurso de reposición
        /// </summary>
        private void LimpiarCamposPresentarRecurso()
        {            
            //Limpiar objetos
            this._lstDocumentos = null;
            this.ltlNumeroVital.Text = "";
            this.ltlAutoridadAmbiental.Text = "";
            this.ltlExpediente.Text = "";
            this.ltlNumeroActoAdministrativo.Text = "";
            this.ltlFechaActoAdministrativo.Text = "";
            this.ltlFechaNotificacion.Text = "";
            this.txtDescripcionPresentarRecurso.Text = "";
            this.txtSegundaContrasenaPresentarRecurso.Text = "";
            this.grdArchivosPresentarRecurso.DataSource = null;
            this.grdArchivosPresentarRecurso.DataBind();   
            this._strNumeroVital = "";
            this._intAutoridadAmbientalID = 0;
            this._strAutoridadAmbiental = "";
            this._strExpediente = "";
            this._strNumeroActo = "";
            this._objFechaActo = default(DateTime);
            this._objFechaNotificacion = default(DateTime);
        }


        /// <summary>
        /// Cargar la informacion del acto administrativo relacionado a una persona especifica
        /// </summary>
        private void CargarInformacionActoAdministrativoPersona()
        {
            Notificacion objNotificacion = null;
            DataTable objInformacionActo = null;
            DataSet objEstadosPersona = null;
            DataRow[] objListadoEstados = null;
            RecursoReposicionService objRecursoReposicionService;
            ActoPersonaRecursoEntity objActoPersonaRecursoEntity;

            if (this.AutoridadID != (int)AutoridadesAmbientales.ANLA)
            {
                //Obtener datos del acto administrativo
                objNotificacion = new Notificacion();
                objInformacionActo = objNotificacion.ConsultaActoNotificacion(this.ActoAdministrativoID);
                objEstadosPersona = objNotificacion.ObtenerListadoEstadosActoPersona(this.ActoAdministrativoID, this.PersonaID);

                //Cargar datos del acto adminsitrativo
                this._strNumeroVital = objInformacionActo.Rows[0]["NOT_NUMERO_SILPA"].ToString();
                this._intAutoridadAmbientalID = Convert.ToInt32(objInformacionActo.Rows[0]["NOT_AUT_ID"]);
                this._strAutoridadAmbiental = objInformacionActo.Rows[0]["AUTORIDAD_AMBIENTAL"].ToString();
                this._strExpediente = objInformacionActo.Rows[0]["NOT_PROCESO_ADMINISTRACION"].ToString();
                this._strNumeroActo = objInformacionActo.Rows[0]["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString();
                this._objFechaActo = Convert.ToDateTime(objInformacionActo.Rows[0]["NOT_FECHA_ACTO"]);


                //Cargar la fecha de notificacion
                if (objEstadosPersona != null)
                {
                    objListadoEstados = objEstadosPersona.Tables[0].Select("ESTADO_ACEPTACION_NOTIFICACION = 1");

                    if (objListadoEstados != null && objListadoEstados.Length > 0)
                        this._objFechaNotificacion = Convert.ToDateTime(objListadoEstados[0]["FECHA_ESTADO_COMPLETA"]);
                }
            }
            else
            {
                //Obtener la informacion
                objRecursoReposicionService = new RecursoReposicionService();
                objActoPersonaRecursoEntity = objRecursoReposicionService.ObtenerActoAdministrativoRecursoPersona(Convert.ToInt32(this.ActoAdministrativoID), Convert.ToInt32(this.PersonaID));

                if (objActoPersonaRecursoEntity != null)
                {
                    this._strNumeroVital = objActoPersonaRecursoEntity.NumeroVITAL;
                    this._intAutoridadAmbientalID = objActoPersonaRecursoEntity.AutoridadID;
                    this._strAutoridadAmbiental = objActoPersonaRecursoEntity.NombreAutoridad;
                    this._strExpediente = objActoPersonaRecursoEntity.Expediente;
                    this._strNumeroActo = objActoPersonaRecursoEntity.NumeroActo;
                    this._objFechaActo = objActoPersonaRecursoEntity.FechaActo;
                    this._objFechaNotificacion = objActoPersonaRecursoEntity.FechaNotificacion;
                }
                else
                    throw new Exception("No se obtuvo informacion del acto administrativo");
            }
        }


        /// <summary>
        /// Carga información de listado de documentos
        /// </summary>
        private void CargarListadoArchivos()
        {
            List<RecursoDocumentoEntity> objLstTemp = null;

            //Si no hay pasajes se iniciliza la grilla
            if (this._lstDocumentos == null || this._lstDocumentos.Count == 0)
            {
                //Cargar mensaje de ingreso de titulares
                objLstTemp = new List<RecursoDocumentoEntity>();
                objLstTemp.Add(new RecursoDocumentoEntity());
                this.grdArchivosPresentarRecurso.DataSource = objLstTemp;
                this.grdArchivosPresentarRecurso.ShowFooter = true;
                this.grdArchivosPresentarRecurso.DataBind();
                this.grdArchivosPresentarRecurso.Rows[0].Cells.Clear();
                this.grdArchivosPresentarRecurso.Rows[0].Cells.Add(new TableCell());
                this.grdArchivosPresentarRecurso.Rows[0].Cells[0].ColumnSpan = 4;
                this.grdArchivosPresentarRecurso.Rows[0].Cells[0].Text = "No se han registrado archivos al recurso de reposición";
            }
            else
            {
                //Cargar datos en grilla
                this.grdArchivosPresentarRecurso.DataSource = this._lstDocumentos;
                this.grdArchivosPresentarRecurso.ShowFooter = true;
                this.DataBind();

                //Cargar triggers
                this.CargarTriggersGrillaArchivosRecursos();
            }    
        }


        /// <summary>
        /// Cargar los trigger especificos que se requieren de la grilla de archivos de recursos de reposición
        /// </summary>
        private void CargarTriggersGrillaArchivosRecursos()
        {
            LinkButton objLinkButton = null;

            //Verificar que la grilla contenga información
            if (this.grdArchivosPresentarRecurso.Visible && this.grdArchivosPresentarRecurso.Rows.Count > 0)
            {
                //Ciclo que adiciona los triggers de la grilla de notificaciones
                foreach (GridViewRow objRowArchivo in this.grdArchivosPresentarRecurso.Rows)
                {
                    //Cargar imagen de descarga
                    objLinkButton = (LinkButton)objRowArchivo.FindControl("lnkVerArchivoPresentarRecurso");

                    //Si existe el control y se encuentra visible
                    if (objLinkButton != null && objLinkButton.Visible)
                    {
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(objLinkButton);
                    }
                }
            }
        }


        /// <summary>
        /// Crear objeto identity para cargar en XML
        /// </summary>
        /// <param name="pintId">int con el ID</param>
        /// <param name="p_strGrupo">string con el nombre del grupo</param>
        /// <param name="p_strValor">string con el valor</param>
        /// <param name="p_intOrden">int con el orden</param>
        /// <param name="p_objArchivo">Arreglo de bytes con archivo</param>
        /// <returns></returns>
        private ValoresIdentity CargarValores(int pintId, string p_strGrupo, string p_strValor, int p_intOrden, Byte[] p_objArchivo)
        {
            ValoresIdentity objValores = new ValoresIdentity();
            objValores.Id = pintId;
            objValores.Grupo = p_strGrupo;
            objValores.Valor = p_strValor;
            objValores.Orden = p_intOrden;
            objValores.Archivo = p_objArchivo;
            return objValores;
        }

        /// <summary>
        /// Registrar la solicitud en VITAL
        /// </summary>
        /// <returns>string con el número VITAL generado</returns>
        private string RegistrarSolicitudVITAL()
        {
            MemoryStream objMemoryStream = null;
            XmlSerializer objSerializador = null;
            Queja objQueja = null;
            List<ValoresIdentity> objValoresList = null;
            DataTable objParametrosFormulario = null;
            string strNumeroVITAL = "";
            string strXml = "";
            string strClientId = "";
            long lngUsuarioQueja = 0;
            long lngFormularioqueja = 0;            
            int intNumeroDocumento = 1;

            try
            {
                //Cargar listado de valores a registrar
                objValoresList = new List<ValoresIdentity>();
                objValoresList.Add(CargarValores(1, "Bas1", "", 1, new Byte[1]));
                objValoresList.Add(CargarValores(2, "Bas1", this._strNumeroVital, 1, new Byte[1]));
                objValoresList.Add(CargarValores(3, "Bas1", this._strExpediente, 1, new Byte[1]));
                objValoresList.Add(CargarValores(4, "Bas1", this._strNumeroActo, 1, new Byte[1]));
                objValoresList.Add(CargarValores(5, "Bas1", this._objFechaActo.ToString("dd/MM/yyyy"), 1, new Byte[1]));
                objValoresList.Add(CargarValores(6, "Bas1", this._objFechaNotificacion.ToString("dd/MM/yyyy"), 1, new Byte[1]));
                objValoresList.Add(CargarValores(7, "Bas1", this.txtDescripcionPresentarRecurso.Text.Trim(), 1, new Byte[1]));

                //Verificar si hay archivos
                if(this._lstDocumentos != null && this._lstDocumentos.Count > 0)
                {
                    foreach( RecursoDocumentoEntity objDocumento in this._lstDocumentos)
                    {
                        objValoresList.Add(CargarValores(8, "List1", objDocumento.NombreDocumento, intNumeroDocumento, new Byte[1]));
                        objValoresList.Add(CargarValores(9, "List1", "", intNumeroDocumento, new Byte[1]));
                        objValoresList.Add(CargarValores(10, "List1", "", intNumeroDocumento, new Byte[1]));
                        objValoresList.Add(CargarValores(11, "List1", "", intNumeroDocumento, new Byte[1]));
                        objValoresList.Add(CargarValores(12, "List1", "", intNumeroDocumento, new Byte[1]));
                        intNumeroDocumento++;
                    }
                }

                //Serializar parametros para envío a VITAL
                objMemoryStream = new MemoryStream();
                objSerializador = new XmlSerializer(typeof(List<ValoresIdentity>));
                objSerializador.Serialize(objMemoryStream, objValoresList);

                //Obtener xml de serialización
                strXml = System.Text.UTF8Encoding.UTF8.GetString(objMemoryStream.ToArray());

                //Cargar informacion del formulario a registrar
                objQueja = new Queja();
                objParametrosFormulario = objQueja.ObtenerUsuarioRecurso(this.UsuarioID.ToString()).Tables[0];
                lngUsuarioQueja = Int64.Parse(this.UsuarioID.ToString());
                lngFormularioqueja = Int64.Parse(objParametrosFormulario.Rows[0]["FORM_ID"].ToString());
                strClientId = objParametrosFormulario.Rows[0]["CLIENT_ID"].ToString();

                //Realizar radicación
                strNumeroVITAL = objQueja.CrearProcesoQueja(strClientId, lngFormularioqueja, lngUsuarioQueja, strXml);

                if (string.IsNullOrWhiteSpace(strNumeroVITAL) || strNumeroVITAL.ToLower().Contains("error"))
                {
                    throw new Exception("No se obtuvo el número vital, error: " + (strNumeroVITAL != null ? strNumeroVITAL : ""));
                }
            }
            catch(Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: RegistrarSolicitudVITAL -> Error registrando solicitud en VITAL: " + exc.Message + " - " + exc.StackTrace.ToString());

                //Escalar excepcion
                throw exc;
            }

            return strNumeroVITAL;
        }


        /// <summary>
        /// Avanza a nuevo estado de proceso de notificacion
        /// </summary>
        /// <param name="p_strNumeroVital">string con el numero vital</param>
        /// <param name="p_strCarpetaAdjuntos">string con la ruta donde se ubican los adjuntos</param>
        private void AvanzarEstadoNotificacion(string p_strNumeroVital, string p_strCarpetaAdjuntos)
        {
            DateTime objFechaNotificacion = default(DateTime);
            NotificacionFachada objNotificacionFachada = null;
            Notificacion objNotificacion = null;
            SILPA.LogicaNegocio.Notificacion.EstadoNotificacion objEstadoNotificacion = null;
            EstadoNotificacionEntity objEstadoNotificacionEntity = null;
            EstadoFlujoNotificacion objEstadoFlujo = null;
            EstadoFlujoNotificacionEntity objConfiguracionEstado = null;
            GenerarLogAuditoria objCrearLogAuditoria = null;
            List<CorreoNotificacionEntity> lstCorreos = null;

            //Consultar datos del estado
            objEstadoNotificacion = new SILPA.LogicaNegocio.Notificacion.EstadoNotificacion();
            objEstadoNotificacionEntity = objEstadoNotificacion.ObtenerEstadoNotificacion(this.EstadoAvanzarID);

            //Cargar configuración del estado
            objEstadoFlujo = new EstadoFlujoNotificacion();
            objConfiguracionEstado = objEstadoFlujo.ConsultarConfiguracionEstadoNotificacionElectronica(this.FlujoEstadoAvanzarID, this.EstadoAvanzarID);

            //Realizar avance de estado
            objNotificacion = new Notificacion();
            objFechaNotificacion = objNotificacion.AvanzarEstadoNotificacionElectronica(this.ActoAdministrativoID, this.FlujoEstadoAvanzarID, this.EstadoAvanzarID, this.PersonaID, this._intAutoridadAmbientalID,
                                                                                        this._strNumeroVital, "Presentación de Recurso de Reposición",
                                                                                        ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString(), this._strExpediente,
                                                                                        this._strNumeroActo, objConfiguracionEstado.EnviaCorreoAvance, lstCorreos, objConfiguracionEstado.TextoCorreoAvance, objConfiguracionEstado.EnviaCorreoAvance, objConfiguracionEstado.EnviaCorreoAvance,
                                                                                        Convert.ToInt32(this.UsuarioID),
                                                                                        -1, -1, "", "", "", p_strCarpetaAdjuntos, p_strNumeroVital, DateTime.Now);
            //Actualizar proceso
            objNotificacionFachada = new SILPA.Servicios.NotificacionFachada();
            objNotificacionFachada.ActualizarProcesos(this.EstadoAvanzarID, objConfiguracionEstado.Estado, (objEstadoNotificacionEntity.EstadoPDI ? "SI" : "NO"), this.ActoAdministrativoID, this.PersonaID);

            //Insertar en el log
            objCrearLogAuditoria = new GenerarLogAuditoria();
            objCrearLogAuditoria.Insertar("SILPA", 1, "Se creó el estado desde recurso de reposición " + this.EstadoAvanzarID.ToString() + " para el usuario " + this.PersonaID.ToString() + " acto " + this.ActoAdministrativoID.ToString());
        }

        
        /// <summary>
        /// Adicionar registro de recurso de reposición en base de datos
        /// </summary>
        /// <param name="p_strNumeroVital">string con el numero vital</param>
        /// <param name="p_strCarpetaVital">string con ruta de archivos carpeta vital</param>
        /// <param name="int_autoridadID">int id autoridad ambiental ID</param>
        private void AdicionarRecursoReposicicion(string p_strNumeroVital,string p_strCarpetaVital, int int_autoridadID)
        {
            Recurso objRecurso = null;
            RecursoEntity objRecursoEntity = null;            

            try
            {
                //Cargar datos del recurso de reposicion
                objRecursoEntity = new RecursoEntity
                {
                    IDRecurso = 0,
                    IdActoNotificacion = this.ActoAdministrativoID,
                    Descripcion = this.txtDescripcionPresentarRecurso.Text.Trim() + " - " + p_strNumeroVital + " - " + this._strNumeroActo + " - " + this._strExpediente,
                    Estado = new RecursoEstadoEntity { IDEstadoRecurso = 2 },
                    Acto = new NotificacionEntity { IdActoNotificacion = this.ActoAdministrativoID },
                    NumeroIdentificacion = this.NumeroIdentificacionPersona
                };

                if (int_autoridadID != (int)AutoridadesAmbientales.ANLA)
                {
                    //Crear objeto manipulacion de datos
                    objRecurso = new Recurso();
                    objRecurso.InsertarRecursoExtendido(ref objRecursoEntity, this._strExpediente, this._strNumeroVital, this._strNumeroVital, p_strNumeroVital, this._strNumeroActo);

                    //Avanzar estado
                    this.AvanzarEstadoNotificacion(p_strNumeroVital, p_strCarpetaVital);
                }
                else if (int_autoridadID == (int)AutoridadesAmbientales.ANLA)
                { 
                    //TODO CONSUMO SERVICIOS REST SINOT
                    RecursoReposicionService objRecursoReposicionService = new RecursoReposicionService();
                    //creamos el objeto y asignamos lo valores a las propiedades
                    FormaPresentarRecursoReposicionEntity objFormaPresentarRecursoReposicionEntity = new FormaPresentarRecursoReposicionEntity{
                        
                        ActoAdministrativoID = objRecursoEntity.IdActoNotificacion,
                        Descripcion = objRecursoEntity.Descripcion,
                        EstadoRecursoID = objRecursoEntity.Estado.IDEstadoRecurso,
                        NumeroIdentificacion = objRecursoEntity.NumeroIdentificacion,
                        ExpedientePadre = this._strExpediente,
                        NumeroVITALAdicional = this._strNumeroVital,
                        NumeroVITALPadre = this._strNumeroVital,
                        NumeroVITALGenerado = p_strNumeroVital,
                        NumeroActoAdministrativo = this._strNumeroActo,
                        EstadoPersonaActoID = this.EstadoPersonaActoID
                    };
                    objRecursoReposicionService.PresentarRecursoReposicion(objFormaPresentarRecursoReposicionEntity);
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: AdicionarRecursoReposicicion -> Error registrando solicitud en BD: " + exc.Message + " - " + exc.StackTrace.ToString());

                //Escalar excepcion
                throw exc;
            }
        }

    #endregion


    #region Metodos Publicos

        /// <summary>
        /// Mostrar modal para solicitar la información del recurso de reposición
        /// </summary>
        public void SolicitarInformacionRecurso()
        {
            string strCarpetaTemporal = "";

            try
            {
                //Eliminar carpeta temporal            
                strCarpetaTemporal = ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString() + "RR_" + this.ActoAdministrativoID.ToString() + "_" + this.PersonaID.ToString() + "//";
                if (Directory.Exists(strCarpetaTemporal))
                    Directory.Delete(strCarpetaTemporal, true);

                //Limpiar campos modal de solicitud de informacion
                this.LimpiarCamposPresentarRecurso();

                //Verificar que se tenga datos del acto administrativo y la persona
                if (this.ActoAdministrativoID > 0 && this.PersonaID > 0 && this.FlujoEstadoAvanzarID > 0 && this.EstadoAvanzarID > 0)
                {
                    //Obtener datos del acto administrativo
                    this.CargarInformacionActoAdministrativoPersona();

                    //Cargar datos del acto adminsitrativo
                    this.ltlNumeroVital.Text = this._strNumeroVital;
                    this.ltlAutoridadAmbiental.Text = this._strAutoridadAmbiental;
                    this.ltlExpediente.Text = this._strExpediente;
                    this.ltlNumeroActoAdministrativo.Text = this._strNumeroActo;
                    this.ltlFechaActoAdministrativo.Text = this._objFechaActo.ToString("dd/MM/yyyy");
                    this.ltlFechaNotificacion.Text = this.FechaNotificacion != null ? this.FechaNotificacion.Value.ToString("dd/MM/yyyy"): this._objFechaNotificacion.ToString("dd/MM/yyyy");

                    //Cargar listado de documentos
                    this.CargarListadoArchivos();

                    //Actualizar panel
                    this.upnlModalPresentarRecurso.Update();
                }
                else
                {
                    throw new Exception("No se especifico información del acto administrativo, persona o estado de avance para manejo de recurso de reposición");
                }
            }
            catch(Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: SolicitarInformacionRecurso -> Se presento error cargando datos de acto administrativo para solicitar recurso de reposición: " + exc.Message + " - " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

        }


        /// <summary>
        /// Registrar un recurso de reposición
        /// </summary>
        /// <returns>string con el número VITAL generado</returns>
        public string RegistrarRecursoReposicion(int int_autoridadID)
        {
            RadicacionDocumento objRadicacionDocumento = null;            
            string strNumeroVital = "";
            string strCarpetaVital = "";
            string strCarpetaTemporal = "";
            string strCarpetaTemporalCopia = "";

            try
            {
                //Verificar que no existan errores
                if (Page.IsValid)
                {
                    //Registrar solictud en vital
                    strNumeroVital = this.RegistrarSolicitudVITAL();

                    //Cargar carpeta temporal
                    strCarpetaTemporal = ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString() + "RR_" + this.ActoAdministrativoID.ToString() + "_" + this.PersonaID.ToString() + "//";

                    //Verificar que se obtenga el numero vital
                    if (!string.IsNullOrWhiteSpace(strNumeroVital) && strNumeroVital != "0")
                    {
                        //Verificar si se anexaron documentos obtener ruta carpeta vital
                        if (this._lstDocumentos != null && this._lstDocumentos.Count > 0)
                        {
                            //Obtener carpeta documentos VITAL
                            objRadicacionDocumento = new RadicacionDocumento();
                            strCarpetaVital = objRadicacionDocumento.ObtenerPathDocumentosNumeroVital(strNumeroVital);

                            //Mover archivos a carpeta vital
                            if(Directory.Exists(strCarpetaTemporal))
                            {
                                foreach(RecursoDocumentoEntity objDocumento in this._lstDocumentos)
                                {
                                    File.Copy(strCarpetaTemporal + objDocumento.NombreDocumento, strCarpetaVital + objDocumento.NombreDocumento);
                                }
                            }
                        }

                        //Adicionar el recurso de reposición en bd
                        this.AdicionarRecursoReposicicion(strNumeroVital, strCarpetaVital, int_autoridadID);                        
                    }
                    else
                    {
                        throw new Exception("No se obtuvo número vital para solicitud");
                    }

                    //Limpiar campos
                    this.LimpiarCamposPresentarRecurso();

                    //Limpiar campos acto administrativo
                    this.ActoAdministrativoID = 0;
                    this.PersonaID = 0;
                    this.FlujoEstadoAvanzarID = 0;
                    this.EstadoAvanzarID = 0;
                    this.EstadoPersonaActoID = 0;
                    this.FechaNotificacion = null;
                    this.NumeroIdentificacionPersona = "";

                    //Actualizar panel
                    this.upnlModalPresentarRecurso.Update();

                    //Mover carpeta temporal            
                    strCarpetaTemporalCopia = ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString() + "CP_RR_" + strNumeroVital + "//";
                    if (Directory.Exists(strCarpetaTemporal))
                    {
                        Directory.Move(strCarpetaTemporal, strCarpetaTemporalCopia);
                    }
                }
                else
                {
                    this.CargarListadoArchivos();
                    this.upnlModalPresentarRecurso.Update();
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error registrando recurso de reposicion')", true);

                //Recargar listados y actualizar panel
                this.CargarListadoArchivos();
                this.upnlModalPresentarRecurso.Update();

                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: RegistrarRecursoReposicion -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
            }

            return strNumeroVital;
        }


        /// <summary>
        /// Cancelar proceso de presentar recurso
        /// </summary>
        public void CancelarProcesoPresentarRecurso()
        {
            string strCarpetaTemporal = "";

            try
            {
                //Eliminar carpeta temporal            
                strCarpetaTemporal = ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString() + "RR_" + this.ActoAdministrativoID.ToString() + "_" + this.PersonaID.ToString() + "//";
                if (Directory.Exists(strCarpetaTemporal))
                    Directory.Delete(strCarpetaTemporal, true);

                //Limpiar campos acto administrativo
                this.ActoAdministrativoID = 0;
                this.PersonaID = 0;
                this.FlujoEstadoAvanzarID = 0;
                this.EstadoAvanzarID = 0;
                this.EstadoPersonaActoID = 0;
                this.FechaNotificacion = null;
                this.NumeroIdentificacionPersona = "";

                //Limpiar campos de solicitud de informacion
                this.LimpiarCamposPresentarRecurso();

                //Ocultar el modal
                this.upnlModalPresentarRecurso.Update();
            }
            catch (Exception exc)
            {
                //Escribir error
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error limpiando y cerrando modal')", true);

                //Recargar listados y actualizar panel
                this.CargarListadoArchivos();
                this.upnlModalPresentarRecurso.Update();

                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: CancelarProcesoPresentarRecurso -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
            }
        }


    #endregion


    #region Eventos


        #region Page

            /// <summary>
            /// Evento que se ejecuta al momento del cargue de la página
            /// </summary>
            protected void Page_Load(object sender, EventArgs e){}

        #endregion


        #region ModalPresentarRecurso


            #region cvSegundaContrasena
    
                /// <summary>
                /// Validador que verifica la segunda clave
                /// </summary>
                protected void cvSegundaContrasena_ServerValidate(object source, ServerValidateEventArgs args)
                {
                    SILPA.LogicaNegocio.Generico.Persona objPersona = new SILPA.LogicaNegocio.Generico.Persona();
                    AjaxControlToolkit.AsyncFileUpload objAsyncFileUpload = null;

                    try
                    {
                        //Cargar objeto 
                        objAsyncFileUpload = (AjaxControlToolkit.AsyncFileUpload)this.grdArchivosPresentarRecurso.FooterRow.FindControl("fuplCargarDocumentoPresentarRecurso");

                        //Verificar que exista el objeto
                        if (objAsyncFileUpload != null && !objAsyncFileUpload.IsUploading)
                        {
                            if (!string.IsNullOrWhiteSpace(this.txtSegundaContrasenaPresentarRecurso.Text))
                            {

                                //Verificar que la segunda clave sea válida
                                objPersona = new SILPA.LogicaNegocio.Generico.Persona();
                                if (!objPersona.ValidarSegundaClave(int.Parse(this.UsuarioID), EnDecript.Encriptar(this.txtSegundaContrasenaPresentarRecurso.Text.Trim())))
                                {
                                    args.IsValid = false;
                                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- La segunda clave ingresada es incorrecta')", true);
                                }
                                else
                                {
                                    args.IsValid = true;
                                }
                            }
                            else
                            {
                                args.IsValid = false;
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
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error verificando la segunda clave')", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: cvSegundaAceptarNotificarse_ServerValidate -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                }

            #endregion


            #region grdArchivosPresentarRecurso

                /// <summary>
                /// Permitir visualizar un documento del listado de documentos
                /// </summary>
                protected void lnkVerArchivoPresentarRecurso_Click(object sender, EventArgs e)
                {
                    int intPosicion = 0;
                    byte[] objContenidoAnexo = null;

                    try
                    {
                        //Cargar posicion del registro
                        intPosicion = Convert.ToInt32(((LinkButton)sender).CommandArgument);

                        //Verificar que la posicion sea valida
                        if (this._lstDocumentos != null && this._lstDocumentos.Count >= (intPosicion + 1))
                        {
                            //Verificar que el archivo exista
                            if (File.Exists(this._lstDocumentos[intPosicion].RutaDocumento + this._lstDocumentos[intPosicion].NombreDocumento))
                            {
                                //Leer archivo
                                objContenidoAnexo = File.ReadAllBytes(this._lstDocumentos[intPosicion].RutaDocumento + this._lstDocumentos[intPosicion].NombreDocumento);

                                //Mostrar archivo
                                Response.Clear();
                                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", this._lstDocumentos[intPosicion].NombreDocumento));
                                Response.ContentType = this._lstDocumentos[intPosicion].TipoDocumento;
                                Response.BinaryWrite(objContenidoAnexo);
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error cargando archivo para visualización')", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: lnkVerArchivoPresentarRecurso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                }


                /// <summary>
                /// Elimina un registro del listado de archivos
                /// </summary>
                protected void lnkEliminarArchivoPresentarRecurso_Click(object sender, EventArgs e)
                {
                    int intPosicion = 0;
                    string strCarpetaTemporal = "";

                    try
                    {
                        //Cargar posicion del registro
                        intPosicion = Convert.ToInt32(((LinkButton)sender).CommandArgument);

                        //Verificar que la posicion sea menor o igual a la longitud de los registros
                        if (this._lstDocumentos != null && this._lstDocumentos.Count >= (intPosicion + 1))
                        {
                            //Cargar carpeta temporal
                            strCarpetaTemporal = ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString() + "RR_" + this.ActoAdministrativoID.ToString() + "_" + this.PersonaID.ToString() + "//";

                            //Eliminar archivo fisico
                            if (File.Exists(this._lstDocumentos[intPosicion].RutaDocumento + this._lstDocumentos[intPosicion].NombreDocumento))
                                File.Delete(this._lstDocumentos[intPosicion].RutaDocumento + this._lstDocumentos[intPosicion].NombreDocumento);

                            //Remover del listado
                            this._lstDocumentos.RemoveAt(intPosicion);

                            //Verificar si el listado se encuentra vacio eliminar carpeta
                            if(this._lstDocumentos.Count == 0 && Directory.Exists(strCarpetaTemporal))
                            {
                                Directory.Delete(strCarpetaTemporal);
                            }
                        }
                    }
                    catch(Exception exc)
                    {
                        //Escribir error
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error eliminando archivo del listado')", true);                        

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: lnkEliminarArchivoPresentarRecurso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                    finally
                    {
                        //Recargar listados
                        this.CargarListadoArchivos();
                        this.upnlModalPresentarRecurso.Update();
                    }
                }


                /// <summary>
                /// Evento que adiciona documento al listado y almacena en carpeta temporal
                /// </summary>
                protected void lnkAdicionarArchivoPresentarRecurso_Click(object sender, EventArgs e)
                {
                    AjaxControlToolkit.AsyncFileUpload objAsyncFileUpload = null;
                    RecursoDocumentoEntity objDocumento = null;
                    string strCarpetaDocumento = "";
                    string strNombreDocumento = "";

                    try
                    {
                        //Cargar objeto 
                        objAsyncFileUpload = (AjaxControlToolkit.AsyncFileUpload)this.grdArchivosPresentarRecurso.FooterRow.FindControl("fuplCargarDocumentoPresentarRecurso");

                        //Verificar que exista objeto
                        if (objAsyncFileUpload != null && objAsyncFileUpload.PostedFile != null && objAsyncFileUpload.HasFile)
                        {                            
                            //Crear la carpeta del documento
                            strCarpetaDocumento = ConfigurationManager.AppSettings["NOT_Carpeta_Temporal"].ToString() + "RR_" + this.ActoAdministrativoID.ToString() + "_" + this.PersonaID.ToString() + "//";
                            if (!Directory.Exists(strCarpetaDocumento))
                                Directory.CreateDirectory(strCarpetaDocumento);

                            //Cargar nombre de documento
                            strNombreDocumento = "RR" + DateTime.Now.ToString("ddMMyyyyHHmmssfffff") + "_" + objAsyncFileUpload.FileName;

                            //Grabar archivo en carpeta temporal
                            objAsyncFileUpload.SaveAs(strCarpetaDocumento + strNombreDocumento);

                            //Cargar datos 
                            objDocumento = new RecursoDocumentoEntity
                            {
                                RutaDocumento = strCarpetaDocumento,
                                NombreDocumento = strNombreDocumento,
                                TipoDocumento = objAsyncFileUpload.PostedFile.ContentType
                            };

                            //Adicionar a listado
                            if (this._lstDocumentos == null)
                                this._lstDocumentos = new List<RecursoDocumentoEntity>();
                            this._lstDocumentos.Add(objDocumento);
                            
                        }
                        else{
                            //Escribir error
                            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- No se encontro archivo para adicionar')", true);
                        }
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error adicionando archivo al listado')", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: lnkAdicionarArchivoPresentarRecurso_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                    finally
                    {
                        this.CargarListadoArchivos();
                        this.upnlModalPresentarRecurso.Update();
                    }
                }

                
                /// <summary>
                /// Validador que verifica que se haya selccionado un archivo
                /// </summary>
                protected void cvCargarDocumentoPresentarRecurso_ServerValidate(object source, ServerValidateEventArgs args)
                {
                    AjaxControlToolkit.AsyncFileUpload objAsyncFileUpload = null;

                    try
                    {
                        //Cargar objeto 
                        objAsyncFileUpload = (AjaxControlToolkit.AsyncFileUpload)this.grdArchivosPresentarRecurso.FooterRow.FindControl("fuplCargarDocumentoPresentarRecurso");

                        //Verificar que exista el objeto
                        if (objAsyncFileUpload != null)
                        {
                            if (!objAsyncFileUpload.IsUploading)
                            {
                                //Verificar que se haya anexado archivo
                                if (objAsyncFileUpload.PostedFile == null || !objAsyncFileUpload.HasFile)
                                {
                                    args.IsValid = false;
                                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Debe seleccionar un documento')", true);
                                }
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
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error verificando si se selecciono documento')", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: cvCargarDocumentoPresentarRecurso_ServerValidate -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                }

            #endregion


            #region cvListaDocumentosPresentarRecurso

                /// <summary>
                /// Validador que verifica se hayan ingresado documentos
                /// </summary>
                protected void cvListaDocumentosPresentarRecurso_ServerValidate(object source, ServerValidateEventArgs args)
                {
                    AjaxControlToolkit.AsyncFileUpload objAsyncFileUpload = null;

                    try
                    {
                        //Cargar objeto 
                        objAsyncFileUpload = (AjaxControlToolkit.AsyncFileUpload)this.grdArchivosPresentarRecurso.FooterRow.FindControl("fuplCargarDocumentoPresentarRecurso");

                        //Verificar que exista el objeto
                        if (objAsyncFileUpload != null && !objAsyncFileUpload.IsUploading)
                        {

                            if (this._lstDocumentos == null || this._lstDocumentos.Count == 0)
                            {
                                args.IsValid = false;
                                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Debe ingresar por lo menos un documento al listado')", true);
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
                        args.IsValid = false;
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Error", "alert('- Se presento error verificando listado de documentos')", true);

                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "Recurso_Controles_RegistrarRecursoReposicion :: cvListaDocumentosPresentarRecurso_ServerValidate -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());
                    }
                }

            #endregion

        #endregion


    #endregion


}