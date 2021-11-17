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
    public class MetodoDalc
    {
        private Configuracion objConfiguracion = new Configuracion();

       

        /// <summary>
        /// metodo que permite agregra un  registro a la base de datos
        /// con la infotmacion nueva de un metodo
        /// </summary>
        /// <param name="ser"> Metodo</param>
        public void Agregar(Metodo met)
        {
            { 
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { met.Ser.Id , met.Nombre, met.Activo};
                    DbCommand cmd = db.GetStoredProcCommand("WSB_INSERT_METODO", parametros);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Metodo con la capacidad de eliminar un registro de Metodos de la
        /// base de datos
        /// </summary>
        /// <param name="ser">Metodo</param>
        public void Eliminar(Metodo met)
        {
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { met.Id};
                    DbCommand cmd = db.GetStoredProcCommand("WSB_DELETE_METODO", parametros);
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
        /// <param name="ser">Metodo</param>
        public void Actualizar(Metodo met)
        {
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { met.Id, met.Ser.Id, met.Nombre, met.Activo};
                    DbCommand cmd = db.GetStoredProcCommand("WSB_UPDATE_METODO", parametros);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        
        /// <summary>
        /// Listar, tiene la capacidad de retornar un LIST<Metodo> del listado de 
        /// servicios almacenados en la base de datos
        /// </summary>
        /// <returns>List<Metodo></returns>
        public List<Metodo> Listar()
        {
            List<Metodo> met = new List<Metodo>();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("WSB_LISTA_METODO");
                DataTable dtResultado = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dtResultado.Rows)
                {
                    Metodo metodo = new Metodo();
                    ServicioDalc serv = new ServicioDalc();  
                    metodo.Id = Convert.ToInt32(r["WSB_ID_METODO"]);
                    metodo.Ser = serv.ListarServicio(Convert.ToInt32(r["WSB_ID_SERVICIO"]));
                    metodo.Nombre = r["WSB_NOMBRE_METODO"].ToString();
                    metodo.Activo = Convert.ToInt32(r["WSB_ACTIVO"]);
                    met.Add(metodo); 
                }
                return met;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Listar, tiene la capacidad de retornar un Metodo encontrado
        /// a partir del identificador del Metodo o servicio ingresado al metodo
        /// </summary>
        /// <returns>Servicio</returns>
        public List<Metodo> ListarMetodo(string[] param)
        {
            List<Metodo> met = new List<Metodo>();
            int idMetodo = -1;
            int idServicio = -1;
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                idMetodo = Convert.ToInt32(param[0]);
                if (param.Length > 1)
                    idServicio = Convert.ToInt32(param[1]);

                object[] parametros = new object[] { idMetodo, idServicio };
                
                DbCommand cmd = db.GetStoredProcCommand("WSB_LISTA_METODO", parametros);
                DataTable dtResultado = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dtResultado.Rows)
                {
                    Metodo metodo = new Metodo();
                    ServicioDalc serv = new ServicioDalc();
                    metodo.Id = Convert.ToInt32(r["WSB_ID_METODO"]);
                    metodo.Ser = serv.ListarServicio(Convert.ToInt32(r["WSB_ID_SERVICIO"]));
                    metodo.Nombre = r["WSB_NOMBRE_METODO"].ToString();
                    metodo.Activo = Convert.ToInt32(r["WSB_ACTIVO"]);
                    met.Add(metodo);
                }
                return met;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
