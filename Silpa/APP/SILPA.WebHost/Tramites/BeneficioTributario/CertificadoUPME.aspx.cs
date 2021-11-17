using SILPA.AccesoDatos.BPMProcess;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME;
using SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

public partial class Tramites_BeneficioTributario_CertificadoUPME : System.Web.UI.Page
{
    /// <summary>
    /// Listado de permisos de la solicitud
    /// </summary>
    private int SolicitanteID
    {
        get
        {
            return (int)ViewState["_intSolicitanteID"];
        }
        set
        {
            ViewState["_intSolicitanteID"] = value;
        }
    }

    public List<proyectos> certificados { get { return (List<proyectos>)ViewState["certificados"]; } set { ViewState["certificados"] = value; } }
    

    private void LimpiarDatosCertificado()
    {
        certificados = null;
        this.txtNombreProyectoData.Text = string.Empty;
        this.lblUbicacionGeograficaData.Text = string.Empty;
        this.txtEmisionesCO2Data.Text = string.Empty;
        this.txtValorTotalInversionData.Text = string.Empty;
        this.txtValorIVAData.Text = string.Empty;
        this.grvBienesCertificacion.DataSource = null;
        this.grvBienesCertificacion.DataBind();
        this.grvServiciosCertificacion.DataSource = null;
        this.grvServiciosCertificacion.DataBind();
        this.lblNumeroCertificadoData.Text = string.Empty;
        this.txtNumeroCertificadoData.Text = string.Empty;
        this.txtEnergiaAnualGeneraSistemaData.Text = string.Empty;
        this.btnSolicitarCertificado.Enabled = false;
        this.txtFuenteNoConvencionalUtilizarData.Text = string.Empty;
        this.ddlTipoCertificacionData.SelectedIndex = 0;
        this.txtNumeroReferencia.Text = string.Empty;
        this.ddlFuenteConvencionalSustituirData.SelectedIndex = 0;
        this.ddlSubfuenteConvencionalSustituirData.SelectedIndex = 0;
        //this.txtCalculoANLAData.Text = string.Empty;

    }
    
    private ValoresIdentity CargarValores(int id, string grupo, string valor, int orden, Byte[] archivo)
    {
        ValoresIdentity objValores = new ValoresIdentity();
        objValores.Id = id;
        objValores.Grupo = grupo;
        objValores.Valor = valor;
        objValores.Orden = orden;
        objValores.Archivo = archivo;
        return objValores;
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

        this.SolicitanteID = Convert.ToInt32(Session["IDForToken"]);

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

    #region Manejo Errores

    /// <summary>
    /// Evento que cierra modal y redirecciona a pagina de listado
    /// </summary>
    protected void cmdAceptarError_Click(object sender, EventArgs e)
    {
        try
        {
            //Limpiar mensaje modal
            this.ltlErrorProceso.Text = "";

            //Actualizar modal
            this.upnlError.Update();

            this.mpeError.Hide();
        }
        catch (Exception exc)
        {
            //MOstrar mensaje de error en pantalla
            this.MostrarMensajeError("Se presento error cerrando modal de error.");
        }
    }

    protected void MostrarMensajeError(string mensaje)
    {
        //Mostrar mensaje de error
        this.ltlErrorProceso.Text = mensaje;

        //Actualizar modal y mostrarlo
        this.upnlError.Update();
        this.mpeError.Show();
    }
    protected void MostrarMensajeOK(string mensaje)
    {
        //Mostrar mensaje de error
        this.ltlMensajeOk.Text = mensaje;

        //Actualizar modal y mostrarlo
        this.upnlMensajeOK.Update();
        this.mpeMensajeOk.Show();
    }

    #endregion

    #region eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.SolicitanteID = 38648;
            //this.cargarPagina();
            if (this.ValidacionToken() == false)
             {
                 Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
             }
             else
             {
                 this.cargarPagina();
             }
        }
    }

    private void cargarPagina()
    {
        CertificadoUPME clsCertificadoUPME = new CertificadoUPME();
        Utilidades.LlenarComboLista(clsCertificadoUPME.listaFuenteConvencional(), this.ddlFuenteConvencionalSustituirData, "descripcion", "fuetenConvencionalID", true);
        Utilidades.LlenarComboVacio(this.ddlSubfuenteConvencionalSustituirData);
    }
    protected void btnBuscarCertificado_Click(object sender, EventArgs e)
    {
        this.btnSolicitarCertificado.Enabled = false;
        // se realizara la consulta del certificado
        if (this.txtNumeroCertificadoFind.Text.Trim() != string.Empty)
        {
            CertificadoUPME _objCertificadoUPME = new CertificadoUPME();
            certificados = _objCertificadoUPME.ConsultaCertificado(this.txtNumeroCertificadoFind.Text);
            if (certificados != null && certificados.Count() > 0)
            {
                PersonaDalc per = new PersonaDalc();
                PersonaIdentity p = new PersonaIdentity();
                p = per.BuscarPersonaByUserId(this.SolicitanteID.ToString());
                // validamos si el usuario del certificado corresponde al usuario que se encuentra logeado
                if (certificados[0].objusuario.username == p.NumeroIdentificacion)
                {
                    //cargamos los datos del certificado
                    this.txtNombreProyectoData.Text = certificados[0].nombre;
                    this.lblCoordenadasData.Text = string.Format("Lat:{0}, Lon:{1}", certificados[0].objGeneralidadesproyecto._objUbicaciones.latitud, certificados[0].objGeneralidadesproyecto._objUbicaciones.longitud);
                    this.lblUbicacionGeograficaData.Text = string.Format("Municipio: {0}, Departamento: {1}", certificados[0].objmunicipio.nombre, certificados[0].objmunicipio.objdepartamento.nombre);
                    this.lblEtapaData.Text = certificados[0].etapa;
                    this.txtEmisionesCO2Data.Text = (certificados[0].objGeneralidadesproyecto._objdatosambientales.emisiones_generadas_sin - certificados[0].objGeneralidadesproyecto._objdatosambientales.emisiones_generadas_con).ToString();
                    this.txtValorTotalInversionData.Text = ((certificados[0].lstServicios != null?certificados[0].lstServicios.Sum(x => x.valor_total):0) + (certificados[0].lstBienes != null ? certificados[0].lstBienes.Sum(x => x.valor_total) : 0)).ToString("C");
                    this.txtValorIVAData.Text = ((certificados[0].lstServicios != null? certificados[0].lstServicios.Sum(x => x.iva):0) + (certificados[0].lstBienes != null? certificados[0].lstBienes.Sum(x => x.iva):0)).ToString("C");
                    this.grvBienesCertificacion.DataSource = certificados[0].lstBienes;
                    this.grvBienesCertificacion.DataBind();
                    this.grvServiciosCertificacion.DataSource = certificados[0].lstServicios;
                    this.grvServiciosCertificacion.DataBind();
                    this.lblNumeroCertificadoData.Text = certificados[0].numero_certificado;
                    this.txtNumeroCertificadoData.Text = certificados[0].numero_certificado;
                    this.txtEnergiaAnualGeneraSistemaData.Text = certificados[0].objGeneralidadesproyecto._objdatostecnicos.energia_media.ToString();
                    this.btnSolicitarCertificado.Enabled = true;
                    this.lblTIpoIdentificacionData.Text = certificados[0].objusuario.lstSolicitantePrincipal.FirstOrDefault().tipo_identificacion;
                    this.txtFuenteNoConvencionalUtilizarData.Text = certificados[0].objGeneralidadesproyecto.tipo_fnce;
                    this.lblObjetoProyectoFinalidadData.Text = string.Format("<a href='{0}' target='_blank'>Ver archivo</a> <br/>", certificados[0].objGeneralidadesproyecto.descripcion_proyecto); ;
                    this.lblUsuariosSecundariosData.Text = _objCertificadoUPME.crearListaSolicitantesSecundatios(certificados[0].lstSolicitanteSecundario);
                    this.mpeDatosCertificado.Show();
                }
                else
                {
                    MostrarMensajeError("No se encontraron certificados asociados a su numero de identificación");
                    LimpiarDatosCertificado();
                }
            }
            else
            {
                MostrarMensajeError("No se encontraron certificados");
                LimpiarDatosCertificado();
            }
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimpiarDatosCertificado();
        this.mpeDatosCertificado.Hide();
    }
    protected void btnAceptarMensajeOk_Click(object sender, EventArgs e)
    {
        try
        {
            //Limpiar mensaje modal
            this.ltlMensajeOk.Text = "";

            //Actualizar modal
            this.upnlMensajeOK.Update();

            this.mpeMensajeOk.Hide();
        }
        catch (Exception exc)
        {
            //MOstrar mensaje de error en pantalla
            this.MostrarMensajeError("Se presento error cerrando modal de error.");
        }
    }
    protected void ddlTipoCertificacionData_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlTipoCertificacionData.SelectedValue == "1")
        {
            this.tdIVA1.Visible = true;
            this.tdIVA2.Visible = true;
            
        }
        else
        {
            this.tdIVA1.Visible = false;
            this.tdIVA2.Visible = false;
            
        }
    }
    protected void btnSolicitarCertificado_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            CertificadoUPME clsCertificadoUPME = new CertificadoUPME();
            string numerosilpa;
            // se almacenan los datos del certificado encontrado y se crea el proceso en VITAL.
            List<ValoresIdentity> objValoresList = new List<ValoresIdentity>();
            objValoresList.Add(CargarValores(1, "Bas", this.lblNumeroCertificadoData.Text, 1, new Byte[1]));
            objValoresList.Add(CargarValores(2, "Bas", this.SolicitanteID.ToString(), 1, new Byte[1]));
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer serializador = new XmlSerializer(typeof(List<ValoresIdentity>));
            serializador.Serialize(memoryStream, objValoresList);
            string xml = System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
            try
            {
                if (this.SolicitanteID > 0)
                {
                    numerosilpa = clsCertificadoUPME.CrearProcesoCertificado("Softmanagement", 333, this.SolicitanteID, xml);
                    if (numerosilpa.Contains("ERROR"))
                    {
                        MostrarMensajeError("Se generó un error al intentar generar el certificado");
                        return;
                    }
                    else
                    {
                        // se almacenan los datos del certificado en el repositorio de datos.
                        // asignamos los valores al objeto para su posterior almacenamiento.
                        CertificadoUPMEEntity objCertificadoUPMEEntity = new CertificadoUPMEEntity();

                        objCertificadoUPMEEntity.numeroCertificado = certificados[0].numero_certificado;
                        objCertificadoUPMEEntity.tipoCertificacion = this.ddlTipoCertificacionData.SelectedValue;
                        objCertificadoUPMEEntity.nombreProyecto = certificados[0].nombre;
                        objCertificadoUPMEEntity.ubicacionGeografia = string.Format("Municipio: {0}, Departamento: {1}", certificados[0].objmunicipio.nombre, certificados[0].objmunicipio.objdepartamento.nombre);
                        objCertificadoUPMEEntity.departamento = certificados[0].objmunicipio.objdepartamento.nombre;
                        objCertificadoUPMEEntity.municipio = certificados[0].objmunicipio.nombre;
                        objCertificadoUPMEEntity.energiaAnualGenerada = Convert.ToDecimal(this.txtEnergiaAnualGeneraSistemaData.Text);
                        objCertificadoUPMEEntity.fuenteNoConvencional = certificados[0].objGeneralidadesproyecto.tipo_fnce;
                        objCertificadoUPMEEntity.fuenteConvencionalSustituirID = Convert.ToInt32(this.ddlFuenteConvencionalSustituirData.SelectedValue);
                        objCertificadoUPMEEntity.subFuenteConvencionalSustituirID = Convert.ToInt32(this.ddlSubfuenteConvencionalSustituirData.SelectedValue);
                        objCertificadoUPMEEntity.emisionesCO2 = (certificados[0].objGeneralidadesproyecto._objdatosambientales.emisiones_generadas_sin - certificados[0].objGeneralidadesproyecto._objdatosambientales.emisiones_generadas_con).Value;
                        //objCertificadoUPMEEntity.calculoANLA = Convert.ToDecimal(this.txtCalculoANLAData.Text);
                        objCertificadoUPMEEntity.valorTotalInversion = ((certificados[0].lstServicios != null ? certificados[0].lstServicios.Sum(x => x.valor_total):0) + (certificados[0].lstBienes != null ? certificados[0].lstBienes.Sum(x => x.valor_total):0));
                        objCertificadoUPMEEntity.valorIVA = this.ddlTipoCertificacionData.SelectedValue == "1" ? ((certificados[0].lstServicios != null ? certificados[0].lstServicios.Sum(x => x.iva):0) + (certificados[0].lstBienes != null ? certificados[0].lstBienes.Sum(x => x.iva):0)):0;
                        objCertificadoUPMEEntity.latitud = certificados[0].objGeneralidadesproyecto._objUbicaciones.latitud;
                        objCertificadoUPMEEntity.longitud = certificados[0].objGeneralidadesproyecto._objUbicaciones.longitud;
                        objCertificadoUPMEEntity.etapa = certificados[0].etapa;
                        objCertificadoUPMEEntity.tipoIdentificacion = certificados[0].objusuario.lstSolicitantePrincipal.FirstOrDefault().tipo_identificacion;
                        

                        foreach (var bien in certificados[0].lstBienes)
                        {
                            objCertificadoUPMEEntity.lstBienes.Add(new bienesEntity
                            {
                                elemento = bien.elemento,
                                subpartida_arancelaria = bien.subpartida_arancelaria,
                                cantidad = bien.cantidad.ToString(),
                                marca = bien.marca,
                                modelo = bien.modelo,
                                fabricante = bien.fabricante,
                                proveedor = bien.proveedor,
                                funcion = bien.funcion,
                                valor_total = bien.valor_total,
                                iva = bien.iva
                            });
                        }
                        foreach (var servicio in certificados[0].lstServicios)
                        {
                            objCertificadoUPMEEntity.lstServicios.Add(new serviciosEntity
                            {
                                servicio = servicio.servicio,
                                proveedor = servicio.proveedor,
                                alcance = servicio.alcance,
                                valor_total = servicio.valor_total,
                                iva = servicio.iva
                            });
                        }
                        foreach (var solicitanteSecundario in certificados[0].lstSolicitanteSecundario)
                        {
                            objCertificadoUPMEEntity.lstSolicitanteSecundario.Add(new solicitanteEntity
                            {
                               nombre = solicitanteSecundario.nombre,
                               sectorProductivo = solicitanteSecundario.objCodigoCIIU.objSectorProductivo.descripcion,
                               codigoCIIU = solicitanteSecundario.objCodigoCIIU.clase,
                               tipoIdentificacion = solicitanteSecundario.tipo_identificacion,
                               identificacion = solicitanteSecundario.identificacion,
                               telefono = solicitanteSecundario.telefono,
                               direccion = solicitanteSecundario.direccion,
                               domicilio = solicitanteSecundario.objMunicipios.nombre,
                               nombreContacto = solicitanteSecundario.nombre_contacto,
                               emailContacto = certificados[0].objusuario.email,
                               telefonoContacto = solicitanteSecundario.telefono_contacto,
                               TipoSolicitante = enumTipoSolicitante.secundario
                            });
                        }
                        
                            objCertificadoUPMEEntity.solicitantePrincial = new solicitanteEntity {
                                nombre = certificados[0].objusuario.first_name,
                                sectorProductivo = certificados[0].objusuario.lstSolicitantePrincipal.First().objCodigoCIIU.objSectorProductivo.descripcion,
                                codigoCIIU = certificados[0].objusuario.lstSolicitantePrincipal.First().objCodigoCIIU.clase,
                                tipoIdentificacion = certificados[0].objusuario.lstSolicitantePrincipal.First().tipo_identificacion,
                                identificacion = certificados[0].objusuario.username,
                                telefono = certificados[0].objusuario.lstSolicitantePrincipal.First().telefono,
                                domicilio = certificados[0].objusuario.lstSolicitantePrincipal.First().objMunicipios.nombre,
                                direccion = certificados[0].objusuario.lstSolicitantePrincipal.First().direccion,
                                nombreContacto = certificados[0].objusuario.lstSolicitantePrincipal.First().nombre_contacto,
                                emailContacto = certificados[0].objusuario.email,
                                telefonoContacto = certificados[0].objusuario.lstSolicitantePrincipal.First().telefono_contacto,
                                TipoSolicitante = enumTipoSolicitante.principal
                            };
                        
                        // asignamos el numero vital asociado al proceso de generacion del certificado
                        objCertificadoUPMEEntity.numeroSILPA = numerosilpa;
                        clsCertificadoUPME.GuardarSolicitudCertificado(ref objCertificadoUPMEEntity);
                        string rutaCarpetaCertificado = (new RadicacionDocumento()).ObtenerPathDocumentosNumeroVital(numerosilpa);

                        if (certificados[0].objGeneralidadesproyecto.descripcion_proyecto != string.Empty)
                        {
                            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(certificados[0].objGeneralidadesproyecto.descripcion_proyecto);

                            webRequest.Method = "GET";
                            webRequest.ContentType = "application/pdf; charset=utf-8";
                            webRequest.Timeout = 60000;

                            using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                            {
                                BinaryReader bin = new BinaryReader(response.GetResponseStream());

                                byte[] buffer = bin.ReadBytes((Int32)response.ContentLength);

                                using (Stream writer = File.Create(rutaCarpetaCertificado + certificados[0].objGeneralidadesproyecto.descripcion_proyecto.Split('/').Last()))
                                {
                                    writer.Write(buffer, 0, buffer.Length);
                                    writer.Flush();
                                }
                            }

                            clsCertificadoUPME.guardarRutaDescripcionProyecto(rutaCarpetaCertificado + certificados[0].objGeneralidadesproyecto.descripcion_proyecto.Split('/').Last(), objCertificadoUPMEEntity.certificadoID);
                        }

                        
                        if (this.fuplAdjuntarSoportePago.PostedFile != null)
                        {
                            //Almacenar archivo
                            this.fuplAdjuntarSoportePago.SaveAs(rutaCarpetaCertificado + this.fuplAdjuntarSoportePago.PostedFile.FileName);
                            clsCertificadoUPME.guardarSoportePago(rutaCarpetaCertificado + this.fuplAdjuntarSoportePago.PostedFile.FileName, objCertificadoUPMEEntity.certificadoID, this.txtNumeroReferencia.Text.Trim());
                        }


                        // se genera el archivo pdf de la solicitud
                        clsCertificadoUPME.GenerarPDFSolicitud(objCertificadoUPMEEntity.certificadoID, rutaCarpetaCertificado);
                        clsCertificadoUPME.GenerarArchivoBienesCSV(objCertificadoUPMEEntity.lstBienes, rutaCarpetaCertificado);
                        clsCertificadoUPME.GenerarArchivoServiciosCSV(objCertificadoUPMEEntity.lstServicios, rutaCarpetaCertificado);
                        mpeDatosCertificado.Hide();
                        LimpiarDatosCertificado();

                        MostrarMensajeOK(string.Format(@"Proceso realizado correctamente <br /><br />Su solicitud de generación de certificado se ha registrado con exito. Número VITAL: <br /><strong>{0}</strong> <br /> fecha de registro {1} de {2} de {3} <br /> <br />Su solicitud será gestionada por <strong>Autoridad Nacional de Licencias Ambientales </strong>
                            <br />Le informamos que todas las solicitudes enviadas por VITAL serán radicadas en los horarios establecidos de atención al ciudadano
                            <br />(Lunes a Viernes de 8:00 AM a 4:00 PM, en días hábiles). 
                            <br />En caso de que su solicitud se envíe en horarios no hábiles, se dará trámite de radicación el día hábil siguiente de la recepción. 
                            <br />Una vez radicada su solicitud, le será enviado el número al correo electrónico registrado.
                            ",
                        numerosilpa,DateTime.Now.ToString("dddd dd"), DateTime.Now.ToString("MMMM"), DateTime.Now.ToString("yyyy HH:mm:ss tt")));
                    }


                }
            }
            catch (Exception ex)
            {
                // error al crear el proceso de certificado en VITAL.
                MostrarMensajeError("Se genero un error al intentar generar el certificado. Error: " + ex.Message);
            }
        }

    }
    #endregion eventos

    #region CustomValidator
    /// <summary>
    /// Verifica que se adjunte archivo de ensayo de laboratorio
    /// </summary>
    protected void cvfuplAdjuntarSoportePago_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (!this.fuplAdjuntarSoportePago.IsUploading)
            {
                //Verificar que se haya anexado archivo
                if (this.fuplAdjuntarSoportePago.PostedFile == null || !this.fuplAdjuntarSoportePago.HasFile)
                {
                    args.IsValid = false;
                    MostrarMensajeError("Debe adjuntar Soporte de pago");
                }
            }
        }
        catch (Exception exc)
        {
            //Genera error y muestra mensaje
            this.MostrarMensajeError("Se presento error validando archivo archivo ensayo laboratorio");
            args.IsValid = false;
        }
    }
    #endregion CustomValidator
    protected void ddlFuenteConvencionalSustituirData_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlFuenteConvencionalSustituirData.SelectedValue != string.Empty)
        {
            CertificadoUPME clsCertificadoUPME = new CertificadoUPME();
            Utilidades.LlenarComboLista(clsCertificadoUPME.listaSubfuenteConvencional(Convert.ToInt32(this.ddlFuenteConvencionalSustituirData.SelectedValue)), this.ddlSubfuenteConvencionalSustituirData, "descripcion", "subFuenteConvencionalSustituirID", true);
        }
    }
}