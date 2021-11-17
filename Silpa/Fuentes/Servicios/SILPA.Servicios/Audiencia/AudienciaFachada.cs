using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.Audiencia;
using SILPA.LogicaNegocio.Generico;
using System.Data;
using SILPA.Comun;

namespace SILPA.Servicios.Audiencia
{
    public class AudienciaFachada
    {
        public SILPA.LogicaNegocio.Audiencia.Audiencia _objAudiencia;

        public AudienciaFachada()
        {

        }

        public bool GuardarAudiencia(string xmlDatos)
        {
            try
            {
                _objAudiencia = new SILPA.LogicaNegocio.Audiencia.Audiencia();
                _objAudiencia.GuardarAudiencia(xmlDatos);
                return true;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Guardar Audiencia Pública.";
                throw new Exception(strException, ex);
            }
        }


        public string ConsultaInscritosAudiencia(string idAudiencia)
        {
            _objAudiencia = new SILPA.LogicaNegocio.Audiencia.Audiencia();

            string inscritos = _objAudiencia.ConsultarInscritosAudiencia(idAudiencia);

            return inscritos;
        }

        public bool ApruebaInscritosAudiencia(string strNumeroInscripcion, bool blnAprobado, string strMotivo)
        {
            _objAudiencia = new SILPA.LogicaNegocio.Audiencia.Audiencia();

            bool aprobacion = _objAudiencia.AprobarInscritosAudiencia(strNumeroInscripcion, blnAprobado, strMotivo);

            return aprobacion;
        }


    }
}
