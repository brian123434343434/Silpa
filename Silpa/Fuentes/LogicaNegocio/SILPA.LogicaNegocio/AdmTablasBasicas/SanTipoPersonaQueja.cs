using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class SanTipoPersonaQueja
    {
        private San_Tipo_Persona_QuejaDALC objSanTipoPersonaQuejaDalc;

        public SanTipoPersonaQueja()
        {
            objSanTipoPersonaQuejaDalc = new San_Tipo_Persona_QuejaDALC();
        }

        #region
        public DataTable Listar_San_Tipo_Persona_Queja(string strDescripcion)
        {
            return objSanTipoPersonaQuejaDalc.Listar_San_Tipo_Persona_Queja(strDescripcion);
        }

        public void Insertar_San_Tipo_Persona_Queja(string strDescripcion, byte byEstado)
        {
            objSanTipoPersonaQuejaDalc.Insertar_San_Tipo_Persona_Queja(strDescripcion, byEstado);
        }

        public void Actualizar_San_Tipo_Persona_Queja(int intId, string strDescripcion, byte byEstado)
        {
            objSanTipoPersonaQuejaDalc.Actualizar_San_Tipo_Persona_Queja(intId, strDescripcion, byEstado);
        }

        public void Eliminar_San_Tipo_Persona_Queja(int intId)
        {
            objSanTipoPersonaQuejaDalc.Eliminar_San_Tipo_Persona_Queja(intId);
        }
        #endregion
    }
}
