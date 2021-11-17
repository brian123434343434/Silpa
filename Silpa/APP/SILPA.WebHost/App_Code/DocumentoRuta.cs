using System;
using System.Collections.Generic;
using System.Web;
/// <summary>
/// Descripción breve de DocumentoRuta
/// </summary>
[Serializable]
public class DocumentoRuta
{
    private string _nombreArchivo;
    private string _rutaArchivo;

    public string NombreArchivo
    {
        get { return _nombreArchivo; }
        set { _nombreArchivo = value; }
    }
    public string RutaArchivo
    {
        get { return _rutaArchivo; }
        set { _rutaArchivo = value; }
    }
    public DocumentoRuta(string nombreArchivo, string rutaArchivo)
    {
        NombreArchivo = nombreArchivo;
        RutaArchivo = rutaArchivo;
    }
    public DocumentoRuta()
    { }
}
