using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.Generico;
using SILPA.Servicios;
using SILPA.Servicios.SolicitudDAA;
using SILPA.AccesoDatos.Generico;
using SILPA.Servicios.Generico.RadicarDocumento;
using SoftManagement.Log;

namespace SILPA.Servicios.Generico
{
    /*
     * Clase útil para la selección del proceso lanzado desde el BPM
     */
    public class ProcesoFachada
    {
        public RadicacionDocumentoFachada rad;
        
        /// <summary>
        /// Constructor del proceso para iniciar desde el bpmservices
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="UserID"></param>
        /// <param name="IDProcessInstance"></param>
        public ProcesoFachada(string Client, Int64 UserID, Int64 IDProcessInstance) 
        {
            rad = new RadicacionDocumentoFachada();
            Proceso proceso =  new Proceso(IDProcessInstance);
            
            /// Proceso: Solicitud DAA
            if (proceso.PIdentity.Clave.Equals("DAA")) 
            {   
                SolicitudFachada SolicitudDAA = new SolicitudFachada(Client, UserID, IDProcessInstance);
                if(SolicitudDAA.idRadicacion!=0)
                rad.ObtenerDatosRadicacion(SolicitudDAA.idRadicacion);
                
            }
            else
            {
                SolicitudFachada SolicitudEstandar = new SolicitudFachada();
                SolicitudEstandar.SolicitudFachadaEstandar(Client, UserID, IDProcessInstance);
                if(SolicitudEstandar.idRadicacion!=0)
                rad.ObtenerDatosRadicacion(SolicitudEstandar.idRadicacion);
                
            }
            

            
        }
    }
}
