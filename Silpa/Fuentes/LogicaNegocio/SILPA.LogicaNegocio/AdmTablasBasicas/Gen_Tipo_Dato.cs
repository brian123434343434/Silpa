using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Gen_Tipo_Dato
    {
        private Gen_Tipo_DatoDALC objGen_Tipo_DatoDALC;
        public Gen_Tipo_Dato()
        {
            objGen_Tipo_DatoDALC = new Gen_Tipo_DatoDALC();
        }

        #region "GEN_TIPO_DATO"

        public DataTable Listar_Gen_Tipo_Dato(string strDescripcion)
        {
            return objGen_Tipo_DatoDALC.Listar_Gen_Tipo_Dato(strDescripcion);
        }

        public void Insertar_Gen_Tipo_Dato(string strDescripcion, byte byEstado)
        {
            objGen_Tipo_DatoDALC.Insertar_Gen_Tipo_Dato(strDescripcion, byEstado);
        }

        public void Actualizar_Gen_Tipo_Dato(int iId, string strDescripcion, byte byEstado)
        {
            objGen_Tipo_DatoDALC.Actualizar_Gen_Tipo_Dato(iId, strDescripcion, byEstado);
        }

        public void Eliminar_Gen_Tipo_Dato(int iId)
        {
            objGen_Tipo_DatoDALC.Eliminar_Gen_Tipo_Dato(iId);
        }

        #endregion
    }
}
