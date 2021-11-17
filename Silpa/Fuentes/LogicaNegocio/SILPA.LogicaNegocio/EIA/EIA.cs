using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SILPA.LogicaNegocio.EIA
{
    public class EIA
    {
        public DataSet ConsultarFormularios(string numeroVital, int userId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            AccesoDatos.EIA.EIADalc objEIA = new AccesoDatos.EIA.EIADalc();
            return objEIA.ConsultarFormularios(numeroVital, userId, fechaDesde, fechaHasta);
        }
        public DataSet ConsultaListaNumeroVital(int userId)
        {
            AccesoDatos.EIA.EIADalc objEIA = new AccesoDatos.EIA.EIADalc();
            return objEIA.ConsultarListaNumeroVital(userId);
        }
    }
}
