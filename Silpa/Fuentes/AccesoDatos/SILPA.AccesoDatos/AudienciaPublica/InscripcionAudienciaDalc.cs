using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.AudienciaPublica
{
   public class InscripcionAudienciaDalc
    {
  /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
       public InscripcionAudienciaDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Lista los inscritos en la audiencia publica.
        /// </summary>
        /// <param name="numeroSilpa"></param>
       /// <param name="idAutoridad"></param>
       /// <param name="nombreProyecto"></param>
       /// <param name="numeroExpediente"></param>
       /// <param name="fechaInformativa"></param>
       /// <param name="fechaAudiencia"></param>
        /// si es null no existen restricciones.</param>
        /// <returns>DataSet con los registros </returns>
       public DataSet ListarInscritosAudiencia(string numeroSilpa,  Nullable<int> idAutoridad, string nombreProyecto, string numeroExpediente,
                                               Nullable<DateTime> fechaInformativa, Nullable<DateTime> fechaAudiencia, int aprobado)
       {
           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { numeroSilpa, idAutoridad, nombreProyecto, 
                                                numeroExpediente, fechaInformativa, fechaAudiencia , aprobado};
           DbCommand cmd = db.GetStoredProcCommand("AUD_LISTA_INSCRITOS_AUDIENCIA_PUBLICA", parametros);
           DataSet dsResultado = db.ExecuteDataSet(cmd);
           return (dsResultado);
       }

    }
}

