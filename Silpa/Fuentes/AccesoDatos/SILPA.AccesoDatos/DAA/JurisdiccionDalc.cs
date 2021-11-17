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

namespace SILPA.AccesoDatos.DAA
{
    public class JurisdiccionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public JurisdiccionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Método que inserta una jurisdicción en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto con los datos de la jurisdicción</param>
        public void InsertarJurisdiccion(ref JurisdiccionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {
                    objIdentity.IdMunicipio,objIdentity.AutoridadAmbiental
                };
                DbCommand cmd = db.GetStoredProcCommand("DAA_CREAR_JURISDICCION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Método que retorna el listado de jurisdicciones o solo una jurisdiccion
        /// </summary>
        /// <param name="_idMunicipio">Identificador del municipio</param>
        /// <returns>conjunto de datos: [JUR_ID] - [AUT_NOMBRE] - [MUN_NOMBRE] - [FECHA_INSERCION]</returns>
        public DataSet ListaJurisdiccion(Nullable<Int64> _idMunicipio)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {_idMunicipio};
                DbCommand cmd = db.GetStoredProcCommand("DAA_LISTA_JURISDICCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        


    }
}
