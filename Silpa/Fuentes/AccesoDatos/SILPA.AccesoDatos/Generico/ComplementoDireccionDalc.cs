using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class ComplementoDireccionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public ComplementoDireccionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los Nomenclaturas para una direccion fisica en la BD. Pueden listarse las Nomenclaturas.
        /// </summary>
        /// <returns>DataSet con los registros y las siguientes columnas:  NOM_ID, NOM_NOMBRE</returns>
        public DataSet ListasComplementoDireccion()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_COMPLEMENTO_DIRECCION");
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }
    }
}
