using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.GrupoYUsuarios;
using SILPA.AccesoDatos.Generico;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Xml;

namespace SILPA.LogicaNegocio.GrupoYUsuarios
{
    public class GrupoYMenu
    {
         /// <summary>
        /// Constructor
        /// </summary>
        public GrupoYMenu()
        {
        }

        public static List<MenuEntity> ConsultarMenu()
        {
            MenuDalc menuD = new MenuDalc();
            return menuD.ConsultarListaMenu(); 
        }

        public static List<GrupoEntity> ConsultarGrupo()
        {
            GrupoDalc grupoD = new GrupoDalc();
            return grupoD.ConsultaListaGrupo(); 
        }

        public bool ConsultarGrupoMenuOpcion(string grupoMenuId)
        {
            GrupoMenuOpcionDalc grupoMenu = new GrupoMenuOpcionDalc();
            return grupoMenu.ConsultarGrupoMenuOpcion(grupoMenuId);
        }

        public DataSet ConsultarGrupoMenuOpcion( int grupoMenuId, string grupoMenuNombre ) {
            GrupoMenuOpcionDalc grupoMenu = new GrupoMenuOpcionDalc( );
            return grupoMenu.ConsultarGrupoMenuOpcion( grupoMenuId, grupoMenuNombre );
        }

        public DataSet ConsultarGrupoMenuOpcion(string grupoId, string MenuId)
        {
            GrupoMenuOpcionDalc grupoMenu = new GrupoMenuOpcionDalc();
            return grupoMenu.ConsultarGrupoMenuOpcion(grupoId, MenuId);
        }

        public DataSet ConsultarGrupoMenuOpcion( string grupoId, string MenuId, string nombreMenu ) {
            GrupoMenuOpcionDalc grupoMenu = new GrupoMenuOpcionDalc( );
            return grupoMenu.ConsultarGrupoMenuOpcion( grupoId, MenuId, nombreMenu );
        }

        public DataSet ConsultarGrupoMenuOpcion( string grupoId, string MenuId, string nombreMenu, string gmoId ) {
            GrupoMenuOpcionDalc grupoMenu = new GrupoMenuOpcionDalc( );
            return grupoMenu.ConsultarGrupoMenuOpcion( grupoId, MenuId, nombreMenu, gmoId );
        }

        public static DataSet ListarMenuOpcion(int menuId, int opcionId)
        {
            MenuDalc menuD = new MenuDalc();
            return menuD.ListarMenuOpcion(menuId, opcionId);
        }

        public DataSet ConsultarMenuOpcion(string menuId, string opcionId)
        {
            MenuDalc menuD = new MenuDalc();
            return menuD.ConsultarMenuOpcion(menuId, opcionId);
        }

        public DataSet ConsultarGrupo(string grupoId)
        {
            GrupoDalc grupoD = new GrupoDalc();
            return grupoD.ConsultarGrupo(grupoId); 

        }

        /// <summary>
        /// Inserta un item de grupo menu opcion
        /// </summary>
        /// <param name="objIdentity">Identidad del grupo menu opcion</param>
        public int InsertarGrupoMenuOpcion(GrupoMenuOpcionEntity objIdentity)
        {
            GrupoMenuOpcionDalc grupoMenu = new GrupoMenuOpcionDalc();
            return grupoMenu.InsertarGrupoMenuOpcion(objIdentity); 
        }

        /// <summary>
        /// Inserta un item de grupo menu opcion
        /// </summary>
        /// <param name="objIdentity">Identidad del grupo menu opcion</param>
        public void ActualizarGrupoMenuOpcion(GrupoMenuOpcionEntity objIdentity)
        {
            GrupoMenuOpcionDalc grupoMenu = new GrupoMenuOpcionDalc();
            grupoMenu.ActualizarGrupoMenuOpcion(objIdentity);
        }

        /// <summary>
        /// Inserta un item de grupo menu opcion
        /// </summary>
        /// <param name="objIdentity">Identidad del grupo menu opcion</param>
        public void ActivarGrupoMenuOpcion(int grupoMenuId, bool activo)
        {
            GrupoMenuOpcionDalc grupoMenu = new GrupoMenuOpcionDalc();
            grupoMenu.ActivarGrupoMenuOpcion(grupoMenuId, activo);
        }

        /// <summary>
        ///Genera el archivo xml
        /// </summary>
        /// <param name="objIdentity">Identidad del grupo menu opcion</param>
        public void GenerarArchivoXml(string menus, string opciones, string grupos, string pathXml)
        {
            string nombreArchivo = "mMenu" + grupos.Replace(",", "-");
            string xmlMenu = "\r\n<MenuItem ValueField=\"_VALOR_\" TextField=\"_TEXTO_\" NavigateUrlField=\"_URL_\" EnabledField=\"True\" FormatString=\"\" ImageUrlField=\"\" PopOutImageUrlField=\"\" SelectableField=\"\" SeparatorImageUrlField=\"\"  TargetField=\"\" ToolTipField=\"\">_ITEM_\r\n</MenuItem>";
            StringBuilder xmlObject = new StringBuilder();
            //Inicio
            xmlObject.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?> \r\n<!--" + nombreArchivo + "--> \r\n<menu>");
            //Se consultan los menus
            MenuDalc menu = new MenuDalc();
            //DataSet _menu = menu.ListarMenu(menus);
            ////Se consultan las opciones
            //OpcionDalc opcion = new OpcionDalc();
            //DataSet _opcion = opcion.ListarOpcion(opciones);

            DataSet dsMenu = menu.ConsultarMenusSeleccionados(menus);//menu.ConsultarMenu();
           DataRow[] menuNivel1 = dsMenu.Tables[0].Select("NIVEL_MENU=1");
           foreach (DataRow rowMenu in  menuNivel1)
           {
               // Primer Nivel

               string xmlMenuItem = xmlMenu;
               xmlMenuItem = xmlMenuItem.Replace("_VALOR_", rowMenu["TITULO_MENU"].ToString());
               xmlMenuItem = xmlMenuItem.Replace("_TEXTO_", rowMenu["DESCRIPCION_MENU"].ToString());
               xmlMenuItem = xmlMenuItem.Replace("_URL_", "");
               // segundo  Nivel
               string filtro = "MENU_PADRE_ID = " + rowMenu["MENU_ID"].ToString();
               string xmlSubMenuItem = string.Empty;
               DataRow[] rows = dsMenu.Tables[0].Select(filtro);
               foreach (DataRow row in rows)
               {
                   string xmlSubMenu = xmlMenu;
                   xmlSubMenu = xmlSubMenu.Replace("_VALOR_", row["TITULO_MENU"].ToString());
                   xmlSubMenu = xmlSubMenu.Replace("_TEXTO_", row["DESCRIPCION_MENU"].ToString());
                   xmlSubMenu = xmlSubMenu.Replace("_URL_", row["URL_MENU"].ToString());

                   // Tercer Nivel
                   string filtro3Nivel = "MENU_PADRE_ID = " + row["MENU_ID"].ToString();
                   DataRow[] rows3Nivel = dsMenu.Tables[0].Select(filtro3Nivel);
                   string xmlSubMenu3Nivel = String.Empty;
                   foreach (DataRow row3Nivel in rows3Nivel)
                   {
                       xmlSubMenu3Nivel = xmlMenu;
                       xmlSubMenu3Nivel = xmlSubMenu3Nivel.Replace("_VALOR_", row3Nivel["TITULO_MENU"].ToString());
                       xmlSubMenu3Nivel = xmlSubMenu3Nivel.Replace("_TEXTO_", row3Nivel["DESCRIPCION_MENU"].ToString());
                       xmlSubMenu3Nivel = xmlSubMenu3Nivel.Replace("_URL_", row3Nivel["URL_MENU"].ToString());
                       xmlSubMenu3Nivel = xmlSubMenu3Nivel.Replace("_ITEM_", "");
                       xmlSubMenuItem = xmlSubMenuItem + xmlSubMenu3Nivel;
                   }

                   xmlSubMenu = xmlSubMenu.Replace("_ITEM_", xmlSubMenuItem);
                   xmlSubMenuItem = xmlSubMenu;// xmlSubMenuItem + xmlSubMenu;

               }
               xmlMenuItem = xmlMenuItem.Replace("_ITEM_", xmlSubMenuItem);
               xmlObject.Append(xmlMenuItem);
           }
            //Por cada menu
            //foreach (DataRow rowMenu in _menu.Tables[0].Rows)
            //{
            //    //Se crea el menu                
            //    string xmlMenuItem = xmlMenu;
            //    xmlMenuItem = xmlMenuItem.Replace("_VALOR_", rowMenu["MNU_MENU"].ToString());
            //    xmlMenuItem = xmlMenuItem.Replace("_TEXTO_", rowMenu["MNU_MENU"].ToString());
            //    xmlMenuItem = xmlMenuItem.Replace("_URL_", "");

            //    //Se crean los submenus
            //    string filtro = "MNU_ID = " + rowMenu["MNU_ID"].ToString();
            //    string xmlSubMenuItem = string.Empty;
            //    DataRow[] rows = _opcion.Tables[0].Select(filtro);
            //    foreach (DataRow row in rows)
            //    {
            //        string xmlSubMenu = xmlMenu;
            //        xmlSubMenu = xmlSubMenu.Replace("_VALOR_", row["OPC_VALOR"].ToString());
            //        xmlSubMenu = xmlSubMenu.Replace("_TEXTO_", row["OPC_OPCION"].ToString());
            //        xmlSubMenu = xmlSubMenu.Replace("_URL_", row["OPC_HTTP"].ToString());
            //        xmlSubMenu = xmlSubMenu.Replace("_ITEM_", "");
            //        xmlSubMenuItem = xmlSubMenuItem + xmlSubMenu;

            //    }

            //    }
            //    xmlMenuItem = xmlMenuItem.Replace("_ITEM_", xmlSubMenuItem);
            //    xmlObject.Append(xmlMenuItem);
            //}
            //Fin
            xmlObject.Append("\r\n</menu>");

            //Escribe el archivo fisicamente
            nombreArchivo = pathXml + nombreArchivo + ".xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlObject.ToString());
            xmlDoc.Save(nombreArchivo);      
           
        }

    }
}
