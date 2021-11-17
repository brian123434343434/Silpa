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
namespace SILPA.AccesoDatos.Generico
{
    public class NomenclaturaDalc
    {

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public NomenclaturaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los Nomenclaturas para una direccion fisica en la BD. Pueden listarse las Nomenclaturas.
        /// </summary>
        /// <returns>DataSet con los registros y las siguientes columnas:  NOM_ID, NOM_NOMBRE</returns>
        public DataSet ListarSector()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_NOMENCLATURA");
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }
    }
}
