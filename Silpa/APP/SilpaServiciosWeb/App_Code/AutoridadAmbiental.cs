using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Descripción breve de AutoridadAmbiental
/// </summary>
[Serializable]

public class AutoridadAmbiental
{
    protected int intAutoridad;
    protected string strNombreAutoridad;
    public int AutoridadID { get{ return intAutoridad;} set{intAutoridad = value;} }
    public string NombreAutoridad { get { return strNombreAutoridad; } set { strNombreAutoridad = value; } }
	public AutoridadAmbiental()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public AutoridadAmbiental(int AutoridadID, string NombreAutoridad)
    {
        this.intAutoridad = AutoridadID;
        this.strNombreAutoridad = NombreAutoridad;
    }
}