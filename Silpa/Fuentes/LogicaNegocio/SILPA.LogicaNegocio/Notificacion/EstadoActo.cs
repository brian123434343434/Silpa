using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class EstadoActo
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Obtiene el listado de estados existentes
            /// </summary>
            /// <returns>List con la información de los estados</returns>
            public List<EstadoActoEntity> ObtenerListaEstados()
            {
                EstadoActoDalc objEstadoActoDalc = new EstadoActoDalc();
                return objEstadoActoDalc.ObtenerListaEstados();
            }
    

        #endregion


    }
}
