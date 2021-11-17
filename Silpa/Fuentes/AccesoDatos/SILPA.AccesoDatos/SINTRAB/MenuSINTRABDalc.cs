using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SoftManagement.Log;
namespace SILPA.AccesoDatos.SINTRAB
{
    public class MenuSINTRABDalc
    {
        private Configuracion objConfiguracion;

        public MenuSINTRABDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<MenuSINTRABIdentity.Menu> ConultarMenuSINTRAB()
        {
            try
            {
                //MenuSINTRABIdentity.Menu ObjMenu = null;
                List<MenuSINTRABIdentity.Menu> ObjLstMenu = new List<MenuSINTRABIdentity.Menu>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_MENU_SINTRAB");
                using(IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ObjLstMenu.Add(new MenuSINTRABIdentity.Menu
                        {
                            ID_MENU = Convert.ToInt32(reader["ID_MENU"]),
                            NOMBRE_MENU = reader["NOMBRE_MENU"].ToString(),
                            SN_HABILITADO = Convert.ToBoolean(reader["SN_HABILITADO"]),
                            ObjLstSubmenus = new List<MenuSINTRABIdentity.SubMenu>(ConsultarSubmenuSINTRAB(Convert.ToInt32(reader["ID_MENU"])))
                        });

                    }
                }

                return ObjLstMenu;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "MenuSINTRABDalc :: ConultarMenuSINTRAB -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }

            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "MenuSINTRABDalc :: ConultarMenuSINTRAB -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }


        public List<MenuSINTRABIdentity.SubMenu> ConsultarSubmenuSINTRAB(int id_menu_padre)
        {
            try
            {
                List<MenuSINTRABIdentity.SubMenu> ObjLstSubmenu = new List<MenuSINTRABIdentity.SubMenu>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_SUBMENU_SINTRAB");
                db.AddInParameter(cmd, "P_ID_MENU_PADRE", DbType.Int32, id_menu_padre);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ObjLstSubmenu.Add( new MenuSINTRABIdentity.SubMenu
                        {
                            ID_MENU_PADRE = Convert.ToInt32(reader["ID_MENU_PADRE"]),
                            ID_SUBMENU = Convert.ToInt32(reader["ID_SUBMENU"]),
                            NOMBRE_SUBMENU = reader["NOMBRE_SUBMENU"].ToString(),
                            TXT_URL = reader["TXT_URL"].ToString(),
                            SN_HABILITADO = Convert.ToBoolean(reader["SN_HABILITADO"])
                        });
                    }
                }
                return ObjLstSubmenu;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "MenuSINTRABDalc :: ConsultarSubmenuSINTRAB -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }

            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "MenuSINTRABDalc :: ConsultarSubmenuSINTRAB -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }


        }
    }
}
