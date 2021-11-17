using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.AdmTablasBasicas;

namespace SILPA.LogicaNegocio.AdmTablasBasicas
{
    public class TablasBasicas
    {
        private TablasBasicasDalc objTablasBasicasDalc;

        public TablasBasicas()
        {
            objTablasBasicasDalc = new TablasBasicasDalc();
        }

        #region "BPM_PARAMETROS"

        public DataTable Listar_Bpm_Parametros(string strNombre)
        {
            return objTablasBasicasDalc.Listar_Bpm_Parametros(strNombre);
        }

        public DataTable Listar_Tipos_Parametros()
        {
            return objTablasBasicasDalc.Listar_Tipos_Parametros();
        }

        public void Insertar_Bpm_Parametros(int iTipo, string strNombre, int iCodigo)
        {
            objTablasBasicasDalc.Insertar_Bpm_Parametros(iTipo, strNombre, iCodigo);
        }

        public void Actualizar_Bpm_Parametros(int iID, int iTipo, string strNombre, int iCodigo)
        {
            objTablasBasicasDalc.Actualizar_Bpm_Parametros(iID, iTipo, strNombre, iCodigo);
        }

        public void Eliminar_Bpm_Parametros(int iID)
        {
            objTablasBasicasDalc.Eliminar_Bpm_Parametros(iID);
        }

        #endregion

        #region "DOC_TIPO_ADQUISICION"

        public DataTable Listar_doc_tipo_adquisicion(string strDescripcion)
        {
            return objTablasBasicasDalc.Listar_doc_tipo_adquisicion(strDescripcion);
        }

        public void Insertar_doc_tipo_adquisicion(string strDescripcion)
        {
            objTablasBasicasDalc.Insertar_doc_tipo_adquisicion(strDescripcion);
        }

        public void Actualizar_doc_tipo_adquisicion(int iId,string strDescripcion)
        {
            objTablasBasicasDalc.Actualizar_doc_tipo_adquisicion(iId, strDescripcion);
        }

        public void Eliminar_doc_tipo_adquisicion(int iId)
        {
            objTablasBasicasDalc.Eliminar_doc_tipo_adquisicion(iId);
        }

        #endregion

        #region "TABLAS BASICAS"
        public DataTable Listar_Tablas_Basicas(string strDescripcion)
        {
            return objTablasBasicasDalc.Listar_Tablas_Basicas(strDescripcion);
        }
        #endregion
    }
}
