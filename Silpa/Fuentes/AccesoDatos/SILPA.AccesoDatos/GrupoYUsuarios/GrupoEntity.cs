using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;

namespace SILPA.AccesoDatos.GrupoYUsuarios
{
    public class GrupoEntity : EntidadSerializable 
    {
        /// <summary>
        /// Artributo que representa el ojbeto de configuracion
        /// </summary>
        private Configuracion objConfiguracion;
        
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        } 
        
        /// <summary>
        /// Constructor base de la entidad
        /// </summary>
        public GrupoEntity()
        {
            objConfiguracion = new Configuracion(); 
        }

    }
}
