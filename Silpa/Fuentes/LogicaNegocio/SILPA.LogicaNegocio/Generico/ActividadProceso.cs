using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;

namespace SILPA.LogicaNegocio.Generico
{
    public class ActividadProceso
    {
        public List<AccesoDatos.Generico.ActividadProcesoIdentity> ListarActividadProceso()
        {
            AccesoDatos.Generico.ActividadProcesoDalc dalc = new ActividadProcesoDalc();
            try
            {
                return dalc.ObtenerActividadesProcesos();
            }
            finally
            {
                dalc = null;
            }
        }

        public AccesoDatos.Generico.ActividadProcesoIdentity ActividadProcesos(int id)
        {
            AccesoDatos.Generico.ActividadProcesoDalc dalc = new ActividadProcesoDalc();
            try
            {
                return dalc.ObtenerActividadProceso(id);
            }
            finally
            {
                dalc = null;
            }
        }

        public void Agregar(AccesoDatos.Generico.ActividadProcesoIdentity entidad )
        {
            AccesoDatos.Generico.ActividadProcesoDalc dalc = new ActividadProcesoDalc();
            try
            {
                dalc.AgregarActividadProceso(entidad); 
            }
            finally
            {
                dalc = null;
            }
        }

        public void Editar(AccesoDatos.Generico.ActividadProcesoIdentity entidad)
        {
            AccesoDatos.Generico.ActividadProcesoDalc dalc = new ActividadProcesoDalc();
            try
            {
                dalc.EditarActividadProceso(entidad);
            }
            finally
            {
                dalc = null;
            }
        }

        public void Eliminar(AccesoDatos.Generico.ActividadProcesoIdentity entidad)
        {
            AccesoDatos.Generico.ActividadProcesoDalc dalc = new ActividadProcesoDalc();
            try
            {
                dalc.EliminarActividadProceso(entidad);
            }
            finally
            {
                dalc = null;
            }
        }
    }
}
