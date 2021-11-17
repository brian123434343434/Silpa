using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Encuestas.Encuesta.Enum
{
    /// <summary>
    /// Tipos de Pregunta de una encuesta
    /// </summary>
    public enum TipoPreguntaEncuestasEnum
    {
        Seleccion_Unica = 1,
        Campo_Abierto = 2,
        Ubicacion_Geografica = 3,
        Coordenadas = 4,
        Documento = 5,
        Calendario = 6,
        Hora = 7,
        Seleccion_Multiple = 8
    }
}
