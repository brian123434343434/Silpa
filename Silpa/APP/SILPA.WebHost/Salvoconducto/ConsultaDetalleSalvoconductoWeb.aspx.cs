using SILPA.AccesoDatos.Salvoconducto;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SILPA.LogicaNegocio.Aprovechamiento;
using SILPA.AccesoDatos.Generico;


public partial class Salvoconducto_ConsultaDetalleSalvoconductoWeb : System.Web.UI.Page
{
    private String SalvoconductoID;
    public SalvoconductoNewIdentity vSalvoconducto { get { return (SalvoconductoNewIdentity)ViewState["vSalvoconducto"]; } set { ViewState["vSalvoconducto"] = value; } }
    public List<RutaEntity> LstRutaEntity { get { return (List<RutaEntity>)ViewState["LstRutaEntity"]; } set { ViewState["LstRutaEntity"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        #region obtengo el valor de cada uno de los parámetros [modo: encriptado]
        string encryptedQueryString = Request.QueryString["enc"].Replace(" ", "+");
        string decryptedQueryString = Utilidades.Decrypt(encryptedQueryString);
        string[] QueryStringParameters = decryptedQueryString.Split(new char[] { '&' });

        SalvoconductoID = QueryStringParameters[0].Substring(QueryStringParameters[0].IndexOf("=") + 1);
        #endregion

        #region carga inro_odtial del formulario [modo: encriptado]
        if (!Page.IsPostBack)
        {
            #region deshabilitar boton atras navegador
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
            #endregion
                #region recepción de parámetros [modo: encriptado]
                if (!String.IsNullOrEmpty(SalvoconductoID))
                {
                    //ejecutar métodos normales de tu formulario
                    CargarSalvoconducto(Convert.ToInt32(SalvoconductoID));
                }
                else
                {
                    #region redirijo al usuario a la página de Login
                    string _strPagina = "window.top.location.href='../Utilitario/MensajeValidacion.aspx'";
                    Utilidades.AlertWindow(this.Page, "", _strPagina, (int)Utilidades._modo_apertura_formulario.ejecutarScript);
                    #endregion
                }
                #endregion
        }
        #endregion
    }

    public void CargarSalvoconducto(int SalvoconductoID)
    {
        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        vSalvoconducto = clsSalvoconductoNew.ConsultaSalvoconductoXSalvoconductoId(SalvoconductoID);
        HtmlAnchor lnk;
        Label lbl;
        string DatoPersonalEncriptado = "#########";
        //Expedicion
        this.LblFechaExp.Text = "";
        this.LblVigenciaDesde.Text = "";
        this.LblVigenciaHasta.Text = "";
        PersonaDalc per = new PersonaDalc();
        PersonaIdentity p = new PersonaIdentity();
        Boolean Sn_SalvoconductoPrecargado = false;
        this.TblBloqueoSalvoconducto.Visible = false;

        //jmartinez se realiza control para los salvocnductos precargados
        if (vSalvoconducto.AutoridadCargueID > 0)
        {
            Sn_SalvoconductoPrecargado = true;
        }

        if (vSalvoconducto.EstadoID == 4 && vSalvoconducto.IdTipoBloqueo > 0) //BLOQUEADOS
        {
            this.BRBloqueo.Visible = true;
            this.TblBloqueoSalvoconducto.Visible = true;
            SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto _listaEstadosSeries = new SILPA.LogicaNegocio.Salvoconducto.AsignarNumeracionSalvoconducto();
            Utilidades.LlenarComboTabla(_listaEstadosSeries.ObtenerMotivoBloqueo(), CboMotivoBloqueo, "DESCRIPCION", "ID_TIPO_BLOQUEO", true);
            this.CboMotivoBloqueo.Enabled = false;
            this.CboMotivoBloqueo.SelectedValue = vSalvoconducto.IdTipoBloqueo.ToString(); //seteo el tipo de bloqueo 
        }

        if (vSalvoconducto.EstadoID == 3) //para los salvocondcutos rechazados muestro el motivo de rechazo
        {
            this.TblMotivoRechazo.Visible = true;
            this.BRMotivoRechazo.Visible = true;
            this.TxtMotivoRechazo.Text = vSalvoconducto.MotivoRechazo.ToString();
            this.ColSerieSalvoconducto.Visible = false;
            this.TituloSerieSalvoconducto.Visible = false;
            this.ColDescCodigoSeguridad.Visible = false;
            this.ColCodigoSeguridad.Visible = false;
        }
        else
        {
            LblSerieAsignada.Text = vSalvoconducto.Numero;

            if (string.IsNullOrEmpty(vSalvoconducto.CodigoSeguridad.ToString()))
            {
                this.ColCodigoSeguridad.Visible = false;
                this.ColDescCodigoSeguridad.Visible = false;
            }
            else
            {
                this.LblCodigoSeguridad.Text = vSalvoconducto.CodigoSeguridad.ToString();
            }


            //vigencia del salvoconducto
            if (Convert.ToDateTime(vSalvoconducto.FechaExpedicion.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
            {
                this.LblFechaExp.Text = vSalvoconducto.FechaExpedicion.ToString("dd/MM/yyyy").Trim(); ;
            }
            if (vSalvoconducto.FechaInicioVigencia != null)
            {
                if (Convert.ToDateTime(vSalvoconducto.FechaInicioVigencia.Value.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
                {
                    this.LblVigenciaDesde.Text = vSalvoconducto.FechaInicioVigencia.Value.ToString("dd/MM/yyyy").Trim();
                }
            }
            if (vSalvoconducto.FechaFinalVigencia != null)
                if (Convert.ToDateTime(vSalvoconducto.FechaFinalVigencia.Value.ToString("dd/MM/yyyy").Trim()) >= Convert.ToDateTime("01/01/1900"))
                {
                    this.LblVigenciaHasta.Text = vSalvoconducto.FechaFinalVigencia.Value.ToString("dd/MM/yyyy").Trim();
                }
        }


        this.LblEstadoSalvoconducto.Text = vSalvoconducto.Estado.ToString().ToUpper();

        var rutaOrigen = vSalvoconducto.LstRuta.Where(x => x.DepartamentoID == vSalvoconducto.DepartamentoOrigenID && x.MunicipioID == vSalvoconducto.MunicipioOrigenID).FirstOrDefault();
        if (rutaOrigen != null)
        {
            this.LblDptoExp.Text = rutaOrigen.Departamento;
            this.LblMunicipioExp.Text = rutaOrigen.Municipio;
            this.LblCodigoDptoExp.Text = rutaOrigen.DepartamentoID.ToString();
            this.LblCodigoMunExp.Text = rutaOrigen.MunicipioID.ToString();
        }
        else
        {
            this.LblDptoExp.Text = "No Aplica";
            this.LblMunicipioExp.Text = "No Aplica";
            this.LblCodigoDptoExp.Text = "No Aplica";
            this.LblCodigoMunExp.Text = "No Aplica";
        }


        //tipo de salvoconducto
        switch (vSalvoconducto.TipoSalvoconductoID)
        {
            case 1:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                if (vSalvoconducto.LstAprovechamientoOrigen.Count() == 1)
                {
                    this.TblProcEspecimenesUnico.Visible = true;
                    this.TblProcEspecimenesVarios.Visible = false;
                    Aprovechamiento vAprovechamiento = new Aprovechamiento();
                    vSalvoconducto.Aprovechamiento = vAprovechamiento.ConsultaAprovechamientoXAprovechamientoId(vSalvoconducto.LstAprovechamientoOrigen.First().AprovechamientoID);
                }
                else if (vSalvoconducto.LstAprovechamientoOrigen.Count() > 1)
                {
                    this.TblProcEspecimenesVarios.Visible = true;
                    this.TblProcEspecimenesUnico.Visible = false;
                    vSalvoconducto.Aprovechamiento = null;
                    foreach (var aprovechamiento in vSalvoconducto.LstAprovechamientoOrigen)
                    {
                        #region creo enlaces por cada salvoconducto
                        lnk = new HtmlAnchor();
                        lnk.InnerHtml = aprovechamiento.Detalle;
                        PlaceHolder2.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                        PlaceHolder2.Controls.Add(lnk);
                        PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                        #endregion
                    }
                }
                this.TblSalvoconductosAnteriores.Visible = false;
                break;
            case 2:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                this.TblProcEspecimenesUnico.Visible = false; //para el movimiento de removilizacion no se muestra la procedencia de los especimenes

                if (vSalvoconducto.LstSalvoconductoAnterior == null || vSalvoconducto.LstSalvoconductoAnterior.Count == 0)
                    TblSalvoconductosAnteriores.Visible = false;
                else 
                    this.TblSalvoconductosAnteriores.Visible = true;
                if (vSalvoconducto.LstSalvoconductoAnterior != null && vSalvoconducto.LstSalvoconductoAnterior.Count > 0)
                {

                    foreach (var SalvoConductoAnterior in vSalvoconducto.LstSalvoconductoAnterior)
                    {
                        #region creo enlaces por cada salvoconducto
                        lnk = new HtmlAnchor();
                        string parametros = "SalvoConductoID=" + SalvoConductoAnterior.SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false ";
                        string query = Utilidades.Encrypt(parametros);
                        lnk.InnerHtml = SalvoConductoAnterior.Detalle;
                        PlaceHolder1.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                        PlaceHolder1.Controls.Add(lnk);
                        PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                        #endregion
                    }

                }
                else if (Sn_SalvoconductoPrecargado)
                {
                    this.TblSalvoconductosAnteriores.Visible = true;
                    lnk = new HtmlAnchor();
                    lnk.InnerHtml = vSalvoconducto.NumeroSUNAnterior;
                    PlaceHolder1.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                    PlaceHolder1.Controls.Add(lnk);
                    PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }
                break;
            default:
                LblTipoSalvoconducto.Text = vSalvoconducto.TipoSalvoconducto.ToUpper();
                this.TblProcEspecimenesUnico.Visible = false;
                this.TblSalvoconductosAnteriores.Visible = true;
                if (vSalvoconducto.LstSalvoconductoAnterior != null && vSalvoconducto.LstSalvoconductoAnterior.Count > 0)
                {

                    foreach (var SalvoConductoAnterior in vSalvoconducto.LstSalvoconductoAnterior)
                    {
                        #region creo enlaces por cada salvoconducto
                        lnk = new HtmlAnchor();
                        string parametros = "SalvoConductoID=" + SalvoConductoAnterior.SalvoconductoID.ToString() + "&BloqueoSalvoConducto = false";
                        string query = Utilidades.Encrypt(parametros);
                        string queryEncriptado = "../Salvoconducto/ConsultaDetalleSalvoconducto.aspx" + query;
                        string _strPagina = queryEncriptado;
                        lnk.ID = "lnk" + SalvoConductoAnterior.SalvoconductoID.ToString();
                        lnk.HRef = _strPagina;
                        lnk.Target = "_blank";
                        lnk.InnerHtml = SalvoConductoAnterior.Detalle;
                        PlaceHolder1.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                        PlaceHolder1.Controls.Add(lnk);
                        PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                        #endregion
                    }
                }
                else if (Sn_SalvoconductoPrecargado)
                {
                    lnk = new HtmlAnchor();
                    lnk.InnerHtml = vSalvoconducto.NumeroSUNAnterior;
                    PlaceHolder1.Controls.Add(new LiteralControl("<img src='../App_Themes/Img/ok.png' />&nbsp;"));
                    PlaceHolder1.Controls.Add(lnk);
                    PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                }
                break;
        }
        string NombreTitularSalvoconducto = "";

        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido))
        {
            NombreTitularSalvoconducto = vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido.ToString();
        }
        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido))
        {
            NombreTitularSalvoconducto = NombreTitularSalvoconducto + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido.ToString();
        }
        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre))
        {
            NombreTitularSalvoconducto = NombreTitularSalvoconducto + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre.ToString();
        }
        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre))
        {
            NombreTitularSalvoconducto = NombreTitularSalvoconducto + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre.ToString();
        }
        if (NombreTitularSalvoconducto == string.Empty)
        {
            NombreTitularSalvoconducto = vSalvoconducto.SolicitanteTitularPersonaIdentity.RazonSocial;
        }
        LblNombreTitular.Text = NombreTitularSalvoconducto.Trim();

        if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.NumeroIdentificacion))
        {
            LblIdentificacionTitular.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.NumeroIdentificacion.ToString();
        }

        //if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.NombreMunicipio))
        //{
        //    //LbLMunicipioDomicilio.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.NombreMunicipio.ToString();
        //}

        //if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.DireccionPersona))
        //{
        //    //LblDireccion.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.DireccionPersona.ToString();
        //}
        //if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.Telefono))
        //{
        //    //LblTelefono.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.Telefono.ToString();
        //}

        LbLMunicipioDomicilio.Text = DatoPersonalEncriptado;
        LblDireccion.Text = DatoPersonalEncriptado;
        LblTelefono.Text = DatoPersonalEncriptado;
        //clase de recurso
        LblClaseRecurso.Text = vSalvoconducto.ClaseRecurso.ToUpper();

        //informacion de la obtencion de los especimenes
        if (vSalvoconducto.TipoSalvoconductoID != 2) // para removilizacion no se carga la informacion de los especimenes
        {
            //infromacion de la obtencion de los especimenes
            string NombreTitularSalvoconductoEspecimen = "";

            if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido))
            {
                NombreTitularSalvoconductoEspecimen = vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido;
            }
            if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido))
            {
                NombreTitularSalvoconductoEspecimen = NombreTitularSalvoconductoEspecimen + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido.ToString();
            }
            if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre))
            {
                NombreTitularSalvoconductoEspecimen = NombreTitularSalvoconductoEspecimen + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre.ToString();
            }
            if (!string.IsNullOrEmpty(vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre))
            {
                NombreTitularSalvoconductoEspecimen = NombreTitularSalvoconductoEspecimen + " " + vSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre.ToString();
            }

            if (!string.IsNullOrEmpty(NombreTitularSalvoconductoEspecimen))
            {
                LblNomTitularsObtLegalEsp.Text = NombreTitularSalvoconductoEspecimen;
            }
            if (vSalvoconducto.SolicitanteTitularPersonaIdentity.TipoPersona.CodigoTipoPersona == 3)
            {
                LblNomTitularsObtLegalEsp.Text = vSalvoconducto.SolicitanteTitularPersonaIdentity.RazonSocial;
            }

            LblTelTitularsObtLegalEsp.Text = DatoPersonalEncriptado;
            LblMunTitularsObtLegalEsp.Text = DatoPersonalEncriptado;
            LblDirTitularsObtLegalEsp.Text = DatoPersonalEncriptado;

            if (vSalvoconducto.Aprovechamiento != null)
            {
                if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.NumeroIdentificacion))
                {
                    LblIdentTitularsObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.Solicitante.NumeroIdentificacion.ToString();
                }
                //if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.NombreMunicipio))
                //{
                //    LblMunTitularsObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.NombreMunicipio.ToString();
                //}
                //if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.Telefono))
                //{
                //    LblTelTitularsObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.Solicitante.Telefono.ToString();
                //}
                //if (!string.IsNullOrEmpty(vSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.DireccionPersona))
                //{
                //    LblDirTitularsObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.DireccionPersona.ToString();
                //}


                this.LblModo.Text = vSalvoconducto.Aprovechamiento.ModoAdquisicionRecurso;
                this.LblActoAdministrativo.Text = vSalvoconducto.Aprovechamiento.Numero.ToString();
                this.LblFechaObtLegalEsp.Text = vSalvoconducto.Aprovechamiento.FechaExpedicion.Value.ToString("dd/MM/yyyy").Trim();
            }
            else
            {
                this.LblMunTitularsObtLegalEsp.Text = "No Aplica";
                this.LblTelTitularsObtLegalEsp.Text = "No Aplica";
                this.LblDirTitularsObtLegalEsp.Text = "No Aplica";
                this.LblIdentTitularsObtLegalEsp.Text = vSalvoconducto.IdentificacionTitularAprovechamientoSUNLPreimpreso.ToString();
                this.LblNomTitularsObtLegalEsp.Text = vSalvoconducto.TitularAprovechamientoSUNLPreimpreso.ToString();
                this.LblModo.Text = vSalvoconducto.ModoAdquisicionRecurso;
                this.LblActoAdministrativo.Text = vSalvoconducto.ActoAdministrativoAprovechamientoSUNLPreimpreso;
                this.LblFechaObtLegalEsp.Text = vSalvoconducto.FechaActoAdministrativoAprovechamientoSUNLPreimpreso.ToString("dd/MM/yyyy").Trim();
            }

            this.LblAutoridadAmbiental.Text = vSalvoconducto.AutoridadEmisora;
            this.LblFinalidadUso.Text = vSalvoconducto.Finalidad;
            this.LblVeredaProcEsp.Text = vSalvoconducto.VeredaProcedencia;
            this.LblMunicipioProcEsp.Text = vSalvoconducto.MunicipioProcedencia;
            this.LblCodMunProcEsp.Text = vSalvoconducto.MunicipioProcedenciaID.ToString();
            this.LblDepartamentoProcEsp.Text = vSalvoconducto.DepartamentoProcedencia;
            this.LblCodDptoProcEsp.Text = vSalvoconducto.DepartamentoProcedenciaID.ToString();

        }


        //ruta de desplazamiento
        if (vSalvoconducto.LstRuta.Count > 0)
        {
            LblTipoMovilizacion.Text = vSalvoconducto.LstRuta.FirstOrDefault().TipoRuta;
            switch (vSalvoconducto.LstRuta.FirstOrDefault().TipoRutaID)
            {
                case 1:
                    grvRutaDesplazamiento.Columns[4].Visible = false; //intermunicipal
                    break;
                case 2:
                    grvRutaDesplazamiento.Columns[4].Visible = true; //casco urbano
                    break;
            }
        }

        LstRutaEntity = vSalvoconducto.LstRuta;
        grvRutaDesplazamiento.DataSource = vSalvoconducto.LstRuta;
        grvRutaDesplazamiento.DataBind();

        //JMARTINEZ SALVOCONDUCTO FASE 2
        if (vSalvoconducto.TipoSalvoconductoID != 1) //si es removilizacion o renovacion no muestro la columna de los salvocondcutos anteriores
        {
            gdvEspecimenes.Columns[0].Visible = true;
            //GrvTotalesEspecies.Columns[0].Visible = true;
        }
        else
        {
            gdvEspecimenes.Columns[0].Visible = false;
            //GrvTotalesEspecies.Columns[0].Visible = false;
        }

        //informacion de Transporte
        if (vSalvoconducto.LstTransporte  == null || vSalvoconducto.LstTransporte.Count == 0)
        {
            TblDetTransporte.Visible = false;
        }
        else
        {
            grvTransporte.DataSource = vSalvoconducto.LstTransporte;
            grvTransporte.DataBind();
        }

        //informacion especimenes
        gdvEspecimenes.DataSource = vSalvoconducto.LstEspecimen;
        gdvEspecimenes.DataBind();
        //jmartinez salvoconducto fase 2
        TotalizarCantidadesProdUnidadMedida();


        if (vSalvoconducto.SalvoconductoAsociados.Rows.Count > 0)
        {
            this.tblSalvoRelacionados.Visible = true;
            this.gdvSalvoconductoRelacionados.DataSource = vSalvoconducto.SalvoconductoAsociados;
            this.gdvSalvoconductoRelacionados.DataBind();
        }
        else
        {
            this.tblSalvoRelacionados.Visible = false;
        }
    }

    protected void TotalizarCantidadesProdUnidadMedida()
    {
        if (vSalvoconducto.LstEspecimen.Count > 0)
        {
            var Resultado = from x in (from y in vSalvoconducto.LstEspecimen
                                       select new
                                       {
                                           y.NombreComunEspecie,
                                           y.NombreEspecie,
                                           y.TipoProducto,
                                           y.UnidadMedida,
                                           y.Cantidad,
                                           y.Volumen,
                                       })
                            group x by new { x.NombreComunEspecie, x.NombreEspecie, x.TipoProducto, x.UnidadMedida } into t
                            select new { t.Key.NombreComunEspecie, t.Key.NombreEspecie, t.Key.TipoProducto, t.Key.UnidadMedida, TotalCantidad = t.Sum(y => y.Cantidad), TotalVolumen = t.Sum(y => y.Volumen) };

            LblLstTotalTipProductUm.Visible = true;
            this.GrvTotalesEspecies.DataSource = Resultado.ToList();
            this.GrvTotalesEspecies.DataBind();
        }
        else
        {
            LblLstTotalTipProductUm.Visible = false;
            this.GrvTotalesEspecies.DataSource = null;
            this.GrvTotalesEspecies.DataBind();
        }
    }

    protected void gdvEspecimenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvEspecimenes.PageIndex = e.NewPageIndex;
        gdvEspecimenes.DataSource = vSalvoconducto.LstEspecimen;
        gdvEspecimenes.DataBind();
    }
    protected void gdvSalvoconductoRelacionados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (vSalvoconducto.SalvoconductoAsociados.Rows.Count > 0)
        {
            this.tblSalvoRelacionados.Visible = true;
            this.gdvSalvoconductoRelacionados.PageIndex = e.NewPageIndex;
            this.gdvSalvoconductoRelacionados.DataSource = vSalvoconducto.SalvoconductoAsociados;
            this.gdvSalvoconductoRelacionados.DataBind();
        }
        else
        {
            this.tblSalvoRelacionados.Visible = false;
        }
    }
}