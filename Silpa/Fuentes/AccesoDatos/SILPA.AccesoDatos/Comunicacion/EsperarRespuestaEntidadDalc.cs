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

namespace SILPA.AccesoDatos.Comunicacion
{
  public class EsperarRespuestaEntidadDalc
    {
     /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
      public EsperarRespuestaEntidadDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Lista las respuestas que estan en espera.
        /// </summary>     
        /// <param name="intIdExpediente">Identificador del expediente</param>
        /// <returns>DataSet con los registros y las siguientes columnas: 
       /// </returns>
       public DataSet ListarEsperarRespuesta(int strIdExpediente)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strIdExpediente };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_ESPERA_RESPUESTA_ENTIDAD", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Inserta la respuesta de la comunicacion a la EE
        /// </summary>
        /// <param name="objIdentity">Objeto de espra de respuesta</param>
       public void InsertarEsperarRespuesta(ref EsperarRespuestaEntidadIdentity objIdentity)
        {          
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.idRespuestaEntidad, objIdentity.idRespuesta, objIdentity.nombre, objIdentity.correoElectronico
                        };

                DbCommand cmd = db.GetStoredProcCommand("GEN_CREAR_ESPERA_RESPUESTA_ENTIDAD", parametros);
                try
                {
                    db.ExecuteDataSet(cmd);
                    objIdentity.idRespuestaEntidad = Convert.ToInt32(cmd.Parameters["@P_ID"].Value);


                }
                finally
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }             
        }

        /// <summary>
        /// Actualiza la respuesta de la comunicacion a la EE
        /// </summary>
        /// <param name="objIdentity">Objeto de espra de respuesta</param>
      public void ActualizarEsperarRespuesta(int idRespuestaEntidad, DateTime fechaRespuesta)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {
                           idRespuestaEntidad, fechaRespuesta
                        };

            DbCommand cmd = db.GetStoredProcCommand("GEN_UPDATE_ESPERA_RESPUESTA_ENTIDAD", parametros);
            try
            {
                db.ExecuteDataSet(cmd); 
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 07-jul-2010 - aegb
        /// Actualiza la respuesta de la comunicacion a la EE
        /// </summary>
        /// <param name="objIdentity">Objeto de espra de respuesta</param>
      public void ActualizarEsperarRespuesta(ref EsperarRespuestaEntidadIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {
                          objIdentity.IdEntidad, objIdentity.NumSilpa 
                        };

            DbCommand cmd = db.GetStoredProcCommand("GEN_UPDATE_RESPUESTA_ENTIDAD", parametros);
            try
            {
                db.ExecuteDataSet(cmd);
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }
  }
}
