using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.LogicaNegocio.Excepciones;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.EstampaTiempo
{
    public class EstampaTiempoServicio
    {

        #region Metodos Publicos


            /// <summary>
            /// Obtiene fecha por medio de servicio de estampa cronologica
            /// </summary>
            /// <returns>DateTime con fecha retornada por estampa</returns>
            public DateTime ObtenerFecha()
            {
                DateTime objFecha = default(DateTime);

                try
                {
                    //TODO JNS Desarrolllo obtención de estampa
                    objFecha = DateTime.Now;
                }
                catch(Exception exc)
                {
                    //Escriba log de error
                    SMLog.Escribir(Severidad.Critico, "EstampaTiempoServicio :: ObtenerFecha -> Se genero un error obteniendo estampa de tiempo. Error: " + exc.Message + " - " + exc.StackTrace);

                    //Escalar error
                    throw new EstampaTiempoException("Se genero un error obteniendo estampa de tiempo", exc);

                }

                return objFecha;
            }

        #endregion

    }
}
