using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.GrupoYUsuarios
{
    public class MenuDalc
    {
        Configuracion objConfiguracion;

        public MenuDalc()
        {
            objConfiguracion = new Configuracion(); 
        }

        public void ConsultarMenu(ref MenuEntity menu)
        {
            List<MenuEntity> lista = new List<MenuEntity>();
            lista = ConsultarListaMenu();
            foreach (MenuEntity m in lista)
            {
                if (m.Id == menu.Id)
                {
                    menu.Menu = m.Menu;
                    menu.MenuPadre = m.MenuPadre;
                    menu.Activo = m.Activo;
                    return; 
                }
            }
        }

        public List<MenuEntity> ConsultarListaMenu()
        {
            MenuEntity objItemLista;
            List<MenuEntity> lista = new List<MenuEntity>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("LISTAR_MENU");
            try
            {
                IDataReader dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    objItemLista = new MenuEntity();
                    objItemLista.Id  = Convert.ToInt32(dr["MNU_ID"]);
                    objItemLista.Menu = dr["MNU_MENU"].ToString();
                    MenuEntity mnuPadre = new MenuEntity();
                    if (dr["MNU_ID_PADRE"] != DBNull.Value)
                    {
                        MenuDalc menuD = new MenuDalc();  
                        mnuPadre.Id = Convert.ToInt32(dr["MNU_ID_PADRE"]);
                        menuD.ConsultarMenu(ref mnuPadre);
                        objItemLista.MenuPadre = mnuPadre;
                    }
                    objItemLista.Activo = Convert.ToBoolean(dr["MNU_ACTIVO"]);
                    lista.Add(objItemLista);
                }
                return lista;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd = null;
                }
            }
        }

        public DataSet ListarMenu(string menuId)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { menuId };

            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_MENU", parametros);

            try
            {
                return db.ExecuteDataSet(cmd);
                //return Int32.Parse(cmd.Parameters["ID"].Value.ToString());
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public DataSet ListarMenuOpcion(int menuId, int opcionId)
        {                     

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { menuId, opcionId };

            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_MENU_OPCION", parametros);

            try
            {
                return db.ExecuteDataSet(cmd);
                //return Int32.Parse(cmd.Parameters["ID"].Value.ToString());
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        public DataSet ConsultarMenuOpcion(string menuId, string opcionId)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { menuId, opcionId };

            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_MENU_OPCION", parametros);

            try
            {
                return db.ExecuteDataSet(cmd);
                //return Int32.Parse(cmd.Parameters["ID"].Value.ToString());
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }


        public DataSet ConsultarMenu()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_MENU_GENERAL_CONSULTAR");
            return db.ExecuteDataSet(cmd);
        }

        public DataSet ConsultarMenusSeleccionados(string menus)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_MENU_GENERAL_SELECCIONADOS_CONSULTAR");
            db.AddInParameter(cmd, "P_MENU_ID", DbType.String, menus);
            return db.ExecuteDataSet(cmd);
        }


    }
}
