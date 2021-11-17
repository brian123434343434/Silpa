using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.IntegracionCorporaciones.Entidades
{
    public class RespuestaSolicitudAccesoWebEntity
    {
         #region Propiedades


            /// <summary>
            /// Identificador de la sesion generada
            /// </summary>
            public string SessionWebID { get; set; }


            /// <summary>
            /// URL de retorno acceso al aplicativo
            /// </summary>
            public string URLAcceso { get; set; }


        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "SessionWebID: " + (this.SessionWebID ?? "null")  + " -- " +
                                    "URLAcceso: " + (this.URLAcceso ?? "null");

                return strDatos;
            }

        #endregion
    }
}
