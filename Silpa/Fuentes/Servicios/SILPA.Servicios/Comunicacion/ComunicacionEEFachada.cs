using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.Generico;
using System.Threading;

namespace SILPA.Servicios.Comunicacion
{
    public class ComunicacionEEFachada
    {
        public SILPA.LogicaNegocio.Comunicacion.ComunicacionEE _objComunicacion;
        private static Queue<string> _mensaje;
        private string _descripcionObjeto;
        private bool _parado;

        [ObsoleteAttribute("Metodo descartado por la utilizacion del ThreadPool en su uso")]
        public bool Parado
        {
            get { return _parado; }
            set
            {
                //EscribirArchivo("Fin del Proceso"); 
                _parado = value;
            }
        }

        [ObsoleteAttribute("Metodo descartado por la utilizacion del ThreadPool en su uso")]
        public string DescripcionObjeto
        {
            get { return _descripcionObjeto; }
        }

        [ObsoleteAttribute("Metodo descartado por la utilizacion del ThreadPool en su uso")]
        public static bool EnviarComunicacionFachada(string strXmlDatos)
        {
            _mensaje.Enqueue(strXmlDatos);
            return true;
        }

        [ObsoleteAttribute("Metodo descartado por la utilizacion del ThreadPool en su uso")]
        public ComunicacionEEFachada()
        {
            _mensaje = new Queue<string>();
            _descripcionObjeto = "Inicializando el proceso de envío de comunicación visita...";
            //EscribirArchivo("Proceso Comunicacion Visita"); 
        }
      
        /// <summary>
        /// Metodo delegado para utilizar enviar peoceso de comunicacion EE con ThradPool
        /// </summary>
        /// <param name="xmlDatos">string que simboliza los datos completo a enviar a la entidad externa</param>
        public static string EnviarProceso(object xmlDatos)
        {
            string strXmlDatos = (string)xmlDatos;

            SILPA.LogicaNegocio.Comunicacion.ComunicacionEE _objComunicacion = new SILPA.LogicaNegocio.Comunicacion.ComunicacionEE();
            return _objComunicacion.EnviarComunicacionEE(strXmlDatos, false);
        }

        /// <summary>
        /// Metodo delegado para utilizar enviar peoceso de comunicacion EE con ThradPool  3008012384
        /// </summary>
        /// <param name="xmlDatos">string que simboliza los datos completo a enviar a la entidad externa</param>
        public static void EnviarProcesoRespuesta(object xmlDatos)
        {
            string strXmlDatos = (string)xmlDatos;

            SILPA.LogicaNegocio.Comunicacion.ComunicacionEE _objComunicacion = new SILPA.LogicaNegocio.Comunicacion.ComunicacionEE();

            _objComunicacion.EnviarComunicacionEE(strXmlDatos, true);
        }
    }
}
