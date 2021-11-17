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
  public  class SolicitudAudienciaDalc
    {
   /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
      public SolicitudAudienciaDalc()
        {
            objConfiguracion = new Configuracion();
        }
        

        /// <summary>
        /// Crea un registro en la tabla solicitud de audiencia publica
        /// </summary>
      public void CrearSolicitudAudiencia(ref SolicitudAudienciaIdentity objIdentity)
       {
           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { objIdentity.NumeroSilpa, objIdentity.NombreProyecto, objIdentity.TitularProyecto, 
                                                objIdentity.NumeroSilpaProyecto, objIdentity.NumeroExpediente,
                                                objIdentity.IdAutoridad, objIdentity.IdPersona, objIdentity.IdTipoFuncionario,
                                                objIdentity.MotivoSolicitud, objIdentity.IdAudiencia};
           DbCommand cmd = db.GetStoredProcCommand("AUD_CREAR_SOLICITUD_AUDIENCIA_PUBLICA", parametros);
           try
           {
               db.ExecuteNonQuery(cmd);
               objIdentity.NumeroSilpa = cmd.Parameters["@P_NUMERO_SILPA"].Value.ToString();
           }
           finally
           {
               cmd.Connection.Close();
               cmd.Dispose();
           }          
       }

    }
}
