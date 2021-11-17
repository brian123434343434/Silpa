using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    
    
    /// <summary>
    /// Clase MunicipioIdentity
    /// </summary>
    [Serializable]
    public class MunicipioIdentity : EntidadSerializable
    {

        /// <summary>
        /// Constructor si parametros
        /// </summary>
        public MunicipioIdentity() { }


        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="strNombreMunicipio">nombre del municipio</param>
        /// <param name="blnActivo">activo</param>
        /// <param name="intDeptoId">Identificador del depto</param>
        /// <param name="fltMunicipioValor">valor del municipio viaticos</param>
        /// <param name="intRegionalId"></param>
        /// <param name="intRegionId"></param>
        /// /// <param name="intUbicacionId"></param>
        public MunicipioIdentity(string strNombreMunicipio, bool blnActivo, int intDeptoId,
            float fltMunicipioValor, int intRegionalId, int intUbicacionId)
        {
            this._nombreMunicipio   = strNombreMunicipio;
            this._activo            = blnActivo;
            this._deptoId           = intDeptoId;
            this._municipioValor    = fltMunicipioValor;
            this._regionalId        = intRegionalId;
            this._ubicacionId       = intUbicacionId;
        }


            #region Declaración de campos...

            /// <summary>
            /// Identificador del municipio en la base de datos
            /// </summary>
            private int _id;

            /// <summary>
            /// Nombre del municipio
            /// </summary>
            private string _nombreMunicipio;

            /// <summary>
            /// Determina si el municipio se encuentra o no activo
            /// </summary>
            private bool _activo;

            /// <summary>
            /// Identificador del departamente al que pertenece.
            /// </summary>
            private int _deptoId;

            /// <summary>
            /// Costos de viaticos al municipio
            /// </summary>
            private float _municipioValor;

            /// <summary>
            /// Identificador de la regional del a la que pertenece el municipio
            /// </summary>
            private int _regionalId;

            /// <summary>
            /// Identificador de la ubicacion del municipio
            /// </summary>
            private int _ubicacionId;

            


            #endregion


            #region Declaración de Propiedades...

            public int Id { get { return this._id; } set { this._id = value; } }
            public string NombreMunicipio { get { return this._nombreMunicipio; } set { this._nombreMunicipio = value; } }
            public bool Activo { get { return this._activo; } set { this._activo = value; } }
            public int DeptoId { get { return this._deptoId; } set { this._deptoId = value; } }
            public float MunicipioValor { get { return this._municipioValor; } set { this._municipioValor = value; } }
            public int RegionalId { get { return this._regionalId; } set { this._regionalId = value; } }
            public int UbicacionId { get { return _ubicacionId; } set { _ubicacionId = value; } }

            #endregion


    }
}
