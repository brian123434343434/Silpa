using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;

namespace SILPA.LogicaNegocio.Generico
{
    public class SolicitudCredenciales
    {
        #region Atributos de la entidad
        private Configuracion _objConfiguracion;
        private SolicitudCredencialesDalc Dalc;
        public SolicitudCredencialesEntity Identity;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public SolicitudCredenciales()
        {
            this._objConfiguracion = new Configuracion();
            this.Dalc = new SolicitudCredencialesDalc();
            this.Identity = new SolicitudCredencialesEntity();

        }
        //23-jun-2010 - aegb
        public void InsertarSolicitudCredenciales(long intPersonaID, int intEnProceso)
        {
            this.Identity.PersonaID = intPersonaID;
            this.Identity.EnProceso = intEnProceso;

            this.Dalc.InsertarSolicitudPersona(ref this.Identity);

        }

        public Boolean ObtenerEstadoProcesoByPersona()
        {
            return false;
        }
    }
}

