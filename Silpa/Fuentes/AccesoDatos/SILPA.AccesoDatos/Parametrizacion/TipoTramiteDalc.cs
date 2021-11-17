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

namespace SILPA.AccesoDatos.Parametrizacion
{
    public class TipoTramiteDalc
    {
        private Configuracion objConfiguracion = new Configuracion();

        /// <summary>
        /// metodo que permite agregra un  registro a la base de datos
        /// con la informacion nueva de un tramite de tramite
        /// </summary>
        /// <param name="ser"> TipoTramite</param>
        public void Agregar(TipoTramite tramite)
        {
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { tramite.Nombre, tramite.TipoProceso };
                    DbCommand cmd = db.GetStoredProcCommand("GEN_INSERT_TIPO_TRAMITE", parametros);
                    db.ExecuteNonQuery(cmd);
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
        /// <param name="ser">TipoTramite</param>
        public void Eliminar(TipoTramite tramite)
        {
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { tramite.Id };
                    DbCommand cmd = db.GetStoredProcCommand("GEN_DELETE_TIPO_TRAMITE", parametros);
                    db.ExecuteNonQuery(cmd);
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
        /// <param name="ser">TipoTramite</param>
        public void Actualizar(TipoTramite tramite)
        {
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { tramite.Id, tramite.Nombre, tramite.TipoProceso };
                    DbCommand cmd = db.GetStoredProcCommand("GEN_UPDATE_TIPO_TRAMITE", parametros);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Listar, tiene la capacidad de retornar un LIST<TipoTramite> del listado de 
        /// servicios almacenados en la base de datos
        /// </summary>
        /// <returns>List<TipoTramite></returns>
        public List<TipoTramite> Listar()
        {
            List<TipoTramite> tramite = new List<TipoTramite>();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_TIPO_TRAMITE");
                DataTable dtResultado = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dtResultado.Rows)
                {
                    TipoTramite tipotramite = new TipoTramite();
                    tipotramite.Id = Convert.ToInt32(r["ID"]);
                    tipotramite.Nombre = r["NOMBRE_TIPO_TRAMITE"].ToString();
                    tipotramite.TipoProceso =  Convert.ToInt32(r["ID_TIPO_PROCESO"]);
                    tipotramite.InicioProceso = Convert.ToInt32(r["INICIO_PROCESO"]);
                    tramite.Add(tipotramite);
                }
                return tramite;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      
        /// <summary>
        /// Listar, tiene la capacidad de retornar un TipoTramite encontrado
        /// a partir del identificador del servicio ingresado al metodo
        /// </summary>
        /// <returns>TipoTramite</returns>
        public TipoTramite ListarTramites(int idTramite)
        {
            TipoTramite tramite = null;
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idTramite };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_TIPO_TRAMITE", parametros);
                DataTable dtResultado = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dtResultado.Rows)
                {
                    tramite = new TipoTramite();
                    tramite.Id = Convert.ToInt32(r["ID"]);
                    tramite.Nombre = r["NOMBRE_TIPO_TRAMITE"].ToString();
                    tramite.TipoProceso =  Convert.ToInt32(r["ID_TIPO_PROCESO"]);                  
                }
                return tramite;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Busca el trámite por caso de proceso
        /// </summary>
        /// <param name="idCaso">Número de Caso de Proceso de BPM</param>
        /// <returns></returns>
        public TipoTramite BuscarTramitePorCaso(int idCaso)
        {
            TipoTramite tramite = null;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { null, null, idCaso };
            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_TIPO_TRAMITE", parametros);
            try
            {
                
                DataTable dtResultado = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dtResultado.Rows)
                {
                    tramite = new TipoTramite();
                    tramite.Id = Convert.ToInt32(r["ID"]);
                    tramite.Nombre = r["NOMBRE_TIPO_TRAMITE"].ToString();
                    tramite.TipoProceso = Convert.ToInt32(r["ID_TIPO_PROCESO"]);
                }
                return tramite;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

    }
}
