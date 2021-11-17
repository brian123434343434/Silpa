using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.ResumenEIA.Basicas;

namespace SILPA.LogicaNegocio.ResumenEIA
{
    public partial class Listas
    {
        /// <summary>
        /// Lista de Los subsectores de los sitios de monitoreo de ruido ambiental
        /// </summary>
        /// <returns>Lista de la clase SubSecSitioMonitRuidoEntity</returns>
        public static List<SubSecSitioMonitRuidoEntity> ListaSubSecMonitRuido()
        {
            SubSecSitioMonitRuidoDalc objSubSecSitioMonitRuido = new SubSecSitioMonitRuidoDalc();
            return objSubSecSitioMonitRuido.Listar();
        }
    }

}
