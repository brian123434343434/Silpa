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
    class ConsultaDocumentosDALC
    {
        private Configuracion objConfiguracion = new Configuracion();

        /// <summary>
        /// Lista la informacion de las entidades externas
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        public DataTable Listar_Entidad_Extrena()
        {
            try
            {
                objConfiguracion = new Configuracion();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("DOC_SELECCION_ENTIDAD_EXTERNA");
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0]);
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }
    }
}
