using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.NotificacionElectronica;
using System.Threading;
using System.IO; 

namespace SILPA.Servicios.Generico.VerificarEstado
{
    [Obsolete("La clase de verificar estado no tuvo evolucion alguna, por lo que la clase de declara obsoleta")] 
    public class VerificarEstado
    {
        private static Queue<ActoNotificacion> _mensaje;
        private string _descripcionObjeto;
        private bool _parado;

        [ObsoleteAttribute("Metodo en desuso por que será utilizado por TrheadPool")]  public bool Parado
        {
            get { return _parado; }
            set
            {
                //EscribirArchivo("Fin del Proceso"); 
                _parado = value;
            }
        }


        [ObsoleteAttribute("Metodo en desuso por que será utilizado por TrheadPool")]
        public string DescripcionObjeto
        {
            get { return _descripcionObjeto; }
        }

        [ObsoleteAttribute("Metodo en desuso por que será utilizado por TrheadPool")]
        public VerificarEstado()
        {
            _mensaje = new Queue<ActoNotificacion>();
            _descripcionObjeto = "Inicializando el proceso de Preguntar porel estado de Acto Administrativo...";
        }

        /// <summary>
        /// AdicionarActo: Permite adicionar un objeto a la cola de espera de consulta de estado en notificación
        /// </summary>
        /// <param name="aa">Representa el objeto de ActoNotificacion</param>
        /// <returns>un true si la operacion de adicion es correcta de lo contrario un false</returns>
        [ObsoleteAttribute("Metodo en desuso por que será utilizado por TrheadPool")]
        public static bool AdicionarActo(ActoNotificacion aa)
        {
            try
            {
                _mensaje.Enqueue(aa);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        [ObsoleteAttribute("Metodo en desuso por que será utilizado por TrheadPool")]
        public void EnviarProceso()
        {
            ActoNotificacion  aa;
            while (_parado == false)
            {
                if (_mensaje.Count != 0)
                {
                    aa = _mensaje.Dequeue();
                    EscribirArchivo(aa);
                    //Realizar el proceso de Consulta de estado y escritura
                    //de  registro de la operación
                    //la respuesta de la operación debe cambiar el estado del
                    //Acto administrativo e ingresar registros en cosulta de estado
                    aa = null;
                }
                Thread.Sleep(1500);
            }
        }

        /// <summary>
        /// Metodo delegado para utilizar la funcionalidad de la clase dentro de un ThreadPool
        /// </summary>
        /// <param name="aa">Objeto que simboliza el Acto de Notificacion</param>
        public static void EnviarProceso(object aa)
        {
            EscribirArchivo((ActoNotificacion)aa);
        }




        [ObsoleteAttribute("Metodo en desuso por que será utilizado por TrheadPool")]
        private static void EscribirArchivo(ActoNotificacion aa)
        {
            string cadena = DateTime.Now.ToString() + " Acto Notificacion Número:" + aa.IdActoNotificacion;
            using (StreamWriter archivo = new StreamWriter(@"c:\temp\logInfo.txt", true))
            {
                archivo.WriteLine(cadena);
            }
        }
    }
}
