using System;
using System.Collections.Generic;
using System.Text;

namespace SoftManagement.Persistencia
{
    public class NumeroEIA
    {
        public static string NumeroVital(int IdProyecto,  string IdEmpresa) 
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Generacion dat = new SILPA.AccesoDatos.ResumenEIA.Generacion.Generacion();
            return dat.NumeroVitalEIA(IdProyecto, IdEmpresa); 
        }
    }
}
