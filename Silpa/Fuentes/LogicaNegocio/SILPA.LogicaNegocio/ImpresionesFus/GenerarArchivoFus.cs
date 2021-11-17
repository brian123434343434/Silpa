using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using System.Collections;

namespace SILPA.LogicaNegocio.ImpresionesFus
{
    public class GenerarArchivoFus
    {
        public List<SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus> CrearArchivo(int ProcessInstance)
        {
            SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFusDalc objImpresion = new SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFusDalc();
            return objImpresion.CrearArchivo(ProcessInstance);
        }

        public string NombreFormulario(int ProcessInstance)
        {
            SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFusDalc objImpresion = new SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFusDalc();
            return objImpresion.NombreFormulario(ProcessInstance);
        }

        public string NombreProyecto(string NumeroVital)
        {
            SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFusDalc objImpresion = new SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFusDalc();
            return objImpresion.NombreProyecto(NumeroVital);
        }

        /// <summary>
        /// HAVA:09-oct-10
        /// crea el fus para una instancia de formulario.
        /// </summary>
        /// <param name="ProcessInstance"></param>
        /// <param name="FormInstance"></param>
        /// <returns></returns>
        public List<SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus> CrearArchivoInfoAdicional(int ProcessInstance, int FormInstance)
        {
            SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFusDalc objImpresion = new SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFusDalc();
            return objImpresion.CrearArchivoInfoAdicional(ProcessInstance, FormInstance);
        }





    }
}
