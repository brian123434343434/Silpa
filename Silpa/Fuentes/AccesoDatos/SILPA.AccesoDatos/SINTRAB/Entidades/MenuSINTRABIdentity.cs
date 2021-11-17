using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.SINTRAB
{
    public class MenuSINTRABIdentity
    {
        public class Menu
        {
            public Menu()
            {
                ObjLstSubmenus = new List<SubMenu>();
            }

            public int ID_MENU { get; set; }
            public string NOMBRE_MENU { get; set; }
            public Boolean SN_HABILITADO { get; set; }
            public List<SubMenu> ObjLstSubmenus { get; set; }
        }

        public class SubMenu
        {
            public int ID_MENU_PADRE { get; set; }
            public int ID_SUBMENU { get; set; }
            public string TXT_URL { get; set; }
            public string NOMBRE_SUBMENU { get; set; }
            public bool SN_HABILITADO { get; set; }
        }

    }
}
