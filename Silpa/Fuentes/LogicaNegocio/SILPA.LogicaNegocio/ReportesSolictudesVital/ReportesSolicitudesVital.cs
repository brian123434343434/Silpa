using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.ReportesSolicitudesVital;
using System.Data;

namespace SILPA.LogicaNegocio.ReportesSolictudesVital
{
    public class ReportesSolicitudesVital
    {
        private ReportesSolicitudesVitalDalc vReportesSolicitudesVitalDalc;

        public ReportesSolicitudesVital()
        {
            vReportesSolicitudesVitalDalc = new ReportesSolicitudesVitalDalc();
        }

        public DataSet ObtenerTramites()
        {
            DataSet ds = new DataSet();
            ds = vReportesSolicitudesVitalDalc.ListarParametrizacion("ID,NOMBRE_TIPO_TRAMITE", "GEN_TIPO_TRAMITE", "ID IN (41,73,74)", "");
            return ds;
        }

        public DataSet ListarSolicitudesVital(int Tramite, string NumeroVital, DateTime FechaSolicitudDesde, DateTime FechaSolicitudHasta, int IdSolicitante, int IdFormulario, int IdAutAmb, int TipoReporte, string Departmento, string Municipio)
        {
            DataSet dsResultado = new DataSet();
            switch (TipoReporte)
            {
                case 1://consulta basica
                    dsResultado = vReportesSolicitudesVitalDalc.ListarSolicitudesVital(Tramite, NumeroVital, FechaSolicitudDesde, FechaSolicitudHasta, IdSolicitante, IdFormulario, IdAutAmb, TipoReporte,  Departmento,  Municipio);
                    break;

                case 2://detalle numero vital
                    dsResultado = vReportesSolicitudesVitalDalc.ListarSolicitudesVital(Tramite, "", DateTime.Now, DateTime.Now, 0, IdFormulario, 0, TipoReporte, Departmento, Municipio);
                    break;

                case 3: //todos con los filtros
                    dsResultado = vReportesSolicitudesVitalDalc.ListarSolicitudesVital(Tramite, NumeroVital, FechaSolicitudDesde, FechaSolicitudHasta, IdSolicitante, IdFormulario, IdAutAmb, TipoReporte,  Departmento,  Municipio);
                    break;
                default:
                    break;
            }
            return dsResultado;
        }

    }
}
