using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST.Servicios.ANLA.Notificaciones.Entidades
{
    public class ArchivoRecursoEntity
    {
        #region Propiedades


            /// <summary>
            /// Nombre del archivo
            /// </summary>
            public string NombreArchivo { get; set; }


            /// <summary>
            /// Striing Base 64 con el archivo
            /// </summary>
            public string Archivo { get; set; }


        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "NumeroVital: " + (this.NombreArchivo ?? "null")  + " -- " +
                                    "Observacion: " + (this.Archivo ?? "null");

                return strDatos;
            }

        #endregion
    }
}
