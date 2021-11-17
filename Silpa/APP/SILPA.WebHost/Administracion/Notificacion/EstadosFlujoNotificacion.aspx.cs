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
using SILPA.LogicaNegocio.Generico;

public partial class Administracion_Notificacion_EstadosFlujoNotificacion : System.Web.UI.Page
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
            this.upnlMensajes.Update();
        }

        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensajeModal(string p_strMensaje)
        {
            this.lblMensajeModal.Text = p_strMensaje;
            this.divMensajeModal.Visible = true;
            this.upnlFormulario.Update();
        }

        /// <summary>
        /// Realizar la busqueda de información de los flujos
        /// </summary>
        private void BuscarFlujos(int p_intAutoridadID)
        {
            FlujoNotificacionElectronica objFlujoNotificacionElectronica = null;

            try
            {
                //Crear objeto interfaz datos
                objFlujoNotificacionElectronica = new FlujoNotificacionElectronica();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Flujos
                this.cboFlujo.DataSource = objFlujoNotificacionElectronica.ConsultaFlujosNotificacion(null, p_intAutoridadID, null);
                this.cboFlujo.DataValueField = "ID_FLUJO_NOT_ELEC";
                this.cboFlujo.DataTextField = "FLUJO_NOT_ELEC_DESC";
                this.cboFlujo.DataBind();
                this.cboFlujo.Items.Insert(0, new ListItem("Seleccione ...", "-1"));

            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosFlujoNotificacion :: BuscarFlujos -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los Flujos");
            }
        }


        /// <summary>
        /// Realizar la busqueda de información de los estados pertenecientes a un flujo
        /// </summary>
        /// <param name="p_intFlujoID">int con el id del flujo</param>
        private void BuscarEstadosFlujo(int p_intFlujoID)
        {
            EstadoFlujoNotificacion objEstadoFlujoNotificacion = null;

            try
            {
                //Crear objeto interfaz datos
                objEstadoFlujoNotificacion = new EstadoFlujoNotificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Consultar Flujos
                this.grdEstados.DataSource = objEstadoFlujoNotificacion.ListarEstadosNotificacionElectronica(p_intFlujoID);
                this.grdEstados.DataBind();

                //Acrtualizar upnl
                this.upnlResultadoEstados.Update();
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosFlujoNotificacion :: BuscarEstadosFlujo -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los estados");
            }
        }


        /// <summary>
        /// Carga el listado de estados no pertenecientes al flujo
        /// </summary>
        /// <param name="p_intFlujoID">int con el id del flujo</param>
        private void BuscarEstadosNoFlujo(int p_intFlujoID)
        {
            EstadoFlujoNotificacion objEstadoFlujoNotificacion = null;

            try
            {
                //Crear objeto interfaz datos
                objEstadoFlujoNotificacion = new EstadoFlujoNotificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Limpiar estados
                this.cboEstadoFlujo.Items.Clear();

                //Consultar Flujos
                this.cboEstadoFlujo.DataSource = objEstadoFlujoNotificacion.ListarEstadosNoFlujoNotificacionElectronica(p_intFlujoID);
                this.cboEstadoFlujo.DataValueField = "ID";
                this.cboEstadoFlujo.DataTextField = "Descripcion";
                this.cboEstadoFlujo.DataBind();
                this.cboEstadoFlujo.Items.Insert(0, new ListItem("Seleccione ...", "-1"));

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosFlujoNotificacion :: BuscarEstadosNoFlujo -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los estados no incluidos en el flujo");
            }
        }


        /// <summary>
        /// Carga el listado de tipos de enexos
        /// </summary>
        private void CargarListadoTiposAnexos()
        {
            Notificacion objNotificacion = null;

            try
            {
                //Crear objeto interfaz datos
                objNotificacion = new Notificacion();

                //Ocultar mensajes
                this.divMensaje.Visible = false;

                //Limpiar estados
                this.cboTipoAnexosCorreo.Items.Clear();

                //Consultar Flujos
                this.cboTipoAnexosCorreo.DataSource = objNotificacion.ConsultarListadoTiposAdjuntosCorreo();
                this.cboTipoAnexosCorreo.DataValueField = "ID_TIPO_ANEXO_CORREO";
                this.cboTipoAnexosCorreo.DataTextField = "DESCRIPCION";
                this.cboTipoAnexosCorreo.DataBind();
                this.cboTipoAnexosCorreo.Items.Insert(0, new ListItem("Seleccione ...", "-1"));

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosFlujoNotificacion :: CargarListadoTiposAnexos -> Error Inesperado: " + exc.Message);

                //Cargar mensaje de error                        
                this.MostrarMensaje("Se genero un error buscando la información de los estados no incluidos en el flujo");
            }
        }


        /// <summary>
        /// Crear un estado
        /// </summary>
        private void CrearEstado()
        {
            EstadoFlujoNotificacion objEstadoFlujo = null;
            EstadoFlujoNotificacionEntity objEstado = null;

            try
            {
                //Cargar los datos
                objEstado = new EstadoFlujoNotificacionEntity
                {
                    EstadoID = Convert.ToInt32(this.cboEstadoFlujo.SelectedValue),
                    FlujoID = Convert.ToInt32(this.cboFlujo.SelectedValue),
                    Descripcion = this.txtDescripcion.Text.Trim(),
                    DiasVencimiento = Convert.ToInt32(this.txtDiasVencimiento.Text.Trim()),
                    GeneraPlantilla = this.chkGeneraPlantilla.Checked,
                    PlantillaID = (this.chkGeneraPlantilla.Checked ? Convert.ToInt32(this.cboPlantilla.SelectedValue) : 0),
                    DocumentoAdicional = this.chkADocumentoAdicional.Checked,
                    EnviaCorreoAvanceManual = this.chkEnvioCorreoManual.Checked,
                    EnviaNotificacionFisica = this.chkNotificacionFisica.Checked,
                    AnexaAdjunto = this.chkAnexaAdjunto.Checked,
                    EnviaCorreoAvance = this.chkEnvioCorreoAutomatico.Checked,
                    TextoCorreoAvance = (this.chkEnvioCorreoAutomatico.Checked ? this.txtTextoCorreoAutomatico.Text.Trim() : ""),
                    TipoAnexoCorreoID = (!string.IsNullOrWhiteSpace(this.cboTipoAnexosCorreo.SelectedValue) && this.cboTipoAnexosCorreo.SelectedValue != "-1" ? Convert.ToInt32(this.cboTipoAnexosCorreo.SelectedValue) : 0),
                    PermitiAnexarActoAdministrativo = this.chkPermiteAnexarActo.Checked,
                    PermitiAnexarConceptosActoAdministrativo = this.chkPermiteAnexarConceptos.Checked,
                    PublicarEstado = this.chkPublicaEstado.Checked,
                    PublicarPlantilla = this.chkPublicaPlantilla.Checked,
                    PublicarAdjunto = this.chkPublicaAdjunto.Checked,
                    SolicitarInformacionPersonaNotificar = this.chkSolitarDatosPersonaNotificada.Checked,
                    SolicitarReferenciaRecepcionNotificacion = this.chkSolicitarConfirmacionNotificacion.Checked,
                    ReferenciaRecepcionNotificacionObligatoria = this.chkConfirmacionNotificacionObligatoria.Checked,
                    EsEstadoEspera = this.chkEstadoEspera.Checked,
                    EsNotificacion = this.chkEsNotificacion.Checked,
                    EsEjecutoria = this.chkEsEjecutoria.Checked,
                    GeneraRecurso = this.chkGeneraRecurso.Checked,
                    EsCitacion = this.chkEsCitacion.Checked,
                    EsEdicto = this.chkEsEdicto.Checked,
                    EsAceptacionNotificacion = this.chkEsAceptacionNotificacion.Checked,
                    EsRechazoNotificacion = this.chkEsRechazoNotificacion.Checked,
                    EsAceptacionCitacion = this.chkEsAceptacionCitacion.Checked,
                    EsRechazoCitacion = this.chkEsRechazoCitacion.Checked,
                    EsAnulacion = this.chkEsAnulacion.Checked,
                    EsFinalPublicidad = this.chkEsFinalPublicidad.Checked,
                    EstadoDependienteID = (!string.IsNullOrWhiteSpace(this.cboEstadoRelacionado.SelectedValue) && this.cboEstadoRelacionado.SelectedValue != "-1" ? Convert.ToInt32(this.cboEstadoRelacionado.SelectedValue) : 0),
                    Activo = true
                };

                //Crear el Flujo
                objEstadoFlujo = new EstadoFlujoNotificacion();
                objEstadoFlujo.CrearEstadoFlujo(objEstado);

                //Consultar los Flujos
                this.BuscarEstadosFlujo(Convert.ToInt32(this.cboFlujo.SelectedValue));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosFlujoNotificacion :: CrearEstado -> Error Inesperado: " + exc.Message);

                throw exc;
            }            
        }


        /// <summary>
        /// Modifica la información de un estado
        /// </summary>
        private void ModificarEstado()
        {
            EstadoFlujoNotificacion objEstadoFlujo = null;
            EstadoFlujoNotificacionEntity objEstado = null;

            try
            {
                //Cargar los datos
                objEstado = new EstadoFlujoNotificacionEntity
                {
                    EstadoFlujoID = Convert.ToInt32(this.hdfEstadoFlujoID.Value),
                    EstadoID = Convert.ToInt32(this.hdfEstadoID.Value),
                    FlujoID = Convert.ToInt32(this.cboFlujo.SelectedValue),
                    Descripcion = this.txtDescripcion.Text.Trim(),
                    DiasVencimiento = Convert.ToInt32(this.txtDiasVencimiento.Text.Trim()),
                    GeneraPlantilla = this.chkGeneraPlantilla.Checked,
                    PlantillaID = (this.chkGeneraPlantilla.Checked ? Convert.ToInt32(this.cboPlantilla.SelectedValue) : 0),
                    DocumentoAdicional = this.chkADocumentoAdicional.Checked,
                    EnviaCorreoAvanceManual = this.chkEnvioCorreoManual.Checked,
                    EnviaNotificacionFisica = this.chkNotificacionFisica.Checked,
                    AnexaAdjunto = this.chkAnexaAdjunto.Checked,
                    EnviaCorreoAvance = this.chkEnvioCorreoAutomatico.Checked,
                    TextoCorreoAvance = (this.chkEnvioCorreoAutomatico.Checked ? this.txtTextoCorreoAutomatico.Text.Trim() : ""),
                    TipoAnexoCorreoID = (!string.IsNullOrWhiteSpace(this.cboTipoAnexosCorreo.SelectedValue) && this.cboTipoAnexosCorreo.SelectedValue != "-1" ? Convert.ToInt32(this.cboTipoAnexosCorreo.SelectedValue) : 0),
                    PermitiAnexarActoAdministrativo = this.chkPermiteAnexarActo.Checked,
                    PermitiAnexarConceptosActoAdministrativo = this.chkPermiteAnexarConceptos.Checked,
                    PublicarEstado = this.chkPublicaEstado.Checked,
                    PublicarPlantilla = this.chkPublicaPlantilla.Checked,
                    PublicarAdjunto = this.chkPublicaAdjunto.Checked,
                    SolicitarInformacionPersonaNotificar = this.chkSolitarDatosPersonaNotificada.Checked,
                    SolicitarReferenciaRecepcionNotificacion = this.chkSolicitarConfirmacionNotificacion.Checked,
                    ReferenciaRecepcionNotificacionObligatoria = this.chkConfirmacionNotificacionObligatoria.Checked,
                    EsEstadoEspera = this.chkEstadoEspera.Checked,
                    EsNotificacion = this.chkEsNotificacion.Checked,
                    EsEjecutoria = this.chkEsEjecutoria.Checked,
                    GeneraRecurso = this.chkGeneraRecurso.Checked,
                    EsCitacion = this.chkEsCitacion.Checked,
                    EsEdicto = this.chkEsEdicto.Checked,
                    EsAceptacionNotificacion = this.chkEsAceptacionNotificacion.Checked,
                    EsRechazoNotificacion = this.chkEsRechazoNotificacion.Checked,
                    EsAceptacionCitacion = this.chkEsAceptacionCitacion.Checked,
                    EsRechazoCitacion = this.chkEsRechazoCitacion.Checked,
                    EsAnulacion = this.chkEsAnulacion.Checked,
                    EsFinalPublicidad = this.chkEsFinalPublicidad.Checked,
                    EstadoDependienteID = (!string.IsNullOrWhiteSpace(this.cboEstadoRelacionado.SelectedValue) && this.cboEstadoRelacionado.SelectedValue != "-1" ? Convert.ToInt32(this.cboEstadoRelacionado.SelectedValue) : 0),
                    Activo = (this.cboEstado.SelectedValue == "1" ? true : false)
                };

                //Modificar el Flujo
                objEstadoFlujo = new EstadoFlujoNotificacion();
                objEstadoFlujo.EditarEstadoFlujo(objEstado);

                //Consultar los Flujos
                this.BuscarEstadosFlujo(Convert.ToInt32(this.cboFlujo.SelectedValue));
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosFlujoNotificacion :: ModificarEstado -> Error Inesperado: " + exc.Message);

                throw exc;
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
        /// Limpia los datos del modal
        /// </summary>
        private void LimpiarModal()
        {
            //Ocultar div
            this.divEstadoFlujoDesplegable.Visible = false;
            this.divEstadoFlujoLabel.Visible = false;
            this.divPlantilla.Visible = false;
            this.divAdjuntos.Visible = false;
            this.divTextoCorreo.Visible = false;
            this.divEstado.Visible = false;
            this.divPublicaPlantilla.Visible = false;
            this.divPublicaAdjunto.Visible = false;
            this.divConfirmacionNotificacionObligatoria.Visible = false;
            this.divTipoAnexosCorreo.Visible = false;
            this.divPermiteAnexarConceptos.Visible = false;

            //Limpia campos            
            this.ltlEstadoFlujo.Text = "";
            this.hdfEstadoFlujoID.Value = "";
            this.cboEstadoFlujo.ClearSelection();
            this.txtDescripcion.Text = "";
            this.txtDiasVencimiento.Text = "";
            this.chkGeneraPlantilla.Checked = false;            
            this.cboPlantilla.ClearSelection();
            this.chkADocumentoAdicional.Checked = false;
            this.chkEnvioCorreoManual.Checked = false;            
            this.chkNotificacionFisica.Checked = false;
            this.chkAnexaAdjunto.Checked = false;
            this.chkEnvioCorreoAutomatico.Checked = false;            
            this.txtTextoCorreoAutomatico.Text = "";
            this.chkPublicaEstado.Checked = false;
            this.chkPublicaPlantilla.Checked = false;
            this.chkPublicaAdjunto.Checked = false;
            this.chkSolicitarConfirmacionNotificacion.Checked = false;
            this.chkSolitarDatosPersonaNotificada.Checked = false;
            this.chkEsNotificacion.Checked = false;
            this.chkEstadoEspera.Checked = false;
            this.chkEsEjecutoria.Checked = false;
            this.chkGeneraRecurso.Checked = false;
            this.chkEsCitacion.Checked = false;
            this.chkEsEdicto.Checked = false;
            this.chkEsAceptacionNotificacion.Checked = false;
            this.chkEsRechazoNotificacion.Checked = false;
            this.chkEsAceptacionCitacion.Checked = false;
            this.chkEsRechazoCitacion.Checked = false;
            this.chkEsAnulacion.Checked = false;
            this.chkEsFinalPublicidad.Checked = false;
            this.chkConfirmacionNotificacionObligatoria.Checked = false;
            this.cboEstado.ClearSelection();
            this.cboTipoAnexosCorreo.ClearSelection();
            this.chkPermiteAnexarActo.Checked = false;
            this.chkPermiteAnexarConceptos.Checked = false;

            //Limpiar mensaje
            this.lblMensajeModal.Text = "";
            this.divMensajeModal.Visible = false;
        }


        /// <summary>
        /// Inicializar la pagina
        /// </summary>
        private void InicializarPagina()
        {
            //Cargar autoridades
            this.CargarAutoridades(this.cboAutoridadBuscar);

            //Buscar los Flujos
            this.BuscarFlujos(-1);

            //Limpiar los campos modal
            this.LimpiarModal();
        }

        /// <summary>
        /// Cargar el listado de formatos existes
        /// </summary>
        /// <param name="p_objLista">DropDownList con la lista en la cual se cargara la información</param>
        /// <param name="blnActivo">bool que indica si se carga la información activa</param>
        private void CargarPlantillas(DropDownList p_objLista, int p_intAutoridadID, bool? blnActivo = null)
        {
            PlantillaNotificacion objPlantilla = null;
            List<PlantillaNotificacionEntity> objLstPlantillas = null;

            //Cargar listado de plantillas
            objPlantilla = new PlantillaNotificacion();
            objLstPlantillas = objPlantilla.ListarPlantillas("", p_intAutoridadID, blnActivo);
            if (objLstPlantillas != null && p_intAutoridadID <= 0)
                objLstPlantillas = objLstPlantillas.Where(plantilla => plantilla.AutoridadID == -1).ToList();

            //Limpiar desplegables
            p_objLista.ClearSelection();
            p_objLista.Items.Clear();

            //Cargar informacion en desplegables            
            p_objLista.DataSource = objLstPlantillas;
            p_objLista.DataTextField = "Nombre";
            p_objLista.DataValueField = "PlantillaID";
            p_objLista.DataBind();
            p_objLista.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }


        /// <summary>
        /// Cargar el listado de estados dependientes de acuerdo al estado actual
        /// </summary>
        /// <param name="p_intEstadoActual">int con el identificador del estado actual</param>
        /// <param name="p_intEstadoDependienteSeleccionar">int con el estado dependiente a seleccionar</param>
        private void CargarEstadoDependientes(int p_intEstadoActual, int p_intEstadoDependienteSeleccionar = 0)
        {
            EstadoFlujoNotificacion objEstadoFlujoNotificacion = null;
            List<EstadoFlujoNotificacionEntity> objEstados = null;

            //Crear objeto interfaz datos
            objEstadoFlujoNotificacion = new EstadoFlujoNotificacion();

            //Ocultar mensajes
            this.divMensaje.Visible = false;

            //Consultar Flujos
            objEstados = objEstadoFlujoNotificacion.ListarEstadosNotificacionElectronica(Convert.ToInt32(this.cboFlujo.SelectedValue));

            //Cargar en listado
            if (objEstados != null)
            {
                this.cboEstadoRelacionado.DataSource = objEstados.Where(estado => estado.EstadoID != p_intEstadoActual).ToList();
                this.cboEstadoRelacionado.DataValueField = "EstadoID";
                this.cboEstadoRelacionado.DataTextField = "Descripcion";
                this.cboEstadoRelacionado.DataBind();
                this.cboEstadoRelacionado.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
            else
            {
                this.cboEstadoRelacionado.Items.Add(new ListItem("Seleccione...", "-1"));
            }

            //Seleccionar estado
            if (p_intEstadoDependienteSeleccionar > 0)
            {
                try
                {
                    this.cboEstadoRelacionado.SelectedValue = p_intEstadoDependienteSeleccionar.ToString();
                }
                catch(Exception exc){
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosFlujoNotificacion :: CargarEstadoDependientes -> Error Inesperado: " + exc.Message);
                }
            }
        }


        /// <summary>
        /// Cargar la información del estado indicado en la pantalla de edición
        /// </summary>
        /// <param name="p_intEstadoFlujoID">int con el id del estado a editar</param>
        private void CargarInformacionEstadoFlujo(int p_intEstadoFlujoID)
        {
            EstadoFlujoNotificacion objEstadoFlujo = null;
            List<EstadoFlujoNotificacionEntity> objEstado = null;

            try
            {
                //Consultar la informacion del estado
                objEstadoFlujo = new EstadoFlujoNotificacion();
                objEstado = objEstadoFlujo.ListarEstadosNotificacionElectronica(0, p_intEstadoFlujoID);

                //Verificar que se encontrará información
                if (objEstado != null && objEstado.Count > 0)
                {
                    //Mostrar div
                    this.divEstadoFlujoLabel.Visible = true;
                    this.divEstadoFlujoDesplegable.Visible = false;
                    this.divPlantilla.Visible = objEstado[0].GeneraPlantilla;
                    this.divTextoCorreo.Visible = objEstado[0].EnviaCorreoAvance;
                    this.divEstado.Visible = true;
                    this.divPublicaPlantilla.Visible = objEstado[0].GeneraPlantilla && objEstado[0].PublicarEstado;
                    this.divPublicaAdjunto.Visible = objEstado[0].AnexaAdjunto && objEstado[0].PublicarEstado;
                    this.divEstadoRelacionado.Visible = true;

                    //Cargar la información en el modal                    
                    this.ltlEstadoFlujo.Text = objEstado[0].EstadoDescripcion.Trim();
                    this.hdfEstadoID.Value = objEstado[0].EstadoID.ToString();
                    this.cboEstadoFlujo.ClearSelection();
                    this.txtDescripcion.Text = objEstado[0].Descripcion;
                    this.txtDiasVencimiento.Text = objEstado[0].DiasVencimiento.ToString();
                    this.chkGeneraPlantilla.Checked = objEstado[0].GeneraPlantilla;
                    this.chkADocumentoAdicional.Checked = objEstado[0].DocumentoAdicional;
                    if (objEstado[0].GeneraPlantilla)
                        this.cboPlantilla.SelectedValue = objEstado[0].PlantillaID.ToString();
                    this.chkEnvioCorreoManual.Checked = objEstado[0].EnviaCorreoAvanceManual;
                    if (objEstado[0].EnviaCorreoAvanceManual)
                    {
                        this.divAdjuntos.Visible = true;
                        this.chkAnexaAdjunto.Checked = objEstado[0].AnexaAdjunto;
                    }
                    this.chkNotificacionFisica.Checked = objEstado[0].EnviaNotificacionFisica;
                    this.chkEnvioCorreoAutomatico.Checked = objEstado[0].EnviaCorreoAvance;                    
                    this.txtTextoCorreoAutomatico.Text = objEstado[0].TextoCorreoAvance.Trim();
                    this.chkPublicaEstado.Checked = objEstado[0].PublicarEstado;
                    this.chkPublicaPlantilla.Checked = objEstado[0].PublicarPlantilla;
                    this.chkPublicaAdjunto.Checked = objEstado[0].PublicarAdjunto;
                    this.chkSolitarDatosPersonaNotificada.Checked = objEstado[0].SolicitarInformacionPersonaNotificar;
                    this.chkSolicitarConfirmacionNotificacion.Checked = objEstado[0].SolicitarReferenciaRecepcionNotificacion;
                    if (this.chkSolicitarConfirmacionNotificacion.Checked)
                    {
                        this.divConfirmacionNotificacionObligatoria.Visible = true;
                        this.chkConfirmacionNotificacionObligatoria.Checked = objEstado[0].ReferenciaRecepcionNotificacionObligatoria;
                    }
                    this.chkEstadoEspera.Checked = objEstado[0].EsEstadoEspera;
                    this.chkEsNotificacion.Checked = objEstado[0].EsNotificacion;
                    this.chkEsEjecutoria.Checked = objEstado[0].EsEjecutoria;
                    this.chkGeneraRecurso.Checked = objEstado[0].GeneraRecurso;
                    this.chkEsCitacion.Checked = objEstado[0].EsCitacion;
                    this.chkEsEdicto.Checked = objEstado[0].EsEdicto;
                    this.chkEsAceptacionNotificacion.Checked = objEstado[0].EsAceptacionNotificacion;
                    this.chkEsRechazoNotificacion.Checked = objEstado[0].EsRechazoNotificacion;
                    this.chkEsAceptacionCitacion.Checked = objEstado[0].EsAceptacionCitacion;
                    this.chkEsRechazoCitacion.Checked = objEstado[0].EsRechazoCitacion;
                    this.chkEsAnulacion.Checked = objEstado[0].EsAnulacion;
                    this.chkEsFinalPublicidad.Checked = objEstado[0].EsFinalPublicidad;
                    this.divEstado.Visible = true;
                    this.cboEstado.SelectedValue = (objEstado[0].Activo ? "1" : "0");
                    this.CargarEstadoDependientes(objEstado[0].EstadoID, objEstado[0].EstadoDependienteID);
                    this.CargarListadoTiposAnexos();

                    //Cargar check de permitir anexar acto
                    this.chkPermiteAnexarActo.Checked = objEstado[0].PermitiAnexarActoAdministrativo;

                    //Validar si se encuentra seleccionado anexar acto administrativo
                    if (this.chkPermiteAnexarActo.Checked)
                    {
                        this.divPermiteAnexarConceptos.Visible = true;
                        this.chkPermiteAnexarConceptos.Checked = objEstado[0].PermitiAnexarConceptosActoAdministrativo;
                    }
                    else
                    {
                        this.divPermiteAnexarConceptos.Visible = false;
                        this.chkPermiteAnexarConceptos.Checked = false;
                    }

                    if (objEstado[0].EnviaCorreoAvanceManual || objEstado[0].EnviaCorreoAvance)
                    {
                        //Seleccionar opcion seleccionad
                        this.cboTipoAnexosCorreo.SelectedValue = (objEstado[0].TipoAnexoCorreoID > 0 ? objEstado[0].TipoAnexoCorreoID.ToString() : "-1");
                        //Mostrar div
                        this.divTipoAnexosCorreo.Visible = true;
                    }

                }
                else
                {
                    throw new Exception("No se encontro información del estado " + p_intEstadoFlujoID.ToString());
                }
            }
            catch(Exception exc){
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Administracion_Notificacion_EstadosFlujoNotificacion :: CargarInformacionEstadoFlujo -> Error Inesperado: " + exc.Message);

                throw exc;
            }
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


        #region cmdGuardarEstado

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de buscar Flujo. Carga la información del flujo
            /// </summary>
            protected void cmdGuardarEstado_Click(object sender, EventArgs e)
            {
                try
                {
                    //Verificar que los datos de validación sean correctos
                    if (Page.IsValid)
                    {
                        if (!string.IsNullOrWhiteSpace(this.hdfEstadoFlujoID.Value))
                        {
                            this.ModificarEstado();
                        }
                        else
                        {
                            this.CrearEstado();
                        }
                    }

                    //Limpiar modal
                    this.LimpiarModal();

                    //ACtualizar update
                    this.upnlFormulario.Update();

                    //Cerrar modal
                    this.mpeCrearEstado.Hide();
                }
                catch (Exception exc)
                {
                    this.MostrarMensajeModal("Se presento un error almacenando la información del estado. " + exc.Message);

                    //ACtualizar update
                    this.upnlFormulario.Update();
                }
            }

        #endregion


        #region cmdCancelar

            /// <summary>
            /// Evento que se ejecuta al dar clic en el botón de cancelar. Limpia los datos del modal
            /// </summary>
            protected void cmdCancelar_Click(object sender, EventArgs e)
            {
                //Limpiar campos de modal
                this.LimpiarModal();

                //Actualizar formulario
                this.upnlFormulario.Update();

                //Cerrar modal
                this.mpeCrearEstado.Hide();
            }

        #endregion


        #region grdEstados

            /// <summary>
            /// Evento que se ejecuta al dar clic en la paginación. Cambia de pagina
            /// </summary>
            protected void grdEstados_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                //Asignar nuevo index
                this.grdEstados.PageIndex = e.NewPageIndex;

                //Consultar informacion
                this.BuscarEstadosFlujo(Convert.ToInt32(this.cboFlujo.SelectedValue));
            }

        #endregion


        #region cboFlujo

            /// <summary>
            /// Evento que se ejecuta al seleccionar un nuevo flujo. Consulta los estados que se encuentran relacionados a un flujo
            /// </summary>
            protected void cboFlujo_SelectedIndexChanged(object sender, EventArgs e)
            {
                //Verificar que se seleccione flujo
                if (!string.IsNullOrWhiteSpace(this.cboFlujo.SelectedValue) && this.cboFlujo.SelectedValue != "-1")
                {
                    //Consultar estados
                    this.BuscarEstadosFlujo(Convert.ToInt32(this.cboFlujo.SelectedValue));

                    //Mostrar div
                    this.divEstados.Visible = true;
                }
                else{
                    //Limpiar grilla
                    this.grdEstados.DataSource = null;
                    this.grdEstados.DataBind();

                    //Ocultar div
                    this.divEstados.Visible = false;
                }

                //Actualizar panel
                this.upnlResultadoEstados.Update();
            }

        #endregion


        #region cboAutoridadBuscar

            /// <summary>
            /// Evento que filtra los flujos
            /// </summary>
            protected void cboAutoridadBuscar_SelectedIndexChanged(object sender, EventArgs e)
            {
                string strFlujo = "";

                //Cargar flujo seleccionado
                strFlujo = this.cboFlujo.SelectedValue;

                //Verificar que se seleccione flujo
                if (!string.IsNullOrWhiteSpace(this.cboAutoridadBuscar.SelectedValue) && this.cboAutoridadBuscar.SelectedValue != "-1")
                {
                    //Consultar estados
                    this.BuscarFlujos(Convert.ToInt32(this.cboAutoridadBuscar.SelectedValue));

                    try
                    {
                        this.cboFlujo.SelectedValue = strFlujo;
                    }
                    catch(Exception){
                        this.cboFlujo.SelectedValue = "-1";
                    }

                    //Actualizar datos de grilla
                    this.cboFlujo_SelectedIndexChanged(null, null);
                }
                else
                {
                    this.BuscarFlujos(-1);

                    //Seleccionar el flujo
                    this.cboFlujo.SelectedValue = strFlujo;
                    this.cboFlujo_SelectedIndexChanged(null, null);
                }

                //Actualizar panel
                this.upnlResultadoEstados.Update();
            }

        #endregion


        #region lnkEditarEstado

            /// <summary>
            /// Evento que se ejecuta la dar click en el link de editar. Muestra el modal de edición.
            /// </summary>
            protected void lnkEditarEstado_Click(object sender, EventArgs e)
            {
                FlujoNotificacionElectronica objFlujoNotificacionElectronica = null;
                DataTable objFlujo = null;

                try
                {
                    //Colocar titulo
                    this.ltlTituloCrear.Text = "EDITAR ESTADO";
                    this.cmdGuardarEstado.Text = "Modificar";

                    //Cargar el id del estado a modificar
                    this.hdfEstadoFlujoID.Value = ((LinkButton)sender).CommandArgument.ToString().Trim();

                    //Mostrar información
                    this.CargarInformacionEstadoFlujo(Convert.ToInt32(this.hdfEstadoFlujoID.Value));

                    //Cargar información del flujo
                    objFlujoNotificacionElectronica = new FlujoNotificacionElectronica();
                    objFlujo = objFlujoNotificacionElectronica.ConsultaFlujosNotificacion(Convert.ToInt32(this.cboFlujo.SelectedValue), null, null);

                    //Cargar el listado de plantillas
                    this.CargarPlantillas(this.cboPlantilla, Convert.ToInt32(objFlujo.Rows[0]["AUT_ID"]) );

                    //Modificar panel
                    this.upnlFormulario.Update();

                    //Mostrar modal
                    this.mpeCrearEstado.Show();

                }
                catch(Exception exc){
                    //Mostrar mensaje
                    this.MostrarMensaje("Se presento error cargando la información del estado. " + exc.Message);
                }
            }

        #endregion


        #region cmdNuevoEstado

            /// <summary>
            /// Evento que se ejecuta la dar click en el link nuevo estado
            /// </summary>
            protected void cmdNuevoEstado_Click(object sender, EventArgs e)
            {
                FlujoNotificacionElectronica objFlujoNotificacionElectronica = null;
                DataTable objFlujo = null;

                //Colocar titulo
                this.ltlTituloCrear.Text = "NUEVO ESTADO";
                this.cmdGuardarEstado.Text = "Adicionar";

                //Cargar el listado de estados
                this.divEstadoFlujoDesplegable.Visible = true;
                this.divEstadoFlujoLabel.Visible = false;
                this.BuscarEstadosNoFlujo(Convert.ToInt32(this.cboFlujo.SelectedValue));

                //Mostrar y ocultar controles                
                this.divEstado.Visible = false;
                this.divEstadoRelacionado.Visible = false;

                //Cargar información del flujo
                objFlujoNotificacionElectronica = new FlujoNotificacionElectronica();
                objFlujo = objFlujoNotificacionElectronica.ConsultaFlujosNotificacion(Convert.ToInt32(this.cboFlujo.SelectedValue), null, null);

                //Cargar el listado de plantillas
                this.CargarPlantillas(this.cboPlantilla, Convert.ToInt32(objFlujo.Rows[0]["AUT_ID"]), true);

                //Cargar listado de tipos adjuntos
                this.CargarListadoTiposAnexos();

                this.upnlFormulario.Update();

                //Mostrar modal
                this.mpeCrearEstado.Show();
            }

        #endregion


        #region chkGeneraPlantilla


            /// <summary>
            /// Evento que se ejecuta al dar clic en check de utilizar plantilla. Habilita o inhabilita campos dependientes.
            /// </summary>
            protected void chkGeneraPlantilla_CheckedChanged(object sender, EventArgs e)
            {
                //Activar plantilla si se requiere
                if (this.chkGeneraPlantilla.Checked)
                {
                    this.divPlantilla.Visible = true;

                    if(this.chkPublicaEstado.Checked)
                        this.divPublicaPlantilla.Visible = true;
                }
                else
                {
                    this.cboPlantilla.ClearSelection();
                    this.divPlantilla.Visible = false;
                    this.divPublicaPlantilla.Visible = false;
                    this.chkPublicaPlantilla.Checked = false;
                }

                this.upnlFormulario.Update();
            }

        #endregion


        #region chkEnvioCorreoAutomatico

            /// <summary>
            /// Evento que se ejecuta al dar clic en check de enviar correo. Habilita o inhabilita campo para texto de correo.
            /// </summary>
            protected void chkEnvioCorreoAutomatico_CheckedChanged(object sender, EventArgs e)
            {
                //Activar plantilla si se requiere
                if (this.chkEnvioCorreoAutomatico.Checked)
                {
                    this.divTextoCorreo.Visible = true;
                    this.divTipoAnexosCorreo.Visible = true;
                }
                else
                {
                    this.divTextoCorreo.Visible = false;
                    this.txtTextoCorreoAutomatico.Text = "";
                    if (!this.chkEnvioCorreoManual.Checked)
                    {
                        this.cboTipoAnexosCorreo.ClearSelection();
                        this.divTipoAnexosCorreo.Visible = false;
                    }
                }

                this.upnlFormulario.Update();
            }

        #endregion


        #region chkPublicaEstado

            /// <summary>
            /// Evento que se ejecuta al dar clic en check de publicar estados. Habilita o inhabilita campos dependientes.
            /// </summary>
            protected void chkPublicaEstado_CheckedChanged(object sender, EventArgs e)
            {
                //Activar plantilla si se requiere
                if (this.chkPublicaEstado.Checked)
                {
                    if(this.chkGeneraPlantilla.Checked)
                        this.divPublicaPlantilla.Visible = true;                    
                    if(this.chkAnexaAdjunto.Checked)
                        this.divPublicaAdjunto.Visible = true;
                }
                else
                {
                    this.divPublicaPlantilla.Visible = false;
                    this.chkPublicaPlantilla.Checked = false;
                    this.divPublicaAdjunto.Visible = false;
                    this.chkPublicaAdjunto.Checked = false;
                }

                this.upnlFormulario.Update();
            }

        #endregion


        #region chkEnvioCorreoManual

            /// <summary>
            /// Evento que se ejecuta al dar clic en check de enviar correo manual. Habilita o inhabilita campo confirmación anexo adjuntos.
            /// </summary>
            protected void chkEnvioCorreoManual_CheckedChanged(object sender, EventArgs e)
            {
                //Activar plantilla si se requiere
                if (this.chkEnvioCorreoManual.Checked)
                {
                    this.divAdjuntos.Visible = true;
                    this.divTipoAnexosCorreo.Visible = true;
                }
                else
                {
                    this.divAdjuntos.Visible = false;
                    this.chkAnexaAdjunto.Checked = false;
                    if (!this.chkEnvioCorreoAutomatico.Checked)
                    {
                        this.cboTipoAnexosCorreo.ClearSelection();
                        this.divTipoAnexosCorreo.Visible = false;
                    }
                }

                this.upnlFormulario.Update();
            }

        #endregion


        #region chkAnexaAdjunto

            /// <summary>
            /// Evento que se ejecuta al dar clic en check de anexar correo. Habilita o inhabilita campos dependientes
            /// </summary>
            protected void chkAnexaAdjunto_CheckedChanged(object sender, EventArgs e)
            {
                //Activar plantilla si se requiere
                if (this.chkAnexaAdjunto.Checked)
                {
                    if(this.chkPublicaEstado.Checked)
                        this.divPublicaAdjunto.Visible = true;
                }
                else
                {
                    this.divPublicaAdjunto.Visible = false;
                    this.chkPublicaAdjunto.Checked = false;
                }

                this.upnlFormulario.Update();
            }

        #endregion


        #region chkSolicitarConfirmacionNotificacion

            /// <summary>
            /// Evento que se ejecuta al dar clic en check de solicitar confirmación de notificación.
            /// </summary>
            protected void chkSolicitarConfirmacionNotificacion_CheckedChanged(object sender, EventArgs e)
            {
                //Activar plantilla si se requiere
                if (this.chkSolicitarConfirmacionNotificacion.Checked)
                {
                    this.divConfirmacionNotificacionObligatoria.Visible = true;
                }
                else
                {
                    this.divConfirmacionNotificacionObligatoria.Visible = false;
                    this.chkConfirmacionNotificacionObligatoria.Checked = false;
                }

                this.upnlFormulario.Update();
            }

        #endregion


        #region cboEstadoFlujo

            /// <summary>
            /// Evento que carga información relacionado a estado
            /// </summary>
            protected void cboEstadoFlujo_SelectedIndexChanged(object sender, EventArgs e)
            {
                //Cargar datos estado
                if (!string.IsNullOrWhiteSpace(this.cboEstadoFlujo.SelectedValue) && this.cboEstadoFlujo.SelectedValue != "-1")
                {
                    this.CargarEstadoDependientes(Convert.ToInt32(this.cboEstadoFlujo.SelectedValue));
                    this.divEstadoRelacionado.Visible = true;
                }
                else
                {
                    this.cboEstadoRelacionado.ClearSelection();
                    this.cboEstadoRelacionado.Items.Clear();
                    this.divEstadoRelacionado.Visible = false;
                }

                //Actualizar panel
                this.upnlFormulario.Update();
            }

        #endregion


        #region chkPermiteAnexarActo


            /// <summary>
            /// Evento que habilita o deshabilita el campos relacionados al check de anexo de acto
            /// </summary>
            protected void chkPermiteAnexarActo_CheckedChanged(object sender, EventArgs e)
            {
                //Validar que se encuentre activo
                if (this.chkPermiteAnexarActo.Checked)
                {
                    //Mostrar check anexos conceptos
                    this.divPermiteAnexarConceptos.Visible = true;
                    this.chkPermiteAnexarConceptos.Checked = false;
                }
                else
                {
                    //Ocultar check anexos conceptos
                    this.divPermiteAnexarConceptos.Visible = false;
                    this.chkPermiteAnexarConceptos.Checked = false;
                }
            }

        #endregion


    #endregion
        
}