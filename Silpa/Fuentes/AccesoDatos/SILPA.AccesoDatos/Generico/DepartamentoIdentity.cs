using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class DepartamentoIdentity:EntidadSerializable
    {

        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public DepartamentoIdentity() { }


        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="strNombre">Nombre del depto</param>
        /// <param name="blnActivo">Activo: true / false</param>
        /// <param name="intRegionId">Identificador de la region del depto</param>
        public DepartamentoIdentity(string strNombre, bool blnActivo, int intRegionId) 
        { 
            this._nombre = strNombre;
            this._activo = blnActivo;
            this._region = intRegionId;
        }

        #region declaración de campos ...

         /// <summary>
         ///  Identificador en la base de datos del departamento
         /// </summary>
         private int _id;   
        
         /// <summary>
         /// Nombre del departamento
         /// </summary>
         private string _nombre;

         /// <summary>
         /// Determina si el depto se encuentra activo o no
         /// </summary>
         private bool _activo;

         /// <summary>
         /// Identificaor de la región a la cual pertenece el departamento.
         /// </summary>
         private int _region;   
        #endregion

        #region Declaración de propiedades

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        public int Region { get { return this._region; } set { this._region = value; } }

        #endregion
     }
}
