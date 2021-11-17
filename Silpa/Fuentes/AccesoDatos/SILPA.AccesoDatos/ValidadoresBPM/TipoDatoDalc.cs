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
    public class TipoDatoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public TipoDatoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los diferentes tipos de datos
        /// </summary>
        /// <returns>Conjunto de datos con los datos de los tipos de datos</returns>
        public DataSet ListarTipoDato()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { };
                DbCommand cmd = db.GetStoredProcCommand("SPVA_TIPO_DATO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Inserta un tipo de dato en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los datos del tipo de dato</param>
        public void InsertarTipoDato(ref TipoDatoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.DescripcionTipoDato
                        };

                DbCommand cmd = db.GetStoredProcCommand("SPVA_CREAR_TIPO_DATO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Actualiza un tipo de dato en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los datos del tipo de dato</param>
        public void EditarTipoDato(ref TipoDatoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.IdTipoDato, objIdentity.DescripcionTipoDato
                        };

                DbCommand cmd = db.GetStoredProcCommand("SPVA_EDITAR_TIPO_DATO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}
