using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Gen_Concepto
    {
        private Gen_ConceptoDALC objGen_ConceptoDALC;
        public Gen_Concepto()
        {
            objGen_ConceptoDALC = new Gen_ConceptoDALC();
        }

        #region "GEB_CONCEPTO"

        public DataTable Listar_Gen_Concepto(string strDescripcion)
        {
            return objGen_ConceptoDALC.Listar_Gen_Concepto(strDescripcion);
        }

        public void Insertar_Gen_Concepto(string strDescripcion, byte byEstado)
        {
            objGen_ConceptoDALC.Insertar_Gen_Concepto(strDescripcion, byEstado);
        }

        public void Actualizar_Gen_Concepto(int iId, string strDescripcion, byte byEstado)
        {
            objGen_ConceptoDALC.Actualizar_Gen_Concepto(iId, strDescripcion, byEstado);
        }

        public void Eliminar_Gen_Concepto(int iId)
        {
            objGen_ConceptoDALC.Eliminar_Gen_Concepto(iId);
        }

        #endregion
    }
}
