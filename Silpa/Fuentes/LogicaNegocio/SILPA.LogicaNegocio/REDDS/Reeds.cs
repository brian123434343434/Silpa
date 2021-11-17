using SILPA.AccesoDatos.REDDS;
using SILPA.LogicaNegocio.BPMProcessL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.REDDS
{
    public class Redds
    {
        public ReddsDalc vReddsDalc;
        public Redds()
        {
            vReddsDalc = new ReddsDalc();
        }
        public List<ReddsEstadoAvanceIniciativa> ConsultaListaEstadosIniciativa()
        {
            try
            {
                return vReddsDalc.ConsultaListaEstadosIniciativa();
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<ReddsActividad> ConsultaListaActividades()
        {
            try
            {
                return vReddsDalc.ConsultaListaActividades();
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<ReddsCompartimientoCarbono> ConsultaListaCompartimientoCarbono()
        {
            try
            {
                return vReddsDalc.ConsultaListaCompartimientoCarbono();
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        
        public int InsertarRedds(AccesoDatos.REDDS.Redds pRedds)
        {
            return vReddsDalc.InsertarRedds(pRedds);
        }

        public string CrearProceso(string ClientId, Int64 FormularioQueja, Int64 UsuarioQueja, string ValoresXml)
        {
            BpmProcessLn objProceso = new BpmProcessLn();
            return objProceso.crearProceso(ClientId, FormularioQueja, UsuarioQueja, ValoresXml);
        }

        public void ActualizarNumeroVital(string numeroVital, int reddsID)
        {
            vReddsDalc.ActualizarNumeroVital(numeroVital, reddsID); 
        }

        public SILPA.AccesoDatos.REDDS.Redds ConsultaRegistroREDDNumeroVital(string numeroVital, bool consultartodo, int? reddsID)
        {
            try
            {
                return vReddsDalc.ConsultaRegistroREDDNumeroVital(numeroVital, consultartodo, reddsID);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<SILPA.AccesoDatos.REDDS.ReddsLocalizacion> ConsultaLocalizaciones(int pReddsID)
        {
            try
            {
                return vReddsDalc.ConsultaLocalizaciones(pReddsID);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable ConsultaRelacionJuridica()
        {
            try
            {
                return vReddsDalc.ConsultaRelacionJuridica();
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}
