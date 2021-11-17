using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;

namespace SILPA.AccesoDatos.Encuestas.Contingencias.Entity
{
    [Serializable]
    public class DocumentoPreguntaSolicitudContingenciasEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del documento de la pregunta
            /// </summary>
            public int DocumentoPreguntaSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la pregunta perteneciente a la solicitud
            /// </summary>
            public int PreguntaSolicitudID { get; set; }

            /// <summary>
            /// Identificador de la solicitud al cual pertnece la pregunta
            /// </summary>
            public int SolicitudID { get; set; }

            /// <summary>
            /// Pregunta asociada a la solicitud
            /// </summary>
            public PreguntaEncuestasEntity Pregunta { get; set; }

            /// <summary>
            /// Ubicacion fisica del documento
            /// </summary>
            public string Ubicacion { get; set; }

            /// <summary>
            /// Nombre del documento dado por el usuario
            /// </summary>
            public string NombreDocumentoUsuario { get; set; }

            /// <summary>
            /// Nombre fisico del documento
            /// </summary>
            public string NombreDocumento { get; set; }

            /// <summary>
            /// Fecha de creacion del documento
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha de ultima modificación del documento
            /// </summary>
            public DateTime FechaModificacion { get; set; }

        #endregion
        
    }
}
