using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class ZonaHidrograficaIdentity : EntidadSerializable
    {
        
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public ZonaHidrograficaIdentity() { }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="intId"></param>
        /// <param name="intAreaHidroId"></param>
        /// <param name="strNombre"></param>
        /// <param name="blnActivo"></param>
        public ZonaHidrograficaIdentity(int intId, int intAreaHidroId, string strNombre, bool blnActivo) 
        { 
            this._id = intId;
            this._areaHidroId = intAreaHidroId;
            this._nombre = strNombre; 
            this._activo = blnActivo;
        }

        #region  Declaración de campos ...

         /// <summary>
         /// Identificador de la zona hidrografica
         /// </summary>
         private int _id;
         
        /// <summary>
        /// Area hidrografica a la que pertenece
        /// </summary>
         private int _areaHidroId;

        /// <summary>
        /// Nombre de la zona hidrografica
        /// </summary>
         private string _nombre;

        /// <summary>
        /// Zona Activa / inactiva
        /// </summary>
         private bool _activo;
        #endregion 

        #region  Declaración de propiedades...
         public int Id { get{return this._id;} set{this._id = value;}}
         public int AreaHidroId{ get{return this._areaHidroId;} set{this._areaHidroId = value;}}
         public string Nombre{ get{return this._nombre;} set{this._nombre= value;}}
         public bool Activo{ get{return this._activo;} set{this._activo= value;}}
        #endregion 


    }
}
