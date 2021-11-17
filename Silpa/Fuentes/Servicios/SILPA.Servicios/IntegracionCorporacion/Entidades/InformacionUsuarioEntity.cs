using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.Servicios.IntegracionCorporacion.Entidades
{
    public class InformacionUsuarioEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador del usuario
            /// </summary>
            public long UsuarioID { get; set; }

            /// <summary>
            /// Nombre del usuario
            /// </summary>
            public string NombreUsuario { get; set; }

            /// <summary>
            /// Fecha del ultimo acceso a VITAL
            /// </summary>
            public string FechaUltimoLogin { get; set; }

        #endregion

    }
}
