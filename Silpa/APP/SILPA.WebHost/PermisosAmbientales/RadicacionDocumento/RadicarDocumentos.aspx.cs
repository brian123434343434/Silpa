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
//using SILA.Servicios.PermisosAmbientales;
//using SILA.Servicios.Genericos;
//using SILPA.EntidadesNegocio.PermisosAmbientales;
using SILPA.LogicaNegocio.Generico;


public partial class LicenciasAmbientales_LPA_05_Anexar_Documentacion_Soporte_Solicitante : System.Web.UI.Page
{
    
    private string _RutaServerFileTraffic;
    public string RutaServerFileTraffic 
    { 
        get { return _RutaServerFileTraffic; } 
        set { _RutaServerFileTraffic = value; }
    }


    private string _numeroSilpa;
    public string NumeroSilpa
    {
        get { return _numeroSilpa; }
        set { _numeroSilpa = value; }
    }

    private string _xmlDatosFormulario;
    public string XmlDatosFormulario
    {
        get { return _xmlDatosFormulario; }
        set { _xmlDatosFormulario = value; }
    }


    private string _numeroActo;
    public string NumeroActo
    {
        get { return _numeroActo; }
        set { _numeroActo = value; }
    }


    private string _numeroRadicadoAA;
    public string NumeroRadicadoAA
    {
        get { return _numeroRadicadoAA; }
        set { _numeroRadicadoAA = value; }
    }

    private string _ubicacionDocumento;
    public string UbicacionDocumento
    {
        get { return _ubicacionDocumento; }
        set { _ubicacionDocumento = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Mensaje.LimpiarMensaje(this);
       
        if(!IsPostBack)
        {
            Random r = new Random();
            this.txtRadicadoAA.Text = DateTime.Now.ToString() + " " + r.Next(9999).ToString();
        }

        this._RutaServerFileTraffic = ConfigurationSettings.AppSettings["FILE_TRAFFIC"];
        
        
    }

    public bool validarControles()
    {
        bool blnResult = true;

        foreach (Control currentControl in Page.Controls)
        {
            foreach (Control child in currentControl.Controls)
            {
                foreach (Control schild in child.Controls)
                {
                    foreach (Control sschild in schild.Controls)
                    {
                        string tipo = child.UniqueID;
                        if (sschild is CheckBox)
                        {
                            if (((CheckBox)(sschild)).Checked == false)
                            {
                                blnResult = false;
                            }
                        }
                    }
                }

            }
        }
        return blnResult;
    }

    protected void BTN_GUARDAR_Click(object sender, EventArgs e)
    {

        //if (validarControles())
        //{
        //    //Response.Redirect("LPA_Diligenciar_Datos_DAA_Sec_II.aspx");
        //}
        //else 
        //{
        //    string Mensaje = "Debe diligenciar todos los documentos.";
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
        //    "<script>alert('" + Mensaje + "');</script>");
        //}

    }


    protected void btnRadicarDocumento_Click(object sender, EventArgs e)
    {
        // this.fldRadicarDocumento.FileBytes  : se otienen los bytes
        if (this.fldRadicarDocumento.FileName != string.Empty && this.fldRadicarDocumento.FileName != null)
        {
            //this.fldRadicarDocumento.SaveAs(this.RutaServerFileTraffic + this.fldRadicarDocumento.FileName);
            //this.RutaServerFileTraffic = this.RutaServerFileTraffic + this.fldRadicarDocumento.FileName;

            //RadicacionDocumento objRadicar = new RadicacionDocumento();

            //objRadicar.NitCorporacion = this.txtNitCorporacion.Text;
            //objRadicar.NombreCorporacion = this.txtNombreCorporacion.Text;
            //objRadicar.TelefonoCorporacion = this.txtTelefonoCorporacion.Text;

            //objRadicar.NombreSolicitante = this.txtNombreSolicitante.Text;
            //objRadicar.NumeroRadicadoAA = this.txtRadicadoAA.Text;
            //objRadicar.NumeroSilpa = this.txtNumeroSilpa.Text;

            //objRadicar.IdentificacionSolicitante = this.txtIdentificacionSolicitante.Text;
            //objRadicar.UbicacionDocumento = this.RutaServerFileTraffic + this.fldRadicarDocumento.FileName;

            //this._xmlDatosFormulario = objRadicar.GetXml();


            //RadicacionDocumentoServicio objRadServicio = new RadicacionDocumentoServicio();
            ////objRadServicio.RadicarSolicitud(this._numeroSilpa, this._xmlDatosFormulario, this.NumeroActo, ref this._numeroRadicadoAA, this.UbicacionDocumento, ref objRadicar);
            //this._numeroRadicadoAA = objRadicar.NumeroRadicadoAA;
            //this.UbicacionDocumento = objRadicar.UbicacionDocumento;

            //TODO Verificar integración con servicio
            //RadicacionDocumentoServicio objRadServicio = new RadicacionDocumentoServicio();
            //objRadServicio.RadicarSolicitud(this._numeroSilpa, this._xmlDatosFormulario, this.NumeroActo, ref this._numeroRadicadoAA, this.UbicacionDocumento, ref objRadicar);
            //this._numeroRadicadoAA = objRadicar.NumeroRadicadoAA;
            //this.UbicacionDocumento = objRadicar.UbicacionDocumento;



            ////objRadServicio.RadicarSolicitud(objRadicar.NumeroSilpa, this._xmlDatosFormulario, this.NumeroActo, ref this._numeroRadicadoAA, this.UbicacionDocumento, ref objRadicar);
            //objRadServicio.RadicarCorrespondencia(objRadicar.NumeroRadicadoAA, objRadicar.NumeroSilpa, "Juan Carlos Mendez Jimenez");
            //this.txtNumeroSilpa.Text = objRadicar.NumeroSilpa;

            //objRadServicio.RadicarSolicitud(objRadicar.NumeroSilpa, this._xmlDatosFormulario, this.NumeroActo, ref this._numeroRadicadoAA, this.UbicacionDocumento, ref objRadicar);
            //TODO - Verificar integración con servicio
            //objRadServicio.RadicarCorrespondencia(objRadicar.NumeroRadicadoAA, objRadicar.NumeroSilpa, "Juan Carlos Mendez Jimenez");
            //this.txtNumeroSilpa.Text = objRadicar.NumeroSilpa;


            Mensaje.MostrarMensaje(this, "Documentos adjuntos exitosamente !!");
        }
        else 
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Alerta", "<script>alert('Debe adjuntar los documentos !!");
        }
    }
}
