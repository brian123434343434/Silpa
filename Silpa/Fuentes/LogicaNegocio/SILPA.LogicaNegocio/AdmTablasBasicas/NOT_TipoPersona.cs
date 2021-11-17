using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class NOT_TipoPersona
    {
        private NOT_TipoPersonaDALC objTipoPersonaDALC;
        public NOT_TipoPersona()
        {
            objTipoPersonaDALC = new NOT_TipoPersonaDALC();
        }

        #region "TIPO_DATO"

        public DataTable Listar_Tipo_Persona(string strDescripcion)
        {
            return objTipoPersonaDALC.Listar_Tipo_Personas(strDescripcion);
        }

        public void Insertar_Tipo_Persona(string strCodigoTipo, string strNombreTipo)
        {
            objTipoPersonaDALC.Insertar_Tipo_Personas(strCodigoTipo, strNombreTipo);
        }

        public void Actualizar_Tipo_Persona(int iID, string strCodigoTipo, string strNombreTipo)
        {
            objTipoPersonaDALC.Actualizar_Tipo_Personas(iID, strCodigoTipo, strNombreTipo);
        }

        public void Eliminar_Tipo_Persona(int iID)
        {
            objTipoPersonaDALC.Eliminar_Tipo_Personas(iID);
        }

        #endregion

    }
}
