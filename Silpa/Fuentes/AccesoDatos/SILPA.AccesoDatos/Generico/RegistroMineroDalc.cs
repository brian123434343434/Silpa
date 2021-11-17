using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    public class RegistroMineroDalc
    {
        private string silpaConnection;

        /// <summary>
        /// Contructor de  la clases
        /// </summary>
        public RegistroMineroDalc()
        {
            silpaConnection = ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString;
        }


        /// <summary>
        /// Obtiene el listado de los registros mineros creados, modificados y eliminados a partir de la fecha de busqueda enviada 
        /// </summary>
        /// <param name="fechaConsulta">DateTime con la fecha de busqueda</param>
        /// <returns>dataset con la informacion de los registros mineros encontrados</returns>
        public DataSet ObtenerInformacionBaseRegistrosMineros(DateTime fechaConsulta) 
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { fechaConsulta };
                DbCommand cmd = db.GetStoredProcCommand("RMH_CONSULTAR_INFO_REGISTRO_MINERO", parametros);
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la información de las coordenadas de localizacion de un registro minero especifico 
        /// </summary>
        /// <param name="idRegistroMinero">int identificador del registro minero</param>
        /// <returns>dataset con las coordenadas del registro minero</returns>
        public DataSet ObtenerCoordenadasLocalizacionRegistroMinero(int idRegistroMinero) 
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { idRegistroMinero };
                DbCommand cmd = db.GetStoredProcCommand("RMH_CONSULTAR_COORD_LOCALIZACION_REGISTRO_MINERO", parametros);
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene la información de los titulares de un registro minero especifico 
        /// </summary>
        /// <param name="idRegistroMinero">int identificador del registro minero</param>
        /// <returns>dataset con la informacion de los titulares del registro minero</returns>
        public DataSet ObtenerTitularesRegistroMinero(int idRegistroMinero)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(silpaConnection);
                object[] parametros = new object[] { idRegistroMinero };
                DbCommand cmd = db.GetStoredProcCommand("RMH_CONSULTAR_TITULARES_REGISTRO_MINERO", parametros);
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
