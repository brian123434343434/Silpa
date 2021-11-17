using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun; 
namespace SILPA.AccesoDatos.Parametrizacion
{
    class EntidadExternaEntity : EntidadSerializable
    {
  
            /// <summary>
            /// constructor sin
            /// </summary>
            public EntidadExternaEntity() { }

            /// <summary>
            /// constructor con parametros
            /// </summary>
        public EntidadExternaEntity(int intEEX_ID,string strEEX_NOMBRE)
            {
                this._EEID = intEEX_ID;
                this._EENOMBRE = strEEX_NOMBRE;              
            }


      
            #region Declaracion de campos ...

             /// <summary>
            ///  Código entidad
            /// </summary>
            private int _EEID;


            /// <summary>
            /// Nombre entidad
            /// </summary>
            private string _EENOMBRE;
          
            #endregion

            #region Declaracion de propiedades ...

            public int ID_Entidad { get { return this._EEID; } set { this._EEID = value; } }
            public string Nombre_Entidad { get { return this._EENOMBRE; } set { this._EENOMBRE = value; } }
          
            #endregion


        }

}



