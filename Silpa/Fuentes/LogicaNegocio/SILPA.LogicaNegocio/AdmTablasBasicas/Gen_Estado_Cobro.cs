using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Gen_Estado_Cobro
    {
        private Gen_Estado_CobroDACL objGen_Estado_CobroDACL;
        public Gen_Estado_Cobro()
        {
            objGen_Estado_CobroDACL = new Gen_Estado_CobroDACL();
        }

        #region "GEN_ESTADO_COBRO"

        public DataTable Listar_Gen_Estado_Cobro(string strDescripcion)
        {
            return objGen_Estado_CobroDACL.Listar_Gen_Estado_Cobro(strDescripcion);
        }

        public void Insertar_Gen_Estado_Cobro(string strNombre,string strDescripcion, byte byEstado)
        {
            objGen_Estado_CobroDACL.Insertar_Gen_Estado_Cobro(strNombre, strDescripcion, byEstado);
        }

        public void Actualizar_Gen_Estado_Cobro(int iId, string strNombre, string strDescripcion, byte byEstado)
        {
            objGen_Estado_CobroDACL.Actualizar_Gen_Estado_Cobro(iId, strNombre, strDescripcion, byEstado);
        }

        public void Eliminar_Gen_Estado_Cobro(int iId)
        {
            objGen_Estado_CobroDACL.Eliminar_Gen_Estado_Cobro(iId);
        }

        #endregion
    }
}
