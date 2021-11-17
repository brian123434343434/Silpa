using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GrupoYUsuarios
{
    public class GrupoMenuOpcionEntity
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
      
        private string _menuId;
        public string MenuId
        {
            get { return _menuId; }
            set { _menuId = value; }
        }
        private string _grupoId;
        public string GrupoId {
            get { return _grupoId; }
            set { _grupoId = value; }
        }
        private string _nombreMenu;
        public string NombreMenu {
            get { return _nombreMenu; }
            set { _nombreMenu = value; }
        }
        private string _nombreXML_Menu;
        public string NombreXMLMenu {
            get { return _nombreXML_Menu; }
            set { _nombreXML_Menu = value; }
        }


        private GrupoEntity _grupo;
        public GrupoEntity Grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }

        private MenuEntity _menuOpcion;
        public MenuEntity MenuOpcion
        {
            get { return _menuOpcion; }
            set { _menuOpcion = value; }
        }  

    }
}
