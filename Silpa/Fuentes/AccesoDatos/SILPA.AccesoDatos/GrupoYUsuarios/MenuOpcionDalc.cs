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
    public class MenuOpcionDalc
    {
        private Configuracion objConfiguracion; 

        public MenuOpcionDalc()
        {
            objConfiguracion = new Configuracion(); 
        }

        public List<MenuOpcionEntity> ConsultarListaOpcionEntidades(MenuEntity menu)
        {
            MenuOpcionEntity objItemLista;
            object[] obj = { -1, -1, menu.Id };
            List<MenuOpcionEntity> lista = new List<MenuOpcionEntity>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("LISTAR_MENU_OPCION", obj);
            try
            {
                IDataReader dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    objItemLista = new MenuOpcionEntity();
                    objItemLista.Id = Convert.ToInt32(dr["MPO_ID"]);

                    MenuDalc menD = new MenuDalc();
                    MenuEntity menE = new MenuEntity();
                    menE.Id = Convert.ToInt32(Convert.ToInt32(dr["MNU_ID"]));
                    menD.ConsultarMenu(ref menE);
                    objItemLista.Menu = menE;

                    OpcionDalc opcD = new OpcionDalc();
                    OpcionEntity opcE = new OpcionEntity();
                    opcE.Id = Convert.ToInt32(Convert.ToInt32(dr["OPC_ID"]));
                    opcD.ConsultarOpcion(ref opcE);
                    objItemLista.Opcion = opcE;

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
    }
}
