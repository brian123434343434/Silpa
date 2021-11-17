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
    /// Clase para el manejo de datos de las areas hidrograficas
    /// </summary>
    public class AreaHidrograficaDalc
    {

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public AreaHidrograficaDalc()
        {
            objConfiguracion = new Configuracion();

        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de Area Hidrografica 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del Area Hidrografica a cargar, en la propiedad ID del objetoIdentity</param>
        public AreaHidrograficaDalc(ref AreaHidrograficaIndenitty objIdentity)
        {
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AREA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AHI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["AHI_NOMBRE"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Carga los valores para una identidad de Area Hidrografic cuyo valor del identificador corresponda 
        /// con la BD        
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del Area Hidrografica a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerAreaHidrografica(ref AreaHidrograficaIndenitty objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AREA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AHI_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["AHI_NOMBRE"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Lista las Areas Hidrograficas en la BD. Pueden listarse todas o una en particular.
        /// </summary>
        /// <param name="objIdentity.Id" >Con este valor se lista el Area Hidrografica con el identificador, si es null se listan todas</param>
        /// <returns>DataSet con los registros y las siguientes columnas: AHI_ID, AHI_NOMBRE, AHI_ACTIVO</returns>
        public DataSet ListarAreaHidrografica(AreaHidrograficaIndenitty objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { objIdentity.Id };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AREA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista las Areas Hidrograficas en la BD. Pueden listarse todas o una en particular.
        /// </summary>
        /// <param name="Id" >Con este valor se lista el Area Hidrografica con el identificador, si es null se listan todas</param>
        /// <returns>DataSet con los registros y las siguientes columnas: AHI_ID, AHI_NOMBRE, AHI_ACTIVO</returns>
        public DataSet ListarAreaHidrografica(Nullable<int> IntIdAreaHidro)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IntIdAreaHidro };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AREA_HIDROGRAFICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }


        /// <summary>
        /// Lista las Areas Hidrograficas relacionadas a una zona hidrografica especifica.
        /// </summary>
        /// <param name="Id" >Con este valor se lista el Area Hidrografica con el identificador, si es null se listan todas</param>
        /// <returns>DataSet con los registros y las siguientes columnas: AHI_ID, AHI_NOMBRE</returns>
        public DataSet ListarAreaHidrograficaZona(int IntIdZonaHidro)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IntIdZonaHidro };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_AREA_HIDROGRAFICA_ZONA", parametros);
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