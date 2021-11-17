using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase pais
    /// </summary>
    public class PaisIdentity : EntidadSerializable
    {

        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public PaisIdentity() { }

        
        /// <summary>
        /// constructor con parametros
        /// </summary>
        /// <param name="intId"></param>
        /// <param name="strNombre"></param>
        /// <param name="blnActivo"></param>
        /// <param name="strCodigoInter"></param>
        public PaisIdentity(int intId,string strNombre, bool blnActivo, string strCodigoInter) 
        {   
            this._id= intId;
            this._nombre = strNombre;
            this._activo = blnActivo;
            this._codigoInter = strCodigoInter;
        }

        #region  Declaración de campos ...
            private int _id;
            private string _nombre;
            private bool _activo;
            private  string _codigoInter;
        #endregion 

        #region  Declaración de propiedades...

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        public string CodigoInter { get { return this._codigoInter; } set { this._codigoInter = value; } }

        #endregion 
    }
}
