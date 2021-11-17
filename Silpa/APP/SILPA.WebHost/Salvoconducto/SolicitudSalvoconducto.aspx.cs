using SILPA.AccesoDatos.Salvoconducto;
using SILPA.AccesoDatos.Aprovechamiento;
using SILPA.LogicaNegocio.Aprovechamiento;
using SILPA.LogicaNegocio.Salvoconducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Generico;
using System.Data;
using SILPA.AccesoDatos.Generico;
using System.Globalization;

public partial class Salvoconducto_SolicitudSalvoconducto : System.Web.UI.Page
{
    #region ViewStates
    public AprovechamientoOrigen objAprovechamientoAnterior { get { return (AprovechamientoOrigen)ViewState["AprovechamientoOrigen"]; } set { ViewState["AprovechamientoOrigen"] = value; } }
    public SalvoconductoNewIdentity objSalvoconductoAnterior { get { return (SalvoconductoNewIdentity)ViewState["objSalvoconductoAnterior"]; } set { ViewState["objSalvoconductoAnterior"] = value; } }
    public List<SalvoconductoAnterior> LstSalvoconductoAnterior { get { return (List<SalvoconductoAnterior>)ViewState["listaSalvoconductoAnterior"]; } set { ViewState["listaSalvoconductoAnterior"] = value; } }
    public List<AprovechamientoOrigen> LstAprovechamientoOrigen { get { return (List<AprovechamientoOrigen>)ViewState["LstAprovechamientoOrigen"]; } set { ViewState["LstAprovechamientoOrigen"] = value; } }
    public List<EspecimenNewIdentity> LstEspecimenNewIdentity { get { return (List<EspecimenNewIdentity>)ViewState["LstEspecimenNewIdentity"]; } set { ViewState["LstEspecimenNewIdentity"] = value; } }
    public List<EspecimenNewIdentity> LstEspeciesSalvoAnterior { get { return (List<EspecimenNewIdentity>)ViewState["LstEspeciesSalvoAnterior"]; } set { ViewState["LstEspeciesSalvoAnterior"] = value; } }
    public List<EspecieAprovechamientoIdentity> LstEspeciesAprovechamientoOrigen { get { return (List<EspecieAprovechamientoIdentity>)ViewState["LstEspeciesAprovechamientoOrigen"]; } set { ViewState["LstEspeciesAprovechamientoOrigen"] = value; } }
    public List<RutaEntity> LstRutaEntity { get { return (List<RutaEntity>)ViewState["LstRutaEntity"]; } set { ViewState["LstRutaEntity"] = value; } }
    public List<TipoTransporteIdentity> LstTipoTransporte { get { return (List<TipoTransporteIdentity>)ViewState["LstTipoTransporte"]; } set { ViewState["LstTipoTransporte"] = value; } }
    public List<TransporteNewIdentity> LstTransporte { get { return (List<TransporteNewIdentity>)ViewState["LstTransporte"]; } set { ViewState["LstTransporte"] = value; } }
    private string TitularSalvoconducto
    {
        get { return ViewState["TitularSalvoconducto"].ToString(); }
        set { ViewState["TitularSalvoconducto"] = value; }
    }
    #endregion
    # region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // USUARIO PARA PRUEBAS
            //Session["Usuario"] = 62487;
            //this.CargarPagina();
            //return;
            //DESCOMENTAR ANTES DEL COMMIT!!!

            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                this.CargarPagina();
            }
        }
        if (Request.Params["__EVENTTARGET"] == "ctl00$ContentPlaceHolder1$tbcContenedor$tabInfoGeneral$cboSalvoconductoAnterior")
        {
            cboSalvoconductoAnterior_SelectedIndexChanged(null, null);
        }
        if (Request.Params["__EVENTTARGET"] == "ctl00$ContentPlaceHolder1$tbcContenedor$tabInfoGeneral$cboAprovechamiento")
        {
            cboAprovechamiento_SelectedIndexChanged(null, null);
        }
    }
    //protected void cboAutoridadAmbientalEmisora_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (this.cboAutoridadAmbientalEmisora.SelectedValue != "")
    //    {
    //        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
    //        Listas _listaAutoridades = new Listas();
    //        this.cboTipoSalvoconducto.Enabled = true;
    //        Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentosPorAutoridadAmbiental(_configuracion.IdPaisPredeterminado, Convert.ToInt32(this.cboAutoridadAmbientalEmisora.SelectedValue)).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
    //        Utilidades.LlenarComboVacio(this.cboMunicipio);
    //        cboTipoSalvoconducto.SelectedIndex = 0;
    //        cboTipoSalvoconducto_SelectedIndexChanged(null, null);
    //    }
    //    else
    //    {
    //        this.cboTipoSalvoconducto.Enabled = false;
    //        Utilidades.LlenarComboVacio(this.cboDepartamento);
    //        Utilidades.LlenarComboVacio(this.cboMunicipio);
    //    }

    //}

    protected void cboAutoridadAmbientalEmisora_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.cboTipoSalvoconducto.SelectedIndex = 0;
        this.cboTipoSalvoconducto_SelectedIndexChanged(null, null);
        this.cboClaseRecurso.SelectedIndex = 0;
        this.cboClaseRecurso_SelectedIndexChanged(null, null);
        this.cboAprovechamiento.SelectedIndex = 0;
        this.cboAprovechamiento_SelectedIndexChanged(null, null);
        this.cboFinalidadRecurso.SelectedIndex = 0;

    }

    protected void cboTipoSalvoconducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.cboClaseRecurso.SelectedValue = "";
        this.cboTipoRuta.SelectedIndex = 0;
        cboTipoRuta_SelectedIndexChanged(null, null);
        this.cboModoTransporte.SelectedIndex = 0;
        cboModoTransporte_SelectedIndexChanged(null, null);
        this.txtEmpresa.Text = "";
        this.txtIdentificacionTransporte.Text = "";
        this.txtNombreTransportador.Text = "";
        this.txtIdentificacionTransportador.Text = "";
        //Jmartinez Salvocondcuto Fase 2
        LstSalvoconductoAnterior = null;
        LstEspeciesSalvoAnterior = null;

        if (cboTipoSalvoconducto.SelectedValue == "2")
        {
            grvEspecies.Columns[0].Visible = true;
            GrvTotalesEspecies.Columns[0].Visible = true;
        }
        else
        {
            grvEspecies.Columns[0].Visible = false;
            GrvTotalesEspecies.Columns[0].Visible = false;
        }
        CargarOriguenSalvoconducto();
    }
    protected void cboAprovechamiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.tblAprovechamiento.Visible)
        {
            if (this.cboAprovechamiento.SelectedValue != "")
            {
                // consultamos el aprovechamiento
                Aprovechamiento vAprovechamiento = new Aprovechamiento();
                var lstEspecies = vAprovechamiento.ListaRecursosAprovechamiento(Convert.ToInt32(this.cboAprovechamiento.SelectedValue));
                LstEspeciesAprovechamientoOrigen = lstEspecies.Where(x => x.CantidadDisponible > 0).ToList();
                Utilidades.LlenarComboLista(LstEspeciesAprovechamientoOrigen, cboEspecies, "NombreEspecie", "EspecieAprovechamientoID", true);
                if (this.cboClaseRecurso.SelectedValue != "3")
                {
                    Aprovechamiento clsAprovechamiento = new Aprovechamiento();
                    LstAprovechamientoOrigen.Add(new AprovechamientoOrigen { AprovechamientoID = Convert.ToInt32(this.cboAprovechamiento.SelectedValue), Detalle = this.cboAprovechamiento.SelectedItem.Text });
                }
                else
                {
                    LstAprovechamientoOrigen = new List<AprovechamientoOrigen>();
                }
            }
            else
            {
                this.lblModoAdquisicionDescrip.Text = "";
            }
        }
    }
    protected void cboClaseRecurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarOriguenSalvoconducto();
    }
    protected void btnAgregarSalvoconductoAnterior_Click(object sender, EventArgs e)
    {
        if (LstSalvoconductoAnterior == null)
            LstSalvoconductoAnterior = new List<SalvoconductoAnterior>();
        if (LstEspeciesSalvoAnterior == null)
            LstEspeciesSalvoAnterior = new List<EspecimenNewIdentity>();
        if (LstSalvoconductoAnterior.Where(x => x.SalvoconductoID == Convert.ToInt32(this.cboSalvoconductoAnterior.SelectedValue)).Count() == 0)
        {
            SalvoconductoNew vSalvoconductoNew = new SalvoconductoNew();
            var lstEspecies = vSalvoconductoNew.ListaEspecieSalvoconducto(Convert.ToInt32(this.cboSalvoconductoAnterior.SelectedValue));
            LstSalvoconductoAnterior.Add(new SalvoconductoAnterior { SalvoconductoID = Convert.ToInt32(this.cboSalvoconductoAnterior.SelectedValue), Detalle = this.cboSalvoconductoAnterior.SelectedItem.Text });
            LstEspeciesSalvoAnterior.AddRange(lstEspecies.Where(x => x.CantidadDisponible > 0));
            if (cboTipoSalvoconducto.SelectedValue == "2")
            {
                foreach (var especie in LstEspeciesSalvoAnterior)
                {
                    //especie.StrEspecieSunlAnterior = this.cboSalvoconductoAnterior.SelectedItem.ToString() + " - " + especie.NombreEspecie;
                    especie.StrEspecieSunlAnterior =  especie.NumeroSUNL + " - " + especie.NombreEspecie;
                }
                Utilidades.LlenarComboLista(LstEspeciesSalvoAnterior, cboEspecies, "StrEspecieSunlAnterior", "EspecieSalvoconductoID", true);
            }
            else
            {
                Utilidades.LlenarComboLista(LstEspeciesSalvoAnterior, cboEspecies, "NombreEspecie", "EspecieSalvoconductoID", true);
            }
            this.grvSalvoconductoAnterior.DataSource = LstSalvoconductoAnterior;
            this.grvSalvoconductoAnterior.DataBind();
        }
    }
    protected void btnAgregarAprovechamientoOrigen_Click(object sender, EventArgs e)
    {
        if (LstAprovechamientoOrigen == null)
            LstAprovechamientoOrigen = new List<AprovechamientoOrigen>();
        if (LstEspeciesAprovechamientoOrigen == null)
            LstEspeciesAprovechamientoOrigen = new List<EspecieAprovechamientoIdentity>();
        if (LstAprovechamientoOrigen.Where(x => x.AprovechamientoID == Convert.ToInt32(this.cboAprovechamiento.SelectedValue)).Count() == 0)
        {
            Aprovechamiento vAprovechamiento = new Aprovechamiento();
            var lstEspecies = vAprovechamiento.ListaRecursosAprovechamiento(Convert.ToInt32(this.cboAprovechamiento.SelectedValue));
            LstAprovechamientoOrigen.Add(new AprovechamientoOrigen { AprovechamientoID = Convert.ToInt32(this.cboAprovechamiento.SelectedValue), Detalle = this.cboAprovechamiento.SelectedItem.Text });
            LstEspeciesAprovechamientoOrigen.AddRange(lstEspecies.Where(x => x.CantidadDisponible > 0));
            Utilidades.LlenarComboLista(LstEspeciesAprovechamientoOrigen, cboEspecies, "NombreEspecie", "EspecieAprovechamientoID", true);
            this.grvAprovechamientoOrigen.DataSource = LstAprovechamientoOrigen;
            this.grvAprovechamientoOrigen.DataBind();
        }
    }
    protected void cboEspecies_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboEspecies.SelectedValue != string.Empty)
        {
            //this.cboClaseProducto.Enabled = false;
            //this.cboTipoProducto.Enabled = false;
            if (this.tblAprovechamiento.Visible)
            {
                var especie = LstEspeciesAprovechamientoOrigen.Where(x => x.EspecieAprovechamientoID == Convert.ToInt32(cboEspecies.SelectedValue)).FirstOrDefault();
                this.lblCantidadDisponible.Text = especie.CantidadDisponible.Value.ToString("N5");
                this.lblUnidadMedida.Text = especie.UnidadMedida;
                this.hfUnidadMedidaID.Value = especie.UnidadMedidaID.ToString();
                //jmartinez salvoconducto fase 2
                if (!string.IsNullOrEmpty(especie.NombreComunEspecie))
                {
                    trNombreEspecie.Visible = true;
                    this.LblNombreComunEspecie.Text = especie.NombreComunEspecie;
                }
                else
                {
                    trNombreEspecie.Visible = false;
                }
                
                if (this.cboClaseRecurso.SelectedValue != "1")
                {
                    this.cboClaseProducto.SelectedValue = especie.ClaseProductoID.ToString();
                    cboClaseProducto_SelectedIndexChanged(null, null);
                    if (cboTipoProducto.Items.FindByValue(especie.TipoProductoID.ToString()) != null)
                        cboTipoProducto.SelectedValue = especie.TipoProductoID.ToString();
                    this.cboClaseProducto.Enabled = true;
                    this.cboTipoProducto.Enabled = true;
                }
                else
                {
                    this.cboClaseProducto.Enabled = true;
                    this.cboTipoProducto.Enabled = true;
                }
            }
            else if (this.tblSalvoanterior.Visible)
            {
                var especie = LstEspeciesSalvoAnterior.Where(x => x.EspecieSalvoconductoID == Convert.ToInt32(cboEspecies.SelectedValue)).FirstOrDefault();
                this.lblCantidadDisponible.Text = especie.CantidadDisponible.ToString("N");
                this.lblUnidadMedida.Text = especie.UnidadMedida;
                this.hfUnidadMedidaID.Value = especie.UnidadMedidaId.ToString();
                this.cboClaseProducto.SelectedValue = especie.ClaseProductoID.ToString();
                cboClaseProducto_SelectedIndexChanged(null, null);
                cboTipoProducto.SelectedValue = especie.TipoProductoId.ToString();
                this.cboClaseProducto.Enabled = true;
                this.cboTipoProducto.Enabled = true;

            }
        }
        else
        {
            this.lblCantidadDisponible.Text = "";
            this.lblUnidadMedida.Text = "";
            this.cboClaseProducto.SelectedIndex = 0;
            this.cboTipoProducto.SelectedIndex = 0;
        }
    }
    protected void cboSalvoconductoAnterior_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblAgregarEspecie.Visible = true;
        if (this.cboSalvoconductoAnterior.SelectedValue != string.Empty)
        {
            SalvoconductoNew vSalvoconductoNew = new SalvoconductoNew();
            objSalvoconductoAnterior = vSalvoconductoNew.ConsultaSalvoconductoXSalvoconductoId(Convert.ToInt32(this.cboSalvoconductoAnterior.SelectedValue));
            LstEspecimenNewIdentity = new List<EspecimenNewIdentity>();
            foreach (var especimen in objSalvoconductoAnterior.LstEspecimen)
            {
                LstEspecimenNewIdentity.Add(new EspecimenNewIdentity
                {
                    AprovechamientoOrigenId = null,
                    Cantidad = especimen.Cantidad,
                    CantidadDisponible = especimen.CantidadDisponible,
                    CantidadLetras = especimen.CantidadLetras,
                    ClaseProducto = especimen.ClaseProducto,
                    ClaseProductoID = especimen.ClaseProductoID,
                    Descripcion = especimen.Descripcion,
                    Dimensiones = especimen.Dimensiones,
                    EspecieSalvoconductoID = especimen.EspecieSalvoconductoID,
                    EspecieTaxonomiaID = especimen.EspecieTaxonomiaID,
                    NombreEspecie = especimen.NombreEspecie,
                    TipoProductoId = especimen.TipoProductoId,
                    TipoProducto = especimen.TipoProducto,
                    UnidadMedidaId = especimen.UnidadMedidaId,
                    UnidadMedida = especimen.UnidadMedida,
                    SalvoconductoOrigenId = objSalvoconductoAnterior.SalvoconductoID,
                    Volumen = especimen.Volumen
                });
            }
            LstRutaEntity = objSalvoconductoAnterior.LstRuta;
            this.cboFinalidadRecurso.SelectedValue = objSalvoconductoAnterior.FinalidadID.ToString();
            LstSalvoconductoAnterior = new List<SalvoconductoAnterior>();
            LstSalvoconductoAnterior.Add(new SalvoconductoAnterior { SalvoconductoID = objSalvoconductoAnterior.SalvoconductoID, Detalle = objSalvoconductoAnterior.Detalle });
            this.grvEspecies.DataSource = LstEspecimenNewIdentity;
            this.grvEspecies.DataBind();
            this.grvRutaDesplazamiento.DataSource = LstRutaEntity;
            this.grvRutaDesplazamiento.DataBind();
            tblAgregarEspecie.Visible = false;
            this.grvEspecies.Columns[10].Visible = true;
            this.grvTransporte.DataSource = objSalvoconductoAnterior.LstTransporte;
            this.grvTransporte.DataBind();
            //this.cboModoTransporte.SelectedValue = objSalvoconductoAnterior.Transporte.ModoTransporteID.ToString();
            //cboModoTransporte_SelectedIndexChanged(null, null);
            //this.cboTipoVehiculo.SelectedValue = objSalvoconductoAnterior.Transporte.TipoTransporteID.ToString();
            //cboTipoVehiculo_SelectedIndexChanged(null, null);
            //this.txtOtroTipoVehiculo.Text = objSalvoconductoAnterior.Transporte.TipoTransporteOtro;
            //this.txtEmpresa.Text = objSalvoconductoAnterior.Transporte.Empresa;
            //this.txtIdentificacionTransporte.Text = objSalvoconductoAnterior.Transporte.NumeroIdentificacionMedioTransporte;
            //this.txtNombreTransportador.Text = objSalvoconductoAnterior.Transporte.NombreTransportador;
            //this.txtIdentificacionTransportador.Text = objSalvoconductoAnterior.Transporte.NumeroIdentificacionTransportador;

        }
    }
    protected void cboClaseProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboClaseProducto.SelectedValue != string.Empty)
        {
            TipoProducto vTipoProducto = new TipoProducto();
            Utilidades.LlenarComboLista(vTipoProducto.ListaTipoProducto(Convert.ToInt32(this.cboClaseProducto.SelectedValue), true), this.cboTipoProducto, "TipoProducto", "TipoProductoID", true);
            HabilitarDimensiones();

        }
        else
        {
            Utilidades.LlenarComboVacio(this.cboTipoProducto);
            HabilitarDimensiones();

        }
    }
    protected void btnAgregarEspecie_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int contador = 0;
            double alto = 1, ancho = 1, largo = 1, factor = 1, cantidad = 0, longitud = 1;
            int? salvoconductoAnteriorId = null, aprovechamientoAnteriorId = null;
            int especieTaxonomicaId = 0, identity = 0;
            string dimensiones = string.Empty;
            bool cantidadOK = false;
            //jmartinez Salvoconducto Fase 2
            string NumeroSunlAnterior = string.Empty;
            string NombreEspecie = string.Empty;


            if (LstEspecimenNewIdentity == null)
                LstEspecimenNewIdentity = new List<EspecimenNewIdentity>();
            if (this.cboClaseRecurso.SelectedValue == "1")
            {
                if (this.tdAlto.Visible)
                {
                    alto = Convert.ToDouble(this.txtAlto.Text, CultureInfo.InvariantCulture);
                    dimensiones += "alto:" + this.txtAlto.Text + ";";
                }
                if (this.tdAncho.Visible)
                {
                    ancho = Convert.ToDouble(this.txtAncho.Text, CultureInfo.InvariantCulture);
                    dimensiones += LblAncho.Text + this.txtAncho.Text + ";";
                }
                if (this.tdLargo.Visible)
                {
                    largo = Convert.ToDouble(this.txtLargo.Text, CultureInfo.InvariantCulture);
                    dimensiones += "largo:" + this.txtLargo.Text + ";";
                }

                if (this.cboClaseProducto.SelectedValue == "17")
                {
                    factor = 0.7854 * Math.Pow(((ancho + ancho) / 2), 2) * largo;
                }
                else
                {
                    factor = alto * ancho * largo;
                }
                cantidad = Math.Round((Convert.ToDouble(this.txtCantidad.Text) * factor), 2);
            }
            else if (this.cboClaseRecurso.SelectedValue == "2" && this.cboClaseProducto.SelectedValue == "10")
            {
                TipoProducto vTipoProducto = new TipoProducto();
                var tipoProducto = vTipoProducto.ListaTipoProducto(Convert.ToInt32(this.cboClaseProducto.SelectedValue), true).Where(x => x.TipoProductoID == Convert.ToInt32(this.cboTipoProducto.SelectedValue)).First();
                if (tipoProducto.Formula != string.Empty)
                {
                    if (tipoProducto.Formula.Contains("longitud"))
                    {
                        dimensiones += "longitud:" + this.txtLongitud.Text.Trim();
                        longitud = Convert.ToDouble(this.txtLongitud.Text.Trim(), CultureInfo.InvariantCulture);
                    }
                    if (tipoProducto.Formula.Contains("cantidad"))
                    {
                        dimensiones += "cantidad:" + this.txtCantidad.Text.Trim();
                        cantidad = Convert.ToDouble(this.txtCantidad.Text.Trim(), CultureInfo.InvariantCulture);
                    }
                    factor = TraerParteNumericaFormula(tipoProducto.Formula);

                    cantidad = (longitud * cantidad) / factor;
                }
            }
            else
            {
                cantidad = Math.Round((Convert.ToDouble(this.txtCantidad.Text) * factor), 2);
            }
            if (tblSalvoanterior.Visible)
            {

                if (LstEspeciesSalvoAnterior.Where(x => x.EspecieSalvoconductoID == Convert.ToInt32(this.cboEspecies.SelectedValue)).Count() > 0)
                {
                    var especie = LstEspeciesSalvoAnterior.Where(x => x.EspecieSalvoconductoID == Convert.ToInt32(this.cboEspecies.SelectedValue)).FirstOrDefault();
                    salvoconductoAnteriorId = especie.SalvocoductoID;
                    especieTaxonomicaId = especie.EspecieTaxonomiaID;
                    identity = especie.EspecieSalvoconductoID;
                    //jmartinez Salvoconducto Fase 2
                    NumeroSunlAnterior = especie.NumeroSUNL;
                    if (NumeroSunlAnterior.Length > 0)
                    {
                        NombreEspecie = this.cboEspecies.SelectedItem.Text.Replace(NumeroSunlAnterior, string.Empty).Replace("-", string.Empty).Trim();
                    }
                    else
                    {
                        NombreEspecie = this.cboEspecies.SelectedItem.Text;
                    }
                    
                    if (especie.CantidadDisponible >= (double)cantidad)
                    {
                        cantidadOK = true;
                        especie.CantidadDisponible = especie.CantidadDisponible - (double)cantidad;
                    }
                }
            }
            else if (tblAprovechamiento.Visible)
            {
                var especie = LstEspeciesAprovechamientoOrigen.Where(x => x.EspecieAprovechamientoID == Convert.ToInt32(this.cboEspecies.SelectedValue)).FirstOrDefault();
                aprovechamientoAnteriorId = especie.AprovechamientoID;
                especieTaxonomicaId = especie.EspecieTaxonomiaID;
                identity = especie.EspecieAprovechamientoID;
                if (especie.CantidadDisponible >= (double)cantidad)
                {
                    cantidadOK = true;
                    especie.CantidadDisponible = especie.CantidadDisponible - (double)cantidad;
                }
                NombreEspecie = this.cboEspecies.SelectedItem.Text;
            }




            if (cantidadOK)
            {

                //jmartinez no permitir que la solicitud grabe las especie en cero
                if (cantidad <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('La cantidad Registrada debe ser mayor a cero');", true);
                    return;
                }

                contador = LstEspecimenNewIdentity.Count() + 1;
                LstEspecimenNewIdentity.Add(new EspecimenNewIdentity
                {
                    Orden = contador,
                    Identity = identity,
                    Cantidad = Convert.ToDouble(this.txtCantidad.Text),
                    CantidadLetras = new Utilidades().NumeroALetras(this.txtCantidad.Text),
                    ClaseProductoID = Convert.ToInt32(this.cboClaseProducto.SelectedValue),
                    ClaseProducto = this.cboClaseProducto.SelectedItem.Text,
                    EspecieTaxonomiaID = especieTaxonomicaId,
                    NombreEspecie = NombreEspecie,
                    TipoProductoId = Convert.ToInt32(this.cboTipoProducto.SelectedValue),
                    TipoProducto = this.cboTipoProducto.SelectedItem.Text,
                    UnidadMedidaId = Convert.ToInt32(hfUnidadMedidaID.Value),
                    UnidadMedida = lblUnidadMedida.Text,
                    AprovechamientoOrigenId = aprovechamientoAnteriorId,
                    SalvoconductoOrigenId = salvoconductoAnteriorId,
                    Volumen = (double)cantidad,
                    Dimensiones = dimensiones,
                    Descripcion = this.txtDescripcion.Text,
                    Identificacion = this.txtIdentificacion.Text,
                    //jmartinez salvoconducto fase 2
                    NombreComunEspecie = this.LblNombreComunEspecie.Text,
                    NumeroSUNLAnterior = NumeroSunlAnterior
                });
                grvEspecies.DataSource = LstEspecimenNewIdentity;
                grvEspecies.DataBind();
                this.grvEspecies.Columns[10].Visible = false;
                TotalizarCantidadesProdUnidadMedida();
                ReiniciarPestañaEspecimenes();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('La cantidad " + cantidad.ToString("N") + " supera la disponible');", true);
            }
        }
    }


    protected void TotalizarCantidadesProdUnidadMedida()
    {
        if (LstEspecimenNewIdentity.Count > 0)
        {
            var Resultado = from x in (from y in LstEspecimenNewIdentity
                                       select new
                                       {
                                           y.NumeroSUNLAnterior,
                                           y.NombreEspecie,
                                           y.UnidadMedida,
                                           y.Volumen,
                                       })
                            group x by new {x.NumeroSUNLAnterior, x.NombreEspecie, x.UnidadMedida } into t
                            select new {t.Key.NumeroSUNLAnterior, t.Key.NombreEspecie, t.Key.UnidadMedida, Total = t.Sum(y => y.Volumen) };

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

    private double TraerParteNumericaFormula(string formula)
    {
        string parteEntera = "";
        for (int i = 0; i < formula.Length; i++)
        {
            Int32 caracterEntero = -1;
            if (Int32.TryParse(formula.Substring(i, 1), out caracterEntero))
            {
                parteEntera += caracterEntero.ToString();
            }
        }
        if (parteEntera != "")
        {
            return Convert.ToDouble(parteEntera);
        }
        else
        {
            return 1.0;
        }
    }
    /// <summary>
    /// Evento que se ejecuta al dar clic en el boton de eliminar
    /// </summary>
    protected void grvSalvoconductoAnterior_lnkEliminar_Click(object sender, EventArgs e)
    {
        int salvoconductoID = 0;
        try
        {
            //Cargamos el id del salvoconducto
            salvoconductoID = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
            LstSalvoconductoAnterior.RemoveAll(x => x.SalvoconductoID == salvoconductoID);
            LstEspeciesSalvoAnterior.RemoveAll(x => x.SalvocoductoID == salvoconductoID);
            LstEspecimenNewIdentity.RemoveAll(x => x.SalvoconductoOrigenId == salvoconductoID);
            Utilidades.LlenarComboLista(LstEspeciesSalvoAnterior, cboEspecies, "NombreEspecie", "EspecieSalvoconductoID", true);
            this.grvEspecies.DataSource = LstEspecimenNewIdentity;
            this.grvEspecies.DataBind();
            this.grvEspecies.Columns[10].Visible = false;
            this.grvSalvoconductoAnterior.DataSource = LstSalvoconductoAnterior;
            this.grvSalvoconductoAnterior.DataBind();
            this.lblCantidadDisponible.Text = string.Empty;
            this.lblUnidadMedida.Text = string.Empty;
            this.hfUnidadMedidaID.Value = string.Empty;
            this.cboClaseProducto.SelectedIndex = 0;
        }
        catch (Exception exc)
        {
        }
    }
    protected void grvAprovechamientoOrigen_lnkEliminar_Click(object sender, EventArgs e)
    {
        int aprovechamientoID = 0;
        try
        {
            //Cargamos el id del salvoconducto
            aprovechamientoID = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
            LstAprovechamientoOrigen.RemoveAll(x => x.AprovechamientoID == aprovechamientoID);
            LstEspeciesAprovechamientoOrigen.RemoveAll(x => x.AprovechamientoID == aprovechamientoID);
            LstEspecimenNewIdentity.RemoveAll(x => x.AprovechamientoOrigenId == aprovechamientoID);
            Utilidades.LlenarComboLista(LstEspeciesAprovechamientoOrigen, cboEspecies, "NombreEspecie", "EspecieAprovechamientoID", true);
            this.grvEspecies.DataSource = LstEspecimenNewIdentity;
            this.grvEspecies.DataBind();
            this.grvEspecies.Columns[10].Visible = false;
            this.grvAprovechamientoOrigen.DataSource = LstAprovechamientoOrigen;
            this.grvAprovechamientoOrigen.DataBind();
            this.lblCantidadDisponible.Text = string.Empty;
            this.lblUnidadMedida.Text = string.Empty;
            this.hfUnidadMedidaID.Value = string.Empty;
            this.cboClaseProducto.SelectedIndex = 0;
        }
        catch (Exception exc)
        {
        }
    }
    /// <summary>
    /// Evento que se ejecuta al dar click en el link de elimiar especie
    /// </summary>
    protected void grvEspecies_lnkEliminar_Click(object sender, EventArgs e)
    {
        int Orden = 0;
        int identity = 0;
        try
        {
            //Cargamos el id del salvoconducto
            Orden = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
            identity = LstEspecimenNewIdentity.Where(x => x.Orden == Orden).FirstOrDefault().Identity;
            //var especie = LstEspecimenNewIdentity.Where(x => x.Identity == identity).FirstOrDefault();
            var especie = LstEspecimenNewIdentity.Where(x => x.Orden == Orden).FirstOrDefault();
            if (tblAprovechamiento.Visible)
            {
                var especieAprovechamiento = LstEspeciesAprovechamientoOrigen.Where(x => x.EspecieAprovechamientoID == identity).FirstOrDefault();
                especieAprovechamiento.CantidadDisponible += especie.Volumen;
            }
            else if (tblSalvoanterior.Visible)
            {
                var especieSalvoAnterior = LstEspeciesSalvoAnterior.Where(x => x.EspecieSalvoconductoID == identity).FirstOrDefault();
                especieSalvoAnterior.CantidadDisponible += especie.Volumen;
            }
            LstEspecimenNewIdentity.RemoveAll(x => x.Orden == Orden);


            Orden = 0;
            foreach (var Especimen in LstEspecimenNewIdentity)
            {
                Especimen.Orden = Orden + 1;
                Orden = Especimen.Orden;
            }



            this.grvEspecies.DataSource = LstEspecimenNewIdentity;
            this.grvEspecies.DataBind();
            TotalizarCantidadesProdUnidadMedida();
            this.grvEspecies.Columns[10].Visible = false;
            this.cboEspecies.SelectedIndex = 0;
            cboEspecies_SelectedIndexChanged(null, null);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void grvRutaDesplazamiento_lnkEliminar_Click(object sender, EventArgs e)
    {
        int ordenEminar = 0;
        int nuevoOrden = 1;
        try
        {
            //Cargamos el id del salvoconducto
            ordenEminar = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
            LstRutaEntity.RemoveAll(x => x.Orden == ordenEminar);
            foreach (var ruta in LstRutaEntity)
            {
                ruta.Orden = nuevoOrden;
                nuevoOrden++;
            }
            this.grvRutaDesplazamiento.DataSource = LstRutaEntity;
            this.grvRutaDesplazamiento.DataBind();
        }
        catch (Exception exc)
        {
        }
    }


    protected void cboTipoRuta_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.cboDepartamento.SelectedIndex = 0;
        this.cboDepartamento_SelectedIndexChanged(null, null);
        LstRutaEntity = new List<RutaEntity>();
        grvRutaDesplazamiento.DataSource = null;
        grvRutaDesplazamiento.DataBind();
        this.cboDepartamento.Enabled = true;
        this.cboMunicipio.Enabled = true;
        if (this.cboTipoRuta.SelectedValue == "1")
        {
            this.trBarrio.Visible = false;
            this.txtBarrio.Text = string.Empty;
        }
        else
        {
            this.trBarrio.Visible = true;
            this.txtBarrio.Text = string.Empty;
        }
    }
    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDepartamento.SelectedValue == "")
        {
            Utilidades.LlenarComboVacio(cboMunicipio);
            return;
        }
        else
        {
            Listas litMunicipio = new Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            Utilidades.LlenarComboLista(dtMunicipios.AsEnumerable().Select(x => new { MUN_NOMBRE = x.Field<string>("MUN_NOMBRE"), MUN_ID = x.Field<Int32>("MUN_ID") }).Distinct().ToList(), cboMunicipio, "MUN_NOMBRE", "MUN_ID", true);
        }
    }
    protected void cboMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnAgregarRuta_Click(object sender, EventArgs e)
    {
        if (LstRutaEntity == null)
            LstRutaEntity = new List<RutaEntity>();
        if (this.cboTipoRuta.SelectedValue == "1")
        {
            if (LstRutaEntity.Where(x => x.DepartamentoID == Convert.ToInt32(this.cboDepartamento.SelectedValue) && x.MunicipioID == Convert.ToInt32(this.cboMunicipio.SelectedValue)).Count() == 0)
            {
                LstRutaEntity.Add(new RutaEntity
                {
                    Barrio = this.txtBarrio.Text.Trim(),
                    DepartamentoID = Convert.ToInt32(this.cboDepartamento.SelectedValue),
                    Departamento = this.cboDepartamento.SelectedItem.Text,
                    MunicipioID = Convert.ToInt32(this.cboMunicipio.SelectedValue),
                    Municipio = this.cboMunicipio.SelectedItem.Text,
                    Orden = LstRutaEntity.Count() + 1,
                    TipoRutaID = Convert.ToInt32(this.cboTipoRuta.SelectedValue)
                });
            }
        }
        else if (this.cboTipoRuta.SelectedValue == "2")
        {
            if (LstRutaEntity.Where(x => x.DepartamentoID == Convert.ToInt32(this.cboDepartamento.SelectedValue) && x.MunicipioID == Convert.ToInt32(this.cboMunicipio.SelectedValue) && x.Barrio == txtBarrio.Text.Trim()).Count() == 0)
            {
                LstRutaEntity.Add(new RutaEntity
                {
                    Barrio = this.txtBarrio.Text.Trim(),
                    DepartamentoID = Convert.ToInt32(this.cboDepartamento.SelectedValue),
                    Departamento = this.cboDepartamento.SelectedItem.Text,
                    MunicipioID = Convert.ToInt32(this.cboMunicipio.SelectedValue),
                    Municipio = this.cboMunicipio.SelectedItem.Text,
                    Orden = LstRutaEntity.Count() + 1,
                    TipoRutaID = Convert.ToInt32(this.cboTipoRuta.SelectedValue)
                });
            }
        }



        if (this.cboTipoRuta.SelectedValue == "2")
        {
            this.cboDepartamento.Enabled = false;
            this.cboMunicipio.Enabled = false;
            this.txtBarrio.Text = string.Empty;
            this.grvRutaDesplazamiento.Columns[2].Visible = true;
        }
        else
        {
            this.cboDepartamento.Enabled = true;
            this.cboMunicipio.Enabled = true;
            this.grvRutaDesplazamiento.Columns[2].Visible = false;
        }
        this.grvRutaDesplazamiento.DataSource = LstRutaEntity;
        this.grvRutaDesplazamiento.DataBind();
    }
    protected void cboModoTransporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboModoTransporte.SelectedValue != string.Empty)
        {
            TipoTransporte clsTipoTransporte = new TipoTransporte();
            LstTipoTransporte = clsTipoTransporte.ListaTipoTransportePorModoTransporte(Convert.ToInt32(this.cboModoTransporte.SelectedValue));
            Utilidades.LlenarComboLista(LstTipoTransporte, cboTipoVehiculo, "TipoTransporte", "TipoTransporteID", true);
        }
        else
        {
            Utilidades.LlenarComboVacio(cboTipoVehiculo);
            cboTipoVehiculo_SelectedIndexChanged(null, null);
        }
    }
    protected void cboTipoVehiculo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoVehiculo.SelectedItem.Text.Trim().Contains("Otro"))
        {
            this.trOtroTipoVehiculo.Visible = true;
            this.txtOtroTipoVehiculo.Text = string.Empty;
        }
        else
        {
            this.trOtroTipoVehiculo.Visible = false;
            this.txtOtroTipoVehiculo.Text = string.Empty;
        }
    }
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        int SalvoconductoId = 0;
        if (IsValid)
        {
            // validamos las lista de las especies y la de la ruta
            if (LstEspecimenNewIdentity == null || LstEspecimenNewIdentity.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe agregar al menos una especie a la solicitud.');", true);
                return;
            }
            if (LstRutaEntity == null || LstRutaEntity.Count < 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe agregar una ruta a la solicitud.');", true);
                return;
            }
            if (LstTransporte == null || LstTransporte.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe agregar un transporte a la solicitud.');", true);
                return;
            }
            // validamos si el tipo de vehiculo tiene capacidad promedio de carga y validamos si el volumen total de las especies es no supera esa capacidad
            foreach (var transporte in LstTransporte)
            {
                string validacionVolumen = "";
                TipoTransporte objTipoTransporte = new TipoTransporte();
                var tipoTransporte = objTipoTransporte.ListaTipoTransportePorModoTransporte(transporte.ModoTransporteID).Where(x => x.TipoTransporteID == transporte.TipoTransporteID).FirstOrDefault();
                if (this.cboClaseRecurso.SelectedValue == "1")
                {
                    if (tipoTransporte != null)
                    {
                        if (tipoTransporte.CapacidadPromedioCarga != null && tipoTransporte.CapacidadPromedioCarga > 0)
                        {
                            var volumenTotal = LstEspecimenNewIdentity.Sum(x => x.Volumen);
                            if (volumenTotal > tipoTransporte.CapacidadPromedioCarga)
                            {
                                validacionVolumen += string.Format("alert('El volumen de las especies {0} es superior a la capacidad de carga del tipo de vehículo {1} m3.');", volumenTotal.ToString(), tipoTransporte.CapacidadPromedioCarga.ToString());
                            }
                        }
                    }
                }
                if (validacionVolumen != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", validacionVolumen, true);
                    return;
                }
            }

            SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
            SalvoconductoNewIdentity objSalvoconductoNewIdentity = new SalvoconductoNewIdentity();
            Aprovechamiento clsAprovechamiento = new Aprovechamiento();
            if (this.cboAprovechamiento.Visible)
            {
                if (LstAprovechamientoOrigen.Count() == 1)
                {
                    var aprovechamiento = clsAprovechamiento.ConsultaAprovechamientoXAprovechamientoId(Convert.ToInt32(this.cboAprovechamiento.SelectedValue));
                    objSalvoconductoNewIdentity.CorregimientoProcedencia = aprovechamiento.CorregimientoProcedencia;
                    objSalvoconductoNewIdentity.DepartamentoProcedenciaID = aprovechamiento.DepartamentoProcedenciaID;
                    objSalvoconductoNewIdentity.MunicipioProcedenciaID = aprovechamiento.MunicipioProcedenciaID;
                    objSalvoconductoNewIdentity.VeredaProcedencia = aprovechamiento.VeredaProcedencia;
                    objSalvoconductoNewIdentity.EstablecimientoProcedencia = aprovechamiento.EstablecimientoProcedencia;
                }
                else
                {
                    var aprovechamiento = clsAprovechamiento.ConsultaAprovechamientoXAprovechamientoId(Convert.ToInt32(this.cboAprovechamiento.SelectedValue));
                    objSalvoconductoNewIdentity.CorregimientoProcedencia = aprovechamiento.CorregimientoProcedencia;
                    objSalvoconductoNewIdentity.DepartamentoProcedenciaID = aprovechamiento.DepartamentoProcedenciaID;
                    objSalvoconductoNewIdentity.MunicipioProcedenciaID = aprovechamiento.MunicipioProcedenciaID;
                    objSalvoconductoNewIdentity.VeredaProcedencia = aprovechamiento.VeredaProcedencia;
                    objSalvoconductoNewIdentity.EstablecimientoProcedencia = aprovechamiento.EstablecimientoProcedencia;
                }

            }
            else
            {
                if (this.cboTipoSalvoconducto.SelectedValue == "3" || this.cboTipoSalvoconducto.SelectedValue == "2")
                {
                    var rutaOrigen = LstRutaEntity.First();
                    objSalvoconductoNewIdentity.CorregimientoProcedencia = rutaOrigen.Corregimiento;
                    objSalvoconductoNewIdentity.DepartamentoProcedenciaID = rutaOrigen.DepartamentoID;
                    objSalvoconductoNewIdentity.MunicipioProcedenciaID = rutaOrigen.MunicipioID;
                    objSalvoconductoNewIdentity.VeredaProcedencia = rutaOrigen.Vereda;
                }
            }
            objSalvoconductoNewIdentity.AutoridadEmisoraID = Convert.ToInt32(this.cboAutoridadAmbientalEmisora.SelectedValue);
            objSalvoconductoNewIdentity.ClaseRecursoID = Convert.ToInt32(this.cboClaseRecurso.SelectedValue);
            objSalvoconductoNewIdentity.EstadoID = 1; //estado de solicitud
            objSalvoconductoNewIdentity.FinalidadID = Convert.ToInt32(this.cboFinalidadRecurso.SelectedValue);
            if (this.cboTipoSalvoconducto.SelectedValue == "3")
            {
                objSalvoconductoNewIdentity.LstEspecimen = EspeciesRenovar();
            }
            else
            {
                objSalvoconductoNewIdentity.LstEspecimen = LstEspecimenNewIdentity;
            }
            objSalvoconductoNewIdentity.LstRuta = LstRutaEntity;
            if (this.tblSalvoanterior.Visible && this.trTitular.Visible && this.txtSolicitante.Text.Trim() != string.Empty)
            {
                objSalvoconductoNewIdentity.SolicitanteID = Convert.ToInt32(this.hfIdSolicitante.Value);
                objSalvoconductoNewIdentity.Solicitante = this.txtSolicitante.Text.Trim();
            }
            else
            {
                objSalvoconductoNewIdentity.SolicitanteID = (Int32)Silpa.Workflow.AccesoDatos.ApplicationUserDao.ObtenerIdUsuario(DatosSesion.Usuario);
            }
            objSalvoconductoNewIdentity.TipoSalvoconductoID = Convert.ToInt32(this.cboTipoSalvoconducto.SelectedValue);
            objSalvoconductoNewIdentity.LstTransporte = LstTransporte;
            objSalvoconductoNewIdentity.LstSalvoconductoAnterior = LstSalvoconductoAnterior;
            objSalvoconductoNewIdentity.LstAprovechamientoOrigen = LstAprovechamientoOrigen;
            objSalvoconductoNewIdentity.FechaInicioVigencia = Convert.ToDateTime(this.txtFechaDesde.Text);
            objSalvoconductoNewIdentity.FechaFinalVigencia = Convert.ToDateTime(this.txtFechaHasta.Text);

            if (objSalvoconductoNewIdentity.SolicitanteID == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Su sesion ha caducado, por favor salga de la aplicaion y vuelva a ingresar.');", true);
                return;
            }
            try
            {
                //jmartinez salvoconducto Fase 2 Valido el Salvoconducto por Base de Datos
                SalvoconductoId = clsSalvoconductoNew.CrearSolicitudSalvoconducto(ref objSalvoconductoNewIdentity);

                DataSet ds = new DataSet();
                ds = clsSalvoconductoNew.ValidarSalvoconducto(SalvoconductoId);
                mpeValidacionSUNL.Hide();
                DivValidacionSUNL.Visible = false;
                if (ds != null && ds.Tables.Count > 0 && ((ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) || (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)))
                {
                    mpeValidacionSUNL.Show();
                    DivValidacionSUNL.Visible = true;
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 )
                    {
                        GrvSaldoEspecies.DataSource = ds.Tables[0];
                        GrvSaldoEspecies.DataBind();
                        clsSalvoconductoNew.EliminarSalvoconducto(SalvoconductoId);
                    }

                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        GrvValidacionGeneral.DataSource = ds.Tables[1];
                        GrvValidacionGeneral.DataBind();
                        clsSalvoconductoNew.EliminarSalvoconducto(SalvoconductoId);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Su solicitud de salvoconducto ha sido registrado con éxito. Nro de solicitud: " + objSalvoconductoNewIdentity.NumeroVitalTramite + ".');", true);
                    LimpiarTodo();
                }

            }
            catch (Exception exc)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + exc.Message + ".');", true);
            }
        }
    }
    protected void lnkSolicitante_Click(object sender, EventArgs e)
    {
        this.mpeSolicitantes.Show();
    }
    protected void btnBuscarSolicitante_Click(object sender, EventArgs e)
    {
        PersonaDalc persona = new PersonaDalc();
        DataTable datos = persona.ConsultarPersona(Convert.ToInt32(this.cboTipoIdentificacion.SelectedValue), this.txtNumeroIdentificacion.Text);
        if (datos.Rows.Count > 0)
        {
            this.lblNombreSolicitante.Text = datos.Rows[0]["NOMBRE"].ToString();
            this.TitularSalvoconducto = datos.Rows[0]["ID_APPLICATION_USER"].ToString();
            this.lnkSeleccionarSolicitante.Visible = true;
        }
        else
        {
            this.lblNombreSolicitante.Text = "";
            this.lnkSeleccionarSolicitante.Visible = false;
            this.lnkSeleccionarSolicitante.Visible = false;
            lblNombreSolicitante.Text = "El usuario debe estar registrado en VITAL";
        }
        this.mpeSolicitantes.Show();
    }
    protected void lnkSeleccionarSolicitante_Click(object sender, EventArgs e)
    {
        this.txtSolicitante.Text = lblNombreSolicitante.Text;
        this.hfIdSolicitante.Value = TitularSalvoconducto;
        LimpiarTitular();
        this.mpeSolicitantes.Hide();
    }
    protected void btnCerrarVinculosActividad_Click(object sender, EventArgs e)
    {
        LimpiarTitular();
        this.mpeSolicitantes.Hide();
    }
    protected void lnkLimpiarSolicitante_Click(object sender, EventArgs e)
    {
        this.txtSolicitante.Text = string.Empty;
        this.hfIdSolicitante.Value = string.Empty;
    }
    protected void btnBuscarOtroSalvoconducto_Click(object sender, EventArgs e)
    {
        if (this.txtOtroSalboconducto.Text.Trim() != string.Empty)
        {
            if (this.cboClaseRecurso.SelectedValue != string.Empty)
            {
                SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
                this.grvOtroSalvoconducto.DataSource = clsSalvoconductoNew.ListaSalvoconducto(null, null, null, null, null, null, Convert.ToInt32(this.cboClaseRecurso.SelectedValue), null, this.txtOtroSalboconducto.Text);
                this.grvOtroSalvoconducto.DataBind();
            }
            else
            {
                this.grvOtroSalvoconducto.DataSource = null;
                this.grvOtroSalvoconducto.DataBind();
            }
        }
        else
        {
            this.grvOtroSalvoconducto.DataSource = null;
            this.grvOtroSalvoconducto.DataBind();
        }
        this.mpeOtroSalvoconducto.Show();
    }
    protected void grvOtroSalvoconducto_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (LstSalvoconductoAnterior == null)
            LstSalvoconductoAnterior = new List<SalvoconductoAnterior>();
        if (LstEspeciesSalvoAnterior == null)
            LstEspeciesSalvoAnterior = new List<EspecimenNewIdentity>();
        string dataKeys = ((GridView)sender).DataKeys[e.NewEditIndex].Value.ToString();
        Label lblTipoSalvoconducto = (Label)grvOtroSalvoconducto.Rows[e.NewEditIndex].FindControl("lblTipoSalvoconducto");
        if (LstSalvoconductoAnterior.Where(x => x.SalvoconductoID == Convert.ToInt32(dataKeys)).Count() == 0)
        {
            SalvoconductoNew vSalvoconductoNew = new SalvoconductoNew();
            var lstEspecies = vSalvoconductoNew.ListaEspecieSalvoconducto(Convert.ToInt32(dataKeys));
            LstSalvoconductoAnterior.Add(new SalvoconductoAnterior { SalvoconductoID = Convert.ToInt32(dataKeys), Detalle = this.txtOtroSalboconducto.Text + " - " + lblTipoSalvoconducto.Text });
            LstEspeciesSalvoAnterior.AddRange(lstEspecies);
            Utilidades.LlenarComboLista(LstEspeciesSalvoAnterior, cboEspecies, "NombreEspecie", "EspecieSalvoconductoID", true);
            this.grvSalvoconductoAnterior.DataSource = LstSalvoconductoAnterior;
            this.grvSalvoconductoAnterior.DataBind();
        }
        this.mpeOtroSalvoconducto.Hide();

    }
    protected void lnkOtroSalvoconducto_Click(object sender, EventArgs e)
    {
        this.mpeOtroSalvoconducto.Show();
    }
    protected void btnCerrarOtroSalvoconducto_Click(object sender, EventArgs e)
    {
        LimpiarOtroSalvoconducto();
        this.mpeOtroSalvoconducto.Hide();
    }
    #endregion
    #region Metodos
    public void CargarPagina()
    {
        SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();

        TipoSalvoconducto clsTipoSalvoconducto = new TipoSalvoconducto();
        ClaseRecurso clsClaseRecurso = new ClaseRecurso();
        SalvoconductoNew clsSalvoconductoNew = new SalvoconductoNew();
        ModoTransporte clsModoTransporte = new ModoTransporte();
        Listas _listaTiposId = new Listas();
        Utilidades.LlenarComboLista(clsClaseRecurso.ListaClaseRecurso(), cboClaseRecurso, "ClaseRecurso", "ClaseRecursoID", true);
        Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridades(null).Tables[0], cboAutoridadAmbientalEmisora, "AUT_NOMBRE", "AUT_ID", true);
        //Utilidades.LlenarComboTabla(_listaAutoridades.ListarAutoridadesSalvoConducto(null).Tables[0], cboAutoridadAmbientalEmisora, "AUT_NOMBRE", "AUT_ID", true);

        Utilidades.LlenarComboLista(clsTipoSalvoconducto.ListaTipoSalvoconducto(), cboTipoSalvoconducto, "TipoSalvoconducto", "TipoSalvoconductoID", true);
        Utilidades.LlenarComboLista(clsSalvoconductoNew.ListaTipoRuta(), cboTipoRuta, "TipoRutaDescripcion", "TipoRutaID", true);
        Utilidades.LlenarComboLista(clsModoTransporte.ListaModoTransporte(), cboModoTransporte, "ModoTransporte", "ModoTransporteID", true);
        SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
        this.cboTipoSalvoconducto.Enabled = true;
        DataTable dtDeptos = _listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0].AsEnumerable().Where(x => x.Field<int>("DEP_ID") != -1).CopyToDataTable();
        Utilidades.LlenarComboTabla(dtDeptos, cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
        Utilidades.LlenarComboVacio(cboMunicipio);
        Utilidades.LlenarComboVacio(cboAprovechamiento);
        Utilidades.LlenarComboVacio(cboFinalidadRecurso);
        Utilidades.LlenarComboVacio(cboClaseProducto);
        Utilidades.LlenarComboVacio(cboEspecies);
        Utilidades.LlenarComboVacio(cboTipoProducto);
        Utilidades.LlenarComboVacio(cboTipoVehiculo);
        DataSet _temp = _listaTiposId.ListaTipoIdentificacion();
        cboTipoIdentificacion.DataSource = _temp.Tables[0]; // ListaDocumentos(_temp, "TPE_ID = " + Convert.ToString((int)TipoPersona.Natural));
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacion, "TID_NOMBRE", "TID_ID", true);
        Utilidades.LlenarComboTabla(_temp.Tables[0], cboTipoIdentificacionTitularApro, "TID_NOMBRE", "TID_ID", true);

    }
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
    private void HabilitarDimensiones()
    {
        LblAncho.Text = "ancho:";
        trDimenciones.Visible = false;
        tdAlto.Visible = false;
        tdLargo.Visible = false;
        tdAncho.Visible = false;
        tdLongitud.Visible = false;
        this.txtAlto.Text = string.Empty;
        this.txtLargo.Text = string.Empty;
        this.txtAncho.Text = string.Empty;
        if (this.cboClaseRecurso.SelectedValue == "1" && this.cboClaseProducto.SelectedValue == "18")
        {
            trDimenciones.Visible = true;
            tdAlto.Visible = true;
            tdLargo.Visible = true;
            tdAncho.Visible = true;
        }
        else if (this.cboClaseRecurso.SelectedValue == "1" && this.cboClaseProducto.SelectedValue == "17")
        {
            trDimenciones.Visible = true;
            //jmartinez salvoconducto Fase 2
            LblAncho.Text = "diametro:";
            tdLargo.Visible = true;
            tdAncho.Visible = true;
        }
        else if (this.cboClaseRecurso.SelectedValue == "2" && this.cboClaseProducto.SelectedValue == "10")
        {
            trDimenciones.Visible = true;
            tdLongitud.Visible = true;
        }
    }
    private void CargarOriguenSalvoconducto()
    {
        this.cboSalvoconductoAnterior.Attributes.Remove("onchange");
        this.cboAprovechamiento.Attributes.Remove("onchange");
        FinalidadAprovechamiento clsFinalidadAprovechamiento = new FinalidadAprovechamiento();
        ClaseProducto vClaseProducto = new ClaseProducto();
        this.tblAprovechamiento.Visible = false;
        this.tblSalvoanterior.Visible = false;
        Utilidades.LlenarComboVacio(cboAprovechamiento);
        Utilidades.LlenarComboVacio(cboSalvoconductoAnterior);
        Utilidades.LlenarComboVacio(cboEspecies);
        LstSalvoconductoAnterior = new List<SalvoconductoAnterior>();
        LstAprovechamientoOrigen = new List<AprovechamientoOrigen>();
        this.grvSalvoconductoAnterior.DataSource = null;
        this.grvSalvoconductoAnterior.DataBind();
        this.grvAprovechamientoOrigen.DataSource = null;
        this.grvAprovechamientoOrigen.DataBind();
        LimpiarPestañaEspecimenes();
        this.btnAgregarSalvoconductoAnterior.Visible = false;
        this.btnAgregarAprovechamientoOrigen.Visible = false;
        this.lnkOtroSalvoconducto.Visible = false;

        //JMARTINEZ SALVOCONDCUTO FASE 2
        LstSalvoconductoAnterior = null;
        LstEspeciesSalvoAnterior = null;

        if (this.cboTipoSalvoconducto.SelectedValue != "")
        {
            switch (this.cboTipoSalvoconducto.SelectedValue)
            {
                case "1": // movilizacion
                    this.tblAprovechamiento.Visible = true;
                    if (this.cboClaseRecurso.SelectedValue == "3")
                    {
                        this.cboAprovechamiento.SelectedIndexChanged -= new EventHandler(cboAprovechamiento_SelectedIndexChanged);
                        this.cboAprovechamiento.Attributes.Remove("onchange");
                        this.btnAgregarAprovechamientoOrigen.Visible = true;
                        this.cboAprovechamiento.AutoPostBack = false;
                    }
                    else
                    {
                        this.cboAprovechamiento.SelectedIndexChanged += new EventHandler(cboAprovechamiento_SelectedIndexChanged);
                        this.cboAprovechamiento.AutoPostBack = true;
                    }
                    break;
                case "2": // removilizacion
                    this.tblSalvoanterior.Visible = true;
                    this.btnAgregarSalvoconductoAnterior.Visible = true;
                    this.lnkOtroSalvoconducto.Visible = true;
                    this.cboSalvoconductoAnterior.Attributes.Remove("onchange");
                    this.trTitular.Visible = true;
                    LimpiarTitular();
                    break;
                case "3": //renovacion
                    this.tblSalvoanterior.Visible = true;
                    this.cboSalvoconductoAnterior.SelectedIndexChanged += new EventHandler(cboSalvoconductoAnterior_SelectedIndexChanged);
                    this.cboSalvoconductoAnterior.AutoPostBack = true;
                    this.trTitular.Visible = false;
                    break;
            }
            if (tblAprovechamiento.Visible)
            {
                if (this.cboClaseRecurso.SelectedValue != string.Empty)
                {
                    Aprovechamiento vAprovechamiento = new Aprovechamiento();
                    var lstaprovehcamiento = vAprovechamiento.ConsultaAprovechamientoAutoridadSolicitante(null, Convert.ToInt32(Session["Usuario"]), Convert.ToInt32(this.cboClaseRecurso.SelectedValue));
                    lstaprovehcamiento = lstaprovehcamiento.Where(x => x.LstEspecies.DefaultIfEmpty().Where(y => y.CantidadDisponible > 0).Count() > 0).ToList();
                    Utilidades.LlenarComboLista(lstaprovehcamiento, this.cboAprovechamiento, "Detalle", "AprovechamientoID", true);
                    Utilidades.LlenarComboLista(clsFinalidadAprovechamiento.ListaFinalidadAprovechamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue)), cboFinalidadRecurso, "FinalidadRecurso", "FinalidadRecursoId", true);
                    Utilidades.LlenarComboLista(vClaseProducto.ListaClaseProducto(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), true), cboClaseProducto, "ClaseProducto", "ClaseProductoID", true);
                }
            }
            else if (tblSalvoanterior.Visible)
            {
                if (this.cboClaseRecurso.SelectedValue != string.Empty)
                {
                    SalvoconductoNew vclsSalvoconducto = new SalvoconductoNew();
                    var lstSalvoconductos = vclsSalvoconducto.ListaSalvoconducto(null, Convert.ToInt32(Session["Usuario"]), 2, null, Convert.ToInt32(this.cboClaseRecurso.SelectedValue));
                    lstSalvoconductos = lstSalvoconductos.Where(x => x.LstEspecimen.Count() > 0).ToList();
                    lstSalvoconductos = lstSalvoconductos.Where(x => x.LstEspecimen.DefaultIfEmpty().Where(y => y.CantidadDisponible > 0).Count() > 0).ToList();
                    Utilidades.LlenarComboLista(lstSalvoconductos, this.cboSalvoconductoAnterior, "Detalle", "SalvoconductoID", true);
                    Utilidades.LlenarComboLista(clsFinalidadAprovechamiento.ListaFinalidadAprovechamiento(Convert.ToInt32(this.cboClaseRecurso.SelectedValue)), cboFinalidadRecurso, "FinalidadRecurso", "FinalidadRecursoId", true);
                    Utilidades.LlenarComboLista(vClaseProducto.ListaClaseProducto(Convert.ToInt32(this.cboClaseRecurso.SelectedValue), true), cboClaseProducto, "ClaseProducto", "ClaseProductoID", true);

                }
            }
        }
        else
        {
            Utilidades.LlenarComboVacio(this.cboAprovechamiento);

        }
    }
    private void LimpiarPestañaEspecimenes()
    {
        LstEspeciesAprovechamientoOrigen = new List<EspecieAprovechamientoIdentity>();
        LstSalvoconductoAnterior = new List<SalvoconductoAnterior>();
        LstEspecimenNewIdentity = new List<EspecimenNewIdentity>();
        this.grvEspecies.DataSource = null;
        this.grvEspecies.DataBind();
        //jmartinez Salvoconducto fase 2
        this.GrvTotalesEspecies.DataSource = null;
        this.GrvTotalesEspecies.DataBind();
        this.LblLstTotalTipProductUm.Visible = false;
        this.lblCantidadDisponible.Text = "";
        this.lblUnidadMedida.Text = "";
        Utilidades.LlenarComboVacio(this.cboClaseProducto);
        Utilidades.LlenarComboVacio(this.cboTipoProducto);
        this.txtCantidad.Text = "";
    }
    public void ReiniciarPestañaEspecimenes()
    {
        //jmartinez salvoconducto fase 2
        //this.TxtNombreComunEspecie.Text = string.Empty;
        this.cboEspecies.SelectedValue = "";
        this.cboClaseProducto.SelectedValue = "";
        cboEspecies_SelectedIndexChanged(null, null);
        this.cboClaseProducto_SelectedIndexChanged(null, null);
        this.txtCantidad.Text = string.Empty;
        this.txtAlto.Text = string.Empty;
        this.txtAncho.Text = string.Empty;
        this.txtLargo.Text = string.Empty;
        this.txtLongitud.Text = string.Empty;
        this.txtDescripcion.Text = string.Empty;
        this.txtIdentificacion.Text = string.Empty;
    }
    private void LimpiarTodo()
    {
        tabInfoGeneral.Focus();
        this.cboAutoridadAmbientalEmisora.SelectedIndex = 0;
        //cboAutoridadAmbientalEmisora_SelectedIndexChanged(null, null);
        LimpiarPestañaEspecimenes();
        this.cboTipoSalvoconducto.SelectedIndex = 0;
        cboTipoSalvoconducto_SelectedIndexChanged(null, null);
        this.cboClaseRecurso.SelectedIndex = 0;
        cboClaseRecurso_SelectedIndexChanged(null, null);
        this.cboTipoRuta.SelectedIndex = 0;
        cboTipoRuta_SelectedIndexChanged(null, null);
        this.cboModoTransporte.SelectedIndex = 0;
        cboModoTransporte_SelectedIndexChanged(null, null);
        this.txtEmpresa.Text = "";
        this.txtIdentificacionTransporte.Text = "";
        this.txtNombreTransportador.Text = "";
        this.txtIdentificacionTransportador.Text = "";
        this.cboFinalidadRecurso.SelectedIndex = 0;
        this.txtFechaDesde.Text = string.Empty;
        this.txtFechaHasta.Text = string.Empty;
        this.grvTransporte.DataSource = null;
        this.grvTransporte.DataBind();
        //JMARTINEZ SALVOCONDUCTO FASE 2
        LstSalvoconductoAnterior = null;
        LstEspeciesSalvoAnterior = null;
        LstTransporte = new List<TransporteNewIdentity>();
    }
    protected void LimpiarTitular()
    {
        this.cboTipoIdentificacion.SelectedIndex = 0;
        this.txtNumeroIdentificacion.Text = "";
        this.lblNombreSolicitante.Text = "";
        this.lnkSeleccionarSolicitante.Visible = false;
    }
    protected void LimpiarOtroSalvoconducto()
    {
        this.txtOtroSalboconducto.Text = string.Empty;
        this.grvOtroSalvoconducto.DataSource = null;
        this.grvOtroSalvoconducto.DataBind();
    }
    protected List<EspecimenNewIdentity> EspeciesRenovar()
    {
        foreach (EspecimenNewIdentity especimen in LstEspecimenNewIdentity)
        {
            especimen.Volumen = especimen.CantidadDisponible;
        }
        return LstEspecimenNewIdentity;
    }

    #endregion
    #region Clases
    [Serializable]
    public class AprovechamientoAnterior
    {
        public int AprovechamientoID { get; set; }
        public string Detalle { get; set; }
    }
    #endregion

    protected void btnAgregarTransporte_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int contador = 0;
            if (LstTransporte == null)
            {
                LstTransporte = new List<TransporteNewIdentity>();
            }
            contador = LstTransporte.Count() + 1;
            LstTransporte.Add(new TransporteNewIdentity
            {
                ModoTransporteID = Convert.ToInt32(this.cboModoTransporte.SelectedValue),
                ModoTransporte = this.cboModoTransporte.SelectedItem.Text,
                TipoTransporteID = Convert.ToInt32(this.cboTipoVehiculo.SelectedValue),
                TipoTransporte = this.cboTipoVehiculo.SelectedItem.Text.Contains("Otro") ? this.txtOtroTipoVehiculo.Text.Trim() : this.cboTipoVehiculo.SelectedItem.Text,
                Empresa = this.txtEmpresa.Text.Trim(),
                NumeroIdentificacionMedioTransporte = this.txtIdentificacionTransporte.Text.Trim(),
                NombreTransportador = this.txtNombreTransportador.Text.Trim(),
                NumeroIdentificacionTransportador = this.txtIdentificacionTransportador.Text.Trim(),
                TransporteSalvoconductoID = contador
            });
            grvTransporte.DataSource = LstTransporte;
            grvTransporte.DataBind();
            ReiniciarPestañaTransporte();
        }
    }

    private void ReiniciarPestañaTransporte()
    {
        this.cboModoTransporte.SelectedIndex = 0;
        this.cboTipoVehiculo.SelectedIndex = 0;
        this.txtEmpresa.Text = string.Empty;
        this.txtIdentificacionTransporte.Text = string.Empty;
        this.txtNombreTransportador.Text = string.Empty;
        this.txtIdentificacionTransportador.Text = string.Empty;
    }
    protected void grvTransporte_lnkEliminar_Click(object sender, EventArgs e)
    {
        int Orden = 0;
        int identity = 0;
        try
        {
            //Cargamos el id del salvoconducto
            Orden = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString().Trim());
            identity = LstTransporte.Where(x => x.TransporteSalvoconductoID == Orden).FirstOrDefault().TransporteSalvoconductoID;
            LstTransporte.RemoveAll(x => x.TransporteSalvoconductoID == Orden);

            Orden = 0;
            foreach (var transporte in LstTransporte)
            {
                transporte.TransporteSalvoconductoID = Orden + 1;
            }
            this.grvTransporte.DataSource = LstTransporte;
            this.grvTransporte.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void grvEspecies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvEspecies.PageIndex = e.NewPageIndex;
        this.grvEspecies.DataSource = LstEspecimenNewIdentity;
        this.grvEspecies.DataBind();
    }


    protected void Salir_Click(object sender, EventArgs e)
    {
        mpeValidacionSUNL.Hide();
        DivValidacionSUNL.Visible = false;
    }
}