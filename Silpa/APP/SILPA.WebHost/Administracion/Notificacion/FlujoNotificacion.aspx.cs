using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.AccesoDatos.Notificacion;
using SILPA.LogicaNegocio.AdmTablasBasicas;
using System.Data;

public partial class Administracion_Notificacion_FlujoNotificacion : System.Web.UI.Page
{

    #region Metodos Privados

        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.divMensaje.Visible = true;
        }

        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensajeModal(string p_strMensaje)
        {
            this.lblMensajeModal.Text = p_strMensaje;
            this.divMensajeModal.Visible = true;
        }

        /// <summary>
        /// Realizar la busqueda de información de los flujos
        /// </summary>
        /// <param name="p_strNombre">string con el nombre del Flujo</param>
        /// <param name="p_strDescripcion">string con el nombre de la descripción</param>
        private void BuscarFlujos(int p_intAutoridadID, string p_strNombre)
        {
            FlujoNotificacionElectronica objFlujoNotificacionElectronica = null;

            try
            {
                //Crear objeto interfaz datos
                objFlujoNotificacionElectronica = new FlujoNotificacionElectronica();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Flujos
                this.grdFlujos.DataSource = objFlujoNotificacionElectronica.ConsultaFlujosNotificacion(null, p_intAutoridadID, p_strNombre);
                this.grdFlujos.DataBind();

            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FlujosNotificacion :: BuscarFlujos -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los Flujos");
            }
        }


        /// <summary>
        /// Guardar la información de un flujo
        /// </summary>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad</param>
        /// <param name="p_strNombre">string con el nombre del Flujo</param>
        private void GuardarFlujo(int p_intAutoridadID, string p_strNombre)
        {
            FlujoNotificacionElectronica objFlujoNotificacionElectronica = null;            

            try
            {
                //Crear el Flujo
                objFlujoNotificacionElectronica = new FlujoNotificacionElectronica();
                objFlujoNotificacionElectronica.CrearFlujoNotificacion(p_intAutoridadID, p_strNombre, true);

                //Consultar los Flujos
                this.BuscarFlujos((!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : -1), this.hdfNombreBuscado.Value);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FlujosNotificacion :: GuardarFlujo -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error guardando el flujo");
            }
            finally
            {
                //Limpiar datos
                this.LimpiarModal();
            }
        }    


        /// <summary>
        /// Verificar si se encuentra autenticado
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            if (DatosSesion.Usuario == string.Empty)
            {
                return false;
            }

            string idFlujo = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

            Session["IDForToken"] = (object)idFlujo;

            Session["Flujo"] = Session["IDForToken"];

            SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

            Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));

            using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
            {
                servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
                string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
                //string mensaje = "Token valido";

                if (mensaje.IndexOf("Token invalido") > 0)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Cargar el listado de formatos existes
        /// </summary>
        /// <param name="p_objLista">DropDownList con la lista en la cual se cargara la información</param>
        private void CargarAutoridades(DropDownList p_objLista)
        {
            Notificacion objNotificacion = null;
            DataSet objAutoridades = null;

            //Consultar las autoridades
            objNotificacion = new Notificacion();
            objAutoridades = objNotificacion.ListarAutoridadAmbientalNotificacion();

            //Cargar informacion en desplegables
            p_objLista.DataSource = objAutoridades;
            p_objLista.DataTextField = "AUT_NOMBRE";
            p_objLista.DataValueField = "AUT_ID";
            p_objLista.DataBind();
            p_objLista.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }

        /// <summary>
        /// Limpia los datos del modal
        /// </summary>
        private void LimpiarModal()
        {
            //Limpia campo de texto
            this.txtNombre.Text = "";

            //Limpiar mensaje
            this.lblMensajeModal.Text = "";
            this.divMensajeModal.Visible = false;
        }


        /// <summary>
        /// Inicializar la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Cargar el listado de autoridades
            this.CargarAutoridades(this.cboAutoridadAmbientalBuscar);
            this.CargarAutoridades(this.cboAutoridad);

            //Buscar los Flujos
            this.BuscarFlujos((!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : -1), this.hdfNombreBuscado.Value);

            //Limpiar los campos modal
            this.LimpiarModal();
        }

    #endregion


    #region Eventos

        #region Page

            /// <summary>
            /// Evento que se ejecuta al cargar la pagina
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {                    
                    //this.InicializarPagina();

                    //Validar sesion de Flujo
                    if (this.ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                    else
                    {
                        //Iniciliazar datos de la página
                        this.InicializarPagina();
                    }
                }
            }

        #endregion


        #region CmdBuscar

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar. Busca el listado de flujos que cumplan los parametros de busqueda
            /// </summary>
            protected void cmdBuscar_Click(object sender, EventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Verificar que los datos de validación sean correctos
                if (Page.IsValid)
                {
                    //Cargar a buscados
                    this.hdfAutoridadIDBuscado.Value = this.cboAutoridadAmbientalBuscar.SelectedValue;
                    this.hdfNombreBuscado.Value = this.txtFlujoBuscar.Text.Trim();

                    //Buscar Flujos
                    this.BuscarFlujos((!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : -1), this.hdfNombreBuscado.Value);
                }
            }

        #endregion
   

        #region cmdGuardarFlujo

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar Flujo. Carga la información del flujo
            /// </summary>
            protected void cmdGuardarFlujo_Click(object sender, EventArgs e)
            {
                //Verificar que los datos de validación sean correctos
                if (Page.IsValid)
                {
                    this.GuardarFlujo(Convert.ToInt32(this.cboAutoridad.SelectedValue), this.txtNombre.Text);
                }     
            }

        #endregion


        #region cmdCancelar

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de cancelar. Limpia los datos del modal
            /// </summary>
            protected void cmdCancelar_Click(object sender, EventArgs e)
            {
                this.LimpiarModal();
            }

        #endregion


        #region grdFlujos

            
            /// <summary>
            /// Evento que se ejecuta cuando se carga la grilla
            /// </summary>
            protected void grdFlujos_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                DropDownList objDropDownList = null;
                DataRowView objFlujo = null;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Cargar datos
                    objFlujo = (DataRowView)e.Row.DataItem;

                    //Seleccionar las opciones
                    objDropDownList = (DropDownList)e.Row.FindControl("cboAutoridad");
                    if (objDropDownList != null)
                    {
                        //Cargar el listado
                        this.CargarAutoridades(objDropDownList);

                        //Seleccionar
                        objDropDownList.SelectedValue = (objFlujo["AUT_ID"] != System.DBNull.Value ? objFlujo["AUT_ID"].ToString() : "-1");
                    }

                    objDropDownList = (DropDownList)e.Row.FindControl("cboEstado");
                    if (objDropDownList != null)
                        objDropDownList.SelectedValue = (objFlujo["ACTIVO"] != System.DBNull.Value && Convert.ToBoolean(objFlujo["ACTIVO"]) ? "1" : "0");
                }
            }


            /// <summary>
            /// Evento que se eejcuta al dar clic en editar. Habilita campos para edición
            /// </summary>
            protected void grdFlujos_RowEditing(object sender, GridViewEditEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Colocar grilla en Flujo de edicion
                this.grdFlujos.EditIndex = e.NewEditIndex;

                //Consultar informacion
                this.BuscarFlujos((!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : -1), this.hdfNombreBuscado.Value);
            }

            
            /// <summary>
            /// Eevnto que se ejecuta al dar clic en Actualizar. Actualiza la información del flujo
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdFlujos_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                FlujoNotificacionElectronica objFlujoNotificacionElectronica = null;
                int intIdFlujo = 0;
                int intAutoridadID = 0;
                string strNombre = "";
                string strActivo = "";

                try
                {
                    //Ocultar mensajes
                    this.lblMensaje.Text = "";
                    this.divMensaje.Visible = false;

                    if (Page.IsValid)
                    {
                        //Cargar datos
                        intAutoridadID = Convert.ToInt32(((DropDownList)grdFlujos.Rows[e.RowIndex].FindControl("cboAutoridad")).SelectedValue);
                        intIdFlujo = Convert.ToInt32(grdFlujos.DataKeys[e.RowIndex].Value.ToString().Trim());
                        strNombre = ((TextBox)grdFlujos.Rows[e.RowIndex].FindControl("txtNombre")).Text.Trim();
                        strActivo = ((DropDownList)grdFlujos.Rows[e.RowIndex].FindControl("cboEstado")).SelectedValue;

                        //Actualizar Flujo
                        objFlujoNotificacionElectronica = new FlujoNotificacionElectronica();
                        objFlujoNotificacionElectronica.EditarFlujoNotificacion(intAutoridadID, intIdFlujo, strNombre, (strActivo == "1" ? true : false));

                        //Cancelanr edicion
                        this.grdFlujos.EditIndex = -1;

                        //Consultar informacion
                        this.BuscarFlujos((!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : -1), this.hdfNombreBuscado.Value);
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FlujosNotificacion :: grdFlujos_RowUpdating  -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error editando el flujo");
                }
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en Cancelar. Cancela la edicion y retorna a vista de consulta de la grilla
            /// </summary>
            protected void grdFlujos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Cancelando edicion
                this.grdFlujos.EditIndex = -1;

                //Consultar informacion
                this.BuscarFlujos((!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : -1), this.hdfNombreBuscado.Value);
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en la paginación. Cambia de pagina
            /// </summary>
            protected void grdFlujos_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Asignar nuevo index
                this.grdFlujos.PageIndex = e.NewPageIndex;

                //Consultar informacion
                this.BuscarFlujos((!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : -1), this.hdfNombreBuscado.Value);
            }

        #endregion

    #endregion


}