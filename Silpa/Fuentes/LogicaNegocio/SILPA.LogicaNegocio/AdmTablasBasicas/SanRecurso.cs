using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class SanRecurso
    {

        private San_RecursoDALC objSanRecursoDalc;

        public SanRecurso()
        {
            objSanRecursoDalc = new San_RecursoDALC();
        }

        #region SanRecurso
        public DataTable Listar_San_Recurso(string strDescripcion)
        {
            return objSanRecursoDalc.Listar_San_Recurso(strDescripcion);
        }

        public void Insertar_San_Recurso(string strDescripcion, byte byEstado)
        {
            objSanRecursoDalc.Insertar_San_Recurso(strDescripcion, byEstado);
        }

        public void Actualizar_San_Recurso(int intId, string strDescripcion, byte byEstado)
        {
            objSanRecursoDalc.Actualizar_San_Recurso(intId, strDescripcion, byEstado);
        }

        public void Eliminar_San_Recurso(int intId)
        {
            objSanRecursoDalc.Eliminar_San_Recurso(intId);
        }
        #endregion
    }
}
