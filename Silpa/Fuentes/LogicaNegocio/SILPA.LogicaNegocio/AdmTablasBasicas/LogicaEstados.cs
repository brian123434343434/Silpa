using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class LogicaEstados
    {
        private LogicaEstadosDALC  objLogicaEstadosDALC;
        public LogicaEstados()
        {
            objLogicaEstadosDALC = new LogicaEstadosDALC();
        }

        #region "ESTADO_CORREO"

        public DataTable Listar_Logica_Estados(string strDescripcion)
        {
            return objLogicaEstadosDALC.Listar_Logica_Estados(strDescripcion);
        }

        public DataTable Cargar_Combo_Servidor()
        {
            return objLogicaEstadosDALC.Carga_Combo_Estados(); 
        }

        public void Insertar_Logica_Estados(bool boPertenece, bool boRequiere, bool boConflicto, bool boFijos, Int32 iEstado)
        {
            objLogicaEstadosDALC.Insertar_Logica_Estados(boPertenece, boRequiere, boConflicto, boFijos, iEstado);
        }

        public void Actualizar_Logica_Estados(int iID, bool boPertenece, bool boRequiere, bool boConflicto, bool boFijos, Int32 iEstado)
        {
            objLogicaEstadosDALC.Actualizar_Logica_Estados(iID, boPertenece, boRequiere, boConflicto, boFijos, iEstado);
        }

        public void Eliminar_Logica_Estados(int iID)
        {
            objLogicaEstadosDALC.Eliminar_Logica_Estados(iID);
        }

        #endregion

    }
}
