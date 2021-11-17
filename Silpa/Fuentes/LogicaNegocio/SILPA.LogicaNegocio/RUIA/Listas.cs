using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.RUIA;
using SILPA.Comun;
using System.Data;
using System.Collections;


namespace SILPA.LogicaNegocio.RUIA
{
    public class Listas
    {
        /// <summary>
        /// Objeto de configuración del sitio, con las  variables globales
        /// </summary>
        private Configuracion objConfiguracion;

        public Listas()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Método que lista los Tipos de Falta de un RUIA
        /// </summary>
        /// <returns>Dataset con los siguientes campos [ID_TIPO_FALTA] - [NOMBRE_TIPO_FALTA]</returns>
        public DataSet ListaTipoFalta()
        {
            TipoFaltaDalc obj = new TipoFaltaDalc();
            return obj.ListaTipoFalta();
        }

        /// <summary>
        /// Método que lista las opciones de un Tipo de Sanción de RUIA
        /// </summary>
        /// <returns>Dataset con los siguientes campos [ID_OPCION_SANCION] - [NOMBRE_OPCION_SANCION]</returns>
        public DataSet ListaOpcionSancion()
        {
            OpcionSancionDalc obj = new OpcionSancionDalc();
            return obj.ListaOpcionSancion();
        }

        /// <summary>
        /// Método que lista los Tipos de Sanción de RUIA
        /// </summary>
        /// <returns>Dataset con los siguientes campos [ID_TIPO_SANCION] - [NOMBRE_TIPO_SANCION]</returns>
        public DataSet ListaTipoSancion()
        {
            TipoSancionDalc obj = new TipoSancionDalc();
            return obj.ListaTipoSancion();
        }
    }
}
