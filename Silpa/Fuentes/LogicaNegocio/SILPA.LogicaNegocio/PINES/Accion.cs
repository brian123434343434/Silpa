using SILPA.AccesoDatos.PINES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.PINES
{
    public class Accion
    {
        public AccionDALC vAccionDALC;
        public Accion()
        {
            vAccionDALC = new AccionDALC();
        }
        public void Insertar(AccionIdentity pAccionIdentity)
        {
            try
            {
                vAccionDALC.Insertar(pAccionIdentity);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Actualizar(AccionIdentity pAccionIdentity)
        {
            try
            {
                vAccionDALC.Actualizar(pAccionIdentity);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void Eliminar(AccionIdentity pAccionIdentity)
        {
            try
            {
                vAccionDALC.Eliminar(pAccionIdentity);

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
                return vAccionDALC.ListaAcciones(pNombreAccion);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}
