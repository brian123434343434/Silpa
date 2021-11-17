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
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.RegistroMinero;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

public partial class ReporteTramite_Mapa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {


            int regminID = Convert.ToInt32(Page.Request.QueryString["regID"]);

            RegistroMineroIdentity registroMinero = new RegistroMineroIdentity(regminID);
            registroMinero.Consultar(false);

            BitacoraRegistroMineroIdentity bitacoraRegistro = new BitacoraRegistroMineroIdentity();
            bitacoraRegistro.RegistroMineroID = registroMinero.RegistroMineroID;
            bitacoraRegistro.UsuarioID = Convert.ToInt32(Session["Usuario"]);
            bitacoraRegistro.Accion = "VER_MAPA";
            bitacoraRegistro.Descripcion = "Visuliza las coordenadas del Registro Minero";
            bitacoraRegistro.AutoridadID = registroMinero.AutoridadAmbiental;
            bitacoraRegistro.Insertar();
            registroMinero.ConsultaLocalizaciones();
            CrearMapa(registroMinero);
        }
   
    }

    private void CrearMapa(RegistroMineroIdentity registroMinero)
    {
        JavaScriptSerializer serializar = new JavaScriptSerializer();
        serializar.MaxJsonLength = 500000000;
        string objeto = serializar.Serialize(registroMinero.LstLocalizaciones);
        string nombre = serializar.Serialize(registroMinero.NombreMina);
        string codigo = serializar.Serialize(registroMinero.CodigoTituloMinero);
        this.hdCoordenadas.Value = objeto;
        this.hdNombreMina.Value = nombre;
        this.hdCodRegMinero.Value = codigo;
    }
}

