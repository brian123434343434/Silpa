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
using Encriptar;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Usuario;
using System.Data.SqlClient;
using SILPA.Comun;
using SILPA.LogicaNegocio.Generico;
using System.Collections.Generic;
using SILPA.AccesoDatos.ImpresionesFus;
using System.IO;
using SoftManagement.Log;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Usuario;

public partial class DatosPersonales : System.Web.UI.Page
{
    #region Objetos

        /// <summary>
        /// Objeto que contiene la configuración del aplicativo
        /// </summary>
        private SILPA.Comun.Configuracion objConfiguracion;

        /// <summary>
        /// Indica si se debe validar el recaptcha
        /// </summary>
        protected string ValidarRecaptcha{ get; set; }

    #endregion


    #region Metodos Privados

        /// <summary>
        /// Valida el usuario registrado
        /// </summary>
        private void ValidarUsuarioRegistrado()
        {
            try
            {
                //SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".ValidarUsuarioRegistrado.Inicio");
                #region Usuario Registrado
                {
                    if (Request.QueryString["IdUser"] != null)
                    {
                        btnActualizar.Text = "Actualizar";
                        cboAutoridadAmbiental.Enabled = false;
                        this.ValidarRecaptcha = "false";
                        this.trRecaptcha.Visible = false;                       
                        SILPA.LogicaNegocio.Generico.Persona _persona = new SILPA.LogicaNegocio.Generico.Persona();
                        _persona.ObternerPersonaByUserIdApp(Request.QueryString["IdUser"]);
                        int tipopersona = _persona.Identity.TipoPersona.CodigoTipoPersona;
                        cboAutoridadAmbiental.SelectedValue = _persona.Identity.IdAutoridadAmbiental.ToString();

                        #region Persona Natural Registrada
                        tbcContenedor.Tabs[0].Enabled = false;
                        tbcContenedor.Tabs[0].HeaderText = string.Empty;
                        //tbcContenedor.Tabs.Remove(tbcContenedor.Tabs["tabDatosUsuario"]);

                        if (tipopersona == 1)  //natural
                        {
                            txtPrimerNombreNatural.Enabled = false;
                            txtSegundoNombreNatural.Enabled = false;
                            txtPrimerApellidoNatural.Enabled = false;
                            txtSegundoApellidoNatural.Enabled = false;
                            HabilitarValidacionJuridica(false);
                            HabilitarValidacionPrivada(false);
                            txtNumeroIdentificacion.Enabled = false;
                            cboTipoDocumento.Enabled = false;
                            cboDepartamentoOrigenNatural.Enabled = cboTipoDocumento.Enabled;
                            cboMunicipioOrigenNatural.Enabled = cboTipoDocumento.Enabled;

                            //JMM - 07-07-2010
                            tbcContenedor.Tabs[1].Enabled = true;
                            //tbcContenedor.Tabs[1].Visible = true;
                            tbcContenedor.Tabs[2].Enabled = false;
                            tbcContenedor.Tabs[2].Visible = false;
                            tbcContenedor.Tabs[2].HeaderText = string.Empty;
                            tbcContenedor.Tabs[3].Enabled = false;
                            tbcContenedor.Tabs[3].Visible = false;
                            tbcContenedor.Tabs[3].HeaderText = string.Empty;
                            tbcContenedor.Tabs[4].Enabled = true;

                            //JMM

                            txtPrimerNombreNatural.Text = _persona.Identity.PrimerNombre;
                            txtSegundoNombreNatural.Text = _persona.Identity.SegundoNombre;
                            txtPrimerApellidoNatural.Text = _persona.Identity.PrimerApellido;
                            txtSegundoApellidoNatural.Text = _persona.Identity.SegundoApellido;

                            cboTipoDocumento.SelectedIndex = retornarIndice(cboTipoDocumento,

                            _persona.Identity.TipoDocumentoIdentificacion.Id.ToString());

                            txtNumeroIdentificacion.Text = _persona.Identity.NumeroIdentificacion;
                            txtTelefonoNatural.Text = _persona.Identity.Telefono;
                            txtCelularNatural.Text = _persona.Identity.Celular;
                            txtFaxNatural.Text = _persona.Identity.Fax;
                            txtCorreoNatural.Text = _persona.Identity.CorreoElectronico;


                            /// HAVA: 26-mayo-10
                            /*Cargar el valor del combo del documento */
                            DireccionPersonaIdentity dirDomicilio =
                            _persona.Identity.Direcciones.Find(delegate(DireccionPersonaIdentity dirDom)
                            { return dirDom.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Domicilio; });


                            if (dirDomicilio != null)
                            {
                                txtDireccionNatural.Text = dirDomicilio.DireccionPersona;

                            }
                            else
                            {
                                txtDireccionNatural.Text = string.Empty;
                            }

                            cboDepartamentoNatural.Enabled = true;
                            cboMunicipioNatural.Enabled = true;
                            cboPaisNatural.SelectedIndex = retornarIndice(cboPaisNatural, _persona.Identity.DireccionPersona.PaisId.ToString());
                            cboDepartamentoNatural.SelectedIndex = retornarIndice(cboDepartamentoNatural,
                            _persona.Identity.DireccionPersona.DepartamentoId.ToString());

                            CargarComboMunicipios(cboDepartamentoNatural.SelectedValue, cboMunicipioNatural);
                            cboMunicipioNatural.SelectedIndex = retornarIndice(cboMunicipioNatural,
                            _persona.Identity.DireccionPersona.MunicipioId.ToString());

                            CargarComboCorregimientos(cboMunicipioNatural.SelectedValue, cboCorregimientoNatural);
                            cboCorregimientoNatural.SelectedIndex = retornarIndice(cboCorregimientoNatural, dirDomicilio.CorregimientoId.ToString());

                            CargarComboVeredas(cboCorregimientoNatural.SelectedValue, cboVeredaNatural);
                            cboVeredaNatural.SelectedIndex = retornarIndice(cboVeredaNatural, dirDomicilio.VeredaId.ToString());

                            /// HAVA: 26-mayo-10
                            /*Cargar el valor del combo del documento */

                            DireccionPersonaIdentity dirExpDocumento =
                            _persona.Identity.Direcciones.Find(delegate(DireccionPersonaIdentity dir)
                            { return dir.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Expedicion_Documento; });

                            if (dirExpDocumento != null)
                            {
                                cboDepartamentoOrigenNatural.SelectedIndex = retornarIndice(cboDepartamentoOrigenNatural, dirExpDocumento.DepartamentoId.ToString());
                                CargarComboMunicipios(cboDepartamentoOrigenNatural.SelectedValue, cboMunicipioOrigenNatural);
                                cboMunicipioOrigenNatural.SelectedIndex = retornarIndice(cboMunicipioOrigenNatural, dirExpDocumento.MunicipioId.ToString());
                            }
                            else
                            {
                                cboDepartamentoOrigenNatural.SelectedIndex = 0;
                                cboMunicipioOrigenNatural.SelectedIndex = 0;
                            }

                            /* Cargar el valor del combo de correspondencia*/
                            DireccionPersonaIdentity dirCorrespondencia =
                            _persona.Identity.Direcciones.Find(delegate(DireccionPersonaIdentity dirCor)
                            { return dirCor.TipoDireccion == (int)SILPA.Comun.TipoDireccion.Correspondencia; });

                            if (dirCorrespondencia != null)
                            {
                                cboPaisCorrespondencia.SelectedIndex = retornarIndice(cboPaisCorrespondencia, dirCorrespondencia.PaisId.ToString());
                                cboDepartamentoCorrespondencia.SelectedIndex = retornarIndice(cboDepartamentoCorrespondencia, dirCorrespondencia.DepartamentoId.ToString());
                                CargarComboMunicipios(cboDepartamentoCorrespondencia.SelectedValue, cboMunicipioCorrespondencia);
                                cboMunicipioCorrespondencia.SelectedIndex = retornarIndice(cboMunicipioCorrespondencia, dirCorrespondencia.MunicipioId.ToString());
                                CargarComboCorregimientos(cboMunicipioCorrespondencia.SelectedValue, cboCorregimientoCorrespondencia);
                                cboCorregimientoCorrespondencia.SelectedIndex = retornarIndice(cboCorregimientoCorrespondencia, dirCorrespondencia.CorregimientoId.ToString());
                                CargarComboVeredas(cboCorregimientoCorrespondencia.SelectedValue, cboVeredaCorrespondencia);
                                cboVeredaCorrespondencia.SelectedIndex = retornarIndice(cboVeredaCorrespondencia, dirCorrespondencia.VeredaId.ToString());
                                txtDireccionCorrespondencia.Text = dirCorrespondencia.DireccionPersona.ToString();
                            }
                            else
                            {
                                cboPaisCorrespondencia.SelectedIndex = 0;
                                cboDepartamentoCorrespondencia.SelectedIndex = 0;
                                cboMunicipioCorrespondencia.SelectedIndex = 0;
                                cboCorregimientoCorrespondencia.SelectedIndex = 0;
                                cboVeredaCorrespondencia.SelectedIndex = 0;
                            }

                            /*--*/

                            cboCorregimientoNatural.SelectedIndex = retornarIndice(cboCorregimientoNatural, _persona.Identity.DireccionPersona.CorregimientoId.ToString());
                            cboVeredaNatural.SelectedIndex = retornarIndice(cboVeredaNatural, _persona.Identity.DireccionPersona.VeredaId.ToString());

                            this.ChkAutorizaNotifPerNatural.Checked = _persona.Identity.AutorizaCorreo; //jmartinez adicionar campo autoriza correo
                        }
                        #endregion

                        #region Persona Juridica Publica Registrada
                        else if (tipopersona == 2)    //juridica publica
                        {
                            txtRazonSocialJuridica.Enabled = false;
                            HabilitarValidacionNatural(false);
                            HabilitarValidacionPrivada(false);
                            cboTipoDocumentoJuridica.Enabled = false;
                            txtNumeroDocumentoJuridica.Enabled = false;
                            tbcContenedor.Tabs[1].Enabled = false;
                            tbcContenedor.Tabs[1].Visible = false;
                            tbcContenedor.Tabs[1].HeaderText = string.Empty;
                            tbcContenedor.Tabs[2].Enabled = true;
                            tbcContenedor.Tabs[2].Visible = true;
                            tbcContenedor.Tabs[3].Enabled = false;
                            tbcContenedor.Tabs[3].Visible = false;
                            tbcContenedor.Tabs[3].HeaderText = string.Empty;
                            tbcContenedor.Tabs[4].Enabled = true;
                            tbcContenedor.Tabs[4].Visible = true;

                            cboTipoDocumentoJuridica.SelectedIndex = retornarIndice(cboTipoDocumentoJuridica,
                                _persona.Identity.TipoDocumentoIdentificacion.Id.ToString());
                            txtRazonSocialJuridica.Text = _persona.Identity.RazonSocial;
                            txtNumeroDocumentoJuridica.Text = _persona.Identity.NumeroIdentificacion;
                            txtTelefonoJuridica.Text = _persona.Identity.Telefono;
                            txtCelularJuridica.Text = _persona.Identity.Celular;
                            txtFaxJuridica.Text = _persona.Identity.Fax;
                            txtCorreoJuridica.Text = _persona.Identity.CorreoElectronico;

                            if (_persona.Identity.DireccionPersona.DireccionPersona == null)
                            {
                                txtDireccionJuridica.Text = "";
                            }
                            else
                            {
                                txtDireccionJuridica.Text = _persona.Identity.DireccionPersona.DireccionPersona.ToString();
                            }

                            cboDepartamentoJuridica.Enabled = true;
                            cboMunicipioJuridica.Enabled = true;
                            cboPaisJuridica.SelectedIndex = retornarIndice(cboPaisJuridica,
                            _persona.Identity.DireccionPersona.PaisId.ToString());
                            cboDepartamentoJuridica.SelectedIndex = retornarIndice(cboDepartamentoJuridica,
                            _persona.Identity.DireccionPersona.DepartamentoId.ToString());

                            CargarComboMunicipios(cboDepartamentoJuridica.SelectedValue, cboMunicipioJuridica);
                            cboMunicipioJuridica.SelectedIndex = retornarIndice(cboMunicipioJuridica,
                            _persona.Identity.DireccionPersona.MunicipioId.ToString());

                            CargarComboCorregimientos(cboMunicipioJuridica.SelectedValue, cboCorregimientoJuridica);
                            cboCorregimientoJuridica.SelectedIndex = retornarIndice(cboCorregimientoJuridica,
                            _persona.Identity.DireccionPersona.CorregimientoId.ToString());

                            CargarComboVeredas(cboCorregimientoJuridica.SelectedValue, cboVeredaJuridica);
                            cboVeredaJuridica.SelectedIndex = retornarIndice(cboVeredaJuridica,
                            _persona.Identity.DireccionPersona.VeredaId.ToString());

                            this.ChkAutorizaNotifPerJuridica.Checked = _persona.Identity.AutorizaCorreo; //jmartinez campo autoriza correo

                        }
                        #endregion

                        #region Persona Juridica Privada Registrada
                        else if (tipopersona == 3)   //juridica privada
                        {
                            txtRazonSocialPrivada.Enabled = false;
                            HabilitarValidacionNatural(false);
                            HabilitarValidacionJuridica(false);
                            cboTipoDocumentoPrivada.Enabled = false;
                            txtNumeroDocumentoPrivada.Enabled = false;
                            tbcContenedor.Visible = true;
                            tbcContenedor.Tabs[1].Enabled = false;
                            tbcContenedor.Tabs[1].Visible = false;
                            tbcContenedor.Tabs[1].HeaderText = string.Empty;
                            tbcContenedor.Tabs[2].Enabled = false;
                            tbcContenedor.Tabs[2].Visible = false;
                            tbcContenedor.Tabs[2].HeaderText = string.Empty;
                            tbcContenedor.ActiveTabIndex = 3;
                            tbcContenedor.Tabs[3].Enabled = true;
                            tbcContenedor.Tabs[4].Enabled = true;
                            tbcContenedor.Tabs[3].Visible = true;
                            tbcContenedor.Tabs[4].Visible = true;

                            cboTipoDocumentoPrivada.SelectedIndex = retornarIndice(cboTipoDocumentoPrivada,
                                _persona.Identity.TipoDocumentoIdentificacion.Id.ToString());
                            txtRazonSocialPrivada.Text = _persona.Identity.RazonSocial;
                            txtNumeroDocumentoPrivada.Text = _persona.Identity.NumeroIdentificacion;
                            txtTelefonoPrivada.Text = _persona.Identity.Telefono;
                            txtCelularPrivada.Text = _persona.Identity.Celular;
                            txtFaxPrivada.Text = _persona.Identity.Fax;
                            txtCorreoPrivada.Text = _persona.Identity.CorreoElectronico;

                            if (_persona.Identity.DireccionPersona.DireccionPersona == null)
                                txtDireccionPrivada.Text = "";
                            else
                                txtDireccionPrivada.Text = _persona.Identity.DireccionPersona.DireccionPersona.ToString();
                            cboDepartamentoPrivada.Enabled = true;
                            cboMunicipioPrivada.Enabled = true;

                            cboPaisPrivada.SelectedIndex = retornarIndice(cboPaisPrivada,
                                _persona.Identity.DireccionPersona.PaisId.ToString());
                            cboDepartamentoPrivada.SelectedIndex = retornarIndice(cboDepartamentoPrivada,
                            _persona.Identity.DireccionPersona.DepartamentoId.ToString());

                            CargarComboMunicipios(cboDepartamentoPrivada.SelectedValue, cboMunicipioPrivada);
                            cboMunicipioPrivada.SelectedIndex = retornarIndice(cboMunicipioPrivada,
                            _persona.Identity.DireccionPersona.MunicipioId.ToString());

                            CargarComboCorregimientos(cboMunicipioPrivada.SelectedValue, cboCorregimientoPrivada);
                            cboCorregimientoPrivada.SelectedIndex = retornarIndice(cboCorregimientoPrivada,
                            _persona.Identity.DireccionPersona.CorregimientoId.ToString());

                            CargarComboVeredas(cboCorregimientoPrivada.SelectedValue, cboVeredaPrivada);
                            cboVeredaPrivada.SelectedIndex = retornarIndice(cboVeredaPrivada,
                            _persona.Identity.DireccionPersona.VeredaId.ToString());

                            this.ChkAutorizaNotifPerPrivada.Checked = _persona.Identity.AutorizaCorreo; //jmartinez campo autoriza correo
                        }
                        #endregion


                        if (Request.QueryString["IdUser"] != null)
                        {
                            hrCerrarVentana.Visible = true;

                            if ((DataTable)Session["Apoderados"] == null)
                            {
                                DataTable dt = new DataTable();
                                DataSet ds = new DataSet();
                                //JMM - Julio 06 de 2010
                                //ds = _persona.PersonasAsociadasSolicitanteLeft(-1, Convert.ToInt32(Request.QueryString["IdUser"]));
                                ds = _persona.PersonasAsociadasSolicitanteApoderado((int)SILPA.Comun.TipoPersona.Apoderado, Convert.ToInt32(_persona.Identity.PersonaId));                    //
                                dt.Merge(ds.Tables[0]);

                                CargarApoderados(dt);
                            }
                            CargarApoderados();

                            if ((DataTable)Session["Representantes"] == null)
                            {
                                DataTable dtr = new DataTable();
                                DataSet dsr = new DataSet();
                                //JMM - Julio 06 de 2010
                                //ds = _persona.PersonasAsociadasSolicitanteLeft(-1, Convert.ToInt32(Request.QueryString["IdUser"]));
                                dsr = _persona.PersonasAsociadasSolicitanteApoderado((int)SILPA.Comun.TipoPersona.RepresentanteLegal, Convert.ToInt32(_persona.Identity.PersonaId));                    //
                                dtr.Merge(dsr.Tables[0]);

                                CargarRepresentantes(dtr);
                            }
                            CargarRepresentantes();
                        }
                    }
                    else
                    {
                        this.ValidarRecaptcha = "true";
                        this.trRecaptcha.Visible = true;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DatosPersonales :: ValidarUsuarioRegistrado -> Error Inesperado: " + ex.StackTrace);
                throw ex;
            }
        }


        /// <summary>
        /// Carga la pestañas de acuerdo al tipo de persona
        /// </summary>
        private void VisualizarPestañas()
        {
            if (Session["Representantes"] != null)
                Session.Remove("Representantes");

            if (optTipoUsuario.SelectedItem.Value == "natural")
            {
                tbcContenedor.Tabs[1].Visible = true;
                tbcContenedor.Tabs[2].Visible = false;
                tbcContenedor.Tabs[3].Visible = false;
                tbcContenedor.Tabs[4].Visible = true;
                HabilitarValidacionNatural(true);
                HabilitarValidacionJuridica(false);
                HabilitarValidacionPrivada(false);
            }

            else if (optTipoUsuario.SelectedItem.Value == "juridica publica")
            {
                tbcContenedor.Tabs[1].Visible = false;
                tbcContenedor.Tabs[2].Visible = true;
                tbcContenedor.Tabs[3].Visible = false;
                tbcContenedor.Tabs[4].Visible = true;
                pnlRepresentantesPrivada.Visible = false;
                HabilitarValidacionNatural(false);
                HabilitarValidacionJuridica(true);
                HabilitarValidacionPrivada(false);
            }
            else if (optTipoUsuario.SelectedItem.Value == "juridica privada")
            {
                tbcContenedor.Tabs[1].Visible = false;
                tbcContenedor.Tabs[2].Visible = false;
                tbcContenedor.Tabs[3].Visible = true;
                tbcContenedor.Tabs[4].Visible = true;
                pnlRepresentante.Visible = false;
                HabilitarValidacionNatural(false);
                HabilitarValidacionJuridica(false);
                HabilitarValidacionPrivada(true);
            }
            tbcContenedor.ActiveTabIndex = 0;
        }


        /// <summary>
        /// Realiza la generación del PDF
        /// </summary>
        /// <param name="numeroDocumento">string con el numero de documento</param>
        /// <param name="tipoUsuario">string con el tipo de usuario</param>
        private void GenerarRTF(string numeroDocumento, string tipoUsuario)
        {
            string nombreDirectorio = ConfigurationManager.AppSettings["DireccionFus"] + numeroDocumento + "\\";
            string nombreArchivo = numeroDocumento + ".rtf";
            if (!(Directory.Exists(nombreDirectorio)))
            {
                Directory.CreateDirectory(nombreDirectorio);
            }


            List<ImpresionArchivoFus> datos = new List<ImpresionArchivoFus>();

            datos.Add(new ImpresionArchivoFus("DATOS DE USUARIO \n", ""));
            datos.Add(new ImpresionArchivoFus(lblAutoridadAmbiental.Text, cboAutoridadAmbiental.SelectedItem.Text));

            switch (tipoUsuario.ToUpper())
            {
                case "NATURAL":
                    datos.Add(new ImpresionArchivoFus(lblPrimerNombreNatural.Text, txtPrimerNombreNatural.Text));
                    datos.Add(new ImpresionArchivoFus(lblSegundoNombreNatural.Text, txtSegundoNombreNatural.Text));
                    datos.Add(new ImpresionArchivoFus(lblPrimerApellidoNatural.Text, txtPrimerApellidoNatural.Text));
                    datos.Add(new ImpresionArchivoFus(lblSegundoApellidoNatural.Text, txtSegundoApellidoNatural.Text));
                    datos.Add(new ImpresionArchivoFus(lblTipoDocumento.Text, cboTipoDocumento.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblNumeroIdentificacion.Text, txtNumeroIdentificacion.Text));
                    datos.Add(new ImpresionArchivoFus(lblOrigenIdentificacion.Text, cboDepartamentoOrigenNatural.SelectedItem.Text + " - " + cboMunicipioOrigenNatural.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus("DATOS PARA CONTACTO", ""));
                    datos.Add(new ImpresionArchivoFus(lblDireccionNatural.Text, txtDireccionNatural.Text));
                    datos.Add(new ImpresionArchivoFus(lblPaisNatural.Text, cboPaisNatural.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblDepartamentoNatural.Text, cboDepartamentoNatural.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblMunicipioNatural.Text, cboMunicipioNatural.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblCorregimientoNatural.Text, cboCorregimientoNatural.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblVeredaNatural.Text, cboVeredaNatural.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblDireccionCorrespondencia.Text, txtDireccionCorrespondencia.Text));
                    datos.Add(new ImpresionArchivoFus(lblPaisCorrespondencia.Text, cboPaisCorrespondencia.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblDepartamentoCorrespondencia.Text, cboDepartamentoCorrespondencia.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblMunicipioCorrespondencia.Text, cboMunicipioCorrespondencia.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblCorregimientoCorrespondencia.Text, cboCorregimientoCorrespondencia.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblVeredaCorrespondencia.Text, cboVeredaCorrespondencia.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblTelefonoNatural.Text, txtTelefonoNatural.Text));
                    datos.Add(new ImpresionArchivoFus(lblCelularNatural.Text, txtCelularNatural.Text));
                    datos.Add(new ImpresionArchivoFus(lblFaxNatural.Text, txtFaxNatural.Text));
                    datos.Add(new ImpresionArchivoFus(lblCorreoNatural.Text, txtCorreoNatural.Text));
                    break;
                case "JURIDICA PUBLICA":
                    datos.Add(new ImpresionArchivoFus(lblRazonSocialJuridica.Text, txtRazonSocialJuridica.Text));
                    datos.Add(new ImpresionArchivoFus(lblTipoDocumentoJuridica.Text, cboTipoDocumentoJuridica.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblNumeroDocumentoJuridica.Text, txtNumeroDocumentoJuridica.Text));
                    datos.Add(new ImpresionArchivoFus("DATOS PARA CONTACTO", ""));
                    datos.Add(new ImpresionArchivoFus(lblDireccionJuridica.Text, txtDireccionJuridica.Text));
                    datos.Add(new ImpresionArchivoFus(lblPaisJuridica.Text, cboPaisJuridica.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblDepartamentoJuridica.Text, cboDepartamentoJuridica.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblMunicipioJuridica.Text, cboMunicipioJuridica.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblCorregimientoJuridica.Text, cboCorregimientoJuridica.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblVeredaJuridica.Text, cboVeredaJuridica.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblTelefonoJuridica.Text, txtTelefonoJuridica.Text));
                    datos.Add(new ImpresionArchivoFus(lblCelularJuridica.Text, txtCelularJuridica.Text));
                    datos.Add(new ImpresionArchivoFus(lblFaxJuridica.Text, txtFaxJuridica.Text));
                    datos.Add(new ImpresionArchivoFus(lblCorreoJuridica.Text, txtCorreoJuridica.Text));

                    break;
                case "JURIDICA PRIVADA":
                    datos.Add(new ImpresionArchivoFus(lblRazonSocialPrivada.Text, txtRazonSocialPrivada.Text));
                    datos.Add(new ImpresionArchivoFus(lblTipoDocumentoPrivada.Text, cboTipoDocumentoPrivada.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblNumeroDocumentoPrivada.Text, txtNumeroDocumentoPrivada.Text));
                    datos.Add(new ImpresionArchivoFus("DATOS PARA CONTACTO", ""));
                    datos.Add(new ImpresionArchivoFus(lblDireccionPrivada.Text, txtDireccionPrivada.Text));
                    datos.Add(new ImpresionArchivoFus(lblPaisPrivada.Text, cboPaisPrivada.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblDepartamentoPrivada.Text, cboDepartamentoPrivada.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblMunicipioPrivada.Text, cboMunicipioPrivada.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblCorregimientoPrivada.Text, cboCorregimientoPrivada.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lblVeredaPrivada.Text, cboVeredaPrivada.SelectedItem.Text));
                    datos.Add(new ImpresionArchivoFus(lbTelefonoPrivada.Text, txtTelefonoPrivada.Text));
                    datos.Add(new ImpresionArchivoFus(lblCelularPrivada.Text, txtCelularPrivada.Text));
                    datos.Add(new ImpresionArchivoFus(lblFaxPrivada.Text, txtFaxPrivada.Text));
                    datos.Add(new ImpresionArchivoFus(lblCorreoPrivada.Text, txtCorreoPrivada.Text));

                    break;
            }

            // Representantes Legales
            if (grdRepresentantes.Rows.Count > 0)
            {
                datos.Add(new ImpresionArchivoFus("REPRESENTANTES LEGALES", "\n\n"));
            }

            foreach (GridViewRow row in grdRepresentantes.Rows)
            {
                datos.Add(new ImpresionArchivoFus("PRIMER NOMBRE:", row.Cells[2].Text));
                datos.Add(new ImpresionArchivoFus("SEGUNDO NOMBRE:", row.Cells[3].Text));
                datos.Add(new ImpresionArchivoFus("PRIMER APELLIDO:", row.Cells[4].Text));
                datos.Add(new ImpresionArchivoFus("SEGUNDO APELLIDO:", row.Cells[5].Text));
                datos.Add(new ImpresionArchivoFus("TARJETA PROFESIONAL:", row.Cells[6].Text));
                datos.Add(new ImpresionArchivoFus("TIPO DE DOCUMENTO:", row.Cells[8].Text));
                datos.Add(new ImpresionArchivoFus("NUMERO DE DOCUMENTO:", row.Cells[9].Text));
                datos.Add(new ImpresionArchivoFus("DEPARTAMENTO ORIGEN:", row.Cells[11].Text));
                datos.Add(new ImpresionArchivoFus("MUNICIPIO ORIGEN:", row.Cells[13].Text));
                datos.Add(new ImpresionArchivoFus("DIRECCION CORRESPONDENCIA:", row.Cells[14].Text));
                datos.Add(new ImpresionArchivoFus("PAIS:", row.Cells[16].Text));
                datos.Add(new ImpresionArchivoFus("DEPARTAMENTO:", row.Cells[18].Text));
                datos.Add(new ImpresionArchivoFus("MUNICIPIO:", row.Cells[20].Text));
                datos.Add(new ImpresionArchivoFus("VEREDA:", row.Cells[22].Text));
                datos.Add(new ImpresionArchivoFus("CORREGIMIENTO:", row.Cells[24].Text));
                datos.Add(new ImpresionArchivoFus("TELEFONO:", row.Cells[25].Text));
                datos.Add(new ImpresionArchivoFus("CELULAR:", row.Cells[26].Text));
                datos.Add(new ImpresionArchivoFus("FAX:", row.Cells[27].Text));
                datos.Add(new ImpresionArchivoFus("CORREO:", row.Cells[28].Text));
                datos.Add(new ImpresionArchivoFus("\n\n", ""));
            }

            //Apoderados
            if (grdApoderados.Rows.Count > 0)
            {
                datos.Add(new ImpresionArchivoFus("\n\n", "APODERADOS"));
            }
            foreach (GridViewRow row in grdApoderados.Rows)
            {
                datos.Add(new ImpresionArchivoFus("PRIMER NOMBRE:", row.Cells[2].Text));
                datos.Add(new ImpresionArchivoFus("SEGUNDO NOMBRE:", row.Cells[3].Text));
                datos.Add(new ImpresionArchivoFus("PRIMER APELLIDO:", row.Cells[4].Text));
                datos.Add(new ImpresionArchivoFus("SEGUNDO APELLIDO:", row.Cells[5].Text));
                datos.Add(new ImpresionArchivoFus("TARJETA PROFESIONAL:", row.Cells[6].Text));
                datos.Add(new ImpresionArchivoFus("TIPO DE DOCUMENTO:", row.Cells[8].Text));
                datos.Add(new ImpresionArchivoFus("NUMERO DE DOCUMENTO:", row.Cells[9].Text));
                datos.Add(new ImpresionArchivoFus("DEPARTAMENTO ORIGEN:", row.Cells[11].Text));
                datos.Add(new ImpresionArchivoFus("MUNICIPIO ORIGEN:", row.Cells[13].Text));
                datos.Add(new ImpresionArchivoFus("DIRECCION CORRESPONDENCIA:", row.Cells[14].Text));
                datos.Add(new ImpresionArchivoFus("PAIS:", row.Cells[16].Text));
                datos.Add(new ImpresionArchivoFus("DEPARTAMENTO:", row.Cells[18].Text));
                datos.Add(new ImpresionArchivoFus("MUNICIPIO:", row.Cells[20].Text));
                datos.Add(new ImpresionArchivoFus("VEREDA:", row.Cells[22].Text));
                datos.Add(new ImpresionArchivoFus("CORREGIMIENTO:", row.Cells[24].Text));
                datos.Add(new ImpresionArchivoFus("TELEFONO:", row.Cells[25].Text));
                datos.Add(new ImpresionArchivoFus("CELULAR:", row.Cells[26].Text));
                datos.Add(new ImpresionArchivoFus("FAX:", row.Cells[27].Text));
                datos.Add(new ImpresionArchivoFus("CORREO:", row.Cells[28].Text));
                datos.Add(new ImpresionArchivoFus("\n\n", ""));

            }

            // Escribir el Archivo
            using (StreamWriter arc = new StreamWriter(nombreDirectorio + nombreArchivo, false))
            {
                foreach (ImpresionArchivoFus fus in datos)
                {
                    arc.WriteLine(fus.strCampo.ToUpper().ToString() + "\t\t" + fus.strValor);
                }
            }
        }


        /// <summary>
        /// Limpia los datos para persona juridica
        /// </summary>
        private void LimpiarCamposJuridica()
        {
            txtNumeroDocumentoJuridica.Text = string.Empty;
            txtDireccionJuridica.Text = string.Empty;
            txtCorreoJuridica.Text = string.Empty;

            cboTipoDocumentoJuridica.SelectedIndex = 0;
            cboPaisJuridica.SelectedIndex = 0;

            txtTelefonoJuridica.Text = string.Empty;
            txtCelularJuridica.Text = string.Empty;
            txtFaxJuridica.Text = string.Empty;
            txtRazonSocialJuridica.Text = string.Empty;

            cboAutoridadAmbiental.SelectedIndex = 0;
            cboDepartamentoJuridica.SelectedIndex = 0;
            cboMunicipioJuridica.SelectedIndex = 0;
            cboVeredaJuridica.SelectedIndex = 0;
            cboCorregimientoJuridica.SelectedIndex = 0;

            Session["Representantes"] = null;
            Session["Apoderados"] = null;
            //Juridica Privada
            txtNumeroDocumentoPrivada.Text = string.Empty;
            txtCorreoPrivada.Text = string.Empty;

            cboTipoDocumentoPrivada.SelectedIndex = 0;
            cboPaisPrivada.SelectedIndex = 0;

            txtTelefonoPrivada.Text = string.Empty;
            txtCelularPrivada.Text = string.Empty;
            txtFaxPrivada.Text = string.Empty;
            txtRazonSocialPrivada.Text = string.Empty;

            cboDepartamentoPrivada.SelectedIndex = 0;
            cboMunicipioPrivada.SelectedIndex = 0;
            cboVeredaPrivada.SelectedIndex = 0;
            cboCorregimientoPrivada.SelectedIndex = 0;

            this.txtDireccionCorrespondenciaPrivada.Text = string.Empty;
            this.txtDireccionPrivada.Text = string.Empty;

            cboPaisCorrespondenciaPrivada.SelectedIndex = 0;
            cboDepartamentoCorrespondenciaPrivada.SelectedIndex = 0;
            cboMunicipioCorrespondenciaPrivada.SelectedIndex = 0;
            cboVeredaCorrespondenciaPrivada.SelectedIndex = 0;
            cboCorregimientoCorrespondenciaPrivada.SelectedIndex = 0;

            grdApoderados.DataSource = new DataTable();
            grdApoderados.DataBind();
            grdRepresentantes.DataSource = new DataTable();
            grdRepresentantes.DataBind();
            grdRepresentantesPrivada.DataSource = new DataTable();
            grdRepresentantesPrivada.DataBind();
            grdApoderados.Visible = false;
            pnlRepresentante.Visible = false;

            Session["Apoderados"] = null;
        }

        /// <summary>
        /// Limpia los datos para persona natural
        /// </summary>
        private void LimpiarCamposNatural()
        {
            txtNumeroIdentificacion.Text = string.Empty;
            txtNumeroDocumentoPrivada.Text = string.Empty;
            txtPrimerNombreNatural.Text = string.Empty;
            txtSegundoNombreNatural.Text = string.Empty;
            txtPrimerApellidoNatural.Text = string.Empty;
            txtSegundoApellidoNatural.Text = string.Empty;
            txtNumeroIdentificacion.Text = string.Empty;
            txtCorreoNatural.Text = string.Empty;
            cboTipoDocumento.SelectedIndex = 0;
            cboMunicipioOrigenNatural.SelectedIndex = 0;
            cboPaisNatural.SelectedIndex = 0;
            txtTelefonoNatural.Text = string.Empty;
            txtCelularNatural.Text = string.Empty;
            txtFaxNatural.Text = string.Empty;

            cboAutoridadAmbiental.SelectedIndex = 0;
            cboDepartamentoNatural.SelectedIndex = 0;
            cboMunicipioNatural.SelectedIndex = -1;
            cboVeredaNatural.SelectedIndex = 0;
            cboCorregimientoNatural.SelectedIndex = 0;

            /*      Agregados ....
             */
            cboDepartamentoOrigenNatural.SelectedIndex = 0;
            cboMunicipioOrigenNatural.SelectedIndex = 0;
            cboMunicipioCorrespondencia.SelectedIndex = 0;

            txtDireccionNatural.Text = string.Empty;
            cboPaisNatural.SelectedIndex = 0;
            cboDepartamentoNatural.SelectedIndex = 0;

            cboCorregimientoNatural.SelectedIndex = 0;
            cboVeredaNatural.SelectedIndex = 0;

            txtDireccionCorrespondencia.Text = string.Empty;
            cboPaisCorrespondencia.SelectedIndex = 0;
            cboDepartamentoCorrespondencia.SelectedIndex = 0;
            cboMunicipioCorrespondencia.SelectedIndex = 0;
            cboCorregimientoCorrespondencia.SelectedIndex = 0;
            cboVeredaCorrespondencia.SelectedIndex = 0;
            txtTelefonoNatural.Text = string.Empty;
            txtCelularNatural.Text = string.Empty;
            txtFaxNatural.Text = string.Empty;
            txtCorreoNatural.Text = string.Empty;

            txtFaxNatural.Text = string.Empty;
            txtCorreoNatural.Text = string.Empty;

            Session["Apoderados"] = null;

            grdApoderados.DataSource = new DataTable();
            grdApoderados.DataBind();
            grdRepresentantes.DataSource = new DataTable();
            grdRepresentantes.DataBind();
            grdApoderados.Visible = false;
            pnlRepresentante.Visible = false;

        }

        /// <summary>
        /// Limpia la información de los usuarios
        /// </summary>
        private void LimpiarDatosUsuario()
        {
            // Seleccione..
            cboAutoridadAmbiental.SelectedIndex = 0;
            // Persona natural..
            optTipoUsuario.SelectedIndex = 0;
        }


        /// <summary>
        /// Carga la información de los apoderados
        /// </summary>
        /// <param name="dtAsociadosDataBase">DataTable con la información de los apoderados</param>
        private void CargarApoderados(DataTable dtAsociadosDataBase)
        {

            CrearVariableApoderados();
            DataTable _apoderadosTemp = (DataTable)Session["Apoderados"];
            DataRow _filaTemp;

            foreach (DataRow _filaData in dtAsociadosDataBase.Rows)
            {
                _filaTemp = _apoderadosTemp.NewRow();
                _filaTemp["PER_ID"] = _filaData["PER_ID"];
                _filaTemp["PRIMER_NOMBRE"] = _filaData["PER_PRIMER_NOMBRE"];
                _filaTemp["SEGUNDO_NOMBRE"] = _filaData["PER_SEGUNDO_NOMBRE"];
                _filaTemp["PRIMER_APELLIDO"] = _filaData["PER_PRIMER_APELLIDO"];
                _filaTemp["SEGUNDO_APELLIDO"] = _filaData["PER_SEGUNDO_APELLIDO"];
                _filaTemp["NO_DOC_ACREDITACION"] = _filaData["NO_DOCUMENTO_ACREDITACION"];
                _filaTemp["TIP_DOC_ACREDITACION"] = _filaData["ID_TIP_DOC_ACREDITACION"];
                _filaTemp["NOM_DOC_ACREDITACION"] = _filaData["NOMBRE_TIP_DOC_ACREDITACION"];
                _filaTemp["ID_TIPO_ID"] = _filaData["TID_ID"];
                _filaTemp["ID_IDENTIFICACION"] = _filaData["PER_NUMERO_IDENTIFICACION"];
                _filaTemp["ID_ORIGEN_DEPARTAMENTO"] = _filaData["PER_IDDEPTO_EXP_DOC"];
                _filaTemp["ORIGEN_DEPARTAMENTO"] = _filaData["PER_DEPTO_EXP_DOC"];      //JMM 08-07-2010
                _filaTemp["ID_ORIGEN_MUNICIPIO"] = _filaData["PER_IDMUN_EXP_DOC"];
                _filaTemp["ORIGEN_MUNICIPIO"] = _filaData["PER_MUN_EXP_DOC"];         //JMM 08-07-2010

                _filaTemp["DIRECCION_CORRESPONDENCIA"] = _filaData["DIP_DIRECCION"];
                _filaTemp["ID_PAIS"] = _filaData["PAI_ID"];
                _filaTemp["PAIS"] = _filaData["PAI_NOMBRE"];
                _filaTemp["ID_DEPARTAMENTO"] = _filaData["DIR_IDDEPTO"];
                _filaTemp["DEPARTAMENTO"] = _filaData["DIR_DEPTO"];
                _filaTemp["ID_MUNICIPIO"] = _filaData["DIR_IDMUN"];
                _filaTemp["MUNICIPIO"] = _filaData["DIR_MUN"];
                _filaTemp["ID_VEREDA"] = _filaData["VER_ID"];
                _filaTemp["VEREDA"] = _filaData["VER_NOMBRE"];
                _filaTemp["ID_CORREGIMIENTO"] = _filaData["COR_ID"];
                _filaTemp["CORREGIMIENTO"] = _filaData["COR_NOMBRE"];
                _filaTemp["TELEFONO"] = _filaData["PER_TELEFONO"];
                _filaTemp["CELULAR"] = _filaData["PER_CELULAR"];
                _filaTemp["FAX"] = _filaData["PER_FAX"];
                _filaTemp["CORREO"] = _filaData["PER_CORREO_ELECTRONICO"];
                _filaTemp["ESTADO"] = _filaData["PER_ACTIVO"];

                _apoderadosTemp.Rows.Add(_filaTemp);
                _apoderadosTemp.AcceptChanges();
            }

            Session["Apoderados"] = _apoderadosTemp;
        }

        /// <summary>
        /// Funcion que crea la variable de sesión "Session["Apoderados"]"
        /// </summary>
        private void CrearVariableApoderados()
        {
            if (Session["Apoderados"] == null)
            {
                DataTable _tabla = new DataTable();
                ColumnasTablaApoderados(ref _tabla);
                Session.Add("Apoderados", _tabla);
            }
        }

        /// <summary>
        /// Funcion que crea la variable de sesión "Session["Representantes"]"
        /// </summary>
        private void CrearVariableRepresentantes()
        {
            if (Session["Representantes"] == null)
            {
                DataTable _tabla = new DataTable();
                ColumnasTablaRepresentantes(ref _tabla);
                Session.Add("Representantes", _tabla);
            }
        }

        /// <summary>
        /// Función que define la estructura de la tabla que se almacena en la variable de sesion "Session["Apoderados"]"
        /// </summary>
        /// <param name="tabla"></param>
        private void ColumnasTablaApoderados(ref DataTable _tabla)
        {
            _tabla.Columns.Add("PER_ID", Type.GetType("System.String"));
            _tabla.Columns.Add("PRIMER_NOMBRE", Type.GetType("System.String"));
            _tabla.Columns.Add("SEGUNDO_NOMBRE", Type.GetType("System.String"));
            _tabla.Columns.Add("PRIMER_APELLIDO", Type.GetType("System.String"));
            _tabla.Columns.Add("SEGUNDO_APELLIDO", Type.GetType("System.String"));
            _tabla.Columns.Add("TIP_DOC_ACREDITACION", Type.GetType("System.String"));
            _tabla.Columns.Add("NOM_DOC_ACREDITACION", Type.GetType("System.String"));
            _tabla.Columns.Add("NO_DOC_ACREDITACION", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_TIPO_ID", Type.GetType("System.String"));
            _tabla.Columns.Add("TIPO_ID", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_IDENTIFICACION", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_ORIGEN_DEPARTAMENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("ORIGEN_DEPARTAMENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_ORIGEN_MUNICIPIO", Type.GetType("System.String"));
            _tabla.Columns.Add("ORIGEN_MUNICIPIO", Type.GetType("System.String"));
            _tabla.Columns.Add("DIRECCION_CORRESPONDENCIA", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_PAIS", Type.GetType("System.String"));
            _tabla.Columns.Add("PAIS", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_DEPARTAMENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("DEPARTAMENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_MUNICIPIO", Type.GetType("System.String"));
            _tabla.Columns.Add("MUNICIPIO", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_VEREDA", Type.GetType("System.String"));
            _tabla.Columns.Add("VEREDA", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_CORREGIMIENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("CORREGIMIENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("TELEFONO", Type.GetType("System.String"));
            _tabla.Columns.Add("CELULAR", Type.GetType("System.String"));
            _tabla.Columns.Add("FAX", Type.GetType("System.String"));
            _tabla.Columns.Add("CORREO", Type.GetType("System.String"));
            _tabla.Columns.Add("ESTADO", Type.GetType("System.Boolean"));
            _tabla.AcceptChanges();
        }

        /// <summary>
        /// Función que define la estructura de la tabla que se almacena en la variable de sesion "Session["Representantes"]"
        /// </summary>
        /// <param name="tabla"></param>
        private void ColumnasTablaRepresentantes(ref DataTable _tabla)
        {
            _tabla.Columns.Add("PER_ID", Type.GetType("System.String"));
            _tabla.Columns.Add("PRIMER_NOMBRE", Type.GetType("System.String"));
            _tabla.Columns.Add("SEGUNDO_NOMBRE", Type.GetType("System.String"));
            _tabla.Columns.Add("PRIMER_APELLIDO", Type.GetType("System.String"));
            _tabla.Columns.Add("SEGUNDO_APELLIDO", Type.GetType("System.String"));
            _tabla.Columns.Add("TARJETA_PROFESIONAL", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_TIPO_ID", Type.GetType("System.String"));
            _tabla.Columns.Add("TIPO_ID", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_IDENTIFICACION", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_ORIGEN_DEPARTAMENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("ORIGEN_DEPARTAMENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_ORIGEN_MUNICIPIO", Type.GetType("System.String"));
            _tabla.Columns.Add("ORIGEN_MUNICIPIO", Type.GetType("System.String"));
            _tabla.Columns.Add("DIRECCION_CORRESPONDENCIA", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_PAIS", Type.GetType("System.String"));
            _tabla.Columns.Add("PAIS", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_DEPARTAMENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("DEPARTAMENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_MUNICIPIO", Type.GetType("System.String"));
            _tabla.Columns.Add("MUNICIPIO", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_VEREDA", Type.GetType("System.String"));
            _tabla.Columns.Add("VEREDA", Type.GetType("System.String"));
            _tabla.Columns.Add("ID_CORREGIMIENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("CORREGIMIENTO", Type.GetType("System.String"));
            _tabla.Columns.Add("TELEFONO", Type.GetType("System.String"));
            _tabla.Columns.Add("CELULAR", Type.GetType("System.String"));
            _tabla.Columns.Add("FAX", Type.GetType("System.String"));
            _tabla.Columns.Add("CORREO", Type.GetType("System.String"));
            _tabla.Columns.Add("ESTADO", Type.GetType("System.Boolean"));
            _tabla.AcceptChanges();
        }

        /// <summary>
        /// Inicializa los controles de la pagina
        /// </summary>
        private void InicializarControles()
        {

            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
            objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
            string MsjAutCorreo = objParametrizacion.ObtenerValorParametroGeneral(-1, "Msj_Aut_Envio_Notificacion_Email");
            LblAutorizaNotifPerNatural.Text = MsjAutCorreo;
            LblAutorizaNotifPerJuridica.Text = MsjAutCorreo;
            LblAutorizaNotifPerPrivada.Text = MsjAutCorreo;

            tbcContenedor.Tabs[2].Visible = false; //juridica
            tbcContenedor.Tabs[3].Visible = false; //privada
            pnlRepresentante.Visible = false;
            pnlApoderado.Visible = false;
            pnlRepresentantesPrivada.Visible = false;
            tbcContenedor.ActiveTabIndex = 0;

            //dropdowns
            cboCorregimientoNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCorregimientoCorrespondenciaPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboVeredaCorrespondenciaPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            //Deshabilitar controles de validacion en Persona Juridica
            HabilitarValidacionJuridica(false);
            //Deshabilitar controles de validacion en Persona Juridica Privada
            HabilitarValidacionPrivada(false);
        }

        /// <summary>
        /// Funcion que carga el listado de Autoridades Ambientales
        /// </summary>
        private void CargarAutoridades()
        {
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            cboAutoridadAmbiental.DataSource = _listaAutoridades.ListarAutoridadesActivas();
            cboAutoridadAmbiental.DataValueField = "AUT_ID";
            cboAutoridadAmbiental.DataTextField = "AUT_NOMBRE";
            cboAutoridadAmbiental.DataBind();
            cboAutoridadAmbiental.Items.Insert(0, new ListItem("Seleccione....", "-1"));
        }

        /// <summary>
        /// Funcion que carga los tipos de identificacion
        /// </summary>
        private void CargarTiposIdentificacion()
        {
            SILPA.LogicaNegocio.Generico.Listas _listaTiposId = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _temp = _listaTiposId.ListaTipoIdentificacionXTipoPersona();

            cboTipoDocumento.DataSource = ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));
            cboTipoDocumento.DataValueField = "TID_ID";
            cboTipoDocumento.DataTextField = "TID_NOMBRE";
            cboTipoDocumento.DataBind();
            cboTipoDocumento.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            cboTipoDocumentoJuridica.DataSource = ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.JuridicaPublica));
            cboTipoDocumentoJuridica.DataValueField = "TID_ID";
            cboTipoDocumentoJuridica.DataTextField = "TID_NOMBRE";
            cboTipoDocumentoJuridica.DataBind();
            cboTipoDocumentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));

            cboTipoDocumentoPrivada.DataSource = ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.JuridicaPrivada));
            cboTipoDocumentoPrivada.DataValueField = "TID_ID";
            cboTipoDocumentoPrivada.DataTextField = "TID_NOMBRE";
            cboTipoDocumentoPrivada.DataBind();
            cboTipoDocumentoPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        }

        /// <summary>
        /// Funcion que carga el listado de paises en los combos de los datos de persona natural
        /// </summary>
        private void CargarPaises()
        {
            CargarPais(cboPaisNatural);
            CargarPais(cboPaisCorrespondencia);
            CargarPais(cboPaisCorrespondenciaPrivada);
            CargarPais(cboPaisJuridica);
            CargarPais(cboPaisPrivada);
        }

        private void CargarPais(DropDownList cboPais)
        {
            SILPA.LogicaNegocio.Generico.Listas _listaPaises = new SILPA.LogicaNegocio.Generico.Listas();
            DataSet _temp = _listaPaises.ListarPaises(null);
            cboPais.DataSource = _temp;
            cboPais.DataValueField = "PAI_ID";
            cboPais.DataTextField = "PAI_NOMBRE";
            cboPais.DataBind();
            cboPais.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            cboPais.SelectedValue = _configuracion.IdPaisPredeterminado.ToString();
        }

        /// <summary>
        /// Funcion que carga el combo de Municipios y selecciona uno segun los datos de la persona Natural.
        /// </summary>
        /// <param name="_indiceDepartamento"></param>
        /// <param name="_indiceMunicipio"></param>

        private void CargarComboMunicipios(string _indiceDepartamento, DropDownList _combo)
        {
            if (_indiceDepartamento != null)
            {
                SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaMunicipios.ListaMunicipios(null, int.Parse(_indiceDepartamento), null);
                _combo.DataSource = _temp;
                _combo.DataValueField = "MUN_ID";
                _combo.DataTextField = "MUN_NOMBRE";
                _combo.DataBind();
                _combo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
        }

        /// <summary>
        /// JMM - 08-07-2010
        /// Consulta los corregimientos
        /// </summary>
        /// <param name="_indiceMunicipio"></param>
        /// <param name="_combo"></param>
        private void CargarComboCorregimientos(string _indiceMunicipio, DropDownList _combo)
        {
            if (_indiceMunicipio != null)
            {
                SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaCorregimientos.ListarCorregimientos(int.Parse(_indiceMunicipio), null);
                _combo.DataSource = _temp;
                _combo.DataValueField = "COR_ID";
                _combo.DataTextField = "COR_NOMBRE";
                _combo.DataBind();
                _combo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
        }

        /// <summary>
        /// JMM - 07-08-2010
        /// Consulta las veredas
        /// </summary>
        /// <param name="_indiceCorregimiento"></param>
        /// <param name="_combo"></param>
        private void CargarComboVeredas(string _indiceCorregimiento, DropDownList _combo)
        {
            if (_indiceCorregimiento != null)
            {
                SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
                DataSet _temp = _listaVeredas.ListarVeredas(null, int.Parse(_indiceCorregimiento), null);
                _combo.DataSource = _temp;
                _combo.DataValueField = "VER_ID";
                _combo.DataTextField = "VER_NOMBRE";
                _combo.DataBind();
                _combo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            }
        }

        /// <summary>
        /// Carga los combos para seleccionar el lugar de expedicion del Documento en Persona Natural
        /// </summary>
        private void CargarCombos()
        {
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();

            DataSet _temp1 = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
            cboDepartamentoOrigenNatural.DataSource = _temp1;
            cboDepartamentoOrigenNatural.DataValueField = "DEP_ID";
            cboDepartamentoOrigenNatural.DataTextField = "DEP_NOMBRE";
            cboDepartamentoOrigenNatural.DataBind();
            cboDepartamentoOrigenNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            CargarComboMunicipios(cboDepartamentoOrigenNatural.SelectedValue, cboMunicipioOrigenNatural);

            cboDepartamentoNatural.DataSource = _temp1;
            cboDepartamentoNatural.DataValueField = "DEP_ID";
            cboDepartamentoNatural.DataTextField = "DEP_NOMBRE";
            cboDepartamentoNatural.DataBind();
            cboDepartamentoNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            CargarComboMunicipios(cboDepartamentoNatural.SelectedValue, cboMunicipioNatural);

            cboDepartamentoCorrespondencia.DataSource = _temp1;
            cboDepartamentoCorrespondencia.DataValueField = "DEP_ID";
            cboDepartamentoCorrespondencia.DataTextField = "DEP_NOMBRE";
            cboDepartamentoCorrespondencia.DataBind();
            cboDepartamentoCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            CargarComboMunicipios(cboDepartamentoCorrespondencia.SelectedValue, cboMunicipioCorrespondencia);

            cboDepartamentoCorrespondenciaPrivada.DataSource = _temp1;
            cboDepartamentoCorrespondenciaPrivada.DataValueField = "DEP_ID";
            cboDepartamentoCorrespondenciaPrivada.DataTextField = "DEP_NOMBRE";
            cboDepartamentoCorrespondenciaPrivada.DataBind();
            cboDepartamentoCorrespondenciaPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            CargarComboMunicipios(cboDepartamentoCorrespondenciaPrivada.SelectedValue, cboMunicipioCorrespondenciaPrivada);

            cboDepartamentoJuridica.DataSource = _temp1;
            cboDepartamentoJuridica.DataValueField = "DEP_ID";
            cboDepartamentoJuridica.DataTextField = "DEP_NOMBRE";
            cboDepartamentoJuridica.DataBind();
            cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            CargarComboMunicipios(cboDepartamentoJuridica.SelectedValue, cboMunicipioJuridica);

            cboDepartamentoPrivada.DataSource = _temp1;
            cboDepartamentoPrivada.DataValueField = "DEP_ID";
            cboDepartamentoPrivada.DataTextField = "DEP_NOMBRE";
            cboDepartamentoPrivada.DataBind();
            cboDepartamentoPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            CargarComboMunicipios(cboDepartamentoPrivada.SelectedValue, cboMunicipioPrivada);
        }

        /// <summary>
        /// Habilita o deshabilita los controles de validacion del panel de persona natural
        /// </summary>
        /// <param name="habilitar">Verdadero o falso para la habilitación</param>
        private void HabilitarValidacionNatural(bool habilitar)
        {
            rfvPrimerNombreNatural.Enabled = habilitar;
            revPrimerNombreNatural.Enabled = habilitar;
            revSegundoNombreNatural.Enabled = habilitar;
            rfvPrimerApellidoNatural.Enabled = habilitar;
            revPrimerApellidoNatural.Enabled = habilitar;
            revSegundoApellidoNatural.Enabled = habilitar;
            rfvNumeroIdentificacionNatural.Enabled = habilitar;
            revNumeroIdentificacionNatural.Enabled = habilitar;
            rfvDireccionNatural.Enabled = habilitar;
            rfvDireccionCorrespondencia.Enabled = habilitar;
            revCorreoNatural.Enabled = habilitar;
            rfvCorreoNatural.Enabled = habilitar;
            covTipoDocumento.Enabled = habilitar;
            covPaisNatural.Enabled = habilitar;
            covPaisCorrespondencia.Enabled = habilitar;
        }

        /// <summary>
        /// Habilita o deshabilita los controles de validacion del panel de persona juridica
        /// </summary>
        /// <param name="habilitar">Verdadero o falso para la habilitación</param>
        private void HabilitarValidacionJuridica(bool habilitar)
        {
            rfvRazonSocialJuridica.Enabled = habilitar;
            rfvNumeroDocumentoJuridica.Enabled = habilitar;
            revNumeroDocumentoJuridica.Enabled = habilitar;
            rfvDireccionJuridica.Enabled = habilitar;
            revCorreoJuridica.Enabled = habilitar;
            rfvCorreoJuridica.Enabled = habilitar;
            covTipoDocumentoJuridica.Enabled = habilitar;
            covPaisJuridica.Enabled = habilitar;
        }

        /// <summary>
        /// Habilita o deshabilita los controles de validacion del panel de persona privada
        /// </summary>
        /// <param name="habilitar">Verdadero o falso para la habilitación</param>
        private void HabilitarValidacionPrivada(bool habilitar)
        {
            rfvRazonSocialPrivada.Enabled = habilitar;
            rfvNumeroDocumentoPrivada.Enabled = habilitar;
            revNumeroDocumentoPrivada.Enabled = habilitar;
            rfvDireccionPrivada.Enabled = habilitar;
            revCorreoPrivada.Enabled = habilitar;
            rfvCorreoPrivada.Enabled = habilitar;
            covTipoDocumentoPrivada.Enabled = habilitar;
            covPaisPrivada.Enabled = habilitar;
            cboDepartamentoCorrespondenciaPrivada.Enabled = habilitar;
            cboMunicipioCorrespondenciaPrivada.Enabled = habilitar;
            cboVeredaCorrespondenciaPrivada.Enabled = habilitar;
            cboCorregimientoCorrespondenciaPrivada.Enabled = habilitar;
            CompareValidator1.Enabled = habilitar;
            RequiredFieldValidator1.Enabled = habilitar;
        }

        /// <summary>
        /// Función que retorna el indice del valor de la opcion que se encuentra en el combo especificado
        /// </summary>
        /// <param name="_combo"></param>
        /// <param name="_valor"></param>
        /// <returns></returns>
        private int retornarIndice(DropDownList _combo, string _valor)
        {
            int index = 0;
            foreach (ListItem _item in _combo.Items)
            {
                _item.Selected = false;

                if (_item.Value == _valor)
                {
                    _item.Selected = true;
                    _combo.SelectedIndex = index;
                    return index;
                    //return _combo.SelectedIndex;
                }
                index++;
            }
            return -1;
        }


        /// <summary>
        /// JMM - 09-7-2010
        /// Metodo que carga la informacion de los apoderador en la grilla
        /// </summary>
        private void CargarApoderados()
        {
            if ((DataTable)Session["Apoderados"] != null &&
                ((DataTable)Session["Apoderados"]).Rows.Count != 0)
            {
                grdApoderados.DataSource = (DataTable)Session["Apoderados"];
                grdApoderados.DataBind();
                pnlApoderado.Visible = true;
            }
        }


        /// <summary>
        /// JMM - 13/07/2010
        /// Metodo para recargar la grila con la informacion de los representantes.
        /// </summary>
        private void CargarRepresentantes()
        {
            if ((DataTable)Session["Representantes"] != null &&
            ((DataTable)Session["Representantes"]).Rows.Count != 0)
            {
                grdRepresentantes.DataSource = (DataTable)Session["Representantes"];
                grdRepresentantes.DataBind();
                pnlRepresentante.Visible = true;
                grdRepresentantesPrivada.DataSource = (DataTable)Session["Representantes"];
                grdRepresentantesPrivada.DataBind();
                pnlRepresentantesPrivada.Visible = true;
            }
        }

        /// <summary>
        /// JMM - 13/07/2010
        /// Metodo para la insercion de datos del apoderado para las perdonas Natutal, Juridica Publica y Juridica Privada.
        /// </summary>
        /// <param name="_idPersona"></param>
        private void InsertarApoderados(Int64 _idPersona)
        {
            SILPA.LogicaNegocio.Generico.Persona _persona = new SILPA.LogicaNegocio.Generico.Persona();
            if ((DataTable)Session["Apoderados"] != null &&
                    ((DataTable)Session["Apoderados"]).Rows.Count != 0)
            {
                SILPA.LogicaNegocio.Generico.Persona _apoderado = new SILPA.LogicaNegocio.Generico.Persona();
                DataTable _apoderados = new DataTable();
                _apoderados = (DataTable)Session["Apoderados"];

                foreach (DataRow _registro in _apoderados.Rows)
                {
                    int veredaID = -1;
                    int corregimientoID = -1;

                    //int.TryParse(_registro["ID_VEREDA"].ToString(), out veredaID);
                    //int.TryParse(_registro["ID_CORREGIMIENTO"].ToString(), out corregimientoID);
                    int _idMun = -1;
                    int _idDepto = -1;
                    Int64 _idApoderado = 0;

                    if (_registro["ID_VEREDA"].ToString() != string.Empty && _registro["ID_VEREDA"] != DBNull.Value)
                    {
                        veredaID = int.Parse(_registro["ID_VEREDA"].ToString());
                    }

                    if (_registro["ID_CORREGIMIENTO"].ToString() != string.Empty && _registro["ID_CORREGIMIENTO"] != DBNull.Value)
                    {
                        corregimientoID = int.Parse(_registro["ID_CORREGIMIENTO"].ToString());
                    }

                    if (_registro["ID_DEPARTAMENTO"].ToString() != string.Empty && _registro["ID_DEPARTAMENTO"] != DBNull.Value)
                    {
                        _idDepto = int.Parse(_registro["ID_DEPARTAMENTO"].ToString());
                    }

                    if (_registro["ID_MUNICIPIO"].ToString() != string.Empty && _registro["ID_MUNICIPIO"] != DBNull.Value)
                    {
                        _idMun = int.Parse(_registro["ID_MUNICIPIO"].ToString());
                    }

                    _idApoderado = _apoderado.InsertarPersona(_registro["ID_IDENTIFICACION"].ToString(),
                            "", "", _registro["PRIMER_NOMBRE"].ToString(),
                            _registro["SEGUNDO_NOMBRE"].ToString(),
                            _registro["PRIMER_APELLIDO"].ToString(),
                            _registro["SEGUNDO_APELLIDO"].ToString(), 1,
                            "Apoderado", _registro["ID_IDENTIFICACION"].ToString(),
                            _registro["CORREO"].ToString(), int.Parse(_registro["ID_TIPO_ID"].ToString()),
                            _registro["ID_ORIGEN_MUNICIPIO"].ToString(), int.Parse(_registro["ID_PAIS"].ToString()),
                            _registro["TELEFONO"].ToString(), _registro["CELULAR"].ToString(),
                            _registro["FAX"].ToString(), "", (int)TipoPersona.Apoderado, _registro["NO_DOC_ACREDITACION"].ToString(),
                            _idPersona, int.Parse(cboAutoridadAmbiental.SelectedValue),
                            "", "", 99999, "True", "True", "NoImage.gif", "",
                            _idDepto, _idMun, veredaID, corregimientoID, (int)TipoSolicitante.Apoderado, false);

                    _persona.InsertarDireccionApoderado(_idApoderado, int.Parse(_registro["ID_PAIS"].ToString()),
                                                _idDepto,
                                                _idMun,
                                                corregimientoID,
                                                veredaID,
                                                (int)SILPA.Comun.TipoDireccion.Correspondencia,
                                                _registro["DIRECCION_CORRESPONDENCIA"].ToString()
                                                );
                    _persona.InsertarTipoDocumentoAcreditacion(_idApoderado, long.Parse(_registro["TIP_DOC_ACREDITACION"].ToString()));
                }
            }
        }

        /// <summary>
        /// JMM - 13/07/2010
        /// Metodo para la actualizacion de datos del apoderado
        /// </summary>
        /// <param name="IdPersona"></param>
        /// <param name="IdAutoridad"></param>
        private void ActualizarApoderado(int IdPersona, int IdAutoridad)
        {
            SILPA.LogicaNegocio.Generico.Persona _personaActualizar = new SILPA.LogicaNegocio.Generico.Persona();

            //Actualizacion de Apoderados --> JMM -2010-07-12
            if ((DataTable)Session["Apoderados"] != null &&
                ((DataTable)Session["Apoderados"]).Rows.Count != 0)
            {
                Int32 iIdCorregimiento;
                Int32 iIdVereda;
                Int32 iIdMunicipio;
                long idTipoAcreditacion = -1;

                Persona _apoderado = new Persona();
                DataTable _apoderados = new DataTable();
                _apoderados = (DataTable)Session["Apoderados"];

                foreach (DataRow _registro in _apoderados.Rows)
                {
                    if (_registro["ID_CORREGIMIENTO"].ToString() == "")
                        iIdCorregimiento = -1;
                    else
                        iIdCorregimiento = Convert.ToInt32(_registro["ID_CORREGIMIENTO"].ToString());

                    if (_registro["ID_VEREDA"].ToString() == "")
                        iIdVereda = -1;
                    else
                        iIdVereda = Convert.ToInt32(_registro["ID_VEREDA"].ToString());

                    if (_registro["ID_MUNICIPIO"].ToString() == "")
                        iIdMunicipio = -1;
                    else
                        iIdMunicipio = Convert.ToInt32(_registro["ID_MUNICIPIO"].ToString());

                    _apoderado.ActualizaApoderadoRepresentante(
                        _registro["PRIMER_NOMBRE"].ToString(),
                        _registro["SEGUNDO_NOMBRE"].ToString(),
                        _registro["PRIMER_APELLIDO"].ToString(),
                        _registro["SEGUNDO_APELLIDO"].ToString(),
                        _registro["ID_IDENTIFICACION"].ToString(),
                        Convert.ToInt32(_registro["ID_TIPO_ID"].ToString()),
                        _registro["ID_ORIGEN_MUNICIPIO"].ToString(),
                        _registro["TELEFONO"].ToString(),
                        _registro["CELULAR"].ToString(),
                        _registro["FAX"].ToString(),
                        _registro["CORREO"].ToString(),
                        0,
                        (int)TipoPersona.Apoderado,
                        _registro["NO_DOC_ACREDITACION"].ToString(),
                        //(int)_personaActualizar.Identity.PersonaId,
                        //(int)_personaActualizar.Identity.IdAutoridadAmbiental,
                        IdPersona,
                        IdAutoridad,
                        false,
                        (int)TipoSolicitante.Apoderado,
                        _registro["DIRECCION_CORRESPONDENCIA"].ToString(),
                        Convert.ToInt32(_registro["ID_PAIS"].ToString()),
                        iIdMunicipio,
                        iIdVereda,
                        iIdCorregimiento,
                        (int)SILPA.Comun.TipoDireccion.Correspondencia,
                        Convert.ToBoolean(_registro["ESTADO"]));
                    if (_registro["TIP_DOC_ACREDITACION"].ToString() != string.Empty)
                        idTipoAcreditacion = long.Parse(_registro["TIP_DOC_ACREDITACION"].ToString());
                    _apoderado.ActualizaApoderadoAcreditacion(IdPersona, _registro["ID_IDENTIFICACION"].ToString(), idTipoAcreditacion);
                }

            }
        }

        /// <summary>
        /// JMM - 13/07/2010
        /// Metodo para insertar los registros de los representantes legales
        /// </summary>
        /// <param name="_idPersona"></param>
        private void InsertarRepresentantes(Int64 _idPersona)
        {
            if ((DataTable)Session["Representantes"] != null &&
                ((DataTable)Session["Representantes"]).Rows.Count != 0)
            {
                SILPA.LogicaNegocio.Generico.Persona _representante = new SILPA.LogicaNegocio.Generico.Persona();
                DataTable _representantes = new DataTable();
                _representantes = (DataTable)Session["Representantes"];

                foreach (DataRow _registro in _representantes.Rows)
                {
                    int veredaID = -1;
                    int corregimientoID = -1;
                    
                    int _idMun = -1;
                    int _idDepto = -1;
                    Int64 _idRepresentante = 0;

                    if (_registro["ID_VEREDA"].ToString() != string.Empty && _registro["ID_VEREDA"] != DBNull.Value)
                    {
                        veredaID = int.Parse(_registro["ID_VEREDA"].ToString());
                    }

                    if (_registro["ID_CORREGIMIENTO"].ToString() != string.Empty && _registro["ID_CORREGIMIENTO"] != DBNull.Value)
                    {
                        corregimientoID = int.Parse(_registro["ID_CORREGIMIENTO"].ToString());
                    }

                    if (_registro["ID_DEPARTAMENTO"].ToString() != string.Empty && _registro["ID_DEPARTAMENTO"] != DBNull.Value)
                    {
                        _idDepto = int.Parse(_registro["ID_DEPARTAMENTO"].ToString());
                    }

                    if (_registro["ID_MUNICIPIO"].ToString() != string.Empty && _registro["ID_MUNICIPIO"] != DBNull.Value)
                    {
                        _idMun = int.Parse(_registro["ID_MUNICIPIO"].ToString());
                    }

                    _idRepresentante = _representante.InsertarPersona(
                        _registro["ID_IDENTIFICACION"].ToString(),
                        "", "", _registro["PRIMER_NOMBRE"].ToString(),
                        _registro["SEGUNDO_NOMBRE"].ToString(),
                        _registro["PRIMER_APELLIDO"].ToString(),
                        _registro["SEGUNDO_APELLIDO"].ToString(), 1,
                        "Representante Legal", _registro["ID_IDENTIFICACION"].ToString(),
                        _registro["CORREO"].ToString(), int.Parse(_registro["ID_TIPO_ID"].ToString()),
                        _registro["ID_ORIGEN_MUNICIPIO"].ToString(), int.Parse(_registro["ID_PAIS"].ToString()),
                        _registro["TELEFONO"].ToString(), _registro["CELULAR"].ToString(),
                        _registro["FAX"].ToString(), "", (int)TipoPersona.RepresentanteLegal, _registro["TARJETA_PROFESIONAL"].ToString(),
                        _idPersona, int.Parse(cboAutoridadAmbiental.SelectedValue),
                        "", "", 99999, "True", "True", "NoImage.gif", "",
                        _idDepto, _idMun, veredaID, corregimientoID, (int)TipoSolicitante.RepresentanteLegal, false); //jmartinez Adicion campo aurtoriza Correo

                    _representante.InsertarDireccionApoderado(_idRepresentante, int.Parse(_registro["ID_PAIS"].ToString()),
                                _idDepto,
                                _idMun,
                                corregimientoID,
                                veredaID,
                                (int)SILPA.Comun.TipoDireccion.Correspondencia,
                                _registro["DIRECCION_CORRESPONDENCIA"].ToString()
                                );
                }
            }
        }

        /// <summary>
        /// JMM - 13/07/2010
        /// Metodo para cargar la grilla de representantes legales por primera vez de acuerdo a la consulta
        /// </summary>
        /// <param name="dtRepresentantesDataBase"></param>
        private void CargarRepresentantes(DataTable dtRepresentantesDataBase)
        {
            CrearVariableRepresentantes();
            DataTable _representantesTemp = (DataTable)Session["Representantes"];
            DataRow _filaTemp;

            foreach (DataRow _filaData in dtRepresentantesDataBase.Rows)
            {
                _filaTemp = _representantesTemp.NewRow();
                _filaTemp["PER_ID"] = _filaData["PER_ID"];
                _filaTemp["PRIMER_NOMBRE"] = _filaData["PER_PRIMER_NOMBRE"];
                _filaTemp["SEGUNDO_NOMBRE"] = _filaData["PER_SEGUNDO_NOMBRE"];
                _filaTemp["PRIMER_APELLIDO"] = _filaData["PER_PRIMER_APELLIDO"];
                _filaTemp["SEGUNDO_APELLIDO"] = _filaData["PER_SEGUNDO_APELLIDO"];
                _filaTemp["TARJETA_PROFESIONAL"] = _filaData["PER_TARJETA_PROFESIONAL"];
                _filaTemp["ID_TIPO_ID"] = _filaData["TID_ID"];
                _filaTemp["ID_IDENTIFICACION"] = _filaData["PER_NUMERO_IDENTIFICACION"];
                _filaTemp["ID_ORIGEN_DEPARTAMENTO"] = _filaData["PER_IDDEPTO_EXP_DOC"];
                _filaTemp["ORIGEN_DEPARTAMENTO"] = _filaData["PER_DEPTO_EXP_DOC"];      //JMM 08-07-2010
                _filaTemp["ID_ORIGEN_MUNICIPIO"] = _filaData["PER_IDMUN_EXP_DOC"];
                _filaTemp["ORIGEN_MUNICIPIO"] = _filaData["PER_MUN_EXP_DOC"];         //JMM 08-07-2010

                _filaTemp["DIRECCION_CORRESPONDENCIA"] = _filaData["DIP_DIRECCION"];
                _filaTemp["ID_PAIS"] = _filaData["PAI_ID"];
                _filaTemp["PAIS"] = _filaData["PAI_NOMBRE"];
                _filaTemp["ID_DEPARTAMENTO"] = _filaData["DIR_IDDEPTO"];
                _filaTemp["DEPARTAMENTO"] = _filaData["DIR_DEPTO"];
                _filaTemp["ID_MUNICIPIO"] = _filaData["DIR_IDMUN"];
                _filaTemp["MUNICIPIO"] = _filaData["DIR_MUN"];
                _filaTemp["ID_VEREDA"] = _filaData["VER_ID"];
                _filaTemp["VEREDA"] = _filaData["VER_NOMBRE"];
                _filaTemp["ID_CORREGIMIENTO"] = _filaData["COR_ID"];
                _filaTemp["CORREGIMIENTO"] = _filaData["COR_NOMBRE"];
                _filaTemp["TELEFONO"] = _filaData["PER_TELEFONO"];
                _filaTemp["CELULAR"] = _filaData["PER_CELULAR"];
                _filaTemp["FAX"] = _filaData["PER_FAX"];
                _filaTemp["CORREO"] = _filaData["PER_CORREO_ELECTRONICO"];
                _filaTemp["ESTADO"] = _filaData["PER_ACTIVO"];

                _representantesTemp.Rows.Add(_filaTemp);
                _representantesTemp.AcceptChanges();
            }

            Session["Representantes"] = _representantesTemp;
        }

        /// <summary>
        /// Actualiza la información del representante
        /// </summary>
        /// <param name="IdPersona">int con el identificador de la persona</param>
        /// <param name="IdAutoridad">int con el identificador de la autoridad</param>
        private void ActualizarRepresentante(int IdPersona, int IdAutoridad)
        {
            SILPA.LogicaNegocio.Generico.Persona _personaActualizar = new SILPA.LogicaNegocio.Generico.Persona();

            //Actualizacion de Apoderados --> JMM -2010-07-12
            if ((DataTable)Session["Representantes"] != null &&
                ((DataTable)Session["Representantes"]).Rows.Count != 0)
            {
                Int32 iIdCorregimiento;
                Int32 iIdVereda;
                Int32 iIdMunicipio;

                Persona _representante = new Persona();
                DataTable _representantes = new DataTable();
                _representantes = (DataTable)Session["Representantes"];

                foreach (DataRow _registro in _representantes.Rows)
                {
                    if (_registro["ID_CORREGIMIENTO"].ToString() == "")
                        iIdCorregimiento = -1;
                    else
                        iIdCorregimiento = Convert.ToInt32(_registro["ID_CORREGIMIENTO"].ToString());

                    if (_registro["ID_VEREDA"].ToString() == "")
                        iIdVereda = -1;
                    else
                        iIdVereda = Convert.ToInt32(_registro["ID_VEREDA"].ToString());

                    if (_registro["ID_MUNICIPIO"].ToString() == "")
                        iIdMunicipio = -1;
                    else
                        iIdMunicipio = Convert.ToInt32(_registro["ID_MUNICIPIO"].ToString());

                    _representante.ActualizaApoderadoRepresentante(
                        _registro["PRIMER_NOMBRE"].ToString(),
                        _registro["SEGUNDO_NOMBRE"].ToString(),
                        _registro["PRIMER_APELLIDO"].ToString(),
                        _registro["SEGUNDO_APELLIDO"].ToString(),
                        _registro["ID_IDENTIFICACION"].ToString(),
                        Convert.ToInt32(_registro["ID_TIPO_ID"].ToString()),
                        _registro["ID_ORIGEN_MUNICIPIO"].ToString(),
                        _registro["TELEFONO"].ToString(),
                        _registro["CELULAR"].ToString(),
                        _registro["FAX"].ToString(),
                        _registro["CORREO"].ToString(),
                        0,
                        (int)TipoPersona.RepresentanteLegal,
                        _registro["TARJETA_PROFESIONAL"].ToString(),
                        IdPersona,
                        IdAutoridad,
                        false,
                        (int)TipoSolicitante.RepresentanteLegal,
                        _registro["DIRECCION_CORRESPONDENCIA"].ToString(),
                        Convert.ToInt32(_registro["ID_PAIS"].ToString()),
                        iIdMunicipio,
                        iIdVereda,
                        iIdCorregimiento,
                        (int)SILPA.Comun.TipoDireccion.Correspondencia,
                        Convert.ToBoolean(_registro["ESTADO"]));
                }

            }
        }

        /// <summary>
        /// JMM - 14/07/2010
        /// Metodo pora cargar los combos de tipo de documento segun el tipo de persona.
        /// </summary>
        /// <param name="dsTipoIdentificacion"></param>
        /// <param name="strCondicion"></param>
        /// <returns></returns>
        private DataTable ListaDocumentos(DataSet dsTipoIdentificacion, string strCondicion)
        {
            DataTable _dtIdentificacion = new DataTable();
            DataRow _Fila;

            _dtIdentificacion.Columns.Add("TPE_ID", Type.GetType("System.String"));
            _dtIdentificacion.Columns.Add("TID_ID", Type.GetType("System.String"));
            _dtIdentificacion.Columns.Add("TID_NOMBRE", Type.GetType("System.String"));
            _dtIdentificacion.Columns.Add("TID_ACTIVO", Type.GetType("System.String"));

            foreach (DataRow _registro in dsTipoIdentificacion.Tables[0].Select(strCondicion))
            {
                _Fila = _dtIdentificacion.NewRow();
                _Fila["TPE_ID"] = _registro[0];
                _Fila["TID_ID"] = _registro[1];
                _Fila["TID_NOMBRE"] = _registro[2];
                _Fila["TID_ACTIVO"] = _registro[3];

                _dtIdentificacion.Rows.Add(_Fila);
            }

            return _dtIdentificacion;
        }


        /// <summary>
        /// Genera link de acceso para activación de la clave
        /// </summary>
        /// <returns>string con el link de acceso</returns>
        private string GenerarLinkActivacionUsuario(long p_lngPersonaID, string p_strNumeroDocumento)
        {
            string strEnlace = "";
            int intTiempoVigenciaEnlace = 0;
            string strLlave = "";
            SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion = null;
            EnlaceActivacionUsuario objEnlaceActivacionUsuarioDalc = null;
            EnlaceActivacionUsuarioEntity objEnlace = null;

            try
            {
                //Generar llave
                strLlave = EnDecript.Encriptar(p_lngPersonaID.ToString() + "_" + p_strNumeroDocumento);

                //Obtener URL
                objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                strEnlace = objParametrizacion.ObtenerValorParametroGeneral(-1, "URL_CONFIRMACION_CORREO_REGISTRO") + HttpUtility.UrlEncode(strLlave) + "&Us=" + HttpUtility.UrlEncode(EnDecript.Encriptar(p_lngPersonaID.ToString())) + "&Id=" + HttpUtility.UrlEncode(EnDecript.Encriptar(p_strNumeroDocumento));
                intTiempoVigenciaEnlace = Convert.ToInt32(objParametrizacion.ObtenerValorParametroGeneral(-1, "TIEMPO_VIGENCIA_ENLACE_ACTIVACION_USUARIO"));

                //Insertar datos de URL
                objEnlace = new EnlaceActivacionUsuarioEntity
                {
                    PersonaID = p_lngPersonaID,
                    NumeroIdentificacion = p_strNumeroDocumento,
                    Llave = strLlave,
                    FechaVigencia = DateTime.Now.AddMinutes(intTiempoVigenciaEnlace),
                    FechaUtilizacion = default(DateTime)
                };
                objEnlaceActivacionUsuarioDalc = new EnlaceActivacionUsuario();
                objEnlaceActivacionUsuarioDalc.CrearEnlace(objEnlace);

            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DatosPersonales :: GenerarLinkActivacionUsuario -> Error Generando Link Activacion: " + exc.Message);

                throw exc;
            }

            return strEnlace;
        }

    #endregion


    #region Eventos

        #region Page

            /// <summary>
            /// Evento que inicializa la página
            /// </summary>
            protected void Page_Load(object sender, EventArgs e)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        InicializarControles();
                        CargarAutoridades();
                        CargarPaises();
                        CargarTiposIdentificacion();
                        CargarCombos();
                        Mensaje.LimpiarMensaje(this);
                        this.objConfiguracion = new Configuracion();
                        Session["Apoderados"] = null;
                        Session["Representantes"] = null;
                        #region Registro
                        if (Request.QueryString["reg"] == "registro")
                        {
                            lblMensaje.Text = "Para validar el siguiente registro debe presentarse ante la Autoridad Ambiental " +
                                "correspondiente y presentar su documento de Identificación. Para el caso de Personas Jurídicas, " +
                                "Certificado de existencia y representación legal. En caso que desee actuar por medio de apoderado " +
                                "se deberá presentar el correspondiente poder y el apoderado deberá registrarse en VITAL.";
                            optTipoUsuario.SelectedIndex = 0;
                        }

                        #endregion
                        //   VisualizarPestañas();
                        ValidarUsuarioRegistrado();

                    }
                }
                catch (Exception ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: Page_Load -> Error Inesperado: " + ex.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de la página');", true);
                }

            }

        #endregion


        #region Formulario

            /// <summary>
            /// Evento que carga la información de los municipios
            /// </summary>
            protected void cboTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    if (cboTipoDocumento.SelectedIndex == (int)TipoIdentificacion.CedulaExtranjeria)
                    {
                        this.cboDepartamentoOrigenNatural.SelectedValue = "11";
                        this.cboDepartamentoOrigenNatural.Enabled = false;
                        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoDep = int.Parse(cboDepartamentoOrigenNatural.SelectedItem.Value);
                        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
                        cboMunicipioOrigenNatural.DataSource = _temp;
                        cboMunicipioOrigenNatural.DataValueField = "MUN_ID";
                        cboMunicipioOrigenNatural.DataTextField = "MUN_NOMBRE";
                        cboMunicipioOrigenNatural.DataBind();
                        cboMunicipioOrigenNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        this.cboMunicipioOrigenNatural.SelectedValue = "11001";
                        this.cboMunicipioOrigenNatural.Enabled = false;
                    }
                    else
                    {
                        this.cboDepartamentoOrigenNatural.Enabled = true;
                        this.cboMunicipioOrigenNatural.Enabled = true;
                    }
                    tbcContenedor.ActiveTabIndex = 1;
                }
                catch (Exception ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboTipoDocumento_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de municipios');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }


            /// <summary>
            /// Evento que realiza el cargue de las pestañas de acuerdo a la seleccion realizada
            /// </summary>
            protected void optTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    VisualizarPestañas();
                }
                catch (Exception ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: optTipoUsuario_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando las pestañas');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            
            /// <summary>
            /// Retorna a pagina principal del sitio
            /// </summary>
            protected void btnCancelar_Click(object sender, EventArgs e)
            {
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion;
                string strURL;

                try
                {
                    if (Session["Representantes"] != null)
                        Session.Remove("Representantes");
                    if (Session["Apoderados"] != null)
                        Session.Remove("Apoderados");

                    if (Request.QueryString["IdUser"] == null)
                    {
                        objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                        strURL = objParametrizacion.ObtenerValorParametroGeneral(-1, "login_silpa");
                    }
                    else
                    {
                        strURL = @"Default.aspx";
                    }

                    //Redirecciona a la ventana principal    
                    Response.Redirect(strURL, false);
                }
                catch (Exception ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: btnCancelar_Click -> Error Inesperado: " + ex.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema retornando a página');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }


            /// <summary>
            /// Ingresa o actualiza los datos de una persona
            /// </summary>
            protected void btnActualizar_Click(object sender, EventArgs e)
            {
                Label lblMensaje = (Label)this.Master.FindControl("lblMensaje");
                lblMensaje.Text = "";
                bool AutorizaCorreo = false;
                string strLinkActivacion = "";
                bool blnCaptchaValido = true;

                //Se coloca como registro 0 debido a que el proceso automaticamente aprueba la solicitud de credenciales
                int enProceso = 0;
                bool bValidaInsercion = true;
                string numeroDocumento = "";

                try
                {
                    if (Page.IsValid)
                    {
                        if (Request.QueryString["IdUser"] != null)
                        {
                            #region Actualizacion

                            SILPA.LogicaNegocio.Generico.Persona _personaActualizar = new SILPA.LogicaNegocio.Generico.Persona();
                            _personaActualizar.ObternerPersonaByUserIdApp(Request.QueryString["IdUser"]);

                            #region Actualizacion Persona Natural

                            if (_personaActualizar.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.Natural)
                            {

                                /// Se valida la existencia o no del correo con el cual el solicitante inicia el registro
                                if (_personaActualizar.ExisteCorreoSol(_personaActualizar.Identity.IdApplicationUser, txtCorreoNatural.Text))
                                {
                                    Mensaje.MostrarMensaje(this, "Ya existe un usuario con este Correo electrónico:  " + txtCorreoNatural.Text + ".  Por favor, ingrese un Correo electrónico diferente.");
                                    return;
                                }
                                AutorizaCorreo = ChkAutorizaNotifPerNatural.Checked;
                                _personaActualizar.ActualizarPersonaSolicitante(_personaActualizar.Identity.PersonaId, txtPrimerNombreNatural.Text.Trim(), txtSegundoNombreNatural.Text.Trim(),
                                    txtPrimerApellidoNatural.Text.Trim(), txtSegundoApellidoNatural.Text.Trim(), txtTelefonoNatural.Text.Trim(), txtCelularNatural.Text.Trim(), txtFaxNatural.Text.Trim(),
                                    txtCorreoNatural.Text.Trim(), "", "", _personaActualizar.Identity.IdSolicitante, AutorizaCorreo);//jmartinez Adiciono campo autoriza correo

                                _personaActualizar.ActualizarDireccion(Convert.ToInt32(cboMunicipioNatural.SelectedValue), Convert.ToInt32(cboCorregimientoNatural.SelectedValue),
                                    Convert.ToInt32(cboVeredaNatural.SelectedValue), txtDireccionNatural.Text.Trim(), _personaActualizar.Identity.PersonaId,
                                    Convert.ToInt32(cboPaisNatural.SelectedValue), Convert.ToInt32(SILPA.Comun.TipoDireccion.Domicilio));

                                _personaActualizar.ActualizarDireccion(Convert.ToInt32(cboMunicipioCorrespondencia.SelectedValue), Convert.ToInt32(cboCorregimientoCorrespondencia.SelectedValue),
                                    Convert.ToInt32(cboVeredaCorrespondencia.SelectedValue), txtDireccionCorrespondencia.Text.Trim(), _personaActualizar.Identity.PersonaId,
                                    Convert.ToInt32(cboPaisCorrespondencia.SelectedValue), Convert.ToInt32(SILPA.Comun.TipoDireccion.Correspondencia));

                                ActualizarApoderado((int)_personaActualizar.Identity.PersonaId, (int)_personaActualizar.Identity.IdAutoridadAmbiental);
                                ActualizarRepresentante((int)_personaActualizar.Identity.PersonaId, (int)_personaActualizar.Identity.IdAutoridadAmbiental);

                            }
                            #endregion

                            #region Actualizacion Persona Juridica Publica

                            if (_personaActualizar.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.JuridicaPublica)
                            {
                                /// Se valida la existencia o no del correo con el cual el solicitante inicia el registro
                                if (_personaActualizar.ExisteCorreoSol(_personaActualizar.Identity.IdApplicationUser, txtCorreoJuridica.Text))
                                {
                                    Mensaje.MostrarMensaje(this, "Ya existe un usuario con este Correo electrónico:  " + txtCorreoJuridica.Text + ".  Por favor, ingrese un Correo electrónico diferente.");
                                    return;
                                }

                                AutorizaCorreo = ChkAutorizaNotifPerJuridica.Checked;
                                _personaActualizar.ActualizarPersonaSolicitante(_personaActualizar.Identity.PersonaId, "", "",
                                    "", "", txtTelefonoJuridica.Text.Trim(), txtCelularJuridica.Text.Trim(), txtFaxJuridica.Text.Trim(),
                                    txtCorreoJuridica.Text.Trim(), txtRazonSocialJuridica.Text.Trim(), "", _personaActualizar.Identity.IdSolicitante, AutorizaCorreo);

                                _personaActualizar.ActualizarDireccion(Convert.ToInt32(cboMunicipioJuridica.SelectedValue), Convert.ToInt32(cboCorregimientoJuridica.SelectedValue),
                                    Convert.ToInt32(cboVeredaJuridica.SelectedValue), txtDireccionJuridica.Text.Trim(), _personaActualizar.Identity.PersonaId,
                                    Convert.ToInt32(cboPaisJuridica.SelectedValue), Convert.ToInt32(SILPA.Comun.TipoDireccion.Domicilio));
                                ///17-08-2010: aegb. incidencia 2074
                                _personaActualizar.ActualizarDireccion(Convert.ToInt32(cboMunicipioJuridica.SelectedValue), Convert.ToInt32(cboCorregimientoJuridica.SelectedValue),
                                  Convert.ToInt32(cboVeredaJuridica.SelectedValue), txtDireccionJuridica.Text.Trim(), _personaActualizar.Identity.PersonaId,
                                  Convert.ToInt32(cboPaisJuridica.SelectedValue), Convert.ToInt32(SILPA.Comun.TipoDireccion.Correspondencia));

                                ActualizarApoderado((int)_personaActualizar.Identity.PersonaId, (int)_personaActualizar.Identity.IdAutoridadAmbiental);
                                ActualizarRepresentante((int)_personaActualizar.Identity.PersonaId, (int)_personaActualizar.Identity.IdAutoridadAmbiental);

                            }
                            #endregion

                            #region Actualizacion Persona Juridica Privada
                            if (_personaActualizar.Identity.TipoPersona.CodigoTipoPersona == (int)TipoPersona.JuridicaPrivada)
                            {
                                /// Se valida la existencia o no del correo con el cual el solicitante inicia el registro
                                if (_personaActualizar.ExisteCorreoSol(_personaActualizar.Identity.IdApplicationUser, txtCorreoPrivada.Text))
                                {
                                    Mensaje.MostrarMensaje(this, "Ya existe un usuario con este Correo electrónico:  " + txtCorreoPrivada.Text + ".  Por favor, ingrese un Correo electrónico diferente.");
                                    return;
                                }

                                AutorizaCorreo = ChkAutorizaNotifPerPrivada.Checked;
                                _personaActualizar.ActualizarPersonaSolicitante(_personaActualizar.Identity.PersonaId, "", "",
                                    "", "", txtTelefonoPrivada.Text.Trim(), txtCelularPrivada.Text.Trim(), txtFaxPrivada.Text.Trim(),
                                    txtCorreoPrivada.Text.Trim(), txtRazonSocialPrivada.Text.Trim(), "", _personaActualizar.Identity.IdSolicitante, AutorizaCorreo); //jmartinez campo adicionar correo

                                _personaActualizar.ActualizarDireccion(Convert.ToInt32(cboMunicipioPrivada.SelectedValue), Convert.ToInt32(cboCorregimientoPrivada.SelectedValue),
                                 Convert.ToInt32(cboVeredaPrivada.SelectedValue), txtDireccionPrivada.Text.Trim(), _personaActualizar.Identity.PersonaId,
                                 Convert.ToInt32(cboPaisPrivada.SelectedValue), Convert.ToInt32(SILPA.Comun.TipoDireccion.Domicilio));
                                ///17-08-2010: aegb. incidencia 2074
                                _personaActualizar.ActualizarDireccion(Convert.ToInt32(cboMunicipioPrivada.SelectedValue), Convert.ToInt32(cboCorregimientoPrivada.SelectedValue),
                                    Convert.ToInt32(cboVeredaPrivada.SelectedValue), txtDireccionPrivada.Text.Trim(), _personaActualizar.Identity.PersonaId,
                                    Convert.ToInt32(cboPaisPrivada.SelectedValue), Convert.ToInt32(SILPA.Comun.TipoDireccion.Correspondencia));
                                //rricaurte inc 2191
                                _personaActualizar.ActualizarDireccion(Convert.ToInt32(cboMunicipioCorrespondenciaPrivada.SelectedValue), Convert.ToInt32(cboCorregimientoCorrespondenciaPrivada.SelectedValue),
                                    Convert.ToInt32(cboVeredaCorrespondenciaPrivada.SelectedValue), txtDireccionCorrespondenciaPrivada.Text.Trim(), _personaActualizar.Identity.PersonaId,
                                    Convert.ToInt32(cboPaisCorrespondenciaPrivada.SelectedValue), Convert.ToInt32(SILPA.Comun.TipoDireccion.Correspondencia));

                                ActualizarApoderado((int)_personaActualizar.Identity.PersonaId, (int)_personaActualizar.Identity.IdAutoridadAmbiental);
                                ActualizarRepresentante((int)_personaActualizar.Identity.PersonaId, (int)_personaActualizar.Identity.IdAutoridadAmbiental);
                            }
                            #endregion

                            bValidaInsercion = false;

                            #endregion
                        }
                        else
                        {
                            //Verificar que el captcha se encuentre bien
                            blnCaptchaValido = Recaptcha.Validar(Request.Form["g-Recaptcha-Response"]);
                            if (blnCaptchaValido)
                            {
                                #region Insercion

                                SILPA.LogicaNegocio.Generico.Persona _persona = new SILPA.LogicaNegocio.Generico.Persona();
                                SILPA.LogicaNegocio.Generico.SolicitudCredenciales _solicitudCredenciales = new SILPA.LogicaNegocio.Generico.SolicitudCredenciales();
                                Int64 _idPersona;
                                int _estado = 0;

                                /// Se verifica la existencia del usuario mediante el número de identificación:
                                switch (optTipoUsuario.SelectedValue)
                                {
                                    case "natural":
                                        _estado = _persona.VerificarExistenciaByNumeroIdentificacion(txtNumeroIdentificacion.Text.Trim());
                                        //jmartinez Asigno valor notificacion correo
                                        AutorizaCorreo = this.ChkAutorizaNotifPerNatural.Checked;
                                        break;
                                    case "juridica publica":
                                        _estado = _persona.VerificarExistenciaByNumeroIdentificacion(txtNumeroDocumentoJuridica.Text.Trim());
                                        //jmartinez Asigno valor notificacion correo
                                        AutorizaCorreo = this.ChkAutorizaNotifPerJuridica.Checked;
                                        break;
                                    case "juridica privada":
                                        _estado = _persona.VerificarExistenciaByNumeroIdentificacion(txtNumeroDocumentoPrivada.Text.Trim());
                                        //jmartinez Asigno valor notificacion correo
                                        AutorizaCorreo = this.ChkAutorizaNotifPerPrivada.Checked;
                                        break;
                                }

                                if (_estado == (int)SILPA.Comun.EstadoSolicitudCredencial.EnProceso)
                                {
                                    Mensaje.MostrarMensaje(this, "Ya existe una solicitud en proceso. Debe esperar un mensaje de correo electrónico con la activación de su solicitud");
                                    return;
                                }
                                if (_estado == (int)SILPA.Comun.EstadoSolicitudCredencial.Aprobado)
                                {
                                    Mensaje.MostrarMensaje(this, "Ya existe un usuario registrado con los mismos datos de identificación");
                                    return;
                                }

                                #region Insercion Persona Natural

                                if (optTipoUsuario.SelectedValue == "natural")
                                {
                                    numeroDocumento = txtNumeroIdentificacion.Text;

                                    this.objConfiguracion = new Configuracion();

                                    _persona.IdentityDir.DireccionPersona = txtDireccionNatural.Text;

                                    _idPersona = _persona.InsertarPersona(txtNumeroIdentificacion.Text, txtNumeroIdentificacion.Text,
                                                             EnDecript.Encriptar(txtNumeroIdentificacion.Text + "*"),
                                                            txtPrimerNombreNatural.Text,
                                                            txtSegundoNombreNatural.Text,
                                                            txtPrimerApellidoNatural.Text,
                                                            txtSegundoApellidoNatural.Text, 1,
                                                            "Natural", txtNumeroIdentificacion.Text,
                                                            txtCorreoNatural.Text,
                                                            int.Parse(cboTipoDocumento.SelectedItem.Value),
                                                            cboMunicipioOrigenNatural.SelectedItem.Value,
                                                            int.Parse(cboPaisNatural.SelectedItem.Value),
                                                            txtTelefonoNatural.Text, txtCelularNatural.Text,
                                                            txtFaxNatural.Text, "", (int)TipoPersona.Natural, "", 0,
                                                            int.Parse(cboAutoridadAmbiental.SelectedValue), "",
                                                            "", 99999, "False", "True", "NoImage.gif", "F",
                                                            Convert.ToInt32(cboDepartamentoNatural.SelectedValue),
                                                            Convert.ToInt32(cboMunicipioNatural.SelectedValue),
                                                            Convert.ToInt32(cboVeredaNatural.SelectedValue),
                                                            Convert.ToInt32(cboCorregimientoNatural.SelectedValue), (int)TipoSolicitante.Solicitante,
                                                            AutorizaCorreo);//jmartinez Adiciono Campo Autorizacion Correo


                                    ///hava
                                    // Se insertan las direcciones de Expedición del documento
                                    _persona.InsertarDireccion(this.objConfiguracion.IdPaisPredeterminado,
                                                                int.Parse(this.cboDepartamentoOrigenNatural.SelectedValue),
                                                                int.Parse(this.cboMunicipioOrigenNatural.SelectedValue),
                                                                -1, -1,
                                                                (int)SILPA.Comun.TipoDireccion.Expedicion_Documento,
                                                                string.Empty
                                                               );

                                    ///hava
                                    // Se insertan las Direcciones de Correspondencia
                                    _persona.InsertarDireccion(int.Parse(this.cboPaisCorrespondencia.SelectedValue),
                                                               int.Parse(this.cboDepartamentoCorrespondencia.SelectedValue),
                                                               int.Parse(this.cboMunicipioCorrespondencia.SelectedValue),
                                                               int.Parse(this.cboCorregimientoCorrespondencia.SelectedValue),
                                                               int.Parse(this.cboVeredaCorrespondencia.SelectedValue),
                                                               (int)SILPA.Comun.TipoDireccion.Correspondencia,
                                                               this.txtDireccionCorrespondencia.Text
                                                               );


                                    _solicitudCredenciales.InsertarSolicitudCredenciales(_idPersona, enProceso);

                                    InsertarApoderados(_idPersona);

                                    #region Envio De Correo
                                    PersonaIdentity objPersona = new PersonaIdentity();
                                    objPersona.PrimerNombre = txtPrimerNombreNatural.Text;
                                    objPersona.SegundoNombre = txtSegundoNombreNatural.Text;
                                    objPersona.PrimerApellido = txtPrimerApellidoNatural.Text;
                                    objPersona.SegundoApellido = txtSegundoApellidoNatural.Text;
                                    objPersona.CorreoElectronico = txtCorreoNatural.Text.Trim();

                                    strLinkActivacion = this.GenerarLinkActivacionUsuario(_idPersona, numeroDocumento);
                                    SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorreoConfirmacionCorreo(objPersona.CorreoElectronico,
                                                                                                      numeroDocumento,
                                                                                                      (!string.IsNullOrWhiteSpace(objPersona.PrimerNombre) ? objPersona.PrimerNombre : "") +
                                                                                                      (!string.IsNullOrWhiteSpace(objPersona.SegundoNombre) ? " " + objPersona.SegundoNombre : "") +
                                                                                                      (!string.IsNullOrWhiteSpace(objPersona.PrimerApellido) ? " " + objPersona.PrimerApellido : "") +
                                                                                                      (!string.IsNullOrWhiteSpace(objPersona.SegundoApellido) ? " " + objPersona.SegundoApellido : ""),
                                                                                                      strLinkActivacion,
                                                                                                      cboAutoridadAmbiental.SelectedItem.Text);
                                    #endregion
                                }
                                #endregion

                                #region Insercion Persona Juridica Publica
                                else if (optTipoUsuario.SelectedValue == "juridica publica")
                                {
                                    numeroDocumento = txtNumeroDocumentoJuridica.Text;

                                    _persona.IdentityDir.DireccionPersona = txtDireccionJuridica.Text;

                                    _idPersona = _persona.InsertarPersona(
                                                           txtNumeroDocumentoJuridica.Text,
                                                           txtRazonSocialJuridica.Text,
                                                           EnDecript.Encriptar(txtNumeroDocumentoJuridica.Text + "*"),
                                                           "", "", "", "", 1, "Juridica Publica", txtNumeroDocumentoJuridica.Text,
                                                           txtCorreoJuridica.Text,
                                                           int.Parse(cboTipoDocumentoJuridica.SelectedValue),
                                                           "", int.Parse(cboPaisJuridica.SelectedValue),
                                                           txtTelefonoJuridica.Text, txtCelularJuridica.Text,
                                                           txtFaxJuridica.Text, txtRazonSocialJuridica.Text, (int)TipoPersona.JuridicaPublica,
                                                           "", 0, int.Parse(cboAutoridadAmbiental.SelectedValue),
                                                           "", "",
                                                           99999, "False", "True", "NoImage.gif", "F", Convert.ToInt32(cboDepartamentoJuridica.SelectedValue),
                                                           Convert.ToInt32(cboMunicipioJuridica.SelectedValue),
                                                           Convert.ToInt32(cboVeredaJuridica.SelectedValue),
                                                           Convert.ToInt32(cboCorregimientoJuridica.SelectedValue), (int)TipoSolicitante.Solicitante, AutorizaCorreo);

                                    ///17-08-2010: aegb. incidencia 2074
                                    // Se insertan las Direcciones de Correspondencia
                                    _persona.InsertarDireccion(int.Parse(this.cboPaisJuridica.SelectedValue),
                                                               int.Parse(this.cboDepartamentoJuridica.SelectedValue),
                                                               int.Parse(this.cboMunicipioJuridica.SelectedValue),
                                                               int.Parse(this.cboCorregimientoJuridica.SelectedValue),
                                                               int.Parse(this.cboVeredaJuridica.SelectedValue),
                                                               (int)SILPA.Comun.TipoDireccion.Correspondencia,
                                                               this.txtDireccionJuridica.Text
                                                               );

                                    _solicitudCredenciales.InsertarSolicitudCredenciales(_idPersona, enProceso);

                                    InsertarApoderados(_idPersona);

                                    InsertarRepresentantes(_idPersona);

                                    #region Envio De Correo
                                    PersonaIdentity objPersona = new PersonaIdentity();
                                    objPersona.PrimerNombre = txtRazonSocialJuridica.Text;
                                    objPersona.CorreoElectronico = txtCorreoJuridica.Text.Trim();

                                    if (!string.IsNullOrEmpty(this.txtCorreoJuridica.Text))
                                    {
                                        strLinkActivacion = this.GenerarLinkActivacionUsuario(_idPersona, numeroDocumento);
                                        SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorreoConfirmacionCorreo(objPersona.CorreoElectronico, numeroDocumento, objPersona.PrimerNombre, strLinkActivacion, cboAutoridadAmbiental.SelectedItem.Text); ;
                                    }

                                    #endregion
                                }
                                #endregion

                                #region Insercion Persona Juridica Privada
                                else if (optTipoUsuario.SelectedValue == "juridica privada")
                                {
                                    numeroDocumento = txtNumeroDocumentoPrivada.Text;

                                    _persona.IdentityDir.DireccionPersona = txtDireccionPrivada.Text;

                                    _idPersona = _persona.InsertarPersona(
                                                           txtNumeroDocumentoPrivada.Text,
                                                           txtRazonSocialPrivada.Text,
                                                           EnDecript.Encriptar(txtNumeroDocumentoPrivada.Text + "*"),
                                                           "", "", "", "", 1, "Juridica Privada", txtNumeroDocumentoPrivada.Text,
                                                           txtCorreoPrivada.Text,
                                                           int.Parse(cboTipoDocumentoPrivada.SelectedValue),
                                                           "", int.Parse(cboPaisPrivada.SelectedValue),
                                                           txtTelefonoPrivada.Text, txtCelularPrivada.Text,
                                                           txtFaxPrivada.Text, txtRazonSocialPrivada.Text, (int)TipoPersona.JuridicaPrivada,
                                                           "", 0, int.Parse(cboAutoridadAmbiental.SelectedValue),
                                                           "", "",
                                                           99999, "False", "True", "NoImage.gif", "F", Convert.ToInt32(cboDepartamentoPrivada.SelectedValue),
                                                           Convert.ToInt32(cboMunicipioPrivada.SelectedValue),
                                                           Convert.ToInt32(cboVeredaPrivada.SelectedValue),
                                                           Convert.ToInt32(cboCorregimientoPrivada.SelectedValue), (int)TipoSolicitante.Solicitante, AutorizaCorreo);

                                    //rrricaurte direccion de correspondencia para Persona Jurídica Privada INC 2191
                                    _persona.InsertarDireccion(int.Parse(this.cboPaisCorrespondenciaPrivada.SelectedValue),
                                       int.Parse(this.cboDepartamentoCorrespondenciaPrivada.SelectedValue),
                                       int.Parse(this.cboMunicipioCorrespondenciaPrivada.SelectedValue),
                                       int.Parse(this.cboCorregimientoCorrespondenciaPrivada.SelectedValue),
                                       int.Parse(this.cboVeredaCorrespondenciaPrivada.SelectedValue),
                                       (int)SILPA.Comun.TipoDireccion.Correspondencia,
                                       this.txtDireccionCorrespondenciaPrivada.Text
                                       );

                                    _solicitudCredenciales.InsertarSolicitudCredenciales(_idPersona, enProceso);


                                    InsertarApoderados(_idPersona);

                                    InsertarRepresentantes(_idPersona);

                                    #region Envio De Correo
                                    PersonaIdentity objPersona = new PersonaIdentity();
                                    objPersona.PrimerNombre = txtRazonSocialPrivada.Text;
                                    objPersona.CorreoElectronico = txtCorreoPrivada.Text.Trim();

                                    if (!string.IsNullOrEmpty(this.txtCorreoPrivada.Text))
                                    {
                                        strLinkActivacion = this.GenerarLinkActivacionUsuario(_idPersona, numeroDocumento);
                                        SILPA.LogicaNegocio.ICorreo.Correo.EnviarCorreoConfirmacionCorreo(objPersona.CorreoElectronico, numeroDocumento, objPersona.PrimerNombre, strLinkActivacion, cboAutoridadAmbiental.SelectedItem.Text); ;
                                    }
                                    #endregion
                                }
                                #endregion

                                bValidaInsercion = true;

                                #endregion

                                GenerarRTF(numeroDocumento, optTipoUsuario.SelectedValue);
                                LimpiarCamposJuridica();
                                LimpiarCamposNatural();

                            }                            
                        }

                        //Verificar que el captcha es valido
                        if (blnCaptchaValido)
                        {

                            Session.Remove("Representantes");
                            Session.Remove("Apoderados");

                            GenerarLogAuditoria CrearLogAuditoria = new GenerarLogAuditoria();
                            CrearLogAuditoria.Insertar("SILPA", 1, "Se almaceno Datos Personales");

                            //Mostrar mensaje de confirmación de creación del usuario
                            if (bValidaInsercion == true)
                            {
                                this.ltlMensajeResultadoOK.Text = "La información se registro de manera exitosa. Se enviará un correo electrónico a la cuenta registrada para confirmar la información y realizar la activación de la cuenta correspondiente.";
                            }
                            else
                            {
                                ValidarUsuarioRegistrado();
                                this.ltlMensajeResultadoOK.Text = "La información fue modificada de manera exitosa.";
                            }

                            this.upnlModalResultadoDatosPersona.Update();
                            this.mpeModalResultadoDatosPersona.Show();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgBox", "<script>alert('Verificación de captcha es incorrecto, por favor trate nuevamente')</script>");
                        }
                    }

                }
                catch (SqlException ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: btnActualizar_Click -> Error Inesperado: " + ex.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema guardando la información de la persona');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            /// <summary>
            /// Retorna si se debe crear o eliminar
            /// </summary>
            protected string DoSomething(string value)
            {
                if (string.IsNullOrEmpty(value))
                    return "No crear";
                else
                    return "Eliminar";

            }


            /// <summary>
            /// Evento que cierra modal y direcciona a la página principal
            /// </summary>
            protected void cmdAceptarResultadoDatosPersona_Click(object sender, EventArgs e)
            {
                SILPA.LogicaNegocio.Parametrizacion.Parametrizacion objParametrizacion;
                string strURL;

                try
                {
                    if (Session["Representantes"] != null)
                        Session.Remove("Representantes");
                    if (Session["Apoderados"] != null)
                        Session.Remove("Apoderados");

                    if (Request.QueryString["IdUser"] == null)
                    {
                        objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                        strURL = objParametrizacion.ObtenerValorParametroGeneral(-1, "login_silpa");
                    }
                    else
                    {
                        strURL = @"Default.aspx";
                    }

                    //Redirecciona a la ventana principal    
                    Response.Redirect(strURL, false);
                }
                catch (Exception ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cmdAceptarResultadoDatosPersona_Click -> Error Inesperado: " + ex.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema retornando a página principal');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

        #endregion


        #region Actualizar Grillas Apoderados y Representantes

            /// <summary>
            /// Actualiza la información del apoderado
            /// </summary>
            protected void btnActualizarApoderado_Click(object sender, EventArgs e)
            {
                try
                {
                    CargarApoderados();
                }
                catch (Exception ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: btnActualizarApoderado_Click -> Error Inesperado: " + ex.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema información del apoderado');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            /// <summary>
            /// Cargar representantes - Publico
            /// </summary>
            protected void btnActualizarRepresentante_Click(object sender, EventArgs e)
            {
                try
                {
                    CargarRepresentantes();
                }
                catch (Exception ex)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: btnActualizarRepresentante_Click -> Error Inesperado: " + ex.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando representantes');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            /// <summary>
            /// Cargar representantes - Publico
            /// </summary>
            protected void btnActualizarPrivada_Click(object sender, EventArgs e)
            {
                if ((DataTable)Session["Representantes"] != null &&
                    ((DataTable)Session["Representantes"]).Rows.Count != 0)
                {
                    grdRepresentantesPrivada.DataSource = (DataTable)Session["Representantes"];
                    grdRepresentantesPrivada.DataBind();
                    pnlRepresentantesPrivada.Visible = true;
                }
            }

        #endregion


        #region Agregar Apoderados y Representantes

            /// <summary>
            /// Evento que abre la ventana para ingresar apoderado
            /// </summary>
            protected void btnAgregarApoderado_Click(object sender, EventArgs e)
            {
                try
                {
                    CrearVariableApoderados();
                    string strScript = "window.open('Apoderado.aspx?modo=0&btnId=" + this.btnActualizarApoderado.ClientID + "','pruebas','location=no,resizable=yes,scrollbars=yes')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", strScript, true);
                    this.upnlFormularioPersona.Update();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: btnAgregarApoderado_Click -> Error Inesperado: " + exc.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Error cargando formulario para agregar apoderado');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            /// <summary>
            /// Evento que abre la ventana para ingresar representante - Publico
            /// </summary>
            protected void btnAgregarRepresentante_Click(object sender, EventArgs e)
            {
                try
                {
                    CrearVariableRepresentantes();
                    string strScript = "window.open('RepresentanteLegal.aspx?modo=0&btnId=" + this.btnActualizarRepresentante.ClientID + "','pruebas','location=no,resizable=yes,scrollbars=yes')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", strScript, true);
                    this.upnlFormularioPersona.Update();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: btnAgregarRepresentante_Click -> Error Inesperado: " + exc.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Error cargando formulario para agregar representante');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            /// <summary>
            /// Evento que abre la ventana para ingresar representante - privado
            /// </summary>
            protected void btnAgregarPrivada_Click(object sender, EventArgs e)
            {
                try
                {
                    CrearVariableRepresentantes();
                    string strScript = "window.open('RepresentanteLegal.aspx?modo=0&btnId=" + this.btnActualizarPrivada.ClientID + "','pruebas','location=no,resizable=yes,scrollbars=yes')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", strScript, true);
                    this.upnlFormularioPersona.Update();
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: btnAgregarPrivada_Click -> Error Inesperado: " + exc.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Error cargando formulario para agregar representante');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

        #endregion


        #region Eventos Grillas Apoderados y Representantes


            /// <summary>
            /// Evento que se encarga de efectuar una acción determinada sobre un apoderado
            /// </summary>
            protected void grdApoderados_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                try
                {
                    GridViewRow row;
                    int index = Convert.ToInt32(e.CommandArgument);

                    if (index < 0)
                        return;

                    if (e.CommandName == "Actualizar")
                    {
                        row = grdApoderados.Rows[index];
                        CrearVariableApoderados();
                        string strScript = "window.open('Apoderado.aspx?modo=1&btnId=" + this.btnActualizarApoderado.ClientID + "&id=" + row.Cells[11].Text + "','Pruebas','location=yes,resizable=yes,scrollbars=yes')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", strScript, true);
                    }
                    else if (e.CommandName == "Eliminar")
                    {
                        DataTable _apoderados = new DataTable();

                        _apoderados = (DataTable)Session["Apoderados"];
                        row = grdApoderados.Rows[index];
                        Persona objPer = new Persona();
                        Label IdPer = (Label)row.FindControl("IdPer");

                        if (string.IsNullOrEmpty(IdPer.Text))
                        {
                            if (_apoderados.Select("ID_IDENTIFICACION = '" + row.Cells[11].Text + "'").Length > 0)
                            {
                                foreach (DataRow _temp in _apoderados.Rows)
                                {
                                    if (_temp["ID_IDENTIFICACION"].ToString() == row.Cells[11].Text)
                                    {
                                        _apoderados.Rows.Remove(_temp);
                                        _apoderados.AcceptChanges();
                                        Session["Apoderados"] = _apoderados;
                                        if (_apoderados.Rows.Count > 0)
                                        {
                                            grdApoderados.DataSource = (DataTable)Session["Apoderados"];
                                            grdApoderados.DataBind();
                                            pnlApoderado.Visible = true;
                                        }
                                        else
                                        {
                                            grdApoderados.DataSource = null;
                                            grdApoderados.DataBind();
                                            pnlApoderado.Visible = false;
                                        }
                                        return;
                                    }
                                }
                            }
                            else
                                pnlApoderado.Visible = false;
                        }
                        else
                        {
                            if (objPer.ConsultarPersonaAsociaApod(IdPer.Text))
                            {
                                Mensaje.MostrarMensaje(this.Page, "Este Apoderado no se puede eliminar por que esta relacionado en un tramite");
                                return;
                            }
                            if (_apoderados.Select("ID_IDENTIFICACION = '" + row.Cells[11].Text + "'").Length > 0)
                            {
                                foreach (DataRow _temp in _apoderados.Rows)
                                {
                                    if (_temp["ID_IDENTIFICACION"].ToString() == row.Cells[11].Text)
                                    {
                                        _apoderados.Rows.Remove(_temp);
                                        _apoderados.AcceptChanges();
                                        Session["Apoderados"] = _apoderados;
                                        PersonaDalc objPersona = new PersonaDalc();
                                        PersonaIdentity objPersonaIdentity = new PersonaIdentity();
                                        objPersonaIdentity.PersonaId = int.Parse(IdPer.Text);
                                        objPersona.EliminarPersona(ref objPersonaIdentity);
                                        if (_apoderados.Rows.Count > 0)
                                        {
                                            grdApoderados.DataSource = (DataTable)Session["Apoderados"];
                                            grdApoderados.DataBind();
                                            pnlApoderado.Visible = true;
                                        }
                                        else
                                        {
                                            grdApoderados.DataSource = null;
                                            grdApoderados.DataBind();
                                            pnlApoderado.Visible = false;
                                        }
                                        return;
                                    }
                                }
                            }
                            else
                                pnlApoderado.Visible = false;
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: grdApoderados_RowCommand -> Error Inesperado: " + exc.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Error efectuando la acción sobre el apoderado seleccionado');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            /// <summary>
            /// Evento que se encarga de efectuar una acción determinada sobre un representante - Publico
            /// </summary>
            protected void grdRepresentantes_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                try
                {
                    GridViewRow row;
                    int index = Convert.ToInt32(e.CommandArgument);

                    if (index < 0)
                        return;

                    if (e.CommandName == "Actualizar")
                    {
                        row = grdRepresentantes.Rows[index];
                        CrearVariableRepresentantes();
                        string popupScript = "window.open('RepresentanteLegal.aspx?modo=1&btnId=" + this.btnActualizarRepresentante.ClientID + "&id=" + row.Cells[9].Text + "','pruebas','location=yes,resizable=yes,scrollbars=yes')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", popupScript, true);
                    }
                    else if (e.CommandName == "Eliminar")
                    {
                        DataTable _representantes = new DataTable();

                        _representantes = (DataTable)Session["Representantes"];
                        row = grdRepresentantes.Rows[index];
                        Persona objPer = new Persona();
                        Label IdPer = (Label)row.FindControl("IdPer2");
                        if (string.IsNullOrEmpty(IdPer.Text))
                        {
                            if (_representantes.Select("ID_IDENTIFICACION = '" + row.Cells[9].Text + "'").Length > 0)
                            {
                                foreach (DataRow _temp in _representantes.Rows)
                                {
                                    if (_temp["ID_IDENTIFICACION"].ToString() == row.Cells[9].Text)
                                    {
                                        _representantes.Rows.Remove(_temp);
                                        _representantes.AcceptChanges();
                                        Session["Representantes"] = _representantes;
                                        if (_representantes.Rows.Count > 0)
                                        {
                                            grdRepresentantes.DataSource = (DataTable)Session["Representantes"];
                                            grdRepresentantes.DataBind();
                                            pnlRepresentante.Visible = true;
                                        }
                                        else
                                        {
                                            grdRepresentantes.DataSource = null;
                                            grdRepresentantes.DataBind();
                                            pnlRepresentante.Visible = false;
                                        }
                                        return;
                                    }
                                }
                            }
                            else
                                pnlRepresentante.Visible = false;
                        }
                        else
                        {
                            if (objPer.ConsultarPersonaAsociaRepre(IdPer.Text))
                            {
                                Mensaje.MostrarMensaje(this.Page, "Este Representante no se puede eliminar por que esta relacionado en un tramite");
                                return;
                            }
                            if (_representantes.Select("ID_IDENTIFICACION = '" + row.Cells[9].Text + "'").Length > 0)
                            {
                                foreach (DataRow _temp in _representantes.Rows)
                                {
                                    if (_temp["ID_IDENTIFICACION"].ToString() == row.Cells[9].Text)
                                    {
                                        _representantes.Rows.Remove(_temp);
                                        _representantes.AcceptChanges();
                                        Session["Representantes"] = _representantes;
                                        PersonaDalc objPersona = new PersonaDalc();
                                        PersonaIdentity objPersonaIdentity = new PersonaIdentity();
                                        objPersonaIdentity.PersonaId = int.Parse(IdPer.Text);
                                        objPersona.EliminarPersona(ref objPersonaIdentity);
                                        if (_representantes.Rows.Count > 0)
                                        {
                                            grdRepresentantes.DataSource = (DataTable)Session["Representantes"];
                                            grdRepresentantes.DataBind();
                                            pnlRepresentante.Visible = true;
                                        }
                                        else
                                        {
                                            grdRepresentantes.DataSource = null;
                                            grdRepresentantes.DataBind();
                                            pnlRepresentante.Visible = false;
                                        }
                                        return;
                                    }
                                }
                            }
                            else
                                pnlRepresentante.Visible = false;
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: grdRepresentantes_RowCommand -> Error Inesperado: " + exc.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Error efectuando la acción sobre el representante seleccionado');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            /// <summary>
            /// Evento que se encarga de efectuar una acción determinada sobre un representante - Privado
            /// </summary>
            protected void grdRepresentantesPrivada_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                try
                {
                    GridViewRow row;
                    int index = Convert.ToInt32(e.CommandArgument);

                    if (index < 0)
                        return;

                    if (e.CommandName == "Actualizar")
                    {
                        row = grdRepresentantesPrivada.Rows[index];
                        CrearVariableRepresentantes();
                        string popupScript = "window.open('RepresentanteLegal.aspx?modo=1&btnId=" + this.btnActualizarPrivada.ClientID + "&id=" + row.Cells[9].Text + "','pruebas','location=yes,resizable=yes,scrollbars=yes')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", popupScript, true);
                        tbcContenedor.ActiveTabIndex = 3;
                    }
                    else if (e.CommandName == "Eliminar")
                    {
                        DataTable _representantes = new DataTable();

                        _representantes = (DataTable)Session["Representantes"];
                        row = grdRepresentantesPrivada.Rows[index];
                        Persona objPer = new Persona();
                        Label IdPer = (Label)row.FindControl("IdPer3");
                        if (string.IsNullOrEmpty(IdPer.Text))
                        {
                            if (_representantes.Select("ID_IDENTIFICACION = '" + row.Cells[9].Text + "'").Length > 0)
                            {
                                foreach (DataRow _temp in _representantes.Rows)
                                {
                                    if (_temp["ID_IDENTIFICACION"].ToString() == row.Cells[9].Text)
                                    {
                                        _representantes.Rows.Remove(_temp);
                                        _representantes.AcceptChanges();
                                        Session["Representantes"] = _representantes;
                                        if (_representantes.Rows.Count > 0)
                                        {
                                            grdRepresentantesPrivada.DataSource = (DataTable)Session["Representantes"];
                                            grdRepresentantesPrivada.DataBind();
                                            pnlRepresentantesPrivada.Visible = true;
                                        }
                                        else
                                        {
                                            grdRepresentantesPrivada.DataSource = null;
                                            grdRepresentantesPrivada.DataBind();
                                            pnlRepresentantesPrivada.Visible = false;
                                        }
                                        return;
                                    }
                                }
                            }
                            else
                                pnlRepresentantesPrivada.Visible = false;
                        }
                        else
                        {
                            if (objPer.ConsultarPersonaAsociaRepre(IdPer.Text))
                            {
                                Mensaje.MostrarMensaje(this.Page, "Este Apoderado no se puede eliminar por que esta relacionado en un tramite");
                                return;
                            }
                            if (_representantes.Select("ID_IDENTIFICACION = '" + row.Cells[9].Text + "'").Length > 0)
                            {
                                foreach (DataRow _temp in _representantes.Rows)
                                {
                                    if (_temp["ID_IDENTIFICACION"].ToString() == row.Cells[9].Text)
                                    {
                                        _representantes.Rows.Remove(_temp);
                                        _representantes.AcceptChanges();
                                        Session["Representantes"] = _representantes;
                                        PersonaDalc objPersona = new PersonaDalc();
                                        PersonaIdentity objPersonaIdentity = new PersonaIdentity();
                                        objPersonaIdentity.PersonaId = int.Parse(IdPer.Text);
                                        objPersona.EliminarPersona(ref objPersonaIdentity);
                                        if (_representantes.Rows.Count > 0)
                                        {
                                            grdRepresentantesPrivada.DataSource = (DataTable)Session["Representantes"];
                                            grdRepresentantesPrivada.DataBind();
                                            pnlRepresentantesPrivada.Visible = true;
                                        }
                                        else
                                        {
                                            grdRepresentantesPrivada.DataSource = null;
                                            grdRepresentantesPrivada.DataBind();
                                            pnlRepresentantesPrivada.Visible = false;
                                        }
                                        return;
                                    }
                                }
                            }
                            else
                                pnlRepresentantesPrivada.Visible = false;
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "DatosPersonales :: grdRepresentantesPrivada_RowCommand -> Error Inesperado: " + exc.StackTrace);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Error efectuando la acción sobre el representante seleccionado');", true);
                }
                finally
                {
                    this.upnlFormularioPersona.Update();
                }
            }

            #endregion


        #region Actualizacion combos

            #region Combos Natural

                /// <summary>
                /// Obtiene la información de los municipios
                /// </summary>
                protected void cboDepartamentoOrigenNatural_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoDep = int.Parse(cboDepartamentoOrigenNatural.SelectedItem.Value);
                        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
                        cboMunicipioOrigenNatural.DataSource = _temp;
                        cboMunicipioOrigenNatural.DataValueField = "MUN_ID";
                        cboMunicipioOrigenNatural.DataTextField = "MUN_NOMBRE";
                        cboMunicipioOrigenNatural.DataBind();
                        cboMunicipioOrigenNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        cboMunicipioOrigenNatural.Enabled = true;
                        tbcContenedor.ActiveTabIndex = 1;
                        cboDepartamentoOrigenNatural.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboDepartamentoOrigenNatural_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de municipios');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Cargar la información de los departamentos
                /// </summary>
                protected void cboPaisNatural_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
                        if (cboPaisNatural.SelectedValue == _configuracion.IdPaisPredeterminado.ToString())
                        {
                            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                            DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
                            cboDepartamentoNatural.DataSource = _temp;
                            cboDepartamentoNatural.DataValueField = "DEP_ID";
                            cboDepartamentoNatural.DataTextField = "DEP_NOMBRE";
                            cboDepartamentoNatural.DataBind();
                            cboDepartamentoNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            CargarComboMunicipios(cboDepartamentoNatural.Items[0].Value, cboMunicipioNatural);
                            cboPaisNatural.Focus();
                        }
                        else
                        {
                            cboDepartamentoNatural.Items.Clear();
                            cboMunicipioNatural.Items.Clear();
                            cboDepartamentoNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            cboMunicipioNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));

                            cboPaisNatural.Focus();
                        }
                        tbcContenedor.ActiveTabIndex = 1;
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboPaisNatural_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de departamentos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de los municipios
                /// </summary>
                protected void cboDepartamentoNatural_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoDep = int.Parse(cboDepartamentoNatural.SelectedItem.Value);
                        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
                        cboMunicipioNatural.DataSource = _temp;
                        cboMunicipioNatural.DataValueField = "MUN_ID";
                        cboMunicipioNatural.DataTextField = "MUN_NOMBRE";
                        cboMunicipioNatural.DataBind();
                        cboMunicipioNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        tbcContenedor.ActiveTabIndex = 1;
                        cboDepartamentoNatural.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboDepartamentoNatural_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de municipios');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de los corregimientos
                /// </summary>
                protected void cboMunicipioNatural_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoMun = int.Parse(cboMunicipioNatural.SelectedItem.Value);
                        DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
                        cboCorregimientoNatural.DataSource = _temp;
                        cboCorregimientoNatural.DataValueField = "COR_ID";
                        cboCorregimientoNatural.DataTextField = "COR_NOMBRE";
                        cboCorregimientoNatural.DataBind();
                        cboCorregimientoNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        cboMunicipioNatural.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboMunicipioNatural_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de corregimientos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de las veredas
                /// </summary>
                protected void cboCorregimientoNatural_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoCor = int.Parse(cboCorregimientoNatural.SelectedItem.Value);
                        int _codigoMun = int.Parse(cboMunicipioNatural.SelectedItem.Value);
                        DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
                        cboVeredaNatural.DataSource = _temp;
                        cboVeredaNatural.DataValueField = "VER_ID";
                        cboVeredaNatural.DataTextField = "VER_NOMBRE";
                        cboVeredaNatural.DataBind();
                        cboVeredaNatural.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        cboCorregimientoNatural.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboCorregimientoNatural_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de veredas');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de departamentos
                /// </summary>
                protected void cboPaisCorrespondencia_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
                        if (cboPaisCorrespondencia.SelectedValue == _configuracion.IdPaisPredeterminado.ToString())
                        {
                            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                            DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
                            cboDepartamentoCorrespondencia.DataSource = _temp;
                            cboDepartamentoCorrespondencia.DataValueField = "DEP_ID";
                            cboDepartamentoCorrespondencia.DataTextField = "DEP_NOMBRE";
                            cboDepartamentoCorrespondencia.DataBind();
                            cboDepartamentoCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            CargarComboMunicipios(cboDepartamentoCorrespondencia.Items[0].Value, cboMunicipioCorrespondencia);
                            cboPaisCorrespondencia.Focus();
                        }
                        else
                        {
                            cboDepartamentoCorrespondencia.Items.Clear();
                            cboMunicipioCorrespondencia.Items.Clear();
                            cboDepartamentoCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            cboMunicipioCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));

                            cboPaisCorrespondencia.Focus();
                        }
                        tbcContenedor.ActiveTabIndex = 1;
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboPaisCorrespondencia_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de departamentos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }


                /// <summary>
                /// Carga la información de los departamentos
                /// </summary>
                protected void cboDepartamentoCorrespondencia_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoDep = int.Parse(cboDepartamentoCorrespondencia.SelectedItem.Value);
                        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
                        cboMunicipioCorrespondencia.DataSource = _temp;
                        cboMunicipioCorrespondencia.DataValueField = "MUN_ID";
                        cboMunicipioCorrespondencia.DataTextField = "MUN_NOMBRE";
                        cboMunicipioCorrespondencia.DataBind();
                        cboMunicipioCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        tbcContenedor.ActiveTabIndex = 1;
                        cboDepartamentoCorrespondencia.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboDepartamentoCorrespondencia_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de municipios');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de los municipios
                /// </summary>
                protected void cboDepartamentoCorrespondenciaPrivada_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoDep = int.Parse(cboDepartamentoCorrespondenciaPrivada.SelectedItem.Value);
                        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
                        cboMunicipioCorrespondenciaPrivada.DataSource = _temp;
                        cboMunicipioCorrespondenciaPrivada.DataValueField = "MUN_ID";
                        cboMunicipioCorrespondenciaPrivada.DataTextField = "MUN_NOMBRE";
                        cboMunicipioCorrespondenciaPrivada.DataBind();
                        cboMunicipioCorrespondenciaPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        tbcContenedor.ActiveTabIndex = 3;
                        cboDepartamentoCorrespondenciaPrivada.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboDepartamentoCorrespondenciaPrivada_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de municipios');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la informacion de los municipios
                /// </summary>
                protected void cboMunicipioCorrespondencia_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoMun = int.Parse(cboMunicipioCorrespondencia.SelectedItem.Value);
                        DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
                        cboCorregimientoCorrespondencia.DataSource = _temp;
                        cboCorregimientoCorrespondencia.DataValueField = "COR_ID";
                        cboCorregimientoCorrespondencia.DataTextField = "COR_NOMBRE";
                        cboCorregimientoCorrespondencia.DataBind();
                        cboCorregimientoCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        cboMunicipioCorrespondencia.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboMunicipioCorrespondencia_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de municipios');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Cargar la información de corregimientos
                /// </summary>
                protected void cboMunicipioCorrespondenciaPrivada_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoMun = int.Parse(cboMunicipioCorrespondenciaPrivada.SelectedItem.Value);
                        DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
                        cboCorregimientoCorrespondenciaPrivada.DataSource = _temp;
                        cboCorregimientoCorrespondenciaPrivada.DataValueField = "COR_ID";
                        cboCorregimientoCorrespondenciaPrivada.DataTextField = "COR_NOMBRE";
                        cboCorregimientoCorrespondenciaPrivada.DataBind();
                        cboCorregimientoCorrespondenciaPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        cboMunicipioCorrespondenciaPrivada.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboMunicipioCorrespondenciaPrivada_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de corregimientos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }


                /// <summary>
                /// Cargar el listado de veredas
                /// </summary>
                protected void cboCorregimientoCorrespondencia_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoCor = int.Parse(cboCorregimientoCorrespondencia.SelectedItem.Value);
                        int _codigoMun = int.Parse(cboMunicipioCorrespondencia.SelectedItem.Value);
                        DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
                        cboVeredaCorrespondencia.DataSource = _temp;
                        cboVeredaCorrespondencia.DataValueField = "VER_ID";
                        cboVeredaCorrespondencia.DataTextField = "VER_NOMBRE";
                        cboVeredaCorrespondencia.DataBind();
                        cboVeredaCorrespondencia.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        cboCorregimientoCorrespondencia.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboCorregimientoCorrespondencia_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de veredas');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }

                }
    
                /// <summary>
                /// Cargar la información de corregimientos
                /// </summary>
                protected void cboCorregimientoCorrespondenciaPrivada_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoCor = int.Parse(cboCorregimientoCorrespondenciaPrivada.SelectedItem.Value);
                        int _codigoMun = int.Parse(cboMunicipioCorrespondenciaPrivada.SelectedItem.Value);
                        DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
                        cboVeredaCorrespondenciaPrivada.DataSource = _temp;
                        cboVeredaCorrespondenciaPrivada.DataValueField = "VER_ID";
                        cboVeredaCorrespondenciaPrivada.DataTextField = "VER_NOMBRE";
                        cboVeredaCorrespondenciaPrivada.DataBind();
                        cboVeredaCorrespondenciaPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        cboCorregimientoCorrespondenciaPrivada.Focus();
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboCorregimientoCorrespondenciaPrivada_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de corregimientos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }

                }

            #endregion


            #region Combos Juridica Publica

                /// <summary>
                /// Carga la información de depártamentos
                /// </summary>
                protected void cboPaisJuridica_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisJuridica_SelectedIndexChanged.Inicio");
                        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
                        if (cboPaisJuridica.SelectedValue == _configuracion.IdPaisPredeterminado.ToString())
                        {
                            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                            DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
                            cboDepartamentoJuridica.DataSource = _temp;
                            cboDepartamentoJuridica.DataValueField = "DEP_ID";
                            cboDepartamentoJuridica.DataTextField = "DEP_NOMBRE";
                            cboDepartamentoJuridica.DataBind();
                            cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            CargarComboMunicipios(cboDepartamentoJuridica.Items[0].Value, cboMunicipioJuridica);
                        }
                        else
                        {
                            cboDepartamentoJuridica.Items.Clear();
                            cboMunicipioJuridica.Items.Clear();
                            cboDepartamentoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            cboMunicipioJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                        tbcContenedor.ActiveTabIndex = 2;
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboPaisJuridica_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de departamentos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de los municipios
                /// </summary>
                protected void cboDepartamentoJuridica_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoJuridica_SelectedIndexChanged.Inicio");
                        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoDep = int.Parse(cboDepartamentoJuridica.SelectedItem.Value);
                        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
                        cboMunicipioJuridica.DataSource = _temp;
                        cboMunicipioJuridica.DataValueField = "MUN_ID";
                        cboMunicipioJuridica.DataTextField = "MUN_NOMBRE";
                        cboMunicipioJuridica.DataBind();
                        cboMunicipioJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        tbcContenedor.ActiveTabIndex = 2;
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboPaisJuridica_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de municipios');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de corregimientos
                /// </summary>
                protected void cboMunicipioJuridica_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioJuridica_SelectedIndexChanged.Inicio");
                        SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoMun = int.Parse(cboMunicipioJuridica.SelectedItem.Value);
                        DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
                        cboCorregimientoJuridica.DataSource = _temp;
                        cboCorregimientoJuridica.DataValueField = "COR_ID";
                        cboCorregimientoJuridica.DataTextField = "COR_NOMBRE";
                        cboCorregimientoJuridica.DataBind();
                        cboCorregimientoJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboMunicipioJuridica_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de corregimientos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de las veredas
                /// </summary>
                protected void cboCorregimientoJuridica_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoJuridica_SelectedIndexChanged.Inicio");
                        SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoCor = int.Parse(cboCorregimientoJuridica.SelectedItem.Value);
                        int _codigoMun = int.Parse(cboMunicipioJuridica.SelectedItem.Value);
                        DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
                        cboVeredaJuridica.DataSource = _temp;
                        cboVeredaJuridica.DataValueField = "VER_ID";
                        cboVeredaJuridica.DataTextField = "VER_NOMBRE";
                        cboVeredaJuridica.DataBind();
                        cboVeredaJuridica.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboCorregimientoJuridica_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de veredas');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

            #endregion


            #region Combos Juridica Privada

                /// <summary>
                /// Cargar el listado de departamentos
                /// </summary>
                protected void cboPaisPrivada_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboPaisPrivada_SelectedIndexChanged.Inicio");
                        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
                        if (cboPaisPrivada.SelectedValue == _configuracion.IdPaisPredeterminado.ToString())
                        {
                            SILPA.LogicaNegocio.Generico.Listas _listaDepartamentos = new SILPA.LogicaNegocio.Generico.Listas();
                            DataSet _temp = _listaDepartamentos.ListarDepartamentos(_configuracion.IdPaisPredeterminado);
                            cboDepartamentoPrivada.DataSource = _temp;
                            cboDepartamentoPrivada.DataValueField = "DEP_ID";
                            cboDepartamentoPrivada.DataTextField = "DEP_NOMBRE";
                            cboDepartamentoPrivada.DataBind();
                            cboDepartamentoPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            CargarComboMunicipios(cboDepartamentoPrivada.Items[0].Value, cboMunicipioPrivada);
                        }
                        else
                        {
                            cboDepartamentoPrivada.Items.Clear();
                            cboMunicipioPrivada.Items.Clear();
                            cboDepartamentoPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                            cboMunicipioPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        }
                        tbcContenedor.ActiveTabIndex = 3;
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboPaisPrivada_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de departamentos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Cargar el listado de municipios
                /// </summary>
                protected void cboDepartamentoPrivada_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboDepartamentoPrivada_SelectedIndexChanged.Inicio");
                        SILPA.LogicaNegocio.Generico.Listas _listaMunicipios = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoDep = int.Parse(cboDepartamentoPrivada.SelectedItem.Value);
                        DataSet _temp = _listaMunicipios.ListaMunicipios(null, _codigoDep, null);
                        cboMunicipioPrivada.DataSource = _temp;
                        cboMunicipioPrivada.DataValueField = "MUN_ID";
                        cboMunicipioPrivada.DataTextField = "MUN_NOMBRE";
                        cboMunicipioPrivada.DataBind();
                        cboMunicipioPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        tbcContenedor.ActiveTabIndex = 3;
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboDepartamentoPrivada_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de municipios');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Cargar la información de corregimientos
                /// </summary>
                protected void cboMunicipioPrivada_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboMunicipioPrivada_SelectedIndexChanged.Inicio");
                        SILPA.LogicaNegocio.Generico.Listas _listaCorregimientos = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoMun = int.Parse(cboMunicipioPrivada.SelectedItem.Value);
                        DataSet _temp = _listaCorregimientos.ListarCorregimientos(_codigoMun, null);
                        cboCorregimientoPrivada.DataSource = _temp;
                        cboCorregimientoPrivada.DataValueField = "COR_ID";
                        cboCorregimientoPrivada.DataTextField = "COR_NOMBRE";
                        cboCorregimientoPrivada.DataBind();
                        cboCorregimientoPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                        tbcContenedor.ActiveTabIndex = 3;
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboMunicipioPrivada_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de corregimientos');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

                /// <summary>
                /// Carga la información de veredas
                /// </summary>
                protected void cboCorregimientoPrivada_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        SMLog.Escribir(Severidad.Critico, Page.AppRelativeVirtualPath.ToString() + ".cboCorregimientoPrivada_SelectedIndexChanged.Inicio");
                        SILPA.LogicaNegocio.Generico.Listas _listaVeredas = new SILPA.LogicaNegocio.Generico.Listas();
                        int _codigoCor = int.Parse(cboCorregimientoPrivada.SelectedItem.Value);
                        int _codigoMun = int.Parse(cboMunicipioPrivada.SelectedItem.Value);
                        DataSet _temp = _listaVeredas.ListarVeredas(_codigoMun, _codigoCor, null);
                        cboVeredaPrivada.DataSource = _temp;
                        cboVeredaPrivada.DataValueField = "VER_ID";
                        cboVeredaPrivada.DataTextField = "VER_NOMBRE";
                        cboVeredaPrivada.DataBind();
                        cboVeredaPrivada.Items.Insert(0, new ListItem("Seleccione...", "-1"));
                    }
                    catch (Exception ex)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "DatosPersonales :: cboCorregimientoPrivada_SelectedIndexChanged -> Error Inesperado: " + ex.StackTrace);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Se presento un problema cargando la información de veredas');", true);
                    }
                    finally
                    {
                        this.upnlFormularioPersona.Update();
                    }
                }

            #endregion   

        #endregion

    #endregion
    
}
