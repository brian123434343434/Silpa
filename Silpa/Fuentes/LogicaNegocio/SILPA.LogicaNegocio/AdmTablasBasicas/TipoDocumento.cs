using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class TipoDocumento
    {
        private TipoDocumentoDALC objTipoDocumentoDALC;

        public TipoDocumento()
        {
            objTipoDocumentoDALC = new TipoDocumentoDALC();
        }

        public DataTable ListarTipoDocumento(int intIdPar, string strNombre)
        {
            return objTipoDocumentoDALC.ListarTipoDocumento(intIdPar, strNombre);
        }

        public void InsertarTipoDocumento(int intIdPar, bool bEstado, string strNombre, string strCodConvenio, int? intIdFlujoNotificacion, int? intCondicionEspecial)
        {
            objTipoDocumentoDALC.InsertarTipoDocumento(intIdPar, bEstado, strNombre, strCodConvenio, intIdFlujoNotificacion, intCondicionEspecial);
        }

        public void ActualizarTipoDocumento(int intId, int intIdPar, bool bEstado, string strNombre, string strCodConvenio, int? intIdFlujoNotificacion, int? intCondicionEspecial)
        {
            objTipoDocumentoDALC.ActualizarTipoDocumento(intId, intIdPar, bEstado, strNombre, strCodConvenio, intIdFlujoNotificacion, intCondicionEspecial);
        }

        public void EliminarTipoDocumento(int intId)
        {
            objTipoDocumentoDALC.EliminarTipoDocumento(intId);
        }        

    }
}

