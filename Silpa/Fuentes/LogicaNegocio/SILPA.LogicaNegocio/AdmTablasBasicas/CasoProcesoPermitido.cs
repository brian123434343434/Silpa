using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class CasoProcesoPermitido
    {
        private CasoProcesoPermitidoDALC objCasoProcesoPermitidoDALC;

        public CasoProcesoPermitido()
        {
            objCasoProcesoPermitidoDALC = new CasoProcesoPermitidoDALC();
        }

        public DataTable ListarCasoProcesoPermitido(string strNombre, string strfechaIni, string strFechaFin)
        {
            return objCasoProcesoPermitidoDALC.ListarCasoProcesoPermitido(strNombre, strfechaIni, strFechaFin);
        }

        public DataTable CargarCboCasoProcesoPermitido(int iIdCaso)
        {
            return objCasoProcesoPermitidoDALC.CargarCboCasoProcesoPermitido(iIdCaso);
        }

        public void InsertarCasoProcesoPermitido(int iIdCaso, string strFecha, bool bEstado, string strNombre, bool bTipoEntidad)
        {
            objCasoProcesoPermitidoDALC.InsertarCasoProcesoPermitido(iIdCaso, strFecha, bEstado, strNombre, bTipoEntidad);
        }

        public void ActualizarCasoProcesoPermitido(int intId, int iIdCaso, string strFecha, bool bEstado, string strNombre, bool bTipoEntidad)
        {
            objCasoProcesoPermitidoDALC.ActualizarCasoProcesoPermitido(intId, iIdCaso, strFecha, bEstado, strNombre, bTipoEntidad);
        }

        public void EliminarCasoProcesoPermitido(int intId)
        {
            objCasoProcesoPermitidoDALC.EliminarCasoProcesoPermitido(intId);
        }
    }
}
