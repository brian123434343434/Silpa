using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    
    public class ParametrizacionSunlIdentity
    {

        public class EspecieTaxonomia
        {
            private int _EspecieTaxonimiaID;
            private string _NombreCientifico;
            private int _ClaseRecursoID;
            private string _strClaseREcurso;
            private string _NombreComun;
            private string _CodigoIdeam;

            public int EspecieTaxonimiaID
            {
                get
                {
                    return _EspecieTaxonimiaID;
                }

                set
                {
                    _EspecieTaxonimiaID = value;
                }
            }

            public string NombreCientifico
            {
                get
                {
                    return _NombreCientifico;
                }

                set
                {
                    _NombreCientifico = value;
                }
            }

            public string NombreComun
            {
                get
                {
                    return _NombreComun;
                }

                set
                {
                    _NombreComun = value;
                }
            }

            public string CodigoIdeam
            {
                get
                {
                    return _CodigoIdeam;
                }

                set
                {
                    _CodigoIdeam = value;
                }
            }

            public string StrClaseREcurso
            {
                get
                {
                    return _strClaseREcurso;
                }

                set
                {
                    _strClaseREcurso = value;
                }
            }

            public int ClaseRecursoID
            {
                get
                {
                    return _ClaseRecursoID;
                }

                set
                {
                    _ClaseRecursoID = value;
                }
            }
        }

        public class TipoProducto
        {
            private int _TipoProductoID;
            private string _strTipoProducto;
            private bool _Salvoconducto;
            private bool _Aprovechamiento;
            private string _Formula;
            private string _CodigoIdeam;

            public int TipoProductoID
            {
                get
                {
                    return _TipoProductoID;
                }

                set
                {
                    _TipoProductoID = value;
                }
            }

            public string StrTipoProducto
            {
                get
                {
                    return _strTipoProducto;
                }

                set
                {
                    _strTipoProducto = value;
                }
            }

            public bool Salvoconducto
            {
                get
                {
                    return _Salvoconducto;
                }

                set
                {
                    _Salvoconducto = value;
                }
            }

            public bool Aprovechamiento
            {
                get
                {
                    return _Aprovechamiento;
                }

                set
                {
                    _Aprovechamiento = value;
                }
            }

            public string Formula
            {
                get
                {
                    return _Formula;
                }

                set
                {
                    _Formula = value;
                }
            }

            public string CodigoIdeam
            {
                get
                {
                    return _CodigoIdeam;
                }

                set
                {
                    _CodigoIdeam = value;
                }
            }
        }

        public class ClaseProducto
        {
            private int _ClaseProductoID;
            private int _ClaseRecursoID;
            private string _strClaseProducto;
            private string _strClaseRecurso;
            private bool _Salvoconducto;
            private bool _Aprovechamiento;
            private string _CodigoIdeam;

            public int ClaseProductoID
            {
                get
                {
                    return _ClaseProductoID;
                }

                set
                {
                    _ClaseProductoID = value;
                }
            }

            public int ClaseRecursoID
            {
                get
                {
                    return _ClaseRecursoID;
                }

                set
                {
                    _ClaseRecursoID = value;
                }
            }


            public bool Salvoconducto
            {
                get
                {
                    return _Salvoconducto;
                }

                set
                {
                    _Salvoconducto = value;
                }
            }

            public bool Aprovechamiento
            {
                get
                {
                    return _Aprovechamiento;
                }

                set
                {
                    _Aprovechamiento = value;
                }
            }

            public string CodigoIdeam
            {
                get
                {
                    return _CodigoIdeam;
                }

                set
                {
                    _CodigoIdeam = value;
                }
            }

            public string StrClaseRecurso
            {
                get
                {
                    return _strClaseRecurso;
                }

                set
                {
                    _strClaseRecurso = value;
                }
            }

            public string StrClaseProducto
            {
                get
                {
                    return _strClaseProducto;
                }

                set
                {
                    _strClaseProducto = value;
                }
            }
        }
        [Serializable]
        public class ClaseProductoXTipoProducto
        {
            public int ClaseProductoID { get; set; }
            public int TipoProductoID { get; set; }
            public string StrTipoProducto { get; set; }
            public string StrClaseProducto { get; set; }
            public bool Salvoconducto { get; set; }
            public bool Aprovechamiento { get; set; }

        }
        [Serializable]
        public class TipoProductoUnidadMedida
        {
            private int _TipoProductoID;
            private int _UnidadMedidaID;
            private string _strUnidadMedida;
            private string _strTipoProducto;

            public int TipoProductoID
            {
                get
                {
                    return _TipoProductoID;
                }

                set
                {
                    _TipoProductoID = value;
                }
            }

            public int UnidadMedidaID
            {
                get
                {
                    return _UnidadMedidaID;
                }

                set
                {
                    _UnidadMedidaID = value;
                }
            }

            public string StrUnidadMedida
            {
                get
                {
                    return _strUnidadMedida;
                }

                set
                {
                    _strUnidadMedida = value;
                }
            }

            public string StrTipoProducto
            {
                get
                {
                    return _strTipoProducto;
                }

                set
                {
                    _strTipoProducto = value;
                }
            }
        }

    }
}
