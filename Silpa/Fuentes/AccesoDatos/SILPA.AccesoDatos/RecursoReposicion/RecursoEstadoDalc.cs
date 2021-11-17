using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace SILPA.AccesoDatos.RecursoReposicion
{
    /// <summary>
    /// Acceso a Datos para Recurso de Reposición
    /// </summary>
    public class RecursoEstadoDalc
    {
        private Configuracion objConfiguracion;


        public RecursoEstadoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public RecursoEstadoEntity obtenerRecursoEstado(object [] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SIH_LISTAR_ESTADO_RECURSO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

             RecursoEstadoEntity recursoEstado;

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        recursoEstado  = new RecursoEstadoEntity();
                        recursoEstado.IDEstadoRecurso = Convert.ToInt32(dt["ID_ESTADO_RECURSO"]);
                        recursoEstado.Nombre = Convert.ToString(dt["SER_NOMBRE"]);
                        if (dt["SER_ACTIVO"] != DBNull.Value)
                            recursoEstado.activo= Convert.ToBoolean(dt["SER_ACTIVO"]);
                       
                        return recursoEstado;
                    }
                }
                return null;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }

    }
}