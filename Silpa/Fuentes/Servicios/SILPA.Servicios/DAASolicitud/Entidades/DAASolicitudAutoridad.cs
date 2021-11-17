using SILPA.Comun;

namespace SILPA.Servicios.DAASolicitud.Entidades
{
    public class DAASolicitudAutoridad : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la autoridad
            /// </summary>
            public int AutoridadID { get; set; }

            /// <summary>
            /// Nombre de la autoridad
            /// </summary>
            public string Autoridad { get; set; }

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
