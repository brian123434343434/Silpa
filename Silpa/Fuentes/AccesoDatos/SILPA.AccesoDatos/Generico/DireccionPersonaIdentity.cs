using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    
    /// <summary>
    /// Contiene los datos de las direcciones de las personas
    /// </summary>
    [Serializable]
    public class DireccionPersonaIdentity:EntidadSerializable
    {
        
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public DireccionPersonaIdentity() { }

        
        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="intIdPersona"> int: Identificador de persona</param>
        /// <param name="intMunicipioId">int: Identificador del municipio</param>
        /// <param name="strNombreMunicipio">string: Nombre del municipio</param>
        /// <param name="intVeredaId">int: identifiador de la vereda</param>
        /// <param name="strNombreVereda">string: Nombre de la vereda</param>
        /// <param name="intCorregimientoId">int: ide del corregimiento</param>
        /// <param name="strNombreCorregimiento">string: nombre del corregimiento</param>
        public DireccionPersonaIdentity
            (
                Int64 intIdPersona, 
                int intMunicipioId, string strNombreMunicipio,
                int intVeredaId, string strNombreVereda, 
                int intCorregimientoId, string strNombreCorregimiento
            )
        {
            this._idPersona             = intIdPersona;
            this._municipioId           = intMunicipioId;
            this._nombreMunicipio       = strNombreVereda;
            this._veredaId              = intVeredaId;
            this._nombreVereda          = strNombreVereda;
            this._corregimientoId       = intCorregimientoId;
            this._nombreCorregimiento   = strNombreCorregimiento;
        }


        /// <summary>
        /// identificador de la r¡dirección de la persona
        /// </summary>
        private Int64 _Id;

        public Int64 Id{get { return _Id; }set { _Id = value; }}

        private int _paisId;
        public int PaisId { get { return this._paisId; } set { this._paisId = value; } }

        private string _nombrePais;
        public string NombrePais { get { return this._nombrePais; } set { this._nombrePais = value; } }

        private int _municipioId;
        public int MunicipioId { get { return this._municipioId; } set { this._municipioId = value; } }

        private string _nombreMunicipio;
        public string NombreMunicipio { get { return this._nombreMunicipio; } set { this._nombreMunicipio = value; } }

        private int _veredaId;
        public int VeredaId { get { return this._veredaId; } set { this._veredaId = value; } }

        /// <summary>
        /// Nombre de la vereda
        /// </summary>
        private string _nombreVereda;
        public string NombreVereda { get { return this._nombreVereda; } set { this._nombreVereda = value; } }

        /// <summary>
        /// identificador del corregimiento
        /// </summary>
        private int _corregimientoId;
        public int CorregimientoId { get { return this._corregimientoId; } set { this._corregimientoId = value; } }

        /// <summary>
        /// Nombre del corregimiento
        /// </summary>
        private string _nombreCorregimiento;
        public string NombreCorregimiento { get { return this._nombreCorregimiento; } set { this._nombreCorregimiento = value; } }

        /// <summary>
        /// Dirección de la persona
        /// </summary>
        private string _direccionPersona;
        public string DireccionPersona { get { return this._direccionPersona; } set { this._direccionPersona = value; } }

        private Int64 _idPersona;
        public Int64 IdPersona { get { return this._idPersona; } set { this._idPersona = value; } }

        private Int32 _departamentoId;

        public Int32 DepartamentoId
        {
            get { return _departamentoId; }
            set { _departamentoId = value; }
        }

        private string _nombreDepartamento;

        public string NombreDepartamento
        {
            get { return _nombreDepartamento; }
            set { _nombreDepartamento = value; }
        }


        /// <summary>
        /// Identificador del tipo de dirección 
        /// </summary>
        private int _tipoDireccion;
        public int TipoDireccion
        {
            get { return _tipoDireccion;  }
            set { _tipoDireccion = value; }
        }

        /// <summary>
        /// Nombre que identifica el tipo de dirección 
        /// </summary>
        private string _nombreTipoDireccion;
        public string NombreTipoDireccion
        {
            get { return _nombreTipoDireccion; }
            set { _nombreTipoDireccion = value; }
        }


    }
}
