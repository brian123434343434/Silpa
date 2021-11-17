using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GrupoYUsuarios
{
    public class MenuOpcionEntity
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Id de la opcion relacionada en la entidad
        /// </summary>
        private OpcionEntity _opcion;
        public OpcionEntity Opcion
        {
            get { return _opcion; }
            set { _opcion = value; }
        }

        /// <summary>
        /// Id del menu relacionado en la entidad
        /// </summary>
        private MenuEntity _menu;
        public MenuEntity Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }
    }
}
