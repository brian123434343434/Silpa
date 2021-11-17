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
    public class ValidacionCampoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public ValidacionCampoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista los campos con sus validaciones en Formbuilder
        /// </summary>
        /// <returns>Conjunto de datos con los campos y sus validaciones en FormBuilder</returns>
        public DataSet ListarValidacionCampo()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { };
                DbCommand cmd = db.GetStoredProcCommand("SPVA_VALIDACION_CAMPO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Inserta la relacion de un campo y una validacion del FormBuilder en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los datos de la relacion campo y validacion</param>
        public void InsertarValidacionCampo(ref ValidacionCampoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.IdCampo, objIdentity.IdValidacion, objIdentity.ActivoValidacionCampo
                        };

                DbCommand cmd = db.GetStoredProcCommand("SPVA_CREAR_VALIDACION_CAMPO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Actualiza la validacion de un campo de formBuilder
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los datos la validacion del campo de FormBuilder</param>
        public void EditarValidacionCampo(ref ValidacionCampoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                       {
                           objIdentity.IdValidacionCampo,objIdentity.IdCampo,objIdentity.IdValidacion,
                           objIdentity.FechaInsercion,objIdentity.ActivoValidacionCampo
                        };

                DbCommand cmd = db.GetStoredProcCommand("SPVA_EDITAR_VALIDACION_CAMPO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

    }
}
