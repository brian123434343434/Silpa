using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;


namespace SILPA.Comun
{
    /// <summary>
    /// Calse que contiene os elementos de configuracion de la aplicación
    /// Pueden ser los datos configurados en el web.config
    /// </summary>
    [Serializable]
    public class Configuracion
    {
        /// <summary>
        /// constructor que carga los parametros desde el web.config
        /// </summary>
        public Configuracion()
        {
            /// Conexiones SILPA - SILA
            this.SilpaCnx = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ToString();
            this.SilaCnx = ConfigurationManager.ConnectionStrings["SILAMCConnectionString"].ToString();
            this.SilaAnbogwCnx = ConfigurationManager.ConnectionStrings["SilaAnbogwConnectionString"].ToString();

            /// conexiones BPM 
            this.FormBuilderCnx = ConfigurationManager.ConnectionStrings["eFormBuilderConnectionString"].ToString();
            this.SecurityCnx = ConfigurationManager.ConnectionStrings["eSecurityConnectionString"].ToString();
            this.WorkFlowCnx = ConfigurationManager.ConnectionStrings["eWorkFlowConnectionString"].ToString();

            /// ubicacion en el servior de aplicacione de los archivos 
            /// que se adjuntan por parte del usuario solicitante
            this.FileTraffic = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString();

            ///Correo:
            this.Seguridad_Correo = Convert.ToBoolean(ConfigurationManager.AppSettings["Seguridad_Correo"].ToString());
            this.DefaultCredentials = ConfigurationManager.AppSettings["DefaultCredentials"].ToString();
            this.ServidorCorreo = ConfigurationManager.AppSettings["str_servidor_correo"].ToString();
            this.UsuarioCorreo = ConfigurationManager.AppSettings["str_usuario_correo"].ToString();
            this.PassWordCorreo = ConfigurationManager.AppSettings["str_clave_correo"].ToString();
            this.SenderCorreo = ConfigurationManager.AppSettings["str_sender_correo"].ToString();
            this.PuertoCorreo = ConfigurationManager.AppSettings["int_puerto_correo"].ToString();
            this.CuentaControl = ConfigurationManager.AppSettings["str_correo_control"].ToString();
            this.IdPaisPredeterminado = int.Parse(ConfigurationManager.AppSettings["int_pais_predeterminado"].ToString());
            this.BDSilaMC = ConfigurationManager.AppSettings["str_nombrebd_silamc"].ToString();

            /// servicios:
            //this._servicios = 
            //CargarLista(ConfigurationManager.AppSettings["str_correo_control"].ToString());

            // Repositorio de archivos del BPM
            this._directorioGattaca = ConfigurationManager.AppSettings["str_path_gattaca_file"].ToString();
            this.RepositorioUsuario = ConfigurationManager.AppSettings["str_path_repoUsuario"].ToString();
            this.FormularioSolicitudLicencia = Convert.ToInt32(ConfigurationManager.AppSettings["FORMULARIO_SOLICITUD_LICENCIA"]);

            this._nombreQuejas = ConfigurationManager.AppSettings["str_archivos_queja"].ToString();
            //Se guardan los archivos al crear un proceso en Form Builder            

            this._idUserFinaliza = Convert.ToInt32(ConfigurationManager.AppSettings["IdUserFinaliza"].ToString());
            this._userFinaliza = ConfigurationManager.AppSettings["userFinaliza"].ToString();

            this._idUserComunicacionEE = Convert.ToInt32(ConfigurationManager.AppSettings["IdUserComunicacionEE"].ToString());

            //hava: 08-oct-10: ubicación del archivo que determina las etiquetas radicables.
            this._archivoEtiquetaRadicable = ConfigurationManager.AppSettings["EtiquetaRadicable"].ToString();

            /// proxy 
            //this._servidorProxy = ConfigurationManager.AppSettings["str_proxy_server"].ToString();
            //this._puertoProxy   = int.Parse(ConfigurationManager.AppSettings["int_proxy_puerto"].ToString());
        }


        /// <summary>
        /// Método que carga los url de los servicios disponibles.
        /// </summary>
        private void CargarListaServicios()
        {
            string pathListaServicios = ConfigurationManager.AppSettings["ListaServicios"].ToString();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathListaServicios);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Servicio");

            //for(int i = 0; i<xemell.Count; i++) 
            foreach (XmlNode xmlNode in nodeList)
            {
                this._servicios.Add(xmlNode.Attributes["url"].Value);
            }
        }
        

        #region variables de conexión


        

        /// <summary>
        /// /// Conexión de la base de datos SILPA
        /// </summary>
        private string _silpaCnx;
        public string SilpaCnx { get { return this._silpaCnx; } set { this._silpaCnx = value; } }

        /// <summary>
        /// Conexión de la base de datos SILA
        /// </summary>
        private string _silaCnx;
        public string SilaCnx { get { return this._silaCnx; } set { this._silaCnx = value; } }


        /// <summary>
        /// Conexión de la base de datos ANBOGWPSQL04 para consulta publica
        /// </summary>
        private string _silaAnbogwCnx;
        /// <summary>
        /// Conexión de la base de datos ANBOGWPSQL04 para consulta publica
        /// </summary>
        public string SilaAnbogwCnx { get { return this._silaAnbogwCnx; } set { this._silaAnbogwCnx = value; } }


        /// <summary>
        /// Conexión de la base de datos FormBuilder
        /// </summary>
        private string _formBuilderCnx;
        public string FormBuilderCnx { get { return _formBuilderCnx; } set { _formBuilderCnx = value; } }

        /// <summary>
        /// Conexión de la base de datos Security
        /// </summary>
        private string _securityCnx;
        public string SecurityCnx { get { return _securityCnx; } set { _securityCnx = value; } }

        /// <summary>
        /// Conexión de la base de datos WorkFlow
        /// </summary>
        private string _workFlowCnx;
        public string WorkFlowCnx { get { return _workFlowCnx; } set { _workFlowCnx = value; } }

        #endregion

        #region variables del correo

        private string _senderCorreo;
        public string SenderCorreo { get { return this._senderCorreo; } set { this._senderCorreo = value; } }

        private bool _seguridad_Correo;
        public bool Seguridad_Correo { get { return this._seguridad_Correo; } set { this._seguridad_Correo = value; } }

        private string _defaultCredentials;
        public string DefaultCredentials { get { return this._defaultCredentials; } set { this._defaultCredentials = value; } }

        private string _servidorCorreo;
        public string ServidorCorreo { get { return this._servidorCorreo; } set { this._servidorCorreo = value; } }

        private string _usuarioCorreo;
        public string UsuarioCorreo { get { return this._usuarioCorreo; } set { this._usuarioCorreo = value; } }

        private string _puertoCorreo;
        public string PuertoCorreo { get { return this._puertoCorreo; } set { this._puertoCorreo = value; } }

        /// <summary>
        /// password correo
        /// </summary>
        private string _passWordCorreo;
        public string PassWordCorreo { get { return this._passWordCorreo; } set { this._passWordCorreo = value; } }

        private string _cuentaControl;

        public string CuentaControl
        {
            get { return _cuentaControl; }
            set { _cuentaControl = value; }
        }

        private string _bdSilaMC;
        public string BDSilaMC
        {
            get { return _bdSilaMC; }
            set { _bdSilaMC = value; }
        }


        private int _idUserFinaliza;
        public int IdUserFinaliza
        {
            get { return _idUserFinaliza; }
            set { _idUserFinaliza = value; }
        }


        private string _userFinaliza;
        public string UserFinaliza
        {
            get { return _userFinaliza; }
            set { _userFinaliza = value; }
        }
        

        #endregion

        #region Servicios ... 

        /// <summary>
        /// Lista contiene las url de los servicios disponibles 
        /// </summary>
        private List<string> _servicios;
        public List<string> Servicios
        {
            get { return _servicios; }
            set { _servicios = value; }
        }

        /// <summary>
        /// Nombre de la carpeta en la cual se guardarán los archivos adjuntos a las quejas 
        /// </summary>
        private string _nombreQuejas;
        public string NombreQuejas
        {
            get { return _nombreQuejas; }
            set { _nombreQuejas = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _urlFileTraffic;
        public string UrlFileTraffic
        {
            get { return _urlFileTraffic; }
            set { _urlFileTraffic = value; }
        }
        #endregion

        #region variables de proxy
        /// <summary>
        /// servidor proxy
        /// </summary>
        private string _servidorProxy;
        public string ServidorProxy { 
            get { return this._servidorProxy; } 
            set { this._servidorProxy = value; } 
        }

        /// <summary>
        /// usuario proxy
        /// </summary>
        private string _usuarioProxy;
        public string UsuarioProxy { 
            get { return this._usuarioProxy; } 
            set { this._usuarioProxy = value; }
        }

        /// <summary>
        /// password proxy
        /// </summary>
        private string _passProxy;
        public string PassProxy { 
            get { return this._passProxy; } 
            set { this._passProxy = value; }
        }


        private int _puertoProxy;
        public int PuertoProxy
        {
            get{ return this._puertoProxy;}
            set{ this._puertoProxy = value;}
        }

        private int _idUserComunicacionEE;
        public int IdUserComunicacionEE
        {
            get { return this._idUserComunicacionEE; }
            set { this._idUserComunicacionEE = value; }
        }

        #endregion

        #region Otras ...
        /// <summary>
        /// ubicación de los archivos 
        /// en el servidor, adjuntos por el solicitante
        /// </summary>
        private string _fileTraffic;
        public string FileTraffic { get { return _fileTraffic; } set { _fileTraffic = value; } }

        /// <summary>
        /// País predeterminado (Colombia)
        /// </summary>
        private int _idPaisPredeterminado;
        public int IdPaisPredeterminado { get { return this._idPaisPredeterminado; } set { this._idPaisPredeterminado = value; } }

        /// <summary>
        /// Directorio donde se almacenan los archivos ingresados por los formularios del BPM
        /// </summary>
        private string _directorioGattaca;
        public string DirectorioGattaca { get { return _directorioGattaca; } set { _directorioGattaca = value; } }

        private string _aplicaSeguridad;

	    private string _nombreRole;

        private string _roleAutenticador;

	    private string _nombreProveedor;

        private string _mensaje401;

        private string _archivoEtiquetaRadicable;
        public string ArchivoEtiquetaRadicable { get { return _archivoEtiquetaRadicable; } set { _archivoEtiquetaRadicable = value; } }

        public string RepositorioUsuario { get; set; }

        public int FormularioSolicitudLicencia { get; set; }

        #endregion
    }
}
