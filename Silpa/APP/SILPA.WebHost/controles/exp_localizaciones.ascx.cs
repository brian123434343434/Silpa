#region Namespaces predefinidos de .NET

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
using SILPA.AccesoDatos.RegistroMinero;
using System.Collections.Generic;

#endregion

/// <summary>
/// 
/// </summary>
public partial class general_exp_localizaciones : System.Web.UI.UserControl
{

    #region Documentación Atributo
    /// <summary>
    ///     Objeto que se encarga de manipular los datos y el comportamiento del administrador
    /// </summary>
    #endregion
    protected SILPA.AccesoDatos.RegistroMinero.RegistroMineroIdentity registroMineroIdentity;

    public int registroMineroID { get { return (int)ViewState["registroMineroId"]; } set { ViewState["registroMineroId"] = value; } }

    //private int IndexItemCoordenada { get { return (int)ViewState["IndexItemCoordenada"]; } set { ViewState["IndexItemCoordenada"] = value; } }

    public int localizacionID { get { return (int)ViewState["localizacionID"]; } set { ViewState["localizacionID"] = value; } }

    public LimitesAutoridadAmbiental Limites { get { return (LimitesAutoridadAmbiental)ViewState["Limites"]; } set { ViewState["Limites"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["idreg"] != null)
            {
                registroMineroID = Convert.ToInt32(Request.QueryString["idreg"]);
                CargarControl();
                localizacionID = 0;
                //IndexItemCoordenada = 0;
            }
        }
    }

    public void CargarControl()
    {
        if (this.registroMineroID > 0)
        {
            this.registroMineroIdentity = new SILPA.AccesoDatos.RegistroMinero.RegistroMineroIdentity(this.registroMineroID);
            registroMineroIdentity.Consultar(false);
            registroMineroIdentity.ConsultaLocalizaciones();
            this.dgv_localizaciones.DataSource = this.registroMineroIdentity.LstLocalizaciones;
            this.dgv_localizaciones.DataBind();

            Limites = new LimitesAutoridadAmbiental(registroMineroIdentity.AutoridadAmbiental).ConsultaLimitesAutoridadAmbiental();
        }
    }

    private bool ValidarCoordenadas(string[] coordenadas, out string strCoordenadasErroneas)
    {
        int cont = 1;
        bool cumple = true;
        strCoordenadasErroneas = "";
        // validamos si las coordenadas estan dentro de los limites de la autoridad ambiental.
        foreach (string coordenada in coordenadas)
        {
            double dcoordenada = 0;
            if (cont % 2 == 0) // pares son LONGITUDES
            {
                try
                {
                    dcoordenada = Convert.ToDouble(coordenada.Replace(".", ","));

                    if (!(Limites.LngMin <= dcoordenada && dcoordenada <= Limites.LngMax))
                    {
                        cumple = false;
                        strCoordenadasErroneas += "," + coordenada;
                    }
                }
                catch (Exception)
                {
                    strCoordenadasErroneas = "El formato de las coordenadas esta incorrecto";
                    cumple = false; 
                }
            }
            else // inpares son LATITUD
            {
                 try
                {
                    dcoordenada = Convert.ToDouble(coordenada.Replace(".", ","));
                    if (!(Limites.LatMin <= dcoordenada && dcoordenada <= Limites.LatMax))
                    {
                        cumple = false;
                        strCoordenadasErroneas += "," + coordenada;
                    }
                 }
                 catch (Exception)
                 {
                     strCoordenadasErroneas = "El formato de las coordenadas esta incorrecto";
                     cumple = false;
                 }
            }
            cont++;
        }
        return cumple;
    }

    private string ConvertiCordenadaGradosAPlana(int grados, int min, double segundos)
    {
        double doplano = ((Math.Abs(grados) + ((min * 60) + segundos) / 3600) * 1000000) / 1000000;
        return doplano.ToString();
    }
    private double CordenadaGradosAPlana(int grados, int min, double segundos)
    {
        double doplano = ((Math.Abs(grados) + ((min * 60) + segundos) / 3600) * 1000000) / 1000000;
        return doplano;
    }
    private string ConvertirRadianesAGrados(double radiandes)
    {
        //tomamos la parte entera del decimal
        string coordenada = "";
        int grados = (int)Math.Truncate(radiandes);
        coordenada = grados.ToString() + "°";
        //restamos los enteros a los radianes
        double ming = double.Parse(radiandes.ToString().Replace(grados.ToString(),"0"))*60;
        //tomamos la parte entera para obtener los minutos
        int min = (int)Math.Truncate(ming);
        coordenada = coordenada + min.ToString() + "'";
        //restamos los enteros a los radianes
        double segg = double.Parse(ming.ToString().Replace(min.ToString(), "0")) * 60;
        coordenada = coordenada + segg.ToString() + @"""";
        return coordenada;

    }

    protected void btn_adicionar_comunidad_Click(object sender, EventArgs e)
    {
        if (this.registroMineroID > -1)
        {

            this.registroMineroIdentity = new SILPA.AccesoDatos.RegistroMinero.RegistroMineroIdentity(this.registroMineroID);
            registroMineroIdentity.Consultar(false);
            registroMineroIdentity.ConsultaLocalizaciones();

            Localizacion localizacion = new Localizacion();
            localizacion.RegistroMineriaID = registroMineroID;
            localizacion.EnuOrigen = Origen.Bogota;
            localizacion.EnuGeometria = (Geometria)Int32.Parse(this.lst_tipo_geometria.SelectedValue);
            // JACOSTA:20111222. Validamos el numero de coordenadas registradas dependiento el tipo de geometría
            int numcoordenadas = 0;
            string[] coordenadas = this.txtCoordenadas.Text.Split(new char[]{','});
            if (coordenadas.Length % 2 == 0)
            {
                string strCoordenadasErroneas = "";
                if (ValidarCoordenadas(coordenadas, out strCoordenadasErroneas))
                {
                    // creamos los objetos de coordenadas
                    for (int i = 0; i < coordenadas.Length;i+=2)
                    {
                        Coordenada oCoor = new Coordenada();
                        oCoor.LocalizacionID = localizacion.LocalizacionID;
                        oCoor.CoordenadaNorte = double.Parse(coordenadas[i].Replace(".",","));
                        oCoor.CoordenadaEste = double.Parse(coordenadas[i+1].Replace(".",","));

                        localizacion.LstCoordenadas.Add(oCoor);
                    }
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Parent.GetType(), "alerta", "<script>alert('Las siguientes coordenadas estan fuera de los limites de la Autoridad: " + strCoordenadasErroneas + "')</script>");
                    return;
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.Parent.GetType(), "alerta", "<script>alert('Las coordenadas estan incompletas (Latitud, Longitud)')</script>");
                return;
            }
            if (localizacionID == 0)
                localizacion.InsertarLocalizacion();
            else
            {
                localizacion.LocalizacionID = localizacionID;
                localizacion.ActualizarLocalizacion();
            }
            this.registroMineroIdentity.ConsultaLocalizaciones();
            this.dgv_localizaciones.DataSource = this.registroMineroIdentity.LstLocalizaciones;
            this.dgv_localizaciones.DataBind();
            localizacionID = 0;
            this.limpiarFormulario();
        }
        else
            this.Page.ClientScript.RegisterStartupScript(this.Parent.GetType(), "alerta", "<script>alert('Primero registre la información general del registro Minero')</script>");

        this.Page.SetFocus(this.btn_adicionar_localizacion);
    }

    protected void limpiarFormulario()
    {
        this.txtCoordenadas.Text = string.Empty;
        this.lst_tipo_geometria.SelectedValue = "-1";
        this.lst_tipo_geometria.SelectedIndex = -1;
        this.btnCancelarEdicion.Visible = false;
        this.btn_adicionar_localizacion.Text = "Adicionar localización";
    }

    protected void dgv_localizaciones_DataBound(object sender, EventArgs e)
    {
        Localizacion localizacion = new Localizacion();
        ImageButton imb_aux;
        
        foreach (GridViewRow row in this.dgv_localizaciones.Rows)
        {
            int int_id = (Int32)this.dgv_localizaciones.DataKeys[row.RowIndex].Value;

            localizacion.LocalizacionID = int_id;
            localizacion.ConsultarLocalizacion();

            row.Cells[0].Text = localizacion.EnuGeometria.ToString();

            int int_cont = 0;

            foreach (Coordenada oCoor in localizacion.LstCoordenadas)
            {
                if (int_cont == 0)
                {
                    row.Cells[1].Text += "N-" + oCoor.CoordenadaNorte.ToString() + " " + "E-" + oCoor.CoordenadaEste + "  ";
                    row.Cells[2].Text += "N-" + ConvertirRadianesAGrados(double.Parse(oCoor.CoordenadaNorte.ToString())) + " E-" + ConvertirRadianesAGrados(double.Parse(oCoor.CoordenadaEste.ToString()));
                }
                else
                {
                    row.Cells[1].Text += "<BR />N-" + oCoor.CoordenadaNorte.ToString() + " " + "E-" + oCoor.CoordenadaEste + "  ";
                    row.Cells[2].Text += "<br />N-" + ConvertirRadianesAGrados(double.Parse(oCoor.CoordenadaNorte.ToString())) + " E-" + ConvertirRadianesAGrados(double.Parse(oCoor.CoordenadaEste.ToString()));
                }

                int_cont++;
            }

            if (this.lbl_sel_todos.Text == true.ToString())
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                chk.Checked = true;
            }
            imb_aux = (ImageButton)row.FindControl("imb_borrar");
            imb_aux.Attributes.Add("onclick", "confirmar('¿Está seguro de borrar la localización?')");
        }
    }

    protected void dgv_localizaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Localizacion localizacion = new Localizacion();
        
        switch (e.CommandName)
        {
            case "borrar":                
                    localizacion.LocalizacionID = (Int32)this.dgv_localizaciones.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                    localizacion.EliminarCoordenadas();
                    localizacion.EliminarLocalizacion();
                    this.CargarControl();
                break;
            //case "Editar":
            //    localizacion.LocalizacionID = (Int32)this.dgv_localizaciones.DataKeys[int_i].Value;
            //    break;
        }

        this.Page.SetFocus(this.btn_adicionar_localizacion);
    }

    protected void dgv_localizaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.dgv_localizaciones.PageIndex = e.NewPageIndex;
        CargarControl();

    }
    protected void dgv_localizaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Localizacion localizacion = new Localizacion();
        localizacion.LocalizacionID = (Int32)this.dgv_localizaciones.DataKeys[e.RowIndex].Value;
        localizacion.EliminarCoordenadas();
        localizacion.EliminarLocalizacion();
        this.CargarControl();
    }
    protected void dgv_localizaciones_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Localizacion localizacion = new Localizacion();
        localizacion.LocalizacionID = (Int32)this.dgv_localizaciones.DataKeys[e.NewEditIndex].Value;
        localizacion.ConsultarLocalizacion();
        this.lst_tipo_geometria.SelectedValue = ((int)localizacion.EnuGeometria).ToString();
        string coordenadas = "";
        foreach (Coordenada coordenada in localizacion.LstCoordenadas)
        {
            coordenadas += "," + coordenada.CoordenadaNorte.ToString().Replace(",", ".") + "," + coordenada.CoordenadaEste.ToString().Replace(",", ".");
        }
        this.txtCoordenadas.Text = coordenadas.Remove(0, 1);

        localizacionID = localizacion.LocalizacionID;
        this.btn_adicionar_localizacion.Text = "Actualizar localización";
        this.btnCancelarEdicion.Visible = true;
    }

    protected void btnCancelarEdicion_Click(object sender, EventArgs e)
    {
        this.btn_adicionar_localizacion.Text = "Adicionar localización";
        this.localizacionID = 0;
        this.btnCancelarEdicion.Visible = false;
        limpiarFormulario();
    }
}
