using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{

    public class SeguimientoRutaSalvoconductoIdentity
    {
        #region Variables de la Tabla Log Consulta
            int _LogID;
            int _AutRevisoraID;
            int _SalvoConductoID;
            int _DptoID;
            int _MunID;
            string _IdentificacionRevisor;
            string _NombreRevisor;
            string _IdAplicationUser;
            string _NumeroSalvoconducto;
            string _CodigoSeguridad;
        #endregion

        #region Variables de la tabla de punto de control 
            int _Orden;
            decimal _Latitud;
            decimal _Longitud;
        #endregion

        #region Datos del salvoconducto 
        string _Estado_Descripcion;
        int _estadoID;
            string _Tipo_Salvoconducto;
            string _Fecha_Ini_Vigencia;
            string _Fecha_Fin_Vigencia;
            string _Mensaje_Error;
            bool _Sn_Error;
        #endregion

        #region PROPIEDADES DE LA TABLA DE LOG
            public int LogID
            {
                get
                {
                    return _LogID;
                }

                set
                {
                    _LogID = value;
                }
            }

            public int AutRevisoraID
        {
            get
            {
                return _AutRevisoraID;
            }

            set
            {
                _AutRevisoraID = value;
            }
        }

            public int SalvoConductoID
            {
                get
                {
                    return _SalvoConductoID;
                }

                set
                {
                    _SalvoConductoID = value;
                }

            }

            public int DptoID
            {
                get
                {
                    return _DptoID;
                }

                set
                {
                    _DptoID = value;
                }
            }

            public int MunID
            {
                get
                {
                    return _MunID;
                }

                set
                {
                    _MunID = value;
                }
            }

            public string IdAplicationUser
        {
            get
            {
                return _IdAplicationUser;
            }

            set
            {
                _IdAplicationUser = value;
            }
        }

            public string IdentificacionRevisor
        {
            get
            {
                return _IdentificacionRevisor;
            }

            set
            {
                _IdentificacionRevisor = value;
            }
        }

            public string NombreRevisor
        {
            get
            {
                return _NombreRevisor;
            }

            set
            {
                _NombreRevisor = value;
            }
        }

            public string NumeroSalvoconducto
            {
                get
                {
                    return _NumeroSalvoconducto;
                }

                set
                {
                    _NumeroSalvoconducto = value;
                }
            }

            public string CodigoSeguridad
            {
                get
                {
                    return _CodigoSeguridad;
                }

                set
                {
                    _CodigoSeguridad = value;
                }
            }
        #endregion

        #region PROPIEDADES DEL PUNTO DE CONTROL
            public int Orden
            {
                get
                {
                    return _Orden;
                }

                set
                {
                    _Orden = value;
                }
            }

            public decimal Latitud
            {
                get
                {
                    return _Latitud;
                }

                set
                {
                    _Latitud = value;
                }
            }

            public decimal Longitud
            {
                get
                {
                    return _Longitud;
                }

                set
                {
                    _Longitud = value;
                }
            }
        #endregion

        #region PROPIEDADES DEL ESTADO DEL SALVOCONDUCTO
        public string Estado_Descripcion
        {
            get
            {
                return _Estado_Descripcion;
            }

            set
            {
                _Estado_Descripcion = value;
            }
        }
        public int EstadoID
        {
            get
            {
                return _estadoID;
            }

            set
            {
                _estadoID = value;
            }
        }

        public string Fecha_Ini_Vigencia
        {
            get
            {
                return _Fecha_Ini_Vigencia;
            }

            set
            {
                _Fecha_Ini_Vigencia = value;
            }
        }

        public string Fecha_Fin_Vigencia
        {
            get
            {
                return _Fecha_Fin_Vigencia;
            }

            set
            {
                _Fecha_Fin_Vigencia = value;
            }
        }

        public string Mensaje_Error
        {
            get
            {
                return _Mensaje_Error;
            }

            set
            {
                _Mensaje_Error = value;
            }
        }

        public bool Sn_Error
        {
            get
            {
                return _Sn_Error;
            }

            set
            {
                _Sn_Error = value;
            }
        }

        public string Tipo_Salvoconducto
        {
            get
            {
                return _Tipo_Salvoconducto;
            }

            set
            {
                _Tipo_Salvoconducto = value;
            }
        }

        #endregion

    }




}
