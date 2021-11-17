using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GrupoYUsuarios
{
    public class MenuEntity
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
        /// Drescripcion textual de la entidad
        /// </summary>
        private string _menu;
        public string Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        /// <summary>
        /// Bit que indica si la entidad estpa activa o no.
        /// </summary>
        private bool _activo;
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        /// <summary>
        /// Indica si el menu tiene un padre o no.
        /// </summary>
        private MenuEntity _menuPadre;
        public MenuEntity MenuPadre
        {
            get { return _menuPadre; }
            set { _menuPadre = value; }
        } 
    }
}
