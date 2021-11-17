using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;

namespace SILPA.AccesoDatos.Documento
{
    public class MisTramitesDocumentosDALC
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private string silpaConnection;

      

        public MisTramitesDocumentosDALC()
        {
            silpaConnection = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
   
        }


        public void InsertarDocumentos(string aa, string NumeroSilpa, string acto_adm, string ruta)
        {
            SqlDatabase db = new SqlDatabase(silpaConnection);
            object[] parametros = new object[] {    aa,
                                                    NumeroSilpa,
                                                    acto_adm,
                                                    ruta
                                                    };
            
            DbCommand cmd = db.GetStoredProcCommand("NOT_INSERTAR_DOCUMENTOS_AA_EXTERNAS", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Documentos.";
                throw new Exception(strException, ex);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }

        }

        public void InsertarDocumentoNoNotifica(int IdAutoirdad, string NumeroVital, string NumeroActo, string ruta, string IdtipoActoAdministrativo)
        {
            SqlDatabase db = new SqlDatabase(silpaConnection);
            DbCommand cmd = db.GetStoredProcCommand("NOT_INSERTAR_NOT_ACTO_NO_NOTIFICA");
            db.AddInParameter(cmd, "P_NUMERO_ACTO", DbType.String, NumeroActo);
            db.AddInParameter(cmd, "P_NUMERO_VITAL", DbType.String, NumeroVital);
            db.AddInParameter(cmd, "P_RUTA_ARCHIVO", DbType.String, ruta);
            db.AddInParameter(cmd, "P_IDAA", DbType.Int32, IdAutoirdad);
            db.AddInParameter(cmd, "P_DOC_ID", DbType.String, IdtipoActoAdministrativo);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }
      

        public DataSet Consultar(int AA, string NumeroSilpa)
        {
            try
            {
                DataSet resultado = null;
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { AA, NumeroSilpa };
                DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_DOCUMENTO_AA_EXTERNAS", parametros);
                DataSet dsResultado = new DataSet();
                dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

 

  

    }
}
