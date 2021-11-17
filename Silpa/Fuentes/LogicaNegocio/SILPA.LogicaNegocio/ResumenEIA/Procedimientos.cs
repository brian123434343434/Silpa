using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SILPA.LogicaNegocio.ResumenEIA
{
    public class Procedimientos
    {
        public DataSet ProcFuentesAguasVertimientos(string evtID)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcFuentesAguasVertimientos(evtID);
        }

        public DataSet ProcFuentesAguasVertimientosSuelos(string evtID)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcFuentesAguasVertimientosSuelos(evtID);
        }

        public DataSet ProcTiposTratamientosVertimientos(string evtID)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcTiposTratamientosVertimientos(evtID);
        }

        public string ProcExisteRelaciionTipoTratamiento(string edvId, object etvId)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcExisteRelaciionTipoTratamiento(edvId,etvId);
        }

        public DataSet ProcCalidadFisicoquimicas(string evtID)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcCalidadFisicoquimicas(evtID);
        }

        public DataSet ProcCalidadFuentesSuperf(string eipID)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcCalidadFuentesSuperf(eipID);
        }

        public DataSet ProcCalidadFuentesSubt(string eipID)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcCalidadFuentesSubt(eipID);
        }
        public DataSet ProcCalidadSitioAire(string eipID)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcCalidadSitioAire(eipID);
        }

        public DataSet ProcCalidadSitioRuido(string eipID)
        {
            SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos objPro = new SILPA.AccesoDatos.ResumenEIA.Generacion.Procedimientos();
            return objPro.ProcCalidadSitioRuido(eipID);
        }
        
        
    }
}
