using System;
using SILPA.Comun;

namespace SILPA.Servicios.BPMProcess.Entidades
{
    public class RadicadoSolicitudTramiteEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la solicitud registrada
            /// </summary>
            public long SolicitudID {get; set;}

            /// <summary>
            /// Identificador del tramite
            /// </summary>
            public int TramiteID { get; set; }

            /// <summary>
            /// Numero Silpa VITAL Registrado
            /// </summary>
            public string NumeroSilpa { get; set; }

            /// <summary>
            /// Numero VITAL Generado
            /// </summary>
            public string NumeroVital { get; set; }

            /// <summary>
            /// Identificador del registro de radicacion generado
            /// </summary>
            public long RadicadoID { get; set; }

            /// <summary>
            /// Descripcion del radicado
            /// </summary>
            public string DescripcionRadicado { get; set; }

            /// <summary>
            /// Path de ubicacion de los documentos
            /// </summary>
            public string PathDocumentos { get; set; }


        #endregion
    }
}
