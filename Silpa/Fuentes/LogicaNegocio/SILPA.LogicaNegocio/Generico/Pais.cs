using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;

namespace SILPA.LogicaNegocio.Generico
{
    public class Pais
    {
        public static string getNombrePaisById(int paisId)
        {
            PaisDalc objPais = new PaisDalc();
            PaisIdentity objPaisIndentity = new PaisIdentity();
            objPaisIndentity.Id = paisId;
            objPais.ObtenerPaises(ref objPaisIndentity);
            return objPaisIndentity.Nombre;
        }         
    }
}
