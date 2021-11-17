using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using SILPA.Comun;
using SILPA.LogicaNegocio.DAA;
using System.Data;

using SILPA.Servicios.SolicitudDAA;


public partial class LicenciasAmbientales_Dec1220 : ControladorBase
{


    //public event System.EventHandler SeleccionesBound;
    //DataTable tbMultiUbicaciones;
    //SolicitudDAAEIA objSolicitud;
    //Ubicacion objUbicacion;
    //SolicitudFachada objSolFachada;
    //List<string> lstNombresFiles;
    //List<byte[]> lstBytesFiles;



    protected void Page_Load(object sender, EventArgs e)
    {
        //objSolicitud = new SolicitudDAAEIA();
        //objUbicacion = new Ubicacion();

        


        //if (!IsPostBack)
        //{
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "fntConflicto()", true);
        //    tbMultiUbicaciones = new DataTable();
        //    tbMultiUbicaciones.Columns.Add("Departamento", Type.GetType("System.String"));
        //    tbMultiUbicaciones.Columns.Add("IdDepto", Type.GetType("System.Int32"));
        //    tbMultiUbicaciones.Columns.Add("Municipio", Type.GetType("System.String"));
        //    tbMultiUbicaciones.Columns.Add("IdMun", Type.GetType("System.Int32"));
        //    tbMultiUbicaciones.Columns.Add("Corregimiento", Type.GetType("System.String"));
        //    tbMultiUbicaciones.Columns.Add("idCorreg", Type.GetType("System.Int32"));
        //    tbMultiUbicaciones.Columns.Add("Vereda", Type.GetType("System.String"));
        //    tbMultiUbicaciones.Columns.Add("IdVereda", Type.GetType("System.Int32"));
        //    tbMultiUbicaciones.Columns.Add("Cuenca", Type.GetType("System.String"));
        //    tbMultiUbicaciones.Columns.Add("IdCuenca", Type.GetType("System.Int32"));
        //    tbMultiUbicaciones.Columns.Add("Urbano", Type.GetType("System.Boolean"));

        //    ViewState["TablaTemp"] = tbMultiUbicaciones;

        //    this.ddlSector.DataSource = objSolicitud.ListarSectores();
        //    this.ddlSector.DataTextField = "SEC_NOMBRE";
        //    this.ddlSector.DataValueField = "SEC_ID";
        //    this.ddlSector.DataBind();

        //    this.ddl_Depto.DataSource = objUbicacion.ListarDeptos(-1).Tables[0];
        //    this.ddl_Depto.DataValueField = "DEP_ID";
        //    this.ddl_Depto.DataTextField = "DEP_NOMBRE";
        //    this.ddl_Depto.DataBind();

        //    this.ddlSector.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        //    this.ddlTipoProyecto.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        //    this.ddl_Depto.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        //    this.ddl_Municipio.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        //    this.ddl_Corregimiento.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        //    this.ddl_Vereda.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        //    this.ddl_Cuenca.Items.Insert(0, new ListItem("Seleccione...", "-1"));
        //    this.ddl_AA.Items.Insert(0, new ListItem("Seleccione...", "-1"));

        //    pnlAA.Visible = false;
        //    //pnl_MAVDT.Visible = false;

        //    gv_Ubicacion.DataBind();

        //}
        //else
        //{

        //}
    }


//    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        objSolicitud.objIdentity.IdSector = Convert.ToInt32(ddlSector.SelectedValue);
//        this.ddlTipoProyecto.DataSource = objSolicitud.ConsultarTipoProyecto(objSolicitud.objIdentity.IdSector);
//        this.ddlTipoProyecto.DataValueField = "SEC_ID";
//        this.ddlTipoProyecto.DataTextField = "SEC_NOMBRE";
//        this.ddlTipoProyecto.DataBind();
//        this.ddlTipoProyecto.Items.Insert(0, new ListItem("Seleccione...", "-1"));


//    }


//    protected void ddl_Depto_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        this.ddl_Municipio.DataSource = objUbicacion.ListarDeptosMunicipios(Convert.ToInt32(ddl_Depto.SelectedValue), -1);
//        this.ddl_Municipio.DataValueField = "MUN_ID";
//        this.ddl_Municipio.DataTextField = "MUN_NOMBRE";
//        this.ddl_Municipio.DataBind();
//        this.ddl_Municipio.Items.Insert(0, new ListItem("Seleccione...", "-1"));

//    }

//    protected void ddl_Municipio_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        this.ddl_Corregimiento.DataSource = objUbicacion.ListarCorregimientos(Convert.ToInt32(ddl_Municipio.SelectedValue));
//        this.ddl_Corregimiento.DataValueField = "COR_ID";
//        this.ddl_Corregimiento.DataTextField = "COR_NOMBRE";
//        this.ddl_Corregimiento.DataBind();
//        this.ddl_Corregimiento.Items.Insert(0, new ListItem("Seleccione...", "-1"));


//        this.ddl_Vereda.DataSource = objUbicacion.ListarVeredas(Convert.ToInt32(ddl_Municipio.SelectedValue));
//        this.ddl_Vereda.DataValueField = "VER_ID";
//        this.ddl_Vereda.DataTextField = "VER_NOMBRE";
//        this.ddl_Vereda.DataBind();
//        this.ddl_Vereda.Items.Insert(0, new ListItem("Seleccione...", "-1"));

//        this.ddl_Cuenca.DataSource = objUbicacion.ListarCuencas();
//        this.ddl_Cuenca.DataValueField = "CUE_ID";
//        this.ddl_Cuenca.DataTextField = "CUE_NOMBRE";
//        this.ddl_Cuenca.DataBind();
//        this.ddl_Cuenca.Items.Insert(0, new ListItem("Seleccione...", "-1"));
//    }

//    protected void btnUbicacion_Click(object sender, EventArgs e)
//    {

//        tbMultiUbicaciones = (DataTable)ViewState["TablaTemp"];
//        DataRow drTemp = tbMultiUbicaciones.NewRow();

//        drTemp[0] = ddl_Depto.SelectedItem.Text;
//        drTemp[1] = Convert.ToInt32(ddl_Depto.SelectedValue);
//        drTemp[2] = ddl_Municipio.SelectedItem.Text;
//        drTemp[3] = Convert.ToInt32(ddl_Municipio.SelectedValue);

//        if (ddl_Corregimiento.SelectedValue == "-1")
//        {
//            drTemp[4] = " ";
//            drTemp[5] = 0;
//        }
//        else
//        {
//            drTemp[4] = ddl_Corregimiento.SelectedItem.Text;
//            drTemp[5] = Convert.ToInt32(ddl_Corregimiento.SelectedValue);
//        }

//        if (ddl_Vereda.SelectedValue == "-1")
//        {
//            drTemp[6] = " ";
//            drTemp[7] = 0;
//        }
//        else
//        {
//            drTemp[6] = ddl_Vereda.SelectedItem.Text;
//            drTemp[7] = Convert.ToInt32(ddl_Vereda.SelectedValue);
//        }

//        if (ddl_Cuenca.SelectedValue == "-1")
//        {
//            drTemp[8] = " ";
//            drTemp[9] = 0;
//        }
//        else
//        {
//            drTemp[8] = ddl_Cuenca.SelectedItem.Text;
//            drTemp[9] = Convert.ToInt32(ddl_Cuenca.SelectedValue);
//        }

//        drTemp[10] = chk_Urbano.Checked;

//        tbMultiUbicaciones.Rows.Add(drTemp);
//        ViewState["Tabla"] = tbMultiUbicaciones;

//        //ViewState["TableTemp"] = tbMultiUbicaciones;
//        gv_Ubicacion.DataSource = tbMultiUbicaciones;

//        gv_Ubicacion.DataBind();


//        ddl_Corregimiento.DataSource = null;
//        ddl_Corregimiento.DataBind();
//        ddl_Municipio.DataSource = null;
//        ddl_Municipio.DataBind();
//        ddl_Vereda.DataSource = null;
//        ddl_Vereda.DataBind();
//        ddl_Cuenca.DataSource = null;
//        ddl_Cuenca.DataBind();

//    }

//    protected void bttAceptar_Click(object sender, EventArgs e)
//    {
//        //pnl_AutAmbiental.Visible = true;
//        //pnl_MAVDT.Visible = true;

//        tbMultiUbicaciones = (DataTable)ViewState["TablaTemp"];
//        objSolicitud.objIdentity.IdTipoProyecto = Convert.ToInt32(ddlTipoProyecto.SelectedValue);
//        objSolicitud.objIdentity.IdSector = Convert.ToInt32(ddlSector.SelectedValue);
//        objSolFachada = new SolicitudFachada();
//        lstNombresFiles = new List<string>();
//        lstBytesFiles = new List<byte[]>();

//        if (fu_AdjAnexo.HasFile)
//        {
//            lstNombresFiles.Add(fu_AdjAnexo.FileName);
//            lstBytesFiles.Add(fu_AdjAnexo.FileBytes);
//        }

//        if (!objSolicitud.PerteneceMAVDT(objSolicitud.objIdentity.IdTipoProyecto, objSolicitud.objIdentity.IdSector))
//        {
////            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "fntConflicto()", true);


//            if (objSolicitud.RequiereDAA(objSolicitud.objIdentity.IdTipoProyecto, objSolicitud.objIdentity.IdSector))
//            {
//                pnlAA.Visible = true;
                
//                foreach (DataRow r in tbMultiUbicaciones.Rows)
//                {
//                    if (ddl_AA.Items.Count < 0)
//                    {
//                        this.ddl_AA.DataSource = objSolicitud.ListarAutoridadAmbientalesXMunicipio(objSolicitud.objIdentity.IdTipoProyecto, Convert.ToInt32(r["Municipio"]));
//                        this.ddl_AA.DataValueField = "ID_AUT_AMBIENTAL";
//                        this.ddl_AA.DataTextField = "AUT_NOMBRE";
//                        this.ddl_AA.DataBind();
//                        this.ddl_AA.Items.Insert(0, new ListItem("Seleccione...", "-1"));
//                    }
//                    else
//                    {
//                        for (int i = 0; i < objSolicitud.ListarAutoridadAmbientalesXMunicipio(objSolicitud.objIdentity.IdTipoProyecto, Convert.ToInt32(r["IdMun"])).Tables[0].Rows.Count; i++)
//                        {

//                            foreach (ListItem liddl in ddl_AA.Items)
//                            {
//                                if (!(Convert.ToString(objSolicitud.ListarAutoridadAmbientalesXMunicipio(objSolicitud.objIdentity.IdTipoProyecto, Convert.ToInt32(r["IdMun"])).Tables[0].Rows[i]["ID_AUT_AMBIENTAL"]) == liddl.Value))
//                                {
//                                    ddl_AA.Items.Add(new ListItem(Convert.ToString(objSolicitud.ListarAutoridadAmbientalesXMunicipio(objSolicitud.objIdentity.IdTipoProyecto, Convert.ToInt32(Convert.ToInt32(r["IdMun"]))).Tables[0].Rows[i]["AUT_NOMBRE"]), Convert.ToString(objSolicitud.ListarAutoridadAmbientalesXMunicipio(objSolicitud.objIdentity.IdTipoProyecto, Convert.ToInt32(Convert.ToInt32(r["IdMun"]))).Tables[0].Rows[i]["ID_AUT_AMBIENTAL"])));
//                                }
//                            }
//                        }
//                    }
//                    objSolicitud.objIdentity.IdAutoridadAmbiental = Convert.ToInt32(ddl_AA.SelectedValue);
//                }
//            }
//            else
//            {
//                objSolicitud.objIdentity.IdAutoridadAmbiental = 76;
//                //Creacion de solicitud para el caso de que sea EIA
//                objSolFachada.SolicitarDDA("SoftManagement", 7, 54, 0, "0", "0", "0",ref objSolicitud, lstNombresFiles, lstBytesFiles, "");
//                ///Response.Redirect("~/LicenciasAmbientales/DAA-Prot.aspx");         
//            }
//        }
//        else
//        {
//            objSolicitud.objIdentity.IdAutoridadAmbiental = 76;
//            //Creacion de solicitud para el caso de que sea DAA donde la AA es el MAVDT
//            //objSolicitud.objIdentity.IdAutoridadAmbiental = 
//            objSolFachada.SolicitarDDA("SoftManagement", 7, 52, 0, "0", "0", "0",ref objSolicitud, lstNombresFiles, lstBytesFiles, "");
//        }
//    }

//    protected void ddl_tipo_proyecto_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        this.pnlAA.Visible = false;
//        //this.pnl_MAVDT.Visible = false;
//    }

//    protected void btn_DAA_Click(object sender, EventArgs e)
//    {
//        Response.Redirect("~/LicenciasAmbientales/DAA-Prot.aspx");
//    }

//    //protected void btn_continuar_Click(object sender, EventArgs e)
//    //{
//    //    objSolFachada = new SolicitudFachada();
//    //    lstNombresFiles = new List<string>();
//    //    lstBytesFiles = new List<byte[]>();
//    //    lstNombresFiles.Add(fu_AdjAnexo.FileName);
//    //    lstBytesFiles.Add(fu_AdjAnexo.FileBytes);

//    //    objSolicitud.objIdentity.IdAutoridadAmbiental = Convert.ToInt32(ddl_AA.SelectedValue);

//    //    if (rbl_eia_especifico.SelectedItem.Value == "76")
//    //    {
//    //        //Creacion de solicitud para el caso de que sea DAA
//    //        objSolFachada.SolicitarDDA("SoftManagement", 7, 52, 0, "0", "0", "0", objSolicitud, lstNombresFiles, lstBytesFiles, "");

//    //    }
//    //}

//    //protected void rbl_tipo_eia_SelectedIndexChanged(object sender, EventArgs e)
//    //{
//    //    if (rbl_tipo_eia.SelectedItem.Value == "2")
//    //    {
//    //        this.lbl_especificos.Text = "¿Desea solicitar los Términos de Referencia para EIA?";
//    //        //this.pnl_eia.Visible = true;
//    //        this.pnlAA.Visible = true;
//    //        this.rbl_eia_especifico.Visible = true;
//    //    }
//    //    else
//    //    {
//    //        //this.pnl_eia.Visible = true;
//    //        this.pnlAA.Visible = true;
//    //        this.lbl_especificos.Text = "Datos EIA";
//    //        this.rbl_eia_especifico.Visible = false;
//    //    }
//    //}

//    //protected void bttUbicacion_Click(object sender, EventArgs e)
//    //{
//    //    bool bln_adicionar = true;

//    //    // Mira si es un hijo
//    //    if (this.ddl_Municipio.Items.Count > 0)
//    //    {
//    //        // Verifica si esta repetido				
//    //        foreach (DataRow r in tbMultiUbicaciones.Rows)
//    //        {
//    //            if (Convert.ToInt32(r["IdMun"]) == this.ddl_Municipio.SelectedValue)
//    //            {
//    //                bln_adicionar = false;
//    //                break;
//    //            }
//    //        }

//    //        if (bln_adicionar)
//    //        {
//    //            if (Convert.ToInt32(this.ddl_Municipio.SelectedValue) > -1)
//    //                this.lbx_items.Items.Add(new ListItem(this.ddl_Municipio.SelectedItem.Text, this.ddl_Municipio.SelectedValue));
//    //            this.OnBound(sender);
//    //        }
//    //        else
//    //        {
//    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Este item ya ha sido adicionado!");
//    //        }
//    //    }



//    //}



    
//    protected virtual void OnBound(object sender)
//    {
//        // Dispara el evento Selecciones Bound 
//        if (this.SeleccionesBound != null)
//            this.SeleccionesBound(sender, new EventArgs());
//    }

//    protected void ddl_AA_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        objSolicitud.objIdentity.IdAutoridadAmbiental = Convert.ToInt32(ddl_AA.SelectedValue);
//    }
}
