using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class CobroPSE : EntidadSerializable
    {
        private string _cobReferencia;

        private DateTime _cobFechaTransacion;

        public string CobReferencia
        {
            get { return _cobReferencia; }
            set { _cobReferencia = value; }
        }

        public DateTime CobFechaTransaccion
        {
            get { return _cobFechaTransacion; }
            set { _cobFechaTransacion = value; }
        }

    }
}
