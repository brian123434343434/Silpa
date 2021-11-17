using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Parametrizacion
{
    public class ConsultaDocumentoDALC
    {
        private Configuracion objConfiguracion;
        public ConsultaDocumentoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista las entidades externas
        /// </summary>
        /// <returns></returns>
        public DataTable ListarEntidadesExternas()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("DOC_SELECCION_ENTIDAD_EXTERNA");
                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

        public DataTable Listar_Documentos(Nullable<long> intIdUsuario, Nullable<int> intEexId, string strCodigoSilpa,
            Nullable<System.DateTime> dateFechaInicial, Nullable<System.DateTime> dateFechaFinal)
        {
            try
            {

                object[] parametros = new object[] 
                  { 
                      intIdUsuario,
                      dateFechaInicial,
                      dateFechaFinal,
                      intEexId,
                      strCodigoSilpa
                  };

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_ENTIDAD_DOCUMENTO_SOLICITUD", parametros);



                DataSet ds_datos = db.ExecuteDataSet(cmd);
                return ds_datos.Tables[0];
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }

    }
}
