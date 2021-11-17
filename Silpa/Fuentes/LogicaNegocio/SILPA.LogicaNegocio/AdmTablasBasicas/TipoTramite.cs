using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class TipoTramite
    {
        private TipoTramiteDALC objTipoTramiteDALC;

        public TipoTramite()
        {
            objTipoTramiteDALC = new TipoTramiteDALC();
        }

        public DataTable ListarTipoTramite(int intIdPar, string strNombre)
        {
            return objTipoTramiteDALC.ListarTipoTramite(intIdPar, strNombre);
        }

        public DataTable ListarCondicionesEspeciales(int? condicion, string codigoCondicion, int? tipoCondicion)
        {
            return objTipoTramiteDALC.ListaCondicionesEspeciales(condicion, codigoCondicion, tipoCondicion);
        }

        public void CrearCondicionesEspeciales(int? condicion, string codigoCondicion, int? tipoCondicion)
        {
            objTipoTramiteDALC.CrearCondicionesEspeciales(condicion, codigoCondicion, tipoCondicion);
        }
        public void ActualizarCondicionesEspeciales(int? condicion, string codigoCondicion, int? tipoCondicion)
        {
            objTipoTramiteDALC.ActualizarCondicionesEspeciales(condicion, codigoCondicion, tipoCondicion);
        }

        public void EliminarCondicionesEspeciales( int? condicion)
        {
            objTipoTramiteDALC.EliminarCondicionesEspeciales( condicion);
        }

        public void InsertarTipoTramite(int intIdPar, string strNombre, bool mostrarDocumentos)
        {
            objTipoTramiteDALC.InsertarTipoTramite(intIdPar, strNombre, mostrarDocumentos);
        }

        public void ActualizarTipoTramite(int intId, int intIdPar, string strNombre,bool bVisible, bool mostrarDocumentos)
        {
            objTipoTramiteDALC.ActualizarTipoTramite(intId, intIdPar, strNombre,bVisible, mostrarDocumentos);
        }

        public void EliminarTipoTramite(int intId)
        {
            objTipoTramiteDALC.EliminarTipoTramite(intId);
        }
     
    }
}

