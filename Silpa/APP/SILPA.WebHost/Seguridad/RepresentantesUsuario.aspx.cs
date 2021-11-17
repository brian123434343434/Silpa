using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using System.Data;

public partial class Seguridad_RepresentantesUsuario : System.Web.UI.Page
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
        /// Realizar la busqueda de información de los representantes d un usuario por su número de identificación
        /// </summary>
        /// <param name="p_strIdentificacion">string con el numero de identificación</param>
        private void BuscarRepresentantes(string p_strIdentificacion)
        {
            Persona objPersona = null;
            PersonaIdentity objDatosPersona = null;
            DataTable objRepresentantes = null;

            try
            {
                //Crear objeto interfaz datos
                objPersona = new Persona();

                //Consultar los datos de la persona
                objDatosPersona = objPersona.ConsultarPersonasNumeroIdentificacion(this.txtNumeroDocumento.Text.Trim());

                //Verificar si la persona existe
                if (objDatosPersona != null)
                {
                    //Ocultar mensajes
                    this.divMensaje.Visible = false;

                    //Mostrar div de datos
                    this.divUsuario.Visible = true;

                    //Cargar el nombre del usuario
                    this.rowNombreUsuario.Visible = true;
                    this.ltlNombreUsuario.Text = (!string.IsNullOrWhiteSpace(objDatosPersona.PrimerNombre) ? objDatosPersona.PrimerNombre.ToUpper() : "") +
                                                (!string.IsNullOrWhiteSpace(objDatosPersona.SegundoNombre) ? " " + objDatosPersona.SegundoNombre.ToUpper() : "") +
                                                (!string.IsNullOrWhiteSpace(objDatosPersona.PrimerApellido) ? " " + objDatosPersona.PrimerApellido.ToUpper() : "") +
                                                (!string.IsNullOrWhiteSpace(objDatosPersona.SegundoApellido) ? " " + objDatosPersona.SegundoApellido.ToUpper() : "");

                    //Cargar id de la persona
                    this.hdfIdPersona.Value = objDatosPersona.IdApplicationUser.ToString();

                    //Consultar el listado de representantes
                    this.ConsultarRepresentantes(objDatosPersona.IdApplicationUser);

                }
                else
                {
                    //Ocultar datos de usuario
                    this.rowNombreUsuario.Visible = false;

                    //Ocultar grillas de datos usuario
                    this.divUsuario.Visible = false;

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("El usuario especificado no se encuentra registrado en el sistema");
                }

            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Seguridad_RepresentantesUsuario :: BuscarRepresentantes -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los representantes del usuario");
            }
        }


        /// <summary>
        /// Realizar la busqueda de información del representante indicado
        /// </summary>
        /// <param name="p_strIdentificacion">string con el numero de identificación</param>
        private void GuardarRepresentante(string p_strIdPersona, string p_strIdentificacion, string p_strDescripcion)
        {
            Persona objPersona = null;
            PersonaIdentity objDatosPersona = null;
            
            try
            {
                //Crear objeto interfaz datos
                objPersona = new Persona();

                //Consultar los datos de la persona
                objDatosPersona = objPersona.ConsultarPersonasNumeroIdentificacion(p_strIdentificacion);

                //Verificar si la persona existe
                if (objDatosPersona != null)
                {
                    //Crear representante
                    objPersona.CrearRepresentantesPersona(long.Parse(p_strIdPersona), objDatosPersona.IdApplicationUser, p_strDescripcion);

                    //Consultar el listado de representantes
                    this.ConsultarRepresentantes(long.Parse(p_strIdPersona));

                    //Limpiar datos
                    this.LimpiarModal();
                }
                else
                {
                    //Cargar mensaje de error                        
                    this.MostrarMensajeModal("El representante especificado no existe");
                    this.mpeCrearRepresentante.Show();
                }

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Seguridad_RepresentantesUsuario :: BuscarRepresentante -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error guardando el representante");

                //Limpiar datos
                this.LimpiarModal();
            }
        }


        /// <summary>
        /// Consultar la información de los representantes de una persona
        /// </summary>
        /// <param name="p_lngIdRepresentante">long con el id de la persona</param>
        private void ConsultarRepresentantes(long p_lngIdPersona)
        {
            DataTable objRepresentantes = null;
            Persona objPersona = null;

            try
            {
                //Crear objeto interfaz datos
                objPersona = new Persona();

                //Consultar representantes
                objRepresentantes = objPersona.ConsultarRepresentantesPersona(p_lngIdPersona);
                this.grdRepresentantes.DataSource = objRepresentantes;
                this.grdRepresentantes.DataBind();
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Seguridad_RepresentantesUsuario :: ConsultarRepresentantes -> Error Inesperado: " + exc.Message);

                //Escalar excepción
                throw exc;
            }
        }


        /// <summary>
        /// Eliminar un representante del listado
        /// </summary>
        /// <param name="p_lngIdRepresentante">long con el id del representante</param>
        private void EliminarRepresentante(long p_lngIdRepresentante)
        {
            Persona objPersona = null;
            
            try
            {
                //Crear objeto interfaz datos
                objPersona = new Persona();

                //Eliminar represe
                objPersona.EliminarRepresentantesPersona(p_lngIdRepresentante);

                //Consultar el listado de representantes
                this.ConsultarRepresentantes(long.Parse(this.hdfIdPersona.Value));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Seguridad_RepresentantesUsuario :: EliminarRepresentante (" + p_lngIdRepresentante.ToString() + ") -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error eliminando el representante");
            }
        }


        /// <summary>
        /// Verificar si se encuentra autenticado el usuario
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            if (DatosSesion.Usuario == string.Empty)
            {
                return false;
            }

            string idUsuario = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

            Session["IDForToken"] = (object)idUsuario;

            Session["Usuario"] = Session["IDForToken"];

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
            this.txtNumeroDocumentoRepresentante.Text = "";
            this.txtDescripcion.Text = "";

            //Limpiar mensaje
            this.lblMensajeModal.Text = "";
            this.divMensajeModal.Visible = false;
        }


        /// <summary>
        /// Inicializar la pagina
        /// </summary>
        private void InicializarPagina()
        {
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
                    //InicializarPagina();

                    //Validar sesion de usuario
                    if (this.ValidacionToken() == false)
                    {
                        Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                    }
                    else
                    {
                        //Iniciliazar datos de la página
                        InicializarPagina();
                    }
                }
            }

        #endregion


        #region CmdBuscar

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar. Busca los representantes asociados a un usuario
            /// </summary>
            protected void cmdBuscar_Click(object sender, EventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Verificar que los datos de validación sean correctos
                if (Page.IsValid)
                {
                    //Buscar representantes
                    this.BuscarRepresentantes(this.txtNumeroDocumento.Text.Trim());
                }
            }

        #endregion
   

        #region cmdGuardarRepresentante

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar representante. Carga la información del usuario del representante
            /// </summary>
            protected void cmdGuardarRepresentante_Click(object sender, EventArgs e)
            {
                //Verificar que los datos de validación sean correctos
                if (Page.IsValid)
                {
                    this.GuardarRepresentante(this.hdfIdPersona.Value, this.txtNumeroDocumentoRepresentante.Text.Trim(), this.txtDescripcion.Text.Trim());
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


        #region grdRepresentantes

            
            /// <summary>
            /// Evento que se ejecuta cuando se carga la grilla
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdRepresentantes_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                    //Asignar a botón de eliminar confirmación
                    if (((LinkButton)e.Row.Cells[3].Controls[2]).Text == "Eliminar")
                        ((LinkButton)e.Row.Cells[3].Controls[2]).OnClientClick = "return confirm('¿En realidad dese eliminar el representante seleccionado?')";
                }
            }


            /// <summary>
            /// Evento que se ejecuta al dar clic en el link de eliminar. Elimina un representante
            /// </summary>
            protected void grdRepresentantes_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Eliminar el representante indicado
                this.EliminarRepresentante(long.Parse(this.grdRepresentantes.DataKeys[e.RowIndex].Value.ToString().Trim()));

            }


            /// <summary>
            /// Evento que se eejcuta al dar clic en editar. Habilita campos para edición
            /// </summary>
            protected void grdRepresentantes_RowEditing(object sender, GridViewEditEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Colocar grilla en estado de edicion
                this.grdRepresentantes.EditIndex = e.NewEditIndex;

                //Consultar informacion
                this.ConsultarRepresentantes(long.Parse(this.hdfIdPersona.Value));
            }

            
            /// <summary>
            /// Eevnto que se ejecuta al dar clic en Actualizar. Actualiza la información del representante
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdRepresentantes_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                long lngIdRepresentante = 0;
                string strDescripcion = "";
                Persona objPersona = null;

                try
                {

                    //Ocultar mensajes
                    this.lblMensaje.Text = "";
                    this.divMensaje.Visible = false;

                    if (Page.IsValid)
                    {
                        //Cargar datos
                        lngIdRepresentante = long.Parse(grdRepresentantes.DataKeys[e.RowIndex].Value.ToString().Trim());
                        strDescripcion = ((TextBox)grdRepresentantes.Rows[e.RowIndex].FindControl("txtDescripcionEditar")).Text.Trim();

                        //Actualizar representante
                        objPersona = new Persona();
                        objPersona.ModificarRepresentantesPersona(lngIdRepresentante, strDescripcion);

                        //Cancelanr edicion
                        this.grdRepresentantes.EditIndex = -1;

                        //Consultar informacion
                        this.ConsultarRepresentantes(long.Parse(this.hdfIdPersona.Value));
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Seguridad_RepresentantesUsuario :: grdRepresentantes_RowUpdating  -> Error Inesperado: " + exc.Message);

                    //Cargar mensaje de error                        
                    this.MostrarMensaje("Se genero un error editando el representante");
                }
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en Cancelar. Cancela la edici{on y retorna a vista de consulta de la grilla
            /// </summary>
            protected void grdRepresentantes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
            {
                //Ocultar mensajes
                this.lblMensaje.Text = "";
                this.divMensaje.Visible = false;

                //Cancelando edicion
                this.grdRepresentantes.EditIndex = -1;

                //Consultar informacion
                this.ConsultarRepresentantes(long.Parse(this.hdfIdPersona.Value));
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en la paginación. Cambia de pagina
            /// </summary>
            protected void grdRepresentantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Asignar nuevo index
                this.grdRepresentantes.PageIndex = e.NewPageIndex;

                //Consultar informacion
                this.ConsultarRepresentantes(long.Parse(this.hdfIdPersona.Value));
            }

        #endregion

    #endregion


}