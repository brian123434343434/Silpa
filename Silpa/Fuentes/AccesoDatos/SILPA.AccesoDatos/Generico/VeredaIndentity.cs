using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase que contiene los datos de vereda
    /// </summary>
    [Serializable]
    public class VeredaIndentity : EntidadSerializable
    {

        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public VeredaIndentity(){}

        /// <summary>
        /// cosntructor con parámetros
        /// </summary>
        /// <param name="intId">Identificador de la vereda en base de datos </param>
        /// <param name="strNombre"> Nombre de la vereda</param>
        /// <param name="blnActivo">Vereda Activa / Inactiva</param>
        /// <param name="intCorregimientoId">identificador del corregimiento al que pertenece</param>
        /// <param name="intMunicipioId">Identificador del municipio al que pertenece</param>
        public VeredaIndentity(int intId,string strNombre,bool blnActivo,int intCorregimientoId,int intMunicipioId)
        {
            this._id = intId;
            this._nombre = strNombre;
            this._activo = blnActivo;
            this._corregimientoId = intCorregimientoId;
            this._municipioId = intMunicipioId;
        }

        #region Declaración de variables ...

        /// <summary>
        /// identificador de la vereda en la base de datos
        /// </summary>
        private int _id;

        /// <summary>
        /// Nombre de la vereda
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Vereda activa o inactiva
        /// </summary>
        private bool _activo;

        /// <summary>
        /// Identificador del corregimiento al que pertenece la vereda
        /// </summary>
        private int _corregimientoId;

        /// <summary>
        /// Identificador del municipio al cual perteneces la vereda
        /// </summary>
        private int _municipioId;


        #endregion


        #region Declaración de propiedades ...

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        public int CorregimientoId { get { return this._corregimientoId; } set { this._corregimientoId = value; } }
        public int MunicipioId { get { return this._municipioId; } set { this._municipioId = value; } }


        #endregion


    }
}
