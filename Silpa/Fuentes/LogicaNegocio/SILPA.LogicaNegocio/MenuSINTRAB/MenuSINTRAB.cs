using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.SINTRAB;

namespace SILPA.LogicaNegocio.MenuSINTRAB
{
    public class MenuSINTRAB
    {
        private MenuSINTRABDalc ObjMenuSINTRABDalc;

        public MenuSINTRAB()
        {
            ObjMenuSINTRABDalc = new MenuSINTRABDalc();
        }

        public List<MenuSINTRABIdentity.Menu> ObtenerMenuSINTRAB()
        {
            List<MenuSINTRABIdentity.Menu> ObjMenus = new List<MenuSINTRABIdentity.Menu>();
            ObjMenus = ObjMenuSINTRABDalc.ConultarMenuSINTRAB();
            return ObjMenus;
        }
    }
}
