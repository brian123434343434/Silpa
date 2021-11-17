using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Utilidades;
using SILPA.Comun;


namespace SILPA.AccesoDatos.EstadoAA
{
    public class EstadoVitalEntity : EntidadSerializable
    {
        private int _estId;
        public int EstId
        {
            get { return _estId; }
            set { _estId = value; }
        }

        private string _estNombre;
        public string EstNombre
        {
            get { return _estNombre; }
            set { _estNombre = value; }
        }

        private int _estActivo;
        public int EstActivo
        {
            get { return _estActivo; }
            set { _estActivo = value; }
        }
    }
}
