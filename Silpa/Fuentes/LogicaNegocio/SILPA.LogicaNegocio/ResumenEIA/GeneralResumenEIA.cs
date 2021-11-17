using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.ResumenEIA.Generacion;

namespace SILPA.LogicaNegocio.ResumenEIA
{
    public class GeneralResumenEIA
    {
        /// <summary>
        /// Crea un nuevo proyecto vacio
        /// </summary>
        /// <returns>El identificador del proyecto recien creado</returns>
        public static string GenerarProyectoEIA()
        {
            Generacion objGeneracion = new Generacion();
            return objGeneracion.GenerarProyectoEIA();
        }
        public static string GenerarNumeroVitalProyectoEIA(int idSolicitante,int idTramite)
        {
            Generacion objGeneracion = new Generacion();
            return objGeneracion.GenerarNumeroVitalProyectoEIA(idSolicitante, idTramite);
        }
    }
}
