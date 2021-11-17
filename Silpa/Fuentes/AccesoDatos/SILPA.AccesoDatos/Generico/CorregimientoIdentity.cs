using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class CorregimientoIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public CorregimientoIdentity() { }


        /// <summary>
        /// Cosntructor con parámetros
        /// </summary>
        /// <param name="intId">Int: identificador del corregimiento </param>
        /// <param name="strNombre">string: Nombre del corregimiento</param>
        /// <param name="intMunicipioId">Int: identificador del municipio</param>
        public CorregimientoIdentity(int intId,string strNombre,int intMunicipioId, bool _activo) 
        {
            this._id = intId;
            this._nombre = strNombre;
            this._municipioId = intMunicipioId;
            this._activo = _activo;
        }


        #region Declaración de campos ...
         private int _id;
         private string _nombre;
         private int _municipioId;
         private bool _activo;

        #endregion

         #region Declaración de propiedades ...

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public int MunicipioId { get { return this._municipioId; } set { this._municipioId = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }

         #endregion
    }
}
