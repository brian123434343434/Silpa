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
    public class TipoPersonaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoPersonaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad del tipo de persona
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipo de persona a cargar, en la propiedad ID del objetoIdentity</param>
        public TipoPersonaDalc(ref TipoPersonaIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.CodigoTipoPersona  };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_PERSONA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado != null)
                {
                    if (dsResultado.Tables.Count > 0)
                    {
                        objIdentity.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TPE_ID"]);
                        objIdentity.NombreTipoPersona = Convert.ToString(dsResultado.Tables[0].Rows[0]["TPE_NOMBRE"]);
                    }
                }

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Carga los valores para una identidad del tipo de persona cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del persona del tipo de dirección a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerTipoPersona(ref TipoPersonaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.CodigoTipoPersona };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_PERSONA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado != null)
                {
                    if (dsResultado.Tables.Count > 0)
                    {
                        objIdentity.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TPE_ID"]);
                        objIdentity.NombreTipoPersona = Convert.ToString(dsResultado.Tables[0].Rows[0]["TPE_NOMBRE"]);
                    }
                }
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Lista los tipos de persona en la BD.
        /// </summary>
        /// <param name="objIdentity.Id" >Valor del identificador del tipo de persona por el cual se filtraran los tipo de direccion.
        /// si es null no existen restricciones.</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  TPE_ID, TPE_NOMBRE, TPE_ACTIVO</returns>
        public DataSet ListarTipoPersona(Nullable<int> intTipoPersona)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intTipoPersona };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_PERSONA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista los tipos de persona en la BD.
        /// </summary>
        /// <param name="objIdentity.Id" >Valor del identificador del tipo de persona por el cual se filtraran los tipo de direccion.
        /// si es null no existen restricciones.</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  TPE_ID, TPE_NOMBRE, TPE_ACTIVO</returns>
        public DataSet ListarTipoPersona(Nullable<int> intTipoPersona, string formulario)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intTipoPersona, formulario };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_PERSONA_FORMULARIO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }

    }
}
