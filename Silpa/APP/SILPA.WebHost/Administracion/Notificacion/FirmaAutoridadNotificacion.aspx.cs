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
using SILPA.LogicaNegocio.AdmTablasBasicas;

public partial class Administracion_Notificacion_FirmaAutoridadNotificacion : System.Web.UI.Page
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
        /// Realizar la busqueda de información de las firmas
        /// </summary>
        /// <param name="p_intAutoridadID">int con el id de la autoridad</param>
        /// <param name="p_strNombreAutorizado">string con el nombre del autorizado</param>
        private void BuscarFirmas(int p_intAutoridadID, string p_strNombreAutorizado)
        {
            FirmaAutoridadNotificacion objFirmaAutoridadNotificacion = null;

            try
            {
                //Crear objeto interfaz datos
                objFirmaAutoridadNotificacion = new FirmaAutoridadNotificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Firmas
                this.grdFirmas.DataSource = objFirmaAutoridadNotificacion.ListarFirmas(p_intAutoridadID, p_strNombreAutorizado);
                this.grdFirmas.DataBind();

            }
            catch(Exception exc){   
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FirmaAutoridadNotificacion :: BuscarFirmas -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de las firmas");
            }
        }


        /// <summary>
        /// Crear una nueva firma
        /// </summary>
        /// <param name="p_intAutoridadID">int con el id de la autoridad</param>
        /// <param name="p_strNombreAutorizado">string con el nombre del autorizado</param>
        /// <param name="p_strCargoAutorizado">string con el cargo del autorizado</param>
        /// <param name="p_strSubdireccion">Subdirección a la cual pertenece el autorizado</param>
        /// <param name="p_strGrupo">Grupo al cual pertenece el autorizado</param>
        /// <param name="p_strEmailAutorizado">string con el email del autorizado</param>
        private void CrearFirma(int p_intAutoridadID, string p_strNombreAutorizado, string p_strCargoAutorizado, string p_strSubdireccion, string p_strGrupo, string p_strEmailAutorizado)
        {
            FirmaAutoridadNotificacion objFirmaAutoridadNotificacion = null;
            FirmaAutoridadNotificacionEntity objFirma = null;

            try
            {
                //Crear objeto de Firma
                objFirma = new FirmaAutoridadNotificacionEntity
                {
                    AutoridadID = p_intAutoridadID,
                    TipoIdentificacionAutorizadoID = Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue),
                    NumeroIdentificaionAutorizado = this.txtNumeroIdentificacion.Text.Trim(),
                    NombreAutorizado = p_strNombreAutorizado,
                    CargoAutorizado = p_strCargoAutorizado,
                    SubdireccionAutorizado = p_strSubdireccion.Trim(),
                    GrupoAutorizado = p_strGrupo.Trim(),
                    EmailAutorizado = p_strEmailAutorizado,
                    Activo = true
                };

                //Cargar informcaion de la imagen
                objFirma.NombreFirma = this.fluFirma.PostedFile.FileName;
                objFirma.TipoFirma = this.fluFirma.PostedFile.ContentType;
                objFirma.LongitudFirma = Convert.ToInt32(this.fluFirma.PostedFile.InputStream.Length);
                objFirma.Firma = new byte[objFirma.LongitudFirma];
                this.fluFirma.PostedFile.InputStream.Read(objFirma.Firma, 0, objFirma.LongitudFirma);

                //Crear la firma
                objFirmaAutoridadNotificacion = new FirmaAutoridadNotificacion();
                objFirmaAutoridadNotificacion.CrearFirma(objFirma);

                //Consultar los Firmas
                this.BuscarFirmas(Convert.ToInt32(this.hdfAutoridadAmbientalBuscado.Value), this.hdfNombreAutorizadoBuscado.Value);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FirmaAutoridadNotificacion :: CrearFirma -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error guardando el Firma");
            }
            finally
            {
                //Limpiar datos
                this.LimpiarModal();
            }
        }


        /// <summary>
        /// Editar una firma
        /// </summary>
        /// <param name="p_intFirmaAutoridadID">int con el identificador de la firma</param>
        /// <param name="p_intAutoridadID">int con el id de la autoridad</param>
        /// <param name="p_strNombreAutorizado">string con el nombre del autorizado</param>
        /// <param name="p_strCargoAutorizado">string con el cargo del autorizado</param>
        /// <param name="p_strSubdireccion">Subdirección a la cual pertenece el autorizado</param>
        /// <param name="p_strGrupo">Grupo al cual pertenece el autorizado</param>
        /// <param name="p_strEmailAutorizado">string con el email del autorizado</param>
        /// <param name="p_blnActivo">bool que indica si se encuentra activa la firma</param>
        private void EditarFirma(int p_intFirmaAutoridadID, int p_intAutoridadID, string p_strNombreAutorizado, string p_strCargoAutorizado, string p_strSubdireccion, string p_strGrupo, string p_strEmailAutorizado, bool p_blnActivo)
        {
            FirmaAutoridadNotificacion objFirmaAutoridadNotificacion = null;
            FirmaAutoridadNotificacionEntity objFirma = null;

            try
            {
                //Crear objeto de Firma
                objFirma = new FirmaAutoridadNotificacionEntity
                {
                    FirmaAutoridadID = p_intFirmaAutoridadID,
                    AutoridadID = p_intAutoridadID,
                    TipoIdentificacionAutorizadoID = Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue),
                    NumeroIdentificaionAutorizado = this.txtNumeroIdentificacion.Text.Trim(),
                    NombreAutorizado = p_strNombreAutorizado,
                    CargoAutorizado = p_strCargoAutorizado,
                    SubdireccionAutorizado = p_strSubdireccion.Trim(),
                    GrupoAutorizado = p_strGrupo.Trim(),
                    EmailAutorizado = p_strEmailAutorizado,
                    Activo = p_blnActivo
                };

                //Cargar informcaion de la imagen
                if (Convert.ToInt32(this.fluFirma.PostedFile.InputStream.Length) > 0)
                {
                    objFirma.NombreFirma = this.fluFirma.PostedFile.FileName;
                    objFirma.TipoFirma = this.fluFirma.PostedFile.ContentType;
                    objFirma.LongitudFirma = Convert.ToInt32(this.fluFirma.PostedFile.InputStream.Length);
                    objFirma.Firma = new byte[objFirma.LongitudFirma];
                    this.fluFirma.PostedFile.InputStream.Read(objFirma.Firma, 0, objFirma.LongitudFirma);
                }

                //Crear el Firma
                objFirmaAutoridadNotificacion = new FirmaAutoridadNotificacion();
                objFirmaAutoridadNotificacion.EditarFirma(objFirma);

                //Consultar los Firmas
                this.BuscarFirmas(Convert.ToInt32(this.hdfAutoridadAmbientalBuscado.Value), this.hdfNombreAutorizadoBuscado.Value);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FirmaAutoridadNotificacion :: EditarFirma -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error editando la Firma");
            }
            finally
            {
                //Limpiar datos
                this.LimpiarModal();
            }
        }  


        /// <summary>
        /// Verificar si se encuentra autenticado el Firma
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            if (DatosSesion.Usuario == string.Empty)
            {
                return false;
            }

            string idFirma = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

            Session["IDForToken"] = (object)idFirma;

            Session["Firma"] = Session["IDForToken"];

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
            this.hdfFirmaAutoridadID.Value = "";
            this.cboTipoIdentificacion.ClearSelection();
            this.txtNumeroIdentificacion.Text = "";
            this.txtNombreAutorizado.Text = "";
            this.txtCargoAutorizado.Text = "";
            this.txtSubdireccion.Text = "";
            this.txtGrupo.Text = "";
            this.txtEmailAutorizado.Text = "";
            this.txtFirma.Value = "";
            this.cboAutoridad.ClearSelection();
            this.divEstado.Visible = false;
            this.imgFirma.Visible = false;
            this.brImagen.Visible = false;
            this.rfvFirma.Visible = true;

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

            //Buscar los Firmas
            this.BuscarFirmas(Convert.ToInt32(this.hdfAutoridadAmbientalBuscado.Value), this.hdfNombreAutorizadoBuscado.Value);

            //Limpiar los campos modal
            this.LimpiarModal();
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
        /// Cargar el listado de formatos existes
        /// </summary>
        /// <param name="p_objLista">DropDownList con la lista en la cual se cargara la información</param>
        /// <param name="blnActivo">bool que indica si se carga la información activa</param>
        private void CargarTiposIdentificacion(DropDownList p_objLista)
        {
            NOT_TipoIdentificacion objTiposIdentificacion = null;

            //Cargar informacion en desplegables
            objTiposIdentificacion = new NOT_TipoIdentificacion();
            p_objLista.DataSource = objTiposIdentificacion.Listar_Tipo_Documento("");
            p_objLista.DataTextField = "NTI_DESCRIPCION";
            p_objLista.DataValueField = "ID_TIPO_IDENTIFICACION";
            p_objLista.DataBind();
            p_objLista.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }


        /// <summary>
        /// Cargar la información de la Firma en el modal
        /// </summary>
        /// <param name="p_intFirmaAutoridadID">int con el id de la firma</param>
        private void CargarInformacionFirma(int p_intFirmaAutoridadID)
        {
            FirmaAutoridadNotificacion objFirmaAutoridadNotificacion = null;
            FirmaAutoridadNotificacionEntity objFirma = null;

            try
            {
                //Crear objeto interfaz datos
                objFirmaAutoridadNotificacion = new FirmaAutoridadNotificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Firma
                objFirma = objFirmaAutoridadNotificacion.ObtenerFirma(p_intFirmaAutoridadID);               

                //Cargar listado de autoridades
                this.CargarAutoridades(this.cboAutoridad);

                //Cargar listado de tipos de identificación
                this.CargarTiposIdentificacion(this.cboTipoIdentificacion);

                //Cargar la información
                this.hdfFirmaAutoridadID.Value = p_intFirmaAutoridadID.ToString();
                this.cboAutoridad.SelectedValue = objFirma.AutoridadID.ToString();
                this.cboTipoIdentificacion.SelectedValue = objFirma.TipoIdentificacionAutorizadoID.ToString();
                this.txtNumeroIdentificacion.Text = objFirma.NumeroIdentificaionAutorizado;
                this.txtNombreAutorizado.Text = objFirma.NombreAutorizado;
                this.txtCargoAutorizado.Text = objFirma.CargoAutorizado;
                this.txtSubdireccion.Text = objFirma.SubdireccionAutorizado;
                this.txtGrupo.Text = objFirma.GrupoAutorizado;
                this.txtEmailAutorizado.Text = objFirma.EmailAutorizado;
                this.cboEstado.SelectedValue = (objFirma.Activo ? "1" : "0");

                //Cargar imagen
                this.imgFirma.ImageUrl = "ImagenFirma.aspx?FIR=" + p_intFirmaAutoridadID.ToString();
                this.imgFirma.Visible = true;
                this.brImagen.Visible = true;
                this.rfvFirma.Visible = false;

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_FirmaAutoridadNotificacion :: CargarInformacionFirma -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de la firma");
            }
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

                    //Validar sesion de Firma
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
            /// Evento que se ejecuta al dar clic en el botón de buscar. Busca los Firmas asociados a un Firma
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
                    this.hdfAutoridadAmbientalBuscado.Value = this.cboAutoridadAmbientalBuscar.SelectedValue;                    
                    this.hdfNombreAutorizadoBuscado.Value = this.txtNombreAutorizadoBuscar.Text.Trim();

                    //Buscar Firmas
                    this.BuscarFirmas(Convert.ToInt32(this.hdfAutoridadAmbientalBuscado.Value), this.hdfNombreAutorizadoBuscado.Value);
                }
            }

        #endregion
   

        #region cmdGuardarFirma

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar Firma. Carga la información del Firma del Firma
            /// </summary>
            protected void cmdGuardarFirma_Click(object sender, EventArgs e)
            {
                //Verificar que los datos de validación sean correctos
                if (Page.IsValid)
                {
                    //Verificar si es editar
                    if (!string.IsNullOrWhiteSpace(this.hdfFirmaAutoridadID.Value))
                    {
                        //Editar la Firma
                        this.EditarFirma(Convert.ToInt32(this.hdfFirmaAutoridadID.Value), Convert.ToInt32(this.cboAutoridad.SelectedValue), this.txtNombreAutorizado.Text.Trim(), this.txtCargoAutorizado.Text.Trim(), this.txtSubdireccion.Text.Trim(), this.txtGrupo.Text.Trim(), this.txtEmailAutorizado.Text.Trim(), (this.cboEstado.SelectedValue == "1" ? true : false));
                    }
                    else
                    {
                        //Crear la Firma
                        this.CrearFirma(Convert.ToInt32(this.cboAutoridad.SelectedValue), this.txtNombreAutorizado.Text.Trim(), this.txtCargoAutorizado.Text.Trim(), this.txtSubdireccion.Text.Trim(), this.txtGrupo.Text.Trim(), this.txtEmailAutorizado.Text.Trim());
                    }

                    //Limpiar capos
                    this.LimpiarModal();

                    //Cerrar modal
                    this.mpeCrearFirma.Hide();

                    //Consultar la informacion de Firma
                    this.BuscarFirmas(Convert.ToInt32(this.hdfAutoridadAmbientalBuscado.Value), this.hdfNombreAutorizadoBuscado.Value);
                }     
            }

        #endregion


        #region cmdCancelar

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de cancelar. Limpia los datos del modal
            /// </summary>
            protected void cmdCancelar_Click(object sender, EventArgs e)
            {
                //Limpiar campos
                this.LimpiarModal();

                //Ocultar modal
                this.mpeCrearFirma.Hide();
            }

        #endregion


        #region cmdNuevaFirma

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de adicionar Firma
            /// </summary>
            protected void cmdNuevaFirma_Click(object sender, EventArgs e)
            {
                //Colocar titulo
                this.ltlTituloCrear.Text = "NUEVA FIRMA";
                this.cmdGuardarFirma.Text = "Adicionar";

                //Cargar listado de sectores
                this.CargarAutoridades(this.cboAutoridad);                

                //Cargar el listado de tipos de identificacion
                this.CargarTiposIdentificacion(this.cboTipoIdentificacion);

                //Ocultar estado
                this.divEstado.Visible = false;

                //Mostrar modal
                this.mpeCrearFirma.Show();                
            }

        #endregion


        #region lnkEditar

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de adicionar Firma
            /// </summary>
            protected void lnkEditar_Click(object sender, EventArgs e)
            {              
                //Colocar titulo
                this.ltlTituloCrear.Text = "EDITAR FIRMA";
                this.cmdGuardarFirma.Text = "Modificar";

                //Cargar listado de sectores
                this.CargarAutoridades(this.cboAutoridad);            
                
                //Cargar información de la Firma
                this.CargarInformacionFirma(Convert.ToInt32(((LinkButton)sender).CommandArgument));

                //Mostrar estado
                this.divEstado.Visible = true;

                //Mostrar modal
                this.mpeCrearFirma.Show();                
            }

        #endregion


        #region grdFirmas

            
            /// <summary>
            /// Evento que se ejecuta cuando se carga la grilla
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdFirmas_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                DropDownList objDropDownList = null;
                FirmaAutoridadNotificacionEntity objFirma = null;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Cargar datos
                    objFirma = (FirmaAutoridadNotificacionEntity)e.Row.DataItem;

                    //Seleccionar las opciones
                    objDropDownList = (DropDownList)e.Row.FindControl("cboEstado");
                    if (objDropDownList != null)
                        objDropDownList.SelectedValue = (objFirma.Activo ? "1" : "0");
                }
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en la paginación. Cambia de pagina
            /// </summary>
            protected void grdFirmas_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Asignar nuevo index
                this.grdFirmas.PageIndex = e.NewPageIndex;

                //Consultar informacion
                this.BuscarFirmas(Convert.ToInt32(this.hdfAutoridadAmbientalBuscado.Value), this.hdfNombreAutorizadoBuscado.Value);
            }

        #endregion


    #endregion
 
}