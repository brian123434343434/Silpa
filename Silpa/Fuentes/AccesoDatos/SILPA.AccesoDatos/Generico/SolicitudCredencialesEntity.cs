using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class SolicitudCredencialesEntity : EntidadSerializable
    {
        #region declaracion de campos ...
        private int _solicitudID;
        private long _personaID;
        private DateTime _fecha;
        //23-jun-2010 - aegb
        private int _enProceso;
        #endregion

        #region declaracion de Propiedades ...
        public int SolicitudID { get { return this._solicitudID; } set { this._solicitudID = value; } }
        public long PersonaID { get { return this._personaID; } set { this._personaID = value; } }
        public DateTime Fecha { get { return this._fecha; } set { this._fecha = value; } }
        //23-jun-2010 - aegb
        public int EnProceso { get { return this._enProceso; } set { this._enProceso = value; } }
        #endregion


        /// <summary>
        /// Constructor sin parametros
        /// </summary>

        public SolicitudCredencialesEntity()
        {

        }

        /// <summary>
        /// 23-jun-2010 - aegb
        /// Constructor con parametros
        /// </summary>
        /// <param name="intSolucionID"></param>
        /// <param name="intPersonaID"></param>
        /// <param name="fecha"></param>
        /// <param name="blnEnProceso"></param>
        public SolicitudCredencialesEntity(int intSolicitudID, int intPersonaID, DateTime fecha, int blnEnProceso)
        {
            this._solicitudID = intSolicitudID;
            this._personaID = intPersonaID;
            this._fecha = fecha;
            this._enProceso = blnEnProceso;

        }
    }
}
