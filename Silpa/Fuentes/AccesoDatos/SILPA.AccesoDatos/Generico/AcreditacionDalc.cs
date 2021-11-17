using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using SILPA.Comun;
using System.Data.Common;

namespace SILPA.AccesoDatos.Generico
{
    public class AcreditacionDalc
    {
        public AcreditacionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Objeto de configuración del sitio, con las  variables globales
        /// </summary>
        private Configuracion objConfiguracion;
        public DataSet ListarTipoDocumentoAcreditacion()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());              
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_DOC_ACREDITACION");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }
    }
}
