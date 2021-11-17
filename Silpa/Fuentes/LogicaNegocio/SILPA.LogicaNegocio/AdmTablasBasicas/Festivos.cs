using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Festivos
    {
        private FestivosDalc objFestivosDalc;

        public Festivos()
        {
            objFestivosDalc = new FestivosDalc();
        }

        public DataTable ConsultaFestivos()
        {
            return objFestivosDalc.Listar_Festivos();
        }

        public void InsertarFestivos(DateTime fecha)
        {
            objFestivosDalc.Insertar_Festivo(fecha);
        }

        public void EliminarFestivos(int fechaId)
        {
            objFestivosDalc.Eliminar_Festivo(fechaId);
        }
    }
}

