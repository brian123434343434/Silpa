using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.DAA
{
    public class DAASolicitudReasignacionEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la autoridad ambiental que realiza la solicitud
            /// </summary>
            public int SolicitudReasignacionID { get; set; }

            /// <summary>
            /// Identificador de la autoridad ambiental que realiza la solicitud
            /// </summary>
            public string CodigoReasignacion { get; set; }

            /// <summary>
            /// Identificador de la autoridad ambiental que realiza la solicitud
            /// </summary>
            public int AutoridadAmbientalSolicitanteID { get; set; }

            /// <summary>
            /// Autoridad ambiental que realiza la solicitud
            /// </summary>
            public string AutoridadAmbientalSolicitante { get; set; }

            /// <summary>
            /// Identificador de la autoridad ambiental a reasignar
            /// </summary>
            public int AutoridadAmbientalReasignarID { get; set; }

            /// <summary>
            /// Autoridad ambiental a reasignar
            /// </summary>
            public string AutoridadAmbientalReasignar { get; set; }

            /// <summary>
            /// Numero VITAL del proceso a reasignar
            /// </summary>
            public string NumeroVITAL { get; set; }

            /// <summary>
            /// Identificador de la solicitud
            /// </summary>
            public long SolicitudID { get; set; }

            /// <summary>
            /// Identificador de la instancia del proceso al cual se asigno la solicitud
            /// </summary>
            public int InstanciaProcesoID { get; set; }

            /// <summary>
            /// Fecha de la solicitud
            /// </summary>
            public DateTime FechaSolicitud { get; set; }

            /// <summary>
            /// Identificador del solicitante que realizo la solicitud
            /// </summary>
            public int SolicitanteID { get; set; }

            /// <summary>
            /// Solicitante que realizo la solicitud
            /// </summary>
            public string Solicitante { get; set; }

            /// <summary>
            /// Identificador de la persona que realizó la solicitud de reasignación
            /// </summary>
            public int? SolicitanteAutoridadID { get; set; }

            /// <summary>
            /// Identificador de la persona que aprueba o rechaza solicitud de reasignación
            /// </summary>
            public int? SolicitanteAutoridadVerificaID { get; set; }

            /// <summary>
            /// Identificador del estado de la solicitud
            /// </summary>
            public int EstadoSolicitudID { get; set; }

            /// <summary>
            /// Estado de la solicitud
            /// </summary>
            public string EstadoSolicitud { get; set; }

            /// <summary>
            /// Fecha de aprobación o rechazo de la solicitud
            /// </summary>
            public DateTime? FechaVerificacionSolicitudReasignacion { get; set; }

            /// <summary>
            /// Fecha de creacion de la solicitud
            /// </summary>
            public DateTime FechaCreacionSolicitudReasignacion { get; set; }

            /// <summary>
            /// Fecha de última modificación de la solicitud
            /// </summary>
            public DateTime FechaActualizacionSolicitudReasignacion { get; set; }

        #endregion


        #region Metodos

            /// <summary>
            /// Retorna en un string el contenido del objeto
            /// </summary>
            /// <returns>string con el contenido del objeto</returns>
            public override string ToString()
            {
                return this.GetXml();
            }


        #endregion

    }
}
