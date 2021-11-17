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
    public class SectorDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public SectorDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de sub zona hidrologica
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la sub zona hidrologica a cargar, en la propiedad ID del objetoIdentity</param>
        public SectorDalc(ref SectorIdentity objIdentity)
        {
            
            try
            {

                objConfiguracion = new Configuracion();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id, objIdentity.IdPadre};
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SECTOR", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                string xml = dsResultado.GetXml();
                if (dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count >0)
                {
                    objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SEC_ID"]);
                    objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["SEC_NOMBRE"]);
                    objIdentity.IdPadre = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SEC_PADRE_ID"]);
                    objIdentity.PerteneceMAVDT = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SEC_CORRESPONDE_MAVDT"]);
                    objIdentity.RequiereDAA = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SEC_REQUIERE_DAA"]);
                    objIdentity.TieneDAATDRFijos = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SEC_DAATDR_FIJOS"]);
                    objIdentity.UrlDAATDR = Convert.ToString(dsResultado.Tables[0].Rows[0]["SEC_URL_DAATDR"]);
                }
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Carga los valores para una identidad del sector o tipo proyecto cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del sector o proyecto a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerSector(ref SectorIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SECTOR", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SEC_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["SEC_NOMBRE"]);
                objIdentity.IdPadre = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SEC_PADRE_ID"]);
                objIdentity.PerteneceMAVDT = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SEC_CORRESPONDE_MAVDT"]);
                objIdentity.RequiereDAA = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SEC_REQUIERE_DAA"]);
                objIdentity.TieneDAATDRFijos = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["SEC_DAATDR_FIJOS"]);
                objIdentity.UrlDAATDR= Convert.ToString(dsResultado.Tables[0].Rows[0]["SEC_URL_DAATDR"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Lista los sectores o proyectos en la BD. Pueden listarse los sectores o proyectos.
        /// </summary>
        /// <param name="intSectorID" >Valor del identificador de la zona hidrografica por el cual se filtraran las sub zonas hidrograficas, 
        /// si es null no existen restricciones. </param>
        /// <param name="intTipoProyecto" >Con este valor se lista la sub zonas hidrograficas con el identificador, si es null no existen restricciones, Si -1 lista solo los sectores.</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  SEC_ID, SEC_NOMBRE</returns>
        public DataSet ListarSector(Nullable<int> intTipoProyecto, Nullable<int> intSectorID)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intTipoProyecto, intSectorID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SECTOR", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }

        /// <summary>
        /// Lista los sectores en la BD. Pueden listarse los sectores.
        /// </summary>
        /// <returns>DataSet con los registros y las siguientes columnas:  SEC_ID, SEC_NOMBRE</returns>
        public DataSet ListarSector()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { null, -1};
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_SECTOR", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }

    }
}
