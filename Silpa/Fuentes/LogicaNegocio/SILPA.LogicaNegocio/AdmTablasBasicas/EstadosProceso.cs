using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class EstadosProceso
    {
        private EstadosProcesoDALC  objEstadosProcesoDALC;
        public EstadosProceso()
        {
            objEstadosProcesoDALC = new EstadosProcesoDALC();
        }

        #region "ESTADOS_PROCESO"

        public DataTable Listar_Estados_Proceso(string strDescripcion)
        {
            return objEstadosProcesoDALC.Listar_Estados_Proceso(strDescripcion);
        }

        public void Insertar_Estados_Proceso(string strNombreEstado, string strDescripcion, byte byActivo)
        {
            objEstadosProcesoDALC.Insertar_Estados_Proceso(strNombreEstado, strDescripcion, byActivo);
        }

        public void Actualizar_Estados_Proceso(int iID, string strNombreEstado, string strDescripcion, byte byActivo)
        {
            objEstadosProcesoDALC.Actualizar_Estados_Proceso(iID, strNombreEstado, strDescripcion, byActivo);
        }

        public void Eliminar_Estados_Proceso(int iID)
        {
            objEstadosProcesoDALC.Eliminar_Estados_Proceso(iID);
        }

        #endregion

    }
}
