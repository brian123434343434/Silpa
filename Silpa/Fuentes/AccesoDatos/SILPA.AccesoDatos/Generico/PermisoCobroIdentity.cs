using System;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class PermisoCobroIdentity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador del permiso
            /// </summary>
            public long PermisoCobroID { get; set; }

            /// <summary>
            /// Identificador del cobro al cual pertenece el permiso
            /// </summary>
            public long CobroID { get; set; }

            /// <summary>
            /// Nombre del permiso
            /// </summary>
            public string Permiso { get; set; }

            /// <summary>
            /// Autoridad a la cual pertenece
            /// </summary>
            public string Autoridad { get; set; }

            /// <summary>
            /// Numero de permisos
            /// </summary>
            public int NumeroPermisos { get; set; }

            /// <summary>
            /// Valor de la administración
            /// </summary>
            public decimal ValorAdministracion { get; set; }

            /// <summary>
            /// Valor del servicio
            /// </summary>
            public decimal ValorServicio { get; set; }

            /// <summary>
            /// Valor Total de los permisos
            /// </summary>
            public decimal ValorTotal { get; set; }

        #endregion


    }
}
