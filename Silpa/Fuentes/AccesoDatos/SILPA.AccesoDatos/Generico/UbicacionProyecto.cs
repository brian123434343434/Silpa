using System;
using System.Data;
using System.Configuration;
/// <summary>
/// Summary description for Movimiento
/// </summary>
/// 

namespace SILPA.AccesoDatos.Generico
{

    public class UbicacionProyecto
    {
       
        /// <summary>
        /// Identificador del Departamento
        /// <summary>
        private int _depId;

        /// <summary>
        /// Identificador del Municipio
        /// <summary
        private int _munId;

        /// <summary>
        /// Identificador de la Vereda
        /// <summary
        private int _verId;

        /// <summary>
        /// Identificador del Corregimiento
        /// <summary
        private int _corId;

        /// <summary>
        /// Identificador de la Cuenca: Area Hidrografica
        /// <summary
        private int _areId;

        /// <summary>
        /// Identificador de la Cuenca: Zona Hidrografica
        /// <summary
        private int _zonId;

        /// <summary>
        /// Identificador de la Cuenca: Subzona Hidrologica
        /// <summary
        private int _subId;

        /// <summary>
        /// Nombre del Departamento
        /// <summary>
        private string _depNombre;

        /// <summary>
        /// Nombre del Municipio
        /// <summary
        private string _munNombre;

        /// <summary>
        /// Nombre de la Vereda
        /// <summary
        private string _verNombre;

        /// <summary>
        /// Nombre del Corregimiento
        /// <summary
        private string _corNombre;

        /// <summary>
        /// Nombre de Area Hidrografica
        /// <summary
        private string _areNombre;

        /// <summary>
        /// Nombre de Zona Hidrografica
        /// <summary
        private string _zonNombre;

        /// <summary>
        /// Nombre de Subzona Hidrologica
        /// <summary
        private string _subNombre;

        public int DepId
        {
            get
            {
                return _depId;
            }
            set
            {
                this._depId = value;
            }
        }

        public int MunId
        {
            get
            {
                return _munId;
            }
            set
            {
                this._munId = value;
            }
        }

        public int VerId
        {
            get
            {
                return _verId;
            }
            set
            {
                this._verId = value;
            }
        }

        public int CorId
        {
            get
            {
                return _corId;
            }
            set
            {
                this._corId = value;
            }
        }

        public int AreId
        {
            get
            {
                return _areId;
            }
            set
            {
                this._areId = value;
            }
        }
        public int ZonId
        {
            get
            {
                return _zonId;
            }
            set
            {
                this._zonId = value;
            }
        }
        public int SubId
        {
            get
            {
                return _subId;
            }
            set
            {
                this._subId = value;
            }
        }

        public string DepNombre
        {
            get
            {
                return _depNombre;
            }
            set
            {
                this._depNombre = value;
            }
        }

        public string MunNombre
        {
            get
            {
                return _munNombre;
            }
            set
            {
                this._munNombre = value;
            }
        }

        public string VerNombre
        {
            get
            {
                return _verNombre;
            }
            set
            {
                this._verNombre = value;
            }
        }

        public string CorNombre
        {
            get
            {
                return _corNombre;
            }
            set
            {
                this._corNombre = value;
            }
        }

        public string AreNombre
        {
            get
            {
                return _areNombre;
            }
            set
            {
                this._areNombre = value;
            }
        }
        public string ZonNombre
        {
            get
            {
                return _zonNombre;
            }
            set
            {
                this._zonNombre = value;
            }
        }
        public string SubNombre
        {
            get
            {
                return _subNombre;
            }
            set
            {
                this._subNombre = value;
            }
        }

        public UbicacionProyecto()
        {
        }
    }

}
