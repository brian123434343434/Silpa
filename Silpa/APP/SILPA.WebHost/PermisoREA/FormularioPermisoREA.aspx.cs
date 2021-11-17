using SILPA.AccesoDatos.EvaluacionREA.Entity;
using SILPA.LogicaNegocio.EvaluacionREA;
using SILPA.LogicaNegocio.Generico;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SILPA.LogicaNegocio.Excepciones;
using SoftManagement.Log;
using System.Configuration;
using System.IO;

public partial class PermisoREA_FormularioPermisoREA : System.Web.UI.Page
{
    #region propiedades
    private int SolicitanteID
    {
        get
        {
            return (int)ViewState["_intSolicitanteID_PermisoREA_FormularioPermisoREA"];
        }
        set
        {
            ViewState["_intSolicitanteID_PermisoREA_FormularioPermisoREA"] = value;
        }
    }
    public List<InsumoRecoleccionEntity> _objLstInsumoRecoleccion { get { return (List<InsumoRecoleccionEntity>)ViewState["LstInsumoRecoleccion"]; } set { ViewState["LstInsumoRecoleccion"] = value; } }
    public List<InsumoPreservacionMovilizacionEntity> _objLstInsumoPreservacionMovilizacion { get { return (List<InsumoPreservacionMovilizacionEntity>)ViewState["LstInsumoPreservacionMovilizacion"]; } set { ViewState["LstInsumoPreservacionMovilizacion"] = value; } }
    public List<InsumoProfesionalEntity> _objLstInsumoProfesional { get { return (List<InsumoProfesionalEntity>)ViewState["LstInsumoProfesional"]; } set { ViewState["LstInsumoProfesional"] = value; } }
    public List<InsumosGrupoBiologicoEntity> _objLstInsumosGrupoBiologico { get { return (List<InsumosGrupoBiologicoEntity>)ViewState["LstInsumosGrupoBiologico"]; } set { ViewState["LstInsumosGrupoBiologico"] = value; } }
    public GrupoBiologicoEntity GrupoBiologicoEdit { get { return (GrupoBiologicoEntity)ViewState["GrupoBiologicoEdit"]; } set { ViewState["GrupoBiologicoEdit"] = value; } }
    public List<InsumoCoberturaEntity> _objLstCobertura { get { return (List<InsumoCoberturaEntity>)ViewState["LstCobertura"]; } set { ViewState["LstCobertura"] = value; } }
    private string NumeroVital
    {
        get
        {
            return (Session["SolicitudREA_NumeroVital"] != null ? Session["SolicitudREA_NumeroVital"].ToString() : "");
        }
        set
        {
            Session["SolicitudREA_NumeroVital"] = value;
        }
    }
    #endregion propiedades

    #region EventosPagina
    protected void cmdCancelarCargaDatos_Click(object sender, EventArgs e)
    {
        GrupoBiologicoEdit = null;
        LimpiarPopupAgregar();
        this.mpeAgregarGrupoBiologico.Hide();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            mpeMensajeAceptacion.Show();
            //this.SolicitanteID = 429;
            //iniciarPagina();

            if (ValidacionToken() == false)
            {
                Response.Redirect(@"..\Utilitario\MensajeValidacion.aspx");
            }
            else
            {
                iniciarPagina();
            }
            
        }
    }
    protected void lnkAgregar_Click(object sender, EventArgs e)
    {
        CargarGrupoNew();
        this.mpeAgregarGrupoBiologico.Show();
        
    }
    protected void cboTecnicaMuestreoNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTecnicaMuestreoNew.SelectedValue != string.Empty)
        {
            Caracteristica objCaracteristica = new Caracteristica();
            UnidadMuestreo objUnidadMuestreo = new UnidadMuestreo();
            EsfuerzoMuestreo objEsfuerzoMuestreo = new EsfuerzoMuestreo();
            Recoleccion objRecoleccion = new Recoleccion();
            Utilidades.LlenarComboLista(objCaracteristica.ListaCaracteristicasXGrupoBiologicoXTecnicamuestreo(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboTecnicaMuestreoNew.SelectedValue), null), cboCaracteristica1New, "Caractaristica", "CaracteristicaID", true);
            Utilidades.LlenarComboLista(objUnidadMuestreo.ListaUnidadMuestreoXGrupoBiologicoXTecnicaMuestreo(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboTecnicaMuestreoNew.SelectedValue)), this.cboUnidadMuestreoNew, "UnidadMuestreo", "UnidadMuestreoID", true);
            Utilidades.LlenarComboLista(objEsfuerzoMuestreo.ListaEsfuerzoMuestreoXGrupoBiologicoXTecnicamuestreo(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboTecnicaMuestreoNew.SelectedValue), null), this.cboEsfuerzo1New, "EsfuerzoMuestreo", "EsfuerzoMuestreoID", true);
            Utilidades.LlenarComboLista(objRecoleccion.ListaRecoleccionXGrupoBiologicoXTecnicaMuestreo(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboTecnicaMuestreoNew.SelectedValue)), this.cboTipoRecoleccionNew, "Recoleccion", "RecoleccionID", true);

        }
        else
        {
            Utilidades.LlenarComboVacio(cboCaracteristica1New);
        }
        cboCaracteristica1New_SelectedIndexChanged(null, null);
    }
    protected void ConsultaCaracteristicaSiguiente(DropDownList comboCaracteristicaPadre, DropDownList comboCaracteristicaHijo, RequiredFieldValidator rfvCaracteristicaHijo)
    {
        Caracteristica objCaracteristica = new Caracteristica();
        List<CaracteristicaEntity> lstCaracteristicaHijo = new List<CaracteristicaEntity>();
        if (comboCaracteristicaPadre.SelectedValue != string.Empty)
        {
            lstCaracteristicaHijo = objCaracteristica.ListaCaracteristicasXGrupoBiologicoXTecnicamuestreo(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboTecnicaMuestreoNew.SelectedValue), Convert.ToInt32(comboCaracteristicaPadre.SelectedValue));
            if (lstCaracteristicaHijo != null && lstCaracteristicaHijo.Count > 0)
            {
                Utilidades.LlenarComboLista(lstCaracteristicaHijo, comboCaracteristicaHijo, "Caractaristica", "CaracteristicaID", true);
                comboCaracteristicaHijo.Visible = true;
                rfvCaracteristicaHijo.Enabled = true;
            }
            else
            {
                comboCaracteristicaHijo.Items.Clear();
                comboCaracteristicaHijo.Visible = false;
                rfvCaracteristicaHijo.Enabled = false;
            }
        }
        else
        {
            comboCaracteristicaHijo.Items.Clear();
            comboCaracteristicaHijo.Visible = false;
            rfvCaracteristicaHijo.Enabled = false;
        }
    }
    protected void ConsultarEsfuerzoMuestreoSiguiente(DropDownList comboEsfuerzoMuestreoPadre, DropDownList comboEsfuerzoMuestreoHijo, RequiredFieldValidator rfvcomboEsfuerzoMuestreoHijo)
    {
        EsfuerzoMuestreo objEsfuerzoMuestreo = new EsfuerzoMuestreo();
        List<EsfuerzoMuestreoEntity> lstEsfuerzoMuestreoHijo = new List<EsfuerzoMuestreoEntity>();
        if (comboEsfuerzoMuestreoPadre.SelectedValue != string.Empty)
        {
            lstEsfuerzoMuestreoHijo = objEsfuerzoMuestreo.ListaEsfuerzoMuestreoXGrupoBiologicoXTecnicamuestreo(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboTecnicaMuestreoNew.SelectedValue), Convert.ToInt32(comboEsfuerzoMuestreoPadre.SelectedValue));
            if (lstEsfuerzoMuestreoHijo != null && lstEsfuerzoMuestreoHijo.Count > 0)
            {
                Utilidades.LlenarComboLista(lstEsfuerzoMuestreoHijo, comboEsfuerzoMuestreoHijo, "EsfuerzoMuestreo", "EsfuerzoMuestreoID", true);
                comboEsfuerzoMuestreoHijo.Visible = true;
                rfvcomboEsfuerzoMuestreoHijo.Enabled = true;
            }
            else
            {
                comboEsfuerzoMuestreoHijo.Items.Clear();
                comboEsfuerzoMuestreoHijo.Visible = false;
                rfvcomboEsfuerzoMuestreoHijo.Enabled = false;
            }
        }
        else
        {
            comboEsfuerzoMuestreoHijo.Items.Clear();
            comboEsfuerzoMuestreoHijo.Visible = false;
            rfvcomboEsfuerzoMuestreoHijo.Enabled = false;
        }
    }
    protected void cboCaracteristica1New_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultaCaracteristicaSiguiente(cboCaracteristica1New, cboCaracteristica2New, rfvcboCaracteristica2New);
        cboCaracteristica2New_SelectedIndexChanged(null, null);
    }
    protected void cboCaracteristica2New_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultaCaracteristicaSiguiente(cboCaracteristica2New, cboCaracteristica3New, rfvcboCaracteristica3New);
        cboCaracteristica3New_SelectedIndexChanged(null, null);
    }
    protected void cboCaracteristica3New_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultaCaracteristicaSiguiente(cboCaracteristica3New, cboCaracteristica4New, rfvcboCaracteristica4New);
        cboCaracteristica4New_SelectedIndexChanged(null, null);
    }
    protected void cboCaracteristica4New_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultaCaracteristicaSiguiente(cboCaracteristica4New, cboCaracteristica5New, rfvcboCaracteristica5New);
        cboCaracteristica5New_SelectedIndexChanged(null, null);
    }
    protected void cboCaracteristica5New_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultaCaracteristicaSiguiente(cboCaracteristica5New, cboCaracteristica6New, rfvcboCaracteristica6New);
    }
    protected void cboEsfuerzo1New_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultarEsfuerzoMuestreoSiguiente(cboEsfuerzo1New, cboEsfuerzo2New, rfvcboEsfuerzo2New);
        cboEsfuerzo2New_SelectedIndexChanged(null, null);
    }
    protected void cboEsfuerzo2New_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultarEsfuerzoMuestreoSiguiente(cboEsfuerzo2New, cboEsfuerzo3New, rfvcboEsfuerzo3New);
        cboEsfuerzo3New_SelectedIndexChanged(null, null);
    }
    protected void cboEsfuerzo3New_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultarEsfuerzoMuestreoSiguiente(cboEsfuerzo3New, cboEsfuerzo4New, rfvcboEsfuerzo4New);
    }
    protected void cboTipoSacrificioNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoSacrificioNew.SelectedValue != string.Empty)
        {
            FijacionPreservacion objFijacionPreservacion = new FijacionPreservacion();
            Utilidades.LlenarComboLista(objFijacionPreservacion.ListaTipoFijacionPreservacionXGrupoBiologicoXTipoSacrificio(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboTipoSacrificioNew.SelectedValue)), this.cboTipoFijacionNew, "Nomenclatura", "TipoFijacionPreservacionID", true);
        }
        else
        {
            Utilidades.LlenarComboVacio(cboTipoFijacionNew);
        }
        cboTipoFijacionNew_SelectedIndexChanged(null, null);
    }
    protected void cboTipoFijacionNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboTipoFijacionNew.SelectedValue != string.Empty)
        {
            TipoMovilizacion objTipoMovilizacion = new TipoMovilizacion();
            Utilidades.LlenarComboLista(objTipoMovilizacion.ListaTipoMovilizacionXGrupoBiologicoXTipoSacrificioXFijaPreserv(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboTipoSacrificioNew.SelectedValue), Convert.ToInt32(this.cboTipoFijacionNew.SelectedValue)), cboTipoMovilizacionNew, "Nomenclatura", "TipoMovilizacionID", true);
        }
        else
        {
            Utilidades.LlenarComboVacio(cboTipoMovilizacionNew);
        }
    }
    protected void cboFormacionAcademicaNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboFormacionAcademicaNew.SelectedValue != string.Empty)
        {
            ExperienciaEspecifica objExperienciaEspecifica = new ExperienciaEspecifica();
            Utilidades.LlenarComboLista(objExperienciaEspecifica.ListaExperienciaEspecificaXGrupoBiologicoXFormacionAcademica(Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), Convert.ToInt32(this.cboFormacionAcademicaNew.SelectedValue)), this.cboExperienciaEspecificaNew, "ExperienciaEspecifica", "ExperienciaEspecificaID", true);
        }
        else
        {
            Utilidades.LlenarComboVacio(this.cboExperienciaEspecificaNew);
        }
    }
    protected void btnAgrearInsumoRecoleccion_Click(object sender, EventArgs e)
    {
        

        if (this._objLstInsumoRecoleccion == null)
            this._objLstInsumoRecoleccion = new List<InsumoRecoleccionEntity>();
        int intGrupoBiologico = Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue);
        string strGrupoBiologico = GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologico : this.cboGrupoBiologicoNew.SelectedItem.Text;
        // instanciamos el objeto insumo
        InsumoRecoleccionEntity _objInsumoRecoleccionNew = new InsumoRecoleccionEntity();
        _objInsumoRecoleccionNew.objGrupoBiologico = new GrupoBiologicoEntity { GrupoBiologicoID = intGrupoBiologico, GrupoBiologico = strGrupoBiologico };
        _objInsumoRecoleccionNew.objTecnicaMuestreoEntity = new TecnicaMuestreoEntity { TecnicaMuestreoID = Convert.ToInt32(this.cboTecnicaMuestreoNew.SelectedValue), TecnicaMuestreo = this.cboTecnicaMuestreoNew.SelectedItem.Text };
        _objInsumoRecoleccionNew.objUnidadMuestreoEntity = new UnidadMuestreoEntity { UnidadMuestreoID = Convert.ToInt32(this.cboUnidadMuestreoNew.SelectedValue), UnidadMuestreo = this.cboUnidadMuestreoNew.SelectedItem.Text };
        _objInsumoRecoleccionNew.objRecoleccionEntity = new RecoleccionEntity { RecoleccionID = Convert.ToInt32(this.cboTipoRecoleccionNew.SelectedValue), Recoleccion = this.cboTipoRecoleccionNew.SelectedItem.Text };
        // instanciamos la lista de las caracteristicas
        List<CaracteristicaEntity> lstCaracteristicas = new List<CaracteristicaEntity>();
        lstCaracteristicas.Add(new CaracteristicaEntity { CaracteristicaID = Convert.ToInt32(this.cboCaracteristica1New.SelectedValue), Caractaristica = this.cboCaracteristica1New.SelectedItem.Text });
        if (this.cboCaracteristica2New.Visible)
            lstCaracteristicas.Add(new CaracteristicaEntity { CaracteristicaID = Convert.ToInt32(this.cboCaracteristica2New.SelectedValue), Caractaristica = this.cboCaracteristica2New.SelectedItem.Text });
        if (this.cboCaracteristica3New.Visible)
            lstCaracteristicas.Add(new CaracteristicaEntity { CaracteristicaID = Convert.ToInt32(this.cboCaracteristica3New.SelectedValue), Caractaristica = this.cboCaracteristica3New.SelectedItem.Text });
        if (this.cboCaracteristica4New.Visible)
            lstCaracteristicas.Add(new CaracteristicaEntity { CaracteristicaID = Convert.ToInt32(this.cboCaracteristica4New.SelectedValue), Caractaristica = this.cboCaracteristica4New.SelectedItem.Text });
        if (this.cboCaracteristica5New.Visible)
            lstCaracteristicas.Add(new CaracteristicaEntity { CaracteristicaID = Convert.ToInt32(this.cboCaracteristica5New.SelectedValue), Caractaristica = this.cboCaracteristica5New.SelectedItem.Text });
        if (this.cboCaracteristica6New.Visible)
            lstCaracteristicas.Add(new CaracteristicaEntity { CaracteristicaID = Convert.ToInt32(this.cboCaracteristica6New.SelectedValue), Caractaristica = this.cboCaracteristica6New.SelectedItem.Text });
        // agregamos la lista de caracteristicas
        _objInsumoRecoleccionNew.ObjLstCaracteristicaEntity = lstCaracteristicas;
        // instanciamos la lista de esfuerzo
        List<EsfuerzoMuestreoEntity> lstEsfuerzoMustreo = new List<EsfuerzoMuestreoEntity>();
        lstEsfuerzoMustreo.Add(new EsfuerzoMuestreoEntity { EsfuerzoMuestreoID = Convert.ToInt32(this.cboEsfuerzo1New.SelectedValue), EsfuerzoMuestreo = this.cboEsfuerzo1New.SelectedItem.Text });
        if(this.cboEsfuerzo2New.Visible)
            lstEsfuerzoMustreo.Add(new EsfuerzoMuestreoEntity { EsfuerzoMuestreoID = Convert.ToInt32(this.cboEsfuerzo2New.SelectedValue), EsfuerzoMuestreo = this.cboEsfuerzo2New.SelectedItem.Text });
        if (this.cboEsfuerzo3New.Visible)
            lstEsfuerzoMustreo.Add(new EsfuerzoMuestreoEntity { EsfuerzoMuestreoID = Convert.ToInt32(this.cboEsfuerzo3New.SelectedValue), EsfuerzoMuestreo = this.cboEsfuerzo3New.SelectedItem.Text });
        if (this.cboEsfuerzo4New.Visible)
            lstEsfuerzoMustreo.Add(new EsfuerzoMuestreoEntity { EsfuerzoMuestreoID = Convert.ToInt32(this.cboEsfuerzo4New.SelectedValue), EsfuerzoMuestreo = this.cboEsfuerzo4New.SelectedItem.Text });
        // asignamos la lista de los esfuerzos al objeto isnumo recoleccion
        _objInsumoRecoleccionNew.objLstEsfuerzoMuestreoEntity = lstEsfuerzoMustreo;
        // agregamos el objeto isnumorecoleccion a la lista global

        if (!this._objLstInsumoRecoleccion.Any(x => x.UniqueKey() == _objInsumoRecoleccionNew.UniqueKey()))
        {
            this._objLstInsumoRecoleccion.Add(_objInsumoRecoleccionNew);    
        }
        this.grvInsumoRecoleccion.DataSource = this._objLstInsumoRecoleccion;
        this.grvInsumoRecoleccion.DataBind();
    }
    protected void btnAgregarInsmoPreservMov_Click(object sender, EventArgs e)
    {
        if (GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0)
        {
            if (_objLstInsumosGrupoBiologico != null && _objLstInsumosGrupoBiologico.Where(x => x.objGrupoBiologicoEntity.GrupoBiologicoID == GrupoBiologicoEdit.GrupoBiologicoID).Count() > 0)
            {
                this._objLstInsumoPreservacionMovilizacion = _objLstInsumosGrupoBiologico.Where(x => x.objGrupoBiologicoEntity.GrupoBiologicoID == GrupoBiologicoEdit.GrupoBiologicoID).First().objLstInsumoPreservacionMovilizacion;
            }
        }


        if (this._objLstInsumoPreservacionMovilizacion == null)
            _objLstInsumoPreservacionMovilizacion = new List<InsumoPreservacionMovilizacionEntity>();
        int intGrupoBiologico = Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue);
        string strGrupoBiologico = GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologico : this.cboGrupoBiologicoNew.SelectedItem.Text;
        // instanciamos un objeto nuevo para insumo preserv mov
        InsumoPreservacionMovilizacionEntity _objInsumoPreservacionMovilizacion = new InsumoPreservacionMovilizacionEntity();
        _objInsumoPreservacionMovilizacion.objGrupoBiologico = new GrupoBiologicoEntity { GrupoBiologicoID = intGrupoBiologico, GrupoBiologico = strGrupoBiologico };
        _objInsumoPreservacionMovilizacion.objTipoSacrificioEntity = new TipoSacrificioEntity { TipoSacrificioID = Convert.ToInt32(this.cboTipoSacrificioNew.SelectedValue), TipoSacrificio = this.cboTipoSacrificioNew.SelectedItem.Text };
        _objInsumoPreservacionMovilizacion.objTipoFijacionPreservacionEntity = new TipoFijacionPreservacionEntity { TipoFijacionPreservacionID = Convert.ToInt32(this.cboTipoFijacionNew.SelectedValue), Nomenclatura = this.cboTipoFijacionNew.SelectedItem.Text };
        _objInsumoPreservacionMovilizacion.objTipoMovilizacionEntity = new TipoMovilizacionEntity { TipoMovilizacionID = Convert.ToInt32(this.cboTipoMovilizacionNew.SelectedValue), Nomenclatura = this.cboTipoMovilizacionNew.SelectedItem.Text };
        // agregamos el objeto insumo preserva movilizacion a la lista global
        if (!_objLstInsumoPreservacionMovilizacion.Any(x => x.UniqueKey() == _objInsumoPreservacionMovilizacion.UniqueKey()))
        {
            _objLstInsumoPreservacionMovilizacion.Add(_objInsumoPreservacionMovilizacion);
        }
        this.grvInsumoPreservMovNew.DataSource = this._objLstInsumoPreservacionMovilizacion;
        this.grvInsumoPreservMovNew.DataBind();
    }
    protected void btnAgregarProfesional_Click(object sender, EventArgs e)
    {
        if (GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0)
        {
            if (_objLstInsumosGrupoBiologico != null && _objLstInsumosGrupoBiologico.Where(x => x.objGrupoBiologicoEntity.GrupoBiologicoID == GrupoBiologicoEdit.GrupoBiologicoID).Count() > 0)
            {
                this._objLstInsumoProfesional = _objLstInsumosGrupoBiologico.Where(x => x.objGrupoBiologicoEntity.GrupoBiologicoID == GrupoBiologicoEdit.GrupoBiologicoID).First().ObjLstInsumoProfesional;
            }
        }

        if (this._objLstInsumoProfesional == null)
            _objLstInsumoProfesional = new List<InsumoProfesionalEntity>();
        // asignamos los valores de la edicion o registro nuevo grupo biologico
        int intGrupoBiologico = Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue);
        string strGrupoBiologico = GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologico : this.cboGrupoBiologicoNew.SelectedItem.Text;

        InsumoProfesionalEntity _objInsumoProfesional = new InsumoProfesionalEntity();
        _objInsumoProfesional.objGrupoBiologico = new GrupoBiologicoEntity { GrupoBiologicoID = intGrupoBiologico, GrupoBiologico = strGrupoBiologico };
        _objInsumoProfesional.objFormacionAcademicaProfesionalEntity = new FormacionAcademicaProfesionalEntity { FormacionAcademicaProfesionalID = Convert.ToInt32(this.cboFormacionAcademicaNew.SelectedValue), FormacionAcademicaProfesional = this.cboFormacionAcademicaNew.SelectedItem.Text };
        _objInsumoProfesional.objTiempoExperienciaEntity = new TiempoExperienciaEntity { TiempoExperienciaID = Convert.ToInt32(this.cboExperienciaTiempoNew.SelectedValue), TiempoExperiencia = this.cboExperienciaTiempoNew.SelectedItem.Text };
        _objInsumoProfesional.objExperienciaEspecificaEntity = new ExperienciaEspecificaEntity { ExperienciaEspecificaID = Convert.ToInt32(this.cboExperienciaEspecificaNew.SelectedValue), ExperienciaEspecifica = this.cboExperienciaEspecificaNew.SelectedItem.Text };
        // agregamos el objeto insumo preserva movilizacion a la lista global
        if (!_objLstInsumoProfesional.Any(x => x.UniqueKey() == _objInsumoProfesional.UniqueKey()))
        {
            _objLstInsumoProfesional.Add(_objInsumoProfesional);
        }
        this.gvrInsumoProfesionalesNew.DataSource = this._objLstInsumoProfesional;
        this.gvrInsumoProfesionalesNew.DataBind();

    }
    protected void cmdCargarDatos_Click(object sender, EventArgs e)
    {
        string str_mensajeValidacion = string.Empty;
        // validmaos que exista almenos un registro por cada insumo del grupo biologico
        if (_objLstInsumoRecoleccion == null || _objLstInsumoRecoleccion.Count == 0)
        {
            str_mensajeValidacion += "Debe agregar al menos un registro para Insumo recolección </br>";
        }
        if(_objLstInsumoPreservacionMovilizacion == null || _objLstInsumoPreservacionMovilizacion.Count  == 0)
        {
            str_mensajeValidacion += "Debe agregar al menos un registro para Insumo preservación y movilización</br>";
        }
        if (_objLstInsumoProfesional == null || _objLstInsumoProfesional.Count == 0)
        {
            str_mensajeValidacion += "Debe agregar al menos un registro para Insumo profesionales</br>";
        }
        if (str_mensajeValidacion != string.Empty)
        {
            MostrarMensajeError(str_mensajeValidacion);
        }
        else
        {
            if (_objLstInsumosGrupoBiologico == null)
                _objLstInsumosGrupoBiologico = new List<InsumosGrupoBiologicoEntity>();

            if (_objLstInsumosGrupoBiologico.Where(x => x.objGrupoBiologicoEntity.GrupoBiologicoID == Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue)).Count() == 0)
            {
                InsumosGrupoBiologicoEntity _objInsumosGrupoBiologico = new InsumosGrupoBiologicoEntity();
                _objInsumosGrupoBiologico.objGrupoBiologicoEntity = new GrupoBiologicoEntity { GrupoBiologicoID = Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), GrupoBiologico = GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologico : this.cboGrupoBiologicoNew.SelectedItem.Text };
                _objInsumosGrupoBiologico.objLstInsumoRecoleccion = _objLstInsumoRecoleccion;
                _objInsumosGrupoBiologico.objLstInsumoPreservacionMovilizacion = _objLstInsumoPreservacionMovilizacion;
                _objInsumosGrupoBiologico.ObjLstInsumoProfesional = _objLstInsumoProfesional;
                _objLstInsumosGrupoBiologico.Add(_objInsumosGrupoBiologico);
                rptInsumos.DataSource = _objLstInsumosGrupoBiologico;
                rptInsumos.DataBind();
                CargarTriggersInsumo();
                this.mpeAgregarGrupoBiologico.Hide();
                this.upnlRegistrosInsumos.Update();
            }
            else if (GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0)
            {
                _objLstInsumosGrupoBiologico.Remove(_objLstInsumosGrupoBiologico.Where(x => x.objGrupoBiologicoEntity.GrupoBiologicoID == Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue)).First());
                InsumosGrupoBiologicoEntity _objInsumosGrupoBiologico = new InsumosGrupoBiologicoEntity();
                _objInsumosGrupoBiologico.objGrupoBiologicoEntity = new GrupoBiologicoEntity { GrupoBiologicoID = Convert.ToInt32(GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologicoID.ToString() : this.cboGrupoBiologicoNew.SelectedValue), GrupoBiologico = GrupoBiologicoEdit != null && GrupoBiologicoEdit.GrupoBiologicoID > 0 ? GrupoBiologicoEdit.GrupoBiologico : this.cboGrupoBiologicoNew.SelectedItem.Text };
                _objInsumosGrupoBiologico.objLstInsumoRecoleccion = _objLstInsumoRecoleccion;
                _objInsumosGrupoBiologico.objLstInsumoPreservacionMovilizacion = _objLstInsumoPreservacionMovilizacion;
                _objInsumosGrupoBiologico.ObjLstInsumoProfesional = _objLstInsumoProfesional;
                _objLstInsumosGrupoBiologico.Add(_objInsumosGrupoBiologico);
                rptInsumos.DataSource = _objLstInsumosGrupoBiologico;
                rptInsumos.DataBind();
                CargarTriggersInsumo();
                this.mpeAgregarGrupoBiologico.Hide();
                this.upnlRegistrosInsumos.Update();
                
            }
            else
            {
                MostrarMensajeError("Ya existe información para este grupo biológico, para editar la información de click en el botón cancelar y dirijase a la sección correspondiente del grupo biológico");
            }
        }
    }

    private void CargarTriggersInsumo()
    {
        LinkButton objLinkButton = null;

        foreach (RepeaterItem objItem in this.rptInsumos.Items)
        {
            //Cargar imagen de descarga
            objLinkButton = (LinkButton)objItem.FindControl("lnkEditarGrupo");
            //Si existe el control y se encuentra visible
            if (objLinkButton != null && objLinkButton.Visible)
            {
                //this.upnlRegistrosInsumos.RegisterAsyncPostBackControl(objLinkButton);
                /*ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(objLinkButton);*/
                this.upnlRegistrosInsumos.Triggers.Add(new AsyncPostBackTrigger { ControlID = objLinkButton.UniqueID, EventName = "Click" });
            }
        }
    }
    protected void grvInsumoRecoleccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strKey = e.Keys[0].ToString();
        _objLstInsumoRecoleccion.Remove(_objLstInsumoRecoleccion.Where(x => x.Key == strKey).First());
        this.grvInsumoRecoleccion.DataSource = this._objLstInsumoRecoleccion;
        this.grvInsumoRecoleccion.DataBind();
        this.upnlAgregarGrupoBiologico.Update();
    }
    protected void grvInsumoPreservMovNew_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strKey = e.Keys[0].ToString();
        _objLstInsumoPreservacionMovilizacion.Remove(_objLstInsumoPreservacionMovilizacion.Where(x => x.Key == strKey).First());
        this.grvInsumoPreservMovNew.DataSource = this._objLstInsumoPreservacionMovilizacion;
        this.grvInsumoPreservMovNew.DataBind();
        this.upnlAgregarGrupoBiologico.Update();
    }
    protected void gvrInsumoProfesionalesNew_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strKey = e.Keys[0].ToString();
        _objLstInsumoProfesional.Remove(_objLstInsumoProfesional.Where(x => x.Key == strKey).First());
        this.gvrInsumoProfesionalesNew.DataSource = this._objLstInsumoProfesional;
        this.gvrInsumoProfesionalesNew.DataBind();
        this.upnlAgregarGrupoBiologico.Update();
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
            if (this.cboDepartamento.SelectedValue == "-1")
            {
                rfvcboMunicipio.Enabled = false;
            }
            else
            {
                rfvcboMunicipio.Enabled = true;
            }
            Configuracion _configuracion = new SILPA.Comun.Configuracion();
            Listas litMunicipio = new SILPA.LogicaNegocio.Generico.Listas();
            DataTable dtMunicipios = litMunicipio.ListaMunicipios(null, int.Parse(cboDepartamento.SelectedValue), null).Tables[0];
            dtMunicipios = dtMunicipios.AsEnumerable().Where(x => !x.Field<string>("MUN_NOMBRE").Contains("Parque")).CopyToDataTable();
            if (dtMunicipios.AsEnumerable().Where(x => x.Field<int>("AUT_ID") == Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue)).Count() > 0)
                dtMunicipios = dtMunicipios.AsEnumerable().Where(x => x.Field<int>("AUT_ID") == Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue)).CopyToDataTable();
            if (cboDepartamento.SelectedValue == "13")
            {
                if (dtMunicipios.AsEnumerable().Where(x => x.Field<int>("AUT_ID") == Convert.ToInt32("195")).Count() > 0)
                    dtMunicipios = dtMunicipios.AsEnumerable().Where(x => x.Field<int>("AUT_ID") == Convert.ToInt32("195")).CopyToDataTable();
            }
            Utilidades.LlenarComboTabla(dtMunicipios, cboMunicipio, "MUN_NOMBRE", "MUN_ID", true);
        }
    }
    protected void grvCobertura_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strKey = e.Keys[0].ToString();
        _objLstCobertura.Remove(_objLstCobertura.Where(x => x.Key == strKey).First());
        this.grvCobertura.DataSource = this._objLstCobertura;
        this.grvCobertura.DataBind();
    }
    protected void btnAgregarCobertura_Click(object sender, EventArgs e)
    {
        if (this.cboDepartamento.SelectedValue != string.Empty && this.cboMunicipio.SelectedValue != string.Empty)
        {
            if (this._objLstCobertura == null)
                this._objLstCobertura = new List<InsumoCoberturaEntity>();
            InsumoCoberturaEntity _objCobertura = new InsumoCoberturaEntity();
            _objCobertura.DepartamentoID = Convert.ToInt32(this.cboDepartamento.SelectedValue);
            _objCobertura.Departamento = this.cboDepartamento.SelectedItem.Text;
            _objCobertura.MunicipioID = Convert.ToInt32(this.cboMunicipio.SelectedValue);
            _objCobertura.Municipio = this.cboMunicipio.SelectedItem.Text;
            if (!_objLstCobertura.Any(x => x.UniqueKey() == _objCobertura.UniqueKey()))
            {
                _objLstCobertura.Add(_objCobertura);
            }
            this.grvCobertura.DataSource = this._objLstCobertura;
            this.grvCobertura.DataBind();
        }
        else if(this.cboDepartamento.SelectedValue == "-1")
        {
            if (this._objLstCobertura == null)
                this._objLstCobertura = new List<InsumoCoberturaEntity>();

            foreach (ListItem item in this.cboDepartamento.Items)
            {
                if (item.Value != "-1" && item.Value != "")
                {
                    InsumoCoberturaEntity _objCobertura = new InsumoCoberturaEntity();
                    _objCobertura.DepartamentoID = Convert.ToInt32(item.Value);
                    _objCobertura.Departamento = item.Text;
                    _objCobertura.MunicipioID = -1;
                    _objCobertura.Municipio = "TODOS";
                    if (!_objLstCobertura.Any(x => x.UniqueKey() == _objCobertura.UniqueKey()))
                    {
                        _objLstCobertura.Add(_objCobertura);
                    }
                }
            }
            this.grvCobertura.DataSource = this._objLstCobertura;
            this.grvCobertura.DataBind();
        }
    }
    protected void cmdEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            //Verificar que no existan errores en la pagina
            if (Page.IsValid)
            {
                //Guardar la información de la solicitud
                this.GuardarInformacionSolicitud();

                //Direccionar a pagina de respuesta
                Response.Redirect("RespuestaPermisoREA.aspx", false);
            }
        }
        catch (RadicacionSolicitudREAException)
        {
            //MOstrar mensaje de error en pantalla
            this.MostrarMensajeError("Se presento error registrando la solicitud en VITAL. La información diligenciada <b>NO</b> fue almacenada en nuestro sistema. Por favor trate nuevamente y si el error persiste comuniquese con el administrador del sistema.");

            //Limpiar formulario
            //this.LimpiarCamposFormulario();

            //Actualizar panel
            this.upnlFormulario.Update();
        }
        catch (DocumentoSolicitudREAException)
        {
            //MOstrar mensaje de error en pantalla
            this.MostrarMensajeError("Se presento durante el proceso de almacenamiento de archivos en VITAL. Por favor comuniquese con el administrador del sistema para conocer el estado de su solicitud.");

            //Limpiar formulario
            //this.LimpiarCamposFormulario();

            //Actualizar panel
            this.upnlFormulario.Update();
        }
        catch (Exception exc)
        {
            //MOstrar mensaje de error en pantalla
            this.MostrarMensajeError("Se presento error enviando la información de la solicitud. La información diligenciada <b>NO</b> fue almacenada en nuestro sistema. Por favor trate nuevamente y si el error persiste comuniquese con el administrador del sistema. <br />" + exc.Message);

            //Escribir error
            SMLog.Escribir(Severidad.Critico, "PermisoREA_FormularioPermisoREA :: cmdEnviar_Click -> Error Inesperado: " + exc.Message + " - " + exc.StackTrace.ToString());

            //Limpiar formulario
            //this.LimpiarCamposFormulario();

            //Actualizar panel
            this.upnlFormulario.Update();
        }
    }
    protected void lnkEditarGrupo_Click(object sender, EventArgs e)
    {
        string str_grupoBiologico = string.Empty;
        string strGrupoBiologicoEdit =((LinkButton)sender).CommandArgument;
        GrupoBiologicoEdit = new GrupoBiologicoEntity { GrupoBiologicoID = Convert.ToInt32(strGrupoBiologicoEdit.Split(';')[0]), GrupoBiologico = strGrupoBiologicoEdit.Split(';')[1] };
        CargarGrupoEditar(GrupoBiologicoEdit);

    }
    protected void cmdAceptarErrorProceso_Click(object sender, EventArgs e)
    {
        this.OcultarMensaje();
    }
    protected void cvgrvCobertura_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (_objLstCobertura == null || _objLstCobertura.Count == 0)
        {
            args.IsValid = false;
        }
    }
    protected void cboAutoridadAmbiental_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboAutoridadAmbiental.SelectedValue != "")
        {
            SILPA.LogicaNegocio.Generico.Listas _listaAutoridades = new SILPA.LogicaNegocio.Generico.Listas();
            SILPA.Comun.Configuracion _configuracion = new SILPA.Comun.Configuracion();
            if (this.cboAutoridadAmbiental.SelectedValue != "144")
            {
                
                this.cboDepartamento.Enabled = true;
                Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentosPorAutoridadAmbiental(_configuracion.IdPaisPredeterminado, Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue)).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
                // agregamos la opcion todos al combo
                cboDepartamento.Items.Insert(1, new ListItem { Text= "TODOS", Value="-1" });
            }
            else
            {
                Utilidades.LlenarComboTabla(_listaAutoridades.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
                this.cboDepartamento.Enabled = true;
            }
        }
        else
        {
            Utilidades.LlenarComboVacio(this.cboDepartamento);
            this.cboDepartamento.Enabled = false;
        }
    }

    #endregion EventosPagina

    #region Manejo Errores

    /// <summary>
    /// Mostrar el mensaje de error especificado
    /// </summary>
    /// <param name="p_strMensaje">string con el mensaje</param>
    /// <param name="p_blnMensajeSincronico">Indica si se maneja como modal sincronico. Opcional</param>
    private void MostrarMensajeError(string p_strMensaje)
    {
        this.mpeErrorProceso.Show();
        this.ltlErrorProceso.Text = p_strMensaje;
        this.upnlErrorProceso.Update();
    }


    /// <summary>
    /// Ocultar los mensajes
    /// </summary>
    private void OcultarMensaje()
    {
        this.mpeErrorProceso.Hide();
        this.ltlErrorProceso.Text = "";
        this.upnlErrorProceso.Update();
    }

    #endregion

    #region Metodos
    protected void rptInsumos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        InsumosGrupoBiologicoEntity objInsumosGrupoBiologico = null;
        //Verificar si es item con información
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //Obtener información de preguntas del sector
            objInsumosGrupoBiologico = (InsumosGrupoBiologicoEntity)e.Item.DataItem;

            if (objInsumosGrupoBiologico.objGrupoBiologicoEntity.GrupoBiologicoID != 0)
            {
                GridView grvInsumoRecoleccionGrupo = (GridView)e.Item.FindControl("grvInsumoRecoleccionGrupo");
                GridView grvInsumoPreservMovGrupo = (GridView)e.Item.FindControl("grvInsumoPreservMovGrupo");
                GridView gvrInsumoProfesionalesGrupo = (GridView)e.Item.FindControl("gvrInsumoProfesionalesGrupo");

                //LinkButton lnkEditarGrupo = (LinkButton)e.Item.FindControl("lnkEditarGrupo");

                //Cargar funcion ejecutar
                //lnkEditarGrupo.OnClientClick = "javascript:__doPostBack('" + lnkEditarGrupo.UniqueID + "','')";
                //AsyncPostBackTrigger objAsyncPostTrigger = null;
                //objAsyncPostTrigger = new AsyncPostBackTrigger();
                //objAsyncPostTrigger.ControlID = lnkEditarGrupo.UniqueID;
                //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(lnkEditarGrupo);


                grvInsumoRecoleccionGrupo.DataSource = objInsumosGrupoBiologico.objLstInsumoRecoleccion;
                grvInsumoRecoleccionGrupo.DataBind();

                grvInsumoPreservMovGrupo.DataSource = objInsumosGrupoBiologico.objLstInsumoPreservacionMovilizacion;
                grvInsumoPreservMovGrupo.DataBind();

                gvrInsumoProfesionalesGrupo.DataSource = objInsumosGrupoBiologico.ObjLstInsumoProfesional;
                gvrInsumoProfesionalesGrupo.DataBind();

                

            }
            else
            {
                
            }
        }
    }
    private void CargarGrupoNew()
    {
        if (this.cboGrupoBiologicoNew.SelectedValue != string.Empty)
        {
            TecnicaMuestreo objTecnicaMuestreo = new TecnicaMuestreo();
            TipoSacrificio objTipoSacrificio = new TipoSacrificio();
            FormacionAcademica objFormacionAcademica = new FormacionAcademica();
            TiempoExperiencia objTiempoExperiencia = new TiempoExperiencia();

            ltlTituloAdcionar.Text = "INSUMO RECOLECCION (" + this.cboGrupoBiologicoNew.SelectedItem.Text.ToUpper() + ")";
            GrupoBiologicoEdit = null;

            LimpiarPopupAgregar();

            Utilidades.LlenarComboLista(objTecnicaMuestreo.ListaTecnicaMuestreoPorGrupoBiologico(Convert.ToInt32(this.cboGrupoBiologicoNew.SelectedValue)), this.cboTecnicaMuestreoNew, "TecnicaMuestreo", "TecnicaMuestreoID", true);
            Utilidades.LlenarComboLista(objTipoSacrificio.ListaTipoSacrificioXGrupoBiologico(Convert.ToInt32(this.cboGrupoBiologicoNew.SelectedValue)), this.cboTipoSacrificioNew, "Nomenclatura", "TipoSacrificioID", true);
            Utilidades.LlenarComboLista(objFormacionAcademica.ListaFormacionAcademicaXGrupoBiologico(Convert.ToInt32(this.cboGrupoBiologicoNew.SelectedValue)), this.cboFormacionAcademicaNew, "FormacionAcademicaProfesional", "FormacionAcademicaProfesionalID", true);
            Utilidades.LlenarComboLista(objTiempoExperiencia.ListaTiempoExperiencia(), this.cboExperienciaTiempoNew, "TiempoExperiencia", "TiempoExperienciaID", true);

            

            cboTecnicaMuestreoNew_SelectedIndexChanged(null, null);
            cboTipoSacrificioNew_SelectedIndexChanged(null, null);
            cboFormacionAcademicaNew_SelectedIndexChanged(null, null);
        }
        else
        {
            Utilidades.LlenarComboVacio(cboTecnicaMuestreoNew);
            Utilidades.LlenarComboVacio(cboTipoSacrificioNew);
            Utilidades.LlenarComboVacio(cboFormacionAcademicaNew);
        }


        
        
    }
    private void CargarGrupoEditar(GrupoBiologicoEntity objGrupoBiologico)
    {
        InsumosGrupoBiologicoEntity objInsumosGrupoBiologico = _objLstInsumosGrupoBiologico.Where(x => x.objGrupoBiologicoEntity.GrupoBiologicoID == objGrupoBiologico.GrupoBiologicoID).FirstOrDefault();
        TecnicaMuestreo objTecnicaMuestreo = new TecnicaMuestreo();
        TipoSacrificio objTipoSacrificio = new TipoSacrificio();
        FormacionAcademica objFormacionAcademica = new FormacionAcademica();
        TiempoExperiencia objTiempoExperiencia = new TiempoExperiencia();

        ltlTituloAdcionar.Text = "INSUMO RECOLECCION (" + objInsumosGrupoBiologico.objGrupoBiologicoEntity.GrupoBiologico.ToUpper() + ")";
        GrupoBiologicoEdit = objInsumosGrupoBiologico.objGrupoBiologicoEntity;

        Utilidades.LlenarComboLista(objTecnicaMuestreo.ListaTecnicaMuestreoPorGrupoBiologico(objInsumosGrupoBiologico.objGrupoBiologicoEntity.GrupoBiologicoID), this.cboTecnicaMuestreoNew, "TecnicaMuestreo", "TecnicaMuestreoID", true);
        Utilidades.LlenarComboLista(objTipoSacrificio.ListaTipoSacrificioXGrupoBiologico(objInsumosGrupoBiologico.objGrupoBiologicoEntity.GrupoBiologicoID), this.cboTipoSacrificioNew, "Nomenclatura", "TipoSacrificioID", true);
        Utilidades.LlenarComboLista(objFormacionAcademica.ListaFormacionAcademicaXGrupoBiologico(objInsumosGrupoBiologico.objGrupoBiologicoEntity.GrupoBiologicoID), this.cboFormacionAcademicaNew, "FormacionAcademicaProfesional", "FormacionAcademicaProfesionalID", true);

        LimpiarPopupAgregar();

        _objLstInsumoRecoleccion = objInsumosGrupoBiologico.objLstInsumoRecoleccion;
        _objLstInsumoPreservacionMovilizacion = objInsumosGrupoBiologico.objLstInsumoPreservacionMovilizacion;
        _objLstInsumoProfesional = objInsumosGrupoBiologico.ObjLstInsumoProfesional;

        this.grvInsumoRecoleccion.DataSource = objInsumosGrupoBiologico.objLstInsumoRecoleccion;
        this.grvInsumoRecoleccion.DataBind();
        this.grvInsumoPreservMovNew.DataSource = objInsumosGrupoBiologico.objLstInsumoPreservacionMovilizacion;
        this.grvInsumoPreservMovNew.DataBind();
        this.gvrInsumoProfesionalesNew.DataSource = objInsumosGrupoBiologico.ObjLstInsumoProfesional;
        this.gvrInsumoProfesionalesNew.DataBind();
        this.mpeAgregarGrupoBiologico.Show();
        this.upnlAgregarGrupoBiologico.Update();
    }
    private void GuardarInformacionSolicitud()
    {
        SolicitudREAEntity objSolicitudREAEntity = null;
        string strMensajeError = "";

        //Cargar los datos de la solicitud
        objSolicitudREAEntity = this.CargarDatosSolicitud();

        //Cargar el listado 
        strMensajeError = this.ValidarDatosSolicitud(objSolicitudREAEntity);

        //Verificar que la información sea consistente
        if (string.IsNullOrWhiteSpace(strMensajeError))
        {
            //Almacenar la información de la solicitud
            this.AlmacenarInformacionSolicitud(ref objSolicitudREAEntity);

            //Cargar el tipo de respuesta y numero vital
            this.NumeroVital = objSolicitudREAEntity.NumeroVITAL;
        }
        else
        {
            throw new Exception(strMensajeError);
        }
    }
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
    private SolicitudREAEntity CargarDatosSolicitud()
    {
        SolicitudREAEntity objSolicitudREA = null;
        //Crear objeto formulario y cargar datos basicos
        objSolicitudREA = new SolicitudREAEntity
        {
            SolicitanteID = this.SolicitanteID,
            AutoridadAmbientalID = Convert.ToInt32(this.cboAutoridadAmbiental.SelectedValue),
            LstInsumosGrupoBilogocio = _objLstInsumosGrupoBiologico,
            LstIsnumosCobertura = _objLstCobertura,
            DuracionPermiso = Convert.ToInt32(this.txtDuracion.Text),
            LstDocumentos = this.CargarDocumentosSolicitud()
        };
        return objSolicitudREA;
    }

    private List<DocumentoSolicitudREAEntity> CargarDocumentosSolicitud()
    {
        List<DocumentoSolicitudREAEntity> objLstDocumentos = null;
        DocumentoSolicitudREAEntity objDocumento = null;
        string strCarpetaTemporal = "";
        string strNombreArchivo = "";

        //Crear el listado de documentos
        objLstDocumentos = new List<DocumentoSolicitudREAEntity>();

        //Cargar la carpeta temporal donde se almacenara los archivos
        strCarpetaTemporal = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + string.Format(@"{0}\{1}\", ConfigurationManager.AppSettings["CarpetaTemporalSolicitudREA"].ToString(), this.SolicitanteID.ToString());

        //Crear la carpeta temporal en caso de que no exista
        if (!Directory.Exists(strCarpetaTemporal))
            Directory.CreateDirectory(strCarpetaTemporal);

        //Cargar los archivos anexados
        if (this.fulDocumentoMetodologiaEstablecida.HasFile)
        {
            //Cargar el nombre de archivo que se asignara
            strNombreArchivo = string.Format("{0}_{1}_{2}{3}", "01", this.SolicitanteID.ToString(), DateTime.Now.ToString("yyyyMMddhhmmssmmm"), this.fulDocumentoMetodologiaEstablecida.FileName.Trim().Substring(this.fulDocumentoMetodologiaEstablecida.FileName.Trim().LastIndexOf(".")));

            //Guardar Archivo
            this.fulDocumentoMetodologiaEstablecida.SaveAs(strCarpetaTemporal + strNombreArchivo);

            //Cargar datos de archivo
            objDocumento = new DocumentoSolicitudREAEntity
            {
                TipoDocumentoID = 1,
                Ubicacion = strCarpetaTemporal,
                NombreDocumentoUsuario = this.fulDocumentoMetodologiaEstablecida.FileName,
                NombreDocumento = strNombreArchivo
            };

            //Adicionar al listado 
            objLstDocumentos.Add(objDocumento);
        }
        //Cargar los archivos anexados
        if (this.fulDocumentoPerfilProfesionales.HasFile)
        {
            //Cargar el nombre de archivo que se asignara
            strNombreArchivo = string.Format("{0}_{1}_{2}{3}", "02", this.SolicitanteID.ToString(), DateTime.Now.ToString("yyyyMMddhhmmssmmm"), this.fulDocumentoPerfilProfesionales.FileName.Trim().Substring(this.fulDocumentoPerfilProfesionales.FileName.Trim().LastIndexOf(".")));

            //Guardar Archivo
            this.fulDocumentoPerfilProfesionales.SaveAs(strCarpetaTemporal + strNombreArchivo);

            //Cargar datos de archivo
            objDocumento = new DocumentoSolicitudREAEntity
            {
                TipoDocumentoID = 2,
                Ubicacion = strCarpetaTemporal,
                NombreDocumentoUsuario = this.fulDocumentoPerfilProfesionales.FileName,
                NombreDocumento = strNombreArchivo
            };

            //Adicionar al listado 
            objLstDocumentos.Add(objDocumento);
        }
        
        //Cargar los archivos anexados
        if (this.fulDocumentoIdentificacion.HasFile)
        {
            //Cargar el nombre de archivo que se asignara
            strNombreArchivo = string.Format("{0}_{1}_{2}{3}", "03", this.SolicitanteID.ToString(), DateTime.Now.ToString("yyyyMMddhhmmssmmm"), this.fulDocumentoIdentificacion.FileName.Trim().Substring(this.fulDocumentoIdentificacion.FileName.Trim().LastIndexOf(".")));

            //Guardar Archivo
            this.fulDocumentoIdentificacion.SaveAs(strCarpetaTemporal + strNombreArchivo);

            //Cargar datos de archivo
            objDocumento = new DocumentoSolicitudREAEntity
            {
                TipoDocumentoID = 3,
                Ubicacion = strCarpetaTemporal,
                NombreDocumentoUsuario = this.fulDocumentoIdentificacion.FileName,
                NombreDocumento = strNombreArchivo
            };

            //Adicionar al listado 
            objLstDocumentos.Add(objDocumento);
        }
        
        //Cargar los archivos anexados
        if (this.fulDocumentoReciboConsignacion.HasFile)
        {
            //Cargar el nombre de archivo que se asignara
            strNombreArchivo = string.Format("{0}_{1}_{2}{3}", "04", this.SolicitanteID.ToString(), DateTime.Now.ToString("yyyyMMddhhmmssmmm"), this.fulDocumentoReciboConsignacion.FileName.Trim().Substring(this.fulDocumentoReciboConsignacion.FileName.Trim().LastIndexOf(".")));

            //Guardar Archivo
            this.fulDocumentoReciboConsignacion.SaveAs(strCarpetaTemporal + strNombreArchivo);

            //Cargar datos de archivo
            objDocumento = new DocumentoSolicitudREAEntity
            {
                TipoDocumentoID = 4,
                Ubicacion = strCarpetaTemporal,
                NombreDocumentoUsuario = this.fulDocumentoReciboConsignacion.FileName,
                NombreDocumento = strNombreArchivo
            };

            //Adicionar al listado 

            objLstDocumentos.Add(objDocumento);
        }

        return objLstDocumentos;
    }
    private string ValidarDatosSolicitud(SolicitudREAEntity objSolicitudREA)
    {
        string strMensajeError = "";

        // validamos que el objeto solicitu no sea null
        if (objSolicitudREA != null)
        {
            // validamos que seleccione la autoridad ambiental
            if (objSolicitudREA.AutoridadAmbientalID != null && objSolicitudREA.AutoridadAmbientalID > 0)
            {
                // validamos que alla al menos un registro por cobertura
                if (objSolicitudREA.LstIsnumosCobertura != null && objSolicitudREA.LstIsnumosCobertura.Count > 0)
                {
                    // validamos que si es la ANLA debe tener minimo dos departamentos en la lista de cobertura
                    if (this.cboAutoridadAmbiental.SelectedValue == "144" && objSolicitudREA.LstIsnumosCobertura.GroupBy(test => test.DepartamentoID).Select(grp => grp.First()).ToList().Count >= 2)
                    {
                        // validamos que halla informacion para almenos un grupo biologico
                        if (objSolicitudREA.LstInsumosGrupoBilogocio != null && objSolicitudREA.LstInsumosGrupoBilogocio.Count > 0)
                        {
                            //validamos los documentos de la solicitu
                            if (objSolicitudREA.LstDocumentos != null && objSolicitudREA.LstDocumentos.Count == 4)
                            {
                                strMensajeError = string.Empty;
                            }
                            else
                            {
                                strMensajeError = "Debe ingresar los documentos correspondientes a la solicitud.";
                            }
                        }
                        else
                        {
                            strMensajeError = "Debe ingresar al menos un grupo biologico con su informacion respectiva.";
                        }
                       
                    }
                    else
                    {
                        strMensajeError = "Debe ingresar minimo dos departamentos para la Autoridad Nacional de Licencias Ambinetales (ANLA).";

                        if (this.cboAutoridadAmbiental.SelectedValue != "144" && objSolicitudREA.LstIsnumosCobertura.GroupBy(test => test.DepartamentoID).Select(grp => grp.First()).ToList().Count == 1)
                        {// validamos que halla informacion para almenos un grupo biologico
                            if (objSolicitudREA.LstInsumosGrupoBilogocio != null && objSolicitudREA.LstInsumosGrupoBilogocio.Count > 0)
                            {
                                //validamos los documentos de la solicitu
                                if (objSolicitudREA.LstDocumentos != null && objSolicitudREA.LstDocumentos.Count == 4)
                                {
                                    strMensajeError = string.Empty;
                                }
                                else
                                {
                                    strMensajeError = "Debe ingresar los documentos correspondientes a la solicitud.";
                                }
                            }
                            else
                            {
                                strMensajeError = "Debe ingresar al menos un grupo biologico con su informacion respectiva.";
                            }
                        }
                        else if (this.cboAutoridadAmbiental.SelectedValue != "144")
                        {
                            strMensajeError = "Cuando la cobertura es de más de un departamento la Autoridad Ambiental competente es la Autoridad Nacional de Licencias Ambinetales (ANLA), por lo tanto debe seleccionarla en la lista Autoridad Ambiental";
                        }
                    }
                }
                else
                {
                    strMensajeError = "Debe ingresar al menos un registro para la cobertura de la solicitud.";
                }
            }
            else
            {
                strMensajeError = "Debe seleccionar una autoridad ambiental.";
            }

        }
        else
        {
            strMensajeError = "No se obtuvo información del formulario";
        }
        return strMensajeError;
    }
    private void AlmacenarInformacionSolicitud(ref SolicitudREAEntity objSolicitudREA)
    {

        SolicitudREA _objSolicitudREA = null;

        //Almacenra la información de cambio menor
        _objSolicitudREA = new SolicitudREA();
        _objSolicitudREA.InsertarSolicitudREA(ref objSolicitudREA);
    }
    protected void iniciarPagina()
    {
        Configuracion _configuracion = new SILPA.Comun.Configuracion();
        GrupoBiologico objGrupoBiologico = new GrupoBiologico();
        Listas _lista = new SILPA.LogicaNegocio.Generico.Listas();
        Utilidades.LlenarComboLista(objGrupoBiologico.ListaGruposBiologicos(), this.cboGrupoBiologicoNew, "GrupoBiologico", "GrupoBiologicoID", true);
        Utilidades.LlenarComboTabla(_lista.ListarDepartamentos(_configuracion.IdPaisPredeterminado).Tables[0], cboDepartamento, "DEP_NOMBRE", "DEP_ID", true);
        Utilidades.LlenarComboTabla(_lista.ListarAutoridades(null).Tables[0], cboAutoridadAmbiental, "AUT_DESCRIPCION", "AUT_ID", true);
        Utilidades.LlenarComboVacio(this.cboMunicipio);
    }
    private void LimpiarPopupAgregar()
    {
        _objLstInsumoRecoleccion = null;
        _objLstInsumoPreservacionMovilizacion = null;
        _objLstInsumoProfesional = null;
        if (this.cboTecnicaMuestreoNew.Items.Count > 1)
            this.cboTecnicaMuestreoNew.SelectedIndex = -1;
        if (this.cboTipoSacrificioNew.Items.Count > 1)
            this.cboTipoSacrificioNew.SelectedIndex = -1;
        Utilidades.LlenarComboVacio(cboFormacionAcademicaNew);
        //if (this.cboFormacionAcademicaNew.Items.Count > 1)
        //    this.cboFormacionAcademicaNew.SelectedIndex = 0;
        Utilidades.LlenarComboVacio(cboCaracteristica1New);
        Utilidades.LlenarComboVacio(cboUnidadMuestreoNew);
        Utilidades.LlenarComboVacio(cboEsfuerzo1New);
        Utilidades.LlenarComboVacio(cboTipoRecoleccionNew);
        //Utilidades.LlenarComboVacio(cboTipoSacrificioNew);
        Utilidades.LlenarComboVacio(cboTipoFijacionNew);
        Utilidades.LlenarComboVacio(cboTipoMovilizacionNew);
        //Utilidades.LlenarComboVacio(cboFormacionAcademicaNew);
        Utilidades.LlenarComboVacio(cboExperienciaEspecificaNew);
        this.cboExperienciaTiempoNew.SelectedIndex = 0;
        Utilidades.LlenarComboVacio(cboTipoRecoleccionNew);
        this.cboCaracteristica2New.Visible = false;
        this.rfvcboCaracteristica2New.Enabled = false;
        this.cboCaracteristica3New.Visible = false;
        this.rfvcboCaracteristica3New.Enabled = false;
        this.cboCaracteristica4New.Visible = false;
        this.rfvcboCaracteristica4New.Enabled = false;
        this.cboCaracteristica5New.Visible = false;
        this.rfvcboCaracteristica5New.Enabled = false;
        this.cboCaracteristica6New.Visible = false;
        this.rfvcboCaracteristica6New.Enabled = false;
        this.cboEsfuerzo2New.Visible = false;
        this.rfvcboEsfuerzo2New.Enabled = false;
        this.cboEsfuerzo3New.Visible = false;
        this.rfvcboEsfuerzo3New.Enabled = false;
        this.cboEsfuerzo4New.Visible = false;
        this.rfvcboEsfuerzo4New.Enabled = false;

        this.grvInsumoRecoleccion.DataSource = null;
        this.grvInsumoRecoleccion.DataBind();

        this.grvInsumoPreservMovNew.DataSource = null;
        this.grvInsumoPreservMovNew.DataBind();

        this.gvrInsumoProfesionalesNew.DataSource = null;
        this.gvrInsumoProfesionalesNew.DataBind();
        this.upnlAgregarGrupoBiologico.Update();

    }
    public string Caracteristicas(object lstobject)
    {
        string str_caracteristicas = string.Empty;
        if (lstobject != null)
        {
            foreach (var item in (List<CaracteristicaEntity>)lstobject)
            {
                str_caracteristicas += item.Caractaristica + " ";
            }
        }
        return str_caracteristicas.Trim();
    }
    public string EsfuerzoMuestreo(object lstobject)
    {
        string str_caracteristicas = string.Empty;
        if (lstobject != null)
        {
            foreach (var item in (List<EsfuerzoMuestreoEntity>)lstobject)
            {
                str_caracteristicas += item.EsfuerzoMuestreo + " ";
            }
        }
        return str_caracteristicas.Trim();
    }
    #endregion Metodos

    protected void btnAceptarMensajeAceptacion_Click(object sender, EventArgs e)
    {
        mpeMensajeAceptacion.Hide();
    }
    protected void grvCobertura_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvCobertura.PageIndex = e.NewPageIndex;
        this.grvCobertura.DataSource = this._objLstCobertura;
        this.grvCobertura.DataBind();
    }
}

