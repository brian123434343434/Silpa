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
    /// Clase encargada de la manipulación de los datos de las jurisdicciones
    /// </summary>
    class JurisdiccionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public JurisdiccionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de Vereda 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Identificador de la jurisdicción</param>
        public JurisdiccionDalc(ref JurisdiccionIdentity objIdentity)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.Id , objIdentity.AutoridadId, objIdentity.MunicipioId };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_JURISDICCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["JUR_ID"]);
                objIdentity.AutoridadId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
                objIdentity.MunicipioId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Carga los valores para una identidad de jurisdiccion cuyo valor del identificador corresponda 
        /// con la BD        
        /// </summary>
        /// <param name="objIdentity.Id">Identificador de la jurisdicción</param>
        public void ObtenerJurisdiccion(ref JurisdiccionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.Id, objIdentity.AutoridadId, objIdentity.MunicipioId };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_JURISDICCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["JUR_ID"]);
                objIdentity.AutoridadId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
                objIdentity.MunicipioId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las jurisdicciones en la BD. Pueden listarse por jurisdiccion por municipio y/o autoridad.
        /// </summary>
        /// <param name="objIdentity.Id">Identificador de la jurisdicción, null ignora este criterio</param>
        /// <param name="objIdentity.AutoridadId" >Valor de la autoridad por la cual se filtraran los registros, null ignora este criterio</param>
        /// <param name="objIdentity.MunicipioId" >>Valor del municipio del corregimiento por el que se filtraran los registros, null ignora este criterio</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  JUR_ID, AUT_ID, MUN_ID, FECHA_INSERCION, AUT_NOMBRE, MUN_NOMBRE'</returns>
        public DataSet ListarJurisdiccion(JurisdiccionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.AutoridadId, objIdentity.MunicipioId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_JURISDICCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
/// Lista las jurisdicciones en la BD. Pueden listarse por jurisdiccion por municipio y/o autoridad.
        /// </summary>
        /// <param name="IntId">Identificador de la jurisdicción, null ignora este criterio</param>
        /// <param name="IntAutoridadId" >Valor de la autoridad por la cual se filtraran los registros, null ignora este criterio</param>
        /// <param name="IntMunicipioId" >>Valor del municipio del corregimiento por el que se filtraran los registros, null ignora este criterio</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  JUR_ID, AUT_ID, MUN_ID, FECHA_INSERCION, AUT_NOMBRE, MUN_NOMBRE'</returns>
        public DataSet ListarJurisdiccion(Nullable<int> IntId, Nullable<int> IntAutoridadId, Nullable<int> IntMunicipioId)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { IntMunicipioId, IntAutoridadId, IntMunicipioId };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_JURISDICCION", parametros);
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
