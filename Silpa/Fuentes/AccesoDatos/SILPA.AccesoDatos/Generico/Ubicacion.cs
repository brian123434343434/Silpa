using System;
using System.Data;
using System.Configuration;
/// <summary>
/// Summary description for Movimiento
/// </summary>
/// 

namespace SILPA.AccesoDatos.Generico
{

    public class Ubicacion
    {
       
        /// <summary>
        /// Identificador del Departamento
        /// <summary>
        private int int_dep_id;

        /// <summary>
        /// Identificador del Municipio
        /// <summary
        private int int_mun_id;

        /// <summary>
        /// Identificador de la Vereda
        /// <summary
        private int int_ver_id;

        /// <summary>
        /// Identificador del Corregimiento
        /// <summary
        private int int_cor_id;

        /// <summary>
        /// Identificador de la Cuenca: Area Hidrografica
        /// <summary
        private int int_are_id;

        /// <summary>
        /// Identificador de la Cuenca: Zona Hidrografica
        /// <summary
        private int int_zon_id;

        /// <summary>
        /// Identificador de la Cuenca: Subzona Hidrologica
        /// <summary
        private int int_sub_id;

        public int DepId
        {
            get
            {
                return int_dep_id;
            }
            set
            {
                this.int_dep_id = value;
            }
        }

        public int MunId
        {
            get
            {
                return int_mun_id;
            }
            set
            {
                this.int_mun_id = value;
            }
        }

        public int VerId
        {
            get
            {
                return int_ver_id;
            }
            set
            {
                this.int_ver_id = value;
            }
        }

        public int CorId
        {
            get
            {
                return int_cor_id;
            }
            set
            {
                this.int_cor_id = value;
            }
        }

        public int AreId
        {
            get
            {
                return int_are_id;
            }
            set
            {
                this.int_are_id = value;
            }
        }
        public int ZonId
        {
            get
            {
                return int_zon_id;
            }
            set
            {
                this.int_zon_id = value;
            }
        }
        public int SubId
        {
            get
            {
                return int_sub_id;
            }
            set
            {
                this.int_sub_id = value;
            }
        }

        public Ubicacion()
        {
        }
    }

}
