using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Gen_Tipo_Doc_Acreditacion
    {
        private GenTipoDocAcreditacionDALC objGenTipoDocAcreditacionDALC;

        public Gen_Tipo_Doc_Acreditacion() {
            objGenTipoDocAcreditacionDALC = new GenTipoDocAcreditacionDALC();
        }

        public DataTable ListarTipoDocAcreditacion(string strNombre)
        {
            return objGenTipoDocAcreditacionDALC.ListarTipoDocAcreditacion(strNombre);
        }

        public void InsertarTipoDocAcreditacion(string strNombre)
        {
            objGenTipoDocAcreditacionDALC.InsertarTipoDocAcreditacion(strNombre);
        }

        public void ActualizarTipoDocAcreditacion(int intId, string strNombre)
        {
            objGenTipoDocAcreditacionDALC.ActualizarTipoDocAcreditacion(intId, strNombre);
        }

        public void EliminarTipoDocAcreditacion(int intId)
        {
            objGenTipoDocAcreditacionDALC.EliminarTipoTipoDocAcreditacion(intId);
        }
    }
}
