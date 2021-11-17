using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.DAA
{
    public class DAASolicitudEstadoReasignacionEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador de los estados de solicitud de reasignacion
            /// </summary>
            public int EstadoID { get; set; }

            /// <summary>
            /// Descripción del estado
            /// </summary>
            public string Estado { get; set; }            

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
