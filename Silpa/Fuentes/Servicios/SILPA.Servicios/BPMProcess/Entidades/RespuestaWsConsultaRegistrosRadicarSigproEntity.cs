using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.DAA;

namespace SILPA.Servicios.BPMProcess.Entidades
{
    public class RespuestaWsConsultaRegistrosRadicarSigproEntity : EntidadSerializable
    {
        public bool Error { get; set; }
        public string TextoError { get; set; }
        public int CantidadRegistro { get; set; }
        public List<RegistroRadicarSigproEntity> ListaRegistrosRadicarSigpro { get; set; }
    }
}
