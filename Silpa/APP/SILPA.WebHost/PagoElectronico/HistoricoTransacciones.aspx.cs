using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Generico;

public partial class PagoElectronico_HistoricoTransacciones : System.Web.UI.Page
{
    #region Metodos Privados


        /// <summary>
        /// Ocultar mensaje mostrado
        /// </summary>
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = "";
            this.divMensaje.Visible = false;
            this.upnlMensaje.Update();
        }

        /// <summary>
        /// Mostrar el mensaje especificado
        /// </summary>
        /// <param name="p_strMensaje">string con el mensaje</param>
        private void MostrarMensaje(string p_strMensaje)
        {
            this.lblMensaje.Text = p_strMensaje;
            this.divMensaje.Visible = true;
            this.upnlMensaje.Update();
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
        /// Inicializar la página
        /// </summary>
        private void CargarPagina()
        {
            //Cargar las fechas de pagina
            this.txtFechaDesde.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");

            //Verificar si se indica el trazability code
            if (Session["TrazabilityCode"] != null && !string.IsNullOrWhiteSpace(Session["TrazabilityCode"].ToString()))
            {
                this.txtCUS.Text = Session["TrazabilityCode"].ToString();                
            }

            //Realizar busqueda
            this.cmdBuscar_Click(null, null);
        }


        /// <summary>
        /// BUscar la informacion de transacciones existentes que cumplan con las condiciones de busqueda
        /// </summary>
        private void BuscarTransacciones()
        {
            Cobro objCobro = null;

            //Realizar la consulta
            objCobro = new Cobro();
            this.grdTransacciones.DataSource = objCobro.ListarTransaccionesPSEUsuario(Convert.ToInt32(Session["Usuario"]), this.hdfNumeroVital.Value, (!string.IsNullOrWhiteSpace(this.hdfCUS.Value) ? Convert.ToInt64(this.hdfCUS.Value) : -1), Convert.ToDateTime(this.hdfFechaDesde.Value), Convert.ToDateTime(this.hdfFechaHasta.Value));
            this.grdTransacciones.DataBind();
            this.upnlTransacciones.Update();
        }


    #endregion


    #region Eventos

        /// <summary>
        /// Evento que realiza la inicialización de los datos
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Session["Usuario"] = "429";
                //this.CargarPagina();

                //Cargar datos pagina
                if (this.ValidacionToken() == false)
                {
                    Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
                }
                else
                {
                    //    Iniciliazar datos de la página
                    this.CargarPagina();
                }
            }
        }


        /// <summary>
        /// Evento que realiza la busqueda de las transacciones
        /// </summary>
        protected void cmdBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Ocultar mensaje
                this.OcultarMensaje();

                //Cargar datos de busqueda
                this.hdfFechaDesde.Value = this.txtFechaDesde.Text.Trim();
                this.hdfFechaHasta.Value = this.txtFechaHasta.Text.Trim();
                this.hdfCUS.Value = this.txtCUS.Text.Trim();
                this.hdfNumeroVital.Value = this.txtNumeroVital.Text.Trim();

                //Buscar informacion
                this.grdTransacciones.PageIndex = 0;
                this.BuscarTransacciones();
            }
            catch(Exception exc){
                //Mostrar mensaje
                this.MostrarMensaje("Se presento error durante la consulta de información de las transacciones");

                //Escribir log
                SMLog.Escribir(Severidad.Critico, "PagoElectronico_HistoricoTransacciones :: cmdBuscar_Click: Error Consultando Información de Transacciones " + exc.StackTrace.ToString());
            }
        }


        /// <summary>
        /// Evento que realiza cambio pagina
        /// </summary>
        protected void grdTransacciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //Ocultar mensaje
                this.OcultarMensaje();

                //Buscar informacion
                this.grdTransacciones.PageIndex = e.NewPageIndex;
                this.BuscarTransacciones();
            }
            catch (Exception exc)
            {
                //Mostrar mensaje
                this.MostrarMensaje("Se presento error durante el cambio de página");

                //Escribir log
                SMLog.Escribir(Severidad.Critico, "PagoElectronico_HistoricoTransacciones :: grdTransacciones_PageIndexChanging: Error Realizando cambio de página " + exc.StackTrace.ToString());
            }
        }

    #endregion

    
}