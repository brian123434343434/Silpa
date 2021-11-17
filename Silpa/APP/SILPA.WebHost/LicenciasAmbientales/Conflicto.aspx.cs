using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SILPA.Servicios.SolicitudDAA;
using SILPA.Comun;
using SILPA.Servicios.Generico.RadicarDocumento;
using SILPA.LogicaNegocio.Generico;
using Silpa.Workflow;
using SoftManagement.Log;

public partial class LicenciasAmbientales_Conflicto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["IDProcessInstance"] = long.Parse(Request.QueryString["IDProcessInstance"]);
        Session["IDActivityInstance"] = long.Parse(Request.QueryString["IDActivityInstance"]);
        Session["EntryData"] = Request.QueryString["EntryData"];
        Session["IDEntryData"] = Request.QueryString["IDEntryData"];
        Session["IdRelated"] = Request.QueryString["IdRelated"];
        if (!IsPostBack)
        {
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            cboAutoridadAmbiental.DataSource = _listaAutoridades.ListarAutoridades(null);
            cboAutoridadAmbiental.DataValueField = "AUT_ID";
            cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            cboAutoridadAmbiental.DataBind();
            cboAutoridadAmbiental.Enabled = true;
        }      
        if (ValidacionToken() == false)
            Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");

        CargarPagina();
        this.optAutoridadAmbiental.SelectedValue = "2";
        CargarCombo();


    }

    /// <summary>
    /// Valida si hay token para el usuario que intenta acceder a la página, y si no ha expirado
    /// </summary>
    /// <returns>Verdadero si hay token válido para el usuario</returns>
    private bool ValidacionToken()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ValidacionToken.Inicio");
            Session["IDForToken"] = Request.QueryString["IdRelated"];

            string nana = HttpContext.Current.User.Identity.Name;

            SILPA.LogicaNegocio.Usuario.TokenUsuario token = new SILPA.LogicaNegocio.Usuario.TokenUsuario();

            Session["Token"] = token.TomarTokenUsuario(Int32.Parse(Session["IDForToken"].ToString()));
            using (WSValidacionToken.GattacaSecurityServices9000 servicio = new WSValidacionToken.GattacaSecurityServices9000())
            {
                servicio.Url = SILPA.Comun.DireccionamientoWS.UrlWS("WSValidacionToken");
                string mensaje = servicio.GetUserInfoByToken("Softmanagement", Session["Token"].ToString());
                if (mensaje.IndexOf("Token invalido") > 0)
                    return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
            return false;
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ValidacionToken.Finalizo");
        }
    }

    private void CargarCombo()
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".optAutoridadAmbiental_SelectedIndexChanged.Inicio");
            if (optAutoridadAmbiental.SelectedValue == "2")
            {
                SolicitudFachada _solicitudFachada = new SolicitudFachada();
                cboAutoridadAmbiental.Visible = true;

                cboAutoridadAmbiental.DataSource = _solicitudFachada.ConsultarAutoridadesAmbientales((long)Session["IDProcessInstance"]);
                cboAutoridadAmbiental.DataValueField = "AUT_ID";
                cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
                cboAutoridadAmbiental.DataBind();

                cboAutoridadAmbiental.Visible = true;

                lblMensaje.Visible = true;
                //this.pnlPrincipal.Visible = true;
            }
            else
            {
                cboAutoridadAmbiental.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".optAutoridadAmbiental_SelectedIndexChanged.Finalizo");
        }
    }

    /// <summary>
    /// Muestra las AA que estan en conflicto para que sean seleccionadas por el usuario.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void optAutoridadAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCombo();

    }

    /// <summary>
    /// Metodo para resolver el conflicto de competencias
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAceptar_Click.Inicio");
            int _idRadicacion = 0;
            SolicitudFachada _solicitudFachada = new SolicitudFachada();
            Proceso _objProceso = new Proceso();

            SILPA.Servicios.BPMServices.GattacaBPMServices9000 _servicioBPM = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();
            _servicioBPM.Url = SILPA.Comun.DireccionamientoWS.UrlWS("GattacaBpm");

            int _intAutoridadAmbiental = 0;
            if (optAutoridadAmbiental.SelectedItem.Value == "1")
            {
                //_intAutoridadAmbiental = (int)AutoridadesAmbientales.MAVDT;
                AutoridadAmbiental aa = new AutoridadAmbiental();
                _intAutoridadAmbiental = aa.ObtenerIdAutoridadAmbientalMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);
            }
            else if (optAutoridadAmbiental.SelectedItem.Value == "2")
                _intAutoridadAmbiental = Convert.ToInt32(cboAutoridadAmbiental.SelectedItem.Value);

            _idRadicacion = _solicitudFachada.ActualizarRadicacion((long)Session["IDProcessInstance"], _intAutoridadAmbiental);

            if (_idRadicacion == -1)
            {
                Mensaje.MostrarMensaje(this, "Se ha detectado que existe mas de una radicación para este proceso");
                return;
            }

            //Se actualiza la solicitud con la AA escogida
            _solicitudFachada.ActualizarAASolicitud((long)Session["IDProcessInstance"], _intAutoridadAmbiental, _idRadicacion);

            //Se publica radicado...

            _objProceso.EstablecerCondicion(Convert.ToInt32(Session["IDActivityInstance"]), "Radicacion");

            //Se finaliza la actividad
            /// Comentado 04-jun-2010
            /// 

            //SMLog.Escribir(Severidad.Informativo,"Conflico A");
            _servicioBPM.EndActivityInstance("Softmanagement", Convert.ToInt64(Session["IdRelated"]), (long)Session["IDActivityInstance"], (long)Session["IDProcessInstance"], _objProceso.IdCondicion, "", "", (string)Session["EntryData"], "0", (string)Session["IDEntryData"]);
            //SMLog.Escribir(Severidad.Informativo, "Conflico B");
            //ServicioWorkflow servicioWorkflow = new ServicioWorkflow();

            //long longIDProcessInstance = (long)Session["IDProcessInstance"];
            //servicioWorkflow.ValidarActividadActual(longIDProcessInstance, ActividadSilpa.ValidarRequiereDAA);
            //servicioWorkflow.FinalizarTarea(longIDProcessInstance, ActividadSilpa.ValidarRequiereDAA, DatosSesion.Usuario);

            //Se actualiza el estado de la solicitud

            _solicitudFachada.ActualizarEstadoSolicitud((long)Session["IDProcessInstance"], (int)EstadoProcesoDAA.Pendiente_Radicacion);

            ActualizarEstadoRadicadoLeido(_idRadicacion);

            //Se esconde el botón para que no sea presionado de nuevo
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;


            optAutoridadAmbiental.Visible = false;
            cboAutoridadAmbiental.Visible = false;

            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
            CrearLogAuditoria.Insertar("LICENCIASAMBIENTALES – RADICAR FU", 1, "Se almaceno Competencias");

            /// 04-jun-10
            //lblMensaje.Text = "Solicitud Enviada con éxito, puede cerrar está ventana ahora";
            Mensaje.MostrarMensaje(this, "Solicitud Enviada con éxito, puede cerrar está ventana ahora", true);
        }
        catch (Exception ex)
        {
            Mensaje.ErrorCritico(this, ex);
            //SMLog.Escribir(Severidad.Informativo,"Conflicto-->>" + ex.ToString());
            SMLog.Escribir(ex);
        }
        finally
        {
            SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".btnAceptar_Click.Finalizo");
        }

    }

    /// <summary>
    /// Cancela la operacion actual y cierra la ventana.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Utilidades.CerrarVentana(this);
    }

    /// <summary>
    /// Carga los valore iniciales en la pagina.
    /// </summary>
    protected void CargarPagina()
    {
        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".CargarPagina.Inicio");
        SolicitudFachada _solicitudFachada = new SolicitudFachada();
        Configuracion _objConfiguracion = new Configuracion();

        lblNombreFormulario.Text = "";
        lblMensaje.Text = "\n Se ha detectado que su Proyecto, Obra o Actividad tiene jurisdicción en varias autoridades ambientales, por tal razón debe seleccionar la Autoridad Ambiental a la cual desea enviar su solicitud:";
        lblMensaje.Visible = true;
        lblNombreFormulario.Visible = true;
        
    
    }

    /// <summary>
    /// Metodo para actualizar en estado de la radicacion a no leido.
    /// </summary>
    /// <param name="idRadicacion"></param>
    protected void ActualizarEstadoRadicadoLeido(int idRadicacion)
    {
        RadicacionDocumento rad = new RadicacionDocumento();
        rad.ObtenerRadicacion(idRadicacion, null);
        if (rad._objRadDocIdentity.Id != 0)
        {
            rad._objRadDocIdentity.Leido = false;
            rad.ActualizarEstadoRadicacion();
        }
    }
}
