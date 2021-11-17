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

public partial class Administracion_Notificacion_FormatoPlantillaNotificacion : System.Web.UI.Page
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
        /// Realizar la busqueda de información de los formatos
        /// </summary>
        /// <param name="p_strNombre">string con el nombre del formato</param>
        private void BuscarFormatos(string p_strNombre)
        {
            FormatoPlantillaNotificacion objFormatoNotificacion = null;

            try
            {
                //Crear objeto interfaz datos
                objFormatoNotificacion = new FormatoPlantillaNotificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Formatos
                this.grdFormatos.DataSource = objFormatoNotificacion.ListarFormatos(p_strNombre);
                this.grdFormatos.DataBind();

            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FormatoPlantillaNotificacion :: BuscarFormatos -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los formatos");
            }
        }


        /// <summary>
        /// Guardar la información de un formato
        /// </summary>
        /// <param name="p_strNombre">string con el nombre del formato</param>
        /// <param name="p_strFormato">string con el formato</param>
        private void GuardarFormato(string p_strNombre, string p_strFormato)
        {
            FormatoPlantillaNotificacion objFormatoNotificacion = null;
            FormatoPlantillaNotificacionEntity objFormato = null;

            try
            {
                //Crear objeto de Formato
                objFormato = new FormatoPlantillaNotificacionEntity
                {
                    Nombre = p_strNombre,
                    Formato = p_strFormato,
                    Activo = true
                };

                //Crear el Formato
                objFormatoNotificacion = new FormatoPlantillaNotificacion();
                objFormatoNotificacion.CrearFormato(objFormato);

                //Consultar los formatos
                this.BuscarFormatos(this.hdfNombreBuscado.Value);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FormatoPlantillaNotificacion :: GuardarFormato -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error guardando el formato");
            }
            finally
            {
                //Limpiar datos
                this.LimpiarModal();
            }
        }    


        /// <summary>
        /// Verificar si se encuentra autenticado el Formato
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            if (DatosSesion.Usuario == string.Empty)
            {
                return false;
            }

            string idFormato = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

            Session["IDForToken"] = (object)idFormato;

            Session["Formato"] = Session["IDForToken"];

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
            this.txtFormato.Text = "";

            //Limpiar mensaje
            this.lblMensajeModal.Text = "";
            this.divMensajeModal.Visible = false;
        }


        /// <summary>
        /// Inicializar la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Buscar los Formatos
            this.BuscarFormatos(this.hdfNombreBuscado.Value);

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

                    //Validar sesion de Formato
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
            /// Evento que se ejecuta al dar clic en el botón de buscar. Busca los Formatos asociados a un Formato
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
                    this.hdfNombreBuscado.Value = this.txtNombreBuscar.Text.Trim();

                    //Buscar Formatos
                    this.BuscarFormatos(this.hdfNombreBuscado.Value);
                }
            }

        #endregion
   
            
        #region cmdGuardarFormato

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar Formato. Carga la información del Formato del Formato
            /// </summary>
            protected void cmdGuardarFormato_Click(object sender, EventArgs e)
            {
                //Verificar que los datos de validación sean correctos
                if (Page.IsValid)
                {
                    this.GuardarFormato(this.txtNombre.Text, this.txtFormato.Text);
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


        #region grdFormatos

            
            /// <summary>
            /// Evento que se ejecuta cuando se carga la grilla
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdFormatos_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                DropDownList objDropDownList = null;
                CheckBox objCheckBox = null;
                TextBox objTextBox = null;
                FormatoPlantillaNotificacionEntity objFormato = null;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Cargar datos
                    objFormato = (FormatoPlantillaNotificacionEntity)e.Row.DataItem;

                    //Seleccionar las opciones
                    objDropDownList = (DropDownList)e.Row.FindControl("cboEstado");
                    if (objDropDownList != null)
                        objDropDownList.SelectedValue = (objFormato.Activo ? "1" : "0");

                    //Verificar si no es nulo
                    if (objCheckBox != null)
                    {
                        //Cargar valor
                        objCheckBox.Attributes.Add("value", e.Row.RowIndex.ToString() );

                        //Verificar si esta seleccionado
                        if (objCheckBox != null && objCheckBox.Checked)
                        {
                            if (objTextBox != null)
                                objTextBox.Visible = true;
                        }
                        else
                        {
                            if (objTextBox != null)
                                objTextBox.Visible = false;
                        }                    
                    }
                    
                }
            }


            /// <summary>
            /// Evento que se eejcuta al dar clic en editar. Habilita campos para edición
            /// </summary>
            protected void grdFormatos_RowEditing(object sender, GridViewEditEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Colocar grilla en Formato de edicion
                this.grdFormatos.EditIndex = e.NewEditIndex;

                //Consultar informacion
                this.BuscarFormatos(this.hdfNombreBuscado.Value);
            }

            
            /// <summary>
            /// Eevnto que se ejecuta al dar clic en Actualizar. Actualiza la información del Formato
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdFormatos_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                FormatoPlantillaNotificacion objFormatoNotificacion = null;
                FormatoPlantillaNotificacionEntity objFormato = null;
                int intIdFormato = 0;
                string strNombre = "";
                string strFormato = "";
                string strActivo = "";

                try
                {
                    //Ocultar mensajes
                    this.lblMensaje.Text = "";
                    this.divMensaje.Visible = false;

                    if (Page.IsValid)
                    {
                        //Cargar datos
                        intIdFormato = Convert.ToInt32(grdFormatos.DataKeys[e.RowIndex].Value.ToString().Trim());
                        strNombre = ((TextBox)grdFormatos.Rows[e.RowIndex].FindControl("txtNombre")).Text.Trim();
                        strFormato = ((TextBox)grdFormatos.Rows[e.RowIndex].FindControl("txtFormato")).Text.Trim();
                        strActivo = ((DropDownList)grdFormatos.Rows[e.RowIndex].FindControl("cboEstado")).SelectedValue;

                        //Crear objeto contenedor
                        objFormato = new FormatoPlantillaNotificacionEntity
                        {
                            FormatoID = intIdFormato,
                            Nombre = strNombre,
                            Formato = strFormato,
                            Activo = (strActivo == "1" ? true : false),
                        };

                        //Actualizar Formato
                        objFormatoNotificacion = new FormatoPlantillaNotificacion();
                        objFormatoNotificacion.EditarFormato(objFormato);

                        //Cancelanr edicion
                        this.grdFormatos.EditIndex = -1;

                        //Consultar informacion
                        this.BuscarFormatos(this.hdfNombreBuscado.Value);
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FormatoPlantillaNotificacion :: grdFormatos_RowUpdating  -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error editando el formato");
                }
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en Cancelar. Cancela la edicion y retorna a vista de consulta de la grilla
            /// </summary>
            protected void grdFormatos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Cancelando edicion
                this.grdFormatos.EditIndex = -1;

                //Consultar informacion
                this.BuscarFormatos(this.hdfNombreBuscado.Value);
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en la paginación. Cambia de pagina
            /// </summary>
            protected void grdFormatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Asignar nuevo index
                this.grdFormatos.PageIndex = e.NewPageIndex;

                //Consultar informacion
                this.BuscarFormatos(this.hdfNombreBuscado.Value);
            }       

        #endregion


    #endregion



            
}