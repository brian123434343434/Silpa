using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Geb_Tipo_Persona
    {

        private Geb_Tipo_PersonaDALC objGeb_Tipo_PersonaDALC;
        public Geb_Tipo_Persona()
        {
            objGeb_Tipo_PersonaDALC = new Geb_Tipo_PersonaDALC();
        }

        #region "GEB_TIPO_PERSONA"

        public DataTable Listar_Geb_Tipo_Persona(string strDescripcion)
        {
            return objGeb_Tipo_PersonaDALC.Listar_Geb_Tipo_Persona(strDescripcion);
        }

        public void Insertar_Geb_Tipo_Persona(string strDescripcion,decimal deEstado)
        {
            objGeb_Tipo_PersonaDALC.Insertar_Geb_Tipo_Persona(strDescripcion, deEstado);
        }

        public void Actualizar_Geb_Tipo_Persona(decimal iId, string strDescripcion, decimal deEstado)
        {
            objGeb_Tipo_PersonaDALC.Actualizar_Geb_Tipo_Persona(iId, strDescripcion, deEstado);
        }

        public void Eliminar_Geb_Tipo_Persona(decimal iId)
        {
            objGeb_Tipo_PersonaDALC.Eliminar_Geb_Tipo_Persona(iId);
        }

        #endregion

        

    }   
}
