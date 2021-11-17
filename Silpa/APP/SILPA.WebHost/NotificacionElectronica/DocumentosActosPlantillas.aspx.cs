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

public partial class DocumentosActosPlantillas_NotificacionEstados : System.Web.UI.Page
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
            objDocumentos.Columns.Add("TIPO_DOCUMENTO", Type.GetType("System.String"));
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
            Notificacion objNotificacion = null;
            DataTable objConceptosActo = null;
            DataRow objDocumento = null;
            string strUrlActo = "";
            string[] lstStrDatosLLave = null;
            long lngActoID = 0;
            long lngPersonaID = 0;

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
                    
                    //Cargar identificador acto administrativo
                    lstStrDatosLLave = strIdentificadorEnlace.Split('_');
                    if (lstStrDatosLLave.Length == 2)
                    {
                        lngActoID = Convert.ToInt64(lstStrDatosLLave[0]);
                        lngPersonaID = Convert.ToInt64(lstStrDatosLLave[1]);
                    }
                    else
                        throw new Exception("Composición de llave incorrecta");

                    //Crear objeto de notificacion
                    objNotificacion = new Notificacion();

                    //Cargar URL acto administrativo
                    strUrlActo = objNotificacion.ConsultarRuta(lngActoID);

                    //Verificar que se encuentre datos del acto
                    if(!string.IsNullOrEmpty(strUrlActo)){

                        //Cargar los conceptos
                        objConceptosActo = objNotificacion.ConsultarConceptosAsociadosActoAdministrativo(lngActoID);

                        //Crear tabla de documentos
                        this._objDocumentos = this.CrearTablaDocumentos();

                        //Adicionar los documentos
                        objDocumento = this._objDocumentos.NewRow();
                        objDocumento["TIPO_DOCUMENTO"] = "ACTO ADMINISTRATIVO";
                        objDocumento["NOMBRE_DOCUMENTO"] = strUrlActo.Substring((strUrlActo.LastIndexOf("/") != -1 ? strUrlActo.LastIndexOf("/") : strUrlActo.LastIndexOf("\\") + 1));
                        objDocumento["RUTA_DOCUMENTO"] = strUrlActo;
                        this._objDocumentos.Rows.Add(objDocumento);
                            
                        //Adicionar conceptos en caso de que existan
                        if (objConceptosActo != null && objConceptosActo.Rows.Count > 0)
                        {
                            foreach (DataRow objConcepto in objConceptosActo.Rows)
                            {
                                objDocumento = this._objDocumentos.NewRow();
                                objDocumento["TIPO_DOCUMENTO"] = "DOCUMENTO ASOCIADO";
                                objDocumento["NOMBRE_DOCUMENTO"] = objConcepto["DOC_ARCHIVO"].ToString();
                                if (Convert.ToInt32(objConcepto["NOT_AUT_ID"]) == (int)AutoridadesAmbientales.ANLA)
                                    objDocumento["RUTA_DOCUMENTO"] = "CON-SILA@" + objConcepto["DOC_ID"].ToString() + "_" + lngActoID.ToString() + "_" + lngPersonaID.ToString();
                                else
                                    objDocumento["RUTA_DOCUMENTO"] = "CON-SILAMC@" + objConcepto["DOC_ID"].ToString() + "_" + lngActoID.ToString() + "_" + lngPersonaID.ToString();
                                this._objDocumentos.Rows.Add(objDocumento);
                            }
                        }

                        //Cargar los datos de los documentos
                        this.grdDocumentos.DataSource = this._objDocumentos;
                        this.grdDocumentos.DataBind();
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
                SMLog.Escribir(Severidad.Critico, "DocumentosActosPlantillas_NotificacionEstados :: InicializarPagina -> Error Inesperado: " + exc.Message);

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
                                    EstadoPersonaID = 0,
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
                                    EstadoPersonaID = 0,
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