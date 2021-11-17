using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    
    /*
     Clase tipo de ubicaci�n
     */
    public class TipoUbicacionIdentity : EntidadSerializable
    {

        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public TipoUbicacionIdentity() { }

        
        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="intId">intt: Identificador del tipo de ubicaci�n</param>
        /// <param name="strNombre">string: nombre del tipo de ubicaci�n</param>
        /// <param name="blnActivo">bool: Tipo activo/ inactivo</param>
        public TipoUbicacionIdentity(int intId, string strNombre, bool blnActivo) 
        { 
           this._id = intId;
           this._nombre =  strNombre;
           this._activo = blnActivo;
        }


        #region Declaraci�n de campos..

        /// <summary>
        /// Identificador del tipo de ubicaci�n
        /// </summary>
        private int _id;

        /// <summary>
        /// Nombre del tipo de ubicaci�n
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Tipo de ubicaci�n Activo / inactivo
        /// </summary>
        private bool _activo;

        #endregion



        #region Declaraci�n de propiedades...
        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }


        #endregion

    }
}
