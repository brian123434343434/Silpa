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
    /// Clase encargada de la manipulación de los datos de las zonas hidrograficas
    /// </summary>
    public class ZonaHidrograficaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public ZonaHidrograficaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de zona hidrografica 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la zona hidrografica a cargar, en la propiedad ID del objetoIdentity</param>
        public ZonaHidrograficaDalc(ref ZonaHidrograficaIdentity objIdentity)
        {
            try
            {
                objConfiguracion = new Configuracion();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.AreaHidroId , objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_ZONA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ZHI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["AHI_ID"]);
                objIdentity.AreaHidroId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ZHI_NOMBRE"]);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Carga los valores para una identidad de zona hidrografica cuyo valor del identificador corresponda 
        /// con la BD        
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la zona hidrografica a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerZonaHidrografica(ref ZonaHidrograficaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.AreaHidroId, objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_ZONA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ZHI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["AHI_ID"]);
                objIdentity.AreaHidroId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ZHI_NOMBRE"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las zona hidrograficas en la BD. Pueden listarse los municipios por departamento, regional o un municipio en particular.
        /// </summary>
        /// <param name="objIdentity.AreaHidroId" >Valor del identificador de la zona hidrografica por el cual se filtraran los municipios, si es null no existen restricciones</param>
        /// <param name="objIdentity.Id" >Con este valor se lista las zonas hidrograficas con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  ZHI_ID, ZHI_NOMBRE, AHI_ID, AHI_NOMBRE</returns>
        public DataSet ListarZonaHidrografica(ZonaHidrograficaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.AreaHidroId, objIdentity.Id };
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
        /// Lista las zona hidrograficas en la BD. Pueden listarse los municipios por departamento, regional o un municipio en particular.
        /// </summary>
        /// <param name="intAreaHidroId" >Valor del identificador de la zona hidrografica por el cual se filtraran los municipios, si es null no existen restricciones</param>
        /// <param name="intId" >Con este valor se lista las zonas hidrograficas con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  ZHI_ID, ZHI_NOMBRE, AHI_ID, AHI_NOMBRE</returns>
        public DataSet ListarZonaHidrografica(Nullable<int> intAreaHidroId, Nullable<int> intId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intId,intAreaHidroId };
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
        /// Lista las zona hidrograficas relacionadas a una subzona.
        /// </summary>
        /// <param name="intZonaHidroId" >Valor del identificador de la zona hidrografica por el cual se filtraran los municipios, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  ZHI_ID, ZHI_NOMBRE</returns>
        public DataSet ListarZonaHidrograficaSubZona(int intZonaHidroId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intZonaHidroId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_ZONA_HIDROGRAFICA_SUBZONA", parametros);
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
