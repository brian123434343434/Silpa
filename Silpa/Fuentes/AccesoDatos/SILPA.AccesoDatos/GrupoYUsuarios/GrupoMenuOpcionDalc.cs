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
    public class GrupoMenuOpcionDalc
    {
        Configuracion objConfiguracion;
        public GrupoMenuOpcionDalc()
        {
            objConfiguracion = new Configuracion(); 
        }

        public List<GrupoMenuOpcionEntity> ConsultarGrupoMenu(int idMenu)
        {
            GrupoMenuOpcionEntity objItemLista;
            object[] obj = { -1, idMenu, -1 };
            List<GrupoMenuOpcionEntity> lista = new List<GrupoMenuOpcionEntity>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("LISTAR_GRUPO_MENU_OPCION", obj);
            try
            {
                IDataReader dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    objItemLista = new GrupoMenuOpcionEntity();
                    objItemLista.Id = Convert.ToInt32(dr["GMO_ID"]);

                    MenuDalc menu = new MenuDalc();  
                    MenuEntity menuE = new MenuEntity();
                    menuE.Id = Convert.ToInt32(dr["MNU_ID"].ToString());
                    menu.ConsultarMenu(ref menuE);
                    objItemLista.MenuOpcion = menuE;

                    GrupoDalc grupo = new GrupoDalc();
                    GrupoEntity grupoE = new GrupoEntity();
                    grupoE.Id = Convert.ToInt32(dr["GRP_ID"].ToString());
                    grupo.ConsultaGrupo(ref grupoE);
                    objItemLista.Grupo = grupoE;

                    lista.Add(objItemLista);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd = null;
                }
            }
        }

        /// <summary>
        /// Consulta el id consecutivo maximo de grupo menu opcion
        /// </summary>
        public bool ConsultarGrupoMenuOpcion(string grupoMenuId)
        {
            bool existe = false;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {                         
                           -1, grupoMenuId                      
                        };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_GRUPO_MENU_OPCION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado.Tables[0].Rows.Count > 0)
                existe = true;

            return existe;
        }
        
        /// <summary>
        /// Consulta el id consecutivo maximo de grupo menu opcion
        /// </summary>
        public DataSet ConsultarGrupoMenuOpcion( string grupoId, string MenuId ){
            return ConsultarGrupoMenuOpcion( grupoId, MenuId, "" );
        }

        /// <summary>
        /// Consulta el id consecutivo maximo de grupo menu opcion
        /// </summary>
        public DataSet ConsultarGrupoMenuOpcion( string grupoId, string MenuId, string nombreMenu ) {
            return ConsultarGrupoMenuOpcion( grupoId, MenuId, nombreMenu, "0" );
        }

        /// <summary>
        /// Consulta el id consecutivo maximo de grupo menu opcion
        /// </summary>
        public DataSet ConsultarGrupoMenuOpcion( string grupoId, string MenuId, string nombreMenu, string gmoId )
        {            
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {                         
                           grupoId, MenuId, nombreMenu, gmoId
                        };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_GRUPOS_MENU_OPCION", parametros);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Consulta el id consecutivo maximo de grupo menu opcion
        /// </summary>
        public DataSet ConsultarGrupoMenuOpcion(int grupoMenuId, string grupoMenu)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {                         
                           grupoMenuId, grupoMenu                   
                        };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_GRUPO_MENU_OPCION", parametros);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Inserta un item de grupo menu opcion
        /// </summary>
        /// <param name="objIdentity">Identidad del grupo menu opcion</param>
        public int InsertarGrupoMenuOpcion(GrupoMenuOpcionEntity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {        
                           objIdentity.Id,
                           objIdentity.GrupoId,
                           objIdentity.MenuId,
                           objIdentity.NombreMenu,
                           objIdentity.NombreXMLMenu
                        };

            DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_GRUPO_MENU_OPCION", parametros);
        
            try
            {
                db.ExecuteDataSet(cmd);
                return Int32.Parse(cmd.Parameters["@P_ID"].Value.ToString());
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// Actualiza un item de grupo menu opcion
        /// </summary>
        /// <param name="objIdentity">Identidad del grupo menu opcion</param>
        public void ActualizarGrupoMenuOpcion(GrupoMenuOpcionEntity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {           
                           objIdentity.Id,
                           objIdentity.MenuId,
                           objIdentity.GrupoId,
                           objIdentity.NombreMenu,
                           objIdentity.NombreXMLMenu
                        };

            DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_GRUPO_MENU_OPCION", parametros);

            try
            {
                db.ExecuteDataSet(cmd);
                //return Int32.Parse(cmd.Parameters["ID"].Value.ToString());
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// Actualiza un item de grupo menu opcion
        /// </summary>
        /// <param name="objIdentity">Identidad del grupo menu opcion</param>
        public void ActivarGrupoMenuOpcion(int grupoMenuId, bool activo)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {           
                           grupoMenuId,                          
                           activo                     
                        };

            DbCommand cmd = db.GetStoredProcCommand("BAS_ACTIVAR_GRUPO_MENU_OPCION", parametros);

            try
            {
                db.ExecuteDataSet(cmd);
                //return Int32.Parse(cmd.Parameters["ID"].Value.ToString());
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }   
    }


}
