using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.AdmTablasBasicas;
using System.Data;

namespace SILPA.LogicaNegocio.Audiencia.AdmTablasBasicas
{
    public class AutoridadAmbiental
    {
        private SILPA.AccesoDatos.AdmTablasBasicas.AutoridadAmbientalDALC autoridadAmbiental;
        public AutoridadAmbiental()
        {
            autoridadAmbiental = new AutoridadAmbientalDALC();
        }

        public void Eliminar_EXT_Autoridad( int AUT_ID ){
            autoridadAmbiental.Eliminar_EXT_Autoridad(AUT_ID);
        }


        public DataSet ListarTodasAutoridadAmbiental()
        {
            return autoridadAmbiental.ListarTodasAutoridadAmbiental();
        }



        
    }
}
