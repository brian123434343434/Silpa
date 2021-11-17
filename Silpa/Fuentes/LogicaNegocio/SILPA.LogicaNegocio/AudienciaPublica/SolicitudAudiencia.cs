using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.AudienciaPublica;
using System.Data;
using SILPA.LogicaNegocio.Generico;

namespace SILPA.LogicaNegocio.AudienciaPublica
{
    public class SolicitudAudiencia
    {
        public SolicitudAudienciaIdentity SolIdentity;
        private SolicitudAudienciaDalc SolDalc;
        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public SolicitudAudiencia()
        {
            _objConfiguracion = new Configuracion();
            SolDalc = new SolicitudAudienciaDalc();
            SolIdentity = new SolicitudAudienciaIdentity();
        }

        /// <summary>
        ///  Crea un registro en la tabla solicitud de audiencia publica
        /// </summary>      
        public void CrearSolicitudAudiencia(ref SolicitudAudienciaIdentity objIdentity)
        {
            //TraficoDocumento tf = new TraficoDocumento();
            //string rutaDocumento = string.Empty;
            //tf.RecibirDocumento
            //        (objIdentity.NumeroSilpaProyecto,
            //           _usuario,
            //            lsBytes,
            //            lsNombres,
            //            ref rutaDocumento
            //         );
            //Crear solicitud audiencia
            SolDalc.CrearSolicitudAudiencia(ref objIdentity);
        }
    }
}