using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for Navegador
/// </summary>
public class Navegador
{
    #region Declaracion Atributos
    /// <summary>
    /// Pagina desde la que se llama
    /// </summary>
    private string _pageRwd;

    /// <summary>
    /// Pagina a la que se llama
    /// </summary>
    private string _pageFwd;

    /// <summary>
    /// Si es necesario enviar datos de una pagina a otra viajan en esta variable
    /// </summary>
    private Xml _datos;

    /// <summary>
    /// Identificador del usuario que realizo el loggin en la aplicación
    /// </summary>
    private static int _idSolicitante;

    /// <summary>
    /// Identificador del proceso que esta realizando el usuario
    /// </summary>
    private int _idProceso;

    /// <summary>
    /// Nombre del usuario que realizó el loggin en la aplicación
    /// </summary>
    private static string _nombreSolicitante;
    #endregion


    public Navegador()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Declaracion Propiedades
    public string PageRwd
    {
        get { return _pageRwd; }
        set { _pageRwd = value; }
    }

    public string PageFwd
    {
        get { return _pageFwd; }
        set { _pageFwd = value; }
    }


    public Xml Datos
    {
        get { return _datos; }
        set { _datos = value; }
    }

    public int IdSolicitante
    {
        get { return _idSolicitante; }
        set { _idSolicitante = value; }
    }

    public int IdProceso
    {
        get { return _idProceso; }
        set { _idProceso = value; }
    }

    public string NombreSolicitante
    {
        get { return _nombreSolicitante; }
        set { _nombreSolicitante = value; }
    }
    #endregion
}
