using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data;
using System.Data.Common; 
using System.Data.SqlClient;


namespace SILPA.AccesoDatos.ListaServicios
{
    public class ServicioDalc
    {
        private Configuracion objConfiguracion = new Configuracion();

        /// <summary>
        /// metodo que permite agregra un  registro a la base de datos
        /// con la infotmacion nueva de un Servicio
        /// </summary>
        /// <param name="ser"> Servicio</param>
        public void Agregar(Servicio ser)
        {
            { 
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { ser.Nombre, ser.Url, ser.Activo };
                    DbCommand cmd = db.GetStoredProcCommand("WSB_INSERT_SERVICIO", parametros);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Metodo con la capacidad de eliminar un registro de servicios de la
        /// base de datos
        /// </summary>
        /// <param name="ser">Servicio</param>
        public void Eliminar(Servicio ser)
        {
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { ser.Id};
                    DbCommand cmd = db.GetStoredProcCommand("WSB_DELETE_SERVICIO", parametros);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Metodo con la capacidad de actualizar los registros de base de datos
        /// </summary>
        /// <param name="ser">Servicio</param>
        public void Actualizar(Servicio ser)
        {
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { ser.Id, ser.Nombre, ser.Url, ser.Activo};
                    DbCommand cmd = db.GetStoredProcCommand("WSB_UPDATE_SERVICIO", parametros);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        
        /// <summary>
        /// Listar, tiene la capacidad de retornar un LIST<Servicio> del listado de 
        /// servicios almacenados en la base de datos
        /// </summary>
        /// <returns>List<Servicio></returns>
        public List<Servicio> Listar()
        {
            List<Servicio> ser = new List<Servicio>();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("WSB_LISTA_SERVICIO");
                DataTable dtResultado = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dtResultado.Rows)
                {
                    Servicio servicio = new Servicio();
                    servicio.Id = Convert.ToInt32(r["WSB_ID"]);
                    servicio.Nombre = r["WSB_NOMBRE_SERVICIO"].ToString();
                    servicio.Url = r["WSB_URL"].ToString();
                    servicio.Activo = Convert.ToInt32(r["WSB_ACTIVO"]);
                    ser.Add(servicio); 
                }
                return ser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Listar, tiene la capacidad de retornar un Servicio encontrado
        /// a partir del identificador del servicio ingresado al metodo
        /// </summary>
        /// <returns>Servicio</returns>
        public Servicio ListarServicio(int idSer)
        {
            Servicio servicio = null;
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {idSer};
                DbCommand cmd = db.GetStoredProcCommand("WSB_LISTA_SERVICIO", parametros);
                DataTable dtResultado = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dtResultado.Rows)
                {
                    servicio = new Servicio();
                    servicio.Id = Convert.ToInt32(r["WSB_ID"]);
                    servicio.Nombre = r["WSB_NOMBRE_SERVICIO"].ToString() ;
                    servicio.Url = r["WSB_URL"].ToString()  ;
                    servicio.Activo = Convert.ToInt32(r["WSB_ACTIVO"]);
                }
                return servicio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
