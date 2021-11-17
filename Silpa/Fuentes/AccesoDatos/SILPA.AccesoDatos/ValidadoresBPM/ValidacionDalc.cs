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


namespace SILPA.AccesoDatos.ValidadoresBPM
{
    public class ValidacionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public ValidacionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los diferentes tipos de validaciones
        /// </summary>
        /// <returns>Conjunto de datos con las validaciones</returns>
        public DataSet ListarValidacion()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { };
                DbCommand cmd = db.GetStoredProcCommand("SPVA_VALIDACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Inserta una validacion en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los datos de la validacion</param>
        public void InsertarValidacion(ref ValidacionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.DescripcionValidacion,objIdentity.SentenciaValidacion
                        };

                DbCommand cmd = db.GetStoredProcCommand("SPVA_CREAR_VALIDACION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Actualiza una validacion en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los datos de la validacion</param>
        public void EditarValidacion(ref ValidacionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.IdValidacion, objIdentity.DescripcionValidacion, objIdentity.SentenciaValidacion
                        };

                DbCommand cmd = db.GetStoredProcCommand("SPVA_EDITAR_VALIDACION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


    }
}
