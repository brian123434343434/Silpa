using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class DetalleCobroIdentity : EntidadSerializable
    {
        #region Atributos
        
        private decimal _idDetalleCobro;
        
        private string _descripcion;

        private decimal _valor;

        #endregion

        #region Propiedades

        public decimal IDDetalleCobro
        {
            get { return _idDetalleCobro; }
            set { _idDetalleCobro = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        #endregion

    }
}
