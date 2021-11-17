using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Notificacion;
using SILPA.AccesoDatos.DAA;
using SILPA.AccesoDatos.Generico;
using System.IO;
using SILPA.LogicaNegocio.Formularios;
using SILPA.Servicios.Generico.RadicarDocumento;
using SoftManagement.Log;
using System.Threading;
namespace SILPA.Servicios.ImpresionFUS
{
    public class ImpresionFUSFachada
    {
        /// <summary>
        /// Constructor inicia la variable necesaria para iniciar el hilo 
        /// exterior de la operación
        /// </summary>
        public ImpresionFUSFachada()
        {
            //_iniciado = true; 
        }


        /// <summary>
        /// Variable necesaria para el inicio del hilo externo
        /// </summary>
        [ObsoleteAttribute("Metodo Obsoleto y reemplazado por la utilizacion de Pooltrhead en el proceso")]
        static bool _iniciado;

        /// <summary>
        /// Objeto representativo del FUS Dalc
        /// </summary>
        SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus _fus;

        /// <summary>
        /// Objeto representativo de la entidad del FUS
        /// </summary>
        List<SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus> _listFusEnyity;

        /// <summary>
        /// Cola que contendrá los objetos pendientes pormprocesar.
        /// </summary>
        private static Queue<Contenido> _cola = new Queue<Contenido>();


        /// <summary>
        /// Metodo estatico que agrega objetos de proceso a la cola
        /// </summary>
        /// <param name="idProceso"></param>
        [ObsoleteAttribute("Metodo Obsoleto y reemplazado por la utilizacion de Pooltrhead en el proceso")]
        public static void AgregarImpresion(int idProceso)
        {
            Contenido con = new Contenido();
            con._proceso = idProceso;
            _cola.Enqueue(con);
        }

        /// <summary>
        /// Start del Hilo externo
        /// </summary>
        [ObsoleteAttribute("Metodo Obsoleto y reemplazado por la utilizacion de Pooltrhead en el proceso")]
        public void IniciarProceso()
        {
            int espera;
            int esperaCorto;
            espera = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings.Get("TiempoEspera").ToString());
            esperaCorto = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings.Get("Espera").ToString());
            Contenido contenido = new Contenido();

            while (_iniciado == true)
            {
                if (_cola.Count != 0)
                {
                    contenido = _cola.Dequeue();
                    try
                    {
                        mensaje(contenido._proceso);
                        System.Threading.Thread.Sleep(esperaCorto);
                    }
                    catch (Exception ex)
                    {
                        //Falla el proceso por lo tanto el objeto se regresa
                        //a la cola y se realiza una espera larga para ello.
                        _cola.Enqueue(contenido);
                        System.Threading.Thread.Sleep(espera);
                    }

                }
                else
                {
                    System.Threading.Thread.Sleep(espera);
                }
            }
        }

        /// <summary>
        /// Metodo de imprimir en carpeta del servidor contenido del fus
        /// </summary>
        /// <param name="proceso"></param>
        [ObsoleteAttribute("Metodo Obsoleto y reemplazado por la utilizacion de Pooltrhead en el proceso")]
        private void mensaje(int proceso)
        {

            
            List<SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus> listFusEnyity;

            string direccion = System.Configuration.ConfigurationSettings.AppSettings.Get("DireccionFus").ToString();

            string linea;
            SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus _fus = new SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus();
            listFusEnyity = _fus.CrearArchivo(proceso);
            using (System.IO.StreamWriter arc = new System.IO.StreamWriter(direccion + "_" + proceso.ToString() + ".rtf", true))
            {
                foreach (SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus fus in listFusEnyity)
                {
                    linea = fus.strCampo.ToUpper().ToString() + ":    " + fus.strValor;
                    arc.WriteLine(linea);
                    linea = "";
                }
            }
        }
        /// <summary>
        /// metodo delegado para la utilizacion de la funcionalidad en el pooltrhead
        /// </summary>
        /// <param name="proceso">variable de tipo object que contiene el Id del proceso a imprimir</param>
        public static void Mensaje(object proceso)
        {

            List<SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus> listFusEnyity;
            //string direccion = System.Configuration.ConfigurationSettings.AppSettings.Get("DireccionFus").ToString();
            string direccion = System.Configuration.ConfigurationManager.AppSettings.Get("DireccionFus").ToString();

            string linea;
            SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus _fus = new SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus();
            SILPA.LogicaNegocio.Generico.Persona _per = new SILPA.LogicaNegocio.Generico.Persona();
            SILPA.AccesoDatos.Generico.PersonaIdentity _persona = new PersonaIdentity();
            SILPA.AccesoDatos.Generico.DireccionPersonaIdentity _direccion = new DireccionPersonaIdentity();
            string numeroVital;

            listFusEnyity = _fus.CrearArchivo((int)proceso);
            using (System.IO.StreamWriter arc = new System.IO.StreamWriter(direccion + "_" + proceso.ToString() + ".rtf", true, Encoding.UTF8))
            {
                //Acá deben ir la cosas de encabezado del archivo. Nombre del Documento del Fus.
                linea = "----------------------------------------------------------------------";
                arc.WriteLine(linea);
                linea = "Nombre del formulario: " + _fus.NombreFormulario((int)proceso);
                arc.WriteLine(linea);
                string[] lineasVital = NumeroVital((int)proceso).Replace("<br />", "/").Split('/');
                if (lineasVital.Length == 2)
                {
                    linea = "El Numero Vital de la Operacion es : " + lineasVital[0];
                    numeroVital=lineasVital[0];
                    arc.WriteLine(linea);
                    linea = lineasVital[1];
                    arc.WriteLine(linea);
                }
                else
                {
                    numeroVital=NumeroVital((int)proceso);                    
                    arc.WriteLine(linea);
                }
                linea= "----------------------------------------------------------------------";
                arc.WriteLine(linea);

                //Ahora deben aparecer los datos personales de quien diligencio el FUS.
                _per.ObternerPersonaByProcessInstace((int)proceso);
                _persona = _per.Identity;
                _direccion = _per.IdentityDir;

                arc.Write(CadenaUsuario(_persona, _direccion));

                foreach (SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus fus in listFusEnyity)
                {
                    linea = fus.strCampo.ToUpper().ToString() + ":    " + fus.strValor;
                    arc.WriteLine(linea);
                    linea = "";
                }
            }
        

        }

        public static void CrearFusRuta(int proceso,string ruta)
        {

            List<SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus> listFusEnyity;
            //string direccion = System.Configuration.ConfigurationSettings.AppSettings.Get("DireccionFus").ToString();
            string direccion = ruta;
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string linea;
            SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus _fus = new SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus();
            SILPA.LogicaNegocio.Generico.Persona _per = new SILPA.LogicaNegocio.Generico.Persona();
            SILPA.AccesoDatos.Generico.PersonaIdentity _persona = new PersonaIdentity();
            SILPA.AccesoDatos.Generico.DireccionPersonaIdentity _direccion = new DireccionPersonaIdentity();
            string numeroVital;

            listFusEnyity = _fus.CrearArchivo((int)proceso);
            string nombreArchivo =ValidarExisteArchivo(direccion,proceso.ToString(),".rtf");
            try
            {
                using (System.IO.StreamWriter arc = new System.IO.StreamWriter(direccion + nombreArchivo, true, Encoding.UTF8))
                {
                    //Acá deben ir la cosas de encabezado del archivo. Nombre del Documento del Fus.
                    linea = "----------------------------------------------------------------------";
                    arc.WriteLine(linea);
                    linea = "Nombre del formulario: " + _fus.NombreFormulario((int)proceso);
                    arc.WriteLine(linea);
                    string[] lineasVital = NumeroVital((int)proceso).Replace("<br />", "/").Split('/');
                    if (lineasVital.Length == 2)
                    {
                        linea = "El Numero Vital de la Operacion es : " + lineasVital[0];
                        numeroVital = lineasVital[0];
                        arc.WriteLine(linea);
                        linea = lineasVital[1];
                        arc.WriteLine(linea);
                    }
                    else
                    {
                        numeroVital = NumeroVital((int)proceso);
                        arc.WriteLine(linea);
                    }
                    linea = "----------------------------------------------------------------------";
                    arc.WriteLine(linea);

                    //Ahora deben aparecer los datos personales de quien diligencio el FUS.
                    _per.ObternerPersonaByProcessInstace((int)proceso);
                    _persona = _per.Identity;
                    _direccion = _per.IdentityDir;

                    arc.Write(CadenaUsuario(_persona, _direccion));

                    foreach (SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus fus in listFusEnyity)
                    {
                        linea = fus.strCampo.ToUpper().ToString() + ":    " + fus.strValor;
                        arc.WriteLine(linea);
                        linea = "";
                    }
                }
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "CrearFusRuta -- proceso -- " + proceso.ToString() + ex.ToString());
            }


        }

        private static string ValidarExisteArchivo(string file,string archivo, string extension)
        {
            int i = 0;
            if (File.Exists(file+archivo+extension))
            {
                try
                {
                    File.Delete(file + archivo + extension);
                }
                catch(Exception ex)
                {
                    SMLog.Escribir(Severidad.Critico, "ImpresionFUSFachada + ValidarExisteArchivo: No se pudo Eliminar el archivo. " + file + archivo + extension+"  -- "+ ex.ToString());
                }
            }
            return archivo +extension;
        }


        /// <summary>
        /// hava:09-oct-2010
        /// </summary>
        /// <param name="processInstance">int: identificador del processInstance</param>
        /// <param name="formInstance">int: identificador formInstance</param>        

        public static void FusActividadRadicable(object processInstance, object formInstance, ref string ruta)
        {

            List<SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus> listFusEnyity;
            //string direccion = System.Configuration.ConfigurationSettings.AppSettings.Get("DireccionFus").ToString();
            string direccion = System.Configuration.ConfigurationManager.AppSettings.Get("DireccionFus").ToString();

            string linea;
            SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus _fus = new SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus();
            SILPA.LogicaNegocio.Generico.Persona _per = new SILPA.LogicaNegocio.Generico.Persona();
            SILPA.AccesoDatos.Generico.PersonaIdentity _persona = new PersonaIdentity();
            SILPA.AccesoDatos.Generico.DireccionPersonaIdentity _direccion = new DireccionPersonaIdentity();

            string nomArch = ruta + "_InfoAdicional_" + ((int)processInstance).ToString() + "_" + ((int)formInstance).ToString() + ".rtf";

            listFusEnyity = _fus.CrearArchivoInfoAdicional((int)processInstance, (int)formInstance);

            using (System.IO.StreamWriter arc = new System.IO.StreamWriter(nomArch, true, Encoding.UTF8))
            {
                //Acá deben ir la cosas de encabezado del archivo. Nombre del Documento del Fus.
                linea = "----------------------------------------------------------------------";
                arc.WriteLine(linea);
                linea = "Nombre del formulario: " + _fus.NombreFormulario((int)processInstance);
                arc.WriteLine(linea);
                string[] lineasVital=NumeroVital((int)processInstance).Replace("<br />","/").Split('/');
                linea = "El Numero Vital de la Operacion es : " + lineasVital[0];
                arc.WriteLine(linea);
                linea = "" + lineasVital[1];
                arc.WriteLine(linea);
                linea = "----------------------------------------------------------------------";
                arc.WriteLine(linea);

                //Ahora deben aparecer los datos personales de quien diligencio el FUS.
                _per.ObternerPersonaByProcessInstace((int)processInstance);
                _persona = _per.Identity;
                _direccion = _per.IdentityDir;

                //arc.Write(CadenaUsuario(_persona, _direccion));

                foreach (SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus fus in listFusEnyity)
                {
                    linea = fus.strCampo.ToUpper().ToString() + ":    " + fus.strValor;
                    arc.WriteLine(linea);
                    linea = "";
                }
            }

        }



        private static string CadenaUsuario(PersonaIdentity persona, DireccionPersonaIdentity direccion)
        {
            string cadena="";
            if (!persona.PrimerNombre.Contains("Ciudadano"))
            {
                cadena = "***INFORMACION PERSONAL***" + '\n';
                if (!persona.TipoDocumentoIdentificacion.Nombre.Equals(IdentificacionPerosonaJuridica()))
                {
                    cadena += "Nombre Completo: " + persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido + '\n';
                    cadena += "Tipo de identificacion:" + persona.TipoDocumentoIdentificacion.Nombre + '\n';
                    cadena += "Número de Identificacion: " + persona.NumeroIdentificacion + '\n';
                    if (string.IsNullOrEmpty(persona.LugarExpediciónDocumento) || persona.LugarExpediciónDocumento == "-1")
                        cadena += "Origen de Documento: - \n";
                    else
                        cadena += "Origen de Documento:" + Municipio.obtenerNomDepMunByMunId(int.Parse(persona.LugarExpediciónDocumento)) + '\n';
                    cadena += "Número Teléfonico:" + persona.Telefono + '\n';
                    cadena += "Número Celular:" + persona.Celular + '\n';
                    cadena += "Número Fax:" + persona.Fax + '\n';
                    cadena += "Correo Electronico:" + persona.CorreoElectronico + '\n';
                    cadena += "***DIRECCION CONTACTO***" + '\n';
                    cadena += "Pais:" + Pais.getNombrePaisById(persona._direccionPersona.PaisId) + '\n';
                    cadena += "Ciudad:" + persona._direccionPersona.NombreDepartamento + "-" + persona._direccionPersona.NombreMunicipio + '\n';
                    cadena += "Direccion:" + persona._direccionPersona.DireccionPersona + '\n';
                    cadena += "***DIRECCION CORRESPONDENCIA***" + '\n';
                    cadena += "Pais:" + Pais.getNombrePaisById(direccion.PaisId) + '\n';
                    cadena += "Ciudad:" + direccion.NombreDepartamento + "-" + direccion.NombreMunicipio + '\n';
                    cadena += "Direccion:" + direccion.DireccionPersona + '\n';
                }
                else
                {
                    cadena += "Razon Social: " + persona.RazonSocial + '\n';
                    cadena += "Número de Identificacion: " + persona.NumeroIdentificacion + '\n';
                    cadena += "Origen de Documento:" + persona.LugarExpediciónDocumento + '\n';
                    cadena += "Número Teléfonico:" + persona.Telefono + '\n';
                    cadena += "Número Celular:" + persona.Celular + '\n';
                    cadena += "Número Fax:" + persona.Fax + '\n';
                    cadena += "Correo Electronico:" + persona.CorreoElectronico + '\n';
                    cadena += "***DIRECCION CONTACTO***" + '\n';
                    cadena += "Pais:" + persona._direccionPersona.PaisId + '\n';
                    cadena += "Ciudad:" + persona._direccionPersona.NombreDepartamento + "-" + persona._direccionPersona.NombreMunicipio + '\n';
                    cadena += "Direccion:" + persona._direccionPersona.DireccionPersona + '\n';
                    cadena += "***DIRECCION CORRESPONDENCIA***" + '\n';
                    cadena += "Pais:" + direccion.PaisId + '\n';
                    cadena += "Ciudad:" + direccion.NombreDepartamento + "-" + direccion.NombreMunicipio + '\n';
                    cadena += "Direccion:" + direccion.DireccionPersona + '\n';
                }
            }
            cadena += "**************************" + '\n';

            return cadena;
        }

        private static string IdentificacionPerosonaJuridica()
        {
            ParametroEntity _parametro = new ParametroEntity();
            ParametroDalc _parametroDalc = new ParametroDalc();
            _parametro.IdParametro = -1;
            _parametro.NombreParametro = _identificacionParametroJuridico;
            _parametroDalc.obtenerParametros(ref _parametro);
            return _parametro.Parametro;
        }

        private static string NumeroVital(int idProceso)
        {
            SILPA.AccesoDatos.Generico.NumeroSilpaDalc silpa = new NumeroSilpaDalc();
            return silpa.NumeroSilpa(idProceso);   
        }


        public const string _identificacionParametroJuridico = "IDE_PERSONA_JURIDICA";

        /// <summary>
        /// Metodo estatico que para el hilo externo
        /// </summary>
        [ObsoleteAttribute("Metodo Obsoleto y reemplazado por la utilizacion de Pooltrhead en el proceso")]
        public static void PararProceso()
        {
            _iniciado = false;
        }

        /// <summary>
        /// Metodo estatico que reinicia el hilo externo
        /// </summary>
        [ObsoleteAttribute("Metodo Obsoleto y reemplazado por la utilizacion de Pooltrhead en el proceso")]
        public static void ReiniciarProceso()
        {
            _iniciado = true;
        }

        /// <summary>
        /// Metodo estatico que reinicia la cola.
        /// </summary>
        [ObsoleteAttribute("Metodo Obsoleto y reemplazado por la utilizacion de Pooltrhead en el proceso")]
        public static void VaciarCola()
        {
            _cola = null;
            _cola = new Queue<Contenido>();
        }

        /// <summary>
        /// Clase contenedora de los Id del proceso que entrar a la clase.
        /// </summary>
        class Contenido
        {

            public int _proceso;

            /// <summary>
            ///  Identificador del  ProcessInstance
            /// </summary>
            public int _idProcessInstance;

        }

        public static void GenerarFus(int radId,int process, string rutaArchivo, string strNumeroSilpa, string usuario)
        {
            //rutaArchivo = "D:\\Reportes\\";
            if (string.IsNullOrEmpty(rutaArchivo))
            {
                SILPA.Comun.TraficoDocumento objTrafico = new SILPA.Comun.TraficoDocumento();
                rutaArchivo = objTrafico.CrearDirectorio(objTrafico.FileTraffic, process.ToString(), usuario);
                RadicacionDocumentoFachada objRadicacion = new RadicacionDocumentoFachada();
                objRadicacion.ActualizarRutaRadicacion(radId, rutaArchivo);      
            }
            CrearFormularios generador = new CrearFormularios();
            CrearFusRuta(process, rutaArchivo);
            string nombreArchivo = ValidarExisteArchivo(rutaArchivo, process.ToString(), ".pdf");
            ThreadPool.QueueUserWorkItem(new WaitCallback(GenerarPDF), new ContenidoRealizarAccion(rutaArchivo, nombreArchivo, strNumeroSilpa));
                
        }        
        public static void GenerarFus2(int radId, int process, string rutaArchivo, string strNumeroSilpa, string usuario)
        {

            if (string.IsNullOrEmpty(rutaArchivo))
            {
                SILPA.Comun.TraficoDocumento objTrafico = new SILPA.Comun.TraficoDocumento();
                rutaArchivo = objTrafico.CrearDirectorio(objTrafico.FileTraffic, process.ToString(), usuario);
                RadicacionDocumentoFachada objRadicacion = new RadicacionDocumentoFachada();
                objRadicacion.ActualizarRutaRadicacion(radId, rutaArchivo);
            }
            CrearFormularios generador = new CrearFormularios();
            CrearFusRuta(process, rutaArchivo);
            string nombreArchivo = ValidarExisteArchivo(rutaArchivo, process.ToString(), ".pdf");
            ThreadPool.QueueUserWorkItem(new WaitCallback(GenerarPDF), new ContenidoRealizarAccion(rutaArchivo, nombreArchivo, strNumeroSilpa));
        }
        public static void GenerarPDF(object objeto)
        {
            ContenidoRealizarAccion con = (ContenidoRealizarAccion)objeto;
            CrearFormularios generador = new CrearFormularios();
            generador.GenerarFormularioPdf(con._rutaArchivo, con._nombreArchivo, con._strNumeroSilpa);
        }
        public class ContenidoRealizarAccion
        {
            public string _rutaArchivo;
            public string _nombreArchivo;
            public string _strNumeroSilpa;

            public ContenidoRealizarAccion(string rutaArchivo, string nombreArchivo, string strNumeroSilpa)
            {
                this._rutaArchivo = rutaArchivo;
                this._nombreArchivo = nombreArchivo; ;
                this._strNumeroSilpa = strNumeroSilpa;
            }
        }
    }
}
