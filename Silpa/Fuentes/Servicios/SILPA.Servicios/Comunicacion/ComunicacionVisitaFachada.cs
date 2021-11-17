using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.Generico;
using System.Threading;

namespace SILPA.Servicios.Comunicacion
{
    public class ComunicacionVisitaFachada
    {
        public SILPA.LogicaNegocio.Comunicacion.ComunicacionVisita _objComunicacion;
        private static Queue<string> _mensaje;
        private string _descripcionObjeto;
        private bool _parado;

        [ObsoleteAttribute("Metodo descartado por la utilizacion del ThreadPool en su uso")]  public bool Parado
        {
            get { return _parado; }
            set
            {
                //EscribirArchivo("Fin del Proceso"); 
                _parado = value;
            }
        }
        [ObsoleteAttribute("Metodo descartado por la utilizacion del Poolthread en su uso")]
        public string DescripcionObjeto
        {
            get { return _descripcionObjeto; }
        }


        //public ComunicacionVisitaFachada() { }

        [ObsoleteAttribute("Metodo descartado por la utilizacion del Poolthread en su uso")]
        public static bool EnviarComunicacionFachada(string strXmlDatos)
        {
            _mensaje.Enqueue(strXmlDatos);
            return true; 
        }

        [ObsoleteAttribute("Metodo descartado por la utilizacion del Poolthread en su uso")]
        public ComunicacionVisitaFachada()
        {
            _mensaje = new Queue<string>();
            _descripcionObjeto = "Inicializando el proceso de envío de comunicacón visita...";
            //EscribirArchivo("Proceso Comunicacion Visita"); 
        }

        [ObsoleteAttribute("Metodo descartado por la utilizacion del Poolthread en su uso")]
        public void EnviarProceso()
        {
            string strXmlDatos;
            //EscribirArchivo("Inicio del Hilo"); 
            while (_parado == false)
            {
                if (_mensaje.Count != 0)
                {
                    strXmlDatos = _mensaje.Dequeue();
                    _objComunicacion = new SILPA.LogicaNegocio.Comunicacion.ComunicacionVisita();
                    _objComunicacion.EnviarComunicacionVisita(strXmlDatos);
                    strXmlDatos = null;
                }
                //activo en prueba
                //Thread.Sleep(1500);
            }
        }

        public static void EnviarProceso(object xmlDatos)
        {
            string strXmlDatos = (string)xmlDatos ;
            SILPA.LogicaNegocio.Comunicacion.ComunicacionVisita _objComunicacion = new SILPA.LogicaNegocio.Comunicacion.ComunicacionVisita();
            _objComunicacion.EnviarComunicacionVisita(strXmlDatos);
            _objComunicacion = null;
        }


    }
}
