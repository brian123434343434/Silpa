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
    public class BpmParametros
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public BpmParametros()
        {
            objConfiguracion = new Configuracion();
        }
        /// <summary>
        /// Lista de parametros segun los filtros especificados
        /// </summary>
        /// <param name="intTipo">Define el tipo de parametro 1.=Actividad, 2.=Condiciones, 3.=Formularios </param>
        /// <param name="strNombre">Realiza un filtro en la busqueda por el nombre de parametro</param>
        /// <param name="Codigo">Realiza un filtro en la busqueda por el codigo de parametro </param>
        /// <returns>Retorna un dataset con un listado de los parametros con las siguientes 
        /// columnas -> [ID - TIPO - NOMBRE - CODIGO]</returns>
        public DataSet ListarBmpParametros(Nullable<int> intTipo, string strNombre, Nullable<int> Codigo)
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intTipo, Codigo, strNombre };
                DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_PARAMETRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
        }

        /// <summary>
        /// Lista de parametros segun los filtros especificados
        /// </summary>
        /// <param name="intTipo">Define el tipo de parametro 1.=Actividad, 2.=Condiciones, 3.=Formularios </param>
        /// <param name="strNombre">Realiza un filtro en la busqueda por el nombre de parametro</param>
        /// <returns>Retorna un dataset con un listado de los parametros con las siguientes 
        /// columnas -> [ID - TIPO - NOMBRE - CODIGO]</returns>
        public DataSet ListarBmpParametros(Nullable<int> intIdParametro)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdParametro, null, null, null };
            DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_PARAMETRO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }

        /// <summary>
        /// Lista el parámetro encontrado para el código
        /// </summary>
        /// <param name="idTipo">Define el tipo de parametro 1.=Actividad, 2.=Condiciones, 3.=Formularios </param>
        /// <param name="codigo">Código</param>
        /// <returns>Dataset con el Resultado</returns>
        public DataSet ListarBmpParametros(int? idTipo, int? codigo)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { null, idTipo, null, codigo };
            DbCommand cmd = db.GetStoredProcCommand("BPM_LISTA_PARAMETRO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }
        
    }
}
