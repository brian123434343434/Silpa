using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SoftManagement.Persistencia
{
    public static class Contexto
    {
        /// <summary>
        /// metodo encargado de realizar el cargue de un dataset
        /// </summary>
        /// <param name="ds">dataset en el que sera almacenado los valores de la tabla</param>
        /// <param name="nombreTabla">string con el nombre de la tabla</param>
        public static void cargarTabla(DataSet ds,string nombreTabla)
        {
            SILPA.Comun.Configuracion conf = new SILPA.Comun.Configuracion();

            SoftManagement.Persistencia.Conexion cnn = new Conexion(conf.SilpaCnx.ToString(), ds, nombreTabla);
            cnn.cargar ();
        }
        /// <summary>
        /// Metodo encargado de realizar el cargue de la tabla con parametros especificos
        /// </summary>
        /// <param name="ParametrosConsulta">lista con los parametros de tipo SqlParameter</param>
        /// <param name="ds">DataSet a cargar con el metodo cargar</param>
        /// <param name="nombreTabla">nombre tabla a cargar</param>
        public static void cargarTabla(List <SqlParameter> ParametrosConsulta ,DataSet ds, string nombreTabla)
        {
            SILPA.Comun.Configuracion conf = new SILPA.Comun.Configuracion();
            SoftManagement.Persistencia.Conexion cnn = new Conexion(conf.SilpaCnx.ToString(), ds, nombreTabla);
            cnn.cargar(ParametrosConsulta);
        }
        /// <summary>
        /// metodo que permite registrar los cabios realizados en los registros cargados
        /// </summary>
        /// <param name="ds">dataset a guardar</param>
        /// <param name="nombreTabla">string con el nombre de la tabla a guardar</param>
        public static void guardarTabla(DataSet ds, string nombreTabla)
        {
            SILPA.Comun.Configuracion conf = new SILPA.Comun.Configuracion();
            SoftManagement.Persistencia.Conexion cnn = new Conexion(conf.SilpaCnx.ToString(), ds, nombreTabla);
            cnn.RegistrarCambios();

        }

    }
}
