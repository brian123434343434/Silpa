using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class ConceptoIdentity:EntidadSerializable
    {

        #region Atributos
        
        private int _idConcepto;

        private string _nombre;
       
        #endregion

        #region Propiedades

        public int IDConcepto
        {
            get { return _idConcepto; }
            set { _idConcepto = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        #endregion


    }
}
