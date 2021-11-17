using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class ExpedienteIdentity : EntidadSerializable
    {
        public ExpedienteIdentity()
        {}

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="expedienteID"></param>
        /// <param name="tramiteID"></param>
        /// <param name="administradorID"></param>
        /// <param name="expedienteNumero"></param>
        /// <param name="expedienteFechaCreacion"></param>
        /// <param name="cobroID"></param>
        /// <param name="expedienteNombre"></param>
        /// <param name="sectorID"></param>
        /// <param name="eST_ID"></param>
        /// <param name="expediente_Descripcion"></param>
        /// <param name="expedienteCodigo"></param>
        /// <param name="expedienteInfoVisita"></param>
        /// <param name="solicitudID"></param>
        /// <param name="expedienteNumeroVital"></param>
        /// <param name="rEP_ID"></param>
        /// <param name="expedienteNumeroRadicacion"></param>
        /// <param name="aPO_ID"></param>
        public ExpedienteIdentity(int expedienteID, int tramiteID, int administradorID, int expedienteNumero, DateTime expedienteFechaCreacion, int cobroID, string expedienteNombre, int sectorID, int eST_ID, string expediente_Descripcion, string expedienteCodigo, string expedienteInfoVisita, int solicitudID, string expedienteNumeroVital, int rEP_ID, string expedienteNumeroRadicacion, int aPO_ID)
        {
            this._expedienteID = expedienteID;
            this._tramiteID = tramiteID;
            this._administradorID = administradorID;
            this._expedienteNumero = expedienteNumero;
            this._expedienteFechaCreacion = expedienteFechaCreacion;
            this._cobroID = cobroID;
            this._expedienteNombre = expedienteNombre;
            this._sectorID = sectorID;
            this._eST_ID = eST_ID;
            this._expediente_Descripcion = expediente_Descripcion;
            this._expedienteCodigo = expedienteCodigo;
            this._expedienteInfoVisita = expedienteInfoVisita;
            this._solicitudID = solicitudID;
            this._expedienteNumeroVital = expedienteNumeroVital;
            this._rEP_ID = rEP_ID;
            this._expedienteNumeroRadicacion = expedienteNumeroRadicacion;
            this._aPO_ID = aPO_ID;
        }

        #region Declaración de campos...
        private int _expedienteID;
        private int _tramiteID;
        private int _administradorID;
        private int _expedienteNumero;
        private DateTime _expedienteFechaCreacion;
        private int _cobroID;
        private string _expedienteNombre;
        private int _sectorID;
        private int _eST_ID;
        private string _expediente_Descripcion;
        private string _expedienteCodigo;
        private string _expedienteInfoVisita;
        private int _solicitudID;
        private string _expedienteNumeroVital;
        private int _rEP_ID;
        private string _expedienteNumeroRadicacion;
        private int _aPO_ID;
        #endregion

        #region Declaración de Propiedades...
        public int ExpedienteID { get { return this._expedienteID; } set { this._expedienteID = value; } }
        public int TramiteID { get { return this._tramiteID; } set { this._tramiteID = value; } }
        public int AdministradorID { get { return this._administradorID; } set { this._administradorID = value; } }
        public int ExpedienteNumero { get { return this._administradorID; } set { this._administradorID = value; } }
        public DateTime ExpedienteFechaCreacion { get { return this._expedienteFechaCreacion; } set { this._expedienteFechaCreacion = value; } }
        public int CobroID { get { return this._administradorID; } set { this._administradorID = value; } }
        public string ExpedienteNombre { get { return this._expedienteNombre; } set { this._expedienteNombre = value; } }
        public int SectorID { get { return this._administradorID; } set { this._administradorID = value; } }
        public int EST_ID { get { return this._eST_ID; } set { this._eST_ID = value; } }
        public string Expediente_Descripcion { get { return this._expediente_Descripcion; } set { this._expediente_Descripcion = value; } }
        public string ExpedienteCodigo { get { return this._expedienteCodigo; } set { this._expedienteCodigo = value; } }
        public string ExpedienteInfoVisita { get { return this._expedienteInfoVisita; } set { this._expedienteInfoVisita = value; } }
        public int SolicitudID { get { return this._solicitudID; } set { this._solicitudID = value; } }
        public string ExpedienteNumeroVital { get { return this._expedienteNumeroVital; } set { this._expedienteNumeroVital = value; } }
        public int REP_ID { get { return this._rEP_ID; } set { this._rEP_ID = value; } }
        public string ExpedienteNumeroRadicacion { get { return this._expedienteNumeroRadicacion; } set { this._expedienteNumeroRadicacion = value; } }
        public int APO_ID { get { return this._aPO_ID; } set { this._aPO_ID = value; } }
        #endregion

    }
}
