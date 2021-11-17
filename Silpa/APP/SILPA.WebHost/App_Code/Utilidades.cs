using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;

/// <summary>
/// Summary description for Utilidades
/// </summary>
public class Utilidades : System.Web.UI.Page
{

    #region     proceso para invocar pestañas y encriptar url's
    public void Dispose()
    {
        // Nothing to dispose
    }

    public void Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    #endregion

    private const string PARAMETER_NAME = "enc=";
    private const string ENCRYPTION_KEY = "key";

    void context_BeginRequest(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
        {
            string query = ExtractQuery(context.Request.RawUrl);
            string path = GetVirtualPath();

            if (query.StartsWith(PARAMETER_NAME, StringComparison.OrdinalIgnoreCase))
            {
                // Decrypts the query string and rewrites the path.
                string rawQuery = query.Replace(PARAMETER_NAME, string.Empty);
                string decryptedQuery = Decrypt(rawQuery);
                context.RewritePath(path, string.Empty, decryptedQuery);
            }
            else if (context.Request.HttpMethod == "GET")
            {
                // Encrypt the query string and redirects to the encrypted URL.
                // Remove if you don't want all query strings to be encrypted automatically.
                string encryptedQuery = Encrypt(query);
                context.Response.Redirect(path + encryptedQuery);
            }
        }
    }

    /// <summary>
    /// Parses the current URL and extracts the virtual path without query string.
    /// </summary>
    /// <returns>The virtual path of the current URL.</returns>
    private static string GetVirtualPath()
    {
        string path = HttpContext.Current.Request.RawUrl;
        path = path.Substring(0, path.IndexOf("?"));
        path = path.Substring(path.LastIndexOf("/") + 1);
        return path;
    }

    /// <summary>
    /// Parses a URL and returns the query string.
    /// </summary>
    /// <param name="url">The URL to parse.</param>
    /// <returns>The query string without the question mark.</returns>
    private static string ExtractQuery(string url)
    {
        int index = url.IndexOf("?") + 1;
        return url.Substring(index);
    }

    #region Encryption/decryption

    /// <summary>
    /// The salt value used to strengthen the encryption.
    /// </summary>
    private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());

    /// <summary>
    /// Encrypts any string using the Rijndael algorithm.
    /// </summary>
    /// <param name="inputText">The string to encrypt.</param>
    /// <returns>A Base64 encrypted string.</returns>
    public static string Encrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

        using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();
                    return "?" + PARAMETER_NAME + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }

    /// <summary>
    /// Decrypts a previously encrypted string.
    /// </summary>
    /// <param name="inputText">The encrypted string to decrypt.</param>
    /// <returns>A decrypted string.</returns>
    public static string Decrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] encryptedData = Convert.FromBase64String(inputText);
        PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

        using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainText = new byte[encryptedData.Length];
                    int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                    return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                }
            }
        }
    }

    public enum _modo_apertura_formulario
    {
        vacio = 0,
        open = 1,
        location = 2,
        ejecutarScript = 3
    }

    #endregion




    public static void AlertWindow(Page page, string sMessage, string sUrlRedirect, int iModoAperturaFormulario)
    {
        System.Text.StringBuilder sJscrpt = new System.Text.StringBuilder("");
        if (sMessage != null && page != null)
        {
            sMessage = sMessage.Replace("'", "").Replace("''", "");
            sMessage = sMessage.Replace("\\r\\n", "");
            sJscrpt.Append("<script language='javascript'> ");

            if (!sMessage.Equals(""))
            {
                sJscrpt.Append("alert('" + sMessage + "'); ");
            }

            if (iModoAperturaFormulario != (int)_modo_apertura_formulario.vacio)
            {
                if (!sUrlRedirect.Equals(""))
                {
                    switch (iModoAperturaFormulario)
                    {
                        case (int)_modo_apertura_formulario.open:
                            //sJscrpt.Append("document.open('" + sUrlRedirect + "');");
                            sJscrpt.Append("window.open('" + sUrlRedirect + "');");
                            break;
                        case (int)_modo_apertura_formulario.location:
                            sJscrpt.Append("window.location.assign('" + sUrlRedirect + "');");
                            break;
                        case (int)_modo_apertura_formulario.ejecutarScript:
                            sJscrpt.Append(sUrlRedirect);
                            break;
                    }
                }
            }

            sJscrpt.Append("</script>");

            ScriptManager Smgr = ScriptManager.GetCurrent(page);
            if (Smgr == null)
            {
                if (!page.ClientScript.IsClientScriptBlockRegistered("Mensaje"))
                {
                    page.ClientScript.RegisterClientScriptBlock(page.GetType(), "Mensaje", sJscrpt.ToString(), false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "Mensaje", sJscrpt.ToString(), false);

            }
        }
    }

    //jmartinez enum para invocar pestañas en java script


    public Utilidades()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void CerrarVentana(Page page)
    {
        string mensaje = "<script>window.close();</script>";
        page.ClientScript.RegisterStartupScript(page.GetType(), "MensajeAlert", mensaje);
    }

    /// <summary>
    /// Convierte un objeto de tipo System.Array a System.Data.DataTable
    /// </summary>
    /// <param name="source">Objeto de tipo System.Array, todos los objetos contenidos en el arreglo deben ser del mismo tipo, si uno de las propiedades de un objeto contenido en el arreglo es de tipo System.Nullable será convertido a System.String</param>
    /// <returns></returns>
    public static DataTable arrayToDataTable(Array source)
    {
        if (source == null || source.Length == 0)
            return null;

        DataTable dtResult = new DataTable();
        PropertyInfo[] properties = source.GetValue(0).GetType().GetProperties();
        DataRow drResult = null;

        for (int i = 0; i < properties.Length; i++)
            dtResult.Columns.Add(properties[i].Name, (properties[i].PropertyType.FullName.ToLower().Contains("system.nullable") ? "".GetType() : properties[i].PropertyType));

        for (int i = 0; i < source.Length; i++)
        {

            drResult = dtResult.NewRow();

            for (int j = 0; j < properties.Length; j++)
                drResult[properties[j].Name] = properties[j].GetValue(source.GetValue(i), null);

            dtResult.Rows.Add(drResult);
        }

        return dtResult;
    }
    public static void LlenarComboTabla(DataTable dtDatos, DropDownList combo, string texto, string valor, bool agregarSeleccione)
    {
        combo.Items.Clear();
        combo.DataSource = dtDatos;
        combo.DataTextField = texto;
        combo.DataValueField = valor;
        combo.DataBind();
        if (agregarSeleccione)
        {
            combo.Items.Insert(0, new ListItem("Seleccione.", ""));
        }
    }
    public static void LlenarComboLista(object dtDatos, DropDownList combo, string texto, string valor, bool agregarSeleccione)
    {
        combo.Items.Clear();
        combo.DataSource = dtDatos;
        combo.DataTextField = texto;
        combo.DataValueField = valor;
        combo.DataBind();
        if (agregarSeleccione)
        {
            combo.Items.Insert(0, new ListItem("Seleccione.", ""));
            combo.SelectedIndex = -1;
        }
    }
    public static void LlenarComboVacio(DropDownList combo)
    {
        combo.Items.Clear();
        combo.Items.Insert(0, new ListItem("Seleccione.", ""));
        combo.SelectedIndex = -1;
    }
    public void DescargarArchivo(string ruta, string nombre, HttpResponse Response)
    {
        Byte[] arrContent;
        System.IO.FileStream FS;
        FS = null;
        FS = new System.IO.FileStream(ruta + nombre, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        Byte[] Input = new byte[FS.Length];
        FS.Read(Input, 0, int.Parse(FS.Length.ToString()));
        arrContent = (byte[])Input;
        Response.ContentType = "application/octet-stream";
        Response.OutputStream.Write(arrContent, 0, arrContent.Length);
        Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre);
        FS.Close();
        FS.Dispose();
        FS = null;
    }
    public static void MoveFileDirectoy(DirectoryInfo source, DirectoryInfo destination)
    {
        if (!destination.Exists)
        {
            destination.Create();
        }

        FileInfo[] files = source.GetFiles();
        foreach (FileInfo file in files)
        {
            file.MoveTo(Path.Combine(destination.FullName,
                file.Name));
        }
    }
    public static void MoveFile(FileInfo ArchivoOrigen, DirectoryInfo destino)
    {
        if (!destino.Exists)
        {
            destino.Create();
        }
        ArchivoOrigen.MoveTo(Path.Combine(destino.FullName, ArchivoOrigen.Name));
    }

    public string NumeroALetras(string num)
    {
        string res, dec = "";
        Int64 entero;
        int decimales;
        double nro;

        try
        {
            nro = Convert.ToDouble(num);
        }
        catch
        {
            return "";
        }

        entero = Convert.ToInt64(Math.Truncate(nro));
        decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
        if (decimales > 0)
        {
            dec = " CON " + NumeroALetras(decimales.ToString()) + "/CENTAVOS";
        }

        res = toText(Convert.ToDouble(entero)) + dec;
        return res;
    }

    private string toText(double value)
    {
        string Num2Text = "";
        value = Math.Truncate(value);
        if (value == 0) Num2Text = "CERO";
        else if (value == 1) Num2Text = "UNO";
        else if (value == 2) Num2Text = "DOS";
        else if (value == 3) Num2Text = "TRES";
        else if (value == 4) Num2Text = "CUATRO";
        else if (value == 5) Num2Text = "CINCO";
        else if (value == 6) Num2Text = "SEIS";
        else if (value == 7) Num2Text = "SIETE";
        else if (value == 8) Num2Text = "OCHO";
        else if (value == 9) Num2Text = "NUEVE";
        else if (value == 10) Num2Text = "DIEZ";
        else if (value == 11) Num2Text = "ONCE";
        else if (value == 12) Num2Text = "DOCE";
        else if (value == 13) Num2Text = "TRECE";
        else if (value == 14) Num2Text = "CATORCE";
        else if (value == 15) Num2Text = "QUINCE";
        else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
        else if (value == 20) Num2Text = "VEINTE";
        else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
        else if (value == 30) Num2Text = "TREINTA";
        else if (value == 40) Num2Text = "CUARENTA";
        else if (value == 50) Num2Text = "CINCUENTA";
        else if (value == 60) Num2Text = "SESENTA";
        else if (value == 70) Num2Text = "SETENTA";
        else if (value == 80) Num2Text = "OCHENTA";
        else if (value == 90) Num2Text = "NOVENTA";
        else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
        else if (value == 100) Num2Text = "CIEN";
        else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
        else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
        else if (value == 500) Num2Text = "QUINIENTOS";
        else if (value == 700) Num2Text = "SETECIENTOS";
        else if (value == 900) Num2Text = "NOVECIENTOS";
        else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
        else if (value == 1000) Num2Text = "MIL";
        else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
        else if (value < 1000000)
        {
            Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
            if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
        }

        else if (value == 1000000) Num2Text = "UN MILLON";
        else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
        else if (value < 1000000000000)
        {
            Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
            if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
        }

        else if (value == 1000000000000) Num2Text = "UN BILLON";
        else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

        else
        {
            Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
            if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        }
        return Num2Text.ToLower();

    }

    public bool ValidacionToken()
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


    /// <summary>
    /// Obtener la dirección IP desde  la cual se realiza llamado
    /// </summary>
    /// <param name="p_objRequest">HttpRequest con información de llamado</param>
    /// <returns>string con la dirección IP</returns>    
    public static string ObtenerDireccionIP(HttpRequest p_objRequest)
    {
        string strDireccionIP = "";

        //Ciclo que obtiene dirección IP
        foreach (IPAddress IPA in Dns.GetHostAddresses(p_objRequest.UserHostAddress))
        {
            if (IPA.AddressFamily.ToString() == "InterNetwork" || IPA.AddressFamily.ToString() == "InterNetworkV6")
            {
                strDireccionIP = IPA.ToString();
                break;
            }
        }

        //Validar si es dirección local
        if (strDireccionIP == "127.0.0.1" || strDireccionIP == "::1")
        {
            //Inicializar direción
            strDireccionIP = "";

            //Ciclo que realiza busqueda nuevamente
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    strDireccionIP = IPA.ToString();
                    break;
                }
            }
        }

        return strDireccionIP;
    }
}
