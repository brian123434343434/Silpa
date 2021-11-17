using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;

namespace SILPA.AccesoDatos.Notificacion
{
    class DependenciaEntidadDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public DependenciaEntidadDalc()
        {
            objConfiguracion = new Configuracion(); 
        }
        //NOT_LISTAR_DEPENDENCIA_ENTIDAD
        public List<DependenciaEntidadEntity> ObtenerDependencias()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_DEPENDENCIA_ENTIDAD");
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            List<DependenciaEntidadEntity> lista;
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<DependenciaEntidadEntity>();
                    DependenciaEntidadEntity _dependencia = new DependenciaEntidadEntity();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        _dependencia.ID = dt["DEN_ID"].ToString();
                        _dependencia.Nombre = dt["DEN_NOMBRE"].ToString();
                        lista.Add(_dependencia);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }
        public DependenciaEntidadEntity ObtenerDependencia(int codigoDep)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DataSet dsResultado = new DataSet();

            try
            {
	            object[] parametros = new object[] { codigoDep };
	            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_DEPENDENCIA_ENTIDAD", parametros);
	            dsResultado = db.ExecuteDataSet(cmd);
	            List<DependenciaEntidadEntity> lista;
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<DependenciaEntidadEntity>();
                    DependenciaEntidadEntity _dependencia = new DependenciaEntidadEntity();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        _dependencia.ID = dt["DEN_ID"].ToString();
                        _dependencia.Nombre = dt["DEN_NOMBRE"].ToString();
                        return _dependencia;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al obtener Dependencia.";
                throw new Exception(strException, ex);
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }
    }
}
