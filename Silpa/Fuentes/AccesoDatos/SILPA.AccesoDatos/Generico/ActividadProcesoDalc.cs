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
    public class ActividadProcesoDalc
    {
        private Configuracion objConfiguracion;
        
        public ActividadProcesoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<ActividadProcesoIdentity> ObtenerActividadesProcesos()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
            List<ActividadProcesoIdentity> lista = new List<ActividadProcesoIdentity>();
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_ACTIVIDAD_ESTADO");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                ActividadProcesoIdentity objIdentity;

                foreach (DataRow r in dsResultado.Tables[0].Rows)
                {
                    objIdentity = new ActividadProcesoIdentity();
                    if (r["GEN_ID_ACTIVIDAD_PROCESO"] != null)
                        objIdentity.IdActividadProceso = Int32.Parse(r["GEN_ID_ACTIVIDAD_PROCESO"].ToString());
                    if (r["GEN_ID_ACTIVIDAD"] != null)
                        objIdentity.IdActividad = Int32.Parse(r["GEN_ID_ACTIVIDAD"].ToString());
                    if (r["GEN_ID_ACTIVIDAD"] != null)
                    {
                        TipoEstadoProcesoDalc tipoEstadoProc = new TipoEstadoProcesoDalc();
                        objIdentity.IdEstadoProceso.Id = Int32.Parse(r["GEN_ID_ACTIVIDAD"].ToString());
                        TipoEstadoProcesoIdentity tipoEstadoProcId = new TipoEstadoProcesoIdentity();
                        tipoEstadoProcId = objIdentity.IdEstadoProceso;
                        tipoEstadoProc.ObtenerTipoEstadoProceso(ref tipoEstadoProcId);
                    }
                    if (r["GEN_FECHA_INSERCION"] != null)
                        objIdentity.FechaInsercion = DateTime.Parse(r["GEN_FECHA_INSERCION"].ToString());
                    if (r["GEN_DESCRIPCION"] != null)
                        objIdentity.Descripcion = Convert.ToString(r["GEN_DESCRIPCION"]);
                    lista.Add(objIdentity); 
                }
                return lista;
            }
            finally
            {
                db = null;
            }
        }

        public ActividadProcesoIdentity ObtenerActividadProceso(int id)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
            try
            {
                object[] parametros = new object[] {id};
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_ACTIVIDAD_ESTADO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                ActividadProcesoIdentity objIdentity = new ActividadProcesoIdentity();

                if (dsResultado.Tables[0].Rows[0]["GEN_ID_ACTIVIDAD_PROCESO"] != null) 
                    objIdentity.IdActividadProceso = Int32.Parse(dsResultado.Tables[0].Rows[0]["GEN_ID_ACTIVIDAD_PROCESO"].ToString());
                if (dsResultado.Tables[0].Rows[0]["GEN_ID_ACTIVIDAD"] != null)
                    objIdentity.IdActividad = Int32.Parse(dsResultado.Tables[0].Rows[0]["GEN_ID_ACTIVIDAD"].ToString());
                if (dsResultado.Tables[0].Rows[0]["GEN_ID_ACTIVIDAD"] != null)
                { 
                    TipoEstadoProcesoDalc tipoEstadoProc = new TipoEstadoProcesoDalc();
                    objIdentity.IdEstadoProceso.Id = Int32.Parse(dsResultado.Tables[0].Rows[0]["GEN_ID_ACTIVIDAD"].ToString());
                    TipoEstadoProcesoIdentity tipoEstadoProcId = new TipoEstadoProcesoIdentity();
                    tipoEstadoProcId  = objIdentity.IdEstadoProceso;
                    tipoEstadoProc.ObtenerTipoEstadoProceso(ref tipoEstadoProcId); 
                }
                if (dsResultado.Tables[0].Rows[0]["GEN_FECHA_INSERCION"] != null)
                    objIdentity.FechaInsercion = DateTime.Parse(dsResultado.Tables[0].Rows[0]["GEN_FECHA_INSERCION"].ToString());
                if (dsResultado.Tables[0].Rows[0]["GEN_DESCRIPCION"] != null)
                    objIdentity.Descripcion = dsResultado.Tables[0].Rows[0]["GEN_DESCRIPCION"].ToString();
                return objIdentity;
            }
            finally 
            {
                db = null;
            }
        }
        
        public void AgregarActividadProceso(ActividadProcesoIdentity actProc)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
            try
            {
                object[] parametros = new object[] { actProc.IdActividad, actProc.IdEstadoProceso.Id, actProc.Descripcion };
                DbCommand cmd = db.GetStoredProcCommand("GEN_INSERT_ACTIVIDAD_ESTADO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                db = null;
            }
        }

        public void EditarActividadProceso(ActividadProcesoIdentity actProc)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
            try
            {
                object[] parametros = new object[] { actProc.IdActividadProceso, actProc.IdActividad, actProc.IdEstadoProceso.Id, actProc.FechaInsercion, actProc.Descripcion };
                DbCommand cmd = db.GetStoredProcCommand("GEN_UPDATE_ACTIVIDAD_ESTADO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                db = null;
            }
        }

        public void EliminarActividadProceso(ActividadProcesoIdentity actProc)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
            try
            {
                object[] parametros = new object[] { actProc.IdActividadProceso };
                DbCommand cmd = db.GetStoredProcCommand("GEN_DELETE_ACTIVIDAD_ESTADO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                db = null;
            }
        }
    }
}
