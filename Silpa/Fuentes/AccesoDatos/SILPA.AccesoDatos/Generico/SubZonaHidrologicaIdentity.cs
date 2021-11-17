using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase de SubZonaHidrologica
    /// </summary>
    [Serializable]
    public class SubZonaHidrologicaIdentity : EntidadSerializable
    {

        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public SubZonaHidrologicaIdentity() { }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="intid"></param>
        /// <param name="intZonaHidroId"></param>
        /// <param name="strNombre"></param>
        /// <param name="blnActivo"></param>
        public SubZonaHidrologicaIdentity(int intid, int intZonaHidroId, string strNombre, bool blnActivo)
        {
            this._activo = blnActivo;
            this._id = intid;
            this._nombre = strNombre;
            this._zonaHidroId = intZonaHidroId;
        }


        #region  Declaración de campos ...

        /// <summary>
        /// Identificador de la subZona hidrologica
        /// </summary>
        private int _id;

        /// <summary>
        /// Identificador de la zona hidrografica a la que pertenece
        /// </summary>
        private int _zonaHidroId;

        /// <summary>
        ///  Nombre de la subzona
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Subzona activa / inactiva
        /// </summary>
        private bool _activo;
        #endregion

        #region  Declaración de propiedades...
        public int Id { get { return this._id; } set { this._id = value; } }
        public int ZonaHidroId { get { return this._zonaHidroId; } set { this._zonaHidroId = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        #endregion


    }
}
