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

public partial class Administracion_Notificacion_PlantillaNotificacion : System.Web.UI.Page
{

    #region Propiedades

        /// <summary>
        /// Firmas autoridades
        /// </summary>
        private List<FirmaAutoridadNotificacionEntity> _objFirmasPlantilla
        {
            get
            {
                return (List<FirmaAutoridadNotificacionEntity>)ViewState["_objFirmasPlantilla"];
            }
            set
            {
                ViewState["_objFirmasPlantilla"] = value;
            }
        }

    #endregion


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
        /// Realizar la busqueda de información de los Plantillas
        /// </summary>
        /// <param name="p_strNombre">string con el nombre del Plantilla</param>
        private void BuscarPlantillas(string p_strNombre, int p_intPlantillaID)
        {
            PlantillaNotificacion objPlantillaNotificacion = null;

            try
            {
                //Crear objeto interfaz datos
                objPlantillaNotificacion = new PlantillaNotificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Plantillas
                this.grdPlantillas.DataSource = objPlantillaNotificacion.ListarPlantillas(p_strNombre, p_intPlantillaID);
                this.grdPlantillas.DataBind();

            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_PlantillaNotificacion :: BuscarPlantillas -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los Plantillas");
            }
        }


        /// <summary>
        /// Crear una nueva plantilla
        /// </summary>
        /// <param name="p_strNombre">string con el nombre de la plantilla</param>
        /// <param name="p_intFormatoID">int con el identificador del formato</param>
        /// <param name="p_strEncabezado">string con el mensaje de encabezado</param>
        /// <param name="p_strCuerpo">string con el cuerpo del mensaje</param>
        /// <param name="p_strPieFirma">string con el mensaje de pie de firma</param>
        /// <param name="p_strPie">string con el mensaje del pie</param>
        private void CrearPlantilla(string p_strNombre, int p_intAutoridadID, int p_intFormatoID, string p_strEncabezado, string p_strCuerpo, string p_strPieFirma, string p_strPie)
        {
            FirmaAutoridadPlantillaNotificacion objFirmaAutoridadPlantillaNotificacion = null;
            PlantillaNotificacion objPlantillaNotificacion = null;
            PlantillaNotificacionEntity objPlantilla = null;
            int intPlantillaID = 0;

            try
            {
                //Crear objeto de Plantilla
                objPlantilla = new PlantillaNotificacionEntity
                {
                    AutoridadID = p_intAutoridadID,
                    Nombre = p_strNombre,
                    Formato = new FormatoPlantillaNotificacionEntity { FormatoID = p_intFormatoID },
                    Encabezado = p_strEncabezado,
                    Cuerpo = p_strCuerpo,
                    PieFirma = p_strPieFirma,
                    Pie = p_strPie,
                    Activo = true
                };

                //Crear el Plantilla
                objPlantillaNotificacion = new PlantillaNotificacion();
                intPlantillaID = objPlantillaNotificacion.CrearPlantilla(objPlantilla);

                //Insertar firmas plantilla
                if (this._objFirmasPlantilla != null && this._objFirmasPlantilla.Count > 0)
                {
                    //Crear objeto
                    objFirmaAutoridadPlantillaNotificacion = new FirmaAutoridadPlantillaNotificacion();

                    //Ciclo que crea firmas
                    foreach (FirmaAutoridadNotificacionEntity objFirma in this._objFirmasPlantilla)
                    {
                        objFirmaAutoridadPlantillaNotificacion.CrearFirmaAutoridadPlantilla(objFirma.FirmaAutoridadID, intPlantillaID);
                    }
                }

                //Consultar los Plantillas
                this.BuscarPlantillas(this.hdfNombreBuscado.Value, (!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : 0));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_PlantillaNotificacion :: CrearPlantilla -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error guardando el Plantilla");
            }
            finally
            {
                //Limpiar datos
                this.LimpiarModal();
            }
        }


        /// <summary>
        /// Editar una plantilla
        /// </summary>
        /// <param name="p_intPlantillaID">int con el identificador de la plantilla</param>
        /// <param name="p_strNombre">string con el nombre de la plantilla</param>
        /// <param name="p_intFormatoID">int con el identificador del formato</param>
        /// <param name="p_strEncabezado">string con el mensaje de encabezado</param>
        /// <param name="p_strCuerpo">string con el cuerpo del mensaje</param>
        /// <param name="p_strPieFirma">string con el mensaje de pie de firma</param>
        /// <param name="p_strPie">string con el mensaje del pie</param>
        /// <param name="p_blnActivo">bool indicando si se encuentra activo</param>
        private void EditarPlantilla(int p_intPlantillaID, int p_intAutoridadID, string p_strNombre, int p_intFormatoID, string p_strEncabezado, string p_strCuerpo, string p_strPieFirma, string p_strPie, bool p_blnActivo)
        {
            FirmaAutoridadPlantillaNotificacion objFirmaAutoridadPlantillaNotificacion = null;
            PlantillaNotificacion objPlantillaNotificacion = null;
            PlantillaNotificacionEntity objPlantilla = null;

            try
            {
                //Crear objeto de Plantilla
                objPlantilla = new PlantillaNotificacionEntity
                {
                    PlantillaID = p_intPlantillaID,
                    AutoridadID = p_intAutoridadID,
                    Nombre = p_strNombre,
                    Formato = new FormatoPlantillaNotificacionEntity { FormatoID = p_intFormatoID },
                    Encabezado = p_strEncabezado,
                    Cuerpo = p_strCuerpo,
                    PieFirma = p_strPieFirma,
                    Pie = p_strPie,
                    Activo = p_blnActivo
                };

                //Crear el Plantilla
                objPlantillaNotificacion = new PlantillaNotificacion();
                objPlantillaNotificacion.EditarPlantilla(objPlantilla);

                //Eliminar firmas de la plantilla
                objFirmaAutoridadPlantillaNotificacion = new FirmaAutoridadPlantillaNotificacion();
                objFirmaAutoridadPlantillaNotificacion.EliminarFirmasPlantilla(p_intPlantillaID);

                //Insertar firmas plantilla
                if (this._objFirmasPlantilla != null && this._objFirmasPlantilla.Count > 0)
                {
                    foreach( FirmaAutoridadNotificacionEntity objFirma in this._objFirmasPlantilla){
                        objFirmaAutoridadPlantillaNotificacion.CrearFirmaAutoridadPlantilla(objFirma.FirmaAutoridadID, p_intPlantillaID);
                    }
                }

                //Consultar los Plantillas
                this.BuscarPlantillas(this.hdfNombreBuscado.Value, (!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : 0));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_PlantillaNotificacion :: EditarPlantilla -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error editando la plantilla");
            }
            finally
            {
                //Limpiar datos
                this.LimpiarModal();
            }
        }  


        /// <summary>
        /// Verificar si se encuentra autenticado el Plantilla
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            if (DatosSesion.Usuario == string.Empty)
            {
                return false;
            }

            string idPlantilla = Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario).ToString();

            Session["IDForToken"] = (object)idPlantilla;

            Session["Plantilla"] = Session["IDForToken"];

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
            this.hdfPlantillaID.Value = "";
            this.txtNombre.Text = "";
            this.cboFormulario.ClearSelection();
            this.txtEncabezado.Text = "";
            this.txtCuerpo.Text = "";
            this.txtPieFirma.Text = "";
            this.txtPie.Text = "";
            this.divEstado.Visible = false;
            this.divFirmas.Visible = false;
            this.grdFirmas.DataSource = null;
            this.grdFirmas.DataBind();
            this._objFirmasPlantilla = null;

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

            //Buscar los Plantillas
            this.BuscarPlantillas(this.hdfNombreBuscado.Value, (!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : 0));

            //Limpiar los campos modal
            this.LimpiarModal();
        }


        /// <summary>
        /// Cargar el listado de formatos existes
        /// </summary>
        private void CargarFormatos(bool? p_blnActivo = null)
        {
            FormatoPlantillaNotificacion objFormatoNotificacion = null;

            //Rrealizar la consulta y cargue de los formatos
            objFormatoNotificacion = new FormatoPlantillaNotificacion();
            this.cboFormulario.DataSource = objFormatoNotificacion.ListarFormatos("", p_blnActivo);
            this.cboFormulario.DataTextField = "Nombre";
            this.cboFormulario.DataValueField = "FormatoID";
            this.cboFormulario.DataBind();
            this.cboFormulario.Items.Insert(0, new ListItem("Seleccione...", "-1"));
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
        /// Cargar el listado de firmas
        /// </summary>
        private void CargarFirmas(DropDownList p_objListado, int p_intAutoridadID, bool? p_blnActivo = null)
        {
            FirmaAutoridadNotificacion objFirmaAutoridadNotificacion = null;

            //Rrealizar la consulta y cargue de los formatos
            objFirmaAutoridadNotificacion = new FirmaAutoridadNotificacion();
            p_objListado.DataSource = objFirmaAutoridadNotificacion.ListarFirmas(p_intAutoridadID, "", p_blnActivo);
            p_objListado.DataTextField = "NombreAutorizado";
            p_objListado.DataValueField = "FirmaAutoridadID";
            p_objListado.DataBind();
            p_objListado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }


        /// <summary>
        /// Cargar la información de la plantilla en el modal
        /// </summary>
        /// <param name="p_intPlantillaID">int con el id de la plantilla</param>
        private void CargarInformacionPlantilla(int p_intPlantillaID)
        {
            PlantillaNotificacion objPlantillaNotificacion = null;
            FirmaAutoridadPlantillaNotificacion objFirmaAutoridadPlantillaNotificacion = null; 
            PlantillaNotificacionEntity objPlantilla = null;

            try
            {
                //Crear objeto interfaz datos
                objPlantillaNotificacion = new PlantillaNotificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Plantilla
                objPlantilla = objPlantillaNotificacion.ObtenerPlantilla(p_intPlantillaID);

                //Cargar la información
                this.hdfPlantillaID.Value = objPlantilla.PlantillaID.ToString();
                this.cboAutoridad.SelectedValue = objPlantilla.AutoridadID.ToString();
                this.txtNombre.Text = objPlantilla.Nombre;
                this.cboFormulario.SelectedValue = objPlantilla.Formato.FormatoID.ToString();
                this.txtEncabezado.Text = objPlantilla.Encabezado;
                this.txtCuerpo.Text = objPlantilla.Cuerpo;
                this.txtPieFirma.Text = objPlantilla.PieFirma;
                this.txtPie.Text = objPlantilla.Pie;
                this.cboEstado.SelectedValue = (objPlantilla.Activo ? "1" : "0");

                if (objPlantilla.AutoridadID > 1)
                {
                    //Cargar el listado de firmas de la plantilla
                    objFirmaAutoridadPlantillaNotificacion = new FirmaAutoridadPlantillaNotificacion();
                    this._objFirmasPlantilla = objFirmaAutoridadPlantillaNotificacion.ListarFirmasPlantilla(objPlantilla.PlantillaID);
                    this.divFirmas.Visible = true;
                    this.CargarDatosFirmas();

                    //Cargar lisadto firmas grilla
                    this.CargarFirmas((DropDownList)this.grdFirmas.FooterRow.FindControl("cboGrdFirma"), objPlantilla.AutoridadID, true);
                }
                else
                {
                    this._objFirmasPlantilla = null;
                    this.divFirmas.Visible = false;
                }
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_PlantillaNotificacion :: CargarInformacionPlantilla -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de la plantilla");
            }
        }


        /// <summary>
        /// Cargar el listado de direcciones en la grilla
        /// </summary>
        private void CargarDatosFirmas()
        {
            List<FirmaAutoridadNotificacionEntity> objLstFirmas = null;

            //Verificar si listado es vacio
            if (this._objFirmasPlantilla == null || _objFirmasPlantilla.Count == 0)
            {
                //Cargar mensaje de ingreso de titulares
                objLstFirmas = new List<FirmaAutoridadNotificacionEntity>();
                objLstFirmas.Add(new FirmaAutoridadNotificacionEntity());
                this.grdFirmas.DataSource = objLstFirmas;
                this.grdFirmas.ShowFooter = true;
                this.grdFirmas.DataBind();
                this.grdFirmas.Rows[0].Cells.Clear();
                this.grdFirmas.Rows[0].Cells.Add(new TableCell());
                this.grdFirmas.Rows[0].Cells[0].ColumnSpan = 2;
                this.grdFirmas.Rows[0].Cells[0].Text = "No se han ingresado firmas";
            }
            else
            {
                //Cargar datos
                this.grdFirmas.DataSource = this._objFirmasPlantilla;
                this.grdFirmas.DataBind();
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

                    //Validar sesion de Plantilla
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
            /// Evento que se ejecuta al dar clic en el botón de buscar. Busca los Plantillas asociados a un Plantilla
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
                    this.hdfAutoridadIDBuscado.Value = this.cboAutoridadAmbientalBuscar.SelectedValue;

                    //Buscar Plantillas
                    this.BuscarPlantillas(this.hdfNombreBuscado.Value, (!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : 0));
                }
            }

        #endregion
   

        #region cmdGuardarPlantilla

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar Plantilla. Carga la información del Plantilla del Plantilla
            /// </summary>
            protected void cmdGuardarPlantilla_Click(object sender, EventArgs e)
            {
                //Verificar que los datos de validación sean correctos
                if (Page.IsValid)
                {
                    //Verificar si es editar
                    if (!string.IsNullOrWhiteSpace(this.hdfPlantillaID.Value))
                    {
                        //Editar la plantilla
                        this.EditarPlantilla(Convert.ToInt32(this.hdfPlantillaID.Value), Convert.ToInt32(this.cboAutoridad.SelectedValue), this.txtNombre.Text.Trim(), Convert.ToInt32(this.cboFormulario.SelectedValue), this.txtEncabezado.Text.Trim(), this.txtCuerpo.Text.Trim(), this.txtPieFirma.Text.Trim(), this.txtPie.Text.Trim(), (this.cboEstado.SelectedValue == "1" ? true : false));
                    }
                    else
                    {
                        //Crear la plantilla
                        this.CrearPlantilla(this.txtNombre.Text.Trim(), Convert.ToInt32(this.cboAutoridad.SelectedValue), Convert.ToInt32(this.cboFormulario.SelectedValue), this.txtEncabezado.Text.Trim(), this.txtCuerpo.Text.Trim(), this.txtPieFirma.Text.Trim(), this.txtPie.Text.Trim());
                    }

                    //Limpiar capos
                    this.LimpiarModal();

                    //Cerrar modal
                    this.mpeCrearPlantilla.Hide();

                    //Consultar la informacion de plantilla
                    this.BuscarPlantillas(this.hdfNombreBuscado.Value, (!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : 0));
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
                this.mpeCrearPlantilla.Hide();
            }

        #endregion


        #region cmdNuevoPlantilla

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de adicionar plantilla
            /// </summary>
            protected void cmdNuevoPlantilla_Click(object sender, EventArgs e)
            {
                //Limpiar modal
                this.LimpiarModal();

                //Colocar titulo
                this.ltlTituloCrear.Text = "NUEVA PLANTILLA";
                this.cmdGuardarPlantilla.Text = "Adicionar";

                //Cargar el listado de autoridades
                this.CargarAutoridades(this.cboAutoridad);

                //Cargar listado de sectores
                this.CargarFormatos(true);

                //Ocultar estado
                this.divEstado.Visible = false;

                //Mostrar modal
                this.mpeCrearPlantilla.Show();                
            }

        #endregion


        #region lnkEditar

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de adicionar plantilla
            /// </summary>
            protected void lnkEditar_Click(object sender, EventArgs e)
            {
                //Limpiar modal
                this.LimpiarModal();

                //Colocar titulo
                this.ltlTituloCrear.Text = "EDITAR PLANTILLA";
                this.cmdGuardarPlantilla.Text = "Modificar";

                //Cargar el listado de autoridades
                this.CargarAutoridades(this.cboAutoridad);

                //Cargar listado de sectores
                this.CargarFormatos();

                //Cargar información de la plantilla
                this.CargarInformacionPlantilla(Convert.ToInt32(((LinkButton)sender).CommandArgument));

                //Mostrar estado
                this.divEstado.Visible = true;

                //Mostrar modal
                this.mpeCrearPlantilla.Show();                
            }

        #endregion


        #region grdPlantillas

            
            /// <summary>
            /// Evento que se ejecuta cuando se carga la grilla
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void grdPlantillas_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                DropDownList objDropDownList = null;
                PlantillaNotificacionEntity objPlantilla = null;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Cargar datos
                    objPlantilla = (PlantillaNotificacionEntity)e.Row.DataItem;

                    //Seleccionar las opciones
                    objDropDownList = (DropDownList)e.Row.FindControl("cboEstado");
                    if (objDropDownList != null)
                        objDropDownList.SelectedValue = (objPlantilla.Activo ? "1" : "0");
                }
            }

            /// <summary>
            /// Evento que se ejecuta al dar clic en la paginación. Cambia de pagina
            /// </summary>
            protected void grdPlantillas_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Asignar nuevo index
                this.grdPlantillas.PageIndex = e.NewPageIndex;

                //Consultar informacion
                this.BuscarPlantillas(this.hdfNombreBuscado.Value, (!string.IsNullOrEmpty(this.hdfAutoridadIDBuscado.Value) ? Convert.ToInt32(this.hdfAutoridadIDBuscado.Value) : 0));
            }

        #endregion


        #region grdFirmas


            /// <summary>
            /// Evento que consulta las firmas autorizadas según autoridad
            /// </summary>
            protected void cboAutoridad_SelectedIndexChanged(object sender, EventArgs e)
            {
                //Limpiar firmas
                this._objFirmasPlantilla = null;

                //Cargar datos de firmas
                this.CargarDatosFirmas();

                //Verificar que haya seleccionado una autoridad
                if(this.cboAutoridad.SelectedValue != "-1"){

                    //Cargar listado de firmas de la autoridad
                    this.CargarFirmas((DropDownList)this.grdFirmas.FooterRow.FindControl("cboGrdFirma"), Convert.ToInt32(this.cboAutoridad.SelectedValue), true);
                    this.divFirmas.Visible = true;
                }
                else{
                    this.divFirmas.Visible = false;
                }

                //Mostrar modal
                this.mpeCrearPlantilla.Show(); 
            }

            /// <summary>
            /// Evento que elimina una firma
            /// </summary>
            protected void lnkEliminarFirma_Click(object sender, EventArgs e)
            {
                List<FirmaAutoridadNotificacionEntity> obLstFirmas = null;

                if (this._objFirmasPlantilla.Count == 1)
                    this._objFirmasPlantilla = null;
                else if(this._objFirmasPlantilla != null)
                {
                    //Buscar firma
                    obLstFirmas = this._objFirmasPlantilla.Where(firma => firma.FirmaAutoridadID == Convert.ToInt32(((LinkButton)sender).CommandArgument)).ToList();

                    //Elimina
                    if (obLstFirmas != null && obLstFirmas.Count > 0)
                    {
                        this._objFirmasPlantilla.Remove(obLstFirmas[0]);
                    }
                }

                //Cargar datos
                this.CargarDatosFirmas();

                //Cargar el listado de firmas
                this.CargarFirmas((DropDownList)this.grdFirmas.FooterRow.FindControl("cboGrdFirma"), Convert.ToInt32(this.cboAutoridad.SelectedValue), true);

                //Mostrar modal
                this.mpeCrearPlantilla.Show(); 
            }


            /// <summary>
            /// Evento que adiciona una firma
            /// </summary>
            protected void lnkAdicionarFirma_Click(object sender, EventArgs e)
            {
                List<FirmaAutoridadNotificacionEntity> obLstFirmas = null;

                //Crear listado si no existe
                if (this._objFirmasPlantilla == null)
                    this._objFirmasPlantilla = new List<FirmaAutoridadNotificacionEntity>();

                //Buscar firma
                obLstFirmas = this._objFirmasPlantilla.Where(firma => firma.FirmaAutoridadID == Convert.ToInt32(((DropDownList)this.grdFirmas.FooterRow.FindControl("cboGrdFirma")).SelectedValue)).ToList();

                //Si la firma no existe adicionarlo
                if (obLstFirmas == null || obLstFirmas.Count == 0)
                {
                    //Cargar datos de la firma
                    this._objFirmasPlantilla.Add(new FirmaAutoridadNotificacionEntity { FirmaAutoridadID = Convert.ToInt32(((DropDownList)this.grdFirmas.FooterRow.FindControl("cboGrdFirma")).SelectedValue), NombreAutorizado = ((DropDownList)this.grdFirmas.FooterRow.FindControl("cboGrdFirma")).SelectedItem.Text });
                }

                //Cargar datos
                this.CargarDatosFirmas();

                //Cargar el listado de firmas
                this.CargarFirmas((DropDownList)this.grdFirmas.FooterRow.FindControl("cboGrdFirma"), Convert.ToInt32(this.cboAutoridad.SelectedValue), true);

                //Mostrar modal
                this.mpeCrearPlantilla.Show(); 
            }        

        #endregion


    #endregion





            
}