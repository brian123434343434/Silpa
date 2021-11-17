using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class WSB_Metodo
    {
        private WSB_MetodoDALC objWSBMetodoDALC;
        public WSB_Metodo()
        {
            objWSBMetodoDALC = new WSB_MetodoDALC();
        }

        #region "TIPO_DATO"

        public DataTable Listar_Metodos(string strDescripcion)
        {
            return objWSBMetodoDALC.Listar_Metodos(strDescripcion);
        }

        public DataTable Cargar_Combo_Servicios()
        {
            return objWSBMetodoDALC.Cargar_Combo_Servicios();
        }

        public void Insertar_Metodos(int iServicio, string strNombreMetodo, byte bActivo)
        {
            objWSBMetodoDALC.Insertar_Metodos(iServicio, strNombreMetodo, bActivo);
        }

        public void Actualizar_Metodos(int iID, int iServicio, string strNombreMetodo, byte bActivo)
        {
            objWSBMetodoDALC.Actualizar_Metodos(iID, iServicio, strNombreMetodo, bActivo);
        }

        public void Eliminar_Metodos(int iID)
        {
            objWSBMetodoDALC.Eliminar_Metodos(iID);
        }

        #endregion

    }
}

