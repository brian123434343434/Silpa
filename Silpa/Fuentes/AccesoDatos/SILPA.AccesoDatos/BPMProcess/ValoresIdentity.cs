using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.BPMProcess
{
    public class ValoresIdentity:EntidadSerializable
    {
        
        /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public ValoresIdentity() { }


        /// <summary>
        /// Constructor con parametros
        /// </summary> 
        public ValoresIdentity(int id, string strGrupo, string strValue,int intOrden) 
        { 
            this._id = id;
            this._grupo = strGrupo;
            this._value = strValue;
            this._orden = intOrden;
        }

        #region declaración de campos ...

         /// <summary>
         ///  Identificador del campo
         /// </summary>
         private int _id;


         /// <summary>
         /// Nombre del grupo al que pertenece el campo
         /// </summary>
         private string _grupo;

         /// <summary>
         /// Dato del campo
         /// </summary>
         private string _value;

         /// <summary>
         /// Orden del campo,
         /// Este orden es usado para aquellos valore que corresponden a una lista y se envian mas de un valor
         /// </summary>
         private int _orden;

         /// <summary>
         /// Archivo,
         /// Convertir Byte[] a String Convert.ToBase64String(Byte[]) el Archivo
         /// </summary>
         private Byte[] _archivo;

         /// <summary>
         /// Identificaor de la región a la cual pertenece el departamento.
         /// </summary>         
        #endregion

        #region Declaración de propiedades

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Grupo { get { return this._grupo; } set { this._grupo = value; } }
        public string Valor { get { return this._value; } set { this._value = value; } }
        public int Orden { get { return this._orden; } set { this._orden = value; } }
        public Byte[] Archivo { get { return this._archivo; } set { this._archivo = value; } }
        #endregion
    } 
}
