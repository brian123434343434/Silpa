using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class SectorIdentity : EntidadSerializable
    {

        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public SectorIdentity() { }


        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="intId"></param>
        /// <param name="strNombre"></param>
        /// <param name="blnActivo"></param>
        public SectorIdentity(int intId, int intIdPadre, string strNombre, bool blnActivo, bool blnRequiereDAA, bool blnPerteneceMADVT, bool blnTieneDAATDRFijos, string strURLDAATDR)
        {
            this._id = intId;
            this._idPadre = intIdPadre;
            this._nombre = strNombre;
            this._activo = blnActivo;
            this._perteneceMAVDT = blnPerteneceMADVT;
            this._requiereDAA = blnRequiereDAA;
            this._tieneDAATDRFijos = blnTieneDAATDRFijos;
            this._urlDAATDR = strURLDAATDR;


        }

        #region  Declaración de campos ...
        private int _id;
        private int _idPadre;
        private string _nombre;
        private bool _activo;
        private bool _requiereDAA;
        private bool _perteneceMAVDT;
        private bool _tieneDAATDRFijos;
        private string _urlDAATDR;






        #endregion


        #region  Declaración de propiedades...
        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        public int IdPadre { get { return _idPadre; } set { _idPadre = value; } }
        public bool RequiereDAA { get { return _requiereDAA; } set { _requiereDAA = value; } }
        public bool PerteneceMAVDT { get { return _perteneceMAVDT; } set { _perteneceMAVDT = value; } }
        public bool TieneDAATDRFijos
        {
            get { return _tieneDAATDRFijos; }
            set { _tieneDAATDRFijos = value; }
        }
        public string UrlDAATDR
        {
            get { return _urlDAATDR; }
            set { _urlDAATDR = value; }
        }
        #endregion


    }
}
