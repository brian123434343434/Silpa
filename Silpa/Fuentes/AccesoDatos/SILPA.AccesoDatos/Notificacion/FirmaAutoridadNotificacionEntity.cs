using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    [Serializable]
    public class FirmaAutoridadNotificacionEntity
    {        

        #region Propiedades

            /// <summary>
            /// Identificador de la firma
            /// </summary>
            public int FirmaAutoridadID { get; set; }

            /// <summary>
            /// Identificador de la autoridad
            /// </summary>
            public int AutoridadID { get; set; }
            
            /// <summary>
            /// Nombre de la actividad
            /// </summary>
            public string Autoridad { get; set; }

            /// <summary>
            /// Tipo de identificación del autorizado
            /// </summary>
            public int TipoIdentificacionAutorizadoID { get; set; }

            /// <summary>
            /// Número de identificaión del autorizado
            /// </summary>
            public string NumeroIdentificaionAutorizado { get; set; }

            /// <summary>
            /// Nombre del autorizado
            /// </summary>
            public string NombreAutorizado { get; set; }
        
            /// <summary>
            /// Cargo del autorizado
            /// </summary>
            public string CargoAutorizado { get; set; }

            /// <summary>
            /// Subdirección del autorizado
            /// </summary>
            public string SubdireccionAutorizado { get; set; }

            /// <summary>
            /// Grupo del autorizado
            /// </summary>
            public string GrupoAutorizado { get; set; }

            /// <summary>
            /// Firma
            /// </summary>
            public byte[] Firma { get; set; }

            /// <summary>
            /// Longitud de la imagen de la firma
            /// </summary>
            public int LongitudFirma { get; set; }

            /// <summary>
            /// Tipo de la imagen de la firma
            /// </summary>
            public string TipoFirma { get; set; }

            /// <summary>
            /// Nombre de la imagen de la firma
            /// </summary>
            public string NombreFirma { get; set; }

            /// <summary>
            /// Email del autorizado
            /// </summary>
            public string EmailAutorizado { get; set; }

            /// <summary>
            /// Indica si la firma se encuentra activa
            /// </summary>
            public bool Activo { get; set; }

        #endregion

    }
}
