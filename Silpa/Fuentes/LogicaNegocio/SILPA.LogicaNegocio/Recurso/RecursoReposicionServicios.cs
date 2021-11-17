using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.RecursoReposicion;
using System.Data;

namespace SILPA.LogicaNegocio.Recurso
{
    public class RecursoReposicionServicios
    {
        public DataSet ObtenerNumeroVitalRecursoReposicion(string numeroVital, string expediente, int idAutoridad)
        {
            RecursoDalc dalc = new RecursoDalc();
            return dalc.ObtenerNumeroVitalRecursoReposicion(numeroVital, expediente, idAutoridad);
        }
     
    }
}
