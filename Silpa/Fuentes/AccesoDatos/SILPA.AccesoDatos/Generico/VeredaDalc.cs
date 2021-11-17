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
    /// Clase encargada de la manipulación de los datos de las veredas
    /// </summary>
    public class VeredaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public VeredaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de Vereda 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la vereda a cargar, en la propiedad ID del objetoIdentity</param>
        public VeredaDalc(ref VeredaIndentity objIdentity)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.CorregimientoId, objIdentity.Id };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_VEREDA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["VER_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["VER_NOMBRE"]);
                objIdentity.MunicipioId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
                objIdentity.CorregimientoId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COR_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Carga los valores para una identidad de vereda cuyo valor del identificador corresponda 
        /// con la BD        
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la vererda a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerVereda(ref VeredaIndentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.CorregimientoId, objIdentity.Id };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_VEREDA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["VER_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["VER_NOMBRE"]);
                objIdentity.MunicipioId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
                objIdentity.CorregimientoId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COR_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las veredas en la BD. Pueden listarse los municipios por departamento, regional o un municipio en particular.
        /// </summary>
        /// <param name="objIdentity.MunicipioId" >Valor del identificador del municipio por el que se filtraran los municipios, si es null no existen restricciones</param>
        /// <param name="objIdentity.CorregimientoId" >Valor del identificador del corregimiento por el que se filtraran los municipios, si es null no existen restricciones</param>
        /// <param name="objIdentity.Id" >Con este valor se lista la vereda con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  VER_ID, VER_NOMBRE, MUN_ID, MUN_NOMBRE, COR_ID, COR_NOMBRE</returns>
        public DataSet ListarVeredas(VeredaIndentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.CorregimientoId, objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_VEREDA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista las veredas en la BD. Pueden listarse los municipios por departamento, regional o un municipio en particular.
        /// </summary>
        /// <param name="IntMunicipioId" >Valor del identificador del municipio por el que se filtraran los municipios, si es null no existen restricciones</param>
        /// <param name="intCorregimientoId" >Valor del identificador del corregimiento por el que se filtraran los municipios, si es null no existen restricciones</param>
        /// <param name="intId" >Con este valor se lista la vereda con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  VER_ID, VER_NOMBRE, MUN_ID, MUN_NOMBRE, COR_ID, COR_NOMBRE</returns>
        public DataSet ListarVeredas(Nullable<int> IntMunicipioId, Nullable<int> intCorregimientoId, Nullable<int> intId)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { IntMunicipioId, intCorregimientoId, intId };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_VEREDA", parametros);
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
