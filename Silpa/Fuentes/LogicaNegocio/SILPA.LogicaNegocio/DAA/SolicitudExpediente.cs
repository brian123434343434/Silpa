using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.DAA;

namespace SILPA.LogicaNegocio.DAA
{
    public class SolicitudExpediente
    {
        /// <summary>
        /// Javier Alcalá
        /// Método para Asociar Expediente y numero silpa
        /// </summary>
        /// <param name="autoridadId"></param>
        /// <param name="numeroExpediente"></param>
        /// <param name="tipoAsociacion"></param>
        /// <param name="numerosSilpa"></param>
        public void AsociarExpedienteNumeroSilpa(int autoridadId, string numeroExpediente, string tipoAsociacion, List<string> numerosSilpa)
        {
            try
            {
	            SolicitudExpedienteDalc dalc = new SolicitudExpedienteDalc();
	
	            dalc.Eliminar(autoridadId, numeroExpediente, tipoAsociacion);
	            foreach (string numeroSilpa in numerosSilpa)
	            {
	                dalc.Insertar(autoridadId, numeroExpediente, tipoAsociacion, numeroSilpa);
	            }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Asociar Expediente y Número VITAL.";
                throw new Exception(strException, ex);
            }
        }


        public void AsociarInfoExpedienteNumeroSilpa(string codigoExpediente, string numeroVital,string nombreExpediente,string descripcionExpediente,string strsecId,List<string> strubicacion,List<string> strlocalizacion)
        {
            SolicitudExpedienteDalc dalc = new SolicitudExpedienteDalc();
            int secId=0;
            string _ubicacion="";
            string _localizacion="";
            if (strsecId!="")
                secId= int.Parse(strsecId);
            if (strubicacion.Count > 0)
            {
                foreach (string u in strubicacion)
                {
                    _ubicacion = _ubicacion+u + ";";
                }
            }
            if (strlocalizacion.Count > 0)
            {
                foreach (string u in strlocalizacion)
                {
                    _localizacion = _localizacion + u + ";";
                }
            }

            dalc.EliminarInfoExp(codigoExpediente, numeroVital);            
            dalc.InsertarInfoExp(codigoExpediente, numeroVital,nombreExpediente,descripcionExpediente,secId,_ubicacion,_localizacion);
            
        }


        /// <summary>
        /// Javier Alcalá
        /// Método para Activar Expediente y numero silpa
        /// </summary>
        /// <param name="autoridadId"></param>
        /// <param name="numeroExpediente"></param>
        /// <param name="tipoAsociacion"></param>
        /// <param name="numerosSilpa"></param>
        public void ActivarExpedienteNumeroSilpa(int autoridadId, string numeroExpediente)
        {
            SolicitudExpedienteDalc dalc = new SolicitudExpedienteDalc();
            dalc.InsertarActivar(autoridadId, numeroExpediente);            
        }


        /// <summary>
        /// Metodo que crea una solicitud de manera manual
        /// </summary>
        /// <param name="AutoridadID">Identificador de la autoridad ambiental</param>
        /// <param name="ExpedienteID">Identificador del expediente</param>
        /// <param name="CodigoExpediente">Codigo del expediente</param>
        /// <param name="SectorID">Sector del Expediente</param>
        /// <param name="PersonaID">Identificador de la persona</param>
        /// <param name="NumeroVITAL">Numero Vital</param>
        /// <param name="NumeroRadicado">Numero de radicado</param>
        /// <param name="TramiteID">Identificador del tramite</param>
        /// <param name="NombreProyecto">Nombre del proyecto</param>
        public void CrearSolicitudManual(int AutoridadID, int ExpedienteID, string CodigoExpediente, int SectorID, int PersonaID, string NumeroVITAL, string NumeroRadicado, int TramiteID, string NombreProyecto)
        {
            try
            {
                SolicitudExpedienteDalc dalc = new SolicitudExpedienteDalc();

                dalc.CrearSolicitudManual(AutoridadID, ExpedienteID, CodigoExpediente, SectorID, PersonaID, NumeroVITAL, NumeroRadicado, TramiteID, NombreProyecto);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Asociar Expediente y Número VITAL.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// Obtener el numero vital padre de una solicitud
        /// </summary>
        /// <param name="strNumeroVital">string con el numero vital de la solicitud</param>
        /// <returns>string con el numero vital de la solicitud</returns>
        public string ObtenerNumeroVITALPadreSolicitud(string strNumeroVital)
        {
            try
            {
                SolicitudExpedienteDalc dalc = new SolicitudExpedienteDalc();

                return dalc.ObtenerNumeroVITALPadreSolicitud(strNumeroVital);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Asociar Expediente y Número VITAL.";
                throw new Exception(strException, ex);
            }
        }
    }
}
