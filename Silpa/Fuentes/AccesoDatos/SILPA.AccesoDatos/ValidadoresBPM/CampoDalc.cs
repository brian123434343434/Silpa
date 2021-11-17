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
    public class CampoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public CampoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los diferentes campos de FormBuilder
        /// </summary>
        /// <returns>Conjunto de datos con los datos de los campos de FormBuilder</returns>
        public DataSet ListarCampo()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { };
                DbCommand cmd = db.GetStoredProcCommand("SPVA_CAMPO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Inserta un campo del FormBuilder en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los datos del campo</param>
        public void InsertarCampo(ref CampoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.IdCampo,objIdentity.DescripcionCampo,objIdentity.IdTipoDato
                        };

                DbCommand cmd = db.GetStoredProcCommand("SPVA_CREAR_CAMPO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Actualiza campo de formbuilder en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los datos del campo</param>
        public void EditarCampo(ref CampoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.IdCampo,objIdentity.DescripcionCampo,objIdentity.IdTipoDato
                        };

                DbCommand cmd = db.GetStoredProcCommand("SPVA_EDITAR_CAMPO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

    }
}
