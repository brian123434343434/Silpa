using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Parametrizacion
{
   public class ParametrizacionDocumentoDalc
    {
         private Configuracion objConfiguracion = new Configuracion();


        /// <summary>
        /// Seleccionar los tipos de adquisición
        /// </summary>
        /// <returns></returns>
        public DataTable ListarTipoAdquisicion()
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("DOC_SELECCION_TIPO_AQUISICION");
            DataSet dsResultado= new DataSet(); 
            
            try
            {

               dsResultado= db.ExecuteDataSet(cmd);
               return dsResultado.Tables[0]; 
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// Seleccionar las entidades externas
        /// </summary>
        /// <returns></returns>
 
        public DataTable ListarEntidadesExternas()
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("DOC_SELECCION_ENTIDAD_EXTERNA");
            DataSet dsResultado = new DataSet(); 
            try
            {
                dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado.Tables[0];
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }


       /// <summary>
       /// metodo que permite agregra un  registro a la base de datos
       /// con la informacion nueva de la parametrización de un documento
       /// </summary>
       /// <param name="strDocNombre"></param>
       /// <param name="strEnlaceAplicativo"></param>
       /// <param name="intCodAquisicion"></param>
       /// <param name="intEntidaExterna"></param>
       /// <returns></returns>
       public Boolean agregarDocumentos(string strDocNombre, string strEnlaceAplicativo, int intCodAquisicion, int intEntidaExterna, string strCodigoProceso)
        {
            
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { strDocNombre, strEnlaceAplicativo, intCodAquisicion, intEntidaExterna, strCodigoProceso };
                    DbCommand cmd = db.GetStoredProcCommand("DOC_INGRESAR_PARAMETRIZACION_DOCUMENTO", parametros);
                    db.ExecuteNonQuery(cmd);
                    return true; 
                }
                catch (Exception ex)
                { 
                    return false;
                }
            }
        }

       public void ActualizarDocumento(ParametrizacionDocumentoEntity pParametrizacionDocumentoEntity)
       {
           objConfiguracion = new Configuracion();
           try
           {
               SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
               DbCommand cmd = db.GetStoredProcCommand("DOC_ACTUALIZAR_PARAMETRIZACION_DOCUMENTO");
               db.AddInParameter(cmd, "P_DOC_ID", DbType.Int32, pParametrizacionDocumentoEntity.DocID);
               db.AddInParameter(cmd, "P_DOC_NOMBRE", DbType.String, pParametrizacionDocumentoEntity.DocNombre);
               db.AddInParameter(cmd, "P_DOC_ENLACE_APLICATIVO", DbType.String, pParametrizacionDocumentoEntity.EnlaceAplicativo);
               db.AddInParameter(cmd, "P_ADQ_ID", DbType.Int32, pParametrizacionDocumentoEntity.TipoAdquisicionID);
               db.AddInParameter(cmd, "P_EEX_ID", DbType.Int32, pParametrizacionDocumentoEntity.EntidadExternaID);
               db.AddInParameter(cmd, "P_CODIGO_PROCESO", DbType.String, pParametrizacionDocumentoEntity.CodigoProceso);
               db.AddInParameter(cmd, "P_IMAGEN_URL", DbType.String, pParametrizacionDocumentoEntity.ImagenUrl);
               db.ExecuteNonQuery(cmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           
           
       }


        /// <summary>
        /// Seleccionar la parametrización de los documentos
        /// </summary>
        /// <param name="intAdquisicion"></param>
        /// <param name="intEntidad"></param>
        /// <returns></returns>
       public DataTable ListarParametrizacionDocumento(Nullable<int> intAdquisicion, Nullable<int> intEntidad)
        {

            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intAdquisicion, intEntidad };
            DbCommand cmd = db.GetStoredProcCommand("DOC_CONSULTAR_PARAMETRIZACION_DOCUMENTO",parametros);
            DataSet dsResultado = new DataSet();

            try
            {
                dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado.Tables[0];
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

       public ParametrizacionDocumentoEntity ConsultarDocumentoXDocID(int DocID)
       {
           objConfiguracion = new Configuracion();

           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           DbCommand cmd = db.GetStoredProcCommand("DOC_CONSULTAR_PARAMETRIZACION_DOCUMENTO_POR_DOC_ID");
           db.AddInParameter(cmd, "P_DOC_ID", DbType.Int32, DocID);
           using (IDataReader reader = db.ExecuteReader(cmd))
           {
               if (reader.Read())
               {
                   return new ParametrizacionDocumentoEntity
                   {
                       DocID = Convert.ToInt32(reader["DOC_ID"]),
                       DocNombre = reader["DOC_NOMBRE"].ToString(),
                       EnlaceAplicativo = reader["DOC_ENLACE_APLICATIVO"].ToString(),
                       TipoAdquisicionID = Convert.ToInt32(reader["ADQ_ID"]),
                       EntidadExternaID = Convert.ToInt32(reader["EEX_ID"]),
                       CodigoProceso = reader["CODIGO_PROCESO"].ToString(),
                       ImagenUrl = reader["IMAGEN_URL"].ToString()
                   };
               }
           }
           return null;
       }

    }
}
