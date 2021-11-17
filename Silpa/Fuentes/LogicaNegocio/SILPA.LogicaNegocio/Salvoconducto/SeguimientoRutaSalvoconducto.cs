using SILPA.AccesoDatos.Salvoconducto;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.BPMProcess;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class SeguimientoRutaSalvoconducto
    {
        private SeguimientoRutaSalvocondcutoDalc vSeguimientoRutaSalvocondcutoDalc;

        public SeguimientoRutaSalvoconducto()
        {
            vSeguimientoRutaSalvocondcutoDalc = new SeguimientoRutaSalvocondcutoDalc();
        }

        public List<SeguimientoRutaSalvoconductoIdentity> ValidarSalvoconducto(string NUMERO, string CODIGO_SEGURIDAD)
        {
            return vSeguimientoRutaSalvocondcutoDalc.ValidarSalvoconductoDalc(NUMERO, CODIGO_SEGURIDAD,string.Empty);
        }

        public List<SeguimientoRutaSalvoconductoIdentity> ValidarSalvoconducto(string NUMERO, string CODIGO_SEGURIDAD, string DOCUMENTO_SOLICITANTE)
        {
            return vSeguimientoRutaSalvocondcutoDalc.ValidarSalvoconductoDalc(NUMERO, CODIGO_SEGURIDAD, DOCUMENTO_SOLICITANTE);
        }

        public DataSet ConsultaSalvoconductosEmitidos(int AUT_ID, DateTime FEC_EXP_DESDE, DateTime FEC_EXP_HASTA, int TIPO_FILTRO_VIGENCIA, string NUMERO_SALVOCNDUCTO, int DPTO_ORIGEN_ID, int MUNICIPIO_ORIGEN_ID, int DPTO_DESTINO_ID, int MUNICIPIO_DESTINO_ID, int TITULAR_ID, int TIPO_SALVOCONDUCTO_ID, int ESTADO_ID, int CLASE_RECURSO_ID, bool USUARIO_CONSULTA, bool SN_RESOLUCION_438)
        {
            return vSeguimientoRutaSalvocondcutoDalc.ConsultaSalvoconductosEmitidosDalc(AUT_ID, FEC_EXP_DESDE, FEC_EXP_HASTA, TIPO_FILTRO_VIGENCIA, NUMERO_SALVOCNDUCTO, DPTO_ORIGEN_ID, MUNICIPIO_ORIGEN_ID, DPTO_DESTINO_ID, MUNICIPIO_DESTINO_ID, TITULAR_ID, TIPO_SALVOCONDUCTO_ID, ESTADO_ID, CLASE_RECURSO_ID, USUARIO_CONSULTA, SN_RESOLUCION_438);
        }
        public DataTable ConsultaSalvoconductosEmitidosFullInfo(int AUT_ID, DateTime FEC_EXP_DESDE, DateTime FEC_EXP_HASTA, int TIPO_FILTRO_VIGENCIA, String NUMERO_SALVOCNDUCTO, int DPTO_ORIGEN_ID, int MUNICIPIO_ORIGEN_ID, int DPTO_DESTINO_ID, int MUNICIPIO_DESTINO_ID, int TITULAR_ID, int TIPO_SALVOCONDUCTO_ID, int ESTADO_ID, int CLASE_RECURSO_ID, bool USUARIO_CONSULTA, bool SN_RESOLUCION_438)
        {
            return vSeguimientoRutaSalvocondcutoDalc.ConsultaSalvoconductosEmitidosFullInfo(AUT_ID, FEC_EXP_DESDE, FEC_EXP_HASTA, TIPO_FILTRO_VIGENCIA, NUMERO_SALVOCNDUCTO, DPTO_ORIGEN_ID, MUNICIPIO_ORIGEN_ID, DPTO_DESTINO_ID, MUNICIPIO_DESTINO_ID, TITULAR_ID, TIPO_SALVOCONDUCTO_ID, ESTADO_ID, CLASE_RECURSO_ID, USUARIO_CONSULTA, SN_RESOLUCION_438);
        }
        public int GrabarLogConsulta(int SalvoconductoID, int DptoID, int MunID, string NombreRevisor, string IdentificacionRevisor, int IdAplicationUser)
        {
            return vSeguimientoRutaSalvocondcutoDalc.GrabarLogConsultaDalc(SalvoconductoID, DptoID, MunID, NombreRevisor, IdentificacionRevisor, IdAplicationUser);
        }

    }
}
