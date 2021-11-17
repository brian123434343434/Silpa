using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.CesionDeDerechos
{
    public class PersonaCesionIdentity
    {
        private long _idPersona;
        private string _strNumeroIdentificacion;
        private string _strNumeroVital;

        public long IdPersona { get { return this._idPersona; } set { this._idPersona = value; } }
        public string NumeroIdentificacion { get { return this._strNumeroIdentificacion; } set { this._strNumeroIdentificacion = value; } }
        public string  NumeroVital { get { return this._strNumeroVital; } set { this._strNumeroVital = value; } }
    }
}
