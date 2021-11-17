using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST.Entidades
{
    public class TokenIntegracionAutoridadEntity
    {
        #region Propiedades

            /// <summary>
            /// identificador del servicio
            /// </summary>
            public int ServicioID { get; set; }

            /// <summary>
            /// identificador de la autoridad
            /// </summary>
            public int AutoridadID { get; set; }

            /// <summary>
            /// Token de acceso
            /// </summary>
            public string Token { get; set; }

        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "ServicioID: " + this.ServicioID.ToString() + " -- " +
                                  "AutoridadID: " + this.AutoridadID.ToString() + " -- " + 
                                  "Token: " + (this.Token ?? "null");
                
                return strDatos;
            }

        #endregion
    }
}
