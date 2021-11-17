using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Notificacion;
using SILPA.AccesoDatos.Notificacion;
using System.Data;

public partial class Administracion_Notificacion_EstadosNotificacion : System.Web.UI.Page
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
        /// Realizar la busqueda de información de los estados
        /// </summary>
        /// <param name="p_strNombre">string con el nombre del estado</param>
        /// <param name="p_strDescripcion">string con el nombre de la descripción</param>
        private void BuscarEstados(string p_strNombre, string p_strDescripcion)
        {
            EstadoNotificacion objEstadoNotificacion = null;

            try
            {
                //Crear objeto interfaz datos
                objEstadoNotificacion = new EstadoNotificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Estados
                this.grdEstados.DataSource = objEstadoNotificacion.ObtenerEstadosNotificacion(p_strNombre, p_strDescripcion);
                this.grdEstados.DataBind();

            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosNotificacion :: BuscarEstados -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los estados");
            }
        }


        /// <summary>
        /// Guardar la información de un estado
        /// </summary>
        /// <param name="p_strNombre">string con el nombre del estado</param>
        /// <param name="p_strDescripcion">string con la descripción del estado</param>
        /// <param name="p_intEstadoPDI">int que indica si es un estado PDI</param>
        private void GuardarEstado(string p_strNombre, string p_strDescripcion, int p_intEstadoPDI)
        {
            EstadoNotificacion objEstadoNotificacion = null;
            EstadoNotificacionEntity objEstado = null;

            try
            {
                //Crear objeto de estado
                objEstado = new EstadoNotificacionEntity
                {
                    Estado = p_strNombre,
                    Descripcion = p_strDescripcion,
                    EstadoPDI = (p_intEstadoPDI > 0 ? true : false),
                    Activo = true
                };

                //Crear el estado
                objEstadoNotificacion = new EstadoNotificacion();
                objEstadoNotificacion.CrearEstadoNotificacion(objEstado);

                //Consultar los estados
                this.BuscarEstados(this.hdfNombreBuscado.Value, this.hdfDescripcionBuscado.Value);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosNotificacion :: BuscarEstado -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error guardando el estado");
            }
            finally
            {
                //Limpiar datos
                this.LimpiarModal();
            }
        }    


        /// <summary>
        /// Verificar si se encuentra autenticado el Estado
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            if (DatosSesion.Usuario == string.Empty)
            {
                return false;
            }

            string idEstado = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

            Session["IDForToken"] = (object)idEstado;

            Session["Estado"] = Session["IDForToken"];

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
        /// Limpia los datos del modal
        /// </summary>
        private void LimpiarModal()
        {
            //Limpia campo de texto
            this.txtNombre.Text = "";
            this.txtDescripcion.Text = "";
            this.cboEstadoPDI.ClearSelection();

            //Limpiar mensaje
            this.lblMensajeModal.Text = "";
            this.divMensajeModal.Visible = false;
        }


        /// <summary>
        /// Inicializar la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Buscar los estados
            this.BuscarEstados(this.hdfNombreBuscado.Value, this.hdfDescripcionBuscado.Value);

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

                    //Validar sesion de Estado
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
            /// Evento que se ejecuta al dar clic en el botón de buscar. Busca los Estados asociados a un Estado
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
                    this.hdfNombreBuscado.Value = this.txtEstadoBuscar.Text.Trim();
                    this.hdfDescripcionBuscado.Value = this.txtDescripcionBuscar.Text.Trim();

                    //Buscar Estados
                    this.BuscarEstados(this.hdfNombreBuscado.Value, this.hdfDescripcionBuscado.Value);
                }
            }

        #endregion
   

        #region cmdGuardarEstado

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar Estado. Carga la información del Estado del Estado
            /// </summary>
            protected void cmdGuardarEstado_Click(object sender, EventArgs e)
            {
                //Verificar que los datos de validación sean correctos
                if (Page.IsValid)
                {
                    this.GuardarEstado(this.txtNombre.Text, this.txtDescripcion.Text, Convert.ToInt32(this.cboEstadoPDI.SelectedValue));
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


        #region grdEstados

            
            /// <summary>
            /// Evento que se ejecuta cuando se carga la grilla
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdEstados_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                DropDownList objDropDownList = null;
                EstadoNotificacionEntity objEstado = null;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Cargar datos
                    objEstado = (EstadoNotificacionEntity)e.Row.DataItem;

                    //Seleccionar las opciones
                    objDropDownList = (DropDownList)e.Row.FindControl("cboEstadoPDI");
                    if (objDropDownList != null)
                        objDropDownList.SelectedValue = (objEstado.EstadoPDI ? "1" : "0");
                    objDropDownList = (DropDownList)e.Row.FindControl("cboEstado");
                    if (objDropDownList != null)
                        objDropDownList.SelectedValue = (objEstado.Activo ? "1" : "0");
                }
            }


            /// <summary>
            /// Evento que se eejcuta al dar clic en editar. Habilita campos para edición
            /// </summary>
            protected void grdEstados_RowEditing(object sender, GridViewEditEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Colocar grilla en estado de edicion
                this.grdEstados.EditIndex = e.NewEditIndex;

                //Consultar informacion
                this.BuscarEstados(this.hdfNombreBuscado.Value, this.hdfDescripcionBuscado.Value);
            }

            
            /// <summary>
            /// Eevnto que se ejecuta al dar clic en Actualizar. Actualiza la información del Estado
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdEstados_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                EstadoNotificacion objEstadoNotificacion = null;
                EstadoNotificacionEntity objEstado = null;
                int intIdEstado = 0;
                string strNombre = "";
                string strDescripcion = "";
                string strEstadoPDI = "";
                string strActivo = "";

                

                try
                {
                    //Ocultar mensajes
                    this.lblMensaje.Text = "";
                    this.divMensaje.Visible = false;

                    if (Page.IsValid)
                    {
                        //Cargar datos
                        intIdEstado = Convert.ToInt32(grdEstados.DataKeys[e.RowIndex].Value.ToString().Trim());
                        strNombre = ((TextBox)grdEstados.Rows[e.RowIndex].FindControl("txtNombre")).Text.Trim();
                        strDescripcion = ((TextBox)grdEstados.Rows[e.RowIndex].FindControl("txtDescripcion")).Text.Trim();
                        strEstadoPDI = ((DropDownList)grdEstados.Rows[e.RowIndex].FindControl("cboEstadoPDI")).SelectedValue;
                        strActivo = ((DropDownList)grdEstados.Rows[e.RowIndex].FindControl("cboEstado")).SelectedValue;

                        //Crear objeto contenedor
                        objEstado = new EstadoNotificacionEntity
                        {
                            ID = intIdEstado,
                            Estado = strNombre,
                            Descripcion = strDescripcion,
                            EstadoPDI = (strEstadoPDI == "1" ? true : false),
                            Activo = (strActivo == "1" ? true : false),
                        };

                        //Actualizar Estado
                        objEstadoNotificacion = new EstadoNotificacion();
                        objEstadoNotificacion.ModificarEstadoNotificacion(objEstado);

                        //Cancelanr edicion
                        this.grdEstados.EditIndex = -1;

                        //Consultar informacion
                        this.BuscarEstados(this.hdfNombreBuscado.Value, this.hdfDescripcionBuscado.Value);
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosNotificacion :: grdEstados_RowUpdating  -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error editando el Estado");
                }
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en Cancelar. Cancela la edicion y retorna a vista de consulta de la grilla
            /// </summary>
            protected void grdEstados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Cancelando edicion
                this.grdEstados.EditIndex = -1;

                //Consultar informacion
                this.BuscarEstados(this.hdfNombreBuscado.Value, this.hdfDescripcionBuscado.Value);
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en la paginación. Cambia de pagina
            /// </summary>
            protected void grdEstados_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Asignar nuevo index
                this.grdEstados.PageIndex = e.NewPageIndex;

                //Consultar informacion
                this.BuscarEstados(this.hdfNombreBuscado.Value, this.hdfDescripcionBuscado.Value);
            }

        #endregion

    #endregion


}