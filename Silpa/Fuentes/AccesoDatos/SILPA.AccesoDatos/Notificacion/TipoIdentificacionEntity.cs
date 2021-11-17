using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Notificacion
{

    /// <summary>
    /// Clase que contiene los atributos del tipo de identificación:
    /// TARJETA DE IDENTIDAD      TI
    /// CÉDULA CIUDADANÍA         CC
    /// CÉDULA EXTRANJERÍA        CE
    /// ADULTO SIN IDENTIFICAR    AS
    /// MENOR SIN IDENTIFICAR     MS
    /// RECIÉN NACIDO             RN
    /// PASAPORTE                 PA
    /// CÉDULA EXTRANJERO         CX
    /// Tipos de dato GEL-XML
    /// </summary>
    [Serializable]
    public class TipoIdentificacionEntity:EntidadSerializable
    {
        /// <summary>
        /// constructor sin
        /// </summary>
        public TipoIdentificacionEntity() {}

        /// <summary>
        /// constructor con parametros
        /// </summary>
        public TipoIdentificacionEntity(int inintId, string strNnombre, string strSigla)
        {
           this._id = inintId;
           this._nombre = strNnombre;
           this._codigo = strSigla;
       }

       #region Declaracion de campos ... 
            /// <summary>
            ///  identificador del tipo de identificación
            /// </summary>
           private int _id;
           
            /// <summary>
            /// nombre del tipo de identificaión
            /// </summary>
            private string _nombre;

            /// <summary>
            /// Sigla del tipo de identificaión
            /// </summary>
           private string _codigo;

       #endregion

       #region Declaracion de propiedades ...
        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public string Sigla { get { return this._codigo; } set { this._codigo = value; } }
       #endregion


    }
}
