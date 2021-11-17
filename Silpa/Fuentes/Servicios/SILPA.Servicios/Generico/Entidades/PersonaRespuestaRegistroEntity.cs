using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;

namespace SILPA.Servicios.Generico.Entidades
{
    public class PersonaRespuestaRegistroEntity : PersonaRespuestaEntity
    {
        #region Propiedades

            /// <summary>
            /// Información de la persona que se registro o actualizo
            /// </summary>
            public PersonaIdentity InformacionPersona { get; set; }

        #endregion
    }
}
