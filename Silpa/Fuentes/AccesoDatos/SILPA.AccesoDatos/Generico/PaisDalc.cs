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
    /// <summary>
    /// Clase encargada de la manipulación de los datos de los paises
    /// </summary>
    public class PaisDalc
    {
        
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public PaisDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de pais 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del pais a cargar, en la propiedad ID del objetoIdentity</param>
        public PaisDalc(ref PaisIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PAIS", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PAI_NOMBRE"]);
                objIdentity.CodigoInter = Convert.ToString(dsResultado.Tables[0].Rows[0]["PAI_ACTIVO"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de pais 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del pais a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerPaises(ref PaisIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PAIS", parametros);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        objIdentity.Id = Convert.ToInt32(reader["PAI_ID"]);
                        objIdentity.Nombre = Convert.ToString(reader["PAI_NOMBRE"]);
                        objIdentity.CodigoInter = Convert.ToString(reader["PAI_CODIGO_INTL"]);
                    }
                }

                
                
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Lista los paises en la BD. Pueden listarse los todos paises o uno en particular.
        /// </summary>
        /// <param name="objIdentity.Id" >Con este valor se lista el pais con el identificador, si es null se listan todos los paises</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  PAI_ID, PAI_NOMBRE, PAI_CODIGO_INTL</returns>
        public DataSet ListarPaises(PaisIdentity objIdentity)
        {

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id  };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PAIS", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista los paises en la BD. Pueden listarse los todos paises o uno en particular.
        /// </summary>
        /// <param name="intId" >Con este valor se lista el pais con el identificador, si es null se listan todos los paises</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  PAI_ID, PAI_NOMBRE, PAI_CODIGO_INTL</returns>
        public DataSet ListarPaises(Nullable<int> intId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intId };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PAIS", parametros);
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
