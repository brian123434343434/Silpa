using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Utilidades;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;   


namespace SILPA.AccesoDatos.EstadoAA
{
    public class HistoriaCambioEstadoEntity : EntidadSerializable
    {
        private int _idHistoriaCambio;
        public int IdHistoriaCambio
        {
          get { return _idHistoriaCambio; }
          set { _idHistoriaCambio = value; }
        }

        private DateTime _fechaRegistro;
        public DateTime FechaRegistro
        {
          get { return _fechaRegistro; }
          set { _fechaRegistro = value; }
        }

        private string _numeroVital;
        public string NumeroVital
        {
          get { return _numeroVital; }
          set { _numeroVital = value; }
        }

        private int _valorExpediente;
        public int ValorExpediente
        {
            get { return _valorExpediente; }
            set { _valorExpediente = value; }
        }


        private EstadoVitalEntity _estadoNuevo;
        public EstadoVitalEntity EstadoNuevo
        {
          get { return _estadoNuevo; }
          set { _estadoNuevo = value; }
        }

        private AutoridadAmbientalIdentity _autoridad;
        public AutoridadAmbientalIdentity Autoridad
        {
            get { return _autoridad; }
            set { _autoridad = value; }
        }

    }
}
