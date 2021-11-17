using SILPA.AccesoDatos.PINES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.PINES
{
    public class AccionActividad
    {
        public AccionActividadDALC vAccionActividadDALC;
        public AccionActividad()
        {
            vAccionActividadDALC = new AccionActividadDALC();
        }

        public void Insertar(AccionActividadIdentity pAccionActividadIdentity)
        {
            try
            {
                vAccionActividadDALC.Insertar(pAccionActividadIdentity);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Actualizar(AccionActividadIdentity pAccionActividadIdentity)
        {
            try
            {
                vAccionActividadDALC.Actualizar(pAccionActividadIdentity);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Consultar(ref AccionActividadIdentity vAccionActividadIdentity)
        {
            try
            {
                vAccionActividadDALC.Consultar(ref vAccionActividadIdentity);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Eliminar(AccionActividadIdentity pAccionActividadIdentity)
        {
            try
            {
                vAccionActividadDALC.Eliminar(pAccionActividadIdentity);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ConsultaEstadoActivityProcessInstance(int? intIdProcessInstance, int? intIdActivity)
        {
            try
            {
                return vAccionActividadDALC.ConsultaEstadoActivityProcessInstance(intIdProcessInstance, intIdActivity);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}
