using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Utilidades;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;   

namespace SILPA.AccesoDatos.EstadoAA
{
    public class EstadoVitalAutoridadEntity : EntidadSerializable
    {
        private int _idEstadoAutoirdad;
        public int IdEstadoAutoirdad
        {
            get { return _idEstadoAutoirdad; }
            set { _idEstadoAutoirdad = value; }
        }

        private EstadoVitalEntity _estadoAAmbiental;
        public EstadoVitalEntity EstadoAAmbiental
        {
            get { return _estadoAAmbiental; }
            set { _estadoAAmbiental = value; }
        }

        private AutoridadAmbientalIdentity _autoridad;
        public AutoridadAmbientalIdentity Autoridad
        {
            get { return _autoridad; }
            set { _autoridad = value; }
        }
    }
}
