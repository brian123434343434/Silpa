using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SILPA.Comun;

using SILPA.LogicaNegocio;
using SILPA.LogicaNegocio.ICorreo;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using SoftManagement.Log;
using System.Xml;
using System.Data;
using System.Configuration;



namespace SILPA.LogicaNegocio.Generico
{
    /// <summary>
    /// 
    /// </summary>
    public class RadicacionDocumento
    {
        /// <summary>
        /// Constructor sin parametros, útil en la serialización XML
        /// </summary>
        public RadicacionDocumento()
        {
            
            _objConfiguracion       = new Configuracion();
            _objRadDocIdentity      = new RadicacionDocumentoIdentity();
            _objAA                  = new AutoridadAmbiental();
            
            _lstEtiquetasRadicables = new List<String>();
            this.CargarListaEtiquetas();
        }


        /// <summary>
        ///  contiene el listado de etiquetas que indican carga de archivos.
        /// </summary>
        private List<String> _lstEtiquetasRadicables;
                
        /// <summary>
        /// Contiene la configuracion desde el web.congfig de la webApp
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// objeto RadicacionDocumento identidad que contiene las propiedades.
        /// </summary>
        public RadicacionDocumentoIdentity _objRadDocIdentity;

        /// <summary>
        /// Encabezado de correo cuando el documento adjunto es un oficio o es un acto
        /// </summary>
        private string EncabezadoCorreo;

        /// <summary>
        /// Autoridad Ambiental
        /// </summary>
        private AutoridadAmbiental _objAA;

        /// <summary>
        /// Entidad con las propiedades de Persona
        /// </summary>
        private Persona _objPersona;

        /// <summary>
        /// Objeto AutoridadAmbiental
        /// </summary>
        public AutoridadAmbientalIdentity objAutoridadIdentity;
        
        #region Declaración de Metodos...


        /// <summary>
        /// Determina el encabezado del correo dependiendo de si es acto u oficio
        /// </summary>
        private void SetEncabezado()
        {
            if (this._objRadDocIdentity.ActoAdministrativo != string.Empty && _objRadDocIdentity.ActoAdministrativo != null) 
            {
                this.EncabezadoCorreo = "Número de acto administrativo: " + this._objRadDocIdentity.ActoAdministrativo;
                //this.EncabezadoCorreo =  this.EncabezadoCorreo  + 
            }
        }


        /// <summary>
        /// hava:08-oct-10
        /// Determina si la etiqueta actual es de un formbuilder de radicación
        /// </summary>
        /// <param name="label">string: nombre de la etiqeuta de radicación</param>
        /// <returns>bool:  true/false</returns>
        public bool EsEtiquetaRadicable(string label)
        {
            bool esRadicable = false;
            foreach (var etiqueta in this._lstEtiquetasRadicables)
            {
                if (label.ToUpper().Contains(etiqueta.ToUpper()))
                    esRadicable = true;
            }
            return esRadicable;
        }


        /// <summary>
        /// hava:08-oct-10
        /// Carga las etiquetas que indican radicaición
        /// </summary>
        public void CargarListaEtiquetas() 
        {
            if (!String.IsNullOrEmpty(_objConfiguracion.ArchivoEtiquetaRadicable) && System.IO.File.Exists(_objConfiguracion.ArchivoEtiquetaRadicable))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(_objConfiguracion.ArchivoEtiquetaRadicable);

                // Carga las etiquetas en memoria. 
                //foreach (DataTable dt in )
                //{
                    foreach (DataRow row in ds.Tables["labelArchivo"].Rows)
                    {
                        this._lstEtiquetasRadicables.Add(row["ID"].ToString());
                    }
                //}
            }
        }

        /// <summary>
        /// Configura las listas de archivos
        /// </summary>
        public void SetAdjuntos(List<string> lstString, List<Byte[]> lstBytes)
        {
            this._objRadDocIdentity.LstNombreDocumentoAdjunto = lstString;
            this._objRadDocIdentity.LstBteDocumentoAdjunto = lstBytes;
        }


        /// <summary>
        /// Envia los correos al solicitante de la soc
        /// </summary>
        private bool EnviarCorreo() 
        {
            //string Host = objConfiguracion.ServidorCorreo;//string.Empty;
            //string From = objConfiguracion.SenderCorreo;
            //string To = objConfiguracion.SenderCorreo;
            //string Subject = "Prueba desde SILPA";
            //string Body = "Prueba";
            //string Attch = string.Empty;
            //int puerto = int.Parse(objConfiguracion.PuertoCorreo);

            //string User = objConfiguracion.UsuarioCorreo;
            //string Clave = objConfiguracion.PassWordCorreo;

            //try 
            //{
            //    Correo objCorreo = new Correo(Host, From, To, Subject, Body, Attch, User, Clave, false, Host, puerto);
            //    objCorreo.EnviarCorreo();
            //    return true;
            //}
            //catch(Exception e)
            //{
            //    return false;
            //}

            this._objPersona = new Persona();
            _objPersona.PersonaByUserId(this._objRadDocIdentity.IdSolicitante);
            if (this._objRadDocIdentity.IdAA.HasValue && _objRadDocIdentity.IdAA.Value != 0)
            _objAA = new AutoridadAmbiental(this._objRadDocIdentity.IdAA.Value);
            

            //if (this._objRadDocIdentity.TipoDocumento == ((int)TipoDocumento.Acto))
            //{   // Acto.
            //    ActoAdministrativo obj =  new ActoAdministrativo();
            //    obj.ObteberActoPorNumeroActo(this._objRadDocIdentity.ActoAdministrativo,this._objRadDocIdentity.IdAA);

            //    ICorreo.Correo.EnviarActo(obj.Identity, _objPersona.Identity, _objAA.objAutoridadIdentity);
            //}
            //else 
            //{  // Oficio.
            //    OficioIdentity obj = new OficioIdentity();
            //    ICorreo.Correo.EnviarOficio(obj, _objPersona.Identity, _objAA.objAutoridadIdentity);
            //}
            return true;
        }

        /// <summary>
        /// Radica el documento adjunto
        /// </summary>
        public int RadicarDocumento() 
        {
            //AutoridadAmbientalDalc objAADalc = new AutoridadAmbientalDalc();
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();

            /// se verifica si los documentos estan relacionados a un acto administrativo
            if (this._objRadDocIdentity.ActoAdministrativo != string.Empty
                    && this._objRadDocIdentity.ActoAdministrativo != null)
            {
                this._objRadDocIdentity.TipoDocumento = (int)TipoDocumento.Acto;
            }
            else 
            { /// documento que relaciona un ofico
                this._objRadDocIdentity.TipoDocumento = (int)TipoDocumento.Oficio;
            }
            //objAADalc.ObtenerAutoridadById(ref objAutoridadIdentity);
            
            ///Si hay documentos adjuntos por el usuario se envia correo a la AA:
            if (this._objRadDocIdentity.LstNombreDocumentoAdjunto != null )
            {
                if (this._objRadDocIdentity.LstNombreDocumentoAdjunto.Count > 0)
                {
                    // Envía el documento al folder FileTraffic determinado
                    TraficoDocumento objTraffic = new TraficoDocumento();
                    string _strRuta = string.Empty;
                    List<string> listaNombresDocumentos = this._objRadDocIdentity.LstNombreDocumentoAdjunto;
                    string directorio = _objConfiguracion.DirectorioGattaca;
                    SMLog.Escribir(Severidad.Informativo, "Metodo RadicarDocumento");
                    //objTraffic.RecibirDocumento(this._objRadDocIdentity.LstBteDocumentoAdjunto, this._objRadDocIdentity.LstNombreDocumentoAdjunto);
                    //objTraffic.RecibirDocumento(this._objRadDocIdentity.NumeroSilpa, this._objRadDocIdentity.IdSolicitante, this._objRadDocIdentity.LstBteDocumentoAdjunto, ref listaNombresDocumentos, ref _strRuta);

                    if (this._objRadDocIdentity.NumeroFormulario == _objConfiguracion.FormularioSolicitudLicencia)
                    {

                        string rutaArchivosUsuario = _objConfiguracion.FileTraffic + @"\" + _objConfiguracion.RepositorioUsuario + @"\" + this._objRadDocIdentity.IdSolicitante + @"\";
                        if (objTraffic.RecibirDocumento(this._objRadDocIdentity.NumeroSilpa, this._objRadDocIdentity.IdSolicitante, directorio, rutaArchivosUsuario, ref listaNombresDocumentos, ref _strRuta))
                        {
                            foreach (string nombreArchivo in this._objRadDocIdentity.LstNombreDocumentoAdjunto)
                            {
                                string archivo = "";
                                if (nombreArchivo.Contains(";"))
                                    archivo = nombreArchivo.Split(';')[0];
                                else
                                    archivo = nombreArchivo;
                                LogicaNegocio.RepositorioArchivos.RepositorioArchivo clsRepositorioArchivo = new RepositorioArchivos.RepositorioArchivo();
                                clsRepositorioArchivo.AsociarArchivo(archivo, Convert.ToInt32(this._objRadDocIdentity.IdSolicitante));
                            }
                        }
                    }
                    else
                    {
                        objTraffic.RecibirDocumento2(this._objRadDocIdentity.NumeroSilpa, this._objRadDocIdentity.IdSolicitante, directorio, ref listaNombresDocumentos, ref _strRuta);
                    }

                    this._objRadDocIdentity.UbicacionDocumento = _strRuta;
                    
                }
            }

            objRadicarDocDalc.RadicarDocumento(ref this._objRadDocIdentity);

            AutoridadAmbientalDalc aaDalc = new AutoridadAmbientalDalc();
            if (_objRadDocIdentity.IdAA.HasValue && _objRadDocIdentity.IdAA.Value!=0)
            {
                objAutoridadIdentity = new AutoridadAmbientalIdentity();
                objAutoridadIdentity.IdAutoridad = _objRadDocIdentity.IdAA.Value;
                //aaDalc.ObtenerAutoridadById(ref objAutoridadIdentity);
                // HAVA: 13 - dic - 2010
                // Consulta la AA sin filtro de Ventanilla Integrada.
                aaDalc.ObtenerAutoridadNoIntegradaById(ref objAutoridadIdentity);
            }
            else
            {
                objAutoridadIdentity = null;
            }


            this._objPersona = new Persona();
            _objPersona.PersonaByUserId(this._objRadDocIdentity.IdSolicitante);
            if (objAutoridadIdentity != null)
            {
                //si la autoridad no tiene radicación automática se envía un correo notificando que debe radicar
                if (objAutoridadIdentity.RadicacionAutomatica == false)
                {
                    ICorreo.Correo.EnviarRadicacionAA(_objRadDocIdentity, objAutoridadIdentity, _objPersona);
                }
            }

            return this._objRadDocIdentity.IdRadicacion;
        }

        public int ActualizarRadicacion()
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();

            objRadicarDocDalc.ActualizarRadicacionAA(ref this._objRadDocIdentity);

            return this._objRadDocIdentity.IdRadicacion;
        }


        /// <summary>
        /// Radica el documento adjunto desde una solicitud de información adicional a otra entidad
        /// </summary>
        /// <param name="idAAOrigen">identificacion de la entidad origen</param>
        /// <returns>int: identificador de la radicación </returns>
        public int RadicarDocumentoEE(int idAAOrigen, bool respuesta, int idAADestino)
        {
            //AutoridadAmbientalDalc objAADalc = new AutoridadAmbientalDalc();
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();

            /// se verifica si los documentos estan relacionados a un acto administrativo
            if (this._objRadDocIdentity.ActoAdministrativo != string.Empty
                    && this._objRadDocIdentity.ActoAdministrativo != null)
            {
                this._objRadDocIdentity.TipoDocumento = (int)TipoDocumento.Acto;
            }
            else
            { /// documento que relaciona un ofico
                this._objRadDocIdentity.TipoDocumento = (int)TipoDocumento.Oficio;
            }
            //objAADalc.ObtenerAutoridadById(ref objAutoridadIdentity);

            List<string> listaNombresDocumentos = new List<string>();

            ///Si hay documentos adjuntos por el usuario se envia correo a la AA:
            if (this._objRadDocIdentity.LstNombreDocumentoAdjunto != null)
            {
                if (this._objRadDocIdentity.LstNombreDocumentoAdjunto.Count > 0)
                {
                    // Envía el documento al folder FileTraffic determinado
                    TraficoDocumento objTraffic = new TraficoDocumento();
                    string _strRuta = string.Empty;
                    //List<string> listaNombresDocumentos = this._objRadDocIdentity.LstNombreDocumentoAdjunto;
                    listaNombresDocumentos = this._objRadDocIdentity.LstNombreDocumentoAdjunto;
                    string directorio = _objConfiguracion.DirectorioGattaca;
                    SMLog.Escribir(Severidad.Informativo, "Metodo RadicarDocumentoEE");
                    //objTraffic.RecibirDocumento(this._objRadDocIdentity.LstBteDocumentoAdjunto, this._objRadDocIdentity.LstNombreDocumentoAdjunto);
                    //objTraffic.RecibirDocumentoEE(this._objRadDocIdentity.NumeroSilpa, this._objRadDocIdentity.IdSolicitante, this._objRadDocIdentity.LstBteDocumentoAdjunto, ref listaNombresDocumentos, ref _strRuta, idAADestino);
                    objTraffic.RecibirDocumentoEE2(this._objRadDocIdentity.NumeroSilpa, this._objRadDocIdentity.IdSolicitante, directorio, ref listaNombresDocumentos, ref _strRuta, idAADestino);

                    this._objRadDocIdentity.UbicacionDocumento = _strRuta;

                }
            }

            //objRadicarDocDalc.RadicarDocumento(ref this._objRadDocIdentity);

            _objRadDocIdentity.LstNombreDocumentoAdjunto = listaNombresDocumentos;

            objRadicarDocDalc.RadicarDocumentoSolicitudEE(ref this._objRadDocIdentity, idAAOrigen, !respuesta);
            
            AutoridadAmbientalDalc aaDalc = new AutoridadAmbientalDalc();
            if (_objRadDocIdentity.IdAA.HasValue && _objRadDocIdentity.IdAA.Value != 0)
            {
                objAutoridadIdentity = new AutoridadAmbientalIdentity();
                objAutoridadIdentity.IdAutoridad = _objRadDocIdentity.IdAA.Value;

                //aaDalc.ObtenerAutoridadById(ref objAutoridadIdentity);
                aaDalc.ObtenerAutoridadNoIntegradaById(ref objAutoridadIdentity);
            }
            else
            {
                objAutoridadIdentity = null;
            }
            this._objPersona = new Persona();
            _objPersona.PersonaByUserId(this._objRadDocIdentity.IdSolicitante);
            if (objAutoridadIdentity != null)
            {
                //si la autoridad no tiene radicación automática se envía un correo notificando que debe radicar
                if (objAutoridadIdentity.RadicacionAutomatica == false)
                {
                   // ICorreo.Correo.EnviarRadicacionAA(_objRadDocIdentity, objAutoridadIdentity, _objPersona);
                }
            }
            return this._objRadDocIdentity.IdRadicacion;
        }


        /// <summary>
        /// La Funcion crea un objeto que ingresa el numero y fecha de radicacion para una solicitud dada.
        /// </summary>
        public void ActualizarDatosRadicacion()
        {
            try
            {
                RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
                objRadicarDocDalc.ActualizarRadicacionDocumento(ref this._objRadDocIdentity);
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible realizar la inserción del número de radicación: " + ex.Message);
            }
        }

        public void ActualizarEstadoRadicacion()
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            objRadicarDocDalc.ActualizarEstadoRadicacion(ref this._objRadDocIdentity);
        }

        /// <summary>
        /// Método que obtiene los datos de correspondencia generados en Silpa 
        /// mediante el identificador de la Autoridad ambiental
        /// </summary>
        /// <param name="IdAA">Int: identificador de la Autoridad </param>
        /// <returns>DataSet: conjunto de resultados</returns>
        public string ObtenerDatosRadicacion(int IdAA) 
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            DataSet dsRadicacion =  objRadicarDocDalc.ObtenerDatosRadicacionPorAA(IdAA);
            return dsRadicacion.GetXml();
        }


        /// <summary>
        /// Método que permite obtener los datos de correspondencia en Silpa
        /// mediante el id de la AA y la bandera que indica que se radica o consulta
        /// </summary>
        /// <param name="IdAA"></param>
        /// <param name="blnPermiteRadicar"></param>
        /// <returns></returns>
        public string ObtenerDatosRadicacion(int IdAA, bool blnPermiteRadicar)
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            DataSet dsRadicacion = objRadicarDocDalc.ObtenerDatosRadicacionPorAA(IdAA,blnPermiteRadicar);
            return dsRadicacion.GetXml();
        }

        /// <summary>
        /// Método que permite obtener los datos de correspondencia en Silpa
        /// mediante el id de la radicacion
        /// </summary>
        /// <param name="IdRadicacion">Identificador de la radicacion</param>
        /// <returns></returns>
        public void ObtenerRadicacion(Nullable<int> IdRadicacion, Nullable<long> NumeroSilpa)
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            this._objRadDocIdentity = objRadicarDocDalc.ObtenerDatosRadicacion(IdRadicacion, NumeroSilpa);
        }

        /// <summary>
        /// Metodo que obtiene los datos de correspondencia en Silpa por número VITAL
        /// </summary>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        public void ObtenerRadicacionNumeroVital(string p_strNumeroVital)
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            this._objRadDocIdentity = objRadicarDocDalc.ObtenerDatosRadicacionNumeroVital(p_strNumeroVital);
        }

        /// <summary>
        /// Soft - Netco - Hava
        /// Método que permite obtener los tipos de documentos asociados 
        /// de acuerdo al tipo de trámite
        /// </summary>
        /// <param name="intIdTipoProceso">Int: identificador del tipo de proceso</param>
        /// <returns>DataSet con el listado de los documentos asociados al trámite</returns>
        public DataSet ObtenerDocumentosAsociados(int intIdProceso)
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            DataSet dsResultado = objRadicarDocDalc.ObtenerListaDocumentosAsociados(intIdProceso);
            return dsResultado;
        }


        /// <summary>
        /// Metodo que asigna el valor de numero de radicacion a un objeto radicacion en particular
        /// </summary>
        /// <param name="intIdRadicacion">int: indentificador del objeto radicacion </param>
        /// <param name="strNumeroRadicacion">string: numero de radicacion asignado al documento</param>
        public bool ActualizarDatosRadicacion(int intIdRadicacion, string strNumeroRadicacion, Nullable<DateTime> dtFechaRadicacion)
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            objRadicarDocDalc.ActualizarRadicacionDocumento(intIdRadicacion, strNumeroRadicacion, dtFechaRadicacion);
            return true;
        }

        /// <summary>
        /// Recibe un listado con los nombres de los archivos retorna una lista con los bytes de los archivos
        /// </summary>
        /// <param name="lstString">Lista con el nombre de los archivos almacenados en gattaca</param>
        /// <returns>Lista de los bytes[] de los archivos en la carpeta de gattaca</returns>
        public List<Byte[]> ObtenerListaDeBytes(List<String> lstString)
        {
            List<Byte[]> _lstBytes = new List<byte[]>();

            for (int i = 0; i < lstString.Count; i++)
            {
                //hava:20-abr-10
                if (System.IO.File.Exists(lstString[i]) == true)
                {
                    _lstBytes.Add(System.IO.File.ReadAllBytes(lstString[i]));
                }
                
            }
            return _lstBytes;
        }

        /// <summary>
        /// Recibe un listado con los nombres de los archivos retorna una lista con los bytes de los archivos
        /// hava:18-abr-10
        /// </summary>
        /// <param name="lstString">Lista con el nombre de los archivos almacenados en gattaca</param>
        /// <returns>Lista de los bytes[] de los archivos en la carpeta de gattaca</returns>
        public List<Byte[]> ObtenerListaDeBytesBPM(List<String> lstString)
        {
            List<Byte[]> _lstBytes = new List<byte[]>();

            string archivo = string.Empty; 
            string directorio = _objConfiguracion.DirectorioGattaca;
            
            Byte[] bytes;

            //Valida la lista de archivos para asegurar que todos existen
            for (int i = lstString.Count - 1; i >= 0; i--)
            {
                archivo = directorio + lstString[i];
                if (!System.IO.File.Exists(archivo))
                {
                    lstString.RemoveAt(i);
                }
                /*else 
                {
                    // valida que el archivo contenga información.
                    bytes = System.IO.File.ReadAllBytes(archivo);

                    if (bytes.Length <= 0) 
                    {
                        lstString.RemoveAt(i);
                    }
                }*/
            }

            for (int i = 0; i < lstString.Count; i++)
            {
                archivo = directorio + lstString[i];

                // hava:20-abr-10
                if (System.IO.File.Exists(archivo)) 
                {
                    bytes = System.IO.File.ReadAllBytes(archivo);
                    _lstBytes.Add(bytes);
                }
            }

            return _lstBytes;
        }



        public void ObtenerArchivosBPM(Int64 int64ProcessInstances)
        {
            FormularioDalc _objFormulario = new FormularioDalc();
            DataTable _dtResultado = new DataTable();
            List<String> _lstString = new List<String>();
            List<Byte[]> _lstByte = new List<Byte[]>();

            int i = 0;

            _dtResultado = _objFormulario.ConsultarListadoCamposMultiRegistrosyPrincipal(int64ProcessInstances).Tables[0];

            if (_dtResultado != null)
            {
                if (_dtResultado.Rows.Count > 0)
                {
                    foreach(DataRow drTemp in _dtResultado.Rows)
                    {
                        if (this.EsEtiquetaRadicable(drTemp["TEXTO"].ToString()) == true && drTemp["DATO"].ToString() != string.Empty)
                        {
                            _lstString.Add(drTemp["DATO"].ToString() + ";" + drTemp["TYPE"].ToString());
                            SMLog.Escribir(Severidad.Informativo,"Lista Archivo --> " + drTemp["DATO"].ToString());
                            i++;
                        }
                    }

                    // No adjuntar Documento en el correo
                    //JM - 24/04/2013 -> NO atachar archivos porque son muy pesados y sale errorde OutOfMemoryException
                    //_lstByte = ObtenerListaDeBytesBPM(_lstString);
                    SetAdjuntos(_lstString, _lstByte);
                }
            }
        }

        /// <summary>
        /// Recibe un listado con los nombres de los archivos retorna una lista con los bytes de los archivos
        /// </summary>
        /// <param name="lstString">Lista con el nombre de los archivos almacenados en gattaca</param>
        /// <returns>Lista de los bytes[] de los archivos en la carpeta de gattaca</returns>
        public void EliminarFilesBPM(List<String> lstString)
        {
            List<Byte[]> _lstBytes = new List<byte[]>();

            for (int i = 0; i < lstString.Count; i++)
            {
                System.IO.File.Delete(_objConfiguracion.DirectorioGattaca + lstString[i]);
            }
        }


        /// <summary>
        /// Recibe un listado con los nombres de los archivos retorna una lista con los bytes de los archivos
        /// </summary>
        /// <param name="lstString">Lista con el nombre de los archivos almacenados en gattaca</param>
        /// <returns>Lista de los bytes[] de los archivos en la carpeta de gattaca</returns>
        public void GuardarArchivo(List<String> lstString)
        {
            List<Byte[]> _lstBytes = new List<byte[]>();

            for (int i = 0; i < lstString.Count; i++)
            {
                System.IO.File.Delete(_objConfiguracion.DirectorioGattaca + lstString[i]);
            }
        }


        /// <summary>
        /// Obtiene la ubicacion del fus generado en el solicitud del tramite mediante el numero vital
        /// </summary>
        /// <param name="numeroVital"></param>
        /// <returns>string: ruta del fus en .rtf</returns>
        public string ObtenerRutaFus(string numeroVital) 
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            
            SMLog.Escribir(Severidad.Informativo,_objConfiguracion.FileTraffic.ToString());
            
            return _objConfiguracion.FileTraffic+"_"+dalc.ObtenerProcessInstance(numeroVital)+".rft";
        }

        /// <summary>
        /// 11-08-2010 - aegb: CU Emitir Documento Manual
        /// Obtiene el id del process instance de acuerdo al numero vital
        /// </summary>
        /// <param name="numeroVital"></param>
        /// <returns>string: ruta del fus en .rtf</returns>
        public string ObtenerProcessInstance(string numeroVital)
        {
           RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
           return dalc.ObtenerProcessInstance(numeroVital);
        }


        /// <summary>
        /// hava:
        /// 20-dic-2010
        /// Lista los documentos asociados por cada radicación
        /// </summary>
        /// <param name="idRadicacion">int: identificador de la radicación</param>
        /// <returns></returns>
        public string ObtenerDocumentosRadicacion(long idRadicacion)
        {
            List<SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity> _lstDocumentos;
            
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string pathDocumento = dalc.ObtenerPathDocumentos(idRadicacion);
            bool incluirFus = dalc.IncluirFus(idRadicacion);
            string rutaFus;
            string[] ListaArchivos = null;

            if (pathDocumento != string.Empty)
            {
                if (System.IO.Directory.Exists(pathDocumento))
                {
                    ListaArchivos = System.IO.Directory.GetFiles(pathDocumento);
                }
            }

            _lstDocumentos = new List<SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity>();

            if (incluirFus == true)
            {
                rutaFus = dalc.ObtenerRutaFUS(idRadicacion);
            }
            else
            {
                rutaFus = string.Empty;
            }


            if (rutaFus != string.Empty)
            {
                if (System.IO.File.Exists(rutaFus))
                {
                    SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity doc = new SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity();
                    doc.NombreArchivo = System.IO.Path.GetFileName(rutaFus);
                    doc.Ubicacion = rutaFus;
                    _lstDocumentos.Add(doc);
                }
            }

            if (ListaArchivos != null)
            {
                if (ListaArchivos.Length > 0)
                {
                    foreach (string str in ListaArchivos)
                    {
                        SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity doc = new SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity();
                        doc.NombreArchivo = System.IO.Path.GetFileName(str);
                        doc.Ubicacion = str;
                        _lstDocumentos.Add(doc);
                    }
                }
            }

            string lstListaDocumentos = string.Empty;
            // hava: 20 dic 2010
            if (_lstDocumentos != null)
            {
                if (_lstDocumentos.Count > 0)
                {                    

                    foreach(SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity detDoc in _lstDocumentos)
                    {                       
                        lstListaDocumentos = lstListaDocumentos + ";" + detDoc.NombreArchivo;
                    }

                }
            }

            return lstListaDocumentos;
        }

        public string ObtenerDocumentosNUR(string NUR)
        {
            List<SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity> _lstDocumentos;

            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string pathDocumento = dalc.ObtenerPathDocumentosNUR(NUR);
            string[] ListaArchivos = null;

            if (pathDocumento != string.Empty)
            {
                if (System.IO.Directory.Exists(pathDocumento))
                {
                    ListaArchivos = System.IO.Directory.GetFiles(pathDocumento);
                }
            }

            _lstDocumentos = new List<SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity>();

            if (ListaArchivos != null)
            {
                if (ListaArchivos.Length > 0)
                {
                    foreach (string str in ListaArchivos)
                    {
                        SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity doc = new SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity();
                        doc.NombreArchivo = System.IO.Path.GetFileName(str);
                        doc.Ubicacion = str;
                        _lstDocumentos.Add(doc);
                    }
                }
            }

            string lstListaDocumentos = string.Empty;
            // hava: 20 dic 2010
            if (_lstDocumentos != null)
            {
                if (_lstDocumentos.Count > 0)
                {

                    foreach (SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity detDoc in _lstDocumentos)
                    {
                        lstListaDocumentos = lstListaDocumentos + ";" + detDoc.NombreArchivo;
                    }

                }
            }

            return lstListaDocumentos;
        }


        /// <summary>
        /// Obtener el path de los documentos de la radicación pertenecientes al número vital especificado
        /// </summary>
        /// <param name="p_strNumeroVital"></param>
        /// <returns>string con el path donde se ubican los documentos</returns>
        public string ObtenerPathDocumentosNumeroVital(string p_strNumeroVital)
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            return objRadicarDocDalc.ObtenerPathDocumentosNumeroVital(p_strNumeroVital);
        }

        /// <summary>
        /// Obtener todos los path de los documentos de la radicación pertenecientes al número vital especificado
        /// </summary>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        /// <returns>List con todos los path donde se ubican los documentos</returns>
        public List<string> ObtenerTodosPathDocumentosNumeroVital(string p_strNumeroVital)
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            return objRadicarDocDalc.ObtenerTodosPathDocumentosNumeroVital(p_strNumeroVital);
        }


        public void ActualizarRutaRadicacion(int radicacionId,string ruta)
        {
           RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
           objRadicarDocDalc.ActualizarRadicacionRuta(radicacionId, ruta);
        }

        /// <summary>
        /// hava:
        /// 20-dic-2010
        /// Lista las url de los documentos asociados por cada radicación
        /// </summary>
        /// <param name="idRadicacion">int: identificador de la radicación</param>
        /// <returns></returns>
        public string ObtenerURLDocumentosRadicacion(long idRadicacion)
        {
            List<SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity> _lstDocumentos;

            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string pathDocumento = dalc.ObtenerPathDocumentos(idRadicacion);
            bool incluirFus = dalc.IncluirFus(idRadicacion);
            string rutaFus;
            string[] ListaArchivos = null;

            if (pathDocumento != string.Empty)
            {
                if (System.IO.Directory.Exists(pathDocumento))
                {
                    ListaArchivos = System.IO.Directory.GetFiles(pathDocumento);
                }
            }

            _lstDocumentos = new List<SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity>();

            if (incluirFus == true)
            {
                rutaFus = dalc.ObtenerRutaFUS(idRadicacion);
            }
            else
            {
                rutaFus = string.Empty;
            }


            if (rutaFus != string.Empty)
            {
                if (System.IO.File.Exists(rutaFus))
                {
                    SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity doc = new SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity();
                    doc.NombreArchivo = System.IO.Path.GetFileName(rutaFus);
                    doc.Ubicacion = rutaFus;
                    _lstDocumentos.Add(doc);
                }
            }

            if (ListaArchivos != null)
            {
                if (ListaArchivos.Length > 0)
                {
                    foreach (string str in ListaArchivos)
                    {
                        SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity doc = new SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity();
                        doc.NombreArchivo = System.IO.Path.GetFileName(str);
                        doc.Ubicacion = str;
                        _lstDocumentos.Add(doc);
                    }
                }
            }

            string lstListaDocumentos = string.Empty;

            string urlFileTraffic = ConfigurationManager.AppSettings["URL_FILE_TRAFFIC"].ToString();
            string fileTraffic = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString();

            // hava: 20 dic 2010
            if (_lstDocumentos != null)
            {
                if (_lstDocumentos.Count > 0)
                {
                    //grdVerDocumentos.DataSource = this._lstDocumentos;
                    //grdVerDocumentos.DataBind();

                    //foreach (SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity detDoc in _lstDocumentos)
                    //{
                    //    detDoc.Ubicacion = System.IO.Path.GetDirectoryName(detDoc.Ubicacion);
                    //    detDoc.Ubicacion = detDoc.Ubicacion.Replace(filetraffic, urlUbicacion);
                    //    lstListaDocumentos = lstListaDocumentos + ";" + detDoc.Ubicacion + @"\" + detDoc.NombreArchivo;
                    //}


                    foreach (SILPA.AccesoDatos.Publicacion.DetalleDocumentoIdentity detDoc in _lstDocumentos)
                    {
                        detDoc.Ubicacion = System.IO.Path.GetDirectoryName(detDoc.Ubicacion) + @"\";
                        detDoc.Ubicacion = detDoc.Ubicacion.Remove(0, fileTraffic.Length);
                        detDoc.Ubicacion = detDoc.Ubicacion.Replace(@"\", @"/");
                        detDoc.Ubicacion = urlFileTraffic + detDoc.Ubicacion;
                        //detDoc.Ubicacion = urlFileTraffic + detDoc.Ubicacion;
                        lstListaDocumentos = lstListaDocumentos + ";" + detDoc.Ubicacion + @"/" + detDoc.NombreArchivo;
                    }

                }
            }

            return lstListaDocumentos;
        }

        #endregion

        public Byte[] ObtenerDocumentoRadicacion(long idRadicacion, string nombreArchivo)
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string pathDocumento = dalc.ObtenerPathDocumentos(idRadicacion);            
            if (File.Exists(pathDocumento+nombreArchivo))       
            {
                Byte[] archivo = File.ReadAllBytes(pathDocumento + nombreArchivo);
                return archivo;
            }
            else
            {
                string urlFileTraffic = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString();            
                if (File.Exists(urlFileTraffic + nombreArchivo))
                {
                    Byte[] archivo = File.ReadAllBytes(urlFileTraffic + nombreArchivo);
                    return archivo;
                }
                else
                    return null;
            }

           
        }
        public string ObtenerPathRadicacion(long idRadicacion, string nombreArchivo)
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string pathDocumento = dalc.ObtenerPathDocumentos(idRadicacion);
            if (File.Exists(pathDocumento + nombreArchivo))
            {
                return pathDocumento + nombreArchivo;
            }
            else
            {
                string urlFileTraffic = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString();
                if (File.Exists(urlFileTraffic + nombreArchivo))
                {
                    return urlFileTraffic + nombreArchivo;
                }
                else
                    return null;
            }


        }

        public string ObtenerPathNUR(string strNUR, string nombreArchivo)
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string pathDocumento = dalc.ObtenerPathDocumentosNUR(strNUR);
            if (File.Exists(pathDocumento + nombreArchivo))
            {
                return pathDocumento + nombreArchivo;
            }
            else
            {
                string urlFileTraffic = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString();
                if (File.Exists(urlFileTraffic + nombreArchivo))
                {
                    return urlFileTraffic + nombreArchivo;
                }
                else
                    return null;
            }


        }

        /// <summary>
        /// Hava: 12-may-10
        /// Obtiene la ruta del fus que fué guardado al crear la solicitud
        /// </summary>
        /// <param name="int_id_radicacion">long: identificador del proceso al cual pertenece el fus</param>
        /// <returns>string: path del fus generado</returns>
        public string ObtenerPathFus(long int_id_radicacion)
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            return dalc.ObtenerPathFus(int_id_radicacion);
        }

        /// <summary>
        /// Obtiene el path de documentos de la radicacion
        /// </summary>
        /// <param name="int_id_radicacion">int con el identificador de los documentos</param>
        /// <returns>string con el path</returns>
        public string ObtenerPathDocumentos(long int_id_radicacion)
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string path = dalc.ObtenerPathDocumentos(int_id_radicacion);

            path = path.Replace("F:\\VITALCARS\\SILPA_PRE\\SILPA.WebHost", "\\\\MAVDTPRU");

            return path;
        }


        public Byte[] ObtenerArchivo(string nombreArchivo)
        {
            string pathDocumento = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString();
            if (File.Exists(pathDocumento+nombreArchivo))
            {
                Byte[] archivo = File.ReadAllBytes(pathDocumento + nombreArchivo);
                return archivo;
            }
            else
            {            
                return null;
            }


        }

        public string EnviarDocumentoRadicacion(long idRadicacion, string nombreArchivo, byte[] bytesArchivo)
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string pathDocumento = dalc.ObtenerPathDocumentos(idRadicacion);

            if (string.IsNullOrEmpty(pathDocumento) || !Directory.Exists(pathDocumento))
            {
                RadicacionDocumento objRadicacion = new RadicacionDocumento();
                objRadicacion.ObtenerRadicacion((int)idRadicacion, null);
                SILPA.Comun.TraficoDocumento objTrafico = new SILPA.Comun.TraficoDocumento();
                pathDocumento = objTrafico.CrearDirectorio(objTrafico.FileTraffic, objRadicacion._objRadDocIdentity.NumeroSilpa, objRadicacion._objRadDocIdentity.IdSolicitante);
                dalc.ActualizarRadicacionRuta((int)idRadicacion, pathDocumento);
            }

            if (!File.Exists(pathDocumento + nombreArchivo))
            {
                try
                {
                    // verificamos la existencia del directorio
                    if (!Directory.Exists(pathDocumento))
                    {
                        Directory.CreateDirectory(pathDocumento);
                    }
                    File.Create(pathDocumento + nombreArchivo).Close();
                    //File.WriteAllBytes(pathDocumento + nombreArchivo, archivoByte);

                    using (FileStream fs = new FileStream(pathDocumento + nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                    {
                        fs.Write(bytesArchivo, 0, bytesArchivo.Length);
                    }
                }
                catch (Exception ex)
                {
                    return "Ocurrió un error al intentar guardar el archivo.";
                    SMLog.Escribir(Severidad.Critico,"EnviarDocumentoRadicacion -- Error al crear el archivo "+pathDocumento + nombreArchivo+" ---- "+ex.ToString());
                }
                return "";
            }
            else
                return "ya existe un archivo con el mismo nombre.";
        }
        public string EnviarDocumentoRadicacion(long idRadicacion, string nombreArchivo, out string rutaArchivos)
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
            string pathDocumento = dalc.ObtenerPathDocumentos(idRadicacion);

            if (string.IsNullOrEmpty(pathDocumento) || !Directory.Exists(pathDocumento))
            {
                RadicacionDocumento objRadicacion = new RadicacionDocumento();
                objRadicacion.ObtenerRadicacion((int)idRadicacion, null);
                SILPA.Comun.TraficoDocumento objTrafico = new SILPA.Comun.TraficoDocumento();
                pathDocumento = objTrafico.CrearDirectorio(objTrafico.FileTraffic, objRadicacion._objRadDocIdentity.NumeroSilpa, objRadicacion._objRadDocIdentity.IdSolicitante);
                dalc.ActualizarRadicacionRuta((int)idRadicacion, pathDocumento);
            }
            rutaArchivos = pathDocumento;

            if (!File.Exists(pathDocumento + nombreArchivo))
            {
                return "No existe el archivo";
            }
            else
                return "ya existe un archivo con el mismo nombre.";
        }

        public string EnviarDocumentoNUR(string NUR, string nombreArchivo, string strCarpetaNURs, out string rutaArchivo)
        {
            RadicacionDocumentoDalc dalc = new RadicacionDocumentoDalc();
             SILPA.Comun.TraficoDocumento objTrafico = new SILPA.Comun.TraficoDocumento();
             string pathDocumento = objTrafico.CrearDirectorioNUR(objTrafico.FileTraffic,strCarpetaNURs, NUR);
             dalc.ActualizarRadicacionNUR(NUR, pathDocumento);
             rutaArchivo = pathDocumento;

             if (!File.Exists(pathDocumento + nombreArchivo))
             {
                 return "No existe el archivo";
             }
             else
                 return "ya existe un archivo con el mismo nombre.";
        }

        public void ActualizarDatosRadicacionPago(int intProcessInstace, string Radicado, DateTime dateTime)
        {
            RadicacionDocumentoDalc objRadicarDocDalc = new RadicacionDocumentoDalc();
            objRadicarDocDalc.ActualizarRadicacionPago(intProcessInstace, Radicado, dateTime);            
        }
    }
}
