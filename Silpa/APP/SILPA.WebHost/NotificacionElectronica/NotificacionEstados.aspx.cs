using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.Comun;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.AccesoDatos.Notificacion;
using System.Data;
using System.Configuration;

public partial class NotificacionElectronica_NotificacionEstados : System.Web.UI.Page
{
    #region Metodos Privados

        /// <summary>
        /// Listado de documentos
        /// </summary>
        private DataTable _objDocumentos
        {
            get
            {
                return (DataTable)ViewState["objDocumentos"];
            }
            set
            {
                ViewState["objDocumentos"] = value;
            }
        }

    #endregion


    #region Metodos Privados

        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.divMensaje.Visible = true;
        }


        /// <summary>
        /// Ocultar los mensajes
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.divMensaje.Visible = false;
        }


        /// <summary>
        /// Crear tabla de de documentos
        /// </summary>
        /// <returns>DataTable vacio</returns>
        private DataTable CrearTablaDocumentos()
        {
            DataTable objDocumentos = null;

            //CRear tabla
            objDocumentos = new DataTable();

            //Adicionar columnas
            objDocumentos.Columns.Add("NOMBRE_DOCUMENTO", Type.GetType("System.String"));
            objDocumentos.Columns.Add("RUTA_DOCUMENTO", Type.GetType("System.String"));

            return objDocumentos;
        }

        /// <summary>
        /// Inicializa la pagina con la información de documentos
        /// </summary>
        private void InicializarPagina()
        {
            string strLlave = "";
            string strIdentificadorEnlace = "";
            EnlaceDocumentoEntity objEnlaceEntity = null;
            EnlaceDocumento objEnlace = null;
            Notificacion objNotificacion = null;
            DataSet objDatosEstado = null;
            DataTable objConceptosActo = null;
            DataRow objDocumento = null;
            string strUrlActo = "";

            try
            {
                //Ocultar mensajes
                this.OcultarMensaje();

                //Cargar querystring
                strLlave = Request.QueryString["Pub"];

                //Validar que se obtenga llave
                if (!string.IsNullOrWhiteSpace(strLlave))
                {
                    //Obtener llave
                    strIdentificadorEnlace = EnDecript.DesencriptarDesplazamiento(strLlave);

                    //Cargar datos del enlace
                    objEnlace = new EnlaceDocumento();
                    objEnlaceEntity = objEnlace.ConsultarEnlace(strIdentificadorEnlace, strLlave);

                    //Validar que se obtenga informacion
                    if (objEnlaceEntity != null)
                    {
                        //Validar la fecha de vigencia
                        if (objEnlaceEntity.FechaVigencia == default(DateTime) || objEnlaceEntity.FechaVigencia >= DateTime.Now)
                        {
                            //Verificar si es un enlace VITAL
                            if (objEnlaceEntity.AutoridadID == (int)AutoridadesAmbientales.ANLA)
                            {
                                if (ConfigurationManager.AppSettings["URLEstadosNotificacionesANLA"] != null && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["URLEstadosNotificacionesANLA"].ToString()))
                                    Response.Redirect(ConfigurationManager.AppSettings["URLEstadosNotificacionesANLA"].ToString() + "?Pub=" + strLlave, false);
                                else
                                    this.MostrarMensaje("No se encontro información de la llave especificada");
                            }
                            else
                            {

                                //Crear objeto de notificacion
                                objNotificacion = new Notificacion();

                                //Cargar url acto administrativo
                                if (objEnlaceEntity.IncluirActoAdministrativo)
                                    strUrlActo = objNotificacion.ConsultarRuta(objEnlaceEntity.ActoNotificacionID);

                                //Si se incluye conceptos cargar información del concepto
                                if (objEnlaceEntity.IncluirConceptosActoAdministrativo)
                                {
                                    objConceptosActo = objNotificacion.ConsultarConceptosAsociadosActoAdministrativo(objEnlaceEntity.ActoNotificacionID);
                                }

                                //Cargar datos del estado
                                objDatosEstado = objNotificacion.ObtenerInformacionEstadoPersonaActo(objEnlaceEntity.EstadoPersonaID);

                                //Validar que se encuentre información del estado
                                if (objDatosEstado != null && objDatosEstado.Tables.Count > 0 && objDatosEstado.Tables[0].Rows.Count > 0)
                                {
                                    //Crear tabla de documentos
                                    this._objDocumentos = this.CrearTablaDocumentos();

                                    //Adicionar los documentos
                                    if (!string.IsNullOrWhiteSpace(strUrlActo))
                                    {
                                        objDocumento = this._objDocumentos.NewRow();
                                        objDocumento["NOMBRE_DOCUMENTO"] = strUrlActo.Substring((strUrlActo.LastIndexOf("/") != -1 ? strUrlActo.LastIndexOf("/") : strUrlActo.LastIndexOf("\\") + 1));
                                        objDocumento["RUTA_DOCUMENTO"] = strUrlActo;
                                        this._objDocumentos.Rows.Add(objDocumento);
                                    }

                                    //Adicionar conceptos en caso de que existan
                                    if (objConceptosActo != null && objConceptosActo.Rows.Count > 0)
                                    {
                                        foreach (DataRow objConcepto in objConceptosActo.Rows)
                                        {
                                            objDocumento = this._objDocumentos.NewRow();
                                            objDocumento["NOMBRE_DOCUMENTO"] = objConcepto["DOC_ARCHIVO"].ToString();
                                            if (Convert.ToInt32(objConcepto["NOT_AUT_ID"]) == (int)AutoridadesAmbientales.ANLA)
                                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILA@" + objConcepto["DOC_ID"].ToString() + "_" + objEnlaceEntity.ActoNotificacionID.ToString() + "_" + objEnlaceEntity.PersonaID.ToString() + "_" + objEnlaceEntity.EstadoPersonaID.ToString();
                                            else
                                                objDocumento["RUTA_DOCUMENTO"] = "CON-SILAMC@" + objConcepto["DOC_ID"].ToString() + "_" + objEnlaceEntity.ActoNotificacionID.ToString() + "_" + objEnlaceEntity.PersonaID.ToString() + "_" + objEnlaceEntity.EstadoPersonaID.ToString();
                                            this._objDocumentos.Rows.Add(objDocumento);
                                        }
                                    }

                                    //Agrgar información de documentos propios del proceso
                                    if (!string.IsNullOrEmpty(objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString()) && !objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("/") && !objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().ToString().EndsWith("\\"))
                                    {
                                        objDocumento = this._objDocumentos.NewRow();
                                        objDocumento["NOMBRE_DOCUMENTO"] = objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().Substring((objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") != -1 ? objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("/") : objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString().LastIndexOf("\\") + 1));
                                        objDocumento["RUTA_DOCUMENTO"] = objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO"].ToString();
                                        this._objDocumentos.Rows.Add(objDocumento);
                                    }
                                    if (!string.IsNullOrEmpty(objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString()) && !objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("/") && !objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().ToString().EndsWith("\\"))
                                    {
                                        objDocumento = this._objDocumentos.NewRow();
                                        objDocumento["NOMBRE_DOCUMENTO"] = objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().Substring((objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("/") != -1 ? objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("/") : objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString().LastIndexOf("\\") + 1));
                                        objDocumento["RUTA_DOCUMENTO"] = objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_ADICIONAL"].ToString();
                                        this._objDocumentos.Rows.Add(objDocumento);
                                    }
                                    if (!string.IsNullOrEmpty(objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString()) && !objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().ToString().EndsWith("/") && !objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().ToString().EndsWith("\\"))
                                    {
                                        objDocumento = this._objDocumentos.NewRow();
                                        objDocumento["NOMBRE_DOCUMENTO"] = objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().Substring((objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().LastIndexOf("/") != -1 ? objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().LastIndexOf("/") : objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString().LastIndexOf("\\") + 1));
                                        objDocumento["RUTA_DOCUMENTO"] = objDatosEstado.Tables[0].Rows[0]["RUTA_DOCUMENTO_PLANTILLA"].ToString();
                                        this._objDocumentos.Rows.Add(objDocumento);
                                    }

                                    //Modificar titulo
                                    this.lblTitulo.Text = this.lblTitulo.Text + "<br/>Acto Administrativo No. " + objDatosEstado.Tables[0].Rows[0]["NOT_NUMERO_ACTO_ADMINISTRATIVO"].ToString() + " Expediente " + objDatosEstado.Tables[0].Rows[0]["EXPEDIENTE"].ToString() + "<br/>" + objDatosEstado.Tables[0].Rows[0]["USUARIO_NOTIFICAR"].ToString() + "<br/><br/>";

                                    //Cargar los datos de los documentos
                                    this.grdDocumentos.DataSource = this._objDocumentos;
                                    this.grdDocumentos.DataBind();
                                }
                                else
                                {
                                    //Cargar mensaje de error                        
                                    this.MostrarMensaje("No se encontro información de estado a mostrar");
                                }
                            }
                        }
                        else if (objEnlaceEntity.FechaVigencia != default(DateTime) && objEnlaceEntity.FechaVigencia < DateTime.Now)
                        {
                            //Cargar mensaje de error                        
                            this.MostrarMensaje("Documentos no disponbibles por exceder fecha de vigencia.");
                        }
                    }
                    else
                    {
                        throw new Exception("No se encontro información del enlace " + (!string.IsNullOrWhiteSpace(strIdentificadorEnlace) ? strIdentificadorEnlace : "") + " Llave: " + strLlave);
                    }
                }
                else
                {
                    throw new Exception("No se especifico información para cargar información de documentos");
                }
            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_NotificacioEstados :: InicializarPagina -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error realizando el cargue de información de la página");
            }

        }

    #endregion


    #region Eventos

        #region Page

            /// <summary>
            /// Evento que inicializa la información de la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    this.InicializarPagina();
                }
            }

        #endregion


        #region grdNotificaciones

            /// <summary>
            /// Evento que se ejecuta al dar clic en el link de documento
            /// </summary>
            protected void imgDescargarDocumento_Click(object sender, ImageClickEventArgs e)
            {
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
                string[] lstURL = null;
                string[] lstLlaves = null;
                string strEnlace = "";
                EnlaceDocumentoSila objEnlace = null;
                EnlaceDocumentoSilaEntity objEnlaceEntity = null;

                try
                {
                    //Ocultar Mensajes
                    this.OcultarMensaje();

                    //Validar que exista información
                    if (((ImageButton)sender).CommandArgument != null)
                    {
                        //Verificar el origen del archivo
                        if( ((ImageButton)sender).CommandArgument.ToString().StartsWith("CON-SILA@")){

                            //Crear objeto de parametros
                            objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

                            //Cargar datos url
                            lstURL = ((ImageButton)sender).CommandArgument.ToString().Split('@');

                            if (lstURL != null && lstURL.Length > 1)
                            {
                                //Cargar llaves
                                lstLlaves = lstURL[1].Split('_');

                                //Cargar datos
                                objEnlaceEntity = new EnlaceDocumentoSilaEntity
                                {
                                    EnlaceID = lstURL[1],
                                    ActoNotificacionID = Convert.ToInt64(lstLlaves[1]),
                                    PersonaID = Convert.ToInt64(lstLlaves[2]),
                                    EstadoPersonaID = Convert.ToInt64(lstLlaves[3]),
                                    DocumentoID = Convert.ToInt32(lstLlaves[0]),
                                    Llave = EnDecript.Encriptar(lstURL[1]),
                                    FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_SILA_NOTIFICACION")))
                                };

                                //Almacenar enlace
                                objEnlace = new EnlaceDocumentoSila();
                                objEnlace.CrearEnlace(objEnlaceEntity);

                                //Obtener URL                                
                                strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_VER_CONCEPTOS_SILA_NOTIFICACION") + objEnlaceEntity.Llave;

                                //Redireccionar
                                Response.Redirect(strEnlace, false);
                            }
                            else
                                throw new Exception("No se encontro información URL " + ((ImageButton)sender).CommandArgument.ToString());
                        }
                        else if (((ImageButton)sender).CommandArgument.ToString().StartsWith("CON-SILAMC@"))
                        {

                            //Crear objeto de parametros
                            objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();

                            //Cargar datos url
                            lstURL = ((ImageButton)sender).CommandArgument.ToString().Split('@');

                            if (lstURL != null && lstURL.Length > 1)
                            {
                                //Cargar llaves
                                lstLlaves = lstURL[1].Split('_');

                                //Cargar datos
                                objEnlaceEntity = new EnlaceDocumentoSilaEntity
                                {
                                    EnlaceID = lstURL[1],
                                    ActoNotificacionID = Convert.ToInt64(lstLlaves[1]),
                                    PersonaID = Convert.ToInt64(lstLlaves[2]),
                                    EstadoPersonaID = Convert.ToInt64(lstLlaves[3]),
                                    DocumentoID = Convert.ToInt32(lstLlaves[0]),
                                    Llave = EnDecript.Encriptar(lstURL[1]),
                                    FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_SILAMC_NOTIFICACION")))
                                };

                                //Almacenar enlace
                                objEnlace = new EnlaceDocumentoSila();
                                objEnlace.CrearEnlace(objEnlaceEntity);

                                //Obtener URL                                
                                strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_VER_CONCEPTOS_SILAMC_NOTIFICACION") + objEnlaceEntity.Llave;

                                //Redireccionar
                                Response.Redirect(strEnlace, false);
                            }
                            else
                                throw new Exception("No se encontro información URL " + ((ImageButton)sender).CommandArgument.ToString());
                        }
                        else
                        {
                            System.IO.FileInfo targetFile = new System.IO.FileInfo(((ImageButton)sender).CommandArgument.ToString());
                            this.Response.Clear();
                            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
                            this.Response.AddHeader("Content-Length", targetFile.Length.ToString());
                            this.Response.ContentType = "application/octet-stream";
                            this.Response.ContentType = "application/base64";
                            this.Response.WriteFile(targetFile.FullName);
                            this.Response.WriteFile(((ImageButton)sender).CommandArgument.ToString());
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "NotificacionElectronica_NotificacionEstados :: imgDescargarDocumento_Click -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se presento error descargando el archivo indicado");
                }
            }

        #endregion

    #endregion

}