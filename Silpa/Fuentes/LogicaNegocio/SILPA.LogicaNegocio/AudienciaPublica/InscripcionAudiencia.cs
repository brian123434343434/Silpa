using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.AudienciaPublica;
using System.Data;
using SILPA.LogicaNegocio.Generico;
namespace SILPA.LogicaNegocio.AudienciaPublica
{
   public class InscripcionAudiencia
    {
  /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;
        private InscripcionAudienciaDalc InsDalc;

        /// <summary>
        /// Constructor
        /// </summary>
        public InscripcionAudiencia()
        {
            _objConfiguracion = new Configuracion();
            InsDalc = new InscripcionAudienciaDalc();           
        }

        /// <summary>
        ///  Lista los inscritos en la audiencia publica.
        /// </summary>
        /// <param name="numeroSilpa"></param>
       /// <param name="idAutoridad"></param>
       /// <param name="nombreProyecto"></param>
       /// <param name="numeroExpediente"></param>
       /// <param name="fechaInformativa"></param>
       /// <param name="fechaAudiencia"></param>
        /// <returns>DataSet con los registros
        /// </returns>
        public DataSet ListarInscritosAudiencia(string numeroSilpa,  Nullable<int> idAutoridad, string nombreProyecto, string numeroExpediente,
                                                Nullable<DateTime> fechaInformativa, Nullable<DateTime> fechaAudiencia, int aprobado)
        {
            return InsDalc.ListarInscritosAudiencia(numeroSilpa, idAutoridad, nombreProyecto,
                                             numeroExpediente, fechaInformativa, fechaAudiencia, aprobado);
        }

    }
}