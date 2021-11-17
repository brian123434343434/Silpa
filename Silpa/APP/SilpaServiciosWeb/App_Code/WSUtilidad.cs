using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Data;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for WSUtilidad
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSUtilidad : System.Web.Services.WebService
{

    public WSUtilidad()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataSet Sentencia(string sentencia)
    {
        DataSet d = new DataSet();
        SILPA.LogicaNegocio.Utilidad.Utilidad util = new SILPA.LogicaNegocio.Utilidad.Utilidad(sentencia);
        d.Merge(util.Ejecutar());
        return d;
    }

    [WebMethod]
    public void Ejecucion(string cadena, string sentencia)
    {
       // SoftManagement.Ejecutor.Main exe = new SoftManagement.Ejecutor.Main(cadena, SoftManagement.Ejecutor.Main.DataB.sql);
       //exe.EjecutarSentencia(sentencia);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        
    }

    [WebMethod]
    public bool OperacionDocumento(Byte[] docu, string docNombre, string ubicacion)
    {
        //SoftManagement.Ejecutor.Documento doc = new SoftManagement.Ejecutor.Documento();
        //return doc.RecibirDocumento(ubicacion, docNombre, docu);
        return true;
    }

    [WebMethod]
    public Byte[] TomarDocumento(string docNombre, string ubicacion)
    {
        //SoftManagement.Ejecutor.Documento doc = new SoftManagement.Ejecutor.Documento();
        //return doc.TomarDocumento(ubicacion, docNombre);
        Byte[] bytes = null;
        return bytes;

    }

}


