using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase Jurisdcción
    /// </summary>
    public class JurisdiccionIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public JurisdiccionIdentity(){}
        
        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="intId">int: Identificador de la jurisdicción</param>
        /// <param name="intAutoridadId">int: identificador de la autoridad ambiental</param>
        /// <param name="intMunicipioId">int: identificador del municipio</param>
        /// <param name="dteFechaInsercion">DateTime: fecha de inserción de la jurisdicción</param>
        public JurisdiccionIdentity(int intId, int intAutoridadId, int intMunicipioId, DateTime dteFechaInsercion)
        {
            this._id = intId;
            this._autoridadId = intAutoridadId;
            this._municipioId = intMunicipioId;
            this._fechaInsercion = dteFechaInsercion;
        }

        #region  Declaración de campos ...
            private int _id;
            private int _autoridadId;
            private int _municipioId;
            private DateTime _fechaInsercion;
        #endregion

        #region  Declaración de propiedades ...
        public int Id { get { return this._id; } set { this._id = value; } }
        public int AutoridadId { get { return this._autoridadId; } set { this._autoridadId = value; } }
        public int MunicipioId { get { return this._municipioId; } set { this._municipioId = value; } }
        public DateTime FechaInsercion { get { return this._fechaInsercion; } set { this._fechaInsercion = value; } }
       #endregion
    }
}
