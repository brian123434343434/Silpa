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
    public class SubZonaHidrologicaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public SubZonaHidrologicaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de sub zona hidrologica
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la sub zona hidrologica a cargar, en la propiedad ID del objetoIdentity</param>
        public SubZonaHidrologicaDalc(ref SubZonaHidrologicaIdentity  objIdentity)
        {
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.ZonaHidroId, objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_ZONA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SHI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["SHI_NOMBRE"]);
                objIdentity.ZonaHidroId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ZHI_ID"]);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Carga los valores para una identidad de sub zona hidrologica cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la sub zona hidrologica a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerZonaHidrografica(ref SubZonaHidrologicaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.ZonaHidroId, objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_ZONA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ZHI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["AHI_ID"]);
                objIdentity.ZonaHidroId  = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ZHI_NOMBRE"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las sub zonas hidrograficas en la BD. Pueden listarse las sub zonas hidrologicas por zona hidrografica o una sub sen particular.
        /// </summary>
        /// <param name="objIdentity.ZonaHidroId" >Valor del identificador de la zona hidrografica por el cual se filtraran las sub zonas hidrograficas, si es null no existen restricciones</param>
        /// <param name="objIdentity.Id" >Con este valor se lista la sub zonas hidrograficas con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  SHI_ID, SHI_NOMBRE, ZHI_ID, ZHI_NOMBRE</returns>
        public DataSet ListarZonaHidrografica(SubZonaHidrologicaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.ZonaHidroId , objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_ZONA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista las sub zonas hidrograficas en la BD. Pueden listarse las sub zonas hidrologicas por zona hidrografica o una sub sen particular.
        /// </summary>
        /// <param name="ZonaHidroId" >Valor del identificador de la zona hidrografica por el cual se filtraran las sub zonas hidrograficas, si es null no existen restricciones</param>
        /// <param name="intId" >Con este valor se lista la sub zonas hidrograficas con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  SHI_ID, SHI_NOMBRE, ZHI_ID, ZHI_NOMBRE</returns>
        public DataSet ListarSubZonaHidrografica(Nullable<int> intZonaHidroId, Nullable<int> intId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intZonaHidroId, intId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SUBZONA_HIDROGRAFICA", parametros);
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
