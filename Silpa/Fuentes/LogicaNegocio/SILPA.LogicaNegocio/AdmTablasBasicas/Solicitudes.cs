using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.AdmTablasBasicas;
using SoftManagement.Log;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Solicitudes
    {
        public SolicitudesEntity    _SolicitudesEntity;
        public SolicitudesDALC      _SolicitudesDALC;

        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public Solicitudes()
        {
            _objConfiguracion   = new Configuracion();
           _SolicitudesEntity   = new SolicitudesEntity();
           _SolicitudesDALC     = new SolicitudesDALC();    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroSilpa"></param>
        /// <returns></returns>
        public DataTable ConsultarSolicitud(string numeroIdentificacion, string numeroSolicitud)
        {
            try
            {
                return _SolicitudesDALC.BuscarSolicitud(numeroIdentificacion, numeroSolicitud);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarSolicitud:asociada a una persona" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Metodo que obtiene las actividaes de un tramite por numero vital
        /// </summary>
        /// <param name="numeroExpediente">numero VITAL</param>
        public List<DataTable> BuscarSolicitud(string numeroExpediente)
        {
            try
            {
                return _SolicitudesDALC.BuscarSolicitud(numeroExpediente);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarSolicitud:lista de actividades asociadas" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public List<DataTable> BuscarDetalleActividadSolicitud(int idActivity, string numeroVital)
        {
            try
            {
                return _SolicitudesDALC.BuscarDetalleActividadSolicitud(idActivity, numeroVital);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarSolicitud:lista de actividades asociadas" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        public DataTable BuscarSolicitudes(string pIDapplicationUSer, string pfechaInicial, string pfechaFinal)
        {
            try
            {
                return _SolicitudesDALC.BuscarSolicitudes(pIDapplicationUSer, pfechaInicial, pfechaFinal);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ConsultarSolicitud:asociada a una persona" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        public Boolean ActualizarSolicitud(int id_solicitud, int id_Autoridad)
        {
            try
            {
                return _SolicitudesDALC.ActualizarSolicitud(id_solicitud, id_Autoridad);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ActualizarSolicitud:asociada a una persona" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public Boolean EliminarSolicitud(int id_solicitud)
        {
            try
            {
                return _SolicitudesDALC.EliminarSolicitud(id_solicitud);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ActualizarSolicitud:asociada a una persona" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }
    }
}
