using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using SILPA.Comun;
using SILPA.AccesoDatos.IntegracionCorporaciones.Entidades;
using SILPA.LogicaNegocio.IntegracionCorporaciones;
using SILPA.LogicaNegocio.IntegracionCorporaciones.Entidades;
using SoftManagement.Log;


public partial class plantillas_SilpaMenu : System.Web.UI.MasterPage
{
    
    #region Metodos Privados

        /// <summary>
        /// Inicializa el menu master
        /// </summary>
        private void InicializarMaster()
        {
            //Cargar la fecha
            this.lblFecha.Text = DateTime.Today.ToString("dddd, dd 'de' MMMM 'de' yyyy");

            //Cargar datos de impersonalizacion
            if (Session["UserOld"] != null)
            {
                if (Session["User"].ToString() != Session["UserOld"].ToString())
                {
                    lblInfoImpers.Text = "Usuario: " + Session["UserOld"].ToString() + " actuando en nombre de: " + Session["User"];
                    lnkFinalizarImpersonalizacion.Visible = true;
                }
                else
                    lnkFinalizarImpersonalizacion.Visible = false;
            }
            else
            {
                lblInfoImpers.Visible = false;
                lnkFinalizarImpersonalizacion.Visible = false;
            }
        }


        /// <summary>
        /// Generar el nodo que se adicionara en el menu
        /// </summary>
        /// <param name="p_intAutoridadID">int con el identifiacador de la autoridad a la cual pertenece</param>
        /// <param name="p_objXmlMenu">XmlDocument con la informacion del documento base</param>
        /// <param name="p_objOpcion">OpcionMenuEntity con la informacion de la opcion con la cual se creara el menu</param>
        /// <returns>XmlElement con el nodo que se adicionara al menu</returns>
        private XmlElement GenerarNodoMenu(int p_intAutoridadID, XmlDocument p_objXmlMenu, OpcionMenuEntity p_objOpcion)
        {
            XmlElement objXmlElement = null;
            XmlAttribute objXmlAttribute = null;

            //Se crea el nodo para retornar
            objXmlElement = p_objXmlMenu.CreateElement("MenuItem");
            
            //Cargar los atributos de la opcion
            objXmlAttribute = p_objXmlMenu.CreateAttribute("ValueField");
            objXmlAttribute.Value = "CORP_" + p_intAutoridadID.ToString() + p_objOpcion.OpcionMenuID.ToString();
            objXmlElement.Attributes.Append(objXmlAttribute);
            objXmlAttribute = p_objXmlMenu.CreateAttribute("TextField");
            objXmlAttribute.Value = p_objOpcion.Opcion.Trim();
            objXmlElement.Attributes.Append(objXmlAttribute);            

            //Verificar si tiene hijos
            if (p_objOpcion.OpcionesHijo != null && p_objOpcion.OpcionesHijo.Count > 0)
            {
                //Se asigna url como vacio
                objXmlAttribute = p_objXmlMenu.CreateAttribute("TargetField");
                objXmlAttribute.Value = "";
                objXmlElement.Attributes.Append(objXmlAttribute);
                objXmlAttribute = p_objXmlMenu.CreateAttribute("NavigateUrlField");
                objXmlAttribute.Value = "";
                objXmlElement.Attributes.Append(objXmlAttribute);
                
                //Ciclo que carga los hijos
                p_objOpcion.OpcionesHijo.OrderBy(opcion => opcion.Orden);
                foreach (OpcionMenuEntity objOpcion in p_objOpcion.OpcionesHijo)
                {
                    objXmlElement.AppendChild(this.GenerarNodoMenu(p_intAutoridadID, p_objXmlMenu, objOpcion));
                }
            }
            else
            {
                //Verificar si se tiene especifica link de acceso
                if(!string.IsNullOrWhiteSpace(p_objOpcion.URL))
                {
                    //Se asigna como url la relacionada
                    objXmlAttribute = p_objXmlMenu.CreateAttribute("TargetField");
                    objXmlAttribute.Value = "_blank";
                    objXmlElement.Attributes.Append(objXmlAttribute);
                    objXmlAttribute = p_objXmlMenu.CreateAttribute("NavigateUrlField");
                    objXmlAttribute.Value = p_objOpcion.URL;
                    objXmlElement.Attributes.Append(objXmlAttribute);
                }
                else
                {
                    //Se relaciona link de acceso a pagina de integracion VITAL
                    objXmlAttribute = p_objXmlMenu.CreateAttribute("TargetField");
                    objXmlAttribute.Value = "";
                    objXmlElement.Attributes.Append(objXmlAttribute);
                    objXmlAttribute = p_objXmlMenu.CreateAttribute("NavigateUrlField");
                    objXmlAttribute.Value = "~/Corporaciones/FormaCorporacion.aspx?corp=" + EnDecript.Encriptar(p_intAutoridadID.ToString()) + "&men=" + EnDecript.Encriptar(p_objOpcion.OpcionMenuID.ToString());
                    objXmlElement.Attributes.Append(objXmlAttribute);
                }
            }

            return objXmlElement;
        }


        /// <summary>
        /// Cargar los menus que correspondan a un usuario a las corporaciones
        /// </summary>
        private void AdicionarMenuCorporaciones(ref XmlDocument p_objXmlMenu, long p_lbgUsuarioID, string p_strRoles)
        {
            XmlNode objNodoRaiz = null;
            XmlNode objNodoFinal = null;
            List<IntegracionCorporacionEntity> objLstAutoridades = null;
            List<OpcionMenuEntity> objLstOpcionesMenu = null;

            try
            {
                //Obtener el listado de menus de las corporaciones
                objLstAutoridades = IntegracionCorporacion.GetInstance().ObtenerCorporacionesIntegradas();

                //Verificar si existen autoridades integradas
                if (objLstAutoridades != null && objLstAutoridades.Count > 0)
                {
                    //Obtener el menu raiz
                    objNodoRaiz = p_objXmlMenu.DocumentElement;

                    //Obtener referencia nodo final
                    objNodoFinal = objNodoRaiz.LastChild;

                    //Ciclo que carga las opciones de menu
                    foreach (IntegracionCorporacionEntity objAutoridad in objLstAutoridades)
                    {
                        try
                        {
                            //Obtener el listado de opciones de la autoridad
                            objLstOpcionesMenu = IntegracionCorporacion.GetInstance().ObtenerMenusEntidad(objAutoridad.AutoridadID, p_strRoles);

                            //Verificar si se obtuvo opciones de menu
                            if (objLstOpcionesMenu != null && objLstOpcionesMenu.Count > 0)
                            {
                                //Ciclo que carga los listados raices de la  autoridad
                                objLstOpcionesMenu.OrderBy(opcion => opcion.Orden);
                                foreach (OpcionMenuEntity objOpcion in objLstOpcionesMenu)
                                {
                                    //Adicionar al nodo el elemento de la corporacion
                                    objNodoRaiz.InsertBefore(this.GenerarNodoMenu(objAutoridad.AutoridadID, p_objXmlMenu, objOpcion), objNodoRaiz.LastChild);
                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            //Escribir error
                            SMLog.Escribir(Severidad.Critico, "plantillas_SilpaMenu :: AdicionarMenuCorporaciones: Error cargando los menus de la corporacion: \nDatos:" + (objAutoridad != null ? objAutoridad.ToString() : "null") + "\nError: " + exc.ToString() + " - " + (exc.InnerException != null ? exc.InnerException.ToString() : "null"));
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "plantillas_SilpaMenu :: AdicionarMenuCorporaciones: Error cargando los menus de las corporaciones: " + exc.ToString() + " - " + (exc.InnerException != null ? exc.InnerException.ToString() : "null"));                
            }
        }


        /// <summary>
        /// Cargar el menu de la aplicacion
        /// </summary>
        private void CargarMenu()
        {
            XmlDocument objXMLMenu = null;
            string strFileMenu = "";

            //Obtener el path del menu que se debe cargar
            strFileMenu = ConfigurationManager.AppSettings["RUTA_XML"] + "mMenu" + DatosSesion.DatosUsuario.MenuAsociado + ".xml";

            //Verificar que se encuentre el menu
            if (System.IO.File.Exists(strFileMenu))
            {
                //Obtener el xml del menu
                objXMLMenu = new XmlDocument();
                objXMLMenu.Load(strFileMenu);

                //Adicionar menu de corporaciones
                this.AdicionarMenuCorporaciones(ref objXMLMenu, DatosSesion.IdUsuario, DatosSesion.DatosUsuario.MenuAsociado);
            }
            else
            {
                //Obtener el xml del menu vacio
                objXMLMenu = new XmlDocument();
                objXMLMenu.Load(ConfigurationManager.AppSettings["RUTA_XML"] + "mMenuVacio.xml");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('El menú no esta configurado correctamente, comuniquese con el administrador.')</script>");
            }
            this.xdsMenu.Data = objXMLMenu.OuterXml;
            this.xdsMenu.XPath = "/*/*";
        }

    #endregion


    #region Eventos

        #region Page

            /// <summary>
            /// Evento que inicializa la pagina maestra
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                try
                {
                    //Inicializar la informacion de la pagina master
                    this.InicializarMaster();

                    //Verificar que no sea post back
                    if (!IsPostBack)
                    {
                        //Cargar el menu
                        this.CargarMenu();

                    }
                }
                catch(Exception exc)
                {
                    //Escribir Log
                    SMLog.Escribir(Severidad.Critico, "plantillas_SilpaMenu :: Page_Load: Se presento un errror cargando la plantilla master. Error: " + exc.Message + " - " + exc.StackTrace.ToString());

                    //Mostrar alert indicando que no se puede cargar de manera correcta la pagina
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>alert('Se presento un error cargando la plantilla de la pagina.')</script>");

                } 

            }



        #endregion


        #region Impersonalizacion

            /// <summary>
            ///Finaliza el proceso de impersonalizacion 
            /// </summary>
            protected void lnkFinalizarImpersonalizacion_Click(object sender, EventArgs e)
            {
                Session["User"] = Session["UserOld"];
                Session["UserOld"] = null;
                Response.Redirect(String.Format(ConfigurationManager.AppSettings["URLFinalizarSilpa"].ToString()));
            }

        #endregion

    #endregion

}
