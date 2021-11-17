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
    public class RecursoDocumentoDalc
    {
        private Configuracion objConfiguracion;


        public RecursoDocumentoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Crea un Recurso de Reposición
        /// </summary>
        /// <param name="objRecurso">Objeto con datos del Recurso</param>
        public void InsertarRecursoDocumento(RecursoDocumentoEntity objRecurso)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {objRecurso.RutaDocumento, objRecurso.IDRadicado, objRecurso.IdRecursoReposicion};
            DbCommand cmd = db.GetStoredProcCommand("SIH_CREAR_RECURSO_DOCUMENTO", parametros);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// Actualiza un Recurso de Reposición
        /// </summary>
        /// <param name="objRecurso">Objeto con los Datos del Recurso</param>
        public void ActualizarRecursoDocumento(RecursoDocumentoEntity objRecurso)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { };
            DbCommand cmd = db.GetStoredProcCommand("", parametros);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public RecursoDocumentoEntity ObtenerRecursoDocumento(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SIH_LISTAR_RECURSO_DOCUMENTO", parametros);
            RecursoDocumentoEntity recursoDocumento = new RecursoDocumentoEntity();
            
            try
            {
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        recursoDocumento = construirRecursoDocumento(ds.Tables[0].Rows[0]);
                    }
                }
                return recursoDocumento;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public List<RecursoDocumentoEntity> ObtenerRecursoDocumentos(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SIH_LISTAR_RECURSO_DOCUMENTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<RecursoDocumentoEntity> lista;
            RecursoDocumentoEntity recursoDocumento;

            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<RecursoDocumentoEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        recursoDocumento = construirRecursoDocumento(dt);                   
                        lista.Add(recursoDocumento);
                    }
                    return lista;
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
        private RecursoDocumentoEntity construirRecursoDocumento(DataRow dt)
        {
            RecursoDocumentoEntity ret = new RecursoDocumentoEntity();
            ret.IDRecursoDocumento = Convert.ToDecimal(dt["ID_RECURSO_DOCUMENTO"]);
            ret.RutaDocumento = (String) dt["SDO_RUTA_DOCUMENTO"];
            if(dt["ID_RADICACION"] != null)
                ret.IDRadicado = Convert.ToDecimal(dt["ID_RADICACION"]);
            ret.IdRecursoReposicion = Convert.ToDecimal(dt["ID_RECURSO_REPOSICION"]);
            
            return ret;
        }


    }
}
