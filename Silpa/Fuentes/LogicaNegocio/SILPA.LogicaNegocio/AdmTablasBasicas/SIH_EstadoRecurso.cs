using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class SIH_EstadoRecurso
    {
        private SIH_EstadoRecursoDALC objEstadoRecursoDALC;
        public SIH_EstadoRecurso()
        {
            objEstadoRecursoDALC = new SIH_EstadoRecursoDALC();
        }

        #region "ESTADO_RECURSO"

        public DataTable Listar_Estado_Recurso(string strDescripcion)
        {
            return objEstadoRecursoDALC.Listar_Estado_Recurso(strDescripcion);
        }

        public void Insertar_Estado_Recurso(string strNombreEstado, bool bActivo)
        {
            objEstadoRecursoDALC.Insertar_Estado_Recurso(strNombreEstado, bActivo);
        }

        public void Actualizar_Estado_Recurso(int iID, string strNombreEstado, bool bActivo)
        {
            objEstadoRecursoDALC.Actualizar_Estado_Recurso(iID, strNombreEstado, bActivo);
        }

        public void Eliminar_Estado_Recurso(int iID)
        {
            objEstadoRecursoDALC.Eliminar_Estado_Recurso(iID);
        }

        #endregion

    }
}
