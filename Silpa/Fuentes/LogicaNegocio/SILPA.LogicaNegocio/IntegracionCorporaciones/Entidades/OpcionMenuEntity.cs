using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.IntegracionCorporaciones.Entidades
{
    public class OpcionMenuEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador de la opcion del menu
            /// </summary>
            public int OpcionMenuID { get; set; }

            /// <summary>
            /// Orden dentro del menu
            /// </summary>
            public int Orden { get; set; }

            /// <summary>
            /// Opcion que se muestra en el menu
            /// </summary>
            public string Opcion { get; set; }

            /// <summary>
            /// URL de la pagina
            /// </summary>
            public string URL { get; set; }

            /// <summary>
            /// Listado de opciones hijo
            /// </summary>
            public List<OpcionMenuEntity> OpcionesHijo { get; set; }


        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "OpcionMenuID: " + this.OpcionMenuID.ToString() + " -- " +
                                    "Orden: " + this.Orden.ToString() + " -- " +
                                    "Opcion: " + (this.Opcion ?? "null")  + " -- " +
                                    "URL: " + (this.URL ?? "null");

                strDatos += "OpcionesHijo: ";
                if (this.OpcionesHijo != null)
                    foreach (OpcionMenuEntity objOpcionHijo in this.OpcionesHijo)
                        strDatos += objOpcionHijo.ToString() + " -- ";
                else
                    strDatos += "null";

                return strDatos;
            }

        #endregion
    }
}
