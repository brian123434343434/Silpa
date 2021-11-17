using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SILPA.LogicaNegocio.Salvoconducto;
using SILPA.AccesoDatos.Salvoconducto;
using System.Data;
using AjaxControlToolkit;
using System.Web.UI;
using SILPA.LogicaNegocio.Generico;
public partial class Salvoconducto_ModuloAdminEspeciesProductos : System.Web.UI.Page
{
    #region ViewState
    public List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto> LstClaseProductoXtipoProducto { get { return (List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto>)ViewState["LstClaseProductoXtipoProducto"]; } set { ViewState["LstClaseProductoXtipoProducto"] = value; } }
    public List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida> LstTipoProductoUnidadMedida { get { return (List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida>)ViewState["TipoProductoUnidadMedida"]; } set { ViewState["TipoProductoUnidadMedida"] = value; } }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            // USUARIO PARA PRUEBAS
            //Session["Usuario"] = 11075;
            //Session["Usuario"] = 429;
            //this.CargarPagina();
            //return;

            //DESCOMENTAR ANTES DEL COMMIT!!!

            if (new Utilidades().ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                this.CargarPagina();
            }
        }
    }

    public void CargarPagina()
    {
        ClaseRecurso vClaseRecurso = new ClaseRecurso();
        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        Utilidades.LlenarComboLista(vClaseRecurso.ListaClaseRecurso(), CboClaseRecurso, "ClaseRecurso", "ClaseRecursoID", true);
        Utilidades.LlenarComboLista(vClaseRecurso.ListaClaseRecurso(), CboClaseRecursoClaseProducto, "ClaseRecurso", "ClaseRecursoID", true);
        Utilidades.LlenarComboLista(ObjParametrizacionSunl.ObtenerTipoProductoDisponible(0), CboClaseTipoProducto, "TIPO_PRODUCTO", "TIPO_PRODUCTO_ID", true);
        Utilidades.LlenarComboLista(ObjParametrizacionSunl.ObtenerUnidadMedidaDisponible(0), CboUnidadMedida, "UNIDAD_MEDIDAD", "UNIDAD_MEDIDA_ID", true);
    }

    #region Panel Especie

    protected void btnCancelarEspecie_Click(object sender, EventArgs e)
    {
        LimpiarFormularioEspecies();
    }


    protected void btnActualizarEspecie_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int EspecieID = 0;
            ParametrizacionSunlIdentity.EspecieTaxonomia ObjEspecieTaxonomia = new ParametrizacionSunlIdentity.EspecieTaxonomia();
            ParametrizacionSunlDalc ObjParametrizacionSunlDalc = new ParametrizacionSunlDalc();
            if (int.TryParse(hfEspcimenID.Value, out EspecieID)) //si es modificacion
            {
                if (EspecieID > 0)
                {
                    ObjEspecieTaxonomia.EspecieTaxonimiaID = EspecieID;
                }
            }
            else
            {
                ObjEspecieTaxonomia.EspecieTaxonimiaID = 0;
            }
            ObjEspecieTaxonomia.ClaseRecursoID = Convert.ToInt32(this.CboClaseRecurso.SelectedValue);
            ObjEspecieTaxonomia.NombreCientifico = this.txtNombreCientificoEspecie.Text.Trim();
            ObjEspecieTaxonomia.NombreComun = this.txtNombreComunEspecie.Text.Trim();
            ObjEspecieTaxonomia.CodigoIdeam = this.txtCodigoIdeam.Text.Trim();
            ObjParametrizacionSunlDalc.InsertarEspecie(ObjEspecieTaxonomia);
            LimpiarFormularioEspecies();
        }


    }

    protected void lnkEspecie_Click(object sender, EventArgs e)
    {
        LimpiarFormularioBusquedaEspecies();
        this.mpeEspecimen.Show();
    }


    protected void btnBuscarEspecie_Click(object sender, EventArgs e)
    {
        BuscarEspecie();
    }

    protected void dgv_Especies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.dgv_Especies.PageIndex = e.NewPageIndex;
        BuscarEspecie();
    }

    protected void dgv_Especies_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string dataKeys = ((GridView)sender).DataKeys[e.NewEditIndex].Value.ToString();
        Label lblNombreComun = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblNombreComun");
        Label lblNombreCientifico = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblNombreCientifico");
        Label lblClaseRecurso = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblClaseRecurso");
        Label lblCodigoIdeam = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblCodigoIdeam");
        Label lblClaseRecursoID = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblClaseRecursoID");

        string[] especie = { dataKeys, lblNombreComun.Text, lblClaseRecursoID.Text };
        this.txtNombreCientificoEspecie.Text = lblNombreCientifico.Text;
        this.txtNombreComunEspecie.Text = lblNombreComun.Text;
        this.txtCodigoIdeam.Text = lblCodigoIdeam.Text;
        this.CboClaseRecurso.SelectedValue = especie[2];
        this.hfEspcimenID.Value = especie[0];
        this.mpeEspecimen.Hide();
    }


    protected void BuscarEspecie()
    {
        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        var lstEspecimen = ObjParametrizacionSunl.ListarEspecies(this.txtNombreComun.Text, Convert.ToInt32(this.CboClaseRecurso.SelectedValue == string.Empty ? "0" : this.CboClaseRecurso.SelectedValue));


        var lista = (from datos in lstEspecimen
                     select new { ESEPCIE_ID = datos.EspecieTaxonimiaID.ToString(), NOMBRE_COMUN = datos.NombreComun, NOMBRE_CIENTIFICO = datos.NombreCientifico, CLASE_RECURSO = datos.StrClaseREcurso, CODIGO_IDEAM = datos.CodigoIdeam, CLASE_RECURSO_ID = datos.ClaseRecursoID });
        this.dgv_Especies.DataSource = lista.ToList();
        this.dgv_Especies.DataBind();
        this.mpeEspecimen.Show();
    }


    protected void LimpiarFormularioEspecies()
    {
        this.txtNombreCientificoEspecie.Text = string.Empty;
        this.txtNombreComunEspecie.Text = string.Empty;
        this.CboClaseRecurso.SelectedIndex = 0;
        this.txtCodigoIdeam.Text = string.Empty;
        this.hfEspcimenID.Value = null;
        LimpiarFormularioBusquedaEspecies();
    }

    protected void LimpiarFormularioBusquedaEspecies()
    {
        this.txtNombreComun.Text = string.Empty;
        this.dgv_Especies.DataSource = null;
        this.dgv_Especies.DataBind();
    }
    #endregion


    #region Panel Clase Producto
    protected void LnkBuscarClaseProducto_Click(object sender, EventArgs e)
    {
        LimpiarFomularioBusquedaClaseProducto();
        this.MpeClaseProducto.Show();
    }

    protected void BtnBuscarClaseProducto_Click(object sender, EventArgs e)
    {
        BuscarClaseProducto();
    }

    protected void GrvClaseProducto_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int ClaseRecursoID = Convert.ToInt32(((GridView)sender).DataKeys[e.NewEditIndex].Values["CLASE_RECURSO_ID"].ToString());
        int ClaseProductoID = Convert.ToInt32(((GridView)sender).DataKeys[e.NewEditIndex].Values["CLASE_PRODUCTO_ID"].ToString());
        Label lblClaseRecursoClaseProducto = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblClaseRecursoClaseProducto");
        Label lbClaseProducto = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lbClaseProducto");
        Label lblCodigoIdeamClaseProducto = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblCodigoIdeamClaseProducto");
        CheckBox GrvChkSalvoconductoTipoProducto = (CheckBox)((GridView)sender).Rows[e.NewEditIndex].FindControl("GrvChkSalvoconductoTipoProducto");
        CheckBox GrvChkAprovechamientosTipoProducto = (CheckBox)((GridView)sender).Rows[e.NewEditIndex].FindControl("GrvChkAprovechamientosTipoProducto");

        this.CboClaseRecursoClaseProducto.SelectedValue = ClaseRecursoID.ToString();
        this.hfClaseProductoID.Value = ClaseProductoID.ToString();
        this.txtCodigoIdeamClaseProducto.Text = lblCodigoIdeamClaseProducto.Text;
        this.TxtNombreClaseProducto.Text = lbClaseProducto.Text;
        CargarClaseProductoTipoProducto(ClaseProductoID);
        LimpiarFomularioBusquedaClaseProducto();
        this.MpeClaseProducto.Hide();
    }

    protected void GrvClaseProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrvTipoProducto.PageIndex = e.NewPageIndex;
        BuscarClaseProducto();
    }

    protected void BuscarClaseProducto()
    {
        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        var LstClaseproducto = ObjParametrizacionSunl.ListarClaseProducto(Convert.ToInt32(this.CboClaseRecursoClaseProducto.SelectedValue == string.Empty ? "0" : this.CboClaseRecursoClaseProducto.SelectedValue), this.txtClaseProducto.Text);


        var lista = (from datos in LstClaseproducto
                     select new { CLASE_RECURSO_ID = datos.ClaseRecursoID, CLASE_PRODUCTO_ID = datos.ClaseProductoID, CLASE_RECURSO = datos.StrClaseRecurso.ToString(), CLASE_PRODUCTO = datos.StrClaseProducto, CODIGO_IDEAM = datos.CodigoIdeam, SALVOCONDUCTO = datos.Salvoconducto, APROVECHAMIENTO = datos.Aprovechamiento });
        this.GrvTipoProducto.DataSource = lista.ToList();
        this.GrvTipoProducto.DataBind();
        this.MpeClaseProducto.Show();
    }


    protected void CargarClaseProductoTipoProducto(int pClaseProductoId)
    {
        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        //List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto> ObjLstClaseProductoXTipoProducto = new List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto>();
        //ObjLstClaseProductoXTipoProducto = ObjParametrizacionSunl.ListarClaseTipoProducto(pClaseProductoId, 0);
        LstClaseProductoXtipoProducto = ObjParametrizacionSunl.ListarClaseTipoProducto(pClaseProductoId, 0);
        Utilidades.LlenarComboLista(ObjParametrizacionSunl.ObtenerTipoProductoDisponible(pClaseProductoId), CboClaseTipoProducto, "TIPO_PRODUCTO", "TIPO_PRODUCTO_ID", true);


        
        var lista = (from datos in LstClaseProductoXtipoProducto
                     select new { CLASE_PRODUCTO_ID = datos.ClaseProductoID, TIPO_PRODUCTO_ID = datos.TipoProductoID, TIPO_PRODUCTO = datos.StrTipoProducto, CLASE_PRODUCTO = datos.StrClaseProducto, SALVOCONDUCTO = datos.Salvoconducto, APROVECHAMIENTO = datos.Aprovechamiento });
        this.grvClaseProductoTipoProducto.DataSource = lista.ToList();
        this.grvClaseProductoTipoProducto.DataBind();

        this.CboClaseRecursoClaseProducto.Enabled = false;
        this.TxtNombreClaseProducto.Enabled = false;

    }

    public void LimpiarFormularioClaseProducto()
    {
        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        this.CboClaseRecursoClaseProducto.SelectedIndex = 0;
        this.TxtNombreClaseProducto.Text = string.Empty;
        this.txtCodigoIdeamClaseProducto.Text = string.Empty;
        this.hfClaseProductoID.Value = null;
        Utilidades.LlenarComboLista(ObjParametrizacionSunl.ObtenerTipoProductoDisponible(0), CboClaseTipoProducto, "TIPO_PRODUCTO", "TIPO_PRODUCTO_ID", true);
        this.CboClaseTipoProducto.SelectedIndex = 0;
        grvClaseProductoTipoProducto.DataSource = null;
        grvClaseProductoTipoProducto.DataBind();
        LstClaseProductoXtipoProducto = null;
        this.CboClaseRecursoClaseProducto.Enabled = true;
        this.TxtNombreClaseProducto.Enabled = true;
        LimpiarFomularioBusquedaClaseProducto();
    }

    public void LimpiarFomularioBusquedaClaseProducto()
    {
        this.txtClaseProducto.Text = string.Empty;
        this.GrvTipoProducto.DataSource = null;
        this.GrvTipoProducto.DataBind();
    }

    protected void BtnAgregarTipoProducto_Click(object sender, EventArgs e)
    {
        ParametrizacionSunlIdentity.ClaseProductoXTipoProducto ObjClaseProductoXTipoProducto = new ParametrizacionSunlIdentity.ClaseProductoXTipoProducto();
        int ClaseproductoID = 0;
        if (LstClaseProductoXtipoProducto == null)
        {
            LstClaseProductoXtipoProducto = new List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto>();
        }

        if (this.hfClaseProductoID.Value != null && this.hfClaseProductoID.Value != string.Empty && int.TryParse(this.hfClaseProductoID.Value, out ClaseproductoID))
        {
            ObjClaseProductoXTipoProducto.ClaseProductoID = Convert.ToInt32(this.hfClaseProductoID.Value);
        }
        else
        {
            ObjClaseProductoXTipoProducto.ClaseProductoID = 0;
        }

        var ValidacionTipoProducto = LstClaseProductoXtipoProducto.Where(x => x.TipoProductoID == Convert.ToInt32(this.CboClaseTipoProducto.SelectedValue));

        if (ValidacionTipoProducto != null && ValidacionTipoProducto.Count() > 0)
        {
            foreach (var TipoProducto in ValidacionTipoProducto)
            {
                TipoProducto.Salvoconducto = Convert.ToBoolean(this.ChkVerTipoProductoSUNL.Checked);
                TipoProducto.Aprovechamiento = Convert.ToBoolean(this.ChkVerTipoProductoAprovechamiento.Checked);
            }
        }
        else
        {
            ObjClaseProductoXTipoProducto.TipoProductoID = Convert.ToInt32(this.CboClaseTipoProducto.SelectedValue);
            ObjClaseProductoXTipoProducto.StrClaseProducto = this.TxtNombreClaseProducto.Text;
            ObjClaseProductoXTipoProducto.StrTipoProducto = this.CboClaseTipoProducto.SelectedItem.Text;
            ObjClaseProductoXTipoProducto.Salvoconducto = Convert.ToBoolean(this.ChkVerTipoProductoSUNL.Checked);
            ObjClaseProductoXTipoProducto.Aprovechamiento = Convert.ToBoolean(this.ChkVerTipoProductoAprovechamiento.Checked);
            LstClaseProductoXtipoProducto.Add(ObjClaseProductoXTipoProducto);
        }




        this.CboClaseTipoProducto.Items.Remove(this.CboClaseTipoProducto.SelectedItem);
        this.CboClaseTipoProducto.DataBind();

        var lista = (from datos in LstClaseProductoXtipoProducto
                     select new { CLASE_PRODUCTO_ID = datos.ClaseProductoID, TIPO_PRODUCTO_ID = datos.TipoProductoID, TIPO_PRODUCTO = datos.StrTipoProducto, CLASE_PRODUCTO = datos.StrClaseProducto, SALVOCONDUCTO = datos.Salvoconducto, APROVECHAMIENTO = datos.Aprovechamiento });
        this.grvClaseProductoTipoProducto.DataSource = lista.ToList();
        this.grvClaseProductoTipoProducto.DataBind();

        CboClaseTipoProducto.Enabled = true;
        this.TxtNombreClaseProducto.Enabled = false;
        this.CboClaseRecursoClaseProducto.Enabled = false;


    }
    protected void grvClaseProductoTipoProducto_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int TipoProductoID = Convert.ToInt32(((GridView)sender).DataKeys[e.NewEditIndex].Values["TIPO_PRODUCTO_ID"].ToString());
        Label lblNomLblTipoProducto = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblNomLblTipoProducto");
        this.hfClaseProductoID.Value = TipoProductoID.ToString();

        CboClaseTipoProducto.Enabled = false;

        if (!this.CboClaseTipoProducto.SelectedValue.Contains(TipoProductoID.ToString()))
        {
            this.CboClaseTipoProducto.Items.Add(new ListItem(lblNomLblTipoProducto.Text, TipoProductoID.ToString()));
            this.CboClaseTipoProducto.SelectedValue = TipoProductoID.ToString();
        }
        else
        {
            this.CboClaseTipoProducto.SelectedValue = TipoProductoID.ToString();
        }

    }

    protected void grvClaseProductoTipoProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvClaseProductoTipoProducto.PageIndex = e.NewPageIndex;
        CargarClaseProductoTipoProducto(Convert.ToInt32(hfClaseProductoID.Value));
    }

    protected void grvClaseProductoTipoProducto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ClaseProductiID = 0;
        if (LstClaseProductoXtipoProducto == null)
        {
            LstClaseProductoXtipoProducto = new List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto>();
        }


        int TipoProductoID = Convert.ToInt32(((GridView)sender).DataKeys[e.RowIndex].Values["TIPO_PRODUCTO_ID"].ToString());
        Label lblNomLblTipoProducto = (Label)((GridView)sender).Rows[e.RowIndex].FindControl("lblNomLblTipoProducto");
        var remover = LstClaseProductoXtipoProducto.Single(x => x.TipoProductoID == TipoProductoID);
        LstClaseProductoXtipoProducto.Remove(remover);

        if (!this.CboClaseTipoProducto.SelectedValue.Contains(TipoProductoID.ToString()))
        {
            this.CboClaseTipoProducto.Items.Add(new ListItem(lblNomLblTipoProducto.Text, TipoProductoID.ToString()));
            this.CboClaseTipoProducto.DataBind();
            this.CboClaseTipoProducto.SelectedValue = string.Empty;
        }
        else
        {
            this.CboClaseTipoProducto.SelectedValue = TipoProductoID.ToString();
        }

        var lista = (from datos in LstClaseProductoXtipoProducto
                     select new { CLASE_PRODUCTO_ID = datos.ClaseProductoID, TIPO_PRODUCTO_ID = datos.TipoProductoID, TIPO_PRODUCTO = datos.StrTipoProducto, CLASE_PRODUCTO = datos.StrClaseProducto, SALVOCONDUCTO = datos.Salvoconducto, APROVECHAMIENTO = datos.Aprovechamiento });


        if ((LstClaseProductoXtipoProducto == null || LstClaseProductoXtipoProducto.Count == 0) && (hfClaseProductoID.Value == null || !int.TryParse(hfClaseProductoID.Value, out ClaseProductiID)))
        {
            if (ClaseProductiID == 0)
            {
                this.CboClaseRecursoClaseProducto.Enabled = true;
                this.TxtNombreClaseProducto.Enabled = true;
            }
        }

        grvClaseProductoTipoProducto.DataSource = lista.ToList();
        grvClaseProductoTipoProducto.DataBind();


    }

    protected void BtnGrabarClaseProducto_Click(object sender, EventArgs e)
    {
        int ClasePropductoID = 0;
        if (LstClaseProductoXtipoProducto == null || LstClaseProductoXtipoProducto.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Debe Insertar minimo un tipo de producto')</script>", false);
            return;
        }

        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        ParametrizacionSunlIdentity.ClaseProducto ObjClaseProducto = new ParametrizacionSunlIdentity.ClaseProducto();


        if (hfClaseProductoID != null && int.TryParse(hfClaseProductoID.Value, out ClasePropductoID))
        {
            ObjClaseProducto.ClaseProductoID = ClasePropductoID;
        }

        ObjClaseProducto.ClaseRecursoID = Convert.ToInt32(this.CboClaseRecursoClaseProducto.SelectedValue);
        ObjClaseProducto.StrClaseProducto = this.TxtNombreClaseProducto.Text;
        ObjClaseProducto.CodigoIdeam = txtCodigoIdeamClaseProducto.Text;
        ObjClaseProducto.Salvoconducto = ChkVerSunlClaseProducto.Checked;
        ObjClaseProducto.Aprovechamiento = ChkVerAprovClaseProducto.Checked;
        ClasePropductoID = ObjParametrizacionSunl.InsertarClaseProducto(ObjClaseProducto);

        foreach (var TipoProducto in LstClaseProductoXtipoProducto)
        {
            TipoProducto.ClaseProductoID = ClasePropductoID;
        }

        ObjParametrizacionSunl.InsertarClaseTipoProducto(LstClaseProductoXtipoProducto);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('La informacion se grabo correctamente')</script>", false);

        LimpiarFormularioClaseProducto();
    }

    protected void BtnCancelarClaseProducto_Click(object sender, EventArgs e)
    {
        LimpiarFormularioClaseProducto();
    }
    #endregion

    #region Panel Tipo de Producto


    protected void BtnGrabarTipoProducto_Click(object sender, EventArgs e)
    {
        int TipoProductoID = 0;


        if (LstTipoProductoUnidadMedida == null || LstTipoProductoUnidadMedida.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Debe Insertar minimo un tipo de unidad de medida')</script>", false);
            return;
        }

        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        ParametrizacionSunlIdentity.TipoProducto ObjTipoProducto = new ParametrizacionSunlIdentity.TipoProducto();

        ObjTipoProducto.StrTipoProducto = this.txtTipoProducto.Text;
        ObjTipoProducto.CodigoIdeam = this.TxtCodigoIdeamTipoProducto.Text;
        if (hfTipoProductoID != null && int.TryParse(hfTipoProductoID.Value, out TipoProductoID))
        {
            if (TipoProductoID > 0)
            {
                ObjTipoProducto.TipoProductoID = TipoProductoID;
            }
        }

        ObjTipoProducto.Aprovechamiento = this.ChkVerTipoProductoAprovechamiento.Checked;
        ObjTipoProducto.Salvoconducto = this.ChkVerTipoProductoSUNL.Checked;

        TipoProductoID = ObjParametrizacionSunl.InsertarTipoProducto(ObjTipoProducto);

        foreach (var UnidadMedida in LstTipoProductoUnidadMedida)
        {
            UnidadMedida.TipoProductoID = TipoProductoID;
        }

        ObjParametrizacionSunl.InsertarTipoProductoUnidadMedida(LstTipoProductoUnidadMedida);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "script", "<script>alert('Clase producto guardada exitosamente.')</script>", false);

        LimpiarFormularioTipoProducto();

    }

    protected void BuscarTipoProducto()
    {
        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        var LstTipoproducto = ObjParametrizacionSunl.ListarTipoProducto(this.txtBuscarTipoproducto.Text);


        var lista = (from datos in LstTipoproducto
                     select new {TIPO_PRODUCTO = datos.StrTipoProducto, TIPO_PRODUCTO_ID = datos.TipoProductoID, FORMULA = datos.Formula, CODIGO_IDEAM = datos.CodigoIdeam, SALVOCONDUCTO = datos.Salvoconducto, APROVECHAMIENTO = datos.Aprovechamiento });
        this.GrvTipoClaseProducto.DataSource = lista.ToList();
        this.GrvTipoClaseProducto.DataBind();

    }

    protected void LimpiarFormularioTipoProducto()
    {
        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        this.txtTipoProducto.Text = string.Empty;
        this.txtTipoProducto.Enabled = true;
        this.hfTipoProductoID.Value = null;
        this.TxtCodigoIdeamTipoProducto.Text = string.Empty;
        this.ChkVerAprovClaseProducto.Checked = true;
        this.ChkVerAprovClaseProducto.Checked = true;
        LstTipoProductoUnidadMedida = null;
        Utilidades.LlenarComboLista(ObjParametrizacionSunl.ObtenerUnidadMedidaDisponible(0), CboUnidadMedida, "UNIDAD_MEDIDAD", "UNIDAD_MEDIDA_ID", true);
        GrvTipoProductoUnidadMedida.DataSource = null;
        GrvTipoProductoUnidadMedida.DataBind();
        LimpiarFomularioBusquedaTipoProducto();
    }

    protected void LimpiarFomularioBusquedaTipoProducto()
    {
        this.txtBuscarTipoproducto.Text = string.Empty;
        this.GrvTipoClaseProducto.DataSource = null;
        this.GrvTipoClaseProducto.DataBind();
    }

    protected void LnkBuscarTipoProducto_Click(object sender, EventArgs e)
    {
        LimpiarFomularioBusquedaTipoProducto();
        this.MpeTipoProducto.Show();
    }

    protected void BtnAdicionarUnidadMedida_Click(object sender, EventArgs e)
    {
        ParametrizacionSunlIdentity.TipoProductoUnidadMedida ObjTipoProductoUnidadMedida = new ParametrizacionSunlIdentity.TipoProductoUnidadMedida();
        int TipoproductoID = 0;
        if (LstTipoProductoUnidadMedida == null)
        {
            LstTipoProductoUnidadMedida = new List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida>();
        }

        if (this.hfTipoProductoID.Value != null && this.hfTipoProductoID.Value != string.Empty && int.TryParse(this.hfTipoProductoID.Value, out TipoproductoID))
        {
            ObjTipoProductoUnidadMedida.TipoProductoID = Convert.ToInt32(this.hfTipoProductoID.Value);
        }
        else
        {
            ObjTipoProductoUnidadMedida.TipoProductoID = 0;
        }

        var ValidacionUnidadMedida = LstTipoProductoUnidadMedida.Where(x => x.UnidadMedidaID == Convert.ToInt32(this.CboUnidadMedida.SelectedValue));

        if (ValidacionUnidadMedida != null && ValidacionUnidadMedida.Count() > 0)
        {
            foreach (var UnidadMedida in ValidacionUnidadMedida)
            {
                ObjTipoProductoUnidadMedida.UnidadMedidaID = UnidadMedida.UnidadMedidaID;
                ObjTipoProductoUnidadMedida.TipoProductoID = UnidadMedida.TipoProductoID;
            }
        }
        else
        {
            ObjTipoProductoUnidadMedida.StrTipoProducto = this.txtTipoProducto.Text;
            ObjTipoProductoUnidadMedida.UnidadMedidaID = Convert.ToInt32(this.CboUnidadMedida.SelectedValue);
            ObjTipoProductoUnidadMedida.StrUnidadMedida = this.CboUnidadMedida.SelectedItem.Text;
            LstTipoProductoUnidadMedida.Add(ObjTipoProductoUnidadMedida);
        }



        this.CboUnidadMedida.Items.Remove(this.CboUnidadMedida.SelectedItem);
        this.CboUnidadMedida.DataBind();

        var lista = (from datos in LstTipoProductoUnidadMedida
                     select new { TIPO_PRODUCTO_ID = datos.TipoProductoID, TIPO_PRODUCTO = datos.StrTipoProducto, UNIDAD_MEDIDA_ID = datos.UnidadMedidaID, UNIDAD_MEDIDA = datos.StrUnidadMedida });
        this.GrvTipoProductoUnidadMedida.DataSource = lista.ToList();
        this.GrvTipoProductoUnidadMedida.DataBind();

       this.txtTipoProducto.Enabled = false;

    }

    protected void GrvTipoProductoUnidadMedida_RowEditing(object sender, GridViewEditEventArgs e)
    {
        


    }

    protected void GrvTipoProductoUnidadMedida_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void GrvTipoProductoUnidadMedida_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int TipoProductiID = 0;
        if (LstTipoProductoUnidadMedida == null)
        {
            LstTipoProductoUnidadMedida = new List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida>();
        }


        int UnidadMedidaID = Convert.ToInt32(((GridView)sender).DataKeys[e.RowIndex].Values["UNIDAD_MEDIDA_ID"].ToString());

        Label LblUnidadMedida = (Label)((GridView)sender).Rows[e.RowIndex].FindControl("LblUnidadMedida");
        var remover = LstTipoProductoUnidadMedida.Single(x => x.UnidadMedidaID == UnidadMedidaID);
        LstTipoProductoUnidadMedida.Remove(remover);

        if (!this.CboUnidadMedida.SelectedValue.Contains(UnidadMedidaID.ToString()))
        {
            this.CboUnidadMedida.Items.Add(new ListItem(LblUnidadMedida.Text, UnidadMedidaID.ToString()));
            this.CboUnidadMedida.DataBind();
            this.CboUnidadMedida.SelectedValue = string.Empty;
        }
        else
        {
            this.CboClaseTipoProducto.SelectedValue = UnidadMedidaID.ToString();
        }

        var lista = (from datos in LstTipoProductoUnidadMedida
                     select new { TIPO_PRODUCTO_ID = datos.TipoProductoID, TIPO_PRODUCTO = datos.StrTipoProducto, UNIDAD_MEDIDA_ID = datos.UnidadMedidaID, UNIDAD_MEDIDA = datos.StrUnidadMedida });



        if ((LstTipoProductoUnidadMedida == null || LstTipoProductoUnidadMedida.Count == 0) && (hfTipoProductoID.Value == null || !int.TryParse(hfTipoProductoID.Value, out TipoProductiID)))
        {
            if (TipoProductiID == 0)
            {
                txtTipoProducto.Enabled = true;
            }
        }



        this.GrvTipoProductoUnidadMedida.DataSource = lista.ToList();
        this.GrvTipoProductoUnidadMedida.DataBind();
    }

    protected void GrvTipoClaseProducto_RowEditing(object sender, GridViewEditEventArgs e)
    {

        int TipoProductoID = Convert.ToInt32(((GridView)sender).DataKeys[e.NewEditIndex].Values["TIPO_PRODUCTO_ID"].ToString());
        CheckBox GrvChkSalvoconductoTipoProducto = (CheckBox)((GridView)sender).Rows[e.NewEditIndex].FindControl("GrvChkSalvoconductoTipoProducto");
        CheckBox GrvChkAprovechamientosTipoProducto = (CheckBox)((GridView)sender).Rows[e.NewEditIndex].FindControl("GrvChkAprovechamientosTipoProducto");
        Label lblCodigoIdeamTipoProducto = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblCodigoIdeamTipoProducto");
        Label lblFormula = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblFormula");
        Label lblNomLblTipoProducto = (Label)((GridView)sender).Rows[e.NewEditIndex].FindControl("lblNomLblTipoProducto");
        this.ChkVerTipoProductoSUNL.Checked = Convert.ToBoolean(GrvChkSalvoconductoTipoProducto.Checked);
        this.ChkVerTipoProductoAprovechamiento.Checked = Convert.ToBoolean(GrvChkAprovechamientosTipoProducto.Checked);
        this.txtTipoProducto.Text = lblNomLblTipoProducto.Text;
        this.txtTipoProducto.Enabled = false;
        this.TxtCodigoIdeamTipoProducto.Text = lblCodigoIdeamTipoProducto.Text;
        this.hfTipoProductoID.Value = TipoProductoID.ToString();
        this.MpeTipoProducto.Hide();
        BuscarTipoProductoUnidadMedida(TipoProductoID);
        
    }

    protected void GrvTipoClaseProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }


    protected void BtnBuscarTipoProducto_Click(object sender, EventArgs e)
    {
        BuscarTipoProducto();
    }

    protected void BuscarTipoProductoUnidadMedida(int pIntTipoproductoID)
    {
        if (LstTipoProductoUnidadMedida == null)
        {
            LstTipoProductoUnidadMedida = new List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida>();
        }

        ParametrizacionSunl ObjParametrizacionSunl = new ParametrizacionSunl();
        LstTipoProductoUnidadMedida = ObjParametrizacionSunl.ListarTipoProductoUnidadMedida(pIntTipoproductoID, 0);
        Utilidades.LlenarComboLista(ObjParametrizacionSunl.ObtenerUnidadMedidaDisponible(pIntTipoproductoID), CboUnidadMedida, "UNIDAD_MEDIDAD", "UNIDAD_MEDIDA_ID", true);

        var lista = (from datos in LstTipoProductoUnidadMedida
                     select new { TIPO_PRODUCTO_ID = datos.TipoProductoID, UNIDAD_MEDIDA_ID = datos.UnidadMedidaID, TIPO_PRODUCTO = datos.StrTipoProducto, UNIDAD_MEDIDA = datos.StrUnidadMedida });
        this.GrvTipoProductoUnidadMedida.DataSource = lista.ToList();
        this.GrvTipoProductoUnidadMedida.DataBind();
    }



    protected void BtnCancelarTipoproducto_Click(object sender, EventArgs e)
    {
        LimpiarFormularioTipoProducto();
    }
    #endregion

}







