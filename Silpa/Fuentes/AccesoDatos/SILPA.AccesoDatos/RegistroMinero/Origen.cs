using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.RegistroMinero
{
    public enum Origen
    {
        /// <summary>
        /// Origen desde Bogotá
        /// </summary>
        Bogota = 0,
        /// <summary>
        /// Punto de origen este
        /// </summary>
        Este = 1,
        /// <summary>
        /// Punto de origen este - este
        /// </summary>
        Este_Este = 2,
        /// <summary>
        /// Punto de origen oeste
        /// </summary>
        Oeste = 3,
        /// <summary>
        /// Punto de origen oeste - oeste
        /// </summary>
        Oeste_Oeste = 4
    }
}
