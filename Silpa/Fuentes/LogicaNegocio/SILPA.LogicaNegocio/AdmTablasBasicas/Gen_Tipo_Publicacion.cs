using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class Gen_Tipo_Publicacion
    {
        private Gen_Tipo_PublicacionDALC objGen_Tipo_PublicacionDALC;
        public Gen_Tipo_Publicacion()
        {
            objGen_Tipo_PublicacionDALC = new Gen_Tipo_PublicacionDALC();
        }

        #region "GEN_TIPO_PUBLICACION"

        public DataTable Listar_Gen_Tipo_Publicacion(string strDescripcion)
        {
            return objGen_Tipo_PublicacionDALC.Listar_Gen_Tipo_Publicacion(strDescripcion);
        }

        public void Insertar_Gen_Tipo_Publicacion(string strDescripcion)
        {
            objGen_Tipo_PublicacionDALC.Insertar_Gen_Tipo_Publicacion(strDescripcion);
        }

        public void Actualizar_Gen_Tipo_Publicacion(int iId, string strDescripcion)
        {
            objGen_Tipo_PublicacionDALC.Actualizar_Gen_Tipo_Publicacion(iId, strDescripcion);
        }

        public void Eliminar_Gen_Tipo_Publicacion(int iId)
        {
            objGen_Tipo_PublicacionDALC.Eliminar_Gen_Tipo_Publicacion(iId);
        }

        #endregion
    }
}
