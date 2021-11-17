using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.DAA
{   
    public class DAASolicitudEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la solicitud
            /// </summary>
            public long SolicitudID { get; set; }

            /// <summary>
            /// Identificador del Tipo de Tramite
            /// </summary>
            public int TipoTramiteID { get; set; }

            /// <summary>
            /// Tipo de Tramite
            /// </summary>
            public string TipoTramite { get; set; }

            /// <summary>
            /// Identificador de la autoridad a la cual pertenece la solicitud
            /// </summary>
            public int AutoridadID { get; set; }

            /// <summary>
            /// Autoridad a la cual pertenece la solicitud
            /// </summary>
            public string Autoridad { get; set; }

            /// <summary>
            /// Identificador del solicitante que realizo la solicitud
            /// </summary>
            public int SolicitanteID { get; set; }

            /// <summary>
            /// Solicitante que realizo la solicitud
            /// </summary>
            public string Solicitante { get; set; }

            /// <summary>
            /// Identificador de la radicacion inicial
            /// </summary>
            public long RadicacionID { get; set; }

            /// <summary>
            /// Identificador de la instancia del proceso al cual se asigno la solicitud
            /// </summary>
            public int InstanciProcesoID { get; set; }

            /// <summary>
            /// Numero VITAL de la solicitud
            /// </summary>
            public string NumeroVITAL { get; set; }

            /// <summary>
            /// Fecha de registro o creacion de la solicitud
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Inidca si tiene solicitud de reasignación pendiente
            /// </summary>
            public bool TieneSolicitudAsignacionPendiente { get; set; }
            

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
