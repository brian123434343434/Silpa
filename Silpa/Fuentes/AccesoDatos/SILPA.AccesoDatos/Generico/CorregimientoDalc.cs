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
    /// Clase para el manejo de datos de los corregimientos
    /// </summary>
    public class CorregimientoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public CorregimientoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de Corregimiento 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del corregimiento a cargar, en la propiedad ID del objetoIdentity</param>
        public CorregimientoDalc(ref CorregimientoIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.Id };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_CORREGIMIENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COR_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["COR_NOMBRE"]);
                objIdentity.MunicipioId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Carga los valores para una identidad de Corregimiento cuyo valor del identificador corresponda 
        /// con la BD        
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del corregimiento a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerCorregimiento(ref CorregimientoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_CORREGIMIENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COR_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["COR_NOMBRE"]);
                objIdentity.MunicipioId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista los corregimientos en la BD. Pueden listarse los corregimientos por municipio o corregimiento particular.
        /// </summary>
        /// <param name="objIdentity.MunicipioId" >Valor del identificador de municipio por el que se filtraran los corregimientos, si es null no existen restricciones</param>
        /// <param name="objIdentity.Id" >Con este valor se lista el corregimiento con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: COR_ID, COR_NOMBRE, MUN_ID, MUN_NOMBRE</returns>
        public DataSet ListarCorregimiento(CorregimientoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.Id };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_CORREGIMIENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista los corregimientos en la BD. Pueden listarse los corregimientos por municipio o corregimiento particular.
        /// </summary>
        /// <param name="intMunicipioId" >Valor del identificador de municipio por el que se filtraran los corregimientos, si es null no existen restricciones</param>
        /// <param name="intId" >Con este valor se lista el corregimietno con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: COR_ID, COR_NOMBRE, MUN_ID, MUN_NOMBRE</returns>
        public DataSet ListarCorregimiento(Nullable<int> IntMunicipioId, Nullable<int> intId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { IntMunicipioId, intId };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_CORREGIMIENTO", parametros);
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
