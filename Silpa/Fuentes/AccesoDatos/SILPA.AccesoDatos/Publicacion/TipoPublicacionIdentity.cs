using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Publicacion
{
    /// <summary>
    /// Clase tipo publicaci�n.
    /// </summary>
    public class TipoPublicacionIdentity
    {

        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public TipoPublicacionIdentity() { }


        /// <summary>
        /// Constructor con par�metros
        /// </summary>
        /// <param name="intId">int: identificador del tipo de publicaci�n</param>
        /// <param name="strNombre">string :nombre del rtipo de publicaci�n</param>
        /// <param name="blnActivo">bool:  Tipo publicaci�n Activo / inactivo </param>
        /// <param name="intActividadId">int: identificador de la actividad relacionada</param>
        public TipoPublicacionIdentity(int intId, string strNombre, bool blnActivo, int intActividadId)
        {
            this._id = intId;
            this._nombre = strNombre;
            this._activo=blnActivo;
            this._actividadId = intActividadId;
        }

        #region  Declaraci�n de campos ...

        /// <summary>
        /// identificador del tipo de publicaci�n
        /// </summary>
        private int _id;

        /// <summary>
        /// nombre del rtipo de publicaci�n
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Tipo publicaci�n Activo / inactivo
        /// </summary>
        private bool _activo;

        /// <summary>
        /// identificador de la actividad relacionada
        /// </summary>
        private int _actividadId;

        #endregion
    
        #region  Declaraci�n de propiedades...

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        public int ActividadId { get { return this._actividadId; } set { this._actividadId = value; } }


        #endregion

    }
}
