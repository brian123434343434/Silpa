using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.PINES
{
    public class AccionDALC
    {
        private Configuracion objConfiguracion;

        public AccionDALC()
        {
            objConfiguracion = new Configuracion();
        }
        public void Insertar(AccionIdentity vAccionIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_INSERTAR_ACCION");
                db.AddInParameter(cmd, "P_DESCRIPCION", DbType.String, vAccionIdentity.NombreAccion);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Eliminar(AccionIdentity vAccionIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_ELIMINAR_ACCION");
                db.AddInParameter(cmd, "P_IDACCION", DbType.Int32, vAccionIdentity.IDAccion);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Actualizar(AccionIdentity vAccionIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_ACTUALIZAR_ACCION");
                db.AddInParameter(cmd, "P_DESCRIPCION", DbType.String, vAccionIdentity.NombreAccion);
                db.AddInParameter(cmd, "P_IDACCION", DbType.Int32, vAccionIdentity.IDAccion);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<AccionIdentity> ListaAcciones(string pNombreAccion)
        {
            try
            {
                List<AccionIdentity> LstAcciones = new List<AccionIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("PINSP_LISTA_ACCION");
                db.AddInParameter(cmd, "P_DESCRIPCION", DbType.String, pNombreAccion);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        AccionIdentity vAccionIdentity = new AccionIdentity {
                            IDAccion = Convert.ToInt32(reader["IDACCION"]),
                            NombreAccion = reader["DESCRIPCION"].ToString()
                        };
                        LstAcciones.Add(vAccionIdentity);
                    }
                }
                return LstAcciones;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}
