using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.RegistroMinero
{
    public enum Geometria
    {
        /// <summary>
        /// Punto de coordenada
        /// </summary>
        Punto = 0,
        /// <summary>
        /// Arco trazado entre varios puntos
        /// </summary>
        Linea = 1,
        /// <summary>
        /// Zona acotada por un conjunto de arcos
        /// </summary>
        Poligino = 2
    }
}
